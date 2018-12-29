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
    public partial class DynamicFormLists : UserForm
    {
        public string table_name;
        public DataRow data_row;
        public int action = 0; // (0 = ADD, 1 = EDIT)
        public int x, y = 10;
        public DataRow return_data_row;

        public DynamicFormLists()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        public DynamicFormLists(string table_name, DataRow data_row, int action)
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            this.Name = String.Format("DynamicFormLists_{0}", table_name);
            this.Text = CommonFunctions.SentenceCase(CommonFunctions.GetSingular(table_name));
            this.table_name = table_name;
            this.data_row = data_row;
            this.action = action;
            this.x = 10;
            this.y = 10;
        }


        private void DynamicFormLists_Load(object sender, EventArgs e)
        {
            this.Padding = new Padding(5, 15, 5, 5);
            foreach (DataColumn dc in this.data_row.Table.Columns)
            {
                if (dc.ColumnName.ToUpper().IndexOf("ID") == -1 && data_row.Table.Columns.IndexOf(String.Format("{0}_ID", dc.ColumnName.ToUpper())) == -1)
                {
                    Label lbl = new Label();
                    lbl.Name = String.Format("label{0}", CommonFunctions.SentenceCase(dc.ColumnName));
                    lbl.Padding = new Padding(3, 10, 3, 3);
                    lbl.Font = new Font(SettingsClass.FontTheme, 8, FontStyle.Bold);
                    lbl.Text = Language.GetColumnHeaderText(dc.ColumnName.ToLower(), dc.ColumnName.ToUpper());
                    lbl.Top = this.x;
                    lbl.Left = this.y;
                    lbl.Width = 120;
                    //lbl.AutoSize = true;
                    this.Controls.Add(lbl);

                    UserTextBox utb = new UserTextBox();
                    utb.Name = String.Format("textBox{0}", CommonFunctions.SentenceCase(dc.ColumnName));
                    utb.Padding = new Padding(3, 10, 3, 3);
                    utb.Text = action == 0 ? "" : data_row[dc.ColumnName].ToString();
                    utb.Top = this.x;
                    utb.Left = (this.y + 10 + lbl.Width);
                    utb.Width = 120;
                    this.Controls.Add(utb);

                    this.x += ((lbl.Height > utb.Height ? lbl.Height : utb.Height) + 10);
                }
                else
                {
                    switch (dc.ColumnName.ToUpper())
                    {
                        case "STATUS_ID":
                            Label lbl = new Label();
                            lbl.Name = String.Format("label{0}", CommonFunctions.SentenceCase(dc.ColumnName));
                            lbl.Padding = new Padding(3, 10, 3, 3);
                            lbl.Font = new Font(SettingsClass.FontTheme, 8, FontStyle.Bold);
                            lbl.Text = Language.GetColumnHeaderText("status", "STATUS");
                            lbl.Top = this.x;
                            lbl.Left = this.y;
                            lbl.Width = 120;
                            this.Controls.Add(lbl);

                            ComboBox cbx = new ComboBox();
                            cbx.Name = String.Format("comboBox{0}", CommonFunctions.SentenceCase(dc.ColumnName));
                            cbx.Padding = new Padding(3, 10, 3, 3);
                            cbx.Top = this.x;
                            cbx.Left = (this.y + 10 + lbl.Width);
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[] { new MySql.Data.MySqlClient.MySqlParameter("_LIST_TYPE_NAME", "ASSET_STATUS") });
                            DataTable dtStatus = da.ExecuteSelectQuery().Tables[0];
                            if (dtStatus != null)
                            {
                                cbx.DisplayMember = "name";
                                cbx.ValueMember = "id";
                                cbx.DataSource = dtStatus;
                            }
                            cbx.SelectedValue = data_row[dc.ColumnName];
                            cbx.Width = 120;
                            this.Controls.Add(cbx);

                            this.x += ((lbl.Height > cbx.Height ? lbl.Height : cbx.Height) + 10);

                            break;
                        case "DISTRICT_ID":
                            lbl = new Label();
                            lbl.Name = String.Format("label{0}", CommonFunctions.SentenceCase(dc.ColumnName));
                            lbl.Padding = new Padding(3, 10, 3, 3);
                            lbl.Font = new Font(SettingsClass.FontTheme, 8, FontStyle.Bold);
                            lbl.Text = Language.GetColumnHeaderText("district", "DISTRICT");
                            lbl.Top = this.x;
                            lbl.Left = this.y;
                            lbl.Width = 120;
                            this.Controls.Add(lbl);

                            cbx = new ComboBox();
                            cbx.Name = String.Format("comboBox{0}", CommonFunctions.SentenceCase(dc.ColumnName));
                            cbx.Padding = new Padding(3, 10, 3, 3);
                            cbx.Top = this.x;
                            cbx.Left = (this.y + 10 + lbl.Width);
                            da = new DataAccess(CommandType.StoredProcedure, "DISTRICTSsp_list");
                            dtStatus = da.ExecuteSelectQuery().Tables[0];
                            if (dtStatus != null)
                            {
                                cbx.DisplayMember = "name";
                                cbx.ValueMember = "id";
                                cbx.DataSource = dtStatus;
                            }
                            cbx.SelectedValue = data_row[dc.ColumnName];
                            cbx.Width = 120;
                            this.Controls.Add(cbx);

                            this.x += ((lbl.Height > cbx.Height ? lbl.Height : cbx.Height) + 10);
                            break;
                    }
                }
            }
            UserButton ubSave = new UserButton();
            ubSave.Name = "buttonSaveListItem";
            ubSave.Text = "Save";
            ubSave.Image = new Bitmap(Path.Combine(SettingsClass.Icons16ImagePath, "22.png"));
            ubSave.Top = this.x + 10;
            ubSave.Left = 10;
            ubSave.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            ubSave.DialogResult = DialogResult.OK;
            ubSave.Click += new EventHandler(ubSave_Click);
            //click event
            this.Controls.Add(ubSave);

            UserButton ubCancel = new UserButton();
            ubCancel.Name = "buttonCancelListItem";
            ubCancel.Text = "Cancel";
            ubCancel.Image = new Bitmap(Path.Combine(SettingsClass.Icons16ImagePath, "76.png"));
            ubCancel.Top = this.x + 10;
            ubCancel.Left = (ubSave.Width + 20);
            ubCancel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            ubCancel.DialogResult = DialogResult.Cancel;
            ubCancel.Click += new EventHandler(ubCancel_Click);
            //click event
            this.Controls.Add(ubCancel);
        }

        private void ubSave_Click(object sender, EventArgs e)
        {
            return_data_row = data_row;
            foreach (DataColumn dc in this.return_data_row.Table.Columns)
            {
                if (dc.ColumnName.ToUpper().IndexOf("ID") == -1 && data_row.Table.Columns.IndexOf(String.Format("{0}_ID", dc.ColumnName.ToUpper())) == -1)
                {
                    return_data_row[dc.ColumnName] = ((UserTextBox)this.Controls[String.Format("textBox{0}", CommonFunctions.SentenceCase(dc.ColumnName))]).Text.Trim()==""? DBNull.Value: (object)((UserTextBox)this.Controls[String.Format("textBox{0}", CommonFunctions.SentenceCase(dc.ColumnName))]).Text;
                }
                else
                {
                    switch (dc.ColumnName.ToUpper())
                    {
                        case "STATUS_ID":
                            return_data_row[dc.ColumnName] = ((ComboBox)this.Controls[String.Format("comboBox{0}", CommonFunctions.SentenceCase(dc.ColumnName))]).SelectedValue;
                            return_data_row[dc.ColumnName.ToUpper().Replace("_ID", "")] = ((DataRowView)((ComboBox)this.Controls[String.Format("comboBox{0}", CommonFunctions.SentenceCase(dc.ColumnName))]).SelectedItem)["name"];
                            break;
                        case "DISTRICT_ID":
                            return_data_row[dc.ColumnName] = ((ComboBox)this.Controls[String.Format("comboBox{0}", CommonFunctions.SentenceCase(dc.ColumnName))]).SelectedValue;
                            return_data_row[dc.ColumnName.ToUpper().Replace("_ID", "")] = ((DataRowView)((ComboBox)this.Controls[String.Format("comboBox{0}", CommonFunctions.SentenceCase(dc.ColumnName))]).SelectedItem)["name"];
                            break;
                    }
                }
            }
            this.Close();
        }

        private void ubCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
