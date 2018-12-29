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
using System.Configuration;
using System.Security.Cryptography;
using System.IO;

namespace FDP
{
    public partial class Owners : UserForm
    {
        public DataAccess daCoOwners = new DataAccess();
        public CheckedListBox columns = new CheckedListBox();
        private int AttemptsLeft;

        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();

        public Owners()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
        }

        public Owners(int id)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            daCoOwners = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_by_id", new object[] { new MySqlParameter("_ID", id) });
            NewDR = daCoOwners.ExecuteSelectQuery().Tables[0].Rows[0];
            buttonSaveOwner.Enabled = false;
            groupBoxCoOwners.Enabled = false;
        }

        public Owners(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
            NewDR = dr;
        }

        private void Owners_Load(object sender, EventArgs e)
        {
            dataGridViewCoOwners.BindingContext = null;
            errorProvider1.SetIconAlignment(toolStrip1, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(toolStrip2, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxOwnerPhones, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(listBoxOwnerEmails, ErrorIconAlignment.MiddleLeft);

            FillCombos();
            if (NewDR != null)
            {
                FillInfo();
            }
            else
            {
                dateTimePickerOwnerPassportExpirationDate.Value = DateTime.Now; 
            }
            //labelStartingBallance1.Visible = userTextBoxStartingBallance1.Visible = (NewDR == null || NewDR.RowState == DataRowState.Detached);
            labelStartingBallance1.Visible = userTextBoxStartingBallance1.Visible = true;
            //labelStartingBallance2.Visible = userTextBoxStartingBallance2.Visible = (NewDR == null || NewDR.RowState == DataRowState.Detached);
            labelStartingBallance2.Visible = userTextBoxStartingBallance2.Visible = true;
            //labelStartingBallance3.Visible = userTextBoxStartingBallance3.Visible = (NewDR == null || NewDR.RowState == DataRowState.Detached);
            labelStartingBallance3.Visible = userTextBoxStartingBallance3.Visible = true;
            //labelCashStartingBallance1.Visible = userTextBoxCashStartingBallance1.Visible = (NewDR == null || NewDR.RowState == DataRowState.Detached);
            labelCashStartingBallance1.Visible = userTextBoxCashStartingBallance1.Visible = true;
            //labelCashStartingBallance2.Visible = userTextBoxCashStartingBallance2.Visible = (NewDR == null || NewDR.RowState == DataRowState.Detached);
            labelCashStartingBallance2.Visible = userTextBoxCashStartingBallance2.Visible = true;
            //labelCashStartingBallance3.Visible = userTextBoxCashStartingBallance3.Visible = (NewDR == null || NewDR.RowState == DataRowState.Detached);
            labelCashStartingBallance3.Visible = userTextBoxCashStartingBallance3.Visible = true;

            groupBoxCoOwners.Enabled = !(NewDR == null || NewDR.RowState == DataRowState.Added || NewDR.RowState == DataRowState.Detached);
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
            try
            {
                if (SettingsClass.LoginOwnerId > 0)
                {
                    labelOldUsername.Visible = true;
                    labelOldPassword.Visible = true;
                    userTextBoxOldUserName.Visible = true;
                    userTextBoxOldPassword.Visible = true;
                    checkBoxAutoLogin.Visible = true;
                    checkBoxRememberName.Visible = true;
                    checkBoxAutoLogin.Checked = SettingsClass.Autologin;
                    checkBoxRememberName.Checked = SettingsClass.RememberName;
                    AttemptsLeft = 3;
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
            base.Opacity = 100;
        }

        private void CheckPasswordExpiration()
        {
            try
            {
                if (DateTime.Now.AddMonths(3) > dateTimePickerOwnerPassportExpirationDate.Value)
                {
                    //MessageBox.Show(Language.GetMessageBoxText("passportExpirationWarning", "Owner's passport will expire in less than 3 months!"), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    errorProvider1.SetError(dateTimePickerOwnerPassportExpirationDate, Language.GetMessageBoxText("passportExpirationWarning", "Owner's passport will expire in less than 3 months!"));
                    base.WarningList.Add(new KeyValuePair<string, string>(dateTimePickerOwnerPassportExpirationDate.Name, Language.GetErrorText("passportExpirationWarning", "Owner's passport will expire in less than 3 months!")));
                    base.ShowWarningsDialog(Language.GetErrorText("warningsOnPage", "WARNING!"));
                }
            }
            catch { }
        }

        private void FillCombos()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "OWNER_STATUS") });
            DataTable dtStatuses = da.ExecuteSelectQuery().Tables[0];
            if (dtStatuses != null)
            {
                comboBoxOwnerStatus.DisplayMember = "name";
                comboBoxOwnerStatus.ValueMember = "id";
                comboBoxOwnerStatus.DataSource = dtStatuses;
            }
            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "OWNER_TYPE") });
            DataTable dtTypes = da.ExecuteSelectQuery().Tables[0];
            if (dtTypes != null)
            {
                comboBoxOwnerType.DisplayMember = "name";
                comboBoxOwnerType.ValueMember = "id";
                comboBoxOwnerType.DataSource = dtTypes;
            }
            da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_currencies_list");
            DataTable dtc1 = da.ExecuteSelectQuery().Tables[0];
            if (dtc1 != null)
            {
                comboBoxOwnerCurrency1.DisplayMember = "currency";
                comboBoxOwnerCurrency1.ValueMember = "currency";
                comboBoxOwnerCurrency1.DataSource = dtc1;
            }
            da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_currencies_list");
            DataTable dtc2 = da.ExecuteSelectQuery().Tables[0];
            if (dtc2 != null)
            {
                comboBoxOwnerCurrency2.DisplayMember = "currency";
                comboBoxOwnerCurrency2.ValueMember = "currency";
                comboBoxOwnerCurrency2.DataSource = dtc2;
            }
            da = new DataAccess(CommandType.StoredProcedure, "CURRENCIESsp_get_currencies_list");
            DataTable dtc3 = da.ExecuteSelectQuery().Tables[0];
            if (dtc3 != null)
            {
                comboBoxOwnerCurrency3.DisplayMember = "currency";
                comboBoxOwnerCurrency3.ValueMember = "currency";
                comboBoxOwnerCurrency3.DataSource = dtc3;
            }
            da = new DataAccess(CommandType.StoredProcedure, "BANKSsp_list");
            DataTable dto1 = da.ExecuteSelectQuery().Tables[0];
            if (dto1 != null)
            {
                comboBoxBank1.DisplayMember = "name";
                comboBoxBank1.ValueMember = "id";
                comboBoxBank1.DataSource = dto1;
            }
            da = new DataAccess(CommandType.StoredProcedure, "BANKSsp_list");
            DataTable dto2 = da.ExecuteSelectQuery().Tables[0];
            if (dto2 != null)
            {
                comboBoxBank2.DisplayMember = "name";
                comboBoxBank2.ValueMember = "id";
                comboBoxBank2.DataSource = dto2;
            }
            da = new DataAccess(CommandType.StoredProcedure, "BANKSsp_list");
            DataTable dto3 = da.ExecuteSelectQuery().Tables[0];
            if (dto3 != null)
            {
                comboBoxBank3.DisplayMember = "name";
                comboBoxBank3.ValueMember = "id";
                comboBoxBank3.DataSource = dto3;
            }
            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "OWNER_LANGUAGE") });
            DataTable dtLanguages1 = da.ExecuteSelectQuery().Tables[0];
            if (dtLanguages1 != null)
            {
                comboBoxLanguage1.DisplayMember = "name";
                comboBoxLanguage1.ValueMember = "id";
                comboBoxLanguage1.DataSource = dtLanguages1;
            }
            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "OWNER_LANGUAGE") });
            DataTable dtLanguages2 = da.ExecuteSelectQuery().Tables[0];
            if (dtLanguages2 != null)
            {
                comboBoxLanguage2.DisplayMember = "name";
                comboBoxLanguage2.ValueMember = "id";
                comboBoxLanguage2.DataSource = dtLanguages2;
            }
            try
            {
                comboBoxOwnerCurrency1.SelectedIndex = 0;
                comboBoxOwnerCurrency2.SelectedIndex = 1;
                comboBoxOwnerCurrency3.SelectedIndex = 2;
            }
            catch { }
        }

        private void FillInfo()
        {
            try
            {
                //if (NewDR.RowState != DataRowState.Added && NewDR != null)
                if (NewDR != null)
                {
                    if (NewDR.RowState != DataRowState.Detached && NewDR.RowState != DataRowState.Added)
                    {
                        comboBoxOwnerType.Enabled = false;
                        string owner_type = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", NewDR["type_id"]) })).ExecuteScalarQuery().ToString();
                        switch (owner_type.ToLower())
                        {
                            case "individual":
                                labelTransferredByCompany.Text = labelTransferredByCompany.Text.Replace("/", "").Replace(Language.GetLabelText("individual", "individual"), "");
                                break;
                            case "company":
                                labelTransferredByCompany.Text = labelTransferredByCompany.Text.Replace("/", "").Replace(Language.GetLabelText("company", "company"), "");
                                break;
                        }
                    }
                    FillCoOwners();
                    userTextBoxOwnerName.Text = NewDR["name"].ToString();
                    userTextBoxOwnerFullName.Text = NewDR["full_name"].ToString();
                    comboBoxOwnerStatus.SelectedValue = NewDR["status_id"];
                    try
                    {
                        comboBoxOwnerType.SelectedValue = NewDR["type_id"];
                    }
                    catch { }
                    userTextBoxOwnerCif.Text = NewDR["cif"].ToString();
                    userTextBoxNIF.Text = NewDR["nif"].ToString();
                    userTextBoxOwnerCnp.Text = NewDR["cnp"].ToString();
                    userTextBoxOwnerCui.Text = NewDR["cui"].ToString();
                    userTextBoxOwnerRegComNumber.Text = NewDR["commercial_register_number"].ToString();
                    userTextBoxOwnerPassportNumber.Text = NewDR["passport_number"].ToString();
                    try
                    {
                        dateTimePickerOwnerPassportExpirationDate.Value = Convert.ToDateTime(NewDR["passport_expiration_date"]);
                    }
                    catch { dateTimePickerOwnerPassportExpirationDate.Value = DateTime.Now; }

                    userTextBoxAddress.Text = NewDR["address"].ToString();
                    userTextBoxDistrict.Text = NewDR["district"].ToString();
                    string[] phones = NewDR["phones"].ToString().Split(';');
                    foreach (string phone in phones)
                    {
                        if (phone.Trim() != "")
                            listBoxOwnerPhones.Items.Add(phone);
                    }
                    string[] emails = NewDR["emails"].ToString().Split(';');
                    foreach (string email in emails)
                    {
                        if (email.Trim() != "")
                            listBoxOwnerEmails.Items.Add(email);
                    }

                    //userTextBoxOwnerBankName1.Text = NewDR["bank_name1"].ToString();
                    try
                    {
                        comboBoxBank1.SelectedValue = NewDR["bank_id1"].ToString();
                    }
                    catch { }
                    userTextBoxOwnerBankAccount1.Text = NewDR["bank_account_details1"].ToString();
                    if (NewDR["bank_account_currency1"] != DBNull.Value && NewDR["bank_account_currency1"] != null)
                    {
                        comboBoxOwnerCurrency1.SelectedValue = NewDR["bank_account_currency1"].ToString();
                    }
                    else
                    {
                        comboBoxOwnerCurrency1.SelectedIndex = 0;
                    }
                    //userTextBoxOwnerBankName2.Text = NewDR["bank_name2"].ToString();
                    try
                    {
                        comboBoxBank2.SelectedValue = NewDR["bank_id2"].ToString();
                    }
                    catch { }
                    userTextBoxOwnerBankAccount2.Text = NewDR["bank_account_details2"].ToString();
                    if (NewDR["bank_account_currency2"] != DBNull.Value && NewDR["bank_account_currency2"] != null)
                    {
                        comboBoxOwnerCurrency2.SelectedValue = NewDR["bank_account_currency2"].ToString();
                    }
                    else
                    {
                        comboBoxOwnerCurrency2.SelectedIndex = 1;
                    }
                    //userTextBoxOwnerBankName3.Text = NewDR["bank_name3"].ToString();
                    try
                    {
                        comboBoxBank3.SelectedValue = NewDR["bank_id3"].ToString();
                    }
                    catch { }
                    userTextBoxOwnerBankAccount3.Text = NewDR["bank_account_details3"].ToString();
                    if (NewDR["bank_account_currency3"] != DBNull.Value && NewDR["bank_account_currency3"] != null)
                    {
                        comboBoxOwnerCurrency3.SelectedValue = NewDR["bank_account_currency3"].ToString();
                    }
                    else
                    {
                        comboBoxOwnerCurrency3.SelectedIndex = 2;
                    }

                    userTextBoxOwnerRecommendedBy.Text = NewDR["recommended_by"].ToString();
                    userTextBoxOwnerLawyer.Text = NewDR["lawyer_information"].ToString();
                    userTextBoxOwnerAccountant.Text = NewDR["accountant_information"].ToString();
                    userTextBoxOtherPersons.Text = NewDR["other_persons_information"].ToString();

                    userTextBoxOwnerComments.Text = NewDR["comments"].ToString();

                    userTextBoxStartingBallance1.Text = NewDR["starting_ballance1"].ToString();
                    try
                    {
                        dateTimePickerStartingBallanceDate1.Value = Convert.ToDateTime(NewDR["starting_ballance_date1"]);
                    }
                    catch { dateTimePickerStartingBallanceDate1.Value = DateTime.Now; }
                    userTextBoxStartingBallance2.Text = NewDR["starting_ballance2"].ToString();
                    try
                    {
                        dateTimePickerStartingBallanceDate2.Value = Convert.ToDateTime(NewDR["starting_ballance_date2"]);
                    }
                    catch { dateTimePickerStartingBallanceDate2.Value = DateTime.Now; }
                    userTextBoxStartingBallance3.Text = NewDR["starting_ballance3"].ToString();
                    try
                    {
                        dateTimePickerStartingBallanceDate3.Value = Convert.ToDateTime(NewDR["starting_ballance_date3"]);
                    }
                    catch { dateTimePickerStartingBallanceDate3.Value = DateTime.Now; }

                    userTextBoxCashStartingBallance1.Text = NewDR["cash_starting_ballance1"].ToString();
                    try
                    {
                        dateTimePickerCashStartingBallanceDate1.Value = Convert.ToDateTime(NewDR["cash_starting_ballance_date1"]);
                    }
                    catch { dateTimePickerCashStartingBallanceDate1.Value = DateTime.Now; }
                    userTextBoxCashStartingBallance2.Text = NewDR["cash_starting_ballance2"].ToString();
                    try
                    {
                        dateTimePickerCashStartingBallanceDate2.Value = Convert.ToDateTime(NewDR["cash_starting_ballance_date2"]);
                    }
                    catch { dateTimePickerCashStartingBallanceDate2.Value = DateTime.Now; }
                    userTextBoxCashStartingBallance3.Text = NewDR["cash_starting_ballance3"].ToString();
                    try
                    {
                        dateTimePickerCashStartingBallanceDate3.Value = Convert.ToDateTime(NewDR["cash_starting_ballance_date3"]);
                    }
                    catch { dateTimePickerCashStartingBallanceDate3.Value = DateTime.Now; }
                    userTextBoxAdministrator.Text = NewDR["administrator_information"].ToString();
                    // --
                    try
                    {
                        comboBoxLanguage1.SelectedValue = NewDR["language_id1"].ToString();
                    }
                    catch { }
                    try
                    {
                        comboBoxLanguage2.SelectedValue = NewDR["language_id2"].ToString();
                    }
                    catch { }
                    checkBoxClosedByCompany.Checked = Convert.ToBoolean(NewDR["closed_by_company"] == DBNull.Value ? false : NewDR["closed_by_company"]);
                    checkBoxTransferredByCompany.Checked = Convert.ToBoolean(NewDR["transferred_by_company"] == DBNull.Value ? false : NewDR["transferred_by_company"]);
                    checkBoxPoaNifAndBankAccounts.Checked = Convert.ToBoolean(NewDR["poa_nif_and_bank_accounts_by_company"] == DBNull.Value ? false : NewDR["poa_nif_and_bank_accounts_by_company"]);
                    checkBoxHeadquartersChangedByCompany.Checked = Convert.ToBoolean(NewDR["headquarters_changed_by_company"] == DBNull.Value ? false : NewDR["headquarters_changed_by_company"]);

                    if(NewDR.RowState != DataRowState.Detached && NewDR.RowState != DataRowState.Added)
                        CheckPasswordExpiration();
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
                    foreach (string phone in listBoxOwnerPhones.Items)
                        phones += String.Format("{0};", phone);
                    phones = phones.Substring(0, phones.Length- 1);
                }
                catch { }
                string emails = "";
                try
                {
                    foreach (string email in listBoxOwnerEmails.Items)
                        emails += String.Format("{0};", email);
                    emails = emails.Substring(0, emails.Length- 1);
                }
                catch { }

                if (NewDR != null)
                {
                    NewDR["name"] = userTextBoxOwnerName.Text;
                    NewDR["full_name"] = userTextBoxOwnerFullName.Text;
                    //NewDR["status_id"] = comboBoxOwnerStatus.SelectedValue;
                    NewDR["status_id"] = CommonFunctions.SetNullable(comboBoxOwnerStatus);
                    //NewDR["type_id"] = comboBoxOwnerType.SelectedValue;
                    NewDR["type_id"] = CommonFunctions.SetNullable(comboBoxOwnerType);
                    NewDR["cif"] = userTextBoxOwnerCif.Text;
                    NewDR["nif"] = userTextBoxNIF.Text;
                    NewDR["cnp"] = userTextBoxOwnerCnp.Text;
                    NewDR["cui"] = userTextBoxOwnerCui.Text;
                    NewDR["commercial_register_number"] = userTextBoxOwnerRegComNumber.Text;
                    NewDR["passport_number"] = userTextBoxOwnerPassportNumber.Text;
                    NewDR["passport_expiration_date"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerOwnerPassportExpirationDate.Value);
                    NewDR["address"] = userTextBoxAddress.Text;
                    NewDR["district"] = userTextBoxDistrict.Text;
                    NewDR["phones"] = phones;
                    NewDR["emails"] = emails;
                    //NewDR["bank_name1"] = userTextBoxOwnerBankName1.Text;
                    NewDR["bank_id1"] = CommonFunctions.SetNullable(comboBoxBank1);
                    NewDR["bank_account_details1"] = userTextBoxOwnerBankAccount1.Text;
                    //NewDR["bank_account_currency1"] = radioButtonRON1.Checked ? null : comboBoxOwnerCurrency1.SelectedValue;
                    NewDR["bank_account_currency1"] = comboBoxOwnerCurrency1.SelectedValue;
                    //NewDR["bank_name2"] = userTextBoxOwnerBankName2.Text;
                    NewDR["bank_id2"] = CommonFunctions.SetNullable(comboBoxBank2);
                    NewDR["bank_account_details2"] = userTextBoxOwnerBankAccount2.Text;
                    //NewDR["bank_account_currency2"] = radioButtonRON2.Checked ? null : comboBoxOwnerCurrency2.SelectedValue;
                    NewDR["bank_account_currency2"] = comboBoxOwnerCurrency2.SelectedValue;
                    //NewDR["bank_name3"] = userTextBoxOwnerBankName3.Text;
                    NewDR["bank_id3"] = CommonFunctions.SetNullable(comboBoxBank3);
                    NewDR["bank_account_details3"] = userTextBoxOwnerBankAccount3.Text;
                    //NewDR["bank_account_currency3"] = radioButtonRON3.Checked ? null : comboBoxOwnerCurrency3.SelectedValue;
                    NewDR["bank_account_currency3"] = comboBoxOwnerCurrency3.SelectedValue;
                    NewDR["recommended_by"] = userTextBoxOwnerRecommendedBy.Text;
                    NewDR["lawyer_information"] = userTextBoxOwnerLawyer.Text;
                    NewDR["accountant_information"] = userTextBoxOwnerAccountant.Text;
                    NewDR["other_persons_information"] = userTextBoxOtherPersons.Text;
                    NewDR["comments"] = userTextBoxOwnerComments.Text;
                    try
                    {
                        NewDR["starting_ballance1"] = userTextBoxStartingBallance1.Text.Trim() == "" ? "0" : userTextBoxStartingBallance1.Text;
                        NewDR["starting_ballance2"] = userTextBoxStartingBallance2.Text.Trim() == "" ? "0" : userTextBoxStartingBallance2.Text;
                        NewDR["starting_ballance3"] = userTextBoxStartingBallance3.Text.Trim() == "" ? "0" : userTextBoxStartingBallance3.Text;
                        NewDR["starting_ballance_date1"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerStartingBallanceDate1.Value);
                        NewDR["starting_ballance_date2"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerStartingBallanceDate2.Value);
                        NewDR["starting_ballance_date3"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerStartingBallanceDate3.Value);

                        NewDR["cash_starting_ballance1"] = userTextBoxCashStartingBallance1.Text.Trim() == "" ? "0" : userTextBoxCashStartingBallance1.Text;
                        NewDR["cash_starting_ballance2"] = userTextBoxCashStartingBallance2.Text.Trim() == "" ? "0" : userTextBoxCashStartingBallance2.Text;
                        NewDR["cash_starting_ballance3"] = userTextBoxCashStartingBallance3.Text.Trim() == "" ? "0" : userTextBoxCashStartingBallance3.Text;
                        NewDR["cash_starting_ballance_date1"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerCashStartingBallanceDate1.Value);
                        NewDR["cash_starting_ballance_date2"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerCashStartingBallanceDate2.Value);
                        NewDR["cash_starting_ballance_date3"] = CommonFunctions.ToMySqlFormatDate(dateTimePickerCashStartingBallanceDate3.Value);
                    }
                    catch { }
                    NewDR["administrator_information"] = userTextBoxAdministrator.Text;
                    // --
                    try
                    {
                        NewDR["language_id1"] = comboBoxLanguage1.SelectedValue;
                    }catch{}
                    try
                    {
                        NewDR["language_id2"] = comboBoxLanguage2.SelectedValue;
                    }
                    catch { }
                    NewDR["closed_by_company"] = checkBoxClosedByCompany.Checked;
                    NewDR["transferred_by_company"] = checkBoxTransferredByCompany.Checked;
                    NewDR["poa_nif_and_bank_accounts_by_company"] = checkBoxPoaNifAndBankAccounts.Checked;
                    NewDR["headquarters_changed_by_company"] = checkBoxHeadquartersChangedByCompany.Checked;
                }
                else
                {
                    MySqlParameters.Clear();
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _NAME = new MySqlParameter("_NAME", userTextBoxOwnerName.Text);
                    MySqlParameters.Add(_NAME);

                    MySqlParameter _FULL_NAME = new MySqlParameter("_FULL_NAME", userTextBoxOwnerFullName.Text);
                    MySqlParameters.Add(_FULL_NAME);

                    //MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", comboBoxOwnerStatus.SelectedValue);
                    MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", CommonFunctions.SetNullable(comboBoxOwnerStatus));
                    MySqlParameters.Add(_STATUS_ID);

                    //MySqlParameter _TYPE_ID = new MySqlParameter("_TYPE_ID", comboBoxOwnerType.SelectedValue);
                    MySqlParameter _TYPE_ID = new MySqlParameter("_TYPE_ID", CommonFunctions.SetNullable(comboBoxOwnerType));
                    MySqlParameters.Add(_TYPE_ID);

                    MySqlParameter _CIF = new MySqlParameter("_CIF", userTextBoxOwnerCif.Text);
                    MySqlParameters.Add(_CIF);

                    MySqlParameter _NIF = new MySqlParameter("_NIF", userTextBoxNIF.Text);
                    MySqlParameters.Add(_NIF);

                    MySqlParameter _CNP = new MySqlParameter("_CNP", userTextBoxOwnerCnp.Text);
                    MySqlParameters.Add(_CNP);

                    MySqlParameter _CUI = new MySqlParameter("_CUI", userTextBoxOwnerCui.Text);
                    MySqlParameters.Add(_CUI);

                    MySqlParameter _COMMERCIAL_REGISTER_NUMBER = new MySqlParameter("_COMMERCIAL_REGISTER_NUMBER", userTextBoxOwnerRegComNumber.Text);
                    MySqlParameters.Add(_COMMERCIAL_REGISTER_NUMBER);

                    MySqlParameter _PASSPORT_NUMBER = new MySqlParameter("_PASSPORT_NUMBER", userTextBoxOwnerPassportNumber.Text);
                    MySqlParameters.Add(_PASSPORT_NUMBER);

                    MySqlParameter _PASSPORT_EXPIRATION_DATE = new MySqlParameter("_PASSPORT_EXPIRATION_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerOwnerPassportExpirationDate.Value));
                    MySqlParameters.Add(_PASSPORT_EXPIRATION_DATE);

                    MySqlParameter _ADDRESS = new MySqlParameter("_ADDRESS", userTextBoxAddress.Text);
                    MySqlParameters.Add(_ADDRESS);

                    MySqlParameter _DISTRICT = new MySqlParameter("_DISTRICT", userTextBoxDistrict.Text);
                    MySqlParameters.Add(_DISTRICT);

                    MySqlParameter _PHONES = new MySqlParameter("_PHONES", phones);
                    MySqlParameters.Add(_PHONES);

                    MySqlParameter _EMAILS = new MySqlParameter("_EMAILS", emails);
                    MySqlParameters.Add(_EMAILS);

                    //MySqlParameter _BANK_NAME1 = new MySqlParameter("_BANK_NAME1", userTextBoxOwnerBankName1.Text);
                    MySqlParameter _BANK_ID1 = new MySqlParameter("_BANK_ID1", CommonFunctions.SetNullable(comboBoxBank1));
                    MySqlParameters.Add(_BANK_ID1);

                    MySqlParameter _BANK_ACCOUNT_DETAILS1 = new MySqlParameter("_BANK_ACCOUNT_DETAILS1", userTextBoxOwnerBankAccount1.Text);
                    MySqlParameters.Add(_BANK_ACCOUNT_DETAILS1);

                    //MySqlParameter _BANK_ACCOUNT_CURRENCY1 = new MySqlParameter("_BANK_ACCOUNT_CURRENCY1", radioButtonRON1.Checked ? null : comboBoxOwnerCurrency1.SelectedValue);
                    MySqlParameter _BANK_ACCOUNT_CURRENCY1 = new MySqlParameter("_BANK_ACCOUNT_CURRENCY1", comboBoxOwnerCurrency1.SelectedValue);
                    MySqlParameters.Add(_BANK_ACCOUNT_CURRENCY1);

                    //MySqlParameter _BANK_NAME2 = new MySqlParameter("_BANK_NAME2", userTextBoxOwnerBankName2.Text);
                    MySqlParameter _BANK_ID2 = new MySqlParameter("_BANK_ID2", CommonFunctions.SetNullable(comboBoxBank2));
                    MySqlParameters.Add(_BANK_ID2);

                    MySqlParameter _BANK_ACCOUNT_DETAILS2 = new MySqlParameter("_BANK_ACCOUNT_DETAILS2", userTextBoxOwnerBankAccount2.Text);
                    MySqlParameters.Add(_BANK_ACCOUNT_DETAILS2);

                    //MySqlParameter _BANK_ACCOUNT_CURRENCY2 = new MySqlParameter("_BANK_ACCOUNT_CURRENCY2", radioButtonRON2.Checked ? null : comboBoxOwnerCurrency2.SelectedValue);
                    MySqlParameter _BANK_ACCOUNT_CURRENCY2 = new MySqlParameter("_BANK_ACCOUNT_CURRENCY2", comboBoxOwnerCurrency2.SelectedValue);
                    MySqlParameters.Add(_BANK_ACCOUNT_CURRENCY2);

                    //MySqlParameter _BANK_NAME3 = new MySqlParameter("_BANK_NAME3", userTextBoxOwnerBankName3.Text);
                    MySqlParameter _BANK_ID3 = new MySqlParameter("_BANK_ID3", CommonFunctions.SetNullable(comboBoxBank3));
                    MySqlParameters.Add(_BANK_ID3);

                    MySqlParameter _BANK_ACCOUNT_DETAILS3 = new MySqlParameter("_BANK_ACCOUNT_DETAILS3", userTextBoxOwnerBankAccount3.Text);
                    MySqlParameters.Add(_BANK_ACCOUNT_DETAILS3);

                    //MySqlParameter _BANK_ACCOUNT_CURRENCY3 = new MySqlParameter("_BANK_ACCOUNT_CURRENCY3", radioButtonRON3.Checked ? null : comboBoxOwnerCurrency3.SelectedValue);
                    MySqlParameter _BANK_ACCOUNT_CURRENCY3 = new MySqlParameter("_BANK_ACCOUNT_CURRENCY3", comboBoxOwnerCurrency3.SelectedValue);
                    MySqlParameters.Add(_BANK_ACCOUNT_CURRENCY3);

                    MySqlParameter _RECOMMENDED_BY = new MySqlParameter("_RECOMMENDED_BY", userTextBoxOwnerRecommendedBy.Text);
                    MySqlParameters.Add(_RECOMMENDED_BY);

                    MySqlParameter _LAWYER_INFORMATION = new MySqlParameter("_LAWYER_INFORMATION", userTextBoxOwnerLawyer.Text);
                    MySqlParameters.Add(_LAWYER_INFORMATION);

                    MySqlParameter _ACCOUNTANT_INFORMATION = new MySqlParameter("_ACCOUNTANT_INFORMATION", userTextBoxOwnerAccountant.Text);
                    MySqlParameters.Add(_ACCOUNTANT_INFORMATION);

                    MySqlParameter _OTHER_PERSONS_INFORMATION = new MySqlParameter("_OTHER_PERSONS_INFORMATION", userTextBoxOtherPersons.Text);
                    MySqlParameters.Add(_OTHER_PERSONS_INFORMATION);

                    MySqlParameter _COMMENTS = new MySqlParameter("_COMMENTS", userTextBoxOwnerComments.Text);
                    MySqlParameters.Add(_COMMENTS);

                    MySqlParameter _STARTING_BALLANCE1 = new MySqlParameter("_STARTING_BALLANCE1", userTextBoxStartingBallance1.Text);
                    MySqlParameters.Add(_STARTING_BALLANCE1);

                    MySqlParameter _STARTING_BALLANCE2 = new MySqlParameter("_STARTING_BALLANCE2", userTextBoxStartingBallance2.Text);
                    MySqlParameters.Add(_STARTING_BALLANCE2);

                    MySqlParameter _STARTING_BALLANCE3 = new MySqlParameter("_STARTING_BALLANCE3", userTextBoxStartingBallance3.Text);
                    MySqlParameters.Add(_STARTING_BALLANCE3);

                    MySqlParameter _STARTING_BALLANCE_DATE1 = new MySqlParameter("_STARTING_BALLANCE_DATE1", CommonFunctions.ToMySqlFormatDate(dateTimePickerStartingBallanceDate1.Value));
                    MySqlParameters.Add(_STARTING_BALLANCE_DATE1);

                    MySqlParameter _STARTING_BALLANCE_DATE2 = new MySqlParameter("_STARTING_BALLANCE_DATE2", CommonFunctions.ToMySqlFormatDate(dateTimePickerStartingBallanceDate2.Value));
                    MySqlParameters.Add(_STARTING_BALLANCE_DATE2);

                    MySqlParameter _STARTING_BALLANCE_DATE3 = new MySqlParameter("_STARTING_BALLANCE_DATE3", CommonFunctions.ToMySqlFormatDate(dateTimePickerStartingBallanceDate3.Value));
                    MySqlParameters.Add(_STARTING_BALLANCE_DATE3);

                    MySqlParameter _CASH_STARTING_BALLANCE1 = new MySqlParameter("_CASH_STARTING_BALLANCE1", userTextBoxCashStartingBallance1.Text);
                    MySqlParameters.Add(_CASH_STARTING_BALLANCE1);

                    MySqlParameter _CASH_STARTING_BALLANCE_DATE1 = new MySqlParameter("_CASH_STARTING_BALLANCE_DATE1", CommonFunctions.ToMySqlFormatDate(dateTimePickerCashStartingBallanceDate1.Value));
                    MySqlParameters.Add(_CASH_STARTING_BALLANCE_DATE1);

                    MySqlParameter _CASH_STARTING_BALLANCE2 = new MySqlParameter("_CASH_STARTING_BALLANCE2", userTextBoxCashStartingBallance2.Text);
                    MySqlParameters.Add(_CASH_STARTING_BALLANCE2);

                    MySqlParameter _CASH_STARTING_BALLANCE_DATE2 = new MySqlParameter("_CASH_STARTING_BALLANCE_DATE2", CommonFunctions.ToMySqlFormatDate(dateTimePickerCashStartingBallanceDate2.Value));
                    MySqlParameters.Add(_CASH_STARTING_BALLANCE_DATE2);

                    MySqlParameter _CASH_STARTING_BALLANCE3 = new MySqlParameter("_CASH_STARTING_BALLANCE3", userTextBoxCashStartingBallance3.Text);
                    MySqlParameters.Add(_CASH_STARTING_BALLANCE3);

                    MySqlParameter _CASH_STARTING_BALLANCE_DATE3 = new MySqlParameter("_CASH_STARTING_BALLANCE_DATE3", CommonFunctions.ToMySqlFormatDate(dateTimePickerCashStartingBallanceDate3.Value));
                    MySqlParameters.Add(_CASH_STARTING_BALLANCE_DATE3);

                    MySqlParameter _ADMINISTRATOR_INFORMATION = new MySqlParameter("_ADMINISTRATOR_INFORMATION", userTextBoxAdministrator.Text);
                    MySqlParameters.Add(_ADMINISTRATOR_INFORMATION);
                    // --
                    MySqlParameter _LANGUAGE_ID1 = new MySqlParameter("_LANGUAGE_ID1", CommonFunctions.SetNullable(comboBoxLanguage1));
                    MySqlParameters.Add(_LANGUAGE_ID1);

                    MySqlParameter _LANGUAGE_ID2 = new MySqlParameter("_LANGUAGE_ID2", CommonFunctions.SetNullable(comboBoxLanguage2));
                    MySqlParameters.Add(_LANGUAGE_ID2);

                    MySqlParameter _CLOSED_BY_COMPANY = new MySqlParameter("_CLOSED_BY_COMPANY", checkBoxClosedByCompany.Checked);
                    MySqlParameters.Add(_CLOSED_BY_COMPANY);

                    MySqlParameter _TRANSFERRED_BY_COMPANY = new MySqlParameter("_TRANSFERRED_BY_COMPANY", checkBoxTransferredByCompany.Checked);
                    MySqlParameters.Add(_TRANSFERRED_BY_COMPANY);

                    MySqlParameter _POA_NIF_AND_BANK_ACCOUNTS_BY_COMPANY = new MySqlParameter("_POA_NIF_AND_BANK_ACCOUNTS_BY_COMPANY", checkBoxPoaNifAndBankAccounts.Checked);
                    MySqlParameters.Add(_POA_NIF_AND_BANK_ACCOUNTS_BY_COMPANY);

                    MySqlParameter _HEADQUARTERS_CHANGED_BY_COMPANY = new MySqlParameter("_HEADQUARTERS_CHANGED_BY_COMPANY", checkBoxHeadquartersChangedByCompany.Checked);
                    MySqlParameters.Add(_HEADQUARTERS_CHANGED_BY_COMPANY);
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
                
                //MessageBox.Show(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                return;
            }
            if (NewDR == null) //add direct
            {
                try
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_insert", MySqlParameters.ToArray());
                    da.ExecuteInsertQuery();
                    CheckPasswordExpiration();
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

        private void toolStripButtonAddPhone_Click(object sender, EventArgs e)
        {
            if (userTextBoxOwnerPhoneEdit.Text.Trim() != "")
            {
                if (Validator.IsPhoneNumber(userTextBoxOwnerPhoneEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip1, "");
                    listBoxOwnerPhones.Items.Add(userTextBoxOwnerPhoneEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip1, Language.GetErrorText("errorInvalidPhoneNumber", "Invalid phone number!"));
                }
            }
        }

        private void toolStripButtonDeletePhone_Click(object sender, EventArgs e)
        {
            if (listBoxOwnerPhones.SelectedIndex > -1)
            {
                listBoxOwnerPhones.Items.RemoveAt(listBoxOwnerPhones.SelectedIndex);
            }
        }

        private void toolStripButtonAddEmail_Click(object sender, EventArgs e)
        {
            if (userTextBoxOwnerEmailEdit.Text.Trim() != "")
            {
                if (Validator.IsEmail(userTextBoxOwnerEmailEdit.Text.Trim()))
                {
                    errorProvider1.SetError(toolStrip2, "");
                    listBoxOwnerEmails.Items.Add(userTextBoxOwnerEmailEdit.Text.Trim());
                }
                else
                {
                    errorProvider1.SetError(toolStrip2, Language.GetErrorText("errorInvalidEmail", "Invalid email address!"));
                }
            }
        }

        private void toolStripButtonDeleteEmail_Click(object sender, EventArgs e)
        {
            if (listBoxOwnerEmails.SelectedIndex > -1)
            {
                listBoxOwnerEmails.Items.RemoveAt(listBoxOwnerEmails.SelectedIndex);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxOwnerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_text = ((DataRowView)((ComboBox)sender).SelectedItem)["name"].ToString().ToLower();
            userTextBoxOwnerCnp.Enabled = (selected_text == "individual");
            userTextBoxOwnerPassportNumber.Enabled = (selected_text == "individual");
            dateTimePickerOwnerPassportExpirationDate.Enabled = (selected_text == "individual");
            userTextBoxOwnerCif.Enabled = (selected_text == "company");
            userTextBoxOwnerCui.Enabled = (selected_text == "company");
            userTextBoxOwnerRegComNumber.Enabled = (selected_text == "company");
            userTextBoxNIF.Enabled = (selected_text == "individual");
            userTextBoxAdministrator.Enabled = (selected_text == "company");
            //checkBoxTransferredByCompany.Checked = true;
        }

        private bool ValidateData()
        {
            base.ErrorList.Clear();
            bool toReturn = true;
            errorProvider1.SetError(userTextBoxOwnerCnp, "");
            errorProvider1.SetError(userTextBoxOwnerCif, "");
            errorProvider1.SetError(userTextBoxOwnerCui, "");
            errorProvider1.SetError(userTextBoxOwnerName, "");
            errorProvider1.SetError(comboBoxOwnerType, "");
            errorProvider1.SetError(listBoxOwnerPhones, "");
            errorProvider1.SetError(listBoxOwnerEmails, "");
            errorProvider1.SetError(userTextBoxStartingBallance1, "");
            errorProvider1.SetError(userTextBoxStartingBallance2, "");
            errorProvider1.SetError(userTextBoxStartingBallance3, "");
            errorProvider1.SetError(userTextBoxCashStartingBallance1, "");
            errorProvider1.SetError(userTextBoxCashStartingBallance2, "");
            errorProvider1.SetError(userTextBoxCashStartingBallance3, "");

            if (NewDR != null)
            {
                if (NewDR["name"].ToString().Trim()  == "")
                {
                    errorProvider1.SetError(userTextBoxOwnerName, Language.GetErrorText("errorEmptyOwnerName", "Owner Name can not by empty!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxOwnerName.Name, Language.GetErrorText("errorEmptyOwnerName", "Owner Name can not by empty!")));
                    toReturn = false;
                }

                if (comboBoxOwnerType.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboBoxOwnerType, Language.GetErrorText("errorEmptyOwnerType", "Owner Type can not by empty!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwnerType.Name, Language.GetErrorText("errorEmptyOwnerType", "Owner Type can not by empty!")));
                    toReturn = false;
                }

                if (listBoxOwnerPhones.Items.Count == 0 && listBoxOwnerEmails.Items.Count == 0)
                {
                    errorProvider1.SetError(listBoxOwnerPhones, Language.GetErrorText("errorEmptyOwnerPhonesAndEmails", "Either phone or email must be filled in!"));
                    errorProvider1.SetError(listBoxOwnerEmails, Language.GetErrorText("errorEmptyOwnerPhonesAndEmails", "Either phone or email must be filled in!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(listBoxOwnerEmails.Name, Language.GetErrorText("errorEmptyOwnerPhonesAndEmails", "Either phone or email must be filled in!")));
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
                if (NewDR["type"].ToString().ToLower() == "individual" && (NewDR["cnp"] != DBNull.Value && NewDR["cnp"].ToString().Trim() != ""))
                {
                    if (!Validator.SimpleValidateCNP(NewDR["cnp"].ToString()))
                    {
                        errorProvider1.SetError(userTextBoxOwnerCnp, Language.GetErrorText("errorCnpInvalid", "CNP invalid!"));
                        toReturn = false;
                    }
                }
                */
                /*
                if (NewDR["type"].ToString().ToLower() == "company" && (NewDR["cif"] != DBNull.Value && NewDR["cif"].ToString().Trim() != ""))
                {
                    if (!Validator.SimpleValidateCUI(NewDR["cif"].ToString()))
                    {
                        errorProvider1.SetError(userTextBoxOwnerCif, Language.GetErrorText("errorCuiCifInvalid", "CUI/CIF invalid!"));
                        toReturn = false;
                    }
                }
                */
                /*
                if (NewDR["type"].ToString().ToLower() == "company" && (NewDR["cui"] != DBNull.Value && NewDR["cui"].ToString().Trim() != ""))
                {
                    if (!Validator.SimpleValidateCUI(NewDR["cui"].ToString()))
                    {
                        errorProvider1.SetError(userTextBoxOwnerCui, Language.GetErrorText("errorCuiCifInvalid", "CUI/CIF invalid!"));
                        toReturn = false;
                    }
                }
                */
            }
            else
            {
                if (userTextBoxOwnerName.Text.Trim() == "")
                {
                    errorProvider1.SetError(userTextBoxOwnerName, Language.GetErrorText("errorEmptyOwnerName", "Owner Name can not by empty!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxOwnerName.Name, Language.GetErrorText("errorEmptyOwnerName", "Owner Name can not by empty!")));
                    toReturn = false;
                }

                if (comboBoxOwnerType.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboBoxOwnerType, Language.GetErrorText("errorEmptyOwnerType", "Owner Type can not by empty!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwnerType.Name, Language.GetErrorText("errorEmptyOwnerType", "Owner Type can not by empty!")));
                    toReturn = false;
                }

                if (listBoxOwnerPhones.Items.Count == 0 && listBoxOwnerEmails.Items.Count == 0)
                {
                    errorProvider1.SetError(listBoxOwnerPhones, Language.GetErrorText("errorEmptyOwnerPhonesAndEmails", "Either phone or email must be filled in!"));
                    errorProvider1.SetError(listBoxOwnerEmails, Language.GetErrorText("errorEmptyOwnerPhonesAndEmails", "Either phone or email must be filled in!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(listBoxOwnerEmails.Name, Language.GetErrorText("errorEmptyOwnerPhonesAndEmails", "Either phone or email must be filled in!")));
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
                if (((DataRowView)comboBoxOwnerType.SelectedItem)["name"].ToString().ToLower() == "individual" && (userTextBoxOwnerCnp.Text.Trim() != ""))
                {
                    if (!Validator.SimpleValidateCNP(userTextBoxOwnerCnp.Text.Trim()))
                    {
                        errorProvider1.SetError(userTextBoxOwnerCnp, Language.GetErrorText("errorCnpInvalid", "CNP invalid!"));
                        toReturn = false;
                    }
                }

                if (((DataRowView)comboBoxOwnerType.SelectedItem)["name"].ToString().ToLower() == "company" && (userTextBoxOwnerCif.Text.Trim() != ""))
                {
                    if (!Validator.SimpleValidateCUI(userTextBoxOwnerCif.Text.Trim()))
                    {
                        errorProvider1.SetError(userTextBoxOwnerCif, Language.GetErrorText("errorCuiCifInvalid", "CUI/CIF invalid!"));
                        toReturn = false;
                    }
                }
                */
                /*
                if (((DataRowView)comboBoxOwnerType.SelectedItem)["name"].ToString().ToLower() == "company" && (userTextBoxOwnerCui.Text.Trim() != ""))
                {
                    if (!Validator.SimpleValidateCUI(userTextBoxOwnerCui.Text.Trim()))
                    {
                        errorProvider1.SetError(userTextBoxOwnerCui, Language.GetErrorText("errorCuiCifInvalid", "CUI/CIF invalid!"));
                        toReturn = false;
                    }
                }
                */
                if (!Validator.IsDouble(userTextBoxStartingBallance1.Text))
                {
                    errorProvider1.SetError(userTextBoxStartingBallance1, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxStartingBallance1.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                    toReturn = false;
                }
                if (!Validator.IsDouble(userTextBoxStartingBallance2.Text))
                {
                    errorProvider1.SetError(userTextBoxStartingBallance2, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxStartingBallance2.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                    toReturn = false;
                }
                if (!Validator.IsDouble(userTextBoxStartingBallance3.Text))
                {
                    errorProvider1.SetError(userTextBoxStartingBallance3, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxStartingBallance3.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                    toReturn = false;
                }
                if (!Validator.IsDouble(userTextBoxCashStartingBallance1.Text))
                {
                    errorProvider1.SetError(userTextBoxCashStartingBallance1, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxCashStartingBallance1.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                    toReturn = false;
                }
                if (!Validator.IsDouble(userTextBoxCashStartingBallance2.Text))
                {
                    errorProvider1.SetError(userTextBoxCashStartingBallance2, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxCashStartingBallance2.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                    toReturn = false;
                }
                if (!Validator.IsDouble(userTextBoxCashStartingBallance3.Text))
                {
                    errorProvider1.SetError(userTextBoxCashStartingBallance3, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxCashStartingBallance3.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                    toReturn = false;
                }

            }
            return toReturn;
        }

        private void buttonEditCoOwners_Click(object sender, EventArgs e)
        {
            if (dataGridViewCoOwners.SelectedRows.Count > 0)
            {
                DataRow owner = ((DataRowView)dataGridViewCoOwners.SelectedRows[0].DataBoundItem).Row;
                var f = new CoOwners(owner);
                //f.TopLevel = false;
                //f.MdiParent = this.ParentForm;
                //((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
                //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                //f.Dock = DockStyle.Fill;
                //f.BringToFront();
                //f.StartPosition = FormStartPosition.CenterParent;
                //f.StartPosition = FormStartPosition.Manual;
                //f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        owner = f.NewDR;
                        object[] mySqlParams = daCoOwners.GenerateMySqlParameters(owner.Table, owner.ItemArray, 1);
                        daCoOwners.AttachUpdateParams(mySqlParams);
                        daCoOwners.mySqlDataAdapter.Update(((DataTable)daCoOwners.bindingSource.DataSource));
                        ((DataTable)daCoOwners.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    ((DataTable)((BindingSource)dataGridViewCoOwners.DataSource).DataSource).RejectChanges();
                f.Dispose();
            }

        }

        private void GenerateColumnsListBox(DataGridView dgv)
        {
            //CheckedListBox columns = new CheckedListBox();
            columns.Name = String.Format("columns{0}", dgv.Name);
            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                if (dgvc.Name.ToLower().IndexOf("id") == -1)
                {
                    columns.Items.Add(dgvc.HeaderText, dgvc.Visible);
                }
            }
            columns.Font = new Font(SettingsClass.FontTheme, 8, FontStyle.Regular);
            columns.BorderStyle = BorderStyle.Fixed3D;
            columns.Width = 150;
            columns.Height = 160;
            columns.Visible = false;
            dgv.Controls.Add(columns);
            columns.ItemCheck += new ItemCheckEventHandler(columns_ItemCheck);
        }

        private void FillCoOwners()
        {
            /*
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CO_OWNERSsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", NewDR["id"]) });
            DataTable co_owners = da.ExecuteSelectQuery().Tables[0];
            if (co_owners != null && co_owners.Rows.Count > 0)
            {
                dataGridViewCoOwners.DataSource = co_owners;
            }
            */
            //dataGridViewCoOwners.BindingContext = this.BindingContext;

            daCoOwners = new DataAccess("CO_OWNERSsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", NewDR["id"]) }, "CO_OWNERSsp_insert", null, "CO_OWNERSsp_update", null, " CO_OWNERSsp_delete", null);

            dataGridViewCoOwners.DataSource = daCoOwners.bindingSource;
            foreach (DataGridViewColumn dgvc in dataGridViewCoOwners.Columns)
                if (dgvc.Name.ToLower() == "name" || dgvc.Name.ToLower() == "full_name" || dgvc.Name.ToLower() == "status" || dgvc.Name.ToLower() == "type")
                    dgvc.Visible = true;
                else
                    dgvc.Visible = false;
            GenerateColumnsListBox(dataGridViewCoOwners);
        }

        private void dataGridViewCoOwners_DataSourceChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridViewCoOwners.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridViewCoOwners.Columns)
            {
                col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
                ((DataGridViewAutoFilterColumnHeaderCell)col.HeaderCell).AutomaticSortingEnabled = false;
            }

            // Format the OrderTotal column as currency. 
            //dataGridViewGroups.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            //dataGridViewGroups.AutoResizeColumns();

            Language.PopulateGridColumnHeaders((DataGridView)sender);
        }

        private void dataGridViewCoOwners_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridViewCoOwners.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridViewCoOwners.Columns)
            {
                col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
                ((DataGridViewAutoFilterColumnHeaderCell)col.HeaderCell).AutomaticSortingEnabled = false;
            }

            // Format the OrderTotal column as currency. 
            //dataGridViewGroups.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            //dataGridViewGroups.AutoResizeColumns();
        }

        // Displays the drop-down list when the user presses 
        // ALT+DOWN ARROW or ALT+UP ARROW.
        private void dataGridViewCoOwners_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell = dataGridViewCoOwners.CurrentCell.OwningColumn.HeaderCell as DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        // Updates the filter status label. 
        private void dataGridViewCoOwners_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dataGridViewCoOwners);
            if (String.IsNullOrEmpty(filterStatus))
            {
                statusStrip1.Visible = false;
                toolStripStatusLabelShowAll.Visible = false;
                toolStripStatusLabelFilter.Visible = false;
            }
            else
            {
                statusStrip1.Visible = true;
                toolStripStatusLabelShowAll.Visible = true;
                toolStripStatusLabelFilter.Visible = true;
                toolStripStatusLabelFilter.Text = filterStatus;
            }
        }

        // Clears the filter when the user clicks the "Show All" link
        // or presses ALT+A. 
        private void toolStripStatusLabelShowAll_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridViewCoOwners);
        }

        private void buttonAddCoOwner_Click(object sender, EventArgs e)
        {
            DataRow coowner = ((DataTable)((BindingSource)dataGridViewCoOwners.DataSource).DataSource).NewRow();
            var f = new CoOwners(coowner);
            //f.TopLevel = false;
            //f.MdiParent = this.ParentForm;
            //((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            //f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            //f.StartPosition = FormStartPosition.Manual;
            //f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    coowner = f.NewDR;
                    coowner["owner_id"] = NewDR["id"];
                    object[] mySqlParams = daCoOwners.GenerateMySqlParameters(coowner.Table, coowner.ItemArray, 0);
                    ((DataTable)daCoOwners.bindingSource.DataSource).Rows.Add(coowner);
                    daCoOwners.AttachInsertParams(mySqlParams);
                    daCoOwners.mySqlDataAdapter.Update(((DataTable)daCoOwners.bindingSource.DataSource));
                    ((DataTable)daCoOwners.bindingSource.DataSource).AcceptChanges();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                ((DataTable)daCoOwners.bindingSource.DataSource).RejectChanges();
            }
            f.Dispose();
        }

        private void buttonEditCoOwner_Click(object sender, EventArgs e)
        {
            if (dataGridViewCoOwners.SelectedRows.Count > 0)
            {
                DataRow coowner = ((DataRowView)dataGridViewCoOwners.SelectedRows[0].DataBoundItem).Row;
                var f = new CoOwners(coowner);
                //f.TopLevel = false;
                //f.MdiParent = this.ParentForm;
                //((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
                //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                //f.Dock = DockStyle.Fill;
                //f.BringToFront();
                //f.StartPosition = FormStartPosition.CenterParent;
                //f.StartPosition = FormStartPosition.Manual;
                //f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        coowner = f.NewDR;
                        coowner["owner_id"] = NewDR["id"];
                        object[] mySqlParams = daCoOwners.GenerateMySqlParameters(coowner.Table, coowner.ItemArray, 1);
                        daCoOwners.AttachUpdateParams(mySqlParams);
                        daCoOwners.mySqlDataAdapter.Update(((DataTable)daCoOwners.bindingSource.DataSource));
                        ((DataTable)daCoOwners.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                    ((DataTable)((BindingSource)dataGridViewCoOwners.DataSource).DataSource).RejectChanges();
                f.Dispose();
            }
        }

        private void dataGridViewCoOwners_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //CheckedListBox columns = (CheckedListBox)((DataGridView)sender).Controls[String.Format("columns{0}", ((DataGridView)sender).Name)];
            columns.Visible = false;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                foreach (DataGridViewColumn col in dataGridViewCoOwners.Columns)
                {
                    ((DataGridViewAutoFilterColumnHeaderCell)col.HeaderCell).AutomaticSortingEnabled = false;
                }
                //CheckedListBox columns = (CheckedListBox)((DataGridView)sender).Controls[String.Format("columns{0}", ((DataGridView)sender).Name)];
                Point p = ((DataGridView)sender).PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
                columns.Location = p.X > ((DataGridView)sender).Width - columns.Width ? new Point(((DataGridView)sender).Width - columns.Width, p.Y) : p;
                columns.Visible = true;
                columns.Focus();
                ((DataGridView)sender).InvalidateCell(((DataGridView)sender).CurrentCell);
                return;
            }
            else
            {
                ((DataGridViewAutoFilterColumnHeaderCell)((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell).AutomaticSortingEnabled = true;
            }
        }

        private void dataGridViewCoOwners_MouseClick(object sender, MouseEventArgs e)
        {
            if (((DataGridView)sender).HitTest(e.X, e.Y).RowIndex != -1)
                columns.Visible = false;
        }

        private void columns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string column_name = columns.Items[e.Index].ToString();
            dataGridViewCoOwners.Columns[column_name].Visible = (e.NewValue == CheckState.Checked ? true : false);
        }

        private void buttonDeleteCoOwner_Click(object sender, EventArgs e)
        {
            if (dataGridViewCoOwners.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        int key = Convert.ToInt32(dataGridViewCoOwners["id", dataGridViewCoOwners.SelectedRows[0].Index].Value);
                        daCoOwners.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGridViewCoOwners["id", dataGridViewCoOwners.SelectedRows[0].Index].Value) });
                        DataRow dr = ((DataTable)daCoOwners.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                        dr.Delete();
                        daCoOwners.mySqlDataAdapter.Update(((DataTable)daCoOwners.bindingSource.DataSource));
                        ((DataTable)daCoOwners.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

        }

        private void comboBoxOwnerCurrency1_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelCashStartingBallance1.Text = String.Format("{0} ({1}):", Language.GetLabelText("Owners.labelCashStartingBallance1", "Cash Starting ballance"), ((ComboBox)sender).SelectedValue.ToString());
        }

        private void comboBoxOwnerCurrency2_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelCashStartingBallance2.Text = String.Format("{0} ({1}):", Language.GetLabelText("Owners.labelCashStartingBallance2", "Cash Starting ballance"), ((ComboBox)sender).SelectedValue.ToString());
        }

        private void comboBoxOwnerCurrency3_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelCashStartingBallance3.Text = String.Format("{0} ({1}):", Language.GetLabelText("Owners.labelCashStartingBallance3", "Cash Starting ballance"), ((ComboBox)sender).SelectedValue.ToString());
        }

        private void pictureBoxSelectBank1_Click(object sender, EventArgs e)
        {
            var f = new BanksAgenciesSelect(true);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxBank1.SelectedValue = f.IdToReturn;
            }
            f.Dispose();
        }

        private void pictureBoxSelectBank2_Click(object sender, EventArgs e)
        {
            var f = new BanksAgenciesSelect(true);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxBank2.SelectedValue = f.IdToReturn;
            }
            f.Dispose();
        }

        private void pictureBoxSelectBank3_Click(object sender, EventArgs e)
        {
            var f = new BanksAgenciesSelect(true);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxBank3.SelectedValue = f.IdToReturn;
            }
            f.Dispose();
        }

        private void comboBoxOwnerStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //checkBoxClosedByCompany.Checked = true;
        }

        private void userTextBoxNIF_TextChanged(object sender, EventArgs e)
        {
            //if (((UserTextBox)sender).Text.Trim() != "")
            //{
            //    checkBoxPoaNifAndBankAccounts.Checked = true;
            //}
        }

        private void checkBoxClosedByCompany_CheckedChanged(object sender, EventArgs e)
        {
            //if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
            if (checkBoxClosedByCompany.Checked)
                comboBoxOwnerStatus.SelectedValue = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Finished"), new MySqlParameter("_LIST_TYPE", "owner_status") })).ExecuteScalarQuery();
            else
                comboBoxOwnerStatus.SelectedIndex = -1;
        }

        private void checkBoxTransferredByCompany_CheckedChanged(object sender, EventArgs e)
        {
            //if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
        }

        private void checkBoxPoaNifAndBankAccounts_CheckedChanged(object sender, EventArgs e)
        {
            //if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
        }

        private void checkBoxHeadquartersChangedByCompany_CheckedChanged(object sender, EventArgs e)
        {
            //if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
        }

        private void userTextBoxAddress_TextChanged(object sender, EventArgs e)
        {
            //if (((UserTextBox)sender).Text.Trim() != "")
            //{
            //    checkBoxHeadquartersChangedByCompany.Checked = true;
            //}
        }

        private void buttonSaveLogin_Click(object sender, EventArgs e)
        {
            try
            {
                MD5 md5Hash = MD5.Create();
                string hash = "";
                if (SettingsClass.LoginOwnerId > 0)
                {
                    md5Hash = MD5.Create();
                    hash = CommonFunctions.GetMd5Hash(md5Hash, userTextBoxOldPassword.Text);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_LOGIN", new object[]{
                        new MySqlParameter("_USER_NAME", userTextBoxOldUserName.Text), 
                        new MySqlParameter("_PASSWORD", hash)});
                    object returned = da.ExecuteScalarQuery();
                    if (returned == null)
                    {
                        AttemptsLeft--;
                        base.ShowErrorsDialog(Language.GetMessageBoxText("invalidLogin", "Invalid username or password!"));
                        userTextBoxOldPassword.Text = "";
                        if (AttemptsLeft == 0) this.Close();
                        return;
                    }
                }
                if (userTextBoxNewPassword.Text != userTextBoxConfirmNewPassword.Text)
                {
                    base.ShowErrorsDialog(Language.GetMessageBoxText("passwordsDontMatch", "The new passwords don't match!"));
                    return;
                }
                /*
                if (userTextBoxNewUserName.Text.Trim() == "")
                {
                    base.ShowErrorsDialog(Language.GetMessageBoxText("emptyUserName", "The new user name can not be empty!"));
                    return;
                }
                */
                md5Hash = MD5.Create();
                hash = CommonFunctions.GetMd5Hash(md5Hash, userTextBoxNewPassword.Text);

                DataAccess dalOGIN = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_update_login", new object[] { 
                    new MySqlParameter("_ID", NewDR["id"]), 
                    new MySqlParameter("_USER_NAME", userTextBoxNewUserName.Text.Trim() != "" ? userTextBoxNewUserName.Text : SettingsClass.UserName), 
                    new MySqlParameter("_PASSWORD", hash) });
                dalOGIN.ExecuteUpdateQuery();
                if (SettingsClass.LoginOwnerId > 0)
                {
                    /* --- FROM 21.08.2013 ---
                    string key_value = String.Format("autologin={0};remembername={1};autoname={2};autopassword={3}", checkBoxAutoLogin.Checked.ToString(), checkBoxRememberName.Checked.ToString(), ((checkBoxAutoLogin.Checked || checkBoxRememberName.Checked) ? (userTextBoxNewUserName.Text.Trim() != "" ? userTextBoxNewUserName.Text : SettingsClass.UserName) : ""), checkBoxAutoLogin.Checked ? hash : "");
                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    try
                    {
                        config.AppSettings.Settings.Remove(SettingsClass.CompanyId.ToString());
                    }
                    catch { }
                    config.AppSettings.Settings.Add(SettingsClass.CompanyId.ToString(), key_value);
                    config.Save(ConfigurationSaveMode.Modified, true);
                    MessageBox.Show(Language.GetMessageBoxText("closeApplicationFirst", "You must close and reopen the application to apply the changes!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    */
                    try
                    {
                        DataTable clone = SettingsClass.Settings().Copy();
                        DataRow dr = null;
                        try
                        {
                            //dr = SettingsClass.Settings().Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0];
                            dr = clone.Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0];
                        }
                        catch
                        {
                            dr = SettingsClass.Settings().NewRow();
                        }
                        dr["id"] = SettingsClass.CompanyId;
                        dr["autologin"] = checkBoxAutoLogin.Checked;
                        dr["remembername"] = checkBoxRememberName.Checked;
                        dr["autoname"] = userTextBoxNewUserName.Text;
                        dr["autopassword"] = CommonFunctions.GetMd5Hash(md5Hash, userTextBoxNewPassword.Text.Trim());
                        if (dr.RowState == DataRowState.Detached)
                        {
                            //SettingsClass.Settings().Rows.Add(dr);
                            clone.Rows.Add(dr);
                        }
                        //SettingsClass.Settings().DataSet.AcceptChanges();
                        clone.AcceptChanges();
                        //SettingsClass.Settings().DataSet.WriteXml(Path.Combine(SettingsClass.SettingsFilesPath, "settings.xml"), XmlWriteMode.IgnoreSchema);
                        clone.WriteXml(Path.Combine(SettingsClass.SettingsFilesPath, "settings.xml"), XmlWriteMode.IgnoreSchema);
                        MessageBox.Show(Language.GetMessageBoxText("closeApplicationFirst", "You must close and reopen the application to apply the changes!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorSavingSettings", "Error saving configuration settings! \n{0}"), exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                base.ShowConfirmationDialog(Language.GetMessageBoxText("dataSaved", "Information was saved successfully!"));
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorSavingSettings", "Error saving configuration settings! \n{0}"), exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
