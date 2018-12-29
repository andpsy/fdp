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
    public partial class RentContractSelect : UserForm
    {
        //public DataAccess daProperties = new DataAccess("PROPERTIESsp_select", "PROPERTIESsp_insert", "PROPERTIESsp_update", "PROPERTIESsp_delete");
        //this.dataGrid1 = new FDP.DataGrid("RENTCONTRACTSsp_select", null, "RENTCONTRACTSsp_insert", null, "RENTCONTRACTSsp_update", null, "RENTCONTRACTSsp_delete", null, new string[] { "START_DATE", "FINISH_DATE" }, new string[] { "RENT", "RENT_VAT_INCLUDED", "DEPOSIT" }, new string[] { "SPLITTING", "PROLONGATION", "REAL_ESTATE_AGENCY" }, new string[] { "STATUS_ID", "OWNER_ID", "PROPERTY_ID", "TENANT_ID", "REGISTERED_ID", "PAYMENT_LIMIT_ID" }, new string[] { "OWNER", "TENANT", "PROPERTY", "PARENT_RENTCONTRACT_NUMBER"}, new string[] { "NUMBER", "ADDENDUM_NUMBER", "ADDENDUM_DATE", "TENANT", "OWNER", "PROPERTY", "START_DATE", "FINISH_DATE", "CURRENCY", "STATUS", "SPLITTING", "PROLONGATION", "REGISTERED", "REGISTRY_NUMBER", "RENT", "RENT_VAT_INCLUDED", "DEPOSIT", "PARENT_RENTCONTRACT_NUMBER" }, this.Selectable, false);
        //this.dataGrid1 = new FDP.DataGrid("RENTCONTRACTSsp_select", null, "RENTCONTRACTSsp_insert", null, "RENTCONTRACTSsp_update", null, "RENTCONTRACTSsp_delete_chain", null, new string[] { "START_DATE", "FINISH_DATE" }, new string[] { "RENT", "RENT_VAT_INCLUDED", "DEPOSIT" }, new string[] { "SPLITTING", "PROLONGATION", "REAL_ESTATE_AGENCY" }, new string[] { "STATUS_ID", "OWNER_ID", "PROPERTY_ID", "TENANT_ID", "REGISTERED_ID", "PAYMENT_LIMIT_ID" }, new string[] { "OWNER", "TENANT", "PROPERTY", "PARENT_RENTCONTRACT_NUMBER" }, new string[] { "NUMBER", "ADDENDUM_NUMBER", "ADDENDUM_DATE", "TENANT", "OWNER", "PROPERTY", "START_DATE", "FINISH_DATE", "CURRENCY", "STATUS", "SPLITTING", "PROLONGATION", "REGISTERED", "REGISTRY_NUMBER", "RENT", "RENT_VAT_INCLUDED", "DEPOSIT", "PARENT_RENTCONTRACT_NUMBER" }, this.Selectable, false);

        public bool Selectable = false;

        public RentContractSelect()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.AddToolStripButton(Language.GetLabelText("ContractSelect.toolStripButtonAddAddendum", "Add addendum"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "62_p.png")), new EventHandler(buttonAddAddendum_Click), "toolStripButtonAddAddendum", 4);
            dataGrid1.dataGridView.RowsRemoved += new DataGridViewRowsRemovedEventHandler(dataGridView_RowsRemoved);
            dataGrid1.dataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView_RowsAdded);
            //Language.LoadLabels(this);
        }

        public RentContractSelect(bool selectable)
        {
            base.Maximized = FormWindowState.Maximized;
            Selectable = selectable;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.AddToolStripButton(Language.GetLabelText("ContractSelect.toolStripButtonAddAddendum", "Add addendum"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "62_p.png")), new EventHandler(buttonAddAddendum_Click), "toolStripButtonAddAddendum", 4);
            dataGrid1.dataGridView.RowsRemoved += new DataGridViewRowsRemovedEventHandler(dataGridView_RowsRemoved);
            dataGrid1.dataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView_RowsAdded);
            //Language.LoadLabels(this);
        }

        public RentContractSelect(int id, string parameter_name)
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.AddToolStripButton(Language.GetLabelText("ContractSelect.toolStripButtonAddAddendum", "Add addendum"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "62_p.png")), new EventHandler(buttonAddAddendum_Click), "toolStripButtonAddAddendum", 4);
            dataGrid1.dataGridView.RowsRemoved += new DataGridViewRowsRemovedEventHandler(dataGridView_RowsRemoved);
            dataGrid1.dataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView_RowsAdded);
            //Language.LoadLabels(this);
        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dataGrid1.dataGridView.Rows.Count <= 0)
                dataGrid1.toolStrip1.Items["toolStripButtonAddAddendum"].Enabled = false;
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGrid1.dataGridView.Rows.Count > 0)
                dataGrid1.toolStrip1.Items["toolStripButtonAddAddendum"].Enabled = true;
        }

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow contract = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new RentContracts(contract);
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
            RentContracts f = (RentContracts)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow contract = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(contract.Table, contract.ItemArray, 0);
                    
                    try
                    {
                        contract["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["registered"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["type_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["tenant"] = (new DataAccess(CommandType.StoredProcedure, "TENANTSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["tenant_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["parent_rentcontract_number"] = (new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_number_by_id", new object[] { new MySqlParameter("_ID", contract["parent_contract_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    
                    if (((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.IndexOf(contract) < 0)
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(contract);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    SavedOrCancelled = true;
                    f.SavedOrCancelled = true;
                    DataAccess da = new DataAccess();
                    foreach (DataRow dr in ((DataTable)((BindingSource)f.dataGridViewTenants.DataSource).DataSource).Select("Assigned = true"))
                    {
                        da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_TENANTSsp_insert", new object[]{
                                new MySqlParameter("_RENTCONTRACT_ID", contract["ID"]),
                                new MySqlParameter("_TENANT_ID", dr["ID"])
                            });
                        da.ExecuteInsertQuery();
                    }
                    InvoiceRequirementsClass.InsertFromRentContract(contract);

                    da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_change_status", new object[] { new MySqlParameter("_ID", contract["property_id"]), new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rented"), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery().ToString() ) });
                    da.ExecuteUpdateQuery();

                    for (int i = 0; i < f.checkedComboBoxChildProperties.Items.Count; i++)
                    {
                        CCBoxItem ci = (CCBoxItem)f.checkedComboBoxChildProperties.Items[i];
                        int _propertyId = ci.Value;
                        if (f.checkedComboBoxChildProperties.GetItemCheckState(i) == CheckState.Checked)
                        {
                            da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_PROPERTIESsp_insert", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract["ID"]), new MySqlParameter("_PROPERTY_ID", _propertyId) });
                            da.ExecuteUpdateQuery();

                            da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_change_status", new object[] { new MySqlParameter("_ID", _propertyId), new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rented"), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery().ToString()) });
                            da.ExecuteUpdateQuery();
                        }
                        if (f.checkedComboBoxChildProperties.GetItemCheckState(i) == CheckState.Unchecked)
                        {
                            da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_PROPERTIESsp_delete_by_params", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract["ID"]), new MySqlParameter("_PROPERTY_ID", _propertyId) });
                            da.ExecuteUpdateQuery();

                            da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_change_status", new object[] { new MySqlParameter("_ID", _propertyId), new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "For rent"), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery().ToString()) });
                            da.ExecuteUpdateQuery();
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Remove(contract);
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
            DataRow contract = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
            var f = new RentContracts(contract);
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
            RentContracts f = (RentContracts)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow contract = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(contract.Table, contract.ItemArray, 1);
                    
                    try
                    {
                        contract["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["registered"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["type_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["tenant"] = (new DataAccess(CommandType.StoredProcedure, "TENANTSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["tenant_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["parent_rentcontract_number"] = (new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_number_by_id", new object[] { new MySqlParameter("_ID", contract["parent_contract_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    
                    dataGrid1.da.AttachUpdateParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    SavedOrCancelled = true;
                    f.SavedOrCancelled = true;
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_TENANTSsp_delete_by_rentcontract_id", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract["ID"]) });
                    da.ExecuteUpdateQuery();
                    foreach (DataRow dr in ((DataTable)((BindingSource)f.dataGridViewTenants.DataSource).DataSource).Select("Assigned = true"))
                    {
                        da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_TENANTSsp_insert", new object[]{
                                new MySqlParameter("_RENTCONTRACT_ID", contract["ID"]),
                                new MySqlParameter("_TENANT_ID", dr["ID"])
                                });
                        da.ExecuteInsertQuery();
                    }
                    InvoiceRequirementsClass.UpdateFromRentContract(f.NewDR, true);

                    da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_change_status", new object[] { new MySqlParameter("_ID", contract["property_id"]), new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rented"), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery().ToString()) });
                    da.ExecuteUpdateQuery();

                    for (int i = 0; i < f.checkedComboBoxChildProperties.Items.Count; i++)
                    {
                        CCBoxItem ci = (CCBoxItem)f.checkedComboBoxChildProperties.Items[i];
                        int _propertyId = ci.Value;
                        if (f.checkedComboBoxChildProperties.GetItemCheckState(i) == CheckState.Checked)
                        {
                            da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_PROPERTIESsp_insert", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract["ID"]), new MySqlParameter("_PROPERTY_ID", _propertyId) });
                            da.ExecuteUpdateQuery();

                            da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_change_status", new object[] { new MySqlParameter("_ID", _propertyId), new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rented"), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery().ToString()) });
                            da.ExecuteUpdateQuery();
                        }
                        if (f.checkedComboBoxChildProperties.GetItemCheckState(i) == CheckState.Unchecked)
                        {
                            da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_PROPERTIESsp_delete_by_params", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract["ID"]), new MySqlParameter("_PROPERTY_ID", _propertyId) });
                            da.ExecuteUpdateQuery();

                            da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_change_status", new object[] { new MySqlParameter("_ID", _propertyId), new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "For rent"), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery().ToString()) });
                            da.ExecuteUpdateQuery();
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Remove(contract);
                }
            }
            else
            {
                ((DataTable)dataGrid1.da.bindingSource.DataSource).RejectChanges();
            }
            //f.Dispose();
        }

        public void buttonDelete_Click(object sender, EventArgs e)
        {
            //if (dataGrid1.dataGridView.SelectedRows[0].Index > -1)
            if (dataGrid1.dataGridView.SelectedRows.Count > 0)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        foreach (DataGridViewRow dgvr in dataGrid1.dataGridView.SelectedRows)
                        {
                            //int key = Convert.ToInt32(dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value);
                            int key = Convert.ToInt32(dataGrid1.dataGridView["id", dgvr.Index].Value);
                            //dataGrid1.da.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value) });
                            dataGrid1.da.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGrid1.dataGridView["id", dgvr.Index].Value) });
                            DataRow dr = ((DataTable)dataGrid1.da.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                            dr.Delete();
                            dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                        }
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        if (exp is MySqlException)
                        {
                            if (((MySqlException)exp).Number == 1451)
                            {
                                if (MessageBox.Show(String.Format("{0} {1}", ExceptionParser.ParseException(exp), Language.GetMessageBoxText("continueConfirm", "Continue?")), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    try
                                    {
                                        ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView.RowStateFilter = DataViewRowState.Deleted;
                                        for (int i = 0; i < ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView.Count; i++)
                                        {
                                            //DataRow dr = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView[0].Row;
                                            DataRow dr = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView[i].Row;
                                            //int key = Convert.ToInt32(((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView[0]["id"]);
                                            dr.ClearErrors();
                                            dr.RejectChanges();
                                            /*
                                            dataGrid1.da.deleteCommand.CommandText = "RENTCONTRACTSsp_delete_chain";
                                            dataGrid1.da.AttachDeleteParams(new object[] { new MySqlParameter("_ID", key) });
                                            dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                                            ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
                                            ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                                            */
                                        }
                                        //((DataTable)dataGrid1.da.bindingSource.DataSource).RejectChanges();
                                        ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
                                        for (int i = 0; i < ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView.Count; i++)
                                        {
                                            int key = Convert.ToInt32(((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView[i]["id"]);
                                            (new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_delete_chain", new object[] { new MySqlParameter("_ID", key) })).ExecuteUpdateQuery();
                                        }
                                        ((DataTable)dataGrid1.da.bindingSource.DataSource).Clear();
                                        dataGrid1.da.mySqlDataAdapter.Fill(((DataTable)dataGrid1.da.bindingSource.DataSource));
                                        dataGrid1.da.bindingSource.ResetBindings(false);
                                    }
                                    catch (Exception exp2)
                                    {
                                        LogWriter.Log(exp2.Message, SettingsClass.ErrorLogFile);
                                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp2)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                        else{
                            if (exp.GetType().Name == "DBConcurrencyException")
                            {
                                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRentContract", "There was an error deleting the selected record(s)! There are Invoices or Receipts associated with the selected contract(s). Error: \r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                dataGrid1.toolStripButtonRefresh_Click(this, null);
                            }
                        }
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        //MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void RentContractSelect_Load(object sender, EventArgs e)
        {
            //dataGrid1.AddLinkColumn("tenants", "tenants", dataGrid1.dataGridView.Columns["owner_id"].Index + 1);

            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            DataGridViewLinkColumn dgvc = (DataGridViewLinkColumn)dataGrid1.dataGridView.Columns["parent_rentcontract_number"];
            //dgvc.DefaultCellStyle.BackColor = System.Drawing.Color.PapayaWhip;
            dgvc.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            dgvc.LinkColor = System.Drawing.Color.Red;
            dgvc.VisitedLinkColor = System.Drawing.Color.Red;
            this.buttonAddAddendum.Visible = false;
            dataGrid1.toolStrip1.Items["toolStripButtonAddAddendum"].Enabled = (dataGrid1.dataGridView.Rows.Count > 0 && dataGrid1.dataGridView.SelectedRows[0].Cells["parent_contract_id"].Value.ToString().Trim() == "") ? true : false;
            dataGrid1.dataGridView.RowEnter += new DataGridViewCellEventHandler(dataGridView_RowEnter);
        }

        private void dataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //this.buttonAddAddendum.Enabled = (((DataGridView)sender).Rows[e.RowIndex].Cells["parent_contract_id"].Value.ToString().Trim() == "");
            dataGrid1.toolStrip1.Items["toolStripButtonAddAddendum"].Enabled = (((DataGridView)sender).Rows[e.RowIndex].Cells["parent_contract_id"].Value.ToString().Trim() == "");
        }

        /*
        private void AddLinkColumn(string language_id, string column_name, int insert_index)
        {
            DataGridViewLinkColumn dgvlc = new DataGridViewLinkColumn();
            dgvlc.UseColumnTextForLinkValue = true;
            dgvlc.Text = column_name;
            dgvlc.HeaderText = Language.GetColumnHeaderText(language_id.ToLower(), column_name.ToUpper());
            //dgvlc.DataPropertyName = dgvc.Name;
            dgvlc.ActiveLinkColor = Color.DarkOrange;
            dgvlc.LinkBehavior = LinkBehavior.SystemDefault;
            dgvlc.LinkColor = Color.DarkOrange;
            dgvlc.TrackVisitedState = false;
            dgvlc.VisitedLinkColor = Color.DarkOrange;
            dgvlc.Visible = true;
            dataGrid1.dataGridView.Columns.Insert(insert_index, dgvlc);
        }
        */

        private void buttonAddAddendum_Click(object sender, EventArgs e)
        {
            //SplashScreen.SplashScreen.ShowSplashScreen();
            SplashScreen.SplashScreen.ShowSplashScreen( ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow contract = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            int parent_contract_id = Convert.ToInt32( ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row["id"]);
            var f = new RentContracts(contract, Convert.ToInt32(parent_contract_id));
            EditMode = 4; // ADD ADENDUM
            main m = FindMainForm();
            f.Launcher = this;
            this.ChildLaunched = f;
            f.TopLevel = false;
            f.MdiParent = m;
            m.panelMain.Controls.Add(f);
            f.StartPosition = FormStartPosition.CenterScreen;
            //f.ShowDialog();
            f.BringToFront();
            f.Show();
        }

        public void SaveAdendumRecord()
        {
            RentContracts f = (RentContracts)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow contract = f.NewDR;

                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(contract.Table, contract.ItemArray, 0);
                    
                    try
                    {
                        contract["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["registered"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["type_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["tenant"] = (new DataAccess(CommandType.StoredProcedure, "TENANTSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["tenant_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["parent_rentcontract_number"] = (new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_number_by_id", new object[] { new MySqlParameter("_ID", contract["parent_contract_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    
                    if (((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.IndexOf(contract) < 0)
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(contract);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    SavedOrCancelled = true;
                    f.SavedOrCancelled = true;
                    int contract_id = Convert.ToInt32(((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).Select("id = max(id)")[0]["id"]);

                    if (f.ParentContractId > 0)
                    {
                        foreach (object li in f.listBoxReasons.SelectedItems)
                        {
                            int reason_id = Convert.ToInt32(((DataRowView)li)["id"]);
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_CONTRACTREASONSsp_insert", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract_id), new MySqlParameter("_CONTRACTREASON_ID", reason_id) });
                            da.ExecuteInsertQuery();
                        }
                        InvoiceRequirementsClass.UpdateFromRentContract(f.NewDR);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Remove(contract);
                }
            }
            else
            {
                ((DataTable)dataGrid1.da.bindingSource.DataSource).RejectChanges();
            }
            //f.Dispose();
        }
    }
}
