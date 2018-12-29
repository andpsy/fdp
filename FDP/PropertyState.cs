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
    public partial class PropertyState : UserForm
    {
        //this.dataGrid1 = new FDP.DataGrid("ROOMASSETSCONDITIONSsp_select", null, "ROOMASSETSCONDITIONSsp_insert", null, "ROOMASSETSCONDITIONSsp_update", null, "ROOMASSETSCONDITIONSsp_delete", null, null, null, null, new string[]{"STATUS_ID"}, null, new string[]{"NAME", "PRODUCER", "NUMBER", "DETAILS", "STATUS"}, this.Selectable, true);
        public DataTable InitialTable = new DataTable();
        public DataRow NewDR;
        public DataRow InitialDR;
        public ArrayList MySqlParameters = new ArrayList();
        public int PropertyId;
        public int VisitId;

        public PropertyState()
        {
            //base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        public PropertyState(DataRow dr)
        {
            //base.Maximized = FormWindowState.Maximized;
            NewDR = dr;
            InitializeComponent();
        }

        public PropertyState(int property_id, int visit_id)
        {
            //base.Maximized = FormWindowState.Maximized;
            PropertyId = property_id;
            VisitId = visit_id;
            InitializeComponent();
        }

        private void PropertyState_Load(object sender, EventArgs e)
        {
            if (PropertyId != 0)
            {
                FillInfo();
            }
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
        }

        private void FillInfo()
        {
            try
            {
                dataGridViewRoomAssets.DataSource = null;
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "VISITROOMASSETSCONDITIONSsp_get_by_property_id", new object[] { new MySqlParameter("_PROPERTY_ID", PropertyId) });
                DataTable dtRoomAssets = da.ExecuteSelectQuery().Tables[0];
                InitialTable = dtRoomAssets.Copy();
                dataGridViewRoomAssets.DataSource = dtRoomAssets;
                foreach (DataGridViewColumn dgvc in dataGridViewRoomAssets.Columns)
                {
                    string col_name = dgvc.Name.ToLower();
                    dgvc.Visible = (col_name == "room_name" || col_name == "asset_name" || col_name == "asset_producer" || col_name == "asset_number" || col_name == "asset_condition" || col_name == "asset_condition_details");                        
                }
                DataGridViewComboBoxColumn dgvcbc = new DataGridViewComboBoxColumn();
                dgvcbc.Name = "asset_condition";
                dgvcbc.HeaderText = Language.GetColumnHeaderText("asset_condition", "ASSET CONDITION");
                DataTable dtRoomAssetsConditions = new DataAccess(CommandType.StoredProcedure, "ROOMASSETCONDITIONSsp_list_for_grid").ExecuteSelectQuery().Tables[0];
                dgvcbc.DisplayMember = "name";
                dgvcbc.ValueMember = "id";
                dgvcbc.DataPropertyName = "roomassetcondition_id";
                dgvcbc.DataSource = dtRoomAssetsConditions;
                int col_index = dataGridViewRoomAssets.Columns["asset_condition"].Index;
                dgvcbc.FlatStyle = FlatStyle.Popup;
                dataGridViewRoomAssets.Columns.RemoveAt(col_index);
                dataGridViewRoomAssets.Columns.Insert(col_index, dgvcbc);
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        /*
        public void GenerateMySqlParameters()
        {
            try
            {
                if (CoOwnerDR != null)
                {
                    CoOwnerDR["name"] = userTextBoxOwnerName.Text;
                    CoOwnerDR["full_name"] = userTextBoxOwnerFullName.Text;
                    CoOwnerDR["cif"] = userTextBoxOwnerCif.Text;
                    CoOwnerDR["nif"] = userTextBoxNIF.Text;
                    CoOwnerDR["cnp"] = userTextBoxOwnerCnp.Text;
                    CoOwnerDR["cui"] = userTextBoxOwnerCui.Text;
                    CoOwnerDR["comments"] = userTextBoxOwnerComments.Text;
                }
                else
                {
                    if (CoOwnerDR.RowState != DataRowState.Added && CoOwnerDR != null)
                    {
                        MySqlParameter _ID = new MySqlParameter("_ID", CoOwnerDR["id"]);
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
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        */

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "VISITSASSETSCONDITIONSsp_delete_by_visit_id", new object[] {new MySqlParameter("_VISIT_ID", VisitId) });
            da.ExecuteUpdateQuery();

            foreach (DataGridViewRow dgvr in dataGridViewRoomAssets.Rows)
            {
                if (dgvr.Cells["asset_condition"].Value != DBNull.Value)
                {
                    da = new DataAccess(CommandType.StoredProcedure, "VISITSASSETSCONDITIONSsp_insert", new object[]{
                        new MySqlParameter("_VISIT_ID", VisitId),
                        new MySqlParameter("_PROPERTY_ID", PropertyId),
                        new MySqlParameter("_PROPERTYROOM_ID", dgvr.Cells["propertyroom_id"].Value),
                        new MySqlParameter("_ROOMASSET_ID", dgvr.Cells["roomasset_id"].Value),
                        new MySqlParameter("_ROOMASSETCONDITION_ID", dgvr.Cells["roomassetcondition_id"].Value),
                        new MySqlParameter("_DETAILS", dgvr.Cells["asset_condition_details"].Value)
                    });
                    da.ExecuteInsertQuery();
                }
            }

            FillInfo();
            InitialDR = CommonFunctions.CopyDataRow(NewDR);
            this.DialogResult = DialogResult.OK;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
/*
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
                toReturn = false;
            }

            if ((CoOwnerDR["cnp"] != DBNull.Value && CoOwnerDR["cnp"].ToString().Trim() != ""))
            {
                if (!Validator.SimpleValidateCNP(CoOwnerDR["cnp"].ToString()))
                {
                    errorProvider1.SetError(userTextBoxOwnerCnp, Language.GetErrorText("errorCnpInvalid", "CNP invalid!"));
                    toReturn = false;
                }
            }
            if ((CoOwnerDR["cui"] != DBNull.Value && CoOwnerDR["cui"].ToString().Trim() != ""))
            {
                if (!Validator.SimpleValidateCUI(CoOwnerDR["cui"].ToString()))
                {
                    errorProvider1.SetError(userTextBoxOwnerCui, Language.GetErrorText("errorCuiCifInvalid", "CUI/CIF invalid!"));
                    toReturn = false;
                }
            }
            return toReturn;
        }
 */
    }
}
