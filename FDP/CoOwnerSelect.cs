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
    public partial class CoOwnerSelect : UserForm
    {
        //public DataAccess daCoOwners = new DataAccess("CO_OWNERSsp_select", "CO_OWNERSsp_insert", "CO_OWNERSsp_update", " CO_OWNERSsp_delete");
        //public CheckedListBox columns = new CheckedListBox();
        public bool Selectable = false;

        public CoOwnerSelect()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            //Language.LoadLabels(this);

            //dataGridViewCoOwners.BindingContextChanged += new EventHandler(dataGridViewCoOwners_BindingContextChanged);
            //dataGridViewCoOwners.KeyDown += new KeyEventHandler(dataGridViewCoOwners_KeyDown);
            //dataGridViewCoOwners.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridViewCoOwners_DataBindingComplete);
            //dataGridViewCoOwners.DataSourceChanged += new EventHandler(dataGridViewCoOwners_DataSourceChanged);
            //toolStripStatusLabelShowAll.Click += new EventHandler(toolStripStatusLabelShowAll_Click);
        }

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow co_owner = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new CoOwners(co_owner);
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
            CoOwners f = (CoOwners)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow co_owner = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(co_owner.Table, co_owner.ItemArray, 0);
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(co_owner);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ((DataTable)dataGrid1.da.bindingSource.DataSource).RejectChanges();
            }
            //f.Dispose();
        }

        public void buttonEdit_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            if (dataGrid1.dataGridView.SelectedRows.Count > 0)
            {
                DataRow co_owner = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
                var f = new CoOwners(co_owner);
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
        }

        public void SaveEditRecord()
        {
            CoOwners f = (CoOwners)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow co_owner = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(co_owner.Table, co_owner.ItemArray, 1);
                    dataGrid1.da.AttachUpdateParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).RejectChanges();
            f.Dispose();
        }

        public void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid1.dataGridView.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        int key = Convert.ToInt32(dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value);
                        dataGrid1.da.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value) });
                        DataRow dr = ((DataTable)dataGrid1.da.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                        dr.Delete();
                        dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CoOwnerSelect_Load(object sender, EventArgs e)
        {
            //FillCoOwners();
            //GenerateColumnsListBox(dataGridViewCoOwners);
        }
        /*
        private void GenerateColumnsListBox(DataGridView dgv)
        {
            //CheckedListBox columns = new CheckedListBox();
            columns.Name = String.Format("columns{0}", dgv.Name);
            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                if (dgvc.Name.ToLower().IndexOf("id") == -1)
                {
                    columns.Items.Add(dgvc.HeaderText, dgvc.Visible);
                }
            }
            columns.Font = new Font(SettingsClass.FontTheme, 8, FontStyle.Regular);
            columns.BorderStyle = BorderStyle.Fixed3D;
            columns.Width = 150;
            columns.Height = 160;
            columns.Visible = false;
            dgv.Controls.Add(columns);
            columns.ItemCheck += new ItemCheckEventHandler(columns_ItemCheck);
        }

        private void FillCoOwners()
        {
            dataGridViewCoOwners.DataSource = daCoOwners.bindingSource;
            foreach (DataGridViewColumn dgvc in dataGridViewCoOwners.Columns)
                if (dgvc.Name.ToLower() == "name" || dgvc.Name.ToLower() == "full_name" || dgvc.Name.ToLower() == "status" || dgvc.Name.ToLower() == "type")
                    dgvc.Visible = true;
                else
                    dgvc.Visible = false;  
        }

        private void dataGridViewCoOwners_DataSourceChanged(object sender, EventArgs e)
        {
            Language.PopulateGridColumnHeaders((DataGridView)sender);
        }

        private void dataGridViewCoOwners_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridViewCoOwners.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridViewCoOwners.Columns)
            {
                col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
                ((DataGridViewAutoFilterColumnHeaderCell)col.HeaderCell).AutomaticSortingEnabled = false;
            }

            // Format the OrderTotal column as currency. 
            //dataGridViewGroups.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            //dataGridViewGroups.AutoResizeColumns();
        }

        // Displays the drop-down list when the user presses 
        // ALT+DOWN ARROW or ALT+UP ARROW.
        private void dataGridViewCoOwners_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell = dataGridViewCoOwners.CurrentCell.OwningColumn.HeaderCell as DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        // Updates the filter status label. 
        private void dataGridViewCoOwners_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dataGridViewCoOwners);
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
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridViewCoOwners);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAddCoOwner_Click(object sender, EventArgs e)
        {
            DataRow coowner = ((DataTable)((BindingSource)dataGridViewCoOwners.DataSource).DataSource).NewRow();
            var f = new CoOwners(coowner);
            //f.TopLevel = false;
            //f.MdiParent = this.ParentForm;
            //((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            //f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            //f.StartPosition = FormStartPosition.Manual;
            //f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    coowner = f.CoOwnerDR;
                    object[] mySqlParams = daCoOwners.GenerateMySqlParameters(coowner.Table, coowner.ItemArray, 0);
                    ((DataTable)daCoOwners.bindingSource.DataSource).Rows.Add(coowner);
                    daCoOwners.AttachInsertParams(mySqlParams);
                    daCoOwners.mySqlDataAdapter.Update(((DataTable)daCoOwners.bindingSource.DataSource));
                    ((DataTable)daCoOwners.bindingSource.DataSource).AcceptChanges();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ((DataTable)daCoOwners.bindingSource.DataSource).RejectChanges();
            }
            f.Dispose();
        }

        private void buttonEditCoOwner_Click(object sender, EventArgs e)
        {
            if (dataGridViewCoOwners.SelectedRows.Count > 0)
            {
                DataRow coowner = ((DataRowView)dataGridViewCoOwners.SelectedRows[0].DataBoundItem).Row;
                var f = new CoOwners(coowner);
                //f.TopLevel = false;
                //f.MdiParent = this.ParentForm;
                //((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
                //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                //f.Dock = DockStyle.Fill;
                //f.BringToFront();
                //f.StartPosition = FormStartPosition.CenterParent;
                //f.StartPosition = FormStartPosition.Manual;
                //f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        coowner = f.CoOwnerDR;
                        object[] mySqlParams = daCoOwners.GenerateMySqlParameters(coowner.Table, coowner.ItemArray, 1);
                        daCoOwners.AttachUpdateParams(mySqlParams);
                        daCoOwners.mySqlDataAdapter.Update(((DataTable)daCoOwners.bindingSource.DataSource));
                        ((DataTable)daCoOwners.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    ((DataTable)((BindingSource)dataGridViewCoOwners.DataSource).DataSource).RejectChanges();
                f.Dispose();
            }
        }

        private void dataGridViewCoOwners_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //CheckedListBox columns = (CheckedListBox)((DataGridView)sender).Controls[String.Format("columns{0}", ((DataGridView)sender).Name)];
            columns.Visible = false;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (DataGridViewColumn col in dataGridViewCoOwners.Columns)
                {
                    ((DataGridViewAutoFilterColumnHeaderCell)col.HeaderCell).AutomaticSortingEnabled = false;
                }
                //CheckedListBox columns = (CheckedListBox)((DataGridView)sender).Controls[String.Format("columns{0}", ((DataGridView)sender).Name)];
                Point p = ((DataGridView)sender).PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
                columns.Location = p.X > ((DataGridView)sender).Width - columns.Width ? new Point(((DataGridView)sender).Width - columns.Width, p.Y) : p;
                columns.Visible = true;
                columns.Focus();
                ((DataGridView)sender).InvalidateCell(((DataGridView)sender).CurrentCell);
                return;
            }
            else 
            {
                ((DataGridViewAutoFilterColumnHeaderCell)((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell).AutomaticSortingEnabled = true;
            }
        }

        private void dataGridViewCoOwners_MouseClick(object sender, MouseEventArgs e)
        {
            if (((DataGridView)sender).HitTest(e.X, e.Y).RowIndex != -1)
                columns.Visible = false;
        }

        private void columns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string column_name = columns.Items[e.Index].ToString().Trim().Replace(" ","_");
            dataGridViewCoOwners.Columns[column_name].Visible = (e.NewValue==CheckState.Checked?true:false);
        }

        private void buttonDeleteCoOwner_Click(object sender, EventArgs e)
        {
            if (dataGridViewCoOwners.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        int key = Convert.ToInt32(dataGridViewCoOwners["id", dataGridViewCoOwners.SelectedRows[0].Index].Value);
                        daCoOwners.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGridViewCoOwners["id", dataGridViewCoOwners.SelectedRows[0].Index].Value) });
                        DataRow dr = ((DataTable)daCoOwners.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                        dr.Delete();
                        daCoOwners.mySqlDataAdapter.Update(((DataTable)daCoOwners.bindingSource.DataSource));
                        ((DataTable)daCoOwners.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void buttonAddCoOwner_Click_1(object sender, EventArgs e)
        {
            DataRow coowner = ((DataTable)((BindingSource)dataGridViewCoOwners.DataSource).DataSource).NewRow();
            var f = new CoOwners(coowner);
            //f.TopLevel = false;
            //f.MdiParent = this.ParentForm;
            //((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            //f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            //f.StartPosition = FormStartPosition.Manual;
            //f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    coowner = f.CoOwnerDR;
                    object[] mySqlParams = daCoOwners.GenerateMySqlParameters(coowner.Table, coowner.ItemArray, 0);
                    ((DataTable)daCoOwners.bindingSource.DataSource).Rows.Add(coowner);
                    daCoOwners.AttachInsertParams(mySqlParams);
                    daCoOwners.mySqlDataAdapter.Update(((DataTable)daCoOwners.bindingSource.DataSource));
                    ((DataTable)daCoOwners.bindingSource.DataSource).AcceptChanges();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ((DataTable)daCoOwners.bindingSource.DataSource).RejectChanges();
            }
            f.Dispose();
        }

        private void buttonEditCoOwner_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewCoOwners.SelectedRows.Count > 0)
            {
                DataRow coowner = ((DataRowView)dataGridViewCoOwners.SelectedRows[0].DataBoundItem).Row;
                var f = new CoOwners(coowner);
                //f.TopLevel = false;
                //f.MdiParent = this.ParentForm;
                //((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
                //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                //f.Dock = DockStyle.Fill;
                //f.BringToFront();
                //f.StartPosition = FormStartPosition.CenterParent;
                //f.StartPosition = FormStartPosition.Manual;
                //f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        coowner = f.CoOwnerDR;
                        object[] mySqlParams = daCoOwners.GenerateMySqlParameters(coowner.Table, coowner.ItemArray, 1);
                        daCoOwners.AttachUpdateParams(mySqlParams);
                        daCoOwners.mySqlDataAdapter.Update(((DataTable)daCoOwners.bindingSource.DataSource));
                        ((DataTable)daCoOwners.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    ((DataTable)((BindingSource)dataGridViewCoOwners.DataSource).DataSource).RejectChanges();
                f.Dispose();
            }

        }
        */
    }
}
