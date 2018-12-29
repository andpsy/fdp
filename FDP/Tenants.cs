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
    public partial class Tenants : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();

        public Tenants()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
        }

        public Tenants(int id)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            NewDR = (new DataAccess(CommandType.StoredProcedure, "TENANTSsp_get_by_id", new object[] { new MySqlParameter("_ID", id) })).ExecuteSelectQuery().Tables[0].Rows[0];
            buttonSaveTenant.Enabled = false;
        }

        public Tenants(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
            NewDR = dr;
        }

        private void Tenants_Load(object sender, EventArgs e)
        {
            errorProvider1.SetIconAlignment(toolStrip1, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip2, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip3, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip4, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxTenantPhones, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxTenantEmails, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxOtherContactPhones, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxOtherContactEmails, ErrorIconAlignment.MiddleLeft);

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
                    userTextBoxTenantName.Text = NewDR["name"].ToString();
                    userTextBoxTenantFullName.Text = NewDR["full_name"].ToString();
                    string[] phones = NewDR["phones"].ToString().Split(';');
                    foreach (string phone in phones)
                    {
                        if (phone.Trim() != "")
                            listBoxTenantPhones.Items.Add(phone);
                    }
                    string[] emails = NewDR["emails"].ToString().Split(';');
                    foreach (string email in emails)
                    {
                        if (email.Trim() != "")
                            listBoxTenantEmails.Items.Add(email);
                    }

                    userTextBoxTenantRepresentative.Text = NewDR["representative"].ToString();
                    userTextBoxOtherContactsInformation.Text = NewDR["other_contact_information"].ToString();

                    string[] other_phones = NewDR["other_contact_phones"].ToString().Split(';');
                    foreach (string phone in other_phones)
                    {
                        if (phone.Trim() != "")
                            listBoxOtherContactPhones.Items.Add(phone);
                    }
                    string[] other_emails = NewDR["other_contact_emails"].ToString().Split(';');
                    foreach (string email in other_emails)
                    {
                        if (email.Trim() != "")
                            listBoxOtherContactEmails.Items.Add(email);
                    }
                    userTextBoxTenantComments.Text = NewDR["comments"].ToString();
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
                    foreach (string phone in listBoxTenantPhones.Items)
                        phones += String.Format("{0};", phone);
                    phones = phones.Substring(0, phones.Length- 1);
                }
                catch { }
                string emails = "";
                try
                {
                    foreach (string email in listBoxTenantEmails.Items)
                        emails += String.Format("{0};", email);
                    emails = emails.Substring(0, emails.Length- 1);
                }
                catch { }

                string other_phones = "";
                try
                {
                    foreach (string phone in listBoxOtherContactPhones.Items)
                        other_phones += String.Format("{0};", phone);
                    other_phones = other_phones.Substring(0, other_phones.Length- 1);
                }
                catch { }
                string other_emails = "";
                try
                {
                    foreach (string email in listBoxOtherContactEmails.Items)
                        other_emails += String.Format("{0};", email);
                    other_emails = other_emails.Substring(0, other_emails.Length- 1);
                }
                catch { }



                if (NewDR != null)
                {
                    NewDR["name"] = userTextBoxTenantName.Text;
                    NewDR["full_name"] = userTextBoxTenantFullName.Text;
                    NewDR["phones"] = phones;
                    NewDR["emails"] = emails;
                    NewDR["representative"] = userTextBoxTenantRepresentative.Text;
                    NewDR["other_contact_information"] = userTextBoxOtherContactsInformation.Text;
                    NewDR["other_contact_phones"] = other_phones;
                    NewDR["other_contact_emails"] = other_emails;
                    NewDR["comments"] = userTextBoxTenantComments.Text;
                }
                else
                {
                    MySqlParameters.Clear();
                    if(NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _NAME = new MySqlParameter("_NAME", userTextBoxTenantName.Text);
                    MySqlParameters.Add(_NAME);

                    MySqlParameter _FULL_NAME = new MySqlParameter("_FULL_NAME", userTextBoxTenantFullName.Text);
                    MySqlParameters.Add(_FULL_NAME);

                    MySqlParameter _PHONES = new MySqlParameter("_PHONES", phones);
                    MySqlParameters.Add(_PHONES);

                    MySqlParameter _EMAILS = new MySqlParameter("_EMAILS", emails);
                    MySqlParameters.Add(_EMAILS);

                    MySqlParameter _REPRESENTATIVE = new MySqlParameter("_REPRESENTATIVE", userTextBoxTenantRepresentative.Text);
                    MySqlParameters.Add(_REPRESENTATIVE);

                    MySqlParameter _OTHER_CONTACT_INFORMATION = new MySqlParameter("_OTHER_CONTACT_INFORMATION", userTextBoxOtherContactsInformation.Text);
                    MySqlParameters.Add(_OTHER_CONTACT_INFORMATION);

                    MySqlParameter _OTHER_CONTACT_PHONES = new MySqlParameter("_OTHER_CONTACT_PHONES", phones);
                    MySqlParameters.Add(_OTHER_CONTACT_PHONES);

                    MySqlParameter _OTHER_CONTACT_EMAILS = new MySqlParameter("_OTHER_CONTACT_EMAILS", emails);
                    MySqlParameters.Add(_OTHER_CONTACT_EMAILS);

                    MySqlParameter _COMMENTS = new MySqlParameter("_COMMENTS", userTextBoxTenantComments.Text);
                    MySqlParameters.Add(_COMMENTS);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }



        private void buttonSaveTenant_Click(object sender, EventArgs e)
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "TENANTSsp_insert", MySqlParameters.ToArray());
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "TENANTSsp_insert", MySqlParameters.ToArray());
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "TENANTSsp_update", MySqlParameters.ToArray());
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

        private void toolStripButtonAddPhone_Click(object sender, EventArgs e)
        {
            if (userTextBoxTenantPhoneEdit.Text.Trim() != "")
            {
                if (Validator.IsPhoneNumber(userTextBoxTenantPhoneEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip1, "");
                    listBoxTenantPhones.Items.Add(userTextBoxTenantPhoneEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip1, Language.GetErrorText("errorInvalidPhoneNumber", "Invalid phone number!"));
                }
            }
        }

        private void toolStripButtonDeletePhone_Click(object sender, EventArgs e)
        {
            if (listBoxTenantPhones.SelectedIndex > -1)
            {
                listBoxTenantPhones.Items.RemoveAt(listBoxTenantPhones.SelectedIndex);
            }
        }

        private void toolStripButtonAddEmail_Click(object sender, EventArgs e)
        {
            if (userTextBoxTenantEmailEdit.Text.Trim() != "")
            {
                if (Validator.IsEmail(userTextBoxTenantEmailEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip2, "");
                    listBoxTenantEmails.Items.Add(userTextBoxTenantEmailEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip2, Language.GetErrorText("errorInvalidEmail", "Invalid email address!"));
                }
            }
        }

        private void toolStripButtonDeleteEmail_Click(object sender, EventArgs e)
        {
            if (listBoxTenantEmails.SelectedIndex > -1)
            {
                listBoxTenantEmails.Items.RemoveAt(listBoxTenantEmails.SelectedIndex);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateData()
        {
            bool toReturn = true;
            errorProvider1.SetError(userTextBoxTenantName, "");
            errorProvider1.SetError(listBoxTenantPhones, "");
            errorProvider1.SetError(listBoxTenantEmails, "");

            if (NewDR != null)
            {
                if (NewDR["name"].ToString().Trim()  == "")
                {
                    errorProvider1.SetError(userTextBoxTenantName, Language.GetErrorText("errorEmptyTenantName", "Tenant Name can not by empty!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxTenantName.Name, Language.GetErrorText("errorEmptyTenantName", "Tenant Name can not by empty!")));
                    toReturn = false;
                }

                if (listBoxTenantPhones.Items.Count == 0 && listBoxTenantEmails.Items.Count == 0)
                {
                    errorProvider1.SetError(listBoxTenantPhones, Language.GetErrorText("errorEmptyTenantPhonesAndEmails", "Either phone or email must be filled in!"));
                    errorProvider1.SetError(listBoxTenantEmails, Language.GetErrorText("errorEmptyTenantPhonesAndEmails", "Either phone or email must be filled in!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(listBoxTenantEmails.Name, Language.GetErrorText("errorEmptyTenantPhonesAndEmails", "Either phone or email must be filled in!")));
                    toReturn = false;
                }
            }
            else
            {
                if (userTextBoxTenantName.Text.Trim() == "")
                {
                    errorProvider1.SetError(userTextBoxTenantName, Language.GetErrorText("errorEmptyTenantName", "Tenant Name can not by empty!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxTenantName.Name, Language.GetErrorText("errorEmptyTenantName", "Tenant Name can not by empty!")));
                    toReturn = false;
                }

                if (listBoxTenantPhones.Items.Count == 0 && listBoxTenantEmails.Items.Count == 0)
                {
                    errorProvider1.SetError(listBoxTenantPhones, Language.GetErrorText("errorEmptyTenantPhonesAndEmails", "Either phone or email must be filled in!"));
                    errorProvider1.SetError(listBoxTenantEmails, Language.GetErrorText("errorEmptyTenantPhonesAndEmails", "Either phone or email must be filled in!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(listBoxTenantEmails.Name, Language.GetErrorText("errorEmptyTenantPhonesAndEmails", "Either phone or email must be filled in!")));
                    toReturn = false;
                }
            }
            return toReturn;
        }

        private void toolStripButtonAddOtherContactPhone_Click(object sender, EventArgs e)
        {
            if (toolStripTextBoxOtherContactPhoneEdit.Text.Trim() != "")
            {
                if (Validator.IsPhoneNumber(toolStripTextBoxOtherContactPhoneEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip4, "");
                    listBoxOtherContactPhones.Items.Add(toolStripTextBoxOtherContactPhoneEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip4, Language.GetErrorText("errorInvalidPhoneNumber", "Invalid phone number!"));
                }
            }
        }

        private void toolStripButtonEditOtherContactPhone_Click(object sender, EventArgs e)
        {
            if (listBoxOtherContactPhones.SelectedIndex > -1)
            {
                listBoxOtherContactPhones.Items.RemoveAt(listBoxOtherContactPhones.SelectedIndex);
            }
        }

        private void toolStripButtonAddOtherContactEmail_Click(object sender, EventArgs e)
        {
            if (toolStripTextBoxOtherContactEmailEdit.Text.Trim() != "")
            {
                if (Validator.IsEmail(toolStripTextBoxOtherContactEmailEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip3, "");
                    listBoxOtherContactEmails.Items.Add(toolStripTextBoxOtherContactEmailEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip3, Language.GetErrorText("errorInvalidEmail", "Invalid email address!"));
                }
            }
        }

        private void toolStripButtonEditOtherContactEmail_Click(object sender, EventArgs e)
        {
            if (listBoxOtherContactEmails.SelectedIndex > -1)
            {
                listBoxOtherContactEmails.Items.RemoveAt(listBoxOtherContactEmails.SelectedIndex);
            }
        }
    }
}
