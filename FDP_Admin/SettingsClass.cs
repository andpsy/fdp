using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace FDP_Admin
{
    static class SettingsClass
    {
        public static string DbTemplateFile
        {
            get { return Path.Combine(Environment.CurrentDirectory, "db_template", "db_template.sql"); }
        }

        public static string ErrorLogFile
        {
            get { return Path.Combine(Environment.CurrentDirectory, "logs", "Errors.log"); }
        }

        public static string ExcelExportPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); }
        }

        public static string DateFormat
        {
            get { return ConfigurationManager.AppSettings["DateFormat"]; }
        }

        public static string DoubleFormat
        {
            get { return ConfigurationManager.AppSettings["DoubleFormat"]; }
        }

        public static string ExchangeRatesFileUrl
        {
            get { return ConfigurationManager.AppSettings["ExchangeRatesFileUrl"]; }
        }

        public static string YearlyExchangeRatesFileUrl
        {
            get { return ConfigurationManager.AppSettings["YearlyExchangeRatesFileUrl"]; }
        }

        public static string ExchangeRatesFilePath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "exchangerates"); }
        }

        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["fdpConnectionString"].ConnectionString;}
        }

        public static string ConnectionStringCompany
        {
            get;
            set;
        }

        public static string Icons16ImagePath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "img", "icons", "16x16"); }
        }

        public static string Icons24ImagePath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "img", "icons", "24x24"); }
        }

        public static string FontTheme
        {
            get { return ConfigurationManager.AppSettings["FontTheme"]; }
        }

        public static int EmployeeId
        {
            get;
            set;
        }

        public static int CompanyId
        {
            get;
            set;
        }

        public static string MySqlToolsPath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "mysql_tools"); }
        }

        public static string MySqlToolsBackupBatchFile
        {
            get { return Path.Combine(Environment.CurrentDirectory, "mysql_tools", "batchscript.bat"); }
        }

        public static string MySqlToolsRestoreBatchFile
        {
            get { return Path.Combine(Environment.CurrentDirectory, "mysql_tools", "batchscript.bat"); }
        }

        public static string ConnectionStringServer
        {
            get {
                    foreach (string x in ConnectionString.Split(';'))
                    {
                        if (x.IndexOf("server") == 0)
                            return x.Split('=')[1].Trim(); ;
                    }
                    return "";
                }
        }

        public static string ConnectionStringPassword
        {
            get
            {
                foreach (string x in ConnectionString.Split(';'))
                {
                    if (x.IndexOf("password") == 0)
                        return x.Split('=')[1].Trim(); ;
                }
                return "";
            }
        }

        public static string ConnectionStringUser
        {
            get
            {
                foreach (string x in ConnectionString.Split(';'))
                {
                    if (x.IndexOf("user id") == 0)
                        return x.Split('=')[1].Trim(); ;
                }
                return "";
            }
        }

        public static string ConnectionStringPort
        {
            get
            {
                foreach (string x in ConnectionString.Split(';'))
                {
                    if (x.IndexOf("port") == 0)
                        return x.Split('=')[1].Trim(); ;
                }
                return "";
            }
        }

        public static string ConnectionStringDataBase
        {
            get
            {
                foreach (string x in ConnectionString.Split(';'))
                {
                    if (x.IndexOf("database") == 0)
                        return x.Split('=')[1].Trim(); ;
                }
                return "";
            }
        }

        public static string MySqlBackupPath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "backup"); }
        }
    }
}
