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
    public partial class DataGridLoadLayout : UserForm
    {
        public string LayoutName;
        //public DataGridView DataGridViewToLoad = new DataGridView();
        public string FormName;

        public DataGridLoadLayout()
        {
            InitializeComponent();
        }

        public DataGridLoadLayout(string form_name)
        {
            //DataGridViewToLoad = dgv;
            FormName = form_name;
            InitializeComponent();
        }

        private void DataGridLoadLayout_Load(object sender, EventArgs e)
        {
            foreach (string f in Directory.GetFiles(SettingsClass.LayoutsPath))
            {
                string[] path = f.Split('\\');
                listBoxLayouts.Items.Add(path[path.Length - 1].Split('.')[0]);
            }
            if (listBoxLayouts.Items.Count > 0)
            {
                buttonLoad.Enabled = true;
                listBoxLayouts.SetSelected(0, true);
            }
            else
                buttonLoad.Enabled = false;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Path.Combine(SettingsClass.LayoutsPath, String.Format("{0}.xml", listBoxLayouts.SelectedItem.ToString())));
            DataTable dt = ds.Tables["GENERAL_INFORMATION"];
            DataTable dtColumns = ds.Tables["COLUMNS"];
            bool has_right = false;
            if (Convert.ToBoolean(dt.Rows[0]["visible_to_others"])) has_right = true;
            else
                if (Convert.ToInt32(dt.Rows[0]["employee_id"]) == SettingsClass.EmployeeId) has_right = true;
            if (!has_right)
            {
                MessageBox.Show(Language.GetMessageBoxText("insuficientRightsForLayout", "You have insufficient rights to use this layout!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
                return;
            }
            else
            {
                if (FormName != dt.Rows[0]["form_name"].ToString())
                {
                    MessageBox.Show(Language.GetMessageBoxText("inpropperFormLayout", "The selected layout is not for the current form/database!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    return;
                }
            }
            LayoutName = listBoxLayouts.SelectedItem.ToString();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void listBoxLayouts_DoubleClick(object sender, EventArgs e)
        {
            buttonLoad_Click(this, null);
        }
    }
}
