﻿namespace FDP
{
    partial class ContractsServices
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
            this.dataGrid1 = new FDP.DataGrid("CONTRACTS_PROPERTIES_SERVICESsp_select_by_contract_id", new object[]{new MySql.Data.MySqlClient.MySqlParameter("_CONTRACT_ID", ContractId)} , "CONTRACTS_PROPERTIES_SERVICESsp_insert", null, "CONTRACTS_PROPERTIES_SERVICESsp_update", null, "CONTRACTS_PROPERTIES_SERVICESsp_delete", null, null, null, null, null, null, null, this.Selectable, false);
            this.SuspendLayout();
            // 
            // pictureBoxExit
            // 
            // 
            // dataGrid1
            // 
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Location = new System.Drawing.Point(5, 5);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(511, 419);
            this.dataGrid1.TabIndex = 7;
            // 
            // Cities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(521, 429);
            this.Controls.Add(this.dataGrid1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "Services";
            this.Text = "Services";
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Load += new System.EventHandler(ContractsServices_Load);
        }

        #endregion

        public DataGrid dataGrid1;



    }
}