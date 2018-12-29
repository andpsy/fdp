using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace FDP
{
    public partial class CompanySelect : UserForm
    {
        public CompanySelect()
        {
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.buttonSelect.Click += new EventHandler(buttonSelect_Click);
            dataGrid1.toolStripButtonSelect.Click += new EventHandler(buttonSelect_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
        }

        private void CompanySelect_Load(object sender, EventArgs e)
        {
            //this.dataGrid1 = new FDP.DataGrid("COMPANIESsp_select", null, "COMPANIESsp_insert", null, "COMPANIESsp_update", null, "COMPANIESsp_delete", null, null, null, null, null, null, new string[] { "NAME", "CIF", "CUI" }, false, false); 
            //dataGrid1.buttonSelect.Visible = true;
            dataGrid1.toolStripButtonSelect.Visible = true;
            dataGrid1.AddToolStripButton(Language.GetLabelText("CompanySelect.toolStripButtonDeleteDB", "Delete DB"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "59.png")), new EventHandler(userButtonDropSchema_Click), "toolStripButtonDeleteDB", 5);
            dataGrid1.AddToolStripButton(Language.GetLabelText("CompanySelect.toolStripButtonCreateDB", "Create DB"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "60.png")), new EventHandler(userButtonCreateDatabase_Click), "toolStripButtonCreateDB", 6);
            dataGrid1.AddToolStripButton(Language.GetLabelText("CompanySelect.toolStripButtonSettings", "Settings"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "21.png")), new EventHandler(userButtonSettings_Click), "toolStripButtonSettings", 7);
            base.Opacity = 100;
        }

        public void buttonSelect_Click(object sender, EventArgs e)
        {
            if (dataGrid1.dataGridView.SelectedRows.Count > 0)
            {
                try
                {
                    string company_id = String.Format("fdp_{0}", dataGrid1.dataGridView[dataGrid1.dataGridView.Columns["company_counter"].Index, dataGrid1.dataGridView.SelectedRows[0].Index].Value.ToString().PadLeft(4, '0'));
                    SettingsClass.ConnectionStringCompany = SettingsClass.ConnectionString.Replace("database=fdp_companies", String.Format("database={0}", company_id));
                    SettingsClass.CompanyId = Convert.ToInt32(dataGrid1.dataGridView[dataGrid1.dataGridView.Columns["id"].Index, dataGrid1.dataGridView.SelectedRows[0].Index].Value);
                    this.DialogResult = DialogResult.OK;
                }
                catch { this.DialogResult = DialogResult.Cancel; }
            }
        }

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            }
            catch { }
            Application.DoEvents();
            Prompt p = new Prompt(Language.GetLabelText("Prompt.masterPassword", "Input Master Password"), true);
            if (p.ShowDialog() == DialogResult.OK)
            {
                if (p.userTextBoxPrompt.Text.Trim() != "")
                {
                    MD5 md5Hash = MD5.Create();
                    string hash = CommonFunctions.GetMd5Hash(md5Hash, p.userTextBoxPrompt.Text.Trim());
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_get_value", new object[] { new MySqlParameter("_NAME", "MASTER PASSWORD") });
                    object returned = da.ExecuteScalarQuery();
                    if (hash == returned.ToString())
                    {
                        DataRow company = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
                        var f = new Companies(company);
                        //f.TopLevel = false;
                        //f.MdiParent = this.ParentForm;
                        //((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
                        //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        //f.Dock = DockStyle.Fill;
                        //f.BringToFront();
                        //f.StartPosition = FormStartPosition.CenterParent;
                        //f.StartPosition = FormStartPosition.Manual;
                        //f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
                        f.StartPosition = FormStartPosition.CenterScreen;
                        f.ShowDialog();
                        if (f.DialogResult == DialogResult.OK)
                        {
                            try
                            {
                                company = f.NewDR;
                                object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(company.Table, company.ItemArray, 0);
                                ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(company);
                                dataGrid1.da.AttachInsertParams(mySqlParams);
                                dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                                ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                                try
                                {
                                    FileInfo file = new FileInfo(SettingsClass.DbTemplateFile);
                                    string script = file.OpenText().ReadToEnd();
                                    script = script.Replace("<<fdp_database_name>>", String.Format("fdp_{0}", company["company_counter"].ToString().PadLeft(4, '0')));
                                    da = new DataAccess(script);
                                    da.ExecuteScript(script);
                                    file.OpenText().Close();

                                    var fe = new Employees();
                                    fe.ShowDialog();
                                }
                                catch (Exception exp) 
                                {
                                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorCreatingDatabase", "There was an error creating the database structure:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception exp)
                            {
                                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            ((DataTable)dataGrid1.da.bindingSource.DataSource).RejectChanges();
                        }
                        f.Dispose();
                    }
                }
            }
        }

        public void buttonEdit_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            Prompt p = new Prompt(Language.GetLabelText("Prompt.masterPassword", "Input Master Password"), true);
            if (p.ShowDialog() == DialogResult.OK)
            {
                if (p.userTextBoxPrompt.Text.Trim() != "")
                {
                    MD5 md5Hash = MD5.Create();
                    string hash = CommonFunctions.GetMd5Hash(md5Hash, p.userTextBoxPrompt.Text.Trim());
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_get_value", new object[] { new MySqlParameter("_NAME", "MASTER PASSWORD") });
                    object returned = da.ExecuteScalarQuery();
                    if (hash == returned.ToString())
                    {
                        DataRow company = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
                        var f = new Companies(company);
                        //f.TopLevel = false;
                        //f.MdiParent = this.ParentForm;
                        //((main)this.ParentForm).splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
                        //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        //f.Dock = DockStyle.Fill;
                        //f.BringToFront();
                        //f.StartPosition = FormStartPosition.CenterParent;
                        //f.StartPosition = FormStartPosition.Manual;
                        //f.Location = new Point(((main)this.ParentForm).splitContainerMain.Panel2.Width / 2 - f.Width / 2, ((main)this.ParentForm).splitContainerMain.Panel2.Height / 2 - f.Height / 2);
                        f.StartPosition = FormStartPosition.CenterScreen;
                        f.ShowDialog();
                        if (f.DialogResult == DialogResult.OK)
                        {
                            try
                            {
                                company = f.NewDR;
                                object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(company.Table, company.ItemArray, 1);
                                dataGrid1.da.AttachUpdateParams(mySqlParams);
                                dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                                ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                                if (f.RecreateSchema)
                                {
                                    string database_name = String.Format("fdp_{0}", company["company_counter"].ToString().PadLeft(4, '0'));
                                    try
                                    {
                                    string drop_schema_script = String.Format("DROP DATABASE {0};", database_name);
                                    da = new DataAccess(drop_schema_script);
                                    da.ExecuteScript(drop_schema_script);
                                    }
                                    catch (Exception exp)
                                    {
                                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingDatabase", "There was an error removing the database structure:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    try
                                    {
                                    FileInfo file = new FileInfo(SettingsClass.DbTemplateFile);
                                    string create_schema_script = file.OpenText().ReadToEnd();
                                    create_schema_script = create_schema_script.Replace("<<fdp_database_name>>", database_name);
                                    da = new DataAccess(create_schema_script);
                                    da.ExecuteScript(create_schema_script);
                                    file.OpenText().Close();
                                    }
                                    catch (Exception exp)
                                    {
                                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorCreatingDatabase", "There was an error creating the database structure:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            catch (Exception exp)
                            {
                                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            ((DataTable)dataGrid1.da.bindingSource.DataSource).RejectChanges();
                        }
                        f.Dispose();
                    }
                }
            }
        }

        public void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid1.dataGridView.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    Prompt p = new Prompt(Language.GetLabelText("Prompt.masterPassword", "Input Master Password"), true);
                    if (p.ShowDialog() == DialogResult.OK)
                    {
                        if (p.userTextBoxPrompt.Text.Trim() != "")
                        {
                            MD5 md5Hash = MD5.Create();
                            string hash = CommonFunctions.GetMd5Hash(md5Hash, p.userTextBoxPrompt.Text.Trim());
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_get_value", new object[] { new MySqlParameter("_NAME", "MASTER PASSWORD") });
                            object returned = da.ExecuteScalarQuery();
                            if (hash == returned.ToString())
                            {
                                try
                                {
                                    int key = Convert.ToInt32(dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value);
                                    dataGrid1.da.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value) });
                                    DataRow dr = ((DataTable)dataGrid1.da.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                                    string database_name = String.Format("fdp_{0}", dr["company_counter"].ToString().PadLeft(4, '0'));
                                    try
                                    {
                                        string drop_schema_script = String.Format("DROP DATABASE {0};", database_name);
                                        da = new DataAccess(drop_schema_script);
                                        da.ExecuteScript(drop_schema_script);
                                        dr.Delete();
                                        dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                                        ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                                    }
                                    catch (Exception exp)
                                    {
                                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingDatabase", "There was an error removing the database structure:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                catch (Exception exp)
                                {
                                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGrid1_Load(object sender, EventArgs e)
        {
        }

        private void userButtonDropSchema_Click(object sender, EventArgs e)
        {
            if (dataGrid1.dataGridView.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    Prompt p = new Prompt(Language.GetLabelText("Prompt.masterPassword", "Input Master Password"), true);
                    if (p.ShowDialog() == DialogResult.OK)
                    {
                        if (p.userTextBoxPrompt.Text.Trim() != "")
                        {
                            MD5 md5Hash = MD5.Create();
                            string hash = CommonFunctions.GetMd5Hash(md5Hash, p.userTextBoxPrompt.Text.Trim());
                            DataAccess da = new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_get_value", new object[] { new MySqlParameter("_NAME", "MASTER PASSWORD") });
                            object returned = da.ExecuteScalarQuery();
                            if (hash == returned.ToString())
                            {

                                try
                                {
                                    int key = Convert.ToInt32(dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value);
                                    DataRow dr = ((DataTable)dataGrid1.da.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                                    string database_name = String.Format("fdp_{0}", dr["company_counter"].ToString().PadLeft(4, '0'));
                                    string drop_schema_script = String.Format("DROP DATABASE {0};", database_name);
                                    da = new DataAccess(drop_schema_script);
                                    da.ExecuteScript(drop_schema_script);
                                }
                                catch (Exception exp)
                                {
                                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingDatabase", "There was an error removing the database structure:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                    }
                }
            }
        }

        private void userButtonCreateDatabase_Click(object sender, EventArgs e)
        {
            Prompt p = new Prompt(Language.GetLabelText("Prompt.masterPassword", "Input Master Password"), true);
            if (p.ShowDialog() == DialogResult.OK)
            {
                if (p.userTextBoxPrompt.Text.Trim() != "")
                {
                    MD5 md5Hash = MD5.Create();
                    string hash = CommonFunctions.GetMd5Hash(md5Hash, p.userTextBoxPrompt.Text.Trim());
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "SETTINGSsp_get_value", new object[] { new MySqlParameter("_NAME", "MASTER PASSWORD") });
                    object returned = da.ExecuteScalarQuery();
                    if (hash == returned.ToString())
                    {
                        try
                        {
                            int key = Convert.ToInt32(dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value);
                            DataRow dr = ((DataTable)dataGrid1.da.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];
                            try{
                            string database_name = String.Format("fdp_{0}", dr["company_counter"].ToString().PadLeft(4, '0'));
                            string drop_schema_script = String.Format("DROP DATABASE {0};", database_name);
                            da = new DataAccess(drop_schema_script);
                            da.ExecuteScript(drop_schema_script);
                            }
                            catch (Exception exp)
                            {
                                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingDatabase", "There was an error removing the database structure:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            try
                            {
                                FileInfo file = new FileInfo(SettingsClass.DbTemplateFile);
                                string script = file.OpenText().ReadToEnd();
                                script = script.Replace("<<fdp_database_name>>", String.Format("fdp_{0}", dr["company_counter"].ToString().PadLeft(4, '0')));
                                da = new DataAccess(script);
                                da.ExecuteScript(script);
                                file.OpenText().Close();
                            }
                            catch (Exception exp)
                            {
                                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingDatabase", "There was an error removing the database structure:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception exp)
                        {
                            LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                            MessageBox.Show(String.Format(Language.GetMessageBoxText("errorCreatingDatabase", "There was an error creating the database structure:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }       
        }

        private void userButtonSettings_Click(object sender, EventArgs e)
        {
            CompaniesSettings cs = new CompaniesSettings(Convert.ToInt32(dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value));
            cs.Show();
        }
    }
}
