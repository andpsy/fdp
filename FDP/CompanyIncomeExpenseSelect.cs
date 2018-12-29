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
    public partial class CompanyIncomeExpenseSelect : UserForm
    {
        //this.dataGrid1 = new FDP.DataGrid("COMPANY_PREDICTED_INCOME_EXPENSESsp_select", null, "COMPANY_PREDICTED_INCOME_EXPENSESsp_insert", null, "COMPANY_PREDICTED_INCOME_EXPENSESsp_update", null, "COMPANY_PREDICTED_INCOME_EXPENSESsp_delete", null, new string[] { "DATE" }, new string[] { "AMOUNT" }, null, null, new string[] { "OWNER", "PROPERTY" }, new string[] { "TYPE", "OWNER", "PROPERTY", "AMOUNT", "CURRENCY", "MONTH", "DATE", "SERVICE", "SERVICE_DESCRIPTION" }, this.Selectable, false);
        public bool Selectable = false;

        public CompanyIncomeExpenseSelect()
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

        public CompanyIncomeExpenseSelect(bool selectable)
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

        private void CompanyIncomeExpenseSelect_Load(object sender, EventArgs e)
        {
        }
        
        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow pie = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new CompanyIncomeExpenses(pie);
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
            CompanyIncomeExpenses f = (CompanyIncomeExpenses)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
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
                            ((MySqlParameter)mySqlParams[i]).DbType = DbType.Boolean;
                            //((MySqlParameter)mySqlParams[i]).Value = Convert.ToBoolean(((MySqlParameter)mySqlParams[i]).Value.ToString().ToLower());
                            ((MySqlParameter)mySqlParams[i]).Value = Convert.ToBoolean(((MySqlParameter)mySqlParams[i]).Value == DBNull.Value || ((MySqlParameter)mySqlParams[i]).Value == null || ((MySqlParameter)mySqlParams[i]).Value.ToString().ToUpper() == "EXPENSE" ? "false" : ((MySqlParameter)mySqlParams[i]).Value.ToString().ToUpper() == "INCOME" ? "true" : ((MySqlParameter)mySqlParams[i]).Value.ToString().ToLower());
                            break;
                        }
                    }
                    /*
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(pie);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    */
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_insert", mySqlParams);
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
            DataRow pie = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
            var f = new CompanyIncomeExpenses(pie);
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

        public void SaveEditRecord()
        {
            CompanyIncomeExpenses f = (CompanyIncomeExpenses)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
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
                    dataGrid1.dataGridView.EndEdit();
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    */
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_update", mySqlParams);
                    da.ExecuteUpdateQuery();
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
                            try
                            {
                                //int key = Convert.ToInt32(dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value);
                                int key = Convert.ToInt32(dataGrid1.dataGridView["id", dgvr.Index].Value);
                                //dataGrid1.da.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value) });
                                dataGrid1.da.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGrid1.dataGridView["id", dgvr.Index].Value) });
                                DataRow dr = ((DataTable)dataGrid1.da.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                                dr.Delete();
                                dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                            }
                            catch (Exception exp) {
                                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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
