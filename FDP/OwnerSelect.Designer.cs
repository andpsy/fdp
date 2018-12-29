namespace FDP
{
    partial class OwnerSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OwnerSelect));
            this.dataGridViewOwners = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelShowAll = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonDeleteOwner = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAddOwner = new System.Windows.Forms.Button();
            this.buttonSelect = new System.Windows.Forms.Button();
            //this.dataGrid1 = new FDP.DataGrid();
            this.dataGrid1 = new DataGrid("OWNERSsp_select", null, "OWNERSsp_insert", null, "OWNERSsp_update", null, "OWNERSsp_delete", null, new string[] { "PASSPORT_EXPIRATION_DATE" }, null, null, new string[] { "STATUS_ID", "TYPE_ID", "CITY_ID" }, new string[] { "CO_OWNERS" }, new string[] { "NAME", "FULL_NAME", "STATUS", "TYPE", "CIF", "CUI", "CNP", "COMMERCIAL_REGISTER_NUMBER", "CITY", "OWNERS" }, this.Selectable, false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOwners)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewOwners
            // 
            this.dataGridViewOwners.AllowUserToAddRows = false;
            this.dataGridViewOwners.AllowUserToDeleteRows = false;
            this.dataGridViewOwners.AllowUserToOrderColumns = true;
            this.dataGridViewOwners.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewOwners.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewOwners.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewOwners.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOwners.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewOwners.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewOwners.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewOwners.Location = new System.Drawing.Point(5, 299);
            this.dataGridViewOwners.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewOwners.Name = "dataGridViewOwners";
            this.dataGridViewOwners.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewOwners.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewOwners.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOwners.Size = new System.Drawing.Size(627, 52);
            this.dataGridViewOwners.TabIndex = 22;
            this.dataGridViewOwners.Visible = false;
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
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.Black;
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(551, 355);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 23);
            this.buttonExit.TabIndex = 30;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Visible = false;
            // 
            // buttonDeleteOwner
            // 
            this.buttonDeleteOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteOwner.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDeleteOwner.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonDeleteOwner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonDeleteOwner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonDeleteOwner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteOwner.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteOwner.ForeColor = System.Drawing.Color.Black;
            this.buttonDeleteOwner.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteOwner.Image")));
            this.buttonDeleteOwner.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteOwner.Location = new System.Drawing.Point(177, 355);
            this.buttonDeleteOwner.Name = "buttonDeleteOwner";
            this.buttonDeleteOwner.Size = new System.Drawing.Size(80, 23);
            this.buttonDeleteOwner.TabIndex = 29;
            this.buttonDeleteOwner.Text = "Delete";
            this.buttonDeleteOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteOwner.UseVisualStyleBackColor = false;
            this.buttonDeleteOwner.Visible = false;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEdit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonEdit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEdit.ForeColor = System.Drawing.Color.Black;
            this.buttonEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonEdit.Image")));
            this.buttonEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEdit.Location = new System.Drawing.Point(91, 355);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(80, 23);
            this.buttonEdit.TabIndex = 28;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Visible = false;
            // 
            // buttonAddOwner
            // 
            this.buttonAddOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddOwner.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAddOwner.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAddOwner.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAddOwner.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAddOwner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddOwner.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddOwner.ForeColor = System.Drawing.Color.Black;
            this.buttonAddOwner.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddOwner.Image")));
            this.buttonAddOwner.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddOwner.Location = new System.Drawing.Point(5, 355);
            this.buttonAddOwner.Name = "buttonAddOwner";
            this.buttonAddOwner.Size = new System.Drawing.Size(80, 23);
            this.buttonAddOwner.TabIndex = 27;
            this.buttonAddOwner.Text = "Add";
            this.buttonAddOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddOwner.UseVisualStyleBackColor = false;
            this.buttonAddOwner.Visible = false;
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
            this.buttonSelect.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelect.Image")));
            this.buttonSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSelect.Location = new System.Drawing.Point(304, 355);
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
            this.dataGrid1.Size = new System.Drawing.Size(627, 397);
            this.dataGrid1.TabIndex = 38;
            //dataGrid1.dataGridView.Paint += new System.Windows.Forms.PaintEventHandler(dataGridView_DataSourceChanged);
            dataGrid1.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dataGridView_RowPostPaint);
            // 
            // OwnerSelect
            //
            //this.Opacity = 0;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.ClientSize = new System.Drawing.Size(637, 407);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonDeleteOwner);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAddOwner);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridViewOwners);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "OwnerSelect";
            this.Text = "OwnerSelect";
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OwnerSelect_Load);
            this.Controls.SetChildIndex(this.dataGridViewOwners, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.Controls.SetChildIndex(this.buttonAddOwner, 0);
            this.Controls.SetChildIndex(this.buttonEdit, 0);
            this.Controls.SetChildIndex(this.buttonDeleteOwner, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.buttonSelect, 0);
            this.Controls.SetChildIndex(this.dataGrid1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOwners)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFilter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelShowAll;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonDeleteOwner;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAddOwner;
        public System.Windows.Forms.Button buttonSelect;
        public System.Windows.Forms.DataGridView dataGridViewOwners;
        public DataGrid dataGrid1;

    }
}