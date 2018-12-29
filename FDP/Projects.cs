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
    public partial class Projects : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();

        public Projects()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
        }

        public Projects(int id)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROJECTSsp_get_by_id", new object[] { new MySqlParameter("_ID", id) });
            NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
            buttonSaveProperty.Enabled = false;
        }

        public Projects(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
            NewDR = dr;
        }

        private void Projects_Load(object sender, EventArgs e)
        {
            errorProvider1.SetIconAlignment(toolStrip1, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip2, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip3, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip4, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip5, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip6, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip7, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip8, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxPropertyAdministratorPhones, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxPropertyAdministratorEmails, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxPresidentOwnerAssociationPhones, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxPresidentOwnerAssociationEmails, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxMaintenancePersonPhones, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxMaintenancePersonEmails, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxDevelopperPhones, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxDevelopperEmails, ErrorIconAlignment.MiddleLeft);
            
            FillCombos();
            if (NewDR != null)
            {
                FillInfo();
            }
        }

        private void FillCombos()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CITIESsp_select");
            DataTable dtCities = da.ExecuteSelectQuery().Tables[0];
            if (dtCities != null)
            {
                comboBoxPropertyCity.DisplayMember = "name";
                comboBoxPropertyCity.ValueMember = "id";
                comboBoxPropertyCity.DataSource = dtCities;
            }
        }

        private void FillInfo()
        {
            try
            {
                //if (PropertyDR.RowState != DataRowState.Added && PropertyDR != null)
                if (NewDR != null)
                {
                    userTextBoxPropertyName.Text = NewDR["name"].ToString();
                    userTextBoxPropertyLocation.Text = NewDR["location"].ToString();
                    comboBoxPropertyCity.SelectedValue = NewDR["city_id"];
                    userTextBoxPropertyAddress.Text = NewDR["address"].ToString();
                    userTextBoxDetails.Text = NewDR["details"].ToString();
                    userTextBoxAdministratorName.Text = NewDR["administrator_name"].ToString();
                    string[] administrator_phones = NewDR["administrator_phones"].ToString().Split(';');
                    foreach (string phone in administrator_phones)
                    {
                        if (phone.Trim() != "")
                            listBoxPropertyAdministratorPhones.Items.Add(phone);
                    }
                    string[] administrator_emails = NewDR["administrator_emails"].ToString().Split(';');
                    foreach (string email in administrator_emails)
                    {
                        if (email.Trim() != "")
                            listBoxPropertyAdministratorEmails.Items.Add(email);
                    }
                    userTextBoxPropertyPresidentOwnereAssociationName.Text = NewDR["president_owners_association_name"].ToString();
                    string[] president_owners_association_phones = NewDR["president_owners_association_phones"].ToString().Split(';');
                    foreach (string phone in president_owners_association_phones)
                    {
                        if (phone.Trim() != "")
                            listBoxPresidentOwnerAssociationPhones.Items.Add(phone);
                    }
                    string[] president_owners_association_emails = NewDR["president_owners_association_emails"].ToString().Split(';');
                    foreach (string email in president_owners_association_emails)
                    {
                        if (email.Trim() != "")
                            listBoxPresidentOwnerAssociationEmails.Items.Add(email);
                    }
                    userTextBoxAssociationName.Text = NewDR["owners_association_name"].ToString();
                    userTextBoxAssociationCif.Text = NewDR["owners_association_cif"].ToString();
                    userTextBoxAssociationBankName.Text = NewDR["owners_association_bank"].ToString();
                    userTextBoxAssociationBankAccount.Text = NewDR["owners_association_bank_account"].ToString();
                    userTextBoxMaintenancePersonName.Text = NewDR["maintenance_person_name"].ToString();
                    string[] maintenance_person_phones = NewDR["maintenance_person_phones"].ToString().Split(';');
                    foreach (string phone in maintenance_person_phones)
                    {
                        if (phone.Trim() != "")
                            listBoxMaintenancePersonPhones.Items.Add(phone);
                    }
                    string[] maintenance_person_emails = NewDR["maintenance_person_emails"].ToString().Split(';');
                    foreach (string email in maintenance_person_emails)
                    {
                        if (email.Trim() != "")
                            listBoxMaintenancePersonEmails.Items.Add(email);
                    }
                    userTextBoxInternetProviderInformation.Text = NewDR["internet_provider_information"].ToString();
                    userTextBoxTelephoneProviderInformation.Text = NewDR["telephone_provider_information"].ToString();
                    userTextBoxTVProviderInformation.Text = NewDR["tv_provider_information"].ToString();
                    userTextBoxInternetRepresentative.Text = NewDR["internet_provider_representative_information"].ToString();
                    userTextBoxTelephoneRepresentative.Text = NewDR["telephone_provider_representative_information"].ToString();
                    userTextBoxTvRepresentative.Text = NewDR["tv_provider_representative_information"].ToString();
                    userTextBoxDevelopperName.Text = NewDR["developer_name"].ToString();
                    userTextBoxDevelopperContactPersonName.Text = NewDR["developer_contact_person"].ToString();
                    string[] developer_phones = NewDR["developer_phones"].ToString().Split(';');
                    foreach (string phone in developer_phones)
                    {
                        if (phone.Trim() != "")
                            listBoxDevelopperPhones.Items.Add(phone);
                    }
                    string[] developer_emails = NewDR["developer_emails"].ToString().Split(';');
                    foreach (string email in developer_emails)
                    {
                        if (email.Trim() != "")
                            listBoxDevelopperEmails.Items.Add(email);
                    }
                    userTextBoxCondominiumFee.Text = NewDR["condominium_fee"].ToString();
                    userTextBoxFloatingCapital.Text = NewDR["floating_capital"].ToString();
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
                string administrator_phones = "";
                try
                {
                    foreach (string phone in listBoxPropertyAdministratorPhones.Items)
                        administrator_phones += String.Format("{0};", phone);
                    administrator_phones = administrator_phones.Substring(0, administrator_phones.Length - 1);
                }
                catch { }
                string administrator_emails = "";
                try
                {
                    foreach (string email in listBoxPropertyAdministratorEmails.Items)
                        administrator_emails += String.Format("{0};", email);
                    administrator_emails = administrator_emails.Substring(0, administrator_emails.Length - 1);
                }
                catch { }

                string president_owners_association_phones = "";
                try
                {
                    foreach (string phone in listBoxPresidentOwnerAssociationPhones.Items)
                        president_owners_association_phones += String.Format("{0};", phone);
                    president_owners_association_phones = president_owners_association_phones.Substring(0, president_owners_association_phones.Length - 1);
                }
                catch { }
                string president_owners_association_emails = "";
                try
                {
                    foreach (string email in listBoxPresidentOwnerAssociationEmails.Items)
                        president_owners_association_emails += String.Format("{0};", email);
                    president_owners_association_emails = president_owners_association_emails.Substring(0, president_owners_association_emails.Length - 1);
                }
                catch { }

                string maintenance_person_phones = "";
                try
                {
                    foreach (string phone in listBoxMaintenancePersonPhones.Items)
                        maintenance_person_phones += String.Format("{0};", phone);
                    maintenance_person_phones = maintenance_person_phones.Substring(0, maintenance_person_phones.Length - 1);
                }
                catch { }
                string maintenance_person_emails = "";
                try
                {
                    foreach (string email in listBoxMaintenancePersonEmails.Items)
                        maintenance_person_emails += String.Format("{0};", email);
                    maintenance_person_emails = maintenance_person_emails.Substring(0, maintenance_person_emails.Length - 1);
                }
                catch { }

                string developer_phones = "";
                try
                {
                    foreach (string phone in listBoxDevelopperPhones.Items)
                        developer_phones += String.Format("{0};", phone);
                    developer_phones = developer_phones.Substring(0, developer_phones.Length - 1);
                }
                catch { }
                string developer_emails = "";
                try
                {
                    foreach (string email in listBoxDevelopperEmails.Items)
                        developer_emails += String.Format("{0};", email);
                    developer_emails = developer_emails.Substring(0, developer_emails.Length - 1);
                }
                catch { }
                
                

                if (NewDR != null)
                {
                    NewDR["name"] = userTextBoxPropertyName.Text;
                    NewDR["location"] = userTextBoxPropertyLocation.Text;
                    NewDR["city_id"] = comboBoxPropertyCity.SelectedValue == null ? DBNull.Value : comboBoxPropertyCity.SelectedValue;
                    NewDR["address"] = userTextBoxPropertyAddress.Text;
                    NewDR["details"] = userTextBoxDetails.Text;
                    NewDR["administrator_name"] = userTextBoxAdministratorName.Text;
                    NewDR["administrator_phones"] = administrator_phones;
                    NewDR["administrator_emails"] = administrator_emails;
                    NewDR["president_owners_association_name"] = userTextBoxPropertyPresidentOwnereAssociationName.Text;
                    NewDR["president_owners_association_phones"] = president_owners_association_phones;
                    NewDR["president_owners_association_emails"] = president_owners_association_emails;
                    NewDR["owners_association_name"] = userTextBoxAssociationName.Text;
                    NewDR["owners_association_cif"] = userTextBoxAssociationCif.Text;
                    NewDR["owners_association_bank"] = userTextBoxAssociationBankName.Text;
                    NewDR["owners_association_bank_account"] = userTextBoxAssociationBankAccount.Text;
                    NewDR["maintenance_person_name"] = userTextBoxMaintenancePersonName.Text;
                    NewDR["maintenance_person_phones"] = maintenance_person_phones;
                    NewDR["maintenance_person_emails"] = maintenance_person_emails;
                    NewDR["internet_provider_information"] = userTextBoxInternetProviderInformation.Text;
                    NewDR["telephone_provider_information"] = userTextBoxTelephoneProviderInformation.Text;
                    NewDR["tv_provider_information"] = userTextBoxTVProviderInformation.Text;
                    NewDR["internet_provider_representative_information"] = userTextBoxInternetRepresentative.Text;
                    NewDR["telephone_provider_representative_information"] = userTextBoxTelephoneRepresentative.Text;
                    NewDR["tv_provider_representative_information"] = userTextBoxTvRepresentative.Text;
                    NewDR["developer_name"] = userTextBoxDevelopperName.Text;
                    NewDR["developer_contact_person"] = userTextBoxDevelopperContactPersonName.Text;
                    NewDR["developer_phones"] = developer_phones;
                    NewDR["developer_emails"] = developer_emails;
                    NewDR["condominium_fee"] = userTextBoxCondominiumFee.Text;
                    NewDR["floating_capital"] = userTextBoxFloatingCapital.Text;
                }
                else
                {
                    MySqlParameters.Clear();
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _NAME = new MySqlParameter("_NAME", userTextBoxPropertyName.Text); MySqlParameters.Add(_NAME);
                    MySqlParameter _LOCATION = new MySqlParameter("_LOCATION", userTextBoxPropertyLocation.Text); MySqlParameters.Add(_LOCATION);
                    MySqlParameter _CITY_ID = new MySqlParameter("_CITY_ID", comboBoxPropertyCity.SelectedValue == null ? DBNull.Value : comboBoxPropertyCity.SelectedValue); MySqlParameters.Add(_CITY_ID);
                    MySqlParameter _ADDRESS = new MySqlParameter("_ADDRESS", userTextBoxPropertyAddress.Text); MySqlParameters.Add(_ADDRESS);
                    MySqlParameter _DETAILS = new MySqlParameter("_DETAILS", userTextBoxDetails.Text); MySqlParameters.Add(_DETAILS);
                    MySqlParameter _ADMINISTRATOR_NAME = new MySqlParameter("_ADMINISTRATOR_NAME", userTextBoxAdministratorName.Text); MySqlParameters.Add(_ADMINISTRATOR_NAME);
                    MySqlParameter _ADMINISTRATOR_PHONES = new MySqlParameter("_ADMINISTRATOR_PHONES", administrator_phones); MySqlParameters.Add(_ADMINISTRATOR_PHONES);
                    MySqlParameter _ADMINISTRATOR_EMAILS = new MySqlParameter("_ADMINISTRATOR_EMAILS", administrator_emails); MySqlParameters.Add(_ADMINISTRATOR_EMAILS);
                    MySqlParameter _PRESIDENT_OWNERS_ASSOCIATION_NAME = new MySqlParameter("_PRESIDENT_OWNERS_ASSOCIATION_NAME", userTextBoxPropertyPresidentOwnereAssociationName.Text); MySqlParameters.Add(_PRESIDENT_OWNERS_ASSOCIATION_NAME);
                    MySqlParameter _PRESIDENT_OWNERS_ASSOCIATION_PHONES = new MySqlParameter("_PRESIDENT_OWNERS_ASSOCIATION_PHONES", president_owners_association_phones); MySqlParameters.Add(_PRESIDENT_OWNERS_ASSOCIATION_PHONES);
                    MySqlParameter _PRESIDENT_OWNERS_ASSOCIATION_EMAILS = new MySqlParameter("_PRESIDENT_OWNERS_ASSOCIATION_EMAILS", president_owners_association_emails); MySqlParameters.Add(_PRESIDENT_OWNERS_ASSOCIATION_EMAILS);
                    MySqlParameter _OWNERS_ASSOCIATION_NAME = new MySqlParameter("_OWNERS_ASSOCIATION_NAME", userTextBoxAssociationName.Text); MySqlParameters.Add(_OWNERS_ASSOCIATION_NAME);
                    MySqlParameter _OWNERS_ASSOCIATION_CIF = new MySqlParameter("_OWNERS_ASSOCIATION_CIF", userTextBoxAssociationCif.Text); MySqlParameters.Add(_OWNERS_ASSOCIATION_CIF);
                    MySqlParameter _OWNERS_ASSOCIATION_BANK = new MySqlParameter("_OWNERS_ASSOCIATION_BANK", userTextBoxAssociationBankName.Text); MySqlParameters.Add(_OWNERS_ASSOCIATION_BANK);
                    MySqlParameter _OWNERS_ASSOCIATION_BANK_ACCOUNT = new MySqlParameter("_OWNERS_ASSOCIATION_BANK_ACCOUNT", userTextBoxAssociationBankAccount.Text); MySqlParameters.Add(_OWNERS_ASSOCIATION_BANK_ACCOUNT);
                    MySqlParameter _MAINTENANCE_PERSON_NAME = new MySqlParameter("_MAINTENANCE_PERSON_NAME", userTextBoxMaintenancePersonName.Text); MySqlParameters.Add(_MAINTENANCE_PERSON_NAME);
                    MySqlParameter _MAINTENANCE_PERSON_PHONES = new MySqlParameter("_MAINTENANCE_PERSON_PHONES", maintenance_person_phones); MySqlParameters.Add(_MAINTENANCE_PERSON_PHONES);
                    MySqlParameter _MAINTENANCE_PERSON_EMAILS = new MySqlParameter("_MAINTENANCE_PERSON_EMAILS", maintenance_person_emails); MySqlParameters.Add(_MAINTENANCE_PERSON_EMAILS);
                    MySqlParameter _INTERNET_PROVIDER_INFORMATION = new MySqlParameter("_INTERNET_PROVIDER_INFORMATION", userTextBoxInternetProviderInformation.Text); MySqlParameters.Add(_INTERNET_PROVIDER_INFORMATION);
                    MySqlParameter _TELEPHONE_PROVIDER_INFORMATION = new MySqlParameter("_TELEPHONE_PROVIDER_INFORMATION", userTextBoxTelephoneProviderInformation.Text); MySqlParameters.Add(_TELEPHONE_PROVIDER_INFORMATION);
                    MySqlParameter _TV_PROVIDER_INFORMATION = new MySqlParameter("_TV_PROVIDER_INFORMATION", userTextBoxTVProviderInformation.Text); MySqlParameters.Add(_TV_PROVIDER_INFORMATION);
                    MySqlParameter _INTERNET_PROVIDER_REPRESENTATIVE_INFORMATION = new MySqlParameter("_INTERNET_PROVIDER_REPRESENTATIVE_INFORMATION", userTextBoxInternetRepresentative.Text); MySqlParameters.Add(_INTERNET_PROVIDER_REPRESENTATIVE_INFORMATION);
                    MySqlParameter _TELEPHONE_PROVIDER_REPRESENTATIVE_INFORMATION = new MySqlParameter("_TELEPHONE_PROVIDER_REPRESENTATIVE_INFORMATION", userTextBoxTelephoneRepresentative.Text); MySqlParameters.Add(_TELEPHONE_PROVIDER_REPRESENTATIVE_INFORMATION);
                    MySqlParameter _TV_PROVIDER_REPRESENTATIVE_INFORMATION = new MySqlParameter("_TV_PROVIDER_REPRESENTATIVE_INFORMATION", userTextBoxTvRepresentative.Text); MySqlParameters.Add(_TV_PROVIDER_REPRESENTATIVE_INFORMATION);
                    MySqlParameter _DEVELOPER_NAME = new MySqlParameter("_DEVELOPER_NAME", userTextBoxDevelopperName.Text); MySqlParameters.Add(_DEVELOPER_NAME);
                    MySqlParameter _DEVELOPER_CONTACT_PERSON = new MySqlParameter("_DEVELOPER_CONTACT_PERSON", userTextBoxDevelopperContactPersonName.Text); MySqlParameters.Add(_DEVELOPER_CONTACT_PERSON);
                    MySqlParameter _DEVELOPER_PHONES = new MySqlParameter("_DEVELOPER_PHONES", developer_phones); MySqlParameters.Add(_DEVELOPER_PHONES);
                    MySqlParameter _DEVELOPER_EMAILS = new MySqlParameter("_DEVELOPER_EMAILS", developer_emails); MySqlParameters.Add(_DEVELOPER_EMAILS);
                    MySqlParameter _CONDOMINIUM_FEE = new MySqlParameter("_CONDOMINIUM_FEE", userTextBoxCondominiumFee.Text); MySqlParameters.Add(_CONDOMINIUM_FEE);
                    MySqlParameter _FLOATING_CAPITAL = new MySqlParameter("_FLOATING_CAPITAL", userTextBoxFloatingCapital.Text); MySqlParameters.Add(_FLOATING_CAPITAL);
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROJECTSsp_insert", MySqlParameters.ToArray());
                    da.ExecuteInsertQuery();
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
            /*
            if (TenantDR.RowState == DataRowState.Added) //add from selection
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

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateData()
        {
            bool toReturn = true;
            errorProvider1.SetError(userTextBoxPropertyName, "");
            errorProvider1.SetError(userTextBoxCondominiumFee, "");
            errorProvider1.SetError(userTextBoxFloatingCapital, "");

            if (userTextBoxPropertyName.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxPropertyName, Language.GetErrorText("errorEmptyPropertyName", "Property Name can not by empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxPropertyName.Name, Language.GetErrorText("errorEmptyPropertyName", "Property Name can not by empty!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxCondominiumFee.Text))
            {
                errorProvider1.SetError(userTextBoxCondominiumFee, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxCondominiumFee.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxFloatingCapital.Text))
            {
                errorProvider1.SetError(userTextBoxFloatingCapital, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxFloatingCapital.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            return toReturn;
        }


        private void toolStripButtonAddAdministratorPhone_Click(object sender, EventArgs e)
        {
            if (userTextBoxPropertyAdministratorPhoneEdit.Text.Trim() != "")
            {
                if (Validator.IsPhoneNumber(userTextBoxPropertyAdministratorPhoneEdit.Text.Trim()))
                {
                    listBoxPropertyAdministratorPhones.Items.Add(userTextBoxPropertyAdministratorPhoneEdit.Text.Trim());
                    errorProvider1.SetError(toolStrip1, "");
                }
                else
                {
                    errorProvider1.SetError(toolStrip1, Language.GetErrorText("errorInvalidPhoneNumber", "Invalid phone number!"));
                }
            }
        }

        private void toolStripButtonDeleteAdministratorPhone_Click(object sender, EventArgs e)
        {
            if (listBoxPropertyAdministratorPhones.SelectedIndex > -1)
            {
                listBoxPropertyAdministratorPhones.Items.RemoveAt(listBoxPropertyAdministratorPhones.SelectedIndex);
            }
        }

        private void toolStripButtonAddAdministratorEmail_Click(object sender, EventArgs e)
        {
            if (userTextBoxPropertyAdministartorEmailEdit.Text.Trim() != "")
            {
                if(Validator.IsEmail(userTextBoxPropertyAdministartorEmailEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip2, "");
                    listBoxPropertyAdministratorEmails.Items.Add(userTextBoxPropertyAdministartorEmailEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip2, Language.GetErrorText("errorInvalidEmail", "Invalid email address!"));
                }
            }
        }

        private void toolStripButtonDeleteAdministratorEmail_Click(object sender, EventArgs e)
        {
            if (listBoxPropertyAdministratorEmails.SelectedIndex > -1)
            {
                listBoxPropertyAdministratorEmails.Items.RemoveAt(listBoxPropertyAdministratorEmails.SelectedIndex);
            }
        }


        private void toolStripButtonAddPresidentOwnerAssociationPhone_Click(object sender, EventArgs e)
        {
            if (toolStripTextBoxPropertyPresidentOwnerAssociationPhoneEdit.Text.Trim() != "")
            {
                if (Validator.IsPhoneNumber(toolStripTextBoxPropertyPresidentOwnerAssociationPhoneEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip6, "");
                    listBoxPresidentOwnerAssociationPhones.Items.Add(toolStripTextBoxPropertyPresidentOwnerAssociationPhoneEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip6, Language.GetErrorText("errorInvalidPhoneNumber", "Invalid phone number!"));
                }
            }
        }

        private void toolStripButtonDeletePresidentOwnerAssociationPhone_Click(object sender, EventArgs e)
        {
            if (listBoxPresidentOwnerAssociationPhones.SelectedIndex > -1)
            {
                listBoxPresidentOwnerAssociationPhones.Items.RemoveAt(listBoxPresidentOwnerAssociationPhones.SelectedIndex);
            }
        }

        private void toolStripButtonAddPresidentOwnerAssociationEmail_Click(object sender, EventArgs e)
        {
            if (toolStripTextBoxPropertyPresidentOwnerAssociationEmailEdit.Text.Trim() != "")
            {
                if (Validator.IsEmail(toolStripTextBoxPropertyPresidentOwnerAssociationEmailEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip5, "");
                    listBoxPresidentOwnerAssociationEmails.Items.Add(toolStripTextBoxPropertyPresidentOwnerAssociationEmailEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip5, Language.GetErrorText("errorInvalidEmail", "Invalid email address!"));
                }
            }
        }

        private void toolStripButtonDeletePresidentOwnerAssociationEmail_Click(object sender, EventArgs e)
        {
            if (listBoxPresidentOwnerAssociationEmails.SelectedIndex > -1)
            {
                listBoxPresidentOwnerAssociationEmails.Items.RemoveAt(listBoxPresidentOwnerAssociationEmails.SelectedIndex);
            }
        }

        private void toolStripButtonAddMaintenancePersonPhone_Click(object sender, EventArgs e)
        {
            if (toolStripTextBoxMaintenancePersonPhoneEdit.Text.Trim() != "")
            {
                if (Validator.IsPhoneNumber(toolStripTextBoxMaintenancePersonPhoneEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip8, "");
                    listBoxMaintenancePersonPhones.Items.Add(toolStripTextBoxMaintenancePersonPhoneEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip8, Language.GetErrorText("errorInvalidPhoneNumber", "Invalid phone number!"));
                }
            }
        }

        private void toolStripButtonDeleteMaintenancePersonPhone_Click(object sender, EventArgs e)
        {
            if (listBoxMaintenancePersonPhones.SelectedIndex > -1)
            {
                listBoxMaintenancePersonPhones.Items.RemoveAt(listBoxMaintenancePersonPhones.SelectedIndex);
            }
        }

        private void toolStripButtonAddMaintenancePersonEmail_Click(object sender, EventArgs e)
        {
            if (toolStripTextBoxMaintenancePersonEmailEdit.Text.Trim() != "")
            {
                if (Validator.IsEmail(toolStripTextBoxMaintenancePersonEmailEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip7, "");
                    listBoxMaintenancePersonEmails.Items.Add(toolStripTextBoxMaintenancePersonEmailEdit.Text.Trim());
                }
                else 
                {
                    errorProvider1.SetError(toolStrip7, Language.GetErrorText("errorInvalidEmail", "Invalid email address!"));
                }
            }
        }

        private void toolStripButtonDeleteMaintenancePersonEmail_Click(object sender, EventArgs e)
        {
            if (listBoxMaintenancePersonEmails.SelectedIndex > -1)
            {
                listBoxMaintenancePersonEmails.Items.RemoveAt(listBoxMaintenancePersonEmails.SelectedIndex);
            }
        }

        private void toolStripButtonAddDevelopperPhone_Click(object sender, EventArgs e)
        {
            if (toolStripTextBoxDevelopperPhoneEdit.Text.Trim() != "")
            {
                if (Validator.IsPhoneNumber(toolStripTextBoxDevelopperPhoneEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip4, "");
                    listBoxDevelopperPhones.Items.Add(toolStripTextBoxDevelopperPhoneEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip4, Language.GetErrorText("errorInvalidPhoneNumber", "Invalid phone number!"));
                }
            }
        }

        private void toolStripButtonDeleteDevelopperPhone_Click(object sender, EventArgs e)
        {
            if (listBoxDevelopperPhones.SelectedIndex > -1)
            {
                listBoxDevelopperPhones.Items.RemoveAt(listBoxDevelopperPhones.SelectedIndex);
            }
        }

        private void toolStripButtonAddDevelopperEmail_Click(object sender, EventArgs e)
        {
            if (toolStripTextBoxDevelopperEmailEdit.Text.Trim() != "")
            {
                if (Validator.IsEmail(toolStripTextBoxDevelopperEmailEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip3, "");
                    listBoxDevelopperEmails.Items.Add(toolStripTextBoxDevelopperEmailEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip3, Language.GetErrorText("errorInvalidEmail", "Invalid email address!"));
                }
            }
        }

        private void toolStripButtonDeleteDevelopperEmail_Click(object sender, EventArgs e)
        {
            if (listBoxDevelopperEmails.SelectedIndex > -1)
            {
                listBoxDevelopperEmails.Items.RemoveAt(listBoxDevelopperEmails.SelectedIndex);
            }
        }
    }
}
