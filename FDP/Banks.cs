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
    public partial class Banks : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();
        public DataAccess daAgencies = new DataAccess();

        public Banks()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
        }

        public Banks(int id)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            DataAccess da = new DataAccess();
            InitializeComponent();
            //Language.LoadLabels(this);
            da = new DataAccess(CommandType.StoredProcedure, "BANKSsp_get_by_id", new object[] { new MySqlParameter("_ID", id) });
            NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
            //this.dataGridContractsServices = new DataGrid("CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", NewDR["id"]) }, "CONTRACTS_PROPERTIES_SERVICESsp_insert", null, "CONTRACTS_PROPERTIES_SERVICESsp_update", null, "CONTRACTS_PROPERTIES_SERVICESsp_delete", null, null, null, null, null, null, new string[] { "SERVICE", "PRICE_VALUE", "PRICE_PERCENT", "PRICE_ONE_PAYMENT", "PRICE_VALUE_APPLICABLE", "PRICE_PERCENT_APPLICABLE", "PRICE_ONE_PAYMENT_APPLICABLE" }, false);
            buttonSaveContract.Enabled = false;
        }

        public Banks(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
            NewDR = dr;
            //this.dataGridContractsServices = new DataGrid("CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", NewDR["id"]) }, "CONTRACTS_PROPERTIES_SERVICESsp_insert", null, "CONTRACTS_PROPERTIES_SERVICESsp_update", null, "CONTRACTS_PROPERTIES_SERVICESsp_delete", null, null, null, null, null, null, new string[] { "SERVICE", "PRICE_VALUE", "PRICE_PERCENT", "PRICE_ONE_PAYMENT", "PRICE_VALUE_APPLICABLE", "PRICE_PERCENT_APPLICABLE", "PRICE_ONE_PAYMENT_APPLICABLE" }, false);
        }

        private void Banks_Load(object sender, EventArgs e)
        {
            if (NewDR != null)
            {
                FillInfo();
            }
            dataGridViewBankAgencies.Enabled = buttonSaveAgencies.Enabled = !(NewDR == null || NewDR.RowState == DataRowState.Added || NewDR.RowState == DataRowState.Detached);
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
        }

        private void FillInfo()
        {
            try
            {
                //if (NewDR.RowState != DataRowState.Added && NewDR != null)
                if (NewDR != null)
                {
                    userTextBoxBankName.Text = NewDR["name"].ToString();
                    userTextBoxComments.Text = NewDR["details"].ToString();
                    try { FillAgencies(NewDR["id"]==DBNull.Value?-1:Convert.ToInt32(NewDR["id"])); }
                    catch (Exception exp) { exp.ToString(); }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void FillAgencies(int bank_id)
        {
            //DataAccess daAgencies = new DataAccess(CommandType.StoredProcedure, "BANKSAGENCIESsp_get_by_bank_id", new object[]{new MySqlParameter("_BANK_ID", bank_id)});
            //DataTable dt = daAgencies.ExecuteSelectQuery().Tables[0];
            daAgencies = new DataAccess("BANKSAGENCIESsp_get_by_bank_id", new object[] { new MySqlParameter("_BANK_ID", bank_id) }, "BANKSAGENCIESsp_insert", null, "BANKSAGENCIESsp_update", null, "BANKSAGENCIESsp_delete", null);
            //daAgencies.AttachSelectParams(new object[] { new MySqlParameter("_BANK_ID", bank_id) });
            dataGridViewBankAgencies.DataSource = daAgencies.bindingSource;
            dataGridViewBankAgencies.Columns["id"].Visible = false;
            dataGridViewBankAgencies.Columns["bank_id"].Visible = false;
        }

        public void GenerateMySqlParameters()
        {
            try
            {
                if (NewDR != null)
                {
                    NewDR["name"] = userTextBoxBankName.Text;
                    NewDR["details"] = userTextBoxComments.Text;
                }
                else
                {
                    MySqlParameters.Clear();
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _NAME = new MySqlParameter("_NAME", userTextBoxBankName.Text); MySqlParameters.Add(_NAME);
                    MySqlParameter _DETAILS = new MySqlParameter("_DETAILS", userTextBoxComments.Text); MySqlParameters.Add(_DETAILS);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }



        private void buttonSaveContract_Click(object sender, EventArgs e)
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "BANKSsp_insert", MySqlParameters.ToArray());
                    da.ExecuteInsertQuery();
                    InitialDR = CommonFunctions.CopyDataRow(NewDR);
                    this.Close();
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateData()
        {
            bool toReturn = true;
            errorProvider1.SetError(userTextBoxBankName, "");

            if (userTextBoxBankName.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxBankName, Language.GetErrorText("errorEmptyBankName", "Bank name can not by empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxBankName.Name, Language.GetErrorText("errorEmptyBankName", "Bank name can not by empty!")));
                toReturn = false;
            }
            return toReturn;
        }

        private void buttonSaveAgencies_Click(object sender, EventArgs e)
        {
            ArrayList agenciesParams = new ArrayList();
            DataAccess da = new DataAccess();
            DataTable changes = ((DataTable)daAgencies.bindingSource.DataSource).GetChanges();
            string stored_procedure = "";
            foreach (DataRow dr in changes.Rows)
            {
                agenciesParams.Clear();
                switch (dr.RowState)
                {
                    case DataRowState.Deleted:
                        try
                        {
                            daAgencies.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dr["id"]) });
                            stored_procedure = "BANKSAGENCIESsp_delete";
                            da = new DataAccess(CommandType.StoredProcedure, stored_procedure, agenciesParams.ToArray());
                            da.ExecuteUpdateQuery();
                        }
                        catch (Exception exp)
                        {
                            LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                            MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case DataRowState.Added:
                        try
                        {
                            foreach (DataColumn dc in dr.Table.Columns)
                                if (dc.ColumnName.ToLower() != "id" && dc.ColumnName.ToLower() != "bank_id")
                                    agenciesParams.Add(new MySqlParameter(String.Format("_{0}", dc.ColumnName.ToUpper()), dr[dc.ColumnName]));
                            agenciesParams.Add(new MySqlParameter("_BANK_ID", NewDR["id"]));
                            daAgencies.AttachInsertParams(agenciesParams.ToArray());
                            stored_procedure = "BANKSAGENCIESsp_insert";
                            da = new DataAccess(CommandType.StoredProcedure, stored_procedure, agenciesParams.ToArray());
                            da.ExecuteInsertQuery();
                        }
                        catch (Exception exp)
                        {
                            LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                            MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case DataRowState.Modified:
                        try
                        {
                            foreach (DataColumn dc in dr.Table.Columns)
                                if (dc.ColumnName.ToLower() != "bank_id")
                                    agenciesParams.Add(new MySqlParameter(String.Format("_{0}", dc.ColumnName.ToUpper()), dr[dc.ColumnName]));
                            agenciesParams.Add(new MySqlParameter("_BANK_ID", NewDR["id"]));
                            daAgencies.AttachUpdateParams(agenciesParams.ToArray());
                            stored_procedure = "BANKSAGENCIESsp_update";
                            da = new DataAccess(CommandType.StoredProcedure, stored_procedure, agenciesParams.ToArray());
                            da.ExecuteUpdateQuery();
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        }
                        break;
                }
                ((DataTable)daAgencies.bindingSource.DataSource).AcceptChanges();
                //daAgencies.mySqlDataAdapter.Update(changes);
            }
        }
    }
}
