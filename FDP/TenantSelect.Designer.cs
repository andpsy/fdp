namespace FDP
{
    partial class TenantSelect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TenantSelect));
            this.dataGridViewTenants = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelShowAll = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonDeleteTenant = new System.Windows.Forms.Button();
            this.buttonEditTenant = new System.Windows.Forms.Button();
            this.buttonAddTenant = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            //this.dataGrid1 = new FDP.DataGrid();
            this.dataGrid1 = new DataGrid("TENANTSsp_select", null, "TENANTSsp_insert", null, "TENANTSsp_update", null, "TENANTSsp_delete", null, null, null, null, null, null, new string[] { "NAME", "FULL_NAME", "PHONES", "EMAILS" }, this.Selectable, false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTenants)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTenants
            // 
            this.dataGridViewTenants.AllowUserToAddRows = false;
            this.dataGridViewTenants.AllowUserToDeleteRows = false;
            this.dataGridViewTenants.AllowUserToOrderColumns = true;
            this.dataGridViewTenants.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTenants.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTenants.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewTenants.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTenants.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTenants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTenants.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTenants.Location = new System.Drawing.Point(5, 255);
            this.dataGridViewTenants.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewTenants.Name = "dataGridViewTenants";
            this.dataGridViewTenants.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTenants.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTenants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTenants.Size = new System.Drawing.Size(627, 70);
            this.dataGridViewTenants.TabIndex = 22;
            this.dataGridViewTenants.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelFilter,
            this.toolStripStatusLabelShowAll});
            this.statusStrip1.Location = new System.Drawing.Point(5, 271);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(626, 22);
            this.statusStrip1.TabIndex = 26;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // toolStripStatusLabelFilter
            // 
            this.toolStripStatusLabelFilter.Name = "toolStripStatusLabelFilter";
            this.toolStripStatusLabelFilter.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelShowAll
            // 
            this.toolStripStatusLabelShowAll.IsLink = true;
            this.toolStripStatusLabelShowAll.Name = "toolStripStatusLabelShowAll";
            this.toolStripStatusLabelShowAll.Size = new System.Drawing.Size(47, 17);
            this.toolStripStatusLabelShowAll.Text = "Show All";
            this.toolStripStatusLabelShowAll.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.Black;
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(551, 329);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 23);
            this.buttonExit.TabIndex = 30;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Visible = false;
            // 
            // buttonDeleteTenant
            // 
            this.buttonDeleteTenant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteTenant.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDeleteTenant.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonDeleteTenant.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonDeleteTenant.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonDeleteTenant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteTenant.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteTenant.ForeColor = System.Drawing.Color.Black;
            this.buttonDeleteTenant.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteTenant.Image")));
            this.buttonDeleteTenant.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteTenant.Location = new System.Drawing.Point(177, 329);
            this.buttonDeleteTenant.Name = "buttonDeleteTenant";
            this.buttonDeleteTenant.Size = new System.Drawing.Size(80, 23);
            this.buttonDeleteTenant.TabIndex = 29;
            this.buttonDeleteTenant.Text = "Delete";
            this.buttonDeleteTenant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteTenant.UseVisualStyleBackColor = false;
            this.buttonDeleteTenant.Visible = false;
            // 
            // buttonEditTenant
            // 
            this.buttonEditTenant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEditTenant.BackColor = System.Drawing.Color.DarkGray;
            this.buttonEditTenant.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonEditTenant.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonEditTenant.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonEditTenant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditTenant.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditTenant.ForeColor = System.Drawing.Color.Black;
            this.buttonEditTenant.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditTenant.Image")));
            this.buttonEditTenant.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditTenant.Location = new System.Drawing.Point(91, 329);
            this.buttonEditTenant.Name = "buttonEditTenant";
            this.buttonEditTenant.Size = new System.Drawing.Size(80, 23);
            this.buttonEditTenant.TabIndex = 28;
            this.buttonEditTenant.Text = "Edit";
            this.buttonEditTenant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEditTenant.UseVisualStyleBackColor = false;
            this.buttonEditTenant.Visible = false;
            // 
            // buttonAddTenant
            // 
            this.buttonAddTenant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddTenant.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAddTenant.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAddTenant.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAddTenant.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAddTenant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddTenant.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddTenant.ForeColor = System.Drawing.Color.Black;
            this.buttonAddTenant.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddTenant.Image")));
            this.buttonAddTenant.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddTenant.Location = new System.Drawing.Point(5, 329);
            this.buttonAddTenant.Name = "buttonAddTenant";
            this.buttonAddTenant.Size = new System.Drawing.Size(80, 23);
            this.buttonAddTenant.TabIndex = 27;
            this.buttonAddTenant.Text = "Add";
            this.buttonAddTenant.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddTenant.UseVisualStyleBackColor = false;
            this.buttonAddTenant.Visible = false;
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelect.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSelect.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelect.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelect.ForeColor = System.Drawing.Color.Black;
            this.buttonSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSelect.Location = new System.Drawing.Point(304, 213);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(80, 23);
            this.buttonSelect.TabIndex = 37;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSelect.UseVisualStyleBackColor = false;
            this.buttonSelect.Visible = false;
            // 
            // dataGrid1
            // 
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Location = new System.Drawing.Point(5, 5);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(627, 371);
            this.dataGrid1.TabIndex = 31;
            // 
            // TenantSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.ClientSize = new System.Drawing.Size(637, 381);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonDeleteTenant);
            this.Controls.Add(this.buttonEditTenant);
            this.Controls.Add(this.buttonAddTenant);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridViewTenants);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "TenantSelect";
            this.Text = "TenantSelect";
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TenantSelect_Load);
            this.Controls.SetChildIndex(this.dataGridViewTenants, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.Controls.SetChildIndex(this.buttonAddTenant, 0);
            this.Controls.SetChildIndex(this.buttonEditTenant, 0);
            this.Controls.SetChildIndex(this.buttonDeleteTenant, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTenants)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTenants;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFilter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelShowAll;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonDeleteTenant;
        private System.Windows.Forms.Button buttonEditTenant;
        private System.Windows.Forms.Button buttonAddTenant;
        public System.Windows.Forms.Button buttonSelect;
        public DataGrid dataGrid1;
     }
}