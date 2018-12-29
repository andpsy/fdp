using System.Collections.Generic;

namespace FDP
{
    partial class RentContractSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RentContractSelect));
            //this.dataGrid1 = new FDP.DataGrid();
            //this.dataGrid1 = new FDP.DataGrid("RENTCONTRACTSsp_select", null, "RENTCONTRACTSsp_insert", null, "RENTCONTRACTSsp_update", null, "RENTCONTRACTSsp_delete", null, new string[] { "START_DATE", "FINISH_DATE" }, new string[] { "RENT", "RENT_VAT_INCLUDED", "DEPOSIT" }, new string[] { "SPLITTING", "PROLONGATION", "REAL_ESTATE_AGENCY" }, new string[] { "STATUS_ID", "OWNER_ID", "PROPERTY_ID", "TENANT_ID", "REGISTERED_ID", "PAYMENT_LIMIT_ID" }, new string[] { "OWNER", "TENANT", "PROPERTY", "PARENT_RENTCONTRACT_NUMBER"}, new string[] { "NUMBER", "ADDENDUM_NUMBER", "ADDENDUM_DATE", "TENANT", "OWNER", "PROPERTY", "START_DATE", "FINISH_DATE", "CURRENCY", "STATUS", "SPLITTING", "PROLONGATION", "REGISTERED", "REGISTRY_NUMBER", "RENT", "RENT_VAT_INCLUDED", "DEPOSIT", "PARENT_RENTCONTRACT_NUMBER" }, this.Selectable, false);
            this.dataGrid1 = new FDP.DataGrid("RENTCONTRACTSsp_select", null, "RENTCONTRACTSsp_insert", null, "RENTCONTRACTSsp_update", null, "RENTCONTRACTSsp_delete_chain", null, new string[] { "START_DATE", "FINISH_DATE" }, new string[] { "RENT", "RENT_VAT_INCLUDED", "DEPOSIT" }, new string[] { "SPLITTING", "PROLONGATION", "REAL_ESTATE_AGENCY" }, new string[] { "STATUS_ID", "OWNER_ID", "PROPERTY_ID", "TENANT_ID", "REGISTERED_ID", "PAYMENT_LIMIT_ID" }, new string[] { "OWNER", "TENANTS", "PROPERTY", "PARENT_RENTCONTRACT_NUMBER" }, new string[] { "NUMBER", "ADDENDUM_NUMBER", "ADDENDUM_DATE", "TENANTS", "OWNER", "PROPERTY", "START_DATE", "FINISH_DATE", "CURRENCY", "STATUS", "SPLITTING", "PROLONGATION", "REGISTERED", "REGISTRY_NUMBER", "RENT", "RENT_VAT_INCLUDED", "DEPOSIT", "PARENT_RENTCONTRACT_NUMBER" }, this.Selectable, false);
            this.dataGrid1.dataGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dataGridView_RowsRemoved);
            this.dataGrid1.dataGridView.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dataGridView_RowsAdded);
            this.buttonAddAddendum = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Location = new System.Drawing.Point(5, 5);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(648, 308);
            this.dataGrid1.TabIndex = 7;
            // 
            // buttonAddAddendum
            // 
            this.buttonAddAddendum.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonAddAddendum.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAddAddendum.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAddAddendum.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAddAddendum.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAddAddendum.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAddAddendum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddAddendum.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddAddendum.ForeColor = System.Drawing.Color.Black;
            this.buttonAddAddendum.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddAddendum.Image")));
            this.buttonAddAddendum.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddAddendum.Location = new System.Drawing.Point(300, 259);
            this.buttonAddAddendum.Name = "buttonAddAddendum";
            this.buttonAddAddendum.Size = new System.Drawing.Size(122, 23);
            this.buttonAddAddendum.TabIndex = 37;
            this.buttonAddAddendum.Text = "Add addendum";
            this.buttonAddAddendum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddAddendum.UseVisualStyleBackColor = false;
            this.buttonAddAddendum.Visible = false;
            this.buttonAddAddendum.Click += new System.EventHandler(this.buttonAddAddendum_Click);
            // 
            // RentContractSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(658, 318);
            this.Controls.Add(this.buttonAddAddendum);
            this.Controls.Add(this.dataGrid1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "RentContractSelect";
            this.Text = "Rent Contract Select";
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RentContractSelect_Load);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.Controls.SetChildIndex(this.buttonAddAddendum, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DataGrid dataGrid1;
        public System.Windows.Forms.Button buttonAddAddendum;

    }
}