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
    public partial class ReceiptSelect : UserForm
    {
        //this.dataGrid1 = new FDP.DataGrid("CASH_RECEIPTSsp_select", null, "CASH_RECEIPTSsp_insert", null, "CASH_RECEIPTSsp_update", null, "CASH_RECEIPTSsp_delete", null, new string[] { "DATE" }, new string[] { "AMOUNT_PAID" }, null, null, new string[] { "INVOICE" }, new string[] { "SERIES", "NUMBER", "DATE", "AMOUNT_PAID", "CURRENCY", "INVOICE" }, this.Selectable, false);
        public bool Selectable = false;

        public ReceiptSelect()
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

        public ReceiptSelect(bool selectable)
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

        public ReceiptSelect(int id, string id_type)
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);

            
            switch (id_type)
            {
                case "invoice_id":
                    dataGrid1.da.bindingSource.Filter = String.Format("INVOICE_ID = {0}", id.ToString());
                    break;
            }
        }


        private void ReceiptSelect_Load(object sender, EventArgs e)
        {
            ////dataGrid1.AddToolStripButton(Language.GetLabelText("InvoiceRequirementSelect.toolStripButtonDefaultFilter", "Default filter"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "61.png")), new EventHandler(userButtonApplyDefaultFilter_Click), "toolStripButtonDefaultFilter", 2);
            //dataGrid1.AddToolStripButton("", new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "61.png")), new EventHandler(userButtonApplyDefaultFilter_Click), "toolStripButtonDefaultFilter", 2);
            //dataGrid1.AddToolStripButton(Language.GetLabelText("InvoiceRequirementSelect.toolStripButtonGenerateInvoice", "Invoice"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "31.png")), new EventHandler(userButtonGenerateInvoice_Click), "toolStripButtonGenerateInvoice", 5);
            ////ApplyDefaultFiler();

            //dataGrid1.dataGridView.ReadOnly = false;
        }

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow cr = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            //EditMode = 1; // ADD
            var f = new Receipt(0, cr);
            //f.Launcher = this;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    cr = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(cr.Table, cr.ItemArray, 0);
                    try
                    {
                        DataRow invoice = (new DataAccess(CommandType.StoredProcedure, "CASH_RECEIPTSsp_get_invoice", new object[] { new MySqlParameter("_ID", cr["id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                        cr["invoice"] = String.Format("{0} {1} / {2}", invoice["series"].ToString(), invoice["number"].ToString().PadLeft(5, '0'), Convert.ToDateTime(invoice["date"]).ToString(SettingsClass.DateFormat));                        
                    }
                    catch { }

                    ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(cr);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    int cr_id = Convert.ToInt32(((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).Select("id = max(id)")[0]["id"]);
                    //TO DO: Insert IEs and modifiy Predicted IE
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
            f.Dispose();
        }

        public void buttonEdit_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            if (dataGrid1.dataGridView.SelectedRows.Count > 0)
            {
                DataRow cr = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
                //EditMode = 2; // EDIT
                var f = new Receipt(0, cr);
                //f.Launcher = this;
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        cr = f.NewDR;
                        object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(cr.Table, cr.ItemArray, 1);
                        try
                        {
                            DataRow invoice = (new DataAccess(CommandType.StoredProcedure, "CASH_RECEIPTSsp_get_invoice", new object[] { new MySqlParameter("_ID", cr["id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                            cr["invoice"] = String.Format("{0} {1} / {2}", invoice["series"].ToString(), invoice["number"].ToString().PadLeft(5, '0'), Convert.ToDateTime(invoice["date"]).ToString(SettingsClass.DateFormat));
                        }
                        catch { }
                        dataGrid1.da.AttachUpdateParams(mySqlParams);
                        dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                        //TO DO: Modifiy IE & Predicted IE
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).RejectChanges();
                f.Dispose();
            }
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

                        //TO DO: Insert IEs and modifiy Predicted IE
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
