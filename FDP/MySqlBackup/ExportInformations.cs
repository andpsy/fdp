using System;
using System.Collections.Generic;
using System.Text;

namespace MySql.Data.MySqlClient
{
    /// <summary>
    /// Informations and Settings of MySQL Database Export Process
    /// </summary>
    public class ExportInformations
    {
        private string _encryptionKey = "1234";
        private int _saltSize = 0;
        private Dictionary<string, string> _tableCustomSql = null;

        /// <summary>
        /// Gets or Sets the tables that will be exported and the SQL for selecting the rows of the tables. If the SQL is left blank, the SQL will replace by "SELECT * FROM tablename". Key = tablename, Value = SQL
        /// </summary>
        public Dictionary<string, string> TableCustomSql
        {
            get
            {
                return _tableCustomSql;
            }
            set
            {
                _tableCustomSql = value;
                if (_tableCustomSql == null)
                    return;
                foreach (KeyValuePair<string, string> kv in _tableCustomSql)
                {
                    if (kv.Value == null || kv.Value == "")
                    {
                        _tableCustomSql[kv.Key] = string.Format("SELECT * FROM `{0}`;", kv.Key);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or Sets the tables that will be exported
        /// </summary>
        public string[] TablesToBeExported
        {
            get
            {
                if (TableCustomSql == null || TableCustomSql.Count == 0)
                    return null;
                else
                {
                    string[] sa = new string[TableCustomSql.Count];
                    int count = -1;
                    foreach (KeyValuePair<string, string> kv in TableCustomSql)
                    {
                        count++;
                        sa[count] = kv.Key;
                    }
                    return sa;
                }
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    foreach (string s in value)
                    {
                        TableCustomSql.Add(s, string.Format("SELECT * FROM `{0}`;", s));
                    }
                }
                else
                {
                    TableCustomSql = null;
                }
            }
        }

        /// <summary>
        /// Gets or Sets a value indicates whether the Dump Time should recorded in dump file.
        /// </summary>
        public bool RecordDumpTime = true;

        /// <summary>
        /// Gets or Sets a value indicates whether the Export process should run in Asynchronous Mode.
        /// </summary>
        public bool AsynchronousMode = false;

        /// <summary>
        /// Gets or Sets a value indicates whether total rows should be calculated before Export Process starts.
        /// </summary>
        public bool CalculateTotalRowsFromDatabase = false;

        /// <summary>
        /// Gets or Sets a value indicates whether the MySqlConnection and MySqlCommand used should close and dispose after export process finished.
        /// </summary>
        public bool AutoCloseConnection = true;

        /// <summary>
        /// Gets or Sets a value indicates whether the Exported Dump File should be encrypted.
        /// </summary>
        public bool EnableEncryption = false;

        /// <summary>
        /// Gets or Sets the key or password used to encrypt the exported dump file.
        /// </summary>
        public string EncryptionKey
        {
            get
            {
                return _encryptionKey;
            }
            set
            {
                Methods methods = new Methods();
                _encryptionKey = methods.Sha2Hash(value);
                _saltSize = methods.GetSaltSize(_encryptionKey);
                methods = null;
            }
        }

        /// <summary>
        /// Gets the length of salt used in encryption.
        /// </summary>
        public int SaltSize
        {
            get
            {
                return _saltSize;
            }
        }

        /// <summary>
        /// Gets or Sets the full path and file name (dump file) that will be saved to. The output of the export process.
        /// </summary>
        public string FileName = "";

        /// <summary>
        /// Gets or Sets a value indicates whether the SQL statement of "CREATE DATABASE" should added into dump file.
        /// </summary>
        public bool AddCreateDatabase = false;

        /// <summary>
        /// Gets or Sets a value indicates whether the Table Structure (CREATE TABLE) should be exported.
        /// </summary>
        public bool ExportTableStructure = true;

        /// <summary>
        /// Gets or Sets a value indicates whether the value of auto-increment of each table should be reset to 1.
        /// </summary>
        public bool ResetAutoIncrement = false;

        /// <summary>
        /// Gets or Sets a value indicates whether the Rows should be exported.
        /// </summary>
        public bool ExportRows = true;

        /// <summary>
        /// Gets or Sets the Maximum Length allowd for multiple INSERT SQL statements to join. Default value is 1MB.
        /// </summary>
        public int MaxSqlLength = 1 * 1024 * 1024;

        /// <summary>
        /// Gets or Sets a value indicates whether the Stored Procedures should be exported.
        /// </summary>
        public bool ExportStoredProcedures = true;
        
        /// <summary>
        /// Gets or Sets a value indicates whether the Stored Functions should be exported.
        /// </summary>
        public bool ExportFunctions = true;

        /// <summary>
        /// Gets or Sets a value indicates whether the Stored Triggers should be exported.
        /// </summary>
        public bool ExportTriggers = true;

        /// <summary>
        /// Gets or Sets a value indicates whether the Stored Views should be exported.
        /// </summary>
        public bool ExportViews = true;

        /// <summary>
        /// Gets or Sets a value indicates whether the Stored Events should be exported.
        /// </summary>
        public bool ExportEvents = true;

        /// <summary>
        /// Gets or Sets a value indicates whether the output file should be compressed as zip file.
        /// </summary>
        public bool ZipOutputFile = false;

        public ExportCompleteArg CompleteArg = null;

        public ExportInformations()
        {
            
        }
    }
}
