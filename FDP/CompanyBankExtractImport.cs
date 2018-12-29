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
using System.Data.Odbc;

namespace FDP
{
    public partial class CompanyBankExtractImport : UserForm
    {
        public string FileToSave;
        public DataSet dataSetFromCSV = new DataSet();
        public BindingSource excelSource = new BindingSource();
        public DataTable PredictedIE = new DataTable();
        public BindingSource predictedIESource = new BindingSource();
        public string BankAccountSequence = "";
        public double OpenningBallance = 0;
        public string Currency = "";
        public int clickedColIndex = -1;
        public int clickedRowIndex = -1;
        public string AccountField = "";
        public object OwnerId;
        public object PropertyId;
        public string BankAccount = "";

        public CompanyBankExtractImport()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void BankExtractImport_Load(object sender, EventArgs e)
        {
            comboBoxAccountTitle.SelectedIndex = 0;
            comboBoxLanguage.SelectedIndex = 0;
            comboBoxLanguage.Enabled = false;
            comboBoxSeparator.SelectedIndex = 0;
            toolStripLabelCompany.Text = String.Format("Company: {0}", SettingsClass.GetCompanySetting("ALIAS"));
        }

        private void FillPredictedIncomeExpenses()
        {
            try
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_get_by_company_id");
                PredictedIE = da.ExecuteSelectQuery().Tables[0];
                try { PropertyId = Convert.ToInt32(PredictedIE.Rows[0]["PROPERTY_ID"]); }
                catch { }
                DataColumn dc = new DataColumn("IMPORT", Type.GetType("System.Boolean"));
                PredictedIE.Columns.Add(dc);
                dc = new DataColumn("AMOUNT", Type.GetType("System.Double"));
                PredictedIE.Columns.Add(dc);

                dc = new DataColumn("EXCHANGE_RATE", Type.GetType("System.Double"));
                PredictedIE.Columns.Add(dc);
                dc = new DataColumn("AMOUNT_EXCHANGED", Type.GetType("System.Double"));
                PredictedIE.Columns.Add(dc);

                PredictedIE.AcceptChanges();

                predictedIESource.DataSource = PredictedIE;
                dataGrid1.Dispose();

                dataGrid1 = new DataGrid(
                    predictedIESource,
                    new string[] { "DATE" },
                    null,
                    null,
                    null,
                    null,
                    new string[] { "ALL" },
                    false,
                    false);
                dataGrid1.dataGridView.RowPostPaint -= dataGrid1.dataGridView_RowPostPaint;
                dataGrid1.dataGridView.CellContentClick += new DataGridViewCellEventHandler(dataGridViewPredictedIE_CellContentClick);
                //dataGrid1.dataGridView.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridViewPredictedIE_RowPostPaint);
                dataGrid1.dataGridView.CellValidated += new DataGridViewCellEventHandler(dataGridViewPredictedIE_CellValidated);
                //dataGrid1.Dock = DockStyle.Fill;
                dataGrid1.Width = splitContainer1.Panel2.Width;
                dataGrid1.Height = splitContainer1.Panel2.Height - panel2.Height - 5;
                dataGrid1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                dataGrid1.toolStrip1.Visible = false;
                dataGrid1.dataGridView.ReadOnly = false;
                splitContainer1.Panel2.Controls.Add(dataGrid1);

                dataGrid1.dataGridView.Refresh();
                #region -- OLD --
                /*
                predictedIESource.DataSource = PredictedIE;
                dataGridViewPredictedIE.DataSource = predictedIESource;

                foreach (DataGridViewColumn dgvc in dataGridViewPredictedIE.Columns)
                {
                    if (dgvc.Name.ToLower() == "id" || dgvc.Name.ToLower().IndexOf("_id") > -1)
                        dgvc.Visible = false;
                    if (dgvc.Name.ToLower() == "import" || dgvc.Name.ToLower() == "amount" || dgvc.Name.ToLower() == "amount_exchanged")
                        dgvc.ReadOnly = false;
                    else
                        dgvc.ReadOnly = true;
                }

                DataGridViewLinkColumn dgvlc = new DataGridViewLinkColumn();
                dgvlc.Name = "INVOICEREQUIREMENT_ID";
                dgvlc.HeaderText = Language.GetColumnHeaderText(dgvlc.Name, "INVOICE REQUIREMENT");
                dgvlc.DataPropertyName = "INVOICEREQUIREMENT_ID";
                dgvlc.TrackVisitedState = false;
                dgvlc.UseColumnTextForLinkValue = false;
                dgvlc.LinkBehavior = LinkBehavior.SystemDefault;
                dataGridViewPredictedIE.Columns.Remove("INVOICEREQUIREMENT_ID");
                dataGridViewPredictedIE.Columns.Add(dgvlc);
                dataGridViewPredictedIE.Columns["INVOICEREQUIREMENT_ID"].Visible = true;

                dgvlc = new DataGridViewLinkColumn();
                dgvlc.Name = "INVOICE";
                dgvlc.HeaderText = Language.GetColumnHeaderText(dgvlc.Name, "INVOICE");
                dgvlc.DataPropertyName = "INVOICE";
                dgvlc.UseColumnTextForLinkValue = false;
                dgvlc.LinkBehavior = LinkBehavior.SystemDefault;
                dgvlc.TrackVisitedState = false;
                dataGridViewPredictedIE.Columns.Remove("INVOICE");
                dataGridViewPredictedIE.Columns.Add(dgvlc);
                dataGridViewPredictedIE.Columns["INVOICE"].Visible = true;

                dataGridViewPredictedIE.Columns["IMPORT"].DisplayIndex = 0;
                dataGridViewPredictedIE.Columns["IMPORT"].Frozen = true;
                dataGridViewPredictedIE.Columns["IMPORT"].ReadOnly = false;

                dataGridViewPredictedIE.Columns["AMOUNT"].HeaderText = "AMOUNT TO MATCH";
                dataGridViewPredictedIE.Columns["AMOUNT"].DisplayIndex = 1;
                dataGridViewPredictedIE.Columns["AMOUNT"].Frozen = true;
                dataGridViewPredictedIE.Columns["AMOUNT"].ReadOnly = false;
                dataGridViewPredictedIE.Columns["AMOUNT"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
                dataGridViewPredictedIE.Columns["AMOUNT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridViewPredictedIE.Columns["EXCHANGE_RATE"].DisplayIndex = 2;
                //dataGridViewPredictedIE.Columns["EXCHANGE_RATE"].Frozen = true;
                //dataGridViewPredictedIE.Columns["EXCHANGE_RATE"].ReadOnly = false;
                dataGridViewPredictedIE.Columns["EXCHANGE_RATE"].DefaultCellStyle.Format = SettingsClass.DoubleFormatFourDigits;
                dataGridViewPredictedIE.Columns["EXCHANGE_RATE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridViewPredictedIE.Columns["AMOUNT_EXCHANGED"].DisplayIndex = 3;
                //dataGridViewPredictedIE.Columns["AMOUNT_EXCHANGED"].Frozen = true;
                dataGridViewPredictedIE.Columns["AMOUNT_EXCHANGED"].ReadOnly = false;
                dataGridViewPredictedIE.Columns["AMOUNT_EXCHANGED"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
                dataGridViewPredictedIE.Columns["AMOUNT_EXCHANGED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                foreach (DataGridViewRow dgvr in dataGridViewPredictedIE.Rows)
                {
                    dgvr.Cells["IMPORT"].Style.BackColor = Color.LightPink;
                    dgvr.Cells["AMOUNT"].Style.BackColor = Color.LightPink;
                    //dgvr.Cells["AMOUNT_EXCHANGED"].Style.BackColor = Color.LightPink;
                    dgvr.Cells["AMOUNT"].Value = dgvr.Cells["BALLANCE"].Value.ToString();
                    dgvr.Cells["AMOUNT"].ReadOnly = false;
                    dgvr.Cells["AMOUNT_EXCHANGED"].ReadOnly = true;
                }
                //dataGridViewPredictedIE.Columns["AMOUNT_CURRENCY"].Visible = false;

                DateTime extract_row_date = CommonFunctions.SwitchBackFormatedDate(dataGridView.SelectedRows[0].Cells["DATA TRANZACTIEI"].Value.ToString());
                CalculateAndFillRonValues(extract_row_date);
                */
                #endregion

                DataGridViewLinkColumn dgvlc = new DataGridViewLinkColumn();
                dgvlc.Name = "INVOICEREQUIREMENT_ID";
                dgvlc.HeaderText = Language.GetColumnHeaderText(dgvlc.Name, "INVOICE REQUIREMENT");
                dgvlc.DataPropertyName = "INVOICEREQUIREMENT_ID";
                dgvlc.TrackVisitedState = false;
                dgvlc.UseColumnTextForLinkValue = false;
                dgvlc.LinkBehavior = LinkBehavior.SystemDefault;
                dataGrid1.dataGridView.Columns.Remove("INVOICEREQUIREMENT_ID");
                dataGrid1.dataGridView.Columns.Add(dgvlc);
                dataGrid1.dataGridView.Columns["INVOICEREQUIREMENT_ID"].Visible = true;

                dgvlc = new DataGridViewLinkColumn();
                dgvlc.Name = "INVOICE";
                dgvlc.HeaderText = Language.GetColumnHeaderText(dgvlc.Name, "INVOICE");
                dgvlc.DataPropertyName = "INVOICE";
                dgvlc.UseColumnTextForLinkValue = false;
                dgvlc.LinkBehavior = LinkBehavior.SystemDefault;
                dgvlc.TrackVisitedState = false;
                dataGrid1.dataGridView.Columns.Remove("INVOICE");
                dataGrid1.dataGridView.Columns.Add(dgvlc);
                dataGrid1.dataGridView.Columns["INVOICE"].Visible = true;

                dataGrid1.dataGridView.Columns["IMPORT"].DisplayIndex = 0;
                dataGrid1.dataGridView.Columns["IMPORT"].Frozen = true;
                dataGrid1.dataGridView.Columns["IMPORT"].ReadOnly = false;
                dataGrid1.dataGridView.Columns["IMPORT"].DefaultCellStyle.BackColor = Color.LightPink;

                dataGrid1.dataGridView.Columns["AMOUNT"].HeaderText = "AMOUNT TO MATCH";
                dataGrid1.dataGridView.Columns["AMOUNT"].DisplayIndex = 1;
                dataGrid1.dataGridView.Columns["AMOUNT"].Frozen = true;
                dataGrid1.dataGridView.Columns["AMOUNT"].ReadOnly = false;
                dataGrid1.dataGridView.Columns["AMOUNT"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
                dataGrid1.dataGridView.Columns["AMOUNT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGrid1.dataGridView.Columns["AMOUNT"].DefaultCellStyle.BackColor = Color.LightPink;

                dataGrid1.dataGridView.Columns["EXCHANGE_RATE"].DisplayIndex = 2;
                //dataGrid1.dataGridView.Columns["EXCHANGE_RATE"].Frozen = true;
                //dataGrid1.dataGridView.Columns["EXCHANGE_RATE"].ReadOnly = false;
                dataGrid1.dataGridView.Columns["EXCHANGE_RATE"].DefaultCellStyle.Format = SettingsClass.DoubleFormatFourDigits;
                dataGrid1.dataGridView.Columns["EXCHANGE_RATE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGrid1.dataGridView.Columns["AMOUNT_EXCHANGED"].DisplayIndex = 3;
                //dataGrid1.dataGridView.Columns["AMOUNT_EXCHANGED"].Frozen = true;
                dataGrid1.dataGridView.Columns["AMOUNT_EXCHANGED"].ReadOnly = false;
                dataGrid1.dataGridView.Columns["AMOUNT_EXCHANGED"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
                dataGrid1.dataGridView.Columns["AMOUNT_EXCHANGED"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                foreach (DataGridViewRow dgvr in dataGrid1.dataGridView.Rows)
                {
                    dgvr.Cells["IMPORT"].Style.BackColor = Color.LightPink;
                    //dgvr.Cells["IMPORT"].ReadOnly = false;
                    dgvr.Cells["AMOUNT"].Style.BackColor = Color.LightPink;
                    ////dgvr.Cells["AMOUNT_EXCHANGED"].Style.BackColor = Color.LightPink;
                    dgvr.Cells["AMOUNT"].Value = dgvr.Cells["BALLANCE"].Value.ToString();
                    //dgvr.Cells["AMOUNT"].ReadOnly = false;
                    //dgvr.Cells["AMOUNT_EXCHANGED"].ReadOnly = true;
                }
                //dataGrid1.dataGridView.Columns["AMOUNT_CURRENCY"].Visible = false;

                DateTime extract_row_date = CommonFunctions.SwitchBackFormatedDate(dataGridView.SelectedRows[0].Cells["DATA TRANZACTIEI"].Value.ToString());
                CalculateAndFillRonValues(extract_row_date);
                dataGrid1.dataGridView.Enabled = !checkBoxManual.Checked;

            }
            catch (Exception exp) { exp.ToString(); }
        }

        private void toolStripButtonShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFile.Text != "") // && toolStripTextBoxSheet.Text != "")
                {
                    //dataSetFromCSV.Dispose();
                    dataSetFromCSV = new DataSet();
                    //dataSetFromCSV.Clear();
                    excelSource.DataSource = null;
                    dataGridView.DataSource = null;
                    /*
                    string ConnectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";", toolStripTextBoxFile.Text);
                    //string ConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"", toolStripTextBoxFile.Text);
                    OleDbConnection ExcelConnection = new OleDbConnection(ConnectionString);
                    OleDbCommand ExcelCommand = new OleDbCommand(@"SELECT * FROM [" + toolStripTextBoxSheet.Text.ToString() + "$]", ExcelConnection);
                    OleDbDataAdapter ExcelAdapter = new OleDbDataAdapter(ExcelCommand);
                    ExcelConnection.Open();
                    //dataSetFromCSV = new DataSet();
                    ExcelAdapter.Fill(dataSetFromCSV);
                    ExcelConnection.Close();
                    */

                    /* --- ODBC Alternative - doesn't work ! ---
                    System.IO.FileInfo fi = new System.IO.FileInfo(toolStripTextBoxFile.Text);
                    // retrives directory
                    string dirCSV = fi.DirectoryName.ToString();
                    // retrives file name with extension
                    string fileNevCSV = fi.Name.ToString();
                    // Creates and opens an ODBC connection
                    string strConnString = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + dirCSV + ";Extensions=asc,csv,tab,txt;Persist Security Info=False";
                    string sql_select;
                    OdbcConnection conn;
                    conn = new OdbcConnection(strConnString.Trim());
                    conn.Open();
                    //Creates the select command text
                    sql_select = "select * from [" + fileNevCSV.Trim() + "]";
                    //Creates the data adapter
                    OdbcDataAdapter obj_oledb_da = new OdbcDataAdapter(sql_select, conn);
                    //Fills dataset with the records from CSV file
                    obj_oledb_da.Fill(dataSetFromCSV);
                    //closes the connection
                    conn.Close();
                    */
                    bool file_loaded_before = false;
                    string FILE_NAME = toolStripTextBoxFile.Text.Substring(toolStripTextBoxFile.Text.LastIndexOf('\\') + 1);
                    FILE_NAME = FILE_NAME.Remove(FILE_NAME.LastIndexOf('.'));
                    string[] filePaths = Directory.GetFiles(SettingsClass.BankExtractPath, "*.xml");

                    foreach (string filePath in filePaths)
                    {
                        if (filePath.Substring(filePath.LastIndexOf('\\') + 1).Replace(".xml", "") == FILE_NAME)
                        {
                            file_loaded_before = true;
                            MessageBox.Show(Language.GetMessageBoxText("bankExtractAlreadyLoaded", "The bank extract was loaded before!"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FileToSave = filePath;
                            dataSetFromCSV.ReadXml(filePath);
                            try
                            {
                                DataTable dtClone = dataSetFromCSV.Tables[0].Clone();
                                dtClone.Columns["IMPORTED"].DataType = typeof(bool);
                                foreach (DataRow row in dataSetFromCSV.Tables[0].Rows)
                                {
                                    dtClone.ImportRow(row);
                                }
                                dtClone.AcceptChanges();
                                dataSetFromCSV.Tables.RemoveAt(0);
                                dataSetFromCSV.Tables.Add(dtClone);
                                dataSetFromCSV.AcceptChanges();

                                DataTable dtClone2 = dataSetFromCSV.Tables[0].Copy();
                                dataSetFromCSV.Tables.RemoveAt(0);
                                dataSetFromCSV.Tables.Add(dtClone2);

                                dataSetFromCSV.AcceptChanges();
                            }
                            catch { }

                            if (dataSetFromCSV != null && dataSetFromCSV.Tables.Count > 0 && dataSetFromCSV.Tables[0].Rows.Count > 0)
                            {
                                try
                                {
                                    BankAccountSequence = dataSetFromCSV.Tables[0].Rows[0]["cont"].ToString();
                                    userTextBoxAccount.Text = BankAccountSequence;
                                }
                                catch { }
                                try
                                {
                                    Currency = dataSetFromCSV.Tables[0].Rows[0]["valuta"].ToString();
                                    userTextBoxCurrency.Text = Currency;
                                }
                                catch {
                                    try
                                    {
                                        Currency = dataSetFromCSV.Tables[0].Rows[0]["valut?"].ToString();
                                        userTextBoxCurrency.Text = Currency;
                                    }
                                    catch { }                                
                                }
                                string openingBallanceColumnName = "";
                                try
                                {
                                    if (dataSetFromCSV.Tables[0].Columns.IndexOf("sold deschidere") > -1) openingBallanceColumnName = "sold deschidere";
                                    else openingBallanceColumnName = "opening balance";
                                    OpenningBallance = Convert.ToDouble(dataSetFromCSV.Tables[0].Rows[0][openingBallanceColumnName]);
                                }
                                catch
                                {
                                    try
                                    {
                                        if (dataSetFromCSV.Tables[0].Columns.IndexOf("sold deschidere") > -1) openingBallanceColumnName = "sold deschidere";
                                        else openingBallanceColumnName = "opening balance";
                                        OpenningBallance = Convert.ToDouble(dataSetFromCSV.Tables[0].Rows[0][openingBallanceColumnName].ToString().Replace(".", "").Replace(",", "."));
                                    }
                                    catch
                                    {
                                        OpenningBallance = 0;
                                    }
                                }
                                userTextBoxOpenningBallance.Text = OpenningBallance.ToString(SettingsClass.DoubleFormat);
                            }
                            break;
                        }
                    }

                    if (!file_loaded_before)
                    {

                        //dataSetFromCSV = CsvImporter.Import(toolStripTextBoxFile.Text, true, '\t');
                        dataSetFromCSV = CsvImporter.Import(toolStripTextBoxFile.Text, true, comboBoxSeparator.SelectedItem.ToString()[0]);
                        FileToSave = Path.Combine(SettingsClass.BankExtractPath, String.Format("{0}.xml", FILE_NAME));

                        // EXCEL FILE HAS ON THE FIRST TWO ROWS ACCOUNT AND OPENNING BALLANCE
                        if (dataSetFromCSV != null && dataSetFromCSV.Tables.Count > 0 && dataSetFromCSV.Tables[0].Rows.Count > 0 && dataSetFromCSV.Tables[0].Columns[0].ColumnName.ToLower() == comboBoxAccountTitle.SelectedItem.ToString().ToLower())
                        {
                            try
                            {
                                BankAccountSequence = dataSetFromCSV.Tables[0].Rows[0]["cont"].ToString();
                                userTextBoxAccount.Text = BankAccountSequence;
                                string BANK_ACCOUNT = (new DataAccess(CommandType.StoredProcedure, "COMPANYsp_GET_ACCOUNT", new object[]{
                                new MySqlParameter("_BANK_ACCOUNT_SEQUENCE", BankAccountSequence), 
                                new MySqlParameter("_COMPANY_ID", SettingsClass.CompanyId)})).ExecuteScalarQuery().ToString();
                                if (BANK_ACCOUNT == null || BANK_ACCOUNT == "")
                                {
                                    MessageBox.Show(Language.GetMessageBoxText("bankAccountSequenceError", "Bank account sequence not found in the database. You can not continue!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    return;
                                }
                            }
                            catch
                            {
                                MessageBox.Show(Language.GetMessageBoxText("bankAccountSequenceError", "Bank account sequence not found in the database. You can not continue!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                return;
                            }
                            try
                            {
                                Currency = dataSetFromCSV.Tables[0].Rows[0]["valuta"].ToString();
                                userTextBoxCurrency.Text = Currency;
                            }
                            catch {
                                try
                                {
                                    Currency = dataSetFromCSV.Tables[0].Rows[0]["valut?"].ToString();
                                    userTextBoxCurrency.Text = Currency;
                                }
                                catch { }
                            }
                            string openingBallanceColumnName = "";
                            try
                            {
                                if (dataSetFromCSV.Tables[0].Columns.IndexOf("sold deschidere") > -1) openingBallanceColumnName = "sold deschidere";
                                else openingBallanceColumnName = "opening balance";
                                OpenningBallance = Convert.ToDouble(dataSetFromCSV.Tables[0].Rows[0][openingBallanceColumnName]);
                            }
                            catch
                            {
                                try
                                {
                                    if (dataSetFromCSV.Tables[0].Columns.IndexOf("sold deschidere") > -1) openingBallanceColumnName = "sold deschidere";
                                    else openingBallanceColumnName = "opening balance";
                                    OpenningBallance = Convert.ToDouble(dataSetFromCSV.Tables[0].Rows[0][openingBallanceColumnName].ToString().Replace(".", "").Replace(",", "."));
                                }
                                catch
                                {
                                    OpenningBallance = 0;
                                }
                            }
                            userTextBoxOpenningBallance.Text = OpenningBallance.ToString(SettingsClass.DoubleFormat);

                            dataSetFromCSV.Tables[0].Rows[0].Delete();
                            dataSetFromCSV.Tables[0].Columns["cont"].ColumnName = "Suma";
                            dataSetFromCSV.AcceptChanges();
                            foreach (DataColumn dc in dataSetFromCSV.Tables[0].Columns)
                            {
                                //if (dc.ColumnName.ToLower().IndexOf("f") == 0)
                                //{
                                try
                                {
                                    dc.ColumnName = dataSetFromCSV.Tables[0].Rows[0][dc.ColumnName].ToString();
                                }
                                catch (Exception exp)
                                {
                                    exp.ToString(); //MessageBox.Show(exp.Message);
                                }
                            }
                            //dataSetFromCSV.Tables[0].Columns["F4"].ColumnName = dataSetFromCSV.Tables[0].Rows[0]["F4"].ToString();
                            dataSetFromCSV.Tables[0].Rows[0].Delete();
                            DataTable SOLD_DESCHIDERE = new DataTable("SOLD_DESCHIDERE");
                            DataColumn dcs = new DataColumn("SOLD DESCHIDERE", Type.GetType("System.Double"));
                            SOLD_DESCHIDERE.Columns.Add(dcs);
                            DataRow dr = SOLD_DESCHIDERE.NewRow();
                            dr["SOLD DESCHIDERE"] = OpenningBallance;
                            SOLD_DESCHIDERE.Rows.Add(dr);
                            SOLD_DESCHIDERE.AcceptChanges();
                            dataSetFromCSV.Tables.Add(SOLD_DESCHIDERE);
                            dataSetFromCSV.AcceptChanges();
                        }
                    }

                    if (dataSetFromCSV.Tables[0].Columns.IndexOf("IMPORTED") < 0 && dataSetFromCSV.Tables[0].Columns.IndexOf("HAS_ERRORS") < 0)
                    {
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
                        /*
                        DataColumn owner_id = new DataColumn();
                        owner_id.ColumnName = "OWNER_ID";
                        owner_id.DataType = Type.GetType("System.Int32");
                        dataSetFromCSV.Tables[0].Columns.Add(owner_id);
                        */
                        DataColumn dc_amount = new DataColumn("TMP", Type.GetType("System.Double"));
                        dataSetFromCSV.Tables[0].Columns.Add(dc_amount);
                        /*
                        DataColumn dc_ballance = new DataColumn("BALLANCE", Type.GetType("System.Double"));
                        dataSetFromCSV.Tables[0].Columns.Add(dc_ballance);
                        */
                        DataColumn full_description = new DataColumn();
                        full_description.ColumnName = "FULL_DESCRIPTION";
                        full_description.DataType = Type.GetType("System.String");
                        dataSetFromCSV.Tables[0].Columns.Add(full_description);
                        dataSetFromCSV.AcceptChanges();
                    }

                    excelSource.DataSource = dataSetFromCSV.Tables[0];
                    dataGridView.DataSource = excelSource;
                    dataGridView.Columns["TMP"].Visible = false;
                    dataGridView.Columns["HAS_ERRORS"].Visible = false;
                    //dataGridView.Columns["OWNER_ID"].Visible = false;
                    dataGridView.Columns["IMPORTED"].DisplayIndex = 0;
                    dataGridView.Columns["IMPORTED"].ReadOnly = true;
                    dataGridView.Columns["FULL_DESCRIPTION"].Visible = false;

                    /*
                    if (dataGridView.Columns["EDIT"] == null)
                    {
                        DataGridViewButtonColumn dgvbc = new DataGridViewButtonColumn();
                        dgvbc.HeaderText = Language.GetColumnHeaderText("EDIT", "EDIT");
                        dgvbc.Text = "EDIT";
                        dgvbc.Name = "EDIT";
                        dgvbc.UseColumnTextForButtonValue = true;
                        dgvbc.ReadOnly = true;
                        dgvbc.DisplayIndex = 1;
                        dataGridView.Columns.Add(dgvbc);
                    }
                    */
                    /*
                    if (dataGridView.Columns["BALLANCE"] == null)
                    {
                        DataGridViewTextBoxColumn dgvc = new DataGridViewTextBoxColumn();
                        dgvc.HeaderText = Language.GetColumnHeaderText("BALLANCE", "BALLANCE");
                        dgvc.Name = "BALLANCE";
                        dgvc.ReadOnly = true;
                        dgvc.DisplayIndex = 0;
                        //dgvc.DefaultCellStyle.BackColor = Color.LightPink;
                        dataGridView.Columns.Add(dgvc);
                    }
                    */

                    // CHECK IF ROW WAS IMPORTED FROM OWNER BANK EXTRACT IMPORT FORM
                    foreach (DataGridViewRow dgvr in dataGridView.Rows)
                    {
                        /*
                        try
                        {
                            dgvr.Cells["IMPORTED"].Value = false;
                            foreach (DataGridViewColumn dgvc in dataGridView.Columns)
                            {
                                if (dgvc.Name.ToLower().IndexOf("detalii tranzactie") > -1 || dgvc.Name.ToLower().IndexOf("detalii plata") > -1)
                                {
                                    DataRow br = (new DataAccess(CommandType.StoredProcedure, "BANK_RECEIPTSsp_get_by_amount_date_description", new object[]{
                                    new MySqlParameter("_AMOUNT", Convert.ToDouble(dgvr.Cells["SUMA"].Value)),
                                    new MySqlParameter("_DATE", Convert.ToDateTime(dgvr.Cells["DATA TRANZACTIEI"].Value)),
                                    new MySqlParameter("_DESCRIPTION_SEQUENCE", dgvr.Cells[dgvc.Name].Value)
                                })).ExecuteSelectQuery().Tables[0].Rows[0];
                                    if (br != null)
                                    {
                                        dgvr.Cells["IMPORTED"].Value = true;
                                        break;
                                    }
                                }
                            }
                        }
                        catch
                        {
                            dgvr.Cells["IMPORTED"].Value = false;
                        }
                        */
                        try
                        {
                            dgvr.Cells["TMP"].Value = Convert.ToDouble(dgvr.Cells["SUMA"].Value.ToString().Replace(",", "."));
                        }
                        catch
                        {
                            try
                            {
                                dgvr.Cells["TMP"].Value = Convert.ToDouble(dgvr.Cells["SUMA"].Value.ToString().Replace(".", "").Replace(",", "."));
                            }
                            catch
                            {
                                dgvr.Cells["TMP"].Value = 0;
                            }
                        }
                        /*
                        dgvr.Cells["BALLANCE"].Value = dgvr.Cells["TMP"].Value;
                        dgvr.Cells["BALLANCE"].Style.BackColor = Color.LightPink;
                        dgvr.Cells["BALLANCE"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvr.Cells["BALLANCE"].Style.Format = SettingsClass.DoubleFormat;
                        */
                    }
                    ////int index_suma = dataGridView.Columns["SUMA"].Index;
                    //dataGridView.Columns["BALLANCE"].DisplayIndex = 1;
                    dataGridView.Columns.Remove("SUMA");
                    dataGridView.Columns["TMP"].Name = "SUMA";
                    dataGridView.Columns["SUMA"].HeaderText = "SUMA";
                    dataGridView.Columns["SUMA"].DisplayIndex = 1; // 2;
                    dataGridView.Columns["SUMA"].DefaultCellStyle.Format = SettingsClass.DoubleFormat;
                    dataGridView.Columns["SUMA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView.Columns["SUMA"].Visible = true;

                    FillPredictedIncomeExpenses();
                }
            }
            catch (Exception exp)
            {
                base.ShowErrorsDialog(Language.GetMessageBoxText("errorGettingExcelData", String.Format("There was an error getting the information from the Excel file: {0}", exp.Message)));
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }
        /*
        private void SelectOwner(string bank_account_sequence)
        {
            try
            {
                //string owner_name = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_by_bank_account", new object[] { new MySqlParameter("_BANK_ACCOUNT_SEQUENCE", bank_account_sequence) })).ExecuteScalarQuery().ToString();
                string account_field = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_account_field_by_bank_account", new object[] { new MySqlParameter("_BANK_ACCOUNT_SEQUENCE", bank_account_sequence) })).ExecuteScalarQuery().ToString();
                switch (account_field)
                {
                    case "1":
                        AccountField = "STARTING_BALLANCE1";
                        break;
                    case "2":
                        AccountField = "STARTING_BALLANCE2";
                        break;
                    case "3":
                        AccountField = "STARTING_BALLANCE3";
                        break;
                }
                
                foreach (string x in toolStripComboBoxOwner.Items)
                {
                    if (x.ToLower() == owner_name.ToLower())
                    {
                        toolStripComboBoxOwner.SelectedIndex = toolStripComboBoxOwner.Items.IndexOf(x);
                        break;
                    }
                }
                OwnerId = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", owner_name) })).ExecuteScalarQuery().ToString();
                FillProperties(owner_name);
                
            }
            catch { }
        }
        */

        private void FillProperties()
        {
            DataTable dt = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get")).ExecuteSelectQuery().Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                toolStripComboBoxProperty.Items.Add(dr["name"].ToString());
            }
        }

        private void FillCombos()
        {
            DataAccess da = new DataAccess();

            da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_list");
            DataTable dtOwners = da.ExecuteSelectQuery().Tables[0];
            if (dtOwners != null)
            {
                comboBoxOwner.DisplayMember = "name";
                comboBoxOwner.ValueMember = "id";
                comboBoxOwner.DataSource = dtOwners;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //openFileDialog1.InitialDirectory = Application.ExecutablePath.ToString();
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                toolStripTextBoxFile.Text = openFileDialog1.FileName;
            }
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            // Load context menu on right mouse click
            DataGridView.HitTestInfo hitTestInfo;
            if (e.Button == MouseButtons.Right)
            {
                hitTestInfo = dgv.HitTest(e.X, e.Y);
                if (hitTestInfo.Type == DataGridViewHitTestType.Cell && (dgv.Columns[hitTestInfo.ColumnIndex].Name.ToLower().IndexOf("detalii tranzactie") > -1 || dgv.Columns[hitTestInfo.ColumnIndex].Name.ToLower().IndexOf("detalii plata") > -1) &&
                    dgv[hitTestInfo.ColumnIndex, hitTestInfo.RowIndex].Value.ToString().Trim() != "" && dgv.CurrentRow.Index == hitTestInfo.RowIndex)
                {
                    contextMenuStrip1.Show(dgv, new Point(e.X, e.Y));
                    clickedColIndex = hitTestInfo.ColumnIndex;
                    clickedRowIndex = hitTestInfo.RowIndex;
                }
            }
        }

        private void sendDescriptionToTextboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxDescription.Text += dataGridView[clickedColIndex, clickedRowIndex].Value.ToString();
                clickedColIndex = -1;
                clickedRowIndex = -1;
            }
            catch { }
        }

        private void copyDescriptionToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(dataGridView[clickedColIndex, clickedRowIndex].Value.ToString());
            clickedColIndex = -1;
            clickedRowIndex = -1;
        }

        private void radioButtonManualExpense_CheckedChanged(object sender, EventArgs e)
        {
            dataGridViewPredictedIE.Enabled = !((RadioButton)sender).Checked;
            userTextBoxManualExpenseAmount.Enabled = ((RadioButton)sender).Checked;
            if (((RadioButton)sender).Checked)
                userTextBoxManualExpenseAmount.Text = dataGridView["suma", dataGridView.CurrentRow.Index].Value.ToString();
            else
                userTextBoxManualExpenseAmount.Text = "";
        }

        private void radioButtonManualIncome_CheckedChanged(object sender, EventArgs e)
        {
            dataGrid1.dataGridView.Enabled = !((RadioButton)sender).Checked;
            userTextBoxManualExpenseAmount.Enabled = ((RadioButton)sender).Checked;
            if (((RadioButton)sender).Checked)
                userTextBoxManualExpenseAmount.Text = dataGridView["suma", dataGridView.CurrentRow.Index].Value.ToString();
            else
                userTextBoxManualExpenseAmount.Text = "";
        }

        private void copyAllDescriptionsToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(CreateStringFromDescriptions());
            clickedColIndex = -1;
            clickedRowIndex = -1;
        }

        private string CreateStringFromDescriptions()
        {
            string toReturn = "";
            foreach (DataGridViewColumn dgvc in dataGridView.Columns)
            {
                if (dgvc.Name.ToLower().IndexOf("detalii tranzactie") > -1 || dgvc.Name.ToLower().IndexOf("detalii plata") > -1)
                {
                    toReturn += String.Format("{0} ", dataGridView[dgvc.Index, dataGridView.CurrentRow.Index].Value.ToString().Trim());
                }
            }
            return toReturn;
        }

        private void sendAllDescriptionsToTextboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxDescription.Text += CreateStringFromDescriptions();
                clickedColIndex = -1;
                clickedRowIndex = -1;
            }
            catch { }
        }

        private void toolStripButtonImport_Click(object sender, EventArgs e)
        {
            if (!checkBoxManual.Checked)
            {
                dataGrid1.dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dataGrid1.dataGridView.EndEdit();
                PredictedIE.AcceptChanges();
                if (PredictedIE.Select("isnull(import,false) = true OR isnull(import,0) = 1 OR isnull(import,'False') = 'True'").Length <= 0)
                {
                    MessageBox.Show(Language.GetMessageBoxText("selectIncomeExpenses", "Please select the Income/Expenses(s) first!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                try
                {
                    int owner_id = Convert.ToInt32(dataGrid1.dataGridView.SelectedRows[0].Cells["owner_id"].Value);
                    foreach (DataGridViewRow dgvr in dataGrid1.dataGridView.SelectedRows)
                    {
                        if (owner_id != Convert.ToInt32( dgvr.Cells["owner_id"].Value))
                        {
                            base.ShowErrorsDialog(Language.GetMessageBoxText("multipleOwnersSelected", "You cannot import a value for multiple owners at once!"));
                            return;
                        }
                    }
                    OwnerId = owner_id;
                }
                catch { }
                DateTime data = DateTime.Now.Date;
                try
                {
                    data = CommonFunctions.SwitchBackFormatedDate(dataGridView["data tranzactiei", dataGridView.CurrentRow.Index].Value.ToString());
                }
                catch { }

                double amount = 0;
                double total_amount = 0;
                object invoice_id = -1;
                double amount_from_extras = Math.Abs(Convert.ToDouble(dataGridView["suma", dataGridView.CurrentRow.Index].Value));

                dataGrid1.dataGridView.EndEdit();
                /*
                foreach (DataGridViewRow dgvr in dataGridViewPredictedIE.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["IMPORT"].Value == DBNull.Value ? false : dgvr.Cells["IMPORT"].Value))
                    {
                        errorProvider1.SetError(dataGridViewPredictedIE, "");
                        if (invoice_id != null && invoice_id.ToString() == "-1") invoice_id = dgvr.Cells["INVOICE_ID"].Value;
                        else if (invoice_id.ToString() != dgvr.Cells["INVOICE_ID"].Value.ToString())
                        {
                            errorProvider1.SetError(dataGridViewPredictedIE, Language.GetErrorText("errorMultipleInvoices", "You have selected multiple invoice! Please select only one invoice!"));
                            base.ErrorList.Add(new KeyValuePair<string, string>(dataGridViewPredictedIE.Name, Language.GetErrorText("errorMultipleInvoices", "You have selected multiple invoice! Please select only one invoice!")));
                            base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                            return;
                        }
                    }
                }
                */

                // check for multiple currencies
                string currency = dataGrid1.dataGridView.SelectedRows[0].Cells["CURRENCY"].Value.ToString();
                foreach (DataGridViewRow dgvr in dataGrid1.dataGridView.SelectedRows)
                {
                    if (dgvr.Cells["CURRENCY"].Value.ToString() != currency)
                    {
                        MessageBox.Show(Language.GetMessageBoxText("multipleSelectedCurrencies", "You can not to import I/E's with different currencies at the same time!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    currency = dgvr.Cells["CURRENCY"].Value.ToString();
                }

                bool doit = true;
                foreach (DataGridViewRow dgvr in dataGrid1.dataGridView.Rows)
                {
                    if (Convert.ToBoolean(dgvr.Cells["IMPORT"].Value == DBNull.Value ? false : dgvr.Cells["IMPORT"].Value))
                    {
                        try
                        {
                            if (dataGridView["VALUTA", dataGridView.CurrentRow.Index].Value.ToString().ToLower() == dgvr.Cells["CURRENCY"].Value.ToString().ToLower())
                            {
                                amount = Math.Abs(Convert.ToDouble(dgvr.Cells["AMOUNT"].Value));
                            }
                            else
                            {
                                amount = Math.Abs(Convert.ToDouble(dgvr.Cells["AMOUNT_EXCHANGED"].Value));
                            }
                            total_amount += amount;
                        }
                        catch { }
                    }
                }
                if (Math.Abs(Convert.ToDouble(dataGridView["SUMA", dataGridView.CurrentRow.Index].Value)) != total_amount)
                {
                    DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("AmountsDontMatch", "The selected value from the file and the sum of the selected I/E(s) don't match! Continue?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    doit = ans == System.Windows.Forms.DialogResult.Yes;
                }


                if (doit)
                {
                    object bankextract_id = DBNull.Value;
                    try
                    {
                        DataAccess da = new DataAccess();
                        if (Convert.ToBoolean(dataGridView["IMPORTED", dataGridView.CurrentRow.Index].Value))
                        {
                            try
                            {
                                da = new DataAccess(CommandType.StoredProcedure, "BANK_RECEIPTSsp_get_by_amount_date_description", new object[]{
                                    new MySqlParameter("_AMOUNT", amount_from_extras),
                                    new MySqlParameter("_DATA_TRANZACTIEI", data),
                                    new MySqlParameter("_DESCRIPTION_SEQUENCE", textBoxDescription.Text)
                                });
                                bankextract_id = da.ExecuteSelectQuery().Tables[0].Rows[0]["ID"];
                            }
                            catch { }
                        }
                        if (bankextract_id == DBNull.Value)
                        {
                            da = new DataAccess(CommandType.StoredProcedure, "BANK_RECEIPTSsp_insert", new object[]{
                                new MySqlParameter("_EXTRACT_NUMBER", "BE"), // TO DO: GET EXTRACT NUMBER FROM FILE NAME
                                new MySqlParameter("_EXTRACT_DATE", DateTime.Now.Date), //TO DO: GET EXTRACT DATE FROM FILE NAME
                                new MySqlParameter("_DATE", data),
                                new MySqlParameter("_INVOICE_ID", Convert.ToInt32(invoice_id)==-1?DBNull.Value:invoice_id),
                                //new MySqlParameter("_AMOUNT_PAID", total_amount),
                                new MySqlParameter("_AMOUNT_PAID", amount_from_extras),
                                new MySqlParameter("_DESCRIPTION", textBoxDescription.Text),
                                new MySqlParameter("_OWNER_ID", OwnerId),
                                new MySqlParameter("_PROPERTY_ID", PropertyId),
                                //new MySqlParameter("_INCOME_EXPENSE_ID", DBNull.Value), // ---- !!! - M:M relation !!!  ----
                                new MySqlParameter("_CURRENCY", userTextBoxCurrency.Text.Trim()),
                                new MySqlParameter("_COMMENTS", textBoxDescription.Text),
                                new MySqlParameter("_BANK_ACCOUNT_DETAILS", BankAccount)
                            });
                            bankextract_id = Convert.ToInt32(da.ExecuteSelectQuery().Tables[0].Rows[0]["id"]);
                        }
                    }
                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                    //Insert bank receipt

                    double left_to_substract = Math.Abs(Convert.ToDouble(dataGridView["SUMA", dataGridView.CurrentRow.Index].Value));
                    double final_amount = 0;
                    double amount_sum = Convert.ToDouble(PredictedIE.Compute("Sum(amount)", "isnull(import, false) = true"));
                    //int pie_count = Convert.ToInt32(PredictedIE.Compute("Count(id)", "isnull(import, false) = true"));
                    foreach (DataGridViewRow dgvr in dataGrid1.dataGridView.Rows)
                    {
                        double pie_sum = amount_sum;
                        if (Convert.ToBoolean(dgvr.Cells["IMPORT"].Value == DBNull.Value ? false : dgvr.Cells["IMPORT"].Value))
                        {
                            try
                            {
                                if (dataGridView["VALUTA", dataGridView.CurrentRow.Index].Value.ToString().ToLower() == dgvr.Cells["CURRENCY"].Value.ToString().ToLower())
                                {
                                    //amount = Math.Abs(Convert.ToDouble(dgvr.Cells["AMOUNT"].Value)) <= Math.Abs(Convert.ToDouble(dataGridView["SUMA", dataGridView.CurrentRow.Index].Value)) ? Math.Abs(Convert.ToDouble(dgvr.Cells["AMOUNT"].Value)) : Math.Abs(Convert.ToDouble(dataGridView["SUMA", dataGridView.CurrentRow.Index].Value));
                                    amount = Math.Abs(Convert.ToDouble(dgvr.Cells["AMOUNT"].Value));
                                }
                                else
                                {
                                    //amount = Math.Abs(Convert.ToDouble(dgvr.Cells["AMOUNT_EXCHANGED"].Value)) <= Math.Abs(Convert.ToDouble(dataGridView["SUMA", dataGridView.CurrentRow.Index].Value)) ? Math.Abs(Convert.ToDouble(dgvr.Cells["AMOUNT"].Value)) : Math.Abs(Convert.ToDouble(dataGridView["SUMA", dataGridView.CurrentRow.Index].Value));
                                    amount = Math.Abs(Convert.ToDouble(dgvr.Cells["AMOUNT_EXCHANGED"].Value));
                                    pie_sum = CommonFunctions.ConvertCurrency(pie_sum, dgvr.Cells["CURRENCY"].Value.ToString().ToLower(), Currency.ToLower(), data);
                                }
                                final_amount = left_to_substract < amount ? left_to_substract : (pie_sum == amount ? left_to_substract : amount);
                                double final_amount_converted = CommonFunctions.ConvertCurrency(final_amount, Currency.ToLower(), dgvr.Cells["CURRENCY"].Value.ToString().ToLower(), data);
                                (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_change_status3", new object[]{
                                    new MySqlParameter("_ID", dgvr.Cells["ID"].Value),
                                    new MySqlParameter("_SOURCE", true),
                                    //new MySqlParameter("_AMOUNT", Math.Abs(final_amount)),
                                    new MySqlParameter("_AMOUNT", final_amount_converted),
                                    //new MySqlParameter("_AMOUNT_PAID", dgvr.Cells["CURRENCY"].Value.ToString() == "RON" ? 0 : dgvr.Cells["AMOUNT"].Value),
                                    new MySqlParameter("_AMOUNT_PAID", Currency == "RON" ? 0 : final_amount),
                                    //new MySqlParameter("_AMOUNT_PAID_RON", dgvr.Cells["CURRENCY"].Value.ToString() == "RON" ? dgvr.Cells["AMOUNT_EXCHANGED"].Value : 0),
                                    new MySqlParameter("_AMOUNT_PAID_RON", Currency == "RON" ? final_amount : 0),
                                    new MySqlParameter("_BANK_ACCOUNT_DETAILS", BankAccount),
                                    new MySqlParameter("_RECEIPT_ID", DBNull.Value),
                                    new MySqlParameter("_BANKEXTRACT_ID", bankextract_id)
                                })).ExecuteUpdateQuery();
                            }
                            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                            //Check if all amount is converted and update Predicted IE ballance accordingly

                            // WE DO THIS ONLY FROM THE OWNER BANK EXTRACT IMPORT
                            /*
                            try
                            {
                                if (dgvr.Cells["INVOICE_ID"].Value != DBNull.Value && dgvr.Cells["INVOICE_ID"].Value != null)
                                {
                                    DataRow invoice = (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_get_by_id", new object[] { new MySqlParameter("_ID", dgvr.Cells["INVOICE_ID"].Value) })).ExecuteSelectQuery().Tables[0].Rows[0];
                                    if (invoice != null)
                                    {
                                        ;//double invoice_amount_paid = CommonFunctions.ConvertCurrency(
                                    }
                                    (new DataAccess(CommandType.StoredProcedure, "INVOICESsp_change_status", new object[]{
                                    new MySqlParameter("_ID", dgvr.Cells["INVOICE_ID"].Value),
                                    //new MySqlParameter("_AMOUNT_PAID", Math.Abs(Convert.ToDouble(dgvr.Cells["AMOUNT"].Value)))
                                    //new MySqlParameter("_AMOUNT_PAID", Math.Abs(amount))
                                    new MySqlParameter("_AMOUNT_PAID", Math.Abs(final_amount))
                                })).ExecuteUpdateQuery();
                                }
                            }
                            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                            */

                            try
                            {
                                ////dataGridView["BALLANCE", dataGridView.CurrentRow.Index].Value = Convert.ToDouble(dataGridView["BALLANCE", dataGridView.CurrentRow.Index].Value) + Convert.ToDouble(dgvr.Cells["AMOUNT"].Value);
                                //dataGridView["BALLANCE", dataGridView.CurrentRow.Index].Value = Convert.ToDouble(dataGridView["BALLANCE", dataGridView.CurrentRow.Index].Value) - Math.Abs(amount);
                                dataGridView["IMPORTED", dataGridView.CurrentRow.Index].Value = true;
                            }
                            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                            //Modifiy balance in imported file data

                            left_to_substract = left_to_substract - final_amount;
                            if (left_to_substract <= 0) break;
                        }
                    }

                    radioButtonManualExpense.Checked = false;
                    radioButtonManualIncome.Checked = false;
                    base.ShowConfirmationDialog(Language.GetMessageBoxText("valueImported", "Value imported!"));
                    //toolStripButtonImport.PerformClick();
                    FillPredictedIncomeExpenses();
                }
            }
            else
            {
                if (!ValidateData())
                {
                    base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                    return;
                }
                DateTime data = DateTime.Now.Date;
                try
                {
                    //data = Convert.ToDateTime(dataGridView["data tranzactiei", dataGridView.CurrentRow.Index].Value);
                    data = CommonFunctions.SwitchBackFormatedDate(dataGridView["data tranzactiei", dataGridView.CurrentRow.Index].Value.ToString());
                }
                catch { }
                double amount = Math.Abs(Convert.ToDouble(userTextBoxTotal.Text));
                double amount_from_extras = Math.Abs(Convert.ToDouble(dataGridView["suma", dataGridView.CurrentRow.Index].Value));

                try
                {
                    /*
                    string property = toolStripComboBoxProperty.SelectedItem.ToString();
                    PropertyId = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", property) })).ExecuteScalarQuery().ToString();
                    */
                    OwnerId = checkBoxNotOwnerRelated.Checked ? null : comboBoxOwner.SelectedValue;

                    object bankextract_id = DBNull.Value;
                    try
                    {
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "BANK_RECEIPTSsp_insert", new object[]{
                            new MySqlParameter("_EXTRACT_NUMBER", "BE"), // TO DO: GET EXTRACT NUMBER FROM FILE NAME
                            new MySqlParameter("_EXTRACT_DATE", DateTime.Now.Date), //TO DO: GET EXTRACT DATE FROM FILE NAME
                            new MySqlParameter("_DATE", data),
                            new MySqlParameter("_INVOICE_ID", null),
                            //new MySqlParameter("_AMOUNT_PAID", amount),
                            new MySqlParameter("_AMOUNT_PAID", amount_from_extras),
                            new MySqlParameter("_DESCRIPTION", textBoxDescription.Text),
                            new MySqlParameter("_OWNER_ID", OwnerId),
                            new MySqlParameter("_PROPERTY_ID", PropertyId),
                            //new MySqlParameter("_INCOME_EXPENSE_ID", DBNull.Value), // ---- !!! - M:M relation !!!  ----
                            new MySqlParameter("_CURRENCY", userTextBoxCurrency.Text.Trim()),
                            new MySqlParameter("_COMMENTS", textBoxDescription.Text),
                            new MySqlParameter("_BANK_ACCOUNT_DETAILS", BankAccount)
                        });
                        bankextract_id = Convert.ToInt32(da.ExecuteSelectQuery().Tables[0].Rows[0]["id"]);
                        //Insert bank receipt
                    }
                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                    //Insert bank receipt    

                    int property_count = checkedComboBoxProperties.CheckedItems.Count;
                    if (!checkBoxNotOwnerRelated.Checked)
                    {
                        foreach (CCBoxItem ci in checkedComboBoxProperties.CheckedItems)
                        {
                            //PropertyId = checkBoxNotPropertyRelated.Checked ? null : comboBoxProperty.SelectedValue;
                            PropertyId = Convert.ToInt32(ci.Value) == -1 ? null : ci.Value.ToString();
                            DataRow new_ie = IncomeExpensesClass.InsertIE(
                                true,
                                radioButtonManualExpense.Checked ? false : true,
                                true, // for bank
                                dataGridView.CurrentRow.Cells["valuta"].Value.ToString().ToUpper(),
                                Math.Round(Convert.ToDouble(userTextBoxManualExpenseAmount.Text) / property_count, 2),
                                data,
                                OwnerId,
                                PropertyId,
                                null,
                                textBoxDescription.Text,
                                null,
                                null,
                                null,
                                null,
                                (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(),
                                /*
                                AccountField,
                                amount_from_extras,
                                (amount - amount_from_extras),
                                Convert.ToDouble(userTextBoxVat.Text),
                                Convert.ToDouble(userTextBoxTotal.Text)
                                */
                                null,
                                null,
                                        // -- we split the amounts for multiple properties !!! - from 16.05.2016 --
                                Math.Round(amount / property_count, 2),
                                Math.Round(Convert.ToDouble(userTextBoxVat.Text) / property_count, 2),
                                Math.Round(Convert.ToDouble(userTextBoxTotal.Text) / property_count, 2)
                                );

                            (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_change_status3", new object[]{
                                    new MySqlParameter("_ID", new_ie["ID"]),
                                    new MySqlParameter("_SOURCE", true),
                                    new MySqlParameter("_AMOUNT", Math.Round(amount/property_count,2)),
                                    new MySqlParameter("_AMOUNT_PAID", Currency == "RON" ? 0 : Math.Round(amount/property_count,2)),
                                    new MySqlParameter("_AMOUNT_PAID_RON", Currency == "RON" ? Math.Round(amount/property_count,2) : 0),
                                    new MySqlParameter("_BANK_ACCOUNT_DETAILS", BankAccount),
                                    new MySqlParameter("_RECEIPT_ID", DBNull.Value),
                                    new MySqlParameter("_BANKEXTRACT_ID", bankextract_id)
                                })).ExecuteUpdateQuery();
                        }
                    }
                    else // only checkBoxNotOwnerRelated can by unchecked
                    {
                        PropertyId = null;
                        DataRow new_ie = IncomeExpensesClass.InsertIE(
                            true,
                            radioButtonManualExpense.Checked ? false : true,
                            true, // for bank
                            dataGridView.CurrentRow.Cells["valuta"].Value.ToString().ToUpper(),
                            Convert.ToDouble(userTextBoxManualExpenseAmount.Text),
                            data,
                            OwnerId,
                            PropertyId,
                            null,
                            textBoxDescription.Text,
                            null,
                            null,
                            null,
                            null,
                            (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", "Predicted"), new MySqlParameter("_LIST_TYPE", "ie_status") })).ExecuteScalarQuery(),
                            /*
                            AccountField,
                            amount_from_extras,
                            (amount - amount_from_extras),
                            Convert.ToDouble(userTextBoxVat.Text),
                            Convert.ToDouble(userTextBoxTotal.Text)
                            */
                            null,
                            null,
                            Math.Round(amount, 2),
                            Math.Round(Convert.ToDouble(userTextBoxVat.Text), 2),
                            Math.Round(Convert.ToDouble(userTextBoxTotal.Text), 2)
                            );

                        (new DataAccess(CommandType.StoredProcedure, "COMPANY_INCOME_EXPENSESsp_change_status3", new object[]{
                                    new MySqlParameter("_ID", new_ie["ID"]),
                                    new MySqlParameter("_SOURCE", true),
                                    new MySqlParameter("_AMOUNT", amount),
                                    new MySqlParameter("_AMOUNT_PAID", Currency == "RON" ? 0 : amount),
                                    new MySqlParameter("_AMOUNT_PAID_RON", Currency == "RON" ? amount : 0),
                                    new MySqlParameter("_BANK_ACCOUNT_DETAILS", BankAccount),
                                    new MySqlParameter("_RECEIPT_ID", DBNull.Value),
                                    new MySqlParameter("_BANKEXTRACT_ID", bankextract_id)
                                })).ExecuteUpdateQuery();
                    }
                    try
                    {
                        //dataGridView["BALLANCE", dataGridView.CurrentRow.Index].Value = Convert.ToDouble(dataGridView["BALLANCE", dataGridView.CurrentRow.Index].Value) - amount;
                        dataGridView["IMPORTED", dataGridView.CurrentRow.Index].Value = true;
                    }
                    catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
                    //Modifiy balance in imported file data

                    base.ShowConfirmationDialog(Language.GetMessageBoxText("valueImported", "Value imported!"));
                    userTextBoxManualExpenseAmount.Text = "";
                    radioButtonManualExpense.Checked = false;
                    radioButtonManualIncome.Checked = false;
                }
                catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
            }
            dataSetFromCSV.AcceptChanges();
            dataSetFromCSV.WriteXml(FileToSave, XmlWriteMode.IgnoreSchema);
        }

        private bool ValidateData()
        {
            bool toReturn = true;
            errorProvider1.SetError(toolStrip1, "");
            errorProvider1.SetError(comboBoxOwner, "");
            errorProvider1.SetError(comboBoxProperty, "");
            errorProvider1.SetError(userTextBoxTotal, "");
            base.ErrorList.Clear();
            base.listBoxErrors.DataSource = null;

            if ((comboBoxOwner.SelectedItem == null || comboBoxOwner.SelectedItem.ToString().Trim() == "") && !checkBoxNotOwnerRelated.Checked)
            {
                errorProvider1.SetError(comboBoxOwner, Language.GetErrorText("errorEmptyOwner", "You must select the owner!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwner.Name, Language.GetErrorText("errorEmptyOwner", "You must select the owner!")));
                toReturn = false;
            }
            /*
            if ((toolStripComboBoxProperty.SelectedItem == null || toolStripComboBoxProperty.SelectedItem.ToString().Trim() == "") && (!checkBoxNotPropertyRelated.Checked && !checkBoxNotOwnerRelated.Checked))
            {
                errorProvider1.SetError(comboBoxProperty, Language.GetErrorText("errorEmptyProperty", "You must select the property!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxProperty.Name, Language.GetErrorText("errorEmptyProperty", "You must select the property!")));
                toReturn = false;
            }
            */
            if (!Validator.IsDouble(userTextBoxTotal.Text))
            {
                errorProvider1.SetError(userTextBoxTotal, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(userTextBoxTotal.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
                toReturn = false;
            }
            return toReturn;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime extract_row_date = CommonFunctions.SwitchBackFormatedDate(dataGridView.CurrentRow.Cells["DATA TRANZACTIEI"].Value.ToString());
                CalculateAndFillRonValues(extract_row_date);
                userTextBoxExchangeRate.Text = CurrenciesClass.GetExchangeRate(extract_row_date, dataGridView.CurrentRow.Cells["VALUTA"].Value.ToString()).ToString(SettingsClass.DoubleFormat);
                if (dataGridView["FULL_DESCRIPTION", dataGridView.CurrentRow.Index].Value.ToString() == "")
                {
                    dataGridView["FULL_DESCRIPTION", dataGridView.CurrentRow.Index].Value = textBoxDescription.Text = CreateStringFromDescriptions();
                }
                else
                {
                    textBoxDescription.Text = dataGridView["FULL_DESCRIPTION", dataGridView.CurrentRow.Index].Value.ToString();
                }
            }
            catch { }
        }

        private void CalculateAndFillRonValues(DateTime date)
        {
            foreach (DataGridViewRow dgvr in dataGrid1.dataGridView.Rows)
            {
                // get exchange rate:
                //DateTime extract_row_date = CommonFunctions.SwitchBackFormatedDate(dataGridView.SelectedRows[0].Cells["DATA TRANZACTIEI"].Value.ToString());
                //double exchange_rate = Currencies.GetExchangeRate(extract_row_date, dgvr.Cells["CURRENCY"].Value.ToString());

                double exchange_rate = 1;
                DateTime invoice_date = date;
                try
                {
                    invoice_date = Convert.ToDateTime(dgvr.Cells["INVOICE_DATE"].Value); // Din 13.05.2016 - nu mai folosim Bank Extract date, ci invoice date!
                }
                catch { }
                if (dgvr.Cells["CURRENCY"].Value.ToString().ToLower() != "ron")
                {
                    //exchange_rate = CurrenciesClass.GetExchangeRate(date, dgvr.Cells["CURRENCY"].Value.ToString());
                    exchange_rate = CurrenciesClass.GetExchangeRate(invoice_date, dgvr.Cells["CURRENCY"].Value.ToString());
                }
                dgvr.Cells["EXCHANGE_RATE"].Value = exchange_rate;
                //dgvr.Cells["AMOUNT_EXCHANGED"].Value = Math.Round(exchange_rate * Convert.ToDouble(dgvr.Cells["AMOUNT"].Value), 2);
                //dgvr.Cells["AMOUNT_EXCHANGED"].Value = CommonFunctions.ConvertCurrency(Convert.ToDouble(dgvr.Cells["AMOUNT"].Value), dgvr.Cells["CURRENCY"].Value.ToString(), dataGridView["VALUTA", dataGridView.CurrentRow.Index].Value.ToString(), date);
                dgvr.Cells["AMOUNT_EXCHANGED"].Value = CommonFunctions.ConvertCurrency(Convert.ToDouble(dgvr.Cells["AMOUNT"].Value), dgvr.Cells["CURRENCY"].Value.ToString(), dataGridView["VALUTA", dataGridView.CurrentRow.Index].Value.ToString(), invoice_date);
            }
        }

        private void dataGridViewPredictedIE_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            switch (dataGrid1.dataGridView.Columns[e.ColumnIndex].Name.ToLower())
            {
                case "amount":
                    dataGrid1.dataGridView[dataGrid1.dataGridView.Columns["AMOUNT_EXCHANGED"].Index, e.RowIndex].Value =
                        Math.Round(Convert.ToDouble(dataGrid1.dataGridView[e.ColumnIndex, e.RowIndex].Value) * Convert.ToDouble(dataGrid1.dataGridView[dataGrid1.dataGridView.Columns["exchange_rate"].Index, e.RowIndex].Value), 2);
                    break;
                case "amount_exchanged":
                    dataGrid1.dataGridView[dataGrid1.dataGridView.Columns["amount"].Index, e.RowIndex].Value =
                        Math.Round(Convert.ToDouble(dataGrid1.dataGridView[e.ColumnIndex, e.RowIndex].Value) / Convert.ToDouble(dataGrid1.dataGridView[dataGrid1.dataGridView.Columns["exchange_rate"].Index, e.RowIndex].Value), 2);
                    break;
            }
        }

        private void dataGridViewPredictedIE_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (((DataGridView)sender).Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex != -1)
                {
                    string id = "";
                    string column_header = "";
                    try
                    {
                        column_header = String.Format("{0}_ID", ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText.Replace(" ", "_").ToUpper());
                    }
                    catch
                    {
                        id = ((DataGridView)sender).Rows[e.RowIndex].Cells["id"].Value.ToString();
                        column_header = ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText;
                    }
                    switch (column_header)
                    {
                        case "INVOICE_REQUIREMENT_ID":
                        case "INVOICE REQUIREMENT":
                            //int id_column_index = ((DataGridView)sender).Columns[column_header].Index;
                            id = ((DataGridView)sender).Rows[e.RowIndex].Cells["INVOICEREQUIREMENT_ID"].Value.ToString();
                            var o = new InvoiceRequirements(Convert.ToInt32(id));
                            o.StartPosition = FormStartPosition.CenterScreen;
                            o.StartPosition = FormStartPosition.CenterScreen;
                            o.buttonSave.Enabled = false;
                            o.ShowInTaskbar = true;
                            o.BringToFront();
                            o.ShowDialog();
                            o.Dispose();
                            break;
                        case "INVOICE_ID":
                        case "INVOICE":
                            id = ((DataGridView)sender).Rows[e.RowIndex].Cells["INVOICE_ID"].Value.ToString();
                            var i = new Invoices(Convert.ToInt32(id));
                            i.StartPosition = FormStartPosition.CenterScreen;
                            i.buttonSave.Enabled = false;
                            i.buttonIncasare.Enabled = false;
                            i.toolStrip1.Enabled = false;
                            i.ShowInTaskbar = true;
                            i.BringToFront();
                            i.ShowDialog();
                            i.Dispose();
                            break;
                    }
                }
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }

        private void checkBoxManual_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = ((CheckBox)sender).Checked;
            dataGrid1.dataGridView.Enabled = !((CheckBox)sender).Checked;
            //toolStripLabelProperty.Visible = ((CheckBox)sender).Checked;
            //toolStripComboBoxProperty.Visible = ((CheckBox)sender).Checked;
            if(((CheckBox)sender).Checked)
                FillCombos();
        }

        private void textBoxDescription_Validated(object sender, EventArgs e)
        {
            dataGridView["FULL_DESCRIPTION", dataGridView.CurrentRow.Index].Value = textBoxDescription.Text;
        }

        private void checkBoxUseVat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonManualIncome.Checked || radioButtonManualExpense.Checked)
                {
                    if (checkBoxUseVat.Checked)
                    {
                        userTextBoxTotal.Text = userTextBoxManualExpenseAmount.Text;
                        userTextBoxManualExpenseAmount.Text = Math.Round(Convert.ToDouble(userTextBoxTotal.Text) / (1 + Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100), 2).ToString();
                        userTextBoxVat.Text = Math.Round(Convert.ToDouble(userTextBoxTotal.Text) - Convert.ToDouble(userTextBoxManualExpenseAmount.Text), 2).ToString();
                        //userTextBoxVat.Text = Math.Round(Convert.ToDouble(userTextBoxManualExpenseAmount.Text) * Convert.ToDouble(SettingsClass.GetCompanySetting("VAT (%)")) / 100, 2).ToString();
                        //userTextBoxTotal.Text = (Convert.ToDouble(userTextBoxManualExpenseAmount.Text) + Convert.ToDouble(userTextBoxVat.Text)).ToString();
                    }
                    else
                    {
                        userTextBoxVat.Text = "0";
                        userTextBoxTotal.Text = userTextBoxManualExpenseAmount.Text;
                    }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                errorProvider1.SetError(checkBoxManual, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(checkBoxManual.Name, Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!")));
            }
        }

        private void comboBoxOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (((ComboBox)sender).SelectedIndex > 0)
                {
                    DataRowView drv = (DataRowView)((ComboBox)sender).SelectedItem;
                    int _owner_id = Convert.ToInt32(drv.Row["id"]);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_select_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", _owner_id) });
                    DataTable dtProperties = da.ExecuteSelectQuery().Tables[0];
                    if (dtProperties != null)
                    {
                        comboBoxProperty.DisplayMember = "name";
                        comboBoxProperty.ValueMember = "id";
                        comboBoxProperty.DataSource = dtProperties;
                        checkedComboBoxProperties.Items.Clear();
                        foreach (DataRow dr in dtProperties.Rows)
                        {
                            CCBoxItem item = new CCBoxItem(dr["name"].ToString(), Convert.ToInt32(dr["id"]));
                            checkedComboBoxProperties.Items.Add(item);
                        }
                        checkedComboBoxProperties.Items.Add(new CCBoxItem("Not property related", -1));
                    }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void checkBoxNotOwnerRelated_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxOwner.Enabled = comboBoxProperty.Enabled = !checkBoxNotOwnerRelated.Checked;
        }

        private void userTextBoxManualExpenseAmount_Validated(object sender, EventArgs e)
        {
            checkBoxUseVat_CheckedChanged(null, null);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
