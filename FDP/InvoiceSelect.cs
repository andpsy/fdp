using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace FDP
{
    public partial class InvoiceSelect : UserForm
    {
        //this.dataGrid1 = new FDP.DataGrid("INVOICESsp_select", null, "INVOICESsp_insert", null, "INVOICESsp_update", null, "INVOICESsp_delete", null, new string[] { "DATE" }, new string[] { "AMOUNT", "VAT", "TOTAL", "VAT_PERCENT" }, null, new string[] { "OWNER_ID", "SUPPLIER_ID", "STATUS_ID" }, new string[] { "OWNER", "SUPPLIER" }, new string[] { "SERIES", "NUMBER", "DATE", "DUE_DATE", "OWNER", "SUPPLIER", "CURRENCY", "VAT_PERCENT", "AMOUNT", "VAT", "TOTAL" }, this.Selectable, true);
        //this.dataGrid1 = new FDP.DataGrid("INVOICESsp_select", null, "INVOICESsp_insert", null, "INVOICESsp_update", null, "INVOICESsp_delete_chain", null, new string[] { "DATE" }, new string[] { "AMOUNT", "VAT", "TOTAL", "VAT_PERCENT" }, null, null, new string[] { "OWNER", "SUPPLIER" }, new string[] { "SERIES", "NUMBER", "DATE", "DUE_DATE", "OWNER", "SUPPLIER", "CURRENCY", "VAT_PERCENT", "AMOUNT", "VAT", "TOTAL", "AMOUNT_PAID", "BALLANCE", "STATUS" }, this.Selectable, false);
        public bool Selectable = false;

        public int IdToReturn
        {
            get;
            set;
        }
        public InvoiceSelect()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
        }

        public InvoiceSelect(bool selectable)
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
        }

        private void userButtonApplyDefaultFilter_Click(object sender, EventArgs e)
        {
            ApplyDefaultFiler();
        }

        private void userButtonGenerateInvoice_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Not implemented");
            string original_filter = ((BindingSource)dataGrid1.dataGridView.DataSource).Filter;
            ((BindingSource)dataGrid1.dataGridView.DataSource).Filter = (original_filter.Trim() == "" ? "invoice=true" : String.Format("{0} AND invoice=true", original_filter));
            if (((BindingSource)dataGrid1.dataGridView.DataSource).Count > 0)
            {
                bool multiple_owners = false;
                object owner_id = dataGrid1.dataGridView["owner_id", 0].Value;
                foreach (DataGridViewRow dgvr in dataGrid1.dataGridView.Rows)
                {
                    if (dgvr.Cells["owner_id"].Value.ToString() != owner_id.ToString())
                    {
                        multiple_owners = true;
                        break;
                    }
                    owner_id = dgvr.Cells["owner_id"].Value;
                }
                if (!multiple_owners)
                {
                    DataTable dt = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView.ToTable();
                    Invoices i = new Invoices(dt);
                    i.ShowDialog();
                }
                else
                {
                    MessageBox.Show(Language.GetMessageBoxText("multipleOwnersForInvoice", "There are more owners selected! Please select only one owner for this operation!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            ((BindingSource)dataGrid1.dataGridView.DataSource).Filter = original_filter;
        }

        private void ApplyDefaultFiler()
        {
            ((BindingSource)dataGrid1.dataGridView.DataSource).Filter =
                String.Format(CultureInfo.InvariantCulture.DateTimeFormat,
                     "date <= #{0}# AND ((CONVERT(SUBSTRING(ISNULL(month,'999999'), IIF(LEN(ISNULL(month,'999999'))=7,4,3), 4), 'System.Int32')={1} AND CONVERT(SUBSTRING(ISNULL(month,'999999'), 1, IIF(LEN(ISNULL(month,'999999'))=7,2,1)), 'System.Int32')<={2}) OR month IS NULL AND status_id={3} AND not_invoiceable=false)",
                     new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                     DateTime.Now.Year,
                     DateTime.Now.Month,
                     (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery().ToString()
                     );
        }

        private void InvoiceSelect_Load(object sender, EventArgs e)
        {
            ////dataGrid1.AddToolStripButton(Language.GetLabelText("InvoiceRequirementSelect.toolStripButtonDefaultFilter", "Default filter"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "61.png")), new EventHandler(userButtonApplyDefaultFilter_Click), "toolStripButtonDefaultFilter", 2);
            //dataGrid1.AddToolStripButton("", new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "61.png")), new EventHandler(userButtonApplyDefaultFilter_Click), "toolStripButtonDefaultFilter", 2);
            //dataGrid1.AddToolStripButton(Language.GetLabelText("InvoiceRequirementSelect.toolStripButtonGenerateInvoice", "Invoice"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "31.png")), new EventHandler(userButtonGenerateInvoice_Click), "toolStripButtonGenerateInvoice", 5);
            ////ApplyDefaultFiler();
            dataGrid1.AddLinkColumn("bank_receipts", "bank_receipts", dataGrid1.dataGridView.Columns["ballance"].Index + 1);
            dataGrid1.AddLinkColumn("cash_receipts", "cash_receipts", dataGrid1.dataGridView.Columns["ballance"].Index + 1);
            dataGrid1.dataGridView.ReadOnly = false;
        }

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow i = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new Invoices(i);
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
            Invoices f = (Invoices)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    /*
                    i = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(i.Table, i.ItemArray, 0);
                    try
                    {
                        i["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", i["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        i["supplier"] = (new DataAccess(CommandType.StoredProcedure, "COMPANIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", i["supplier_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        i["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", i["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }

                    ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(i);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    int ir_id = Convert.ToInt32(((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).Select("id = max(id)")[0]["id"]);
                    //TO DO: modifiy PREDICTED IE
                    */
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
                DataRow invoice = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
                var f = new Invoices(invoice);
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
            Invoices f = (Invoices)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    /*
                    object predicted_income_expense_id = (new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_get_by_ir_id", new object[]{
                        new MySqlParameter("_INVOICEREQUIREMENT_ID", invoice["id"])})).ExecuteScalarQuery();

                    invoice = f.InvoiceRequirementDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(ir.Table, ir.ItemArray, 1);
                    try
                    {
                        i["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", i["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        i["supplier"] = (new DataAccess(CommandType.StoredProcedure, "COMPANIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", i["supplier_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        i["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", i["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    dataGrid1.da.AttachUpdateParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();

                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_update", new object[]{
                        new MySqlParameter("_ID", predicted_income_expense_id),
                        new MySqlParameter("_TYPE", false),
                        new MySqlParameter("_CURRENCY", ir["currency"]),
                        new MySqlParameter("_AMOUNT", ir["price"]),
                        new MySqlParameter("_DATE", ir["date"]),
                        new MySqlParameter("_OWNER_ID", ir["owner_id"]),
                        new MySqlParameter("_PROPERTY_ID", ir["property_id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", ir["contractservice_id"]),
                        new MySqlParameter("_SERVICE_DESCRIPTION", ir["comments"]),
                        new MySqlParameter("_MONTH", ir["month"])
                    });
                    da.ExecuteUpdateQuery();
                     */
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
                        /*
                        object predicted_income_expense_id = (new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_get_id", new object[]{
                            new MySqlParameter("_TYPE", false),
                            new MySqlParameter("_CURRENCY", dataGrid1.dataGridView["currency", dataGrid1.dataGridView.SelectedRows[0].Index].Value),
                            new MySqlParameter("_AMOUNT", dataGrid1.dataGridView["price", dataGrid1.dataGridView.SelectedRows[0].Index].Value),
                            new MySqlParameter("_DATE", dataGrid1.dataGridView["date", dataGrid1.dataGridView.SelectedRows[0].Index].Value),
                            new MySqlParameter("_OWNER_ID", dataGrid1.dataGridView["owber_id", dataGrid1.dataGridView.SelectedRows[0].Index].Value),
                            new MySqlParameter("_PROPERTY_ID", dataGrid1.dataGridView["property_id", dataGrid1.dataGridView.SelectedRows[0].Index].Value),
                            new MySqlParameter("_CONTRACTSERVICE_ID", dataGrid1.dataGridView["contractservice_id", dataGrid1.dataGridView.SelectedRows[0].Index].Value),
                            new MySqlParameter("_SERVICE_DESCRIPTION", dataGrid1.dataGridView["comments", dataGrid1.dataGridView.SelectedRows[0].Index].Value)
                        })).ExecuteScalarQuery();
                        */
                        try
                        {
                            object receipts = new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_payments", new object[] { new MySqlParameter("_INVOICE_ID", key) }).ExecuteSelectQuery();
                            if (receipts != null && ((DataSet)receipts).Tables.Count > 0 && ((DataSet)receipts).Tables[0].Rows.Count > 0)
                            {
                                ans = MessageBox.Show(Language.GetMessageBoxText("invoiceHasPayments", "The selected invoice has payments! Deleting the invoice will also delete the attached payments! Are you sure you want to continue?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (ans == System.Windows.Forms.DialogResult.No)
                                    return;
                            }
                        }
                        catch { }

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
    }
}
