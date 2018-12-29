using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FDP
{
    public partial class Roles : UserForm
    {
        public DataTable TableList = new DataTable();
        public DataTable RolesTable = new DataTable();

        public Roles()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            //Language.LoadLabels(this);
        }

        private void Roles_Load(object sender, EventArgs e)
        {
            dataGridViewRoles.CellDoubleClick += new DataGridViewCellEventHandler(dataGridViewRoles_CellDoubleClick);
            dataGridViewRoles.DataSourceChanged += new EventHandler(dataGridViewRoles_DataSourceChanged);
            FillTableList();
            FillRolesList();
            CompareLists();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillTableList()
        {
            checkedListBoxAvailableTables.Items.Clear();
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "TABLESsp_show");
            TableList = da.ExecuteSelectQuery().Tables[0];
            foreach (DataRow dr in TableList.Rows)
                checkedListBoxAvailableTables.Items.Add(dr[0].ToString());
        }

        private void FillRolesList()
        {
            try
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "ROLESsp_select");
                RolesTable = da.ExecuteSelectQuery().Tables[0];
                dataGridViewRoles.DataSource = RolesTable;
            }
            catch { }
        }

        private void buttonAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                if (userTextBoxSpecialRole.Text != "")
                {
                    DataRow dr = RolesTable.NewRow();
                    dr["name"] = userTextBoxSpecialRole.Text;
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "ROLESsp_insert", new object[] { new MySqlParameter("_NAME", userTextBoxSpecialRole.Text) });
                    da.ExecuteInsertQuery();
                }
                foreach (object x in checkedListBoxAvailableTables.CheckedItems)
                {
                    if (RolesTable.Select(String.Format("NAME = '{0}'", x.ToString())).Length == 0)
                    {
                        DataRow dr = RolesTable.NewRow();
                        dr["name"] = x.ToString();
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "ROLESsp_insert", new object[]{ new MySqlParameter("_NAME", x.ToString()) });
                        da.ExecuteInsertQuery();
                    }
                }
                FillRolesList();
            }
            catch (Exception exp)
            {
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridViewRoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataRowView drv = (DataRowView)(((DataGridView)sender).Rows[e.RowIndex].DataBoundItem);
                CallUpdateRoles(drv.Row);
            }
        }

        private void dataGridViewRoles_DataSourceChanged(object sender, EventArgs e)
        {
            Language.PopulateGridColumnHeaders((DataGridView)sender);
        }

        private void CallUpdateRoles(DataRow _dr)
        {
            DynamicFormLists df = new DynamicFormLists("roles", _dr, 1);
            if (df.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    DataRow dr = df.return_data_row;
                    object[] mySqlParams = (new DataAccess()).GenerateMySqlParameters(dr.Table, dr.ItemArray, 1);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "ROLESsp_update", mySqlParams);
                    da.ExecuteUpdateQuery();
                    FillRolesList();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the selected record(s):\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            df.Dispose();
        }

        private void buttonEditRole_Click(object sender, EventArgs e)
        {
            if (dataGridViewRoles.SelectedRows[0].Index > -1)
            {
                DataRowView drv = (DataRowView)(dataGridViewRoles.SelectedRows[0].DataBoundItem);
                CallUpdateRoles(drv.Row);
            }

        }

        private void buttonDeleteRole_Click(object sender, EventArgs e)
        {
            if (dataGridViewRoles.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "ROLESsp_delete", new object[] { new MySqlParameter("_ID", dataGridViewRoles["id", dataGridViewRoles.SelectedRows[0].Index].Value) });
                        da.ExecuteUpdateQuery();
                        FillRolesList();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void CompareLists()
        {
            foreach (DataGridViewRow dgvr in dataGridViewRoles.Rows)
            {
                try
                {
                    int index = checkedListBoxAvailableTables.Items.IndexOf(dataGridViewRoles["name", dgvr.Index].Value);
                    if (index > -1)
                        checkedListBoxAvailableTables.SetItemChecked(index, true);
                }
                catch { }
            }
        }
    }
}
