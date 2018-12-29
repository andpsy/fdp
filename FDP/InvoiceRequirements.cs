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
    public partial class InvoiceRequirements : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();
        string month = "";

        public InvoiceRequirements()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
        }

        public InvoiceRequirements(int id)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            DataRow dr = (new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_get_by_id", new object[] { new MySqlParameter("_ID", id) })).ExecuteSelectQuery().Tables[0].Rows[0];
            NewDR = dr;
            InitializeComponent();
        }
        
        public InvoiceRequirements(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            NewDR = dr;
            InitializeComponent();
        }

        private void InvoiceRequirements_Load(object sender, EventArgs e)
        {
            FillCombos();
            if (NewDR != null)
            {
                FillInfo();
                if (NewDR["status_id"].ToString() == (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Invoiced"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery().ToString())
                    buttonSave.Enabled = false;
            }
            else
            {
                dateTimePickerDate.Value = DateTime.Now; 
            }
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
        }

        private void FillCombos()
        {
            DataAccess da = new DataAccess();

            da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_list");
            DataTable dtOwners = da.ExecuteSelectQuery().Tables[0];
            if (dtOwners != null)
            {
                comboBoxOwner.DisplayMember = "name";
                comboBoxOwner.ValueMember = "id";
                comboBoxOwner.DataSource = dtOwners;
            }

            da = new DataAccess(CommandType.StoredProcedure, "SERVICESsp_list");
            DataTable dtServices = da.ExecuteSelectQuery().Tables[0];
            if (dtServices != null)
            {
                comboBoxService.DisplayMember = "name";
                comboBoxService.ValueMember = "id";
                comboBoxService.DataSource = dtServices;
            }

            da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_currencies_list");
            DataTable dtc1 = da.ExecuteSelectQuery().Tables[0];
            if (dtc1 != null)
            {
                comboBoxCurrency.DisplayMember = "currency";
                comboBoxCurrency.ValueMember = "currency";
                comboBoxCurrency.DataSource = dtc1;
            }
        }


        private void FillInfo()
        {
            try
            {
                if (NewDR != null && NewDR.RowState != DataRowState.Added)
                {
                    //comboBoxOwner.SelectedValue = NewDR["owner_id"];
                    comboBoxService.SelectedValue = NewDR["contractservice_id"];
                    try{
                        dateTimePickerDate.Value = Convert.ToDateTime(NewDR["date"]);
                    }
                    catch { dateTimePickerDate.Value = DateTime.Now; }
                    userTextBoxPrice.Text = NewDR["price"].ToString();
                    userTextBoxComments.Text = NewDR["comments"].ToString();
                    try { FillContracts(Convert.ToInt32(NewDR["contract_id"])); }
                    catch { }
                    comboBoxCurrency.SelectedValue = NewDR["currency"];
                    checkBoxNotInvoiceable.Checked = NewDR["not_invoiceable"] == DBNull.Value ? false : Convert.ToBoolean(NewDR["not_invoiceable"]);
                    month = Convert.ToString(NewDR["month"]);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void FillContracts(int contract_id)
        {
            comboBoxOwner.SelectedValue = NewDR["owner_id"];
            foreach (DataGridViewRow dgvr in dataGridViewContracts.Rows)
            {
                if (dgvr.Cells["id"].Value.ToString() == contract_id.ToString())
                {
                    dgvr.Selected = true;
                    FillProperties(Convert.ToInt32(NewDR["property_id"]));
                    break;
                }
            }
        }

        private void FillProperties(int property_id)
        {
            /*
            foreach (DataGridViewRow dgvr in dataGridViewContracts.Rows)
            {
                if (dgvr.Cells["id"].Value.ToString() == contract_id.ToString())
                {
                    dgvr.Selected = true;
                    break;
                }
            }
            */
            foreach (DataGridViewRow dgvr in dataGridViewProperties.Rows)
            {
                if (dgvr.Cells["id"].Value.ToString() == property_id.ToString())
                {
                    dgvr.Selected = true;
                    break;
                }
            }
        }


        public void GenerateMySqlParameters()
        {
            try
            {
                MySqlParameters.Clear();
                if (NewDR != null)
                {
                    NewDR["owner_id"] = comboBoxOwner.SelectedValue;
                    NewDR["property_id"] = dataGridViewProperties.SelectedRows[0].Cells["id"].Value;
                    NewDR["contractservice_id"] = comboBoxService.SelectedValue;
                    NewDR["contract_id"] = dataGridViewContracts.SelectedRows[0].Cells["id"].Value;
                    NewDR["rentcontract_id"] = DBNull.Value;
                    NewDR["date"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerDate.Value);
                    NewDR["month"] = month;
                    NewDR["price"] = userTextBoxPrice.Text;
                    NewDR["status_id"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status") })).ExecuteScalarQuery();
                    NewDR["comments"] = userTextBoxComments.Text;
                    NewDR["currency"] = comboBoxCurrency.SelectedValue;
                    NewDR["not_invoiceable"] = checkBoxNotInvoiceable.Checked;
                }
                else
                {
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _OWNER_ID = new MySqlParameter("_OWNER_ID", CommonFunctions.SetNullable(comboBoxOwner));
                    MySqlParameters.Add(_OWNER_ID);

                    MySqlParameter _PROPERTY_ID = new MySqlParameter("_PROPERTY_ID", dataGridViewProperties.SelectedRows[0].Cells["id"].Value);
                    MySqlParameters.Add(_PROPERTY_ID);

                    MySqlParameter _CONTRACTSERVICE_ID = new MySqlParameter("_CONTRACTSERVICE_ID", CommonFunctions.SetNullable(comboBoxService));
                    MySqlParameters.Add(_CONTRACTSERVICE_ID);

                    MySqlParameter _CONTRACT_ID = new MySqlParameter("_CONTRACT_ID", DBNull.Value);
                    MySqlParameters.Add(_CONTRACT_ID);

                    MySqlParameter _RENTCONTRACT_ID = new MySqlParameter("_RENTCONTRACT_ID", DBNull.Value);
                    MySqlParameters.Add(_RENTCONTRACT_ID);

                    MySqlParameter _DATE = new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerDate.Value));
                    MySqlParameters.Add(_DATE);

                    MySqlParameter _MONTH = new MySqlParameter("_MONTH", month);
                    MySqlParameters.Add(_MONTH);

                    MySqlParameter _PRICE = new MySqlParameter("_PRICE", userTextBoxPrice.Text);
                    MySqlParameters.Add(_PRICE);

                    MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[]{new MySqlParameter("_NAME", "To be processed"), new MySqlParameter("_LIST_TYPE", "invoicerequirement_status")})).ExecuteScalarQuery());
                    MySqlParameters.Add(_STATUS_ID);

                    MySqlParameter _COMMENTS = new MySqlParameter("_COMMENTS", userTextBoxComments.Text);
                    MySqlParameters.Add(_COMMENTS);

                    MySqlParameter _CURRENCY = new MySqlParameter("_CURRENCY", comboBoxCurrency.SelectedValue);
                    MySqlParameters.Add(_CURRENCY);

                    MySqlParameter _NOT_INVOICEABLE = new MySqlParameter("_NOT_INVOICEABLE", checkBoxNotInvoiceable.Checked);
                    MySqlParameters.Add(_NOT_INVOICEABLE);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "INVOICEREQUIREMENTSsp_insert", MySqlParameters.ToArray());
                    //da.ExecuteInsertQuery();
                    DataRow new_ir = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    /*
                    da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_insert", new object[]{
                        new MySqlParameter("_TYPE", false),
                        new MySqlParameter("_CURRENCY", new_ir["currency"]),
                        new MySqlParameter("_AMOUNT", new_ir["price"]),
                        new MySqlParameter("_DATE", new_ir["date"]),
                        new MySqlParameter("_OWNER_ID", new_ir["owner_id"]),
                        new MySqlParameter("_PROPERTY_ID", new_ir["property_id"]),
                        new MySqlParameter("_CONTRACTSERVICE_ID", new_ir["contractservice_id"]),
                        new MySqlParameter("_SERVICE_DESCRIPTION", new_ir["comments"]),
                        new MySqlParameter("_MONTH", new_ir["month"]),
                        new MySqlParameter("_INVOICEREQUIREMENT_ID", new_ir["id"])
                    });
                    da.ExecuteInsertQuery();
                    */
                    IncomeExpensesClass.InsertIE(false, false, DBNull.Value, new_ir, false);
                    IncomeExpensesClass.InsertIE(true, true, DBNull.Value, new_ir, false);

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
            errorProvider1.SetError(userTextBoxPrice, "");
            errorProvider1.SetError(comboBoxOwner, "");
            errorProvider1.SetError(comboBoxService, "");
            errorProvider1.SetError(dataGridViewProperties, "");

            if (comboBoxOwner.SelectedValue == null || comboBoxOwner.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxOwner, Language.GetErrorText("errorEmptyOwner", "You must select the owner!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwner.Name, Language.GetErrorText("errorEmptyOwner", "You must select the owner!")));
                toReturn = false;
            }
            
            if (comboBoxService.SelectedValue == null || comboBoxService.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxService, Language.GetErrorText("errorEmptyService", "You must select the service!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxService.Name, Language.GetErrorText("errorEmptyService", "You must select the service!")));
                toReturn = false;
            }
            
            if (dataGridViewProperties.SelectedRows.Count <= 0)
            {
                errorProvider1.SetError(dataGridViewProperties, Language.GetErrorText("errorEmptyProperty", "You must select the property!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(dataGridViewProperties.Name, Language.GetErrorText("errorEmptyProperty", "You must select the property!")));
                toReturn = false;
            }
            if (!Validator.IsNumeric(userTextBoxPrice.Text.Trim()))
            {
                errorProvider1.SetError(userTextBoxPrice, Language.GetErrorText("errorInvalidPrice", "Invalid Price value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxPrice.Name, Language.GetErrorText("errorInvalidPrice", "Invalid Price value!")));
                toReturn = false;
            }
            return toReturn;
        }

        private void pictureBoxSelectOwner_Click(object sender, EventArgs e)
        {
            var f = new OwnerSelect(true);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxOwner.SelectedValue = f.IdToReturn;
            }
            f.Dispose();
        }

        private void comboBoxOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedIndex > 0)
                {
                    DataRowView drv = (DataRowView)((ComboBox)sender).SelectedItem;
                    int _owner_id = Convert.ToInt32(drv.Row["id"]);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", _owner_id) });
                    DataTable dtContracts = da.ExecuteSelectQuery().Tables[0];
                    if (dtContracts != null)
                    {
                        dataGridViewContracts.DataSource = dtContracts;
                        foreach (DataGridViewColumn dc in dataGridViewContracts.Columns)
                        {
                            dc.Visible = (dc.Name.ToLower() == "number") ? true : false;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void dataGridViewContracts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int _contract_id = Convert.ToInt32(((DataGridView)sender).Rows[e.RowIndex].Cells["id"].Value);
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", _contract_id) });
                DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
                if (dtProperties != null)
                {
                    dataGridViewProperties.DataSource = dtProperties;
                    foreach (DataGridViewColumn dc in dataGridViewProperties.Columns)
                    {
                        dc.Visible = (dc.Name.ToLower() == "name") ? true : false;
                    }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
            
        }
    }
}
