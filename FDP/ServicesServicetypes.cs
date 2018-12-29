using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FDP
{
    public partial class ServicesServicetypes : UserForm
    {
        //this.dataGrid1 = new FDP.DataGrid("SERVICES_SERVICETYPESsp_select", null, "SERVICES_SERVICETYPESsp_insert", null, "SERVICES_SERVICETYPESsp_update", null, "SERVICES_SERVICETYPESsp_delete", null, null, null, null, new string[]{"SERVICE_ID", "SERVICETYPE_ID"}, null, null, this.Selectable, true);
        public DataAccess da = new DataAccess("SERVICES_SERVICETYPESsp_select", "SERVICES_SERVICETYPESsp_insert", "SERVICES_SERVICETYPESsp_update", "SERVICES_SERVICETYPESsp_delete");

        public ServicesServicetypes()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in ((DataTable)((BindingSource)dataGridViewServiceTypes.DataSource).DataSource).GetChanges().Rows)
            {
                switch(dr.RowState){
                    case DataRowState.Modified:
                    da.AttachUpdateParams(da.GenerateMySqlParameters(dr.Table, dr.ItemArray, 1));
                    da.mySqlDataAdapter.Update(((DataTable)((BindingSource)dataGridViewServiceTypes.DataSource).DataSource).GetChanges().Select(String.Format("id = {0}", dr["id"].ToString())));
                    dr.AcceptChanges();
                    break;
                    case DataRowState.Added:
                    da.AttachInsertParams(da.GenerateMySqlParameters(dr.Table, dr.ItemArray, 0));
                    da.mySqlDataAdapter.Update(((DataTable)((BindingSource)dataGridViewServiceTypes.DataSource).DataSource).GetChanges().Select(String.Format("service_id = {0} AND servicetype_id = {1}", dr["service_id"].ToString(), dr["servicetype_id"].ToString())));
                    dr.AcceptChanges();
                    break;
                    case DataRowState.Deleted:
                    break;
                }
            }
            ((DataTable)((BindingSource)dataGridViewServiceTypes.DataSource).DataSource).AcceptChanges();
        }

        private void ServicesServicetypes_Load(object sender, EventArgs e)
        {
            dataGridViewServiceTypes.DataSource = da.bindingSource;
            dataGridViewServiceTypes.Columns["id"].Visible = false;
            DataGridViewComboBoxColumn dgvcbc = new DataGridViewComboBoxColumn();
            DataTable dtSource = (new DataAccess(CommandType.StoredProcedure, CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name_for_grid", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "service_type") })).ExecuteSelectQuery().Tables[0];
            dgvcbc.DisplayMember = "name";
            dgvcbc.ValueMember = "id";
            dgvcbc.HeaderText = Language.GetColumnHeaderText("SERVICETYPE_ID", "SERVICE TYPE");
            dgvcbc.DataPropertyName = "SERVICETYPE_ID";
            dgvcbc.DataSource = dtSource;
            dgvcbc.FlatStyle = FlatStyle.Popup;
            int col_index = dataGridViewServiceTypes.Columns["SERVICETYPE_ID"].Index;
            dataGridViewServiceTypes.Columns.Remove("SERVICETYPE_ID");
            dataGridViewServiceTypes.Columns.Insert(col_index, dgvcbc);

            DataGridViewComboBoxColumn dgvcbcs = new DataGridViewComboBoxColumn();
            DataTable dtSourceServices = (new DataAccess(CommandType.StoredProcedure, CommandType.StoredProcedure, "SERVICESsp_list_for_grid")).ExecuteSelectQuery().Tables[0];
            dgvcbcs.DisplayMember = "name";
            dgvcbcs.ValueMember = "id";
            dgvcbcs.HeaderText = Language.GetColumnHeaderText("SERVICE_ID", "SERVICE");
            dgvcbcs.DataPropertyName = "SERVICE_ID";
            dgvcbcs.DataSource = dtSourceServices;
            dgvcbcs.FlatStyle = FlatStyle.Popup;
            col_index = dataGridViewServiceTypes.Columns["SERVICE_ID"].Index;
            dataGridViewServiceTypes.Columns.Remove("SERVICE_ID");
            dataGridViewServiceTypes.Columns.Insert(col_index, dgvcbcs);

            Language.LoadLabels(this.Name, dataGridViewServiceTypes);
            buttonExit.Visible = true;
            buttonExit.BringToFront();
            buttonSave.Visible = true;
            buttonSave.BringToFront();
        }
    }
}
