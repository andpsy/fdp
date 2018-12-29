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
    public partial class Reminders : UserForm
    {
        //public string EditingTable = "contracts"; // the first page from the tab control
        public BindingSource OwnersWithExpiredPassportsSource = new BindingSource();
        public BindingSource UnpayedTenantsSource = new BindingSource();
        public DataRow OldRow
        {
            get;
            set;
        }

        public Reminders()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void Reminders_Load(object sender, EventArgs e)
        {
            this.checkBoxShowOnStartup.Checked = SettingsClass.LoadRemindersOnStartup;

            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_renewable");
            DataTable dtRenewableContracts = da.ExecuteSelectQuery().Tables[0];
            DataColumn dc = new DataColumn("GENERATE");
            dc.DataType = Type.GetType("System.Boolean");
            dc.DefaultValue = true;
            dtRenewableContracts.Columns.Add(dc);
            dataGridViewContracts.DataSource = dtRenewableContracts;
            dataGridViewContracts.Columns["GENERATE"].DisplayIndex = 0;
            foreach (DataGridViewColumn dgvc in dataGridViewContracts.Columns)
                if (dgvc.Name.ToLower() == "id" || dgvc.Name.IndexOf("_id") > -1)
                    dgvc.Visible = false;

            #region --- LOAD OWNERS WITH EXPIRING PASSPORTS ---
            da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_select_passport_expiration");
            DataTable dtOwnersWithExpiringPassports = da.ExecuteSelectQuery().Tables[0];
            /*
            dataGridViewPassportExpiration.DataSource = dtOwnersWithExpiringPassports;
            foreach (DataGridViewColumn dgvc in dataGridViewPassportExpiration.Columns)
                if (dgvc.Name.ToLower() == "id" || dgvc.Name.IndexOf("_id") > -1)
                    dgvc.Visible = false;
            */
            OwnersWithExpiredPassportsSource.DataSource = dtOwnersWithExpiringPassports;
            dataGrid1.Dispose();
            /*
            dataGrid1 = new DataGrid(
                OwnersWithExpiredPassportsSource,
                new string[] { "PASSPORT_EXPIRATION_DATE" },
                null,
                null,
                null,
                null,
                new string[] { "NAME", "FULL NAME", "NIF", "CNP", "PASSPORT_NUMBER", "PASSPORT_EXPIRATION_DATE", "PHONES", "EMAILS" },
                false,
                false);
            */
            dataGrid1 = new DataGrid("OWNERSsp_select_passport_expiration", null, "OWNERSsp_insert", null, "OWNERSsp_update", null, "OWNERSsp_delete", null, new string[] { "PASSPORT_EXPIRATION_DATE" }, null, null, new string[] { "STATUS_ID", "TYPE_ID", "CITY_ID" }, new string[] { "CO_OWNERS" }, new string[] { "NAME", "FULL NAME", "NIF", "CNP", "PASSPORT_NUMBER", "PASSPORT_EXPIRATION_DATE", "PHONES", "EMAILS" }, false, false);

            tabPagePassportExpirations.Controls.Add(dataGrid1);
            dataGrid1.Dock = DockStyle.Fill;
            foreach (DataGridViewColumn dgvc in dataGrid1.dataGridView.Columns)
                if (dgvc.Name.ToLower() == "id" || dgvc.Name.IndexOf("_id") > -1)
                    dgvc.Visible = false;
            dataGrid1.toolStripButtonAdd.Visible = false;
            dataGrid1.toolStripButtonDelete.Visible = false;
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            #endregion

            #region --- LOAD DUE DATE RENT PAYMENTS ---
            da = new DataAccess(CommandType.StoredProcedure, "TENANTSsp_select_unpayed");
            DataTable dtUnpayedTenants = da.ExecuteSelectQuery().Tables[0];
            UnpayedTenantsSource.DataSource = dtUnpayedTenants;
            DataGrid dataGrid2 = new DataGrid(
                UnpayedTenantsSource,
                null,
                null,
                null,
                null,
                null,
                new string[] { "ALL" },
                false,
                false);

            tabPageRentMgmt.Controls.Add(dataGrid2);
            dataGrid2.Dock = DockStyle.Fill;
            foreach (DataGridViewColumn dgvc in dataGrid2.dataGridView.Columns)
                if (dgvc.Name.ToLower() == "id" || dgvc.Name.IndexOf("_id") > -1)
                    dgvc.Visible = false;
            dataGrid2.toolStripButtonAdd.Visible = false;
            dataGrid2.toolStripButtonDelete.Visible = false;
            dataGrid2.toolStripButtonEdit.Visible = false;
            dataGrid2.toolStripButtonRefresh.Visible = false;
            dataGrid2.toolStripSeparator1.Visible = dataGrid2.toolStripSeparator2.Visible = dataGrid2.toolStripSeparator3.Visible = false;
            #endregion

        }
        /*
        private void tabControl_SelectedPageChanged(object sender, LidorSystems.IntegralUI.ObjectEventArgs e)
        {
            switch (tabControl.SelectedPage.Index)
            {
                case 0:
                    EditingTable = "contracts";
                    break;
                case 1:
                    EditingTable = "owners";
                    break;
                case 2:
                    EditingTable = "tenants";
                    break;
                case 3:
                    EditingTable = "tax";
                    break;
            }
        }
        */
        public void buttonEdit_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            //switch (EditingTable)
            switch(tabControl.SelectedPage.Name)
            {
                case "tabPagePassportExpirations":
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
                    break;
                case "tabPageRenewableContracts":
                    break;
            }
        }

        public void SaveEditRecord()
        {
            //switch (EditingTable)
            switch (tabControl.SelectedPage.Name)
            {
                case "tabPagePassportExpirations":
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
                    break;
                case "tabPageRenewableContracts":
                    break;
            }
        }


        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSaveContracts_Click(object sender, EventArgs e)
        {
            dataGridViewContracts.EndEdit();
            foreach (DataGridViewRow dgvr in dataGridViewContracts.Rows)
            {
                if (Convert.ToBoolean(dgvr.Cells["GENERATE"].Value == DBNull.Value ? false : dgvr.Cells["GENERATE"].Value))
                {
                    DataRow InitialDR = CommonFunctions.CopyDataRow((DataRow)dgvr.DataBoundItem);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_change_expiration_date", new object[]{
                        new MySqlParameter("_ID", dgvr.Cells["id"].Value),
                        new MySqlParameter("_EXPIRATION_DATE", dgvr.Cells["expiration_date"].Value)});
                    da.ExecuteUpdateQuery();

                    DataRow property = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[]{new MySqlParameter("_ID", dgvr.Cells["property_id"].Value)})).ExecuteSelectQuery().Tables[0].Rows[0];
                    InvoiceRequirementsClass.UpdateFromFDPContract((DataRow)dgvr.DataBoundItem, property, InitialDR, true);
                }
            }
        }

        private void checkBoxShowOnStartup_Click(object sender, EventArgs e)
        {
            SettingsClass.LoadRemindersOnStartup = ((CheckBox)sender).Checked;
        }
    }
}
