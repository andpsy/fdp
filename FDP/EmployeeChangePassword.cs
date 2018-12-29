using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace FDP
{
    public partial class EmployeeChangePassword : UserForm
    {
        public string table_name;
        public DataRow data_row;
        public int action = 0; // (0 = ADD, 1 = EDIT)
        public int x, y = 10;
        public DataRow return_data_row;


        public EmployeeChangePassword()
        {
            InitializeComponent();
        }

        public EmployeeChangePassword(string table_name, DataRow data_row, int action)
        {
            InitializeComponent();
            this.Padding = new Padding(5, 5, 5, 5);
            this.table_name = table_name;
            this.data_row = data_row;
            this.action = action;
            this.x = 10;
            this.y = 10;
        }


        private void EmployeeChangePassword_Load(object sender, EventArgs e)
        {
            userTextBoxName.Text = data_row["name"].ToString();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonSavePassword_Click(object sender, EventArgs e)
        {
            try
            {
                return_data_row = data_row;
                MD5 md5Hash = MD5.Create();
                string hash = CommonFunctions.GetMd5Hash(md5Hash, userTextBoxPassword.Text.Trim());
                return_data_row["password"] = hash;
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingPassword", "There was an error saving the new password:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
