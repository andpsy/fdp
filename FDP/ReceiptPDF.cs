using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;
using MySql.Data.MySqlClient;


namespace FDP
{
    public partial class ReceiptPDF : Form
    {
        public DataTable InvoiceRows;
        public DataRow Invoice;
        public DataRow Company;
        public DataRow Customer;
        public DataRow LastPayment;
        public bool PrintReceiptTogether;

        public ReceiptPDF()
        {
            InitializeComponent();
        }

        public ReceiptPDF(DataRow _company, DataRow _customer, DataRow _invoice, DataRow _last_payment)
        {
            Company = _company;
            Customer = _customer;
            Invoice = _invoice;
            LastPayment = _last_payment;
            InitializeComponent();
        }

        private void ReceiptPDF_Load(object sender, EventArgs e)
        {

            List<ReportParameter> paramList = new List<ReportParameter>();

            //paramList.Add(new ReportParameter("invoiceLogo", Path.Combine(SettingsClass.ImagePath, "logo.jpg"), true)); // TO DO: parametrizat cale logo in setari !!!
            paramList.Add(new ReportParameter("invoiceSupplier", Company["name"].ToString(), true));
            paramList.Add(new ReportParameter("supplierRegComNo", Company["commercial_register_number"].ToString(), true));
            paramList.Add(new ReportParameter("supplierAddress", Company["address"].ToString(), true));
            paramList.Add(new ReportParameter("supplierDistrict", Company["district"].ToString(), true));
            paramList.Add(new ReportParameter("supplierCui", Company["cui"].ToString(), true));
            paramList.Add(new ReportParameter("supplierIban1", Company["bank_account_details1"].ToString(), true));
            paramList.Add(new ReportParameter("supplierBank1", Company["bank_name1"].ToString(), true));
            paramList.Add(new ReportParameter("supplierIban2", Company["bank_account_details2"].ToString(), true));
            paramList.Add(new ReportParameter("supplierBank2", Company["bank_name2"].ToString(), true));
            paramList.Add(new ReportParameter("supplierCapital", Company["capital"].ToString(), true));
            paramList.Add(new ReportParameter("supplierEmails", Company["emails"].ToString().ToLower(), true));
            paramList.Add(new ReportParameter("supplierPhones", Company["phones"].ToString().ToLower(), true));
            paramList.Add(new ReportParameter("supplierFaxes", Company["faxes"].ToString().ToLower(), true));
            paramList.Add(new ReportParameter("supplierWebsites", Company["websites"].ToString().ToLower(), true));

            paramList.Add(new ReportParameter("invoiceCustomer", Customer["name"].ToString().ToUpper(), true));
            paramList.Add(new ReportParameter("customerRegComNo", Customer["commercial_register_number"].ToString().ToUpper(), true));
            if (Customer["type_id"].ToString() == (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Individual"), new MySqlParameter("_LIST_TYPE", "owner_type") })).ExecuteScalarQuery().ToString())
            {
                paramList.Add(new ReportParameter("customerCnpCui", "CNP:", true));
                paramList.Add(new ReportParameter("customerCui", Customer["cnp"].ToString().ToUpper(), true));
            }
            else
            {
                paramList.Add(new ReportParameter("customerCnpCui", "Cod inregistrare fiscala:", true));
                paramList.Add(new ReportParameter("customerCui", Customer["cui"].ToString().ToUpper(), true));
            }
            paramList.Add(new ReportParameter("customerAddress", Customer["address"].ToString(), true));
            paramList.Add(new ReportParameter("customerDistrict", Customer["district"].ToString().ToUpper(), true));
            paramList.Add(new ReportParameter("customerIban1", Customer["bank_account_details1"].ToString().ToUpper(), true));
            paramList.Add(new ReportParameter("customerBank1", Customer["bank_name1"].ToString(), true));
            paramList.Add(new ReportParameter("customerIban2", Customer["bank_account_details2"].ToString().ToUpper(), true));
            paramList.Add(new ReportParameter("customerBank2", Customer["bank_name2"].ToString(), true));
            paramList.Add(new ReportParameter("customerEmails", Customer["emails"].ToString().ToLower(), true));
            paramList.Add(new ReportParameter("customerPhones", Customer["phones"].ToString().ToLower(), true));
            //paramList.Add(new ReportParameter("customerFaxes", Customer["faxes"].ToString().ToLower(), true));

            paramList.Add(new ReportParameter("invoiceSeries", Invoice["series"].ToString().ToUpper(), true));
            paramList.Add(new ReportParameter("invoiceNumber", Invoice["number"].ToString().PadLeft(5, '0'), true));
            paramList.Add(new ReportParameter("invoiceDate", Convert.ToDateTime(Invoice["date"]).ToString("dd-MM-yyyy"), true));
            paramList.Add(new ReportParameter("invoiceDueDate", String.Format("Termen de plata: {0}", Convert.ToDateTime(Invoice["date"]).ToString("dd-MM-yyyy")), true));
            paramList.Add(new ReportParameter("invoiceBnrExchangeRate", String.Format("Curs BNR la data facturarii: {0}", ""), true)); // TO DO: bring exchange rates
            paramList.Add(new ReportParameter("invoiceLongVat", String.Format("Cota TVA: {0}%", Invoice["vat_percent"].ToString()), true));
            paramList.Add(new ReportParameter("invoiceTotalAmount", Invoice["amount"].ToString(), true));
            paramList.Add(new ReportParameter("invoiceTotalVat", Invoice["vat"].ToString(), true));
            paramList.Add(new ReportParameter("invoiceTotal", Invoice["total"].ToString(), true));
            paramList.Add(new ReportParameter("invoiceTotalAmountRon", Invoice["amount_ron"].ToString(), true));
            paramList.Add(new ReportParameter("invoiceTotalVatRon", Invoice["vat_ron"].ToString(), true));
            paramList.Add(new ReportParameter("invoiceTotalRon", Invoice["total_ron"].ToString(), true));

            string receipt_series = (LastPayment == null || LastPayment["series"] == DBNull.Value) ? "" : LastPayment["series"].ToString();
            paramList.Add(new ReportParameter("receiptSeries", receipt_series, true));
            string receipt_number = (LastPayment == null || LastPayment["number"] == DBNull.Value) ? "" : LastPayment["number"].ToString().PadLeft(5,'0');
            paramList.Add(new ReportParameter("receiptNumber", receipt_number, true));
            string receipt_date = (LastPayment == null || LastPayment["date"] == DBNull.Value) ? "" : Convert.ToDateTime(LastPayment["date"]).ToString("dd-MM-yyyy");
            paramList.Add(new ReportParameter("receiptDate", receipt_date, true));
            string receipt_amount = (LastPayment == null || LastPayment["amount_paid"] == DBNull.Value) ? "" : LastPayment["amount_paid"].ToString();
            paramList.Add(new ReportParameter("receiptAmount", receipt_amount, true));
            string reprezentand = "";
            try
            {
                //reprezentand = String.Format("Contravaloare factura {0} {1} din data {2}", Invoice["series"].ToString().ToUpper(), Invoice["number"].ToString().PadLeft(5, '0'), Convert.ToDateTime(Invoice["date"]).ToString("dd-MM-yyyy"));
                if (Convert.ToDouble(Invoice["amount"]) > Convert.ToDouble(LastPayment["amount_paid"]))
                    reprezentand = String.Format("Plata partiala factura {0} {1} / {2}", Invoice["series"].ToString().ToUpper(), Invoice["number"].ToString().PadLeft(5, '0'), Convert.ToDateTime(Invoice["date"]).ToString("dd-MM-yyyy"));
                else
                    reprezentand = String.Format("Contravaloare factura {0} {1} din data {2}", Invoice["series"].ToString().ToUpper(), Invoice["number"].ToString().PadLeft(5, '0'), Convert.ToDateTime(Invoice["date"]).ToString("dd-MM-yyyy"));
            }
            catch { reprezentand = ""; }
            paramList.Add(new ReportParameter("reprezentand", reprezentand, true));
            string amount_string = "";
            try
            {
                amount_string = CommonFunctions.NumberToLetters(Convert.ToDouble(LastPayment["amount_paid"]));
            }
            catch { amount_string = ""; }
            paramList.Add(new ReportParameter("receiptTotalRonString", amount_string, true));
            //InvoiceRows.AcceptChanges();
            this.reportViewer1.LocalReport.SetParameters(paramList);
            //this.reportViewer1.LocalReport.DataSources.Clear();
            //this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", InvoiceRows));

            this.reportViewer1.RefreshReport();

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = reportViewer1.LocalReport.Render(
              "PDF", null, out mimeType, out encoding, out extension,
              out streamids, out warnings);
            FileStream fs = new FileStream(Path.Combine(SettingsClass.PDFExportPath, String.Format("receipt_{0}_{1}", receipt_series.ToUpper(), receipt_number)), FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }
    }
}
