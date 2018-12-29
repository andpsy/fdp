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
    public partial class PredictedIncomeExpenses : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();

        public PredictedIncomeExpenses()
        {
            base.CheckDataOnClosing = true;
            InitializeComponent();
        }

        public PredictedIncomeExpenses(DataRow dr)
        {
            base.CheckDataOnClosing = true;
            NewDR = dr;
            InitializeComponent();
        }

        private void PredictedIncomeExpenses_Load(object sender, EventArgs e)
        {
            FillCombos();
            if (NewDR != null)
            {
                FillInfo();
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

            comboBoxType.SelectedIndex = 0;
        }


        private void FillInfo()
        {
            try
            {
                if (NewDR != null && NewDR.RowState != DataRowState.Added)
                {
                    comboBoxType.SelectedItem = NewDR["type"].ToString();
                    comboBoxService.SelectedValue = NewDR["contractservice_id"];
                    try{
                        dateTimePickerDate.Value = Convert.ToDateTime(NewDR["date"]);
                    }
                    catch { dateTimePickerDate.Value = DateTime.Now; }
                    userTextBoxPrice.Text = NewDR["amount"].ToString();
                    userTextBoxComments.Text = NewDR["service_description"].ToString();
                    comboBoxCurrency.SelectedValue = NewDR["currency"];
                    comboBoxOwner.SelectedValue = NewDR["owner_id"];
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        public void GenerateMySqlParameters()
        {
            try
            {
                MySqlParameters.Clear();
                if (NewDR != null)
                {
                    NewDR["type"] = (comboBoxType.SelectedItem.ToString() == "EXPENSE" ? false : true);
                    NewDR["owner_id"] = comboBoxOwner.SelectedValue;
                    NewDR["property_id"] = dataGridViewProperties.SelectedRows[0].Cells["id"].Value;
                    NewDR["contractservice_id"] = comboBoxService.SelectedValue;
                    NewDR["date"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerDate.Value);
                    NewDR["month"] = DBNull.Value;
                    NewDR["amount"] = userTextBoxPrice.Text;
                    NewDR["service_description"] = userTextBoxComments.Text;
                    NewDR["currency"] = comboBoxCurrency.SelectedValue;
                }
                else
                {
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }

                    MySqlParameter _TYPE = new MySqlParameter("_TYPE", (comboBoxType.SelectedItem.ToString() == "EXPENSE" ? false : true));
                    MySqlParameters.Add(_TYPE);

                    MySqlParameter _OWNER_ID = new MySqlParameter("_OWNER_ID", CommonFunctions.SetNullable(comboBoxOwner));
                    MySqlParameters.Add(_OWNER_ID);

                    MySqlParameter _PROPERTY_ID = new MySqlParameter("_PROPERTY_ID", dataGridViewProperties.SelectedRows[0].Cells["id"].Value);
                    MySqlParameters.Add(_PROPERTY_ID);

                    MySqlParameter _CONTRACTSERVICE_ID = new MySqlParameter("_CONTRACTSERVICE_ID", CommonFunctions.SetNullable(comboBoxService));
                    MySqlParameters.Add(_CONTRACTSERVICE_ID);

                    MySqlParameter _DATE = new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerDate.Value));
                    MySqlParameters.Add(_DATE);

                    MySqlParameter _MONTH = new MySqlParameter("_MONTH", DBNull.Value);
                    MySqlParameters.Add(_MONTH);

                    MySqlParameter _AMOUNT = new MySqlParameter("_AMOUNT", userTextBoxPrice.Text);
                    MySqlParameters.Add(_AMOUNT);

                    MySqlParameter _SERVICE_DESCRIPTION = new MySqlParameter("_SERVICE_DESCRIPTION", userTextBoxComments.Text);
                    MySqlParameters.Add(_SERVICE_DESCRIPTION);

                    MySqlParameter _CURRENCY = new MySqlParameter("_CURRENCY", comboBoxCurrency.SelectedValue);
                    MySqlParameters.Add(_CURRENCY);
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_insert", MySqlParameters.ToArray());
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", _owner_id) });
                    DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
                    if (dtProperties != null)
                    {
                        dataGridViewProperties.DataSource = dtProperties;
                        dataGridViewProperties.Columns.Remove("assigned");
                        DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
                        c.Name = "Assigned";
                        c.Width = 30;
                        c.HeaderText = "";
                        c.DataPropertyName = "assigned";
                        dataGridViewProperties.Columns.Insert(0, c);
                        foreach (DataGridViewColumn dc in dataGridViewProperties.Columns)
                        {
                            dc.ReadOnly = dc.Name.ToLower() == "assigned" ? false : true;
                            dc.Visible = (dc.Name.ToLower() == "name" || dc.Name.ToLower() == "assigned") ? true : false;
                        }
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
