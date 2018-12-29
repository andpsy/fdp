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
    public partial class PredictedIncomeExpenseSelect : UserForm
    {
        //this.dataGrid1 = new FDP.DataGrid("PREDICTED_INCOME_EXPENSESsp_select", null, "PREDICTED_INCOME_EXPENSESsp_insert", null, "PREDICTED_INCOME_EXPENSESsp_update", null, "PREDICTED_INCOME_EXPENSESsp_delete", null, new string[] { "DATE" }, new string[] { "AMOUNT" }, null, new string[]{ "STATUS" }, new string[] { "OWNER", "PROPERTY" }, new string[] {"TYPE", "OWNER", "PROPERTY", "AMOUNT", "CURRENCY", "MONTH", "DATE", "SERVICE", "SERVICE_DESCRIPTION" }, this.Selectable, false);
        public bool Selectable = false;

        public PredictedIncomeExpenseSelect()
        {
            try
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
            catch (Exception exp) { exp.ToString(); }
        }

        public PredictedIncomeExpenseSelect(bool selectable)
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

        private void PredictedIncomeExpenseSelect_Load(object sender, EventArgs e)
        {
        }
        
        public void buttonAdd_Click(object sender, EventArgs e)
        {
            DataRow pie = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new PredictedIncomeExpenses(pie);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    pie = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(pie.Table, pie.ItemArray, 0);
                    for (int i = 0; i < mySqlParams.Length; i++)
                    {
                        if (((MySqlParameter)mySqlParams[i]).ParameterName == "_TYPE")
                        {
                            ((MySqlParameter)mySqlParams[i]).DbType = DbType.Boolean;
                            ((MySqlParameter)mySqlParams[i]).Value = Convert.ToBoolean(((MySqlParameter)mySqlParams[i]).Value.ToString().ToLower());
                            break;
                        }
                    }
                    try
                    {
                        pie["type"] = ((pie["type"] == DBNull.Value || Convert.ToBoolean(pie["type"]) == false) ? "EXPENSE" : "INCOME");
                    }
                    catch { }
                    try
                    {
                        pie["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        pie["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    /*
                    try
                    {
                        pie["service"] = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["contractservice_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    */

                    ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(pie);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    //int pie_id = Convert.ToInt32(((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).Select("id = max(id)")[0]["id"]);
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
            if (dataGrid1.dataGridView.SelectedRows.Count > 0)
            {
                DataRow pie = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
                var f = new PredictedIncomeExpenses(pie);
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        pie = f.NewDR;
                        object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(pie.Table, pie.ItemArray, 1);
                        for (int i = 0; i < mySqlParams.Length; i++)
                        {
                            if (((MySqlParameter)mySqlParams[i]).ParameterName == "_TYPE")
                            {
                                ((MySqlParameter)mySqlParams[i]).DbType = DbType.Boolean;
                                ((MySqlParameter)mySqlParams[i]).Value = Convert.ToBoolean(((MySqlParameter)mySqlParams[i]).Value.ToString().ToLower());
                                break;
                            }
                        }
                        try
                        {
                            pie["type"] = ((pie["type"] == DBNull.Value || Convert.ToBoolean(pie["type"]) == false) ? "EXPENSE" : "INCOME");
                        }
                        catch { }
                        try
                        {
                            pie["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["owner_id"]) })).ExecuteScalarQuery().ToString();
                        }
                        catch { }
                        try
                        {
                            pie["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["property_id"]) })).ExecuteScalarQuery().ToString();
                        }
                        catch { }
                        /*
                        try
                        {
                            pie["service"] = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", pie["contractservice_id"]) })).ExecuteScalarQuery().ToString();
                        }
                        catch { }
                        */
                        dataGrid1.da.AttachUpdateParams(mySqlParams);
                        dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
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
