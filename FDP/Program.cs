using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.IO;

namespace FDP
{
    static class Program
    {
        //public static DataSet language = new DataSet("language");
        public static Dictionary<string, Dictionary<string, string>> language_dictionary = new Dictionary<string, Dictionary<string,string>>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //var Test = new Test();
                //Test.ShowDialog();

                try
                {
                    string language_file = Path.Combine(Path.Combine(Application.StartupPath, "languages"), ConfigurationManager.AppSettings["language"].ToString() + ".xml");
                    language_dictionary = Language.Populate(language_file);
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Error reading language file!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    string errors_log_file = Path.Combine(Path.Combine(Application.StartupPath, "logs"), "Errors.log");
                    LogWriter.Log(exp.Message, errors_log_file);
                }
                if (args != null && args.Length > 0 && args[0].Trim().ToLower().IndexOf("a") > -1)
                {
                    var f = new FDP_Client_Admin.main();
                    f.ShowDialog();
                    f.Dispose();
                }
                else
                {
                    CompanySelect companies = new CompanySelect();
                    companies.StartPosition = FormStartPosition.CenterScreen;
                    companies.Visible = false;
                    if (companies.ShowDialog() == DialogResult.OK)
                    {
                        Login fLogin = new Login();
                        fLogin.Visible = false;
                        if (fLogin.ShowDialog() == DialogResult.OK)
                        {
                            Application.Run(new main());
                            fLogin.Dispose();
                        }
                        companies.Dispose();
                    }
                    else
                    {
                        Application.Exit();
                    }

                    /*
                    Login fLogin = new Login();
                    if (fLogin.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new main());
                        fLogin.Dispose();
                    }
                    else
                    {
                        Application.Exit();
                    }
                    */
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(String.Format("General application error!\n{0}", exp.ToString()));
                Application.Exit();
            }

        }
    }
}
