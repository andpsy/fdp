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
    public partial class Receipt : UserForm
    {
        public DataRow NewDR;
        public DataRow Invoice;
        public DataRow IncomeExpense;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();

        public DataRow company;
        public DataRow customer;
        public DataRow invoice;
        public bool Saved = false;

        public Receipt()
        {
            //base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
        }

        public Receipt(int tip, DataRow row) // tip = 0 => receipt ; tip = 1 => invoice ; tip = 2 => income/expense
        {
            //base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            switch (tip)
            {
                case 0:
                    NewDR = row;
                    if(NewDR != null && NewDR["invoice_id"] != DBNull.Value)
                        Invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", NewDR["invoice_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    break;
                case 1:
                    Invoice = row;
                    break;
                case 2:
                    IncomeExpense = row;
                    if (IncomeExpense != null && IncomeExpense["invoice_id"] != DBNull.Value)
                        Invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", IncomeExpense["invoice_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    break;
            }
            InitializeComponent();
        }

        public Receipt(int tip, int id) // tip = 0 => receipt ; tip = 1 => invoice
        {
            //base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            switch (tip)
            {
                case 0:
                    NewDR = (new DataAccess(CommandType.StoredProcedure, "CASH_RECEIPTSsp_get_by_id", new object[] { new MySqlParameter("_ID", id) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    if(NewDR != null && NewDR["invoice_id"] != DBNull.Value)
                        Invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", NewDR["invoice_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    break;
                case 1:
                    Invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", id) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    break;
                case 2:
                    IncomeExpense = (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_get_by_id", new object[] { new MySqlParameter("_ID", id) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    if (IncomeExpense != null && IncomeExpense["invoice_id"] != DBNull.Value)
                        Invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", IncomeExpense["invoice_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    break;
            }
            InitializeComponent();
        }

        private int GetMaxReceiptNumber()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CASH_RECEIPTSsp_get_max_number");
            object max_receipt_number = da.ExecuteScalarQuery();
            try
            {
                return ((max_receipt_number == null || max_receipt_number == DBNull.Value) ? 0 : Convert.ToInt32(max_receipt_number));
            }
            catch { return 0; }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillCombos()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_currencies_list");
            DataTable dtc1 = da.ExecuteSelectQuery().Tables[0];
            if (dtc1 != null)
            {
                comboBoxCurrency.DisplayMember = "currency";
                comboBoxCurrency.ValueMember = "currency";
                comboBoxCurrency.DataSource = dtc1;
                foreach (object x in comboBoxCurrency.Items)
                {
                    if (((DataRowView)x)["currency"].ToString().ToUpper() == "RON")
                    {
                        comboBoxCurrency.SelectedIndex = comboBoxCurrency.Items.IndexOf(x);
                        break;
                    }
                }
            }
        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            FillCombos();
            if (IncomeExpense != null)
            {
                //if (IncomeExpense["invoice_id"] != DBNull.Value && IncomeExpense["invoice_id"] != null)
                //{
                //    Invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", IncomeExpense["invoice_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                //}
                //else
                //{
                    userTextBoxInvoice.Visible = labelInvoice.Visible = pictureBoxSelectInvoice.Visible = !(Invoice == null);
                    userTextBoxSeries.Text = "";
                    userTextBoxNumber.Text = "";
                    userTextBoxAmount.Text = Convert.ToString(Math.Abs(Convert.ToDouble(IncomeExpense["BALLANCE"])));
                    dateTimePickerDate.Value = Convert.ToDateTime(DateTime.Now);
                    foreach (object x in comboBoxCurrency.Items)
                    {
                        if (((DataRowView)x)["currency"].ToString().ToUpper() == IncomeExpense["currency"].ToString().ToUpper())
                        {
                            comboBoxCurrency.SelectedIndex = comboBoxCurrency.Items.IndexOf(x);
                            break;
                        }
                    }
                //}
            }
            if (Invoice != null)
            {
                pictureBoxSelectInvoice.Enabled = false;
                userTextBoxInvoice.Text = String.Format("{0} {1} / {2}", Invoice["series"].ToString(), Invoice["number"].ToString().PadLeft(5, '0'), Convert.ToDateTime(Invoice["date"]).ToString(SettingsClass.DateFormat));
                userTextBoxSeries.Text = SettingsClass.GetCompanySetting("INVOICE SERIES").ToString();
                userTextBoxNumber.Text = (GetMaxReceiptNumber() + 1).ToString().PadLeft(5, '0');
                //userTextBoxAmount.Text = Invoice["total_ron"].ToString();
                //userTextBoxAmount.Text = Convert.ToString(Convert.ToDouble(Invoice["total_ron"]) - Convert.ToDouble(Invoice["amount_paid_ron"]));
                userTextBoxAmount.Text = Convert.ToString(Convert.ToDouble(Invoice["total"] == DBNull.Value ? "0" : Invoice["total"]) - Convert.ToDouble(Invoice["amount_paid"] == DBNull.Value ? "0" : Invoice["amount_paid"]));
                dateTimePickerDate.Value = Convert.ToDateTime(Invoice["date"]);
                foreach (object x in comboBoxCurrency.Items)
                {
                    if (((DataRowView)x)["currency"].ToString().ToUpper() == Invoice["currency"].ToString().ToUpper())
                    {
                        comboBoxCurrency.SelectedIndex = comboBoxCurrency.Items.IndexOf(x);
                        break;
                    }
                }
                comboBoxCurrency.Visible = labelCurrency.Visible = false;
            }
            if (NewDR != null)
            {
                try
                {
                    if(Invoice == null)
                        Invoice = (new DataAccess(CommandType.StoredProcedure, "CASH_RECEIPTSsp_get_invoice", new object[] { new MySqlParameter("_ID", NewDR["id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                }
                catch { Invoice = null; }
                if (Invoice != null)
                {
                    userTextBoxInvoice.Text = String.Format("{0} {1} / {2}", Invoice["series"].ToString(), Invoice["number"].ToString().PadLeft(5, '0'), Convert.ToDateTime(Invoice["date"]).ToString(SettingsClass.DateFormat));
                    /*
                    foreach (object x in comboBoxCurrency.Items)
                    {
                        if (((DataRowView)x)["currency"].ToString().ToUpper() == Invoice["currency"].ToString().ToUpper())
                        {
                            comboBoxCurrency.SelectedIndex = comboBoxCurrency.Items.IndexOf(x);
                            break;
                        }
                    }
                    */
                }
                comboBoxCurrency.SelectedValue = NewDR["currency"].ToString();
                userTextBoxSeries.Text = NewDR["series"].ToString();
                userTextBoxNumber.Text = NewDR["number"].ToString().PadLeft(5, '0');
                //userTextBoxAmount.Text = NewDR["amount_paid_ron"].ToString();
                userTextBoxAmount.Text = NewDR["amount_paid"].ToString();
                dateTimePickerDate.Value = Convert.ToDateTime(NewDR["date"]);
                userTextBoxComments.Text = NewDR["comments"].ToString();
                comboBoxCurrency.Enabled = false;
                buttonSave.Enabled = false;
                buttonPrint.Enabled = true;
            }
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            GenerateMySqlParameters();
            if (!ValidateData())
            {
                //this.DialogResult = DialogResult.Cancel;
                base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                return;
            }
            if (EditMode == 2) // DELETE
            {
                try
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "CASH_RECEIPTSsp_delete", new object[] { new MySqlParameter("_ID", NewDR["id"]) });
                    da.ExecuteUpdateQuery();
                    //NewDR.Delete();
                    NewDR = null;
                    Saved = true;
                }
                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            }

            if (NewDR == null) //add direct
            {
                try
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "CASH_RECEIPTSsp_insert", MySqlParameters.ToArray());
                    NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    int receipt_id = Convert.ToInt32(NewDR["id"]);

                    //update invoice paid amount and status:
                    //(new DataAccess(CommandType.StoredProcedure, "INVOICESsp_change_status", new object[] { new MySqlParameter("_ID", NewDR["invoice_id"]), new MySqlParameter("_AMOUNT_PAID_RON", NewDR["amount_paid_ron"]) })).ExecuteUpdateQuery();
                    if (NewDR["invoice_id"] != DBNull.Value && NewDR["invoice_id"] != null)
                    {
                        //(new DataAccess(CommandType.StoredProcedure, "INVOICESsp_change_status", new object[] { new MySqlParameter("_ID", NewDR["invoice_id"]), new MySqlParameter("_AMOUNT_PAID", NewDR["amount_paid"]) })).ExecuteUpdateQuery();
                        double amount_paid_for_invoice = CommonFunctions.ConvertCurrency(Convert.ToDouble(userTextBoxAmount.Text), comboBoxCurrency.SelectedValue.ToString(), Invoice["currency"].ToString(), dateTimePickerDate.Value);
                        (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_change_status", new object[] { new MySqlParameter("_ID", NewDR["invoice_id"]), new MySqlParameter("_AMOUNT_PAID", amount_paid_for_invoice) })).ExecuteUpdateQuery();
                    }
                    //TO DO: Insert IEs and modifiy Predicted IE
                    //(new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_convert_from_invoice", new object[] {new MySqlParameter("_INVOICE_ID", NewDR["invoice_id"]), new MySqlParameter("_AMOUNT", NewDR["amount_paid_ron"]) })).ExecuteUpdateQuery();

                    double initial_amount = 0;
                    double original_initial_amount = 0;
                    double receipt_amount = Convert.ToDouble(userTextBoxAmount.Text);
                    string currency = comboBoxCurrency.SelectedValue.ToString();
                    double pie_amount_sum = 0;
                    //double exchange_rate = 1;

                    #region --- old ---
                    /*
                    if (invoice["currency"].ToString().ToLower() != "ron")
                    {
                        exchange_rate = Currencies.GetExchangeRate(dateTimePickerDate.Value, invoice["currency"].ToString());
                        initial_amount = Math.Round(Convert.ToDouble(invoice["amount"]) * exchange_rate, 2);
                    }
                    else
                    {
                        initial_amount = Convert.ToDouble(invoice["amount_ron"]);
                    }
                    */
                    /*
                    if (Invoice["currency"].ToString().ToLower() != "ron")
                    {
                        exchange_rate = Currencies.GetExchangeRate(dateTimePickerDate.Value, Invoice["currency"].ToString());
                        initial_amount = Math.Round(receipt_amount / exchange_rate, 2);
                    }
                    else
                    {
                        initial_amount = receipt_amount;
                    }
                    */
                    //initial_amount = Math.Round(receipt_amount - receipt_amount * Convert.ToDouble(Invoice["VAT_PERCENT"])/100, 2);
                    #endregion

                    //original_initial_amount = initial_amount = Math.Round(receipt_amount / (1 + Convert.ToDouble(Invoice == null? 0 : Invoice["VAT_PERCENT"]) / 100), 2);
                    original_initial_amount = initial_amount = Math.Round(receipt_amount, 2); // ACUM AVEM SI I/E urile PENTRU VAT DE ACTUALIZAT  !!!

                    //TO DO:  GET REAL CURRENCY !!! - DONE
                    DateTime data = dateTimePickerDate.Value;

                    DataRow[] pie_rows = null;
                    if (IncomeExpense == null && (NewDR["invoice_id"] != DBNull.Value && NewDR["invoice_id"] != null))
                        pie_rows = (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", NewDR["invoice_id"]) })).ExecuteSelectQuery().Tables[0].Select("1 = 1");
                    else
                        pie_rows = new DataRow[] { IncomeExpense };
                    foreach (DataRow predicted_ie in pie_rows)
                    {
                        //double pie_amount = (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round(Convert.ToDouble(predicted_ie["amount"]) * exchange_rate, 2) : Convert.ToDouble(predicted_ie["amount"]));
                        //double pie_amount = Math.Abs(Convert.ToDouble(predicted_ie["amount"] == DBNull.Value ? "0" : predicted_ie["amount"]));
                        double pie_amount = Math.Round(Math.Abs(Convert.ToDouble(predicted_ie["amount_total"] == DBNull.Value ? "0" : predicted_ie["amount_total"])), 2);
                        double pie_ballance = Math.Round(Math.Abs(Convert.ToDouble(predicted_ie["ballance"] == DBNull.Value ? "0" : predicted_ie["ballance"])), 2);
                        //initial_amount = CommonFunctions.ConvertCurrency(receipt_amount, Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                        //initial_amount = CommonFunctions.ConvertCurrency(Math.Round(receipt_amount - receipt_amount * Convert.ToDouble(Invoice["VAT_PERCENT"]) / 100, 2), Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                        //initial_amount = CommonFunctions.ConvertCurrency(Math.Round(receipt_amount / (1 + Convert.ToDouble(Invoice["VAT_PERCENT"]) / 100), 2), Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                        
                        //initial_amount = CommonFunctions.ConvertCurrency(Math.Round(receipt_amount / (1 + Convert.ToDouble(Invoice == null ? 0 :Invoice["VAT_PERCENT"]) / 100), 2), comboBoxCurrency.SelectedValue.ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                        initial_amount = Math.Round(CommonFunctions.ConvertCurrency(Math.Round(receipt_amount, 2), comboBoxCurrency.SelectedValue.ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data), 2); // ACUM AVEM SI IE FOR VAT DE ACTUALIZAT !!!
                        
                        //if (initial_amount > pie_amount_sum)
                        if (initial_amount > pie_amount_sum && pie_ballance > 0)
                        {
                            //if (pie_amount <= initial_amount - pie_amount_sum) // we still have amount (pie) to sustract
                            if (pie_ballance <= initial_amount - pie_amount_sum) // we still have amount (pie) to sustract
                            {
                                try
                                {
                                    //double amount_ron = CommonFunctions.ConvertCurrency(pie_ballance, Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                                    double amount_ron = CommonFunctions.ConvertCurrency(pie_ballance, predicted_ie["currency"].ToString().ToLower(), comboBoxCurrency.SelectedValue.ToString().ToLower(), data);
                                    (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_change_status3", new object[]{
                                        new MySqlParameter("_ID", predicted_ie["ID"]),
                                        new MySqlParameter("_SOURCE", false),
                                        //new MySqlParameter("_AMOUNT", predicted_ie["AMOUNT"]),
                                        //new MySqlParameter("_AMOUNT", predicted_ie["BALLANCE"]),
                                        new MySqlParameter("_AMOUNT", pie_ballance),
                                        //new MySqlParameter("_AMOUNT_PAID", predicted_ie["CURRENCY"].ToString() == "RON" ? 0 : predicted_ie["BALLANCE"]),
                                        new MySqlParameter("_AMOUNT_PAID", currency == "RON" ? 0 : pie_ballance),
                                        //new MySqlParameter("_AMOUNT_PAID_RON", predicted_ie["CURRENCY"].ToString() == "RON" ? amount_ron : 0),
                                        new MySqlParameter("_AMOUNT_PAID_RON", currency == "RON" ? amount_ron : 0),
                                        new MySqlParameter("_BANK_ACCOUNT_DETAILS", predicted_ie["BANK_ACCOUNT_DETAILS"]),
                                        new MySqlParameter("_RECEIPT_ID", receipt_id),
                                        new MySqlParameter("_BANKEXTRACT_ID", DBNull.Value)
                                    })).ExecuteUpdateQuery();
                                }
                                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }

                                #region --- old ---
                                /*
                                try
                                {
                                    //IncomeExpensesClass.InsertIE(predicted_ie["TYPE"].ToString().ToUpper() == "EXPENSE" ? false : true,
                                    IncomeExpensesClass.InsertIE(
                                        false,
                                        predicted_ie["TYPE"],
                                        false, // for cash
                                        predicted_ie["CURRENCY"],
                                        predicted_ie["AMOUNT"],
                                        data,
                                        predicted_ie["OWNER_ID"],
                                        predicted_ie["PROPERTY_ID"],
                                        predicted_ie["CONTRACTSERVICE_ID"],
                                        predicted_ie["SERVICE_DESCRIPTION"],
                                        predicted_ie["MONTH"],
                                        predicted_ie["INVOICEREQUIREMENT_ID"],
                                        predicted_ie["INVOICE_ID"],
                                        predicted_ie["CONTRACT_SERVICE_ADDITIONAL_COST_ID"],
                                        (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Paid"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery()
                                        );
                                }
                                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                                */
                                //Update (Insert) "real" IE
                                /*
                                try
                                {
                                    string SP = String.Format("UPDATE OWNERS SET {0} = IFNULL({1},0) - {2} WHERE ID = {3};", account_field, account_field, predicted_ie["AMOUNT"].ToString(), predicted_ie["OWNER_ID"].ToString());
                                    (new DataAccess(CommandType.Text, SP)).ExecuteUpdateQuery();
                                }
                                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                                //Update Owner/FDP general Ballance
                                */
                                #endregion
                            }
                            else
                            {
                                if (initial_amount - pie_amount_sum > 0)  // we still have a fraction of the amount (receipt) to substract
                                {
                                    try
                                    {
                                        //double amount_ron = CommonFunctions.ConvertCurrency(initial_amount - pie_amount_sum, Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                                        double amount_ron = Math.Round(CommonFunctions.ConvertCurrency(initial_amount - pie_amount_sum, predicted_ie["currency"].ToString().ToLower(), comboBoxCurrency.SelectedValue.ToString().ToLower(), data), 2);
                                        (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_change_status3", new object[]{
                                            new MySqlParameter("_ID", predicted_ie["id"]),
                                            new MySqlParameter("_SOURCE", false),
                                            //new MySqlParameter("_AMOUNT", (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round((initial_amount - pie_amount_sum) / exchange_rate, 2) : (initial_amount - pie_amount_sum))),
                                            new MySqlParameter("_AMOUNT", initial_amount - pie_amount_sum),
                                            //new MySqlParameter("_AMOUNT_PAID", predicted_ie["CURRENCY"].ToString() == "RON" ? 0 : initial_amount - pie_amount_sum),
                                            new MySqlParameter("_AMOUNT_PAID", currency == "RON" ? 0 : initial_amount - pie_amount_sum),
                                            //new MySqlParameter("_AMOUNT_PAID_RON", predicted_ie["CURRENCY"].ToString() == "RON" ? amount_ron : 0),
                                            new MySqlParameter("_AMOUNT_PAID_RON", currency == "RON" ? amount_ron : 0),
                                            new MySqlParameter("_BANK_ACCOUNT_DETAILS", predicted_ie["BANK_ACCOUNT_DETAILS"]),
                                            new MySqlParameter("_RECEIPT_ID", receipt_id),
                                            new MySqlParameter("_BANKEXTRACT_ID", DBNull.Value)
                                        })).ExecuteUpdateQuery();
                                    }
                                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }

                                    #region --- old ---
                                    /*
                                    try
                                    {
                                        //IncomeExpensesClass.InsertIE(predicted_ie["TYPE"].ToString().ToUpper() == "EXPENSE" ? false : true,
                                        IncomeExpensesClass.InsertIE(
                                            false,
                                            predicted_ie["TYPE"],
                                            false, // for cash
                                            predicted_ie["CURRENCY"],
                                            (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round((initial_amount - pie_amount_sum) / exchange_rate, 2) : (initial_amount - pie_amount_sum)),
                                            data,
                                            predicted_ie["OWNER_ID"],
                                            predicted_ie["PROPERTY_ID"],
                                            predicted_ie["CONTRACTSERVICE_ID"],
                                            predicted_ie["SERVICE_DESCRIPTION"],
                                            predicted_ie["MONTH"],
                                            predicted_ie["INVOICEREQUIREMENT_ID"],
                                            predicted_ie["INVOICE_ID"],
                                            predicted_ie["CONTRACT_SERVICE_ADDITIONAL_COST_ID"],
                                            (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Partially paid"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery()
                                            );
                                    }
                                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                                    */ 
                                    //Update (Insert) "real" IE
                                    /*
                                    try
                                    {
                                        string SP = String.Format("UPDATE OWNERS SET {0} = IFNULL({1},0) - {2} WHERE ID = {3};", account_field, account_field, (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round((initial_amount - pie_amount_sum) / exchange_rate, 2) : (initial_amount - pie_amount_sum)).ToString(), predicted_ie["OWNER_ID"].ToString());
                                        (new DataAccess(CommandType.Text, SP)).ExecuteUpdateQuery();
                                    }
                                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                                    //Update Owner/FDP general Ballance
                                    */
                                    #endregion
                                }
                            }
                        }
                        //pie_amount_sum += pie_amount;
                        pie_amount_sum += pie_ballance;
                        Saved = true;
                    }

                    // COMPANY I/E
                    try
                    {
                        pie_amount_sum = 0;

                        DataTable pie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", NewDR["invoice_id"]) })).ExecuteSelectQuery().Tables[0];
                        foreach (DataRow predicted_ie in pie.Rows)
                        {
                            //double pie_amount = (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round(Convert.ToDouble(predicted_ie["amount"]) * exchange_rate, 2) : Convert.ToDouble(predicted_ie["amount"]));
                            //double pie_amount = Math.Round(Convert.ToDouble(predicted_ie["amount"] == DBNull.Value ? "0" : predicted_ie["amount"]), 2);
                            double pie_amount = Math.Round(Math.Abs(Convert.ToDouble(predicted_ie["amount_total"] == DBNull.Value ? "0" : predicted_ie["amount_total"])), 2);
                            double pie_ballance = Math.Round(Convert.ToDouble(predicted_ie["ballance"] == DBNull.Value ? "0" : predicted_ie["ballance"]), 2);
                            //initial_amount = CommonFunctions.ConvertCurrency(receipt_amount, Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                            //initial_amount = CommonFunctions.ConvertCurrency(Math.Round(receipt_amount - receipt_amount * Convert.ToDouble(Invoice["VAT_PERCENT"]) / 100, 2), Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                            
                            //initial_amount = CommonFunctions.ConvertCurrency(Math.Round(receipt_amount / (1 + Convert.ToDouble(Invoice["VAT_PERCENT"]) / 100), 2), Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                            initial_amount = Math.Round(CommonFunctions.ConvertCurrency(Math.Round(receipt_amount, 2), Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data), 2);

                            //if (initial_amount > pie_amount_sum)
                            if (initial_amount > pie_amount_sum && pie_ballance > 0)
                            {
                                //if (pie_amount <= initial_amount - pie_amount_sum) // we still have amount (pie) to sustract
                                if (pie_ballance <= initial_amount - pie_amount_sum) // we still have amount (pie) to sustract
                                {
                                    try
                                    {
                                        double amount_ron = CommonFunctions.ConvertCurrency(pie_ballance, predicted_ie["currency"].ToString().ToLower(), comboBoxCurrency.SelectedValue.ToString().ToLower(), data);
                                        (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_change_status3", new object[]{
                                        new MySqlParameter("_ID", predicted_ie["ID"]),
                                        new MySqlParameter("_SOURCE", false),
                                        //new MySqlParameter("_AMOUNT", predicted_ie["AMOUNT"]),
                                        //new MySqlParameter("_AMOUNT", predicted_ie["BALLANCE"]),
                                        new MySqlParameter("_AMOUNT", pie_ballance),
                                        //new MySqlParameter("_AMOUNT_PAID", predicted_ie["CURRENCY"].ToString() == "RON" ? 0 : predicted_ie["BALLANCE"]),
                                        new MySqlParameter("_AMOUNT_PAID", currency == "RON" ? 0 : pie_ballance),
                                        //new MySqlParameter("_AMOUNT_PAID_RON", predicted_ie["CURRENCY"].ToString() == "RON" ? amount_ron : 0),
                                        new MySqlParameter("_AMOUNT_PAID_RON", currency == "RON" ? amount_ron : 0),
                                        new MySqlParameter("_BANK_ACCOUNT_DETAILS", predicted_ie["BANK_ACCOUNT_DETAILS"]),
                                        new MySqlParameter("_RECEIPT_ID", receipt_id),
                                        new MySqlParameter("_BANKEXTRACT_ID", DBNull.Value)
                                    })).ExecuteUpdateQuery();
                                    }
                                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }

                                    #region --- old ---
                                    /*
                                    try
                                    {
                                        IncomeExpensesClass.InsertIE(
                                            true,
                                            predicted_ie["TYPE"],
                                            false, // for cash
                                            predicted_ie["CURRENCY"],
                                            predicted_ie["AMOUNT"],
                                            data,
                                            predicted_ie["OWNER_ID"],
                                            predicted_ie["PROPERTY_ID"],
                                            predicted_ie["CONTRACTSERVICE_ID"],
                                            predicted_ie["SERVICE_DESCRIPTION"],
                                            predicted_ie["MONTH"],
                                            predicted_ie["INVOICEREQUIREMENT_ID"],
                                            predicted_ie["INVOICE_ID"],
                                            predicted_ie["CONTRACT_SERVICE_ADDITIONAL_COST_ID"],
                                            (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Partially paid"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery()
                                            );
                                    }
                                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                                    */
                                    //Update (Insert) "real" IE
                                    /*
                                    try
                                    {
                                        string SP = String.Format("UPDATE OWNERS SET {0} = IFNULL({1},0) - {2} WHERE ID = {3};", account_field, account_field, predicted_ie["AMOUNT"].ToString(), predicted_ie["OWNER_ID"].ToString());
                                        (new DataAccess(CommandType.Text, SP)).ExecuteUpdateQuery();
                                    }
                                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                                    //Update Owner/FDP general Ballance
                                    */
                                    #endregion
                                }
                                else
                                {
                                    if (initial_amount - pie_amount_sum > 0)  // we still have a fraction of the amount (RECEIPT) to substract
                                    {
                                        try
                                        {
                                            double amount_ron = Math.Round(CommonFunctions.ConvertCurrency(initial_amount - pie_amount_sum, predicted_ie["currency"].ToString().ToLower(), comboBoxCurrency.SelectedValue.ToString().ToLower(), data), 2);
                                            (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_change_status3", new object[]{
                                            new MySqlParameter("_ID", predicted_ie["id"]),
                                            new MySqlParameter("_SOURCE", false),
                                            //new MySqlParameter("_AMOUNT", (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round((initial_amount - pie_amount_sum) / exchange_rate, 2) : (initial_amount - pie_amount_sum))),
                                            new MySqlParameter("_AMOUNT", initial_amount - pie_amount_sum),
                                            //new MySqlParameter("_AMOUNT_PAID", predicted_ie["CURRENCY"].ToString() == "RON" ? 0 : initial_amount - pie_amount_sum),
                                            new MySqlParameter("_AMOUNT_PAID", currency == "RON" ? 0 : initial_amount - pie_amount_sum),
                                            //new MySqlParameter("_AMOUNT_PAID_RON", predicted_ie["CURRENCY"].ToString() == "RON" ? amount_ron : 0),
                                            new MySqlParameter("_AMOUNT_PAID_RON", currency == "RON" ? amount_ron : 0),
                                            new MySqlParameter("_BANK_ACCOUNT_DETAILS", predicted_ie["BANK_ACCOUNT_DETAILS"]),
                                            new MySqlParameter("_RECEIPT_ID", receipt_id),
                                            new MySqlParameter("_BANKEXTRACT_ID", DBNull.Value)
                                        })).ExecuteUpdateQuery();
                                        }
                                        catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }

                                        #region --- old ---
                                        /*
                                        try
                                        {
                                            IncomeExpensesClass.InsertIE(
                                                true,
                                                predicted_ie["TYPE"],
                                                false, // for cash
                                                predicted_ie["CURRENCY"],
                                                (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round((initial_amount - pie_amount_sum) / exchange_rate, 2) : (initial_amount - pie_amount_sum)),
                                                data,
                                                predicted_ie["OWNER_ID"],
                                                predicted_ie["PROPERTY_ID"],
                                                predicted_ie["CONTRACTSERVICE_ID"],
                                                predicted_ie["SERVICE_DESCRIPTION"],
                                                predicted_ie["MONTH"],
                                                predicted_ie["INVOICEREQUIREMENT_ID"],
                                                predicted_ie["INVOICE_ID"],
                                                predicted_ie["CONTRACT_SERVICE_ADDITIONAL_COST_ID"],
                                                (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Partially paid"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery()
                                                );
                                        }
                                        catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                                        */
                                        //Update (Insert) "real" IE
                                        /*
                                        try
                                        {
                                            string SP = String.Format("UPDATE OWNERS SET {0} = IFNULL({1},0) - {2} WHERE ID = {3};", account_field, account_field, (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round((initial_amount - pie_amount_sum) / exchange_rate, 2) : (initial_amount - pie_amount_sum)).ToString(), predicted_ie["OWNER_ID"].ToString());
                                            (new DataAccess(CommandType.Text, SP)).ExecuteUpdateQuery();
                                        }
                                        catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                                        //Update Owner/FDP general Ballance
                                        */
                                        #endregion
                                    }
                                }
                            }
                            //pie_amount_sum += pie_amount;
                            pie_amount_sum += pie_ballance;
                            Saved = true;
                        }
                    }
                    catch { }

                    buttonPrint.Enabled = true;
                    base.ShowConfirmationDialog(Language.GetMessageBoxText("dataSaved", "Information was saved successfully!"));
                    buttonSave.Enabled = false;
                }
                catch (Exception exp) { 
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void GenerateMySqlParameters()
        {
            try
            {
                if (NewDR != null && EditMode != 2)
                {
                    NewDR["series"] = userTextBoxSeries.Text;
                    NewDR["number"] = userTextBoxNumber.Text.Trim() == "" ? DBNull.Value : (object)userTextBoxNumber.Text.TrimStart('0');
                    NewDR["date"] = dateTimePickerDate.Value;
                    NewDR["invoice_id"] = Invoice == null ? NewDR["invoice_id"] : Invoice["id"];
                    //NewDR["amount_paid_ron"] = userTextBoxAmount.Text;
                    NewDR["amount_paid"] = userTextBoxAmount.Text;
                    //NewDR["income_expense_id"] = IncomeExpense == null ? DBNull.Value : IncomeExpense["id"];
                    NewDR["currency"] = CommonFunctions.SetNullable(comboBoxCurrency);
                    NewDR["comments"] = userTextBoxComments.Text;
                }
                else
                {
                    MySqlParameters.Clear();
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _SERIES = new MySqlParameter("_SERIES", userTextBoxSeries.Text); MySqlParameters.Add(_SERIES);
                    MySqlParameter _NUMBER = new MySqlParameter("_NUMBER", userTextBoxNumber.Text.Trim() == "" ? DBNull.Value : (object)userTextBoxNumber.Text.TrimStart('0')); MySqlParameters.Add(_NUMBER);
                    MySqlParameter _DATE = new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerDate.Value)); MySqlParameters.Add(_DATE);
                    MySqlParameter _INVOICE_ID = new MySqlParameter("_INVOICE_ID", Invoice == null ? (NewDR == null ? DBNull.Value : NewDR["invoice_id"]) : Invoice["id"]); MySqlParameters.Add(_INVOICE_ID);
                    //MySqlParameter _AMOUNT_PAID_RON = new MySqlParameter("_AMOUNT_PAID_RON", userTextBoxAmount.Text); MySqlParameters.Add(_AMOUNT_PAID_RON);
                    MySqlParameter _AMOUNT_PAID = new MySqlParameter("_AMOUNT_PAID", userTextBoxAmount.Text); MySqlParameters.Add(_AMOUNT_PAID);
                    //MySqlParameter _INCOME_EXPENSE_ID = new MySqlParameter("_INCOME_EXPENSE_ID", IncomeExpense == null ? (NewDR == null ? DBNull.Value : NewDR["income_expense_id"]) : IncomeExpense["id"]); MySqlParameters.Add(_INCOME_EXPENSE_ID);
                    MySqlParameter _CURRENCY = new MySqlParameter("_CURRENCY", CommonFunctions.SetNullable(comboBoxCurrency)); MySqlParameters.Add(_CURRENCY);
                    MySqlParameter _COMMENTS = new MySqlParameter("_COMMENTS", userTextBoxComments.Text); MySqlParameters.Add(_COMMENTS);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private bool ValidateData()
        {
            bool toReturn = true;
            //errorProvider1.SetError(userTextBoxSeries, "");
            //errorProvider1.SetError(userTextBoxNumber, "");
            errorProvider1.SetError(userTextBoxAmount, "");
            /*
            if (userTextBoxSeries.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxSeries, Language.GetErrorText("errorEmptyReceiptSeries", "Receipt series can not be empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxSeries.Name, Language.GetErrorText("errorEmptyReceiptSeries", "Receipt series can not be empty!")));
                toReturn = false;
            }
            if (userTextBoxNumber.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxNumber, Language.GetErrorText("errorEmptyReceiptNumber", "Receipt number can not be empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxNumber.Name, Language.GetErrorText("errorEmptyReceiptNumber", "Receipt number can not be empty!")));
                toReturn = false;
            }
            */
            if (!Validator.IsNumeric(userTextBoxAmount.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxAmount, Language.GetErrorText("errorInvalidAmount", "Invalid amount value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxAmount.Name, Language.GetErrorText("errorInvalidAmount", "Invalid amount value!")));
                toReturn = false;
            }
            return toReturn;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", NewDR["invoice_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
            company = (new DataAccess(CommandType.StoredProcedure, "COMPANIESsp_get_by_id", new object[] { new MySqlParameter("_ID", invoice["supplier_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
            customer = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_by_id", new object[] { new MySqlParameter("_ID", invoice["owner_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
            ReceiptPDF rpdf = new ReceiptPDF(company, customer, invoice, NewDR);
            rpdf.ShowDialog();
            rpdf.Dispose();
        }

        private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currency = comboBoxCurrency.SelectedValue.ToString();
            if (currency != "RON" && currency != "EUR")
                MessageBox.Show(Language.GetMessageBoxText("impropperCurrency", "You have selected a currency that is not properly coverred by this application. Errors may occur if you continue!"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void pictureBoxSelectInvoice_Click(object sender, EventArgs e)
        {
            var f = new InvoiceSelect(true);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICESsp_GetById", new object[] { new MySqlParameter("_ID", f.IdToReturn) });
                Invoice = da.ExecuteSelectQuery().Tables[0].Rows[0];
                userTextBoxInvoice.Text = String.Format("{0} {1} / {2}", Invoice["series"].ToString(), Invoice["number"].ToString().PadLeft(5, '0'), Convert.ToDateTime(Invoice["date"]).ToString(SettingsClass.DateFormat));

                userTextBoxSeries.Text = SettingsClass.GetCompanySetting("INVOICE SERIES").ToString();
                userTextBoxNumber.Text = (GetMaxReceiptNumber() + 1).ToString().PadLeft(5, '0');
                //userTextBoxAmount.Text = Invoice["total_ron"].ToString();
                //userTextBoxAmount.Text = Convert.ToString(Convert.ToDouble(Invoice["total_ron"]) - Convert.ToDouble(Invoice["amount_paid_ron"]));
                userTextBoxAmount.Text = Convert.ToString(Convert.ToDouble(Invoice["total"] == DBNull.Value ? "0" : Invoice["total"]) - Convert.ToDouble(Invoice["amount_paid"] == DBNull.Value ? "0" : Invoice["amount_paid"]));
                dateTimePickerDate.Value = Convert.ToDateTime(Invoice["date"]);
                foreach (object x in comboBoxCurrency.Items)
                {
                    if (((DataRowView)x)["currency"].ToString().ToUpper() == Invoice["currency"].ToString().ToUpper())
                    {
                        comboBoxCurrency.SelectedIndex = comboBoxCurrency.Items.IndexOf(x);
                        break;
                    }
                }
                comboBoxCurrency.Visible = labelCurrency.Visible = false;

            }
            f.Dispose();
        }
    }
}
