using System.Collections.Generic;

namespace FDP
{
    partial class ProportySelect
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
            this.dataGrid1 = new FDP.DataGrid("PROPERTIESsp_select", null, "PROPERTIESsp_insert", null, "PROPERTIESsp_update", null, "PROPERTIESsp_delete", null, new string[] { "OPTIONAL_INSURANCE_POLICY_EXPIRATION_DATE", "PURCHASE_DATE", "MANDATORY_INSURANCE_POLICY_EXPIRATION_DATE", "REEVALUATION_DATE" }, new string[] { "APPARTMENT_AREA", "TERRACE_AREA", "TOTAL_AREA", "COMMON_SPACES", "TOTAL_AREA_INCLUDING_COMMON_SPACES", "INDIVIDUAL_COMPLETELY_FURNISHED_PRICE", "COMPANY_COMPLETELY_FURNISHED_PRICE", "INDIVIDUAL_KITCHEN_FURNISHED_PRICED", "COMPANY_KITCHEN_FURNISHED_PRICE", "INDIVIDUAL_EMPTY_PRICE", "COMPANY_EMPTY_PRICE", "SELLING_PRICE", "VAT", "NOTARY_TAXES", "AGENCY_COMMISSION", "CONDOMINIUM_FEE", "FLOATING_CAPITAL", "AVERAGE_APPROXIMATIVE_CONSUMPTION", "REEVALUATION_VALUE" }, new string[] { "PROPERTY_MANAGEMENT", "POA", "BATHROOM", "STORAGE_ROOM", "VAT_APPLICABLE", "INCLUDE_FOR_AGENCIES", "CENTRAL_HEATING", "REGISTERED_PROPERTY" }, new string[] { "STATUS_ID", "OWNER_ID", "TYPE_ID", "PROJECT_ID", "CITY_ID", "PARENT_PROPERTY_ID" }, new string[] { "OWNER", "PROJECT", "PARENT_PROPERTY" }, new string[] { "NAME", "OWNER", "STATUS", "PROPERTY_MANAGEMENT", "TYPE", "PROJECT", "POA" }, this.Selectable, false);
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
            // ProportySelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(658, 318);
            this.Controls.Add(this.dataGrid1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "ProportySelect";
            this.Text = "ProportySelect";
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DataGrid dataGrid1;

    }
}