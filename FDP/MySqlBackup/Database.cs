// The authors disclaims copyright of this project. Use at your own risk.
// For bugs report, feature request, discussions, supports, please visit:
// http://mysqlbackupnet.codeplex.com/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MySql.Data.MySqlClient
{
    public class Database
    {
        private long _totalRows = -1;
        private string _createDatabaseSql = "";
        private string _databaseName = "";
        private string _serverVersionNo = "";
        private string _serverVersion = "";
        private double _ServerMajorVersion = 0;
        private string _defaultDatabaseCharSet = "";

        /// <summary>
        /// Gets the SQL Statement of CREATE DATABASE of this database.
        /// </summary>
        public string CreateDatabaseSql { get { return _createDatabaseSql; } }

        /// <summary>
        /// Gets the Name of this Database.
        /// </summary>
        public string DatabaseName { get { return _databaseName; } }

        /// <summary>
        /// Gets all the Tables' information. Key = Table name; Value = Table.
        /// </summary>
        public Dictionary<string, Table> Tables { get; set; }

        /// <summary>
        /// Gets the MySQL Server Version's number and name.
        /// </summary>
        public string ServerVersion { get { return _serverVersion; } }

        /// <summary>
        /// Gets the MySQL Server Version's number.
        /// </summary>
        public string ServerVersionNo { get { return _serverVersionNo; } }

        /// <summary>
        /// Gets the MSQL Server Major Version's number.
        /// </summary>
        public double ServerMajorVersion { get { return _ServerMajorVersion; } }

        /// <summary>
        /// Gets the default character set of current database.
        /// </summary>
        public string DefaultDatabaseCharSet { get { return _defaultDatabaseCharSet; } }

        /// <summary>
        /// Gets all table's name.
        /// </summary>
        public string[] TableNames
        {
            get
            {
                string[] sa = new string[Tables.Count];
                int count = -1;
                foreach (KeyValuePair<string, Table> kv in Tables)
                {
                    count++;
                    sa[count] = kv.Key;
                }
                return sa;
            }
        }

        /// <summary>
        /// Gets or Sets Stored Procedures of current database.
        /// </summary>
        public Dictionary<string, string> StoredProcedure { get; set; }

        /// <summary>
        /// Gets or Sets Stored Functions of current database.
        /// </summary>
        public Dictionary<string, string> StoredFunction { get; set; }

        /// <summary>
        /// Gets or Sets Stored Triggers of current database;
        /// </summary>
        public Dictionary<string, string> StoredTrigger { get; set; }

        /// <summary>
        /// Gets or Sets Stored Events of current database.
        /// </summary>
        public Dictionary<string, string> StoredEvents { get; set; }

        /// <summary>
        /// Gets or Sets Stored Views of current database.
        /// </summary>
        public Dictionary<string, string> StoredView { get; set; }

        public delegate void calculateTotalRowsProgressChange(object sender, int percentageCompleted);

        /// <summary>
        /// Occur when total rows calculation of 1 table has completed.
        /// </summary>
        public event calculateTotalRowsProgressChange CalculateTotalRowsProgressChanged;

        public delegate void calculateTotalRowsProgressComplete(object sender, long totalRows);

        /// <summary>
        /// Occur when total rows calcualtion of all table has completed.
        /// </summary>
        public event calculateTotalRowsProgressComplete CalculateTotalRowsCompleted;

        /// <summary>
        /// Informations about current database.
        /// </summary>
        /// <param name="cmd"></param>
        public Database(ref MySqlCommand cmd)
        {
            StoredEvents = new Dictionary<string, string>();
            StoredFunction = new Dictionary<string, string>();
            StoredProcedure = new Dictionary<string, string>();
            StoredTrigger = new Dictionary<string, string>();
            StoredView = new Dictionary<string, string>();
            Tables = new Dictionary<string, Table>();

            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();

            cmd.CommandText = "SELECT DATABASE();";
            _databaseName = cmd.ExecuteScalar().ToString();

            // No database is initialized, abondon info retrieval of database
            if (_databaseName == "")
                return;

            Methods md = new Methods();
            cmd.CommandText = "SHOW CREATE DATABASE " + DatabaseName + ";";
            MySqlDataAdapter daDB = new MySqlDataAdapter(cmd);
            DataTable dtDB = new DataTable();
            daDB.Fill(dtDB);
            daDB = null;

            _createDatabaseSql = dtDB.Rows[0][1].ToString().Replace("CREATE DATABASE", "CREATE DATABASE IF NOT EXISTS") + ";";

            _serverVersion = md.GetServerVersion(ref cmd, ref _serverVersionNo);

            GetTables(ref cmd);

            //cmd.CommandText = "SHOW VARIABLES LIKE 'character_set_server';";
            cmd.CommandText = "SHOW VARIABLES LIKE 'character_set_database';";
            MySqlDataAdapter daDBC = new MySqlDataAdapter(cmd);
            DataTable dtDBC = new DataTable();
            daDBC.Fill(dtDBC);
            daDBC = null;

            _defaultDatabaseCharSet = dtDBC.Rows[0][1].ToString();

            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();

            //string aaa = cmd.Connection.ServerVersion;

            string[] vsa = ServerVersionNo.Split('.');
            string v = "";
            if (vsa.Length > 1)
                v = vsa[0] + "." + vsa[1];
            else
                v = vsa[0];
            double.TryParse(v, out _ServerMajorVersion);

            GetRoutines(ref cmd, "PROCEDURE");
            GetRoutines(ref cmd, "FUNCTION");
            GetTriggers(ref cmd);
            GetViews(ref cmd);
            GetEvents(ref cmd);

            md = null;
        }

        private void GetEvents(ref MySqlCommand cmd)
        {
            try
            {
                if (ServerMajorVersion >= 5.1)
                {
                    // Edited by Niemand
                    //cmd.commandText = "SHOW EVENTS WHERE Db LIKE '" + DatabaseName + "';";
                    cmd.CommandText = "SHOW EVENTS WHERE UPPER(TRIM(Db))=UPPER(TRIM('" + DatabaseName + "'));";
                    DataTable dtEvents = new DataTable();
                    MySqlDataAdapter daEvents = new MySqlDataAdapter(cmd);
                    daEvents.Fill(dtEvents);
                    daEvents = null;

                    foreach (DataRow dr in dtEvents.Rows)
                    {
                        cmd.CommandText = "SHOW CREATE EVENT `" + dr[1].ToString() + "`;";
                        MySqlDataAdapter daEventCur = new MySqlDataAdapter(cmd);
                        DataTable dtEventCur = new DataTable();
                        daEventCur.Fill(dtEventCur);
                        daEventCur = null;

                        string createSql = "";
                        object ob = dtEventCur.Rows[0][3];

                        if (ob is System.String)
                        {
                            createSql = ob + "";
                        }
                        // It has been reported that, in some unknown cases, the query will return
                        // byte array.
                        // Report: http://www.codeproject.com/Messages/4450086/Small-changes-in-Code.aspx
                        else if (ob is System.Byte[])
                        {
                            UTF8Encoding enc = new UTF8Encoding();
                            createSql = enc.GetString((byte[])ob);
                        }

                        createSql = createSql.Replace("\r\n", "^^^^").Replace("\n", "\r\n").Replace("^^^^", "\r\n");

                        StoredEvents.Add(dr[1].ToString(), createSql);
                    }
                }
            }
            catch
            {
                // Purposely do nothing.
                // Trap exception that raised due to restriction of user privilege
                // The MySQL User used in this connection is not allowed to access Events

                // Clear and Reset Collection
                StoredEvents = new Dictionary<string, string>();
            }
        }

        private void GetViews(ref MySqlCommand cmd)
        {
            try
            {
                if (ServerMajorVersion >= 5)
                {
                    cmd.CommandText = "SHOW FULL TABLES FROM " + DatabaseName + " WHERE Table_type like 'VIEW';";
                    MySqlDataAdapter daView = new MySqlDataAdapter(cmd);
                    DataTable dtView = new DataTable();
                    daView.Fill(dtView);
                    daView = null;

                    foreach (DataRow dr in dtView.Rows)
                    {
                        cmd.CommandText = "SHOW CREATE VIEW `" + dr[0].ToString() + "`;";
                        MySqlDataAdapter daViewCur = new MySqlDataAdapter(cmd);
                        DataTable dtCurView = new DataTable();
                        daViewCur.Fill(dtCurView);
                        daViewCur = null;

                        string createSql = "";
                        object ob = dtCurView.Rows[0][1];

                        if (ob is System.String)
                        {
                            createSql = ob + "";
                        }
                        // It has been reported that, in some unknown cases, the query will return
                        // byte array.
                        // Report: http://www.codeproject.com/Messages/4450086/Small-changes-in-Code.aspx
                        else if (ob is System.Byte[])
                        {
                            UTF8Encoding enc = new UTF8Encoding();
                            createSql = enc.GetString((byte[])ob);
                        }

                        StoredView.Add(dr[0].ToString(), createSql.Replace("\r\n", "^^^^").Replace("\n", "\r\n").Replace("^^^^", "\r\n") + ";");
                    }
                }
            }
            catch
            {
                // Purposely do nothing.
                // Trap exception that raised due to restriction of user privilege
                // The MySQL User used in this connection is not allowed to access Views

                // Clear and Reset Collection
                StoredView = new Dictionary<string, string>();
            }
        }

        private void GetTriggers(ref MySqlCommand cmd)
        {
            try
            {
                if (ServerMajorVersion >= 5)
                {
                    cmd.CommandText = "SHOW TRIGGERS;";
                    DataTable dtTriggers = new DataTable();
                    MySqlDataAdapter daTriggers = new MySqlDataAdapter(cmd);
                    daTriggers.Fill(dtTriggers);
                    daTriggers = null;

                    foreach (DataRow dr in dtTriggers.Rows)
                    {
                        DataTable dtTriggerCur = new DataTable();
                        cmd.CommandText = "SHOW CREATE TRIGGER `" + dr[0] + "`;";
                        MySqlDataAdapter daTriggerCur = new MySqlDataAdapter(cmd);
                        daTriggerCur.Fill(dtTriggerCur);
                        daTriggerCur = null;

                        string createSql = "";
                        object ob = dtTriggerCur.Rows[0][2];

                        if (ob is System.String)
                        {
                            createSql = ob + "";
                        }
                        // It has been reported that, in some unknown cases, the query will return
                        // byte array.
                        // Report: http://www.codeproject.com/Messages/4450086/Small-changes-in-Code.aspx
                        else if (ob is System.Byte[])
                        {
                            UTF8Encoding enc = new UTF8Encoding();
                            createSql = enc.GetString((byte[])ob);
                        }

                        string cr = createSql.Replace("\r\n", "^^^^").Replace("\n", "\r\n").Replace("^^^^", "\r\n");
                        StoredTrigger.Add(dr[0].ToString(), cr);
                    }
                }
            }
            catch
            {
                // Purposely do nothing.
                // Trap exception that raised due to restriction of user privilege
                // The MySQL User used in this connection is not allowed to get info about Triggers

                // Clear and Reset Collection
                StoredTrigger = new Dictionary<string, string>();
            }
        }

        private void GetRoutines(ref MySqlCommand cmd, string routine)
        {
            try
            {
                if (ServerMajorVersion >= 5)
                {
                    DataTable dtR = new DataTable();

                    // Edited by Niemand
                    //cmd.CommandText = "SHOW " + routine + " STATUS WHERE Db LIKE '" + DatabaseName + "';";
                    cmd.CommandText = "SHOW " + routine + " STATUS WHERE UPPER(TRIM(Db))=UPPER(TRIM('" + DatabaseName + "'));";
                    
                    MySqlDataAdapter daR = new MySqlDataAdapter(cmd);
                    daR.Fill(dtR);
                    daR = null;

                    foreach (DataRow dr in dtR.Rows)
                    {
                        cmd.CommandText = "SHOW CREATE " + routine + " `" + dr[1] + "`;";
                        DataTable dtP = new DataTable();
                        MySqlDataAdapter daP = new MySqlDataAdapter(cmd);
                        daP.Fill(dtP);
                        daP = null;
                        if (dtP.Rows.Count != 0)
                        {
                            string createSql = "";
                            object ob = dtP.Rows[0][2];

                            if (ob is System.String)
                            {
                                createSql = ob + "";
                            }
                            // It has been reported that, in some unknown cases, the query will return
                            // byte array.
                            // Report: http://www.codeproject.com/Messages/4450086/Small-changes-in-Code.aspx
                            else if (ob is System.Byte[])
                            {
                                UTF8Encoding enc = new UTF8Encoding();
                                createSql = enc.GetString((byte[])ob);
                            }

                            string cr = createSql.Replace("\r\n", "^^^^").Replace("\n", "\r\n").Replace("^^^^", "\r\n");
                            if (routine == "PROCEDURE")
                                StoredProcedure.Add(dr[1].ToString(), cr);
                            else if (routine == "FUNCTION")
                                StoredFunction.Add(dr[1].ToString(), cr);
                        }
                    }
                }
            }
            catch
            {
                // Purposely do nothing.
                // Trap exception that raised due to restriction of user privilege
                // The MySQL User used in this connection is not allowed to access Functions or Stored Procedures

                // Clear and Reset Collection
                if (routine == "PROCEDURE")
                {
                    StoredProcedure = new Dictionary<string, string>();
                }
                else if (routine == "FUNCTION")
                {
                    StoredFunction = new Dictionary<string, string>();
                }
            }
        }

        /// <summary>
        /// Gets the total rows in all tables of current database.
        /// </summary>
        /// <param name="cmd">Instance of MySqlCommand</param>
        /// <returns></returns>
        public long GetTotalRows(ref MySqlCommand cmd)
        {
            if (_totalRows <1)
            {
                int count = 0;
               foreach(KeyValuePair<string, Table> kv in Tables)
               {
                   count++;
                   kv.Value.GetTotalRows(ref cmd);
                   _totalRows += kv.Value.TotalRows;
                    if (CalculateTotalRowsProgressChanged != null)
                    {
                        CalculateTotalRowsProgressChanged(this, ((count / Tables.Count) * 100));
                    }
                }
            }
            if (CalculateTotalRowsCompleted != null)
            {
                CalculateTotalRowsCompleted(this, _totalRows);
            }
            return _totalRows;
        }

        private void GetTables(ref MySqlCommand cmd)
        {
            DataTable dTables = new DataTable();

            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();

            cmd.CommandText = "SHOW FULL TABLES WHERE Table_type LIKE 'BASE TABLE';";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dTables);
            cmd.Connection.Close();

            Tables = new Dictionary<string, Table>();
            foreach (DataRow dr in dTables.Rows)
            {
                string name = dr[0].ToString();
                Tables.Add(name, new Table(name, ref cmd));
            }
        }
    }
}
