using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace FDP
{
    public partial class BackupDB : UserForm
    {
        public BackupDB()
        {
            InitializeComponent();
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            string tmp_db_dump_file_name = Path.Combine(SettingsClass.PDFExportPath, String.Format("db_{0}", DateTime.Now.ToString("dd_MM_yy_hh_mm")));
            try
            {
                MySqlBackup mb = new MySqlBackup(SettingsClass.ConnectionString.Replace("fdp_companies", "fdp_0002"));
                mb.ExportInfo.FileName = tmp_db_dump_file_name;
                mb.ExportInfo.RecordDumpTime = true;
                mb.ExportInfo.AddCreateDatabase = true;
                mb.ExportInfo.AsynchronousMode = false;
                mb.ExportInfo.AutoCloseConnection = true;
                mb.ExportInfo.CalculateTotalRowsFromDatabase = true;
                mb.ExportInfo.EnableEncryption = false;
                mb.ExportInfo.EncryptionKey = "";
                mb.ExportInfo.ExportEvents = true;
                mb.ExportInfo.ExportFunctions = true;
                mb.ExportInfo.ExportRows = true;
                mb.ExportInfo.ExportStoredProcedures = true;
                mb.ExportInfo.ExportTableStructure = true;
                mb.ExportInfo.ExportTriggers = true;
                mb.ExportInfo.ExportViews = true;
                mb.ExportInfo.ResetAutoIncrement = false;
                //mb.ExportInfo.MaxSqlLength = (int)nmSQLLength.Value;
                //mb.ExportInfo.TableCustomSql = GetTableSql();
                mb.ExportInfo.ZipOutputFile = false;
                mb.Export();
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
            try
            {
                var fileContents = System.IO.File.ReadAllText(tmp_db_dump_file_name);

                fileContents = fileContents.Replace("fdp_0002", "rtermo93_fdp_0002");
                fileContents = fileContents.Replace("`root`@", "`rtermo93`@");

                System.IO.File.WriteAllText(tmp_db_dump_file_name, fileContents);

                MySqlBackup mb = new MySqlBackup(SettingsClass.RemoteConnectionString);
                //MySqlBackup mb = new MySqlBackup(SettingsClass.ConnectionString.Replace("fdp_companies", "fdp_0002"));
                mb.ImportInfo.AsynchronousMode = false;
                mb.ImportInfo.AutoCloseConnection = true;
                mb.ImportInfo.EnableEncryption = false;
                mb.ImportInfo.EncryptionKey = "";
                mb.ImportInfo.FileName = tmp_db_dump_file_name;
                mb.ImportInfo.IgnoreSqlError = true;
                /*
                string newCharSet = "";
                if (cbCharSet.SelectedIndex > 0)
                    newCharSet = cbCharSet.Text;
                */
                //mb.ImportInfo.SetTargetDatabase(txtTargetDatabase.Text, newCharSet);
                mb.Import();
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }

        }
    }
}
