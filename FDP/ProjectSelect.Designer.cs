using System.Collections.Generic;

namespace FDP
{
    partial class ProjectSelect
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
            this.dataGrid1 = new FDP.DataGrid("PROJECTSsp_select", null, "PROJECTSsp_insert", null, "PROJECTSsp_update", null, "PROJECTSsp_delete", null, null, null, null, new string[] { "CITY_ID" }, null, new string[] { "NAME", "LOCATION", "CITY", "ADDRESS" }, this.Selectable, false);
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
            // ProjectSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(658, 318);
            this.Controls.Add(this.dataGrid1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "ProjectSelect";
            this.Text = "Projects";
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DataGrid dataGrid1;

    }
}