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
    public partial class InitialImportOwners : UserForm
    {
        public DataSet dataSetFromCSV = new DataSet();
        public BindingSource excelSource = new BindingSource();
        public DataSet columns = new DataSet();

        public InitialImportOwners()
        {
            InitializeComponent();
        }

        private void InitialImportOwners_Load(object sender, EventArgs e)
        {
            try
            {
                toolStripTextBoxSheet.Text = "OWNERS";
                columns.ReadXml(Path.Combine(SettingsClass.ImportPath, "owners_import.xml"));
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

                    DataColumn owner_id = new DataColumn();
                    owner_id.ColumnName = "OWNER_ID";
                    owner_id.DataType = Type.GetType("System.Int32");
                    dataSetFromCSV.Tables[0].Columns.Add(owner_id);
                    
                    dataSetFromCSV.AcceptChanges();

                    excelSource.DataSource = dataSetFromCSV.Tables[0];
                    dataGridView.DataSource = excelSource;
                    dataGridView.Columns["HAS_ERRORS"].Visible = false;
                    dataGridView.Columns["OWNER_ID"].Visible = false;
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
                    Owners o = new Owners(Convert.ToInt32(dataGridView["OWNER_ID", e.RowIndex].Value));
                    o.ShowDialog();
                    o.Dispose();
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
                                ArrayList _aListCoOwners = new ArrayList();
                                foreach (DataRow drn in columns.Tables[0].Rows)
                                {
                                    switch (drn["DB_COLUMN"].ToString().Trim().ToLower())
                                    {
                                        case "":
                                            break;
                                        case "status_id":
                                            MySqlParameter _STATUS_ID = new MySqlParameter("_STATUS_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", CommonFunctions.SentenceCase(dr[drn["XLS_COLUMN"].ToString()].ToString())), new MySqlParameter("_LIST_TYPE", "owner_status") })).ExecuteScalarQuery());
                                            _aList.Add(_STATUS_ID);
                                            break;
                                        case "type_id":
                                            MySqlParameter _TYPE_ID = new MySqlParameter("_TYPE_ID", (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", CommonFunctions.SentenceCase(dr[drn["XLS_COLUMN"].ToString()].ToString())), new MySqlParameter("_LIST_TYPE", "owner_type") })).ExecuteScalarQuery());
                                            _aList.Add(_TYPE_ID);
                                            break;
                                        case "bank_id1":
                                            object b_id = (new DataAccess(CommandType.StoredProcedure, "BANKSAGENCIESsp_get_id_by_name", new object[] {new MySqlParameter("_NAME", "CENTRALA"), new MySqlParameter("_BANK_NAME", dr[drn["XLS_COLUMN"].ToString()].ToString()) })).ExecuteScalarQuery();
                                            if (b_id == null)
                                            {
                                                DataRow bank = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_insert", new object[] { new MySqlParameter("_NAME", dr[drn["XLS_COLUMN"].ToString()].ToString()), new MySqlParameter("_DETAILS", null) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                                b_id = (new DataAccess(CommandType.StoredProcedure, "BANKSAGENCIESsp_insert", new object[] { new MySqlParameter("_NAME", "CENTRALA"), new MySqlParameter("_DETAILS", null), new MySqlParameter("_BANK_ID", bank["id"]) })).ExecuteScalarQuery();
                                            }
                                            MySqlParameter _BANK_ID1 = new MySqlParameter("_BANK_ID1", b_id);
                                            _aList.Add(_BANK_ID1);
                                            break;
                                        case "bank_id2":
                                            object b_id2 = (new DataAccess(CommandType.StoredProcedure, "BANKSAGENCIESsp_get_id_by_name", new object[] {new MySqlParameter("_NAME", "CENTRALA"), new MySqlParameter("_BANK_NAME", dr[drn["XLS_COLUMN"].ToString()].ToString()) })).ExecuteScalarQuery();
                                            if (b_id2 == null)
                                            {
                                                DataRow bank = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_insert", new object[] { new MySqlParameter("_NAME", dr[drn["XLS_COLUMN"].ToString()].ToString()), new MySqlParameter("_DETAILS", null) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                                b_id2 = (new DataAccess(CommandType.StoredProcedure, "BANKSAGENCIESsp_insert", new object[] { new MySqlParameter("_NAME", "CENTRALA"), new MySqlParameter("_DETAILS", null), new MySqlParameter("_BANK_ID", bank["id"]) })).ExecuteScalarQuery();
                                            }
                                            MySqlParameter _BANK_ID2 = new MySqlParameter("_BANK_ID2", b_id2);
                                            _aList.Add(_BANK_ID2);
                                            break;
                                        case "bank_id3":
                                            object b_id3 = (new DataAccess(CommandType.StoredProcedure, "BANKSAGENCIESsp_get_id_by_name", new object[] {new MySqlParameter("_NAME", "CENTRALA"), new MySqlParameter("_BANK_NAME", dr[drn["XLS_COLUMN"].ToString()].ToString()) })).ExecuteScalarQuery();
                                            if (b_id3 == null)
                                            {
                                                DataRow bank = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_insert", new object[] { new MySqlParameter("_NAME", dr[drn["XLS_COLUMN"].ToString()].ToString()), new MySqlParameter("_DETAILS", null) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                                b_id3 = (new DataAccess(CommandType.StoredProcedure, "BANKSAGENCIESsp_insert", new object[] { new MySqlParameter("_NAME", "CENTRALA"), new MySqlParameter("_DETAILS", null), new MySqlParameter("_BANK_ID", bank["id"]) })).ExecuteScalarQuery();
                                            }
                                            MySqlParameter _BANK_ID3 = new MySqlParameter("_BANK_ID3", b_id3);
                                            _aList.Add(_BANK_ID3);
                                            break;
                                        case "bank_account_details1":
                                            MySqlParameter _BANK_ACCOUNT_DETAILS1 = new MySqlParameter("_BANK_ACCOUNT_DETAILS1", dr[drn["XLS_COLUMN"].ToString()].ToString().ToUpper());
                                            _aList.Add(_BANK_ACCOUNT_DETAILS1);
                                            MySqlParameter _BANK_ACCOUNT_CURRENCY1 = new MySqlParameter("_BANK_ACCOUNT_CURRENCY1", "GBP");
                                            _aList.Add(_BANK_ACCOUNT_CURRENCY1);
                                            break;
                                        case "bank_account_details2":
                                            MySqlParameter _BANK_ACCOUNT_DETAILS2 = new MySqlParameter("_BANK_ACCOUNT_DETAILS2", dr[drn["XLS_COLUMN"].ToString()].ToString().ToUpper());
                                            _aList.Add(_BANK_ACCOUNT_DETAILS2);
                                            MySqlParameter _BANK_ACCOUNT_CURRENCY2 = new MySqlParameter("_BANK_ACCOUNT_CURRENCY2", "EUR");
                                            _aList.Add(_BANK_ACCOUNT_CURRENCY2);
                                            break;
                                        case "bank_account_details3":
                                            MySqlParameter _BANK_ACCOUNT_DETAILS3 = new MySqlParameter("_BANK_ACCOUNT_DETAILS3", dr[drn["XLS_COLUMN"].ToString()].ToString().ToUpper());
                                            _aList.Add(_BANK_ACCOUNT_DETAILS3);
                                            MySqlParameter _BANK_ACCOUNT_CURRENCY3 = new MySqlParameter("_BANK_ACCOUNT_CURRENCY3", "RON");
                                            _aList.Add(_BANK_ACCOUNT_CURRENCY3);
                                            break;
                                        case "phones":
                                            //MySqlParameter _PHONES = new MySqlParameter("_PHONES", String.Format("{0}{1}{2}", dr[drn["XLS_COLUMN"].ToString()].ToString(), dr["PHONE2"] != DBNull.Value ? (";" + dr["PHONE2"].ToString()) : "", dr["PHONE 3"] != DBNull.Value ? (dr["PHONE 3"].ToString()) : ""));
                                            MySqlParameter _PHONES = new MySqlParameter("_PHONES", String.Format("{0}{1}{2}", dr[drn["XLS_COLUMN"].ToString()].ToString(), dr["PHONE2"] != DBNull.Value ? (";" + dr["PHONE2"].ToString()) : "", dr["PHONE3"] != DBNull.Value ? (dr["PHONE3"].ToString()) : ""));
                                            _aList.Add(_PHONES);
                                            break;
                                        case "emails":
                                            //MySqlParameter _EMAILS = new MySqlParameter("_EMAILS", String.Format("{0}{1}{2}", dr[drn["XLS_COLUMN"].ToString()].ToString(), dr["E-mail 2:"] != DBNull.Value ? (";" + dr["E-mail 2:"].ToString()) : "", dr["E-mail 3:"] != DBNull.Value ? (dr["E-mail 3:"].ToString()) : ""));
                                            MySqlParameter _EMAILS = new MySqlParameter("_EMAILS", String.Format("{0}{1}{2}", dr[drn["XLS_COLUMN"].ToString()].ToString(), dr["E-mail_2:"] != DBNull.Value ? (";" + dr["E-mail_2:"].ToString()) : "", dr["E-mail_3:"] != DBNull.Value ? (dr["E-mail_3:"].ToString()) : ""));
                                            _aList.Add(_EMAILS);
                                            break;
                                        case "lawyer_information":
                                            //MySqlParameter _LAWYER_INFORMATION = new MySqlParameter("_LAWYER_INFORMATION", String.Format("{0}{1}", dr[drn["XLS_COLUMN"].ToString()].ToString(), dr["Lawyer phone no#"] != DBNull.Value ? (";" + dr["Lawyer phone no#"].ToString()) : ""));
                                            MySqlParameter _LAWYER_INFORMATION = new MySqlParameter("_LAWYER_INFORMATION", String.Format("{0}{1}", dr[drn["XLS_COLUMN"].ToString()].ToString(), dr["Lawyer_phone"] != DBNull.Value ? (";" + dr["Lawyer_phone"].ToString()) : ""));
                                            _aList.Add(_LAWYER_INFORMATION);
                                            break;
                                        case "passport_number":
                                            string[] pd = dr[drn["XLS_COLUMN"].ToString()].ToString().Trim().Split('/');
                                            MySqlParameter _PASSPORT_NUMBER = new MySqlParameter();
                                            try
                                            {
                                                _PASSPORT_NUMBER = new MySqlParameter("_PASSPORT_NUMBER", pd[0]);
                                            }
                                            catch
                                            {
                                                _PASSPORT_NUMBER = new MySqlParameter("_PASSPORT_NUMBER", DBNull.Value);
                                            }
                                            _aList.Add(_PASSPORT_NUMBER);
                                            MySqlParameter _PASSPORT_EXPIRATION_DATE = new MySqlParameter();
                                            try
                                            {
                                                _PASSPORT_EXPIRATION_DATE = new MySqlParameter("_PASSPORT_EXPIRATION_DATE", Convert.ToDateTime(pd[1]));
                                            }
                                            catch
                                            {
                                                _PASSPORT_EXPIRATION_DATE = new MySqlParameter("_PASSPORT_EXPIRATION_DATE", DBNull.Value);
                                            }
                                            _aList.Add(_PASSPORT_EXPIRATION_DATE);
                                            break;
                                        case "cif":
                                            string[] cif_cnp = dr[drn["XLS_COLUMN"].ToString()].ToString().Split('/');
                                            bool cif_found = false, cnp_found = false, nif_found = false, cui_found = false;
                                            foreach (string cc in cif_cnp)
                                            {
                                                if (cc.ToLower().IndexOf("cif:") > -1)
                                                {
                                                    MySqlParameter _CIF = new MySqlParameter("_CIF", cc.ToUpper().Replace("CIF:", "").Trim());
                                                    _aList.Add(_CIF);
                                                    cif_found = true;
                                                }
                                                if (cc.ToLower().IndexOf("cui:") > -1)
                                                {
                                                    MySqlParameter _CUI = new MySqlParameter("_CUI", cc.ToUpper().Replace("CUI:", "").Trim());
                                                    _aList.Add(_CUI);
                                                    cui_found = true;
                                                }
                                                if (cc.ToLower().IndexOf("cnp:") > -1)
                                                {
                                                    MySqlParameter _CNP = new MySqlParameter("_CNP", cc.ToUpper().Replace("CNP:", "").Trim());
                                                    _aList.Add(_CNP);
                                                    cnp_found = true;
                                                }
                                                if (cc.ToLower().IndexOf("nif:") > -1)
                                                {
                                                    MySqlParameter _NIF = new MySqlParameter("_NIF", cc.ToUpper().Replace("NIF:", "").Trim());
                                                    _aList.Add(_NIF);
                                                    nif_found = true;
                                                }
                                            }
                                            if (!cif_found)
                                            {
                                                MySqlParameter _CIF = new MySqlParameter("_CIF", null);
                                                _aList.Add(_CIF);
                                            }
                                            if (!cui_found)
                                            {
                                                MySqlParameter _CUI = new MySqlParameter("_CUI", null);
                                                _aList.Add(_CUI);
                                            }
                                            if (!cnp_found)
                                            {
                                                MySqlParameter _CNP = new MySqlParameter("_CNP", null);
                                                _aList.Add(_CNP);
                                            }
                                            if (!nif_found)
                                            {
                                                MySqlParameter _NIF = new MySqlParameter("_NIF", null);
                                                _aList.Add(_NIF);
                                            }
                                            break;
                                        default:
                                            MySqlParameter p = new MySqlParameter();
                                            p.ParameterName = String.Format("_{0}", drn["DB_COLUMN"].ToString());
                                            p.Value = (drn["XLS_COLUMN"]==DBNull.Value || drn["XLS_COLUMN"].ToString() == "")?null:dr[drn["XLS_COLUMN"].ToString().Replace('.', '#')].ToString();
                                            _aList.Add(p);
                                            break;
                                    }
                                }

                                //MySqlParameter _NAME = new MySqlParameter("_NAME", dr["OWNER"].ToString());
                                //_aList.Add(_NAME);
                                //MySqlParameter _FULL_NAME = new MySqlParameter("_FULL_NAME", dr["OWNER NAME"].ToString());
                                //_aList.Add(_FULL_NAME);
                                DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_insert", _aList.ToArray());
                                //da.ExecuteInsertQuery();
                                DataRow owner = da.ExecuteSelectQuery().Tables[0].Rows[0];
                                dr["OWNER_ID"] = owner["id"];
                                dr["IMPORTED"] = true;
                                dgvr.DefaultCellStyle.BackColor = Color.LightGreen;
                                try
                                {
                                    string[] coowners = dr["CO-OWNER"].ToString().Split('/');
                                    string[] coowners_nifs = null;
                                    try
                                    {
                                        coowners_nifs = dr["CO-OWNER_FISCAL_NUMBER"].ToString().Split(';');
                                    }
                                    catch { }
                                    for (int i = 0; i < coowners.Length; i++)
                                    {
                                        string coowner = coowners[i];
                                        MySqlParameter _NAME = new MySqlParameter("_NAME", coowner);
                                        _aListCoOwners.Add(_NAME);
                                        MySqlParameter _FULL_NAME = new MySqlParameter("_FULL_NAME", coowner);
                                        _aListCoOwners.Add(_FULL_NAME);
                                        string[] cif_cnp = coowners_nifs[i].Split('/');
                                        bool cif_found = false, cnp_found = false, nif_found = false, cui_found = false;
                                        try
                                        {
                                            foreach (string cc in cif_cnp)
                                            {
                                                if (cc.ToLower().IndexOf("cif:") > -1)
                                                {
                                                    MySqlParameter _CIF = new MySqlParameter("_CIF", cc.ToUpper().Replace("CIF:", "").Trim());
                                                    _aListCoOwners.Add(_CIF);
                                                    cif_found = true;
                                                }
                                                if (cc.ToLower().IndexOf("cui:") > -1)
                                                {
                                                    MySqlParameter _CUI = new MySqlParameter("_CUI", cc.ToUpper().Replace("CUI:", "").Trim());
                                                    _aListCoOwners.Add(_CUI);
                                                    cui_found = true;
                                                }
                                                if (cc.ToLower().IndexOf("cnp:") > -1)
                                                {
                                                    MySqlParameter _CNP = new MySqlParameter("_CNP", cc.ToUpper().Replace("CNP:", "").Trim());
                                                    _aListCoOwners.Add(_CNP);
                                                    cnp_found = true;
                                                }
                                                if (cc.ToLower().IndexOf("nif:") > -1)
                                                {
                                                    MySqlParameter _NIF = new MySqlParameter("_NIF", cc.ToUpper().Replace("NIF:", "").Trim());
                                                    _aListCoOwners.Add(_NIF);
                                                    nif_found = true;
                                                }
                                            }
                                        }
                                        catch { }
                                        if (!cif_found)
                                        {
                                            MySqlParameter _CIF = new MySqlParameter("_CIF", null);
                                            _aListCoOwners.Add(_CIF);
                                        }
                                        if (!cui_found)
                                        {
                                            MySqlParameter _CUI = new MySqlParameter("_CUI", null);
                                            _aListCoOwners.Add(_CUI);
                                        }
                                        if (!cnp_found)
                                        {
                                            MySqlParameter _CNP = new MySqlParameter("_CNP", null);
                                            _aListCoOwners.Add(_CNP);
                                        }
                                        if (!nif_found)
                                        {
                                            MySqlParameter _NIF = new MySqlParameter("_NIF", null);
                                            _aListCoOwners.Add(_NIF);
                                        }
                                    }
                                    _aListCoOwners.Add(new MySqlParameter("_COMMENTS", String.Format("Passport: {0}", dr["CO-OWNER_PASSPORT"].ToString())));
                                    _aListCoOwners.Add(new MySqlParameter("_RENOUNCEMENT_FOR_RENT_INCOMES", false));

                                    if (_aListCoOwners.Count > 0)
                                    {
                                        MySqlParameter _OWNER_ID = new MySqlParameter("_OWNER_ID", owner["id"]);
                                        _aListCoOwners.Add(_OWNER_ID);
                                        da = new DataAccess(CommandType.StoredProcedure, "CO_OWNERSsp_insert", _aListCoOwners.ToArray());
                                        da.ExecuteInsertQuery();
                                    }
                                }
                                catch { }
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
            columns.WriteXml(Path.Combine(SettingsClass.ImportPath, "owners_import.xml"), XmlWriteMode.IgnoreSchema);
        }
    }
}
