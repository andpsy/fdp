using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;

namespace FDP
{
    public partial class RentContracts : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();

        public int ParentContractId;

        public RentContracts()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
        }

        public RentContracts(int id, string id_type)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            DataAccess da = new DataAccess();
            switch (id_type)
            {
                case "id":
                    InitializeComponent();
                    //Language.LoadLabels(this);
                    da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", id) });
                    NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    //this.dataGridContractsServices = new DataGrid("CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", NewDR["id"]) }, "CONTRACTS_PROPERTIES_SERVICESsp_insert", null, "CONTRACTS_PROPERTIES_SERVICESsp_update", null, "CONTRACTS_PROPERTIES_SERVICESsp_delete", null, null, null, null, null, null, new string[] { "SERVICE", "PRICE_VALUE", "PRICE_PERCENT", "PRICE_ONE_PAYMENT", "PRICE_VALUE_APPLICABLE", "PRICE_PERCENT_APPLICABLE", "PRICE_ONE_PAYMENT_APPLICABLE" }, false);
                    buttonSaveContract.Enabled = false;
                    break;
                case "contract_id":
                    ParentContractId = id;
                    InitializeComponent();
                    //Language.LoadLabels(this);
                    da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ParentContractId) });
                    NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    break;
            }
        }

        public RentContracts(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
            NewDR = dr;
            //this.dataGridContractsServices = new DataGrid("CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", NewDR["id"]) }, "CONTRACTS_PROPERTIES_SERVICESsp_insert", null, "CONTRACTS_PROPERTIES_SERVICESsp_update", null, "CONTRACTS_PROPERTIES_SERVICESsp_delete", null, null, null, null, null, null, new string[] { "SERVICE", "PRICE_VALUE", "PRICE_PERCENT", "PRICE_ONE_PAYMENT", "PRICE_VALUE_APPLICABLE", "PRICE_PERCENT_APPLICABLE", "PRICE_ONE_PAYMENT_APPLICABLE" }, false);
            if (dr["parent_contract_id"] != DBNull.Value)
                ParentContractId = Convert.ToInt32(dr["parent_contract_id"]);
        }

        public RentContracts(DataRow dr, int parent_contract_id)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            ParentContractId = parent_contract_id;
            InitializeComponent();
            //Language.LoadLabels(this);
            NewDR = dr;
            //this.dataGridContractsServices = new DataGrid("CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", NewDR["id"]) }, "CONTRACTS_PROPERTIES_SERVICESsp_insert", null, "CONTRACTS_PROPERTIES_SERVICESsp_update", null, "CONTRACTS_PROPERTIES_SERVICESsp_delete", null, null, null, null, null, null, new string[] { "SERVICE", "PRICE_VALUE", "PRICE_PERCENT", "PRICE_ONE_PAYMENT", "PRICE_VALUE_APPLICABLE", "PRICE_PERCENT_APPLICABLE", "PRICE_ONE_PAYMENT_APPLICABLE" }, false);

            DataAccess da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ParentContractId) });
            DataRow ParentNewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];

            foreach (DataColumn dc in NewDR.Table.Columns)
            {
                NewDR[dc.ColumnName] = ParentNewDR[dc.ColumnName];
            }
            NewDR["parent_contract_id"] = ParentContractId;
        }

        private void RentContracts_Load(object sender, EventArgs e)
        {
            checkedComboBoxChildProperties.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedComboBoxChildProperties_ItemCheck);

            if (NewDR != null)
            {
                this.tabPageAddendums.Visible = !(ParentContractId <= 0);
                DataAccess dac = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_has_changes", new object[]{
                    new MySqlParameter("_ID", NewDR["id"]),
                    new MySqlParameter("_NUMBER", NewDR["number"]),
                    new MySqlParameter("_ADENDUM_NUMBER", NewDR["addendum_number"])
                });
                object has_changes = dac.ExecuteScalarQuery();
                bool hc = false;
                if (has_changes != null && Convert.ToInt32(has_changes) > 0)
                {
                    MessageBox.Show(Language.GetMessageBoxText("contractHasChanges", "This contract has addendums or already invoiced requirements! Further modifications to sensitive data will be possible only through an addendum!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    hc = true;
                }
                /*
                comboBoxOwner.Enabled = !(NewDR.RowState == DataRowState.Modified);
                comboBoxTenant.Enabled = !(NewDR.RowState == DataRowState.Modified);
                pictureBoxSelectTenant.Enabled = !(NewDR.RowState == DataRowState.Modified);
                pictureBoxSelectOwner.Enabled = !(NewDR.RowState == DataRowState.Modified);
                dataGridViewProperties.Enabled = !(NewDR.RowState == DataRowState.Modified);
                userTextBoxContractNumber.Enabled = !(NewDR.RowState == DataRowState.Modified);
                dateTimePickerStartDate.Enabled = !(NewDR.RowState == DataRowState.Modified);
                dateTimePickerFinishDate.Enabled = !(NewDR.RowState == DataRowState.Modified);
                userTextBoxRent.Enabled = !(NewDR.RowState == DataRowState.Modified);
                userTextBoxRentVatIncluded.Enabled = !(NewDR.RowState == DataRowState.Modified);
                checkBoxProlongation.Enabled = !(NewDR.RowState == DataRowState.Modified);
                checkBoxSplitting.Enabled = !(NewDR.RowState == DataRowState.Modified);
                 */
                comboBoxOwner.Enabled = !hc;
                //comboBoxTenant.Enabled = !hc;
                //pictureBoxSelectTenant.Enabled = !hc;
                dataGridViewTenants.Enabled = !hc;
                pictureBoxSelectOwner.Enabled = !hc;
                comboBoxCurrency.Enabled = !hc;
                dataGridViewProperties.Enabled = !hc;
                userTextBoxContractNumber.Enabled = !hc;
                dateTimePickerStartDate.Enabled = !hc;
                dateTimePickerFinishDate.Enabled = !hc;
                userTextBoxRent.Enabled = !hc;
                userTextBoxRentVatIncluded.Enabled = !hc;
                checkBoxProlongation.Enabled = !hc;
                checkBoxSplitting.Enabled = !hc;
                tabPageAddendums.Visible = !hc;
            }
            FillCombos();
            if (NewDR != null)
            {
                FillInfo();
                InitialDR = CommonFunctions.CopyDataRow(NewDR);
                if (NewDR.RowState == DataRowState.Added || NewDR.RowState == DataRowState.Detached)
                {
                    InitialDR["finish_date"] = DateTime.Now.AddYears(1).AddDays(-1);
                }
            }
            else
            {
                dateTimePickerStartDate.Value = DateTime.Now;
                dateTimePickerFinishDate.Value = DateTime.Now.AddYears(1).AddDays(-1);
                dateTimePickerRegistrationStartDate.Value = DateTime.Now;
                dateTimePickerRegistrationFinishDate.Value = DateTime.Now.AddYears(1).AddDays(-1);
                dateTimePickerAddendumDate.Value = DateTime.Now;
                try { FillTenants(null); }
                catch { }
                FillLodgers("");
            }
        }

        private void FillCombos()
        {
            DataAccess da = new DataAccess();
            /*
            da = new DataAccess(CommandType.StoredProcedure, "TENANTSsp_list");
            DataTable dtTenants = da.ExecuteSelectQuery().Tables[0];
            if (dtTenants != null)
            {
                comboBoxTenant.DisplayMember = "name";
                comboBoxTenant.ValueMember = "id";
                comboBoxTenant.DataSource = dtTenants;
            }
            */
            da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_list");
            DataTable dtOwners = da.ExecuteSelectQuery().Tables[0];
            if (dtOwners != null)
            {
                comboBoxOwner.DisplayMember = "name";
                comboBoxOwner.ValueMember = "id";
                comboBoxOwner.DataSource = dtOwners;
            }

            da = new DataAccess(CommandType.StoredProcedure, "CITIESsp_list");
            DataTable dtRegistrationCities = da.ExecuteSelectQuery().Tables[0];
            if (dtRegistrationCities != null)
            {
                comboBoxRegistrationCity.DisplayMember = "name";
                comboBoxRegistrationCity.ValueMember = "id";
                comboBoxRegistrationCity.DataSource = dtRegistrationCities;
            }

            da = new DataAccess(CommandType.StoredProcedure, "DISTRICTSsp_list");
            DataTable dtRegistrationDistricts = da.ExecuteSelectQuery().Tables[0];
            if (dtRegistrationDistricts != null)
            {
                comboBoxRegistrationDistrict.DisplayMember = "name";
                comboBoxRegistrationDistrict.ValueMember = "id";
                comboBoxRegistrationDistrict.DataSource = dtRegistrationDistricts;
            }

            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "PAYMENT_LIMITS") });
            DataTable dtPaymentLimits = da.ExecuteSelectQuery().Tables[0];
            if (dtPaymentLimits != null)
            {
                comboBoxPaymentLimitDate.DisplayMember = "name";
                comboBoxPaymentLimitDate.ValueMember = "id";
                comboBoxPaymentLimitDate.DataSource = dtPaymentLimits;
            }

            /*
            da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_select");
            DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
            if (dtProperties != null)
            {
                checkedListBoxProperties.DataSource = dtProperties;
            }
            */
            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "RENTCONTRACT_STATUS") });
            DataTable dtStatuses = da.ExecuteSelectQuery().Tables[0];
            if (dtStatuses != null)
            {
                comboBoxStatus.DisplayMember = "name";
                comboBoxStatus.ValueMember = "id";
                comboBoxStatus.DataSource = dtStatuses;
            }

            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "RENTCONTRACT_TYPE") });
            DataTable dtTypes = da.ExecuteSelectQuery().Tables[0];
            if (dtTypes != null)
            {
                comboBoxRegistered.DisplayMember = "name";
                comboBoxRegistered.ValueMember = "id";
                comboBoxRegistered.DataSource = dtTypes;
            }
            /*
            da = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_list");
            DataTable dtContracts = da.ExecuteSelectQuery().Tables[0];
            if (dtContracts != null)
            {
                comboBoxParentContract.DisplayMember = "number";
                comboBoxParentContract.ValueMember = "id";
                comboBoxParentContract.DataSource = dtContracts;
            }
            */
            da = new DataAccess(CommandType.StoredProcedure, "CONTRACTREASONSsp_list");
            DataTable dtReasons = da.ExecuteSelectQuery().Tables[0];
            if (dtReasons != null)
            {
                listBoxReasons.DisplayMember = "details";
                listBoxReasons.ValueMember = "id";
                listBoxReasons.DataSource = dtReasons;
            }

            da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_currencies_list");
            DataTable dtc1 = da.ExecuteSelectQuery().Tables[0];
            if (dtc1 != null)
            {
                comboBoxCurrency.DisplayMember = "currency";
                comboBoxCurrency.ValueMember = "currency";
                comboBoxCurrency.DataSource = dtc1;
            }
        }

        private void FillInfo()
        {
            try
            {
                //if (NewDR.RowState != DataRowState.Added && NewDR != null)
                if (NewDR != null)
                {
                    userTextBoxContractNumber.Text = NewDR["number"].ToString();
                    //comboBoxTenant.SelectedValue = NewDR["tenant_id"];
                    try { FillTenants(NewDR["id"]); }
                    catch { }
                    try { dateTimePickerStartDate.Value = Convert.ToDateTime(NewDR["start_date"]); }
                    catch { dateTimePickerStartDate.Value = DateTime.Now; }
                    try { dateTimePickerFinishDate.Value = Convert.ToDateTime(NewDR["finish_date"]); }
                    catch { dateTimePickerFinishDate.Value = DateTime.Now.AddYears(1).AddDays(-1); }
                    comboBoxStatus.SelectedValue = NewDR["status_id"];
                    comboBoxRegistered.SelectedValue = NewDR["registered_id"];
                    try
                    {
                        dateTimePickerRegistrationStartDate.Value = Convert.ToDateTime(NewDR["registration_start_date"]);
                    }
                    catch { dateTimePickerRegistrationStartDate.Value = DateTime.Now; }
                    try
                    {
                        dateTimePickerRegistrationFinishDate.Value = Convert.ToDateTime(NewDR["registration_finish_date"]);
                    }
                    catch { dateTimePickerRegistrationFinishDate.Value = DateTime.Now.AddYears(1).AddDays(-1); }
                    userTextBoxRegistrationAmount.Text = NewDR["registration_amount"].ToString();

                    userTextBoxRegistryNumber.Text = NewDR["registry_number"].ToString();
                    userTextBoxRent.Text = NewDR["rent"].ToString();
                    userTextBoxRentVatIncluded.Text = NewDR["rent_vat_included"].ToString();
                    try
                    {
                        if (NewDR["rent_vat_included"] != DBNull.Value && NewDR["rent_vat_included"] != null)
                        {
                            userTextBoxVat.Text = Math.Round(Convert.ToDouble(NewDR["rent_vat_included"]) - Convert.ToDouble(NewDR["rent"]), 2).ToString();
                        }
                    }
                    catch { }
                    userTextBoxDeposit.Text = NewDR["deposit"].ToString();
                    checkBoxSplitting.Checked = NewDR["splitting"] != DBNull.Value ? Convert.ToBoolean(NewDR["splitting"]) : false;
                    checkBoxProlongation.Checked = NewDR["prolongation"] != DBNull.Value ? Convert.ToBoolean(NewDR["prolongation"]) : false;
                    checkBoxRealEstateAgency.Checked = NewDR["real_estate_agency"] != DBNull.Value ? Convert.ToBoolean(NewDR["real_estate_agency"]) : false;
                    userTextBoxRealEstateAgencyName.Text = NewDR["real_estate_agency_name"].ToString();
                    userTextBoxComments.Text = NewDR["comments"].ToString();
                    comboBoxPaymentLimitDate.SelectedValue = NewDR["payment_limit_id"];
                    userTextBoxAddendumNumber.Text = NewDR["addendum_number"].ToString();
                    try { dateTimePickerAddendumDate.Value = Convert.ToDateTime(NewDR["addendum_date"]); }
                    catch { dateTimePickerAddendumDate.Value = DateTime.Now; }
                    try
                    {
                        comboBoxOwner.SelectedValue = NewDR["owner_id"];
                    }
                    catch { }
                    /*
                    try { FillProperties(Convert.ToInt32(NewDR["id"])); }
                    catch { }
                    */
                    try { FillLodgers(NewDR["lodger"].ToString()); }
                    catch { }
                    comboBoxCurrency.SelectedValue = NewDR["currency"];
                    try { FillReasons(Convert.ToInt32(NewDR["id"])); }
                    catch { }
                    try
                    {
                        FillHistory(Convert.ToInt32(NewDR["ID"]));
                    }
                    catch { }
                }
                else
                {
                    try { FillTenants(null); }
                    catch { }
                    FillLodgers("");
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void FillHistory(int contract_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_sp_get_history", new object[] { new MySqlParameter("_ID", contract_id) });
            dataGridViewHistory.DataSource = da.ExecuteSelectQuery().Tables[0];
            foreach (DataGridViewColumn dgvc in dataGridViewHistory.Columns)
            {
                if (dgvc.Name.IndexOf("ID") > -1) dgvc.Visible = false;
            }
        }

        private void FillReasons(int contract_id)
        {
            try
            {
                DataTable contract_reasons = (new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_CONTRACTREASONSsp_get_by_contract_id", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract_id) })).ExecuteSelectQuery().Tables[0];
                if (contract_reasons.Rows.Count > 0)
                {
                    foreach (DataRow dr in contract_reasons.Rows)
                    {
                        try
                        {
                            foreach (object x in listBoxReasons.Items)
                            {
                                if (dr["details"].ToString().ToLower() == ((DataRowView)x)["details"].ToString().ToLower())
                                {
                                    listBoxReasons.SetSelected(listBoxReasons.Items.IndexOf(x), true);
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }

        private void FillLodgers(string lodger_info)
        {
            DataTable lodgers = new DataTable("lodgers");
            DataColumn dc = new DataColumn(Language.GetColumnHeaderText("NAME", "NAME"), Type.GetType("System.String"));
            lodgers.Columns.Add(dc);
            dc = new DataColumn(Language.GetColumnHeaderText("PHONES", "PHONES"), Type.GetType("System.String"));
            lodgers.Columns.Add(dc);
            dc = new DataColumn(Language.GetColumnHeaderText("EMAILS", "EMAILS"), Type.GetType("System.String"));
            lodgers.Columns.Add(dc);
            //if (lodger_info != null && lodger_info.Trim() != "")
            //{
                string[] rows = lodger_info.Split('|');
                if (lodger_info == null || lodger_info.Trim() == "" || (rows.Length == 1 && rows[0].Trim() == ""))
                {
                    DataRow row = lodgers.NewRow();
                    lodgers.Rows.Add(row);
                }
                else
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        try
                        {
                            if (rows[i].Trim() != "")
                            {
                                DataRow row = lodgers.NewRow();
                                string[] elements = rows[i].Split(';');
                                row[Language.GetColumnHeaderText("NAME", "NAME")] = elements[0];
                                row[Language.GetColumnHeaderText("PHONES", "PHONES")] = elements[1];
                                row[Language.GetColumnHeaderText("EMAILS", "EMAILS")] = elements[2];
                                lodgers.Rows.Add(row);
                            }
                        }
                        catch { }
                    }
                }
                lodgers.AcceptChanges();
                dataGridViewLodgers.DataSource = lodgers;
            //}
        }

        private string CreateLodgerText()
        {
            string lodger_text = "";
            try
            {
                foreach (DataRow dr in ((DataTable)dataGridViewLodgers.DataSource).Rows)
                {
                    if(dr[Language.GetColumnHeaderText("NAME", "NAME")] != DBNull.Value && dr[Language.GetColumnHeaderText("NAME", "NAME")] != null && dr[Language.GetColumnHeaderText("NAME", "NAME")].ToString().Trim() != "")
                        lodger_text += String.Format("{0};{1};{2}|", dr[Language.GetColumnHeaderText("NAME", "NAME")].ToString(), dr[Language.GetColumnHeaderText("PHONES", "PHONES")].ToString(), dr[Language.GetColumnHeaderText("EMAILS", "EMAILS")].ToString());
                }
                //lodger_text = lodger_text.Substring(0, lodger_text.Length - 1);
                lodger_text.Remove(lodger_text.Length - 1);
            }
            catch { }
            return lodger_text;
        }

        private void FillProperties()
        {
            try
            {
                if (comboBoxOwner.SelectedIndex > 0)
                {
                    DataRowView drv = (DataRowView)comboBoxOwner.SelectedItem;
                    int _owner_id = Convert.ToInt32(drv.Row["id"]);

                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", _owner_id) });
                    DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
                    if (dtProperties != null)
                    {
                        dataGridViewProperties.DataSource = dtProperties;
                        //dataGridViewProperties.Columns.Remove("assigned");
                    }
                    foreach (DataGridViewColumn dc in dataGridViewProperties.Columns)
                    {
                        dc.Visible = (dc.Name.ToLower() == "name") ? true : false;
                    }

                        foreach (DataGridViewRow dgvr in dataGridViewProperties.Rows)
                        {
                            dataGridViewProperties.Rows[dgvr.Index].Selected = false;
                            if (NewDR != null && NewDR["property_id"] != null && NewDR["property_id"] != DBNull.Value)
                            {
                                if (Convert.ToInt32(dgvr.Cells["id"].Value) == Convert.ToInt32(NewDR["property_id"]))
                                {
                                    dataGridViewProperties.Rows[dgvr.Index].Selected = true;
                                    dataGridViewProperties.CurrentCell = dataGridViewProperties.Rows[dgvr.Index].Cells["NAME"];
                                    dataGridViewProperties.FirstDisplayedScrollingRowIndex = dgvr.Index;
                                    //break;
                                }
                            }
                            DataRowView dvr = (DataRowView)dgvr.DataBoundItem;
                            if (Convert.ToBoolean(dvr["assigned"]) && ((NewDR == null || NewDR["property_id"] == DBNull.Value) || (NewDR != null && Convert.ToInt32(NewDR["property_id"]) != Convert.ToInt32(dvr["id"]))))
                            {
                                ((DataRowView)dgvr.DataBoundItem).Row.RowError = Language.GetErrorText("propertyAlreadyAssigned", "Property is allready assigned to a Rent Contract!");
                                dgvr.DefaultCellStyle.BackColor = Color.LightPink;
                            }
                        }
                    
                    dataGridViewProperties.EndEdit();
                    //((DataTable)dataGridViewProperties.DataSource).AcceptChanges(); 
                    dataGridViewProperties.Refresh();
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void FillTenants(object contract_id)
        {
            try
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "TENANTSsp_list_for_rentcontracts");
                BindingSource bs = new BindingSource();
                bs.DataSource = da.ExecuteSelectQuery().Tables[0];
                bs.Sort = "ASSIGNED DESC, NAME ASC";
                dataGridViewTenants.DataSource = bs;
                DataGridViewCheckBoxColumn dgvcbc = new  DataGridViewCheckBoxColumn();
                dgvcbc.DataPropertyName = "ASSIGNED";
                dgvcbc.Name = "ASSIGNED";
                dataGridViewTenants.Columns.Remove("ASSIGNED");
                dataGridViewTenants.Columns.Insert(0, dgvcbc);
                dataGridViewTenants.Columns["ID"].Visible = false;
                foreach (DataGridViewColumn dgvc in dataGridViewTenants.Columns)
                {
                    dgvc.ReadOnly = true;
                }
                dgvcbc.ReadOnly = false;
                dgvcbc.Width = 60;
                try
                {
                    if (contract_id != null)
                    {
                        da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_TENANTSsp_select_by_rentcontract_id", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract_id) });
                        DataTable rentcontracts_tenants = da.ExecuteSelectQuery().Tables[0];
                        foreach (DataGridViewRow dgvr in dataGridViewTenants.Rows)
                        {
                            //if (Convert.ToInt32(dgvr.Cells["rentcontract_id"]) == Convert.ToInt32(NewDR["rentcontract_id"]))
                            //if (Convert.ToInt32(dgvr.Cells["rentcontract_id"]) == Convert.ToInt32(contract_id))
                            if(rentcontracts_tenants.Select(String.Format("TENANT_ID = {0}", dgvr.Cells["ID"].Value.ToString())).Length > 0)
                            {
                                dataGridViewTenants["ASSIGNED", dgvr.Index].Value = true;
                            }
                        }
                    }
                }
                catch { }
                dataGridViewHistory.EndEdit();
                ((DataTable)((BindingSource)dataGridViewTenants.DataSource).DataSource).AcceptChanges();
                dataGridViewTenants.Rows[0].Selected = true;
                dataGridViewTenants.CurrentCell = dataGridViewTenants.Rows[0].Cells["NAME"];
                dataGridViewTenants.FirstDisplayedScrollingRowIndex = 0;
            }
            catch (Exception exp) { exp.ToString(); }
        }

        public void GenerateMySqlParameters()
        {
            try
            {
                if (NewDR != null)
                {
                    NewDR["number"] = userTextBoxContractNumber.Text;
                    //NewDR["tenant_id"] = CommonFunctions.SetNullable(comboBoxTenant);
                    NewDR["owner_id"] = CommonFunctions.SetNullable(comboBoxOwner);
                    NewDR["property_id"] = dataGridViewProperties.SelectedRows[0].Cells["id"].Value;
                    NewDR["start_date"] = dateTimePickerStartDate.Value;
                    NewDR["finish_date"] = dateTimePickerFinishDate.Value;
                    NewDR["rent"] = Validator.IsDouble(userTextBoxRent.Text.Trim()) && userTextBoxRent.Text.Trim() != "" ? (object)Convert.ToDouble(userTextBoxRent.Text.Trim()) : DBNull.Value;
                    NewDR["rent_vat_included"] = Validator.IsDouble(userTextBoxRentVatIncluded.Text.Trim()) && userTextBoxRentVatIncluded.Text.Trim() != "" ? (object)Convert.ToDouble(userTextBoxRentVatIncluded.Text.Trim()) : DBNull.Value;
                    NewDR["deposit"] = Validator.IsDouble(userTextBoxDeposit.Text.Trim()) && userTextBoxDeposit.Text.Trim() != "" ? (object)Convert.ToDouble(userTextBoxDeposit.Text.Trim()) : DBNull.Value;
                    NewDR["status_id"] = CommonFunctions.SetNullable(comboBoxStatus);
                    NewDR["registered_id"] = CommonFunctions.SetNullable(comboBoxRegistered);
                    NewDR["registry_number"] = userTextBoxRegistryNumber.Text;
                    NewDR["registration_start_date"] = dateTimePickerRegistrationStartDate.Value;
                    NewDR["registration_finish_date"] = dateTimePickerRegistrationFinishDate.Value;
                    NewDR["registration_amount"] = userTextBoxRegistrationAmount.Text.Trim()==""?DBNull.Value:(object)userTextBoxRegistrationAmount.Text;
                    NewDR["payment_limit_id"] = CommonFunctions.SetNullable(comboBoxPaymentLimitDate);
                    NewDR["splitting"] = checkBoxSplitting.Checked;
                    NewDR["prolongation"] = checkBoxProlongation.Checked;
                    NewDR["real_estate_agency"] = checkBoxRealEstateAgency.Checked;
                    NewDR["real_estate_agency_name"] = userTextBoxRealEstateAgencyName.Text;
                    NewDR["lodger"] = CreateLodgerText();
                    NewDR["comments"] = userTextBoxComments.Text;
                    //NewDR["parent_contract_id"] = CommonFunctions.SetNullable(comboBoxParentContract);
                    NewDR["parent_contract_id"] = ParentContractId <= 0 ? NewDR["parent_contract_id"] : (object)ParentContractId;
                    NewDR["addendum_number"] = userTextBoxAddendumNumber.Text;
                    NewDR["addendum_date"] = ParentContractId > 0 ? (object)dateTimePickerAddendumDate.Value : DBNull.Value;
                    NewDR["currency"] = comboBoxCurrency.SelectedValue;
                    NewDR["registration_city"] = CommonFunctions.SetNullable(comboBoxRegistrationCity);
                    NewDR["registration_district"] = CommonFunctions.SetNullable(comboBoxRegistrationDistrict);
                }
                else
                {
                    MySqlParameters.Clear();
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _NUMBER = new MySqlParameter("_NUMBER", userTextBoxContractNumber.Text); MySqlParameters.Add(_NUMBER);
                    //MySqlParameter _TENANT_ID = new MySqlParameter("_TENANT_ID", CommonFunctions.SetNullable(comboBoxTenant)); MySqlParameters.Add(_TENANT_ID);
                    MySqlParameter _OWNER_ID = new MySqlParameter("_OWNER_ID", CommonFunctions.SetNullable(comboBoxOwner)); MySqlParameters.Add(_OWNER_ID);
                    MySqlParameter _PROPERTY_ID = new MySqlParameter("_PROPERTY_ID", dataGridViewProperties.SelectedRows[0].Cells["id"].Value); MySqlParameters.Add(_PROPERTY_ID);
                    MySqlParameter _START_DATE = new MySqlParameter("_START_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerStartDate.Value)); MySqlParameters.Add(_START_DATE);
                    MySqlParameter _FINISH_DATE = new MySqlParameter("_FINISH_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerFinishDate.Value)); MySqlParameters.Add(_FINISH_DATE);
                    MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", CommonFunctions.SetNullable(comboBoxStatus)); MySqlParameters.Add(_STATUS_ID);
                    MySqlParameter _REGISTERED_ID = new MySqlParameter("_REGISTERED_ID", CommonFunctions.SetNullable(comboBoxRegistered)); MySqlParameters.Add(_REGISTERED_ID);
                    MySqlParameter _REGISTRY_NUMBER = new MySqlParameter("_REGISTRY_NUMBER", userTextBoxRegistryNumber.Text); MySqlParameters.Add(_REGISTRY_NUMBER);
                    MySqlParameter _REGISTRATION_START_DATE = new MySqlParameter("_REGISTRATION_START_DATE", dateTimePickerRegistrationStartDate.Value); MySqlParameters.Add(_REGISTRATION_START_DATE);
                    MySqlParameter _REGISTRATION_FINISH_DATE = new MySqlParameter("_REGISTRATION_FINISH_DATE", dateTimePickerRegistrationFinishDate.Value); MySqlParameters.Add(_REGISTRATION_FINISH_DATE);
                    MySqlParameter _REGISTRATION_AMOUNT = new MySqlParameter("_REGISTRATION_AMOUNT", userTextBoxRegistrationAmount.Text.Trim()==""?DBNull.Value:(object)userTextBoxRegistrationAmount.Text); MySqlParameters.Add(_REGISTRATION_AMOUNT);

                    MySqlParameter _RENT = new MySqlParameter("_RENT", Validator.IsDouble(userTextBoxRent.Text.Trim()) && userTextBoxRent.Text.Trim() != "" ? (object)Convert.ToDouble(userTextBoxRent.Text.Trim()) : DBNull.Value); MySqlParameters.Add(_RENT);
                    MySqlParameter _RENT_VAT_INCLUDED = new MySqlParameter("_RENT_VAT_INCLUDED", Validator.IsDouble(userTextBoxRentVatIncluded.Text.Trim()) && userTextBoxRentVatIncluded.Text.Trim() != "" ? (object)Convert.ToDouble(userTextBoxRentVatIncluded.Text.Trim()) : DBNull.Value); MySqlParameters.Add(_RENT_VAT_INCLUDED);
                    MySqlParameter _DEPOSIT = new MySqlParameter("_DEPOSIT", Validator.IsDouble(userTextBoxDeposit.Text.Trim()) && userTextBoxDeposit.Text.Trim() != "" ? (object)Convert.ToDouble(userTextBoxDeposit.Text.Trim()) : DBNull.Value); MySqlParameters.Add(_DEPOSIT);
                    MySqlParameter _SPLITTING = new MySqlParameter("_SPLITTING", checkBoxSplitting.Checked); MySqlParameters.Add(_SPLITTING);
                    MySqlParameter _PROLONGATION = new MySqlParameter("_PROLONGATION", checkBoxProlongation.Checked); MySqlParameters.Add(_PROLONGATION);
                    MySqlParameter _REAL_ESTATE_AGENCY = new MySqlParameter("_REAL_ESTATE_AGENCY", checkBoxRealEstateAgency.Checked); MySqlParameters.Add(_REAL_ESTATE_AGENCY);
                    MySqlParameter _REAL_ESTATE_AGENCY_NAME = new MySqlParameter("_REAL_ESTATE_AGENCY_NAME", userTextBoxRealEstateAgencyName.Text); MySqlParameters.Add(_REAL_ESTATE_AGENCY_NAME);
                    MySqlParameter _PAYMENT_LIMIT_ID = new MySqlParameter("_PAYMENT_LIMIT_ID", CommonFunctions.SetNullable(comboBoxPaymentLimitDate)); MySqlParameters.Add(_PAYMENT_LIMIT_ID);
                    MySqlParameter _LODGER = new MySqlParameter("_LODGER", CreateLodgerText()); MySqlParameters.Add(_LODGER);
                    MySqlParameter _COMMENTS = new MySqlParameter("_COMMENTS", userTextBoxComments.Text); MySqlParameters.Add(_COMMENTS);
                    //MySqlParameter _PARENT_CONTRACT_ID = new MySqlParameter("_PARENT_CONTRACT_ID", CommonFunctions.SetNullable(comboBoxParentContract)); MySqlParameters.Add(_PARENT_CONTRACT_ID);
                    MySqlParameter _PARENT_CONTRACT_ID = new MySqlParameter("_PARENT_CONTRACT_ID", DBNull.Value); MySqlParameters.Add(_PARENT_CONTRACT_ID);
                    MySqlParameter _ADDENDUM_NUMBER = new MySqlParameter("_ADDENDUM_NUMBER", userTextBoxAddendumNumber.Text); MySqlParameters.Add(_ADDENDUM_NUMBER);
                    MySqlParameter _ADDENDUM_DATE = new MySqlParameter("_ADDENDUM_DATE", ParentContractId > 0 ? (object)CommonFunctions.ToMySqlFormatDate(dateTimePickerAddendumDate.Value) : DBNull.Value); MySqlParameters.Add(_ADDENDUM_DATE);
                    MySqlParameter _CURRENCY = new MySqlParameter("_CURRENCY", comboBoxCurrency.SelectedValue); MySqlParameters.Add(_CURRENCY);

                    MySqlParameter _REGISTRATION_CITY = new MySqlParameter("_REGISTRATION_CITY", CommonFunctions.SetNullable( comboBoxRegistrationCity)); MySqlParameters.Add(_REGISTRATION_CITY);
                    MySqlParameter _REGISTRATION_DISTRICT = new MySqlParameter("_REGISTRATION_DISTRICT", CommonFunctions.SetNullable(comboBoxRegistrationDistrict)); MySqlParameters.Add(_REGISTRATION_DISTRICT);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void buttonSaveContract_Click(object sender, EventArgs e)
        {
            GenerateMySqlParameters();
            if (!ValidateData())
            {
                if(base.WarningList.Count > 0)
                    base.ShowWarningsDialog(Language.GetErrorText("warningsOnPage", "WARNING!"));
                else
                    base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                return;
            }

            if (NewDR == null) //add direct
            {
                try
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_insert", MySqlParameters.ToArray());
                    DataRow rent_contract = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    SavedOrCancelled = true;
                    int contract_id = Convert.ToInt32(rent_contract["id"]);

                    foreach (DataRow dr in ((DataTable)((BindingSource)dataGridViewTenants.DataSource).DataSource).Select("Assigned = true"))
                    {
                        da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_TENANTSsp_insert", new object[]{
                            new MySqlParameter("_RENTCONTRACT_ID", contract_id),
                            new MySqlParameter("_TENANT_ID", dr["ID"])
                        });
                        da.ExecuteInsertQuery();
                    }
                    InvoiceRequirementsClass.InsertFromRentContract(rent_contract);

                    da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_change_status", new object[] { new MySqlParameter("_ID", rent_contract["property_id"]), new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rented"), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery().ToString()) });
                    da.ExecuteUpdateQuery();

                    for (int i = 0; i < checkedComboBoxChildProperties.Items.Count; i++)
                    {
                        CCBoxItem ci = (CCBoxItem)checkedComboBoxChildProperties.Items[i];
                        int _propertyId = ci.Value;
                        if (checkedComboBoxChildProperties.GetItemCheckState(i) == CheckState.Checked)
                        {
                            da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_PROPERTIESsp_insert", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract_id), new MySqlParameter("_PROPERTY_ID", _propertyId) });
                            da.ExecuteUpdateQuery();

                            da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_change_status", new object[] { new MySqlParameter("_ID", _propertyId), new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Rented"), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery().ToString()) });
                            da.ExecuteUpdateQuery();
                        }
                        if (checkedComboBoxChildProperties.GetItemCheckState(i) == CheckState.Unchecked)
                        {
                            da = new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTS_PROPERTIESsp_delete_by_params", new object[] { new MySqlParameter("_RENTCONTRACT_ID", contract_id), new MySqlParameter("_PROPERTY_ID", _propertyId) });
                            da.ExecuteUpdateQuery();

                            da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_change_status", new object[] { new MySqlParameter("_ID", _propertyId), new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "For rent"), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery().ToString()) });
                            da.ExecuteUpdateQuery();
                        }
                    }


                    InitialDR = CommonFunctions.CopyDataRow(NewDR);
                    this.Close();
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
            //((DataTable)((BindingSource)((RentContracts)this).dataGridViewTenants.DataSource).DataSource).AcceptChanges();
            //((DataTable)((RentContracts)this).dataGridViewProperties.DataSource).AcceptChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateData()
        {
            bool toReturn = true;
            errorProvider1.SetError(userTextBoxContractNumber, "");
            errorProvider1.SetError(comboBoxOwner, "");
            errorProvider1.SetError(comboBoxStatus, "");
            //errorProvider1.SetError(comboBoxTenant, "");
            errorProvider1.SetError(comboBoxCurrency, "");
            errorProvider1.SetError(comboBoxRegistered, "");
            errorProvider1.SetError(userTextBoxAddendumNumber, "");
            errorProvider1.SetError(dateTimePickerAddendumDate, "");
            errorProvider1.SetError(userTextBoxRegistrationAmount, "");

            errorProvider1.SetError(userTextBoxRent, "");
            errorProvider1.SetError(userTextBoxRentVatIncluded, "");
            errorProvider1.SetError(userTextBoxDeposit, "");

            /* - since the meeting on 22.08.2012 - its not mandatory any more
            if (userTextBoxContractNumber.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxContractNumber, Language.GetErrorText("errorEmptyContractNumber", "Contract number can not by empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxContractNumber.Name, Language.GetErrorText("errorEmptyContractNumber", "Contract number can not by empty!")));
                toReturn = false;
            }
            */ 

            if(comboBoxOwner.SelectedValue == null || comboBoxOwner.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxOwner, Language.GetErrorText("errorEmptyContractOwner", "You must select an owner for the contract!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwner.Name, Language.GetErrorText("errorEmptyContractOwner", "You must select an owner for the contract!")));
                toReturn = false;
            }
            try
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_active_contracts_by_id", new object[] { new MySqlParameter("_ID", comboBoxOwner.SelectedValue) });
                if (Convert.ToInt32(da.ExecuteScalarQuery()) <= 0)
                {
                    errorProvider1.SetError(comboBoxOwner, Language.GetErrorText("ownerDoesNotHaveActiveContract", "The selected owner doesn't have an active contract. You cannot sign a Rent contract for an owner without an active contract!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwner.Name, Language.GetErrorText("ownerDoesNotHaveActiveContract", "The selected owner doesn't have an active contract. You cannot sign a Rent contract for an owner without an active contract!")));
                    toReturn = false;
                }
            }
            catch
            {
                errorProvider1.SetError(comboBoxOwner, Language.GetErrorText("ownerDoesNotHaveActiveContract", "The selected owner doesn't have an active contract. You cannot sign a Rent contract for an owner without an active contract!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwner.Name, Language.GetErrorText("ownerDoesNotHaveActiveContract", "The selected owner doesn't have an active contract. You cannot sign a Rent contract for an owner without an active contract!")));
                toReturn = false;
            }
            //if (comboBoxTenant.SelectedValue == null || comboBoxTenant.SelectedIndex < 1)
            if (((DataTable)((BindingSource)dataGridViewTenants.DataSource).DataSource).Select("Assigned = true").Length <= 0)
            {
                errorProvider1.SetError(dataGridViewTenants, Language.GetErrorText("errorEmptyContractTenant", "You must select a tenant for the contract!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(dataGridViewTenants.Name, Language.GetErrorText("errorEmptyContractTenant", "You must select a tenant for the contract!")));
                toReturn = false;
            }
            if (comboBoxCurrency.SelectedValue == null || comboBoxCurrency.SelectedIndex < 0)
            {
                errorProvider1.SetError(comboBoxCurrency, Language.GetErrorText("errorEmptyContractCurrency", "You must select the currency for the contract!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxCurrency.Name, Language.GetErrorText("errorEmptyContractCurrency", "You must select the currency for the contract!")));
                toReturn = false;
            }
            if (comboBoxStatus.SelectedValue == null || comboBoxStatus.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxStatus, Language.GetErrorText("errorEmptyStatus", "You must select the status!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxStatus.Name, Language.GetErrorText("errorEmptyStatus", "You must select the status!")));
                toReturn = false;
            }
            if (dataGridViewProperties.SelectedRows.Count<=0)
            {
                errorProvider1.SetError(dataGridViewProperties, Language.GetErrorText("errorEmptyContractProperties", "You must select at least one property for the contract!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(dataGridViewProperties.Name, Language.GetErrorText("errorEmptyContractProperties", "You must select at least one property for the contract!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxRent.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxRent, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxRent.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxRentVatIncluded.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxRentVatIncluded, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxRentVatIncluded.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxDeposit.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxDeposit, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxDeposit.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (comboBoxRegistered.SelectedValue == null || comboBoxRegistered.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxRegistered, Language.GetErrorText("errorEmptyRegistered", "You must select the Registred status!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxRegistered.Name, Language.GetErrorText("errorEmptyRegistered", "You must select the Registred status!")));
                toReturn = false;
            }
            if (dateTimePickerFinishDate.Value < dateTimePickerStartDate.Value)
            {
                errorProvider1.SetError(dateTimePickerFinishDate, Language.GetErrorText("errorFinishDateLowerThanStartDate", "The finish date is lower than the start date!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(dateTimePickerFinishDate.Name, Language.GetErrorText("errorFinishDateLowerThanStartDate", "The finish date is lower than the start date!")));
                toReturn = false;
            }
            try
            {
                DataRow contract = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", comboBoxOwner.SelectedValue) })).ExecuteSelectQuery().Tables[0].Select(String.Format("PROPERTY_ID = {0}", NewDR["property_id"]))[0];
                if (comboBoxCurrency.SelectedValue.ToString() != contract["currency"].ToString())
                {
                    errorProvider1.SetError(comboBoxCurrency, Language.GetErrorText("errorCurrencyNotFoundForOwner", "The selected currency doesn't match with the contract currency!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxCurrency.Name, Language.GetErrorText("errorCurrencyNotFoundForOwner", "The selected currency doesn't match with the contract currency!")));
                    toReturn = false;
                }
            }
            catch { }

            if (ParentContractId > 0 && userTextBoxAddendumNumber.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxAddendumNumber, Language.GetErrorText("errorEmptyAdendumNumber", "The Adendum number can not be empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxAddendumNumber.Name, Language.GetErrorText("errorEmptyAdendumNumber", "The Adendum number can not be empty!")));
                toReturn = false;
            }
            if (ParentContractId > 0 && dateTimePickerAddendumDate.Value.ToString() == "")
            {
                errorProvider1.SetError(dateTimePickerAddendumDate, Language.GetErrorText("errorEmptyAdendumDate", "The Adendum date can not be empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(dateTimePickerAddendumDate.Name, Language.GetErrorText("errorEmptyAdendumDate", "The Adendum date can not be empty!")));
                toReturn = false;
            }

            if (!Validator.IsDouble(userTextBoxRegistrationAmount.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxRegistrationAmount, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxRegistrationAmount.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }

            if (!Validator.IsDouble(userTextBoxRent.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxRent, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxRent.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }

            if (!Validator.IsDouble(userTextBoxRentVatIncluded.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxRentVatIncluded, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxRentVatIncluded.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }

            if (!Validator.IsDouble(userTextBoxDeposit.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxDeposit, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxDeposit.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }

            if (userTextBoxRent.Text != "" && userTextBoxRentVatIncluded.Text != "" &&  Math.Round( Convert.ToDouble(userTextBoxRentVatIncluded.Text), 2) != Math.Round(Convert.ToDouble(userTextBoxRent.Text) + Convert.ToDouble(userTextBoxRent.Text) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2))
            {
                errorProvider1.SetError(userTextBoxRent, Language.GetErrorText("errorInvalidVatValue", "Invalid VAT value!"));
                errorProvider1.SetError(userTextBoxRentVatIncluded, Language.GetErrorText("errorInvalidVatValue", "Invalid VAT value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxRentVatIncluded.Name, Language.GetErrorText("errorInvalidVatValue", "Invalid VAT value!")));
                toReturn = false;
            }

            if (((DataRow)((DataRowView)dataGridViewProperties.SelectedRows[0].DataBoundItem).Row).HasErrors && !base.ConfirmSaveAfterWarnings && toReturn)
            {
                errorProvider1.SetError(dataGridViewProperties, Language.GetErrorText("propertyAlreadyAssigned", "Property is allready assigned to a Rent Contract!"));
                base.WarningList.Add(new KeyValuePair<string, string>(dataGridViewProperties.Name, Language.GetErrorText("propertyAlreadyAssigned", "Property is allready assigned to a Rent Contract!")));
                toReturn = false;
            }
            return toReturn;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var f = new OwnerSelect(true);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxOwner.SelectedValue = f.IdToReturn;
            }
            f.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            /*
            var f = new ContractSelect(true);
            f.dataGrid1.Selectable = true;
            f.StartPosition = FormStartPosition.CenterScreen;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxParentContract.SelectedValue = f.dataGrid1.IdToReturn;
            }
            f.Dispose();
            */
        }

        private void comboBoxOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedIndex > 0)
                {
                    DataRowView drv = (DataRowView)((ComboBox)sender).SelectedItem;
                    int _owner_id = Convert.ToInt32(drv.Row["id"]);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_active_contracts_by_id", new object[] { new MySqlParameter("_ID", _owner_id) });
                    try
                    {
                        if (Convert.ToInt32(da.ExecuteScalarQuery()) <= 0)
                        {
                            MessageBox.Show(Language.GetMessageBoxText("ownerDoesNotHaveActiveContract", "The selected owner doesn't have an active contract. You cannot sign a Rent contract for an owner without an active contract!"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch {
                        MessageBox.Show(Language.GetMessageBoxText("ownerDoesNotHaveActiveContract", "The selected owner doesn't have an active contract. You cannot sign a Rent contract for an owner without an active contract!"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    /*
                    da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", _owner_id) });
                    DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
                    if (dtProperties != null)
                    {
                        dataGridViewProperties.DataSource = dtProperties;
                        //dataGridViewProperties.Columns.Remove("assigned");
                    }
                    foreach (DataGridViewColumn dc in dataGridViewProperties.Columns)
                    {
                        dc.Visible = (dc.Name.ToLower() == "name") ? true : false;
                    }
                    foreach (DataGridViewRow dgvr in dataGridViewProperties.Rows)
                    {
                        DataRowView dvr = (DataRowView)dgvr.DataBoundItem;
                        if (Convert.ToBoolean(dvr["assigned"]) && ((NewDR == null || NewDR["property_id"] == DBNull.Value) || (NewDR != null && Convert.ToInt32(NewDR["property_id"]) != Convert.ToInt32(dvr["id"]))))
                        {
                            ((DataRowView)dgvr.DataBoundItem).Row.RowError = Language.GetErrorText("propertyAlreadyAssigned", "Property is allready assigned to a Rent Contract!");
                            dgvr.DefaultCellStyle.BackColor = Color.LightPink;
                        }
                    }
                    dataGridViewProperties.Refresh();
                    */
                    //FillProperties(NewDR["id"]);
                    FillProperties();
                }
            }
            catch(Exception exp) {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void pictureBoxSelectTenant_Click(object sender, EventArgs e)
        {
            var f = new TenantSelect(true);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxOwner.SelectedValue = f.IdToReturn;
            }
            f.Dispose();
        }

        private void dataGridViewLodgers_MouseDown(object sender, MouseEventArgs e)
        {
            int row_index = dataGridViewLodgers.HitTest(e.X, e.Y).RowIndex;
            int col_index = dataGridViewLodgers.HitTest(e.X, e.Y).ColumnIndex;

            try
            {
                if (((DataGridView)sender).Columns[col_index] is DataGridViewTextBoxColumn && row_index != -1)
                {
                    if (this.Controls.Find(String.Format("panel_{0}_{1}_{2}", dataGridViewLodgers.Name, row_index.ToString(), col_index.ToString()), true).Length <= 0)
                    {
                        //ShowLargeTextBox((DataGridView)sender, row_index, col_index, dataGridViewLodgers[col_index, row_index].Value.ToString());
                        ShowLargeTextBox((DataGridView)sender, e.X, e.Y, dataGridViewLodgers[col_index, row_index].Value.ToString());
                    }
                }
            }
            catch { }
        }

        private void ShowLargeTextBox(DataGridView sender, int x, int y, string content)
        {
            int row_x = sender.HitTest(x, y).RowIndex;
            int col_y = sender.HitTest(x, y).ColumnIndex;
            Panel tmp_pnl = new Panel();
            tmp_pnl.Name = String.Format("panel_{0}_{1}_{2}", sender.Name, row_x.ToString(), col_y.ToString());
            tmp_pnl.Width = 202;
            tmp_pnl.Left = x;
            tmp_pnl.Top = y;
            //tmp_pnl.BackColor = Color.DarkOrange;
            tmp_pnl.BorderStyle = BorderStyle.Fixed3D;
            TextBox tmp = new TextBox();
            tmp.Name = String.Format("textBox_{0}_{1}_{2}", sender.Name, row_x.ToString(), col_y.ToString());
            tmp.Text = content;
            tmp.Multiline = true;
            tmp.Height = 70;
            tmp.Width = 186;
            tmp_pnl.Controls.Add(tmp);
            Button btn = new Button();
            btn.Name = String.Format("btn_{0}_{1}_{2}", sender.Name, row_x.ToString(), col_y.ToString());
            btn.Text = "OK";
            btn.Top = tmp.Top + tmp.Height + 1;
            btn.Click += new EventHandler(btn_Click);
            tmp_pnl.Controls.Add(btn);
            sender.FindForm().Controls.Add(tmp_pnl);
            //sender.Controls.Add(tmp);
            tmp_pnl.Height = tmp.Height + btn.Height + 6;
            tmp_pnl.BringToFront();
            tmp.Focus();
            tmp.SelectAll();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            string[] nme = ((Button)sender).Name.Split('_');
            int x = Convert.ToInt32(nme[2]);
            int y = Convert.ToInt32(nme[3]);
            string grid = nme[1];
            DataGridView dgv = (DataGridView)this.Controls.Find(grid, true)[0];
            //int row_index = dgv.HitTest(x,y).RowIndex;
            int row_index = x;
            //int col_index = dgv.HitTest(x, y).ColumnIndex;
            int col_index = y;
            TextBox tmp = (TextBox)this.Controls.Find(((Button)sender).Name.Replace("btn", "textBox"), true)[0];
            dgv[col_index, row_index].Value = tmp.Text;
            Panel tmp_pnl = (Panel)((Button)sender).Parent;
            foreach (Control ctrl in tmp_pnl.Controls)
            {
                ctrl.Dispose();
            }
            tmp_pnl.Dispose();
        }

        private void comboBoxRegistrationCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "DISTRICTSsp_GetIdByCityId", new object[] { new MySqlParameter("_CITY_ID", Convert.ToInt32(((ComboBox)sender).SelectedValue)) });
            try { 
                int distrinct_id = Convert.ToInt32(da.ExecuteScalarQuery());
                for (int i = 0; i < comboBoxRegistrationDistrict.Items.Count; i++)
                {
                    if (Convert.ToInt32( ((DataRowView)comboBoxRegistrationDistrict.Items[i])["ID"]) == distrinct_id)
                    {
                        comboBoxRegistrationDistrict.SelectedIndex = i;
                        break;
                    }
                }
                //comboBoxRegistrationDistrict.SelectedValue = distrinct_id;
            }
            catch { }
        }

        private void userTextBoxRent_Leave(object sender, EventArgs e)
        {
            try
            {
                if (userTextBoxRentVatIncluded.Text.Trim() == "")
                {
                    userTextBoxVat.Text = "";
                }
                else
                {
                    double rent = ((TextBox)sender).Text.Trim() == "" ? 0 : Convert.ToDouble(((TextBox)sender).Text);
                    double rent_vat_included = userTextBoxRentVatIncluded.Text.Trim() == "" ? 0 : Convert.ToDouble(userTextBoxRentVatIncluded.Text);
                    userTextBoxVat.Text = (rent_vat_included - rent).ToString();
                }
            }
            catch { }
        }

        private void userTextBoxRentVatIncluded_Leave(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox)sender).Text.Trim() == "")
                {
                    userTextBoxVat.Text = "";
                }
                else
                {
                    double rent_vat_included = ((TextBox)sender).Text.Trim() == "" ? 0 : Convert.ToDouble(((TextBox)sender).Text);
                    double rent = userTextBoxRent.Text.Trim() == "" ? 0 : Convert.ToDouble(userTextBoxRent.Text);
                    userTextBoxVat.Text = (rent_vat_included - rent).ToString();
                }
            }
            catch { }
        }

        private void dataGridViewProperties_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (e.RowIndex != -1)
            {
                int parentProperty = Convert.ToInt32(dataGridViewProperties.Rows[e.RowIndex].Cells["ID"].Value);
                GetChildProperties(parentProperty);
            }
            */
        }

        private void GetChildProperties(int _parentPropertyId)
        {
            checkedComboBoxChildProperties.Items.Clear();
            checkedComboBoxChildProperties.Text = "";
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_childs", new object[] { new MySqlParameter("_PARENT_PROPERTY_ID", _parentPropertyId) });
            DataTable ChildProperties = da.ExecuteSelectQuery().Tables[0];
            int i=0;
            foreach (DataRow dr in ChildProperties.Rows)
            {
                CCBoxItem item = new CCBoxItem(dr["name"].ToString(), Convert.ToInt32(dr["id"]));
                checkedComboBoxChildProperties.Items.Add(item);
                checkedComboBoxChildProperties.SetItemCheckState(i, dr["RENTED"] == null || dr["RENTED"] == DBNull.Value ? CheckState.Unchecked : CheckState.Checked);
                i++;
            }
        }

        private void checkedComboBoxChildProperties_ItemCheck(object sender, ItemCheckEventArgs e)
        {
        }

        private void dataGridViewProperties_SelectionChanged(object sender, EventArgs e)
        {
            /*
            try
            {
                int parentProperty = Convert.ToInt32(dataGridViewProperties.SelectedRows[0].Cells["ID"].Value);
                GetChildProperties(parentProperty);
            }
            catch { }
            */
        }

        private void dataGridViewProperties_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;
            
            try
            {
                int parentProperty = Convert.ToInt32(e.Row.Cells["ID"].Value);
                GetChildProperties(parentProperty);
            }
            catch { }
        }
    }
}
