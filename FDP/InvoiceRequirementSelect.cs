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
    public partial class InvoiceRequirementSelect : UserForm
    {
        ////this.dataGrid1 = new FDP.DataGrid("INVOICEREQUIREMENTSsp_select", null, "INVOICEREQUIREMENTSsp_insert", null, "INVOICEREQUIREMENTSsp_update", null, "INVOICEREQUIREMENTSsp_delete", null, new string[] { "DATE" }, new string[] { "PRICE" }, null, new string[] { "STATUS_ID" }, new string[] { "OWNER", "PROPERTY", "CONTRACT", "RENTCONTRACT" }, new string[] { "OWNER", "CONTRACT", "RENTCONTRACT", "PROPERTY", "SERVICE", "COMMENTS", "PRICE", "CURRENCY", "MONTH", "DATE", "STATUS" }, this.Selectable, true);
        //this.dataGrid1 = new FDP.DataGrid("INVOICEREQUIREMENTSsp_select", null, "INVOICEREQUIREMENTSsp_insert", null, "INVOICEREQUIREMENTSsp_update", null, "INVOICEREQUIREMENTSsp_delete", null, new string[] { "DATE" }, new string[] { "PRICE" }, null, null, new string[] { "OWNER", "PROPERTY", "CONTRACT", "RENTCONTRACT" }, new string[] { "OWNER", "CONTRACT", "RENTCONTRACT", "PROPERTY", "SERVICE", "COMMENTS", "PRICE", "CURRENCY", "MONTH", "DATE", "STATUS" }, this.Selectable, false);

        public bool Selectable = false;
        public string OriginalFilter
        {
            get;
            set;
        }

        public InvoiceRequirementSelect()
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

        public InvoiceRequirementSelect(bool selectable)
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
            try
            {
                dataGrid1.dataGridView.EndEdit();
                //((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).AcceptChanges();
                ((BindingSource)dataGrid1.dataGridView.DataSource).EndEdit();
                OriginalFilter = ((BindingSource)dataGrid1.dataGridView.DataSource).Filter == null ? "" : ((BindingSource)dataGrid1.dataGridView.DataSource).Filter;
                ((BindingSource)dataGrid1.dataGridView.DataSource).Filter = (OriginalFilter.Trim() == "" ? "invoice=1" : String.Format("({0}) AND invoice=1", OriginalFilter));
                if (((BindingSource)dataGrid1.dataGridView.DataSource).Count > 0)
                {
                    bool multiple_owners = false;
                    bool not_invoiceable_invoices_selected = false;
                    object owner_id = dataGrid1.dataGridView["owner_id", 0].Value;
                    foreach (DataGridViewRow dgvr in dataGrid1.dataGridView.Rows)
                    {
                        if (dgvr.Cells["owner_id"].Value.ToString() != owner_id.ToString())
                        {
                            multiple_owners = true;
                            break;
                        }
                        if (Convert.ToBoolean(dgvr.Cells["NOT_INVOICEABLE"].Value))
                        {
                            not_invoiceable_invoices_selected = true;
                            break;
                        }
                        owner_id = dgvr.Cells["owner_id"].Value;
                    }
                    if (!multiple_owners && !not_invoiceable_invoices_selected)
                    {
                        DataTable dt = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).DefaultView.ToTable();
                        Invoices i = new Invoices(dt);
                        //i.ShowDialog(this);
                        EditMode = 9; // SPECIAL
                        main m = FindMainForm();
                        i.Launcher = this;
                        this.ChildLaunched = i;
                        i.TopLevel = false;
                        i.MdiParent = m;
                        m.panelMain.Controls.Add(i);
                        i.StartPosition = FormStartPosition.CenterScreen;
                        i.BringToFront();
                        i.Show();
                    }
                    if(multiple_owners)
                    {
                        MessageBox.Show(Language.GetMessageBoxText("multipleOwnersForInvoice", "There are more owners selected! Please select only one owner for this operation!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    if (not_invoiceable_invoices_selected)
                    {
                        MessageBox.Show(Language.GetMessageBoxText("notInvoiceableIRSelected", "You have selected one or more \"NOT INVOICEABLE\" Invoice Requirements! You can not continue"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void ApplyDefaultFiler()
        {
            ((BindingSource)dataGrid1.dataGridView.DataSource).Filter =
                String.Format(CultureInfo.InvariantCulture.DateTimeFormat,
                     "CONVERT(date, System.DateTime) <= #{0}# AND ((CONVERT(SUBSTRING(ISNULL(month,'999999'), IIF(LEN(ISNULL(month,'999999'))=7,4,3), 4), 'System.Int32')={1} AND CONVERT(SUBSTRING(ISNULL(month,'999999'), 1, IIF(LEN(ISNULL(month,'999999'))=7,2,1)), 'System.Int32')<={2}) OR month IS NULL) AND status_id={3} AND not_invoiceable=false",
                     new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                     DateTime.Now.Year,
                     DateTime.Now.Month,
                     (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery().ToString()
                     );
        }

        private void InvoiceRequirements_Load(object sender, EventArgs e)
        {
            //dataGrid1.AddToolStripButton(Language.GetLabelText("InvoiceRequirementSelect.toolStripButtonDefaultFilter", "Default filter"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "61.png")), new EventHandler(userButtonApplyDefaultFilter_Click), "toolStripButtonDefaultFilter", 2);
            dataGrid1.AddToolStripButton("", new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "61.png")), new EventHandler(userButtonApplyDefaultFilter_Click), "toolStripButtonDefaultFilter", 2);
            dataGrid1.AddToolStripButton(Language.GetLabelText("InvoiceRequirementSelect.toolStripButtonGenerateInvoice", "Invoice"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "31.png")), new EventHandler(userButtonGenerateInvoice_Click), "toolStripButtonGenerateInvoice", 5);
            ApplyDefaultFiler();

            dataGrid1.dataGridView.ReadOnly = false;

            int col_index = dataGrid1.dataGridView.Columns["invoice"].Index;
            dataGrid1.dataGridView.Columns.Remove("invoice");
            DataGridViewCheckBoxColumn dgvcbc_invoice = new DataGridViewCheckBoxColumn();
            dgvcbc_invoice.HeaderText = Language.GetColumnHeaderText("invoice", "INVOICE");
            dgvcbc_invoice.DataPropertyName = "invoice";
            dgvcbc_invoice.Name = "invoice";
            dgvcbc_invoice.Width = 30;
            dgvcbc_invoice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvcbc_invoice.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGrid1.dataGridView.Columns.Insert(col_index, dgvcbc_invoice);
            // add checkbox header
            Rectangle rect = dataGrid1.dataGridView.GetCellDisplayRectangle(0, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position correctly.
            //rect.X = rect.Location.X + (rect.Width / 4);

            CheckBox checkboxHeader = new CheckBox();
            checkboxHeader.Name = "checkboxHeader";
            checkboxHeader.Size = new Size(rect.Height - 4, rect.Height - 4);
            checkboxHeader.BackColor = Color.Transparent;
            checkboxHeader.Location = new Point(rect.Location.X + 2, rect.Location.Y + 2);
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            dataGrid1.dataGridView.Columns["invoice"].Frozen = true;
            dataGrid1.dataGridView.Controls.Add(checkboxHeader);

            /*
            DataGridViewCheckBoxColumn dgvcbc_group = new DataGridViewCheckBoxColumn();
            dgvcbc_group.HeaderText = Language.GetColumnHeaderText("group", "GROUP");
            dgvcbc_group.DataPropertyName = "group";
            dgvcbc_group.Name = "group";
            col_index = dataGrid1.dataGridView.Columns["group"].Index;
            dataGrid1.dataGridView.Columns.Remove("group");
            dataGrid1.dataGridView.Columns.Insert(col_index, dgvcbc_group);
            dataGrid1.dataGridView.Columns["group"].Visible = false;
            */
            foreach (DataGridViewColumn dgvc in dataGrid1.dataGridView.Columns)
            {
                if (dgvc.Name.ToLower() == "invoice" || dgvc.Name.ToLower() == "group" || dgvc.Name.ToLower() == "status")
                    dgvc.ReadOnly = false;
                else
                    dgvc.ReadOnly = true;
            }
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGrid1.dataGridView.RowCount; i++)
            {
                if (dataGrid1.dataGridView["status", i].Value.ToString().ToLower() != "invoiced")
                {
                    dataGrid1.dataGridView[0, i].Value = ((CheckBox)dataGrid1.dataGridView.Controls.Find("checkboxHeader", true)[0]).Checked;
                }
            }
            dataGrid1.dataGridView.EndEdit();
        }

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow ir = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new InvoiceRequirements(ir);
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
            InvoiceRequirements f = (InvoiceRequirements)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow ir = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(ir.Table, ir.ItemArray, 0);
                    /*
                    try
                    {
                        ir["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", ir["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        ir["contract"] = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_number_by_id", new object[] { new MySqlParameter("_ID", ir["contract_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        ir["rentcontract"] = (new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_number_by_id", new object[] { new MySqlParameter("_ID", ir["rentcontract_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        ir["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", ir["property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        ir["service"] = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", ir["contractservice_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        ir["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ir["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    */
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(ir);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    int ir_id = Convert.ToInt32(((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).Select("id = max(id)")[0]["id"]);
                    /*
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_insert", new object[]{
                        new MySqlParameter("_TYPE", false),
                        new MySqlParameter("_CURRENCY", ir["currency"]),
                        new MySqlParameter("_AMOUNT", ir["price"]),
                        new MySqlParameter("_DATE", ir["date"]),
                        new MySqlParameter("_OWNER_ID", ir["owner_id"]),
                        new MySqlParameter("_PROPERTY_ID", ir["property_id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", ir["contractservice_id"]),
                        new MySqlParameter("_SERVICE_DESCRIPTION", ir["comments"]),
                        new MySqlParameter("_MONTH", ir["month"]),
                        new MySqlParameter("_INVOICEREQUIREMENT_ID", ir_id)
                    });
                    da.ExecuteInsertQuery();
                    */
                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, ir, false);
                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, ir, false);
                    //dataGrid1.toolStripButtonRefresh.PerformClick();
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
                DataRow ir = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
                var f = new InvoiceRequirements(ir);
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
            InvoiceRequirements f = (InvoiceRequirements)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    /*
                    object predicted_income_expense_id = (new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_get_id", new object[]{
                        new MySqlParameter("_TYPE", false),
                        new MySqlParameter("_CURRENCY", ir["currency"]),
                        new MySqlParameter("_AMOUNT", ir["price"]),
                        new MySqlParameter("_DATE", ir["date"]),
                        new MySqlParameter("_OWNER_ID", ir["owner_id"]),
                        new MySqlParameter("_PROPERTY_ID", ir["property_id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", ir["contractservice_id"]),
                        new MySqlParameter("_SERVICE_DESCRIPTION", ir["comments"])
                    })).ExecuteScalarQuery();
                    */
                    DataRow ir = f.NewDR;
                    DataRow predicted_income_expense = (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_get_by_ir_id", new object[]{
                            new MySqlParameter("_INVOICEREQUIREMENT_ID", ir["id"])})).ExecuteSelectQuery().Tables[0].Rows[0];

                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(ir.Table, ir.ItemArray, 1);
                    /*
                    try
                    {
                        ir["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", ir["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        ir["contract"] = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_number_by_id", new object[] { new MySqlParameter("_ID", ir["contract_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        ir["rentcontract"] = (new DataAccess(CommandType.StoredProcedure, "RENTCONTRACTSsp_get_number_by_id", new object[] { new MySqlParameter("_ID", ir["rentcontract_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        ir["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", ir["property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        ir["service"] = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", ir["contractservice_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        ir["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ir["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    */
                    dataGrid1.da.AttachUpdateParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();

                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", new object[]{
                            new MySqlParameter("_ID", predicted_income_expense["id"]),
                            new MySqlParameter("_TYPE", false),
                            new MySqlParameter("_CURRENCY", ir["currency"]),
                            new MySqlParameter("_AMOUNT", ir["price"]),
                            new MySqlParameter("_DATE", ir["date"]),
                            new MySqlParameter("_OWNER_ID", ir["owner_id"]),
                            new MySqlParameter("_PROPERTY_ID", ir["property_id"]),
                            new MySqlParameter("_CONTRACTSERVICE_ID", ir["contractservice_id"]),
                            new MySqlParameter("_SERVICE_DESCRIPTION", ir["comments"]),
                            new MySqlParameter("_MONTH", ir["month"]),
                            new MySqlParameter("_INVOICEREQUIREMENT_ID", ir["id"]),
                            new MySqlParameter("_INVOICE_ID", predicted_income_expense["invoice_id"]),
                            new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", predicted_income_expense["contract_service_additional_cost_id"]),
                            new MySqlParameter("_STATUS_ID", predicted_income_expense["status_id"]),
                            new MySqlParameter("_BANK_ACCOUNT_DETAILS", predicted_income_expense["bank_account_details"]),
                            new MySqlParameter("_AMOUNT_PAID", predicted_income_expense["amount_paid"]),
                            new MySqlParameter("_AMOUNT_PAID_RON", predicted_income_expense["amount_paid_ron"]),
                            new MySqlParameter("_BALLANCE", ir["price"])
                        });
                    da.ExecuteUpdateQuery();
                    //dataGrid1.toolStripButtonRefresh.PerformClick();
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
                            object predicted_income_expense_id = (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_get_by_ir_id", new object[]{new MySqlParameter("_INVOICEREQUIREMENT_ID", key)})).ExecuteScalarQuery();
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_delete", new object[] { new MySqlParameter("_ID", predicted_income_expense_id) });
                            da.ExecuteNonQuery();
                            
                            object company_predicted_income_expense_id = (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_get_by_ir_id", new object[]{new MySqlParameter("_INVOICEREQUIREMENT_ID", key)})).ExecuteScalarQuery();
                            da = new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_delete", new object[] { new MySqlParameter("_ID", company_predicted_income_expense_id) });
                            da.ExecuteNonQuery();

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
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
