using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewAutoFilter
{
    public partial class CustomFilter : Form
    {
        public string ColumnName;
        public string TableName;

        public CustomFilter()
        {
            InitializeComponent();
        }

        public CustomFilter(string column_name, string table_name)
        {
            ColumnName = column_name;
            TableName = table_name;
            InitializeComponent();
        }

        private void CustomFilter_Load(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, String.Format("{0}sp_select", TableName.ToUpper()));
            DataTable dt = da.ExecuteSelectQuery().Tables[0];
            comboBoxValues1.DisplayMember = ColumnName;
            comboBoxValues1.ValueMember = "ID";
            comboBoxValues1.DataSource = dt;

            comboBoxValues2.DisplayMember = ColumnName;
            comboBoxValues2.ValueMember = "ID";
            comboBoxValues2.DataSource = dt;

        }
    }
}
