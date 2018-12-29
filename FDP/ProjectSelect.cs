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
    public partial class ProjectSelect : UserForm
    {
        //this.dataGrid1 = new FDP.DataGrid("PROJECTSsp_select", null, "PROJECTSsp_insert", null, "PROJECTSsp_update", null, "PROJECTSsp_delete", null, null, null, null, new string[] { "CITY_ID" }, null, new string[] { "NAME", "LOCATION", "CITY", "ADDRESS" });
        //public DataAccess daProperties = new DataAccess("PROPERTIESsp_select", "PROPERTIESsp_insert", "PROPERTIESsp_update", "PROPERTIESsp_delete");
        public bool Selectable = false;

        public ProjectSelect()
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

        public ProjectSelect(bool selectable)
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
       
        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow project = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            var f = new Projects(project);
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
            Projects f = (Projects)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow project = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(project.Table, project.ItemArray, 0);
                    /*
                    try
                    {
                        project["city"] = (new DataAccess(CommandType.StoredProcedure, "CITIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", project["city_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    */
                    if (((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.IndexOf(project) < 0)
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(project);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Remove(project);
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
            DataRow project = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
            var f = new Projects(project);
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
            Projects f = (Projects)((main)FindMainForm()).Controls.Find(this.ChildLaunched.Name, true)[0];
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    DataRow project = f.NewDR;
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(project.Table, project.ItemArray, 1);
                    /*
                    try
                    {
                        project["city"] = (new DataAccess(CommandType.StoredProcedure, "CITIESsp_get_name_by_id", new object[] { new MySqlParameter("_ID", project["city_id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    */
                    dataGrid1.da.AttachUpdateParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Remove(project);
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
    }
}
