using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FDP
{
    public partial class Currencies : UserForm
    {
        public DataAccess da = new DataAccess("CURRENCIESsp_select", "CURRENCIESsp_insert", "CURRENCIESsp_update", "CURRENCIESsp_delete");

        public Currencies()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            //Language.LoadLabels(this);

            dataGridViewCurrencies.BindingContextChanged += new EventHandler(dataGridViewCurrencies_BindingContextChanged);
            dataGridViewCurrencies.KeyDown += new KeyEventHandler(dataGridViewCurrencies_KeyDown);
            dataGridViewCurrencies.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridViewCurrencies_DataBindingComplete);
            dataGridViewCurrencies.DataSourceChanged += new EventHandler(dataGridViewCurrencies_DataSourceChanged);

            toolStripStatusLabelShowAll.Click += new EventHandler(toolStripStatusLabelShowAll_Click);
        }

        private void CurrenciesForm_Load(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            dataGridViewCurrencies.DataSource = da.bindingSource;
            dataGridViewCurrencies.Columns["id"].Visible = false;           
        }

        private void dataGridViewCurrencies_DataSourceChanged(object sender, EventArgs e)
        {
            Language.PopulateGridColumnHeaders((DataGridView)sender);
        }

        private void dataGridViewCurrencies_BindingContextChanged(object sender, EventArgs e)
        {
            // Continue only if the data source has been set.
            if (dataGridViewCurrencies.DataSource == null)
            {
                return;
            }

            // Add the AutoFilter header cell to each column.
            foreach (DataGridViewColumn col in dataGridViewCurrencies.Columns)
            {
                col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
            }

            // Format the OrderTotal column as currency. 
            //dataGridViewEmployees.Columns["OrderTotal"].DefaultCellStyle.Format = "c";

            // Resize the columns to fit their contents.
            //dataGridViewEmployees.AutoResizeColumns();
        }

        // Displays the drop-down list when the user presses 
        // ALT+DOWN ARROW or ALT+UP ARROW.
        private void dataGridViewCurrencies_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up))
            {
                DataGridViewAutoFilterColumnHeaderCell filterCell = dataGridViewCurrencies.CurrentCell.OwningColumn.HeaderCell as DataGridViewAutoFilterColumnHeaderCell;
                if (filterCell != null)
                {
                    filterCell.ShowDropDownList();
                    e.Handled = true;
                }
            }
        }

        // Updates the filter status label. 
        private void dataGridViewCurrencies_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            String filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dataGridViewCurrencies);
            if (String.IsNullOrEmpty(filterStatus))
            {
                statusStrip1.Visible = false;
                toolStripStatusLabelShowAll.Visible = false;
                toolStripStatusLabelFilter.Visible = false;
            }
            else
            {
                statusStrip1.Visible = true;
                toolStripStatusLabelShowAll.Visible = true;
                toolStripStatusLabelFilter.Visible = true;
                toolStripStatusLabelFilter.Text = filterStatus;
            }
        }

        // Clears the filter when the user clicks the "Show All" link
        // or presses ALT+A. 
        private void toolStripStatusLabelShowAll_Click(object sender, EventArgs e)
        {
            DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(dataGridViewCurrencies);
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGetMissingCurrencies_Click(object sender, EventArgs e)
        {
            try
            {
                CurrenciesClass.GetMissingCurrencies(DateTime.Now.Year);
                //Currencies.GetMissingCurrencies(2011);
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorGettingMissingCurrencies", "There was an error getting the missing exchange rates:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
