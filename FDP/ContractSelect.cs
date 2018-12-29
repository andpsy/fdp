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
    public partial class ContractSelect : UserForm
    {
        //public DataAccess daProperties = new DataAccess("PROPERTIESsp_select", "PROPERTIESsp_insert", "PROPERTIESsp_update", "PROPERTIESsp_delete");
        //this.dataGrid1 = new FDP.DataGrid("CONTRACTSsp_select", null, "CONTRACTSsp_insert", null, "CONTRACTSsp_update", null, "CONTRACTSsp_delete", null, new string[] { "START_DATE", "FINISH_DATE", "EXPIRATION_DATE" }, null, new string[] { "AUTOMATICALLY_RENEWED" }, new string[] { "STATUS_ID", "EXPIRED_ID" }, new string[] { "OWNER", "PARENT_CONTRACT_NUMBER" }, new string[] { "NUMBER", "OWNER", "START_DATE", "FINISH_DATE", "CURRENCY", "STATUS", "EXPIRED", "EXPIRATION_DATE", "AUTOMATICALLY_RENEWED", "PARENT_CONTRACT_NUMBER" }, this.Selectable, false);
        //this.dataGrid1 = new FDP.DataGrid("CONTRACTSsp_select", null, "CONTRACTSsp_insert", null, "CONTRACTSsp_update", null, "CONTRACTSsp_delete", null, new string[] { "START_DATE", "FINISH_DATE", "EXPIRATION_DATE" }, null, new string[] { "AUTOMATICALLY_RENEWED" }, new string[] { "STATUS_ID", "EXPIRED_ID" }, new string[] { "OWNER", "PARENT_CONTRACT_NUMBER" }, new string[] { "ALL" }, this.Selectable, false);
        //this.dataGrid1 = new FDP.DataGrid("CONTRACTSsp_select", null, "CONTRACTSsp_insert", null, "CONTRACTSsp_update", null, "CONTRACTSsp_delete_chain", null, new string[] { "START_DATE", "FINISH_DATE", "EXPIRATION_DATE" }, null, new string[] { "AUTOMATICALLY_RENEWED" }, new string[] { "STATUS_ID", "EXPIRED_ID" }, new string[] { "OWNER", "PARENT_CONTRACT_NUMBER" }, new string[] { "ALL" }, this.Selectable, false);

        public bool Selectable = false;
        //public int EditMode
        //{
        //    get;
        //    set;
        //} // 1 = ADD, 2 = EDIT

        public ContractSelect()
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

        public ContractSelect(bool selectable)
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
        /*
        public ContractSelect(int id, string parameter_name)
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
            switch (parameter_name.ToLower())
            {
                case "contract_id":
                    dataGrid1.da = new DataAccess("PROPERTIESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", id) }, "PROPERTIESsp_insert", null, "PROPERTIESsp_update", null, "PROPERTIESsp_delete", null);
                    //dataGrid1.da.selectCommand.CommandText = "PROPERTIESsp_select_by_contract_id";
                    //dataGrid1.da.AttachSelectParams(new object[] { new MySqlParameter("_CONTRACT_ID", id) });
                    dataGrid1.dataGridView.DataSource = dataGrid1.da.bindingSource;
                    break;
            }
        }
        */
        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dataGrid1.dataGridView.Rows.Count <= 0 || dataGrid1.dataGridView.SelectedRows.Count <= 0)
                dataGrid1.toolStrip1.Items["toolStripButtonAddAddendum"].Enabled = false;
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGrid1.dataGridView.Rows.Count > 0 || dataGrid1.dataGridView.SelectedRows.Count <= 0)
                dataGrid1.toolStrip1.Items["toolStripButtonAddAddendum"].Enabled = true;
        }

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            //bool SavedOrCancelled = false;
            DataRow contract = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            //while (!SavedOrCancelled)
            //{
                EditMode = 1; // ADD
                var f = new Contracts(contract);
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
            //}
        }

        public void SaveAddRecord()
        {
            //Contracts f = (Contracts)this.MdiChildren[0];
            //Contracts f = (Contracts)this.OwnedForms[0];
            //Contracts f = (Contracts)((main)FindMainForm()).Controls.Find("Contracts", true)[0];
            Contracts f = (Contracts)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
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
                        //contract["expired"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["expired_id"]) })).ExecuteScalarQuery().ToString();
                        contract["signed?"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["expired_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["parent_contract_number"] = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_number_by_id", new object[] { new MySqlParameter("_ID", contract["parent_contract_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICESsp_get_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract["id"]) })).ExecuteSelectQuery().Tables[0];
                        foreach (DataRow service in contract_services.Rows)
                        {
                            foreach (DataColumn dc in contract.Table.Columns)
                            {
                                if (service["service"].ToString().ToLower().Replace(" ", "_") == dc.ColumnName.ToLower())
                                {
                                    contract[dc.ColumnName] = true;
                                    break;
                                }
                            }
                        }
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
                    string contract_status = Convert.ToString(contract["status"]).ToLower();
                    DateTime contract_start_date = Convert.ToDateTime(contract["start_date"]);
                    DateTime contract_finish_date = Convert.ToDateTime(contract["finish_date"]);
                    DateTime contract_expiration_date = Convert.ToDateTime(contract["expiration_date"]);
                    bool contract_automatically_renewed = contract["automatically_renewed"] == null || contract["automatically_renewed"] == DBNull.Value ? false : Convert.ToBoolean(contract["automatically_renewed"]);
                    bool use_expiration_date = Convert.ToBoolean(contract["use_expiration_date"] == DBNull.Value || contract["use_expiration_date"] == null ? false : contract["use_expiration_date"]);
                    ArrayList months_years = contract_automatically_renewed ? CommonFunctions.DateDifferenceInYears(use_expiration_date ? contract_expiration_date : contract_finish_date, contract_start_date) : CommonFunctions.DateDifferenceInYears(contract_finish_date, contract_start_date);

                    foreach (DataGridViewRow dgvr in f.dataGridViewProperties.Rows)
                    {
                        if (Convert.ToBoolean(dgvr.Cells["Assigned"].Value == DBNull.Value ? false : dgvr.Cells["Assigned"].Value))
                        {
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIESsp_insert", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id), new MySqlParameter("_PROPERTY_ID", dgvr.Cells["id"].Value) });
                            object[] returned = da.ExecuteInsertQuery();
                            int _contract_property_id = Convert.ToInt32(returned[2]);

                            //foreach (DataGridViewRow dgvr_service in f.dataGridViewContractServices.Rows)
                            foreach (DataRow dr_service in f.ContractServices.Select(
                                (dgvr.Cells["id"].Value == DBNull.Value || dgvr.Cells["id"].Value == null) ?
                                    String.Format("IsNull(PROPERTY_ID, -1) = -1") :
                                    String.Format("PROPERTY_ID = {0}", dgvr.Cells["id"].Value.ToString())
                                    ))
                            {
                                try
                                {
                                    {
                                        da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIES_SERVICESsp_insert", new object[] { 
                                                new MySqlParameter("_CONTRACT_PROPERTY_ID", _contract_property_id),
                                                new MySqlParameter("_SERVICE_ID", dr_service["service_id"]),
                                                new MySqlParameter("_VALUE", dr_service["value"]),
                                                new MySqlParameter("_PERIOD", dr_service["period"]),
                                                new MySqlParameter("_PERCENT", dr_service["percent"]),
                                                new MySqlParameter("_MODIFY", dr_service["modify"]),
                                                new MySqlParameter("_NOT_INVOICEABLE", dr_service["not_invoiceable"])
                                            });
                                        DataRow cps = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                        try
                                        {
                                            //foreach (DataRow dr in f.ServicesAdditionalCosts.Rows)
                                            //foreach (DataRow dr in f.ServicesAdditionalCosts.Select(String.Format("service_id = {0} and period = {1} and percent = {2}", dgvr_service.Cells["service"].Value.ToString(), dgvr_service.Cells["period"].Value.ToString(), dgvr_service.Cells["percent"].Value.ToString())))
                                            foreach (DataRow dr in f.ServicesAdditionalCosts.Select(String.Format("service_id = {0} and period = {1} and percent = {2} and {3}", dr_service["service_id"].ToString(), dr_service["period"].ToString(), dr_service["percent"].ToString(), ((dgvr.Cells["id"].Value == DBNull.Value || dgvr.Cells["id"].Value == null) ? "IsNull(property_id, -1) = -1" : String.Format("property_id = {0}", dgvr.Cells["id"].Value.ToString())))))
                                            {
                                                try
                                                {
                                                    (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_delete_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", dr["ID"]) })).ExecuteUpdateQuery();
                                                    (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_delete_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", dr["ID"]) })).ExecuteUpdateQuery();
                                                    (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_delete_by_contractservice", new object[] { new MySqlParameter("_CONTRACTSERVICE_ID", dr["CONTRACTPROPERTYSERVICE_ID"]) })).ExecuteUpdateQuery();
                                                }
                                                catch { }
                                                DataRow csad = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_insert", new object[] { 
                                                    new MySqlParameter("_CONTRACT_PROPERTY_SERVICE_ID", cps["id"]),
                                                    new MySqlParameter("_DESCRIPTION", dr["description"]),
                                                    new MySqlParameter("_VALUE", dr["value"])
                                                    })).ExecuteSelectQuery().Tables[0].Rows[0];

                                                if (contract_status == "active")
                                                {
                                                    foreach (int[] months_year in months_years)
                                                    {
                                                        DateTime start_date = new DateTime(months_year[1], contract_start_date.Month, contract_start_date.Day);
                                                        DateTime end_date = start_date.AddMonths(months_year[0]);


                                                        double value = 0;
                                                        //switch (dgvr_service.Cells["service"].EditedFormattedValue.ToString().ToLower())
                                                        string service = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", dr_service["service_id"].ToString().ToLower()) })).ExecuteScalarQuery().ToString().ToLower();
                                                        //switch (dr_service["service_id"].ToString().ToLower())
                                                        switch (service)
                                                        {
                                                            case "rent":
                                                                if ((months_years.IndexOf(months_year) == 0 && !use_expiration_date) || use_expiration_date)
                                                                {
                                                                    if (Convert.ToBoolean(dr_service["percent"] == DBNull.Value || dr_service["percent"] == null ? false : dr_service["percent"]))
                                                                    {
                                                                        value = Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(dr_service["value"]) / 100, 2);
                                                                        double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        double _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                    }
                                                                    else // FROM 30.01.2013 - FOR THE CASES WHEN RENT IS A FIX VALUE, NOT A PERCENT FROM THE RENT ! - BUG #1 / 29.01.2013
                                                                    {
                                                                        value = Math.Round(Convert.ToDouble(dr_service["value"]), 2);
                                                                        double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        double _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                    }
                                                                }
                                                                break;
                                                            case "rent management":
                                                                if (Convert.ToBoolean(dr_service["percent"] == DBNull.Value || dr_service["percent"] == null ? false : dr_service["percent"]))
                                                                {
                                                                    //for (int i = 0; i < months_year[0] + 1; i++)
                                                                    for (int i = 0; i < months_year[0]; i++)
                                                                    {
                                                                        value = Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(dr_service["value"]) / 100, 2);
                                                                        double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        double _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        /* -- nu se genereaza VAT la Rent income
                                                                        _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        _amount_total = value + _vat;
                                                                         */
                                                                        _vat = 0; _amount_total = Convert.ToDouble(dr["value"]);
                                                                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                                                    }
                                                                }
                                                                else // FROM 30.01.2013 - FOR THE CASES WHEN RENT IS A FIX VALUE, NOT A PERCENT FROM THE RENT ! - BUG #1 / 29.01.2013
                                                                {
                                                                    //for (int i = 0; i < months_year[0] + 1; i++)
                                                                    for (int i = 0; i < months_year[0]; i++)
                                                                    {
                                                                        value = Math.Round(Convert.ToDouble(dr_service["value"]), 2);
                                                                        double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        double _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        /* -- nu se genereaza VAT la Rent income
                                                                        _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        _amount_total = value + _vat;
                                                                        */
                                                                        _vat = 0; _amount_total = Convert.ToDouble(dr["value"]);
                                                                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                                                    }
                                                                }
                                                                break;
                                                            default:
                                                                if (months_years.IndexOf(months_year) == 0)
                                                                {
                                                                    double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                    double _amount_total = value + _vat;
                                                                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                                                    // we don't generate income from another costs for fdp, they are only expenses for the owner
                                                                }
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                }
                                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                            }
                            try
                            {
                                if (contract_status == "active")
                                {
                                    DataRow property = ((DataRowView)dgvr.DataBoundItem).Row;
                                    InvoiceRequirementsClass.InsertFromFDPContract(contract, property);
                                }
                            }
                            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                        }
                    }
                    //EditMode = 0;
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
                //SavedOrCancelled = true;
                //EditMode = 0;
            }
            //dataGrid1.toolStripButtonRefresh.PerformClick();
            //f.Dispose();
        }




        public void buttonEdit_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            //bool SavedOrCancelled = false;
            DataRow contract = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;

            //while (!SavedOrCancelled)
            {
                EditMode = 2; // EDIT
                var f = new Contracts(contract);
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
            Contracts f = (Contracts)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
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
                        //contract["expired"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["expired_id"]) })).ExecuteScalarQuery().ToString();
                        contract["signed?"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["expired_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["parent_contract_number"] = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_number_by_id", new object[] { new MySqlParameter("_ID", contract["parent_contract_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICESsp_get_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract["id"]) })).ExecuteSelectQuery().Tables[0];
                        foreach (DataRow service in contract_services.Rows)
                        {
                            foreach (DataColumn dc in contract.Table.Columns)
                            {
                                if (service["service"].ToString().ToLower().Replace(" ", "_") == dc.ColumnName.ToLower())
                                {
                                    contract[dc.ColumnName] = true;
                                    break;
                                }
                            }
                        }
                    }
                    catch { }
                    
                    try
                    {
                        dataGrid1.da.AttachUpdateParams(mySqlParams);
                        dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                        SavedOrCancelled = true;
                        f.SavedOrCancelled = true;
                        int contract_id = Convert.ToInt32(contract["id"]);
                        string contract_status = Convert.ToString(contract["status"]).ToLower();
                        DateTime contract_start_date = Convert.ToDateTime(contract["start_date"]);
                        DateTime contract_finish_date = Convert.ToDateTime(contract["finish_date"]);
                        DateTime contract_expiration_date = Convert.ToDateTime(contract["expiration_date"]);
                        bool contract_automatically_renewed = contract["automatically_renewed"] == null || contract["automatically_renewed"] == DBNull.Value ? false : Convert.ToBoolean(contract["automatically_renewed"]);
                        bool use_expiration_date = Convert.ToBoolean(contract["use_expiration_date"] == DBNull.Value || contract["use_expiration_date"] == null ? false : contract["use_expiration_date"]);
                        ArrayList months_years = contract_automatically_renewed ? CommonFunctions.DateDifferenceInYears(use_expiration_date ? contract_expiration_date : contract_finish_date, contract_start_date) : CommonFunctions.DateDifferenceInYears(contract_finish_date, contract_start_date);

                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_delete_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id) });
                        da.ExecuteUpdateQuery();
                        // TO DO: Delete (company) predicted ie with no IR id - done in SP
                        da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIES_SERVICESsp_delete_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id) });
                        da.ExecuteUpdateQuery();
                        da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIESsp_delete_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id) });
                        da.ExecuteUpdateQuery();
                        da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_REASONSsp_delete_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id) });
                        da.ExecuteUpdateQuery();
                        foreach (DataGridViewRow dgvr in f.dataGridViewProperties.Rows)
                        {
                            if (Convert.ToBoolean(dgvr.Cells["Assigned"].Value == DBNull.Value ? false : dgvr.Cells["Assigned"].Value))
                            {
                                da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIESsp_insert", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id), new MySqlParameter("_PROPERTY_ID", dgvr.Cells["id"].Value) });
                                object[] returned = da.ExecuteInsertQuery();
                                int _contract_property_id = Convert.ToInt32(returned[2]);

                                //foreach (DataGridViewRow dgvr_service in f.dataGridViewContractServices.Rows)
                                foreach (DataRow dr_service in f.ContractServices.Select(
                                    (dgvr.Cells["id"].Value == DBNull.Value || dgvr.Cells["id"].Value == null) ?
                                        String.Format("IsNull(PROPERTY_ID, -1) = -1") :
                                        String.Format("PROPERTY_ID = {0}", dgvr.Cells["id"].Value.ToString())
                                        ))
                                {
                                    try
                                    {
                                        //if (dgvr_service.Cells["property_id"].Value == dgvr.Cells["id"].Value)
                                        {
                                            da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIES_SERVICESsp_insert", new object[] { 
                                                    new MySqlParameter("_CONTRACT_PROPERTY_ID", _contract_property_id),
                                                    new MySqlParameter("_SERVICE_ID", dr_service["service_id"]),
                                                    new MySqlParameter("_VALUE", dr_service["value"]),
                                                    new MySqlParameter("_PERIOD", dr_service["period"]),
                                                    new MySqlParameter("_PERCENT", dr_service["percent"]),
                                                    new MySqlParameter("_MODIFY", dr_service["modify"]),
                                                    new MySqlParameter("_NOT_INVOICEABLE", dr_service["not_invoiceable"])
                                                });
                                            DataRow cps = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                            try
                                            {
                                                //foreach (DataRow dr in f.ServicesAdditionalCosts.Rows)
                                                //foreach (DataRow dr in f.ServicesAdditionalCosts.Select(String.Format("service_id = {0} and period = {1} and percent = {2}", dgvr_service.Cells["service"].Value.ToString(), dgvr_service.Cells["period"].Value.ToString(), dgvr_service.Cells["percent"].Value.ToString())))
                                                foreach (DataRow dr in f.ServicesAdditionalCosts.Select(String.Format("service_id = {0} and period = {1} and percent = {2} and {3}", dr_service["service_id"].ToString(), dr_service["period"].ToString(), dr_service["percent"].ToString(), ((dgvr.Cells["id"].Value == DBNull.Value || dgvr.Cells["id"].Value == null) ? "IsNull(property_id, -1) = -1" : String.Format("property_id = {0}", dgvr.Cells["id"].Value.ToString())))))
                                                {
                                                    try
                                                    {
                                                        (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_delete_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", dr["ID"]) })).ExecuteUpdateQuery();
                                                        (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_delete_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", dr["ID"]) })).ExecuteUpdateQuery();
                                                        (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_delete_by_contractservice", new object[] { new MySqlParameter("_CONTRACTSERVICE_ID", dr["CONTRACTPROPERTYSERVICE_ID"]) })).ExecuteUpdateQuery();
                                                    }
                                                    catch (Exception exp) { exp.ToString(); }
                                                    DataRow csad = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_insert", new object[] { 
                                                        new MySqlParameter("_CONTRACT_PROPERTY_SERVICE_ID", cps["id"]),
                                                        new MySqlParameter("_DESCRIPTION", dr["description"]),
                                                        new MySqlParameter("_VALUE", dr["value"])
                                                        })).ExecuteSelectQuery().Tables[0].Rows[0];
                                                    if (contract_status == "active")
                                                    {
                                                        foreach (int[] months_year in months_years)
                                                        {
                                                            DateTime start_date = new DateTime(months_year[1], contract_start_date.Month, contract_start_date.Day);
                                                            DateTime end_date = start_date.AddMonths(months_year[0]);

                                                            double value = 0;
                                                            //switch (dgvr_service.Cells["service"].EditedFormattedValue.ToString().ToLower())
                                                            //switch (dr_service["service"].ToString().ToLower())
                                                            string service = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", dr_service["service_id"].ToString().ToLower()) })).ExecuteScalarQuery().ToString().ToLower();
                                                            switch (service)
                                                            {
                                                                case "rent":
                                                                    if ((months_years.IndexOf(months_year) == 0 && !use_expiration_date) || use_expiration_date)
                                                                    {
                                                                        if (Convert.ToBoolean(dr_service["percent"] == DBNull.Value || dr_service["percent"] == null ? false : dr_service["percent"]))
                                                                        {
                                                                            value = Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(dr_service["value"]) / 100, 2);
                                                                            double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            double _amount_total = value + _vat;
                                                                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        }
                                                                        else // FROM 30.01.2013 - FOR THE CASES WHEN RENT IS A FIX VALUE, NOT A PERCENT FROM THE RENT ! - BUG #1 / 29.01.2013
                                                                        {
                                                                            value = Math.Round(Convert.ToDouble(dr_service["value"]), 2);
                                                                            double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            double _amount_total = value + _vat;
                                                                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        }
                                                                    }
                                                                    break;
                                                                case "rent management":
                                                                    if (Convert.ToBoolean(dr_service["percent"] == DBNull.Value || dr_service["percent"] == null ? false : dr_service["percent"]))
                                                                    {
                                                                        //for (int i = 0; i < months_year[0] + 1; i++)
                                                                        for (int i = 0; i < months_year[0]; i++)
                                                                        {
                                                                            value = Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(dr_service["value"]) / 100, 2);
                                                                            double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            double _amount_total = value + _vat;
                                                                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            /* -- nu se aplica VAT la RENT - 24.04.2016 --
                                                                            _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            _amount_total = value + _vat;
                                                                            */
                                                                            _vat = 0; _amount_total = Convert.ToDouble(dr["value"]);
                                                                            IncomeExpensesClass.InsertIE(false, true, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                                                        }
                                                                    }
                                                                    else // FROM 30.01.2013 - FOR THE CASES WHEN RENT IS A FIX VALUE, NOT A PERCENT FROM THE RENT ! - BUG #1 / 29.01.2013
                                                                    {
                                                                        //for (int i = 0; i < months_year[0] + 1; i++)
                                                                        for (int i = 0; i < months_year[0]; i++)
                                                                        {
                                                                            value = Math.Round(Convert.ToDouble(dr_service["value"]), 2);
                                                                            double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            double _amount_total = value + _vat;
                                                                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            /* -- nu se aplica VAT la RENT - 24.04.2016 --
                                                                            _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            _amount_total = value + _vat;
                                                                            */
                                                                            _vat = 0; _amount_total = Convert.ToDouble(dr_service["value"]);
                                                                            IncomeExpensesClass.InsertIE(false, true, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                                                        }
                                                                    }
                                                                    break;
                                                                default:
                                                                    if (months_years.IndexOf(months_year) == 0)
                                                                    {
                                                                        ////IncomeExpensesClass.InsertPredictedIE(false, false, contract["currency"], dr["value"], Convert.ToDateTime(contract["start_date"]).AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dgvr_service.Cells["service"].Value, dr["description"], null, null, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "New"), new MySqlParameter("_LIST_TYPE", "predicted_ie_status") })).ExecuteScalarQuery());
                                                                        //IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], dr["value"], Convert.ToDateTime(contract["start_date"]).AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"]);
                                                                        double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        double _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                                                        ////IncomeExpensesClass.InsertPredictedIE(true, true, contract["currency"], dr["value"], Convert.ToDateTime(contract["start_date"]).AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], null, null, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "New"), new MySqlParameter("_LIST_TYPE", "predicted_ie_status") })).ExecuteScalarQuery());
                                                                        // we don't generate income from another costs for fdp, they are only expenses for the owner
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception exp) { exp.ToString(); }
                                        }
                                    }
                                    catch (Exception exp) { exp.ToString(); }
                                }
                                try
                                {
                                    if (contract_status == "active")
                                    {
                                        DataRow property = ((DataRowView)dgvr.DataBoundItem).Row;
                                        InvoiceRequirementsClass.UpdateFromFDPContract(contract, property, f.InitialExternalDR, true);
                                    }
                                }
                                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                            }
                        }

                    }
                    catch (Exception exp) { exp.ToString(); }
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
                //SavedOrCancelled = true;
            }
            //f.Dispose();
            //dataGrid1.toolStripButtonRefresh.PerformClick();
        }

        private void buttonAddAddendum_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            if (dataGrid1.dataGridView.SelectedRows.Count <= 0) return;
            //bool SavedOrCancelled = false;
            DataRow contract = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            //while (!SavedOrCancelled)
            {
                EditMode = 4; // ADD ADENDUM
                int parent_contract_id = Convert.ToInt32(((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row["id"]);
                var f = new Contracts(contract, Convert.ToInt32(parent_contract_id));
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
        }

        public void SaveAdendumRecord()
        {
            Contracts f = (Contracts)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
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
                        //contract["expired"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["expired_id"]) })).ExecuteScalarQuery().ToString();
                        contract["signed?"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["expired_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", contract["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        contract["parent_contract_number"] = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_number_by_id", new object[] { new MySqlParameter("_ID", contract["parent_contract_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        DataTable contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICESsp_get_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract["id"]) })).ExecuteSelectQuery().Tables[0];
                        foreach (DataRow service in contract_services.Rows)
                        {
                            foreach (DataColumn dc in contract.Table.Columns)
                            {
                                if (service["service"].ToString().ToLower().Replace(" ", "_") == dc.ColumnName.ToLower())
                                {
                                    contract[dc.ColumnName] = true;
                                    break;
                                }
                            }
                        }
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
                    string contract_status = Convert.ToString(contract["status"]).ToLower();
                    DateTime contract_start_date = Convert.ToDateTime(contract["start_date"]);
                    DateTime contract_finish_date = Convert.ToDateTime(contract["finish_date"]);
                    DateTime contract_expiration_date = Convert.ToDateTime(contract["expiration_date"]);
                    bool contract_automatically_renewed = contract["automatically_renewed"] == null || contract["automatically_renewed"] == DBNull.Value ? false : Convert.ToBoolean(contract["automatically_renewed"]);
                    bool use_expiration_date = Convert.ToBoolean(contract["use_expiration_date"] == DBNull.Value || contract["use_expiration_date"] == null ? false : contract["use_expiration_date"]);
                    ArrayList months_years = contract_automatically_renewed ? CommonFunctions.DateDifferenceInYears(use_expiration_date ? contract_expiration_date : contract_finish_date, contract_start_date) : CommonFunctions.DateDifferenceInYears(contract_finish_date, contract_start_date);

                    if (f.ParentContractId > 0)
                    {
                        foreach (object li in f.listBoxReasons.SelectedItems)
                        {
                            int reason_id = Convert.ToInt32(((DataRowView)li)["id"]);
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_REASONSsp_insert", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id), new MySqlParameter("_CONTRACTREASON_ID", reason_id) });
                            da.ExecuteInsertQuery();
                        }
                    }

                    foreach (DataGridViewRow dgvr in f.dataGridViewProperties.Rows)
                    {
                        if (Convert.ToBoolean(dgvr.Cells["Assigned"].Value == DBNull.Value ? false : dgvr.Cells["Assigned"].Value))
                        {
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIESsp_insert", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id), new MySqlParameter("_PROPERTY_ID", dgvr.Cells["id"].Value) });
                            object[] returned = da.ExecuteInsertQuery();
                            int _contract_property_id = Convert.ToInt32(returned[2]);
                            //foreach (DataGridViewRow dgvr_service in f.dataGridViewContractServices.Rows)
                            foreach (DataRow dr_service in f.ContractServices.Select(
                                (dgvr.Cells["id"].Value == DBNull.Value || dgvr.Cells["id"].Value == null) ?
                                    String.Format("IsNull(PROPERTY_ID, -1) = -1") :
                                    String.Format("PROPERTY_ID = {0}", dgvr.Cells["id"].Value.ToString())
                                    ))
                            {
                                try
                                {
                                    //if (dgvr_service.Cells["property_id"].Value == dgvr.Cells["id"].Value)
                                    {
                                        da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIES_SERVICESsp_insert", new object[] { 
                                                new MySqlParameter("_CONTRACT_PROPERTY_ID", _contract_property_id),
                                                new MySqlParameter("_SERVICE_ID", dr_service["service_id"]),
                                                new MySqlParameter("_VALUE", dr_service["value"]),
                                                new MySqlParameter("_PERIOD", dr_service["period"]),
                                                new MySqlParameter("_PERCENT", dr_service["percent"]),
                                                new MySqlParameter("_MODIFY", dr_service["modify"]),
                                                new MySqlParameter("_NOT_INVOICEABLE", dr_service["not_invoiceable"])
                                            });
                                        DataRow cps = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                        try
                                        {
                                            //foreach (DataRow dr in f.ServicesAdditionalCosts.Rows)
                                            //foreach (DataRow dr in f.ServicesAdditionalCosts.Select(String.Format("service_id = {0} and period = {1} and percent = {2}", dgvr_service.Cells["service"].Value.ToString(), dgvr_service.Cells["period"].Value.ToString(), dgvr_service.Cells["percent"].Value.ToString())))
                                            foreach (DataRow dr in f.ServicesAdditionalCosts.Select(String.Format("service_id = {0} and period = {1} and percent = {2} and {3}", dr_service["service_id"].ToString(), dr_service["period"].ToString(), dr_service["percent"].ToString(), ((dgvr.Cells["id"].Value == DBNull.Value || dgvr.Cells["id"].Value == null) ? "IsNull(property_id, -1) = -1" : String.Format("property_id = {0}", dgvr.Cells["id"].Value.ToString())))))
                                            {
                                                try
                                                {
                                                    (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_change_status_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", dr["ID"]), new MySqlParameter("_DELETED", true) })).ExecuteUpdateQuery();
                                                    (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_change_status_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", dr["ID"]), new MySqlParameter("_DELETED", true) })).ExecuteUpdateQuery();
                                                    //(new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_delete_by_contractservice", new object[] { new MySqlParameter("_CONTRACTSERVICE_ID", dr["CONTRACTPROPERTYSERVICE_ID"]) })).ExecuteUpdateQuery();
                                                }
                                                catch { }
                                                DataRow csad = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_insert", new object[] { 
                                                    new MySqlParameter("_CONTRACT_PROPERTY_SERVICE_ID", cps["id"]),
                                                    new MySqlParameter("_DESCRIPTION", dr["description"]),
                                                    new MySqlParameter("_VALUE", dr["value"])
                                                    })).ExecuteSelectQuery().Tables[0].Rows[0];
                                                if (contract_status == "active")
                                                {
                                                    foreach (int[] months_year in months_years)
                                                    {
                                                        DateTime start_date = new DateTime(months_year[1], contract_start_date.Month, contract_start_date.Day);
                                                        DateTime end_date = start_date.AddMonths(months_year[0]);

                                                        double value = 0;
                                                        //switch (dgvr_service.Cells["service"].EditedFormattedValue.ToString().ToLower())
                                                        //switch (dr_service["service"].ToString().ToLower())
                                                        string service = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", dr_service["service_id"].ToString().ToLower()) })).ExecuteScalarQuery().ToString().ToLower();
                                                        switch (service)
                                                        {
                                                            case "rent":
                                                                if ((months_years.IndexOf(months_year) == 0 && !use_expiration_date) || use_expiration_date)
                                                                {
                                                                    if (Convert.ToBoolean(dr_service["percent"] == DBNull.Value || dr_service["percent"] == null ? false : dr_service["percent"]))
                                                                    {
                                                                        value = Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(dr_service["value"]) / 100, 2);
                                                                        double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        double _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                    }
                                                                    else // FROM 30.01.2013 - FOR THE CASES WHEN RENT IS A FIX VALUE, NOT A PERCENT FROM THE RENT ! - BUG #1 / 29.01.2013
                                                                    {
                                                                        value = Math.Round(Convert.ToDouble(dr_service["value"]), 2);
                                                                        double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        double _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                    }
                                                                }
                                                                break;
                                                            case "rent management":
                                                                if (Convert.ToBoolean(dr_service["percent"] == DBNull.Value || dr_service["percent"] == null ? false : dr_service["percent"]))
                                                                {
                                                                    //for (int i = 0; i < months_year[0] + 1; i++)
                                                                    for (int i = 0; i < months_year[0]; i++)
                                                                    {
                                                                        value = Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(dr_service["value"]) / 100, 2);
                                                                        double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        double _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"],_vat,_amount_total);
                                                                    }
                                                                }
                                                                else // FROM 30.01.2013 - FOR THE CASES WHEN RENT IS A FIX VALUE, NOT A PERCENT FROM THE RENT ! - BUG #1 / 29.01.2013
                                                                {
                                                                    //for (int i = 0; i < months_year[0] + 1; i++)
                                                                    for (int i = 0; i < months_year[0]; i++)
                                                                    {
                                                                        value = Math.Round(Convert.ToDouble(dr_service["value"]), 2);
                                                                        double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        double _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, true, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                                                    }
                                                                }
                                                                break;
                                                            default:
                                                                if (months_years.IndexOf(months_year) == 0)
                                                                {
                                                                    double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                    double _amount_total = value + _vat;
                                                                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                    // we don't generate income from another costs for fdp, they are only expenses for the owner
                                                                }
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        catch { }
                                    }
                                }
                                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                            }
                            try
                            {
                                if (contract_status == "active")
                                {
                                    DataRow property = ((DataRowView)dgvr.DataBoundItem).Row;
                                    InvoiceRequirementsClass.UpdateFromFDPContract(contract, property, f.InitialExternalDR);
                                }
                            }
                            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
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
                //SavedOrCancelled = true;
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

                            DataAccess dac = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_has_changes_by_id", new object[]{
                                new MySqlParameter("_ID", key)
                            });
                            object has_changes = dac.ExecuteScalarQuery();
                            if (has_changes != null && Convert.ToInt32(has_changes) > 0)
                            {
                                MessageBox.Show(Language.GetMessageBoxText("contractHasChangesOrRentContracts", "There are newest addendums of this contract or the contract has a Rent contract or Invoice(s) associated. Please delete them first!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                continue;
                            }

                            /*
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIES_SERVICESsp_delete_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", key) });
                            da.ExecuteUpdateQuery();
                            da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIESsp_delete_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", key) });
                            da.ExecuteUpdateQuery();
                            */
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
                                            ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
                                            dataGrid1.da.deleteCommand.CommandText = "CONTRACTSsp_delete_chain";
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
                                            (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_delete_chain", new object[] { new MySqlParameter("_ID", key) })).ExecuteUpdateQuery();
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

                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        //MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ContractSelect_Load(object sender, EventArgs e)
        {
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //AddLinkColumn("properties", "properties", dataGrid1.dataGridView.Columns["owner_id"].Index + 2);
            dataGrid1.AddLinkColumn("properties", "properties", dataGrid1.dataGridView.Columns["owner_id"].Index + 2);
            //AddLinkColumn("services", "services", dataGrid1.dataGridView.Columns["owner_id"].Index + 3);
            dataGrid1.AddLinkColumn("services", "services", dataGrid1.dataGridView.Columns["owner_id"].Index + 3);
            buttonAddAddendum.Visible = false;
            DataGridViewLinkColumn dgvc = (DataGridViewLinkColumn)dataGrid1.dataGridView.Columns["parent_contract_number"];
            //dgvc.DefaultCellStyle.BackColor = System.Drawing.Color.PapayaWhip;
            dgvc.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            dgvc.LinkColor = System.Drawing.Color.Red;
            dgvc.VisitedLinkColor = System.Drawing.Color.Red;
            dataGrid1.toolStrip1.Items["toolStripButtonAddAddendum"].Enabled = (dataGrid1.dataGridView.Rows.Count > 0 && dataGrid1.dataGridView.SelectedRows[0].Cells["parent_contract_id"].Value.ToString().Trim() == "") ? true : false;

            dataGrid1.dataGridView.RowEnter += new DataGridViewCellEventHandler(dataGridView_RowEnter);
        }

        private void dataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //this.buttonAddAddendum.Enabled = (((DataGridView)sender).Rows[e.RowIndex].Cells["parent_contract_id"].Value.ToString().Trim() == "");
            dataGrid1.toolStrip1.Items["toolStripButtonAddAddendum"].Enabled = (((DataGridView)sender).Rows[e.RowIndex].Cells["parent_contract_id"].Value.ToString().Trim() == "") && this.ChildLaunched == null;
        }
        /*
        public void AddLinkColumn(string language_id, string column_name, int insert_index)
        {
            DataGridViewLinkColumn dgvlc = new DataGridViewLinkColumn();
            dgvlc.UseColumnTextForLinkValue = true;
            dgvlc.Text = column_name;
            dgvlc.Name = column_name;
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
    }
}
