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
    public partial class Companies : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;

        public ArrayList MySqlParameters = new ArrayList();
        public int counter;
        public bool RecreateSchema = false;

        public Companies()
        {
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
        }

        public Companies(int id)
        {
            base.CheckDataOnClosing = true;
            InitializeComponent();
            NewDR = new DataAccess(CommandType.StoredProcedure, "COMPANIESsp_get_by_id", new object[] { new MySqlParameter("_ID", id) }).ExecuteSelectQuery().Tables[0].Rows[0];
            buttonSave.Enabled = false;
        }

        public Companies(DataRow dr)
        {
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
            NewDR = dr;
        }

        private void Companies_Load(object sender, EventArgs e)
        {
            errorProvider1.SetIconAlignment(toolStrip1, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip2, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxPhones, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxEmails, ErrorIconAlignment.MiddleLeft);

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
                //if (OwnerDR.RowState != DataRowState.Added && OwnerDR != null)
                if (NewDR != null)
                {
                    userTextBoxCounter.Text = NewDR["company_counter"].ToString().PadLeft(4, '0');
                    if (NewDR.RowState == DataRowState.Modified)
                    {
                        labelRecreateDB.Visible = labelWarningRecreateSchema.Visible = checkBoxRecreateDB.Visible = true;
                    }
                    if (NewDR.RowState == DataRowState.Detached)
                    {
                        counter = Convert.ToInt32(new DataAccess(CommandType.StoredProcedure, "COMPANIESsp_get_counter").ExecuteScalarQuery()) + 1;
                        userTextBoxCounter.Text = counter.ToString().PadLeft(4, '0');
                    }
                    userTextBoxName.Text = NewDR["name"].ToString();
                    userTextBoxCif.Text = NewDR["cif"].ToString();
                    userTextBoxCui.Text = NewDR["cui"].ToString();
                    userTextBoxRegComNumber.Text = NewDR["commercial_register_number"].ToString();
                    counter = Convert.ToInt32(NewDR["company_counter"]);

                    userTextBoxAddress.Text = NewDR["address"].ToString();
                    userTextBoxDistrict.Text = NewDR["district"].ToString();
                    string[] phones = NewDR["phones"].ToString().Split(';');
                    foreach (string phone in phones)
                    {
                        if (phone.Trim() != "")
                            listBoxPhones.Items.Add(phone);
                    }
                    string[] emails = NewDR["emails"].ToString().Split(';');
                    foreach (string email in emails)
                    {
                        if (email.Trim() != "")
                            listBoxEmails.Items.Add(email);
                    }
                    string[] websites = NewDR["websites"].ToString().Split(';');
                    foreach (string website in websites)
                    {
                        if (website.Trim() != "")
                            listBoxWebsites.Items.Add(website);
                    }
                    string[] faxes = NewDR["faxes"].ToString().Split(';');
                    foreach (string fax in faxes)
                    {
                        if (fax.Trim() != "")
                            listBoxFaxes.Items.Add(fax);
                    }

                    userTextBoxBankName1.Text = NewDR["bank_name1"].ToString();
                    userTextBoxBankAccount1.Text = NewDR["bank_account_details1"].ToString();
                    userTextBoxBankName2.Text = NewDR["bank_name2"].ToString();
                    userTextBoxBankAccount2.Text = NewDR["bank_account_details2"].ToString();
                    userTextBoxCapital.Text = NewDR["capital"].ToString();
                    userTextBoxComments.Text = NewDR["comments"].ToString();
                }
                else
                {
                    counter = Convert.ToInt32(new DataAccess(CommandType.StoredProcedure, "COMPANIESsp_get_counter").ExecuteScalarQuery()) + 1;
                    userTextBoxCounter.Text = counter.ToString().PadLeft(4, '0');
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
                string phones = "";
                try
                {
                    foreach (string phone in listBoxPhones.Items)
                        phones += String.Format("{0};", phone);
                    phones = phones.Substring(0, phones.Length- 1);
                }
                catch { }
                string emails = "";
                try
                {
                    foreach (string email in listBoxEmails.Items)
                        emails += String.Format("{0};", email);
                    emails = emails.Substring(0, emails.Length- 1);
                }
                catch { }
                string websites = "";
                try
                {
                    foreach (string website in listBoxWebsites.Items)
                        websites += String.Format("{0};", website);
                    websites = websites.Substring(0, websites.Length - 1);
                }
                catch { }
                string faxes = "";
                try
                {
                    foreach (string fax in listBoxFaxes.Items)
                        faxes += String.Format("{0};", fax);
                    faxes = faxes.Substring(0, faxes.Length - 1);
                }
                catch { }

                if (NewDR != null)
                {
                    NewDR["name"] = userTextBoxName.Text;
                    NewDR["cif"] = userTextBoxCif.Text;
                    NewDR["cui"] = userTextBoxCui.Text;
                    NewDR["commercial_register_number"] = userTextBoxRegComNumber.Text;
                    NewDR["address"] = userTextBoxAddress.Text;
                    NewDR["district"] = userTextBoxDistrict.Text;
                    NewDR["phones"] = phones;
                    NewDR["faxes"] = faxes;
                    NewDR["emails"] = emails;
                    NewDR["websites"] = websites;
                    NewDR["company_counter"] = counter;
                    NewDR["bank_name1"] = userTextBoxBankName1.Text;
                    NewDR["bank_account_details1"] = userTextBoxBankAccount1.Text;
                    NewDR["bank_name2"] = userTextBoxBankName2.Text;
                    NewDR["bank_account_details2"] = userTextBoxBankAccount2.Text;
                    NewDR["comments"] = userTextBoxComments.Text;
                    NewDR["capital"] = userTextBoxCapital.Text;
                }
                else
                {
                    MySqlParameters.Clear();
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _NAME = new MySqlParameter("_NAME", userTextBoxName.Text);
                    MySqlParameters.Add(_NAME);

                    MySqlParameter _CIF = new MySqlParameter("_CIF", userTextBoxCif.Text);
                    MySqlParameters.Add(_CIF);

                    MySqlParameter _CUI = new MySqlParameter("_CUI", userTextBoxCui.Text);
                    MySqlParameters.Add(_CUI);

                    MySqlParameter _COMPANY_COUNTER = new MySqlParameter("_COMPANY_COUNTER", userTextBoxCounter.Text);
                    MySqlParameters.Add(_COMPANY_COUNTER);

                    MySqlParameter _COMMERCIAL_REGISTER_NUMBER = new MySqlParameter("_COMMERCIAL_REGISTER_NUMBER", userTextBoxRegComNumber.Text);
                    MySqlParameters.Add(_COMMERCIAL_REGISTER_NUMBER);

                    MySqlParameter _ADDRESS = new MySqlParameter("_ADDRESS", userTextBoxAddress.Text);
                    MySqlParameters.Add(_ADDRESS);

                    MySqlParameter _DISTRICT = new MySqlParameter("_DISTRICT", userTextBoxDistrict.Text);
                    MySqlParameters.Add(_DISTRICT);

                    MySqlParameter _PHONES = new MySqlParameter("_PHONES", phones);
                    MySqlParameters.Add(_PHONES);

                    MySqlParameter _FAXES = new MySqlParameter("_FAXES", faxes);
                    MySqlParameters.Add(_FAXES);

                    MySqlParameter _EMAILS = new MySqlParameter("_EMAILS", emails);
                    MySqlParameters.Add(_EMAILS);

                    MySqlParameter _WEBSITES = new MySqlParameter("_WEBSITES", websites);
                    MySqlParameters.Add(_WEBSITES);

                    MySqlParameter _BANK_NAME1 = new MySqlParameter("_BANK_NAME1", userTextBoxBankName1.Text);
                    MySqlParameters.Add(_BANK_NAME1);

                    MySqlParameter _BANK_ACCOUNT_DETAILS1 = new MySqlParameter("_BANK_ACCOUNT_DETAILS1", userTextBoxBankAccount1.Text);
                    MySqlParameters.Add(_BANK_ACCOUNT_DETAILS1);

                    MySqlParameter _BANK_NAME2 = new MySqlParameter("_BANK_NAME2", userTextBoxBankName2.Text);
                    MySqlParameters.Add(_BANK_NAME2);

                    MySqlParameter _BANK_ACCOUNT_DETAILS2 = new MySqlParameter("_BANK_ACCOUNT_DETAILS2", userTextBoxBankAccount2.Text);
                    MySqlParameters.Add(_BANK_ACCOUNT_DETAILS2);

                    MySqlParameter _CAPITAL = new MySqlParameter("_CAPITAL", userTextBoxCapital.Text);
                    MySqlParameters.Add(_CAPITAL);

                    MySqlParameter _COMMENTS = new MySqlParameter("_COMMENTS", userTextBoxComments.Text);
                    MySqlParameters.Add(_COMMENTS);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }



        private void buttonSaveOwners_Click(object sender, EventArgs e)
        {
            GenerateMySqlParameters();
            if (!ValidateData())
            {
                //this.DialogResult = DialogResult.Cancel;
                return;
            }
            
            if (NewDR == null) //add direct
            {
                try
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "COMPANIESsp_insert", MySqlParameters.ToArray());
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "COMPANIESsp_insert", MySqlParameters.ToArray());
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "COMPANIESsp_update", MySqlParameters.ToArray());
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
        }

        private void toolStripButtonAddPhone_Click(object sender, EventArgs e)
        {
            if (userTextBoxPhoneEdit.Text.Trim() != "")
            {
                if (Validator.IsPhoneNumber(userTextBoxPhoneEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip1, "");
                    listBoxPhones.Items.Add(userTextBoxPhoneEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip1, Language.GetErrorText("errorInvalidPhoneNumber", "Invalid phone number!"));
                }
            }
        }

        private void toolStripButtonDeletePhone_Click(object sender, EventArgs e)
        {
            if (listBoxPhones.SelectedIndex > -1)
            {
                listBoxPhones.Items.RemoveAt(listBoxPhones.SelectedIndex);
            }
        }

        private void toolStripButtonAddEmail_Click(object sender, EventArgs e)
        {
            if (userTextBoxEmailEdit.Text.Trim() != "")
            {
                if (Validator.IsEmail(userTextBoxEmailEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip2, "");
                    listBoxEmails.Items.Add(userTextBoxEmailEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip2, Language.GetErrorText("errorInvalidEmail", "Invalid email address!"));
                }
            }
        }

        private void toolStripButtonDeleteEmail_Click(object sender, EventArgs e)
        {
            if (listBoxEmails.SelectedIndex > -1)
            {
                listBoxEmails.Items.RemoveAt(listBoxEmails.SelectedIndex);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool ValidateData()
        {
            bool toReturn = true;
            errorProvider1.SetError(userTextBoxCif, "");
            errorProvider1.SetError(userTextBoxCui, "");
            errorProvider1.SetError(userTextBoxName, "");
            errorProvider1.SetError(listBoxPhones, "");
            errorProvider1.SetError(listBoxEmails, "");

            if (NewDR != null)
            {
                if (NewDR["name"].ToString().Trim() == "")
                {
                    errorProvider1.SetError(userTextBoxName, Language.GetErrorText("errorEmptyCompanyName", "Company Name can not by empty!"));
                    toReturn = false;
                }

                if (listBoxPhones.Items.Count == 0 && listBoxEmails.Items.Count == 0)
                {
                    errorProvider1.SetError(listBoxPhones, Language.GetErrorText("errorEmptyOwnerPhonesAndEmails", "Either phone or email must be filled in!"));
                    errorProvider1.SetError(listBoxEmails, Language.GetErrorText("errorEmptyOwnerPhonesAndEmails", "Either phone or email must be filled in!"));
                    toReturn = false;
                }

                if (NewDR["cui"] != DBNull.Value && NewDR["cui"].ToString().Trim() != "")
                {
                    if (!Validator.SimpleValidateCUI(NewDR["cui"].ToString()))
                    {
                        errorProvider1.SetError(userTextBoxCif, Language.GetErrorText("errorCuiCifInvalid", "CUI/CIF invalid!"));
                        toReturn = false;
                    }
                }
            }
            else
            {
                if (userTextBoxName.Text.Trim() == "")
                {
                    errorProvider1.SetError(userTextBoxName, Language.GetErrorText("errorEmptyOwnerName", "Owner Name can not by empty!"));
                    toReturn = false;
                }

                if (listBoxPhones.Items.Count == 0 && listBoxEmails.Items.Count == 0)
                {
                    errorProvider1.SetError(listBoxPhones, Language.GetErrorText("errorEmptyOwnerPhonesAndEmails", "Either phone or email must be filled in!"));
                    errorProvider1.SetError(listBoxEmails, Language.GetErrorText("errorEmptyOwnerPhonesAndEmails", "Either phone or email must be filled in!"));
                    toReturn = false;
                }

                if (userTextBoxCui.Text.Trim() != "")
                {
                    if (!Validator.SimpleValidateCUI(userTextBoxCui.Text.Trim()))
                    {
                        errorProvider1.SetError(userTextBoxCui, Language.GetErrorText("errorCuiCifInvalid", "CUI/CIF invalid!"));
                        toReturn = false;
                    }
                }
            }
            return toReturn;
        }

        private void checkBoxRecreateDB_CheckedChanged(object sender, EventArgs e)
        {
            this.RecreateSchema = ((CheckBox)sender).Checked;
        }

        private void toolStripButtonAddWebsite_Click(object sender, EventArgs e)
        {
            listBoxWebsites.Items.Add(toolStripTextBoxWebsiteEdit.Text.Trim());
        }

        private void toolStripButtonAddFax_Click(object sender, EventArgs e)
        {
            listBoxFaxes.Items.Add(toolStripTextBoxFaxEdit.Text.Trim());
        }

        private void toolStripButtonDeleteWebsite_Click(object sender, EventArgs e)
        {
            if (listBoxWebsites.SelectedIndex > -1)
            {
                listBoxWebsites.Items.RemoveAt(listBoxWebsites.SelectedIndex);
            }
        }

        private void toolStripButtonDeleteFax_Click(object sender, EventArgs e)
        {
            if (listBoxFaxes.SelectedIndex > -1)
            {
                listBoxFaxes.Items.RemoveAt(listBoxFaxes.SelectedIndex);
            }
        }
    }
}
