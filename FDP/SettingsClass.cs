using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace FDP
{
    static class SettingsClass
    {
        public static string PDFExportPath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "pdf"); }
        }

        public static string SettingsFilesPath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "Settings"); }
        }

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

        public static string LayoutsPath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "layouts"); }
        }

        public static string ImportPath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "import_xml"); }
        }

        public static string BankExtractPath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "BANK_EXTRACTS"); }
        }

        public static string DateFormat
        {
            get { return ConfigurationManager.AppSettings["DateFormat"]; }
        }

        public static string DoubleFormat
        {
            get { return ConfigurationManager.AppSettings["DoubleFormat"]; }
        }

        public static int RoundingDigits
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["RoundingDigits"]); }
        }

        public static string DoubleFormatFourDigits
        {
            get { return ConfigurationManager.AppSettings["DoubleFormatFourDigits"]; }
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

        public static string RemoteConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["remoteConnectionString"].ConnectionString; }
        }

        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["fdpConnectionString"].ConnectionString;}
        }

        public static string ConnectionStringDataBase
        {
            get {
                string[] tmp = ConfigurationManager.ConnectionStrings["fdpConnectionString"].ConnectionString.Split(';');
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (tmp[i].Split('=')[0].ToLower() == "database")
                        return tmp[i].Split('=')[1];
                }
                return null;
            }
        }

        public static string ConnectionStringServer
        {
            get
            {
                string[] tmp = ConfigurationManager.ConnectionStrings["fdpConnectionString"].ConnectionString.Split(';');
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (tmp[i].Split('=')[0].ToLower() == "server")
                        return tmp[i].Split('=')[1];
                }
                return null;
            }
        }

        public static string ConnectionStringPort
        {
            get
            {
                string[] tmp = ConfigurationManager.ConnectionStrings["fdpConnectionString"].ConnectionString.Split(';');
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (tmp[i].Split('=')[0].ToLower() == "port")
                        return tmp[i].Split('=')[1];
                }
                return null;
            }
        }

        public static string ConnectionStringUser
        {
            get
            {
                string[] tmp = ConfigurationManager.ConnectionStrings["fdpConnectionString"].ConnectionString.Split(';');
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (tmp[i].Split('=')[0].ToLower() == "user id")
                        return tmp[i].Split('=')[1];
                }
                return null;
            }
        }

        public static string ConnectionStringPassword
        {
            get
            {
                string[] tmp = ConfigurationManager.ConnectionStrings["fdpConnectionString"].ConnectionString.Split(';');
                for (int i = 0; i < tmp.Length; i++)
                {
                    if (tmp[i].Split('=')[0].ToLower() == "password")
                        return tmp[i].Split('=')[1];
                }
                return null;
            }
        }

        public static string ConnectionStringCompany
        {
            get;
            set;
        }

        public static string ImagePath
        {
            get { return Path.Combine(Environment.CurrentDirectory, "img"); }
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

        public static int LoginOwnerId
        {
            get;
            set;
        }

        public static string UserName
        {
            get;
            set;
        }

        public static int CompanyId
        {
            get;
            set;
        }

        public static string GetUserSetting(string setting_name, int employee_id)
        {
            object tmp = (new DataAccess(CommandType.StoredProcedure, "EMPLOYEES_USERSETTINGSsp_get_value", new object[] { 
                new MySql.Data.MySqlClient.MySqlParameter("_EMPLOYEE_ID", employee_id),
                new MySql.Data.MySqlClient.MySqlParameter("_USERSETTING_NAME", setting_name)
            })).ExecuteScalarQuery();
            return tmp.ToString();
        }

        public static void SetUserSetting(int employee_id, string setting_name, string setting_value)
        {
            (new DataAccess(CommandType.StoredProcedure, "EMPLOYEES_USERSETTINGSsp_update_by_setting_name", new object[] { 
                    new MySql.Data.MySqlClient.MySqlParameter("_EMPLOYEE_ID", employee_id), 
                    new MySql.Data.MySqlClient.MySqlParameter("_USERSETTING_NAME", setting_name),
                    new MySql.Data.MySqlClient.MySqlParameter("_VALUE", setting_value)               
                })).ExecuteUpdateQuery();
        }

        public static void RemoveUserSetting(int employee_id, string setting_name, string setting_value)
        {
            (new DataAccess(CommandType.StoredProcedure, "EMPLOYEES_USERSETTINGSsp_delete_by_setting_name", new object[] { 
                    new MySql.Data.MySqlClient.MySqlParameter("_EMPLOYEE_ID", employee_id), 
                    new MySql.Data.MySqlClient.MySqlParameter("_USERSETTING_NAME", setting_name),
                    new MySql.Data.MySqlClient.MySqlParameter("_VALUE", setting_value)               
                })).ExecuteUpdateQuery();
        }

        public static DataTable Settings()
        {
            try
            {
                DataSet Settings = new DataSet();
                Settings.ReadXml(Path.Combine(SettingsClass.SettingsFilesPath, "settings.xml"));
                return Settings.Tables[0];
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                return null;
            }
        }

        public static bool Autologin
        {
            get {
                //object tmp = (new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_get_value", new object[] { new MySql.Data.MySqlClient.MySqlParameter("_NAME", "AUTOLOGIN") })).ExecuteScalarQuery();
                //return tmp.ToString() == "1" ? true : false;
                /*
                object tmp = (new DataAccess(CommandType.StoredProcedure, "EMPLOYEES_USERSETTINGSsp_get_value", new object[] { 
                    new MySql.Data.MySqlClient.MySqlParameter("_EMPLOYEE_ID", SettingsClass.EmployeeId),
                    new MySql.Data.MySqlClient.MySqlParameter("_USERSETTING_NAME", "AUTOLOGIN")
                })).ExecuteScalarQuery();
                return tmp.ToString().ToLower() == "true" ? true : false;
                */

                /* --- DIN 21.08.2013 ---
                foreach (string key in ConfigurationManager.AppSettings.AllKeys)
                {
                    if (key == SettingsClass.CompanyId.ToString())
                    {
                        foreach(string pair in ConfigurationManager.AppSettings[key].Split(';'))
                        {
                            if(pair.Split('=')[0].ToLower() == "autologin")
                                return Convert.ToBoolean(pair.Split('=')[1]);
                        }
                    }
                }
                return false;
                */
                try {
                    return Convert.ToBoolean(Settings().Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0]["autologin"]);
                }
                catch
                {
                    return false;
                }
            }
            /*
            set {
                //(new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_update_by_name", new object[] { new MySql.Data.MySqlClient.MySqlParameter("_NAME", "AUTOLOGIN"), new MySql.Data.MySqlClient.MySqlParameter("_VALUE", value) })).ExecuteUpdateQuery();
                
                (new DataAccess(CommandType.StoredProcedure, "EMPLOYEES_USERSETTINGSsp_update_by_setting_name", new object[] { 
                    new MySql.Data.MySqlClient.MySqlParameter("_EMPLOYEE_ID", SettingsClass.EmployeeId), 
                    new MySql.Data.MySqlClient.MySqlParameter("_USERSETTING_NAME", "AUTOLOGIN"),
                    new MySql.Data.MySqlClient.MySqlParameter("_VALUE", value)               
                })).ExecuteUpdateQuery();
                
            }
            */
        }

        public static bool RememberName
        {
            get 
            {
                /*
                object tmp = (new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_get_value", new object[] { new MySql.Data.MySqlClient.MySqlParameter("_NAME", "REMEMBERNAME") })).ExecuteScalarQuery();
                return tmp == "1" ? true : false;
                */

                /* --- FROM 21.08.2013 ---
                foreach (string key in ConfigurationManager.AppSettings.AllKeys)
                {
                    if (key == SettingsClass.CompanyId.ToString())
                    {
                        foreach (string pair in ConfigurationManager.AppSettings[key].Split(';'))
                        {
                            if (pair.Split('=')[0].ToLower() == "remembername")
                                return Convert.ToBoolean(pair.Split('=')[1]);
                        }
                    }
                }
                return false;
                */
                try
                {
                    return Convert.ToBoolean(Settings().Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0]["remembername"]);
                }
                catch
                {
                    return false;
                }
            }
            /*
            set
            {
                (new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_update_by_name", new object[] { new MySql.Data.MySqlClient.MySqlParameter("_NAME", "REMEMBERNAME"), new MySql.Data.MySqlClient.MySqlParameter("_VALUE", value) })).ExecuteUpdateQuery();
            }
            */
        }

        public static string AutoName
        {
            get 
            {
                //return Convert.ToString((new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_get_value", new object[] { new MySql.Data.MySqlClient.MySqlParameter("_NAME", "AUTONAME") })).ExecuteScalarQuery()); 
                /* --- FROM 21.08.2013 ---
                foreach (string key in ConfigurationManager.AppSettings.AllKeys)
                {
                    if (key == SettingsClass.CompanyId.ToString())
                    {
                        foreach (string pair in ConfigurationManager.AppSettings[key].Split(';'))
                        {
                            if (pair.Split('=')[0].ToLower() == "autoname")
                                return Convert.ToString(pair.Split('=')[1]);
                        }
                    }
                }
                return null;
                */
                try
                {
                    return Convert.ToString(Settings().Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0]["autoname"]);
                }
                catch
                {
                    return null;
                }
            }
            /*
            set
            {
                (new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_update_by_name", new object[] { new MySql.Data.MySqlClient.MySqlParameter("_NAME", "AUTONAME"), new MySql.Data.MySqlClient.MySqlParameter("_VALUE", value) })).ExecuteUpdateQuery();
            }
            */
        }

        public static string AutoPassword
        {
            get 
            { 
                //return Convert.ToString((new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_get_value", new object[] { new MySql.Data.MySqlClient.MySqlParameter("_NAME", "AUTOPASSWORD") })).ExecuteScalarQuery()); 
                /* --- FROM 21.08.2013 ---
                foreach (string key in ConfigurationManager.AppSettings.AllKeys)
                {
                    if (key == SettingsClass.CompanyId.ToString())
                    {
                        foreach (string pair in ConfigurationManager.AppSettings[key].Split(';'))
                        {
                            if (pair.Split('=')[0].ToLower() == "autopassword")
                                return Convert.ToString(pair.Split('=')[1]);
                        }
                    }
                }
                return null;
                */
                try
                {
                    return Convert.ToString(Settings().Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0]["autopassword"]);
                }
                catch
                {
                    return null;
                }
            }
            /*
            set
            {
                (new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_update_by_name", new object[] { new MySql.Data.MySqlClient.MySqlParameter("_NAME", "AUTOPASSWORD"), new MySql.Data.MySqlClient.MySqlParameter("_VALUE", value) })).ExecuteUpdateQuery();
            }
            */
        }

        public static bool LoadRemindersOnStartup
        {
            get
            {
                try
                {
                    return Convert.ToBoolean(Settings().Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0]["loadremindersonstartup"]);
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    DataTable clone = SettingsClass.Settings().Copy();
                    DataRow dr = null;
                    try
                    {
                        //dr = SettingsClass.Settings().Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0];
                        dr = clone.Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0];
                    }
                    catch
                    {
                        throw new Exception("User settings not found!");
                    }
                    dr["loadremindersonstartup"] = value;
                    clone.AcceptChanges();
                    clone.WriteXml(Path.Combine(SettingsClass.SettingsFilesPath, "settings.xml"), XmlWriteMode.IgnoreSchema);
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    throw new Exception("Error saving user settings!");
                }

            }
        }



        public static DataTable CompanySettings
        {
            get
            {
                try
                {
                    return (new DataAccess(CommandType.StoredProcedure, "COMPANIES_SETTINGSsp_get_by_company_id", new object[] { new MySqlParameter("_COMPANY_ID", SettingsClass.CompanyId) })).ExecuteSelectQuery().Tables[0];
                }
                catch { return null; }
            }
        }

        public static string GetCompanySetting(string setting_name)
        {
            try
            {
                return SettingsClass.CompanySettings.Select(String.Format("name = '{0}'", setting_name))[0]["value"].ToString();
            }
            catch { return null; }
        }

    }
}
