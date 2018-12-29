using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FDP
{
    public partial class MonthlyRentRates : UserForm
    {
        public DataAccess da = new DataAccess("MONTHLY_RENT_RATESsp_select", null, "MONTHLY_RENT_RATESsp_update", null);
        public MonthlyRentRates()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void MonthlyRentRates_Load(object sender, EventArgs e)
        {
            dataGridViewMonthlyRates.DataSource = da.bindingSource;
            dataGridViewMonthlyRates.Columns["id"].Visible = false;
            Language.LoadLabels(this.Name, dataGridViewMonthlyRates);
            buttonExit.Visible = true;
            buttonExit.BringToFront();
            buttonSave.Visible = true;
            buttonSave.BringToFront();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in ((DataTable)((BindingSource)dataGridViewMonthlyRates.DataSource).DataSource).GetChanges().Rows)
            {
                if (dr.RowState == DataRowState.Modified)
                {
                    da.AttachUpdateParams(da.GenerateMySqlParameters(dr.Table, dr.ItemArray, 1));
                    da.mySqlDataAdapter.Update(dr.Table);
                }
            }
        }
    }
}
