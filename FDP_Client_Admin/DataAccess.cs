using System;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace FDP_Client_Admin
{
    /// <summary>
    /// This class does the data manipulation
    /// </summary>
    public class DataAccess
    {
        readonly MySqlConnection mySqlConnection = new MySqlConnection();
        readonly MySqlCommand mySqlCommand = new MySqlCommand();
        public MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
        readonly DataSet dataSet = new DataSet();
        public System.Windows.Forms.BindingSource bindingSource = new System.Windows.Forms.BindingSource();
        public MySqlCommand selectCommand = new MySqlCommand();
        public MySqlCommand insertCommand = new MySqlCommand();
        public MySqlCommand updateCommand = new MySqlCommand();
        public MySqlCommand deleteCommand = new MySqlCommand();
        
        int _id_utilizator;
        int ID_UTILIZATOR
        {
            get { return _id_utilizator; }
            set { _id_utilizator = value; }
        }

        public DataAccess()
        {
        }

        public DataAccess(string script)
        {
            mySqlConnection.ConnectionString = SettingsClass.CompanyId != 0 ? SettingsClass.ConnectionStringCompany : SettingsClass.ConnectionString;
            mySqlCommand.Connection = mySqlConnection;
        }

        public DataAccess(CommandType _commandType, string _commandText)
        {
            mySqlConnection.ConnectionString = SettingsClass.CompanyId != 0 ? SettingsClass.ConnectionStringCompany : SettingsClass.ConnectionString;
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.CommandType = _commandType;
            mySqlCommand.CommandText = _commandText;

            mySqlConnection.Open();
        }

        public DataAccess(object _ID_UTILIZATOR, CommandType _commandType, string _commandText)
        {
            ID_UTILIZATOR = Convert.ToInt32(_ID_UTILIZATOR);
            mySqlConnection.ConnectionString = SettingsClass.CompanyId != 0 ? SettingsClass.ConnectionStringCompany : SettingsClass.ConnectionString;

            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.CommandType = _commandType;
            mySqlCommand.CommandText = _commandText;

            mySqlConnection.Open();
        }

        public DataAccess(CommandType _commandType, string _commandText, object[] _commandParameters)
        {
            mySqlConnection.ConnectionString = SettingsClass.CompanyId != 0 ? SettingsClass.ConnectionStringCompany : SettingsClass.ConnectionString;
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.CommandType = _commandType;
            mySqlCommand.CommandText = _commandText;
            foreach (MySqlParameter mySqlParameter in _commandParameters)
            {
                mySqlCommand.Parameters.Add(mySqlParameter);
            }
            mySqlConnection.Open();
        }

        public DataAccess(object _ID_UTILIZATOR, CommandType _commandType, string _commandText, object[] _commandParameters)
        {
            ID_UTILIZATOR = Convert.ToInt32(_ID_UTILIZATOR);
            mySqlConnection.ConnectionString = SettingsClass.CompanyId != 0 ? SettingsClass.ConnectionStringCompany : SettingsClass.ConnectionString;
            mySqlCommand.Connection = mySqlConnection;
            mySqlCommand.CommandType = _commandType;
            mySqlCommand.CommandText = _commandText;
            foreach (MySqlParameter mySqlParameter in _commandParameters)
            {
                mySqlCommand.Parameters.Add(mySqlParameter);
            }
            mySqlConnection.Open();
        }

        public DataSet ExecuteSelectQuery()
        {
            try
            {
                //MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(mySqlCommand);
                mySqlDataAdapter.SelectCommand = mySqlCommand;
                mySqlDataAdapter.Fill(dataSet);
                mySqlConnection.Close();
                return dataSet;
            }
            catch (MySqlException mySqlException)
            {
                mySqlException.ToString();
                mySqlConnection.Close();
                //return null;
                LogWriter.Log(mySqlException.Message, SettingsClass.ErrorLogFile);
                throw new Exception(ExceptionParser.ParseException(mySqlException));
            }
        }

        public void ExecuteScript(string script)
        {
            try
            {
                mySqlConnection.Open();
                MySqlScript mySqlScript = new MySqlScript(mySqlConnection, script);
                mySqlScript.Execute();
                mySqlConnection.Close();
            }
            catch (MySqlException mySqlException)
            {
                mySqlException.ToString();
                try
                {
                    mySqlConnection.Close();
                }
                catch { }
                //return null;
                LogWriter.Log(mySqlException.Message, SettingsClass.ErrorLogFile);
                throw new Exception(ExceptionParser.ParseException(mySqlException));
            }
        }

        public void ExecuteNonQuery()
        {
            try
            {
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
            }
            catch (MySqlException mySqlException)
            {
                mySqlException.ToString();
                mySqlConnection.Close();
                //return null;
                LogWriter.Log(mySqlException.Message, SettingsClass.ErrorLogFile);
                throw new Exception(ExceptionParser.ParseException(mySqlException));
            }
        }


        public object[] ExecuteUpdateQuery()
        {
            try
            {
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                // LOG INFO --
                try
                {
                    string action = "";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("insert") > 0) action = "INSERT";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("update") > 0) action = "UPDATE";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("delete") > 0) action = "DELETE";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("import") > 0) action = "IMPORT";
                    string table = action != "IMPORT" ? mySqlCommand.CommandText.ToUpper().Replace("SP_", "").Replace(action, "") : mySqlCommand.CommandText.ToUpper().Replace("SP", "").Replace("REGULARIMPORT", "");
                    string detalii_before = "";
                    if (mySqlCommand.Parameters.Contains("_ID"))
                    {
                        try
                        {
                            detalii_before = GetDetaliiBefore(table, Convert.ToInt32(mySqlCommand.Parameters["_ID"].Value));
                        }
                        catch { detalii_before = ""; }
                    }
                    string detalii_after = "";
                    try
                    {
                        foreach (MySqlParameter mp in mySqlCommand.Parameters)
                            detalii_after += (mp.ParameterName + " = " + mp.Value.ToString() + ", ");
                    }
                    catch { }

                    SaveLog(DateTime.Now, action, table, detalii_before, detalii_after);
                }
                catch (Exception exp)                 
                {
                    LogWriter.Log(exp.Message, SettingsClass.ErrorLogFile);
                }
                // END LOG ---
                return new object[] { true, "" };
            }
            catch (MySqlException mySqlException)
            {
                mySqlConnection.Close();
                //return new object[] { false, mySqlException.Message };
                LogWriter.Log(mySqlException.Message, SettingsClass.ErrorLogFile);
                throw new Exception(ExceptionParser.ParseException(mySqlException));
            }
        }

        public object[] ExecuteInsertQuery()
        {
            try
            {
                MySqlParameter _ID = new MySqlParameter();
                _ID.ParameterName = "_ID";
                _ID.DbType = DbType.Int32;
                _ID.Direction = ParameterDirection.Output;
                mySqlCommand.Parameters.Add(_ID);
                mySqlCommand.ExecuteNonQuery();
                mySqlConnection.Close();
                // LOG INFO --
                try
                {
                    string action = "";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("insert") > 0) action = "INSERT";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("update") > 0) action = "UPDATE";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("delete") > 0) action = "DELETE";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("import") > 0) action = "IMPORT";
                    string table = action != "IMPORT" ? mySqlCommand.CommandText.ToUpper().Replace("SP_", "").Replace(action, "") : mySqlCommand.CommandText.ToUpper().Replace("SP", "").Replace("REGULARIMPORT", "");
                    string detalii_before = "";
                    if (mySqlCommand.Parameters.Contains("_ID") && mySqlCommand.Parameters["_ID"].Direction != ParameterDirection.Output)
                    {
                        try
                        {
                            detalii_before = GetDetaliiBefore(table, Convert.ToInt32(mySqlCommand.Parameters["_ID"].Value));
                        }
                        catch { detalii_before = ""; }
                    }
                    string detalii_after = "";
                    try
                    {
                        foreach (MySqlParameter mp in mySqlCommand.Parameters)
                            detalii_after += (mp.ParameterName + " = " + mp.Value.ToString() + ", ");
                    }
                    catch { }

                    SaveLog(DateTime.Now, action, table, detalii_before, detalii_after);
                }
                catch (Exception exp) {
                    LogWriter.Log(exp.Message, SettingsClass.ErrorLogFile);
                }
                // END LOG ---
                return new object[] { true, "", mySqlCommand.Parameters["_ID"].Value };
            }
            catch (MySqlException mySqlException)
            {
                mySqlConnection.Close();
                //return new object[] { false, mySqlException.Message, null };
                LogWriter.Log(mySqlException.Message, SettingsClass.ErrorLogFile);
                throw new Exception(ExceptionParser.ParseException(mySqlException));
            }
        }

        public object ExecuteScalarQuery()
        {
            object toReturn = new object();
            try
            {
                toReturn = mySqlCommand.ExecuteScalar();
            }
            catch (MySqlException mySqlException)
            {
                mySqlException.ToString();
                toReturn = null;
            }
            mySqlConnection.Close();
            return toReturn;
        }

        public object[] GenerateMySqlParameters(DataTable _dt, object[] _object, int _action)
        {
            ArrayList _alist = new ArrayList();
            for (int i = 0; i < _dt.Columns.Count; i++)
            {
                DataColumn dc = _dt.Columns[i];
                if (!(dc.ColumnName.ToLower() == "id" && _action == 0))
                    _alist.Add(new MySqlParameter("_" + dc.ColumnName.ToUpper(), _object[i]));
                //else
                //    ;//_alist.Add(new MySqlParameter("_" + dc.ColumnName.ToUpper(), "-1"));

                /*
                if ((dc.ColumnName.ToLower() != "id" && _action == 0) || _action == 1)
                {
                    MySqlParameter mp = new MySqlParameter("_" + dc.ColumnName.ToUpper(), _object[i].ToString());
                    _alist.Add(mp);
                }
                */
                /*
                    MySqlParameter mp = new MySqlParameter("_" + dc.ColumnName.ToUpper(), _object[i].ToString());
                    if (dc.ColumnName.ToLower() == "id" && _action == 0)
                        mp.Direction = ParameterDirection.Output;
                    _alist.Add(mp);
                */
            }
            return _alist.ToArray();
        }

        public void SaveLog(DateTime _data, string _actiune, string _tabela, string _detalii_before, string _detalii_after)
        {
            SaveLog(_data, _actiune, _tabela, _detalii_before, _detalii_after, ID_UTILIZATOR);
        }

        public void SaveLog(DateTime _data, string _actiune, string _tabela, string _detalii_before, string _detalii_after, int _id_utilizator)
        {
            try
            {
                MySqlConnection mc = new MySqlConnection();
                //mc.ConnectionString = Convert.ToString(System.Configuration.ConfigurationSettingsClass.AppSettings["termodinamic.Properties.SettingsClass.termodinamicConnectionString"]);
                //mc.ConnectionString = ConfigurationManager.ConnectionStrings["termodinamic.Properties.SettingsClass.termodinamicConnectionString"].ConnectionString;
                mc.ConnectionString = SettingsClass.CompanyId != 0 ? SettingsClass.ConnectionStringCompany : SettingsClass.ConnectionString;
                MySqlCommand m = new MySqlCommand();
                m.Connection = mc;
                m.CommandType = CommandType.StoredProcedure;
                m.CommandText = "LOGsp_insert";
                MySqlParameter _DATA = new MySqlParameter("_LOG_DATE", _data);
                m.Parameters.Add(_DATA);
                MySqlParameter _ACTIUNE = new MySqlParameter("_ACTION_NAME", _actiune);
                m.Parameters.Add(_ACTIUNE);
                MySqlParameter _TABELA = new MySqlParameter("_TABLE_NAME", _tabela);
                m.Parameters.Add(_TABELA);
                MySqlParameter _DETALII_BEFORE = new MySqlParameter("_DETAILS_BEFORE", _detalii_before);
                m.Parameters.Add(_DETALII_BEFORE);
                MySqlParameter _DETALII_AFTER = new MySqlParameter("_DETAILS_AFTER", _detalii_after);
                m.Parameters.Add(_DETALII_AFTER);
                MySqlParameter _ID_UTILIZATOR = new MySqlParameter("_EMPLOYEE_ID", _id_utilizator);
                m.Parameters.Add(_ID_UTILIZATOR);
                mc.Open();
                m.ExecuteNonQuery();
                mc.Close();
            }
            catch (Exception exp) 
            {
                LogWriter.Log(exp.Message, SettingsClass.ErrorLogFile);
            }
        }

        public string GetDetaliiBefore(string _tabela, int _id)
        {
            string toReturn = "";
            MySqlConnection mc = new MySqlConnection();
            mc.ConnectionString = SettingsClass.CompanyId != 0 ? SettingsClass.ConnectionStringCompany : SettingsClass.ConnectionString;
            MySqlCommand m = new MySqlCommand();
            m.Connection = mySqlConnection;
            m.CommandType = CommandType.StoredProcedure;
            m.CommandText = _tabela + "sp_GetById";
            MySqlParameter _ID = new MySqlParameter("_ID", _id);
            m.Parameters.Add(_ID);
            mc.Open();
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(m);
                mySqlDataAdapter.Fill(ds);
            }
            catch (MySqlException mySqlException)
            {
                mySqlException.ToString();
                ds = null;
                //throw;
            }
            mc.Close();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    toReturn += (ds.Tables[0].Columns[i].ColumnName.ToUpper() + " = " + ds.Tables[0].Rows[0][i].ToString() + ", ");
            }
            return toReturn;
        }

        public object[] InsertRow(DataSet ds)
        {
            try
            {
                MySqlParameter _ID = new MySqlParameter();
                _ID.ParameterName = "_ID";
                _ID.DbType = DbType.Int32;
                _ID.Direction = ParameterDirection.Output;
                mySqlCommand.Parameters.Add(_ID);
                //mySqlCommand.ExecuteNonQuery();
                mySqlDataAdapter.InsertCommand = mySqlCommand;
                mySqlDataAdapter.Update(ds);

                mySqlConnection.Close();
                // LOG INFO --
                try
                {
                    string action = "";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("insert") > 0) action = "INSERT";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("update") > 0) action = "UPDATE";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("delete") > 0) action = "DELETE";
                    if (mySqlCommand.CommandText.ToLower().IndexOf("import") > 0) action = "IMPORT";
                    string table = action != "IMPORT" ? mySqlCommand.CommandText.ToUpper().Replace("SP_", "").Replace(action, "") : mySqlCommand.CommandText.ToUpper().Replace("SP", "").Replace("REGULARIMPORT", "");
                    string detalii_before = "";
                    if (mySqlCommand.Parameters.Contains("_ID") && mySqlCommand.Parameters["_ID"].Direction != ParameterDirection.Output)
                    {
                        try
                        {
                            detalii_before = GetDetaliiBefore(table, Convert.ToInt32(mySqlCommand.Parameters["_ID"].Value));
                        }
                        catch { detalii_before = ""; }
                    }
                    string detalii_after = "";
                    try
                    {
                        foreach (MySqlParameter mp in mySqlCommand.Parameters)
                            detalii_after += (mp.ParameterName + " = " + mp.Value.ToString() + ", ");
                    }
                    catch { }

                    SaveLog(DateTime.Now, action, table, detalii_before, detalii_after);
                }
                catch (Exception exp) 
                {
                    LogWriter.Log(exp.Message, SettingsClass.ErrorLogFile);
                }
                // END LOG ---
                return new object[] { ds, "", mySqlCommand.Parameters["_ID"].Value };
            }
            catch (MySqlException mySqlException)
            {
                mySqlConnection.Close();
                //return new object[] { null, mySqlException.Message, null };
                LogWriter.Log(mySqlException.Message, SettingsClass.ErrorLogFile);
                throw new Exception(ExceptionParser.ParseException(mySqlException));
            }
        }

        public DataTable SetTableDataSource(string table_name)
        {
            return (this.ExecuteSelectQuery()).Tables[table_name];
        }


        #region --- working with BindingSource ---

        public DataAccess(
            string select_command, 
            string insert_command, 
            string update_command, 
            string delete_command )
        {
            mySqlDataAdapter.RowUpdated += new MySqlRowUpdatedEventHandler(mySqlDataAdapter_RowUpdated);
            mySqlConnection.ConnectionString = SettingsClass.CompanyId != 0 ? SettingsClass.ConnectionStringCompany : SettingsClass.ConnectionString;
            selectCommand.Connection = mySqlConnection;
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.CommandText = select_command;

            insertCommand.Connection = mySqlConnection;
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.CommandText = insert_command;
            //insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            insertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;

            updateCommand.Connection = mySqlConnection;
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.CommandText = update_command;

            deleteCommand.Connection = mySqlConnection;
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.CommandText = delete_command;

            mySqlDataAdapter.SelectCommand = selectCommand;
            mySqlDataAdapter.UpdateCommand = updateCommand;
            mySqlDataAdapter.DeleteCommand = deleteCommand;
            mySqlDataAdapter.InsertCommand = insertCommand;

            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();
            //dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["ID"] };

            bindingSource.DataSource = dataTable;
        }

        public DataAccess(
            string select_command,
            object[] select_parameters,
            string insert_command,
            object[] insert_parameters,
            string update_command,
            object[] update_parameters,
            string delete_command,
            object[] delete_parameters)
        {
            mySqlDataAdapter.RowUpdated += new MySqlRowUpdatedEventHandler(mySqlDataAdapter_RowUpdated);
            mySqlConnection.ConnectionString = SettingsClass.CompanyId != 0 ? SettingsClass.ConnectionStringCompany : SettingsClass.ConnectionString;
            selectCommand.Connection = mySqlConnection;
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.CommandText = select_command;
            if (select_parameters != null)
                AttachSelectParams(select_parameters);

            insertCommand.Connection = mySqlConnection;
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.CommandText = insert_command;
            //insertCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
            insertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
            if (insert_parameters != null)
                AttachInsertParams(insert_parameters);

            updateCommand.Connection = mySqlConnection;
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.CommandText = update_command;
            if (update_parameters != null)
                AttachUpdateParams(update_parameters);

            deleteCommand.Connection = mySqlConnection;
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.CommandText = delete_command;
            if (delete_parameters != null)
                AttachDeleteParams(delete_parameters);

            mySqlDataAdapter.SelectCommand = selectCommand;
            mySqlDataAdapter.UpdateCommand = updateCommand;
            mySqlDataAdapter.DeleteCommand = deleteCommand;
            mySqlDataAdapter.InsertCommand = insertCommand;

            DataTable dataTable = new DataTable();
            mySqlDataAdapter.Fill(dataTable);
            mySqlConnection.Close();
            //dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["ID"] };

            bindingSource.DataSource = dataTable;
        }



        public void AttachSelectParams(object[] mySqlParams)
        {
            this.selectCommand.Parameters.Clear();
            this.selectCommand.Parameters.AddRange(mySqlParams);
        }

        public void AttachInsertParams(object[] mySqlParams)
        {
            this.insertCommand.Parameters.Clear();
            this.insertCommand.Parameters.AddRange(mySqlParams);
        }

        public void AttachUpdateParams(object[] mySqlParams)
        {
            this.updateCommand.Parameters.Clear();
            this.updateCommand.Parameters.AddRange(mySqlParams);
        }

        public void AttachDeleteParams(object[] mySqlParams)
        {
            this.deleteCommand.Parameters.Clear();
            this.deleteCommand.Parameters.AddRange(mySqlParams);
        }

        #endregion

        private void mySqlDataAdapter_RowUpdated(object sender, MySqlRowUpdatedEventArgs e)
        {
            //Console.Write(SettingsClass.EmployeeId);
            // LOG INFO --
            try
            {
                string action = "";
                if (e.Command.CommandText.ToLower().IndexOf("insert") > 0) action = "INSERT";
                if (e.Command.CommandText.ToLower().IndexOf("update") > 0) action = "UPDATE";
                if (e.Command.CommandText.ToLower().IndexOf("delete") > 0) action = "DELETE";
                if (e.Command.CommandText.ToLower().IndexOf("import") > 0) action = "IMPORT";
                string table = action != "IMPORT" ? e.Command.CommandText.ToUpper().Replace("SP_", "").Replace(action, "") : e.Command.CommandText.ToUpper().Replace("SP", "").Replace("REGULARIMPORT", "");
                string detalii_before = "";
                if (e.Command.Parameters.Contains("_ID") && e.Command.Parameters["_ID"].Direction != ParameterDirection.Output)
                {
                    try
                    {
                        detalii_before = GetDetaliiBefore(table, Convert.ToInt32(e.Command.Parameters["_ID"].Value));
                    }
                    catch { detalii_before = ""; }
                }
                string detalii_after = "";
                try
                {
                    foreach (MySqlParameter mp in e.Command.Parameters)
                        detalii_after += (mp.ParameterName + " = " + mp.Value.ToString() + ", ");
                }
                catch { }

                SaveLog(DateTime.Now, action, table, detalii_before, detalii_after, SettingsClass.EmployeeId);
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp.Message, SettingsClass.ErrorLogFile);
            }
            // END LOG ---
        }
    }
}