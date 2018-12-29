namespace FDP
{
    partial class InvoiceRequirementSelect
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
            //this.dataGrid1 = new FDP.DataGrid("INVOICEREQUIREMENTSsp_select", null, "INVOICEREQUIREMENTSsp_insert", null, "INVOICEREQUIREMENTSsp_update", null, "INVOICEREQUIREMENTSsp_delete", null, new string[] { "DATE" }, new string[] { "PRICE" }, null, new string[] { "STATUS_ID" }, new string[] { "OWNER", "PROPERTY", "CONTRACT", "RENTCONTRACT" }, new string[] { "OWNER", "CONTRACT", "RENTCONTRACT", "PROPERTY", "SERVICE", "COMMENTS", "PRICE", "CURRENCY", "MONTH", "DATE", "STATUS" }, this.Selectable, true);
            this.dataGrid1 = new FDP.DataGrid("INVOICEREQUIREMENTSsp_select", null, "INVOICEREQUIREMENTSsp_insert", null, "INVOICEREQUIREMENTSsp_update", null, "INVOICEREQUIREMENTSsp_delete", null, new string[] { "DATE" }, new string[] { "PRICE" }, null, null, new string[] { "OWNER", "PROPERTY", "CONTRACT", "RENTCONTRACT", "INVOICE_NUMBER" }, new string[] { "OWNER", "CONTRACT", "RENTCONTRACT", "PROPERTY", "SERVICE", "COMMENTS", "PRICE", "CURRENCY", "MONTH", "DATE", "STATUS", "INVOICE_NUMBER" }, this.Selectable, false);
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
            this.Name = "InvoiceRequirementSelect";
            this.Text = "Invoice Requirements";
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.InvoiceRequirements_Load);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DataGrid dataGrid1;



    }
}