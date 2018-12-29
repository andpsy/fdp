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
    public partial class CoOwners : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();

        public CoOwners()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
        }

        public CoOwners(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            NewDR = dr;
            InitializeComponent();
        }

        private void Owners_Load(object sender, EventArgs e)
        {
            if (NewDR != null)
            {
                FillInfo();
            }
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
        }

        private void FillInfo()
        {
            try
            {
                if (NewDR.RowState != DataRowState.Added && NewDR != null)
                {
                    userTextBoxOwnerName.Text = NewDR["name"].ToString();
                    userTextBoxOwnerFullName.Text = NewDR["full_name"].ToString();
                    userTextBoxOwnerCif.Text = NewDR["cif"].ToString();
                    userTextBoxNIF.Text = NewDR["nif"].ToString();
                    userTextBoxOwnerCnp.Text = NewDR["cnp"].ToString();
                    userTextBoxOwnerCui.Text = NewDR["cui"].ToString();
                    userTextBoxOwnerComments.Text = NewDR["comments"].ToString();
                    checkBoxRenouncementForRentIncomes.Checked = NewDR["renouncement_for_rent_incomes"] == DBNull.Value ? false : Convert.ToBoolean(NewDR["renouncement_for_rent_incomes"]);
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
                if (NewDR != null)
                {
                    NewDR["name"] = userTextBoxOwnerName.Text;
                    NewDR["full_name"] = userTextBoxOwnerFullName.Text;
                    NewDR["cif"] = userTextBoxOwnerCif.Text;
                    NewDR["nif"] = userTextBoxNIF.Text;
                    NewDR["cnp"] = userTextBoxOwnerCnp.Text;
                    NewDR["cui"] = userTextBoxOwnerCui.Text;
                    NewDR["comments"] = userTextBoxOwnerComments.Text;
                    NewDR["renouncement_for_rent_incomes"] = checkBoxRenouncementForRentIncomes.Checked;
                }
                else
                {
                    if (NewDR.RowState != DataRowState.Added && NewDR != null)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _NAME = new MySqlParameter("_NAME", userTextBoxOwnerName.Text);
                    MySqlParameters.Add(_NAME);

                    MySqlParameter _FULL_NAME = new MySqlParameter("_FULL_NAME", userTextBoxOwnerFullName.Text);
                    MySqlParameters.Add(_FULL_NAME);

                    MySqlParameter _CIF = new MySqlParameter("_CIF", userTextBoxOwnerCif.Text);
                    MySqlParameters.Add(_CIF);

                    MySqlParameter _NIF = new MySqlParameter("_NIF", userTextBoxNIF.Text);
                    MySqlParameters.Add(_NIF);

                    MySqlParameter _CNP = new MySqlParameter("_CNP", userTextBoxOwnerCnp.Text);
                    MySqlParameters.Add(_CNP);

                    MySqlParameter _CUI = new MySqlParameter("_CUI", userTextBoxOwnerCui.Text);
                    MySqlParameters.Add(_CUI);

                    MySqlParameter _COMMENTS = new MySqlParameter("_COMMENTS", userTextBoxOwnerComments.Text);
                    MySqlParameters.Add(_COMMENTS);

                    MySqlParameter _RENOUNCEMENT_FOR_RENT_INCOMES = new MySqlParameter("_RENOUNCEMENT_FOR_RENT_INCOMES", checkBoxRenouncementForRentIncomes.Checked);
                    MySqlParameters.Add(_RENOUNCEMENT_FOR_RENT_INCOMES);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void buttonSaveCoOwners_Click(object sender, EventArgs e)
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "CO_OWNERSsp_insert", MySqlParameters.ToArray());
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
            /*
            if (NewDR.RowState == DataRowState.Added) //add from selection
            {
                try
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_insert", MySqlParameters.ToArray());
                    da.ExecuteInsertQuery();
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else  //edit from selection
            {
                try
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_update", MySqlParameters.ToArray());
                    da.ExecuteUpdateQuery();
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }   
            */
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
            errorProvider1.SetError(userTextBoxOwnerCnp, "");
            errorProvider1.SetError(userTextBoxOwnerCif, "");
            errorProvider1.SetError(userTextBoxOwnerCui, "");
            errorProvider1.SetError(userTextBoxOwnerName, "");

            if (userTextBoxOwnerName.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxOwnerName, Language.GetErrorText("errorEmptyOwnerName", "Owner Name can not by empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxOwnerName.Name, Language.GetErrorText("errorEmptyOwnerName", "Owner Name can not by empty!")));
                toReturn = false;
            }

            /*
            ArrayList cnp_errors = Validator.ValidateCNP(NewDR["cnp"].ToString());
            if (cnp_errors.Count > 0)
            {
                toReturn = false;
                foreach (string s in cnp_errors)
                    errorProvider1.SetError(userTextBoxOwnerCnp, s);
            }
            */
            /*
            if ((NewDR["cnp"] != DBNull.Value && NewDR["cnp"].ToString().Trim() != ""))
            {
                if (!Validator.SimpleValidateCNP(NewDR["cnp"].ToString()))
                {
                    errorProvider1.SetError(userTextBoxOwnerCnp, Language.GetErrorText("errorCnpInvalid", "CNP invalid!"));
                    toReturn = false;
                }
            }
            */
            /*
            if ((NewDR["cif"] != DBNull.Value && NewDR["cif"].ToString().Trim() != ""))
            {
                if (!Validator.SimpleValidateCUI(NewDR["cif"].ToString()))
                {
                    errorProvider1.SetError(userTextBoxOwnerCif, Language.GetErrorText("errorCuiCifInvalid", "CUI/CIF invalid!"));
                    toReturn = false;
                }
            }
            */
            /*
            if ((NewDR["cui"] != DBNull.Value && NewDR["cui"].ToString().Trim() != ""))
            {
                if (!Validator.SimpleValidateCUI(NewDR["cui"].ToString()))
                {
                    errorProvider1.SetError(userTextBoxOwnerCui, Language.GetErrorText("errorCuiCifInvalid", "CUI/CIF invalid!"));
                    toReturn = false;
                }
            }
            */
            return toReturn;
        }
    }
}
