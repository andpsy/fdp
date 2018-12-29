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
    public partial class IncomeExpenses : UserForm
    {

        public DataRow InitialDetailedRow;
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();
        public DataRow LastPayment;
        public DataTable dtPayments;
        public DataRow Invoice;
        public int NewDRId;

        public IncomeExpenses()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
        }

        public IncomeExpenses(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            //NewDR = dr;
            PopulateRow(dr);
            InitializeComponent();
        }

        private void PopulateRow(DataRow dr)
        {
            try
            {
                NewDRId = Convert.ToInt32(dr["id"]);
            }
            catch { }
            InitialDetailedRow = dr;
            DataTable original_fields_table = dr.Table.Clone();
            original_fields_table.Columns.Remove("ORIGINAL AMOUNT (EUR)");
            original_fields_table.Columns.Remove("ORIGINAL AMOUNT (RON)");
            original_fields_table.Columns.Remove("BALLANCE (RON)");
            original_fields_table.Columns.Remove("VAT (RON)");
            original_fields_table.Columns.Remove("TOTAL (RON)");
            original_fields_table.Columns["AMOUNT PAID (EUR)"].ColumnName = "AMOUNT_PAID";
            original_fields_table.Columns["AMOUNT PAID (RON)"].ColumnName = "AMOUNT_PAID_RON";
            original_fields_table.Columns["VAT (EUR)"].ColumnName = "VAT";
            original_fields_table.Columns["TOTAL (EUR)"].ColumnName = "AMOUNT_TOTAL";
            DataColumn dc = new DataColumn("AMOUNT");
            dc.DataType = Type.GetType("System.Double");
            original_fields_table.Columns.Add(dc);

            dc = new DataColumn("BALLANCE");
            dc.DataType = Type.GetType("System.Double");
            original_fields_table.Columns.Add(dc);
            original_fields_table.AcceptChanges();

            NewDR = original_fields_table.NewRow();

            foreach (DataColumn dcol in dr.Table.Columns)
            {
                try
                {
                    NewDR[dcol.ColumnName] = dr[dcol.ColumnName];
                }
                catch { }
            }
            try
            {
                NewDR["AMOUNT"] = dr["ORIGINAL AMOUNT (EUR)"] != DBNull.Value && dr["ORIGINAL AMOUNT (EUR)"] != null ? dr["ORIGINAL AMOUNT (EUR)"] : dr["ORIGINAL AMOUNT (RON)"];
                try
                {
                    NewDR["VAT"] = dr["VAT (EUR)"] != DBNull.Value && dr["VAT (EUR)"] != null ? dr["VAT (EUR)"] : dr["VAT (RON)"];
                    NewDR["AMOUNT_TOTAL"] = dr["TOTAL (EUR)"] != DBNull.Value && dr["TOTAL (EUR)"] != null ? dr["TOTAL (EUR)"] : dr["TOTAL (RON)"];
                }
                catch { }
                NewDR["AMOUNT_PAID"] = dr["AMOUNT PAID (EUR)"];
                NewDR["AMOUNT_PAID_RON"] = dr["AMOUNT PAID (RON)"];
                NewDR["BALLANCE"] = dr["BALLANCE (EUR)"] != DBNull.Value && dr["BALLANCE (EUR)"] != null ? dr["BALLANCE (EUR)"] : dr["BALLANCE (RON)"];
            }
            catch { }
            original_fields_table.Rows.Add(NewDR);
            original_fields_table.AcceptChanges();
        }

        private void PopulateRow(int id)
        {
            DataRow dr = (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_get_by_id2", new object[] { new MySqlParameter("_ID", id) })).ExecuteSelectQuery().Tables[0].Rows[0];
            PopulateRow(dr);
        }

        private void IncomeExpenses_Load(object sender, EventArgs e)
        {
            userTextBoxPrice.Validated += new EventHandler(userTextBoxPrice_Validated);
            userTextBoxPriceRon.Validated += new EventHandler(userTextBoxPriceRon_Validated);
            buttonClearBallance.Click += new EventHandler(buttonClearBallance_Click);
            comboBoxCurrency.SelectedIndexChanged += new EventHandler(comboBoxCurrency_SelectedIndexChanged);
            buttonIncasare.Click += new EventHandler(buttonIncasare_Click);
            listBoxPayments.DoubleClick += new EventHandler(listBoxPayments_DoubleClick);
            buttonInvoice.Click += new EventHandler(buttonInvoice_Click);
            buttonDeletePayment.Click += new EventHandler(buttonDeletePayment_Click);
            checkBoxUseVat.CheckedChanged += new EventHandler(checkBoxUseVat_CheckedChanged);
            pictureBoxSelectOwner.Click += new EventHandler(pictureBoxSelectOwner_Click);
            comboBoxOwner.SelectedIndexChanged += new EventHandler(comboBoxOwner_SelectedIndexChanged);
            buttonSave.Click += new EventHandler(buttonSave_Click);
            buttonExit.Click += new EventHandler(buttonExit_Click);

            FillGeneralInfo();
        }

        private void FillGeneralInfo()
        {
            buttonIncasare.Enabled = false;
            buttonClearBallance.Enabled = false;
            FillCombos();
            if (NewDR != null)
            {
                FillInfo();
                try
                {
                    if (NewDR["service_description"].ToString().ToUpper().IndexOf("VAT") > -1) // this was for separated VAT I/E's
                    {
                        labelVATEur.Visible = userTextBoxVATEur.Visible = labelExchangedVat.Visible = false;
                    }
                }
                catch { }
                if (NewDR.RowState == DataRowState.Added || NewDR.RowState == DataRowState.Detached || (NewDR.RowState == DataRowState.Unchanged && NewDR["ID"] == DBNull.Value))
                {
                    userTextBoxPeriods.Enabled = comboBoxPeriod.Enabled = true;
                }
                else
                {
                    userTextBoxPeriods.Enabled = comboBoxPeriod.Enabled = false;
                    /* --- scos 31.10.2012 - mail Oana ---
                    buttonClearBallance.Enabled = (Math.Abs(Convert.ToDouble(NewDR["ballance"])) < 1 && Math.Abs(Convert.ToDouble(NewDR["ballance"])) > 0);
                    */
                    buttonClearBallance.Enabled = true;
                    buttonIncasare.Enabled = true;
                    FillPayments(Convert.ToInt32(NewDR["id"]));
                }
                userTextBoxPaid.Enabled = (NewDR["invoicerequirement_id"] == DBNull.Value);
            }
            else
            {
                dateTimePickerDate.Value = DateTime.Now;
                userTextBoxPeriods.Enabled = comboBoxPeriod.Enabled = true;
                labelAmountExchanged.Visible = false;
                labelPaidExchanged.Visible = false;
                userTextBoxPaidRon.Visible = false;
                labelBallanceExchanged.Visible = false;
                labelPaidRon.Visible = false;
            }
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
        }

        private void FillCombos()
        {
            DataAccess da = new DataAccess();

            da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_list");
            DataTable dtOwners = da.ExecuteSelectQuery().Tables[0];
            if (dtOwners != null)
            {
                comboBoxOwner.DisplayMember = "name";
                comboBoxOwner.ValueMember = "id";
                comboBoxOwner.DataSource = dtOwners;
            }

            da = new DataAccess(CommandType.StoredProcedure, "SERVICESsp_listCombo");
            DataTable dtServices = da.ExecuteSelectQuery().Tables[0];
            if (dtServices != null)
            {
                comboBoxService.DisplayMember = "name";
                comboBoxService.ValueMember = "id";
                comboBoxService.DataSource = dtServices;
            }

            da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_currencies_list");
            DataTable dtc1 = da.ExecuteSelectQuery().Tables[0];
            if (dtc1 != null)
            {
                comboBoxCurrency.DisplayMember = "currency";
                comboBoxCurrency.ValueMember = "currency";
                comboBoxCurrency.DataSource = dtc1;
            }

            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "IE_STATUS") });
            DataTable dtStatus = da.ExecuteSelectQuery().Tables[0];
            if (dtStatus != null)
            {
                comboBoxStatus.DisplayMember = "name";
                comboBoxStatus.ValueMember = "id";
                comboBoxStatus.DataSource = dtStatus;
            }

            comboBoxPeriod.Items.Clear();
            comboBoxPeriod.Items.Add("---");
            comboBoxPeriod.Items.Add("Day");
            comboBoxPeriod.Items.Add("Month");
            comboBoxPeriod.Items.Add("Year");

            comboBoxStatus.SelectedIndex = 0;
            comboBoxType.SelectedIndex = 0;
            comboBoxCurrency.SelectedIndex = 0;
        }

        private void FillInfo()
        {
            try
            {
                if (NewDR != null && NewDR.RowState != DataRowState.Added && NewDR.RowState != DataRowState.Detached && !(NewDR.RowState == DataRowState.Unchanged && NewDR["ID"] == DBNull.Value))
                {
                    // invisible textboxes
                    try {
                        month.Text = NewDR["month"].ToString();
                        contract_service_additional_cost_id.Text = NewDR["contract_service_additional_cost_id"].ToString();
                        source.Text = NewDR["source"].ToString();
                        invoicerequirement_id.Text = NewDR["invoicerequirement_id"].ToString();
                        invoice_id.Text = NewDR["invoice_id"].ToString();
                        bank_account_details.Text = NewDR["bank_account_details"].ToString();
                    }
                    catch { }

                    comboBoxType.SelectedItem = NewDR["type"].ToString();
                    comboBoxOwner.SelectedValue = NewDR["owner_id"];
                    comboBoxService.SelectedValue = NewDR["contractservice_id"];
                    try
                    {
                        dateTimePickerDate.Value = Convert.ToDateTime(NewDR["date"]);
                    }
                    catch { dateTimePickerDate.Value = DateTime.Now; }

                    if(NewDR["currency"] != DBNull.Value && NewDR["currency"].ToString() != "")
                        comboBoxCurrency.SelectedValue = NewDR["currency"];

                    userTextBoxComments.Text = NewDR["service_description"].ToString();

                    foreach (DataGridViewRow dgvr in dataGridViewProperties.Rows)
                    {
                        if (dgvr.Cells["ID"].Value.ToString() == NewDR["PROPERTY_ID"].ToString())
                        {
                            dgvr.Selected = true;
                            dataGridViewProperties.CurrentCell = dgvr.Cells["NAME"];
                            break;
                        }
                    }
                    if (NewDR["INVOICE_ID"] != DBNull.Value && NewDR["INVOICE_ID"] != null)
                    {
                        Invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", NewDR["invoice_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                        userTextBoxInvoice.Text = String.Format("{0} {1} / {2}", Invoice["series"].ToString(), Invoice["number"].ToString().PadLeft(5, '0'), Convert.ToDateTime(Invoice["date"]).ToString(SettingsClass.DateFormat));
                        invoice_id.Text = NewDR["invoice_id"].ToString();
                    }
                    comboBoxStatus.SelectedValue = NewDR["status_id"];
                    buttonInvoice.Enabled = NewDR["invoice_id"] != DBNull.Value;

                    #region --- old - commented on 20.03.13 ---
                    /*
                    labelPrice.Text = String.Format("{0} ({1}):", labelPrice.Text, NewDR["currency"].ToString());
                    userTextBoxPrice.Text = Math.Abs(Convert.ToDouble(NewDR["amount"])).ToString();
                    labelPaid.Text = String.Format("{0} ({1}):", labelPaid.Text.IndexOf("(") > 0 ? labelPaid.Text.Remove(labelPaid.Text.IndexOf("(") - 1) : labelPaid.Text, NewDR["currency"].ToString());
                    userTextBoxPaid.Text = Math.Abs(Convert.ToDouble(NewDR["amount_paid"] == DBNull.Value ? "0" : NewDR["amount_paid"])).ToString();
                    labelBallance.Text = String.Format("{0} ({1}):", labelBallance.Text.IndexOf("(") > 0 ? labelBallance.Text.Remove(labelBallance.Text.IndexOf("(") - 1) : labelBallance.Text, NewDR["currency"].ToString());
                    userTextBoxBallance.Text = Math.Abs(Convert.ToDouble(NewDR["ballance"] == DBNull.Value ? "0" : NewDR["ballance"])).ToString();

                    if (NewDR["currency"].ToString() != "RON" && NewDR["currency"] != DBNull.Value)
                    {
                        labelPaidRon.Visible = true;
                        try
                        {
                            labelAmountExchanged.Visible = true;
                            labelAmountExchanged.Text = String.Format("({0} RON)", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(NewDR["amount"])), NewDR["currency"].ToString().ToLower(), "ron", dateTimePickerDate.Value), 2).ToString());
                        }
                        catch { labelAmountExchanged.Visible = false; }
                        try
                        {
                            labelPaidExchanged.Visible = true;
                            labelPaidExchanged.Text = String.Format("({0} RON)", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(NewDR["amount_paid"])), NewDR["currency"].ToString().ToLower(), "ron", dateTimePickerDate.Value), 2).ToString());
                        }
                        catch { labelPaidExchanged.Visible = false; }
                        try
                        {
                            userTextBoxPaidRon.Visible = true;
                            userTextBoxPaidRon.Text = Math.Abs(Convert.ToDouble(NewDR["amount_paid_ron"] == DBNull.Value ? "0" : NewDR["amount_paid_ron"])).ToString();
                        }
                        catch { }
                        try
                        {
                            labelBallanceExchanged.Visible = true;
                            labelBallanceExchanged.Text = String.Format("({0} RON)", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(NewDR["ballance"])), NewDR["currency"].ToString().ToLower(), "ron", dateTimePickerDate.Value), 2).ToString());
                        }
                        catch { labelBallanceExchanged.Visible = false; }
                    }
                    else
                    {
                        labelAmountExchanged.Visible = false;
                        labelPaidExchanged.Visible = false;
                        userTextBoxPaidRon.Visible = false;
                        labelBallanceExchanged.Visible = false;
                        labelPaidRon.Visible = false;
                    }

                    try
                    {
                        labelVAT.Text = String.Format("{0} ({1})", labelVAT.Text, NewDR["currency"].ToString());
                        userTextBoxVAT.Text = Math.Round(Math.Abs(Convert.ToDouble(userTextBoxPrice.Text)) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2).ToString();
                    }
                    catch { }
                    */
                    #endregion

                    checkBoxUseVat.Checked = (InitialDetailedRow["vat (ron)"] != DBNull.Value && Convert.ToDouble(InitialDetailedRow["vat (ron)"]) != 0) || (InitialDetailedRow["vat (eur)"] != DBNull.Value && Convert.ToDouble(InitialDetailedRow["vat (eur)"]) != 0);
                    userTextBoxPrice.Text = InitialDetailedRow["original amount (eur)"] == DBNull.Value ? "" : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["original amount (eur)"])),2).ToString();
                    userTextBoxPriceRon.Text = InitialDetailedRow["original amount (ron)"] == DBNull.Value ? "" : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["original amount (ron)"])),2).ToString();
                    userTextBoxVATEur.Text = InitialDetailedRow["vat (eur)"] == DBNull.Value ? "" : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["vat (eur)"])), 2).ToString();
                    userTextBoxVATRon.Text = InitialDetailedRow["vat (ron)"] == DBNull.Value ? "" : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["vat (ron)"])), 2).ToString();
                    userTextBoxTotal.Text = InitialDetailedRow["total (eur)"] == DBNull.Value ? "" : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["total (eur)"])), 2).ToString();
                    userTextBoxTotalRon.Text = InitialDetailedRow["total (ron)"] == DBNull.Value ? "" : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["total (ron)"])), 2).ToString();
                    userTextBoxPaid.Text = InitialDetailedRow["amount paid (eur)"] == DBNull.Value ? "" : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["amount paid (eur)"])),2).ToString();
                    userTextBoxPaidRon.Text = InitialDetailedRow["amount paid (ron)"] == DBNull.Value ? "" : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["amount paid (ron)"])),2).ToString();
                    userTextBoxBallance.Text = InitialDetailedRow["ballance (eur)"] == DBNull.Value ? "" : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["ballance (eur)"])),2).ToString();
                    userTextBoxBallanceRon.Text = InitialDetailedRow["ballance (ron)"] == DBNull.Value ? "" : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["ballance (ron)"])), 2).ToString();
                    /* -- we take the value from db --
                    try
                    {
                        //labelVAT.Text = String.Format("{0} ({1})", labelVAT.Text, NewDR["currency"].ToString());
                        userTextBoxVATEur.Text = Math.Round(Math.Abs(Convert.ToDouble(userTextBoxPrice.Text)) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2).ToString();
                    }
                    catch { }
                    */
                    FillExchangedValues();
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void FillExchangedValues()
        {
            try
            {
                labelAmountExchanged.Visible = userTextBoxPrice.Text.Trim() != "";
                labelAmountRonExchanged.Visible = userTextBoxPriceRon.Text.Trim() != "";
                labelExchangedVat.Visible = userTextBoxVATEur.Text.Trim() != "";
                labelVATRonExchanged.Visible = userTextBoxVATRon.Text.Trim() != "";
                labelExchangedTotal.Visible = userTextBoxTotal.Text.Trim() != "";
                labelTotalRonExchanged.Visible = userTextBoxTotalRon.Text.Trim() != "";
                labelPaidExchanged.Visible = userTextBoxPaid.Text.Trim() != "";
                labelPaidRonExchanged.Visible = userTextBoxPaidRon.Text.Trim() != "";
                labelBallanceExchanged.Visible = userTextBoxBallance.Text.Trim() != "";
                labelBallanceRonExchanged.Visible = userTextBoxBallanceRon.Text.Trim() != "";

                //labelAmountExchanged.Text = InitialDetailedRow["original amount (eur)"] == DBNull.Value ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(InitialDetailedRow["original amount (eur)"])), comboBoxCurrency.SelectedValue.ToString(), "RON", dateTimePickerDate.Value), 2).ToString());
                labelAmountExchanged.Text = userTextBoxPrice.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxPrice.Text)), comboBoxCurrency.SelectedValue.ToString(), "RON", dateTimePickerDate.Value), 2).ToString());
                //labelAmountRonExchanged.Text = InitialDetailedRow["original amount (ron)"] == DBNull.Value ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(InitialDetailedRow["original amount (ron)"])), "RON", comboBoxCurrency.SelectedValue.ToString(), dateTimePickerDate.Value), 2).ToString());
                labelAmountRonExchanged.Text = userTextBoxPriceRon.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxPriceRon.Text)), "RON", comboBoxCurrency.SelectedValue.ToString(), dateTimePickerDate.Value), 2).ToString());

                labelExchangedVat.Text = userTextBoxVATEur.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxVATEur.Text)), comboBoxCurrency.SelectedValue.ToString(), "RON", dateTimePickerDate.Value), 2).ToString());
                labelVATRonExchanged.Text = userTextBoxVATRon.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxVATRon.Text)), "RON", comboBoxCurrency.SelectedValue.ToString(), dateTimePickerDate.Value), 2).ToString());
                labelExchangedTotal.Text = userTextBoxTotal.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxTotal.Text)), comboBoxCurrency.SelectedValue.ToString(), "RON", dateTimePickerDate.Value), 2).ToString());
                labelTotalRonExchanged.Text = userTextBoxTotalRon.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxTotalRon.Text)), "RON", comboBoxCurrency.SelectedValue.ToString(), dateTimePickerDate.Value), 2).ToString());

                //labelPaidExchanged.Text = InitialDetailedRow["amount paid (eur)"] == DBNull.Value ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(InitialDetailedRow["amount paid (eur)"])), comboBoxCurrency.SelectedValue.ToString(), "RON", dateTimePickerDate.Value), 2).ToString());
                labelPaidExchanged.Text = userTextBoxPaid.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxPaid.Text)), comboBoxCurrency.SelectedValue.ToString(), "RON", dateTimePickerDate.Value), 2).ToString());
                //labelPaidRonExchanged.Text = InitialDetailedRow["amount paid (ron)"] == DBNull.Value ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(InitialDetailedRow["amount paid (ron)"])), "RON", comboBoxCurrency.SelectedValue.ToString(), dateTimePickerDate.Value), 2).ToString());
                labelPaidRonExchanged.Text = userTextBoxPaidRon.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxPaidRon.Text)), "RON", comboBoxCurrency.SelectedValue.ToString(), dateTimePickerDate.Value), 2).ToString());
                //labelBallanceExchanged.Text = InitialDetailedRow["ballance (eur)"] == DBNull.Value ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(InitialDetailedRow["ballance (eur)"])), comboBoxCurrency.SelectedValue.ToString(), "RON", dateTimePickerDate.Value), 2).ToString());
                labelBallanceExchanged.Text = userTextBoxBallance.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxBallance.Text)), comboBoxCurrency.SelectedValue.ToString(), "RON", dateTimePickerDate.Value), 2).ToString());
                //labelBallanceRonExchanged.Text = InitialDetailedRow["ballance (ron)"] == DBNull.Value ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(InitialDetailedRow["ballance (ron)"])), "RON", comboBoxCurrency.SelectedValue.ToString(), dateTimePickerDate.Value), 2).ToString());
                labelBallanceRonExchanged.Text = userTextBoxBallanceRon.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxBallanceRon.Text)), "RON", comboBoxCurrency.SelectedValue.ToString(), dateTimePickerDate.Value), 2).ToString());

                //labelExchangedVat.Text = userTextBoxVATEur.Text.Trim() == "" ? "" : String.Format("({0})", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxVATEur.Text)), comboBoxCurrency.SelectedValue.ToString(), "RON", dateTimePickerDate.Value), 2).ToString());

                if ((NewDR["INVOICE_ID"] != DBNull.Value && NewDR["INVOICE_ID"] != null) || (NewDR["amount_paid"] != DBNull.Value && Convert.ToDouble(NewDR["amount_paid"]) != 0) || (NewDR["amount_paid_ron"] != DBNull.Value && Convert.ToDouble(NewDR["amount_paid_ron"]) != 0))
                {
                    comboBoxCurrency.Enabled = false;
                    userTextBoxPrice.Enabled = false;
                    userTextBoxPriceRon.Enabled = false;
                    checkBoxUseVat.Enabled = false;
                }
            }
            catch { }
        }

        public void GenerateMySqlParameters()
        {
            try
            {
                MySqlParameters.Clear();
                if (NewDR != null)
                {
                    //NewDR["type"] = (comboBoxType.SelectedItem.ToString() == "EXPENSE" ? false : true);
                    NewDR["type"] = comboBoxType.SelectedItem.ToString();
                    NewDR["owner_id"] = comboBoxOwner.SelectedValue;
                    NewDR["property_id"] = dataGridViewProperties.SelectedRows[0].Cells["id"].Value;
                    //NewDR["contractservice_id"] = comboBoxService.SelectedValue;
                    NewDR["contractservice_id"] = CommonFunctions.SetNullable(comboBoxService);
                    NewDR["currency"] = comboBoxCurrency.SelectedValue;
                    NewDR["date"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerDate.Value);
                    NewDR["month"] = month.Text == "" ? null : month.Text;
                    NewDR["amount"] = NewDR["currency"].ToString() == "RON" ? Math.Abs(Convert.ToDouble(userTextBoxPriceRon.Text)) : Math.Abs(Convert.ToDouble(userTextBoxPrice.Text));
                    NewDR["service_description"] = userTextBoxComments.Text;
                    NewDR["contract_service_additional_cost_id"] = contract_service_additional_cost_id.Text == "" ? null : contract_service_additional_cost_id.Text;
                    NewDR["status_id"] = CommonFunctions.SetNullable(comboBoxStatus);
                    NewDR["source"] = source.Text == "" ? null : source.Text;
                    NewDR["invoicerequirement_id"] = invoicerequirement_id.Text == "" ? null : invoicerequirement_id.Text;
                    NewDR["invoice_id"] = invoice_id.Text == "" ? null : invoice_id.Text;
                    NewDR["bank_account_details"] = bank_account_details.Text == "" ? null : bank_account_details.Text;
                    NewDR["amount_paid"] = userTextBoxPaid.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxPaid.Text));
                    NewDR["amount_paid_ron"] = userTextBoxPaidRon.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxPaidRon.Text));
                    NewDR["ballance"] = NewDR["currency"].ToString() == "RON" ? userTextBoxBallanceRon.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxBallanceRon.Text)) : userTextBoxBallance.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxBallance.Text));
                    NewDR["vat"] = NewDR["currency"].ToString() == "RON" ? userTextBoxVATRon.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxVATRon.Text)) : userTextBoxVATEur.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxVATEur.Text));
                    NewDR["amount_total"] = NewDR["currency"].ToString() == "RON" ? userTextBoxTotalRon.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxTotalRon.Text)) : userTextBoxTotal.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxTotal.Text));
                }
                else
                {
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }

                    MySqlParameter _TYPE = new MySqlParameter("_TYPE", (comboBoxType.SelectedItem.ToString() == "EXPENSE" ? false : true));
                    MySqlParameters.Add(_TYPE);

                    MySqlParameter _OWNER_ID = new MySqlParameter("_OWNER_ID", CommonFunctions.SetNullable(comboBoxOwner));
                    MySqlParameters.Add(_OWNER_ID);

                    MySqlParameter _PROPERTY_ID = new MySqlParameter("_PROPERTY_ID", dataGridViewProperties.SelectedRows[0].Cells["id"].Value);
                    MySqlParameters.Add(_PROPERTY_ID);

                    MySqlParameter _CONTRACTSERVICE_ID = new MySqlParameter("_CONTRACTSERVICE_ID", CommonFunctions.SetNullable(comboBoxService));
                    MySqlParameters.Add(_CONTRACTSERVICE_ID);

                    MySqlParameter _CURRENCY = new MySqlParameter("_CURRENCY", comboBoxCurrency.SelectedValue);
                    MySqlParameters.Add(_CURRENCY);

                    MySqlParameter _DATE = new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerDate.Value));
                    MySqlParameters.Add(_DATE);

                    MySqlParameter _MONTH = new MySqlParameter("_MONTH", DBNull.Value);
                    MySqlParameters.Add(_MONTH);

                    MySqlParameter _AMOUNT = new MySqlParameter("_AMOUNT", comboBoxCurrency.SelectedValue.ToString() == "RON" ? Math.Abs(Convert.ToDouble(userTextBoxPriceRon.Text)) : Math.Abs(Convert.ToDouble(userTextBoxPrice.Text)));
                    MySqlParameters.Add(_AMOUNT);

                    MySqlParameter _SERVICE_DESCRIPTION = new MySqlParameter("_SERVICE_DESCRIPTION", userTextBoxComments.Text);
                    MySqlParameters.Add(_SERVICE_DESCRIPTION);

                    MySqlParameter _CONTRACT_SERVICE_ADDITIONAL_COST_ID = new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", DBNull.Value);
                    MySqlParameters.Add(_CONTRACT_SERVICE_ADDITIONAL_COST_ID);

                    MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", CommonFunctions.SetNullable(comboBoxStatus));
                    MySqlParameters.Add(_STATUS_ID);

                    MySqlParameter _SOURCE = new MySqlParameter("_SOURCE", DBNull.Value);
                    MySqlParameters.Add(_SOURCE);

                    MySqlParameter _INVOICEREQUIREMENT_ID = new MySqlParameter("_INVOICEREQUIREMENT_ID", DBNull.Value);
                    MySqlParameters.Add(_INVOICEREQUIREMENT_ID);

                    MySqlParameter _INVOICE_ID = new MySqlParameter("_INVOICE_ID", DBNull.Value);
                    MySqlParameters.Add(_INVOICE_ID);

                    MySqlParameter _BANK_ACCOUNT_DETAILS = new MySqlParameter("_BANK_ACCOUNT_DETAILS", DBNull.Value);
                    MySqlParameters.Add(_BANK_ACCOUNT_DETAILS);

                    MySqlParameter _AMOUNT_PAID = new MySqlParameter("_AMOUNT_PAID", userTextBoxPaid.Text.Trim() == "" ? "0" : userTextBoxPaid.Text);
                    MySqlParameters.Add(_AMOUNT_PAID);

                    MySqlParameter _AMOUNT_PAID_RON = new MySqlParameter("_AMOUNT_PAID_RON", userTextBoxPaidRon.Text.Trim() == "" ? "0" : userTextBoxPaidRon.Text);
                    MySqlParameters.Add(_AMOUNT_PAID_RON);

                    MySqlParameter _BALLANCE = new MySqlParameter("_BALLANCE", comboBoxCurrency.SelectedValue.ToString() == "RON" ? userTextBoxBallanceRon.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxBallanceRon.Text)) : userTextBoxBallance.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxBallance.Text)));
                    MySqlParameters.Add(_BALLANCE);

                    MySqlParameter _VAT = new MySqlParameter("_VAT", comboBoxCurrency.SelectedValue.ToString() == "RON" ? userTextBoxVATRon.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxVATRon.Text)) : userTextBoxVATEur.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxVATEur.Text)));
                    MySqlParameters.Add(_VAT);

                    MySqlParameter _AMOUNT_TOTAL = new MySqlParameter("_AMOUNT_TOTAL", comboBoxCurrency.SelectedValue.ToString() == "RON" ? userTextBoxTotalRon.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxTotalRon.Text)) : userTextBoxTotal.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxTotal.Text)));
                    MySqlParameters.Add(_AMOUNT_TOTAL);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        public void GenerateMySqlParameters(DateTime data)
        {
            try
            {
                MySqlParameters.Clear();

                MySqlParameter _TYPE = new MySqlParameter("_TYPE", (comboBoxType.SelectedItem.ToString() == "EXPENSE" ? false : true));
                MySqlParameters.Add(_TYPE);

                MySqlParameter _OWNER_ID = new MySqlParameter("_OWNER_ID", CommonFunctions.SetNullable(comboBoxOwner));
                MySqlParameters.Add(_OWNER_ID);

                MySqlParameter _PROPERTY_ID = new MySqlParameter("_PROPERTY_ID", dataGridViewProperties.SelectedRows[0].Cells["id"].Value);
                MySqlParameters.Add(_PROPERTY_ID);

                MySqlParameter _CONTRACTSERVICE_ID = new MySqlParameter("_CONTRACTSERVICE_ID", CommonFunctions.SetNullable(comboBoxService));
                MySqlParameters.Add(_CONTRACTSERVICE_ID);

                MySqlParameter _CURRENCY = new MySqlParameter("_CURRENCY", comboBoxCurrency.SelectedValue);
                MySqlParameters.Add(_CURRENCY);

                MySqlParameter _DATE = new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(data));
                MySqlParameters.Add(_DATE);

                MySqlParameter _MONTH = new MySqlParameter("_MONTH", DBNull.Value);
                MySqlParameters.Add(_MONTH);

                MySqlParameter _AMOUNT = new MySqlParameter("_AMOUNT", comboBoxCurrency.SelectedValue.ToString() == "RON" ? Math.Abs(Convert.ToDouble(userTextBoxPriceRon.Text)) : Math.Abs(Convert.ToDouble(userTextBoxPrice.Text)));
                MySqlParameters.Add(_AMOUNT);

                MySqlParameter _SERVICE_DESCRIPTION = new MySqlParameter("_SERVICE_DESCRIPTION", userTextBoxComments.Text);
                MySqlParameters.Add(_SERVICE_DESCRIPTION);

                MySqlParameter _CONTRACT_SERVICE_ADDITIONAL_COST_ID = new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", DBNull.Value);
                MySqlParameters.Add(_CONTRACT_SERVICE_ADDITIONAL_COST_ID);

                MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", comboBoxStatus.SelectedValue);
                MySqlParameters.Add(_STATUS_ID);

                MySqlParameter _SOURCE = new MySqlParameter("_SOURCE", DBNull.Value);
                MySqlParameters.Add(_SOURCE);

                MySqlParameter _INVOICEREQUIREMENT_ID = new MySqlParameter("_INVOICEREQUIREMENT_ID", DBNull.Value);
                MySqlParameters.Add(_INVOICEREQUIREMENT_ID);

                MySqlParameter _INVOICE_ID = new MySqlParameter("_INVOICE_ID", DBNull.Value);
                MySqlParameters.Add(_INVOICE_ID);

                MySqlParameter _BANK_ACCOUNT_DETAILS = new MySqlParameter("_BANK_ACCOUNT_DETAILS", DBNull.Value);
                MySqlParameters.Add(_BANK_ACCOUNT_DETAILS);

                MySqlParameter _AMOUNT_PAID = new MySqlParameter("_AMOUNT_PAID", userTextBoxPaid.Text.Trim() == "" ? "0" : userTextBoxPaid.Text);
                MySqlParameters.Add(_AMOUNT_PAID);

                MySqlParameter _AMOUNT_PAID_RON = new MySqlParameter("_AMOUNT_PAID_RON", userTextBoxPaidRon.Text.Trim() == "" ? "0" : userTextBoxPaidRon.Text);
                MySqlParameters.Add(_AMOUNT_PAID_RON);

                MySqlParameter _BALLANCE = new MySqlParameter("_BALLANCE", comboBoxCurrency.SelectedValue.ToString() == "RON" ? userTextBoxBallanceRon.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxBallanceRon.Text)) : userTextBoxBallance.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxBallance.Text)));
                MySqlParameters.Add(_BALLANCE);

                MySqlParameter _VAT = new MySqlParameter("_VAT", comboBoxCurrency.SelectedValue.ToString() == "RON" ? userTextBoxVATRon.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxVATRon.Text)) : userTextBoxVATEur.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxVATEur.Text)));
                MySqlParameters.Add(_VAT);

                MySqlParameter _AMOUNT_TOTAL = new MySqlParameter("_AMOUNT_TOTAL", comboBoxCurrency.SelectedValue.ToString() == "RON" ? userTextBoxTotalRon.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxTotalRon.Text)) : userTextBoxTotal.Text.Trim() == "" ? 0 : Math.Abs(Convert.ToDouble(userTextBoxTotal.Text)));
                MySqlParameters.Add(_AMOUNT_TOTAL);
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateData())
                {
                    //this.DialogResult = DialogResult.Cancel;
                    base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                    return;
                }
                if (userTextBoxPeriods.Text.Trim() != "" && comboBoxPeriod.SelectedIndex > 0)
                {
                    if (!ValidateDataPeriods())
                    {
                        //this.DialogResult = DialogResult.Cancel;
                        base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                        return;
                    }
                    switch (comboBoxPeriod.SelectedItem.ToString())
                    {
                        case "Day":
                            int days = Convert.ToInt32(userTextBoxPeriods.Text.Trim());
                            for (int i = 0; i < days; i++)
                            {
                                GenerateMySqlParameters(dateTimePickerDate.Value.AddDays(i));
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_insert", MySqlParameters.ToArray());
                                da.ExecuteInsertQuery();
                            }
                            break;
                        case "Month":
                            int months = Convert.ToInt32(userTextBoxPeriods.Text.Trim());
                            for (int i = 0; i < months; i++)
                            {
                                GenerateMySqlParameters(dateTimePickerDate.Value.AddMonths(i));
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_insert", MySqlParameters.ToArray());
                                da.ExecuteInsertQuery();
                            }
                            break;
                        case "Year":
                            int years = Convert.ToInt32(userTextBoxPeriods.Text.Trim());
                            for (int i = 0; i < years; i++)
                            {
                                GenerateMySqlParameters(dateTimePickerDate.Value.AddYears(i));
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_insert", MySqlParameters.ToArray());
                                da.ExecuteInsertQuery();
                            }
                            break;
                    }
                }
                else
                {
                    GenerateMySqlParameters();
                    if (NewDR == null) //add direct
                    {
                        try
                        {
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_insert", MySqlParameters.ToArray());
                            da.ExecuteInsertQuery();
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
                }
                InitialDR = CommonFunctions.CopyDataRow(NewDR);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateDataPeriods()
        {
            bool toReturn = true;
            errorProvider1.SetError(userTextBoxPeriods, "");

            if (!Validator.IsNumeric(userTextBoxPeriods.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxPeriods, Language.GetErrorText("errorInvalidPeriods", "Invalid periods number!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxPeriods.Name, Language.GetErrorText("errorInvalidPeriods", "Invalid periods number!")));
                toReturn = false;
            }
            return toReturn;
        }

        private bool ValidateData()
        {
            bool toReturn = true;
            errorProvider1.SetError(userTextBoxPrice, "");
            errorProvider1.SetError(userTextBoxPaid, "");
            errorProvider1.SetError(comboBoxOwner, "");
            errorProvider1.SetError(comboBoxService, "");
            errorProvider1.SetError(dataGridViewProperties, "");
            errorProvider1.SetError(comboBoxStatus, "");
            errorProvider1.SetError(userTextBoxPaidRon, "");
            base.ErrorList.Clear();
            base.listBoxErrors.DataSource = null;

            if (comboBoxOwner.SelectedValue == null || comboBoxOwner.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxOwner, Language.GetErrorText("errorEmptyOwner", "You must select the owner!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwner.Name, Language.GetErrorText("errorEmptyOwner", "You must select the owner!")));
                toReturn = false;
            }
            if (comboBoxStatus.SelectedValue == null || comboBoxStatus.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxStatus, Language.GetErrorText("errorEmptyStatus", "You must select the status!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxStatus.Name, Language.GetErrorText("errorEmptyStatus", "You must select the status!")));
                toReturn = false;
            }
            
            if (comboBoxService.SelectedValue == null || comboBoxService.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxService, Language.GetErrorText("errorEmptyService", "You must select the service!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxService.Name, Language.GetErrorText("errorEmptyService", "You must select the service!")));
                toReturn = false;
            }
            
            if (dataGridViewProperties.SelectedRows.Count <= 0)
            {
                errorProvider1.SetError(dataGridViewProperties, Language.GetErrorText("errorEmptyProperty", "You must select the property!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(dataGridViewProperties.Name, Language.GetErrorText("errorEmptyProperty", "You must select the property!")));
                toReturn = false;
            }
            if (!Validator.IsNumeric(userTextBoxPrice.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxPrice, Language.GetErrorText("errorInvalidPrice", "Invalid Price value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxPrice.Name, Language.GetErrorText("errorInvalidPrice", "Invalid Price value!")));
                toReturn = false;
            }
            if (!Validator.IsNumeric(userTextBoxPaid.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxPaid, Language.GetErrorText("errorInvalidPaidValue", "Invalid Paid value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxPaid.Name, Language.GetErrorText("errorInvalidPaidValue", "Invalid Paid value!")));
                toReturn = false;
            }
            if (!Validator.IsNumeric(userTextBoxPaidRon.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxPaidRon, Language.GetErrorText("errorInvalidPaidRonValue", "Invalid Paid value (RON)!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxPaidRon.Name, Language.GetErrorText("errorInvalidPaidRonValue", "Invalid Paid value (RON)!")));
                toReturn = false;
            }

            try
            {
                if (NewDR["status"].ToString().ToLower() == "invoiced" && ((DataRowView)comboBoxStatus.SelectedItem)["name"].ToString().ToLower() == "paid")
                {
                    errorProvider1.SetError(comboBoxStatus, Language.GetErrorText("errorInvalidStatus1", "You can not change the status from Invoiced to Paid directly!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxStatus.Name, Language.GetErrorText("errorInvalidStatus1", "You can not change the status from Invoiced to Paid directly!")));
                    toReturn = false;
                }
            }
            catch { }
            try
            {
                if (((DataRowView)comboBoxStatus.SelectedItem)["name"].ToString().ToLower() == "paid" && Convert.ToDouble(userTextBoxPrice.Text) > Convert.ToDouble(userTextBoxPaid.Text))
                {
                    errorProvider1.SetError(comboBoxStatus, Language.GetErrorText("errorInvalidStatus2", "The amount is greater than the Paid value! You can not change the status to Paid!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxStatus.Name, Language.GetErrorText("errorInvalidStatus2", "The amount is greater than the Paid value! You can not change the status to Paid!")));
                    toReturn = false;
                }
            }
            catch { }
            try
            {
                if (((DataRowView)comboBoxStatus.SelectedItem)["name"].ToString().ToLower() == "partially paid" && (Convert.ToDouble(userTextBoxPaid.Text) == 0 || Convert.ToDouble(userTextBoxPrice.Text) <= Convert.ToDouble(userTextBoxPaid.Text)))
                {
                    errorProvider1.SetError(comboBoxStatus, Language.GetErrorText("errorInvalidStatus3", "You can not change the status to Partially paid for the filled values!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxStatus.Name, Language.GetErrorText("errorInvalidStatus3", "You can not change the status to Partially paid for the filled values!")));
                    toReturn = false;
                }
            }
            catch { }

            if (userTextBoxPrice.Text == "" && userTextBoxPriceRon.Text == "")
            {
                errorProvider1.SetError(userTextBoxPrice, Language.GetErrorText("errorInvalidPrice", "You must fill in the Price!"));
                errorProvider1.SetError(userTextBoxPriceRon, Language.GetErrorText("errorInvalidPrice", "You must fill in the Price!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxPaidRon.Name, Language.GetErrorText("errorInvalidPrice", "You must fill in the Price!")));
                toReturn = false;
            }
            return toReturn;
        }

        private void pictureBoxSelectOwner_Click(object sender, EventArgs e)
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

        private void comboBoxOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedIndex > 0)
                {
                    DataRowView drv = (DataRowView)((ComboBox)sender).SelectedItem;
                    int _owner_id = Convert.ToInt32(drv.Row["id"]);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", _owner_id) });
                    DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
                    if (dtProperties == null)
                    {
                        dtProperties = new DataTable();
                        DataColumn dc = new DataColumn("ID");
                        dc.DataType = Type.GetType("System.Int32");
                        dtProperties.Columns.Add(dc);
                        dc = new DataColumn("NAME");
                        dc.DataType = Type.GetType("System.String");
                        dtProperties.Columns.Add(dc);
                        dtProperties.AcceptChanges();
                    }
                    if (dtProperties != null)
                    {
                        DataRow dr = dtProperties.NewRow();
                        dr["ID"] = DBNull.Value;
                        dr["NAME"] = "Not property related";
                        dtProperties.Rows.Add(dr);
                        dtProperties.AcceptChanges();

                        dataGridViewProperties.DataSource = dtProperties;
                        /*
                        dataGridViewProperties.Columns.Remove("assigned");

                        DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
                        c.Name = "Assigned";
                        c.Width = 30;
                        c.HeaderText = "";
                        c.DataPropertyName = "assigned";
                        dataGridViewProperties.Columns.Insert(0, c);
                        */
                        foreach (DataGridViewColumn dc in dataGridViewProperties.Columns)
                        {
                            dc.ReadOnly = dc.Name.ToLower() == "assigned" ? false : true;
                            //dc.Visible = (dc.Name.ToLower() == "name" || dc.Name.ToLower() == "assigned") ? true : false;
                            dc.Visible = (dc.Name.ToLower() == "name") ? true : false;
                        }
                    }
                }
                else
                {
                    dataGridViewProperties.DataSource = null;
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void userTextBoxPrice_Validated(object sender, EventArgs e)
        {
            try
            {
                userTextBoxVATEur.Text = checkBoxUseVat.Checked ? Math.Round(Convert.ToDouble(userTextBoxPrice.Text) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2).ToString() : "0";
                userTextBoxTotal.Text = (Convert.ToDouble(userTextBoxPrice.Text.Trim() == "" ? "0" : userTextBoxPrice.Text) + Convert.ToDouble(userTextBoxVATEur.Text.Trim() == "" ? "0" : userTextBoxVATEur.Text)).ToString();
                userTextBoxBallance.Text = (Convert.ToDouble(userTextBoxTotal.Text.Trim() == "" ? "0" : userTextBoxTotal.Text) - Convert.ToDouble(userTextBoxPaid.Text.Trim() == "" ? "0" : userTextBoxPaid.Text)).ToString();
            }
            catch { }
        }

        private void userTextBoxPriceRon_Validated(object sender, EventArgs e)
        {
            try
            {
                userTextBoxVATRon.Text = checkBoxUseVat.Checked ? Math.Round(Convert.ToDouble(userTextBoxPriceRon.Text) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2).ToString() : "0";
                userTextBoxTotalRon.Text = (Convert.ToDouble(userTextBoxPriceRon.Text.Trim() == "" ? "0" : userTextBoxPriceRon.Text) + Convert.ToDouble(userTextBoxVATRon.Text.Trim() == "" ? "0" : userTextBoxVATRon.Text)).ToString();
                userTextBoxBallanceRon.Text = (Convert.ToDouble(userTextBoxTotalRon.Text.Trim() == "" ? "0" : userTextBoxTotalRon.Text) - Convert.ToDouble(userTextBoxPaidRon.Text.Trim() == "" ? "0" : userTextBoxPaidRon.Text)).ToString();
            }
            catch { }
        }

        private void buttonClearBallance_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmClearBallance", "Are you sure you want to clear the ballance for this income / expense?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_clear_ballance", new object[] { new MySqlParameter("_ID", NewDR["id"]) });
                    da.ExecuteUpdateQuery();
                    userTextBoxBallance.Text = "0";
                    foreach (DataRowView drv in comboBoxStatus.Items)
                    {
                        if (drv["name"].ToString() == "Paid")
                        {
                            comboBoxStatus.SelectedItem = drv;
                            break;
                        }
                    }
                    FillExchangedValues();
                }
            }
            catch { }
        }

        private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string currency = comboBoxCurrency.SelectedValue.ToString();
                if (currency != "RON" && currency != "EUR")
                    MessageBox.Show(Language.GetMessageBoxText("impropperCurrency", "You have selected a currency that is not properly coverred by this application. Errors may occur if you continue!"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                #region --- old - commented on 21.03.2013 ---
                /*
                labelPrice.Text = String.Format("{0} ({1}):", labelPrice.Text.IndexOf("(") > 0 ? labelPrice.Text.Remove(labelPrice.Text.IndexOf("(") - 1) : labelPrice.Text, currency);
                //userTextBoxPrice.Text = Math.Abs(Convert.ToDouble(NewDR["amount"])).ToString();
                labelPaid.Text = String.Format("{0} ({1}):", labelPaid.Text.IndexOf("(") > 0 ? labelPaid.Text.Remove(labelPaid.Text.IndexOf("(") - 1) : labelPaid.Text, currency);
                //userTextBoxPaid.Text = Math.Abs(Convert.ToDouble(NewDR["amount_paid"] == DBNull.Value ? "0" : NewDR["amount_paid"])).ToString();
                labelBallance.Text = String.Format("{0} ({1}):", labelBallance.Text.IndexOf("(")>0?labelBallance.Text.Remove(labelBallance.Text.IndexOf("(")-1):labelBallance.Text, currency);
                //userTextBoxBallance.Text = Math.Abs(Convert.ToDouble(NewDR["ballance"] == DBNull.Value ? "0" : NewDR["ballance"])).ToString();
                if (((ComboBox)sender).SelectedIndex > -1 && ((ComboBox)sender).SelectedValue.ToString() != "RON")
                {
                    labelPaidRon.Visible = true;
                    try
                    {
                        labelAmountExchanged.Visible = true;
                        labelAmountExchanged.Text = String.Format("({0} RON)", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxPrice.Text)), currency.ToLower(), "ron", dateTimePickerDate.Value), 2).ToString());
                    }
                    catch { labelAmountExchanged.Visible = false; }
                    try
                    {
                        labelPaidExchanged.Visible = true;
                        labelPaidExchanged.Text = String.Format("({0} RON)", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxPaid.Text)), currency.ToLower(), "ron", dateTimePickerDate.Value), 2).ToString());
                    }
                    catch { labelPaidExchanged.Visible = false; }
                    try
                    {
                        userTextBoxPaidRon.Visible = true;
                        userTextBoxPaidRon.Text = Math.Abs(Convert.ToDouble(NewDR["amount_paid_ron"] == DBNull.Value ? "0" : NewDR["amount_paid_ron"])).ToString();
                    }
                    catch { }
                    try
                    {
                        labelBallanceExchanged.Visible = true;
                        labelBallanceExchanged.Text = String.Format("({0} RON)", Math.Round(CommonFunctions.ConvertCurrency(Math.Abs(Convert.ToDouble(userTextBoxBallance.Text)), currency.ToLower(), "ron", dateTimePickerDate.Value), 2).ToString());
                    }
                    catch { labelBallanceExchanged.Visible = false; }
                }
                else
                {
                    labelAmountExchanged.Visible = false;
                    labelPaidExchanged.Visible = false;
                    userTextBoxPaidRon.Visible = false;
                    labelBallanceExchanged.Visible = false;
                    labelPaidRon.Visible = false;
                }
                */
                #endregion

                //if (NewDR["currency"].ToString() != currency)
                //{
                    if (currency == "EUR") {
                        userTextBoxBallance.Text = userTextBoxBallanceRon.Text;  userTextBoxBallanceRon.Text = "";
                        userTextBoxPrice.Text = userTextBoxPriceRon.Text; userTextBoxPriceRon.Text = "";
                        userTextBoxVATEur.Text = userTextBoxVATRon.Text; userTextBoxVATRon.Text = "";
                        userTextBoxTotal.Text = userTextBoxTotalRon.Text; userTextBoxTotalRon.Text = "";
                    }
                    if (currency == "RON") {
                        userTextBoxBallanceRon.Text = userTextBoxBallance.Text; userTextBoxBallance.Text = "";
                        userTextBoxPriceRon.Text = userTextBoxPrice.Text; userTextBoxPrice.Text = "";
                        userTextBoxVATRon.Text = userTextBoxVATEur.Text; userTextBoxVATEur.Text = "";
                        userTextBoxTotalRon.Text = userTextBoxTotal.Text; userTextBoxTotal.Text = "";
                    }
                //}
                userTextBoxPrice.Enabled = currency == "EUR";
                userTextBoxPriceRon.Enabled = currency == "RON";
                FillExchangedValues();
            }
            catch { }
        }

        private void FillPayments(int income_expense_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_get_payments", new object[] { new MySqlParameter("_INCOME_EXPENSE_ID", income_expense_id) });
            dtPayments = da.ExecuteSelectQuery().Tables[0];
            /*
            if (dtPayments != null && dtPayments.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPayments.Rows)
                {
                    listBoxPayments.Items.Add(dr["receipt"].ToString());
                }
            }
            */
            listBoxPayments.DisplayMember = "receipt";
            listBoxPayments.ValueMember = "id";
            listBoxPayments.DataSource = dtPayments;
            buttonDeletePayment.Enabled = !(dtPayments == null || dtPayments.Rows.Count < 1);
        }

        private void buttonIncasare_Click(object sender, EventArgs e)
        {
            Receipt rc = new Receipt(2, NewDR);
            DialogResult ans = rc.ShowDialog();
            //if (ans != System.Windows.Forms.DialogResult.OK) (rc.Dispose(); return;}
            if (!rc.Saved) { rc.Dispose(); return; }
            LastPayment = rc.NewDR;
            rc.Dispose();
            FillPayments(Convert.ToInt32(NewDR["id"]));
            try
            {
                if (LastPayment["currency"].ToString() == "RON")
                {
                    NewDR["amount_paid_ron"] = Math.Round(Math.Abs(Convert.ToDouble(NewDR["amount_paid_ron"] == DBNull.Value ? "0" : NewDR["amount_paid_ron"])) + Convert.ToDouble(LastPayment["amount_paid"] == DBNull.Value ? "0" : LastPayment["amount_paid"]), 2);
                    if (NewDR["currency"].ToString() != "RON")
                    {
                        double exchanged_amount_paid = CommonFunctions.ConvertCurrency(Convert.ToDouble(LastPayment["amount_paid"]), "RON", "EUR", Convert.ToDateTime(LastPayment["date"]));
                        //userTextBoxPaid.Text = (Convert.ToDouble(NewDR["amount_paid"] == DBNull.Value ? 0 : NewDR["amount_paid"]) + exchanged_amount_paid).ToString();
                        userTextBoxPaidRon.Text = NewDR["amount_paid_ron"] == DBNull.Value ? "" : Math.Abs(Convert.ToDouble(NewDR["amount_paid_ron"])).ToString();
                        NewDR["ballance"] = Math.Abs(Convert.ToDouble(NewDR["ballance"] == DBNull.Value ? "0" : NewDR["ballance"])) - exchanged_amount_paid;
                        userTextBoxBallance.Text = NewDR["ballance"].ToString();
                    }
                    else  //RON
                    {
                        userTextBoxPaidRon.Text = NewDR["amount_paid_ron"] == DBNull.Value ? "" : Math.Abs(Convert.ToDouble(NewDR["amount_paid_ron"])).ToString();
                        NewDR["ballance"] = Math.Abs(Convert.ToDouble(NewDR["ballance"] == DBNull.Value ? "0" : NewDR["ballance"])) - Convert.ToDouble(LastPayment["amount_paid"]);
                        userTextBoxBallanceRon.Text = NewDR["ballance"].ToString();
                    }
                }
                else  //EUR
                {
                    NewDR["amount_paid"] = Math.Round(Math.Abs(Convert.ToDouble(NewDR["amount_paid"] == DBNull.Value ? "0" : NewDR["amount_paid"])) + Convert.ToDouble(LastPayment["amount_paid"] == DBNull.Value ? "0" : LastPayment["amount_paid"]), 2);
                    if (NewDR["currency"].ToString() == "EUR")
                    {
                        userTextBoxPaid.Text = NewDR["amount_paid"] == DBNull.Value ? "" : Math.Abs(Convert.ToDouble(NewDR["amount_paid"])).ToString();
                        NewDR["ballance"] = Math.Abs(Convert.ToDouble(NewDR["ballance"] == DBNull.Value ? "0" : NewDR["ballance"])) - Convert.ToDouble(LastPayment["amount_paid"]);
                        userTextBoxBallance.Text = NewDR["ballance"].ToString();
                    }
                    else  //RON
                    {
                        double exchanged_amount_paid = CommonFunctions.ConvertCurrency(Convert.ToDouble(LastPayment["amount_paid"]), "EUR", "RON", Convert.ToDateTime(LastPayment["date"]));
                        userTextBoxPaid.Text = NewDR["amount_paid"] == DBNull.Value ? "" : Math.Abs(Convert.ToDouble(NewDR["amount_paid"])).ToString();
                        NewDR["ballance"] = Math.Abs(Convert.ToDouble(NewDR["ballance"] == DBNull.Value ? "0" : NewDR["ballance"])) - exchanged_amount_paid;
                        userTextBoxBallanceRon.Text = NewDR["ballance"].ToString();
                    }
                }
                FillExchangedValues();
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }

            try
            {
                foreach (DataRowView drv in listBoxPayments.Items)
                {
                    if (LastPayment["id"].ToString() == drv["id"].ToString())
                    {
                        listBoxPayments.SelectedItem = drv;
                        break;
                    }
                }
            }
            catch { }

            if (Convert.ToDouble(NewDR["ballance"] == DBNull.Value ? 0 : NewDR["ballance"]) < Math.Abs(Convert.ToDouble(NewDR["amount"])))
            {
                for (int i = 0; i < comboBoxStatus.Items.Count; i++)
                {
                    if (Convert.ToDouble(NewDR["ballance"] == DBNull.Value ? 0 : NewDR["ballance"]) > 0 && ((DataRowView)comboBoxStatus.Items[i])["name"].ToString().ToLower() == "partially paid")
                    {
                        comboBoxStatus.SelectedIndex = comboBoxStatus.Items.IndexOf(comboBoxStatus.Items[i]);
                        NewDR["status_id"] = ((DataRowView)comboBoxStatus.Items[i])["id"];
                        NewDR["status"] = ((DataRowView)comboBoxStatus.Items[i])["name"];
                        break;
                    }
                    if (Convert.ToDouble(NewDR["ballance"] == DBNull.Value ? 0 : NewDR["ballance"]) == 0 && ((DataRowView)comboBoxStatus.Items[i])["name"].ToString().ToLower() == "paid")
                    {
                        comboBoxStatus.SelectedIndex = comboBoxStatus.Items.IndexOf(comboBoxStatus.Items[i]);
                        NewDR["status_id"] = ((DataRowView)comboBoxStatus.Items[i])["id"];
                        NewDR["status"] = ((DataRowView)comboBoxStatus.Items[i])["name"];
                        break;
                    }
                }
            }
        }

        private void listBoxPayments_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (((DataRowView)listBoxPayments.SelectedItem)["receipt"].ToString().IndexOf("(C)") > -1)
                {
                    Receipt rc = new Receipt(0, Convert.ToInt32(listBoxPayments.SelectedValue));
                    rc.EditMode = 2;
                    rc.ShowDialog();
                    rc.Dispose();
                }
            }
            catch { }
        }

        private void buttonInvoice_Click(object sender, EventArgs e)
        {
            if (Invoice != null)
            {
                var f = new Invoices(Invoice);
                EditMode = 0; // NONE
                main m = FindMainForm();
                //f.Launcher = this;
                //this.ChildLaunched = f;
                f.TopLevel = false;
                f.MdiParent = m;
                m.panelMain.Controls.Add(f);
                f.buttonSave.Enabled = false;
                f.toolStrip1.Enabled = false;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.BringToFront();
                f.Show();
                //f.ShowDialog();
            }
        }

        private void buttonDeletePayment_Click(object sender, EventArgs e)
        {
            if (listBoxPayments.SelectedIndex < 0)
            {
                //MessageBox.Show(Language.GetMessageBoxText("selectPayment", "Please select a payment first!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                base.ErrorList.Add(new KeyValuePair<string, string>(buttonDeletePayment.Name, Language.GetErrorText("selectPayment", "Please select a payment first!")));
                base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                return;
            }
            else
            {
                try
                {
                    DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ans == DialogResult.Yes)
                    {
                        DataRow payment = dtPayments.Select(String.Format("ID = {0}", listBoxPayments.SelectedValue.ToString()))[0];
                        DataAccess da = new DataAccess();
                        switch (payment["source"].ToString())
                        {
                            case "0":
                                da = new DataAccess(CommandType.StoredProcedure, "CASH_RECEIPTSsp_delete", new object[] { new MySqlParameter("_ID", payment["id"]) });
                                da.ExecuteNonQuery();
                                break;
                            case "1":
                                da = new DataAccess(CommandType.StoredProcedure, "BANK_RECEIPTSsp_delete", new object[] { new MySqlParameter("_ID", payment["id"]) });
                                da.ExecuteNonQuery();
                                break;
                        }
                        //FillPayments(Convert.ToInt32(NewDR["id"]));
                        try
                        {
                            PopulateRow(Convert.ToInt32(NewDRId));
                            FillGeneralInfo();
                        }
                        catch { }
                    }
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    base.ErrorList.Add(new KeyValuePair<string, string>(buttonDeletePayment.Name, Language.GetErrorText("null", exp.Message)));
                    base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                }
            }
        }

        private void checkBoxUseVat_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                try{userTextBoxVATEur.Text = InitialDetailedRow["vat (eur)"] == DBNull.Value || Convert.ToDouble(InitialDetailedRow["vat (eur)"]) == 0 ? Math.Round(Convert.ToDouble(userTextBoxPrice.Text) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2).ToString() : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["vat (eur)"])), 2).ToString();}catch{}
                try{userTextBoxVATRon.Text = InitialDetailedRow["vat (ron)"] == DBNull.Value || Convert.ToDouble(InitialDetailedRow["vat (ron)"]) == 0 ? Math.Round(Convert.ToDouble(userTextBoxPriceRon.Text) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2).ToString() : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["vat (ron)"])), 2).ToString();}catch{}
                //try{userTextBoxTotal.Text = InitialDetailedRow["total (eur)"] == DBNull.Value || Convert.ToDouble(InitialDetailedRow["total (eur)"]) == 0 ? Convert.ToString(Convert.ToDouble(userTextBoxPrice.Text) + Convert.ToDouble(userTextBoxVATEur.Text)) : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["total (eur)"])), 2).ToString();}catch{}
                try { userTextBoxTotal.Text = Convert.ToString(Convert.ToDouble(userTextBoxPrice.Text) + Convert.ToDouble(userTextBoxVATEur.Text)); }catch { }
                //try{userTextBoxTotalRon.Text = InitialDetailedRow["total (ron)"] == DBNull.Value || Convert.ToDouble(InitialDetailedRow["total (ron)"]) == 0 ? Convert.ToString(Convert.ToDouble(userTextBoxPriceRon.Text) + Convert.ToDouble(userTextBoxVATRon.Text)) : Math.Round(Math.Abs(Convert.ToDouble(InitialDetailedRow["total (ron)"])), 2).ToString();}catch{}
                try { userTextBoxTotalRon.Text = Convert.ToString(Convert.ToDouble(userTextBoxPriceRon.Text) + Convert.ToDouble(userTextBoxVATRon.Text)).ToString(); }catch { }
                try { userTextBoxBallance.Text = userTextBoxTotal.Text; }catch { }
                try { userTextBoxBallanceRon.Text = userTextBoxTotalRon.Text; }catch { }
            }
            else
            {
                try
                {
                    userTextBoxVATEur.Text = "0";
                    userTextBoxVATRon.Text = "0";
                    userTextBoxBallance.Text = userTextBoxTotal.Text = userTextBoxPrice.Text;
                    userTextBoxBallanceRon.Text = userTextBoxTotalRon.Text = userTextBoxPriceRon.Text;
                }
                catch { }
            }
            FillExchangedValues();
        }

    }
}
