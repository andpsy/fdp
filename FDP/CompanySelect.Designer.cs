namespace FDP
{
    partial class CompanySelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompanySelect));
            //this.dataGrid1 = new FDP.DataGrid();
            this.dataGrid1 = new FDP.DataGrid("COMPANIESsp_select", null, "COMPANIESsp_insert", null, "COMPANIESsp_update", null, "COMPANIESsp_delete", null, null, null, null, null, null, new string[] { "NAME", "CIF", "CUI" }, false, false); 
            this.userButtonDropSchema = new FDP.UserButton();
            this.userButtonCreateDatabase = new FDP.UserButton();
            this.panelErrors.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 251);
            this.panelErrors.Size = new System.Drawing.Size(675, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(669, 39);
            // 
            // dataGrid1
            // 
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.IdToReturn = 0;
            this.dataGrid1.Location = new System.Drawing.Point(5, 5);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(675, 246);
            this.dataGrid1.TabIndex = 9;
            this.dataGrid1.Load += new System.EventHandler(this.dataGrid1_Load);
            // 
            // userButtonDropSchema
            // 
            this.userButtonDropSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.userButtonDropSchema.BackColor = System.Drawing.Color.DarkGray;
            this.userButtonDropSchema.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.userButtonDropSchema.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.userButtonDropSchema.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.userButtonDropSchema.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userButtonDropSchema.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userButtonDropSchema.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.userButtonDropSchema.Location = new System.Drawing.Point(405, 267);
            this.userButtonDropSchema.Name = "userButtonDropSchema";
            this.userButtonDropSchema.Size = new System.Drawing.Size(80, 23);
            this.userButtonDropSchema.TabIndex = 10;
            this.userButtonDropSchema.Text = "Delete DB";
            this.userButtonDropSchema.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.userButtonDropSchema.UseVisualStyleBackColor = false;
            this.userButtonDropSchema.Visible = false;
            this.userButtonDropSchema.Click += new System.EventHandler(this.userButtonDropSchema_Click);
            // 
            // userButtonCreateDatabase
            // 
            this.userButtonCreateDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.userButtonCreateDatabase.BackColor = System.Drawing.Color.DarkGray;
            this.userButtonCreateDatabase.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.userButtonCreateDatabase.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.userButtonCreateDatabase.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.userButtonCreateDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userButtonCreateDatabase.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userButtonCreateDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.userButtonCreateDatabase.Location = new System.Drawing.Point(491, 267);
            this.userButtonCreateDatabase.Name = "userButtonCreateDatabase";
            this.userButtonCreateDatabase.Size = new System.Drawing.Size(80, 23);
            this.userButtonCreateDatabase.TabIndex = 11;
            this.userButtonCreateDatabase.Text = "Create DB";
            this.userButtonCreateDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.userButtonCreateDatabase.UseVisualStyleBackColor = false;
            this.userButtonCreateDatabase.Visible = false;
            this.userButtonCreateDatabase.Click += new System.EventHandler(this.userButtonCreateDatabase_Click);
            // 
            // CompanySelect
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(685, 326);
            this.Controls.Add(this.userButtonCreateDatabase);
            this.Controls.Add(this.userButtonDropSchema);
            this.Controls.Add(this.dataGrid1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CompanySelect";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.Text = "FDP Company select";
            this.Load += new System.EventHandler(this.CompanySelect_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.Controls.SetChildIndex(this.userButtonDropSchema, 0);
            this.Controls.SetChildIndex(this.userButtonCreateDatabase, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public DataGrid dataGrid1;
        private UserButton userButtonDropSchema;
        private UserButton userButtonCreateDatabase;
    }
}