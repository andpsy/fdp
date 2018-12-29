using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;

namespace FDP
{
    public partial class LoginSettings : UserForm
    {
        //private int AttemptsLeft;
        private bool AutoLogin;
        private bool RememberName;
        //private string UserName;
        //private string Password;

        public LoginSettings()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            AutoLogin = checkBoxAutoLogin.Checked = SettingsClass.Autologin;
            RememberName = checkBoxRememberName.Checked = SettingsClass.RememberName;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            MD5 md5Hash = MD5.Create();
            string hash = CommonFunctions.GetMd5Hash(md5Hash, userTextBoxOldPassword.Text.Trim());
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "LOGINsp", new object[]{
                new MySqlParameter("_NAME", userTextBoxOldUserName.Text.Trim()), new MySqlParameter("_PASSWORD", hash)});
            object returned = da.ExecuteScalarQuery();
            if (returned == null)
            {
                userTextBoxOldUserName.Text = "";
                userTextBoxOldPassword.Text = "";
                MessageBox.Show(Language.GetMessageBoxText("invalidLogin", "Invalid username or password!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(returned) > 0)
                    {
                        SettingsClass.UserName = userTextBoxNewUserName.Text;

                        if (AutoLogin != checkBoxAutoLogin.Checked || RememberName != checkBoxRememberName.Checked || (SettingsClass.UserName != userTextBoxNewUserName.Text && (checkBoxRememberName.Checked || checkBoxAutoLogin.Checked)))
                        {
                            try
                            {
                                MD5 md5Hash1 = MD5.Create();
                                DataTable clone = SettingsClass.Settings().Copy();
                                DataRow dr = null;
                                try
                                {
                                    dr = clone.Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0];
                                }
                                catch
                                {
                                    dr = SettingsClass.Settings().NewRow();
                                }
                                dr["id"] = SettingsClass.CompanyId;
                                dr["autologin"] = checkBoxAutoLogin.Checked;
                                dr["remembername"] = checkBoxRememberName.Checked;
                                dr["autoname"] = userTextBoxNewUserName.Text;
                                dr["autopassword"] = CommonFunctions.GetMd5Hash(md5Hash1, userTextBoxNewPassword.Text.Trim());
                                clone.AcceptChanges();
                                clone.WriteXml(Path.Combine(SettingsClass.SettingsFilesPath, "settings.xml"), XmlWriteMode.IgnoreSchema);

                                da = new DataAccess(CommandType.StoredProcedure, "EMPLOYEESsp_change_password", new object[]{
                                    new MySqlParameter("_ID", SettingsClass.EmployeeId),
                                    new MySqlParameter("_PASSWORD", CommonFunctions.GetMd5Hash(md5Hash1, userTextBoxNewPassword.Text.Trim()))
                                });
                                da.ExecuteUpdateQuery();
                                MessageBox.Show(Language.GetMessageBoxText("closeApplicationFirst", "You must close and reopen the application to apply the changes!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception exp)
                            {
                                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorSavingSettings", "Error saving configuration settings! \n{0}"), exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        userTextBoxOldUserName.Text = "";
                        userTextBoxOldPassword.Text = "";
                    }
                }
                catch(Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    userTextBoxOldUserName.Text = "";
                    userTextBoxOldPassword.Text = "";
                }
            }
        }
    }
}
