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
using System.Threading;
using System.IO;

namespace FDP
{
    public partial class Contracts : UserForm
    {
        public DataRow NewDR;
        public DataRow InitialDR;
        public DataRow InitialExternalDR;
        public ArrayList MySqlParameters = new ArrayList();
        public int ParentContractId;
        public DataTable ContractServices = new DataTable("services");
        public DataTable ServicesServicetypes;
        public DataTable ServicesAdditionalCosts;

        public Contracts()
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            //LoaderClass lc = new LoaderClass();
            //oThread = new Thread(new ThreadStart(lc.ShowLoader));
            //oThread.Priority = ThreadPriority.Highest;
            //oThread.Start();

            //oThread.Join(1000);
            //SuspendResumeLayout.SuspendDrawing(this);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true); 
            InitializeComponent();
            //SuspendResumeLayout.ResumeDrawing(this);
            //oThread.Abort();
            //Language.LoadLabels(this);
        }

        public Contracts(int id, string id_type)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            //LoaderClass lc = new LoaderClass();
            //oThread = new Thread(new ThreadStart(lc.ShowLoader));
            //oThread.Start();
            //SuspendResumeLayout.SuspendDrawing(this);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true); 
            InitializeComponent();
            //SuspendResumeLayout.ResumeDrawing(this);
            DataAccess da = new DataAccess();
            switch (id_type)
            {
                case "id":
                    InitializeComponent();
                    //Language.LoadLabels(this);
                    da = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", id) });
                    NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    //this.dataGridContractsServices = new DataGrid("CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", NewDR["id"]) }, "CONTRACTS_PROPERTIES_SERVICESsp_insert", null, "CONTRACTS_PROPERTIES_SERVICESsp_update", null, "CONTRACTS_PROPERTIES_SERVICESsp_delete", null, null, null, null, null, null, new string[] { "SERVICE", "PRICE_VALUE", "PRICE_PERCENT", "PRICE_ONE_PAYMENT", "PRICE_VALUE_APPLICABLE", "PRICE_PERCENT_APPLICABLE", "PRICE_ONE_PAYMENT_APPLICABLE" }, false);
                    buttonSaveContract.Enabled = false;
                    break;
                case "contract_id":
                    ParentContractId = id;
                    InitializeComponent();
                    //Language.LoadLabels(this);
                    da = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ParentContractId) });
                    NewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    break;
            }
        }

        public Contracts(DataRow dr)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            //LoaderClass lc = new LoaderClass();
            //oThread = new Thread(new ThreadStart(lc.ShowLoader));
            //oThread.Start();
            //SuspendResumeLayout.SuspendDrawing(this);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true); 
            InitializeComponent();
            //SuspendResumeLayout.ResumeDrawing(this);
            //oThread.Abort();
            //Language.LoadLabels(this);
            NewDR = dr;
            //this.dataGridContractsServices = new DataGrid("CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", NewDR["id"]) }, "CONTRACTS_PROPERTIES_SERVICESsp_insert", null, "CONTRACTS_PROPERTIES_SERVICESsp_update", null, "CONTRACTS_PROPERTIES_SERVICESsp_delete", null, null, null, null, null, null, new string[] { "SERVICE", "PRICE_VALUE", "PRICE_PERCENT", "PRICE_ONE_PAYMENT", "PRICE_VALUE_APPLICABLE", "PRICE_PERCENT_APPLICABLE", "PRICE_ONE_PAYMENT_APPLICABLE" }, false);
            if (dr["parent_contract_id"] != DBNull.Value)
                ParentContractId = Convert.ToInt32(dr["parent_contract_id"]);
        }

        public Contracts(DataRow dr, int parent_contract_id)
        {
            base.Maximized = FormWindowState.Maximized;
            base.CheckDataOnClosing = true;
            //LoaderClass lc = new LoaderClass();
            //oThread = new Thread(new ThreadStart(lc.ShowLoader));
            //oThread.Start();
            //SuspendResumeLayout.SuspendDrawing(this);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true); 
            InitializeComponent();
            //SuspendResumeLayout.ResumeDrawing(this);
            //Language.LoadLabels(this);
            NewDR = dr;
            ParentContractId = parent_contract_id;
            //this.dataGridContractsServices = new DataGrid("CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", NewDR["id"]) }, "CONTRACTS_PROPERTIES_SERVICESsp_insert", null, "CONTRACTS_PROPERTIES_SERVICESsp_update", null, "CONTRACTS_PROPERTIES_SERVICESsp_delete", null, null, null, null, null, null, new string[] { "SERVICE", "PRICE_VALUE", "PRICE_PERCENT", "PRICE_ONE_PAYMENT", "PRICE_VALUE_APPLICABLE", "PRICE_PERCENT_APPLICABLE", "PRICE_ONE_PAYMENT_APPLICABLE" }, false);

            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_by_id", new object[] { new MySqlParameter("_ID", ParentContractId) });
            DataRow ParentNewDR = da.ExecuteSelectQuery().Tables[0].Rows[0];

            foreach (DataColumn dc in NewDR.Table.Columns)
            {
                try
                {
                    NewDR[dc.ColumnName] = ParentNewDR[dc.ColumnName];
                }
                catch { }
            }
            NewDR["parent_contract_id"] = ParentContractId;
        }

        #region --- obsolete ---
        /*
        private DataTable CreateServicesTable()
        {
            ////DataTable services = new DataTable("services");
            ////DataColumn dc = new DataColumn("service_id", System.Type.GetType("System.Int64"));
            ////dc.Caption = Language.GetColumnHeaderText("service_id", "SERVICE ID");
            ////services.Columns.Add(dc);

            ////dc = new DataColumn("price_value", System.Type.GetType("System.Double"));
            ////dc.Caption = Language.GetColumnHeaderText("price_value", "PRICE VALUE");
            ////services.Columns.Add(dc);

            ////dc = new DataColumn("price_value_applicable", System.Type.GetType("System.Boolean"));
            ////dc.Caption = Language.GetColumnHeaderText("price_value_applicable", "PRICE VALUE APPLICABLE");
            ////services.Columns.Add(dc);

            ////dc = new DataColumn("price_percent", System.Type.GetType("System.Double"));
            ////dc.Caption = Language.GetColumnHeaderText("price_percent", "PRICE PERCENT");
            ////services.Columns.Add(dc);

            ////dc = new DataColumn("price_percent_applicable", System.Type.GetType("System.Boolean"));
            ////dc.Caption = Language.GetColumnHeaderText("price_percent_applicable", "PRICE PERCENT APPLICABLE");
            ////services.Columns.Add(dc);

            ////dc = new DataColumn("price_one_payment", System.Type.GetType("System.Double"));
            ////dc.Caption = Language.GetColumnHeaderText("price_one_payment", "PRICE ONE PAYMENT");
            ////services.Columns.Add(dc);

            ////dc = new DataColumn("price_one_payment_applicable", System.Type.GetType("System.Boolean"));
            ////dc.Caption = Language.GetColumnHeaderText("price_one_payment_applicable", "PRICE ONE PAYMENT APPLICABLE");
            ////services.Columns.Add(dc);
            ////return services;

            DataTable services = new DataTable("services");
            DataColumn dc = new DataColumn("modify", System.Type.GetType("System.Boolean"));
            dc.DefaultValue = false;
            dc.Caption = Language.GetColumnHeaderText("modify", "MODIFY");
            services.Columns.Add(dc);

            dc = new DataColumn("id", System.Type.GetType("System.Int64"));
            dc.Caption = Language.GetColumnHeaderText("id", "ID");
            services.Columns.Add(dc);

            dc = new DataColumn("service_id", System.Type.GetType("System.Int64"));
            dc.Caption = Language.GetColumnHeaderText("service_id", "SERVICE ID");
            services.Columns.Add(dc);

            dc = new DataColumn("value", System.Type.GetType("System.Double"));
            dc.Caption = Language.GetColumnHeaderText("value", "VALUE");
            services.Columns.Add(dc);

            dc = new DataColumn("period", System.Type.GetType("System.UInt32"));
            dc.Caption = Language.GetColumnHeaderText("period", "PERIOD");
            services.Columns.Add(dc);

            dc = new DataColumn("percent", System.Type.GetType("System.Boolean"));
            dc.Caption = Language.GetColumnHeaderText("percent", "PERCENT");
            dc.DefaultValue = false;
            services.Columns.Add(dc);

            return services;
        }
        */
        #endregion

        private void CreateServicesTable()
        {
            DataColumn dc = new DataColumn("modify", System.Type.GetType("System.Boolean"));
            dc.DefaultValue = false;
            dc.Caption = Language.GetColumnHeaderText("modify", "MODIFY");
            ContractServices.Columns.Add(dc);

            dc = new DataColumn("id", System.Type.GetType("System.Int64"));
            dc.Caption = Language.GetColumnHeaderText("id", "ID");
            ContractServices.Columns.Add(dc);

            dc = new DataColumn("service_id", System.Type.GetType("System.Int64"));
            dc.Caption = Language.GetColumnHeaderText("service_id", "SERVICE ID");
            ContractServices.Columns.Add(dc);

            dc = new DataColumn("value", System.Type.GetType("System.Double"));
            dc.Caption = Language.GetColumnHeaderText("value", "VALUE");
            ContractServices.Columns.Add(dc);

            dc = new DataColumn("period", System.Type.GetType("System.UInt32"));
            //dc.Caption = Language.GetColumnHeaderText("period", "PERIOD");
            dc.Caption = Language.GetColumnHeaderText("period", "PAYMENT PLAN");
            ContractServices.Columns.Add(dc);

            dc = new DataColumn("percent", System.Type.GetType("System.Boolean"));
            dc.Caption = Language.GetColumnHeaderText("percent", "PERCENT");
            dc.DefaultValue = false;
            ContractServices.Columns.Add(dc);

            dc = new DataColumn("property_id", System.Type.GetType("System.UInt32"));
            dc.Caption = Language.GetColumnHeaderText("property_id", "PROPERTY ID");
            dc.DefaultValue = false;
            ContractServices.Columns.Add(dc);

            dc = new DataColumn("not_invoiceable", System.Type.GetType("System.Boolean"));
            dc.Caption = Language.GetColumnHeaderText("not_invoiceable", "NOT INVOICEABLE");
            dc.DefaultValue = false;
            ContractServices.Columns.Add(dc);

            ContractServices.PrimaryKey = new DataColumn[] { 
                ContractServices.Columns["property_id"], 
                ContractServices.Columns["service_id"]
                //ContractServices.Columns["value"], 
                //ContractServices.Columns["period"], 
                //ContractServices.Columns["percent"] 
            };

            ContractServices.AcceptChanges();
        }

        private void CreateServicesAdditonalCosts()
        {
            ServicesAdditionalCosts = ((DataTable)dataGridViewContractServices.DataSource).Clone();
            ServicesAdditionalCosts.PrimaryKey = null;

            DataColumn dc = new DataColumn("contractpropertyservice_id", System.Type.GetType("System.Int64"));
            dc.Caption = Language.GetColumnHeaderText("contractpropertyservice_id", "CONTRACTPROPERTYSERVICE_ID");
            ServicesAdditionalCosts.Columns.Add(dc);

            dc = new DataColumn("description", System.Type.GetType("System.String"));
            dc.Caption = Language.GetColumnHeaderText("description", "DESCRIPTION");
            ServicesAdditionalCosts.Columns.Add(dc);

            // we don't need the value column because it comes from the services table
            ServicesAdditionalCosts.AcceptChanges();
        }

        private void Contracts_Load(object sender, EventArgs e)
        {
            //this.Hide();
            CreateServicesTable();
            dataGridViewContractServices.DataSource = ContractServices;
            dataGridViewContractServices.Columns["id"].Visible = false;
            dataGridViewContractServices.Columns["property_id"].Visible = false;

            CreateServicesAdditonalCosts();

            ServicesServicetypes = (new DataAccess(CommandType.StoredProcedure, "SERVICES_SERVICETYPESsp_select")).ExecuteSelectQuery().Tables[0];

            dataGridViewContractServices.Columns["modify"].Visible = !(ParentContractId<=0);

            if (NewDR == null || (NewDR != null && NewDR.RowState == DataRowState.Detached && ParentContractId <= 0))  //add only
            {
                object max_number = (new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_max_number")).ExecuteScalarQuery();
                try
                {
                    userTextBoxFormatedContractNumber.Text = String.Format("FDP {0}", (Convert.ToInt32(max_number)+1).ToString().PadLeft(5, '0'));
                    userTextBoxContractNumber.Text = (Convert.ToInt32(max_number)+1).ToString();
                }
                catch
                {
                    userTextBoxFormatedContractNumber.Text = "FDP 00001";
                    userTextBoxContractNumber.Text = "1";
                }
                splitContainer1.Panel1Collapsed = true;
                splitContainer2.Panel1Collapsed = true;
                splitContainer3.Panel1Collapsed = true;
                EnableDisableControls(this, true);
            }
            if (NewDR != null && NewDR.RowState == DataRowState.Unchanged && ParentContractId <= 0) // edit only
            {
                DataAccess dac = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_has_changes", new object[]{
                    new MySqlParameter("_ID", NewDR["id"]),
                    new MySqlParameter("_NUMBER", NewDR["number"]),
                    new MySqlParameter("_ADENDUM_NUMBER", NewDR["addendum_number"])
                });
                object has_changes = dac.ExecuteScalarQuery();
                if (has_changes != null && Convert.ToInt32(has_changes) > 0)
                {
                    MessageBox.Show(Language.GetMessageBoxText("contractHasChanges", "This contract has addendums, rent contracts or already invoiced requirements! Further modifications will be possible only through an addendum!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    splitContainer1.Panel1Collapsed = true;
                    splitContainer2.Panel1Collapsed = true;
                    splitContainer3.Panel1Collapsed = true;
                    //EnableDisableControls(this, false);
                    comboBoxCurrency.Enabled = true;
                    comboBoxExpired.Enabled = true;
                    comboBoxStatus.Enabled = true;
                    checkBoxAutomaticallyRenewed.Enabled = true;
                    userTextBoxContractNumber.Enabled = true;
                    buttonSaveContract.Enabled = false;
                }
                else
                {
                    splitContainer1.Panel1Collapsed = true;
                    splitContainer2.Panel1Collapsed = true;
                    splitContainer3.Panel1Collapsed = true;
                    EnableDisableControls(this, true);
                }
            }
            if (NewDR != null && NewDR.RowState == DataRowState.Detached && ParentContractId > 0)  //add adendums only
            {
                EnableDisableControls(this, false);
                base.listBoxErrors.Enabled = true;
                splitContainer1.Panel1Collapsed = false;
                EnableDisableControls(splitContainer1.Panel1, true);
                splitContainer2.Panel1Collapsed = false;
                EnableDisableControls(splitContainer2.Panel1, true);
                splitContainer3.Panel1Collapsed = false;
                EnableDisableControls(splitContainer3.Panel1, true);
                userTextBoxAddendumNumber.Enabled = true;
                dateTimePickerAddendumDate.Enabled = true;
                //groupBoxServiceAdditionalCosts.Enabled = true;
                //dataGridViewServiceAdditionalCosts.Enabled = true;
                foreach (DataGridViewColumn dgvc in dataGridViewContractServices.Columns)
                {
                    dgvc.ReadOnly = (dgvc.Name.ToLower() != "modifiy");
                }
            }

            if (NewDR != null && NewDR.RowState == DataRowState.Unchanged && ParentContractId > 0)  //edit adendums only
            {
                DataAccess dac = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_has_changes", new object[]{
                    new MySqlParameter("_ID", NewDR["id"]),
                    new MySqlParameter("_NUMBER", NewDR["number"]),
                    new MySqlParameter("_ADENDUM_NUMBER", NewDR["addendum_number"])
                });
                object has_changes = dac.ExecuteScalarQuery();
                if (has_changes != null && Convert.ToInt32(has_changes) > 0)
                {
                    MessageBox.Show(Language.GetMessageBoxText("contractHasChanges", "This contract has addendums or already invoiced requirements! Further modifications to sensitive data will be possible only through an addendum!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    EnableDisableControls(this, false);
                    base.listBoxErrors.Enabled = true;
                    splitContainer1.Panel1Collapsed = false;
                    EnableDisableControls(splitContainer1.Panel1, true);
                    splitContainer2.Panel1Collapsed = false;
                    EnableDisableControls(splitContainer2.Panel1, true);
                    splitContainer3.Panel1Collapsed = false;
                    EnableDisableControls(splitContainer3.Panel1, true);
                    userTextBoxAddendumNumber.Enabled = true;
                    dateTimePickerAddendumDate.Enabled = true;
                    foreach (DataGridViewColumn dgvc in dataGridViewContractServices.Columns)
                    {
                        dgvc.ReadOnly = (dgvc.Name.ToLower() != "modifiy");
                    }
                }
                else
                {
                    EnableDisableControls(this, true);
                    base.listBoxErrors.Enabled = true;
                    splitContainer1.Panel1Collapsed = false;
                    EnableDisableControls(splitContainer1.Panel2, false);
                    splitContainer2.Panel1Collapsed = false;
                    EnableDisableControls(splitContainer2.Panel2, false);
                    splitContainer3.Panel1Collapsed = false;
                    EnableDisableControls(splitContainer3.Panel2, false);
                    userTextBoxAddendumNumber.Enabled = true;
                    dateTimePickerAddendumDate.Enabled = true;
                    foreach (DataGridViewColumn dgvc in dataGridViewContractServices.Columns)
                    {
                        dgvc.ReadOnly = (dgvc.Name.ToLower() != "modifiy");
                    }
                }
            }

            Language.PopulateGridColumnHeaders(dataGridViewContractServices);

            DataAccess da = new DataAccess(CommandType.StoredProcedure, "SERVICESsp_list");
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.DataSource = da.ExecuteSelectQuery().Tables[0];
            col.DisplayMember = "name";
            col.ValueMember = "id";
            col.DataPropertyName = "service_id";
            col.Name = "service";
            col.FlatStyle = FlatStyle.Popup;
            col.HeaderText = Language.GetColumnHeaderText("service", "SERVICE");
            dataGridViewContractServices.Columns.Insert(1, col);
            //dataGridViewContractServices.Columns["service_id"].Visible = false;
            dataGridViewContractServices.Columns.Remove("service_id");
            /*
            DataTable dtDuration = new DataTable();
            dtDuration.Columns.Add(new DataColumn("period", Type.GetType("System.String")));
            dtDuration.Rows.Add(new object[] { "per month" });
            dtDuration.Rows.Add(new object[] { "per year" });
            dtDuration.Rows.Add(new object[] { "one time" });
            dtDuration.Rows.Add(new object[] { "none" });
            dtDuration.AcceptChanges();
            dataGridViewContractServices.Columns.Remove("period");
            DataGridViewComboBoxColumn col2 = new DataGridViewComboBoxColumn();
            col2.DisplayMember = "period";
            col2.ValueMember = "period";
            col2.DataPropertyName = "period";
            col2.Name = "period";
            col2.HeaderText = Language.GetColumnHeaderText("period", "PERIOD");
            col2.DataSource = dtDuration;
            dataGridViewContractServices.Columns.Insert(2, col2);
            */
            DataGridViewComboBoxColumn dgvcbc = new DataGridViewComboBoxColumn();
            DataTable dtSource = (new DataAccess(CommandType.StoredProcedure, CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name_for_grid", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "service_type") })).ExecuteSelectQuery().Tables[0];
            dgvcbc.DisplayMember = "name";
            dgvcbc.ValueMember = "id";
            //dgvcbc.HeaderText = Language.GetColumnHeaderText("period", "PERIOD");
            dgvcbc.HeaderText = Language.GetColumnHeaderText("period", "PAYMENT PLAN");
            dgvcbc.DataPropertyName = "period";
            dgvcbc.DataSource = dtSource;
            dgvcbc.Name = "period";
            int col_index = dataGridViewContractServices.Columns["period"].Index;
            dataGridViewContractServices.Columns.Remove("period");
            dgvcbc.FlatStyle = FlatStyle.Popup;
            dataGridViewContractServices.Columns.Insert(col_index, dgvcbc);
            
            
            DataGridViewButtonColumn dgvbc = new DataGridViewButtonColumn();
            dgvbc.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvbc.FlatStyle = FlatStyle.System;
            dgvbc.CellTemplate.Style.BackColor = Color.Red;
            dgvbc.CellTemplate.Style.ForeColor = Color.White;
            //dgvbc.DefaultCellStyle.BackColor = Color.Red;
            //dgvbc.HeaderText = Language.GetColumnHeaderText("delete", "-");
            dgvbc.HeaderText = "-";
            dgvbc.Text = "-";
            dgvbc.Name = "DELETE_SERVICE";
            dgvbc.ToolTipText = Language.GetLabelText("contractDeleteService", "Delete this service (row)");
            dgvbc.UseColumnTextForButtonValue = true;
            dataGridViewContractServices.Columns.Insert(0, dgvbc);
            

            DataGridViewButtonColumn dgvbc2 = new DataGridViewButtonColumn();
            dgvbc2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvbc2.FlatStyle = FlatStyle.System;
            dgvbc2.CellTemplate.Style.BackColor = Color.LightGreen;
            dgvbc2.CellTemplate.Style.ForeColor = Color.Black;
            //dgvbc2.DefaultCellStyle.BackColor = Color.LightGreen;
            dgvbc2.HeaderText = Language.GetColumnHeaderText("addToAll", "+");
            dgvbc2.Name = "ADD_TO_ALL";
            dgvbc2.Text = "+";
            dgvbc2.ToolTipText = Language.GetLabelText("contractAddToAll", "Add this service and values to all selected properties");
            dgvbc2.UseColumnTextForButtonValue = true;
            dataGridViewContractServices.Columns.Insert(1, dgvbc2);


            FillCombos();
            if (NewDR != null)
            {
                FillInfo();
            }
            else
            {
                dateTimePickerStartDate.Value = DateTime.Now;
                dateTimePickerExpirationDate.Value = dateTimePickerFinishDate.Value = DateTime.Now.AddYears(1).AddDays(-1);
                try
                { FillAdditionalCosts(); }
                catch { }
            }
            this.tabPageAddendums.Visible = !(ParentContractId <= 0);
            try
            {
                foreach (DataColumn dc in NewDR.Table.Columns)
                {
                    if (dc.DataType.ToString() == "System.Boolean" && NewDR[dc.ColumnName] == DBNull.Value)
                        NewDR[dc.ColumnName] = false;
                }
                InitialDR = CommonFunctions.CopyDataRow(NewDR);
                InitialExternalDR = CommonFunctions.CopyDataRow(NewDR);
                if (NewDR.RowState == DataRowState.Added || NewDR.RowState == DataRowState.Detached)
                {
                    InitialDR["finish_date"] = DateTime.Now.AddYears(1).AddDays(-1);
                    InitialDR["expiration_date"] = DateTime.Now.AddYears(1).AddDays(-1);
                }
            }
            catch { }
            //this.Show();
            labelSelectedProperty.Font = new Font(labelSelectedProperty.Font.FontFamily, 9);
            this.Opacity = 100;
            //oThread.Abort();
        }

        /*
        private void dataGridViewContractServices_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv[dgv.Columns["period"].Index, e.RowIndex].EditedFormattedValue.ToString().ToLower() == (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "per month"), new MySqlParameter("_LIST_TYPE", "service_type") })).ExecuteScalarQuery().ToString().ToLower() ||
                dgv[dgv.Columns["period"].Index, e.RowIndex].EditedFormattedValue.ToString().ToLower() == (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "one time payment"), new MySqlParameter("_LIST_TYPE", "service_type") })).ExecuteScalarQuery().ToString().ToLower() ||
                dgv[dgv.Columns["period"].Index, e.RowIndex].EditedFormattedValue.ToString().ToLower() == (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "50%-50%"), new MySqlParameter("_LIST_TYPE", "service_type") })).ExecuteScalarQuery().ToString().ToLower() ||
                dgv[dgv.Columns["period"].Index, e.RowIndex].EditedFormattedValue.ToString().ToLower() == (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "none"), new MySqlParameter("_LIST_TYPE", "service_type") })).ExecuteScalarQuery().ToString().ToLower()
                )
            {
                dgv[dgv.Columns["percent"].Index, e.RowIndex].ReadOnly = false;
            }
            else
            {
                dgv[dgv.Columns["percent"].Index, e.RowIndex].Value = false;
                dgv[dgv.Columns["percent"].Index, e.RowIndex].ReadOnly = true;
            }
        }
        */

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
            /*
            da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_select");
            DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
            if (dtProperties != null)
            {
                checkedListBoxProperties.DataSource = dtProperties;
            }
            */
            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "CONTRACT_STATUS") });
            DataTable dtStatuses = da.ExecuteSelectQuery().Tables[0];
            if (dtStatuses != null)
            {
                comboBoxStatus.DisplayMember = "name";
                comboBoxStatus.ValueMember = "id";
                comboBoxStatus.DataSource = dtStatuses;
            }

            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "CONTRACT_TYPE") });
            DataTable dtTypes = da.ExecuteSelectQuery().Tables[0];
            if (dtTypes != null)
            {
                comboBoxExpired.DisplayMember = "name";
                comboBoxExpired.ValueMember = "id";
                comboBoxExpired.DataSource = dtTypes;
            }
            /*
            da = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_list");
            DataTable dtContracts = da.ExecuteSelectQuery().Tables[0];
            if (dtContracts != null)
            {
                comboBoxParentContract.DisplayMember = "number";
                comboBoxParentContract.ValueMember = "id";
                comboBoxParentContract.DataSource = dtContracts;
            }
            */
            da = new DataAccess(CommandType.StoredProcedure, "CONTRACTREASONSsp_list");
            DataTable dtReasons = da.ExecuteSelectQuery().Tables[0];
            if (dtReasons != null)
            {
                listBoxReasons.DisplayMember = "details";
                listBoxReasons.ValueMember = "id";
                listBoxReasons.DataSource = dtReasons;
            }
            listBoxReasons.ClearSelected();                

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
                //if (NewDR.RowState != DataRowState.Added && NewDR != null)
                if (NewDR != null)
                {
                    if (NewDR.RowState != DataRowState.Detached || ParentContractId > 0) // not add only
                    {
                        userTextBoxContractNumber.Text = NewDR["number"].ToString();
                        userTextBoxFormatedContractNumber.Text = String.Format("FDP {0}", NewDR["number"].ToString().Trim().PadLeft(5, '0'));
                    }
                    try { dateTimePickerStartDate.Value = Convert.ToDateTime(NewDR["start_date"]); }
                    catch { dateTimePickerStartDate.Value = DateTime.Now; }
                    try { dateTimePickerFinishDate.Value = Convert.ToDateTime(NewDR["finish_date"]); }
                    catch { dateTimePickerFinishDate.Value = DateTime.Now.AddYears(1).AddDays(-1); }
                    try { comboBoxExpired.SelectedValue = NewDR["expired_id"]; }
                    catch { }
                    try { dateTimePickerExpirationDate.Value = Convert.ToDateTime(NewDR["expiration_date"]); }
                    catch { dateTimePickerExpirationDate.Value = DateTime.Now.AddYears(1).AddDays(-1); }
                    try { comboBoxStatus.SelectedValue = NewDR["status_id"]; }
                    catch { }
                    checkBoxAutomaticallyRenewed.Checked = NewDR["automatically_renewed"] != DBNull.Value ? Convert.ToBoolean(NewDR["automatically_renewed"]) : false;
                    checkBoxUseAsEndDate.Checked = NewDR["use_expiration_date"] != DBNull.Value ? Convert.ToBoolean(NewDR["use_expiration_date"]) : false;
                    userTextBoxAddendumNumber.Text = NewDR["addendum_number"].ToString();
                    try { dateTimePickerAddendumDate.Value = Convert.ToDateTime(NewDR["addendum_date"]); }
                    catch { dateTimePickerAddendumDate.Value = DateTime.Now; }
                    //comboBoxParentContract.SelectedValue = NewDR["parent_contract_id"];
                    try { FillProperties(Convert.ToInt32(NewDR["id"]));}
                    catch { }
                    try { FillServices(Convert.ToInt32(NewDR["id"])); }
                    catch { }
                    try
                    { FillAdditionalCosts(); }
                    catch { }
                    try { FillReasons(Convert.ToInt32(NewDR["id"])); }
                    catch { }
                    comboBoxCurrency.SelectedValue = NewDR["currency"];
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void FillReasons(int contract_id)
        {
            try
            {
                DataTable contract_reasons = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_REASONSsp_get_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id) })).ExecuteSelectQuery().Tables[0];
                if (contract_reasons.Rows.Count > 0)
                {
                    foreach (DataRow dr in contract_reasons.Rows)
                    {
                        try
                        {
                            foreach (object x in listBoxReasons.Items)
                            {
                                if (dr["details"].ToString().ToLower() == ((DataRowView)x)["details"].ToString().ToLower())
                                {
                                    listBoxReasons.SetSelected(listBoxReasons.Items.IndexOf(x), true);
                                    //checkBoxModifyProperties.Enabled = 
                                    checkBoxModifyProperties.Checked = (dr["details"].ToString().ToLower().IndexOf("property(ies)") > -1);
                                    //checkBoxModifyDates.Enabled = 
                                    checkBoxModifyDates.Checked = (dr["details"].ToString().ToLower().IndexOf("date(s)") > -1);
                                    //checkBoxModifyServices.Enabled = 
                                    checkBoxModifyServices.Checked = (dr["details"].ToString().ToLower().IndexOf("price(s)") > -1);
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }

        private void FillServices(int contract_id)
        {
            //dataGridViewContractServices.CancelEdit();
            DataTable contract_services = new DataTable();
            if(ParentContractId > 0 && (NewDR.RowState == DataRowState.Detached || NewDR.RowState == DataRowState.Added))
                contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id_last", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id) })).ExecuteSelectQuery().Tables[0];
            else
                contract_services = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id) })).ExecuteSelectQuery().Tables[0];
            if (contract_services.Rows.Count > 0)
            {
                foreach (DataRow dr in contract_services.Rows)
                {
                    /*
                    DataRow new_service = ((DataTable)dataGridViewContractServices.DataSource).NewRow();
                    new_service["service_id"] = dr["service_id"];
                    new_service["price_value"] = dr["price_value"];
                    new_service["price_percent"] = dr["price_percent"];
                    new_service["price_one_payment"] = dr["price_one_payment"];
                    new_service["price_value_applicable"] = dr["price_value_applicable"];
                    new_service["price_percent_applicable"] = dr["price_percent_applicable"];
                    new_service["price_one_payment_applicable"] = dr["price_one_payment_applicable"];
                    ((DataTable)dataGridViewContractServices.DataSource).Rows.Add(new_service);
                    */

                    DataRow new_service = ((DataTable)dataGridViewContractServices.DataSource).NewRow();
                    new_service["modify"] = dr["modify"];
                    new_service["id"] = dr["id"];
                    new_service["service_id"] = dr["service_id"];
                    new_service["value"] = dr["value"];
                    new_service["period"] = dr["period"];
                    new_service["percent"] = dr["percent"];
                    new_service["property_id"] = dr["property_id"];
                    new_service["not_invoiceable"] = dr["not_invoiceable"];
                    ((DataTable)dataGridViewContractServices.DataSource).Rows.Add(new_service);
                    /*
                    DataTable dtAdditionalCosts = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_get_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACTSERVICE_ID", dr["id"]) })).ExecuteSelectQuery().Tables[0];
                    if (dtAdditionalCosts != null && dtAdditionalCosts.Rows.Count > 0)
                    {
                        foreach (DataRow additionalCost in dtAdditionalCosts.Rows)
                        {
                            DataRow newAdditoanlCost = ServicesAdditionalCosts.NewRow();
                            newAdditoanlCost["modify"] = dr["modify"];
                            newAdditoanlCost["id"] = dr["id"];
                            newAdditoanlCost["contractpropertyservice_id"] = additionalCost["contractpropertyservice_id"];
                            newAdditoanlCost["service_id"] = dr["service_id"];
                            newAdditoanlCost["period"] = dr["period"];
                            newAdditoanlCost["percent"] = dr["percent"];
                            newAdditoanlCost["contractpropertyservice_id"] = additionalCost["contractpropertyservice_id"];
                            newAdditoanlCost["value"] = additionalCost["value"];
                            newAdditoanlCost["description"] = additionalCost["description"];
                            ServicesAdditionalCosts.Rows.Add(newAdditoanlCost);
                        }
                        ServicesAdditionalCosts.AcceptChanges();
                    }
                    */
                }
                ((DataTable)dataGridViewContractServices.DataSource).AcceptChanges();
                
                dataGridViewContractServices.EndEdit();

                //dataGridViewContractServices.CancelEdit();
                //dataGridViewContractServices.Rows[0].Selected = true;
                //dataGridViewContractServices.CurrentCell = dataGridViewContractServices.Rows[0].Cells["SERVICE"];
                            
                foreach (DataGridViewRow dgrv in dataGridViewContractServices.Rows)
                {
                    if (dgrv.Cells["PROPERTY_ID"].Value != null && dgrv.Cells["PROPERTY_ID"].Value != DBNull.Value && dgrv.Cells["PROPERTY_ID"].Value.ToString() == "0")                    {
                        dataGridViewContractServices.Rows.Remove(dgrv);
                    }
                }
                
                /*
                dataGridViewServiceAdditionalCosts.DataSource = ServicesAdditionalCosts;
                foreach (DataGridViewColumn dgvc in dataGridViewServiceAdditionalCosts.Columns)
                {
                    dgvc.Visible = (dgvc.Name.ToLower().IndexOf("description") > -1 || dgvc.Name.ToLower().IndexOf("value") > -1);
                }
                */
            }

        }

        private void FillProperties(int contract_id)
        {
            if (NewDR["owner_id"] != DBNull.Value && NewDR["owner_id"] != null)
                comboBoxOwner.SelectedValue = NewDR["owner_id"];
            /*
            //DataTable properties = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_select").ExecuteSelectQuery()).Tables[0];
            //DataTable properties = (DataTable)cCheckedListBoxProperties.DataSource;
            DataTable selected_properties = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIESsp_select_by_contract_id", new object[]{new MySqlParameter("_CONTRACT_ID", contract_id)}).ExecuteSelectQuery()).Tables[0];
            foreach(object x in cCheckedListBoxProperties.Items){
                foreach(DataRow dr in selected_properties.Rows){
                    if( ((DataRowView)x).Row["id"].ToString() == dr["property_id"].ToString()){
                        cCheckedListBoxProperties.SetItemChecked(cCheckedListBoxProperties.Items.IndexOf(x), true);
                    break;
                    }
                }
            }
            */
            DataTable selected_properties = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id) }).ExecuteSelectQuery()).Tables[0];
            foreach (DataGridViewRow dgvr in dataGridViewProperties.Rows)
            {
                dgvr.Cells["Assigned"].Value = false;
                foreach (DataRow dr in selected_properties.Rows)
                {
                    if (dgvr.Cells["id"].Value.ToString() == dr["property_id"].ToString())
                    {
                        dgvr.Cells["Assigned"].Value = true;
                        break;
                    }
                }
            }

            if (dataGridViewProperties.Rows.Count > 0)
                dataGridViewProperties.Rows[0].Selected = true;
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in dataGridViewProperties.Rows)
            {
                if (dgvr.Cells["ID"].Value != DBNull.Value)
                {
                    dgvr.Cells["Assigned"].Value = ((CheckBox)sender).Checked;
                    dataGridViewProperties.EndEdit();
                }
            }
        }

        public void GenerateMySqlParameters()
        {
            try
            {
                if (NewDR != null)
                {
                    NewDR["number"] = userTextBoxContractNumber.Text;
                    NewDR["start_date"] = dateTimePickerStartDate.Value;
                    NewDR["finish_date"] = dateTimePickerFinishDate.Value;
                    NewDR["expiration_date"] = dateTimePickerExpirationDate.Value;
                    NewDR["expired_id"] = CommonFunctions.SetNullable(comboBoxExpired);
                    NewDR["automatically_renewed"] = checkBoxAutomaticallyRenewed.Checked;
                    NewDR["owner_id"] = CommonFunctions.SetNullable(comboBoxOwner);
                    NewDR["status_id"] = CommonFunctions.SetNullable(comboBoxStatus);
                    //NewDR["parent_contract_id"] = CommonFunctions.SetNullable(comboBoxParentContract);
                    NewDR["parent_contract_id"] = ParentContractId <= 0 ? NewDR["parent_contract_id"] : (object)ParentContractId;
                    NewDR["addendum_number"] = userTextBoxAddendumNumber.Text;
                    NewDR["addendum_date"] = ParentContractId > 0?(object)dateTimePickerAddendumDate.Value:DBNull.Value;
                    NewDR["currency"] = comboBoxCurrency.SelectedValue;
                    NewDR["use_expiration_date"] = checkBoxUseAsEndDate.Checked;
                }
                else
                {
                    MySqlParameters.Clear();
                    if (NewDR != null && NewDR.RowState != DataRowState.Added)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", NewDR["id"]);
                        MySqlParameters.Add(_ID);
                    }
                    MySqlParameter _NUMBER = new MySqlParameter("_NUMBER", userTextBoxContractNumber.Text); MySqlParameters.Add(_NUMBER);
                    MySqlParameter _START_DATE = new MySqlParameter("_START_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerStartDate.Value)); MySqlParameters.Add(_START_DATE);
                    MySqlParameter _FINISH_DATE = new MySqlParameter("_FINISH_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerFinishDate.Value)); MySqlParameters.Add(_FINISH_DATE);
                    MySqlParameter _EXPIRATION_DATE = new MySqlParameter("_EXPIRATION_DATE", CommonFunctions.ToMySqlFormatDate(dateTimePickerExpirationDate.Value)); MySqlParameters.Add(_EXPIRATION_DATE);
                    MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", CommonFunctions.SetNullable(comboBoxStatus)); MySqlParameters.Add(_STATUS_ID);
                    MySqlParameter _AUTOMATICALLY_RENEWED = new MySqlParameter("_AUTOMATICALLY_RENEWED", checkBoxAutomaticallyRenewed.Checked); MySqlParameters.Add(_AUTOMATICALLY_RENEWED);
                    MySqlParameter _OWNER_ID = new MySqlParameter("_OWNER_ID", CommonFunctions.SetNullable(comboBoxOwner)); MySqlParameters.Add(_OWNER_ID);
                    MySqlParameter _EXPIRED_ID = new MySqlParameter("_EXPIRED_ID", CommonFunctions.SetNullable(comboBoxExpired)); MySqlParameters.Add(_EXPIRED_ID);
                    //MySqlParameter _PARENT_CONTRACT_ID = new MySqlParameter("_PARENT_CONTRACT_ID", CommonFunctions.SetNullable(comboBoxParentContract)); MySqlParameters.Add(_PARENT_CONTRACT_ID);
                    MySqlParameter _PARENT_CONTRACT_ID = new MySqlParameter("_PARENT_CONTRACT_ID", DBNull.Value); MySqlParameters.Add(_PARENT_CONTRACT_ID);
                    MySqlParameter _ADDENDUM_NUMBER = new MySqlParameter("_ADDENDUM_NUMBER", userTextBoxAddendumNumber.Text); MySqlParameters.Add(_ADDENDUM_NUMBER);
                    MySqlParameter _ADDENDUM_DATE = new MySqlParameter("_ADDENDUM_DATE", ParentContractId > 0?(object)CommonFunctions.ToMySqlFormatDate(dateTimePickerAddendumDate.Value):DBNull.Value); MySqlParameters.Add(_ADDENDUM_DATE);
                    MySqlParameter _CURRENCY = new MySqlParameter("_CURRENCY", comboBoxCurrency.SelectedValue); MySqlParameters.Add(_CURRENCY);
                    MySqlParameter _USE_EXPIRATION_DATE = new MySqlParameter("_USE_EXPIRATION_DATE", checkBoxUseAsEndDate.Checked); MySqlParameters.Add(_USE_EXPIRATION_DATE);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }



        private void buttonSaveContract_Click(object sender, EventArgs e)
        {
            dataGridViewContractServices.EndEdit();
            dataGridViewServiceAdditionalCosts.EndEdit();
            ContractServices.AcceptChanges();
            ServicesAdditionalCosts.AcceptChanges();
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
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_insert", MySqlParameters.ToArray());
                    DataRow contract = da.ExecuteSelectQuery().Tables[0].Rows[0];
                    SavedOrCancelled = true;
                    int contract_id = Convert.ToInt32(contract["id"]);
                    //string contract_status = Convert.ToString(contract["status"]).ToLower();
                    string contract_status = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", contract["status_id"]) })).ExecuteScalarQuery().ToString().ToLower();
                    DateTime contract_start_date = Convert.ToDateTime(contract["start_date"]);
                    DateTime contract_finish_date = Convert.ToDateTime(contract["finish_date"]);
                    DateTime contract_expiration_date = Convert.ToDateTime(contract["expiration_date"]);
                    bool contract_automatically_renewed = contract["automatically_renewed"] == null || contract["automatically_renewed"] == DBNull.Value ? false : Convert.ToBoolean(contract["automatically_renewed"]);
                    bool use_expiration_date = Convert.ToBoolean(contract["use_expiration_date"] == DBNull.Value || contract["use_expiration_date"] == null ? false : contract["use_expiration_date"]);
                    ArrayList months_years = contract_automatically_renewed ? CommonFunctions.DateDifferenceInYears(use_expiration_date ? contract_expiration_date : contract_finish_date, contract_start_date) : CommonFunctions.DateDifferenceInYears(contract_finish_date, contract_start_date);

                    foreach (DataGridViewRow dgvr in dataGridViewProperties.Rows)
                    {
                        try
                        {
                            if (Convert.ToBoolean(dgvr.Cells["Assigned"].Value == null ? false : dgvr.Cells["Assigned"].Value))
                            {
                                da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIESsp_insert", new object[] { new MySqlParameter("_CONTRACT_ID", contract_id), new MySqlParameter("_PROPERTY_ID", dgvr.Cells["id"].Value) });
                                object[] returned2 = da.ExecuteInsertQuery();
                                int _contract_property_id = Convert.ToInt32(returned2[2]);
                                //foreach (DataGridViewRow dgvr_service in dataGridViewContractServices.Rows)
                                foreach (DataRow dr_service in ContractServices.Select(
                                    (dgvr.Cells["id"].Value == DBNull.Value || dgvr.Cells["id"].Value == null) ?
                                        String.Format("IsNull(PROPERTY_ID, -1) = -1") :
                                        String.Format("PROPERTY_ID = {0}", dgvr.Cells["id"].Value.ToString())
                                        ))
                                {
                                    try
                                    {
                                        //if (dgvr_service.Cells["property_id"].Value == dgvr.Cells["id"].Value)
                                        {
                                            /*
                                            da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIES_SERVICESsp_insert", new object[] { 
                                                    new MySqlParameter("_CONTRACT_PROPERTY_ID", _contract_property_id),
                                                    new MySqlParameter("_SERVICE_ID", dgvr_service.Cells["service"].Value),
                                                    new MySqlParameter("_PRICE_VALUE", dgvr_service.Cells["price_value"].Value),
                                                    new MySqlParameter("_PRICE_PERCENT", dgvr_service.Cells["price_percent"].Value),
                                                    new MySqlParameter("_PRICE_ONE_PAYMENT", dgvr_service.Cells["price_one_payment"].Value),
                                                    new MySqlParameter("_PRICE_VALUE_APPLICABLE", dgvr_service.Cells["price_value_applicable"].Value),
                                                    new MySqlParameter("_PRICE_PERCENT_APPLICABLE", dgvr_service.Cells["price_percent_applicable"].Value),
                                                    new MySqlParameter("_PRICE_ONE_PAYMENT_APPLICABLE", dgvr_service.Cells["price_one_payment_applicable"].Value)
                                                });
                                            da.ExecuteInsertQuery();
                                            */
                                            /*
                                            da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIES_SERVICESsp_insert", new object[] { 
                                                new MySqlParameter("_CONTRACT_PROPERTY_ID", _contract_property_id),
                                                new MySqlParameter("_SERVICE_ID", dgvr_service.Cells["service"].Value),
                                                new MySqlParameter("_VALUE", dgvr_service.Cells["value"].Value),
                                                new MySqlParameter("_PERIOD", dgvr_service.Cells["PERIOD"].Value),
                                                new MySqlParameter("_PERCENT", dgvr_service.Cells["_percent"].Value)
                                            });
                                            */
                                            da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_PROPERTIES_SERVICESsp_insert", new object[] { 
                                                new MySqlParameter("_CONTRACT_PROPERTY_ID", _contract_property_id),
                                                new MySqlParameter("_SERVICE_ID", dr_service["service_id"]),
                                                new MySqlParameter("_VALUE", dr_service["value"]),
                                                new MySqlParameter("_PERIOD", dr_service["period"]),
                                                new MySqlParameter("_PERCENT", dr_service["percent"]),
                                                new MySqlParameter("_MODIFY", dr_service["modify"]),
                                                new MySqlParameter("_NOT_INVOICEABLE", dr_service["not_invoiceable"])
                                            });

                                            //da.ExecuteInsertQuery();
                                            DataRow cps = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                            try
                                            {
                                                foreach (DataRow dr in ServicesAdditionalCosts.Select(String.Format("service_id = {0} and period = {1} and percent = {2} and {3}", dr_service["service_id"].ToString(), dr_service["period"].ToString(), dr_service["percent"].ToString(), ((dgvr.Cells["id"].Value == DBNull.Value || dgvr.Cells["id"].Value == null) ? "IsNull(property_id, -1) = -1" : String.Format("property_id = {0}", dgvr.Cells["id"].Value.ToString())))))
                                                {
                                                    try
                                                    {
                                                        (new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_delete_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", dr["CONTRACT_SERVICE_ADDITIONAL_COST_ID"]) })).ExecuteUpdateQuery();
                                                        (new DataAccess(CommandType.StoredProcedure, "COMPANY_IEsp_delete_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACT_SERVICE_ADDITIONAL_COST_ID", dr["CONTRACT_SERVICE_ADDITIONAL_COST_ID"]) })).ExecuteUpdateQuery();
                                                        (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_delete_by_contractservice", new object[] { new MySqlParameter("_CONTRACTSERVICE_ID", dr["CONTRACTPROPERTYSERVICE_ID"]) })).ExecuteUpdateQuery();
                                                    }
                                                    catch { }
                                                    DataRow csad = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_insert", new object[] { 
                                                new MySqlParameter("_CONTRACT_PROPERTY_SERVICE_ID", cps["id"]),
                                                new MySqlParameter("_DESCRIPTION", dr["description"]),
                                                new MySqlParameter("_VALUE", dr["value"])
                                                })).ExecuteSelectQuery().Tables[0].Rows[0];
                                                    if (contract_status == "active")
                                                    {
                                                        foreach (int[] months_year in months_years)
                                                        {
                                                            DateTime start_date = new DateTime(months_year[1], contract_start_date.Month, contract_start_date.Day);
                                                            DateTime end_date = start_date.AddMonths(months_year[0]);

                                                            double value = 0;
                                                            //switch (dgvr_service.Cells["service"].EditedFormattedValue.ToString().ToLower())
                                                            string service = (new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", dr_service["service_id"].ToString().ToLower()) })).ExecuteScalarQuery().ToString().ToLower();
                                                            //switch (dr_service["service_id"].ToString().ToLower())
                                                            switch (service)
                                                            {
                                                                case "rent":
                                                                    if ((months_years.IndexOf(months_year) == 0 && !use_expiration_date) || use_expiration_date)
                                                                    {
                                                                        if (Convert.ToBoolean(dr_service["percent"] == DBNull.Value || dr_service["percent"] == null ? false : dr_service["percent"]))
                                                                        {
                                                                            value = Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(dr_service["value"]) / 100, 2);
                                                                            double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            double _amount_total = value + _vat;
                                                                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        }
                                                                        else // FROM 30.01.2013 - FOR THE CASES WHEN RENT IS A FIX VALUE, NOT A PERCENT FROM THE RENT ! - BUG #1 / 29.01.2013
                                                                        {
                                                                            value = Math.Round(Convert.ToDouble(dr_service["value"]), 2);
                                                                            double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            double _amount_total = value + _vat;
                                                                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                        }
                                                                    }
                                                                    break;
                                                                case "rent management":
                                                                    if (Convert.ToBoolean(dr_service["percent"] == DBNull.Value || dr_service["percent"] == null ? false : dr_service["percent"]))
                                                                    {
                                                                        //for (int i = 0; i < months_year[0] + 1; i++)
                                                                        for (int i = 0; i < months_year[0]; i++)
                                                                        {
                                                                            value = Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(dr_service["value"]) / 100, 2);
                                                                            double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            double _amount_total = value + _vat;
                                                                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            /* -- nu se aplica VAT la Rent income - 24.04.2016 -- 
                                                                            _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            _amount_total = value + _vat;
                                                                             */
                                                                            _vat = 0; _amount_total = Convert.ToDouble(dr["value"]);
                                                                            IncomeExpensesClass.InsertIE(false, true, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                                                        }
                                                                    }
                                                                    else // FROM 30.01.2013 - FOR THE CASES WHEN RENT IS A FIX VALUE, NOT A PERCENT FROM THE RENT ! - BUG #1 / 29.01.2013
                                                                    {
                                                                        //for (int i = 0; i < months_year[0] + 1; i++)
                                                                        for (int i = 0; i < months_year[0]; i++)
                                                                        {
                                                                            value = Math.Round(Convert.ToDouble(dr_service["value"]), 2);
                                                                            double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(value * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            double _amount_total = value + _vat;
                                                                            IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            IncomeExpensesClass.InsertIE(true, true, DBNull.Value, contract["currency"], value, start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, value, _vat, _amount_total);
                                                                            /* -- nu se aplica VAT la Rent income - 24.04.2016 -- 
                                                                            _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                            _amount_total = value + _vat;
                                                                             */
                                                                            _vat = 0; _amount_total = Convert.ToDouble(dr["value"]);
                                                                            IncomeExpensesClass.InsertIE(false, true, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2 + i), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                                                        }
                                                                    }
                                                                    break;
                                                                default:
                                                                    if (months_years.IndexOf(months_year) == 0)
                                                                    {
                                                                        double _vat = Convert.ToBoolean(dr_service["not_invoiceable"] == DBNull.Value ? false : dr_service["not_invoiceable"]) ? 0 : Math.Round(Convert.ToDouble(dr["value"]) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2);
                                                                        double _amount_total = value + _vat;
                                                                        IncomeExpensesClass.InsertIE(false, false, DBNull.Value, contract["currency"], dr["value"], start_date.AddMonths(2), contract["owner_id"], dgvr.Cells["id"].Value, dr_service["service_id"], dr["description"], DBNull.Value, DBNull.Value, DBNull.Value, csad["id"], (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(), DBNull.Value, DBNull.Value, dr["value"], _vat, _amount_total);
                                                                        // we don't generate income from another costs for fdp, they are only expenses for the owner
                                                                    }
                                                                    break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            catch { }
                                        }
                                    }
                                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                                }
                                try
                                {
                                    if (contract_status == "active")
                                    {
                                        DataRow property = ((DataRowView)dgvr.DataBoundItem).Row;
                                        InvoiceRequirementsClass.InsertFromFDPContract(contract, property);
                                    }
                                }
                                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                            }
                        }
                        catch { }
                    }
                    InitialDR = CommonFunctions.CopyDataRow(NewDR);
                    ContractServices.AcceptChanges();
                    ServicesAdditionalCosts.AcceptChanges();
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
            errorProvider1.SetError(userTextBoxContractNumber, "");
            errorProvider1.SetError(comboBoxOwner, "");
            errorProvider1.SetError(comboBoxExpired, "");
            errorProvider1.SetError(dateTimePickerFinishDate, "");
            errorProvider1.SetError(userTextBoxAddendumNumber, "");
            errorProvider1.SetError(dateTimePickerAddendumDate, "");

            if (userTextBoxContractNumber.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxContractNumber, Language.GetErrorText("errorEmptyContractNumber", "Contract number can not by empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxContractNumber.Name, Language.GetErrorText("errorEmptyContractNumber", "Contract number can not by empty!")));
                toReturn = false;
            }
            if (!(Validator.IsInteger(userTextBoxContractNumber.Text.Trim()) && (userTextBoxContractNumber.Text.Trim().Length <= 5) && (userTextBoxContractNumber.Text.Trim() != "0")))
            {
                errorProvider1.SetError(userTextBoxContractNumber, Language.GetErrorText("InvalidContractNumber", "Invalid contract number (eg. XXXXX) !"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxContractNumber.Name, Language.GetErrorText("InvalidContractNumber", "Invalid contract number (eg. XXXXX) !")));
                toReturn = false;
            }

            if(comboBoxOwner.SelectedValue == null || comboBoxOwner.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxOwner, Language.GetErrorText("errorEmptyContractOwner", "You must select an owner for the contract!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwner.Name, Language.GetErrorText("errorEmptyContractOwner", "You must select an owner for the contract!")));
                toReturn = false;
            }
            if (comboBoxExpired.SelectedValue == null || comboBoxExpired.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxStatus, Language.GetErrorText("errorEmptyPropertyStatus", "You must select a status for the property!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxStatus.Name, Language.GetErrorText("errorEmptyPropertyStatus", "You must select a status for the property!")));
                toReturn = false;
            }

            try
            {
                if (((DataTable)dataGridViewProperties.DataSource).Select("Assigned = true").Length == 0)
                {
                    errorProvider1.SetError(dataGridViewProperties, Language.GetErrorText("errorEmptyContractProperties", "You must select at least one property for the contract!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(dataGridViewProperties.Name, Language.GetErrorText("errorEmptyContractProperties", "You must select at least one property for the contract!")));
                    toReturn = false;
                }
                else
                {
                    if (NewDR.RowState == DataRowState.Added || NewDR.RowState == DataRowState.Detached)
                    {
                        foreach (DataRow dr in ((DataTable)dataGridViewProperties.DataSource).Select("Assigned = true"))
                        {
                            if (ParentContractId <= 0 && Validator.PropertyIsAssignedToActiveContract(Convert.ToInt32(dr["id"])))
                            {
                                errorProvider1.SetError(dataGridViewProperties, Language.GetErrorText("errorPropertyAssignedToActiveContract", "At least one of the selected properties is already assigned to an active contract!"));
                                base.ErrorList.Add(new KeyValuePair<string, string>(dataGridViewProperties.Name, Language.GetErrorText("errorPropertyAssignedToActiveContract", "At least one of the selected properties is already assigned to an active contract!")));
                                toReturn = false;
                                break;
                            }
                        }
                    }
                }
            }
            catch { }
            if (dateTimePickerFinishDate.Value < dateTimePickerStartDate.Value)
            {
                errorProvider1.SetError(dateTimePickerFinishDate, Language.GetErrorText("errorFinishDateLowerThanStartDate", "The finish date is lower than the start date!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(dateTimePickerFinishDate.Name, Language.GetErrorText("errorFinishDateLowerThanStartDate", "The finish date is lower than the start date!")));
                toReturn = false;
            }
            if (dateTimePickerExpirationDate.Value < dateTimePickerStartDate.Value)
            {
                errorProvider1.SetError(dateTimePickerExpirationDate, Language.GetErrorText("errorExpirationDateLowerThanStartDate", "The expiration date is lower than the start date!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(dateTimePickerExpirationDate.Name, Language.GetErrorText("errorExpirationDateLowerThanStartDate", "The expiration date is lower than the start date!")));
                toReturn = false;
            }
            /* -- scos din 31.10.2012 - mail Dana ---
            try
            {
                DataRow owner = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_by_id", new object[] { new MySqlParameter("_ID", comboBoxOwner.SelectedValue) })).ExecuteSelectQuery().Tables[0].Rows[0];
                if (comboBoxCurrency.SelectedValue.ToString() != owner["bank_account_currency1"].ToString() &&
                    comboBoxCurrency.SelectedValue.ToString() != owner["bank_account_currency2"].ToString() &&
                    comboBoxCurrency.SelectedValue.ToString() != owner["bank_account_currency3"].ToString())
                {
                    errorProvider1.SetError(comboBoxCurrency, Language.GetErrorText("errorCurrencyNotFoundForOwner", "The selected currency doesn't match with the owner's accounts currency!"));
                    base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxCurrency.Name, Language.GetErrorText("errorCurrencyNotFoundForOwner", "The selected currency doesn't match with the owner's accounts currency!")));
                    toReturn = false;
                }
            }
            catch { }
            */

            if (ParentContractId > 0 && userTextBoxAddendumNumber.Text.Trim() == "")
            {
                errorProvider1.SetError(userTextBoxAddendumNumber, Language.GetErrorText("errorEmptyAdendumNumber", "The Adendum number can not be empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxAddendumNumber.Name, Language.GetErrorText("errorEmptyAdendumNumber", "The Adendum number can not be empty!")));
                toReturn = false;
            }
            if (ParentContractId > 0 && dateTimePickerAddendumDate.Value.ToString() == "")
            {
                errorProvider1.SetError(dateTimePickerAddendumDate, Language.GetErrorText("errorEmptyAdendumDate", "The Adendum date can not be empty!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(dateTimePickerAddendumDate.Name, Language.GetErrorText("errorEmptyAdendumDate", "The Adendum date can not be empty!")));
                toReturn = false;
            }

            return toReturn;
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            /*
            var f = new ContractSelect(true);
            f.dataGrid1.Selectable = true;
            f.StartPosition = FormStartPosition.CenterScreen;
            if (f.ShowDialog() == DialogResult.OK)
            {
                comboBoxParentContract.SelectedValue = f.dataGrid1.IdToReturn;
            }
            f.Dispose();
            */
        }

        private void comboBoxOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedIndex > 0)
                {
                    DataRowView drv = (DataRowView)((ComboBox)sender).SelectedItem;
                    int _owner_id = Convert.ToInt32(drv.Row["id"]);
                    try
                    {
                        if (NewDR["owner_id"].ToString() != comboBoxOwner.SelectedValue.ToString())
                        {
                            //int _old_owner_index = comboBoxOwner.SelectedIndex;
                            bool owner_is_assigned_to_contract = Convert.ToBoolean((new DataAccess(CommandType.StoredProcedure, "CONTRACTSsp_get_owner", new object[] { new MySqlParameter("_OWNER_ID", _owner_id) })).ExecuteScalarQuery());
                            if (owner_is_assigned_to_contract)
                            {
                                MessageBox.Show(Language.GetMessageBoxText("ownerAssignedToOtherActiveContract", "The selectd owner is already assigned to an active contract! You can not continue!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                comboBoxOwner.SelectedIndex = 0; // -1;
                                dataGridViewProperties.DataSource = new DataTable();
                                return;
                            }
                        }
                    }
                    catch { }
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", _owner_id) });
                    DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
                    if (dtProperties != null)
                    {
                        DataRow newP = dtProperties.NewRow();
                        newP["NAME"] = "NOT PROPERTY RELATED";
                        dtProperties.Rows.Add(newP);
                        //dtProperties.AcceptChanges();
                        /*
                        try
                        {
                            dataGridViewProperties.Columns.Remove("Assigned");
                        }
                        catch { }
                        dataGridViewProperties.DataSource = dtProperties;
                        DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
                        c.Name = "Assigned";
                        c.Width = 30;
                        c.HeaderText = "";
                        dataGridViewProperties.Columns.Insert(0, c);
                        foreach (DataGridViewColumn dc in dataGridViewProperties.Columns)
                        {
                            dc.ReadOnly = dc.Name == "Assigned" ? false : true;
                            dc.Visible = (dc.Name.ToLower() == "name" || dc.Name.ToLower()=="assigned") ? true : false;
                        }
                        */
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
                            dc.ReadOnly = (dc.Name.ToLower() == "assigned" ? false : true);
                            dc.Visible = ((dc.Name.ToLower() == "name" || dc.Name.ToLower() == "assigned") ? true : false);
                        }

                        Rectangle rect = dataGridViewProperties.GetCellDisplayRectangle(0, -1, true);
                        // set checkbox header to center of header cell. +1 pixel to position correctly.
                        //rect.X = rect.Location.X + (rect.Width / 4);

                        CheckBox checkboxHeader = new CheckBox();
                        checkboxHeader.Name = "checkboxHeader";
                        checkboxHeader.Size = new Size(rect.Height - 4, rect.Height - 4);
                        checkboxHeader.BackColor = Color.Transparent;
                        checkboxHeader.Location = new Point(rect.Location.X + 2, rect.Location.Y + 2);
                        checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
                        dataGridViewProperties.Columns[0].Frozen = true;
                        dataGridViewProperties.Controls.Add(checkboxHeader);
                    }
                }
                else
                {
                    //dataGridViewProperties.DataSource = null;
                    dataGridViewProperties.DataSource = new DataTable();
                }
            }
            catch(Exception exp) {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                //dataGridViewProperties.DataSource = null;
                dataGridViewProperties.DataSource = new DataTable();
            }
        }

        private void dataGridViewContractServices_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            /*
            if (((DataGridView)sender)[e.ColumnIndex, e.RowIndex].IsInEditMode)
            {
                switch (((DataGridView)sender)[e.ColumnIndex, e.RowIndex].ValueType.ToString())
                {
                    case "System.Double":
                        if (!Validator.IsDouble(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show(Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                        break;
                }
            }
            */
            try
            {
                if (!Validator.DataGridViewCellVallidator(((DataGridView)sender)[e.ColumnIndex, e.RowIndex]))
                {
                    e.Cancel = true;
                }
            }
            catch { }
        }
        
        private void dataGridViewContractServices_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dgvr in dataGridViewServiceAdditionalCosts.Rows)
                {
                    if (dgvr.Cells["property_id"].Value == DBNull.Value || dgvr.Cells["property_id"].Value == null)
                        dgvr.Cells["property_id"].Value = dataGridViewProperties.CurrentRow.Cells["id"].Value;
                }
            }
            catch { }
            try
            {
                if (dataGridViewContractServices.Columns[e.ColumnIndex].Name.ToLower() == "period" || dataGridViewContractServices.Columns[e.ColumnIndex].Name.ToLower() == "percent")
                {
                    foreach (DataGridViewRow dgvr in dataGridViewServiceAdditionalCosts.Rows)
                    {
                        dgvr.Cells[dataGridViewContractServices.Columns[e.ColumnIndex].Name.ToLower()].Value = dataGridViewContractServices[dataGridViewContractServices.Columns[e.ColumnIndex].Name.ToLower(), e.RowIndex].Value;
                    }
                }
                if (dataGridViewContractServices.Columns[e.ColumnIndex].Name.ToLower() == "service")
                {
                    foreach (DataGridViewRow dgvr in dataGridViewServiceAdditionalCosts.Rows)
                    {
                        dgvr.Cells["service_id"].Value = dataGridViewContractServices[dataGridViewContractServices.Columns[e.ColumnIndex].Name.ToLower(), e.RowIndex].Value;
                    }
                }
                if (dataGridViewContractServices.Columns[e.ColumnIndex].Name.ToLower() == "id")
                {
                    foreach (DataGridViewRow dgvr in dataGridViewServiceAdditionalCosts.Rows)
                    {
                        dgvr.Cells["contractpropertyservice_id"].Value = dataGridViewContractServices[dataGridViewContractServices.Columns[e.ColumnIndex].Name.ToLower(), e.RowIndex].Value;
                    }
                }
            }
            catch { }
            dataGridViewContractServices.EndEdit();
            dataGridViewServiceAdditionalCosts.EndEdit();

            //((DataTable)dataGridViewServiceAdditionalCosts.DataSource).AcceptChanges();
            try
            {
                int row_index = dataGridViewContractServices.CurrentRow.Index;
                try
                {
                    if (dataGridViewContractServices["id", row_index].Value != null && dataGridViewContractServices["id", row_index].Value.ToString().Trim() != "")
                    {
                        ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("contractpropertyservice_id = {0}", dataGridViewContractServices["id", row_index].Value.ToString().Trim());
                    }
                    else
                    {
                        //ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("service_id = {0} and period = {1} and percent = {2}", dataGridViewContractServices["service", row_index].Value != null ? dataGridViewContractServices["service", row_index].Value.ToString().Trim() : "-1", dataGridViewContractServices["period", row_index].Value != null ? dataGridViewContractServices["period", row_index].Value.ToString().Trim() : "-1", dataGridViewContractServices["percent", row_index].Value != null ? dataGridViewContractServices["percent", row_index].Value.ToString().Trim() : "false");
                        ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("service_id = {0} and period = {1} and percent = {2} and IsNull(property_id, -1) = {3}", dataGridViewContractServices["service", row_index].Value != null ? dataGridViewContractServices["service", row_index].Value.ToString().Trim() : "-1", dataGridViewContractServices["period", row_index].Value != null ? dataGridViewContractServices["period", row_index].Value.ToString().Trim() : "-1", dataGridViewContractServices["percent", row_index].Value != null ? dataGridViewContractServices["percent", row_index].Value.ToString().Trim() : "false", (dataGridViewContractServices["property_id", row_index].Value == DBNull.Value || dataGridViewContractServices["property_id", row_index].Value == null ? "-1" : dataGridViewContractServices["property_id", row_index].Value.ToString()));
                    }
                }
                catch (Exception exp)
                {
                    try
                    {
                        exp.ToString();
                        ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("id = 0");
                    }
                    catch { }
                }
            }
            catch {
                try {
                    ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("id = 0");                
                }
                catch { }
            }

        }
        
        private void dataGridViewContractServices_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int row_index = dataGridViewContractServices.CurrentRow.Index;
                groupBoxServiceAdditionalCosts.Visible = true;
                try
                {
                    if (dataGridViewContractServices["id", row_index].Value != null && dataGridViewContractServices["id", row_index].Value.ToString().Trim() != "")
                    {
                        ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("contractpropertyservice_id = {0}", dataGridViewContractServices["id", row_index].Value.ToString().Trim());
                    }
                    else
                    {
                        ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("service_id = {0} and period = {1} and percent = {2} and IsNull(property_id, -1) = {3}", dataGridViewContractServices["service", row_index].Value != null ? dataGridViewContractServices["service", row_index].Value.ToString().Trim() : "-1", dataGridViewContractServices["period", row_index].Value != null ? dataGridViewContractServices["period", row_index].Value.ToString().Trim() : "-1", dataGridViewContractServices["percent", row_index].Value != null ? dataGridViewContractServices["percent", row_index].Value.ToString().Trim() : "false", (dataGridViewContractServices["property_id", row_index].Value == DBNull.Value || dataGridViewContractServices["property_id", row_index].Value == null ? "-1" : dataGridViewContractServices["property_id", row_index].Value.ToString()));
                    }
                }
                catch (Exception exp)
                {
                    try
                    {
                        exp.ToString();
                        ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("id = 0");
                    }
                    catch { }
                }
            }
            catch {
                try
                {
                    ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("id = 0");
                }
                catch { }
            }
        }

        /*
        private void dataGridViewContractServices_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            groupBoxServiceAdditionalCosts.Visible = true;

            try
            {
                //FillAdditionalCosts(Convert.ToInt32(dataGridViewContractServices["id", e.RowIndex].Value));
                if (dataGridViewContractServices["id", e.RowIndex].Value != null && dataGridViewContractServices["id", e.RowIndex].Value.ToString().Trim() != "")
                {
                    ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("contractpropertyservice_id = {0}", dataGridViewContractServices["id", e.RowIndex].Value.ToString().Trim());
                }
                else
                {
                    ServicesAdditionalCosts.DefaultView.RowFilter = String.Format("service_id = {0} and period = {1} and percent = {2}", dataGridViewContractServices["service", e.RowIndex].Value != null ? dataGridViewContractServices["service", e.RowIndex].Value.ToString().Trim() : "-1", dataGridViewContractServices["period", e.RowIndex].Value != null ? dataGridViewContractServices["period", e.RowIndex].Value.ToString().Trim() : "-1", dataGridViewContractServices["percent", e.RowIndex].Value != null ? dataGridViewContractServices["percent", e.RowIndex].Value.ToString().Trim() : "false");
                }
            }
            catch (Exception exp) { exp.ToString(); }
        }
        */
         
        private void FillAdditionalCosts()
        {
            ServicesAdditionalCosts.Clear();
            DataTable cs = (DataTable)dataGridViewContractServices.DataSource;
            if (cs.Rows.Count > 0)
            {
                foreach (DataRow dr in cs.Rows)
                {
                    DataTable dtAdditionalCosts = (new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_get_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACTSERVICE_ID", dr["id"]) })).ExecuteSelectQuery().Tables[0];
                    if (dtAdditionalCosts != null && dtAdditionalCosts.Rows.Count > 0)
                    {
                        foreach (DataRow additionalCost in dtAdditionalCosts.Rows)
                        {
                            DataRow newAdditoanlCost = ServicesAdditionalCosts.NewRow();
                            newAdditoanlCost["modify"] = dr["modify"];
                            newAdditoanlCost["id"] = additionalCost["id"];
                            //newAdditoanlCost["contractpropertyservice_id"] = additionalCost["contractpropertyservice_id"];
                            newAdditoanlCost["service_id"] = dr["service_id"];
                            newAdditoanlCost["period"] = dr["period"];
                            newAdditoanlCost["percent"] = dr["percent"];
                            newAdditoanlCost["contractpropertyservice_id"] = additionalCost["contractpropertyservice_id"];
                            newAdditoanlCost["value"] = additionalCost["value"];
                            newAdditoanlCost["description"] = additionalCost["description"];
                            newAdditoanlCost["property_id"] = dr["property_id"];
                            ServicesAdditionalCosts.Rows.Add(newAdditoanlCost);
                        }
                        ServicesAdditionalCosts.AcceptChanges();
                    }
                }
            }
            else { 
                
            }
            dataGridViewServiceAdditionalCosts.DataSource = ServicesAdditionalCosts;
            foreach (DataGridViewColumn dgvc in dataGridViewServiceAdditionalCosts.Columns)
            {
                dgvc.Visible = (dgvc.Name.ToLower().IndexOf("description") > -1 || dgvc.Name.ToLower().IndexOf("value") > -1);
            }
        }

        public void FillAdditionalCosts(int contractservice_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTRACTS_SERVICES_ADDITIONAL_COSTSsp_get_by_contractservice_id", new object[] { new MySqlParameter("_CONTRACTSERVICE_ID", contractservice_id) });
            DataTable dt = da.ExecuteSelectQuery().Tables[0];
            dataGridViewServiceAdditionalCosts.DataSource = dt;
        }

        private void dataGridViewContractServices_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridViewRow row = dataGridViewContractServices.Rows[e.RowIndex];
                int service_id = Convert.ToInt32(row.Cells[dataGridViewContractServices.Columns["service"].Index].Value);
                int servicetype_id = Convert.ToInt32(row.Cells[dataGridViewContractServices.Columns["period"].Index].Value);
                int property_id = Convert.ToInt32(row.Cells[dataGridViewContractServices.Columns["property_id"].Index].Value);
                DataRow[] serviceTypes = ServicesServicetypes.Select(String.Format("service_id = {0} AND servicetype_id = {1}", service_id.ToString(), servicetype_id.ToString()));
                if ((serviceTypes == null || serviceTypes.Length <= 0) && (service_id != 0 && servicetype_id != 0 && property_id != 0))
                {
                    DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("serviceTypeNotInListForService", "The service type you have selected is not appropriate for the selected service! Continue?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (ans == System.Windows.Forms.DialogResult.No) e.Cancel = true;
                }
                //e.Cancel = true; 
            }
            catch { }
        }

        private void dataGridViewContractServices_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                if (((DataGridView)sender).Rows[e.RowIndex].Cells["modify"].Value.ToString().ToLower() == "true")
                {
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Pink;
                }
                else
                {
                    ((DataGridView)sender).Rows[e.RowIndex].DefaultCellStyle.BackColor = e.RowIndex % 2 == 0 ? ((DataGridView)sender).DefaultCellStyle.BackColor : ((DataGridView)sender).AlternatingRowsDefaultCellStyle.BackColor;
                }
            }
            catch { }
        }

        private void EnableDisableControls(System.Windows.Forms.Control control, bool enabled)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is System.Windows.Forms.TextBox || ctrl is System.Windows.Forms.ListBox || ctrl is System.Windows.Forms.ComboBox || ctrl is System.Windows.Forms.DateTimePicker || ctrl is System.Windows.Forms.CheckBox || ctrl is System.Windows.Forms.DataGridView || ctrl is System.Windows.Forms.PictureBox)
                {
                    ctrl.Enabled = enabled;
                }
                EnableDisableControls(ctrl, enabled);
            }
        }

        private void checkBoxModifyProperties_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewProperties.Enabled = ((CheckBox)sender).Checked;
            listBoxReasons.SetSelected(2, ((CheckBox)sender).Checked);
            splitContainer2.Panel1.BackColor = ((CheckBox)sender).Checked?Color.LightSalmon:Color.DarkGray;
        }

        private void checkBoxModifyDates_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerStartDate.Enabled = dateTimePickerFinishDate.Enabled = dateTimePickerExpirationDate.Enabled = ((CheckBox)sender).Checked;
            listBoxReasons.SetSelected(0, ((CheckBox)sender).Checked);
            splitContainer1.Panel1.BackColor = ((CheckBox)sender).Checked ? Color.LightSalmon : Color.DarkGray;
        }

        private void checkBoxModifyServices_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewContractServices.Enabled = ((CheckBox)sender).Checked;
            listBoxReasons.SetSelected(1, ((CheckBox)sender).Checked);
            splitContainer3.Panel1.BackColor = ((CheckBox)sender).Checked ? Color.LightSalmon : Color.DarkGray;
            groupBoxServiceAdditionalCosts.Enabled = ((CheckBox)sender).Checked;
            dataGridViewServiceAdditionalCosts.Enabled = true;
        }

        private void dataGridViewContractServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                //ch1 = (DataGridViewCheckBoxCell)dataGridViewContractServices.Rows[dataGridViewContractServices.CurrentRow.Index].Cells["modify"];
                ch1 = (DataGridViewCheckBoxCell)dataGridViewContractServices.Rows[e.RowIndex].Cells["modify"];
                //foreach (DataGridViewCell dgvc in dataGridViewContractServices.Rows[dataGridViewContractServices.CurrentRow.Index].Cells)
                foreach (DataGridViewCell dgvc in dataGridViewContractServices.Rows[e.RowIndex].Cells)
                {
                    //dgvc.ReadOnly = (!(dataGridViewContractServices.Columns[dgvc.ColumnIndex].Name.ToLower() == "modify") && !Convert.ToBoolean(ch1.Value == null ? true : ch1.EditedFormattedValue));
                    if (dataGridViewContractServices.Columns[dgvc.ColumnIndex].Name.ToLower() != "modify")
                    {
                        //dgvc.ReadOnly = !Convert.ToBoolean(ch1.Value == null ? true : ch1.EditedFormattedValue);
                        if (e.ColumnIndex == dataGridViewContractServices.Columns["modify"].Index)
                            dgvc.ReadOnly = !Convert.ToBoolean(ch1.EditedFormattedValue == null ? true : ch1.EditedFormattedValue);
                        else
                            dgvc.ReadOnly = false;
                    }
                    else
                        dgvc.ReadOnly = false;
                }
            }
            catch { }
            try
            {
                if (dataGridViewContractServices.Columns[e.ColumnIndex].Name == "DELETE_SERVICE" && !dataGridViewContractServices.Rows[e.RowIndex].IsNewRow)
                {
                    /*
                    //var rowsToDelete = ServicesAdditionalCosts.Select(string.Format("PROPERTY_ID={0} AND SERVICE_ID={1}", dataGridViewContractServices["property_id", dataGridViewContractServices.CurrentRow.Index].Value.ToString(), dataGridViewContractServices["service", dataGridViewContractServices.CurrentRow.Index].Value.ToString());
                    var rowsToDelete = ServicesAdditionalCosts.Select(string.Format("PROPERTY_ID={0} AND SERVICE_ID={1}", dataGridViewContractServices["property_id", e.RowIndex].Value.ToString(), dataGridViewContractServices["service", e.RowIndex].Value.ToString()));
                    foreach (var row in rowsToDelete)
                        row.Delete();
                    ServicesAdditionalCosts.AcceptChanges();
                    dataGridViewServiceAdditionalCosts.EndEdit();
                    dataGridViewContractServices.Rows.Remove(dataGridViewContractServices.CurrentRow);
                    dataGridViewContractServices.EndEdit();
                    */
                    DeleteGridService(dataGridViewContractServices["property_id", e.RowIndex].Value.ToString(), dataGridViewContractServices["service", e.RowIndex].Value.ToString());
                }
            }
            catch { }
            try
            {
                if (dataGridViewContractServices.Columns[e.ColumnIndex].Name == "ADD_TO_ALL" && !dataGridViewContractServices.Rows[e.RowIndex].IsNewRow)
                {
                    try
                    {
                        if (dataGridViewContractServices.Rows[e.RowIndex].IsNewRow || dataGridViewContractServices["service", e.RowIndex].Value == null || dataGridViewContractServices["service", e.RowIndex].Value == DBNull.Value || dataGridViewContractServices["service", e.RowIndex].Value.ToString() == "0") return;
                        if (MessageBox.Show(Language.GetMessageBoxText("addServiceToSelectedProperties", "Are you sure you want to add this service and values to all selected properties?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                        {
                            return;
                        }
                        dataGridViewContractServices.EndEdit();
                        foreach (DataGridViewRow dgvr in dataGridViewProperties.Rows)
                        {
                            if (Convert.ToBoolean(dgvr.Cells["Assigned"].Value) && dgvr.Index != dataGridViewProperties.CurrentRow.Index && dgvr.Cells["NAME"].Value.ToString() != "NOT PROPERTY RELATED")
                            {
                                try
                                {
                                    //foreach(DataRow dr in ContractServices.Select(String.Format("PROPERTY_ID={0}", dgvr.Cells["ID"].Value.ToString()))){
                                    DataRow new_row = ContractServices.NewRow();
                                    new_row["modify"] = dataGridViewContractServices["modify", e.RowIndex].Value;
                                    new_row["service_id"] = dataGridViewContractServices["service", e.RowIndex].Value;
                                    new_row["period"] = dataGridViewContractServices["period", e.RowIndex].Value;
                                    new_row["percent"] = dataGridViewContractServices["percent", e.RowIndex].Value;
                                    new_row["value"] = dataGridViewContractServices["value", e.RowIndex].Value;
                                    new_row["property_id"] = dgvr.Cells["id"].Value;
                                    new_row["not_invoiceable"] = dataGridViewContractServices["not_invoiceable", e.RowIndex].Value;
                                    ContractServices.Rows.Add(new_row);
                                    //}
                                }
                                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }

                            }
                        }
                        ContractServices.AcceptChanges();
                    }
                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                }
            }
            catch { }
        }

        private void DeleteGridService(string property_id, string service_id)
        {
            try
            {
                if (dataGridViewContractServices.Rows[dataGridViewContractServices.CurrentRow.Index].IsNewRow) return;
                try
                {
                    //var rowsToDelete = ServicesAdditionalCosts.Select(string.Format("PROPERTY_ID={0} AND SERVICE_ID={1}", dataGridViewContractServices["property_id", dataGridViewContractServices.CurrentRow.Index].Value.ToString(), dataGridViewContractServices["service", dataGridViewContractServices.CurrentRow.Index].Value.ToString());
                    var rowsToDelete = ServicesAdditionalCosts.Select(string.Format("(PROPERTY_ID={0} AND SERVICE_ID={1})", property_id, service_id));
                    foreach (var row in rowsToDelete)
                        row.Delete();
                    ServicesAdditionalCosts.AcceptChanges();
                    dataGridViewServiceAdditionalCosts.EndEdit();
                }
                catch { }
                dataGridViewContractServices.Rows.Remove(dataGridViewContractServices.CurrentRow);
                dataGridViewContractServices.EndEdit();
                ContractServices.AcceptChanges();
                dataGridViewContractServices.Rows[0].Selected = true;
                dataGridViewContractServices.CurrentCell = dataGridViewContractServices.Rows[0].Cells["SERVICE"];
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }

        private void dataGridViewServiceAdditionalCosts_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                dataGridViewServiceAdditionalCosts.Rows[e.Row.Index - 1].Cells["property_id"].Value = dataGridViewContractServices.SelectedRows[0].Cells["property_id"].Value;
                dataGridViewServiceAdditionalCosts.Rows[e.Row.Index - 1].Cells["contractpropertyservice_id"].Value = dataGridViewContractServices.SelectedRows[0].Cells["id"].Value;
                dataGridViewServiceAdditionalCosts.Rows[e.Row.Index - 1].Cells["service_id"].Value = dataGridViewContractServices.SelectedRows[0].Cells["service"].Value;
                dataGridViewServiceAdditionalCosts.Rows[e.Row.Index - 1].Cells["period"].Value = dataGridViewContractServices.SelectedRows[0].Cells["period"].Value;
                dataGridViewServiceAdditionalCosts.Rows[e.Row.Index - 1].Cells["percent"].Value = dataGridViewContractServices.SelectedRows[0].Cells["percent"].Value;
            }
            catch { }
        }

        private void dataGridViewProperties_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int row_index = dataGridViewProperties.CurrentRow.Index;
                try
                {
                    labelSelectedProperty.Text = String.Format("{0}: {1}", Language.GetLabelText("Contracts.labelSelectedProperty", "Selected property"), dataGridViewProperties["name", row_index].Value.ToString());
                    try
                    {
                        dataGridViewContractServices.Rows[0].Selected = true;
                        dataGridViewContractServices.CurrentCell = dataGridViewContractServices.Rows[0].Cells["SERVICE"];
                    }
                    catch { }
                    ContractServices.DefaultView.RowFilter = (dataGridViewProperties["ID", dataGridViewProperties.CurrentRow.Index].Value == DBNull.Value || dataGridViewProperties["ID", dataGridViewProperties.CurrentRow.Index].Value == null) ? String.Format("IsNull(PROPERTY_ID, -1) = -1") : String.Format("PROPERTY_ID = {0}", dataGridViewProperties["ID", dataGridViewProperties.CurrentRow.Index].Value.ToString());
                }
                catch
                {
                    labelSelectedProperty.Text = String.Format("{0}: {1}", Language.GetLabelText("Contracts.labelSelectedProperty", "Selected property"), "");
                }
                try
                {
                    /*
                    if (dataGridViewContractServices["PROPERTY_ID", 0].Value.ToString() == "0")
                    {
                        dataGridViewContractServices.Rows.RemoveAt(0);
                        dataGridViewContractServices.Rows[0].Selected = true;
                        dataGridViewContractServices.CurrentCell = dataGridViewContractServices.Rows[0].Cells["SERVICE"];
                    }
                    */

                    foreach (DataGridViewRow dgrv in dataGridViewContractServices.Rows)
                    {
                        if (dgrv.Cells["PROPERTY_ID"].Value != null && dgrv.Cells["PROPERTY_ID"].Value != DBNull.Value && dgrv.Cells["PROPERTY_ID"].Value.ToString() == "0")
                        {
                            dataGridViewContractServices.Rows.Remove(dgrv);
                        }
                    }
                    /*
                    dataGridViewContractServices.EndEdit();
                    ContractServices.AcceptChanges();
                    var rows = ContractServices.Select("PROPERTY_ID=0");
                    foreach (var row in rows)
                        row.Delete();
                    ContractServices.AcceptChanges();
                    dataGridViewContractServices.EndEdit();
                    */
                    dataGridViewContractServices.Rows[0].Selected = true;
                    dataGridViewContractServices.CurrentCell = dataGridViewContractServices.Rows[0].Cells["SERVICE"];
                }
                catch { }
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }

        private void dataGridViewContractServices_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                dataGridViewContractServices.Rows[e.Row.Index - 1].Cells["property_id"].Value = dataGridViewProperties.SelectedRows[0].Cells["id"].Value;
                if (dataGridViewContractServices.Columns["MODIFY"].Visible)
                {
                    dataGridViewContractServices["MODIFY", e.Row.Index - 1].Value = true;
                }
            }
            catch { }
        }

        private void dataGridViewServiceAdditionalCosts_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!Validator.DataGridViewCellVallidator(((DataGridView)sender)[e.ColumnIndex, e.RowIndex]))
            {
                e.Cancel = true;
            }
        }

        /*
        private void dataGridViewServiceAdditionalCosts_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int row_index = dataGridViewServiceAdditionalCosts.CurrentRow.Index;
                dataGridViewServiceAdditionalCosts.Rows[row_index].Cells["contractpropertyservice_id"].Value = dataGridViewContractServices.SelectedRows[0].Cells["id"].Value;
                dataGridViewServiceAdditionalCosts.Rows[row_index].Cells["service_id"].Value = dataGridViewContractServices.SelectedRows[0].Cells["service"].Value;
                dataGridViewServiceAdditionalCosts.Rows[row_index].Cells["period"].Value = dataGridViewContractServices.SelectedRows[0].Cells["period"].Value;
                dataGridViewServiceAdditionalCosts.Rows[row_index].Cells["percent"].Value = dataGridViewContractServices.SelectedRows[0].Cells["percent"].Value;
            }
            catch { }
        }
        */

        private void dataGridViewProperties_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(String.Format("Data error on DataGridView: {0}, Row: {1}, Column: {2};\n Error message: {3}", ((DataGridView)sender).Name, (e.RowIndex + 1).ToString(), (e.ColumnIndex + 1).ToString(), e.Exception.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Cancel = true;
        }

        private void dataGridViewContractServices_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            switch (e.Exception.GetType().Name)
            {
                case "ConstraintException":
                    MessageBox.Show(Language.GetMessageBoxText("duplicateValues", "You have inserted/selected duplicate values! Please correct them before continuing!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
                case "NoNullAllowedException":
                    MessageBox.Show(Language.GetMessageBoxText("nullValue", "There are empty fields that are not allowed! Please correct them before continuing!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
                default:
                    MessageBox.Show(String.Format("Data error on DataGridView: {0}, Row: {1}, Column: {2};\n Error message: {3}", ((DataGridView)sender).Name, (e.RowIndex + 1).ToString(), (e.ColumnIndex + 1).ToString(), e.Exception.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            e.Cancel = true;
        }

        private void dataGridViewServiceAdditionalCosts_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(String.Format("Data error on DataGridView: {0}, Row: {1}, Column: {2};\n Error message: {3}", ((DataGridView)sender).Name, (e.RowIndex + 1).ToString(), (e.ColumnIndex + 1).ToString(), e.Exception.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Cancel = true;
        }

        private void userTextBoxFormatedContractNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                userTextBoxContractNumber.Text = Convert.ToInt32(((TextBox)sender).Text.Replace("FDP ", "").TrimStart('0')).ToString();
            }
            catch { }
        }
        /*
        private void userTextBoxFormatedContractNumber_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (((TextBox)sender).Text.Trim().Length == 9)
                {
                    userTextBoxContractNumber.Text = Convert.ToInt32(((TextBox)sender).Text.Replace("FDP ", "").TrimStart('0')).ToString();
                }
                else
                {
                    MessageBox.Show(Language.GetMessageBoxText("InvalidContractNumber", "Invalid contract number (eg. FDP 000XX) !"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            catch
            {
                MessageBox.Show(Language.GetMessageBoxText("InvalidContractNumber", "Invalid contract number (eg. FDP 000XX) !"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        */
        private void userTextBoxFormatedContractNumber_Validated(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Trim().Length <= 5 && Validator.IsInteger(((TextBox)sender).Text.Trim()))
            {
                try
                {
                    userTextBoxContractNumber.Text = (Convert.ToInt32(((TextBox)sender).Text.Trim())).ToString();
                    userTextBoxFormatedContractNumber.Text = String.Format("FDP {0}", (Convert.ToInt32(((TextBox)sender).Text.Trim())).ToString().PadLeft(5, '0'));
                }
                catch
                {
                    userTextBoxContractNumber.Text = "0";
                }
            }
            else
            {
                userTextBoxContractNumber.Text = "0";
            }
        }

        private void dataGridViewContractServices_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridViewContractServices[e.ColumnIndex, e.RowIndex].Value = ((ComboBox)dataGridViewContractServices.EditingControl).SelectedValue;
            }
            catch (Exception exp) { exp.ToString(); }
            //dataGridViewContractServices.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //dataGridViewContractServices.EndEdit();
            //dataGridViewContractServices.BindingContext[dataGridViewContractServices.DataSource, dataGridViewContractServices.DataMember].EndCurrentEdit();
            try
            {
                if (dataGridViewContractServices["PROPERTY_ID", e.RowIndex].Value.ToString() == "0" && dataGridViewProperties.CurrentRow.Index > -1)
                    dataGridViewContractServices["PROPERTY_ID", e.RowIndex].Value = dataGridViewProperties["ID", dataGridViewProperties.CurrentRow.Index].Value;
            }
            catch (Exception exp) { exp.ToString(); }
        }

        private void dataGridViewContractServices_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = dataGridViewContractServices.HitTest(e.X, e.Y);
            if (hti.Type == DataGridViewHitTestType.RowHeader)
            {
                dataGridViewContractServices.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                dataGridViewContractServices.EndEdit();
            }
        }

        private void dataGridViewContractServices_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridViewContractServices.Columns["ADD_TO_ALL"].Index || e.ColumnIndex == dataGridViewContractServices.Columns["DELETE_SERVICE"].Index)
                {
                    switch (dataGridViewContractServices.Columns[e.ColumnIndex].Name)
                    {
                        case "ADD_TO_ALL":
                            dataGridViewContractServices[e.ColumnIndex, e.RowIndex].ToolTipText = Language.GetLabelText("contractAddToAll", "Add this service and values to all selected properties");
                            break;
                        case "DELETE_SERVICE":
                            dataGridViewContractServices[e.ColumnIndex, e.RowIndex].ToolTipText = Language.GetLabelText("contractDeleteService", "Delete this service (row)");
                            break;
                    }
                    this.Cursor = Cursors.Hand;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
            catch { }
        }

        private void dataGridViewContractServices_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*
            try
            {
                dataGridViewContractServices[e.ColumnIndex, e.RowIndex].Value = ((ComboBox)dataGridViewContractServices.EditingControl).SelectedValue;
            }
            catch (Exception exp) { exp.ToString(); }
            */
            dataGridViewContractServices.EditMode = DataGridViewEditMode.EditOnEnter;
        }

        private void dataGridViewContractServices_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int row_index = e.Row.Index;
            var rows = ServicesAdditionalCosts.Select(String.Format("(service_id = {0} or IsNull(service_id, 0) = 0) and (period = {1} or IsNull(period, 0) = 0) and (percent = {2} or IsNull(percent, 0) = 0) and IsNull(property_id, -1) = {3}", dataGridViewContractServices["service", row_index].Value != null ? dataGridViewContractServices["service", row_index].Value.ToString().Trim() : "-1", dataGridViewContractServices["period", row_index].Value != null ? dataGridViewContractServices["period", row_index].Value.ToString().Trim() : "-1", dataGridViewContractServices["percent", row_index].Value != null ? dataGridViewContractServices["percent", row_index].Value.ToString().Trim() : "false", (dataGridViewContractServices["property_id", row_index].Value == DBNull.Value || dataGridViewContractServices["property_id", row_index].Value == null ? "-1" : dataGridViewContractServices["property_id", row_index].Value.ToString())));
            foreach (var row in rows)
                row.Delete();
            ServicesAdditionalCosts.AcceptChanges();
        }

        private void dataGridViewProperties_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridViewContractServices.Rows[0].Selected = true;
                dataGridViewContractServices.CurrentCell = dataGridViewContractServices.Rows[0].Cells["service"];
            }
            catch (Exception exp) { exp.ToString(); }
        }

        private void Contracts_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            try
            {
                //ContractSelect cs = (ContractSelect)this.Owner;
                //ContractSelect cs = (ContractSelect)this.ParentForm;
                //ContractSelect cs =   (ContractSelect)((main)FindMainForm()).Controls.Find("ContractSelect", true)[0];
                ContractSelect cs = (ContractSelect)this.Launcher;
                switch (cs.EditMode)
                {
                    case 1:
                        cs.SaveRecord();
                        EnableDisableToolBarButtons(true);
                        EnableDisableMainMenuButtons(true);
                        break;
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
            */
        }

        #region --- for example ---
        /*
        
        private void ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //((ComboBox)sender).BackColor = (Color)((ComboBox)sender).SelectedItem;
            dataGridViewContractServices.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }
        
        private void dataGridViewContractServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex != ((DataGridView)sender).Columns["DELETE_SERVICE"].Index)
                    return;
                var rows = ServicesAdditionalCosts.Select(String.Format("(service_id = {0} or IsNull(service_id, 0) = 0) and (period = {1} or IsNull(period, 0) = 0) and (percent = {2} or IsNull(percent, 0) = 0) and IsNull(property_id, -1) = {3}", dataGridViewContractServices["service", e.RowIndex].Value != null ? dataGridViewContractServices["service", e.RowIndex].Value.ToString().Trim() : "-1", dataGridViewContractServices["period", e.RowIndex].Value != null ? dataGridViewContractServices["period", e.RowIndex].Value.ToString().Trim() : "-1", dataGridViewContractServices["percent", e.RowIndex].Value != null ? dataGridViewContractServices["percent", e.RowIndex].Value.ToString().Trim() : "false", (dataGridViewContractServices["property_id", e.RowIndex].Value == DBNull.Value || dataGridViewContractServices["property_id", e.RowIndex].Value == null ? "-1" : dataGridViewContractServices["property_id", e.RowIndex].Value.ToString())));
                foreach (var row in rows)
                    row.Delete();
                ServicesAdditionalCosts.AcceptChanges();

                ((DataGridView)sender).Rows.RemoveAt(e.RowIndex);
                ((DataGridView)sender).EndEdit();
            }
            catch (Exception exp) { exp.ToString(); }
        }
        
        private void dataGridViewContractServices_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                // Remove an existing event-handler, if present, to avoid 
                // adding multiple handlers when the editing control is reused.
                combo.SelectedValueChanged -=
                    new EventHandler(ComboBox_SelectedValueChanged);

                // Add the event handler. 
                combo.SelectedValueChanged +=
                    new EventHandler(ComboBox_SelectedValueChanged);
            }
            try
            {
                Button b = e.Control as Button;
                if (b != null)
                {
                    b.Image = new Bitmap(Path.Combine(SettingsClass.Icons16ImagePath, "33.png"));
                }
            }
            catch { }
        }

        private void dataGridViewContractServices_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Delete)
                {
                    int row_index = dataGridViewContractServices.CurrentCell.RowIndex;
                    int col_index = dataGridViewContractServices.CurrentCell.ColumnIndex;
                    var rows = ServicesAdditionalCosts.Select(String.Format("(service_id = {0} or IsNull(service_id, 0) = 0) and (period = {1} or IsNull(period, 0) = 0) and (percent = {2} or IsNull(percent, 0) = 0) and IsNull(property_id, -1) = {3}", dataGridViewContractServices["service", row_index].Value != null ? dataGridViewContractServices["service", row_index].Value.ToString().Trim() : "-1", dataGridViewContractServices["period", row_index].Value != null ? dataGridViewContractServices["period", row_index].Value.ToString().Trim() : "-1", dataGridViewContractServices["percent", row_index].Value != null ? dataGridViewContractServices["percent", row_index].Value.ToString().Trim() : "false", (dataGridViewContractServices["property_id", row_index].Value == DBNull.Value || dataGridViewContractServices["property_id", row_index].Value == null ? "-1" : dataGridViewContractServices["property_id", row_index].Value.ToString())));
                    foreach (var row in rows)
                        row.Delete();
                    ServicesAdditionalCosts.AcceptChanges();

                    ((DataGridView)sender).Rows.RemoveAt(row_index);
                    ((DataGridView)sender).EndEdit();
                }
            }
            catch (Exception exp) { exp.ToString(); }
        }
        */
        #endregion

        private void dataGridViewContractServices_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (e.ColumnIndex == dataGridViewContractServices.Columns["property_id"].Index &&
                dataGridViewContractServices["property_id", e.RowIndex].ToString() == "0")
                MessageBox.Show("fucckkkk");
             */
        }

        private void checkBoxAutomaticallyRenewed_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxUseAsEndDate.Checked = ((CheckBox)sender).Checked ? checkBoxUseAsEndDate.Checked : false;
            checkBoxUseAsEndDate.Enabled = ((CheckBox)sender).Checked;
        }
    }
}
