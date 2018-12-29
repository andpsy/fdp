using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using DataGridViewAutoFilter;
using MySql.Data.MySqlClient;

namespace FDP
{
    public partial class Groups : UserForm
    {
        public DataAccess da = new DataAccess("GROUPSsp_select", "GROUPSsp_insert", "GROUPSsp_update", "GROUPSsp_delete");

        public Groups()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            //Language.LoadLabels(this);
            dataGridViewGroups.BindingContextChanged += new EventHandler(dataGridViewGroups_BindingContextChanged);
            dataGridViewGroups.KeyDown += new KeyEventHandler(dataGridViewGroups_KeyDown);
            dataGridViewGroups.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridViewGroups_DataBindingComplete);
            dataGridViewGroups.DataSourceChanged += new EventHandler(dataGridViewGroups_DataSourceChanged);
            toolStripStatusLabelShowAll.Click += new EventHandler(toolStripStatusLabelShowAll_Click);
        }
        /*
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        */
        private void Groups_Load(object sender, EventArgs e)
        {
            /*
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "EMPLOYEESsp_select");
            GroupsDS = da.ExecuteSelectQuery();
            dataSource = new BindingSource(GroupsDS.Tables[0], null);
            */
            FillListContent();
        }

        private void dataGridViewGroups_DataSourceChanged(object sender, EventArgs e)
        {
            Language.PopulateGridColumnHeaders((DataGridView)sender);
        }

        private void dataGridViewGroups_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridViewGroups.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridViewGroups.Columns)
            {
                col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }

            // Format the OrderTotal column as currency. 
            //dataGridViewGroups.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            //dataGridViewGroups.AutoResizeColumns();
        }

        // Displays the drop-down list when the user presses 
        // ALT+DOWN ARROW or ALT+UP ARROW.
        private void dataGridViewGroups_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell = dataGridViewGroups.CurrentCell.OwningColumn.HeaderCell as DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        // Updates the filter status label. 
        private void dataGridViewGroups_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dataGridViewGroups);
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
        }

        // Clears the filter when the user clicks the "Show All" link
        // or presses ALT+A. 
        private void toolStripStatusLabelShowAll_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridViewGroups);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            EnableDisableMenuButtons(false);
            DynamicFormEmployees f = new DynamicFormEmployees("groups", ((DataTable)da.bindingSource.DataSource).NewRow(), 0);
            EditMode = 1; // ADD
            main m = FindMainForm();
            f.Launcher = this;
            this.ChildLaunched = f;
            f.TopLevel = false;
            f.MdiParent = m;
            m.panelMain.Controls.Add(f);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.BringToFront();
            f.Show();
            //f.ShowDialog();
        }

        public void SaveAddRecord()
        {
            DynamicFormEmployees f = (DynamicFormEmployees)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow dr = f.return_data_row;
                    object[] mySqlParams = da.GenerateMySqlParameters(dr.Table, dr.ItemArray, 0);
                    //DataAccess da = new DataAccess(CommandType.StoredProcedure, "EMPLOYEESsp_insert", mySqlParams);
                    //da.ExecuteInsertQuery();
                    //FillListContent();
                    ((DataTable)da.bindingSource.DataSource).Rows.Add(dr);
                    da.AttachInsertParams(mySqlParams);
                    //da.mySqlDataAdapter.Update(((DataTable)da.bindingSource.DataSource).GetChanges());
                    da.mySqlDataAdapter.Update(((DataTable)da.bindingSource.DataSource));
                    ((DataTable)da.bindingSource.DataSource).AcceptChanges();
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //df.Dispose();
            EnableDisableMenuButtons(true);
        }

        private void FillListContent()
        {
            dataGridViewGroups.DataSource = da.bindingSource;
            dataGridViewGroups.Columns["id"].Visible = false;
        }

        private void buttonDeleteGroup_Click(object sender, EventArgs e)
        {
            if (dataGridViewGroups.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        int key = Convert.ToInt32(dataGridViewGroups["id", dataGridViewGroups.SelectedRows[0].Index].Value);
                        da.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGridViewGroups["id", dataGridViewGroups.SelectedRows[0].Index].Value) });
                        //dataGridViewGroups.Rows.Remove(dataGridViewGroups.SelectedRows[0]);
                        //DataRow dr = ((DataTable)da.bindingSource.DataSource).Rows.Find(key);
                        DataRow dr = ((DataTable)da.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                        //((DataTable)da.bindingSource.DataSource).Rows.Remove(dr);
                        dr.Delete();
                        da.mySqlDataAdapter.Update(((DataTable)da.bindingSource.DataSource));
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

        private void buttonEditGroup_Click(object sender, EventArgs e)
        {
            EnableDisableMenuButtons(false);
            DataRowView drv = (DataRowView)(dataGridViewGroups.SelectedRows[0].DataBoundItem);
            DynamicFormEmployees f = new DynamicFormEmployees("groups", drv.Row, 1);
            EditMode = 2; // EDIT
            main m = FindMainForm();
            f.Launcher = this;
            this.ChildLaunched = f;
            f.TopLevel = false;
            f.MdiParent = m;
            m.panelMain.Controls.Add(f);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.BringToFront();
            f.Show();
            //f.ShowDialog();
        }

        public void SaveEditRecord()
        {
            DynamicFormEmployees f = (DynamicFormEmployees)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow dr = f.return_data_row;
                    object[] mySqlParams = da.GenerateMySqlParameters(dr.Table, dr.ItemArray, 1);
                    //DataAccess da = new DataAccess(CommandType.StoredProcedure, "EMPLOYEESsp_insert", mySqlParams);
                    //da.ExecuteInsertQuery();
                    //FillListContent();
                    da.AttachUpdateParams(mySqlParams);
                    //da.mySqlDataAdapter.Update(((DataTable)da.bindingSource.DataSource).GetChanges());
                    da.mySqlDataAdapter.Update(((DataTable)da.bindingSource.DataSource));
                    ((DataTable)da.bindingSource.DataSource).AcceptChanges();
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //f.Dispose();
            EnableDisableMenuButtons(true);
        }

        private void buttonSetGroupRights_Click(object sender, EventArgs e)
        {
            EnableDisableMenuButtons(false);
            var f = new GroupsRoles();
            f.Launcher = this;
            this.ChildLaunched = f;
            f.EditMode = 9;
            f.GroupId = Convert.ToInt32(dataGridViewGroups["id", dataGridViewGroups.SelectedRows[0].Index].Value);
            f.TopLevel = false;
            f.MdiParent = this.ParentForm;
            ((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
        }

        private void buttonAddEmployees_Click(object sender, EventArgs e)
        {
            EnableDisableMenuButtons(false);
            var f = new GroupsEmployees();
            f.Launcher = this;
            this.ChildLaunched = f;
            f.EditMode = 9;
            f.GroupId = Convert.ToInt32(dataGridViewGroups["id", dataGridViewGroups.SelectedRows[0].Index].Value);
            f.TopLevel = false;
            f.MdiParent = this.ParentForm;
            ((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.BringToFront();
            f.Show();
        }

        public void EnableDisableMenuButtons(bool _enabled)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button)
                {
                    ((Button)ctrl).Enabled = ctrl.Name == "buttonExit" ? true : _enabled;
                }
            }
        }
    }
}
