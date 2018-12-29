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
    public partial class InitialImportProperties : UserForm
    {
        public DataSet dataSetFromCSV = new DataSet();
        public BindingSource excelSource = new BindingSource();
        public DataSet columns = new DataSet();
        public DataTable dbDoubleColumns = new DataTable();
        public DataTable dbBoolColumns = new DataTable();
        public DataTable dbIntColumns = new DataTable();
        public DataTable dbDateColumns = new DataTable();

        public InitialImportProperties()
        {
            InitializeComponent();
        }

        private void InitialImportOwners_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBoxSheet.Text = "PROPERTIES";
                columns.ReadXml(Path.Combine(SettingsClass.ImportPath, "properties_import.xml"));
                dataGridViewColumns.DataSource = columns.Tables[0];
                dbDoubleColumns = (new DataAccess(CommandType.Text, "SHOW COLUMNS FROM PROPERTIES WHERE TYPE = 'double';").ExecuteSelectQuery()).Tables[0];
                dbBoolColumns = (new DataAccess(CommandType.Text, "SHOW COLUMNS FROM PROPERTIES WHERE TYPE = 'tinyint(1)';").ExecuteSelectQuery()).Tables[0];
                dbIntColumns = (new DataAccess(CommandType.Text, "SHOW COLUMNS FROM PROPERTIES WHERE TYPE LIKE 'int(10)%';").ExecuteSelectQuery()).Tables[0];
                dbDateColumns = (new DataAccess(CommandType.Text, "SHOW COLUMNS FROM PROPERTIES WHERE TYPE = 'date';").ExecuteSelectQuery()).Tables[0];
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
                    property_id.ColumnName = "PROPERTY_ID";
                    property_id.DataType = Type.GetType("System.Int32");
                    dataSetFromCSV.Tables[0].Columns.Add(property_id);

                    DataColumn owner_id = new DataColumn();
                    owner_id.ColumnName = "OWNER_ID";
                    owner_id.DataType = Type.GetType("System.Int32");
                    dataSetFromCSV.Tables[0].Columns.Add(owner_id);

                    DataColumn project_id = new DataColumn();
                    project_id.ColumnName = "PROJECT_ID";
                    project_id.DataType = Type.GetType("System.Int32");
                    dataSetFromCSV.Tables[0].Columns.Add(project_id);

                    dataSetFromCSV.AcceptChanges();

                    excelSource.DataSource = dataSetFromCSV.Tables[0];
                    dataGridView.DataSource = excelSource;
                    dataGridView.Columns["HAS_ERRORS"].Visible = false;
                    dataGridView.Columns["PROPERTY_ID"].Visible = false;
                    dataGridView.Columns["OWNER_ID"].Visible = false;
                    dataGridView.Columns["PROJECT_ID"].Visible = false;
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

                    DataAccess da = new DataAccess();
                    foreach (DataGridViewRow dgvr in dataGridView.Rows)
                    {
                        string name = dgvr.Cells["OWNER"].Value.ToString().Trim();
                        da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_id_by_name2", new object[] { new MySqlParameter("_NAME", name) });
                        object iowner_id = da.ExecuteScalarQuery();
                        if (iowner_id == null || Convert.ToInt32(iowner_id) <= 0)
                        {
                            dgvr.Cells["OWNER"].ErrorText = Language.GetMessageBoxText("ownerNotFound", "The owner could not be found in the database!");
                            dgvr.Cells["HAS_ERRORS"].Value = true;
                        }
                        else
                        {
                            dgvr.Cells["OWNER_ID"].Value = iowner_id;
                        }

                        string project = dgvr.Cells["PROJECT"].Value.ToString().Trim();
                        da = new DataAccess(CommandType.StoredProcedure, "PROJECTSsp_get_id_by_name2", new object[] { new MySqlParameter("_NAME", project) });
                        object iproject_id = da.ExecuteScalarQuery();
                        /*
                        if (iproject_id == null || Convert.ToInt32(iproject_id) <= 0)
                        {
                            dgvr.Cells["PROJECT"].ErrorText = Language.GetMessageBoxText("projectNotFound", "The project could not be found in the database!");
                            dgvr.Cells["HAS_ERRORS"].Value = true;
                        }
                        else
                        {
                            dgvr.Cells["PROJECT_ID"].Value = iproject_id;
                        }
                        */
                        dgvr.Cells["PROJECT_ID"].Value = iproject_id == null ? DBNull.Value : iproject_id;

                        string prop_name = dgvr.Cells["PROPERTY"].Value.ToString().Trim();
                        if ((new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", prop_name) })).ExecuteScalarQuery() != null)
                            dgvr.Cells["IMPORTED"].Value = true;

                    }
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
                    Property p = new Property(Convert.ToInt32(dataGridView["PROPERTY_ID", e.RowIndex].Value));
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
                                foreach (DataRow drn in columns.Tables[0].Rows)
                                {
                                    if (dbDoubleColumns.Select(String.Format("Field = '{0}'", drn["DB_COLUMN"].ToString().Trim().ToLower())).Length > 0)
                                    {
                                        try
                                        {
                                            double val = Convert.ToDouble(dr[drn["XLS_COLUMN"].ToString().Trim().Replace('.', '#')].ToString().Trim());
                                            MySqlParameter p = new MySqlParameter();
                                            p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                            p.Value = val;
                                            _aList.Add(p);
                                        }
                                        catch
                                        {
                                            MySqlParameter p = new MySqlParameter();
                                            p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                            p.Value = DBNull.Value;
                                            _aList.Add(p);
                                        }
                                    }
                                    else
                                    {
                                        if (dbIntColumns.Select(String.Format("Field = '{0}'", drn["DB_COLUMN"].ToString().Trim().ToLower())).Length > 0 &&
                                            drn["DB_COLUMN"].ToString().Trim().ToLower().IndexOf("_id") < 0)
                                        {
                                            try
                                            {
                                                int val = Convert.ToInt32(dr[drn["XLS_COLUMN"].ToString().Trim().Replace('.', '#')]);
                                                MySqlParameter p = new MySqlParameter();
                                                p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                                p.Value = val;
                                                _aList.Add(p);
                                            }
                                            catch
                                            {
                                                MySqlParameter p = new MySqlParameter();
                                                p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                                p.Value = DBNull.Value;
                                                _aList.Add(p);
                                            }
                                        }
                                        else
                                        {
                                            if (dbBoolColumns.Select(String.Format("Field = '{0}'", drn["DB_COLUMN"].ToString().Trim().ToLower())).Length > 0)
                                            {
                                                try
                                                {
                                                    bool val = dr[drn["XLS_COLUMN"].ToString().Trim().Replace('.', '#')].ToString().Trim().ToLower() == "yes" ? true : false;
                                                    MySqlParameter p = new MySqlParameter();
                                                    p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                                    p.Value = val;
                                                    _aList.Add(p);
                                                }
                                                catch
                                                {
                                                    MySqlParameter p = new MySqlParameter();
                                                    p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                                    p.Value = false;
                                                    _aList.Add(p);
                                                }
                                            }
                                            else
                                            {
                                                if (dbDateColumns.Select(String.Format("Field = '{0}'", drn["DB_COLUMN"].ToString().Trim().ToLower())).Length > 0)
                                                {
                                                    try
                                                    {
                                                        //DateTime val = CommonFunctions.FromMySqlFormatDate(dr[drn["XLS_COLUMN"].ToString().Trim().Replace('.', '#')].ToString().Trim());
                                                        DateTime val = Convert.ToDateTime(dr[drn["XLS_COLUMN"].ToString().Trim().Replace('.', '#')].ToString().Trim());
                                                        MySqlParameter p = new MySqlParameter();
                                                        p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                                        p.Value = val;
                                                        _aList.Add(p);
                                                    }
                                                    catch
                                                    {
                                                        MySqlParameter p = new MySqlParameter();
                                                        p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                                        p.Value = DBNull.Value;
                                                        _aList.Add(p);
                                                    }
                                                }
                                                else
                                                {
                                                    switch (drn["DB_COLUMN"].ToString().Trim().ToLower())
                                                    {
                                                        case "":
                                                            break;
                                                        case "status_id":
                                                            MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", CommonFunctions.SentenceCase(dr[drn["XLS_COLUMN"].ToString()].ToString())), new MySqlParameter("_LIST_TYPE", "property_status") })).ExecuteScalarQuery());
                                                            _aList.Add(_STATUS_ID);
                                                            break;
                                                        case "type_id":
                                                            MySqlParameter _TYPE_ID = new MySqlParameter("_TYPE_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", CommonFunctions.SentenceCase(dr[drn["XLS_COLUMN"].ToString()].ToString())), new MySqlParameter("_LIST_TYPE", "property_type") })).ExecuteScalarQuery());
                                                            _aList.Add(_TYPE_ID);
                                                            break;
                                                        case "property_management":
                                                            MySqlParameter _PROPERTY_MANAGEMENT = new MySqlParameter("_PROPERTY_MANAGEMENT", dr[drn["XLS_COLUMN"].ToString()].ToString().Trim().ToLower() == "yes" ? true : false);
                                                            _aList.Add(_PROPERTY_MANAGEMENT);
                                                            break;
                                                        case "poa":
                                                            MySqlParameter _POA = new MySqlParameter("_POA", dr[drn["XLS_COLUMN"].ToString()].ToString().Trim().ToLower() == "yes" ? true : false);
                                                            _aList.Add(_POA);
                                                            break;
                                                        case "storage":
                                                            MySqlParameter _STORAGE = new MySqlParameter("_STORAGE", dr[drn["XLS_COLUMN"].ToString()].ToString().Trim().ToLower() == "yes" ? true : false);
                                                            _aList.Add(_STORAGE);
                                                            break;
                                                        case "vat_applicable":
                                                            MySqlParameter _VAT_APPLICABLE = new MySqlParameter("_VAT_APPLICABLE", dr[drn["XLS_COLUMN"].ToString()].ToString().Trim().ToLower() == "yes" ? true : false);
                                                            _aList.Add(_VAT_APPLICABLE);
                                                            break;
                                                        case "include_for_agencies":
                                                            MySqlParameter _INCLUDE_FOR_AGENCIES = new MySqlParameter("_INCLUDE_FOR_AGENCIES", dr[drn["XLS_COLUMN"].ToString()].ToString().Trim().ToLower() == "yes" ? true : false);
                                                            _aList.Add(_INCLUDE_FOR_AGENCIES);
                                                            break;
                                                        case "central_heating":
                                                            MySqlParameter _CENTRAL_HEATING = new MySqlParameter("_CENTRAL_HEATING", dr[drn["XLS_COLUMN"].ToString()].ToString().Trim().ToLower() == "yes" ? true : false);
                                                            _aList.Add(_CENTRAL_HEATING);
                                                            break;
                                                        case "registered_property":
                                                            MySqlParameter _REGISTERED_PROPERTY = new MySqlParameter("_REGISTERED_PROPERTY", dr[drn["XLS_COLUMN"].ToString()].ToString().Trim().ToLower() == "yes" ? true : false);
                                                            _aList.Add(_REGISTERED_PROPERTY);
                                                            break;
                                                        case "owner_id":
                                                            MySqlParameter _OWNER_ID = new MySqlParameter("_OWNER_ID", dr["OWNER_ID"]);
                                                            _aList.Add(_OWNER_ID);
                                                            break;
                                                        case "project_id":
                                                            MySqlParameter _PROJECT_ID = new MySqlParameter("_PROJECT_ID", dr["PROJECT_ID"]);
                                                            _aList.Add(_PROJECT_ID);
                                                            break;
                                                        //case "owners_association_bank_account":
                                                        //    MySqlParameter _OWNERS_ASSOCIATION_BANK_ACCOUNT = new MySqlParameter("_OWNERS_ASSOCIATION_BANK_ACCOUNT", DBNull.Value);
                                                        //    _aList.Add(_OWNERS_ASSOCIATION_BANK_ACCOUNT);
                                                        //    break;
                                                        default:
                                                            MySqlParameter p = new MySqlParameter();
                                                            p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                                            p.Value = (drn["XLS_COLUMN"] == DBNull.Value || drn["XLS_COLUMN"].ToString() == "") ? null : dr[drn["XLS_COLUMN"].ToString().Trim().Replace('.', '#')].ToString();
                                                            _aList.Add(p);
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_insert", _aList.ToArray());
                                //da.ExecuteInsertQuery();
                                DataRow property = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                dr["PROPERTY_ID"] = property["id"];
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
            columns.WriteXml(Path.Combine(SettingsClass.ImportPath, "properties_import.xml"), XmlWriteMode.IgnoreSchema);
        }
    }
}
