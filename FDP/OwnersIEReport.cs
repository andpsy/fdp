using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
using System.IO;

namespace FDP   
{
    public partial class OwnersIEReport : UserForm
    {
        public OwnersIEReport()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void OwnersIEReport_Load(object sender, EventArgs e)
        {
            FillCombos();
            /*
            dataGrid1.toolStripButtonAdd.Visible = false;
            dataGrid1.toolStripButtonEdit.Visible = false;
            dataGrid1.toolStripButtonDelete.Visible = false;
            dataGrid1.toolStripButtonExit.Visible = false;
            dataGrid1.toolStripButtonRefresh.Visible = false;
            */
        }

        private void FillCombos()
        {
            DataAccess da = new DataAccess();

            da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_list");
            DataTable dtOwners = da.ExecuteSelectQuery().Tables[0];
            if (SettingsClass.LoginOwnerId > 0)
            {
                dtOwners.DefaultView.RowFilter = String.Format("ID={0} OR ID=-1", SettingsClass.LoginOwnerId);
            }

            if (dtOwners != null)
            {
                comboBoxOwner.DisplayMember = "name";
                comboBoxOwner.ValueMember = "id";
                comboBoxOwner.DataSource = dtOwners;
                if (SettingsClass.LoginOwnerId > 0)
                {
                    comboBoxOwner.SelectedIndex = 1;
                    comboBoxOwner.Enabled = false;
                }
            }
            /*
            da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name", new object[]{new MySqlParameter("_LIST_TYPE_NAME", "ie_status")});
            DataTable dtIEStatus = da.ExecuteSelectQuery().Tables[0];
            if (dtIEStatus != null)
            {
                comboBoxStatus.DisplayMember = "name";
                comboBoxStatus.ValueMember = "id";
                comboBoxStatus.DataSource = dtIEStatus;
            }
            */
            comboBoxStatus.SelectedIndex = 0;
            comboBoxSource.SelectedIndex = 0;
            comboBoxType.SelectedIndex = 0;
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
                        dataGridViewProperties.DataSource = dtProperties;
                        dataGridViewProperties.Columns.Remove("assigned");
                        DataGridViewCheckBoxColumn c = new DataGridViewCheckBoxColumn();
                        c.Name = "Assigned";
                        c.Width = 30;
                        c.HeaderText = "";
                        c.DataPropertyName = "assigned";
                        dataGridViewProperties.Columns.Insert(0, c);
                        foreach (DataGridViewColumn dc in dataGridViewProperties.Columns)
                        {
                            dc.ReadOnly = dc.Name.ToLower() == "assigned" ? false : true;
                            dc.Visible = (dc.Name.ToLower() == "name" || dc.Name.ToLower() == "assigned") ? true : false;
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }

            try
            {
                if (((ComboBox)sender).SelectedIndex > 0)
                {
                    DataRowView drv = (DataRowView)((ComboBox)sender).SelectedItem;
                    int _owner_id = Convert.ToInt32(drv.Row["id"]);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_GET_CURRENCIES", new object[] { new MySqlParameter("_OWNER_ID", _owner_id) });
                    DataTable dtCurrencies = da.ExecuteSelectQuery().Tables[0];
                    if (dtCurrencies != null)
                    {
                        comboBoxCurrency.ValueMember = "CURRENCY";
                        comboBoxCurrency.DisplayMember = "CURRENCY";
                        comboBoxCurrency.DataSource = dtCurrencies;
                        comboBoxCurrency.SelectedIndex = 0;
                    }
                }
            }
            catch { }

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateData())
                {
                    //this.DialogResult = DialogResult.Cancel;
                    base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
                    return;
                }
                splitContainer1.Panel2.Controls.Clear();
                TabControl tabControl1 = new TabControl();
                //tabControl1.TabPages.Clear();
                tabControl1.Dock = DockStyle.Fill;
                splitContainer1.Panel2.Controls.Add(tabControl1);

                if (comboBoxCurrency.SelectedIndex < 1)
                {
                    for (int i = 1; i < comboBoxCurrency.Items.Count; i++)
                    {
                        GenerateReport(tabControl1, ((DataRowView)comboBoxCurrency.Items[i])["currency"].ToString());
                    }
                }
                else
                {
                    GenerateReport(tabControl1, ((DataRowView)comboBoxCurrency.SelectedItem)["currency"].ToString());
                }
            }
            catch(Exception exp) { 
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(Language.GetMessageBoxText("errorGeneratingReport", "There was an error generating the report!"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateReport(TabControl tabControl1, string currency)
        {
            /*
            ArrayList parameters = new ArrayList();
            parameters.Add(new MySqlParameter("_OWNER_ID", comboBoxOwner.SelectedValue));
            parameters.Add(new MySqlParameter("_PROPERTY_ID", !checkBoxUseProperty.Checked || dataGridViewProperties.SelectedRows.Count < 0 || dataGridViewProperties.SelectedRows[0].Cells["ID"].Value == DBNull.Value || dataGridViewProperties.SelectedRows[0].Cells["ID"].Value == null ? null : dataGridViewProperties.SelectedRows[0].Cells["ID"].Value));
            parameters.Add(new MySqlParameter("_SOURCE", comboBoxSource.SelectedIndex <= 0 ? null : comboBoxSource.SelectedItem.ToString() == "CASH" ? "0" : "1"));
            parameters.Add(new MySqlParameter("_TYPE", comboBoxType.SelectedIndex <= 0 ? null : comboBoxType.SelectedItem.ToString() == "EXPENSE" ? "0" : "1"));
            parameters.Add(new MySqlParameter("_CURRENCY", comboBoxCurrency.SelectedIndex <= 0 ? null : comboBoxCurrency.SelectedValue.ToString()));
            parameters.Add(new MySqlParameter("_STATUS_ID", comboBoxStatus.SelectedIndex <= 0 ? null : comboBoxType.SelectedValue));
            parameters.Add(new MySqlParameter("_START_DATE", !checkBoxUseStartDate.Checked ? null : CommonFunctions.ToMySqlFormatDate(dateTimePickerStartDate.Value)));
            parameters.Add(new MySqlParameter("_END_DATE", !checkBoxUseEndDate.Checked ? null : CommonFunctions.ToMySqlFormatDate(dateTimePickerEndDate.Value)));

            DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_IE_REPORT", parameters.ToArray());
            DataTable dt = da.ExecuteSelectQuery().Tables[0];
            */

            ArrayList detailes_parameters = new ArrayList();
            detailes_parameters.Add(new MySqlParameter("_OWNER_ID", comboBoxOwner.SelectedValue));
            detailes_parameters.Add(new MySqlParameter("_PROPERTY_ID", !checkBoxUseProperty.Checked || dataGridViewProperties.SelectedRows.Count < 0 || dataGridViewProperties.SelectedRows[0].Cells["ID"].Value == DBNull.Value || dataGridViewProperties.SelectedRows[0].Cells["ID"].Value == null ? null : dataGridViewProperties.SelectedRows[0].Cells["ID"].Value));
            detailes_parameters.Add(new MySqlParameter("_SOURCE", comboBoxSource.SelectedIndex <= 0 ? null : comboBoxSource.SelectedItem.ToString() == "CASH" ? "0" : "1"));
            detailes_parameters.Add(new MySqlParameter("_TYPE", comboBoxType.SelectedIndex <= 0 ? null : comboBoxType.SelectedItem.ToString() == "EXPENSE" ? "0" : "1"));
            //detailes_parameters.Add(new MySqlParameter("_CURRENCY", comboBoxCurrency.SelectedIndex <= 0 ? null : comboBoxCurrency.SelectedValue.ToString()));
            detailes_parameters.Add(new MySqlParameter("_CURRENCY", currency));
            //detailes_parameters.Add(new MySqlParameter("_STATUS_ID", comboBoxStatus.SelectedIndex <= 0 ? null : comboBoxType.SelectedValue));
            detailes_parameters.Add(new MySqlParameter("_START_DATE", !checkBoxUseStartDate.Checked ? null : CommonFunctions.ToMySqlFormatDate(dateTimePickerStartDate.Value)));
            detailes_parameters.Add(new MySqlParameter("_END_DATE", !checkBoxUseEndDate.Checked ? null : CommonFunctions.ToMySqlFormatDate(dateTimePickerEndDate.Value)));

            DataAccess da = new DataAccess(CommandType.StoredProcedure, comboBoxStatus.SelectedIndex == 0 ? "OWNERSsp_IE_DETAILS_REPORT_N" : "OWNERSsp_IE_DETAILS_REPORT", detailes_parameters.ToArray());
            DataTable dtDetails = da.ExecuteSelectQuery().Tables[0];
            BindingSource bs = new BindingSource();
            bs.DataSource = dtDetails;

            //dataGrid1.Dispose();

            DataGrid dataGrid1 = new DataGrid(
                bs,
                new string[] { "DATE" },
                null,
                null,
                null,
                null,
                new string[] { "ALL" },
                false,
                false);
            dataGrid1.Name = String.Format("dataGrid1_{0}", currency);
            //dataGrid1.dataGridView.RowPostPaint -= dataGrid1.dataGridView_RowPostPaint;
            dataGrid1.dataGridView.CellContentClick += new DataGridViewCellEventHandler(dataGridViewPredictedIE_CellContentClick);
            //dataGrid1.dataGridView.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridViewPredictedIE_RowPostPaint);
            //dataGrid1.dataGridView.CellValidated += new DataGridViewCellEventHandler(dataGridViewPredictedIE_CellValidated);
            dataGrid1.Dock = DockStyle.Fill;
            //dataGrid1.Width = splitContainer1.Panel2.Width;
            //dataGrid1.Height = splitContainer1.Panel2.Height - panel2.Height - 5;
            //dataGrid1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            //dataGrid1.toolStrip1.Visible = false;
            //dataGrid1.dataGridView.ReadOnly = false;
            //splitContainer1.Panel2.Controls.Add(dataGrid1);
            TabPage tp1 = new TabPage(currency.ToUpper());
            tp1.Name = String.Format("tabPage_{0}", currency);
            tp1.Controls.Add(dataGrid1);
            tabControl1.TabPages.Add(tp1);

            foreach (DataGridViewColumn dgvc in dataGrid1.dataGridView.Columns)
            {
                if (dgvc.Name.ToLower().IndexOf("income") > -1 || dgvc.Name.ToLower().IndexOf("expense") > -1 || dgvc.Name.ToLower().IndexOf("ballance") > -1)
                {
                    dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvc.Name.ToLower().IndexOf("ballance") > -1)
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGrid1.dataGridView.Columns["OWNER_ID"].Visible = false;
            /* --- save html ---
            string file_name = Path.Combine(SettingsClass.PDFExportPath, "test.html");
            CommonFunctions.CreateFile(file_name, CommonFunctions.GenerateHTMLString(dtDetails));
            System.Diagnostics.Process.Start(file_name);
            */

            dataGrid1.toolStripButtonAdd.Visible = false;
            dataGrid1.toolStripButtonEdit.Visible = false;
            dataGrid1.toolStripButtonDelete.Visible = false;
            dataGrid1.toolStripButtonExit.Visible = false;
            dataGrid1.toolStripButtonRefresh.Visible = false;
        }
        
        private void dataGridViewPredictedIE_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (((DataGridView)sender)[e.ColumnIndex, e.RowIndex] is DataGridViewColumnHeaderCell)
                if(e.RowIndex == -1)
                {
                    string column_header = "";
                    try
                    {
                        column_header = ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText;
                    }
                    catch
                    {
                        return;
                    }
                    if(column_header.IndexOf("BALLANCE") > -1)
                    {
                        ((DataGridView)sender).Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                        return;
                    }
                    /*
                    if(column_header.IndexOf("DATE") < 0){
                        foreach (DataGridViewColumn dgvc in ((DataGridView)sender).Columns)
                        {
                            if (dgvc.HeaderText.IndexOf("BALLANCE") > -1)
                                dgvc.Visible = false;
                        }
                    }
                    */
                    foreach (DataGridViewColumn dgvc in ((DataGridView)sender).Columns)
                    {
                        if (dgvc.HeaderText.IndexOf("BALLANCE") > -1)
                            dgvc.Visible = (column_header.IndexOf("DATE") > -1 && ((DataGridView)sender).SortOrder == SortOrder.Ascending);
                    }
                }
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
            catch (Exception exp) { 
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                base.ErrorList.Add(new KeyValuePair<string, string>(buttonReport.Name, Language.GetErrorText("null", exp.Message)));
                base.ShowErrorsDialog(Language.GetMessageBoxText("errorsOnPage", "There are validation errors on the form! Please check the filled in information!"));
            }
        }

        private bool ValidateData()
        {
            bool toReturn = true;
            errorProvider1.SetError(comboBoxOwner, "");
            //errorProvider1.SetError(comboBoxCurrency, "");
            if (comboBoxOwner.SelectedValue == null || comboBoxOwner.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxOwner, Language.GetErrorText("errorEmptyOwner", "You must select the owner!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxOwner.Name, Language.GetErrorText("errorEmptyOwner", "You must select the owner!")));
                toReturn = false;
            }
            /*
            if (comboBoxCurrency.SelectedValue == null || comboBoxCurrency.SelectedIndex < 1)
            {
                errorProvider1.SetError(comboBoxCurrency, Language.GetErrorText("errorEmptyCurrency", "You must select the currency!"));
                base.ErrorList.Add(new KeyValuePair<string, string>(comboBoxCurrency.Name, Language.GetErrorText("errorEmptyCurrency", "You must select the currency!")));
                toReturn = false;
            }
            */
            return toReturn;
        }
    }
}
