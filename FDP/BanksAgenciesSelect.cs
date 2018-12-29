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
    public partial class BanksAgenciesSelect : UserForm
    {
        //this.dataGrid1 = new FDP.DataGrid("BANKSsp_select", null, "BANKSsp_insert", null, "BANKSsp_update", null, "BANKSsp_delete", null, null, null, null, null, null, null, this.Selectable, false);        
        public bool Selectable = false;

        public int IdToReturn
        {
            get;
            set;
        }

        public BanksAgenciesSelect()
        {
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
        }

        public BanksAgenciesSelect(bool selectable)
        {
            Selectable = selectable;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
        }

        public void buttonAdd_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            DataRow bank = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).NewRow();
            //var f = new DynamicFormLists("banks", bank, 0);
            var f = new Banks(bank);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            if (f.DialogResult == DialogResult.OK)
            {
                try
                {
                    bank = f.NewDR;
                    try
                    {
                        bank["agencies"] = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_get_agencies", new object[] { new MySqlParameter("_ID", bank["id"]) })).ExecuteScalarQuery().ToString();
                    }
                    catch { }
                    object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(bank.Table, bank.ItemArray, 0);
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).Rows.Add(bank);
                    dataGrid1.da.AttachInsertParams(mySqlParams);
                    dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                    ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();

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

        public void buttonEdit_Click(object sender, EventArgs e)
        {
            SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            Application.DoEvents();
            if (dataGrid1.dataGridView.SelectedRows.Count > 0)
            {
                DataRow bank = ((DataRowView)dataGrid1.dataGridView.SelectedRows[0].DataBoundItem).Row;
                //var f = new DynamicFormLists("banks", bank, 1);
                var f = new Banks(bank);
                f.StartPosition = FormStartPosition.CenterScreen;
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        bank = f.NewDR;
                        try
                        {
                            bank["agencies"] = (new DataAccess(CommandType.StoredProcedure, "BANKSsp_get_agencies", new object[] { new MySqlParameter("_ID", bank["id"]) })).ExecuteScalarQuery().ToString();
                        }
                        catch { }
                        object[] mySqlParams = dataGrid1.da.GenerateMySqlParameters(bank.Table, bank.ItemArray, 1);
                        dataGrid1.da.AttachUpdateParams(mySqlParams);
                        dataGrid1.da.mySqlDataAdapter.Update(((DataTable)dataGrid1.da.bindingSource.DataSource));
                        ((DataTable)dataGrid1.da.bindingSource.DataSource).AcceptChanges();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the record:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).RejectChanges();
                f.Dispose();
            }
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

        private void BanksAgenciesSelect_Load(object sender, EventArgs e)
        {
            dataGrid1.toolStripButtonAdd.Visible = false;
            dataGrid1.toolStripButtonEdit.Visible = false;
            dataGrid1.toolStripButtonDelete.Visible = false;
        }
    }
}
