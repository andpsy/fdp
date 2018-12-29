// The authors disclaims copyright of this project. Use at your own risk.
// For bugs report, feature request, discussions, supports, please visit:
// http://mysqlbackupnet.codeplex.com/
//
// In order for this class library to work, 
// the following items are needed to add into your project.
//
// Required Dependencies:
// 1. MySql.Data.DLL (http://www.mysql.com)
// 2. DotNetZip (http://dotnetzip.codeplex.com/)

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Globalization;
using System.ComponentModel;
using System.Timers;
using Ionic.Zip;

namespace MySql.Data.MySqlClient
{
    /// <summary>
    /// Backup and Restore of MySQL database.
    /// </summary>
    public class MySqlBackup : IDisposable
    {
        /// <summary>
        /// Release Verson of MySqlBackup.NET
        /// </summary>
        public const string Version = "1.5.7 beta";

        #region Private Field / Variables

        MySqlConnection _conn = new MySqlConnection();
        MySqlCommand _cmd = new MySqlCommand();
        ExportInformations _exportInfo = new ExportInformations();
        ImportInformations _importInfo = new ImportInformations();
        Database _database = null;

        Methods methods;
        NumberFormatInfo nf;
        DateTimeFormatInfo df;
        Encoding utf8WithoutBOM;
        TextReader textReader;
        TextWriter textWriter;

        bool cancelProcess = false;

        BackgroundWorker bwExport;
        BackgroundWorker bwImport;

        string _delimeter = "|";

        #endregion

        #region Events

        ImportCompleteArg importCompleteArg;
        ImportProgressArg importProgressArg;
        ExportCompleteArg exportCompleteArg;
        ExportProgressArg exportProgressArg;

        public delegate void importComplete(object sender, ImportCompleteArg e);

        /// <summary>
        /// Occur when Import process is finished.
        /// </summary>
        public event importComplete ImportCompleted;

        public delegate void importProgressChange(object sender, ImportProgressArg e);

        /// <summary>
        /// Occur when a line in the dump file is imported.
        /// </summary>
        public event importProgressChange ImportProgressChanged;

        public delegate void exportComplete(object sender, ExportCompleteArg e);

        /// <summary>
        /// Occur when Export processs is finished.
        /// </summary>
        public event exportComplete ExportCompleted;


        public delegate void exportProgressChange(object sender, ExportProgressArg e);

        /// <summary>
        /// Occur when a row of data is exported or calculation of total rows of a table is completed.
        /// </summary>
        public event exportProgressChange ExportProgressChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the infomations about the connected database.
        /// </summary>
        public Database DatabaseInfo
        {
            get
            {
                return _database;
            }
        }

        /// <summary>
        /// Gets or Sets the MySqlConnection that used by this instance.
        /// </summary>
        public MySqlConnection Connection
        {
            get
            {
                return _conn;
            }
            set
            {
                _conn = value;
                _cmd = new MySqlCommand();
                _cmd.Connection = _conn;
                _database = new Database(ref _cmd);
            }
        }

        /// <summary>
        /// Gets or Sets the Informations that define behaviour of Export Process.
        /// </summary>
        public ExportInformations ExportInfo
        {
            get
            {
                return _exportInfo;
            }
            set
            {
                if (value == null)
                    _exportInfo = new ExportInformations();
                else
                    _exportInfo = value;
            }
        }

        /// <summary>
        /// Gets or Sets the Informations that define behaviour of Import Process.
        /// </summary>
        public ImportInformations ImportInfo
        {
            get
            {
                return _importInfo;
            }
            set
            {
                if (value == null)
                    _importInfo = new ImportInformations();
                else
                    _importInfo = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Backup and Restore MySQL database.
        /// </summary>
        public MySqlBackup()
        {
            InitializeInternalComponent();
        }

        /// <summary>
        /// Backup and Restore MySQL database.
        /// </summary>
        /// <param name="ConnectionString">Sets th MySQL connection parameters.</param>
        public MySqlBackup(string ConnectionString)
        {
            Connection = new MySqlConnection(ConnectionString);
            InitializeInternalComponent();
        }

        /// <summary>
        /// Backup and Restore MySQL database.
        /// </summary>
        /// <param name="Connection">Sets the MySqlConnection used by this instance.</param>
        public MySqlBackup(MySqlConnection connection)
        {
            Connection = connection;
            InitializeInternalComponent();
        }

        /// <summary>
        /// Backup and Restore MySQL database.
        /// </summary>
        /// <param name="Command">Sets the MySqlCommand used by this instance.</param>
        public MySqlBackup(MySqlCommand Command)
        {
            _conn = Command.Connection;
            _cmd = Command;
            _database = new Database(ref _cmd);
            InitializeInternalComponent();
        }

        #endregion

        private void InitializeInternalComponent()
        {
            nf = new NumberFormatInfo();
            nf.NumberDecimalSeparator = ".";
            nf.NumberGroupSeparator = string.Empty;
            methods = new Methods();
            utf8WithoutBOM = new UTF8Encoding(false);
            df = new DateTimeFormatInfo();
            df.DateSeparator = "-";
            df.TimeSeparator = ":";
        }

        #region Export / Backup

        /// <summary>
        /// Execute the export (backup) process. Sets the ExportInfo used by this export process.
        /// </summary>
        /// <param name="exportInfo">Sets the Informations that define behaviour of Export Process.</param>
        public void Export(ExportInformations exportInfo)
        {
            ExportInfo = exportInfo;
            Export();
        }

        /// <summary>
        /// Execute the export (backup) process.
        /// </summary>
        public void Export()
        {
            if (_exportInfo.AsynchronousMode)
            {
                bwExport = new BackgroundWorker();
                bwExport.DoWork += new DoWorkEventHandler(bwExport_DoWork);
                bwExport.RunWorkerAsync();
            }
            else
            {
                ExportExecute();
            }
        }

        void bwExport_DoWork(object sender, DoWorkEventArgs e)
        {
            ExportExecute();
        }

        private void ExportExecute()
        {
            try
            {
                using (textWriter = new StreamWriter(_exportInfo.FileName, false, utf8WithoutBOM))
                {
                    ExportStart();
                }
            }
            catch (Exception ex)
            {
                exportCompleteArg.Error = ex;
                exportCompleteArg.CompletedType = ExportCompleteArg.CompleteType.Error;
                exportCompleteArg.TimeEnd = DateTime.Now;
                _exportInfo.CompleteArg = exportCompleteArg;
                Dispose(_exportInfo.AutoCloseConnection);

                if (!_exportInfo.AsynchronousMode)
                {
                    throw ex;
                }
            }

            Dispose(_exportInfo.AutoCloseConnection);

            exportCompleteArg.TimeEnd = DateTime.Now;
            _exportInfo.CompleteArg = exportCompleteArg;

            if (ExportProgressChanged != null)
            {
                ExportProgressChanged(this, exportProgressArg);
            }
            if (ExportCompleted != null)
            {
                ExportCompleted(this, exportCompleteArg);
            }
        }

        private void ExportStart()
        {
            cancelProcess = false;

            InitializeInternalComponent();

            exportProgressArg = new ExportProgressArg();
            exportCompleteArg = new ExportCompleteArg();
            exportCompleteArg.TimeStart = DateTime.Now;
            exportCompleteArg.Error = null;

            Dictionary<string, long> dicTableName_TotalRows = new Dictionary<string, long>();

            #region Check the connection is initialized or not
            if (_conn == null)
            {
                throw new Exception("Connection has disposed. Set ExportSettings.AutoCloseConnection to false if you want to reuse this instance.");
            }

            // If the connection is not opened, open it.
            if (_conn.State != ConnectionState.Open)
                _conn.Open();

            // Check if any database is selected to be exported.
            _cmd.CommandText = "SELECT DATABASE();";
            if ((_cmd.ExecuteScalar() + "").Length == 0)
            {
                throw new Exception("No database is selected or initialized for exporting.");
            }
            #endregion

            Dictionary<string, string> _dicTableCustomSql = null;

            // Current value of max_allowed_packet will store here
            // This value will be restored at the end of this process
            string originalMaxAllowedPacket = "";

            #region Set max_allowed_packet to Maximum (1GB)
            try
            {
                _cmd.CommandText = "SHOW GLOBAL VARIABLES LIKE 'max_allowed_packet';";
                MySqlDataAdapter da = new MySqlDataAdapter(_cmd);
                DataTable dtMAP = new DataTable();
                da.Fill(dtMAP);
                originalMaxAllowedPacket = dtMAP.Rows[0]["Value"] + "";
                da = null;
                dtMAP = null;

                // 1GB, MySQL Maximum Length allowed in single SQL Query.
                _cmd.CommandText = "SET GLOBAL max_allowed_packet=1*1024*1024*1024;";
                _cmd.ExecuteNonQuery();

                // Modified max_allowed_packet will only take effect for new connection.
                // Therefore the connection has to be closed and reopen.
                _cmd.Connection.Close();
                _cmd.Connection.Open();
            }
            catch
            {
                // Purposely do nothing
                // Error will be trapped if the MySQL User used in this connection
                // does not has the privilege to modify GLOBAL Variables.
            }
            #endregion

            #region Initialize Value for Progress Report

            if (_exportInfo.TableCustomSql == null || _exportInfo.TableCustomSql.Count == 0)
            {
                _dicTableCustomSql = new Dictionary<string, string>();
                foreach (string s in DatabaseInfo.TableNames)
                {
                    _dicTableCustomSql.Add(s, string.Format("SELECT * FROM `{0}`;", s));
                }
            }
            else
            {
                _dicTableCustomSql = _exportInfo.TableCustomSql;
            }

            exportProgressArg.TotalTables = _dicTableCustomSql.Count;

            // Get & Calculate total of rows

            double tcs = 0;
            exportProgressArg.TotalRowsInAllTables = 0;
            foreach (KeyValuePair<string, string> kv in _dicTableCustomSql)
            {
                long totalRowsInCurTable = 0;

                if (_exportInfo.CalculateTotalRowsFromDatabase && (_exportInfo.ExportRows || _exportInfo.ExportTableStructure))
                {
                    #region Calculate Total Rows in Each Table
                    string countRowsSql = "";
                    if (kv.Value.ToUpper().Contains(" WHERE "))
                    {
                        int d = kv.Value.ToUpper().IndexOf(" WHERE ", 0);
                        string aa = kv.Value.Substring(d);
                        countRowsSql = "SELECT COUNT(*) FROM `" + kv.Key + "`" + aa;
                    }
                    else
                    {
                        countRowsSql = "SELECT COUNT(*) FROM `" + kv.Key + "`;";
                    }
                    _cmd.CommandText = countRowsSql;
                    totalRowsInCurTable = Convert.ToInt64(_cmd.ExecuteScalar());
                    #endregion
                }

                dicTableName_TotalRows[kv.Key] = totalRowsInCurTable;

                exportProgressArg.TotalRowsInAllTables += dicTableName_TotalRows[kv.Key];

                tcs += 1;
                if (ExportProgressChanged != null)
                {
                    exportProgressArg.PercentageGetTotalRowsCompleted = (int)(tcs / (double)_dicTableCustomSql.Count * (double)100);
                    ExportProgressChanged(this, exportProgressArg);
                }
            }
            #endregion

            #region Document Header

            textWriter.WriteLine(Encrypt("-- MySqlBackup.NET dump " + MySqlBackup.Version));
            if (_exportInfo.RecordDumpTime)
                textWriter.WriteLine(Encrypt("-- Dump time: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            else
                textWriter.WriteLine(Encrypt("--"));
            textWriter.WriteLine(Encrypt("-- ------------------------------------------------------"));
            textWriter.WriteLine(Encrypt("-- Server version	" + DatabaseInfo.ServerVersion));
            textWriter.WriteLine(Encrypt(""));
            textWriter.WriteLine(Encrypt(""));
            textWriter.WriteLine(Encrypt("/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;"));
            textWriter.WriteLine(Encrypt("/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;"));
            textWriter.WriteLine(Encrypt("/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;"));
            textWriter.WriteLine(Encrypt("/*!40101 SET NAMES " + DatabaseInfo.DefaultDatabaseCharSet + " */;"));
            textWriter.WriteLine(Encrypt("/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;"));
            textWriter.WriteLine(Encrypt("/*!40103 SET TIME_ZONE='+00:00' */;"));
            textWriter.WriteLine(Encrypt("/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;"));
            textWriter.WriteLine(Encrypt("/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;"));
            textWriter.WriteLine(Encrypt("/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;"));
            textWriter.WriteLine(Encrypt("/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;"));

            if (_exportInfo.AddCreateDatabase)
            {
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt("--"));
                textWriter.WriteLine(Encrypt("-- Create schema " + DatabaseInfo.DatabaseName));
                textWriter.WriteLine(Encrypt("--"));
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt(DatabaseInfo.CreateDatabaseSql));
                textWriter.WriteLine(Encrypt("USE " + DatabaseInfo.DatabaseName + ";"));
            }

            textWriter.Flush();
            #endregion

            foreach (KeyValuePair<string, string> kv in _dicTableCustomSql)
            {
                #region Reset Values for Progress Report of Current Table
                if (!ExportInfo.ExportRows && !ExportInfo.ExportTableStructure)
                {
                    if (ExportProgressChanged != null)
                    {
                        exportProgressArg.TotalTables = 0;
                        exportProgressArg.TotalRowsInCurrentTable = 0;
                        exportProgressArg.CurrentTableIndex = 0;
                        exportProgressArg.CurrentRowInCurrentTable = 0;
                        exportProgressArg.PercentageCompleted = 100;
                        ExportProgressChanged(this, exportProgressArg);
                    }
                    break;
                }

                if (cancelProcess)
                {
                    exportCompleteArg.CompletedType = ExportCompleteArg.CompleteType.Cancelled;
                    return;
                }

                exportProgressArg.CurrentTableName = kv.Key;

                if (ExportProgressChanged != null)
                {
                    //exportProgressArg.TotalRowsInCurrentTable = DatabaseInfo.Tables[exportProgressArg.CurrentTableName].TotalRows;
                    if (dicTableName_TotalRows.ContainsKey(kv.Key))
                        exportProgressArg.TotalRowsInCurrentTable = dicTableName_TotalRows[kv.Key];
                    exportProgressArg.CurrentTableIndex += 1;
                    exportProgressArg.CurrentRowInCurrentTable = 0;
                    exportProgressArg.PercentageCompleted = (int)((double)exportProgressArg.CurrentTableIndex / (double)exportProgressArg.TotalTables * (double)100);
                    ExportProgressChanged(this, exportProgressArg);
                }
                #endregion

                #region Table Header

                if (_exportInfo.ExportTableStructure)
                {
                    textWriter.WriteLine(Encrypt(""));
                    textWriter.WriteLine(Encrypt(""));
                    textWriter.WriteLine(Encrypt("--"));
                    textWriter.WriteLine(Encrypt("-- Definition of table `" + exportProgressArg.CurrentTableName + "`"));
                    textWriter.WriteLine(Encrypt("--"));
                    textWriter.WriteLine(Encrypt(""));
                    textWriter.WriteLine(Encrypt("DROP TABLE IF EXISTS `" + exportProgressArg.CurrentTableName + "`;"));
                    textWriter.WriteLine(Encrypt(DatabaseInfo.Tables[exportProgressArg.CurrentTableName].CreateTableSql));
                    if (_exportInfo.ResetAutoIncrement)
                        textWriter.WriteLine(Encrypt("ALTER TABLE `" + exportProgressArg.CurrentTableName + "` AUTO_INCREMENT = 1;"));
                }

                if (_exportInfo.ExportRows)
                {
                    textWriter.WriteLine(Encrypt(""));
                    textWriter.WriteLine(Encrypt("--"));
                    textWriter.WriteLine(Encrypt("-- Dumping data for table `" + exportProgressArg.CurrentTableName + "`"));
                    textWriter.WriteLine(Encrypt("--"));
                    textWriter.WriteLine(Encrypt(""));
                    textWriter.WriteLine(Encrypt("/*!40000 ALTER TABLE `" + exportProgressArg.CurrentTableName + "` DISABLE KEYS */;"));
                }
                textWriter.Flush();
                #endregion

                if (_exportInfo.ExportRows)
                {
                    #region Export Rows

                    string InsertStatementHeader = null;

                    _cmd.CommandText = _dicTableCustomSql[exportProgressArg.CurrentTableName];

                    MySqlDataReader rdr = _cmd.ExecuteReader();

                    StringBuilder sb = new StringBuilder();

                    while (rdr.Read())
                    {
                        // Cancel the export process
                        if (cancelProcess)
                        {
                            exportCompleteArg.CompletedType = ExportCompleteArg.CompleteType.Cancelled;
                            return;
                        }

                        // Raise the ProgressChanged Event
                        if (ExportProgressChanged != null)
                        {
                            exportProgressArg.CurrentRowInCurrentTable += 1;
                            exportProgressArg.CurrentRowInAllTable += 1;
                            ExportProgressChanged(this, exportProgressArg);
                        }

                        if (InsertStatementHeader == null)
                        {
                            int fc = rdr.FieldCount;
                            string[] ColumnNames = new string[fc];
                            for (int ci = 0; ci < rdr.FieldCount; ci++)
                            {
                                ColumnNames[ci] = rdr.GetName(ci);
                            }
                            InsertStatementHeader = GetInsertStatementHeader(exportProgressArg.CurrentTableName, ColumnNames);
                        }

                        string ValueString = GetSqlValueString(rdr);

                        if (sb.Length == 0)
                        {
                            sb.Append(InsertStatementHeader);
                            sb.Append("\r\n");
                            sb.Append(ValueString);
                        }
                        else if (((long)sb.Length + (long)ValueString.Length) < _exportInfo.MaxSqlLength)
                        {
                            sb.Append(",\r\n");
                            sb.Append(ValueString);
                        }
                        else
                        {
                            sb.Append(";");

                            string readyText = Encrypt(sb.ToString());

                            textWriter.WriteLine(readyText);
                            textWriter.Flush();

                            sb = new StringBuilder();
                            sb.Append(InsertStatementHeader);
                            sb.Append("\r\n");
                            sb.Append(ValueString);
                        }

                    }
                    rdr.Close();

                    if (sb.Length > 0)
                    {
                        sb.Append(";");
                    }

                    textWriter.WriteLine(Encrypt(sb.ToString()));
                    textWriter.Flush();
                    #endregion
                }

                #region Table Footer

                if (_exportInfo.ExportRows)
                {
                    textWriter.WriteLine(Encrypt("/*!40000 ALTER TABLE `" + exportProgressArg.CurrentTableName + "` ENABLE KEYS */;"));
                    textWriter.Flush();
                }
                #endregion
            }

            #region Export Function
            if (_exportInfo.ExportFunctions && DatabaseInfo.StoredFunction.Count != 0)
            {
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt("--"));
                textWriter.WriteLine(Encrypt("-- Dumping functions"));
                textWriter.WriteLine(Encrypt("--"));

                foreach (KeyValuePair<string, string> kv in DatabaseInfo.StoredFunction)
                {
                    textWriter.WriteLine(Encrypt(""));
                    textWriter.WriteLine(Encrypt(string.Format("DROP FUNCTION IF EXISTS `{0}`;", kv.Key)));
                    textWriter.WriteLine(Encrypt("DELIMITER " + _delimeter));
                    textWriter.WriteLine(Encrypt(kv.Value));
                    textWriter.WriteLine(Encrypt(_delimeter));
                    textWriter.WriteLine(Encrypt("DELIMITER ;"));
                    textWriter.WriteLine(Encrypt(""));
                }

                textWriter.Flush();
            }
            #endregion

            #region Export Stored Procedure
            if (_exportInfo.ExportStoredProcedures && DatabaseInfo.StoredProcedure.Count != 0)
            {
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt("--"));
                textWriter.WriteLine(Encrypt("-- Dumping stored procedures"));
                textWriter.WriteLine(Encrypt("--"));

                foreach (KeyValuePair<string, string> kv in DatabaseInfo.StoredProcedure)
                {
                    textWriter.WriteLine(Encrypt(""));
                    textWriter.WriteLine(Encrypt(string.Format("DROP PROCEDURE IF EXISTS `{0}`;", kv.Key)));
                    textWriter.WriteLine(Encrypt("DELIMITER " + _delimeter));
                    textWriter.WriteLine(Encrypt(kv.Value));
                    textWriter.WriteLine(Encrypt(_delimeter));
                    textWriter.WriteLine(Encrypt("DELIMITER ;"));
                    textWriter.WriteLine(Encrypt(""));
                }
                textWriter.Flush();
            }
            #endregion

            #region Export Events
            if (_exportInfo.ExportEvents && DatabaseInfo.StoredEvents.Count != 0)
            {
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt("--"));
                textWriter.WriteLine(Encrypt("-- Dumping events"));
                textWriter.WriteLine(Encrypt("--"));

                foreach (KeyValuePair<string, string> kv in DatabaseInfo.StoredEvents)
                {
                    textWriter.WriteLine(Encrypt(""));
                    textWriter.WriteLine(Encrypt(string.Format("DROP EVENT IF EXISTS `{0}`;", kv.Key)));
                    textWriter.WriteLine(Encrypt("DELIMITER " + _delimeter));
                    textWriter.WriteLine(Encrypt(kv.Value));
                    textWriter.WriteLine(Encrypt(_delimeter));
                    textWriter.WriteLine(Encrypt("DELIMITER ;"));
                    textWriter.WriteLine(Encrypt(""));
                }

                textWriter.WriteLine(Encrypt("SET GLOBAL event_scheduler = ON;"));
                textWriter.Flush();
            }
            #endregion

            #region Export View
            if (_exportInfo.ExportViews && DatabaseInfo.StoredView.Count != 0)
            {
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt("--"));
                textWriter.WriteLine(Encrypt("-- Dumping views"));
                textWriter.WriteLine(Encrypt("--"));

                foreach (KeyValuePair<string, string> kv in DatabaseInfo.StoredView)
                {
                    textWriter.WriteLine(Encrypt(""));
                    textWriter.WriteLine(Encrypt(string.Format("DROP VIEW IF EXISTS `{0}`;", kv.Key)));
                    textWriter.WriteLine(Encrypt(kv.Value));
                    textWriter.WriteLine(Encrypt(""));
                }

                textWriter.Flush();
            }
            #endregion

            #region Export Trigger
            if (_exportInfo.ExportTriggers && DatabaseInfo.StoredTrigger.Count != 0)
            {

                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt(""));
                textWriter.WriteLine(Encrypt("--"));
                textWriter.WriteLine(Encrypt("-- Dumping triggers"));
                textWriter.WriteLine(Encrypt("--"));

                foreach (KeyValuePair<string, string> kv in DatabaseInfo.StoredTrigger)
                {
                    textWriter.WriteLine(Encrypt(""));
                    textWriter.WriteLine(Encrypt(string.Format("DROP TRIGGER IF EXISTS `{0}`;", kv.Key)));
                    textWriter.WriteLine(Encrypt("DELIMITER " + _delimeter));
                    textWriter.WriteLine(Encrypt(kv.Value));
                    textWriter.WriteLine(Encrypt(_delimeter));
                    textWriter.WriteLine(Encrypt("DELIMITER ;"));
                    textWriter.WriteLine(Encrypt(""));
                }

                textWriter.Flush();
            }
            #endregion

            #region Document Footer

            textWriter.WriteLine(Encrypt(""));
            textWriter.WriteLine(Encrypt(""));
            textWriter.WriteLine(Encrypt("/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;"));
            textWriter.WriteLine(Encrypt("/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;"));
            textWriter.WriteLine(Encrypt("/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;"));
            textWriter.WriteLine(Encrypt("/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;"));
            textWriter.WriteLine(Encrypt("/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;"));
            textWriter.WriteLine(Encrypt("/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;"));
            textWriter.WriteLine(Encrypt("/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;"));
            textWriter.WriteLine(Encrypt("/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;"));

            textWriter.Flush();
            #endregion

            #region Reset max_allowed_packet to original value
            try
            {
                _cmd.CommandText = "SET GLOBAL max_allowed_packet = " + originalMaxAllowedPacket + ";";
                _cmd.ExecuteNonQuery();
            }
            catch
            {
                // Purposely do nothing.
                // Error will be trapped if the user does not has the privilege
                // to modify GLOBAL variables.
            }
            #endregion

            #region Zip Output File
            if (ExportInfo.ZipOutputFile)
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                    string newZipFile = Path.GetDirectoryName(ExportInfo.FileName) + "\\" + Path.GetFileNameWithoutExtension(ExportInfo.FileName) + ".zip";
                    zip.AddFile(ExportInfo.FileName);
                    zip.Save(newZipFile);
                    ExportInfo.FileName = newZipFile;
                }
            }
            #endregion

            exportCompleteArg.CompletedType = ExportCompleteArg.CompleteType.Completed;
        }

        /// <summary>
        /// Cancel the current executing export process.
        /// </summary>
        public void CancelExport()
        {
            cancelProcess = true;
        }

        private string GetInsertStatementHeader(string table, string[] columnNames)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO `" + table + "` (");
            foreach (string s in columnNames)
            {
                sb.Append("`" + s + "`,");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(") VALUES");
            return sb.ToString();
        }

        private string GetSqlValueString(MySqlDataReader rdr)
        {
            StringBuilder sbData = new StringBuilder();

            sbData.Append("(");

            for (int i = 0; i < rdr.FieldCount; i++)
            {
                object ob = rdr[i];

                if (ob == null || ob is System.DBNull)
                {
                    sbData.Append("NULL,");
                }
                else if (ob is System.String)
                {
                    string data = ob.ToString();

                    EscapeStringSequence(ref data);

                    sbData.Append(String.Format("'{0}',", data));
                }
                else if (ob is System.DateTime)
                {
                    sbData.Append(String.Format("'{0}',", ((DateTime)ob).ToString("yyyy-MM-dd HH:mm:ss", df)));
                }
                else if (ob is System.Boolean)
                {
                    sbData.Append(Convert.ToInt32(ob).ToString() + ",");
                }
                else if (ob is System.Byte[])
                {
                    sbData.Append(methods.GetBLOBSqlDataStringFromBytes((byte[])ob) + ",");
                }
                else if (ob is short)
                {
                    sbData.Append(((short)ob).ToString(nf) + ",");
                }
                else if (ob is int)
                {
                    sbData.Append(((int)ob).ToString(nf) + ",");
                }
                else if (ob is long)
                {
                    sbData.Append(((long)ob).ToString(nf) + ",");
                }
                else if (ob is ushort)
                {
                    sbData.Append(((ushort)ob).ToString(nf) + ",");
                }
                else if (ob is uint)
                {
                    sbData.Append(((uint)ob).ToString(nf) + ",");
                }
                else if (ob is ulong)
                {
                    sbData.Append(((ulong)ob).ToString(nf) + ",");
                }
                else if (ob is double)
                {
                    sbData.Append(((double)ob).ToString(nf) + ",");
                }
                else if (ob is decimal)
                {
                    sbData.Append(((decimal)ob).ToString(nf) + ",");
                }
                else if (ob is float)
                {
                    sbData.Append(((float)ob).ToString(nf) + ",");
                }
                else if (ob is byte)
                {
                    sbData.Append(((byte)ob).ToString(nf) + ",");
                }
                else if (ob is sbyte)
                {
                    sbData.Append(((sbyte)ob).ToString(nf) + ",");
                }
                else if (ob is TimeSpan)
                {
                    sbData.Append("'" + ((TimeSpan)ob).Hours.ToString().PadLeft(2, '0') + ":" + ((TimeSpan)ob).Minutes.ToString().PadLeft(2, '0') + ":" + ((TimeSpan)ob).Seconds.ToString().PadLeft(2, '0') + "',");
                }
                else if (ob is MySql.Data.Types.MySqlDateTime)
                {
                    if (((MySql.Data.Types.MySqlDateTime)ob).IsNull)
                    {
                        sbData.Append("NULL,");
                    }
                    else
                    {
                        string dataType = DatabaseInfo.Tables[exportProgressArg.CurrentTableName].ColumnDataType[rdr.GetName(i)];

                        if (((MySql.Data.Types.MySqlDateTime)ob).IsValidDateTime)
                        {
                            DateTime dtime = ((MySql.Data.Types.MySqlDateTime)ob).Value;

                            if (dataType == "datetime")
                                sbData.Append("'" + dtime.ToString("yyyy-MM-dd HH:mm:ss", df) + "',");
                            else if (dataType == "date")
                                sbData.Append("'" + dtime.ToString("yyyy-MM-dd", df) + "',");
                            else if (dataType == "time")
                                sbData.Append("'" + dtime.ToString("HH:mm:ss", df) + "',");
                        }
                        else
                        {
                            if (dataType == "datetime")
                                sbData.Append("'0000-00-00 00:00:00',");
                            else if (dataType == "date")
                                sbData.Append("'0000-00-00',");
                            else if (dataType == "time")
                                sbData.Append("'00:00:00',");
                        }
                    }
                }
                else if (ob is System.Guid)
                {
                    string dataType = DatabaseInfo.Tables[exportProgressArg.CurrentTableName].ColumnDataType[rdr.GetName(i)];
                    if (dataType == "binary(16)")
                    {
                        sbData.Append(methods.GetBLOBSqlDataStringFromBytes(((Guid)ob).ToByteArray()) + ",");
                    }
                    else if (dataType == "char(36)")
                    {
                        sbData.Append("'" + ob + "',");
                    }
                }
                else
                {
                    throw new Exception("Unhandled data type. Current processing data type: " + ob.GetType().ToString() + ". Please report this bug with this message to the development team.");
                }
            }

            sbData.Remove(sbData.Length - 1, 1);
            sbData.Append(")");
            return sbData.ToString();
        }

        /// <summary>
        /// Escape string sequence of data and make it safe and compatible to used in SQL queries.
        /// </summary>
        /// <param name="data"></param>
        public void EscapeStringSequence(ref string data)
        {
            var builder = new StringBuilder();
            foreach (var ch in data)
            {
                switch (ch)
                {
                    case '\\': // Backslash
                        builder.AppendFormat("\\\\");
                        break;
                    case '\r': // Carriage return
                        builder.AppendFormat("\\r");
                        break;
                    case '\n': // New Line
                        builder.AppendFormat("\\n");
                        break;
                    case '\a': // Vertical tab
                        builder.AppendFormat("\\a");
                        break;
                    case '\b': // Backspace
                        builder.AppendFormat("\\b");
                        break;
                    case '\f': // Formfeed
                        builder.AppendFormat("\\f");
                        break;
                    case '\t': // Horizontal tab
                        builder.AppendFormat("\\t");
                        break;
                    case '\v': // Vertical tab
                        builder.AppendFormat("\\v");
                        break;
                    case '\"': // Double quotation mark
                        builder.AppendFormat("\\\"");
                        break;
                    case '\'': // Single quotation mark
                        builder.AppendFormat("\\\'");
                        break;
                    default:
                        builder.Append(ch);
                        break;
                }
            }
            data = builder.ToString();
        }
        #endregion

        #region Import / Restore

        /// <summary>
        /// Execute the import (restore) process. Sets the ImportInfo about this import process.
        /// </summary>
        /// <param name="importInfo">Sets the Informations that define behaviour of Import Process.</param>
        public void Import(ImportInformations importInfo)
        {
            ImportInfo = importInfo;
            Import();
        }

        /// <summary>
        /// Execute the import process.
        /// </summary>
        public void Import()
        {
            if (_importInfo.AsynchronousMode)
            {
                bwImport = new BackgroundWorker();
                bwImport.DoWork += new DoWorkEventHandler(bwImport_DoWork);
                bwImport.RunWorkerAsync();
            }
            else
            {
                ImportExecute();
            }
        }

        void bwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            ImportExecute();
        }

        private void ImportExecute()
        {
            try
            {
                ImportStart();
            }
            catch (Exception ex)
            {
                Dispose(_importInfo.AutoCloseConnection);
                importCompleteArg.LastError = ex;
                importCompleteArg.HasErrors = true;
                importCompleteArg.CompletedType = ImportCompleteArg.CompleteType.Error;
                importCompleteArg.TimeEnd = DateTime.Now;
                _importInfo.CompleteArg = importCompleteArg;
                if (!_importInfo.AsynchronousMode)
                {
                    throw ex;
                }
            }


            if (ImportProgressChanged != null)
            {
                importProgressArg.CurrentByte = importProgressArg.TotalBytes;
                ImportProgressChanged(this, importProgressArg);
            }

            importCompleteArg.TimeEnd = DateTime.Now;

            _importInfo.CompleteArg = importCompleteArg;

            if (ImportCompleted != null)
            {
                ImportCompleted(this, importCompleteArg);
            }
        }

        private void ImportStart()
        {
            bool isValidFile = false;
            cancelProcess = false;
            bool sqlExecutedOnce = false;
            InitializeInternalComponent();

            bool IsScript = false;
            string delimeter = "";

            bool detectedCharSet = false;
            bool detectedCreateDatabase = false;
            bool detectedUseDatabase = false;

            #region Initialize Value for Progress Report

            importCompleteArg = new ImportCompleteArg(ImportCompleteArg.CompleteType.Completed);
            importCompleteArg.TimeStart = DateTime.Now;
            importCompleteArg.Errors = new Dictionary<long, Exception>();

            importProgressArg = new ImportProgressArg();
            importProgressArg.CurrentLineNo = 0;
            FileInfo fi = new FileInfo(_importInfo.FileName);
            importProgressArg.TotalBytes = fi.Length;

            #endregion

            #region Check Connection
            if (_conn == null)
            {
                throw new Exception("Connection has disposed. Set ImportSettings.AutoCloseConnection to false if you want to reuse this instance.");
            }

            if (_cmd.Connection.State != ConnectionState.Open)
                _cmd.Connection.Open();
            #endregion

            // Current value of max_allowed_packet will store here
            // This value will be restored at the end of this process
            string originalMaxAllowedPacket = "";

            #region Set max_allowed_packet to Maximum (1GB)
            try
            {
                _cmd.CommandText = "SHOW GLOBAL VARIABLES LIKE 'max_allowed_packet';";
                MySqlDataAdapter da = new MySqlDataAdapter(_cmd);
                DataTable dtMAP = new DataTable();
                da.Fill(dtMAP);
                originalMaxAllowedPacket = dtMAP.Rows[0]["Value"] + "";
                da = null;
                dtMAP = null;

                _cmd.CommandText = "SET GLOBAL max_allowed_packet=1024*1024*1024;"; // 1GB, MySQL Maximum Length
                _cmd.ExecuteNonQuery();

                // Modified max_allowed_packet will only take effect for new connection.
                // Therefore the connection has to be closed and reopen.

                _cmd.Connection.Close();
                _cmd.Connection.Open();
            }
            catch
            {
                // Purposely do nothing
                // Error will be trapped if the user does not has the privilege
                // to modify GLOBAL VARIABLES.
            }
            #endregion

            bool ignoreCreateDB = false;
            string createDBSql = _importInfo.CreateTargetDatabaseSql;

            string file = "";

            #region Extract file from Zip File
            if (Path.GetExtension(ImportInfo.FileName).ToLower() == ".zip")
            {
                using (ZipFile zip = new ZipFile(ImportInfo.FileName))
                {
                    string curPath = Path.GetDirectoryName(ImportInfo.FileName);
                    zip.ExtractAll(curPath, ExtractExistingFileAction.OverwriteSilently);
                    foreach (string s in zip.EntryFileNames)
                    {
                        file = curPath + "\\" + s;
                    }
                }
                fi = new FileInfo(file);
                importProgressArg.TotalBytes = fi.Length;
            }
            else
            {
                file = ImportInfo.FileName;
            }
            #endregion

            #region Create or Use Database
            if (createDBSql != "")
            {
                try
                {
                    _cmd.CommandText = createDBSql;
                    _cmd.ExecuteNonQuery();
                    _cmd.CommandText = string.Format("USE `{0}`;", _importInfo.TargetDatabase);
                    _cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + ". Fail to create or use database.", ex);
                }
                ignoreCreateDB = true;
            }

            #endregion

            //textReader = new StreamReader(_importInfo.FileName, true);
            textReader = new StreamReader(file, true);

            StringBuilder sb = new StringBuilder();

            string line = "";

            while (line != null)
            {
                try
                {
                    #region Import process is called to stop
                    if (cancelProcess)
                    {
                        importCompleteArg.CompletedType = ImportCompleteArg.CompleteType.Cancelled;
                        importCompleteArg.CurrentLineNo = importProgressArg.CurrentLineNo;
                        Dispose(_importInfo.AutoCloseConnection);
                        return;
                    }
                    #endregion

                    line = textReader.ReadLine();
                    importCompleteArg.CurrentLineNo++;
                    if (line == null)
                    {
                        continue;
                    }

                    #region Report Progress

                    if (ImportProgressChanged != null)
                    {
                        importProgressArg.Error = null;
                        importProgressArg.CurrentByte += line.Length;
                        if (importProgressArg.CurrentByte != 0 && importProgressArg.TotalBytes != 0)
                        {
                            importProgressArg.PercentageCompleted = (int)(((double)importProgressArg.CurrentByte / (double)importProgressArg.TotalBytes) * (double)100);
                        }
                        ImportProgressChanged(this, importProgressArg);
                    }
                    #endregion

                    #region Detect Empty String, Do nothing if detect
                    if (line.Trim().Length == 0)
                        continue;

                    line = Decrypt(line).TrimEnd();

                    if (line.Length == 0)
                        continue;
                    if (line == "\r\n")
                        continue;
                    if (line.StartsWith("--"))
                        continue;
                    #endregion

                    #region Detect Char Set
                    if (!detectedCharSet)
                    {
                        if (line.StartsWith("/*!40101 SET NAMES ") || line.StartsWith("SET NAMES "))
                        {
                            _cmd.CommandText = "SHOW VARIABLES LIKE 'character_set_database';";
                            MySqlDataAdapter da = new MySqlDataAdapter(_cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            string charset = dt.Rows[0][1] + "";
                            //if (line.StartsWith("/*!40101"))
                            //    _cmd.CommandText = "/*!40101 SET NAMES " + charset + "*/;";
                            //else
                            _cmd.CommandText = "SET NAMES " + charset + ";";
                            _cmd.ExecuteNonQuery();
                            detectedCharSet = true;
                            continue;
                        }
                    }
                    #endregion

                    #region Ignore Create Database if Target is Set
                    if (ignoreCreateDB)
                    {
                        if (!detectedCreateDatabase)
                        {
                            if (line.StartsWith("CREATE DATABASE"))
                            {
                                detectedCreateDatabase = true;
                                continue;
                            }
                        }
                        if (!detectedUseDatabase)
                        {
                            if (line.StartsWith("USE "))
                            {
                                detectedUseDatabase = true;
                                continue;
                            }
                        }
                    }
                    #endregion

                    sb.Append(line);

                    #region Detect SQL Script
                    if (line.StartsWith("DELIMITER"))
                    {
                        string nextDelimiter = line.Replace("DELIMITER ", string.Empty);
                        nextDelimiter = nextDelimiter.Replace(" ", string.Empty);
                        delimeter = nextDelimiter;

                        if (delimeter == ";")
                        {
                            sb = new StringBuilder();
                            IsScript = false;
                            continue;
                        }
                        else
                        {
                            IsScript = true;
                            sb.Append("\r\n");
                            continue;
                        }
                    }
                    #endregion

                    #region Execute SQL Commands
                    if (IsScript)
                    {
                        sb.Append("\r\n");
                        #region Execute SQL Script
                        if (line.Contains(delimeter))
                        {
                            sb.Append(delimeter);

                            //// Log Executed SQL - For debug use
                            //textWriter = new StreamWriter("D:\\Log2.txt", true, utf8WithoutBOM);
                            //textWriter.WriteLine(sb.ToString() + "\r\n");
                            //textWriter.Close();

                            MySqlScript ms = new MySqlScript(_conn, sb.ToString());
                            ms.Execute();
                            ms = null;
                            if (!sqlExecutedOnce)
                                sqlExecutedOnce = true;
                            sb = new StringBuilder();
                            IsScript = false;
                            continue;
                        }
                        else
                        {

                            continue;
                        }
                        #endregion
                    }
                    else
                    {
                        if (line.EndsWith(";"))
                        {
                            #region Execute Single SQL Statement
                            //// Log Executed SQL - For debug use
                            //textWriter = new StreamWriter("D:\\Log2.txt", true, utf8WithoutBOM);
                            //textWriter.WriteLine(sb.ToString() + "\r\n");
                            //textWriter.Close();

                            if (line.TrimStart().StartsWith("DELIMETER"))
                            {
                                sb = new StringBuilder();
                                continue;
                            }

                            _cmd.CommandText = sb.ToString();
                            _cmd.ExecuteNonQuery();

                            if (!detectedCharSet || !detectedCreateDatabase || !detectedUseDatabase)
                            {
                                if (_cmd.CommandText.StartsWith("CREATE TABLE") || _cmd.CommandText.StartsWith("INSERT"))
                                {
                                    detectedCharSet = true;
                                    detectedCreateDatabase = true;
                                    detectedUseDatabase = true;
                                }
                            }

                            if (!sqlExecutedOnce)
                                sqlExecutedOnce = true;

                            sb = new StringBuilder();
                            #endregion
                        }
                        else
                        {
                            if (!isValidFile && !sqlExecutedOnce)
                            {
                                string aa = line.ToLower();
                                if (aa.StartsWith("/*!4") || aa.StartsWith("drop") || aa.StartsWith("create") ||
                                    aa.StartsWith("delimeter") || aa.StartsWith("insert"))
                                {
                                    isValidFile = true;
                                }
                                else
                                {
                                    throw new Exception("This is not a valid SQL Dump File. No executeable SQL query found.");
                                }
                            }
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    IsScript = false;
                    importProgressArg.ErrorSql = sb.ToString();
                    importProgressArg.Error = ex;
                    importCompleteArg.HasErrors = true;
                    importCompleteArg.LastError = ex;
                    try
                    {
                        importCompleteArg.Errors.Add(importProgressArg.CurrentLineNo, ex);
                    }
                    catch { }
                    sb = new StringBuilder();

                    if (_importInfo.IgnoreSqlError)
                    {
                        #region Report Error

                        if (ImportProgressChanged != null)
                        {
                            ImportProgressChanged(this, importProgressArg);
                        }
                        continue;
                        #endregion
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }

            textReader.Close();

            importProgressArg.CurrentByte = importProgressArg.TotalBytes;
            importProgressArg.PercentageCompleted = 100;

            if (ImportProgressChanged != null)
            {
                ImportProgressChanged(this, importProgressArg);
            }

            if (!sqlExecutedOnce)
            {
                throw new Exception("This is not a valid SQL Dump File. No executeable SQL query found.");
            }


            #region Reset max_allowed_packet to original value
            try
            {
                _cmd.CommandText = "SET GLOBAL max_allowed_packet = " + originalMaxAllowedPacket + ";";
                _cmd.ExecuteNonQuery();
            }
            catch
            {
                // Purposely do nothing.
                // Error will be trapped if the user does not has the privilege
                // to modify GLOBAL variables.
            }
            #endregion


            importCompleteArg.CompletedType = ImportCompleteArg.CompleteType.Completed;
        }

        /// <summary>
        /// Cancel the current executing import process.
        /// </summary>
        public void CancelImport()
        {
            cancelProcess = true;
        }
        #endregion

        #region Private Encryption Methods

        private string Encrypt(string input)
        {
            if (_exportInfo.EnableEncryption)
            {
                if (input == null || input.Length == 0)
                    return methods.EncryptWithSalt("-- ||||" + methods.RandomString(methods.random.Next(100, 500)), _exportInfo.EncryptionKey, _exportInfo.SaltSize);
                return methods.EncryptWithSalt(input, _exportInfo.EncryptionKey, _exportInfo.SaltSize);
            }
            else
                return input;
        }

        private string Decrypt(string input)
        {
            if (input == null || input.Length == 0)
                return input;
            if (_importInfo.EnableEncryption)
            {
                string str = methods.DecryptWithSalt(input, _importInfo.EncryptionKey, _importInfo.SaltSize);
                if (str == "-- ||||")
                    return "";
                else
                    return str;
            }
            else
                return input;
        }

        #endregion

        /// <summary>
        /// Delete all rows in all tables.
        /// </summary>
        /// <param name="resetAutoIncrement">Sets a value indicates whether Auto-Increment should reset to 1.</param>
        public void DeleteAllRows(bool resetAutoIncrement)
        {
            DeleteAllRows(resetAutoIncrement, null);
        }

        /// <summary>
        /// Delete all rows in all tables.
        /// </summary>
        /// <param name="resetAutoIncrement">Sets a value indicates whether Auto-Increment should reset to 1.</param>
        /// <param name="excludeTables">Exclude these tables from rows deletion.</param>
        public void DeleteAllRows(bool resetAutoIncrement, string[] excludeTables)
        {
            if (_conn.State != ConnectionState.Open)
                _conn.Open();

            string[] tables = _database.TableNames;

            foreach (string t in tables)
            {
                bool skipThisTable = false;

                if (excludeTables != null && excludeTables.Length > 0)
                {
                    foreach (string s in excludeTables)
                    {
                        if (s == t)
                        {
                            skipThisTable = true;
                            break;
                        }
                    }
                }

                if (skipThisTable)
                {
                    continue;
                }

                _cmd.CommandText = "DELETE FROM `" + t + "`;";
                _cmd.ExecuteNonQuery();
                if (resetAutoIncrement)
                {
                    _cmd.CommandText = "ALTER TABLE `" + t + "` AUTO_INCREMENT = 1;";
                    _cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Decrypt a SQL Dump File and save as new file.
        /// </summary>
        /// <param name="originalFile">The source of Encrypted SQL Dump File.</param>
        /// <param name="newFile">The new file of Decrypted SQL Dump File.</param>
        public void DecryptSqlDumpFile(string originalFile, string newFile, string encryptionKey)
        {
            methods = new Methods();
            encryptionKey = methods.Sha2Hash(encryptionKey);
            int saltSize = methods.GetSaltSize(encryptionKey);

            if (!File.Exists(originalFile))
                throw new Exception("Original file is not exists.");

            textReader = new StreamReader(originalFile, utf8WithoutBOM);

            if (File.Exists(newFile))
            {
                File.Delete(newFile);
            }

            string line = "";

            bool firstWrite = true;

            while (line != null)
            {
                line = textReader.ReadLine();
                if (line == null)
                    continue;
                line = methods.DecryptWithSalt(line, encryptionKey, saltSize);
                if (line.StartsWith("-- ||||"))
                    line = "";
                TextWriter textWriter = new StreamWriter(newFile, !firstWrite, utf8WithoutBOM);
                textWriter.WriteLine(line);
                textWriter.Close();

                firstWrite = false;
            }
            methods = null;
        }

        /// <summary>
        /// Encrypt a SQL Dump File and save as new file.
        /// </summary>
        /// <param name="originalFile">The source of SQL Dump File.</param>
        /// <param name="newFile">The new file of Encrypted SQL Dump File.</param>
        public void EncryptSqlDumpFile(string originalFile, string newFile, string encryptionKey)
        {
            methods = new Methods();
            encryptionKey = methods.Sha2Hash(encryptionKey);
            int saltSize = methods.GetSaltSize(encryptionKey);

            if (!File.Exists(originalFile))
                throw new Exception("Original file is not exists.");

            textReader = new StreamReader(originalFile, utf8WithoutBOM);

            if (File.Exists(newFile))
            {
                File.Delete(newFile);
            }

            string line = "";

            bool firstWrite = true;

            while (line != null)
            {
                line = textReader.ReadLine();
                if (line == null)
                    continue;
                if (line + "" == "")
                    line = "-- ||||" + methods.RandomString(methods.random.Next(50, 300));
                line = methods.EncryptWithSalt(line, encryptionKey, saltSize);
                TextWriter textWriter = new StreamWriter(newFile, !firstWrite, utf8WithoutBOM);
                textWriter.WriteLine(line);
                textWriter.Close();

                firstWrite = false;
            }
            methods = null;
        }

        /// <summary>
        /// Export BLOB data type and save as file.
        /// </summary>
        /// <param name="targetSaveFolder">The folder that the files will save to.</param>
        /// <param name="table">The name of table that contains BLOB.</param>
        /// <param name="colBlob">The name of column that contains BLOB data.</param>
        /// <param name="colFileName">The name of column that contains file name.</param>
        /// <param name="colFilesize">The name of column that contains the size of the BLOB.</param>
        /// <returns></returns>
        public void ExportBlobAsFile(string targetSaveFolder, string table, string colBlob, string colFileName, string colFilesize)
        {
            MySqlDataReader rdr;
            try
            {
                if (_cmd.Connection.State != ConnectionState.Open)
                    _cmd.Connection.Open();

                string SQL;
                UInt32 FileSize;
                byte[] rawData;

                SQL = "select `" + colFileName + "`, `" + colFilesize + "`, `" + colBlob + "` from `" + table + "`;";

                _cmd.CommandText = SQL;

                rdr = _cmd.ExecuteReader();

                if (!rdr.HasRows)
                    throw new Exception("There are no BLOBs to save");

                while (rdr.Read())
                {

                    FileSize = rdr.GetUInt32(rdr.GetOrdinal(colFilesize));
                    rawData = new byte[FileSize];

                    rdr.GetBytes(rdr.GetOrdinal(colBlob), 0, rawData, 0, (int)FileSize);

                    if (!Directory.Exists(targetSaveFolder))
                    {
                        Directory.CreateDirectory(targetSaveFolder);
                    }

                    using (FileStream fs = new FileStream(targetSaveFolder + "\\" + rdr[colFileName], FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fs.Write(rawData, 0, (int)FileSize);
                        fs.Close();
                    }
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                Dispose();
                throw ex;
            }
            finally
            {
                Dispose();
            }
        }

        /// <summary>
        /// Release all resources used by this instance.  
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Release all resources used by this instance. Determine whether MySqlConnection and MySqlCommand used by this instance should remain.
        /// </summary>
        /// <param name="disposeConnection">Sets a value indicates whether MySqlConnection and MySqlCommand used by this instance should remain.</param>
        public void Dispose(bool disposeConnection)
        {
            if (textReader != null)
                textReader.Close();
            if (textWriter != null)
                textWriter.Close();

            methods = null;
            textReader = null;
            textWriter = null;

            if (disposeConnection)
            {
                try
                {
                    if (_conn != null)
                    {
                        if (_conn.State != ConnectionState.Closed)
                        {
                            _conn.Close();
                        }
                    }
                }
                catch
                { }
                finally
                {
                    _conn = null;
                    _cmd = null;
                }
            }
        }
    }
}