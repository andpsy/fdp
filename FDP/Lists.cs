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
    public partial class Lists : UserForm
    {
        public DataSet ListTypes = new DataSet();
        public DataSet ListContent = new DataSet();

        public Lists()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void Lists_Load(object sender, EventArgs e)
        {
            dataGridViewList.CellDoubleClick += new DataGridViewCellEventHandler(dataGridViewList_CellDoubleClick);
            dataGridViewList.DataSourceChanged += new EventHandler(dataGridViewList_DataSourceChanged);
            FillListTypes();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAddListType_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(userTextBoxNewListType.Text) || Validator.IsCharactersOnly(userTextBoxNewListType.Text))
                {
                    MessageBox.Show(Language.GetMessageBoxText("invalidListType", "Invalid List Type name!"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataAccess da = new DataAccess(CommandType.StoredProcedure, "LIST_TYPESsp_insert", new object[] { new MySqlParameter("_NAME", userTextBoxNewListType.Text) });
                da.ExecuteInsertQuery();
                FillListTypes();
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillListTypes()
        {
            //listBoxListTypes.Items.Clear();
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "LIST_TYPESsp_select");
            ListTypes = da.ExecuteSelectQuery();
            listBoxListTypes.DisplayMember = "name";
            listBoxListTypes.ValueMember = "id";
            listBoxListTypes.DataSource = ListTypes.Tables[0];           
        }

        private void buttonDeleteListType_Click(object sender, EventArgs e)
        {
            foreach (DataRowView objDataRowView in listBoxListTypes.SelectedItems)
            {
                try
                {
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "LIST_TYPESsp_delete", new object[] { new MySqlParameter("_ID", objDataRowView["id"]) });
                    da.ExecuteUpdateQuery();
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            FillListTypes();
        }

        private void listBoxListTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // validate if changes
            FillListContent( ((ListBox)sender).SelectedValue.ToString());
        }

        private void FillListContent()
        {
            FillListContent(listBoxListTypes.SelectedValue.ToString());
        }

        private void FillListContent(string list_type_id)
        {
            try
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_select_by_list_type_id", new object[] { new MySqlParameter("_LIST_TYPE_ID", list_type_id) });
                ListContent = da.ExecuteSelectQuery();
                dataGridViewList.DataSource = ListContent.Tables[0];
                dataGridViewList.Columns["LIST_TYPE_ID"].Visible = false;
                dataGridViewList.Columns["ID"].Visible = false;
            }
            catch(Exception exp) {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorSelectingRecord", "There was an error getting the record(s) from the database:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataRowView drv = (DataRowView)(((DataGridView)sender).Rows[e.RowIndex].DataBoundItem);
                CallUpdateListItem(drv.Row);
            }
        }

        private void dataGridViewList_DataSourceChanged(object sender, EventArgs e)
        {
            Language.PopulateGridColumnHeaders((DataGridView)sender);
        }

        private void buttonAddListItem_Click(object sender, EventArgs e)
        {
            DynamicFormLists df = new DynamicFormLists("lists", ListContent.Tables[0].NewRow(), 0);
            if (df.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    DataRow dr = df.return_data_row;
                    dr["list_type_id"] = listBoxListTypes.SelectedValue.ToString();
                    object[] mySqlParams = (new DataAccess()).GenerateMySqlParameters(dr.Table, dr.ItemArray, 0);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_insert", mySqlParams);
                    da.ExecuteInsertQuery();
                    FillListContent();
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorAddingRecord", "There was an error adding the record:\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            df.Dispose();
        }

        private void buttonEditListItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows[0].Index > -1)
            {
                DataRowView drv = (DataRowView)(dataGridViewList.SelectedRows[0].DataBoundItem);
                CallUpdateListItem(drv.Row);
            }
        }

        private void CallUpdateListItem(DataRow _dr)
        {
            DynamicFormLists df = new DynamicFormLists("lists", _dr, 1);
            if (df.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    DataRow dr = df.return_data_row;
                    dr["list_type_id"] = listBoxListTypes.SelectedValue.ToString();
                    object[] mySqlParams = (new DataAccess()).GenerateMySqlParameters(dr.Table, dr.ItemArray, 1);
                    DataAccess da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_update", mySqlParams);
                    da.ExecuteUpdateQuery();
                    FillListContent();
                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    MessageBox.Show(String.Format(Language.GetMessageBoxText("errorUpdatingRecord", "There was an error updating the selected record(s):\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            df.Dispose();
        }

        private void buttonDeleteListItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows[0].Index > -1)
            {
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmDeleteRecord", "Are you sure you want to delete the selected record?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ans == DialogResult.Yes)
                {
                    try
                    {
                        DataAccess da = new DataAccess(CommandType.StoredProcedure, "LISTSsp_delete", new object[] { new MySqlParameter("_ID", dataGridViewList["id", dataGridViewList.SelectedRows[0].Index].Value) });
                        da.ExecuteUpdateQuery();
                        FillListContent();
                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                        MessageBox.Show(String.Format(Language.GetMessageBoxText("errorDeletingRecord", "There was an error deleting the selected record(s):\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
