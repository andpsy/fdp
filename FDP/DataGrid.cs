using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace FDP
{
    public partial class DataGrid : UserControl
    {
        public DataAccess da = new DataAccess();
        public CheckedListBox columns = new CheckedListBox();
        public string[] DateColumns;
        public string[] DoubleColumns;
        public string[] BooleanColumns;
        public string[] ExternalColumns;
        public string[] LinkColumns;
        public string[] VisibleColumns;
        public bool Selectable = false;
        public bool GenerateComboBoxesForExternalColumns = false;
        public int last_found_column = -1;
        public int last_found_row = -1;

        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int colIndexFromMouseDown = -1; 
        public double Sum = 0;
        private int lastRowIndexFromMouseDown;

        public int IdToReturn{
            get;
            set;
        }

        public DataGrid()
        {
            InitializeComponent();
            GenerateColumnsListBox(this.dataGridView);
            GenerateLinkButtonColums(this.dataGridView);
            GenerateExternalColumns();
            buttonAdd.Visible = false;
            buttonEdit.Visible = false;
            buttonDelete.Visible = false;
            DisableControllsForOwnerLogin();
        }

        public DataGrid(
            string select_procedure, 
            object[] select_params,
            string insert_procedure, 
            object[] insert_params,
            string update_procedure, 
            object[] update_params,
            string delete_procedure,
            object[] delete_params,
            string[] date_columns,
            string[] double_columns,
            string[] boolean_columns,
            string[] external_columns,
            string[] link_columns,
            string[] visible_columns,
            bool selectable,
            bool generate_comboboxes_for_external_columns
            )
        {
            InitializeComponent();
            DateColumns = date_columns;
            DoubleColumns = double_columns;
            BooleanColumns = boolean_columns;
            ExternalColumns = external_columns;
            LinkColumns = link_columns;
            VisibleColumns = visible_columns;
            Selectable = selectable;
            GenerateComboBoxesForExternalColumns = generate_comboboxes_for_external_columns;
            da = new DataAccess(select_procedure, select_params, insert_procedure, insert_params, update_procedure, update_params, delete_procedure, delete_params);
            FillGrid();
            //LoadDefaultLayout();
            toolStripButtonEdit.Enabled = dataGridView.Rows.Count > 0;
            toolStripButtonDelete.Enabled = dataGridView.Rows.Count > 0;
            GenerateColumnsListBox(this.dataGridView);
            GenerateLinkButtonColums(this.dataGridView);
            GenerateExternalColumns();
            buttonAdd.Visible = false;
            buttonEdit.Visible = false;
            buttonDelete.Visible = false;
            buttonSelect.Visible = false;
            //buttonAdd.Visible = !Selectable;
            //buttonEdit.Visible = !Selectable;
            //buttonDelete.Visible = !Selectable;
            //buttonSelect.Visible = Selectable;
            toolStripButtonAdd.Visible = !Selectable;
            toolStripButtonEdit.Visible = !Selectable;
            toolStripButtonDelete.Visible = !Selectable;
            toolStripButtonSelect.Visible = Selectable;
            DisableControllsForOwnerLogin();
        }

        public DataGrid(
            BindingSource bs,
            string[] date_columns,
            string[] double_columns,
            string[] boolean_columns,
            string[] external_columns,
            string[] link_columns,
            string[] visible_columns,
            bool selectable,
            bool generate_comboboxes_for_external_columns
            )
        {
            InitializeComponent();
            DateColumns = date_columns;
            DoubleColumns = double_columns;
            BooleanColumns = boolean_columns;
            ExternalColumns = external_columns;
            LinkColumns = link_columns;
            VisibleColumns = visible_columns;
            Selectable = selectable;
            GenerateComboBoxesForExternalColumns = generate_comboboxes_for_external_columns;
            //da = new DataAccess(select_procedure, select_params, insert_procedure, insert_params, update_procedure, update_params, delete_procedure, delete_params);
            FillGrid(bs);
            //LoadDefaultLayout();
            toolStripButtonEdit.Enabled = dataGridView.Rows.Count > 0;
            toolStripButtonDelete.Enabled = dataGridView.Rows.Count > 0;
            GenerateColumnsListBox(this.dataGridView);
            GenerateLinkButtonColums(this.dataGridView);
            GenerateExternalColumns();
            buttonAdd.Visible = false;
            buttonEdit.Visible = false;
            buttonDelete.Visible = false;
            buttonSelect.Visible = false;
            //buttonAdd.Visible = !Selectable;
            //buttonEdit.Visible = !Selectable;
            //buttonDelete.Visible = !Selectable;
            //buttonSelect.Visible = Selectable;
            toolStripButtonAdd.Visible = !Selectable;
            toolStripButtonEdit.Visible = !Selectable;
            toolStripButtonDelete.Visible = !Selectable;
            toolStripButtonSelect.Visible = Selectable;
            DisableControllsForOwnerLogin();
        }

        private void DisableControllsForOwnerLogin()
        {
            if (SettingsClass.LoginOwnerId <= 0) return;
            this.toolStripButtonRefresh.Visible = toolStripSeparator1.Visible = this.toolStripButtonAdd.Visible = toolStripButtonEdit.Visible = toolStripButtonDelete.Visible = toolStripSeparator3.Visible = false;
        }

        private void saveLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridSaveLayout dgsv = new DataGridSaveLayout(dataGridView, this.ParentForm.Name);
            dgsv.ShowDialog();
            dgsv.Dispose();
        }

        private void openLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridLoadLayout dgll = new DataGridLoadLayout(this.ParentForm.Name);
            if (dgll.ShowDialog() == DialogResult.OK)
            {
                /*
                DataSet ds = new DataSet();
                ds.ReadXml(Path.Combine(SettingsClass.LayoutsPath, String.Format("{0}.xml", dgll.LayoutName)));
                DataTable dt = ds.Tables["GENERAL_INFORMATION"];
                DataTable dtColumns = ds.Tables["COLUMNS"];
                try
                {
                    ((BindingSource)dataGridView.DataSource).Filter = dt.Rows[0]["filter"].ToString();
                    foreach (DataGridViewColumn dgvc in dataGridView.Columns)
                    {
                        ((DataGridViewAutoFilterColumnHeaderCell)dgvc.HeaderCell).UpdateFilterFromLayout();
                    }
                }
                catch { }
                dataGridView.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)Enum.Parse(typeof(DataGridViewAutoSizeColumnsMode), dt.Rows[0]["layout_mode"].ToString());
                foreach (DataRow dr in dtColumns.Rows)
                {
                    dataGridView.Columns[dr["column_name"].ToString()].Visible = Convert.ToBoolean(dr["column_visible"]);
                    dataGridView.Columns[dr["column_name"].ToString()].Frozen = Convert.ToBoolean(dr["column_frozen"]);
                    dataGridView.Columns[dr["column_name"].ToString()].Width = Convert.ToInt32(dr["column_width"]);
                    dataGridView.Columns[dr["column_name"].ToString()].DisplayIndex = Convert.ToInt32(dr["displaynumber"]);
                }
                if (dt.Rows[0]["sort_column"] != null && dt.Rows[0]["sort_column"] != DBNull.Value && dt.Rows[0]["sort_column"].ToString().Trim() != "")
                {
                    dataGridView.Columns[dt.Rows[0]["sort_column"].ToString()].SortMode = DataGridViewColumnSortMode.Programmatic;
                    dataGridView.Sort(dataGridView.Columns[dt.Rows[0]["sort_column"].ToString()], (ListSortDirection)Enum.Parse(typeof(ListSortDirection), dt.Rows[0]["sort_order"].ToString()));
                    dataGridView.Columns[dt.Rows[0]["sort_column"].ToString()].HeaderCell.SortGlyphDirection = dt.Rows[0]["sort_order"].ToString().ToLower().IndexOf("asc") > -1 ? SortOrder.Ascending : dt.Rows[0]["sort_order"].ToString().ToLower().IndexOf("desc") > -1 ? SortOrder.Descending : SortOrder.None;
                }
                */
                LoadLayout(dgll.LayoutName);
            }
        }
        
        public void LoadDefaultLayout()
        {
            LoadDefaultLayout(this.FindForm().Name);
        }

        public void LoadDefaultLayout(string form_name)
        {
            try
            {
                string default_layout = SettingsClass.GetUserSetting(form_name, SettingsClass.EmployeeId);
                if (default_layout != null && default_layout.Trim() != "")
                {
                    /*
                    DataSet ds = new DataSet();
                    ds.ReadXml(Path.Combine(SettingsClass.LayoutsPath, String.Format("{0}.xml", default_layout)));
                    DataTable dt = ds.Tables["GENERAL_INFORMATION"];
                    DataTable dtColumns = ds.Tables["COLUMNS"];
                    try
                    {
                        ((BindingSource)dataGridView.DataSource).Filter = dt.Rows[0]["filter"].ToString();
                        foreach (DataGridViewColumn dgvc in dataGridView.Columns)
                        {
                            ((DataGridViewAutoFilterColumnHeaderCell)dgvc.HeaderCell).UpdateFilterFromLayout();
                        }
                    }
                    catch (Exception exp) { exp.ToString(); }
                    dataGridView.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)Enum.Parse(typeof(DataGridViewAutoSizeColumnsMode), dt.Rows[0]["layout_mode"].ToString());
                    foreach (DataRow dr in dtColumns.Rows)
                    {
                        try
                        {
                            dataGridView.Columns[dr["column_name"].ToString()].Visible = Convert.ToBoolean(dr["column_visible"]);
                            dataGridView.Columns[dr["column_name"].ToString()].Frozen = Convert.ToBoolean(dr["column_frozen"]);
                            dataGridView.Columns[dr["column_name"].ToString()].Width = Convert.ToInt32(dr["column_width"]);
                            dataGridView.Columns[dr["column_name"].ToString()].DisplayIndex = Convert.ToInt32(dr["displaynumber"]);
                        }
                        catch { }
                    }
                    if (dt.Rows[0]["sort_column"] != null && dt.Rows[0]["sort_column"] != DBNull.Value)
                    {
                        dataGridView.Columns[dt.Rows[0]["sort_column"].ToString()].SortMode = DataGridViewColumnSortMode.Programmatic;
                        dataGridView.Sort(dataGridView.Columns[dt.Rows[0]["sort_column"].ToString()], (ListSortDirection)Enum.Parse(typeof(ListSortDirection), dt.Rows[0]["sort_order"].ToString()));
                        dataGridView.Columns[dt.Rows[0]["sort_column"].ToString()].HeaderCell.SortGlyphDirection = dt.Rows[0]["sort_order"].ToString().ToLower().IndexOf("asc") > -1 ? SortOrder.Ascending : dt.Rows[0]["sort_order"].ToString().ToLower().IndexOf("desc") > -1 ? SortOrder.Descending : SortOrder.None;
                    }
                    */
                    LoadLayout(default_layout);
                }
            }
            catch(Exception exp) {LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }

        private void LoadLayout(string layout_name)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Path.Combine(SettingsClass.LayoutsPath, String.Format("{0}.xml", layout_name)));
                DataTable dt = ds.Tables["GENERAL_INFORMATION"];
                DataTable dtColumns = ds.Tables["COLUMNS"];
                try
                {
                    ((BindingSource)dataGridView.DataSource).Filter = dt.Rows[0]["filter"].ToString();
                    foreach (DataGridViewColumn dgvc in dataGridView.Columns)
                    {
                        ((DataGridViewAutoFilterColumnHeaderCell)dgvc.HeaderCell).UpdateFilterFromLayout();
                    }
                }
                catch (Exception exp) { exp.ToString(); }
                dataGridView.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)Enum.Parse(typeof(DataGridViewAutoSizeColumnsMode), dt.Rows[0]["layout_mode"].ToString());
                foreach (DataRow dr in dtColumns.Rows)
                {
                    try
                    {
                        dataGridView.Columns[dr["column_name"].ToString()].Visible = Convert.ToBoolean(dr["column_visible"]);
                        dataGridView.Columns[dr["column_name"].ToString()].Frozen = Convert.ToBoolean(dr["column_frozen"]);
                        dataGridView.Columns[dr["column_name"].ToString()].Width = Convert.ToInt32(dr["column_width"]);
                        dataGridView.Columns[dr["column_name"].ToString()].DisplayIndex = Convert.ToInt32(dr["displaynumber"]);
                    }
                    catch { }
                }
                if (dt.Rows[0]["sort_column"] != null && dt.Rows[0]["sort_column"] != DBNull.Value && dt.Rows[0]["sort_column"].ToString().Trim() != "")
                {
                    dataGridView.Columns[dt.Rows[0]["sort_column"].ToString()].SortMode = DataGridViewColumnSortMode.Programmatic;
                    dataGridView.Sort(dataGridView.Columns[dt.Rows[0]["sort_column"].ToString()], (ListSortDirection)Enum.Parse(typeof(ListSortDirection), dt.Rows[0]["sort_order"].ToString()));
                    dataGridView.Columns[dt.Rows[0]["sort_column"].ToString()].HeaderCell.SortGlyphDirection = dt.Rows[0]["sort_order"].ToString().ToLower().IndexOf("asc") > -1 ? SortOrder.Ascending : dt.Rows[0]["sort_order"].ToString().ToLower().IndexOf("desc") > -1 ? SortOrder.Descending : SortOrder.None;
                }
                GenerateColumnsListBox(this.dataGridView);
                toolStripActiveLayoutLabel.Visible = true;
                //toolStripActiveLayoutLabel.BackColor = Color.Red;
                toolStripActiveLayoutLabel.ForeColor = Color.Red;
                toolStripActiveLayoutLabel.Text = "...";
                toolStripActiveLayoutLabel.ToolTipText = String.Format("{0}: {1}", Language.GetLabelText("activeLayout", "Active layout"), layout_name);
                toolStripActiveLayoutLabel.Image = new Bitmap(Path.Combine(SettingsClass.Icons24ImagePath, "Erase.png"));
                toolStripActiveLayoutLabel.Click -= new EventHandler(toolStripActiveLayoutLabel_Click);
                toolStripActiveLayoutLabel.Click += new EventHandler(toolStripActiveLayoutLabel_Click);
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(Language.GetMessageBoxText("layoutLoadingError", "There was an error loading the selected layout!"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripActiveLayoutLabel_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView.Columns.Clear();
                dataGridView.DataSource = null;
                da.bindingSource.RemoveFilter();
                da.bindingSource.RemoveSort();
                da.bindingSource.ResetBindings(false);
                FillGrid();
                GenerateColumnsListBox(this.dataGridView);
                GenerateLinkButtonColums(this.dataGridView);
                GenerateExternalColumns();
                try
                {
                    switch (this.ParentForm.Name)
                    {
                        case "ContractSelect":

                            //((ContractSelect)this.ParentForm).AddLinkColumn("properties", "properties", dataGridView.Columns["owner_id"].Index + 2);
                            this.AddLinkColumn("properties", "properties", dataGridView.Columns["owner_id"].Index + 2);
                            //((ContractSelect)this.ParentForm).AddLinkColumn("services", "services", dataGridView.Columns["owner_id"].Index + 3);
                            this.AddLinkColumn("services", "services", dataGridView.Columns["owner_id"].Index + 3);
                            break;
                        case "RentContractSelect":
                            this.AddLinkColumn("tenants", "tenants", dataGridView.Columns["owner_id"].Index + 1);
                            break;
                        case "InvoicesSelect":
                            this.AddLinkColumn("cash_receipts", "cash_receipts", dataGridView.Columns["ballance"].Index + 1);
                            this.AddLinkColumn("cash_receipts", "cash_receipts", dataGridView.Columns["ballance"].Index + 1);
                            break;
                    }
                }
                catch (Exception exp) { exp.ToString(); }
                toolStripActiveLayoutLabel.Visible = false;
            }
            catch (Exception exp) { exp.ToString(); }
        }

        public void GenerateExternalColumns()
        {
            if (GenerateComboBoxesForExternalColumns)
            {
                foreach (string external_column in ExternalColumns)
                {
                    int col_index = dataGridView.Columns[external_column].Index;
                    string stored_procedure_table_name = external_column.ToLower().Replace("_id", "");
                    DataAccess da = new DataAccess();
                    DataTable dtSource = new DataTable();
                    DataGridViewComboBoxColumn dgvcbc = new DataGridViewComboBoxColumn();
                    switch (external_column.ToLower())
                    {
                        case "status_id":
                            //stored_procedure_table_name = stored_procedure_table_name[stored_procedure_table_name.Length - 1] == 'y' ? String.Concat(stored_procedure_table_name.Remove(stored_procedure_table_name.Length - 1), "ies") : String.Concat(stored_procedure_table_name, "s");
                            da = new DataAccess(CommandType.StoredProcedure, CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name_for_grid", new object[]{new MySqlParameter("_LIST_TYPE_NAME", "invoicerequirement_status")});
                            dtSource = da.ExecuteSelectQuery().Tables[0];
                            dgvcbc.DisplayMember = "name";
                            dgvcbc.ValueMember = "id";
                            dgvcbc.HeaderText = Language.GetColumnHeaderText("status", "STATUS");
                            dgvcbc.DataPropertyName = external_column;
                            dgvcbc.DataSource = dtSource;
                            dataGridView.Columns.Remove(external_column);
                            //dataGridView.Columns.Remove(external_column.ToLower().Replace("_id", "").ToUpper());
                            //dataGridView.Columns.Remove("status");
                            dataGridView.Columns.Insert(col_index, dgvcbc);
                            break;
                        case "servicetype_id":
                            //stored_procedure_table_name = stored_procedure_table_name[stored_procedure_table_name.Length - 1] == 'y' ? String.Concat(stored_procedure_table_name.Remove(stored_procedure_table_name.Length - 1), "ies") : String.Concat(stored_procedure_table_name, "s");
                            da = new DataAccess(CommandType.StoredProcedure, CommandType.StoredProcedure, "LISTSsp_select_by_list_type_name_for_grid", new object[] { new MySqlParameter("_LIST_TYPE_NAME", "service_type") });
                            dtSource = da.ExecuteSelectQuery().Tables[0];
                            dgvcbc.DisplayMember = "name";
                            dgvcbc.ValueMember = "id";
                            dgvcbc.HeaderText = Language.GetColumnHeaderText("service_type", "SERVICE TYPE");
                            dgvcbc.DataPropertyName = external_column;
                            dgvcbc.DataSource = dtSource;
                            try
                            {
                                dataGridView.Columns.Remove(external_column);
                            }
                            catch { }
                            //dataGridView.Columns.Remove(external_column.ToLower().Replace("_id", "").ToUpper());
                            //dataGridView.Columns.Remove("status");
                            dataGridView.Columns.Insert(col_index, dgvcbc);
                            break;
                        default:
                            stored_procedure_table_name = stored_procedure_table_name[stored_procedure_table_name.Length - 1] == 'y' ? String.Concat(stored_procedure_table_name.Remove(stored_procedure_table_name.Length - 1), "ies") : String.Concat(stored_procedure_table_name, "s");
                            da = new DataAccess(CommandType.StoredProcedure, String.Format("{0}sp_list", stored_procedure_table_name.ToUpper()));
                            dtSource = da.ExecuteSelectQuery().Tables[0];
                            dgvcbc.DisplayMember = "name";
                            dgvcbc.ValueMember = "id";
                            dgvcbc.DataPropertyName = external_column;
                            dgvcbc.HeaderText = Language.GetColumnHeaderText(external_column.ToLower().Replace("_id", ""), external_column.ToUpper().Replace("_ID", ""));
                            dgvcbc.DataSource = dtSource;
                            dataGridView.Columns.Remove(external_column);
                            try
                            {
                                dataGridView.Columns.Remove(external_column.ToLower().Replace("_id", ""));
                            }
                            catch { }
                            dgvcbc.FlatStyle = FlatStyle.Popup;
                            dataGridView.Columns.Insert(col_index, dgvcbc);
                            break;
                    }
                }
            }
        }

        public void GenerateColumnsListBox(DataGridView dgv)
        {
            //CheckedListBox columns = new CheckedListBox();
            columns.Items.Clear();
            columns.Name = String.Format("columns{0}", dgv.Name);
            columns.Items.Add("ALL", false);
            foreach (DataGridViewColumn dgvc in dgv.Columns)
            {
                if (dgvc.Name.ToLower().IndexOf("id") != 0 && dgvc.Name.ToLower().IndexOf("_id") == -1)
                {
                    //columns.Items.Add(dgvc.HeaderText, dgvc.Visible);
                    columns.Items.Add(dgvc.Name, dgvc.Visible);
                }
            }
            columns.Font = new Font(SettingsClass.FontTheme, 8, FontStyle.Regular);
            columns.BorderStyle = BorderStyle.Fixed3D;
            columns.Width = 150;
            columns.Height = dgv.Height < 160 ? dgv.Height - dgv.ColumnHeadersHeight : 160;
            columns.Visible = false;
            dgv.Controls.Add(columns);
            columns.ItemCheck += new ItemCheckEventHandler(columns_ItemCheck);
        }

        private void columns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string column_name = columns.Items[e.Index].ToString().Trim().Replace(" ", "_");
            if (column_name == "ALL")
            {
                for (int i = 0; i < columns.Items.Count; i++)
                {
                    try
                    {
                        string col_name = columns.Items[i].ToString().Trim().Replace(" ", "_");
                        if (col_name != "ALL")
                        {
                            columns.SetItemCheckState(i, e.NewValue);
                            this.dataGridView.Columns[col_name].Visible = (e.NewValue == CheckState.Checked ? true : false);
                        }
                    }
                    catch
                    {
                        try
                        {
                            string col_name = columns.Items[i].ToString().Trim();
                            if (col_name != "ALL")
                            {
                                columns.SetItemCheckState(i, e.NewValue);
                                this.dataGridView.Columns[col_name].Visible = (e.NewValue == CheckState.Checked ? true : false);
                            }
                        }
                        catch { }
                    }
                }
            }
            else
            {
                try
                {
                    this.dataGridView.Columns[column_name].Visible = (e.NewValue == CheckState.Checked ? true : false);
                    //this.dataGridView.Width += (this.dataGridView.Columns[column_name].Width * (e.NewValue == CheckState.Checked ? 1 : -1));
                }
                catch
                {
                    try
                    {
                        column_name = columns.Items[e.Index].ToString().Trim();
                        this.dataGridView.Columns[column_name].Visible = (e.NewValue == CheckState.Checked ? true : false);
                        //this.dataGridView.Width += (this.dataGridView.Columns[column_name].Width * (e.NewValue == CheckState.Checked ? 1 : -1));
                    }
                    catch { }
                }
            }
        }

        public virtual void dataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            Language.PopulateGridColumnHeaders((DataGridView)sender);
        }

        private void dataGridView_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridView.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
                ((DataGridViewAutoFilterColumnHeaderCell)col.HeaderCell).AutomaticSortingEnabled = false;
            }

            // Format the OrderTotal column as currency. 
            //dataGridViewGroups.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            //dataGridViewGroups.AutoResizeColumns();
        }

        // Displays the drop-down list when the user presses 
        // ALT+DOWN ARROW or ALT+UP ARROW.
        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell = dataGridView.CurrentCell.OwningColumn.HeaderCell as DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        // Updates the filter status label. 
        protected virtual void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dataGridView);
            if (String.IsNullOrEmpty(filterStatus))
            {
                statusStripDataGrid.Visible = false;
                toolStripStatusLabelShowAll.Visible = false;
                toolStripStatusLabelFilter.Visible = false;
            }
            else
            {
                statusStripDataGrid.Visible = true;
                toolStripStatusLabelShowAll.Visible = true;
                toolStripStatusLabelFilter.Visible = true;
                toolStripStatusLabelFilter.Text = filterStatus;
            }
            toolStripButtonEdit.Enabled = dataGridView.Rows.Count > 0 && (this.ParentForm is UserForm && ((UserForm)this.ParentForm).ChildLaunched == null);
            toolStripButtonDelete.Enabled = dataGridView.Rows.Count > 0 && (this.ParentForm is UserForm && ((UserForm)this.ParentForm).ChildLaunched == null);
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            }
        }

        private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //CheckedListBox columns = (CheckedListBox)((DataGridView)sender).Controls[String.Format("columns{0}", ((DataGridView)sender).Name)];
                columns.Visible = false;
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    foreach (DataGridViewColumn col in dataGridView.Columns)
                    {
                        if (col.HeaderCell is DataGridViewAutoFilterColumnHeaderCell)
                        {
                            ((DataGridViewAutoFilterColumnHeaderCell)col.HeaderCell).AutomaticSortingEnabled = false;
                        }
                    }
                    //CheckedListBox columns = (CheckedListBox)((DataGridView)sender).Controls[String.Format("columns{0}", ((DataGridView)sender).Name)];
                    Point p = ((DataGridView)sender).PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
                    columns.Location = p.X > ((DataGridView)sender).Width - columns.Width ? new Point(((DataGridView)sender).Width - columns.Width, p.Y) : p;
                    columns.Visible = true;
                    columns.Focus();
                    try
                    {
                        ((DataGridView)sender).InvalidateCell(((DataGridView)sender).CurrentCell);
                    }
                    catch { }
                    return;
                }
                else
                {
                    if (((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell is DataGridViewAutoFilterColumnHeaderCell)
                    {
                        ((DataGridViewAutoFilterColumnHeaderCell)((DataGridView)sender).Columns[e.ColumnIndex].HeaderCell).AutomaticSortingEnabled = true;
                    }
                }
            }
            catch { }
        }

        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            //if (((DataGridView)sender).HitTest(e.X, e.Y).RowIndex != -1)
                columns.Visible = false;
        }

        private void dataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (((UserForm)this.ParentForm).ChildLaunched != null) return;
            //SplashScreen.SplashScreen.ShowSplashScreen();
            try
            {
                SplashScreen.SplashScreen.ShowSplashScreen(((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            }
            catch { }
            Application.DoEvents();
            if (((DataGridView)sender).HitTest(e.X, e.Y).RowIndex != -1)
            {
                if (Selectable || toolStripButtonSelect.Visible)
                {
                    try
                    {
                        var method = this.ParentForm.GetType().GetMethod("buttonSelect_Click");
                        method.Invoke(this.ParentForm, new object[] { new System.Object(), new System.EventArgs() });
                    }
                    catch
                    {
                        try
                        {
                            this.buttonSelect_Click(this, null);
                        }
                        catch { }
                    }
                }
                else
                {
                    try
                    {
                        var method = this.ParentForm.GetType().GetMethod("buttonEdit_Click");
                        method.Invoke(this.ParentForm, new object[] { new System.Object(), new System.EventArgs() });
                    }
                    catch
                    {
                        try
                        {
                            this.buttonEdit_Click(this, null);
                        }
                        catch { }
                    }
                }
            }
            SplashScreen.SplashScreen.CloseForm();
        }

        private void dataGridView_ColumnDividerDoubleClick(object sender, DataGridViewColumnDividerDoubleClickEventArgs e)
        {
            if (((DataGridView)sender).SelectedRows.Count == ((DataGridView)sender).RowCount)
            {
                ((DataGridView)sender).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //((DataGridView)sender).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            }
        }

        // Clears the filter when the user clicks the "Show All" link
        // or presses ALT+A. 
        private void toolStripStatusLabelShowAll_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridView);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        public void FillGrid()
        {
            FillGrid(da.bindingSource);
        }

        public void FillGrid(BindingSource _bs)
        {
            dataGridView.DataSource = _bs;

            if (VisibleColumns != null)
            {
                if (VisibleColumns[0].ToUpper() == "ALL")
                {
                    foreach (DataGridViewColumn dgvc in dataGridView.Columns)
                    {
                        dgvc.Visible = ((dgvc.Name.ToLower()=="id" || dgvc.Name.ToUpper().IndexOf("_ID") > -1)?false:true);

                        if ((dgvc.ValueType != null && dgvc.ValueType.ToString() == "System.DateTime") || (DateColumns != null && Array.IndexOf(DateColumns, dgvc.Name.ToUpper()) > -1))
                        {
                            dgvc.DefaultCellStyle.Format = SettingsClass.DateFormat; //default "dd.MM.yyyy";
                        }
                        if ((dgvc.ValueType != null && dgvc.ValueType.ToString() == "System.Double") || (DoubleColumns != null && Array.IndexOf(DoubleColumns, dgvc.Name.ToUpper()) > -1))
                        {
                            dgvc.DefaultCellStyle.Format = SettingsClass.DoubleFormat; //default "n2";
                            dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                    }
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                else
                {
                    foreach (DataGridViewColumn dgvc in dataGridView.Columns)
                    {
                        dgvc.Visible = Array.IndexOf(VisibleColumns, dgvc.Name.ToUpper()) > -1 ? true : false;
                        if (dgvc.ValueType.ToString() == "System.DateTime" || (DateColumns != null && Array.IndexOf(DateColumns, dgvc.Name.ToUpper()) > -1))
                        {
                            dgvc.DefaultCellStyle.Format = SettingsClass.DateFormat; //default "dd.MM.yyyy";
                        }
                        if (dgvc.ValueType.ToString() == "System.Double" || (DoubleColumns != null && Array.IndexOf(DoubleColumns, dgvc.Name.ToUpper()) > -1))
                        {
                            dgvc.DefaultCellStyle.Format = SettingsClass.DoubleFormat; //default "n2";
                            dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                    }
                }
            }
            toolStripComboBoxSearchType.SelectedIndex = 0;

            // --- SET FILTER FOR OWNER LOGIN ---
            if (SettingsClass.LoginOwnerId > 0)
            {
                try
                {
                    //if( ((DataTable) ((BindingSource)dataGridView.DataSource).DataSource).TableName.ToLower() == "owners")
                    //if (this.ParentForm.Name == "ownerselect")
                    /*
                    bool is_owners_select = false;
                    foreach (DataGridViewColumn dgvc in this.dataGridView.Columns)
                        if (dgvc.Name.ToLower() == "cif")
                        {
                            is_owners_select = true;
                            break;
                        }
                    if (is_owners_select)
                        ((BindingSource)dataGridView.DataSource).Filter = String.Format("ID={0}", SettingsClass.LoginOwnerId);
                    else
                        ((BindingSource)dataGridView.DataSource).Filter = String.Format("OWNER_ID={0}", SettingsClass.LoginOwnerId);
                    */
                    ((BindingSource)dataGridView.DataSource).Filter = dataGridView.Columns["cif"] == null ? String.Format("OWNER_ID={0}", SettingsClass.LoginOwnerId) : String.Format("ID={0}", SettingsClass.LoginOwnerId);
                    //toolStripStatusLabelFilter.Visible = toolStripStatusLabelShowAll.Visible = false;
                }
                catch (Exception exp) { exp.ToString(); }
            }
        }

        public void GenerateLinkButtonColums(DataGridView dgv)
        {
            try
            {
                for (int i = 0; i < LinkColumns.Length; i++)
                {
                    foreach (DataGridViewColumn dgvc in dgv.Columns)
                    {
                        if (LinkColumns[i].ToUpper() == dgvc.Name.ToUpper())
                        {
                            DataGridViewLinkColumn dgvlc = new DataGridViewLinkColumn();
                            dgvlc.UseColumnTextForLinkValue = false;
                            dgvlc.Name = dgvc.Name;
                            dgvlc.HeaderText = Language.GetColumnHeaderText(dgvc.Name.ToLower(), dgvc.Name.ToUpper());
                            dgvlc.DataPropertyName = dgvc.Name;
                            dgvlc.ActiveLinkColor = Color.DarkOrange;
                            dgvlc.LinkBehavior = LinkBehavior.SystemDefault;
                            dgvlc.LinkColor = Color.DarkOrange;
                            dgvlc.TrackVisitedState = false;
                            dgvlc.VisitedLinkColor = Color.DarkOrange;
                            dgvlc.Visible = true;

                            int column_index = dgvc.Index;
                            dgv.Columns.Remove(dgvc);

                            //dgvlc.Name = dgvc.Name;
                            dgv.Columns.Insert(column_index, dgvlc);
                            break;
                        }
                    }
                }
            }
            catch { }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && ((DataGridView)sender).Columns[e.ColumnIndex] is DataGridViewLinkColumn && e.RowIndex != -1)
            {
                //MessageBox.Show(((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                string id = "";
                string column_header = "";
                try
                {
                    column_header = String.Format("{0}_ID", ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText.Replace(" ", "_").ToUpper());
                    int id_column_index = ((DataGridView)sender).Columns[column_header].Index;
                    id = ((DataGridView)sender).Rows[e.RowIndex].Cells[id_column_index].Value.ToString();
                }
                catch {
                    id = ((DataGridView)sender).Rows[e.RowIndex].Cells["id"].Value.ToString();
                    column_header = ((DataGridView)sender).Columns[e.ColumnIndex].HeaderText;
                }
                switch (column_header.ToUpper().Replace(" ", "_"))
                {
                    case "OWNER_ID":
                        var o = new Owners(Convert.ToInt32(id));
                        o.StartPosition = FormStartPosition.CenterScreen;
                        o.ShowDialog();
                        o.Dispose();
                        break;
                    case "TENANT_ID":
                        var t = new Tenants(Convert.ToInt32(id));
                        t.StartPosition = FormStartPosition.CenterScreen;
                        t.ShowDialog();
                        t.Dispose();
                        break;
                    case "PROPERTY_ID":
                        var p = new Property(Convert.ToInt32(id));
                        p.StartPosition = FormStartPosition.CenterScreen;
                        p.ShowDialog();
                        p.Dispose();
                        break;
                    case "PROJECT_ID":
                        var pj = new Projects(Convert.ToInt32(id));
                        pj.StartPosition = FormStartPosition.CenterScreen;
                        pj.ShowDialog();
                        pj.Dispose();
                        break;
                    case "PROPERTIES":
                        var pr = new ProportySelect(Convert.ToInt32(id), "contract_id");
                        pr.StartPosition = FormStartPosition.CenterScreen;
                        pr.ShowDialog();
                        pr.Dispose();
                        break;
                    case "SERVICES":
                        var cs = new ContractsServices(Convert.ToInt32(id));
                        cs.StartPosition = FormStartPosition.CenterScreen;
                        cs.ShowDialog();
                        cs.Dispose();
                        break;
                    case "PARENT_CONTRACT_NUMBER":
                        var pc = new Contracts(Convert.ToInt32(id), "id");
                        pc.StartPosition = FormStartPosition.CenterScreen;
                        pc.ShowDialog();
                        pc.Dispose();
                        break;
                    case "PARENT_RENTCONTRACT_NUMBER":
                        var prc = new RentContracts(Convert.ToInt32(id), "id");
                        prc.StartPosition = FormStartPosition.CenterScreen;
                        prc.ShowDialog();
                        prc.Dispose();
                        break;
                    case "CONTRACT_ID":
                        var fc = new Contracts(Convert.ToInt32(id), "id");
                        fc.StartPosition = FormStartPosition.CenterScreen;
                        fc.ShowDialog();
                        fc.Dispose();
                        break;
                    case "RENTCONTRACT_ID":
                        var rc = new RentContracts(Convert.ToInt32(id), "id");
                        rc.StartPosition = FormStartPosition.CenterScreen;
                        rc.ShowDialog();
                        rc.Dispose();
                        break;
                    case "CO_OWNERS":
                        var co = new Owners(Convert.ToInt32(id));
                        co.StartPosition = FormStartPosition.CenterScreen;
                        co.ShowDialog();
                        co.Dispose();
                        break;
                    case "SUPPLIER_ID":
                        var comp = new Companies(Convert.ToInt32(id));
                        comp.StartPosition = FormStartPosition.CenterScreen;
                        comp.ShowDialog();
                        comp.Dispose();
                        break;
                    case "INVOICE_ID":
                        var i = new Invoices(Convert.ToInt32(id));
                        i.StartPosition = FormStartPosition.CenterScreen;
                        i.ShowDialog();
                        i.Dispose();
                        break;
                    case "TENANTS":
                        var tenants = new TenantSelect(Convert.ToInt32(id));
                        tenants.StartPosition = FormStartPosition.CenterScreen;
                        tenants.ShowDialog();
                        tenants.Dispose();
                        break;
                    case "INVOICE_NUMBER":
                        var inv = new Invoices(Convert.ToInt32(id), "invoicerequirement_id");
                        inv.StartPosition = FormStartPosition.CenterScreen;
                        inv.ShowDialog();
                        inv.Dispose();
                        break;
                    case "CASH_RECEIPTS":
                        var cr = new ReceiptSelect(Convert.ToInt32(id), "invoice_id");
                        cr.StartPosition = FormStartPosition.CenterScreen;
                        cr.ShowDialog();
                        cr.Dispose();
                        break;
                    case "BANK_RECEIPTS":
                        var br = new BankReceiptSelect(Convert.ToInt32(id), "invoice_id");
                        br.StartPosition = FormStartPosition.CenterScreen;
                        br.ShowDialog();
                        br.Dispose();
                        break;

                    /* // TO ADD
                    case "EMPLOYEE_ID":
                        var em = new DynamicFormEmployees(Convert.ToInt32(id));
                        cs.StartPosition = FormStartPosition.CenterScreen;
                        cs.ShowDialog();
                        cs.Dispose();
                        break;
                    */
                }
                //MessageBox.Show(id);
            }
        }

        public void buttonSelect_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                try
                {
                    IdToReturn = Convert.ToInt32(dataGridView[dataGridView.Columns["ID"].Index, dataGridView.SelectedRows[0].Index].Value);
                    try
                    {
                        var property = this.ParentForm.GetType().GetProperty("IdToReturn");
                        property.SetValue(this.ParentForm, IdToReturn, null);
                    }
                    catch { }
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.buttonExit_Click(null, null);
                }
                catch { }
            }
            else
            {
                MessageBox.Show(Language.GetMessageBoxText("selectARecordFirst", "Please select a record!"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonSelect_Click(object sender, EventArgs e)
        {
            this.buttonSelect_Click(null, null);
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            this.buttonExit_Click(null, null);
        }

        public void AddToolStripButton(ToolStripButton tsb, int index)
        {
            this.toolStrip1.Items.Insert(index, tsb);
        }

        public void AddToolStripButton(string text, Image image, EventHandler e, string name, int index)
        {
            ToolStripButton tsb = new ToolStripButton(text, image, e, name);
            this.toolStrip1.Items.Insert(index, tsb);
        }

        private void toolStripButtonMove_Click(object sender, EventArgs e)
        {
            toolStrip1.Dock = toolStrip1.Dock == DockStyle.Bottom ? DockStyle.Top : DockStyle.Bottom;
            ((ToolStripButton)sender).Image = toolStrip1.Dock == DockStyle.Bottom ? new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "55.png")) : new Bitmap(System.IO.Path.Combine(SettingsClass.Icons16ImagePath, "54.png"));
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView.Width, this.dataGridView.Height);
            this.dataGridView.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView.Width, this.dataGridView.Height));
            e.Graphics.DrawImage(bm, 0, 0);

        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            //printDocument1.Print();
            DGVPrinterHelper.DGVPrinter printer = new DGVPrinterHelper.DGVPrinter();
            CustomPrintDialog cpd = new CustomPrintDialog();
            if (cpd.ShowDialog() == DialogResult.OK)
            {
                printer.Title = cpd.userTextBoxDocTitle.Text;
                printer.SubTitle = cpd.userTextBoxDocSubtitle.Text;
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.Footer = cpd.userTextBoxDocFooter.Text;
                printer.FooterSpacing = 15;
            }
            else
                return;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintColumnHeaders = true;
            printer.PrintPreviewDataGridView(dataGridView);
            //printer.PrintDataGridView(dataGridView);
        }

        private void toolStripButtonExportExcel_Click(object sender, EventArgs e)
        {
        }

        private void fixedGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void scrollableGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        public virtual void buttonEdit_Click(object sender, EventArgs e)
        {
        
        }

        public virtual void buttonAdd_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            try
            {
                if (columns.Visible) columns.Visible = false;
            }
            catch { }
        }

        private void toolStripTextBoxSearchFor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                toolStripButtonSearchForward_Click(null, null);
            }
        }

        private void toolStripButtonSearchForward_Click(object sender, EventArgs e)
        {
            Color last_color = new Color();
            if (toolStripTextBoxSearchFor.Text.Trim() != "")
            {
                try
                {
                    bool found = false;
                    if (last_found_column > -1 && last_found_column < dataGridView.ColumnCount - 1)
                    {
                        for (int i = last_found_column + 1; i < dataGridView.ColumnCount; i++)
                        {
                            if (dataGridView.Columns[i].Visible)
                            {
                                if ((toolStripComboBoxSearchType.SelectedItem.ToString() == "Exact search" && dataGridView.Rows[last_found_row].Cells[i].Value.ToString().ToLower() == toolStripTextBoxSearchFor.Text.ToLower()) || (toolStripComboBoxSearchType.SelectedItem.ToString() == "Partial search" && dataGridView.Rows[last_found_row].Cells[i].Value.ToString().ToLower().IndexOf(toolStripTextBoxSearchFor.Text.ToLower()) > -1))
                                {
                                    dataGridView[last_found_column, last_found_row].Style.BackColor = last_color;
                                    last_color = dataGridView[i, last_found_row].Style.BackColor;
                                    dataGridView.Rows[last_found_row].Cells[i].Style.BackColor = Color.Goldenrod;
                                    dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows[last_found_row].Index;
                                    //dataGridView.Rows[last_found_row].Cells[i].Style.ForeColor = Color.Red;
                                    //dataGridView.Rows[last_found_row].Cells[i].Style.Font = new Font(dataGridView.Rows[last_found_row].Cells[i].Style.Font, FontStyle.Bold);
                                    last_found_column = i;
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!found)
                    {
                        for (int i = last_found_row + 1; i < dataGridView.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView.ColumnCount; j++)
                            {
                                if (dataGridView.Columns[j].Visible)
                                {
                                    if ((toolStripComboBoxSearchType.SelectedItem.ToString() == "Exact search" && dataGridView[j, i].Value.ToString().ToLower() == toolStripTextBoxSearchFor.Text.ToLower()) || (toolStripComboBoxSearchType.SelectedItem.ToString() == "Partial search" && dataGridView[j, i].Value.ToString().ToLower().IndexOf(toolStripTextBoxSearchFor.Text.ToLower()) > -1))
                                    {
                                        try
                                        {
                                            dataGridView[last_found_column, last_found_row].Style.BackColor = last_color;
                                        }
                                        catch { }
                                        last_color = dataGridView[j, i].Style.BackColor;
                                        dataGridView.Rows[i].Cells[j].Style.BackColor = Color.DarkGoldenrod;
                                        dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows[i].Index;
                                        last_found_row = i;
                                        last_found_column = j;
                                        found = true;
                                        break;
                                    }
                                }
                            }
                            if (found) break;
                        }
                    }
                }
                catch { }
            }
            else
            {
                //TO DO: RESET LAST FOUND CELL COLOR AND COUNTERS IF PRESS WITH BLANK
            }
        }

        private void toolStripButtonSearchBack_Click(object sender, EventArgs e)
        {
            Color last_color = new Color();
            if (toolStripTextBoxSearchFor.Text.Trim() != "")
            {
                try
                {
                    bool found = false;
                    if (last_found_column > 0)
                    {
                        for (int i = last_found_column - 1; i > -1; i--)
                        {
                            if (dataGridView.Columns[i].Visible)
                            {
                                if ((toolStripComboBoxSearchType.SelectedItem.ToString() == "Exact search" && dataGridView.Rows[last_found_row].Cells[i].Value.ToString().ToLower() == toolStripTextBoxSearchFor.Text.ToLower()) || (toolStripComboBoxSearchType.SelectedItem.ToString() == "Partial search" && dataGridView.Rows[last_found_row].Cells[i].Value.ToString().ToLower().IndexOf(toolStripTextBoxSearchFor.Text.ToLower()) > -1))
                                {
                                    dataGridView[last_found_column, last_found_row].Style.BackColor = last_color;
                                    last_color = dataGridView[i, last_found_row].Style.BackColor;
                                    dataGridView.Rows[last_found_row].Cells[i].Style.BackColor = Color.Goldenrod;
                                    dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows[last_found_row].Index;
                                    //dataGridView.Rows[last_found_row].Cells[i].Style.ForeColor = Color.Red;
                                    //dataGridView.Rows[last_found_row].Cells[i].Style.Font = new Font(dataGridView.Rows[last_found_row].Cells[i].Style.Font, FontStyle.Bold);
                                    last_found_column = i;
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!found)
                    {
                        for (int i = last_found_row - 1; i > -1; i--)
                        {
                            for (int j = dataGridView.ColumnCount - 1; j > -1; j--)
                            {
                                if (dataGridView.Columns[j].Visible)
                                {
                                    if ((toolStripComboBoxSearchType.SelectedItem.ToString() == "Exact search" && dataGridView[j, i].Value.ToString().ToLower() == toolStripTextBoxSearchFor.Text.ToLower()) || (toolStripComboBoxSearchType.SelectedItem.ToString() == "Partial search" && dataGridView[j, i].Value.ToString().ToLower().IndexOf(toolStripTextBoxSearchFor.Text.ToLower()) > -1))
                                    {
                                        try
                                        {
                                            dataGridView[last_found_column, last_found_row].Style.BackColor = last_color;
                                        }
                                        catch { }
                                        last_color = dataGridView[j, i].Style.BackColor;
                                        dataGridView.Rows[i].Cells[j].Style.BackColor = Color.DarkGoldenrod;
                                        dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.Rows[i].Index;
                                        last_found_row = i;
                                        last_found_column = j;
                                        found = true;
                                        break;
                                    }
                                }
                            }
                            if (found) break;
                        }
                    }
                }
                catch { }
            }
            else
            {
                //TO DO: RESET LAST FOUND CELL COLOR AND COUNTERS IF PRESS WITH BLANK
            }
        }

        private void toolStripButtonComplexSort_Click(object sender, EventArgs e)
        {
            var f = new ComplexSort(dataGridView);
            if (f.ShowDialog() == DialogResult.OK)
            {
                if (f.Order != "")
                {
                    try
                    {
                        //((DataTable) ((BindingSource)dataGridView.DataSource).DataSource).DefaultView.Sort = f.Order;
                        ((BindingSource)dataGridView.DataSource).Sort = f.Order;
                    }
                    catch (Exception exp)
                    {
                        LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                    }
                }
            }
            f.Dispose();
        }

        private void allRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.InitialDirectory = SettingsClass.ExcelExportPath;
                saveFileDialog1.DefaultExt = "xls";
                saveFileDialog1.Filter = "Excel Worksheets|*.xls";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
                {
                    //ExcelExport.exportToExcel(dataGridView, saveFileDialog1.FileName);
                    ExcelExport.exportToExcel2(dataGridView, saveFileDialog1.FileName, true);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(Language.GetMessageBoxText("excelExportUnsupportedType", exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectedRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.InitialDirectory = SettingsClass.ExcelExportPath;
                saveFileDialog1.DefaultExt = "xls";
                saveFileDialog1.Filter = "Excel Worksheets|*.xls";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != "")
                {
                    //ExcelExport.exportToExcel(dataGridView, saveFileDialog1.FileName);
                    ExcelExport.exportToExcel2(dataGridView, saveFileDialog1.FileName, false);
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(Language.GetMessageBoxText("excelExportUnsupportedType", exp.Message), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            //dataGridView.EndEdit();
            //da.bindingSource.EndEdit();
            //dataGridView.Invalidate();
            ((DataTable)da.bindingSource.DataSource).Clear();
            da.mySqlDataAdapter.Fill(((DataTable)da.bindingSource.DataSource));
            //((BindingSource)dataGridView.DataSource).ResetBindings(false);
            da.bindingSource.ResetBindings(false);
            //dataGridView.Refresh();
        }

        public virtual void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            /*
            if (((DataGridView)sender)[e.ColumnIndex, e.RowIndex].IsInEditMode)
            {
                switch (((DataGridView)sender)[e.ColumnIndex, e.RowIndex].ValueType.ToString())
                {
                    case "System.Double":
                        if (!Validator.IsDouble(e.FormattedValue.ToString()))
                        {
                            MessageBox.Show(Language.GetErrorText("errorInvalidNumericValue", "Invalid numeric value!"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                        break;
                }
            }
            */
            if (!Validator.DataGridViewCellVallidator(((DataGridView)sender)[e.ColumnIndex, e.RowIndex]))
            {
                e.Cancel = true;
            }
        }

        public virtual void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ((DataGridView)sender)[e.ColumnIndex, e.RowIndex].ErrorText = Language.GetMessageBoxText("dataGridDataError", "There was an unhandled error while performing the requested operation!");
            MessageBox.Show(Language.GetMessageBoxText("dataGridDataError", "There was an unhandled error while performing the requested operation!"), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            LogWriter.Log(e.Exception.Message, SettingsClass.ErrorLogFile);
            e.Cancel = true;
        }

        public virtual void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            /*
            DataGridView x = (DataGridView)sender;
            x.Rows[e.RowIndex].HeaderCell.Style.BackColor = x.Rows[e.RowIndex].Selected ? Color.White : (e.RowIndex % 2 != 0 ? Color.DarkGray : Color.LightGray);
            int RowNumWidth = x.RowCount.ToString().Length;
            StringBuilder RowNumber = new StringBuilder(RowNumWidth);
            RowNumber.Append(e.RowIndex + 1);
            while (RowNumber.Length < RowNumWidth)
                RowNumber.Insert(0, "0");

            // get the size of the row number string
            SizeF Sz = e.Graphics.MeasureString(RowNumber.ToString(), x.Font);

            // adjust the width of the column that contains the row header cells 
            if (x.RowHeadersWidth < (int)(Sz.Width + 20))
                x.RowHeadersWidth = (int)(Sz.Width + 20);

            //e.Graphics.DrawRectangle(Pens.LightGray, e.RowBounds.Left, e.RowBounds.Top, x.RowHeadersWidth, e.RowBounds.Height);
            //e.Graphics.FillRectangle(Brushes.LightGray, e.RowBounds.Left, e.RowBounds.Top, x.RowHeadersWidth, e.RowBounds.Height);

            // draw the row number
            e.Graphics.DrawString(
                RowNumber.ToString(),
                new Font(x.Font.FontFamily, x.Font.Size, FontStyle.Bold),
                SystemBrushes.ControlText,
                e.RowBounds.Location.X + 15,
                e.RowBounds.Location.Y + ((e.RowBounds.Height - Sz.Height) / 2));

            x.Rows[e.RowIndex].HeaderCell.Style.ForeColor = x.Rows[e.RowIndex].Selected ? Color.Red : Color.DarkBlue;
            x.Rows[e.RowIndex].HeaderCell.Style.BackColor = x.Rows[e.RowIndex].Selected ? Color.White : (e.RowIndex % 2 != 0 ? Color.DarkGray : Color.LightGray);
             */
            try
            {
                if (((DataGridView)sender).Columns["invoice"] != null)
                {
                    if (((DataGridView)sender).Rows[e.RowIndex].Cells["status"].Value.ToString().ToLower() == "invoiced")
                    {
                        //((CheckBox)((DataGridView)sender).Controls.Find("checkboxHeader", true)[0]).Enabled = false;
                        ((DataGridView)sender).Rows[e.RowIndex].Cells["invoice"].ReadOnly = true;
                    }
                }
            }
            catch { }
        }

        public virtual void dataGridView_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    // If the mouse moves outside the rectangle, start the drag.
                    if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                    {
                        /*
                        // Proceed with the drag and drop, passing in the list item.                    
                        DragDropEffects dropEffect = dataGridView.DoDragDrop(
                        dataGridView.Rows[rowIndexFromMouseDown],
                        DragDropEffects.Move);
                        */
                        int multiplier = 1;
                        if (lastRowIndexFromMouseDown != dataGridView.HitTest(e.X, e.Y).RowIndex)
                        {
                            double value_to_add = 0;
                            if (dataGridView.HitTest(e.X, e.Y).RowIndex < lastRowIndexFromMouseDown)
                            {
                                multiplier = -1;
                                value_to_add = Convert.ToDouble(dataGridView[colIndexFromMouseDown, lastRowIndexFromMouseDown].Value);
                            }
                            else
                            {
                                multiplier = 1;
                                value_to_add = Convert.ToDouble(dataGridView[colIndexFromMouseDown, dataGridView.HitTest(e.X, e.Y).RowIndex].Value);
                            }
                            Sum += (value_to_add * multiplier);
                            ((UserForm)this.ParentForm).AddLabelToStatusBar(String.Format("{0}LabelSum", this.Name), Sum.ToString(SettingsClass.DoubleFormat));
                        }
                        lastRowIndexFromMouseDown = dataGridView.HitTest(e.X, e.Y).RowIndex;
                    }
                }
            }
            catch { }
        }

        private void dataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Sum = 0;
                ((UserForm)this.ParentForm).RemoveLabelFromStatusBar(String.Format("{0}LabelSum", this.Name));
            }
            catch { }
        }
        
        private void dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Sum = 0;
                if (dataGridView.HitTest(e.X, e.Y).ColumnIndex > -1)
                {
                    // Get the index of the item the mouse is below.
                    rowIndexFromMouseDown = dataGridView.HitTest(e.X, e.Y).RowIndex;
                    colIndexFromMouseDown = dataGridView.HitTest(e.X, e.Y).ColumnIndex;
                    lastRowIndexFromMouseDown = rowIndexFromMouseDown;
                    bool summable_column = false;
                    switch (dataGridView.Columns[colIndexFromMouseDown].ValueType.ToString())
                    {
                        case "System.Double":
                        case "System.Int32":
                            summable_column = true;
                            break;
                        default:
                            summable_column = false;
                            break;
                    }
                    if (rowIndexFromMouseDown != -1 && summable_column)
                    {
                        // Remember the point where the mouse down occurred. 
                        // The DragSize indicates the size that the mouse can move 
                        // before a drag event should be started.                
                        Size dragSize = SystemInformation.DragSize;
                        // Create a rectangle using the DragSize, with the mouse position being
                        // at the center of the rectangle.
                        dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                        e.Y - (dragSize.Height / 2)),
                        dragSize);
                        Sum += Convert.ToDouble(dataGridView[colIndexFromMouseDown, rowIndexFromMouseDown].Value);
                    }
                    else
                        // Reset the rectangle if the mouse is not over an item in the ListBox.
                        dragBoxFromMouseDown = Rectangle.Empty;
                }
            }
            catch { }
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            Sum = 0;
            bool summable_column = false;
            try
            {
                ((UserForm)this.ParentForm).RemoveLabelFromStatusBar(String.Format("{0}LabelSum", this.Name));
                if (colIndexFromMouseDown > -1 && dataGridView.SelectedRows.Count > 1)
                {
                    switch (dataGridView.Columns[colIndexFromMouseDown].ValueType.ToString())
                    {
                        case "System.Double":
                        case "System.Int32":
                            summable_column = true;
                            break;
                        default:
                            summable_column = false;
                            break;
                    }
                    if (summable_column)
                    {
                        foreach (DataGridViewRow dgvr in dataGridView.SelectedRows)
                        {
                            double value_to_add = Convert.ToDouble(dgvr.Cells[colIndexFromMouseDown].Value);
                            Sum += value_to_add;
                        }
                        ((UserForm)this.ParentForm).AddLabelToStatusBar(String.Format("{0}LabelSum", this.Name), Sum.ToString(SettingsClass.DoubleFormat));
                    }
                }
            }
            catch { }
        }

        private void dataGridView_LostFocus(object sender, EventArgs e)
        {
            try
            {
                ((UserForm)this.ParentForm).RemoveLabelFromStatusBar(String.Format("{0}LabelSum", this.Name));
            }
            catch { }
        }

        public void AddLinkColumn(string language_id, string column_name, int insert_index)
        {
            DataGridViewLinkColumn dgvlc = new DataGridViewLinkColumn();
            dgvlc.UseColumnTextForLinkValue = true;
            dgvlc.Text = column_name;
            dgvlc.Name = column_name;
            dgvlc.HeaderText = Language.GetColumnHeaderText(language_id.ToLower(), column_name.ToUpper());
            //dgvlc.DataPropertyName = dgvc.Name;
            dgvlc.ActiveLinkColor = Color.DarkOrange;
            dgvlc.LinkBehavior = LinkBehavior.SystemDefault;
            dgvlc.LinkColor = Color.DarkOrange;
            dgvlc.TrackVisitedState = false;
            dgvlc.VisitedLinkColor = Color.DarkOrange;
            dgvlc.Visible = true;
            dataGridView.Columns.Insert(insert_index, dgvlc);
        }

        public void dataGridView_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (((DataGridView)sender).Columns[e.Column.Index].Name.ToLower() == "month")
            {
                try
                {
                    string[] v1 = e.CellValue1.ToString().Split('/');
                    int month1 = Convert.ToInt32(v1[0].Trim());
                    int year1 = Convert.ToInt32(v1[1].Trim());
                    string[] v2 = e.CellValue2.ToString().Split('/');
                    int month2 = Convert.ToInt32(v2[0].Trim());
                    int year2 = Convert.ToInt32(v2[1].Trim());
                    e.SortResult = String.Compare(year1.ToString(), year2.ToString());
                    if (e.SortResult == 0)
                        e.SortResult = String.Compare(month1.ToString(), month2.ToString());
                }
                catch
                {
                    e.SortResult = System.String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
                }
                e.Handled = true;
            }
        }
        /*
        private void toolStripButton_MouseDown(object sender, MouseEventArgs e)
        {
            //SplashScreen.SplashScreen.ShowSplashScreen();
            SplashScreen.SplashScreen.ShowSplashScreen( ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).X, ((Panel)this.FindForm().Parent).PointToScreen(Point.Empty).Y, ((Panel)this.FindForm().Parent).Width, ((Panel)this.FindForm().Parent).Height);
            //Application.DoEvents();
        }
        */
    }
}
