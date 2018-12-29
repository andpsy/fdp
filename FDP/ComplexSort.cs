using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FDP
{
    public partial class ComplexSort : UserForm
    {
        public DataGridView data_grid_view;
        public string Order;

        public ComplexSort()
        {
            InitializeComponent();
        }

        public ComplexSort(DataGridView dgv)
        {
            data_grid_view = dgv;
            InitializeComponent();
        }

        private void ComplexSort_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Column", Type.GetType("System.String"));
            dt.Columns.Add(dc);
            dc = new DataColumn("Direction", Type.GetType("System.String"));
            dt.Columns.Add(dc);
            dataGridView.DataSource = dt;

            DataTable dtColumns = new DataTable();
            dtColumns.Columns.Add(new DataColumn("Column", Type.GetType("System.String")));
            /*
            foreach (DataColumn dcol in ((DataTable)((BindingSource)data_grid_view.DataSource).DataSource).Columns)
            {
                if (data_grid_view.Columns[dcol.ColumnName.ToLower()] != null && data_grid_view.Columns[dcol.ColumnName.ToLower()].Visible)
                {
                    dtColumns.Rows.Add(new object[] { dcol.ColumnName });
                }
            }
            */
            
            foreach (DataGridViewColumn dgvc in data_grid_view.Columns)
            {
                if (dgvc.Visible)
                {
                    dtColumns.Rows.Add(new object[] { dgvc.Name });
                }
            }
            
            dtColumns.AcceptChanges();
            DataTable dtDirections = new DataTable();
            dtDirections.Columns.Add(new DataColumn("Direction", Type.GetType("System.String")));
            dtDirections.Rows.Add(new object[] { "ASC" });
            dtDirections.Rows.Add(new object[] { "DESC" });
            dtDirections.AcceptChanges();

            DataGridViewComboBoxColumn dgvcbc = new DataGridViewComboBoxColumn();
            dgvcbc.Name = "Column";
            dgvcbc.DataPropertyName = "Column";
            dgvcbc.DisplayMember = "Column";
            dgvcbc.ValueMember = "Column";
            dgvcbc.DataSource = dtColumns;
            dgvcbc.FlatStyle = FlatStyle.Popup;
            dataGridView.Columns.Remove("Column");
            dataGridView.Columns.Add(dgvcbc);

            DataGridViewComboBoxColumn dgvcbc2 = new DataGridViewComboBoxColumn();
            dgvcbc2.Name = "Direction";
            dgvcbc2.DataPropertyName = "Direction";
            dgvcbc2.DisplayMember = "Direction";
            dgvcbc2.ValueMember = "Direction";
            dgvcbc2.DataSource = dtDirections;
            dgvcbc2.FlatStyle = FlatStyle.Popup;
            dataGridView.Columns.Remove("Direction");
            dataGridView.Columns.Add(dgvcbc2);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonApplySorting_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView.DataSource).AcceptChanges();
            foreach (DataRow dr in ((DataTable)dataGridView.DataSource).Rows)
                Order += String.Format(" {0} {1},", dr["Column"].ToString(), dr["Direction"].ToString().ToUpper());
            Order = Order.Remove(Order.Length - 1);
        }
    }
}
