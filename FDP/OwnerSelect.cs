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
    public partial class OwnerSelect : UserForm
    {
        //public DataAccess daOwners = new DataAccess("OWNERSsp_select", "OWNERSsp_insert", "OWNERSsp_update", "OWNERSsp_delete");
        //public CheckedListBox columns = new CheckedListBox();
        public bool Selectable = false;
        //this.dataGrid1 = new DataGrid("OWNERSsp_select", null, "OWNERSsp_insert", null, "OWNERSsp_update", null, "OWNERSsp_delete", null, new string[] { "PASSPORT_EXPIRATION_DATE" }, null, null, new string[] { "STATUS_ID", "TYPE_ID", "CITY_ID" }, null, new string[] { "NAME", "FULL_NAME", "STATUS", "TYPE", "CIF", "CUI", "CNP", "COMMERCIAL_REGISTER_NUMBER", "CITY"}, false);
        public int IdToReturn
        {
            get;
            set;
        }

        public DataRow OldRow
        {
            get;
            set;
        }

        public OwnerSelect()
        {
            //base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            //Language.LoadLabels(this);
            //dataGridViewOwners.BindingContextChanged += new EventHandler(dataGridViewOwners_BindingContextChanged);
            //dataGridViewOwners.KeyDown += new KeyEventHandler(dataGridViewOwners_KeyDown);
            //dataGridViewOwners.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridViewOwners_DataBindingComplete);
            //dataGridViewOwners.DataSourceChanged += new EventHandler(dataGridViewOwners_DataSourceChanged);
            //toolStripStatusLabelShowAll.Click += new EventHandler(toolStripStatusLabelShowAll_Click);
            //this.dataGrid1.dataGridView.RowValidated += new DataGridViewCellEventHandler(dataGridView_RowValidated);
            base.Maximized = FormWindowState.Maximized;
        }

        public OwnerSelect(bool selectable)
        {
            //base.Maximized = FormWindowState.Maximized;
            Selectable = selectable;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            //Language.LoadLabels(this);
            //dataGridViewOwners.BindingContextChanged += new EventHandler(dataGridViewOwners_BindingContextChanged);
            //dataGridViewOwners.KeyDown += new KeyEventHandler(dataGridViewOwners_KeyDown);
            //dataGridViewOwners.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridViewOwners_DataBindingComplete);
            //dataGridViewOwners.DataSourceChanged += new EventHandler(dataGridViewOwners_DataSourceChanged);
            //toolStripStatusLabelShowAll.Click += new EventHandler(toolStripStatusLabelShowAll_Click);
            //Selectable = selectable;
            //buttonAddOwner.Visible = !Selectable;
            //buttonEdit.Visible = !Selectable;
            //buttonDeleteOwner.Visible = !Selectable;
            //buttonSelect.Visible = Selectable;
            //this.dataGrid1.dataGridView.RowValidated += new DataGridViewCellEventHandler(dataGridView_RowValidated);
            base.Maximized = FormWindowState.Maximized;
        }

        private void OwnerSelect_Load(object sender, EventArgs e)
        {
            //FillOwners();
            //GenerateColumnsListBox(dataGridViewOwners);
            //this.Maximized = FormWindowState.Maximized;
            //base.Opacity = 100;
        }

        private void dataGridView_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DateTime.Now.AddMonths(3) > Convert.ToDateTime(dataGrid1.dataGridView.Rows[e.RowIndex].Cells["passport_expiration_date"].Value))
                {
                    dataGrid1.dataGridView.Rows[e.RowIndex].ErrorText = Language.GetMessageBoxText("passportExpirationWarning", "Owner's password will expire in less than 3 months!");
                }
            }
            catch { }
        }


        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                if (DateTime.Now.AddMonths(3) > Convert.ToDateTime(dataGrid1.dataGridView.Rows[e.RowIndex].Cells["passport_expiration_date"].Value))
                {
                    dataGrid1.dataGridView.Rows[e.RowIndex].ErrorText = Language.GetMessageBoxText("passportExpirationWarning", "Owner's password will expire in less than 3 months!");
                }
            }
            catch { }
        }

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow owner = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new Owners(owner);
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
            Owners f = (Owners)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow owner = f.NewDR;
                    
                    try
                    {
                        owner["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["type"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["type_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_name1"] = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", owner["bank_id1"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_name2"] = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", owner["bank_id2"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_name3"] = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", owner["bank_id3"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_account_currency1"] = (owner["bank_account_currency_id1"] == DBNull.Value || owner["bank_account_currency_id1"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id1"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_account_currency2"] = (owner["bank_account_currency_id2"] == DBNull.Value || owner["bank_account_currency_id2"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id2"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_account_currency3"] = (owner["bank_account_currency_id3"] == DBNull.Value || owner["bank_account_currency_id3"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id3"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(owner.Table, owner.ItemArray, 0);
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(owner);
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
                DataRow owner = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
                OldRow = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
                for (int i = 0; i < OldRow.ItemArray.Length; i++)
                {
                    OldRow[i] = owner[i];
                }
                var f = new Owners(owner);
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
            Owners f = (Owners)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow owner = f.NewDR;
                    
                    try
                    {
                        owner["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["type"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["type_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_name1"] = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", owner["bank_id1"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_name2"] = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", owner["bank_id2"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_name3"] = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", owner["bank_id3"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_account_currency1"] = (owner["bank_account_currency_id1"] == DBNull.Value || owner["bank_account_currency_id1"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id1"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_account_currency2"] = (owner["bank_account_currency_id2"] == DBNull.Value || owner["bank_account_currency_id2"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id2"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        owner["bank_account_currency3"] = (owner["bank_account_currency_id3"] == DBNull.Value || owner["bank_account_currency_id3"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id3"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(owner.Table, owner.ItemArray, 1);
                    dataGrid1.da.AttachUpdateParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    InvoiceRequirementsClass.InsertFromOwner(owner, OldRow);
                    OldRow = null;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).RejectChanges();
            //f.Dispose();
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

        private void FillOwners()
        {
            dataGridViewOwners.DataSource = daOwners.bindingSource;
            foreach (DataGridViewColumn dgvc in dataGridViewOwners.Columns)
                if (dgvc.Name.ToLower() == "name" || dgvc.Name.ToLower() == "full_name" || dgvc.Name.ToLower() == "status" || dgvc.Name.ToLower() == "type")
                    dgvc.Visible = true;
                else
                    dgvc.Visible = false;  
        }

        private void dataGridViewOwners_DataSourceChanged(object sender, EventArgs e)
        {
            Language.PopulateGridColumnHeaders((DataGridView)sender);
        }

        private void dataGridViewOwners_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridViewOwners.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridViewOwners.Columns)
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
        private void dataGridViewOwners_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell = dataGridViewOwners.CurrentCell.OwningColumn.HeaderCell as DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        // Updates the filter status label. 
        private void dataGridViewOwners_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dataGridViewOwners);
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
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridViewOwners);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void buttonAddOwner_Click(object sender, EventArgs e)
        {
            bool SavedOrCancelled = false;
            DataRow owner = ((DataTable)((BindingSource)dataGridViewOwners.DataSource).DataSource).NewRow();
            while (!SavedOrCancelled)
            {
                var f = new Owners(owner);
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
                        owner = f.NewDR;
                        object[] mySqlParams = daOwners.GenerateMySqlParameters(owner.Table, owner.ItemArray, 0);
                        try{
                            owner["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["status_id"]) })).ExecuteScalarQuery().ToString();
                        }catch{}
                        try{
                            owner["type"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["type_id"]) })).ExecuteScalarQuery().ToString();
                        }catch{}
                        try{
                            owner["bank_account_currency1"] = (owner["bank_account_currency_id1"] == DBNull.Value || owner["bank_account_currency_id1"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id1"]) })).ExecuteScalarQuery().ToString();
                        }catch{}
                        try{
                            owner["bank_account_currency2"] = (owner["bank_account_currency_id2"] == DBNull.Value || owner["bank_account_currency_id2"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id2"]) })).ExecuteScalarQuery().ToString();
                        }catch{}
                        try{
                            owner["bank_account_currency3"] = (owner["bank_account_currency_id3"] == DBNull.Value || owner["bank_account_currency_id3"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id3"]) })).ExecuteScalarQuery().ToString();
                        }catch { }
                        if (((DataTable)daOwners.bindingSource.DataSource).Rows.IndexOf(owner) < 0)
                            ((DataTable)daOwners.bindingSource.DataSource).Rows.Add(owner);
                        daOwners.AttachInsertParams(mySqlParams);
                        daOwners.mySqlDataAdapter.Update(((DataTable)daOwners.bindingSource.DataSource));
                        ((DataTable)daOwners.bindingSource.DataSource).AcceptChanges();
                        SavedOrCancelled = true;
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    ((DataTable)daOwners.bindingSource.DataSource).RejectChanges();
                    SavedOrCancelled = true;
                }
                f.Dispose();
            }
        }

        public void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewOwners.SelectedRows.Count > 0)
            {
                bool SavedOrCancelled = false;
                DataRow owner = ((DataRowView)dataGridViewOwners.SelectedRows[0].DataBoundItem).Row;
                while (!SavedOrCancelled)
                {
                    var f = new Owners(owner);
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
                            owner = f.NewDR;
                            object[] mySqlParams = daOwners.GenerateMySqlParameters(owner.Table, owner.ItemArray, 1);
                            try
                            {
                                owner["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["status_id"]) })).ExecuteScalarQuery().ToString();
                            }
                            catch { }
                            try
                            {
                                owner["type"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["type_id"]) })).ExecuteScalarQuery().ToString();
                            }
                            catch { }
                            try
                            {
                                owner["bank_account_currency1"] = (owner["bank_account_currency_id1"] == DBNull.Value || owner["bank_account_currency_id1"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id1"]) })).ExecuteScalarQuery().ToString();
                            }
                            catch { }
                            try
                            {
                                owner["bank_account_currency2"] = (owner["bank_account_currency_id2"] == DBNull.Value || owner["bank_account_currency_id2"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id2"]) })).ExecuteScalarQuery().ToString();
                            }
                            catch { }
                            try
                            {
                                owner["bank_account_currency3"] = (owner["bank_account_currency_id3"] == DBNull.Value || owner["bank_account_currency_id3"] == null) ? "RON" : (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_id", new object[] { new MySqlParameter("_ID", owner["bank_account_currency_id3"]) })).ExecuteScalarQuery().ToString();
                            }
                            catch { }
                            daOwners.AttachUpdateParams(mySqlParams);
                            daOwners.mySqlDataAdapter.Update(((DataTable)daOwners.bindingSource.DataSource));
                            ((DataTable)daOwners.bindingSource.DataSource).AcceptChanges();
                            SavedOrCancelled = true;
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        ((DataTable)((BindingSource)dataGridViewOwners.DataSource).DataSource).RejectChanges();
                        SavedOrCancelled = true;
                    }
                    f.Dispose();
                }
            }
        }
        
        private void dataGridViewOwners_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //CheckedListBox columns = (CheckedListBox)((DataGridView)sender).Controls[String.Format("columns{0}", ((DataGridView)sender).Name)];
            columns.Visible = false;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (DataGridViewColumn col in dataGridViewOwners.Columns)
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

        private void dataGridViewOwners_MouseClick(object sender, MouseEventArgs e)
        {
            if (((DataGridView)sender).HitTest(e.X, e.Y).RowIndex != -1)
                columns.Visible = false;
        }

        private void columns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string column_name = columns.Items[e.Index].ToString().Trim().Replace(" ", "_");
            dataGridViewOwners.Columns[column_name].Visible = (e.NewValue==CheckState.Checked?true:false);
        }
        
        private void buttonDeleteOwner_Click(object sender, EventArgs e)
        {
            if (dataGridViewOwners.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        int key = Convert.ToInt32(dataGridViewOwners["id", dataGridViewOwners.SelectedRows[0].Index].Value);
                        daOwners.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGridViewOwners["id", dataGridViewOwners.SelectedRows[0].Index].Value) });
                        DataRow dr = ((DataTable)daOwners.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                        dr.Delete();
                        daOwners.mySqlDataAdapter.Update(((DataTable)daOwners.bindingSource.DataSource));
                        ((DataTable)daOwners.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        public void buttonSelect_Click(object sender, EventArgs e)
        {
            if (dataGridViewOwners.SelectedRows.Count > 0)
            {
                try
                {
                    IdToReturn = Convert.ToInt32(dataGridViewOwners[dataGridViewOwners.Columns["ID"].Index, dataGridViewOwners.SelectedRows[0].Index].Value);
                    this.buttonExit_Click(null, null);
                }
                catch { }
            }
            else
            {
                MessageBox.Show(Language.GetMessageBoxText("selectARecordFirst", "Please select a record!"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        */
    }
}
