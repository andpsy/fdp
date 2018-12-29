using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FDP
{
    public partial class CustomFilter : UserForm
    {
        public string ColumnName;
        public BindingSource Table1 = new BindingSource();
        public BindingSource Table2 = new BindingSource();
        public string Filter;

        public CustomFilter()
        {
            InitializeComponent();
        }

        public CustomFilter(string column_name, BindingSource table)
        {
            ColumnName = column_name;
            //Table1.DataSource = ((DataTable)table.DataSource).Copy();
            //Table2.DataSource = ((DataTable)table.DataSource).Copy();
            Table1.DataSource = ((DataTable)table.DataSource).DefaultView.ToTable(true, new string[] { ColumnName });
            Table2.DataSource = ((DataTable)table.DataSource).DefaultView.ToTable(true, new string[] { ColumnName });
            InitializeComponent();
        }

        private void CustomFilter_Load(object sender, EventArgs e)
        {
            comboBoxValues1.DisplayMember = ColumnName;
            //comboBoxValues1.ValueMember = "ID";
            comboBoxValues1.ValueMember = ColumnName;
            comboBoxValues1.DataSource = Table1;

            comboBoxValues2.DisplayMember = ColumnName;
            //comboBoxValues2.ValueMember = "ID";
            comboBoxValues2.ValueMember = ColumnName;
            comboBoxValues2.DataSource = Table2;

            groupBoxColumn.Text = Language.GetColumnHeaderText(ColumnName, ColumnName.ToUpper());
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string newColumnFilter = "";
            try
            {
                switch (comboBoxCondition1.SelectedItem.ToString().ToLower())
                {
                    case "equals":
                        newColumnFilter = String.Format("[{0}]='{1}'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "does not equal":
                        newColumnFilter = String.Format("[{0}]<>'{1}'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "is greater than":
                        newColumnFilter = String.Format("[{0}]>'{1}'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "is greater than or equal to":
                        newColumnFilter = String.Format("[{0}]>='{1}'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "is less than":
                        newColumnFilter = String.Format("[{0}]<'{1}'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "is less than or equal to":
                        newColumnFilter = String.Format("[{0}]<='{1}'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "begins with":
                        newColumnFilter = String.Format("Convert([{0}], 'System.String') LIKE '{1}*'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "does not begin with":
                        newColumnFilter = String.Format("Convert([{0}], 'System.String') NOT LIKE '{1}*'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "ends with":
                        newColumnFilter = String.Format("Convert([{0}], 'System.String') LIKE '*{1}'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "does not end with":
                        newColumnFilter = String.Format("Convert([{0}], 'System.String') NOT LIKE '*{1}'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "contains":
                        newColumnFilter = String.Format("Convert([{0}], 'System.String') LIKE '*{1}*'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "does not contain":
                        newColumnFilter = String.Format("Convert([{0}], 'System.String') NOT LIKE '*{1}*'", ColumnName, comboBoxValues1.Text != "" ? comboBoxValues1.Text : ((DataRowView)comboBoxValues1.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                        break;
                }

                if ((radioButtonAnd.Checked || radioButtonOr.Checked) && comboBoxCondition2.SelectedItem != null && comboBoxValues2.Text != "")
                {
                    newColumnFilter = String.Format(" ({0}{1}", newColumnFilter, radioButtonAnd.Checked ? " AND " : " OR ");
                    switch (comboBoxCondition2.SelectedItem.ToString().ToLower())
                    {
                        case "equals":
                            newColumnFilter += String.Format("[{0}]='{1}'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "does not equal":
                            newColumnFilter += String.Format("[{0}]<>'{1}'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "is greater than":
                            newColumnFilter += String.Format("[{0}]>'{1}'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "is greater than or equal to":
                            newColumnFilter += String.Format("[{0}]>='{1}'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "is less than":
                            newColumnFilter += String.Format("[{0}]<'{1}'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "is less than or equal to":
                            newColumnFilter += String.Format("[{0}]<='{1}'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "begins with":
                            newColumnFilter += String.Format("Convert([{0}], 'System.String') LIKE '{1}*'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "does not begin with":
                            newColumnFilter += String.Format("Convert([{0}], 'System.String') NOT LIKE '{1}*'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "ends with":
                            newColumnFilter += String.Format("Convert([{0}], 'System.String') LIKE '*{1}'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "does not end with":
                            newColumnFilter += String.Format("Convert([{0}], 'System.String') NOT LIKE '*{1}'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "contains":
                            newColumnFilter += String.Format("Convert([{0}], 'System.String') LIKE '*{1}*'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                        case "does not contain":
                            newColumnFilter += String.Format("Convert([{0}], 'System.String') NOT LIKE '*{1}*'", ColumnName, comboBoxValues2.Text != "" ? comboBoxValues2.Text : ((DataRowView)comboBoxValues2.SelectedItem)[ColumnName].ToString().Replace("'", "''"));
                            break;
                    }
                    newColumnFilter += ") ";
                }

                this.Filter = newColumnFilter;
            }
            catch 
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
