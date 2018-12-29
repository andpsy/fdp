namespace FDP
{
    partial class BankReceiptSelect
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
            this.dataGrid1 = new FDP.DataGrid("BANK_RECEIPTSsp_select", null, "BANK_RECEIPTSsp_insert", null, "BANK_RECEIPTSsp_update", null, "BANK_RECEIPTSsp_delete", null, new string[] { "DATE" }, new string[] { "AMOUNT_PAID" }, null, null, new string[] { "INVOICE" }, new string[] { "NUMBER", "DATE", "AMOUNT_PAID", "INVOICE" }, this.Selectable, false);
            this.panelErrors.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 354);
            this.panelErrors.Size = new System.Drawing.Size(511, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(505, 39);
            // 
            // dataGrid1
            // 
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.IdToReturn = 0;
            this.dataGrid1.Location = new System.Drawing.Point(5, 5);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(511, 349);
            this.dataGrid1.TabIndex = 7;
            // 
            // BankReceiptSelect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(521, 429);
            this.Controls.Add(this.dataGrid1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "BankReceiptSelect";
            this.Text = "Bank Receipts";
            this.Load += new System.EventHandler(this.BankReceiptSelect_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public DataGrid dataGrid1;



    }
}