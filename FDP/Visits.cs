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
    public partial class Visits : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();

        public Visits()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
        }

        public Visits(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            NewDR = dr;
            InitializeComponent();
            buttonAssetConditions.Enabled = true;
        }

        private void Visits_Load(object sender, EventArgs e)
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

            da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_list");
            DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
            if (dtProperties != null)
            {
                comboBoxProperties.DisplayMember = "name";
                comboBoxProperties.ValueMember = "id";
                comboBoxProperties.DataSource = dtProperties;
            }

            da = new DataAccess(CommandType.StoredProcedure, "VISITREASONSsp_list");
            DataTable dtReasons = da.ExecuteSelectQuery().Tables[0];
            if (dtReasons != null)
            {
                comboBoxReason.DisplayMember = "name";
                comboBoxReason.ValueMember = "id";
                comboBoxReason.DataSource = dtReasons;
            }

            da = new DataAccess(CommandType.StoredProcedure, "EMPLOYEESsp_list");
            DataTable dtEmployees = da.ExecuteSelectQuery().Tables[0];
            if (dtEmployees != null)
            {
                comboBoxEmployee.DisplayMember = "name";
                comboBoxEmployee.ValueMember = "id";
                comboBoxEmployee.DataSource = dtEmployees;
            }

            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "PAYERS_TYPE") });
            DataTable dtPayers = da.ExecuteSelectQuery().Tables[0];
            if (dtPayers != null)
            {
                comboBoxPayer.DisplayMember = "name";
                comboBoxPayer.ValueMember = "id";
                comboBoxPayer.DataSource = dtPayers;
            }
        }


        private void FillInfo()
        {
            try
            {
                if (NewDR != null && NewDR.RowState != DataRowState.Added)
                {
                    try { comboBoxProperties.SelectedValue = NewDR["property_id"]; }
                    catch { }
                    try { comboBoxReason.SelectedValue = NewDR["visitreason_id"]; }
                    catch { }
                    try { comboBoxEmployee.SelectedValue = NewDR["employee_id"]; }
                    catch { }
                    try
                    {
                        dateTimePickerDate.Value = Convert.ToDateTime(NewDR["date"]);
                    }
                    catch { dateTimePickerDate.Value = DateTime.Now; }
                    userTextBoxComments.Text = NewDR["comments"].ToString();
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
                    NewDR["property_id"] = CommonFunctions.SetNullable(comboBoxProperties);
                    NewDR["visitreason_id"] = CommonFunctions.SetNullable(comboBoxReason);
                    NewDR["employee_id"] = CommonFunctions.SetNullable(comboBoxEmployee);
                    NewDR["date"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerDate.Value);
                    NewDR["comments"] = userTextBoxComments.Text;
                    //buttonAssetConditions.Visible = CheckAssetButtonVisibility();
                }
                else
                {
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }

                    MySqlParameter _PROPERTY_ID = new MySqlParameter("_PROPERTY_ID", CommonFunctions.SetNullable(comboBoxProperties));
                    MySqlParameters.Add(_PROPERTY_ID);

                    MySqlParameter _VISITREASON_ID = new MySqlParameter("_VISITREASON_ID", CommonFunctions.SetNullable(comboBoxReason));
                    MySqlParameters.Add(_VISITREASON_ID);

                    MySqlParameter _EMPLOYEE_ID = new MySqlParameter("_EMPLOYEE_ID", CommonFunctions.SetNullable(comboBoxEmployee));
                    MySqlParameters.Add(_EMPLOYEE_ID);

                    MySqlParameter _DATE = new MySqlParameter("_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerDate.Value));
                    MySqlParameters.Add(_DATE);

                    MySqlParameter _COMMENTS = new MySqlParameter("_COMMENTS", userTextBoxComments.Text);
                    MySqlParameters.Add(_COMMENTS);
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "VISITSsp_insert", MySqlParameters.ToArray());
                    da.ExecuteInsertQuery();

                    string selected_reason = ((DataRowView)comboBoxReason.SelectedItem)["name"].ToString().Trim();
                    if (selected_reason == "Snagging" || selected_reason == "Furnish")
                    {
                        DataRow property = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[]{new MySqlParameter("_ID", NewDR["property_id"])})).ExecuteSelectQuery().Tables[0].Rows[0];
                        InvoiceRequirementsClass.InsertPunctualService(property, selected_reason, 2);
                    }
                    buttonAssetConditions.Enabled = true;
                    InitialDR = CommonFunctions.CopyDataRow(NewDR);                   
                    //this.Close();
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            //buttonAssetConditions.Visible = CheckAssetButtonVisibility();
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
            errorProvider1.SetError(comboBoxProperties, "");
            errorProvider1.SetError(comboBoxReason, "");
            errorProvider1.SetError(comboBoxEmployee, "");

            if (comboBoxProperties.SelectedValue == null || comboBoxProperties.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxProperties, Language.GetErrorText("errorEmptyProperty", "You must select the property!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxProperties.Name, Language.GetErrorText("errorEmptyProperty", "You must select the property!")));
                toReturn = false;
            }
            if (comboBoxReason.SelectedValue == null || comboBoxReason.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxReason, Language.GetErrorText("errorEmptyReason", "You must select the reason!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxReason.Name, Language.GetErrorText("errorEmptyReason", "You must select the reason!")));
                toReturn = false;
            }
            if (comboBoxEmployee.SelectedValue == null || comboBoxEmployee.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxEmployee, Language.GetErrorText("errorEmptyEmployee", "You must select the employee!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxEmployee.Name, Language.GetErrorText("errorEmptyEmployee", "You must select the employee!")));
                toReturn = false;
            }
            return toReturn;
        }

        private void pictureBoxSelectProperty_Click(object sender, EventArgs e)
        {
            var f = new ProportySelect(true);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxProperties.SelectedValue = f.IdToReturn;
            }
            f.Dispose();
        }

        private void comboBoxReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxProperties.SelectedIndex <= 0 && ((ComboBox)sender).SelectedIndex > 0)
            {
                MessageBox.Show(Language.GetMessageBoxText("errorEmptyProperty", "You must select the property first!"));
                ((ComboBox)sender).SelectedIndex = 0;
                return;
            }
            else
            {
                if (NewDR != null && NewDR.RowState != DataRowState.Added)
                {
                    //buttonAssetConditions.Visible = CheckAssetButtonVisibility();
                }
                this.labelPayer.Visible = this.comboBoxPayer.Visible = (((DataRowView)comboBoxReason.SelectedItem)["name"].ToString().Trim() == "Repairs");
            }
        }

        private void buttonAssetConditions_Click(object sender, EventArgs e)
        {
            /*
            if (MessageBox.Show(Language.GetMessageBoxText("addPropertyState", "Would you like to fill in the property state now?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var f = new PropertyState(Convert.ToInt32(comboBoxProperties.SelectedValue));
                f.ShowDialog();
            }
            */
            if (comboBoxProperties.SelectedIndex <= 0)
            {
                MessageBox.Show(Language.GetMessageBoxText("errorEmptyProperty", "You must select the property first!"));
                ((ComboBox)sender).SelectedIndex = -1;
                return;
            }
            else
            {
                var f = new PropertyState(Convert.ToInt32(comboBoxProperties.SelectedValue), Convert.ToInt32(NewDR["id"]));
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
            }
        }

        private bool CheckAssetButtonVisibility()
        {
            string selected_reason = ((DataRowView)comboBoxReason.SelectedItem)["name"].ToString().Trim();
            return (selected_reason == "Snagging" || selected_reason == "Three months inspection" || selected_reason == "Repairs" || selected_reason == "Furnish");
        }
    }
}
