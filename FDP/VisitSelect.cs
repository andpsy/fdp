using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FDP
{
    public partial class VisitSelect : UserForm
    {
        //this.dataGrid1 = new DataGrid("VISITSsp_select", null, "VISITSsp_insert", null, "VISITSsp_update", null, "VISITSsp_delete", null, new string[]{"DATE"}, null, null, new string[] { "PROPERTY_ID", "VISITREASON_ID", "EMPLOYEE_ID"}, new string[] { "PROPERTY"}, new string[] { "DATE", "PROPERTY", "REASON", "EMPLOYEE"}, this.Selectable, false);
        //public DataAccess daCoOwners = new DataAccess("CO_OWNERSsp_select", "CO_OWNERSsp_insert", "CO_OWNERSsp_update", " CO_OWNERSsp_delete");
        //public CheckedListBox columns = new CheckedListBox();
        public bool Selectable = false;

        public VisitSelect()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.dataGridView.RowEnter += new DataGridViewCellEventHandler(dataGridView_RowEnter);
            //Language.LoadLabels(this);

            //dataGridViewCoOwners.BindingContextChanged += new EventHandler(dataGridViewCoOwners_BindingContextChanged);
            //dataGridViewCoOwners.KeyDown += new KeyEventHandler(dataGridViewCoOwners_KeyDown);
            //dataGridViewCoOwners.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridViewCoOwners_DataBindingComplete);
            //dataGridViewCoOwners.DataSourceChanged += new EventHandler(dataGridViewCoOwners_DataSourceChanged);
            //toolStripStatusLabelShowAll.Click += new EventHandler(toolStripStatusLabelShowAll_Click);
        }

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow visit = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new Visits(visit);
            EditMode = 1; // ADD
            main m = FindMainForm();
            f.Launcher = this;
            this.ChildLaunched = f;
            f.TopLevel = false;
            f.MdiParent = m;
            m.panelMain.Controls.Add(f);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.BringToFront();
            f.Show();
            //f.ShowDialog();
        }

        public void SaveAddRecord()
        {
            Visits f = (Visits)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow visit = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(visit.Table, visit.ItemArray, 0);
                    /*
                    try
                    {
                        visit["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", visit["property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        visit["employee"] = (new DataAccess(CommandType.StoredProcedure, "EMPLOYEESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", visit["employee_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    */
                    try
                    {
                        visit["reason"] = (new DataAccess(CommandType.StoredProcedure, "VISITREASONSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", visit["visitreason_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(visit);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();

                    //if (visit["reason"].ToString() == "Snagging" || visit["reason"].ToString() == "Furnish")
                    if (visit["reason"].ToString().ToLower() == "snagging" || visit["reason"].ToString().ToLower() == "tenant check-out")
                    {
                        DataRow property = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[] { new MySqlParameter("_ID", visit["property_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                        InvoiceRequirementsClass.InsertPunctualService(property, visit["reason"].ToString(), 2);
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
            //f.Dispose();
        }

        public void buttonEdit_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            if (dataGrid1.dataGridView.SelectedRows.Count > 0)
            {
                DataRow visit = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
                var f = new Visits(visit);
                EditMode = 2; // EDIT
                main m = FindMainForm();
                f.Launcher = this;
                this.ChildLaunched = f;
                f.TopLevel = false;
                f.MdiParent = m;
                m.panelMain.Controls.Add(f);
                f.StartPosition = FormStartPosition.CenterScreen;
                f.BringToFront();
                f.Show();
                //f.ShowDialog();
            }
        }

        public void SaveEditRecord()
        {
            Visits f = (Visits)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow visit = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(visit.Table, visit.ItemArray, 1);
                    /*
                    try
                    {
                        visit["property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", visit["property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        visit["employee"] = (new DataAccess(CommandType.StoredProcedure, "EMPLOYEESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", visit["employee_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    */
                    try
                    {
                        visit["reason"] = (new DataAccess(CommandType.StoredProcedure, "VISITREASONSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", visit["visitreason_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    dataGrid1.da.AttachUpdateParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();

                    if (visit["reason"].ToString() == "Snagging" || visit["reason"].ToString() == "Furnish")
                    {
                        DataRow property = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[] { new MySqlParameter("_ID", visit["property_id"]) })).ExecuteSelectQuery().Tables[0].Rows[0];
                        InvoiceRequirementsClass.UpdatePunctualService(property, visit["reason"].ToString(), 2);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).RejectChanges();
            //f.Dispose();
        }

        public void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid1.dataGridView.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        int key = Convert.ToInt32(dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value);
                        dataGrid1.da.AttachDeleteParams(new object[] { new MySqlParameter("_ID", dataGrid1.dataGridView["id", dataGrid1.dataGridView.SelectedRows[0].Index].Value) });
                        DataRow dr = ((DataTable)dataGrid1.da.bindingSource.DataSource).Select(String.Format("ID = {0}", key))[0];

                        try
                        {
                            DataRow property = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_by_id", new object[]{new MySqlParameter("_ID", dr["property_id"])})).ExecuteSelectQuery().Tables[0].Rows[0];
                            int service_id = Convert.ToInt32(new DataAccess(CommandType.StoredProcedure, "SERVICESsp_get_id_by_name", new object[] { new MySqlParameter("_NAME", dr["service"]) }).ExecuteScalarQuery());
                            InvoiceRequirementsClass.DeletePunctualService(property, service_id, 2);
                        }
                        catch { }

                        dr.Delete();
                        dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();


                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void VisitSelect_Load(object sender, EventArgs e)
        {
            //FillCoOwners();
            //GenerateColumnsListBox(dataGridViewCoOwners);
            dataGrid1.AddToolStripButton(Language.GetLabelText("VisitSelect.toolStripButtonGenerateMinutes", "Minutes"), new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "2.png")), new EventHandler(toolStripButtonGenerateMinutes_Click), "toolStripButtonGenerateMinutes", 1);
        }

        private void dataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string selected_reason = dataGrid1.dataGridView.Rows[e.RowIndex].Cells["reason"].Value.ToString();
                dataGrid1.toolStrip1.Items["toolStripButtonGenerateMinutes"].Enabled = (selected_reason == "Snagging" || selected_reason == "Three months inspection" || selected_reason == "Repairs");
            }
            catch { }
        }

        private void toolStripButtonGenerateMinutes_Click(object sender, EventArgs e)
        {
            // TO DO: GENERATE PDF PROCES VERBAL
            MessageBox.Show("NOT IMPLEMENTED !");
        }
    }
}
