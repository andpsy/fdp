using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Management;
using System.IO;
using System.Diagnostics;

namespace FDP_Admin
{
    public partial class main : UserForm
    {
        public main()
        {
            InitializeComponent();
            tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
        }

        private void tabControl1_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.WhiteSmoke, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", (float)10.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void main_Load(object sender, EventArgs e)
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

        private void buttonSaveConnectionString_Click(object sender, EventArgs e)
        {
            if (errorProvider.GetError(userTextBoxPort) != "" || errorProvider.GetError(userTextBoxDatabase) != "")
            {
                MessageBox.Show("There are errors that prevent data from being saved! Please corect the errors first!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    config.Save(ConfigurationSaveMode.Modified, true);
                    ConfigurationManager.RefreshSection("connectionStrings");
                    MessageBox.Show("You must close and reopen the application to apply the changes!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format("Error saving configuration settings! \n{0}", exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            //MySql.Data.MySqlClient.MySqlConnection mysql_conn = new MySql.Data.MySqlClient.MySqlConnection(ConfigurationManager.ConnectionStrings["fdpConnectionString"].ConnectionString);
            MySql.Data.MySqlClient.MySqlConnection mysql_conn = new MySql.Data.MySqlClient.MySqlConnection();
            mysql_conn.ConnectionString = String.Format("server={0};port={1};user id={2};password={3};database={4};Persist Security Info=True;", userTextBoxServer.Text, userTextBoxPort.Text, userTextBoxUser.Text, userTextBoxPassowrd.Text, userTextBoxDatabase.Text);
            try
            {
                mysql_conn.Open();
                mysql_conn.Close();
                MessageBox.Show("Connection successful!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show(String.Format("Connection to server could not be established! \n{0}", exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void userTextBoxPort_Validating(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(((TextBox)sender).Text);
                errorProvider.SetError(userTextBoxPort, "");
            }
            catch
            {
                errorProvider.SetError(userTextBoxPort, "The value you have entered is not valid! It should be integer.");
            }
        }

        private void userTextBoxDatabase_Validating(object sender, EventArgs e)
        {
            //errorProvider.SetError(userTextBoxDatabase, Language.GetErrorText("valueNotInteger", "The value you have entered is not valid! It should be integer."));
        }

        private void buttonSaveMasterPassword_Click(object sender, EventArgs e)
        {
            errorProvider.SetError(userTextBoxNewPassword, "");
            errorProvider.SetError(userTextBoxConfirmNewPassword, "");
            MD5 md5Hash = MD5.Create();
            string hash = CommonFunctions.GetMd5Hash(md5Hash, userTextBoxOldPassword.Text.Trim());
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_get_value", new object[] { new MySqlParameter("_NAME", "MASTER PASSWORD") });
            object returned = da.ExecuteScalarQuery();
            if ((returned == null && userTextBoxOldPassword.Text == "") || hash == returned.ToString())
            {
                if (userTextBoxNewPassword.Text.Trim() == "")
                {
                    MessageBox.Show("New Password cannot be empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (userTextBoxNewPassword.Text != userTextBoxConfirmNewPassword.Text)
                {
                    MessageBox.Show("Passwords do not match!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                try
                {
                    da = new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_delete_by_name", new object[] { new MySqlParameter("_NAME", "MASTER PASSWORD") });
                    da.ExecuteUpdateQuery();
                }
                catch { }
                try
                {
                    da = new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_insert", new object[] { new MySqlParameter("_NAME", "MASTER PASSWORD"), new MySqlParameter("_VALUE", CommonFunctions.GetMd5Hash(md5Hash, userTextBoxNewPassword.Text.Trim())) });
                    da.ExecuteInsertQuery();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format("Error updating password!\n {0}", exp.Message));
                }
                MessageBox.Show("Password saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Old Password do not match!", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        private void buttonBackupDB_Click(object sender, EventArgs e)
        {
            ShowLoadingPicture(true);
            try
            {
                ManagementObjectSearcher MyWMIQuery = new ManagementObjectSearcher("SELECT Caption, InstallLocation FROM Win32_Product WHERE Caption LIKE 'MySQL Server %'");
                MyWMIQuery.Scope = new ManagementScope();
                //ManagementObjectSearcher MyWMIQuery = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
                ManagementObjectCollection MyWMIQueryCollection = MyWMIQuery.Get();
                string mysqldump_path = "";

                foreach (ManagementObject MyMO in MyWMIQueryCollection)
                {
                    /*
                    Console.WriteLine("Caption : " + MyMO["Caption"].ToString());
                    Console.WriteLine("Description : " + MyMO["Description"].ToString());
                    Console.WriteLine("InstallDate2 : " + MyMO["InstallDate2"].ToString());
                    //Some installed applications don't return installlocation
                    Console.WriteLine("InstallLocation : " + (MyMO["InstallLocation"] == null ? " " : MyMO["InstallLocation"].ToString()));
                    Console.WriteLine("InstallState : " + MyMO["InstallState"].ToString());
                    Console.WriteLine("Name : " + MyMO["Name"].ToString());
                    Console.WriteLine("PackageCache : " + MyMO["PackageCache"].ToString());
                    //Some installed applications don't return SKUNumber
                    Console.WriteLine("SKUNumber : " + (MyMO["SKUNumber"] == null ? " " : MyMO["SKUNumber"].ToString()));
                    Console.WriteLine("Vendor : " + MyMO["Vendor"].ToString());
                    Console.WriteLine("Version : " + MyMO["Version"].ToString());
                
                    Console.ReadLine();
                    */
                    if (MyMO["Caption"].ToString().ToLower().IndexOf("mysql server") > -1)
                    {
                        //MessageBox.Show(MyMO["InstallLocation"] == null ? " " : MyMO["InstallLocation"].ToString());
                        mysqldump_path = Path.Combine(MyMO["InstallLocation"].ToString(), "bin", "mysqldump.exe");
                        break;
                    }
                }
                //object ret = ExecuteBatchFile(SettingsClass.MySqlToolsBackupBatchFile, new string[] { SettingsClass.ConnectionStringDataBase, SettingsClass.ConnectionStringUser, SettingsClass.ConnectionStringPassword, userTextBoxBackupFile.Text, mysqldump_path });
                object ret = ExecuteBatchFile(SettingsClass.MySqlToolsRestoreBatchFile, new string[] { String.Format("\"{0}\" --add-drop-table -B {1} -u {2} --password={3} --routines --triggers > \"{4}\"", mysqldump_path, SettingsClass.ConnectionStringDataBase, SettingsClass.ConnectionStringUser, SettingsClass.ConnectionStringPassword, userTextBoxBackupFile.Text.Trim()) });
                MyWMIQueryCollection.Dispose();
                MyWMIQuery.Dispose();
                userTextBoxBackupFile.Text = "";
                MessageBox.Show("Database saved successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch {
                MessageBox.Show("Error saving database!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ShowLoadingPicture(false);
        }



        protected object ExecuteBatchFile(string batchFileName, string[] argumentsToBatchFile)
        {
            string argumentsString = string.Empty;
            try
            {
                //Add up all arguments as string with space separator between the arguments
                if (argumentsToBatchFile != null)
                {
                    for (int count = 0; count < argumentsToBatchFile.Length; count++)
                    {
                        argumentsString += " ";
                        argumentsString += argumentsToBatchFile[count];
                        //argumentsString += "\"";
                    }
                }
                //MessageBox.Show(String.Format("\"{0}\" {1}", batchFileName, argumentsString));
                //Create process start information
                System.Diagnostics.ProcessStartInfo DBProcessStartInfo = new System.Diagnostics.ProcessStartInfo(batchFileName, argumentsString.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", ""));

                //Redirect the output to standard window
                DBProcessStartInfo.RedirectStandardOutput = true;

                //The output display window need not be falshed onto the front.
                DBProcessStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                DBProcessStartInfo.UseShellExecute = false;
                DBProcessStartInfo.RedirectStandardError = true;


                //Create the process and run it
                System.Diagnostics.Process dbProcess;
                dbProcess = System.Diagnostics.Process.Start(DBProcessStartInfo);

                //Catch the output text from the console so that if error happens, the output text can be logged.
                System.IO.StreamReader standardOutput = dbProcess.StandardOutput;
                System.IO.StreamReader errorOutput = dbProcess.StandardError;

                /* Wait as long as the DB Backup or Restore or Repair is going on. 
                Ping once in every 2 seconds to check whether process is completed. */
                while (!dbProcess.HasExited)
                {
                    dbProcess.WaitForExit(500);
                    pictureBox1.Show();
                }

                if (dbProcess.HasExited)
                {
                    string consoleOutputText = standardOutput.ReadToEnd();
                    string consoleErrorText = errorOutput.ReadToEnd();
                    //TODO - log consoleOutputText to the log file.
                    string logFile = Path.Combine(SettingsClass.MySqlToolsPath, (userTextBoxRestoreFile.Text.Trim()==""?userTextBoxBackupFile.Text:userTextBoxRestoreFile.Text).Replace(".sql", String.Format("_{0}.log", DateTime.Now.ToString("ddMMyyyy"))));
                    TextWriter tw = new StreamWriter(logFile);

                    // write a line of text to the file
                    tw.WriteLine(consoleOutputText);
                    tw.WriteLine(consoleErrorText);

                    // close the stream
                    tw.Close();

                }

                return true;
            }
            // Catch the SQL exception and throw the customized exception made out of that
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        private void pictureBoxSelectFile_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.InitialDirectory = SettingsClass.MySqlBackupPath;
            this.saveFileDialog1.ShowDialog();
            if (this.saveFileDialog1.FileName != "")
            {
                userTextBoxBackupFile.Text = saveFileDialog1.FileName;
            }           
        }

        private void pictureBoxSelectRestoreFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.InitialDirectory = SettingsClass.MySqlBackupPath;
            openFileDialog1.FileName = "";
            this.openFileDialog1.ShowDialog();
            if (this.openFileDialog1.FileName != "")
            {
                userTextBoxRestoreFile.Text = openFileDialog1.FileName;
            }          
        }

        private void buttonRestoreDB_Click(object sender, EventArgs e)
        {
            ShowLoadingPicture(true);
            try
            {
                ManagementObjectSearcher MyWMIQuery = new ManagementObjectSearcher("SELECT Caption, InstallLocation FROM Win32_Product WHERE Caption LIKE 'MySQL Server %'");
                MyWMIQuery.Scope = new ManagementScope();
                //ManagementObjectSearcher MyWMIQuery = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
                ManagementObjectCollection MyWMIQueryCollection = MyWMIQuery.Get();
                string mysql_path = "";

                foreach (ManagementObject MyMO in MyWMIQueryCollection)
                {
                    /*
                    Console.WriteLine("Caption : " + MyMO["Caption"].ToString());
                    Console.WriteLine("Description : " + MyMO["Description"].ToString());
                    Console.WriteLine("InstallDate2 : " + MyMO["InstallDate2"].ToString());
                    //Some installed applications don't return installlocation
                    Console.WriteLine("InstallLocation : " + (MyMO["InstallLocation"] == null ? " " : MyMO["InstallLocation"].ToString()));
                    Console.WriteLine("InstallState : " + MyMO["InstallState"].ToString());
                    Console.WriteLine("Name : " + MyMO["Name"].ToString());
                    Console.WriteLine("PackageCache : " + MyMO["PackageCache"].ToString());
                    //Some installed applications don't return SKUNumber
                    Console.WriteLine("SKUNumber : " + (MyMO["SKUNumber"] == null ? " " : MyMO["SKUNumber"].ToString()));
                    Console.WriteLine("Vendor : " + MyMO["Vendor"].ToString());
                    Console.WriteLine("Version : " + MyMO["Version"].ToString());
                
                    Console.ReadLine();
                    */
                    if (MyMO["Caption"].ToString().ToLower().IndexOf("mysql server") > -1)
                    {
                        //MessageBox.Show(MyMO["InstallLocation"] == null ? " " : MyMO["InstallLocation"].ToString());
                        mysql_path = Path.Combine(MyMO["InstallLocation"].ToString(), "bin", "mysql.exe");
                        break;
                    }
                }
                object ret = ExecuteBatchFile(SettingsClass.MySqlToolsRestoreBatchFile, new string[] { String.Format("\"{0}\" -u {1} --password={2} < \"{3}\"", mysql_path, SettingsClass.ConnectionStringUser, SettingsClass.ConnectionStringPassword, userTextBoxRestoreFile.Text.Trim()) });

                MyWMIQueryCollection.Dispose();
                MyWMIQuery.Dispose();
                userTextBoxRestoreFile.Text = "";
                MessageBox.Show("Database restored successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch {
                MessageBox.Show("Error restoring database!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);           
            }
            ShowLoadingPicture(false);
        }

        private void ShowLoadingPicture(bool visibility)
        {
            this.pictureBox1.Location = new System.Drawing.Point(this.Width/2-pictureBox1.Width/2, this.Height/2-pictureBox1.Height/2);
            pictureBox1.BringToFront();
            pictureBox1.Visible = visibility;
            //pictureBox1.Show();
        }
    }
}
