using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace FDP
{
    static class Rights
    {
        private static int employee_id = 0;
        public static int EmployeeId
        {
            get{return employee_id;}
            set{employee_id = value;}
        }
        public static DataSet RightsDS = new DataSet();
        public static DataTable UserRightsDT = new DataTable();
        public static DataTable GroupRightsDT = new DataTable();

        public static DataTable OwnerRights
        {
            get
            {
                return Rights.GetOwnersRights(SettingsClass.LoginOwnerId);
            }
        }

        public static bool HasVisualize(string role)
        {
            try
            {
                DataRow dr = UserRightsDT.Select(String.Format("NAME = '{0}'", role))[0];
                if (Convert.ToBoolean(dr["visualize"])) return true;
            }
            catch
            {
                return false;
            }
            try
            {
                DataRow dr = GroupRightsDT.Select(String.Format("NAME = '{0}'", role))[0];
                if (Convert.ToBoolean(dr["visualize"])) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static bool HasAdd(string role)
        {
            try
            {
                DataRow dr = UserRightsDT.Select(String.Format("NAME = '{0}'", role))[0];
                if (Convert.ToBoolean(dr["add"])) return true;
            }
            catch
            {
                return false;
            }
            try
            {
                DataRow dr = GroupRightsDT.Select(String.Format("NAME = '{0}'", role))[0];
                if (Convert.ToBoolean(dr["add"])) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static bool HasEdit(string role)
        {
            try
            {
                DataRow dr = UserRightsDT.Select(String.Format("NAME = '{0}'", role))[0];
                if (Convert.ToBoolean(dr["edit"])) return true;
            }
            catch
            {
                return false;
            }
            try
            {
                DataRow dr = GroupRightsDT.Select(String.Format("NAME = '{0}'", role))[0];
                if (Convert.ToBoolean(dr["edit"])) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static bool HasDelete(string role)
        {
            try
            {
                DataRow dr = UserRightsDT.Select(String.Format("NAME = '{0}'", role))[0];
                if (Convert.ToBoolean(dr["delete"])) return true;
            }
            catch
            {
                return false;
            }
            try
            {
                DataRow dr = GroupRightsDT.Select(String.Format("NAME = '{0}'", role))[0];
                if (Convert.ToBoolean(dr["delete"])) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static void InitializeRights(int employee_id)
        {
            EmployeeId = employee_id;
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "EMPLOYEESsp_get_rights", new object[] { new MySqlParameter("_ID", EmployeeId) });
            RightsDS = da.ExecuteSelectQuery();
            UserRightsDT = RightsDS.Tables[0];
            GroupRightsDT = RightsDS.Tables[1];
        }

        public static DataTable GetOwnersRights(int owner_id)
        {
            try
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTROLLS_OWNERS_RIGHTSsp_get_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", owner_id) });
                DataTable dt = da.ExecuteSelectQuery().Tables[0];
                return dt;
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                return null;
            }
        }

        public static bool GetOwnerRight(string control_name, string form_name)
        {
            try
            {
                if (OwnerRights.Select(String.Format("NAME='{0}' AND FORM='{1}'", control_name, form_name)).Length > 0)
                    return Convert.ToBoolean(OwnerRights.Select(String.Format("NAME='{0}' AND FORM='{1}'", control_name, form_name))[0]["visible"]);
                else
                    return true;
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                return false;
            }
        }
    }
}
