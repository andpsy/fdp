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
    public partial class ProportySelect : UserForm
    {
        //public DataAccess daProperties = new DataAccess("PROPERTIESsp_select", "PROPERTIESsp_insert", "PROPERTIESsp_update", "PROPERTIESsp_delete");
        public bool Selectable = false;
        public DataRow OldRow
        {
            get;
            set;
        }
        public int IdToReturn
        {
            get;
            set;
        }

        public ProportySelect()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            //Language.LoadLabels(this);
        }

        public ProportySelect(bool selectable)
        {
            base.Maximized = FormWindowState.Maximized;
            Selectable = selectable;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            //Language.LoadLabels(this);
        }
        
        public ProportySelect(int id, string parameter_name)
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
            //Language.LoadLabels(this);
            switch (parameter_name.ToLower())
            {
                case "contract_id":
                    dataGrid1.da = new DataAccess("PROPERTIESsp_select_by_contract_id", new object[] { new MySqlParameter("_CONTRACT_ID", id) }, "PROPERTIESsp_insert", null, "PROPERTIESsp_update", null, "PROPERTIESsp_delete", null);
                    //dataGrid1.da.selectCommand.CommandText = "PROPERTIESsp_select_by_contract_id";
                    //dataGrid1.da.AttachSelectParams(new object[] { new MySqlParameter("_CONTRACT_ID", id) });
                    dataGrid1.dataGridView.DataSource = dataGrid1.da.bindingSource;
                    dataGrid1.Refresh();
                    break;
            }
        }
        
        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow property = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new Property(property);
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
            Property f = (Property)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow property = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(property.Table, property.ItemArray, 0);
                    
                    try
                    {
                        property["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", property["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        property["type"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", property["type_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        property["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", property["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        property["project"] = (new DataAccess(CommandType.StoredProcedure, "PROJECTSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", property["project_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        property["city"] = (new DataAccess(CommandType.StoredProcedure, "CITIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", property["city_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        property["parent_property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", property["parent_property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    
                    if (((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.IndexOf(property) < 0)
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(property);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    PropertyChangeStatus(property["id"], property["status_id"]);

                    //int property_id = Convert.ToInt32(((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).Select("id = max(id)")[0]["id"]);
                    //InvoiceRequirementsClass.InsertFromProperty(((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).Select("id = max(id)")[0]); // NOT THE CASE BECAUSE THERE CAN NOT BE A CONTRACT WITHOUT THE PROPERTY
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Remove(property);
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
            DataRow property = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
            OldRow = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new Property(property);
            for (int i = 0; i < OldRow.ItemArray.Length; i++)
            {
                OldRow[i] = property[i];
            }
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

        public void SaveEditRecord()
        {
            Property f = (Property)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow property = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(property.Table, property.ItemArray, 1);
                    
                    try
                    {
                        property["status"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", property["status_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        property["type"] = (new DataAccess(CommandType.StoredProcedure, "LISTSsp_get_by_id", new object[] { new MySqlParameter("_ID", property["type_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        property["owner"] = (new DataAccess(CommandType.StoredProcedure, "OWNERSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", property["owner_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        property["project"] = (new DataAccess(CommandType.StoredProcedure, "PROJECTSsp_get_name_by_id", new object[] { new MySqlParameter("_ID", property["project_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        property["city"] = (new DataAccess(CommandType.StoredProcedure, "CITIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", property["city_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    try
                    {
                        property["parent_property"] = (new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", property["parent_property_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    
                    dataGrid1.da.AttachUpdateParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    PropertyChangeStatus(property["id"], property["status_id"]);
                    InvoiceRequirementsClass.InsertFromProperty(property, OldRow);
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Remove(property);
                }
            }
            else
            {
                ((DataTable)dataGrid1.da.bindingSource.DataSource).RejectChanges();
            }
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

        private void PropertyChangeStatus(object property_id, object status_id)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "PROPERTIESsp_change_status", new object[] { new MySqlParameter("_ID", property_id), new MySqlParameter("_STATUS_ID", status_id) });
            da.ExecuteUpdateQuery();
        }
    }
}
