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
    public partial class Predicted_IncomeExpenses : UserForm
    {
        public Int32 OwnerId;
        public Int32 InvoiceId;
        public Predicted_IncomeExpenses()
        {
            InitializeComponent();
        }

        public Predicted_IncomeExpenses(Int32 owner_id, Int32 invoice_id)
        {
            OwnerId = owner_id;
            InvoiceId = invoice_id;
            InitializeComponent();
        }
        private void Predicted_IncomeExpenses_Load(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_get_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", OwnerId) });
            DataTable dtPIE = da.ExecuteSelectQuery().Tables[0];
            //dtPIE.Columns.Remove("deleted");
            DataColumn dc = new DataColumn();
            dc.ColumnName = "DELETE";
            dc.DataType = Type.GetType("System.Boolean");
            dc.DefaultValue = false;
            dtPIE.Columns.Add(dc);
            dataGridViewPredictedIE.DataSource = dtPIE;
            foreach(DataGridViewColumn dgvc in dataGridViewPredictedIE.Columns)
                if(dgvc.Name.ToLower() == "id" || dgvc.Name.ToLower().IndexOf("_id") > -1)
                    dgvc.Visible = false;
            dataGridViewPredictedIE.Columns["DELETE"].DisplayIndex = 0;

            da = new DataAccess(CommandType.StoredProcedure, "INCOME_EXPENSESsp_get_by_invoice_id", new object[] { new MySqlParameter("_INVOICE_ID", InvoiceId) });
            DataTable dtIE = da.ExecuteSelectQuery().Tables[0];

            foreach (DataGridViewRow dgvrPIE in dataGridViewPredictedIE.Rows)
            {
                foreach (DataRow drIE in dtIE.Rows)
                {
                    if (dgvrPIE.Cells["invoicerequirement_id"].Value.ToString() == drIE["invoicerequirement_id"].ToString())
                    {
                        dgvrPIE.DefaultCellStyle.BackColor = Color.LightPink;
                        dgvrPIE.Cells["DELETE"].Value = true;
                        break;
                    }
                }
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvrPIE in dataGridViewPredictedIE.Rows)
            {
                if (Convert.ToBoolean(dgvrPIE.Cells["DELETE"].Value))
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PREDICTED_INCOME_EXPENSESsp_change_status", new object[]{
                        new MySqlParameter("_ID", dgvrPIE.Cells["id"].Value),
                        new MySqlParameter("_DELETED", true)});
                    da.ExecuteUpdateQuery();
                }
            }
        }
    }
}
