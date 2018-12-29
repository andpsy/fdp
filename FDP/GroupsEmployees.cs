using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace FDP
{
    public partial class GroupsEmployees : UserForm
    {
        public DataAccess da = new DataAccess();
        public int GroupId;
        public DataTable GroupsTable = new DataTable();
        public DataTable EmployeesTable = new DataTable();
        public DataTable GroupsEmployeesTable = new DataTable();


        public GroupsEmployees()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            //Language.LoadLabels(this);
        }

        private void GroupsEmployees_Load(object sender, EventArgs e)
        {
            FillGroups();
            if (listBoxGroups.Items.Count > 0)
                foreach (object x in listBoxGroups.Items)
                    if (Convert.ToInt32(((DataRowView)x)["id"]) == GroupId)
                    {
                        listBoxGroups.SetSelected(listBoxGroups.Items.IndexOf(x), true);
                        break;
                    }
            FillEmployees(GroupId);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillGroups()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "GROUPSsp_select");
            GroupsTable = da.ExecuteSelectQuery().Tables[0];
            listBoxGroups.DisplayMember = "name";
            listBoxGroups.ValueMember = "id";
            listBoxGroups.DataSource = GroupsTable;
        }

        private void FillEmployees(int group_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "EMPLOYEESsp_select_by_group_id", new object[] { new MySqlParameter("_GROUP_ID", group_id) });
            DataSet ds = da.ExecuteSelectQuery();
            EmployeesTable = ds.Tables[0];
            checkedListBoxExistingEmployees.DisplayMember = "name";
            checkedListBoxExistingEmployees.ValueMember = "id";
            checkedListBoxExistingEmployees.DataSource = EmployeesTable;

            GroupsEmployeesTable = ds.Tables[1];
            if (GroupsEmployeesTable == null || GroupsEmployeesTable.Rows.Count == 0)
                GroupsEmployeesTable = EmployeesTable.Clone();
            checkedListBoxGroupEmployees.DisplayMember = "name";
            checkedListBoxGroupEmployees.ValueMember = "id";
            checkedListBoxGroupEmployees.DataSource = GroupsEmployeesTable;

        }

        private void listBoxGroups_Click(object sender, EventArgs e)
        {
            if (((ListBox)sender).Items.Count > 0)
            {
                int key = Convert.ToInt32(((ListBox)sender).SelectedValue);
                GroupId = key;
                FillEmployees(key);
            }
        }

        private void listBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (((ListBox)sender).Items.Count > 0)
            {
                int key = Convert.ToInt32(((ListBox)sender).SelectedValue);
                GroupId = key;
                FillEmployees(key);
            }
            */
        }

        private void buttonEmployeeRight_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList aList = new ArrayList();

                foreach (object x in checkedListBoxExistingEmployees.CheckedItems)
                {
                    //DataRow dr_left = EmployeesTable.Select(String.Format("ID = {0}", ((DataRowView)x).Row["id"].ToString()))[0];
                    DataRow dr_left = ((DataRowView)x).Row;
                    DataRow dr_right = GroupsEmployeesTable.NewRow();
                    foreach (DataColumn dc in EmployeesTable.Columns)
                    {
                        //if (dc.ColumnName.ToLower() != "id")
                        dr_right[dc.ColumnName] = dr_left[dc.ColumnName];
                    }
                    GroupsEmployeesTable.Rows.Add(dr_right);
                    //dr_left.Delete();
                    aList.Add(dr_left);
                }

                foreach (DataRow dr in aList)
                {
                    dr.Delete();
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("unidentifiedError", "There was an unidentified error executed command:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEmployeesLeft_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList aList = new ArrayList();

                foreach (object x in checkedListBoxGroupEmployees.CheckedItems)
                {
                    DataRow dr_left = ((DataRowView)x).Row;
                    DataRow dr_right = EmployeesTable.NewRow();
                    foreach (DataColumn dc in EmployeesTable.Columns)
                    {
                        //if (dc.ColumnName.ToLower() != "id")
                        dr_right[dc.ColumnName] = dr_left[dc.ColumnName];
                    }
                    EmployeesTable.Rows.Add(dr_right);
                    //dr_left.Delete();
                    aList.Add(dr_left);
                }

                foreach (DataRow dr in aList)
                {
                    dr.Delete();
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("unidentifiedError", "There was an unidentified error executed command:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveGroupRoles_Click(object sender, EventArgs e)
        {
            try
            {
                //foreach (object x in checkedListBoxGroupEmployees.Items)
                foreach (DataRow dr in GroupsEmployeesTable.Rows)
                {
                    //DataRow dr = ((DataRowView)x).Row;
                    if (dr.RowState == DataRowState.Added)
                    {
                        //DataRow dr_init = EmployeesTable.GetChanges().Select(String.Format("FULL_NAME = '{0}'", dr["full_name"].ToString()))[0];
                        object[] mySqlParams = new object[] { new MySqlParameter("_GROUP_ID", GroupId), new MySqlParameter("_EMPLOYEE_ID", dr["id"]) };
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "GROUPS_EMPLOYEESsp_insert", mySqlParams);
                        da.ExecuteInsertQuery();
                    }
                }
                foreach (DataRow dr in EmployeesTable.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        object[] mySqlParams = new object[] { new MySqlParameter("_GROUP_ID", GroupId), new MySqlParameter("_EMPLOYEE_ID", dr["id"]) };
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "GROUPS_EMPLOYEESsp_delete", mySqlParams);
                        da.ExecuteUpdateQuery();
                    }
                }
                EmployeesTable.AcceptChanges();
                GroupsEmployeesTable.AcceptChanges();
                FillEmployees(GroupId);
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GroupsEmployees_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.Launcher != null && this.Launcher is Groups)
                {
                    ((Groups)this.Launcher).EnableDisableMenuButtons(true);
                }
            }
            catch { }
        }
    }
}