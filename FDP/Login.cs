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
    public partial class Login : UserForm
    {
        private int AttemptsLeft;
        private bool AutoLogin;
        private bool RememberName;
        private string UserName;
        //private string Password;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            #region --- old ---
            /*
            if (Boolean.Parse(ConfigurationManager.AppSettings["autologin"].ToString()))
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "LOGINsp", new object[]{
                    new MySqlParameter("_NAME", ConfigurationManager.AppSettings["autoname"].ToString()), new MySqlParameter("_PASSWORD", ConfigurationManager.AppSettings["autopassword"].ToString())});
                object returned = da.ExecuteScalarQuery();
                if (returned != null && Convert.ToInt32(returned) > 0)
                {
                    SettingsClass.EmployeeId = Convert.ToInt32(returned);
                    Rights.InitializeRights(Convert.ToInt32(returned));
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Language.GetMessageBoxText("invalidLogin", "Invalid username or password!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            AutoLogin = checkBoxAutoLogin.Checked = Boolean.Parse(ConfigurationManager.AppSettings["autologin"].ToString());
            RememberName = checkBoxRememberName.Checked = Boolean.Parse(ConfigurationManager.AppSettings["remembername"].ToString());
            if (checkBoxRememberName.Checked || checkBoxAutoLogin.Checked)
            {
                UserName = userTextBoxName.Text = Convert.ToString(ConfigurationManager.AppSettings["autoname"]);
                userTextBoxPassword.Focus();
            }
            AttemptsLeft = 3;
            toolStripStatusLabelAttemptsLeft.Text = String.Format(Language.GetLabelText("toolStripStatusLabelAttemptsLeft", "Attempts left: {0}"), AttemptsLeft);
            */
            /*
            if (SettingsClass.Autologin)
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "LOGINsp", new object[]{
                    new MySqlParameter("_NAME", SettingsClass.AutoName), new MySqlParameter("_PASSWORD", SettingsClass.AutoPassword)});
                object returned = da.ExecuteScalarQuery();
                if (returned != null && Convert.ToInt32(returned) > 0)
                {
                    SettingsClass.EmployeeId = Convert.ToInt32(returned);
                    Rights.InitializeRights(Convert.ToInt32(returned));
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Language.GetMessageBoxText("invalidLogin", "Invalid username or password!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            AutoLogin = checkBoxAutoLogin.Checked = SettingsClass.Autologin;
            RememberName = checkBoxRememberName.Checked = SettingsClass.RememberName;
            if (checkBoxRememberName.Checked || checkBoxAutoLogin.Checked)
            {
                UserName = userTextBoxName.Text = SettingsClass.AutoName;
                userTextBoxPassword.Focus();
            }

            AttemptsLeft = 3;
            toolStripStatusLabelAttemptsLeft.Text = String.Format(Language.GetLabelText("toolStripStatusLabelAttemptsLeft", "Attempts left: {0}"), AttemptsLeft);
            */
            #endregion

            if (SettingsClass.Autologin)
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "LOGINsp", new object[]{
                    new MySqlParameter("_NAME", SettingsClass.AutoName), new MySqlParameter("_PASSWORD", SettingsClass.AutoPassword)});
                object returned = da.ExecuteScalarQuery();
                if (returned != null && Convert.ToInt32(returned) > 0)
                {
                    SettingsClass.EmployeeId = Convert.ToInt32(returned);
                    Rights.InitializeRights(Convert.ToInt32(returned));
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Language.GetMessageBoxText("invalidLogin", "Invalid username or password!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            AutoLogin = checkBoxAutoLogin.Checked = SettingsClass.Autologin;
            RememberName = checkBoxRememberName.Checked = SettingsClass.RememberName;
            if (checkBoxRememberName.Checked || checkBoxAutoLogin.Checked)
            {
                UserName = userTextBoxName.Text = SettingsClass.AutoName;
                userTextBoxPassword.Focus();
            }
            AttemptsLeft = 3;
            toolStripStatusLabelAttemptsLeft.Text = String.Format(Language.GetLabelText("toolStripStatusLabelAttemptsLeft", "Attempts left: {0}"), AttemptsLeft);

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmExit", "Are you sure you want to exit?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ans == DialogResult.Yes)
                Application.Exit();
            else
            {
                return;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region --- old ---
            /*
            if (AutoLogin != checkBoxAutoLogin.Checked || RememberName != checkBoxRememberName.Checked || (UserName != userTextBoxName.Text && (checkBoxRememberName.Checked || checkBoxAutoLogin.Checked)))
            {
                try
                {
                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["autologin"].Value = checkBoxAutoLogin.Checked.ToString().ToLower();
                    config.AppSettings.Settings["remembername"].Value = checkBoxRememberName.Checked.ToString().ToLower();
                    config.AppSettings.Settings["autoname"].Value = userTextBoxName.Text;
                    config.AppSettings.Settings["autopassword"].Value = userTextBoxPassword.Text;

                    config.Save(ConfigurationSaveMode.Modified, true);
                    ConfigurationManager.RefreshSection("appSettings");
                    MessageBox.Show(Language.GetMessageBoxText("closeApplicationFirst", "You must close and reopen the application to apply the changes!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorSavingSettings", "Error saving configuration settings! \n{0}"), exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            */
            #endregion

            MD5 md5Hash = MD5.Create();
            string hash = CommonFunctions.GetMd5Hash(md5Hash, userTextBoxPassword.Text.Trim());
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "LOGINsp", new object[]{
                new MySqlParameter("_NAME", userTextBoxName.Text.Trim()), new MySqlParameter("_PASSWORD", hash)});
            object returned = da.ExecuteScalarQuery();
            //this.DialogResult = DialogResult.Cancel;
            bool is_owner = false;
            if (returned == null || !(returned is UInt32 || returned is Int32 || returned is int))
            {
                da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_LOGIN", new object[]{
                new MySqlParameter("_USER_NAME", userTextBoxName.Text.Trim()), new MySqlParameter("_PASSWORD", hash)});
                is_owner = true;
                returned = da.ExecuteScalarQuery();
            }
            if (returned == null)
            {
                userTextBoxName.Text = "";
                userTextBoxPassword.Text = "";
                AttemptsLeft--;
                toolStripStatusLabelAttemptsLeft.Text = String.Format(Language.GetLabelText("toolStripStatusLabelAttemptsLeft", "Attempts left: {0}"), AttemptsLeft);
                MessageBox.Show(Language.GetMessageBoxText("invalidLogin", "Invalid username or password!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                if (AttemptsLeft == 0) Application.Exit();
                //return;
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(returned) > 0)
                    {
                        SettingsClass.UserName = userTextBoxName.Text;
                        if (!is_owner)
                        {
                            SettingsClass.EmployeeId = Convert.ToInt32(returned);
                            Rights.InitializeRights(Convert.ToInt32(returned));
                        }
                        if(is_owner)
                            SettingsClass.LoginOwnerId = Convert.ToInt32(returned);

                        if (AutoLogin != checkBoxAutoLogin.Checked || RememberName != checkBoxRememberName.Checked || (UserName != userTextBoxName.Text && (checkBoxRememberName.Checked || checkBoxAutoLogin.Checked)))
                        {
                            try
                            {
                                /*
                                SettingsClass.Autologin = checkBoxAutoLogin.Checked;
                                SettingsClass.RememberName = checkBoxRememberName.Checked;
                                SettingsClass.AutoName = userTextBoxName.Text;
                                SettingsClass.AutoPassword = userTextBoxPassword.Text;
                                */
                                MD5 md5Hash1 = MD5.Create();
                                /* --- FROM 21.08.2013 ---
                                string key_value = String.Format("autologin={0};remembername={1};autoname={2};autopassword={3}", checkBoxAutoLogin.Checked.ToString(), checkBoxRememberName.Checked.ToString(), userTextBoxName.Text, CommonFunctions.GetMd5Hash(md5Hash1, userTextBoxPassword.Text.Trim()));
                                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                                try
                                {
                                    config.AppSettings.Settings.Remove(SettingsClass.CompanyId.ToString());
                                }
                                catch { }
                                config.AppSettings.Settings.Add(SettingsClass.CompanyId.ToString(), key_value);
                                config.Save(ConfigurationSaveMode.Modified, true);
                                */
                                DataTable clone = SettingsClass.Settings().Copy();
                                DataRow dr = null;
                                try
                                {
                                    //dr = SettingsClass.Settings().Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0];
                                    dr = clone.Select(String.Format("id={0}", SettingsClass.CompanyId.ToString()))[0];
                                }
                                catch
                                {
                                    /* --- ONLY FOR LOADING SETTINGS DATATABLE IN THE BEGINNING ---
                                    DataTable dt = new DataTable("SETTINGS");
                                    DataColumn dc = new DataColumn("id", Type.GetType("System.Int32"));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("autologin", Type.GetType("System.Boolean"));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("remembername", Type.GetType("System.Boolean"));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("autoname", Type.GetType("System.String"));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("autopassword", Type.GetType("System.String"));
                                    dt.Columns.Add(dc);
                                    dt.AcceptChanges();
                                    dr = dt.NewRow();
                                    dr["id"] = SettingsClass.CompanyId;
                                    dr["autologin"] = checkBoxAutoLogin.Checked;
                                    dr["remembername"] = checkBoxRememberName.Checked;
                                    dr["autoname"] = userTextBoxName.Text;
                                    dr["autopassword"] = CommonFunctions.GetMd5Hash(md5Hash1, userTextBoxPassword.Text.Trim());
                                    dt.Rows.Add(dr);
                                    dt.AcceptChanges();
                                    dt.WriteXml(Path.Combine(SettingsClass.SettingsFilesPath, "settings.xml"));
                                    */
                                    dr = SettingsClass.Settings().NewRow();
                                }
                                dr["id"] = SettingsClass.CompanyId;
                                dr["autologin"] = checkBoxAutoLogin.Checked;
                                dr["remembername"] = checkBoxRememberName.Checked;
                                dr["autoname"] = userTextBoxName.Text;
                                dr["autopassword"] = CommonFunctions.GetMd5Hash(md5Hash1, userTextBoxPassword.Text.Trim());
                                if (dr.RowState == DataRowState.Detached)
                                {
                                    //SettingsClass.Settings().Rows.Add(dr);
                                    clone.Rows.Add(dr);
                                }
                                //SettingsClass.Settings().DataSet.AcceptChanges();
                                clone.AcceptChanges();
                                //SettingsClass.Settings().DataSet.WriteXml(Path.Combine(SettingsClass.SettingsFilesPath, "settings.xml"), XmlWriteMode.IgnoreSchema);
                                clone.WriteXml(Path.Combine(SettingsClass.SettingsFilesPath, "settings.xml"), XmlWriteMode.IgnoreSchema);
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
                        userTextBoxName.Text = "";
                        userTextBoxPassword.Text = "";
                        AttemptsLeft--;
                        toolStripStatusLabelAttemptsLeft.Text = String.Format(Language.GetLabelText("toolStripStatusLabelAttemptsLeft", "Attempts left: {0}"), AttemptsLeft);
                        MessageBox.Show(Language.GetMessageBoxText("invalidLogin", "Invalid username or password!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        if (AttemptsLeft == 0) Application.Exit();
                        //return;
                    }
                }
                catch(Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    userTextBoxName.Text = "";
                    userTextBoxPassword.Text = "";
                    AttemptsLeft--;
                    toolStripStatusLabelAttemptsLeft.Text = String.Format(Language.GetLabelText("toolStripStatusLabelAttemptsLeft", "Attempts left: {0}"), AttemptsLeft);
                    MessageBox.Show(Language.GetMessageBoxText("invalidLogin", "Invalid username or password!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    if (AttemptsLeft == 0) Application.Exit();
                    //return;
                }
            }
        }

        private void userTextBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnLogin_Click(null, null);
                return;
            }

        }
    }
}
