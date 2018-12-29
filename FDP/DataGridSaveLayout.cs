using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FDP
{
    public partial class DataGridSaveLayout : UserForm
    {
        public DataGridView DataGridViewToSave = new DataGridView();
        public DataSet DataSetToSave = new DataSet();
        public string FormName;
        public DataGridSaveLayout()
        {
            InitializeComponent();
        }
        public DataGridSaveLayout(DataGridView dgv, string form_name)
        {
            FormName = form_name;
            DataGridViewToSave = dgv;
            InitializeComponent();
        }


        private void DataGridSaveLayout_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("GENERAL_INFORMATION");
            DataColumn dc = new DataColumn();
            dc.ColumnName = "name";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "form_name";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "employee_id";
            dc.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "visible_to_others";
            dc.DataType = Type.GetType("System.Boolean");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "default_layout";
            dc.DataType = Type.GetType("System.Boolean");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "filter";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "sort_column";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "sort_order";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "layout_mode";
            dc.DataType = Type.GetType("System.String");
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "date";
            dc.DataType = Type.GetType("System.DateTime");
            dt.Columns.Add(dc);

            dt.AcceptChanges();

            DataSetToSave.Tables.Add(dt);

            DataTable dtColumns = new DataTable("COLUMNS");
            dc = new DataColumn();
            dc.ColumnName = "column_name";
            dc.DataType = Type.GetType("System.String");
            dtColumns.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "column_header";
            dc.DataType = Type.GetType("System.String");
            dtColumns.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "column_width";
            dc.DataType = Type.GetType("System.Int32");
            dtColumns.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "column_frozen";
            dc.DataType = Type.GetType("System.Boolean");
            dtColumns.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "column_visible";
            dc.DataType = Type.GetType("System.Boolean");
            dtColumns.Columns.Add(dc);

            dc = new DataColumn();
            dc.ColumnName = "displaynumber";
            dc.DataType = Type.GetType("System.Int32");
            dtColumns.Columns.Add(dc);

            dtColumns.AcceptChanges();

            DataSetToSave.Tables.Add(dtColumns);

            DataSetToSave.AcceptChanges();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dr = DataSetToSave.Tables["GENERAL_INFORMATION"].NewRow();
                dr["name"] = userTextBoxLayoutName.Text;
                dr["form_name"] = FormName;
                dr["employee_id"] = SettingsClass.EmployeeId;
                dr["visible_to_others"] = checkBoxVisibleForOthers.Checked;
                dr["default_layout"] = checkBoxDefaultLayout.Checked;
                dr["filter"] = ((BindingSource)DataGridViewToSave.DataSource).Filter == null ? "" : ((BindingSource)DataGridViewToSave.DataSource).Filter;
                dr["sort_column"] = DataGridViewToSave.SortedColumn == null ? "" : DataGridViewToSave.SortedColumn.Name;
                dr["sort_order"] = DataGridViewToSave.SortOrder.ToString();
                dr["layout_mode"] = DataGridViewToSave.AutoSizeColumnsMode.ToString();
                dr["date"] = DateTime.Now.Date;
                DataSetToSave.Tables["GENERAL_INFORMATION"].Rows.Add(dr);
                DataSetToSave.Tables["GENERAL_INFORMATION"].AcceptChanges();

                foreach (DataGridViewColumn dgvc in DataGridViewToSave.Columns)
                {
                    DataRow drc = DataSetToSave.Tables["COLUMNS"].NewRow();
                    drc["column_name"] = dgvc.Name;
                    drc["column_header"] = dgvc.HeaderText;
                    drc["column_width"] = dgvc.Width;
                    drc["column_frozen"] = dgvc.Frozen;
                    drc["column_visible"] = dgvc.Visible;
                    drc["displaynumber"] = dgvc.DisplayIndex;
                    DataSetToSave.Tables["COLUMNS"].Rows.Add(drc);
                }
                DataSetToSave.Tables["COLUMNS"].AcceptChanges();

                DataSetToSave.AcceptChanges();
                if (File.Exists(Path.Combine(SettingsClass.LayoutsPath, String.Format("{0}.xml", userTextBoxLayoutName.Text))))
                {
                    var ans = MessageBox.Show(Language.GetMessageBoxText("confirmFileOverride", "Overwrite existing file?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ans == DialogResult.Yes)
                        DataSetToSave.WriteXml(Path.Combine(SettingsClass.LayoutsPath, String.Format("{0}.xml", userTextBoxLayoutName.Text)));
                }
                else
                {
                    DataSetToSave.WriteXml(Path.Combine(SettingsClass.LayoutsPath, String.Format("{0}.xml", userTextBoxLayoutName.Text)));
                }
                if(checkBoxDefaultLayout.Checked)
                    SettingsClass.SetUserSetting(SettingsClass.EmployeeId, FormName, userTextBoxLayoutName.Text);
                else
                    SettingsClass.RemoveUserSetting(SettingsClass.EmployeeId, FormName, userTextBoxLayoutName.Text);

                base.ShowConfirmationDialog(Language.GetMessageBoxText("dataSaved", "Information was saved successfully!"));
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(Language.GetMessageBoxText("errorSavingLayout", String.Format("There was an error saving the layout: {0}", exp.Message)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Path.Combine(SettingsClass.LayoutsPath);
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                userTextBoxLayoutName.Text = openFileDialog1.FileName.Split('.')[0];
            }
        }
    }
}
