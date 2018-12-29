using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FDP
{
    public partial class IncomeExpenseSelect : UserForm
    {
        //this.dataGrid1 = new FDP.DataGrid("INCOME_EXPENSESsp_select", null, "INCOME_EXPENSESsp_insert", null, "INCOME_EXPENSESsp_update", null, "INCOME_EXPENSESsp_delete", null, new string[] { "DATE" }, new string[] { "AMOUNT" }, null, null, new string[] { "OWNER", "PROPERTY", "INVOICE" }, new string[] { "TYPE", "OWNER", "PROPERTY", "AMOUNT", "CURRENCY", "MONTH", "DATE", "SERVICE", "SERVICE_DESCRIPTION", "INVOICE", "STATUS", "AMOUNT_PAID", "AMOUNT_PAID_RON", "BALLANCE", "SOURCE", "BANK_ACCOUNT_DETAILS", "ORIGINAL AMOUNT (EUR)", "ORIGINAL AMOUNT (RON)", "AMOUNT PAID (EUR)", "AMOUNT PAID (RON)", "BALLANCE (EUR)", "BALLANCE (RON)" }, this.Selectable, false);
        public bool Selectable = false;

        public IncomeExpenseSelect()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.dataGridView.BindingContextChanged += new EventHandler(dataGridView_BindingContextChanged);
            dataGrid1.dataGridView.Sorted += new EventHandler(dataGridView_Sorted);
        }

        public IncomeExpenseSelect(bool selectable)
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
            dataGrid1.dataGridView.BindingContextChanged += new EventHandler(dataGridView_BindingContextChanged);
            dataGrid1.dataGridView.Sorted += new EventHandler(dataGridView_Sorted);
        }

        private void IncomeExpenseSelect_Load(object sender, EventArgs e)
        {
        }

        private void dataGridView_BindingContextChanged(object sender, EventArgs e)
        {
            //AddExchangedColumns();
        }

        private void dataGridView_Sorted(object sender, EventArgs e)
        {
            //AddExchangedColumns();
        }

        #region --- au vrut in 04.03.13 si in 07.03.13 nu au mai vrut ---
        private void AddExchangedColumns()
        {
            try
            {
                DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvc.DefaultCellStyle.Format = SettingsClass.DoubleFormat;
                dgvc.Name = "AMOUNT_EXCHANGED";
                dgvc.HeaderText = String.Format("{0} (RON)", Language.GetColumnHeaderText(dgvc.Name, "ORIGINAL AMOUNT EXCHANGED"));
                dataGrid1.dataGridView.Columns.Insert(dataGrid1.dataGridView.Columns["AMOUNT"].Index + 1, dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvc.DefaultCellStyle.Format = SettingsClass.DoubleFormat;
                dgvc.Name = "AMOUNT_PAID_EXCHANGED";
                dgvc.HeaderText = String.Format("{0} (RON)", Language.GetColumnHeaderText(dgvc.Name, "AMOUNT PAID EXCHANGED"));
                dataGrid1.dataGridView.Columns.Insert(dataGrid1.dataGridView.Columns["AMOUNT_PAID"].Index + 1, dgvc);

                dgvc = new DataGridViewTextBoxColumn();
                dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvc.DefaultCellStyle.Format = SettingsClass.DoubleFormat;
                dgvc.Name = "BALLANCE_EXCHANGED";
                dgvc.HeaderText = String.Format("{0} (RON)", Language.GetColumnHeaderText(dgvc.Name, "BALLANCE EXCHANGED"));
                dataGrid1.dataGridView.Columns.Insert(dataGrid1.dataGridView.Columns["BALLANCE"].Index + 1, dgvc);

                foreach (DataGridViewRow dgvr in dataGrid1.dataGridView.Rows)
                {
                    if (dataGrid1.dataGridView["CURRENCY", dgvr.Index].Value.ToString() != "RON")
                    {
                        try
                        {
                            double amount_exchanged = Math.Round(CommonFunctions.ConvertCurrency(Convert.ToDouble(dataGrid1.dataGridView["AMOUNT", dgvr.Index].Value), dataGrid1.dataGridView["CURRENCY", dgvr.Index].Value.ToString().ToLower(), "ron", CommonFunctions.SwitchBackFormatedDate(dataGrid1.dataGridView["DATE", dgvr.Index].Value.ToString())), SettingsClass.RoundingDigits);
                            dataGrid1.dataGridView["AMOUNT_EXCHANGED", dgvr.Index].Value = amount_exchanged;
                        }
                        catch { }
                        try
                        {
                            double amount_paid_exchanged = Math.Round(CommonFunctions.ConvertCurrency(Convert.ToDouble(dataGrid1.dataGridView["AMOUNT_PAID", dgvr.Index].Value), dataGrid1.dataGridView["CURRENCY", dgvr.Index].Value.ToString().ToLower(), "ron", CommonFunctions.SwitchBackFormatedDate(dataGrid1.dataGridView["DATE", dgvr.Index].Value.ToString())), SettingsClass.RoundingDigits);
                            dataGrid1.dataGridView["AMOUNT_PAID_EXCHANGED", dgvr.Index].Value = amount_paid_exchanged;
                        }
                        catch { }
                        try
                        {
                            double ballance_exchanged = Math.Round(CommonFunctions.ConvertCurrency(Convert.ToDouble(dataGrid1.dataGridView["BALLANCE", dgvr.Index].Value), dataGrid1.dataGridView["CURRENCY", dgvr.Index].Value.ToString().ToLower(), "ron", CommonFunctions.SwitchBackFormatedDate(dataGrid1.dataGridView["DATE", dgvr.Index].Value.ToString())), SettingsClass.RoundingDigits);
                            dataGrid1.dataGridView["BALLANCE_EXCHANGED", dgvr.Index].Value = ballance_exchanged;
                        }
                        catch { }
                    }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }
        #endregion

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow pie = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new IncomeExpenses(pie);
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
            IncomeExpenses f = (IncomeExpenses)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                if (f.userTextBoxPeriods.Text.Trim() != "" && f.comboBoxPeriod.SelectedIndex > 0)
                {
                    dataGrid1.toolStripButtonRefresh.PerformClick();
                }
                else
                {
                    try
                    {
                        DataRow pie = f.NewDR;
                        object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(pie.Table, pie.ItemArray, 0);
                        /*
                        try
                        {
                            f.NewDR["amount"] = pie["amount"] = Convert.ToDouble(pie["amount"]) * ((pie["type"] == DBNull.Value || Convert.ToBoolean(pie["type"]) == false) ? -1 : 1);
                        }
                        catch { }
                        try
                        {
                            f.NewDR["type"] = pie["type"] = ((pie["type"] == DBNull.Value || pie["type"] == null || Convert.ToBoolean(pie["type"]) == false) ? "EXPENSE" : "INCOME");
                        }
                        catch { }
                        try
                        {
                            f.NewDR["owner"] = pie["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["owner_id"]) })).ExecuteScalarQuery().ToString();
                        }
                        catch { }
                        try
                        {
                            f.NewDR["property"] = pie["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["property_id"]) })).ExecuteScalarQuery().ToString();
                        }
                        catch { }

                        try
                        {
                            f.NewDR["service"] = pie["service"] = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["contractservice_id"]) })).ExecuteScalarQuery().ToString();
                        }
                        catch { }

                        try
                        {
                            f.NewDR["status"] = pie["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", pie["status_id"]) })).ExecuteScalarQuery().ToString();
                        }
                        catch { }
                        */
                        for (int i = 0; i < mySqlParams.Length; i++)
                        {
                            if (((MySqlParameter)mySqlParams[i]).ParameterName == "_TYPE")
                            {
                                try
                                {
                                    ((MySqlParameter)mySqlParams[i]).DbType = DbType.Boolean;
                                    //((MySqlParameter)mySqlParams[i]).Value = Convert.ToBoolean(((MySqlParameter)mySqlParams[i]).Value.ToString().ToLower());
                                    ((MySqlParameter)mySqlParams[i]).Value = Convert.ToBoolean(((MySqlParameter)mySqlParams[i]).Value == DBNull.Value || ((MySqlParameter)mySqlParams[i]).Value == null || ((MySqlParameter)mySqlParams[i]).Value.ToString().ToUpper() == "EXPENSE" ? "false" : ((MySqlParameter)mySqlParams[i]).Value.ToString().ToUpper() == "INCOME" ? "true" : ((MySqlParameter)mySqlParams[i]).Value.ToString().ToLower());
                                    break;
                                }
                                catch { }
                            }
                        }
                        /*
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(pie);
                        dataGrid1.da.AttachInsertParams(mySqlParams);
                        dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                        */
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_insert", mySqlParams);
                        da.ExecuteInsertQuery();
                        //dataGrid1.dataGridView.EndEdit();
                        //int pie_id = Convert.ToInt32(((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).Select("id = max(id)")[0]["id"]);                        
                        //dataGrid1.toolStripButtonRefresh.PerformClick();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                DataRow pie = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
                var f = new IncomeExpenses(pie);
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
            IncomeExpenses f = (IncomeExpenses)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow pie = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(pie.Table, pie.ItemArray, 1);
                    /*
                    try
                    {
                        f.NewDR["amount"] = pie["amount"] = Convert.ToDouble(pie["amount"]) * ((pie["type"] == DBNull.Value || Convert.ToBoolean(pie["type"]) == false) ? -1 : 1);
                    }
                    catch { }
                    try
                    {
                        f.NewDR["type"] = pie["type"] = ((pie["type"] == DBNull.Value || pie["type"] == null || Convert.ToBoolean(pie["type"]) == false) ? "EXPENSE" : "INCOME");
                    }
                    catch { }
                    try
                    {
                        f.NewDR["owner"] = pie["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        f.NewDR["property"] = pie["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }

                    try
                    {
                        f.NewDR["service"] = pie["service"] = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["contractservice_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }

                    try
                    {
                        f.NewDR["status"] = pie["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", pie["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    */
                    for (int i = 0; i < mySqlParams.Length; i++)
                    {
                        if (((MySqlParameter)mySqlParams[i]).ParameterName == "_TYPE")
                        {
                            ((MySqlParameter)mySqlParams[i]).DbType = DbType.Boolean;
                            //((MySqlParameter)mySqlParams[i]).Value = Convert.ToBoolean(((MySqlParameter)mySqlParams[i]).Value.ToString().ToLower());
                            ((MySqlParameter)mySqlParams[i]).Value = Convert.ToBoolean(((MySqlParameter)mySqlParams[i]).Value == DBNull.Value || ((MySqlParameter)mySqlParams[i]).Value == null || ((MySqlParameter)mySqlParams[i]).Value.ToString().ToUpper() == "EXPENSE" ? "false" : ((MySqlParameter)mySqlParams[i]).Value.ToString().ToUpper() == "INCOME" ? "true" : ((MySqlParameter)mySqlParams[i]).Value.ToString().ToLower());
                            break;
                        }
                    }
                    /* --- DIN 10.03.13 - FROM MODIFICATION OF AMOUNT(S) COLUMNS 
                    dataGrid1.da.AttachUpdateParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    */
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_update", mySqlParams);
                    da.ExecuteUpdateQuery();

                    //dataGrid1.dataGridView.EndEdit();                    
                    //dataGrid1.toolStripButtonRefresh.PerformClick();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).RejectChanges();
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
