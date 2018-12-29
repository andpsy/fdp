using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using System.IO;

namespace FDP
{
    public partial class Invoices : UserForm
    {
        public DataTable InvoiceRequirements;
        public int OwnerId;
        public double InvoiceTotalAmount, InvoiceTotalVat, InvoiceTotal, InvoiceTotalAmountRon, InvoiceTotalVatRon, InvoiceTotalRon;
        public DataTable invoiceRows;
        //public int EditMode; // 0 - none / 1- add / 2 - edit /  3 - delete
        public DataRow NewDR;
        public DataRow InitialDR;
        public DataRow company;
        public DataRow customer;
        public double ExchangeRate;
        public double VatPercent;
        public string Currency;
        public DataRow LastPayment;

        public Invoices()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            //LoaderClass lc = new LoaderClass();
            //oThread = new Thread(new ThreadStart(lc.ShowLoader));
            //oThread.Priority = ThreadPriority.Highest;
            //oThread.Start();
            InitializeComponent();
            //toolStrip1.Enabled = true;  // -- for phase 2 - invoice direct
        }

        public Invoices(DataTable dt)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            //LoaderClass lc = new LoaderClass();
            //oThread = new Thread(new ThreadStart(lc.ShowLoader));
            //oThread.Priority = ThreadPriority.Highest;
            //oThread.Start();
            InvoiceRequirements = dt;
            OwnerId = Convert.ToInt32(dt.Rows[0]["owner_id"]);
            Currency = dt.Rows[0]["currency"].ToString();
            InitializeComponent();
        }

        public Invoices(int id)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            DataRow dr = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", id) })).ExecuteSelectQuery().Tables[0].Rows[0];
            if (dr != null)
            {
                NewDR = dr;
                InitializeComponent();
                OwnerId = Convert.ToInt32(NewDR["owner_id"]);
                Currency = NewDR["currency"].ToString();
                InvoiceRequirements = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", NewDR["id"]) })).ExecuteSelectQuery().Tables[0];
                toolStrip1.Enabled = true;
            }
            else
            {
                InitializeComponent();
            }
        }

        public Invoices(int id, string id_type)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            DataRow dr = null;
            switch (id_type)
            {
                case "invoicerequirement_id":
                    dr = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_ir_id", new object[] { new MySqlParameter("_INVOICEREQUIREMENT_ID", id) })).ExecuteSelectQuery().Tables[0].Rows[0];
                    break;
            }
            if (dr != null)
            {
                NewDR = dr;
                InitializeComponent();
                OwnerId = Convert.ToInt32(NewDR["owner_id"]);
                Currency = NewDR["currency"].ToString();
                InvoiceRequirements = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", NewDR["id"]) })).ExecuteSelectQuery().Tables[0];
                toolStrip1.Enabled = true;
            }
            else
            {
                InitializeComponent();
            }
        }


        
        public Invoices(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            //LoaderClass lc = new LoaderClass();
            //oThread = new Thread(new ThreadStart(lc.ShowLoader));
            //oThread.Priority = ThreadPriority.Highest;
            //oThread.Start();
            NewDR = dr;
            InitializeComponent();
            try { OwnerId = Convert.ToInt32(NewDR["owner_id"]); }
            catch { }
            try { Currency = NewDR["currency"].ToString(); }
            catch { }
            try { InvoiceRequirements = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", NewDR["id"]) })).ExecuteSelectQuery().Tables[0]; }
            catch { }
            toolStrip1.Enabled = true;
        }

        private void Invoices_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime CurrencyDate = (NewDR == null || NewDR["date"] == null) ? DateTime.Now : Convert.ToDateTime(NewDR["date"]);
                //this.currencyShow1 = new CurrencyShow(CurrencyDate);
                currencyShow1.GetCurrencies(CurrencyDate);
                /*
                object exchange_rate = (new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_by_date_and_currency", new object[]{
                new MySqlParameter("_DATE", CurrencyDate),
                new MySqlParameter("_CURRENCY", NewDR == null ? Currency : NewDR["currency"].ToString())})).ExecuteScalarQuery();
                ExchangeRate = Math.Round(Convert.ToDouble(exchange_rate == null ? 1 : exchange_rate), 2);
                */
                ExchangeRate = CurrenciesClass.GetExchangeRate(CurrencyDate, (NewDR == null || NewDR["currency"] == null) ? Currency : NewDR["currency"].ToString());
                FillCombos();
                FillSupplier();
                FillCustomer();
                FillInvoice();
                FillInvoiceRows();
                InitialDR = CommonFunctions.CopyDataRow(NewDR);
                //oThread.Abort();
                this.Opacity = 100;
                buttonPDF.Enabled = buttonIncasare.Enabled = (NewDR != null);
                ResizeDataGrid();
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
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

        private void FillInvoice()
        {
            userTextBoxInvoiceNumber.Text = NewDR == null ? (GetMaxInvoiceNumber() + 1).ToString().PadLeft(5, '0') : NewDR["number"].ToString().PadLeft(5, '0');
            userTextBoxInvoiceSeries.Text = NewDR == null ? SettingsClass.GetCompanySetting("INVOICE SERIES").ToString() : NewDR["series"].ToString();
            userTextBoxInvoiceVatPercent.Text = NewDR == null ? SettingsClass.GetCompanySetting("VAT (%)") : NewDR["vat_percent"].ToString();
            VatPercent = Convert.ToDouble(userTextBoxInvoiceVatPercent.Text);
            dateTimePickerInvoiceDate.Value = NewDR == null ? DateTime.Now : Convert.ToDateTime(NewDR["date"]);
            dateTimePickerInvoiceDueDate.Value = NewDR == null ? DateTime.Now : Convert.ToDateTime(NewDR["due_date"]);
            //comboBoxCurrency.SelectedValue = NewDR == null ? Currency : NewDR["currency"].ToString();
            //userTextBoxBallance.Text = NewDR == null ? "" : Convert.ToString(Convert.ToDouble(NewDR["TOTAL_RON"].ToString()) - Convert.ToDouble(NewDR["AMOUNT_PAID_RON"]==DBNull.Value?0:NewDR["AMOUNT_PAID_RON"]));
            //userTextBoxBallance.Text = NewDR == null ? "" : Convert.ToString(Math.Round(Convert.ToDouble(NewDR["TOTAL"].ToString()) - Convert.ToDouble(NewDR["AMOUNT_PAID"] == DBNull.Value ? 0 : NewDR["AMOUNT_PAID"]), 2));
            userTextBoxBallance.Text = NewDR == null ? "" : Convert.ToString(Math.Round(Convert.ToDouble(NewDR["BALLANCE"]), 2));
            if (NewDR != null)
            {
                FillPayments(Convert.ToInt32(NewDR["id"]));
            }
        }

        private void FillPayments(int invoice_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_payments", new object[] { new MySqlParameter("_INVOICE_ID", invoice_id) });
            DataTable dtPayments = da.ExecuteSelectQuery().Tables[0];
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
        }

        private void FillSupplier()
        {
            company = (new DataAccess(CommandType.StoredProcedure, "COMPANIESsp_get_by_id", new object[] { new MySqlParameter("_ID", NewDR==null?SettingsClass.CompanyId:NewDR["supplier_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
            userTextBoxSupplierName.Text = company["name"].ToString().ToUpper();
            userTextBoxSupplierRegComNo.Text = company["commercial_register_number"].ToString().ToUpper();
            userTextBoxSupplierCui.Text = company["cui"].ToString().ToUpper();
            userTextBoxSupplierAddress.Text = company["address"].ToString();
            userTextBoxSupplierDistrict.Text = company["district"].ToString().ToUpper();
            userTextBoxSupplierBankAccount1.Text = company["bank_account_details1"].ToString().ToUpper();
            userTextBoxSupplierBankName1.Text = company["bank_name1"].ToString().ToUpper();
            userTextBoxSupplierBankAcount2.Text = company["bank_account_details2"].ToString().ToUpper();
            userTextBoxSupplierBankName2.Text = company["bank_name2"].ToString().ToUpper();
            userTextBoxSupplierCapital.Text = company["capital"].ToString();
            userTextBoxSupplierEmail.Text = company["emails"].ToString().ToLower();
            userTextBoxSupplierPhone.Text = company["phones"].ToString();
            userTextBoxSupplierFax.Text = company["faxes"].ToString();
            userTextBoxSupplierWebsite.Text = company["websites"].ToString().ToLower();
        }

        private void FillCustomer()
        {
            customer = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_by_id", new object[] { new MySqlParameter("_ID", NewDR==null?OwnerId:NewDR["owner_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
            userTextBoxClientName.Text = customer["full_name"].ToString().ToUpper();
            userTextBoxClientRegComNo.Text = customer["commercial_register_number"].ToString().ToUpper();
            object type_id = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Individual"), new MySqlParameter("_LIST_TYPE", "owner_type") })).ExecuteScalarQuery();
            userTextBoxClientCui.Text = (type_id != null && customer["type_id"].ToString() == type_id.ToString()) ? customer["cnp"].ToString().ToUpper() : customer["cui"].ToString().ToUpper();
            userTextBoxClientAddress.Text = customer["address"].ToString();
            userTextBoxClientDistrict.Text = customer["district"].ToString().ToUpper();
            userTextBoxClientBankAccount1.Text = customer["bank_account_details1"].ToString().ToUpper();
            userTextBoxClientBankName1.Text = customer["bank_name1"].ToString().ToUpper();
            userTextBoxClientBankAccount2.Text = customer["bank_account_details2"].ToString().ToUpper();
            userTextBoxClientBankName2.Text = customer["bank_name2"].ToString().ToUpper();
            //userTextBoxClientCapital.Text = customer["capital"].ToString();
            userTextBoxClientEmail.Text = customer["emails"].ToString().ToLower();
            userTextBoxClientPhone.Text = customer["phones"].ToString();
            //userTextBoxClientFax.Text = customer["faxes"].ToString();
            //userTextBoxClientWebsite.Text = customer["websites"].ToString().ToLower();
        }

        private void FillInvoiceRows()
        {
            invoiceRows = new DataTable();
            if (NewDR == null)
            {
                DataColumn dc = new DataColumn("id", Type.GetType("System.Int32"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("description", Type.GetType("System.String"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("unit_measure", Type.GetType("System.String"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("quantity", Type.GetType("System.Double"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("vat_percent", Type.GetType("System.Double"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("unit_price", Type.GetType("System.Double"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("unit_price_ron", Type.GetType("System.Double"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("amount", Type.GetType("System.Double"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("amount_ron", Type.GetType("System.Double"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("vat", Type.GetType("System.Double"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("vat_ron", Type.GetType("System.Double"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("total", Type.GetType("System.Double"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("total_ron", Type.GetType("System.Double"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("invoice_id", Type.GetType("System.Int32"));
                invoiceRows.Columns.Add(dc);

                dc = new DataColumn("invoicerequirements_ids", Type.GetType("System.String"));
                invoiceRows.Columns.Add(dc);
                try
                {
                    var query = from row in InvoiceRequirements.AsEnumerable()
                                group row by new { OWNER_ID = row.Field<UInt32>("owner_id"), PROPERTY_ID = row.Field<object>("property_id"), CONTRACTSERVICE_ID = row.Field<UInt32>("contractservice_id"), PRICE = Math.Round(row.Field<double>("price"),2) } into grp
                                //orderby grp.Key
                                select new
                                {
                                    _owner_id = grp.Key.OWNER_ID,
                                    _property_id = grp.Key.PROPERTY_ID,
                                    _contractservivce_id = grp.Key.CONTRACTSERVICE_ID,
                                    _price = Math.Round(grp.Key.PRICE,2),
                                    _group = grp
                                    //Sum = grp.Aggregate(new StringBuilder() String.Format("{0};", row.Field<string>("service"))
                                };

                    foreach (var grp in query)
                    {
                        DataRow[] drs = InvoiceRequirements.Select(String.Format("owner_id={0} AND (property_id={1} OR IsNull(property_id, -1)=-1) AND contractservice_id={2} AND price={3}", grp._owner_id, grp._property_id == null ? "-1" : grp._property_id.ToString(), grp._contractservivce_id, grp._price));
                        if (drs.Length == 1)
                        {
                            DataRow invoice_row = invoiceRows.NewRow();
                            invoice_row["description"] = String.Format("{0} FOR PROPERTY <<{1}>> ACC. TO CONTRACT NO. {2} {3}",
                                drs[0]["service"].ToString().ToUpper(),
                                drs[0]["property"].ToString().ToUpper(),
                                drs[0]["contract"].ToString().ToUpper(),
                                (drs[0]["service"].ToString().ToUpper() == "RENT MANAGEMENT" ? String.Format(" - MONTH: {0}", drs[0]["month"].ToString().ToUpper()) : "")
                                );
                            // !!! TO DO: service descriptio from corespondence table
                            // TO DO: get the contract date
                            invoice_row["unit_measure"] = "-";
                            invoice_row["vat_percent"] = VatPercent;
                            invoice_row["quantity"] = 1;

                            invoice_row["invoicerequirements_ids"] = drs[0]["id"];

                            //string IRcurrency = drs[0]["currency"].ToString();
                            string IRcurrency = comboBoxCurrency.SelectedValue.ToString();
                            if (IRcurrency == Currency && IRcurrency == "RON") //both contract and invoice currencies are "RON"
                            {
                                invoice_row["unit_price"] = invoice_row["unit_price_ron"] = drs[0]["price"];
                                invoice_row["amount"] = invoice_row["amount_ron"] = drs[0]["price"];
                                invoice_row["vat"] = invoice_row["vat_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                invoice_row["total"] = invoice_row["total_ron"] = Convert.ToDouble(invoice_row["amount"]) + Convert.ToDouble(invoice_row["vat"]);
                            }
                            else
                            {
                                if (IRcurrency != Currency && IRcurrency == "RON") // invoice currency is "RON" but contract currency is different
                                {
                                    /*
                                    invoice_row["unit_price"] = drs[0]["price"];
                                    invoice_row["amount"] = drs[0]["price"];
                                    invoice_row["vat"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                    invoice_row["total"] = Convert.ToDouble(invoice_row["amount"]) + Convert.ToDouble(invoice_row["vat"]);
                                    invoice_row["unit_price_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate, 2);
                                    invoice_row["amount_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate, 2);
                                    invoice_row["vat_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                    invoice_row["total_ron"] = Convert.ToDouble(invoice_row["amount_ron"]) + Convert.ToDouble(invoice_row["vat_ron"]);
                                    */
                                    invoice_row["unit_price"] = invoice_row["unit_price_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate, 2);
                                    invoice_row["amount"] = invoice_row["amount_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate, 2);
                                    invoice_row["vat"] = invoice_row["vat_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                    invoice_row["total"] = invoice_row["total_ron"] = Convert.ToDouble(invoice_row["amount_ron"]) + Convert.ToDouble(invoice_row["vat_ron"]);
                                }
                                else // very rare case when contract currency and invoice currency are different and neither is "RON"
                                {
                                    invoice_row["unit_price_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate, 2);
                                    invoice_row["amount_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate, 2);
                                    invoice_row["vat_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                    invoice_row["total_ron"] = Convert.ToDouble(invoice_row["amount_ron"]) + Convert.ToDouble(invoice_row["vat_ron"]);

                                    double InvoiceCurrencyExchangeRate = CurrenciesClass.GetExchangeRate(dateTimePickerInvoiceDate.Value, IRcurrency);

                                    invoice_row["unit_price"] = Math.Round(Convert.ToDouble(invoice_row["unit_price_ron"]) / InvoiceCurrencyExchangeRate, 2);
                                    invoice_row["amount"] = Math.Round(Convert.ToDouble(invoice_row["amount_ron"]) / InvoiceCurrencyExchangeRate, 2);
                                    invoice_row["vat"] = Math.Round(Convert.ToDouble(invoice_row["vat_ron"]) / InvoiceCurrencyExchangeRate, 2);
                                    invoice_row["total"] = Math.Round(Convert.ToDouble(invoice_row["total_ron"]) / InvoiceCurrencyExchangeRate, 2);
                                    invoice_row["unit_price_ron"] = invoice_row["unit_price"];
                                    invoice_row["amount_ron"] = invoice_row["amount"];
                                    invoice_row["vat_ron"] = invoice_row["vat"];
                                    invoice_row["total_ron"] = invoice_row["total"];
                                }
                            }
                            invoiceRows.Rows.Add(invoice_row);
                        }
                        else
                        {
                            int quantity = 0;
                            string invoicerequirements_ids = "";
                            string description = String.Format("{0} FOR PROPERTY <<{1}>> ACC. TO CONTRACT NO. {2} {3}",
                                drs[0]["service"].ToString().ToUpper(),
                                drs[0]["property"].ToString().ToUpper(),
                                drs[0]["contract"].ToString().ToUpper(),
                                (drs[0]["service"].ToString().ToUpper() == "RENT MANAGEMENT" ? " - MONTH: " : "")
                                // TO DO: get the contract date
                                );
                            foreach (DataRow dr in drs)
                            {
                                quantity += 1;
                                invoicerequirements_ids += String.Format("{0},", dr["id"].ToString());
                                description = String.Format("{0}{1}", description, (dr["service"].ToString().ToUpper() == "RENT MANAGEMENT" ? (dr["month"] == DBNull.Value ? "" : String.Format(" ,{0}", dr["month"].ToString())) : ""));
                            }
                            invoicerequirements_ids = (invoicerequirements_ids[invoicerequirements_ids.Length - 1] == ',' ? invoicerequirements_ids.Remove(invoicerequirements_ids.Length - 1) : invoicerequirements_ids);
                            DataRow invoice_row = invoiceRows.NewRow();
                            invoice_row["description"] = description; // !!! TO DO: service descriptio from corespondence table
                            invoice_row["unit_measure"] = "-";
                            invoice_row["vat_percent"] = VatPercent;
                            invoice_row["quantity"] = quantity;
                            invoice_row["invoicerequirements_ids"] = invoicerequirements_ids;
                            /*
                            invoice_row["unit_price"] = drs[0]["price"];
                            invoice_row["amount"] = Convert.ToDouble(drs[0]["price"]) * quantity;
                            invoice_row["vat"] = Math.Round(Convert.ToDouble(invoice_row["amount"]) * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2); 
                            invoice_row["total"] = Convert.ToDouble(invoice_row["amount"]) + Convert.ToDouble(invoice_row["vat"]);

                            invoice_row["unit_price_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate, 2);
                            invoice_row["amount_ron"] = Convert.ToDouble(drs[0]["price"]) * ExchangeRate * quantity;
                            invoice_row["vat_ron"] = Math.Round(Convert.ToDouble(invoice_row["amount"]) * ExchangeRate * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                            invoice_row["total_ron"] = Convert.ToDouble(invoice_row["amount_ron"]) + Convert.ToDouble(invoice_row["vat_ron"]);
                            */
                            //string IRcurrency = drs[0]["currency"].ToString();
                            string IRcurrency = comboBoxCurrency.SelectedValue.ToString();
                            if (IRcurrency == Currency && IRcurrency == "RON") //both contract and invoice currencies are "RON"
                            {
                                invoice_row["unit_price"] = invoice_row["unit_price_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]), 2);
                                invoice_row["amount"] = invoice_row["amount_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * quantity, 2);
                                invoice_row["vat"] = invoice_row["vat_ron"] = Math.Round(Convert.ToDouble(invoice_row["amount"]) * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                invoice_row["total"] = invoice_row["total_ron"] = Convert.ToDouble(invoice_row["amount"]) + Convert.ToDouble(invoice_row["vat"]);
                            }
                            else
                            {
                                if (IRcurrency != Currency && IRcurrency == "RON") // invoice currency is "RON" but contract currency is different
                                {
                                    /*
                                    invoice_row["unit_price"] = drs[0]["price"];
                                    invoice_row["amount"] = Convert.ToDouble(drs[0]["price"]) * quantity;
                                    invoice_row["vat"] = Math.Round(Convert.ToDouble(invoice_row["amount"]) * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                    invoice_row["total"] = Convert.ToDouble(invoice_row["amount"]) + Convert.ToDouble(invoice_row["vat"]);

                                    invoice_row["unit_price_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate, 2);
                                    invoice_row["amount_ron"] = Convert.ToDouble(drs[0]["price"]) * ExchangeRate * quantity;
                                    invoice_row["vat_ron"] = Math.Round(Convert.ToDouble(invoice_row["amount"]) * ExchangeRate * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                    invoice_row["total_ron"] = Convert.ToDouble(invoice_row["amount_ron"]) + Convert.ToDouble(invoice_row["vat_ron"]);
                                    */
                                    invoice_row["unit_price"] = invoice_row["unit_price_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate, 2);
                                    invoice_row["amount"] = invoice_row["amount_ron"] = Convert.ToDouble(drs[0]["price"]) * ExchangeRate * quantity;
                                    //invoice_row["vat"] = invoice_row["vat_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate * quantity * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                    invoice_row["vat"] = invoice_row["vat_ron"] = Math.Round(Convert.ToDouble(invoice_row["amount"]) * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                    invoice_row["total"] = invoice_row["total_ron"] = Convert.ToDouble(invoice_row["amount_ron"]) + Convert.ToDouble(invoice_row["vat_ron"]);
                                }
                                else // very rare case when contract currency and invoice currency are different and neither is "RON"
                                {
                                    invoice_row["unit_price_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * ExchangeRate, 2);
                                    invoice_row["amount_ron"] = Math.Round(Convert.ToDouble(drs[0]["price"]) * quantity * ExchangeRate, 2);
                                    invoice_row["vat_ron"] = Math.Round(Convert.ToDouble(invoice_row["amount_ron"]) * ExchangeRate * Convert.ToDouble(invoice_row["vat_percent"]) / 100, 2);
                                    invoice_row["total_ron"] = Convert.ToDouble(invoice_row["amount_ron"]) + Convert.ToDouble(invoice_row["vat_ron"]);

                                    double InvoiceCurrencyExchangeRate = CurrenciesClass.GetExchangeRate(dateTimePickerInvoiceDate.Value, IRcurrency);

                                    invoice_row["unit_price"] = Math.Round(Convert.ToDouble(invoice_row["unit_price_ron"]) / InvoiceCurrencyExchangeRate, 2);
                                    invoice_row["amount"] = Math.Round(Convert.ToDouble(invoice_row["amount_ron"]) / InvoiceCurrencyExchangeRate, 2);
                                    invoice_row["vat"] = Math.Round(Convert.ToDouble(invoice_row["vat_ron"]) / InvoiceCurrencyExchangeRate, 2);
                                    invoice_row["total"] = Math.Round(Convert.ToDouble(invoice_row["total_ron"]) / InvoiceCurrencyExchangeRate, 2);
                                    invoice_row["unit_price_ron"] = invoice_row["unit_price"];
                                    invoice_row["amount_ron"] = invoice_row["amount"];
                                    invoice_row["vat_ron"] = invoice_row["vat"];
                                    invoice_row["total_ron"] = invoice_row["total"];
                                }
                            }

                            invoiceRows.Rows.Add(invoice_row);
                        }
                    }
                }
                catch {
                    //invoiceRows.Rows.Add(invoiceRows.NewRow());  // -- for phase 2 - factura direct din meniu
                    MessageBox.Show(Language.GetMessageBoxText("errorAddingNewInvoice", "Adding new invoices directly from the main menu is not implemented yet!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //invoiceRows.AcceptChanges();
                DataRow total = invoiceRows.NewRow();
                total["description"] = "TOTAL";
                total["amount"] = InvoiceTotalAmount;
                total["vat"] = InvoiceTotalVat;
                total["total"] = InvoiceTotal;
                /*
                total["amount_ron"] = Math.Round(InvoiceTotalAmount * ExchangeRate, 2);
                total["vat_ron"] = Math.Round(InvoiceTotalVat * ExchangeRate, 2);
                total["total_ron"] = Math.Round(InvoiceTotal * ExchangeRate, 2);
                */
                total["amount_ron"] = InvoiceTotalAmountRon;
                total["vat_ron"] = InvoiceTotalVatRon;
                total["total_ron"] = InvoiceTotalRon;

                invoiceRows.Rows.Add(total);
            }
            else
            {
                invoiceRows = (new DataAccess(CommandType.StoredProcedure, "INVOICEROWSsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", NewDR["id"]) })).ExecuteSelectQuery().Tables[0];
                DataColumn dc = new DataColumn("invoicerequirements_ids", Type.GetType("System.String"));
                invoiceRows.Columns.Add(dc);
                DataTable invoicerequirements_invoicerows = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTS_INVOICEROWSsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", NewDR["id"]) })).ExecuteSelectQuery().Tables[0];
                foreach (DataRow dr in invoiceRows.Rows)
                {
                    DataRow[] irs = invoicerequirements_invoicerows.Select(String.Format("invoicerow_id = {0}", dr["id"].ToString()));
                    string invoicerequirements_ids = "";
                    foreach (DataRow irir in irs)
                    {
                        invoicerequirements_ids += String.Format("{0},", irir["invoicerequirement_id"].ToString());
                    }
                    invoicerequirements_ids = invoicerequirements_ids.Remove(invoicerequirements_ids.Length - 1);
                    dr["invoicerequirements_ids"] = invoicerequirements_ids;
                }
                if(NewDR.RowState != DataRowState.Added && NewDR.RowState != DataRowState.Detached) invoiceRows.AcceptChanges();
                DataRow total = invoiceRows.NewRow();
                total["description"] = "TOTAL";
                total["amount"] = InvoiceTotalAmount;
                total["vat"] = InvoiceTotalVat;
                total["total"] = InvoiceTotal;
                /*
                total["amount_ron"] = Math.Round(InvoiceTotalAmount * ExchangeRate, 2);
                total["vat_ron"] = Math.Round(InvoiceTotalVat * ExchangeRate, 2);
                total["total_ron"] = Math.Round(InvoiceTotal * ExchangeRate, 2);
                */
                total["amount_ron"] = InvoiceTotalAmountRon;
                total["vat_ron"] = InvoiceTotalVatRon;
                total["total_ron"] = InvoiceTotalRon;
                invoiceRows.Rows.Add(total);
            }
            //invoiceRows.AcceptChanges();
            invoiceRows.DefaultView.Sort = "description ASC";
            
            BindingSource bs = new BindingSource();
            bs.DataSource = invoiceRows;
            dataGridView1.DataSource = bs;
            Language.PopulateGridColumnHeaders(dataGridView1);

            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["invoice_id"].Visible = false;
            dataGridView1.Columns["invoicerequirements_ids"].Visible = false;
            dataGridView1.Columns["description"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns["unit_measure"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["description"].Width = 300;
            dataGridView1.Columns["description"].Resizable = DataGridViewTriState.False ;
            //dataGridView1.Columns["description"].Frozen = true;
            dataGridView1.Columns["description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.Columns["unit_measure"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["vat_percent"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["vat_percent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["vat_percent"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
            dataGridView1.Columns["quantity"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["quantity"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
            dataGridView1.Columns["unit_price"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["unit_price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["unit_price"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
            dataGridView1.Columns["amount"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["amount"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
            dataGridView1.Columns["vat"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["vat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["vat"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
            dataGridView1.Columns["total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["total"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;

            dataGridView1.Columns["amount_ron"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["amount_ron"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["amount_ron"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
            dataGridView1.Columns["amount_ron"].DefaultCellStyle.BackColor = Color.Salmon;
            dataGridView1.Columns["amount_ron"].ReadOnly = true;
            dataGridView1.Columns["vat_ron"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["vat_ron"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["vat_ron"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
            dataGridView1.Columns["vat_ron"].DefaultCellStyle.BackColor = Color.Salmon;
            dataGridView1.Columns["vat_ron"].ReadOnly = true;
            dataGridView1.Columns["total_ron"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["total_ron"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["total_ron"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
            dataGridView1.Columns["total_ron"].DefaultCellStyle.BackColor = Color.Salmon;
            dataGridView1.Columns["total_ron"].ReadOnly = true;

            ShowHideRonColumns();

            //invoiceRows.AcceptChanges();
            //ComputeTotals(true);
            ComputeTotals(false);
        }

        
        private void ShowHideRonColumns()
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                if (column.Name.ToLower() == "unit_price_ron" || column.Name.ToLower() == "amount_ron" || column.Name.ToLower() == "vat_ron" || column.Name.ToLower() == "total_ron")
                {
                    //column.Visible = (NewDR == null ? comboBoxCurrency.SelectedValue.ToString().ToUpper() == "RON" : NewDR["currency"].ToString().ToUpper() == "RON");
                    column.Visible = !(NewDR == null ? comboBoxCurrency.SelectedValue.ToString().ToUpper() == "RON" : NewDR["currency"].ToString().ToUpper() == "RON");
                }
            }
        }
        

        private void ComputeTotals(bool accept_changes)
        {
            try { InvoiceTotalAmount = (double)invoiceRows.Compute("SUM(amount)", "description <> 'TOTAL'"); }
            catch { InvoiceTotalAmount = 0; }
            try { InvoiceTotalVat = (double)invoiceRows.Compute("SUM(vat)", "description <> 'TOTAL'"); }
            catch { InvoiceTotalVat = 0; }
            try { InvoiceTotal = (double)invoiceRows.Compute("SUM(total)", "description <> 'TOTAL'"); }
            catch { InvoiceTotal = 0; }

            try { InvoiceTotalAmountRon = (double)invoiceRows.Compute("SUM(amount_ron)", "description <> 'TOTAL'"); }
            catch { InvoiceTotalAmountRon = 0; }
            try { InvoiceTotalVatRon = (double)invoiceRows.Compute("SUM(vat_ron)", "description <> 'TOTAL'"); }
            catch { InvoiceTotalVatRon = 0; }
            try { InvoiceTotalRon = (double)invoiceRows.Compute("SUM(total_ron)", "description <> 'TOTAL'"); }
            catch { InvoiceTotalRon = 0; }
            
            int x = dataGridView1.Rows.Count;
            dataGridView1.Rows[x - 1].Cells["description"].Value = "TOTAL";
            dataGridView1.Rows[x - 1].Cells["amount"].Value = InvoiceTotalAmount.ToString();
            dataGridView1.Rows[x - 1].Cells["vat"].Value = InvoiceTotalVat.ToString();
            dataGridView1.Rows[x - 1].Cells["total"].Value = InvoiceTotal.ToString();
            /*
            dataGridView1.Rows[x - 1].Cells["amount_ron"].Value = Math.Round(InvoiceTotalAmount * ExchangeRate, 2).ToString();
            dataGridView1.Rows[x - 1].Cells["vat_ron"].Value = Math.Round(InvoiceTotalVat * ExchangeRate, 2).ToString();
            dataGridView1.Rows[x - 1].Cells["total_ron"].Value = Math.Round(InvoiceTotal * ExchangeRate, 2).ToString();
            */
            dataGridView1.Rows[x - 1].Cells["amount_ron"].Value = InvoiceTotalAmountRon.ToString();
            dataGridView1.Rows[x - 1].Cells["vat_ron"].Value = InvoiceTotalVatRon.ToString();
            dataGridView1.Rows[x - 1].Cells["total_ron"].Value = InvoiceTotalRon.ToString();


            if (accept_changes) invoiceRows.AcceptChanges();
            /*
            dataGridView1.Rows[x - 1].DefaultCellStyle.BackColor = Color.Beige;
            //dataGridView1.Rows[x - 1].DefaultCellStyle.Font.Style = FontStyle.Bold;
            dataGridView1.Rows[x - 1].DefaultCellStyle.ForeColor = Color.Red;
            //dataGridView1.Rows[x - 1].Cells["total"].Style.Font.Size = 12;
            */
            ApplyStyleToFooter();
        }

        private void ApplyStyleToFooter()
        {
            foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
            {
                dataGridView1[dgvc.Index, dataGridView1.Rows.Count - 1].Style.BackColor = Color.Beige;
                dataGridView1[dgvc.Index, dataGridView1.Rows.Count - 1].Style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                dataGridView1[dgvc.Index, dataGridView1.Rows.Count - 1].Style.ForeColor = Color.Red;
            }
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            EditMode = 2;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.ReadOnly = false;
                foreach (DataGridViewRow dgvr in dataGridView1.Rows)
                {
                    if (dgvr.Selected)
                    {
                        dgvr.ReadOnly = false;
                        dgvr.Cells["description"].Selected = true;
                    }
                    else
                    {
                        dgvr.ReadOnly = true;
                    }
                }
                SwitchButtons(true);
                dataGridView1.BeginEdit(true);
            }
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            EditMode = 1;
            //dataGridView1.Rows.Insert(dataGridView1.Rows.Count - 2, 1);
            DataRow nrow = invoiceRows.NewRow();
            nrow["description"] = "";
            invoiceRows.Rows.Add(nrow);
            dataGridView1.ReadOnly = false;
            foreach (DataGridViewRow dgvr in dataGridView1.Rows)
            {
                if (dgvr.Cells["description"].Value.ToString().Trim() == "")
                {
                    dataGridView1.Rows[0].ReadOnly = false;
                    dgvr.Selected = true;
                    dgvr.Cells["description"].Selected = true;
                }
                else
                {
                    dgvr.Selected = false;
                    dgvr.ReadOnly = true;
                }
            }
            SwitchButtons(true);
            dataGridView1.BeginEdit(true);
        }

        private void SwitchButtons(bool edit_mode)
        {
            toolStripButtonAdd.Visible = toolStripButtonEdit.Visible = toolStripButtonDelete.Visible = !edit_mode;
            toolStripButtonSave.Visible = toolStripButtonCanel.Visible = edit_mode;
        }

        private void toolStripButtonCanel_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            //invoiceRows.RejectChanges();
            //invoiceRows.Select(String.Format("id = {0}", dataGridView1["id", dataGridView1.SelectedRows[0].Index].Value.ToString()))[0].RejectChanges();
            ((DataRowView)dataGridView1.SelectedRows[0].DataBoundItem).Row.RejectChanges();
            dataGridView1.ReadOnly = true;
            dataGridView1.ClearSelection();
            ComputeTotals(false);
            SwitchButtons(false);
            EditMode = 0;
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            EditMode = 3;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        /*
                        string ir_ids = dataGridView1.SelectedRows[0].Cells["invoicerequirements_ids"].Value.ToString();
                        foreach (string invoicerequirement_id in ir_ids.Trim().Split(','))
                        {
                            try
                            {
                                object status_id = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery();
                                InvoiceRequirementsClass.ChangeStatus(Convert.ToInt32(invoicerequirement_id.Trim()), Convert.ToInt32(status_id));
                            }
                            catch { }
                            // TO DO: Insert IE si remove (Company) Predicted IE
                            DataRow ir = InvoiceRequirements.Select(String.Format("id = {0}", invoicerequirement_id.Trim()))[0];
                            //IncomeExpensesClass.InsertIE(false, false, NewDR["currency"].ToString(), Convert.ToDouble(new_invoicerow["amount"]), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), Convert.ToInt32(ir["property_id"]), Convert.ToInt32(ir["contractservice_id"]), ir["service"].ToString(), ir["month"].ToString(), Convert.ToInt32(invoicerequirement_id.Trim()), Convert.ToInt32(NewDR["ID"]));
                            //IncomeExpensesClass.InsertIE(true, true, NewDR["currency"].ToString(), Convert.ToDouble(new_invoicerow["amount"]), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), Convert.ToInt32(ir["property_id"]), Convert.ToInt32(ir["contractservice_id"]), ir["service"].ToString(), ir["month"].ToString(), Convert.ToInt32(invoicerequirement_id.Trim()), Convert.ToInt32(NewDR["ID"]));
                            IncomeExpensesClass.IEChangeStatusByIRId(ir["id"], false, NewDR["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery());
                            // --- only for owner ---
                            IncomeExpensesClass.CompanyIEChangeStatusByIRId(ir["id"], true, NewDR["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery());
                        }
                        */
                        //dataGridView1.ReadOnly = false;
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                        dataGridView1.ClearSelection();
                        ComputeTotals(false);
                        //SwitchButtons(true);
                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            //TO DO:  Validate Data !!!
            if (ValidateData())
            {
                if (EditMode == 1 && NewDR == null) buttonSave_Click(null, null);
                SaveInvoiceRows();
                ApplyStyleToFooter();
            }
            else
            {
                //this.DialogResult = DialogResult.Cancel;
                base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                return;
            }
        }

        private bool ValidateData()
        {
            bool toReturn = true;
            errorProvider1.SetError(userTextBoxInvoiceSeries, "");
            errorProvider1.SetError(userTextBoxInvoiceNumber, "");
            errorProvider1.SetError(dateTimePickerInvoiceDate, "");
            //errorProvider1.SetError(dateTimePickerInvoiceDueDate, "");
            errorProvider1.SetError(userTextBoxInvoiceVatPercent, "");
            errorProvider1.SetError(userTextBoxClientName, "");
            errorProvider1.SetError(userTextBoxClientCui, "");
            errorProvider1.SetError(userTextBoxClientAddress, "");
            errorProvider1.SetError(userTextBoxClientRegComNo, "");
            errorProvider1.SetError(userTextBoxSupplierName, "");
            errorProvider1.SetError(userTextBoxSupplierAddress, "");
            errorProvider1.SetError(userTextBoxSupplierCui, "");
            errorProvider1.SetError(userTextBoxSupplierRegComNo, "");
            errorProvider1.SetError(dataGridView1, "");

            if (userTextBoxInvoiceSeries.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxInvoiceSeries, Language.GetErrorText("errorEmptyInvoiceSeries", "Invoice series can not by empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxInvoiceSeries.Name, Language.GetErrorText("errorEmptyInvoiceSeries", "Invoice series can not by empty!")));
                toReturn = false;
            }

            if (userTextBoxInvoiceNumber.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxInvoiceNumber, Language.GetErrorText("errorEmptyInvoiceNumber", "Invoice number can not by empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxInvoiceNumber.Name, Language.GetErrorText("errorEmptyInvoiceNumber", "Invoice number can not by empty!")));
                toReturn = false;
            }

            if (dateTimePickerInvoiceDate.Value.ToString().Trim() == "")
            {
                errorProvider1.SetError(dateTimePickerInvoiceDate, Language.GetErrorText("errorEmptyInvoiceDate", "Invoice date can not by empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(dateTimePickerInvoiceDate.Name, Language.GetErrorText("errorEmptyInvoiceDate", "Invoice date can not by empty!")));
                toReturn = false;
            }

            if (!Validator.IsDouble(userTextBoxInvoiceVatPercent.Text))
            {
                errorProvider1.SetError(userTextBoxInvoiceVatPercent, Language.GetErrorText("errorInvalidVatPercent", "Invalid VAT percent!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxInvoiceVatPercent.Name, Language.GetErrorText("errorInvalidVatPercent", "Invalid VAT percent!")));
                toReturn = false;
            }
            return toReturn;
        }

        private void SaveInvoiceRows()
        {
            try
            {
                dataGridView1.EndEdit();
                ((BindingSource)dataGridView1.DataSource).EndEdit();
                //foreach (DataRow dr in invoiceRows.GetChanges().Select("", "", DataViewRowState.Added))
                DataAccess da = new DataAccess();
                int position = 1;
                foreach (DataRow dr in invoiceRows.GetChanges().Select("", "", DataViewRowState.CurrentRows))
                {
                    switch (dr.RowState)
                    {
                        case DataRowState.Added:
                            if (dr["description"].ToString() != "TOTAL")
                            {
                                string[] invoicerequirements_ids = dr["invoicerequirements_ids"].ToString().Split(',');
                                da = new DataAccess(CommandType.StoredProcedure, "INVOICEROWSsp_insert", new object[]{
                                        new MySqlParameter("_DESCRIPTION", dr["description"]),
                                        new MySqlParameter("_UNIT_MEASURE", dr["unit_measure"]),
                                        new MySqlParameter("_QUANTITY", dr["quantity"]),
                                        new MySqlParameter("_UNIT_PRICE", dr["unit_price"]),
                                        new MySqlParameter("_VAT_PERCENT", dr["vat_percent"]),
                                        new MySqlParameter("_AMOUNT", dr["amount"]),
                                        new MySqlParameter("_VAT", dr["vat"]),
                                        new MySqlParameter("_TOTAL", dr["total"]),
                                        new MySqlParameter("_INVOICE_ID", NewDR["id"]),
                                        new MySqlParameter("_UNIT_PRICE_RON", dr["unit_price_ron"]),
                                        new MySqlParameter("_AMOUNT_RON", dr["amount_ron"]),
                                        new MySqlParameter("_VAT_RON", dr["vat_ron"]),
                                        new MySqlParameter("_TOTAL_RON", dr["total_ron"]),
                                        new MySqlParameter("_POSITION", position)
                                    });
                                DataRow new_invoicerow = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                if (dr["invoicerequirements_ids"].ToString().Trim() == "")
                                {
                                    double _vat = Math.Round(Convert.ToDouble(new_invoicerow["amount"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                    double _amount_total = Convert.ToDouble(new_invoicerow["amount"]) + _vat;
                                    //IncomeExpensesClass.InsertIE(false, false, NewDR["currency"].ToString(), Convert.ToDouble(new_invoicerow["amount"]), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), -1, -1, new_invoicerow["description"].ToString(), null, Convert.ToInt32(new_invoicerow["id"]), Convert.ToInt32(NewDR["ID"]));
                                    //IncomeExpensesClass.InsertIE(true, true, NewDR["currency"].ToString(), Convert.ToDouble(new_invoicerow["amount"]), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), -1, -1, new_invoicerow["description"].ToString(), null, Convert.ToInt32(new_invoicerow["id"]), Convert.ToInt32(NewDR["ID"]));
                                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, NewDR["currency"].ToString(), Convert.ToDouble(new_invoicerow["amount"]), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), DBNull.Value, DBNull.Value, new_invoicerow["description"].ToString(), DBNull.Value, DBNull.Value, NewDR["id"], DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, Convert.ToDouble(new_invoicerow["amount"]), _vat, _amount_total);
                                    /*
                                    DIN 01.06.2013 - NU MAI VOR ASA, DAR TREBUIE LASAT CA ESTE FOARTE POSIBIL SA SE REVINA 
                                    // --- insert IE for VAT ---
                                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, NewDR["currency"].ToString(), Math.Round(Convert.ToDouble(new_invoicerow["amount"]) * Convert.ToDouble(NewDR["VAT_PERCENT"])/100, 2), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), DBNull.Value, DBNull.Value, String.Format("VAT - {0}", new_invoicerow["description"].ToString()), DBNull.Value, DBNull.Value, NewDR["id"], DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, Math.Round(Convert.ToDouble(new_invoicerow["amount"]) * Convert.ToDouble(NewDR["VAT_PERCENT"])/100, 2));
                                    */

                                    // --- only for owner ---
                                    //IncomeExpensesClass.InsertIE(true, true, DBNull.Value, NewDR["currency"].ToString(), Convert.ToDouble(new_invoicerow["amount"]), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), -1, -1, new_invoicerow["description"].ToString(), DBNull.Value, DBNull.Value, NewDR["id"], DBNull.Value, new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery()));
                                }
                                else
                                {
                                    int ir_ir_position = 1;
                                    foreach (string invoicerequirement_id in invoicerequirements_ids)
                                    {
                                        if (invoicerequirement_id.Trim() != "")
                                        {
                                            da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTS_INVOICEROWSsp_insert", new object[]{
                                                new MySqlParameter("_INVOICEREQUIREMENT_ID", invoicerequirement_id),
                                                new MySqlParameter("_INVOICEROW_ID", new_invoicerow["id"]),
                                                new MySqlParameter("_POSITION", ir_ir_position)
                                            });
                                            da.ExecuteInsertQuery();
                                            try
                                            {
                                                object status_id = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery();
                                                InvoiceRequirementsClass.ChangeStatus(Convert.ToInt32(invoicerequirement_id.Trim()), Convert.ToInt32(status_id));
                                            }
                                            catch { }
                                            // TO DO: Insert IE si remove (Company) Predicted IE
                                            DataRow ir = InvoiceRequirements.Select(String.Format("id = {0}", invoicerequirement_id.Trim()))[0];
                                            //IncomeExpensesClass.InsertIE(false, false, NewDR["currency"].ToString(), Convert.ToDouble(new_invoicerow["amount"]), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), Convert.ToInt32(ir["property_id"]), Convert.ToInt32(ir["contractservice_id"]), ir["service"].ToString(), ir["month"].ToString(), Convert.ToInt32(invoicerequirement_id.Trim()), Convert.ToInt32(NewDR["ID"]));
                                            //IncomeExpensesClass.InsertIE(true, true, NewDR["currency"].ToString(), Convert.ToDouble(new_invoicerow["amount"]), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), Convert.ToInt32(ir["property_id"]), Convert.ToInt32(ir["contractservice_id"]), ir["service"].ToString(), ir["month"].ToString(), Convert.ToInt32(invoicerequirement_id.Trim()), Convert.ToInt32(NewDR["ID"]));
                                            IncomeExpensesClass.IEChangeStatusByIRId(ir["id"], false, NewDR["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery());
                                            /*
                                            DIN 01.06.2013 - NU MAI VOR ASA, DAR TREBUIE LASAT CA ESTE FOARTE POSIBIL SA SE REVINA 
                                            //--- insert IE for VAT ---
                                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, ir["currency"].ToString(), Math.Round(Convert.ToDouble(ir["price"]) * Convert.ToDouble(NewDR["VAT_PERCENT"])/100, 2), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), ir["property_id"], ir["contractservice_id"], String.Format("VAT - {0}", new_invoicerow["description"].ToString()), ir["month"], ir["id"], NewDR["id"], DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, Math.Round(Convert.ToDouble(ir["price"]) * Convert.ToDouble(NewDR["VAT_PERCENT"])/100, 2));
                                            */
                                            // --- only for owner ---
                                            IncomeExpensesClass.CompanyIEChangeStatusByIRId(ir["id"], true, NewDR["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery());
                                            /*
                                            DIN 01.06.2013 - NU MAI VOR ASA, DAR TREBUIE LASAT CA ESTE FOARTE POSIBIL SA SE REVINA 
                                            //--- insert IE for VAT ---
                                            //IncomeExpensesClass.InsertIE(true, true, DBNull.Value, ir["currency"].ToString(), Math.Round(Convert.ToDouble(ir["price"]) * Convert.ToDouble(NewDR["VAT_PERCENT"]) / 100, 2), Convert.ToDateTime(NewDR["date"]), Convert.ToInt32(NewDR["owner_id"]), ir["property_id"], ir["contractservice_id"], String.Format("VAT - {0}", new_invoicerow["description"].ToString()), ir["month"], ir["id"], NewDR["id"], DBNull.Value, (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, Math.Round(Convert.ToDouble(ir["price"]) * Convert.ToDouble(NewDR["VAT_PERCENT"]) / 100, 2));
                                            */
                                            ir_ir_position++;
                                        }
                                    }
                                }
                            }
                            break;
                        case DataRowState.Modified:
                            if (dr["description"].ToString() != "TOTAL")
                            {
                                string[] invoicerequirements_ids = dr["invoicerequirements_ids"].ToString().Split(',');

                                da = new DataAccess(CommandType.StoredProcedure, "INVOICEROWSsp_update", new object[]{
                                    new MySqlParameter("_ID", dr["id"]),
                                    new MySqlParameter("_DESCRIPTION", dr["description"]),
                                    new MySqlParameter("_UNIT_MEASURE", dr["unit_measure"]),
                                    new MySqlParameter("_QUANTITY", dr["quantity"]),
                                    new MySqlParameter("_UNIT_PRICE", dr["unit_price"]),
                                    new MySqlParameter("_VAT_PERCENT", dr["vat_percent"]),
                                    new MySqlParameter("_AMOUNT", dr["amount"]),
                                    new MySqlParameter("_VAT", dr["vat"]),
                                    new MySqlParameter("_TOTAL", dr["total"]),
                                    new MySqlParameter("_INVOICE_ID", NewDR["id"]),
                                    new MySqlParameter("_UNIT_PRICE_RON", dr["unit_price_ron"]),
                                    new MySqlParameter("_AMOUNT_RON", dr["amount_ron"]),
                                    new MySqlParameter("_VAT_RON", dr["vat_ron"]),
                                    new MySqlParameter("_TOTAL_RON", dr["total_ron"]),
                                    new MySqlParameter("_POSITION", position)
                                });
                                da.ExecuteUpdateQuery();
                            }
                            break;
                    }
                    position++;
                }


                for (int i = 0; i<invoiceRows.GetChanges().Select("", "", DataViewRowState.Deleted).Length; i++)
                {
                    int id = Convert.ToInt32(invoiceRows.Rows[i]["ID", DataRowVersion.Original]);
                    da = new DataAccess(CommandType.StoredProcedure, "INVOICEROWSsp_delete_chain", new object[]{
                                        new MySqlParameter("_ID", id)
                                    });
                    da.ExecuteUpdateQuery();
                    //TO DO: Restore (Company) Predicted I/E
                }                     


                invoiceRows.AcceptChanges();
                dataGridView1.ReadOnly = true;
                dataGridView1.ClearSelection();
                SwitchButtons(false);
                EditMode = 0;
                //Predicted_IncomeExpenses p_ie = new Predicted_IncomeExpenses(OwnerId, Convert.ToInt32(NewDR["id"]));
                //p_ie.ShowDialog();
                //p_ie.Dispose();
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (NewDR == null || NewDR.RowState == DataRowState.Added || NewDR.RowState == DataRowState.Detached) // add
            {
                try
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICESsp_insert", new object[]{
                        new MySqlParameter("_SERIES", userTextBoxInvoiceSeries.Text),
                        new MySqlParameter("_NUMBER", userTextBoxInvoiceNumber.Text.TrimStart('0')),
                        new MySqlParameter("_DATE", dateTimePickerInvoiceDate.Value),
                        new MySqlParameter("_DUE_DATE", dateTimePickerInvoiceDueDate.Value),
                        new MySqlParameter("_OWNER_ID", OwnerId),
                        new MySqlParameter("_SUPPLIER_ID", SettingsClass.CompanyId),
                        new MySqlParameter("_CURRENCY", comboBoxCurrency.SelectedValue),
                        new MySqlParameter("_VAT_PERCENT", userTextBoxInvoiceVatPercent.Text),
                        new MySqlParameter("_AMOUNT", InvoiceTotalAmount),
                        new MySqlParameter("_VAT", InvoiceTotalVat),
                        new MySqlParameter("_TOTAL", InvoiceTotal),
                        /*
                        new MySqlParameter("_AMOUNT_RON", Math.Round(InvoiceTotalAmount * ExchangeRate, 2)),
                        new MySqlParameter("_VAT_RON", Math.Round(InvoiceTotalVat * ExchangeRate, 2)),
                        new MySqlParameter("_TOTAL_RON", Math.Round(InvoiceTotal * ExchangeRate, 2)),
                        */
                        new MySqlParameter("_AMOUNT_RON", InvoiceTotalAmountRon),
                        new MySqlParameter("_VAT_RON", InvoiceTotalVatRon),
                        new MySqlParameter("_TOTAL_RON", InvoiceTotalRon),
                        new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "Unpaid"), new MySqlParameter("_LIST_TYPE", "invoice_status")})).ExecuteScalarQuery())
                    });
                    NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    SaveInvoiceRows();

                    this.toolStrip1.Enabled = true;
                    //userTextBoxBallance.Text = NewDR["total_ron"].ToString();
                    //userTextBoxBallance.Text = NewDR["total"].ToString();
                    userTextBoxBallance.Text = Math.Round(Convert.ToDouble(NewDR["ballance"]), 2).ToString();
                    //InitialDR = CommonFunctions.CopyDataRow(NewDR);
                    buttonIncasare.Enabled = true;
                    buttonPDF.Enabled = true;
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(NewDR.RowState == DataRowState.Modified) // update
            {
                FillInvoiceData();
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICESsp_update", CommonFunctions.GenerateMySqlParameters(NewDR.Table, NewDR.ItemArray, 1));
                da.ExecuteUpdateQuery();
                SaveInvoiceRows();
                //InitialDR = CommonFunctions.CopyDataRow(NewDR);
            }
            else if (NewDR.RowState == DataRowState.Deleted || invoiceRows.GetChanges() != null) // delete
            {
                SaveInvoiceRows();
            }
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
            ApplyStyleToFooter();
        }

        private void FillInvoiceData()
        {
            NewDR["series"] = userTextBoxInvoiceSeries.Text;
            NewDR["number"] = userTextBoxInvoiceNumber.Text.TrimStart('0');
            NewDR["date"] = dateTimePickerInvoiceDate.Value;
            NewDR["due_date"] = dateTimePickerInvoiceDueDate.Value;
            NewDR["owner_id"] = OwnerId;
            NewDR["supplier_id"] = SettingsClass.CompanyId;
            NewDR["currency"] = comboBoxCurrency.SelectedValue;
            NewDR["vat_percent"] = userTextBoxInvoiceVatPercent.Text;
            NewDR["amount"] = InvoiceTotalAmount;
            NewDR["vat"] = InvoiceTotalVat;
            NewDR["total"] = InvoiceTotal;
            /*
            NewDR["amount_ron"] = Math.Round(InvoiceTotalAmount * ExchangeRate, 2);
            NewDR["vat_ron"] = Math.Round(InvoiceTotalVat * ExchangeRate, 2);
            NewDR["total_ron"] = Math.Round(InvoiceTotal * ExchangeRate, 2);
            */
            NewDR["amount_ron"] = InvoiceTotalAmountRon;
            NewDR["vat_ron"] = InvoiceTotalVatRon;
            NewDR["total_ron"] = InvoiceTotalRon;
        }

        private void buttonPDF_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                if (LastPayment == null)
                {
                    if (((DataRowView)listBoxPayments.SelectedItem)["receipt"].ToString().IndexOf("(C)") > -1)
                        LastPayment = (new DataAccess(CommandType.StoredProcedure, "CASH_RECEIPTSsp_get_by_id", new object[] { new MySqlParameter("_ID", listBoxPayments.SelectedValue) })).ExecuteSelectQuery().Tables[0].Rows[0];
                }
            }
            catch { }
            InvoicePDF ipdf = new InvoicePDF(company, customer, NewDR, invoiceRows, LastPayment, checkBoxReceiptWithInvoice.Checked);
            */
            DataTable payments = (new DataAccess(CommandType.StoredProcedure, "CASH_RECEIPTSsp_get_by_invoice_id", new object[]{new MySqlParameter("_INVOICE_ID", NewDR["id"])})).ExecuteSelectQuery().Tables[0];
            InvoicePDF ipdf = new InvoicePDF(company, customer, NewDR, invoiceRows, payments, checkBoxReceiptWithInvoice.Checked);
            ipdf.ShowDialog();
            ipdf.Dispose();
        }

        private int GetMaxInvoiceNumber()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_max_number");
            object max_invoice_number = da.ExecuteScalarQuery();
            try
            {
                return ((max_invoice_number == null || max_invoice_number == DBNull.Value) ? 0 : Convert.ToInt32(max_invoice_number));
            }
            catch { return 0; }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double amount = 0;
            double vat = 0;
            double total = 0;
            double vat_percent = 0;
            switch (dataGridView1.Columns[e.ColumnIndex].Name.ToLower())
            {
                case "quantity":
                    try
                    {
                        amount = Math.Round(Convert.ToDouble(dataGridView1[dataGridView1.Columns["unit_price"].Index, e.RowIndex].Value) * Convert.ToDouble(dataGridView1[e.ColumnIndex, e.RowIndex].Value), 2);
                        dataGridView1[dataGridView1.Columns["amount"].Index, e.RowIndex].Value = amount;
                        vat_percent = (dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value == null || dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value.ToString() == "" || Convert.ToDouble(dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value) <= 0) ? Convert.ToDouble(userTextBoxInvoiceVatPercent.Text) : Convert.ToDouble(dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value);
                        vat = Math.Round(amount * vat_percent / 100, 2);
                        dataGridView1[dataGridView1.Columns["vat"].Index, e.RowIndex].Value = vat;
                        total = amount + vat;
                        dataGridView1[dataGridView1.Columns["total"].Index, e.RowIndex].Value = total;
                        try
                        {
                            dataGridView1[dataGridView1.Columns["amount_ron"].Index, e.RowIndex].Value = Math.Round(amount * ExchangeRate, 2);
                            dataGridView1[dataGridView1.Columns["vat_ron"].Index, e.RowIndex].Value = Math.Round(vat * ExchangeRate, 2);
                            dataGridView1[dataGridView1.Columns["total_ron"].Index, e.RowIndex].Value = Math.Round(total * ExchangeRate, 2);
                            ComputeTotals(false);
                        }
                        catch { }
                    }
                    catch { }
                    break;
                case "unit_price":
                    try
                    {
                        amount = Math.Round(Convert.ToDouble(dataGridView1[dataGridView1.Columns["quantity"].Index, e.RowIndex].Value) * Convert.ToDouble(dataGridView1[e.ColumnIndex, e.RowIndex].Value), 2);
                        dataGridView1[dataGridView1.Columns["amount"].Index, e.RowIndex].Value = amount;
                        vat_percent = (dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value == null || dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value.ToString() == "" || Convert.ToDouble(dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value) <= 0) ? Convert.ToDouble(userTextBoxInvoiceVatPercent.Text) : Convert.ToDouble(dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value);
                        vat = Math.Round(amount * vat_percent / 100, 2);
                        dataGridView1[dataGridView1.Columns["vat"].Index, e.RowIndex].Value = vat;
                        total = amount + vat;
                        dataGridView1[dataGridView1.Columns["total"].Index, e.RowIndex].Value = total;
                        try
                        {
                            dataGridView1[dataGridView1.Columns["amount_ron"].Index, e.RowIndex].Value = Math.Round(amount * ExchangeRate, 2);
                            dataGridView1[dataGridView1.Columns["vat_ron"].Index, e.RowIndex].Value = Math.Round(vat * ExchangeRate, 2);
                            dataGridView1[dataGridView1.Columns["total_ron"].Index, e.RowIndex].Value = Math.Round(total * ExchangeRate, 2);
                            ComputeTotals(false);
                        }
                        catch { }
                    }
                    catch { }
                    break;
                case "vat_percent":
                    try
                    {
                        amount = Math.Round(Convert.ToDouble(dataGridView1[dataGridView1.Columns["quantity"].Index, e.RowIndex].Value) * Convert.ToDouble(dataGridView1[dataGridView1.Columns["unit_price"].Index, e.RowIndex].Value), 2);
                        dataGridView1[dataGridView1.Columns["amount"].Index, e.RowIndex].Value = amount;
                        vat_percent = (dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value == null || dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value.ToString() == "" || Convert.ToDouble(dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value) <= 0) ? Convert.ToDouble(userTextBoxInvoiceVatPercent.Text) : Convert.ToDouble(dataGridView1[dataGridView1.Columns["vat_percent"].Index, e.RowIndex].Value);
                        vat = Math.Round(amount * vat_percent / 100, 2);
                        dataGridView1[dataGridView1.Columns["vat"].Index, e.RowIndex].Value = vat;
                        total = amount + vat;
                        dataGridView1[dataGridView1.Columns["total"].Index, e.RowIndex].Value = total;
            try
            {
                dataGridView1[dataGridView1.Columns["amount_ron"].Index, e.RowIndex].Value = Math.Round(amount * ExchangeRate, 2);
                dataGridView1[dataGridView1.Columns["vat_ron"].Index, e.RowIndex].Value = Math.Round(vat * ExchangeRate, 2);
                dataGridView1[dataGridView1.Columns["total_ron"].Index, e.RowIndex].Value = Math.Round(total * ExchangeRate, 2);
                ComputeTotals(false);
            }
            catch { }
                    }
                    catch { }
                    break;
                case "amount":
                    break;
                case "vat":
                    break;
                case "total":
                    break;
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
                else {
                    BankReceipt rc = new BankReceipt(0, Convert.ToInt32(listBoxPayments.SelectedValue));
                    rc.EditMode = 2;
                    rc.ShowDialog();
                    rc.Dispose();                
                }
            }
            catch { }
        }

        private void buttonIncasare_Click(object sender, EventArgs e)
        {
            Receipt rc = new Receipt(1, NewDR);
            rc.ShowDialog();
            try
            {
                LastPayment = rc.NewDR;
                rc.Dispose();
                FillPayments(Convert.ToInt32(NewDR["id"]));
                NewDR["amount_paid"] = Math.Round(Convert.ToDouble(NewDR["amount_paid"] == DBNull.Value ? "0" : NewDR["amount_paid"]) + Convert.ToDouble(rc.NewDR["amount_paid"] == DBNull.Value ? "0" : rc.NewDR["amount_paid"]), 2);
                NewDR["ballance"] = Math.Round(Convert.ToDouble(NewDR["ballance"] == DBNull.Value ? "0" : NewDR["ballance"]) - Convert.ToDouble(rc.NewDR["amount_paid"] == DBNull.Value ? "0" : rc.NewDR["amount_paid"]), 2);
                foreach (DataRowView drv in listBoxPayments.Items)
                {
                    if (LastPayment["id"].ToString() == drv["id"].ToString())
                    {
                        listBoxPayments.SelectedItem = drv;
                        break;
                    }
                }
                //TO DO: To check if it works !!!
                //userTextBoxBallance.Text = (Convert.ToDouble(NewDR["total_ron"]) - Convert.ToDouble(rc.NewDR["amount_paid_ron"])).ToString();
                //userTextBoxBallance.Text = (Convert.ToDouble(NewDR["total"] == DBNull.Value ? "0" : NewDR["total"]) - Convert.ToDouble(NewDR["amount_paid"] == DBNull.Value ? "0" : NewDR["amount_paid"])).ToString();
                userTextBoxBallance.Text = Convert.ToString(NewDR["ballance"]);
            }
            catch { }
        }

        private void Invoices_ResizeBegin(object sender, EventArgs e)
        {
            ResizeDataGrid();
        }

        private void Invoices_Resize(object sender, EventArgs e)
        {
            ResizeDataGrid();
        }

        private void Invoices_SizeChanged(object sender, EventArgs e)
        {
            ResizeDataGrid();
        }

        private void dataGridView1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Columns["description"].Width = 300;
        }

        private void ResizeDataGrid()
        {
            try
            {
                //if ((dataGridView1.PreferredSize.Width < groupBoxConcepts.Width && dataGridView1.Width < groupBoxConcepts.Width) || (dataGridView1.Width < groupBoxConcepts.Width && dataGridView1.PreferredSize.Width > groupBoxConcepts.Width))
                if (dataGridView1.PreferredSize.Width < groupBoxConcepts.Width)
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                else
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.Columns["description"].Width = 300;
                int min_col_width = (dataGridView1.Width - dataGridView1.Columns["description"].Width) / (Convert.ToInt32(dataGridView1.DisplayedColumnCount(true)) - 1);
                foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
                {
                    if (dgvc.Visible && dgvc.Name.ToLower() != "description")
                    {
                        dgvc.MinimumWidth = min_col_width - 4;
                    }
                }
            }
            catch { }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!panel2.Visible)
            {
                panel2.Visible = true;
                pictureBox1.Image = new Bitmap(Path.Combine(SettingsClass.Icons16ImagePath, "54.png"));
                groupBoxConcepts.Top = panel2.Top + panel2.Height + 10;
                groupBoxConcepts.Height = this.Height - panel1.Height - panel2.Height - 15;
            }
            else
            {
                panel2.Visible = false;
                pictureBox1.Image = new Bitmap(Path.Combine(SettingsClass.Icons16ImagePath, "55.png"));
                groupBoxConcepts.Top = panel1.Top + panel1.Height + 5;
                groupBoxConcepts.Height = this.Height - panel1.Height - 15;
            }
        }

        private void dateTimePickerInvoiceDate_Validated(object sender, EventArgs e)
        {
            //NewDR["date"] = dateTimePickerInvoiceDate.Value;
            //DateTime CurrencyDate = NewDR == null ? DateTime.Now : Convert.ToDateTime(NewDR["date"]);
            //this.currencyShow1 = new CurrencyShow(CurrencyDate);
            currencyShow1.GetCurrencies(dateTimePickerInvoiceDate.Value);
            ExchangeRate = CurrenciesClass.GetExchangeRate(dateTimePickerInvoiceDate.Value, NewDR == null ? Currency : NewDR["currency"].ToString());
        }

        private void comboBoxCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Currency = comboBoxCurrency.SelectedValue.ToString();
            //ExchangeRate = Currencies.GetExchangeRate(dateTimePickerInvoiceDate.Value, NewDR == null ? Currency : NewDR["currency"].ToString());
            //ShowHideRonColumns();
            FillInvoiceRows();
        }

        private void Invoices_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                InvoiceRequirementSelect ir = (InvoiceRequirementSelect)this.Launcher;
                ((BindingSource)ir.dataGrid1.dataGridView.DataSource).Filter = ir.OriginalFilter;
                ir.dataGrid1.toolStripButtonRefresh.PerformClick();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime CurrencyDate = (NewDR == null || NewDR["date"] == null) ? DateTime.Now : Convert.ToDateTime(NewDR["date"]);
            currencyShow1.GetCurrencies(CurrencyDate);
            ExchangeRate = CurrenciesClass.GetExchangeRate(CurrencyDate, (NewDR == null || NewDR["currency"] == null) ? Currency : NewDR["currency"].ToString());
        }
    }
}
