using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using MySql.Data.MySqlClient;
using System.IO;

namespace FDP
{
    public partial class InitialImportTenants : UserForm
    {
        public DataSet dataSetFromCSV = new DataSet();
        public BindingSource excelSource = new BindingSource();
        public DataSet columns = new DataSet();

        public InitialImportTenants()
        {
            InitializeComponent();
        }

        private void InitialImportTenants_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBoxSheet.Text = "TENANTS";
                columns.ReadXml(Path.Combine(SettingsClass.ImportPath, "tenants_import.xml"));
                dataGridViewColumns.DataSource = columns.Tables[0];
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                toolStripTextBoxFile.Text = openFileDialog1.FileName;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFile.Text != "" && toolStripTextBoxSheet.Text != "")
                {
                    dataSetFromCSV = new DataSet();
                    string ConnectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";", toolStripTextBoxFile.Text);
                    //string ConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"", toolStripTextBoxFile.Text);
                    OleDbConnection ExcelConnection = new OleDbConnection(ConnectionString);
                    OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT * FROM [" + toolStripTextBoxSheet.Text.ToString() + "$]", ExcelConnection);
                    OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                    ExcelConnection.Open();

                    //dataSetFromCSV = new DataSet();
                    ExcelAdapter.Fill(dataSetFromCSV);
                    ExcelConnection.Close();

                    DataColumn hasErr = new DataColumn();
                    hasErr.ColumnName = "HAS_ERRORS";
                    hasErr.DataType = Type.GetType("System.Boolean");
                    hasErr.DefaultValue = false;
                    dataSetFromCSV.Tables[0].Columns.Add(hasErr);

                    DataColumn imported = new DataColumn();
                    imported.ColumnName = "IMPORTED";
                    imported.DataType = Type.GetType("System.Boolean");
                    imported.DefaultValue = false;
                    dataSetFromCSV.Tables[0].Columns.Add(imported);

                    DataColumn property_id = new DataColumn();
                    property_id.ColumnName = "TENANT_ID";
                    property_id.DataType = Type.GetType("System.Int32");
                    dataSetFromCSV.Tables[0].Columns.Add(property_id);
                    
                    dataSetFromCSV.AcceptChanges();

                    excelSource.DataSource = dataSetFromCSV.Tables[0];
                    dataGridView.DataSource = excelSource;
                    dataGridView.Columns["HAS_ERRORS"].Visible = false;
                    dataGridView.Columns["TENANT_ID"].Visible = false;
                    dataGridView.Columns["IMPORTED"].DisplayIndex = 0;
                    dataGridView.Columns["IMPORTED"].ReadOnly = true;

                    DataGridViewButtonColumn dgvbc = new DataGridViewButtonColumn();
                    dgvbc.HeaderText = Language.GetColumnHeaderText("EDIT", "EDIT");
                    dgvbc.Text = "EDIT";
                    dgvbc.Name = "EDIT";
                    dgvbc.UseColumnTextForButtonValue = true;
                    dgvbc.ReadOnly = true;
                    dgvbc.DisplayIndex = 1;
                    dataGridView.Columns.Add(dgvbc);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["EDIT"].Index)
            {
                if (Convert.ToBoolean(dataGridView[dataGridView.Columns["IMPORTED"].Index, e.RowIndex].Value))
                {
                    Projects p = new Projects(Convert.ToInt32(dataGridView["TENANT_ID", e.RowIndex].Value));
                    p.ShowDialog();
                    p.Dispose();
                }
                else
                {
                    MessageBox.Show(Language.GetMessageBoxText("importFirst", "You must first import the Excel row!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (dataSetFromCSV != null && dataSetFromCSV.Tables.Count > 0 && dataSetFromCSV.Tables[0].Rows.Count > 0)
            {
                try
                {
                    //foreach (DataRow dr in dataSetFromCSV.Tables[0].Rows)
                    foreach (DataGridViewRow dgvr in dataGridView.Rows)
                    {
                        if (dgvr.Selected)
                        {
                            DataRow dr = ((DataRowView)dgvr.DataBoundItem).Row;
                            try
                            {
                                dr.ClearErrors();
                                dr["HAS_ERRORS"] = false;
                                ArrayList _aList = new ArrayList();
                                //ArrayList _aListCoOwners = new ArrayList();
                                foreach (DataRow drn in columns.Tables[0].Rows)
                                {
                                    switch (drn["DB_COLUMN"].ToString().Trim().ToLower())
                                    {
                                        case "":
                                            break;
                                        default:
                                            MySqlParameter p = new MySqlParameter();
                                            p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                            p.Value = (drn["XLS_COLUMN"]==DBNull.Value || drn["XLS_COLUMN"].ToString() == "")?null:dr[drn["XLS_COLUMN"].ToString().Replace('.', '#')].ToString();
                                            _aList.Add(p);
                                            break;
                                    }
                                }

                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "TENANTSsp_insert", _aList.ToArray());
                                //da.ExecuteInsertQuery();
                                DataRow tenant = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                dr["TENANT_ID"] = tenant["id"];
                                dr["IMPORTED"] = true;
                                dgvr.DefaultCellStyle.BackColor = Color.LightGreen;
                            }
                            catch (Exception exp)
                            {
                                dr["IMPORTED"] = false;
                                dr.RowError = exp.Message;
                                dr["HAS_ERRORS"] = true;
                                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                }
            }
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void errorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelSource.Filter = "HAS_ERRORS = true";
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelSource.Filter = "";
        }

        private void importedOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelSource.Filter = "IMPORTED = true";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            columns.AcceptChanges();
            columns.WriteXml(Path.Combine(SettingsClass.ImportPath, "tenants_import.xml"), XmlWriteMode.IgnoreSchema);
        }
    }
}
