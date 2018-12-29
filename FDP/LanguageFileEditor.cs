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
    public partial class LanguageFileEditor : UserForm
    {
        public DataSet language_sections = new DataSet();
        string language_file = "";

        public LanguageFileEditor()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LanguageFileEditor_Load(object sender, EventArgs e)
        {
            //Language.LoadLabels(this);
            string language_path = Path.Combine(Application.StartupPath, "languages");
            DirectoryInfo dinfo = new DirectoryInfo(language_path);
            FileInfo[] Files = dinfo.GetFiles("*.xml");
            foreach (FileInfo file in Files)
            {
                comboBoxLanguageFiles.Items.Add(file.Name.Replace(".xml",""));
            }
            //PopulateGrids(comboBoxLanguageFiles.SelectedItem.ToString());
        }

        private void LanguageFileEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(language_sections.HasChanges())
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmExistWithoutSaving", "There are unsaved changes! Do you wish to close without saving?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(ans != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxLanguageFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateGrids(((ComboBox)sender).SelectedItem.ToString());
        }

        private void PopulateGrids(string language_file_name)
        {
            language_file = Path.Combine(Path.Combine(Application.StartupPath, "languages"), String.Format("{0}.xml", language_file_name));
            language_sections.ReadXml(language_file);
            dataGridViewLabels.DataSource = language_sections.Tables["Labels"];
            dataGridViewMessageBoxes.DataSource = language_sections.Tables["MessageBoxes"];
            dataGridViewErrorMessages.DataSource = language_sections.Tables["ErrorMessages"];
            dataGridViewColumnHeaders.DataSource = language_sections.Tables["ColumnHeaders"];
            dataGridViewServices.DataSource = language_sections.Tables["Services"];
        }

        private void buttonSaveLanguageFile_Click(object sender, EventArgs e)
        {
            try
            {
                language_sections.AcceptChanges();
                language_sections.WriteXml(language_file);
                MessageBox.Show(Language.GetMessageBoxText("closeApplicationFirst", "You must close and reopen the application to apply the changes!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Close();
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(Language.GetMessageBoxText("errorSavingLanguageFile", "There was an error saving the language file!"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
