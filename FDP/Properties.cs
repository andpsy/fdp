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
    public partial class Property : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();
        public DataTable AvailablePropertyRooms = new DataTable();
        public DataTable AssignedPropertyRooms = new DataTable();
        public DataTable AvailableRoomAssets = new DataTable();
        public DataTable AssignedRoomAssets = new DataTable();
        public DataTable PropertiesPropertyRooms = new DataTable();
        public DataTable PropertyRoomsRoomAssets = new DataTable();

        public Property()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
        }

        public Property(int id)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[] { new MySqlParameter("_ID", id) });
            NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
            buttonSaveProperty.Enabled = false;
        }

        public Property(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            InitializeComponent();
            //Language.LoadLabels(this);
            NewDR = dr;
        }

        private void Property_Load(object sender, EventArgs e)
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
            else
            {
                dateTimePickerPurchaseDate.Value = DateTime.Now;
                dateTimePickerSellingDate.Value = DateTime.Now;
                dateTimePickerReevaluationDate.Value = DateTime.Now;
                dateTimePickerOptionalInsurancePolicyExpirationDate.Value = DateTime.Now;
                dateTimePickerMandatoryInsurancePolicyExpirationDate.Value = DateTime.Now;
                dateTimePickerRegistrationDate.Value = DateTime.Now;
            }
            FillPropertyRooms(NewDR != null && NewDR.RowState != DataRowState.Detached ? Convert.ToInt32(NewDR["id"]) : 0);
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
        }

        private void FillCombos()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "PROPERTY_STATUS") });
            DataTable dtStatuses = da.ExecuteSelectQuery().Tables[0];
            if (dtStatuses != null)
            {
                comboBoxPropertyStatus.DisplayMember = "name";
                comboBoxPropertyStatus.ValueMember = "id";
                comboBoxPropertyStatus.DataSource = dtStatuses;
            }

            da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_list");
            DataTable dtOwners = da.ExecuteSelectQuery().Tables[0];
            if (dtOwners != null)
            {
                comboBoxOwner.DisplayMember = "name";
                comboBoxOwner.ValueMember = "id";
                comboBoxOwner.DataSource = dtOwners;
            }

            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "PROPERTY_TYPE") });
            DataTable dtTypes = da.ExecuteSelectQuery().Tables[0];
            if (dtTypes != null)
            {
                comboBoxPropertyType.DisplayMember = "name";
                comboBoxPropertyType.ValueMember = "id";
                comboBoxPropertyType.DataSource = dtTypes;
            }

            da = new DataAccess(CommandType.StoredProcedure, "PROJECTSsp_list");
            DataTable dtProjects = da.ExecuteSelectQuery().Tables[0];
            if (dtProjects != null)
            {
                comboBoxPropertyProject.DisplayMember = "name";
                comboBoxPropertyProject.ValueMember = "id";
                comboBoxPropertyProject.DataSource = dtProjects;
            }

            da = new DataAccess(CommandType.StoredProcedure, "CITIESsp_list");
            DataTable dtCities = da.ExecuteSelectQuery().Tables[0];
            if (dtCities != null)
            {
                comboBoxPropertyCity.DisplayMember = "name";
                comboBoxPropertyCity.ValueMember = "id";
                comboBoxPropertyCity.DataSource = dtCities;
            }

            da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_list");
            DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
            if (dtProperties != null)
            {
                comboBoxParentProperty.DisplayMember = "name";
                comboBoxParentProperty.ValueMember = "id";
                comboBoxParentProperty.DataSource = dtProperties;
            }
        }

        private void FillPropertyRooms(int _property_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "ROOMSsp_select_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", _property_id) });
            DataSet ds = da.ExecuteSelectQuery();
            AvailablePropertyRooms = ds.Tables[0];
            cCheckedListBoxAvailableRooms.DisplayMember = "name";
            cCheckedListBoxAvailableRooms.ValueMember = "id";
            cCheckedListBoxAvailableRooms.DataSource = AvailablePropertyRooms;

            AssignedPropertyRooms = ds.Tables[1];
            if (AssignedPropertyRooms == null || AssignedPropertyRooms.Rows.Count == 0)
                AssignedPropertyRooms = AvailablePropertyRooms.Clone();
            cCheckedListBoxSelectedRooms.DisplayMember = "name";
            cCheckedListBoxSelectedRooms.ValueMember = "id";
            cCheckedListBoxSelectedRooms.DataSource = AssignedPropertyRooms;
        }

        private void FillRoomAssets(int _propertyroom_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "ASSETSsp_select_by_room_id", new object[] { new MySqlParameter("_PROPERTYROOM_ID", _propertyroom_id) });
            DataSet ds = da.ExecuteSelectQuery();
            AvailableRoomAssets = ds.Tables[0];
            cCheckedListBoxAvailableAssets.DisplayMember = "name";
            cCheckedListBoxAvailableAssets.ValueMember = "id";
            cCheckedListBoxAvailableAssets.DataSource = AvailableRoomAssets;

            AssignedRoomAssets = ds.Tables[1];
            if (AssignedRoomAssets == null || AssignedRoomAssets.Rows.Count == 0)
                AssignedRoomAssets = AvailableRoomAssets.Clone();
            cCheckedListBoxSelectedAssets.DisplayMember = "name";
            cCheckedListBoxSelectedAssets.ValueMember = "id";
            cCheckedListBoxSelectedAssets.DataSource = AssignedRoomAssets;
        }

        private void FillInfo()
        {
            try
            {
                //if (NewDR.RowState != DataRowState.Added && NewDR != null)
                if (NewDR != null)
                {
                    userTextBoxPropertyName.Text = NewDR["name"].ToString();
                    comboBoxPropertyStatus.SelectedValue = NewDR["status_id"];
                    checkBoxPropertyManagement.Checked = Convert.ToBoolean(NewDR["property_management"]==DBNull.Value?false:NewDR["property_management"]);
                    try { comboBoxOwner.SelectedValue = NewDR["owner_id"]; }
                    catch { }
                    checkBoxPropertyPOA.Checked = Convert.ToBoolean(NewDR["poa"] == DBNull.Value ? false : NewDR["poa"]);
                    try { comboBoxPropertyType.SelectedValue = NewDR["type_id"]; }
                    catch { }
                    try { comboBoxPropertyProject.SelectedValue = NewDR["project_id"]; }
                    catch { }
                    userTextBoxPropertyLocation.Text = NewDR["location"].ToString();
                    try { comboBoxPropertyCity.SelectedValue = NewDR["city_id"]; }
                    catch { }
                    userTextBoxPropertyAddress.Text = NewDR["address"].ToString();
                    try { comboBoxParentProperty.SelectedValue = NewDR["parent_property_id"]; }
                    catch { }
                    userTextBoxAdministratorName.Text = NewDR["administrator_name"].ToString();

                    listBoxPropertyAdministratorPhones.Items.Clear();
                    listBoxPropertyAdministratorEmails.Items.Clear();
                    listBoxPresidentOwnerAssociationPhones.Items.Clear();
                    listBoxPresidentOwnerAssociationEmails.Items.Clear();
                    listBoxMaintenancePersonPhones.Items.Clear();
                    listBoxMaintenancePersonEmails.Items.Clear();
                    listBoxDevelopperPhones.Items.Clear();
                    listBoxDevelopperEmails.Items.Clear();

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
                    userTextBoxCondominiumFee.Text = NewDR["condominium_fee"].ToString();
                    userTextBoxFloatingCapital.Text = NewDR["floating_capital"].ToString();
                    userTextBoxAverageAproximatedConsumption.Text = NewDR["average_approximative_consumption"].ToString();
                    userTextBoxGasContract.Text = NewDR["gas_contract"].ToString();
                    userTextBoxGasClientCode.Text = NewDR["gas_client_code"].ToString();
                    userTextBoxElectricityContract.Text = NewDR["electricity_contract"].ToString();
                    userTextBoxElectricityClientCode.Text = NewDR["electricity_client_code"].ToString();
                    userTextBoxElectricityEneltelCode.Text = NewDR["electricity_eneltel_code"].ToString();
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
                    userTextBoxRooms.Text = NewDR["rooms"].ToString();
                    userTextBoxBathrooms.Text = NewDR["bathroom"].ToString();
                    userTextBoxFloor.Text = NewDR["floor"].ToString();
                    userTextBoxBuildingFloors.Text = NewDR["building_floors"].ToString();
                    userTextBoxAppartmentArea.Text = NewDR["appartment_area"].ToString();
                    userTextBoxTerraceArea.Text = NewDR["terrace_area"].ToString();
                    userTextBoxCommonSpaces.Text = NewDR["common_spaces"].ToString();
                    userTextBoxTotalArea.Text = NewDR["total_area"].ToString();
                    userTextBoxTotalAreaIncludingCommonSpaces.Text = NewDR["total_area_including_common_spaces"].ToString();
                    checkBoxCentralHeating.Checked = Convert.ToBoolean(NewDR["central_heating"] == DBNull.Value ? false : NewDR["central_heating"]);
                    checkBoxRegisteredProperty.Checked = Convert.ToBoolean(NewDR["registered_property"] == DBNull.Value ? false : NewDR["registered_property"]);
                    checkBoxVATApplicable.Checked = Convert.ToBoolean(NewDR["vat_applicable"] == DBNull.Value ? false : NewDR["vat_applicable"]);
                    userTextBoxIndividualCompletelyFurnishedPrice.Text = NewDR["individual_completely_furnished_price"].ToString();
                    userTextBoxCompanyCompletelyFurnishedPrice.Text = NewDR["company_completely_furnished_price"].ToString();
                    userTextBoxIndividualKitchenFurnishedPrice.Text = NewDR["individual_kitchen_furnished_priced"].ToString();
                    userTextBoxCompanyKitchenFurnishedPrice.Text = NewDR["company_kitchen_furnished_price"].ToString();
                    userTextBoxIndividualEmptyPrice.Text = NewDR["individual_empty_price"].ToString();
                    userTextBoxCompanyEmptyPrice.Text = NewDR["company_empty_price"].ToString();
                    userTextBoxPurchasedFrom.Text = NewDR["purchased_from"].ToString();
                    userTextBoxAuthenticationNumber.Text = NewDR["authentication_number"].ToString();
                    try
                    {
                        dateTimePickerPurchaseDate.Value = Convert.ToDateTime(NewDR["purchase_date"]);
                    }
                    catch { dateTimePickerPurchaseDate.Value = DateTime.Now; }
                    userTextBoxNotaryTaxes.Text = NewDR["notary_taxes"].ToString();
                    userTextBoxReceiptNumber.Text = NewDR["receipt_number"].ToString();
                    userTextBoxAgencyCommission.Text = NewDR["agency_commission"].ToString();
                    userTextBoxPurchasePrice.Text = NewDR["purchasing_price"].ToString();
                    userTextBoxPurchasingVAT.Text = NewDR["purchasing_vat"].ToString();
                    userTextBoxSoldTo.Text = NewDR["sold_to"].ToString();
                    try
                    {
                        dateTimePickerSellingDate.Value = Convert.ToDateTime(NewDR["selling_date"]);
                    }
                    catch { dateTimePickerSellingDate.Value = DateTime.Now; }
                    userTextBoxSellingPrice.Text = NewDR["selling_price"].ToString();
                    userTextBoxSellingVAT.Text = NewDR["selling_vat"].ToString();
                    checkBoxIncludeForAgencies.Checked = Convert.ToBoolean(NewDR["include_for_agencies"] == DBNull.Value ? false : NewDR["include_for_agencies"]);
                    try
                    {
                        dateTimePickerReevaluationDate.Value = Convert.ToDateTime(NewDR["reevaluation_date"]);
                    }
                    catch { dateTimePickerReevaluationDate.Value = DateTime.Now; }
                    userTextBoxReevaluationValue.Text = NewDR["reevaluation_value"].ToString();
                    userTextBoxOptionalInsuranceCompany.Text = NewDR["optional_insurance_company"].ToString();
                    userTextBoxAppartmentOptionalInsurancePolicyNumber.Text = NewDR["apartment_optional_insurance_policy_number"].ToString();
                    userTextBoxContentsOptionalInsurancePolicyNumber.Text = NewDR["contents_optional_insurance_policy_number"].ToString();
                    try
                    {
                        dateTimePickerOptionalInsurancePolicyExpirationDate.Value = Convert.ToDateTime(NewDR["optional_insurance_policy_expiration_date"]);
                    }
                    catch { dateTimePickerOptionalInsurancePolicyExpirationDate.Value = DateTime.Now; }
                    userTextBoxMandatoryInsuranceCompany.Text = NewDR["mandatory_insurance_company"].ToString();
                    userTextBoxMandatoryInsurancePolicyNumber.Text = NewDR["mandatory_insurance_policy_number"].ToString();
                    try
                    {
                        dateTimePickerMandatoryInsurancePolicyExpirationDate.Value = Convert.ToDateTime(NewDR["mandatory_insurance_policy_expiration_date"]);
                    }
                    catch { dateTimePickerMandatoryInsurancePolicyExpirationDate.Value = DateTime.Now; }
                    // --
                    userTextBoxStorageRoom.Text = NewDR["storage_room"].ToString();
                    userTextBoxAssociationName.Text = NewDR["owners_association_name"].ToString();
                    userTextBoxAssociationCif.Text = NewDR["owners_association_cif"].ToString();
                    userTextBoxInternetRepresentative.Text = NewDR["internet_provider_representative_information"].ToString();
                    userTextBoxTelephoneRepresentative.Text = NewDR["telephone_provider_representative_information"].ToString();
                    userTextBoxTvRepresentative.Text = NewDR["tv_provider_representative_information"].ToString();
                    userTextBoxGasUser.Text = NewDR["gas_user"].ToString();
                    userTextBoxGasPassword.Text = NewDR["gas_password"].ToString();
                    userTextBoxElectricityUser.Text = NewDR["electricity_user"].ToString();
                    userTextBoxElectricityPassword.Text = NewDR["electricity_password"].ToString();
                    userTextBoxFiscalUser.Text = NewDR["fiscal_administration_user"].ToString();
                    userTextBoxFiscalPassword.Text = NewDR["fiscal_administration_password"].ToString();
                    userTextBoxIndividualSellingPrice.Text = NewDR["individual_selling_price"].ToString();
                    userTextBoxCompanySellingPrice.Text = NewDR["company_selling_price"].ToString();
                    // --
                    try
                    {
                        dateTimePickerRegistrationDate.Value = Convert.ToDateTime(NewDR["registration_date"]);
                    }
                    catch { dateTimePickerRegistrationDate.Value = DateTime.Now; }
                    checkBoxFurnishedByCompany.Checked = Convert.ToBoolean(NewDR["furnished_by_company"] == DBNull.Value ? false : NewDR["furnished_by_company"]);
                    checkBoxSignedPurchaseContract.Checked = Convert.ToBoolean(NewDR["signed_purchase_contract"] == DBNull.Value ? false : NewDR["signed_purchase_contract"]);
                    checkBoxGasContractDoneByCompany.Checked = Convert.ToBoolean(NewDR["gas_contract_done_by_company"] == DBNull.Value ? false : NewDR["gas_contract_done_by_company"]);
                    checkBoxElectricityContractDoneByCompany.Checked = Convert.ToBoolean(NewDR["electricity_contract_done_by_company"] == DBNull.Value ? false : NewDR["electricity_contract_done_by_company"]);
                    checkBoxInsuranceDoneByCompany.Checked = Convert.ToBoolean(NewDR["insurance_done_by_company"] == DBNull.Value ? false : NewDR["insurance_done_by_company"]);
                    checkBoxSoldByCompany.Checked = Convert.ToBoolean(NewDR["sold_by_company"] == DBNull.Value ? false : NewDR["sold_by_company"]);
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
                    //NewDR["status_id"] = comboBoxPropertyStatus.SelectedValue == null ? DBNull.Value : comboBoxPropertyStatus.SelectedValue;
                    NewDR["status_id"] = CommonFunctions.SetNullable(comboBoxPropertyStatus);
                    NewDR["property_management"] = checkBoxPropertyManagement.Checked;
                    //NewDR["owner_id"] = comboBoxOwner.SelectedValue == null ? DBNull.Value : comboBoxOwner.SelectedValue;
                    NewDR["owner_id"] = CommonFunctions.SetNullable(comboBoxOwner);
                    NewDR["poa"] = checkBoxPropertyPOA.Checked;
                    //NewDR["type_id"] = comboBoxPropertyType.SelectedValue == null ? DBNull.Value : comboBoxPropertyType.SelectedValue;
                    NewDR["type_id"] = CommonFunctions.SetNullable(comboBoxPropertyType);
                    //NewDR["project_id"] = comboBoxPropertyProject.SelectedValue == null ? DBNull.Value : comboBoxPropertyProject.SelectedValue;
                    NewDR["project_id"] = CommonFunctions.SetNullable(comboBoxPropertyProject);
                    NewDR["location"] = userTextBoxPropertyLocation.Text;
                    //NewDR["city_id"] = comboBoxPropertyCity.SelectedValue == null ? DBNull.Value : comboBoxPropertyCity.SelectedValue;
                    NewDR["city_id"] = CommonFunctions.SetNullable(comboBoxPropertyCity);
                    NewDR["address"] = userTextBoxPropertyAddress.Text;
                    //NewDR["parent_property_id"] = comboBoxParentProperty.SelectedValue == null ? DBNull.Value : comboBoxParentProperty.SelectedValue;
                    NewDR["parent_property_id"] = CommonFunctions.SetNullable(comboBoxParentProperty);
                    NewDR["administrator_name"] = userTextBoxAdministratorName.Text;
                    NewDR["administrator_phones"] = administrator_phones;
                    NewDR["administrator_emails"] = administrator_emails;
                    NewDR["president_owners_association_name"] = userTextBoxPropertyPresidentOwnereAssociationName.Text;
                    NewDR["president_owners_association_phones"] = president_owners_association_phones;
                    NewDR["president_owners_association_emails"] = president_owners_association_emails;
                    NewDR["owners_association_bank"] = userTextBoxAssociationBankName.Text;
                    NewDR["owners_association_bank_account"] = userTextBoxAssociationBankAccount.Text;
                    NewDR["maintenance_person_name"] = userTextBoxMaintenancePersonName.Text;
                    NewDR["maintenance_person_phones"] = maintenance_person_phones;
                    NewDR["maintenance_person_emails"] = maintenance_person_emails;
                    NewDR["internet_provider_information"] = userTextBoxInternetProviderInformation.Text;
                    NewDR["telephone_provider_information"] = userTextBoxTelephoneProviderInformation.Text;
                    NewDR["tv_provider_information"] = userTextBoxTVProviderInformation.Text;
                    //NewDR["representative_information"] = userTextBoxRepresentativeInformation.Text;
                    NewDR["condominium_fee"] = userTextBoxCondominiumFee.Text.Trim() == "" ? DBNull.Value : (object)Convert.ToDouble(userTextBoxCondominiumFee.Text);
                    NewDR["floating_capital"] = userTextBoxFloatingCapital.Text.Trim() == "" ? DBNull.Value : (object)Convert.ToDouble(userTextBoxFloatingCapital.Text);
                    NewDR["average_approximative_consumption"] = userTextBoxAverageAproximatedConsumption.Text.Trim() == "" ? DBNull.Value : (object)Convert.ToDouble(userTextBoxAverageAproximatedConsumption.Text);
                    NewDR["gas_contract"] = userTextBoxGasContract.Text;
                    NewDR["gas_client_code"] = userTextBoxGasClientCode.Text;
                    NewDR["electricity_contract"] = userTextBoxElectricityContract.Text;
                    NewDR["electricity_client_code"] = userTextBoxElectricityClientCode.Text;
                    NewDR["electricity_eneltel_code"] = userTextBoxElectricityEneltelCode.Text;
                    //NewDR["user"] = userTextBoxGasUser.Text;
                    //NewDR["password"] = userTextBoxGasPassword.Text;
                    NewDR["developer_name"] = userTextBoxDevelopperName.Text;
                    NewDR["developer_contact_person"] = userTextBoxDevelopperContactPersonName.Text;
                    NewDR["developer_phones"] = developer_phones;
                    NewDR["developer_emails"] = developer_emails;
                    NewDR["rooms"] = userTextBoxRooms.Text.Trim() == ""? DBNull.Value : (object)Convert.ToInt32(userTextBoxRooms.Text);
                    NewDR["bathroom"] = userTextBoxBathrooms.Text.Trim() == "" ? DBNull.Value : (object)Convert.ToInt32(userTextBoxBathrooms.Text);
                    NewDR["floor"] = userTextBoxFloor.Text.Trim() == "" ? DBNull.Value : (object)Convert.ToInt32(userTextBoxFloor.Text);
                    NewDR["building_floors"] = userTextBoxBuildingFloors.Text.Trim()==""?DBNull.Value:(object)Convert.ToInt32(userTextBoxBuildingFloors.Text);
                    NewDR["appartment_area"] = userTextBoxAppartmentArea.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxAppartmentArea.Text);
                    NewDR["terrace_area"] = userTextBoxTerraceArea.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxTerraceArea.Text);
                    NewDR["common_spaces"] = userTextBoxCommonSpaces.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxCommonSpaces.Text);
                    NewDR["total_area"] = userTextBoxTotalArea.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxTotalArea.Text);
                    NewDR["total_area_including_common_spaces"] = userTextBoxTotalAreaIncludingCommonSpaces.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxTotalAreaIncludingCommonSpaces.Text);
                    //NewDR["storage_room"] = checkBoxStorageRoom.Checked;
                    NewDR["central_heating"] = checkBoxCentralHeating.Checked;
                    NewDR["registered_property"] = checkBoxRegisteredProperty.Checked;
                    NewDR["vat_applicable"] = checkBoxVATApplicable.Checked;
                    NewDR["individual_completely_furnished_price"] = userTextBoxIndividualCompletelyFurnishedPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxIndividualCompletelyFurnishedPrice.Text);
                    NewDR["company_completely_furnished_price"] = userTextBoxCompanyCompletelyFurnishedPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxCompanyCompletelyFurnishedPrice.Text);
                    NewDR["individual_kitchen_furnished_priced"] = userTextBoxIndividualKitchenFurnishedPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxIndividualKitchenFurnishedPrice.Text);
                    NewDR["company_kitchen_furnished_price"] = userTextBoxCompanyKitchenFurnishedPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxCompanyKitchenFurnishedPrice.Text);
                    NewDR["individual_empty_price"] = userTextBoxIndividualEmptyPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxIndividualEmptyPrice.Text);
                    NewDR["company_empty_price"] = userTextBoxCompanyEmptyPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxCompanyEmptyPrice.Text);
                    NewDR["purchased_from"] = userTextBoxPurchasedFrom.Text;
                    NewDR["authentication_number"] = userTextBoxAuthenticationNumber.Text;
                    NewDR["purchase_date"] = dateTimePickerPurchaseDate.Value;
                    NewDR["notary_taxes"] = userTextBoxNotaryTaxes.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxNotaryTaxes.Text);
                    NewDR["receipt_number"] = userTextBoxReceiptNumber.Text;
                    NewDR["agency_commission"] = userTextBoxAgencyCommission.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxAgencyCommission.Text);
                    NewDR["purchasing_price"] = userTextBoxPurchasePrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxPurchasePrice.Text);
                    NewDR["purchasing_vat"] = userTextBoxPurchasingVAT.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxPurchasingVAT.Text);
                    NewDR["sold_to"] = userTextBoxSoldTo.Text;
                    NewDR["selling_date"] = dateTimePickerSellingDate.Value;
                    NewDR["selling_price"] = userTextBoxSellingPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxSellingPrice.Text);
                    NewDR["selling_vat"] = userTextBoxSellingVAT.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxSellingVAT.Text);
                    NewDR["include_for_agencies"] = checkBoxIncludeForAgencies.Checked;
                    NewDR["reevaluation_date"] = dateTimePickerReevaluationDate.Value;
                    NewDR["reevaluation_value"] = userTextBoxReevaluationValue.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxReevaluationValue.Text);
                    NewDR["optional_insurance_company"] = userTextBoxOptionalInsuranceCompany.Text;
                    NewDR["apartment_optional_insurance_policy_number"] = userTextBoxAppartmentOptionalInsurancePolicyNumber.Text;
                    NewDR["contents_optional_insurance_policy_number"] = userTextBoxContentsOptionalInsurancePolicyNumber.Text;
                    NewDR["optional_insurance_policy_expiration_date"] = dateTimePickerOptionalInsurancePolicyExpirationDate.Value;
                    NewDR["mandatory_insurance_company"] = userTextBoxMandatoryInsuranceCompany.Text;
                    NewDR["mandatory_insurance_policy_number"] = userTextBoxMandatoryInsurancePolicyNumber.Text;
                    NewDR["mandatory_insurance_policy_expiration_date"] = dateTimePickerMandatoryInsurancePolicyExpirationDate.Value;
                    // --
                    NewDR["storage_room"] = userTextBoxStorageRoom.Text;
                    NewDR["owners_association_name"] = userTextBoxAssociationName.Text;
                    NewDR["owners_association_cif"] = userTextBoxAssociationCif.Text;
                    NewDR["internet_provider_representative_information"] = userTextBoxInternetRepresentative.Text;
                    NewDR["telephone_provider_representative_information"] = userTextBoxTelephoneRepresentative.Text;
                    NewDR["tv_provider_representative_information"] = userTextBoxTvRepresentative.Text;
                    NewDR["gas_user"] = userTextBoxGasUser.Text;
                    NewDR["gas_password"] = userTextBoxGasPassword.Text;
                    NewDR["electricity_user"] = userTextBoxElectricityUser.Text;
                    NewDR["electricity_password"] = userTextBoxElectricityPassword.Text;
                    NewDR["fiscal_administration_user"] = userTextBoxFiscalUser.Text;
                    NewDR["fiscal_administration_password"] = userTextBoxFiscalPassword.Text;
                    NewDR["individual_selling_price"] = userTextBoxIndividualSellingPrice.Text.Trim() == "" ? DBNull.Value : (object)Convert.ToDouble(userTextBoxIndividualSellingPrice.Text);
                    NewDR["company_selling_price"] = userTextBoxCompanySellingPrice.Text.Trim() == "" ? DBNull.Value : (object)Convert.ToDouble(userTextBoxCompanySellingPrice.Text);
                    // --
                    NewDR["registration_date"] = dateTimePickerRegistrationDate.Value;
                    NewDR["signed_purchase_contract"] = checkBoxSignedPurchaseContract.Checked;
                    NewDR["furnished_by_company"] = checkBoxFurnishedByCompany.Checked;
                    NewDR["gas_contract_done_by_company"] = checkBoxGasContractDoneByCompany.Checked;
                    NewDR["electricity_contract_done_by_company"] = checkBoxElectricityContractDoneByCompany.Checked;
                    NewDR["insurance_done_by_company"] = checkBoxInsuranceDoneByCompany.Checked;
                    NewDR["sold_by_company"] = checkBoxSoldByCompany.Checked;
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
                    //MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", comboBoxPropertyStatus.SelectedValue == null ? DBNull.Value : comboBoxPropertyStatus.SelectedValue); MySqlParameters.Add(_STATUS_ID);
                    MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", CommonFunctions.SetNullable(comboBoxPropertyStatus)); MySqlParameters.Add(_STATUS_ID);
                    MySqlParameter _PROPERTY_MANAGEMENT = new MySqlParameter("_PROPERTY_MANAGEMENT", checkBoxPropertyManagement.Checked); MySqlParameters.Add(_PROPERTY_MANAGEMENT);
                    //MySqlParameter _OWNER_ID = new MySqlParameter("_OWNER_ID", comboBoxOwner.SelectedValue == null ? DBNull.Value : comboBoxOwner.SelectedValue); MySqlParameters.Add(_OWNER_ID);
                    MySqlParameter _OWNER_ID = new MySqlParameter("_OWNER_ID", CommonFunctions.SetNullable(comboBoxOwner)); MySqlParameters.Add(_OWNER_ID);
                    MySqlParameter _POA = new MySqlParameter("_POA", checkBoxPropertyPOA.Checked); MySqlParameters.Add(_POA);
                    //MySqlParameter _TYPE_ID = new MySqlParameter("_TYPE_ID", comboBoxPropertyType.SelectedValue == null ? DBNull.Value : comboBoxPropertyType.SelectedValue); MySqlParameters.Add(_TYPE_ID);
                    MySqlParameter _TYPE_ID = new MySqlParameter("_TYPE_ID", CommonFunctions.SetNullable(comboBoxPropertyType)); MySqlParameters.Add(_TYPE_ID);
                    //MySqlParameter _PROJECT_ID = new MySqlParameter("_PROJECT_ID", comboBoxPropertyProject.SelectedValue == null ? DBNull.Value : comboBoxPropertyProject.SelectedValue); MySqlParameters.Add(_PROJECT_ID);
                    MySqlParameter _PROJECT_ID = new MySqlParameter("_PROJECT_ID", CommonFunctions.SetNullable(comboBoxPropertyProject)); MySqlParameters.Add(_PROJECT_ID);
                    MySqlParameter _LOCATION = new MySqlParameter("_LOCATION", userTextBoxPropertyLocation.Text); MySqlParameters.Add(_LOCATION);
                    //MySqlParameter _CITY_ID = new MySqlParameter("_CITY_ID", comboBoxPropertyCity.SelectedValue == null ? DBNull.Value : comboBoxPropertyCity.SelectedValue); MySqlParameters.Add(_CITY_ID);
                    MySqlParameter _CITY_ID = new MySqlParameter("_CITY_ID", CommonFunctions.SetNullable(comboBoxPropertyCity)); MySqlParameters.Add(_CITY_ID);
                    MySqlParameter _ADDRESS = new MySqlParameter("_ADDRESS", userTextBoxPropertyAddress.Text); MySqlParameters.Add(_ADDRESS);
                    //MySqlParameter _PARENT_PROPERTY_ID = new MySqlParameter("_PARENT_PROPERTY_ID", comboBoxParentProperty.SelectedValue == null ? DBNull.Value : comboBoxParentProperty.SelectedValue); MySqlParameters.Add(_PARENT_PROPERTY_ID);
                    MySqlParameter _PARENT_PROPERTY_ID = new MySqlParameter("_PARENT_PROPERTY_ID", CommonFunctions.SetNullable(comboBoxParentProperty)); MySqlParameters.Add(_PARENT_PROPERTY_ID);
                    MySqlParameter _ADMINISTRATOR_NAME = new MySqlParameter("_ADMINISTRATOR_NAME", userTextBoxAdministratorName.Text); MySqlParameters.Add(_ADMINISTRATOR_NAME);
                    MySqlParameter _ADMINISTRATOR_PHONES = new MySqlParameter("_ADMINISTRATOR_PHONES", administrator_phones); MySqlParameters.Add(_ADMINISTRATOR_PHONES);
                    MySqlParameter _ADMINISTRATOR_EMAILS = new MySqlParameter("_ADMINISTRATOR_EMAILS", administrator_emails); MySqlParameters.Add(_ADMINISTRATOR_EMAILS);
                    MySqlParameter _PRESIDENT_OWNERS_ASSOCIATION_NAME = new MySqlParameter("_PRESIDENT_OWNERS_ASSOCIATION_NAME", userTextBoxPropertyPresidentOwnereAssociationName.Text); MySqlParameters.Add(_PRESIDENT_OWNERS_ASSOCIATION_NAME);
                    MySqlParameter _PRESIDENT_OWNERS_ASSOCIATION_PHONES = new MySqlParameter("_PRESIDENT_OWNERS_ASSOCIATION_PHONES", president_owners_association_phones); MySqlParameters.Add(_PRESIDENT_OWNERS_ASSOCIATION_PHONES);
                    MySqlParameter _PRESIDENT_OWNERS_ASSOCIATION_EMAILS = new MySqlParameter("_PRESIDENT_OWNERS_ASSOCIATION_EMAILS", president_owners_association_emails); MySqlParameters.Add(_PRESIDENT_OWNERS_ASSOCIATION_EMAILS);
                    MySqlParameter _OWNERS_ASSOCIATION_BANK = new MySqlParameter("_OWNERS_ASSOCIATION_BANK", userTextBoxAssociationBankName.Text); MySqlParameters.Add(_OWNERS_ASSOCIATION_BANK);
                    MySqlParameter _OWNERS_ASSOCIATION_BANK_ACCOUNT = new MySqlParameter("_OWNERS_ASSOCIATION_BANK_ACCOUNT", userTextBoxAssociationBankAccount.Text); MySqlParameters.Add(_OWNERS_ASSOCIATION_BANK_ACCOUNT);
                    MySqlParameter _MAINTENANCE_PERSON_NAME = new MySqlParameter("_MAINTENANCE_PERSON_NAME", userTextBoxMaintenancePersonName.Text); MySqlParameters.Add(_MAINTENANCE_PERSON_NAME);
                    MySqlParameter _MAINTENANCE_PERSON_PHONES = new MySqlParameter("_MAINTENANCE_PERSON_PHONES", maintenance_person_phones); MySqlParameters.Add(_MAINTENANCE_PERSON_PHONES);
                    MySqlParameter _MAINTENANCE_PERSON_EMAILS = new MySqlParameter("_MAINTENANCE_PERSON_EMAILS", maintenance_person_emails); MySqlParameters.Add(_MAINTENANCE_PERSON_EMAILS);
                    MySqlParameter _INTERNET_PROVIDER_INFORMATION = new MySqlParameter("_INTERNET_PROVIDER_INFORMATION", userTextBoxInternetProviderInformation.Text); MySqlParameters.Add(_INTERNET_PROVIDER_INFORMATION);
                    MySqlParameter _TELEPHONE_PROVIDER_INFORMATION = new MySqlParameter("_TELEPHONE_PROVIDER_INFORMATION", userTextBoxTelephoneProviderInformation.Text); MySqlParameters.Add(_TELEPHONE_PROVIDER_INFORMATION);
                    MySqlParameter _TV_PROVIDER_INFORMATION = new MySqlParameter("_TV_PROVIDER_INFORMATION", userTextBoxTVProviderInformation.Text); MySqlParameters.Add(_TV_PROVIDER_INFORMATION);
                    //MySqlParameter _REPRESENTATIVE_INFORMATION = new MySqlParameter("_REPRESENTATIVE_INFORMATION", userTextBoxRepresentativeInformation.Text); MySqlParameters.Add(_REPRESENTATIVE_INFORMATION);
                    MySqlParameter _CONDOMINIUM_FEE = new MySqlParameter("_CONDOMINIUM_FEE", userTextBoxCondominiumFee.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxCondominiumFee.Text)); MySqlParameters.Add(_CONDOMINIUM_FEE);
                    MySqlParameter _FLOATING_CAPITAL = new MySqlParameter("_FLOATING_CAPITAL", userTextBoxFloatingCapital.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxFloatingCapital.Text)); MySqlParameters.Add(_FLOATING_CAPITAL);
                    MySqlParameter _AVERAGE_APPROXIMATIVE_CONSUMPTION = new MySqlParameter("_AVERAGE_APPROXIMATIVE_CONSUMPTION", userTextBoxAverageAproximatedConsumption.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxAverageAproximatedConsumption.Text)); MySqlParameters.Add(_AVERAGE_APPROXIMATIVE_CONSUMPTION);
                    MySqlParameter _GAS_CONTRACT = new MySqlParameter("_GAS_CONTRACT", userTextBoxGasContract.Text); MySqlParameters.Add(_GAS_CONTRACT);
                    MySqlParameter _GAS_CLIENT_CODE = new MySqlParameter("_GAS_CLIENT_CODE", userTextBoxGasClientCode.Text); MySqlParameters.Add(_GAS_CLIENT_CODE);
                    MySqlParameter _ELECTRICITY_CONTRACT = new MySqlParameter("_ELECTRICITY_CONTRACT", userTextBoxElectricityContract.Text); MySqlParameters.Add(_ELECTRICITY_CONTRACT);
                    MySqlParameter _ELECTRICITY_CLIENT_CODE = new MySqlParameter("_ELECTRICITY_CLIENT_CODE", userTextBoxElectricityClientCode.Text); MySqlParameters.Add(_ELECTRICITY_CLIENT_CODE);
                    MySqlParameter _ELECTRICITY_ENELTEL_CODE = new MySqlParameter("_ELECTRICITY_ENELTEL_CODE", userTextBoxElectricityEneltelCode.Text); MySqlParameters.Add(_ELECTRICITY_ENELTEL_CODE);
                    //MySqlParameter _USER = new MySqlParameter("_USER", userTextBoxGasUser.Text); MySqlParameters.Add(_USER);
                    //MySqlParameter _PASSWORD = new MySqlParameter("_PASSWORD", userTextBoxGasPassword.Text); MySqlParameters.Add(_PASSWORD);
                    MySqlParameter _DEVELOPER_NAME = new MySqlParameter("_DEVELOPER_NAME", userTextBoxDevelopperName.Text); MySqlParameters.Add(_DEVELOPER_NAME);
                    MySqlParameter _DEVELOPER_CONTACT_PERSON = new MySqlParameter("_DEVELOPER_CONTACT_PERSON", userTextBoxDevelopperContactPersonName.Text); MySqlParameters.Add(_DEVELOPER_CONTACT_PERSON);
                    MySqlParameter _DEVELOPER_PHONES = new MySqlParameter("_DEVELOPER_PHONES", developer_phones); MySqlParameters.Add(_DEVELOPER_PHONES);
                    MySqlParameter _DEVELOPER_EMAILS = new MySqlParameter("_DEVELOPER_EMAILS", developer_emails); MySqlParameters.Add(_DEVELOPER_EMAILS);
                    MySqlParameter _ROOMS = new MySqlParameter("_ROOMS", userTextBoxRooms.Text.Trim() == "" ? DBNull.Value : (object)userTextBoxRooms.Text); MySqlParameters.Add(_ROOMS);
                    MySqlParameter _BATHROOM = new MySqlParameter("_BATHROOM", userTextBoxBathrooms.Text.Trim() == "" ? DBNull.Value : (object)userTextBoxBathrooms.Text); MySqlParameters.Add(_BATHROOM);
                    MySqlParameter _FLOOR = new MySqlParameter("_FLOOR", userTextBoxFloor.Text.Trim() == "" ? DBNull.Value : (object)userTextBoxFloor.Text); MySqlParameters.Add(_FLOOR);
                    MySqlParameter _BUILDING_FLOORS = new MySqlParameter("_BUILDING_FLOORS", userTextBoxBuildingFloors.Text.Trim() == "" ? DBNull.Value : (object)userTextBoxBuildingFloors.Text); MySqlParameters.Add(_BUILDING_FLOORS);
                    MySqlParameter _APPARTMENT_AREA = new MySqlParameter("_APPARTMENT_AREA", userTextBoxAppartmentArea.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxAppartmentArea.Text)); MySqlParameters.Add(_APPARTMENT_AREA);
                    MySqlParameter _TERRACE_AREA = new MySqlParameter("_TERRACE_AREA", userTextBoxTerraceArea.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxTerraceArea.Text)); MySqlParameters.Add(_TERRACE_AREA);
                    MySqlParameter _COMMON_SPACES = new MySqlParameter("_COMMON_SPACES", userTextBoxCommonSpaces.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxCommonSpaces.Text)); MySqlParameters.Add(_COMMON_SPACES);
                    MySqlParameter _TOTAL_AREA = new MySqlParameter("_TOTAL_AREA", userTextBoxTotalArea.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxTotalArea.Text)); MySqlParameters.Add(_TOTAL_AREA);
                    MySqlParameter _TOTAL_AREA_INCLUDING_COMMON_SPACES = new MySqlParameter("_TOTAL_AREA_INCLUDING_COMMON_SPACES", userTextBoxTotalAreaIncludingCommonSpaces.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxTotalAreaIncludingCommonSpaces.Text)); MySqlParameters.Add(_TOTAL_AREA_INCLUDING_COMMON_SPACES);
                    //MySqlParameter _STORAGE_ROOM = new MySqlParameter("_STORAGE_ROOM", checkBoxStorageRoom.Checked); MySqlParameters.Add(_STORAGE_ROOM);
                    MySqlParameter _CENTRAL_HEATING = new MySqlParameter("_CENTRAL_HEATING", checkBoxCentralHeating.Checked); MySqlParameters.Add(_CENTRAL_HEATING);
                    MySqlParameter _REGISTERED_PROPERTY = new MySqlParameter("_REGISTERED_PROPERTY", checkBoxRegisteredProperty.Checked); MySqlParameters.Add(_REGISTERED_PROPERTY);
                    MySqlParameter _VAT_APPLICABLE = new MySqlParameter("_VAT_APPLICABLE", checkBoxVATApplicable.Checked); MySqlParameters.Add(_VAT_APPLICABLE);
                    MySqlParameter _INDIVIDUAL_COMPLETELY_FURNISHED_PRICE = new MySqlParameter("_INDIVIDUAL_COMPLETELY_FURNISHED_PRICE", userTextBoxIndividualCompletelyFurnishedPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxIndividualCompletelyFurnishedPrice.Text)); MySqlParameters.Add(_INDIVIDUAL_COMPLETELY_FURNISHED_PRICE);
                    MySqlParameter _COMPANY_COMPLETELY_FURNISHED_PRICE = new MySqlParameter("_COMPANY_COMPLETELY_FURNISHED_PRICE", userTextBoxCompanyCompletelyFurnishedPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxCompanyCompletelyFurnishedPrice.Text)); MySqlParameters.Add(_COMPANY_COMPLETELY_FURNISHED_PRICE);
                    MySqlParameter _INDIVIDUAL_KITCHEN_FURNISHED_PRICED = new MySqlParameter("_INDIVIDUAL_KITCHEN_FURNISHED_PRICED", userTextBoxIndividualKitchenFurnishedPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxIndividualKitchenFurnishedPrice.Text)); MySqlParameters.Add(_INDIVIDUAL_KITCHEN_FURNISHED_PRICED);
                    MySqlParameter _COMPANY_KITCHEN_FURNISHED_PRICE = new MySqlParameter("_COMPANY_KITCHEN_FURNISHED_PRICE", userTextBoxCompanyKitchenFurnishedPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxCompanyKitchenFurnishedPrice.Text)); MySqlParameters.Add(_COMPANY_KITCHEN_FURNISHED_PRICE);
                    MySqlParameter _INDIVIDUAL_EMPTY_PRICE = new MySqlParameter("_INDIVIDUAL_EMPTY_PRICE", userTextBoxIndividualEmptyPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxIndividualEmptyPrice.Text)); MySqlParameters.Add(_INDIVIDUAL_EMPTY_PRICE);
                    MySqlParameter _COMPANY_EMPTY_PRICE = new MySqlParameter("_COMPANY_EMPTY_PRICE", userTextBoxCompanyEmptyPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxCompanyEmptyPrice.Text)); MySqlParameters.Add(_COMPANY_EMPTY_PRICE);
                    MySqlParameter _PURCHASED_FROM = new MySqlParameter("_PURCHASED_FROM", userTextBoxPurchasedFrom.Text); MySqlParameters.Add(_PURCHASED_FROM);
                    MySqlParameter _AUTHENTICATION_NUMBER = new MySqlParameter("_AUTHENTICATION_NUMBER", userTextBoxAuthenticationNumber.Text); MySqlParameters.Add(_AUTHENTICATION_NUMBER);
                    MySqlParameter _PURCHASE_DATE = new MySqlParameter("_PURCHASE_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerPurchaseDate.Value)); MySqlParameters.Add(_PURCHASE_DATE);
                    MySqlParameter _NOTARY_TAXES = new MySqlParameter("_NOTARY_TAXES", userTextBoxNotaryTaxes.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxNotaryTaxes.Text)); MySqlParameters.Add(_NOTARY_TAXES);
                    MySqlParameter _RECEIPT_NUMBER = new MySqlParameter("_RECEIPT_NUMBER", userTextBoxReceiptNumber.Text); MySqlParameters.Add(_RECEIPT_NUMBER);
                    MySqlParameter _AGENCY_COMMISSION = new MySqlParameter("_AGENCY_COMMISSION", userTextBoxAgencyCommission.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxAgencyCommission.Text)); MySqlParameters.Add(_AGENCY_COMMISSION);
                    MySqlParameter _PURCHASING_PRICE = new MySqlParameter("_PURCHASING_PRICE", userTextBoxPurchasePrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxPurchasePrice.Text)); MySqlParameters.Add(_PURCHASING_PRICE);
                    MySqlParameter _PURCHASING_VAT = new MySqlParameter("_PURCHASING_VAT", userTextBoxPurchasingVAT.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxPurchasingVAT.Text)); MySqlParameters.Add(_PURCHASING_VAT);
                    MySqlParameter _SOLD_TO = new MySqlParameter("_SOLD_TO", userTextBoxSoldTo.Text); MySqlParameters.Add(_SOLD_TO);
                    MySqlParameter _SELLING_DATE = new MySqlParameter("_SELLING_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerSellingDate.Value)); MySqlParameters.Add(_SELLING_DATE);
                    MySqlParameter _SELLING_PRICE = new MySqlParameter("_SELLING_PRICE", userTextBoxSellingPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxSellingPrice.Text)); MySqlParameters.Add(_SELLING_PRICE);
                    MySqlParameter _SELLING_VAT = new MySqlParameter("_SELLING_VAT", userTextBoxSellingVAT.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxSellingVAT.Text)); MySqlParameters.Add(_SELLING_VAT);
                    MySqlParameter _INCLUDE_FOR_AGENCIES = new MySqlParameter("_INCLUDE_FOR_AGENCIES", checkBoxIncludeForAgencies.Checked); MySqlParameters.Add(_INCLUDE_FOR_AGENCIES);
                    MySqlParameter _REEVALUATION_DATE = new MySqlParameter("_REEVALUATION_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerReevaluationDate.Value)); MySqlParameters.Add(_REEVALUATION_DATE);
                    MySqlParameter _REEVALUATION_VALUE = new MySqlParameter("_REEVALUATION_VALUE", userTextBoxReevaluationValue.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxReevaluationValue.Text)); MySqlParameters.Add(_REEVALUATION_VALUE);
                    MySqlParameter _OPTIONAL_INSURANCE_COMPANY = new MySqlParameter("_OPTIONAL_INSURANCE_COMPANY", userTextBoxOptionalInsuranceCompany.Text); MySqlParameters.Add(_OPTIONAL_INSURANCE_COMPANY);
                    MySqlParameter _APARTMENT_OPTIONAL_INSURANCE_POLICY_NUMBER = new MySqlParameter("_APARTMENT_OPTIONAL_INSURANCE_POLICY_NUMBER", userTextBoxAppartmentOptionalInsurancePolicyNumber.Text); MySqlParameters.Add(_APARTMENT_OPTIONAL_INSURANCE_POLICY_NUMBER);
                    MySqlParameter _CONTENTS_OPTIONAL_INSURANCE_POLICY_NUMBER = new MySqlParameter("_CONTENTS_OPTIONAL_INSURANCE_POLICY_NUMBER", userTextBoxContentsOptionalInsurancePolicyNumber.Text); MySqlParameters.Add(_CONTENTS_OPTIONAL_INSURANCE_POLICY_NUMBER);
                    MySqlParameter _OPTIONAL_INSURANCE_POLICY_EXPIRATION_DATE = new MySqlParameter("_OPTIONAL_INSURANCE_POLICY_EXPIRATION_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerOptionalInsurancePolicyExpirationDate.Value)); MySqlParameters.Add(_OPTIONAL_INSURANCE_POLICY_EXPIRATION_DATE);
                    MySqlParameter _MANDATORY_INSURANCE_COMPANY = new MySqlParameter("_MANDATORY_INSURANCE_COMPANY", userTextBoxMandatoryInsuranceCompany.Text); MySqlParameters.Add(_MANDATORY_INSURANCE_COMPANY);
                    MySqlParameter _MANDATORY_INSURANCE_POLICY_NUMBER = new MySqlParameter("_MANDATORY_INSURANCE_POLICY_NUMBER", userTextBoxMandatoryInsurancePolicyNumber.Text); MySqlParameters.Add(_MANDATORY_INSURANCE_POLICY_NUMBER);
                    MySqlParameter _MANDATORY_INSURANCE_POLICY_EXPIRATION_DATE = new MySqlParameter("_MANDATORY_INSURANCE_POLICY_EXPIRATION_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerMandatoryInsurancePolicyExpirationDate.Value)); MySqlParameters.Add(_MANDATORY_INSURANCE_POLICY_EXPIRATION_DATE);
                    //--
                    MySqlParameter _STORAGE_ROOM = new MySqlParameter("_STORAGE_ROOM", userTextBoxStorageRoom.Text); MySqlParameters.Add(_STORAGE_ROOM);
                    MySqlParameter _OWNERS_ASSOCIATION_NAME = new MySqlParameter("_OWNERS_ASSOCIATION_NAME", userTextBoxAssociationName.Text); MySqlParameters.Add(_OWNERS_ASSOCIATION_NAME);
                    MySqlParameter _OWNERS_ASSOCIATION_CIF = new MySqlParameter("_OWNERS_ASSOCIATION_CIF", userTextBoxAssociationCif.Text); MySqlParameters.Add(_OWNERS_ASSOCIATION_CIF);
                    MySqlParameter _INTERNET_PROVIDER_REPRESENTATIVE_INFORMATION = new MySqlParameter("_INTERNET_PROVIDER_REPRESENTATIVE_INFORMATION", userTextBoxInternetRepresentative.Text); MySqlParameters.Add(_INTERNET_PROVIDER_REPRESENTATIVE_INFORMATION);
                    MySqlParameter _TELEPHONE_PROVIDER_REPRESENTATIVE_INFORMATION = new MySqlParameter("_TELEPHONE_PROVIDER_REPRESENTATIVE_INFORMATION", userTextBoxTelephoneRepresentative.Text); MySqlParameters.Add(_TELEPHONE_PROVIDER_REPRESENTATIVE_INFORMATION);
                    MySqlParameter _TV_PROVIDER_REPRESENTATIVE_INFORMATION = new MySqlParameter("_TV_PROVIDER_REPRESENTATIVE_INFORMATION", userTextBoxTvRepresentative.Text); MySqlParameters.Add(_TV_PROVIDER_REPRESENTATIVE_INFORMATION);
                    MySqlParameter _GAS_USER = new MySqlParameter("_GAS_USER", userTextBoxGasUser.Text); MySqlParameters.Add(_GAS_USER);
                    MySqlParameter _GAS_PASSWORD = new MySqlParameter("_GAS_PASSWORD", userTextBoxGasPassword.Text); MySqlParameters.Add(_GAS_PASSWORD);
                    MySqlParameter _ELECTRICITY_USER = new MySqlParameter("_ELECTRICITY_USER", userTextBoxElectricityUser.Text); MySqlParameters.Add(_ELECTRICITY_USER);
                    MySqlParameter _ELECTRICITY_PASSWORD = new MySqlParameter("_ELECTRICITY_PASSWORD", userTextBoxElectricityPassword.Text); MySqlParameters.Add(_ELECTRICITY_PASSWORD);
                    MySqlParameter _FISCAL_ADMINISTRATION_USER = new MySqlParameter("_FISCAL_ADMINISTRATION_USER", userTextBoxFiscalUser.Text); MySqlParameters.Add(_FISCAL_ADMINISTRATION_USER);
                    MySqlParameter _FISCAL_ADMINISTRATION_PASSWORD = new MySqlParameter("_FISCAL_ADMINISTRATION_PASSWORD", userTextBoxFiscalPassword.Text); MySqlParameters.Add(_FISCAL_ADMINISTRATION_PASSWORD);
                    MySqlParameter _INDIVIDUAL_SELLING_PRICE = new MySqlParameter("_INDIVIDUAL_SELLING_PRICE", userTextBoxIndividualSellingPrice.Text.Trim()==""?DBNull.Value:(object)Convert.ToDouble(userTextBoxIndividualSellingPrice.Text)); MySqlParameters.Add(_INDIVIDUAL_SELLING_PRICE);
                    MySqlParameter _COMPANY_SELLING_PRICE = new MySqlParameter("_COMPANY_SELLING_PRICE", userTextBoxCompanySellingPrice.Text.Trim() == "" ? DBNull.Value : (object)Convert.ToDouble(userTextBoxCompanySellingPrice.Text)); MySqlParameters.Add(_COMPANY_SELLING_PRICE);
                    // --
                    MySqlParameter _REGISTRATION_DATE = new MySqlParameter("_REGISTRATION_DATE", dateTimePickerRegistrationDate.Value); MySqlParameters.Add(_REGISTRATION_DATE);
                    MySqlParameter _SIGNED_PURCHASE_CONTRACT = new MySqlParameter("_SIGNED_PURCHASE_CONTRACT", checkBoxSignedPurchaseContract.Checked); MySqlParameters.Add(_SIGNED_PURCHASE_CONTRACT);
                    MySqlParameter _FURNISHED_BY_COMPANY = new MySqlParameter("_FURNISHED_BY_COMPANY", checkBoxFurnishedByCompany.Checked); MySqlParameters.Add(_FURNISHED_BY_COMPANY);
                    MySqlParameter _GAS_CONTRACT_DONE_BY_COMPANY = new MySqlParameter("_GAS_CONTRACT_DONE_BY_COMPANY", checkBoxGasContractDoneByCompany.Checked); MySqlParameters.Add(_GAS_CONTRACT_DONE_BY_COMPANY);
                    MySqlParameter _ELECTRICITY_CONTRACT_DONE_BY_COMPANY = new MySqlParameter("_ELECTRICITY_CONTRACT_DONE_BY_COMPANY", checkBoxElectricityContractDoneByCompany.Checked); MySqlParameters.Add(_ELECTRICITY_CONTRACT_DONE_BY_COMPANY);
                    MySqlParameter _INSURANCE_DONE_BY_COMPANY = new MySqlParameter("_INSURANCE_DONE_BY_COMPANY", checkBoxInsuranceDoneByCompany.Checked); MySqlParameters.Add(_INSURANCE_DONE_BY_COMPANY);
                    MySqlParameter _SOLD_BY_COMPANY = new MySqlParameter("_SOLD_BY_COMPANY", checkBoxSoldByCompany.Checked); MySqlParameters.Add(_SOLD_BY_COMPANY);
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_insert", MySqlParameters.ToArray());
                    //da.ExecuteInsertQuery();
                    NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    //InvoiceRequirementsClass.InsertFromProperty(NewDR); // NOT THE CASE BECAUSE THERE CAN NOT BE A CONTRACT WITHOUT THE PROPERTY
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
            errorProvider1.SetError(comboBoxOwner, "");
            errorProvider1.SetError(comboBoxPropertyStatus, "");
            errorProvider1.SetError(comboBoxPropertyType, "");

            errorProvider1.SetError(userTextBoxCondominiumFee, "");
            errorProvider1.SetError(userTextBoxFloatingCapital, "");
            errorProvider1.SetError(userTextBoxAverageAproximatedConsumption, "");

            errorProvider1.SetError(userTextBoxRooms, "");
            errorProvider1.SetError(userTextBoxFloor, "");
            errorProvider1.SetError(userTextBoxBuildingFloors, "");
            errorProvider1.SetError(userTextBoxAppartmentArea, "");
            errorProvider1.SetError(userTextBoxTerraceArea, "");
            errorProvider1.SetError(userTextBoxTotalArea, "");
            errorProvider1.SetError(userTextBoxCommonSpaces, "");
            errorProvider1.SetError(userTextBoxTotalAreaIncludingCommonSpaces, "");

            errorProvider1.SetError(userTextBoxIndividualCompletelyFurnishedPrice, "");
            errorProvider1.SetError(userTextBoxCompanyCompletelyFurnishedPrice, "");
            errorProvider1.SetError(userTextBoxIndividualKitchenFurnishedPrice, "");
            errorProvider1.SetError(userTextBoxCompanyKitchenFurnishedPrice, "");
            errorProvider1.SetError(userTextBoxIndividualEmptyPrice, "");
            errorProvider1.SetError(userTextBoxCompanyEmptyPrice, "");

            errorProvider1.SetError(userTextBoxNotaryTaxes, "");
            errorProvider1.SetError(userTextBoxAgencyCommission, "");
            errorProvider1.SetError(userTextBoxPurchasePrice, "");
            errorProvider1.SetError(userTextBoxPurchasingVAT, "");

            errorProvider1.SetError(userTextBoxSellingPrice, "");
            errorProvider1.SetError(userTextBoxSellingVAT, "");
            errorProvider1.SetError(userTextBoxReevaluationValue, "");

            errorProvider1.SetError(userTextBoxIndividualSellingPrice, "");
            errorProvider1.SetError(userTextBoxCompanySellingPrice, "");

            errorProvider1.SetError(userTextBoxSoldTo, "");

            if (userTextBoxPropertyName.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxPropertyName, Language.GetErrorText("errorEmptyPropertyName", "Property Name can not by empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxPropertyName.Name, Language.GetErrorText("errorEmptyPropertyName", "Property Name can not by empty!")));
                toReturn = false;
            }
            if (comboBoxOwner.SelectedValue == null || comboBoxOwner.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxOwner, Language.GetErrorText("errorEmptyPropertyOwner", "You must select an owner for the property!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwner.Name, Language.GetErrorText("errorEmptyPropertyOwner", "You must select an owner for the property!")));
                toReturn = false;
            }
            if (comboBoxPropertyStatus.SelectedValue == null || comboBoxPropertyStatus.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxPropertyStatus, Language.GetErrorText("errorEmptyPropertyStatus", "You must select a status for the property!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxPropertyStatus.Name, Language.GetErrorText("errorEmptyPropertyStatus", "You must select a status for the property!")));
                toReturn = false;
            }
            if (comboBoxPropertyType.SelectedValue == null || comboBoxPropertyType.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxPropertyType, Language.GetErrorText("errorEmptyPropertyType", "You must select a type for the property!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxPropertyType.Name, Language.GetErrorText("errorEmptyPropertyType", "You must select a type for the property!")));
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
            if (!Validator.IsDouble(userTextBoxBathrooms.Text))
            {
                errorProvider1.SetError(userTextBoxBathrooms, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxBathrooms.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxAverageAproximatedConsumption.Text))
            {
                errorProvider1.SetError(userTextBoxAverageAproximatedConsumption, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxAverageAproximatedConsumption.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsInteger(userTextBoxRooms.Text))
            {
                errorProvider1.SetError(userTextBoxRooms, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxRooms.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsInteger(userTextBoxFloor.Text))
            {
                errorProvider1.SetError(userTextBoxFloor, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxFloor.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsInteger(userTextBoxBuildingFloors.Text))
            {
                errorProvider1.SetError(userTextBoxBuildingFloors, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxBuildingFloors.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxAppartmentArea.Text))
            {
                errorProvider1.SetError(userTextBoxAppartmentArea, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxAppartmentArea.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxTerraceArea.Text))
            {
                errorProvider1.SetError(userTextBoxTerraceArea, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxTerraceArea.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxTotalArea.Text))
            {
                errorProvider1.SetError(userTextBoxTotalArea, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxTotalArea.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxCommonSpaces.Text))
            {
                errorProvider1.SetError(userTextBoxCommonSpaces, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxCommonSpaces.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxTotalAreaIncludingCommonSpaces.Text))
            {
                errorProvider1.SetError(userTextBoxTotalAreaIncludingCommonSpaces, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxTotalAreaIncludingCommonSpaces.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxIndividualCompletelyFurnishedPrice.Text))
            {
                errorProvider1.SetError(userTextBoxIndividualCompletelyFurnishedPrice, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxIndividualCompletelyFurnishedPrice.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxCompanyCompletelyFurnishedPrice.Text))
            {
                errorProvider1.SetError(userTextBoxCompanyCompletelyFurnishedPrice, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxCompanyCompletelyFurnishedPrice.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxIndividualKitchenFurnishedPrice.Text))
            {
                errorProvider1.SetError(userTextBoxIndividualKitchenFurnishedPrice, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxIndividualKitchenFurnishedPrice.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxCompanyKitchenFurnishedPrice.Text))
            {
                errorProvider1.SetError(userTextBoxCompanyKitchenFurnishedPrice, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxCompanyKitchenFurnishedPrice.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxIndividualEmptyPrice.Text))
            {
                errorProvider1.SetError(userTextBoxIndividualEmptyPrice, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxIndividualEmptyPrice.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxCompanyEmptyPrice.Text))
            {
                errorProvider1.SetError(userTextBoxCompanyEmptyPrice, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxCompanyEmptyPrice.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxNotaryTaxes.Text))
            {
                errorProvider1.SetError(userTextBoxNotaryTaxes, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxNotaryTaxes.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxAgencyCommission.Text))
            {
                errorProvider1.SetError(userTextBoxAgencyCommission, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxAgencyCommission.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxPurchasePrice.Text))
            {
                errorProvider1.SetError(userTextBoxPurchasePrice, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxPurchasePrice.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxPurchasingVAT.Text))
            {
                errorProvider1.SetError(userTextBoxPurchasingVAT, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxPurchasingVAT.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxSellingPrice.Text))
            {
                errorProvider1.SetError(userTextBoxSellingPrice, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxSellingPrice.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxSellingVAT.Text))
            {
                errorProvider1.SetError(userTextBoxSellingVAT, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxSellingVAT.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxReevaluationValue.Text))
            {
                errorProvider1.SetError(userTextBoxReevaluationValue, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxReevaluationValue.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxIndividualSellingPrice.Text))
            {
                errorProvider1.SetError(userTextBoxIndividualSellingPrice, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxIndividualSellingPrice.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            if (!Validator.IsDouble(userTextBoxCompanySellingPrice.Text))
            {
                errorProvider1.SetError(userTextBoxCompanySellingPrice, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxCompanySellingPrice.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }

            if (userTextBoxSoldTo.Text.Trim() != "" && userTextBoxSellingPrice.Text.Trim() != "")
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("propertyStatusModification", "Modifying \"Sold to\" and \"Selling price\" fileds will change the status of the property to \"Sold\" making further modifications imposible!"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (ans == DialogResult.Cancel)
                {
                    errorProvider1.SetError(userTextBoxSellingPrice, Language.GetMessageBoxText("propertyStatusModification", "Modifying \"Sold to\" and \"Selling price\" fileds will change the status of the property to \"Sold\" making further modifications imposible!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxSellingPrice.Name, Language.GetMessageBoxText("propertyStatusModification", "Modifying \"Sold to\" and \"Selling price\" fileds will change the status of the property to \"Sold\" making further modifications imposible!")));
                    errorProvider1.SetError(userTextBoxSoldTo, Language.GetMessageBoxText("propertyStatusModification", "Modifying \"Sold to\" and \"Selling price\" fileds will change the status of the property to \"Sold\" making further modifications imposible!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxSoldTo.Name, Language.GetMessageBoxText("propertyStatusModification", "Modifying \"Sold to\" and \"Selling price\" fileds will change the status of the property to \"Sold\" making further modifications imposible!")));
                    toReturn = false;
                }
                else
                {
                    comboBoxPropertyStatus.SelectedValue = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Sold"), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery();
                }
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

        private void comboBoxPropertyProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((ComboBox)sender).SelectedValue);
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROJECTSsp_get_by_id", new object[] { new MySqlParameter("_ID", id) });
                DataRow project = da.ExecuteSelectQuery().Tables[0].Rows[0];
                //userTextBoxPropertyLocation.Text = userTextBoxPropertyLocation.Text.Trim() == ""?project["location"].ToString():userTextBoxPropertyLocation.Text;
                userTextBoxPropertyLocation.Text = project["location"].ToString();
                //comboBoxPropertyCity.SelectedValue = CommonFunctions.SetNullable(comboBoxPropertyCity) == DBNull.Value ? project["city_id"] : comboBoxPropertyCity.SelectedValue;
                comboBoxPropertyCity.SelectedValue = project["city_id"];
                //userTextBoxPropertyAddress.Text = userTextBoxPropertyAddress.Text.Trim() == "" ? project["address"].ToString() : userTextBoxPropertyAddress.Text;
                userTextBoxPropertyAddress.Text = project["address"].ToString();
                //userTextBoxAdministratorName.Text = userTextBoxAdministratorName.Text.Trim() == "" ? project["administrator_name"].ToString() : userTextBoxAdministratorName.Text;
                userTextBoxAdministratorName.Text = project["administrator_name"].ToString();
                /*
                listBoxPropertyAdministratorPhones.Items.Clear();
                listBoxPropertyAdministratorEmails.Items.Clear();
                listBoxPresidentOwnerAssociationPhones.Items.Clear();
                listBoxPresidentOwnerAssociationEmails.Items.Clear();
                listBoxMaintenancePersonPhones.Items.Clear();
                listBoxMaintenancePersonEmails.Items.Clear();
                listBoxDevelopperPhones.Items.Clear();
                listBoxDevelopperEmails.Items.Clear();
                */

                //if (listBoxPropertyAdministratorPhones.Items.Count == 0)
                {
                    try
                    {
                        listBoxPropertyAdministratorPhones.Items.Clear();
                        string[] administrator_phones = project["administrator_phones"].ToString().Split(';');
                        foreach (string phone in administrator_phones)
                        {
                            listBoxPropertyAdministratorPhones.Items.Add(phone);
                        }
                    }
                    catch { }
                }
                //if (listBoxPropertyAdministratorEmails.Items.Count == 0)
                {
                    try
                    {
                        listBoxPropertyAdministratorEmails.Items.Clear();
                        string[] administrator_emails = project["administrator_emails"].ToString().Split(';');
                        foreach (string email in administrator_emails)
                        {
                            listBoxPropertyAdministratorEmails.Items.Add(email);
                        }
                    }
                    catch { }
                }
                //userTextBoxPropertyPresidentOwnereAssociationName.Text = userTextBoxPropertyPresidentOwnereAssociationName.Text.Trim() == "" ? project["president_owners_association_name"].ToString() : userTextBoxPropertyPresidentOwnereAssociationName.Text;
                userTextBoxPropertyPresidentOwnereAssociationName.Text = project["president_owners_association_name"].ToString();
                //if (listBoxPresidentOwnerAssociationPhones.Items.Count == 0)
                {
                    try
                    {
                        listBoxPresidentOwnerAssociationPhones.Items.Clear();
                        string[] president_owner_association_phones = project["president_owners_association_phones"].ToString().Split(';');
                        foreach (string phone in president_owner_association_phones)
                        {
                            listBoxPresidentOwnerAssociationPhones.Items.Add(phone);
                        }
                    }
                    catch { }
                }
                //if (listBoxPresidentOwnerAssociationEmails.Items.Count == 0)
                {
                    try
                    {
                        listBoxPresidentOwnerAssociationEmails.Items.Clear();
                        string[] president_owner_association_emails = project["president_owners_association_emails"].ToString().Split(';');
                        foreach (string email in president_owner_association_emails)
                        {
                            listBoxPresidentOwnerAssociationEmails.Items.Add(email);
                        }
                    }
                    catch { }
                }
                //userTextBoxAssociationBankName.Text = userTextBoxAssociationBankName.Text.Trim() == "" ? project["owners_association_bank"].ToString() : userTextBoxAssociationBankName.Text;
                userTextBoxAssociationBankName.Text = project["owners_association_bank"].ToString();
                //userTextBoxAssociationBankAccount.Text = userTextBoxAssociationBankAccount.Text.Trim() == "" ? project["owners_association_bank_account"].ToString() : userTextBoxAssociationBankAccount.Text;
                userTextBoxAssociationBankAccount.Text = project["owners_association_bank_account"].ToString();
                //userTextBoxMaintenancePersonName.Text = userTextBoxMaintenancePersonName.Text.Trim() == "" ? project["maintenance_person_name"].ToString() : userTextBoxMaintenancePersonName.Text;
                userTextBoxMaintenancePersonName.Text = project["maintenance_person_name"].ToString();
                //if (listBoxMaintenancePersonPhones.Items.Count == 0)
                {
                    try
                    {
                        listBoxMaintenancePersonPhones.Items.Clear();
                        string[] maintenance_person_phones = project["maintenance_person_phones"].ToString().Split(';');
                        foreach (string phone in maintenance_person_phones)
                        {
                            listBoxMaintenancePersonPhones.Items.Add(phone);
                        }
                    }
                    catch { }
                }
                //if (listBoxMaintenancePersonEmails.Items.Count == 0)
                {
                    try
                    {
                        listBoxMaintenancePersonEmails.Items.Clear();
                        string[] maintenance_person_emails = project["maintenance_person_emails"].ToString().Split(';');
                        foreach (string email in maintenance_person_emails)
                        {
                            listBoxMaintenancePersonEmails.Items.Add(email);
                        }
                    }
                    catch { }
                }
                //userTextBoxInternetProviderInformation.Text = userTextBoxInternetProviderInformation.Text.Trim() == "" ? project["internet_provider_information"].ToString() : userTextBoxInternetProviderInformation.Text;
                userTextBoxInternetProviderInformation.Text = project["internet_provider_information"].ToString();
                //userTextBoxTelephoneProviderInformation.Text = userTextBoxTelephoneProviderInformation.Text.Trim() == "" ? project["telephone_provider_information"].ToString() : userTextBoxTelephoneProviderInformation.Text;
                userTextBoxTelephoneProviderInformation.Text = project["telephone_provider_information"].ToString();
                //userTextBoxTVProviderInformation.Text = userTextBoxTVProviderInformation.Text.Trim() == "" ? project["tv_provider_information"].ToString() : userTextBoxTVProviderInformation.Text;
                userTextBoxTVProviderInformation.Text = project["tv_provider_information"].ToString();
                //userTextBoxRepresentativeInformation.Text = userTextBoxRepresentativeInformation.Text.Trim() == "" ? project["representative_information"].ToString() : userTextBoxRepresentativeInformation.Text;
                //userTextBoxRepresentativeInformation.Text = project["representative_information"].ToString();
                //userTextBoxDevelopperName.Text = userTextBoxDevelopperName.Text.Trim() == "" ? project["developer_name"].ToString() : userTextBoxDevelopperName.Text;
                userTextBoxDevelopperName.Text = project["developer_name"].ToString();
                //userTextBoxDevelopperContactPersonName.Text = userTextBoxDevelopperContactPersonName.Text.Trim() == "" ? project["developer_contact_person"].ToString() : userTextBoxDevelopperContactPersonName.Text;
                userTextBoxDevelopperContactPersonName.Text = project["developer_contact_person"].ToString();
                //if (listBoxDevelopperPhones.Items.Count == 0)
                {
                    try
                    {
                        listBoxDevelopperPhones.Items.Clear();
                        string[] developer_phones = project["developer_phones"].ToString().Split(';');
                        foreach (string phone in developer_phones)
                        {
                            listBoxDevelopperPhones.Items.Add(phone);
                        }
                    }
                    catch { }
                }
                //if (listBoxDevelopperEmails.Items.Count == 0)
                {
                    try
                    {
                        listBoxDevelopperEmails.Items.Clear();
                        string[] developer_emails = project["developer_emails"].ToString().Split(';');
                        foreach (string email in developer_emails)
                        {
                            listBoxDevelopperEmails.Items.Add(email);
                        }
                    }
                    catch { }
                }
                userTextBoxCondominiumFee.Text = project["condominium_fee"].ToString();
                userTextBoxFloatingCapital.Text = project["floating_capital"].ToString();
                userTextBoxInternetRepresentative.Text = project["internet_provider_representative_information"].ToString();
                userTextBoxTelephoneRepresentative.Text = project["telephone_provider_representative_information"].ToString();
                userTextBoxTvRepresentative.Text = project["tv_provider_representative_information"].ToString();
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }

        private void comboBoxParentProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(((ComboBox)sender).SelectedValue);
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[] { new MySqlParameter("_ID", id) });
                DataRow property = da.ExecuteSelectQuery().Tables[0].Rows[0];
                //comboBoxPropertyStatus.SelectedValue = CommonFunctions.SetNullable(comboBoxPropertyStatus) == DBNull.Value ? property["status_id"] : comboBoxPropertyStatus.SelectedValue;
                comboBoxPropertyStatus.SelectedValue = property["status_id"];
                checkBoxPropertyManagement.Checked = (bool)property["property_management"];
                //comboBoxOwner.SelectedValue = CommonFunctions.SetNullable(comboBoxOwner) == DBNull.Value ? property["owner_id"] : comboBoxOwner.SelectedValue;
                comboBoxOwner.SelectedValue = property["owner_id"];
                checkBoxPropertyPOA.Checked = (bool)property["poa"];
                comboBoxPropertyProject.SelectedValue = property["project_id"];
            }
            catch { }
        }

        private void buttonRoomsRight_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList aList = new ArrayList();

                foreach (object x in cCheckedListBoxAvailableRooms.CheckedItems)
                {
                    //DataRow dr_left = EmployeesTable.Select(String.Format("ID = {0}", ((DataRowView)x).Row["id"].ToString()))[0];
                    DataRow dr_left = ((DataRowView)x).Row;
                    DataRow dr_right = AssignedPropertyRooms.NewRow();
                    foreach (DataColumn dc in AvailablePropertyRooms.Columns)
                    {
                        //if (dc.ColumnName.ToLower() != "id")
                        dr_right[dc.ColumnName] = dr_left[dc.ColumnName];
                    }
                    AssignedPropertyRooms.Rows.Add(dr_right);
                    //dr_left.Delete();
                    aList.Add(dr_left);
                }

                foreach (DataRow dr in aList)
                {
                    dr.Delete();
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("unidentifiedError", "There was an unidentified error executed command:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRoomsLeft_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList aList = new ArrayList();

                foreach (object x in cCheckedListBoxSelectedRooms.CheckedItems)
                {
                    DataRow dr_left = ((DataRowView)x).Row;
                    DataRow dr_right = AvailablePropertyRooms.NewRow();
                    foreach (DataColumn dc in AvailablePropertyRooms.Columns)
                    {
                        //if (dc.ColumnName.ToLower() != "id")
                        dr_right[dc.ColumnName] = dr_left[dc.ColumnName];
                    }
                    AvailablePropertyRooms.Rows.Add(dr_right);
                    //dr_left.Delete();
                    aList.Add(dr_left);
                }

                foreach (DataRow dr in aList)
                {
                    dr.Delete();
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("unidentifiedError", "There was an unidentified error executed command:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cCheckedListBoxSelectedRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow dr = ((DataRowView) ((cCheckedListBox)sender).SelectedItem ).Row;
            if (dr.RowState == DataRowState.Unchanged)
            {
                groupBoxAssets.Enabled = true;
                FillRoomAssets(Convert.ToInt32(dr["id"]));
                labelWarningSavePropertyRooms.Visible = false;
            }
            else
            {
                groupBoxAssets.Enabled = false;
                labelWarningSavePropertyRooms.Visible = true;
            }
        }

        private void buttonAssetsRight_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList aList = new ArrayList();

                foreach (object x in cCheckedListBoxAvailableAssets.CheckedItems)
                {
                    //DataRow dr_left = EmployeesTable.Select(String.Format("ID = {0}", ((DataRowView)x).Row["id"].ToString()))[0];
                    DataRow dr_left = ((DataRowView)x).Row;
                    DataRow dr_right = AssignedRoomAssets.NewRow();
                    foreach (DataColumn dc in AvailableRoomAssets.Columns)
                    {
                        //if (dc.ColumnName.ToLower() != "id")
                        dr_right[dc.ColumnName] = dr_left[dc.ColumnName];
                    }
                    AssignedRoomAssets.Rows.Add(dr_right);
                    //dr_left.Delete();
                    aList.Add(dr_left);
                }

                foreach (DataRow dr in aList)
                {
                    dr.Delete();
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("unidentifiedError", "There was an unidentified error executed command:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonAssetsLeft_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList aList = new ArrayList();

                foreach (object x in cCheckedListBoxSelectedAssets.CheckedItems)
                {
                    DataRow dr_left = ((DataRowView)x).Row;
                    DataRow dr_right = AvailableRoomAssets.NewRow();
                    foreach (DataColumn dc in AvailableRoomAssets.Columns)
                    {
                        //if (dc.ColumnName.ToLower() != "id")
                        dr_right[dc.ColumnName] = dr_left[dc.ColumnName];
                    }
                    AvailableRoomAssets.Rows.Add(dr_right);
                    //dr_left.Delete();
                    aList.Add(dr_left);
                }

                foreach (DataRow dr in aList)
                {
                    dr.Delete();
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("unidentifiedError", "There was an unidentified error executed command:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSavePropertyRooms_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in AssignedPropertyRooms.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        object[] mySqlParams = new object[] { new MySqlParameter("_PROPERTY_ID", NewDR["id"]), new MySqlParameter("_PROPERTYROOM_ID", dr["id"]) };
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIES_PROPERTYROOMSsp_insert", mySqlParams);
                        da.ExecuteInsertQuery();
                    }
                }
                foreach (DataRow dr in AvailablePropertyRooms.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        object[] mySqlParams = new object[] { new MySqlParameter("_PROPERTY_ID", NewDR["id"]), new MySqlParameter("_PROPERTYROOM_ID", dr["id"]) };
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIES_PROPERTYROOMSsp_delete", mySqlParams);
                        da.ExecuteUpdateQuery();
                    }
                }
                AvailablePropertyRooms.AcceptChanges();
                AssignedPropertyRooms.AcceptChanges();
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveRoomAssets_Click(object sender, EventArgs e)
        {
            try
            {
                int propertyroom_id = Convert.ToInt32(((DataRowView)cCheckedListBoxSelectedRooms.SelectedItem).Row["id"]);
                foreach (DataRow dr in AssignedRoomAssets.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        object[] mySqlParams = new object[] { new MySqlParameter("_PROPERTYROOM_ID", propertyroom_id), new MySqlParameter("_ROOMASSET_ID", dr["id"]) };
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTYROOMS_ROOMASSETSsp_insert", mySqlParams);
                        da.ExecuteInsertQuery();
                    }
                }
                foreach (DataRow dr in AvailableRoomAssets.Rows)
                {
                    if (dr.RowState == DataRowState.Added)
                    {
                        object[] mySqlParams = new object[] { new MySqlParameter("_PROPERTYROOM_ID", propertyroom_id), new MySqlParameter("_ROOMASSET_ID", dr["id"]) };
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTYROOMS_ROOMASSETSsp_delete", mySqlParams);
                        da.ExecuteUpdateQuery();
                    }
                }
                AvailableRoomAssets.AcceptChanges();
                AssignedRoomAssets.AcceptChanges();
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void pictureBoxProjectSelect_Click(object sender, EventArgs e)
        {
            var f = new ProjectSelect(true);
            f.dataGrid1.Selectable = true;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxPropertyProject.SelectedValue = f.dataGrid1.IdToReturn;
            }
            f.Dispose();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            var f = new Cities(true);
            f.dataGrid1.Selectable = true;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxPropertyCity.SelectedValue = f.dataGrid1.IdToReturn;
            }
            f.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var f = new ProportySelect(true);
            f.dataGrid1.Selectable = true;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Maximized = FormWindowState.Normal;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxParentProperty.SelectedValue = f.dataGrid1.IdToReturn;
            }
            f.Dispose();
        }

        private void userTextBoxGasContract_TextChanged(object sender, EventArgs e)
        {
            //if (((UserTextBox)sender).Text.Trim() != "")
            //{
            //    checkBoxGasContractDoneByCompany.Checked = true;
            //}
        }

        private void userTextBoxElectricityContract_TextChanged(object sender, EventArgs e)
        {
            //if (((UserTextBox)sender).Text.Trim() != "")
            //{
            //    checkBoxElectricityContractDoneByCompany.Checked = true;
            //}
        }

        private void dateTimePickerRegistrationDate_ValueChanged(object sender, EventArgs e)
        {
            //checkBoxRegisteredProperty.Checked = true;
        }

        private void dateTimePickerSellingDate_ValueChanged(object sender, EventArgs e)
        {
            //checkBoxSoldByCompany.Checked = true;
        }

        private void userTextBoxSellingPrice_TextChanged(object sender, EventArgs e)
        {
            //if (((UserTextBox)sender).Text.Trim() != "")
            //{
            //    checkBoxSoldByCompany.Checked = true;
            //}
        }

        private void userTextBoxAppartmentOptionalInsurancePolicyNumber_TextChanged(object sender, EventArgs e)
        {
            //if (((UserTextBox)sender).Text.Trim() != "")
            //{
            //    checkBoxInsuranceDoneByCompany.Checked = true;
            //}
        }

        private void userTextBoxMandatoryInsurancePolicyNumber_TextChanged(object sender, EventArgs e)
        {
            //if (((UserTextBox)sender).Text.Trim() != "")
            //{
            //    checkBoxInsuranceDoneByCompany.Checked = true;
            //}
        }

        private void dateTimePickerPurchaseDate_ValueChanged(object sender, EventArgs e)
        {
            //checkBoxSignedPurchaseContract.Checked = true;
        }

        private void userTextBoxPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            //if (((UserTextBox)sender).Text.Trim() != "")
            //{
            //    checkBoxSignedPurchaseContract.Checked = true;
            //}
        }

        private void checkBoxGasContractDoneByCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
        }

        private void checkBoxElectricityContractDoneByCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
        }

        private void checkBoxRegisteredProperty_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
        }

        private void checkBoxFurnishedByCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
        }

        private void checkBoxSignedPurchaseContract_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
        }

        private void checkBoxSoldByCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
        }

        private void checkBoxInsuranceDoneByCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked) ((CheckBox)sender).BackColor = Color.Red;
        }
    }
}
