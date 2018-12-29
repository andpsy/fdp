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
    public partial class BankReceipt : UserForm
    {
        public DataRow NewDR;
        public DataRow Invoice;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();

        public DataRow company;
        public DataRow customer;
        public DataRow invoice;

        public BankReceipt()
        {
            //base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
        }

        public BankReceipt(int tip, DataRow row) // tip = 0 => receipt ; tip = 1 => invoice
        {
            //base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            switch (tip)
            {
                case 0:
                    NewDR = row;
                    break;
                case 1:
                    Invoice = row;
                    break;
            }
            InitializeComponent();
        }

        public BankReceipt(int tip, int id) // tip = 0 => receipt ; tip = 1 => invoice
        {
            //base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            switch (tip)
            {
                case 0:
                    NewDR = (new DataAccess(CommandType.StoredProcedure, "BANK_RECEIPTSsp_get_by_id", new object[] { new MySqlParameter("_ID", id) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    break;
                case 1:
                    Invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", id) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    break;
            }
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FillOwners()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_list");
            DataTable dtc1 = da.ExecuteSelectQuery().Tables[0];
            if (dtc1 != null)
            {
                comboBoxOwner.DisplayMember = "name";
                comboBoxOwner.ValueMember = "id";
                comboBoxOwner.DataSource = dtc1;
            }
        }
        /*
        private void FillProperties()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_list_by_owner_id", new object[]{new MySqlParameter("_OWNER_ID", comboBoxOwner.SelectedValue)});
            DataTable dtc1 = da.ExecuteSelectQuery().Tables[0];
            if (dtc1 != null)
            {
                comboBoxProperty.DisplayMember = "name";
                comboBoxProperty.ValueMember = "id";
                comboBoxProperty.DataSource = dtc1;
            }
        }
        */
        private void FillBankAccounts()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_bank_accounts", new object[] { new MySqlParameter("_ID", comboBoxOwner.SelectedValue) });
            DataTable dtc1 = da.ExecuteSelectQuery().Tables[0];
            if (dtc1 != null)
            {
                comboBoxBankAccount.DisplayMember = "account";
                comboBoxBankAccount.ValueMember = "id";
                comboBoxBankAccount.DataSource = dtc1;
            }
        }

        private void FillInvoices()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICESsp_list");
            DataTable dtc1 = da.ExecuteSelectQuery().Tables[0];
            if (dtc1 != null)
            {
                comboBoxInvoice.DisplayMember = "invoice";
                comboBoxInvoice.ValueMember = "id";
                comboBoxInvoice.DataSource = dtc1;
            }
        }

        private void FillInvoiceInfo()
        {
            try
            {
                foreach (object x in comboBoxInvoice.Items)
                {
                    if (((DataRowView)x)["id"].ToString() == Invoice["id"].ToString())
                    {
                        comboBoxInvoice.SelectedIndex = comboBoxInvoice.Items.IndexOf(x);
                        foreach (object y in comboBoxOwner.Items)
                        {
                            if (((DataRowView)y)["id"].ToString() == Invoice["owner_id"].ToString())
                            {
                                comboBoxOwner.SelectedIndex = comboBoxOwner.Items.IndexOf(y);
                                break;
                            }
                        }
                        /*
                        foreach (object z in comboBoxProperty.Items)
                        {
                            if (((DataRowView)z)["id"].ToString() == Invoice["property_id"].ToString())
                            {
                                comboBoxProperty.SelectedIndex = comboBoxProperty.Items.IndexOf(z);
                                break;
                            }
                        }
                        */
                        break;
                    }
                }
                DataRow owner = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", Invoice["id"]) }).ExecuteSelectQuery()).Tables[0].Rows[0];
                string bank_account_column = "";
                foreach (DataColumn dc in owner.Table.Columns)
                {
                    if (owner[dc.ColumnName].ToString().ToUpper() == Invoice["currency"].ToString().ToUpper())
                    {
                        bank_account_column = dc.ColumnName.ToLower().Replace("currency", "details");
                        break;
                    }
                }
                FillBankAccounts();
                foreach (object y in comboBoxBankAccount.Items)
                {
                    if (((DataRowView)y)["account"].ToString().ToLower() == owner[bank_account_column].ToString().ToLower())
                    {
                        comboBoxBankAccount.SelectedIndex = comboBoxBankAccount.Items.IndexOf(y);
                        break;
                    }
                }
            }
            catch { }
        }

        private void BankReceipt_Load(object sender, EventArgs e)
        {
            FillInvoices();
            FillOwners();
            if (Invoice != null)
            {
                FillInvoiceInfo();
                //userTextBoxAmount.Text = Convert.ToString(Convert.ToDouble(Invoice["total_ron"]) - Convert.ToDouble(Invoice["amount_paid_ron"]));
                userTextBoxAmount.Text = Convert.ToString(Convert.ToDouble(Invoice["total"] == DBNull.Value ? "0" : Invoice["total"]) - Convert.ToDouble(Invoice["amount_paid"] == DBNull.Value ? "0" : Invoice["amount_paid"]));
                dateTimePickerDate.Value = Convert.ToDateTime(Invoice["date"]);
                dateTimePickerExtractDate.Value = DateTime.Now;
            }
            if (NewDR != null)
            {
                try
                {
                    Invoice = (new DataAccess(CommandType.StoredProcedure, "BANK_RECEIPTSsp_get_invoice", new object[] { new MySqlParameter("_ID", NewDR["id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    if (Invoice == null)
                    {
                        MessageBox.Show(Language.GetMessageBoxText("noInvoiceInTheDataBase", "There are no invoices in the database. You can not add a bank receipt without an invoice!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    FillInvoiceInfo();
                    userTextBoxExtractNumber.Text = NewDR["extract_number"].ToString().PadLeft(5, '0');
                    userTextBoxAmount.Text = NewDR["amount_paid"].ToString();
                    dateTimePickerExtractDate.Value = Convert.ToDateTime(NewDR["extract_date"]);
                    dateTimePickerDate.Value = Convert.ToDateTime(NewDR["date"]);
                    userTextBoxComments.Text = NewDR["comments"].ToString();

                    buttonPrint.Enabled = true;
                }
                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
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

            if (NewDR == null) //add direct
            {
                try
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "BANK_RECEIPTSsp_insert", MySqlParameters.ToArray());
                    NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    int receipt_id = Convert.ToInt32(NewDR["id"]);

                    //update invoice paid amount and status:
                    //(new DataAccess(CommandType.StoredProcedure, "INVOICESsp_change_status", new object[] { new MySqlParameter("_ID", NewDR["invoice_id"]), new MySqlParameter("_AMOUNT_PAID_RON", NewDR["amount_paid_ron"]) })).ExecuteUpdateQuery();
                    (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_change_status", new object[] { new MySqlParameter("_ID", NewDR["invoice_id"]), new MySqlParameter("_AMOUNT_PAID", NewDR["amount_paid"]) })).ExecuteUpdateQuery();
                    
                    //TO DO: Insert IEs and modifiy Predicted IE
                    //(new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_convert_from_invoice", new object[] {new MySqlParameter("_INVOICE_ID", NewDR["invoice_id"]), new MySqlParameter("_AMOUNT", NewDR["amount_paid_ron"]) })).ExecuteUpdateQuery();

                    double initial_amount = 0;
                    double original_initial_amount = 0;
                    double receipt_amount = Convert.ToDouble(userTextBoxAmount.Text);
                    double pie_amount_sum = 0;
                    double exchange_rate = 1;

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
                        exchange_rate = CurrenciesClass.GetExchangeRate(dateTimePickerExtractDate.Value, Invoice["currency"].ToString());
                        initial_amount = Math.Round(receipt_amount / exchange_rate, 2);
                    }
                    else
                    {
                        initial_amount = receipt_amount;
                    }
                    */
                    //TO DO:  GET REAL CURRENCY !!!
                    #endregion

                    original_initial_amount = initial_amount = Math.Round(receipt_amount / (1 + Convert.ToDouble(Invoice["VAT_PERCENT"]) / 100), 2);

                    DateTime data = dateTimePickerExtractDate.Value;

                    /*
                    string account_field = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_cash_account_by_currency", new object[] { new MySqlParameter("_ID", Invoice["OWNER_ID"]), new MySqlParameter("_CURRENCY", Invoice["CURRENCY"]) })).ExecuteScalarQuery().ToString();
                    switch (account_field)
                    {
                        case "1":
                            account_field = "CASH_STARTING_BALLANCE1";
                            break;
                        case "2":
                            account_field = "CASH_STARTING_BALLANCE2";
                            break;
                        case "3":
                            account_field = "CASH_STARTING_BALLANCE3";
                            break;
                    }
                    */

                    DataTable pie = (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", NewDR["invoice_id"]) })).ExecuteSelectQuery().Tables[0];
                    foreach (DataRow predicted_ie in pie.Rows)
                    {
                        //double pie_amount = (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round(Convert.ToDouble(predicted_ie["amount"]) * exchange_rate, 2) : Convert.ToDouble(predicted_ie["amount"]));
                        double pie_amount = Convert.ToDouble(predicted_ie["amount"] == DBNull.Value ? "0" : predicted_ie["amount"]);
                        double pie_ballance = Convert.ToDouble(predicted_ie["ballance"] == DBNull.Value ? "0" : predicted_ie["ballance"]);

                        if (initial_amount > pie_amount_sum)
                        {
                            if (pie_amount <= initial_amount - pie_amount_sum) // we still have amount (pie) to sustract
                            {
                                try
                                {
                                    double amount_ron = CommonFunctions.ConvertCurrency(pie_ballance, Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                                    (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_change_status3", new object[]{
                                        new MySqlParameter("_ID", predicted_ie["ID"]),
                                        new MySqlParameter("_SOURCE", true),
                                        new MySqlParameter("_AMOUNT", predicted_ie["AMOUNT"]),
                                        new MySqlParameter("_AMOUNT_PAID", predicted_ie["CURRENCY"].ToString() == "RON" ? 0 : predicted_ie["BALLANCE"]),
                                        new MySqlParameter("_AMOUNT_PAID_RON", predicted_ie["CURRENCY"].ToString() == "RON" ? amount_ron : 0),
                                        new MySqlParameter("_BANK_ACCOUNT_DETAILS", ((DataRowView)comboBoxBankAccount.SelectedItem)["account"].ToString()),
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
                                if (initial_amount - pie_amount_sum > 0)  // we still have a fraction of the amount (pie) to substract
                                {
                                    try
                                    {
                                        double amount_ron = CommonFunctions.ConvertCurrency(initial_amount - pie_amount_sum, Invoice["currency"].ToString().ToLower(), predicted_ie["currency"].ToString().ToLower(), data);
                                        (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_change_status3", new object[]{
                                            new MySqlParameter("_ID", predicted_ie["id"]),
                                            new MySqlParameter("_SOURCE", true),
                                            new MySqlParameter("_AMOUNT", (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round((initial_amount - pie_amount_sum) / exchange_rate, 2) : (initial_amount - pie_amount_sum))),
                                            new MySqlParameter("_AMOUNT_PAID", predicted_ie["CURRENCY"].ToString() == "RON" ? 0 : initial_amount - pie_amount_sum),
                                            new MySqlParameter("_AMOUNT_PAID_RON", predicted_ie["CURRENCY"].ToString() == "RON" ? amount_ron : 0),
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
                        pie_amount_sum += pie_amount;
                    }

                    pie_amount_sum = 0;
                    pie = (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", NewDR["invoice_id"]) })).ExecuteSelectQuery().Tables[0];
                    foreach (DataRow predicted_ie in pie.Rows)
                    {
                        double pie_amount = (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round(Convert.ToDouble(predicted_ie["amount"]) * exchange_rate, 2) : Convert.ToDouble(predicted_ie["amount"]));

                        if (initial_amount > pie_amount_sum)
                        {
                            if (pie_amount <= initial_amount - pie_amount_sum) // we still have amount (pie) to sustract
                            {
                                try
                                {
                                    (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_change_status3", new object[]{
                                        new MySqlParameter("_ID", predicted_ie["ID"]),
                                        new MySqlParameter("_SOURCE", true),
                                        new MySqlParameter("_AMOUNT", predicted_ie["AMOUNT"]),
                                        new MySqlParameter("_BANK_ACCOUNT_DETAILS", ((DataRowView)comboBoxBankAccount.SelectedItem)["account"].ToString())
                                    })).ExecuteUpdateQuery();
                                }
                                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
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
                            }
                            else
                            {
                                if (initial_amount - pie_amount_sum > 0)  // we still have a fraction of the amount (pie) to substract
                                {
                                    try
                                    {
                                        (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_change_status3", new object[]{
                                            new MySqlParameter("_ID", predicted_ie["id"]),
                                            new MySqlParameter("_SOURCE", true),
                                            new MySqlParameter("_AMOUNT", (predicted_ie["currency"].ToString().ToLower() != "ron" ? Math.Round((initial_amount - pie_amount_sum) / exchange_rate, 2) : (initial_amount - pie_amount_sum)))
                                        })).ExecuteUpdateQuery();
                                    }
                                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
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
                                }
                            }
                        }
                        pie_amount_sum += pie_amount;
                    }
                    buttonPrint.Enabled = true;
                    base.ShowConfirmationDialog(Language.GetMessageBoxText("dataSaved", "Information was saved successfully!"));
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
                if (NewDR != null)
                {
                    NewDR["extract_number"] = userTextBoxExtractNumber.Text.TrimStart('0');
                    NewDR["extract_date"] = dateTimePickerExtractDate.Value;
                    NewDR["invoice_id"] = Invoice["id"];
                    NewDR["amount_paid"] = userTextBoxAmount.Text;
                    NewDR["date"] = dateTimePickerDate.Value;
                    NewDR["bank_account_details"] = ((DataRowView)comboBoxBankAccount.SelectedItem)["account"];
                    NewDR["description"] = NewDR["comments"] = userTextBoxComments.Text;
                }
                else
                {
                    MySqlParameters.Clear();
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _EXTRACT_NUMBER = new MySqlParameter("_EXTRACT_NUMBER", userTextBoxExtractNumber.Text.TrimStart('0')); MySqlParameters.Add(_EXTRACT_NUMBER);
                    MySqlParameter _EXTRACT_DATE = new MySqlParameter("_EXTRACT_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerExtractDate.Value)); MySqlParameters.Add(_EXTRACT_DATE);
                    MySqlParameter _DATE = new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerDate.Value)); MySqlParameters.Add(_DATE);
                    MySqlParameter _INVOICE_ID = new MySqlParameter("_INVOICE_ID", Invoice["id"]); MySqlParameters.Add(_INVOICE_ID);
                    MySqlParameter _AMOUNT_PAID = new MySqlParameter("_AMOUNT_PAID", userTextBoxAmount.Text); MySqlParameters.Add(_AMOUNT_PAID);
                    MySqlParameter _DESCRIPTION = new MySqlParameter("_DESCRIPTION", userTextBoxComments.Text); MySqlParameters.Add(_DESCRIPTION);
                    MySqlParameter _BANK_ACCOUNT_DETAILS = new MySqlParameter("_BANK_ACCOUNT_DETAILS", ((DataRowView)comboBoxBankAccount.SelectedItem)["account"]); MySqlParameters.Add(_BANK_ACCOUNT_DETAILS);
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
            errorProvider1.SetError(userTextBoxExtractNumber, "");
            errorProvider1.SetError(userTextBoxAmount, "");

            if (userTextBoxExtractNumber.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxExtractNumber, Language.GetErrorText("errorEmptyReceiptNumber", "Receipt number can not by empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxExtractNumber.Name, Language.GetErrorText("errorEmptyReceiptNumber", "Receipt number can not by empty!")));
                toReturn = false;
            }
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

        private void comboBoxInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", comboBoxInvoice.SelectedValue) }).ExecuteSelectQuery()).Tables[0].Rows[0];
                FillInvoiceInfo();
            }
            catch { }
        }
    }
}
