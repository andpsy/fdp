using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
//using DataGridViewAutoFilter;

namespace FDP
{
    public partial class GroupsRoles : UserForm
    {
        public DataAccess da = new DataAccess();
        public DataTable GroupsTable = new DataTable();
        public DataTable RolesTable = new DataTable();
        public DataTable GroupsRolesTable = new DataTable();
        public int GroupId;

        public GroupsRoles()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            //Language.LoadLabels(this);
            dataGridViewGroupsRoles.BindingContextChanged += new EventHandler(dataGridViewGroupsRoles_BindingContextChanged);
            dataGridViewGroupsRoles.KeyDown += new KeyEventHandler(dataGridViewGroupsRoles_KeyDown);
            dataGridViewGroupsRoles.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridViewGroupsRoles_DataBindingComplete);
            dataGridViewGroupsRoles.DataSourceChanged += new EventHandler(dataGridViewGroupsRoles_DataSourceChanged);
            dataGridViewGroupsRoles.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridViewGroupsRoles_RowsAdded);
            dataGridViewGroupsRoles.RowsRemoved += new DataGridViewRowsRemovedEventHandler(dataGridViewGroupsRoles_RowsRemoved);
            toolStripStatusLabelShowAll.Click += new EventHandler(toolStripStatusLabelShowAll_Click);
        }

        private void dataGridViewGroupsRoles_RowsAdded(object sender, EventArgs e)
        {
            //CompareLists();
        }

        private void dataGridViewGroupsRoles_RowsRemoved(object sender, EventArgs e)
        {
            //CompareLists();
        }

        private void dataGridViewGroupsRoles_DataSourceChanged(object sender, EventArgs e)
        {
            Language.PopulateGridColumnHeaders((DataGridView)sender);
        }

        private void dataGridViewGroupsRoles_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridViewGroupsRoles.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridViewGroupsRoles.Columns)
            {
                col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }

            // Format the OrderTotal column as currency. 
            //dataGridViewEmployees.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            //dataGridViewEmployees.AutoResizeColumns();
        }

        // Displays the drop-down list when the user presses 
        // ALT+DOWN ARROW or ALT+UP ARROW.
        private void dataGridViewGroupsRoles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell = dataGridViewGroupsRoles.CurrentCell.OwningColumn.HeaderCell as DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        // Updates the filter status label. 
        private void dataGridViewGroupsRoles_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dataGridViewGroupsRoles);
            if (String.IsNullOrEmpty(filterStatus))
            {
                statusStrip1.Visible = false;
                toolStripStatusLabelShowAll.Visible = false;
                toolStripStatusLabelFilter.Visible = false;
            }
            else
            {
                statusStrip1.Visible = true;
                toolStripStatusLabelShowAll.Visible = true;
                toolStripStatusLabelFilter.Visible = true;
                toolStripStatusLabelFilter.Text = filterStatus;
            }

            foreach (DataGridViewColumn dgvc in dataGridViewGroupsRoles.Columns)
                if (dgvc.Name.ToLower().IndexOf("id") > -1)
                    dgvc.Visible = false;
        }

        // Clears the filter when the user clicks the "Show All" link
        // or presses ALT+A. 
        private void toolStripStatusLabelShowAll_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridViewGroupsRoles);
        }

        public GroupsRoles(int group_id)
            : this()
        {
            GroupId = group_id;
        }

        private void GroupsRoles_Load(object sender, EventArgs e)
        {
            FillGroups();
            if (listBoxGroups.Items.Count > 0)
                foreach (object x in listBoxGroups.Items)
                    if (Convert.ToInt32(((DataRowView)x)["id"]) == GroupId)
                    {
                        listBoxGroups.SetSelected(listBoxGroups.Items.IndexOf(x), true);
                        break;
                    }
            FillRoles();
            FillGroupsRoles(GroupId);
            CompareLists();
        }

        private void FillGroups()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "GROUPSsp_select");
            GroupsTable = da.ExecuteSelectQuery().Tables[0];
            listBoxGroups.DisplayMember = "name";
            listBoxGroups.ValueMember = "id";
            listBoxGroups.DataSource = GroupsTable;
        }

        private void FillRoles()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "ROLESsp_select");
            RolesTable = da.ExecuteSelectQuery().Tables[0];
            checkedListBoxRoles.DisplayMember = "name";
            checkedListBoxRoles.ValueMember = "id";
            checkedListBoxRoles.DataSource = RolesTable;
        }

        private void FillGroupsRoles(int group_id)
        {
            da = new DataAccess("GROUPS_ROLESsp_select_by_group_id", new object[] { new MySqlParameter("_GROUP_ID", group_id) }, "GROUPS_ROLESsp_insert", null, "GROUPS_ROLESsp_update", null, "GROUPS_ROLESsp_delete", null);
            //da = new DataAccess("EMPLOYEES_ROLESsp_select", "EMPLOYEES_ROLESsp_insert", "EMPLOYEES_ROLESsp_update", "EMPLOYEES_ROLESsp_delete");
            dataGridViewGroupsRoles.DataSource = da.bindingSource;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAddRole_Click(object sender, EventArgs e)
        {
            //DynamicFormLists df = new DynamicFormLists("employees_roles", EmployeesRolesTable.NewRow(), 0);
            //if (df.ShowDialog(this) == DialogResult.OK)
            //{
                try
                {
                    //DataRow dr = df.return_data_row;
                    foreach (object x in checkedListBoxRoles.CheckedItems)
                    {
                        int role_id = Convert.ToInt32(((DataRowView)x)["id"]);
                        if (((DataTable)this.da.bindingSource.DataSource).Select(string.Format("ROLE_ID = {0}", role_id)).Length == 0)
                        {
                            DataRow dr = ((DataTable)this.da.bindingSource.DataSource).NewRow();
                            dr["group_id"] = listBoxGroups.SelectedValue.ToString();
                            dr["role_id"] = ((DataRowView)x)["id"];
                            dr["group_name"] = (new DataAccess(CommandType.StoredProcedure, "GROUPSsp_get_by_id", new object[]{new MySqlParameter("_ID", Convert.ToInt32(listBoxGroups.SelectedValue))}).ExecuteSelectQuery()).Tables[0].Rows[0]["name"];
                            dr["role_name"] = (new DataAccess(CommandType.StoredProcedure, "ROLESsp_get_by_id", new object[]{new MySqlParameter("_ID", Convert.ToInt32( ((DataRowView)x)["id"] ))}).ExecuteSelectQuery()).Tables[0].Rows[0]["name"];
                            object[] mySqlParams = (new DataAccess()).GenerateMySqlParameters(dr.Table, dr.ItemArray, 0);
                            //DataAccess da = new DataAccess(CommandType.StoredProcedure, "EMPLOYEES_ROLESsp_insert", mySqlParams);
                            //da.ExecuteInsertQuery();
                            ((DataTable)da.bindingSource.DataSource).Rows.Add(dr);
                            da.AttachInsertParams(mySqlParams);
                            da.mySqlDataAdapter.Update(((DataTable)da.bindingSource.DataSource));
                            ((DataTable)da.bindingSource.DataSource).AcceptChanges();
                        }
                    }
                    //FillEmployeeRoles(Convert.ToInt32(listBoxEmployees.SelectedValue));
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            //}
            //df.Dispose();
        }

        private void buttonEditRole_Click(object sender, EventArgs e)
        {
            SwitchButtons(0);
        }

        private void buttonSaveGroupsRoles_Click(object sender, EventArgs e)
        {
            try
            {
                //foreach (DataGridViewRow dgvr in dataGridViewEmployeesRoles.Rows)
                foreach(DataRow dr in ((DataTable)da.bindingSource.DataSource).GetChanges().Rows)
                {
                    //DataRowView drv = (DataRowView)(dgvr.DataBoundItem);
                    //if (drv.Row.RowState == DataRowState.Modified)
                    //{
                        //DataRow dr = drv.Row;
                        object[] mySqlParams = da.GenerateMySqlParameters(dr.Table, dr.ItemArray, 1);
                        //DataAccess da = new DataAccess(CommandType.StoredProcedure, "EMPLOYEESsp_insert", mySqlParams);
                        //da.ExecuteInsertQuery();
                        //FillListContent();
                        da.AttachUpdateParams(mySqlParams);
                        //da.mySqlDataAdapter.Update(((DataTable)da.bindingSource.DataSource).GetChanges());
                        //da.mySqlDataAdapter.Update(((DataTable)da.bindingSource.DataSource));
                        da.mySqlDataAdapter.Update(new DataRow[]{dr});
                    //}
                }
                ((DataTable)da.bindingSource.DataSource).AcceptChanges();
                SwitchButtons(1);
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SwitchButtons(int action)
        {
            switch (action)
            {
                case 1:
                    buttonAddRole.Enabled = true;
                    buttonDeleteRole.Enabled = true;
                    buttonEditRole.Enabled = true;
                    buttonSaveGroupRoles.Visible = false;
                    buttonCancelSaveGroupsRoles.Visible = false;
                    dataGridViewGroupsRoles.ReadOnly = true;
                    break;
                case 0:
                    dataGridViewGroupsRoles.ReadOnly = false;
                    try
                    {
                        foreach (DataGridViewColumn dgvc in dataGridViewGroupsRoles.Columns)
                            if (dgvc.Name.ToLower() != "visualize" && dgvc.Name.ToLower() != "add" && dgvc.Name.ToLower() != "edit" && dgvc.Name.ToLower() != "delete")
                                dgvc.ReadOnly = true;
                    }
                    catch { }
                    buttonAddRole.Enabled = false;
                    buttonDeleteRole.Enabled = false;
                    buttonEditRole.Enabled = false;
                    buttonSaveGroupRoles.Visible = true;
                    buttonCancelSaveGroupsRoles.Visible = true;
                    break;
            }
        }

        private void buttonDeleteRole_Click(object sender, EventArgs e)
        {
            if (dataGridViewGroupsRoles.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        int key = Convert.ToInt32(dataGridViewGroupsRoles["id", dataGridViewGroupsRoles.SelectedRows[0].Index].Value);
                        da.AttachDeleteParams(new object[] { new MySqlParameter("_ID", key) });
                        //dataGridViewEmployeesRoles.Rows.Remove(dataGridViewEmployeesRoles.SelectedRows[0]);
                        //DataRow dr = ((DataTable)da.bindingSource.DataSource).Rows.Find(key);
                        DataRow dr = ((DataTable)da.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                        //((DataTable)da.bindingSource.DataSource).Rows.Remove(dr);
                        try
                        {
                            foreach (object x in checkedListBoxRoles.CheckedItems)
                            {
                                if (Convert.ToInt32(((DataRowView)x)["id"]) == Convert.ToInt32(dr["role_id"]))
                                {
                                    int index = checkedListBoxRoles.Items.IndexOf(x);
                                    checkedListBoxRoles.SetItemChecked(index, false);
                                    break;
                                }
                            }
                        }
                        catch { }
                        dr.Delete();
                        da.mySqlDataAdapter.Update(((DataTable)da.bindingSource.DataSource));
                        //da.mySqlDataAdapter.Update(((DataTable)da.bindingSource.DataSource).GetChanges(DataRowState.Detached));
                        //da.mySqlDataAdapter.Update(new DataRow[]{dr});
                        //da.bindingSource.ResetBindings(false);
                        ((DataTable)da.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void buttonCancelSaveGroupsRoles_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)(dataGridViewGroupsRoles.SelectedRows[0].DataBoundItem);
            DataRow dr = drv.Row;
            dr.RejectChanges();
            SwitchButtons(1);
        }

        private void listBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGroupsRoles( Convert.ToInt32(((ListBox)sender).SelectedValue));
        }

        private void CompareLists()
        {
            try
            {
                foreach (object x in checkedListBoxRoles.Items)
                    checkedListBoxRoles.SetItemChecked(checkedListBoxRoles.Items.IndexOf(x), false);
            }
            catch { }

            foreach (DataGridViewRow dgvr in dataGridViewGroupsRoles.Rows)
            {
                if (Convert.ToInt32(listBoxGroups.SelectedValue) == Convert.ToInt32(dataGridViewGroupsRoles["group_id", dgvr.Index].Value))
                {
                    try
                    {
                        foreach (object x in checkedListBoxRoles.Items)
                        {
                            if (Convert.ToInt32(((DataRowView)x)["id"]) == Convert.ToInt32(dataGridViewGroupsRoles["role_id", dgvr.Index].Value))
                            {
                                int index = checkedListBoxRoles.Items.IndexOf(x);
                                checkedListBoxRoles.SetItemChecked(index, true);
                                break;
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        private void GroupsRoles_FormClosing(object sender, FormClosingEventArgs e)
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
