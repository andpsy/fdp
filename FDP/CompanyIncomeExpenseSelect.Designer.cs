namespace FDP
{
    partial class CompanyIncomeExpenseSelect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //this.dataGrid1 = new FDP.DataGrid();
            //this.dataGrid1 = new FDP.DataGrid("COMPANY_INCOME_EXPENSESsp_select", null, "COMPANY_INCOME_EXPENSESsp_insert", null, "COMPANY_INCOME_EXPENSESsp_update", null, "COMPANY_INCOME_EXPENSESsp_delete", null, new string[] { "DATE" }, new string[] { "AMOUNT" }, null, null, new string[] { "OWNER", "PROPERTY", "INVOICE" }, new string[] { "TYPE", "OWNER", "PROPERTY", "AMOUNT", "CURRENCY", "MONTH", "DATE", "SERVICE", "SERVICE_DESCRIPTION", "INVOICE", "STATUS", "BALLANCE", "SOURCE", "BANK_ACCOUNT_DETAILS" }, this.Selectable, false);
            this.dataGrid1 = new FDP.DataGrid("COMPANY_INCOME_EXPENSESsp_select", null, "COMPANY_INCOME_EXPENSESsp_insert", null, "COMPANY_INCOME_EXPENSESsp_update", null, "COMPANY_INCOME_EXPENSESsp_delete", null, new string[] { "DATE" }, new string[] { "AMOUNT" }, null, null, new string[] { "OWNER", "PROPERTY", "INVOICE" }, new string[] { "TYPE", "OWNER", "PROPERTY", "AMOUNT", "CURRENCY", "MONTH", "DATE", "SERVICE", "SERVICE_DESCRIPTION", "INVOICE", "STATUS", "AMOUNT_PAID", "AMOUNT_PAID_RON", "BALLANCE", "SOURCE", "BANK_ACCOUNT_DETAILS", "ORIGINAL AMOUNT (EUR)", "ORIGINAL AMOUNT (RON)", "VAT (EUR)", "VAT (RON)", "TOTAL (EUR)", "TOTAL (RON)", "AMOUNT PAID (EUR)", "AMOUNT PAID (RON)", "BALLANCE (EUR)", "BALLANCE (RON)" }, this.Selectable, false);

            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Location = new System.Drawing.Point(5, 5);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(511, 419);
            this.dataGrid1.TabIndex = 7;
            // 
            // InvoiceRequirements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(521, 429);
            this.Controls.Add(this.dataGrid1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "CompanyIncomeExpenseSelect";
            this.Text = "Company Income/Expenses";
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CompanyIncomeExpenseSelect_Load);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DataGrid dataGrid1;



    }
}