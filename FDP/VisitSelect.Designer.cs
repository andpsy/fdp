namespace FDP
{
    partial class VisitSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoOwnerSelect));
            this.dataGridViewCoOwners = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelShowAll = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonDeleteCoOwner = new System.Windows.Forms.Button();
            this.buttonEditCoOwner = new System.Windows.Forms.Button();
            this.buttonAddCoOwner = new System.Windows.Forms.Button();
            //this.dataGrid1 = new FDP.DataGrid();
            this.dataGrid1 = new DataGrid("VISITSsp_select", null, "VISITSsp_insert", null, "VISITSsp_update", null, "VISITSsp_delete", null, new string[]{"DATE"}, null, null, new string[] { "PROPERTY_ID", "VISITREASON_ID", "EMPLOYEE_ID"}, new string[] { "PROPERTY"}, new string[] { "DATE", "PROPERTY", "REASON", "EMPLOYEE"}, this.Selectable, false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCoOwners)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewCoOwners
            // 
            this.dataGridViewCoOwners.AllowUserToAddRows = false;
            this.dataGridViewCoOwners.AllowUserToDeleteRows = false;
            this.dataGridViewCoOwners.AllowUserToOrderColumns = true;
            this.dataGridViewCoOwners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCoOwners.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCoOwners.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewCoOwners.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCoOwners.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCoOwners.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCoOwners.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewCoOwners.Location = new System.Drawing.Point(5, 278);
            this.dataGridViewCoOwners.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewCoOwners.Name = "dataGridViewCoOwners";
            this.dataGridViewCoOwners.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCoOwners.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewCoOwners.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCoOwners.Size = new System.Drawing.Size(627, 31);
            this.dataGridViewCoOwners.TabIndex = 22;
            this.dataGridViewCoOwners.Visible = false;
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
            this.buttonExit.Location = new System.Drawing.Point(551, 313);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 23);
            this.buttonExit.TabIndex = 30;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Visible = false;
            // 
            // buttonDeleteCoOwner
            // 
            this.buttonDeleteCoOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteCoOwner.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDeleteCoOwner.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonDeleteCoOwner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonDeleteCoOwner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonDeleteCoOwner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteCoOwner.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteCoOwner.ForeColor = System.Drawing.Color.Black;
            this.buttonDeleteCoOwner.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteCoOwner.Image")));
            this.buttonDeleteCoOwner.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteCoOwner.Location = new System.Drawing.Point(177, 313);
            this.buttonDeleteCoOwner.Name = "buttonDeleteCoOwner";
            this.buttonDeleteCoOwner.Size = new System.Drawing.Size(80, 23);
            this.buttonDeleteCoOwner.TabIndex = 29;
            this.buttonDeleteCoOwner.Text = "Delete";
            this.buttonDeleteCoOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteCoOwner.UseVisualStyleBackColor = false;
            this.buttonDeleteCoOwner.Visible = false;
            // 
            // buttonEditCoOwner
            // 
            this.buttonEditCoOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEditCoOwner.BackColor = System.Drawing.Color.DarkGray;
            this.buttonEditCoOwner.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonEditCoOwner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonEditCoOwner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonEditCoOwner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditCoOwner.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditCoOwner.ForeColor = System.Drawing.Color.Black;
            this.buttonEditCoOwner.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditCoOwner.Image")));
            this.buttonEditCoOwner.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditCoOwner.Location = new System.Drawing.Point(91, 313);
            this.buttonEditCoOwner.Name = "buttonEditCoOwner";
            this.buttonEditCoOwner.Size = new System.Drawing.Size(80, 23);
            this.buttonEditCoOwner.TabIndex = 28;
            this.buttonEditCoOwner.Text = "Edit";
            this.buttonEditCoOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEditCoOwner.UseVisualStyleBackColor = false;
            this.buttonEditCoOwner.Visible = false;
            // 
            // buttonAddCoOwner
            // 
            this.buttonAddCoOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddCoOwner.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAddCoOwner.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAddCoOwner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAddCoOwner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAddCoOwner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddCoOwner.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddCoOwner.ForeColor = System.Drawing.Color.Black;
            this.buttonAddCoOwner.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddCoOwner.Image")));
            this.buttonAddCoOwner.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddCoOwner.Location = new System.Drawing.Point(5, 313);
            this.buttonAddCoOwner.Name = "buttonAddCoOwner";
            this.buttonAddCoOwner.Size = new System.Drawing.Size(80, 23);
            this.buttonAddCoOwner.TabIndex = 27;
            this.buttonAddCoOwner.Text = "Add";
            this.buttonAddCoOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddCoOwner.UseVisualStyleBackColor = false;
            this.buttonAddCoOwner.Visible = false;
            // 
            // dataGrid1
            // 
            this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid1.Location = new System.Drawing.Point(5, 5);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(627, 355);
            this.dataGrid1.TabIndex = 31;
            // 
            // CoOwnerSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.ClientSize = new System.Drawing.Size(637, 365);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonDeleteCoOwner);
            this.Controls.Add(this.buttonEditCoOwner);
            this.Controls.Add(this.buttonAddCoOwner);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridViewCoOwners);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "VisitSelect";
            this.Text = "Inspections";
            this.Controls.SetChildIndex(this.dataGridViewCoOwners, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.Controls.SetChildIndex(this.buttonAddCoOwner, 0);
            this.Controls.SetChildIndex(this.buttonEditCoOwner, 0);
            this.Controls.SetChildIndex(this.buttonDeleteCoOwner, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            this.Load += new System.EventHandler(VisitSelect_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCoOwners)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCoOwners;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFilter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelShowAll;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonDeleteCoOwner;
        private System.Windows.Forms.Button buttonEditCoOwner;
        private System.Windows.Forms.Button buttonAddCoOwner;
        public DataGrid dataGrid1;

    }
}