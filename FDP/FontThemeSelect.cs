using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace FDP
{
    public partial class FontThemeSelect : UserForm
    {
        public FontThemeSelect()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void FontThemeSelect_Load(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = false;
            userTextBox1.Text = ConfigurationManager.AppSettings["FontTheme"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                userTextBox1.Text = fontDialog1.Font.FontFamily.Name;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSaveFontTheme_Click(object sender, EventArgs e)
        {
            if (userTextBox1.Text.Trim() != "")
            {
                try
                {
                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings.Remove("FontTheme");
                    config.AppSettings.Settings.Add("FontTheme", userTextBox1.Text);
                    config.Save(ConfigurationSaveMode.Modified, true);
                    MessageBox.Show(Language.GetMessageBoxText("closeApplicationFirst", "You must close and reopen the application to apply the changes!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorSavingSettings", "Error saving configuration settings! \n{0}"), exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
