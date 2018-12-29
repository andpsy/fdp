using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FDP;
using System.Configuration;
using MySql.Data;

namespace FDP
{
    public partial class ConnectionSettings : UserForm
    {
        public ConnectionSettings()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void ConnectionSettings_Load(object sender, EventArgs e)
        {
            //CommonFunctions.SetFont(this);
            /*
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonSaveConnectionString.Click += new System.EventHandler(this.buttonSaveConnectionString_Click);
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            */
            this.userTextBoxPort.Validating += new System.ComponentModel.CancelEventHandler(userTextBoxPort_Validating);
            this.userTextBoxDatabase.Validating += new System.ComponentModel.CancelEventHandler(userTextBoxDatabase_Validating);
            userTextBoxPassowrd.PasswordChar = '*';
            
            //Language.LoadLabels(this);

            string connection_string = ConfigurationManager.ConnectionStrings["fdpConnectionString"].ConnectionString;
            string[] connection_string_values = connection_string.Split(';');
            userTextBoxServer.Text = (connection_string_values[0].Split('='))[1];
            userTextBoxPort.Text = (connection_string_values[1].Split('='))[1];
            userTextBoxUser.Text = (connection_string_values[2].Split('='))[1];
            userTextBoxPassowrd.Text = (connection_string_values[3].Split('='))[1];
            userTextBoxDatabase.Text = (connection_string_values[4].Split('='))[1];
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSaveConnectionString_Click(object sender, EventArgs e)
        {
            if (errorProvider.GetError(userTextBoxPort) != "" || errorProvider.GetError(userTextBoxDatabase) != "")
            {
                MessageBox.Show(Language.GetMessageBoxText("formHasErrors", "There are errors that prevent data from being saved! Please corect the errors first!"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    ConnectionStringSettings old_connection_string = config.ConnectionStrings.ConnectionStrings["fdpConnectionString"];
                    //config.ConnectionStrings.ConnectionStrings.Remove(old_connection_string);
                    string new_conn_string = String.Format("server={0};port={1};user id={2};password={3};database={4};Persist Security Info=True;", userTextBoxServer.Text, userTextBoxPort.Text, userTextBoxUser.Text, userTextBoxPassowrd.Text, userTextBoxDatabase.Text);
                    ConnectionStringSettings new_connection_string = new ConnectionStringSettings("fdpConnectionString", new_conn_string, "MySql.Data.MySqlClient");
                    //config.ConnectionStrings.ConnectionStrings.Add(new_connection_string);
                    //config.Save(ConfigurationSaveMode.Full, true);
                    //ConfigurationManager.RefreshSection("connectionStrings");

                    ConnectionStringsSection connectionStringsSection = config.ConnectionStrings;
                    connectionStringsSection.ConnectionStrings.Remove(old_connection_string);
                    connectionStringsSection.ConnectionStrings.Add(new_connection_string);
                    config.Save(ConfigurationSaveMode.Full, true);
                    ConfigurationManager.RefreshSection("connectionStrings");
                    MessageBox.Show(Language.GetMessageBoxText("closeApplicationFirst", "You must close and reopen the application to apply the changes!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorSavingSettings", "Error saving configuration settings! \n{0}"), exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection mysql_conn = new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings["fdpConnectionString"].ConnectionString);
            try
            {
                mysql_conn.Open();
                mysql_conn.Close();
                MessageBox.Show(Language.GetMessageBoxText("connectionSuccessful", "Connection successful!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorConnectingToServer", "Connection to server could not be established! \n{0}"), exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void userTextBoxPort_Validating(object sender, EventArgs e)
        {
            if(!Validator.IsInteger( ((TextBox)sender).Text))
            {
                errorProvider.SetError(userTextBoxPort, Language.GetErrorText("valueNotInteger", "The value you have entered is not valid! It should be integer."));
            }
            else
            {
                errorProvider.SetError(userTextBoxPort, "");
            }
        }

        private void userTextBoxDatabase_Validating(object sender, EventArgs e)
        {
            //errorProvider.SetError(userTextBoxDatabase, Language.GetErrorText("valueNotInteger", "The value you have entered is not valid! It should be integer."));
        }        
    }
}
