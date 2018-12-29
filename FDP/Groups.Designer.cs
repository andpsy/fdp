namespace FDP
{
    partial class Groups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Groups));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonAddGroup = new System.Windows.Forms.Button();
            this.buttonEditGroup = new System.Windows.Forms.Button();
            this.buttonDeleteGroup = new System.Windows.Forms.Button();
            this.buttonSetGroupRights = new System.Windows.Forms.Button();
            this.dataGridViewGroups = new System.Windows.Forms.DataGridView();
            this.buttonExit = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelShowAll = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonAddEmployees = new System.Windows.Forms.Button();
            this.panelErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroups)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 168);
            this.panelErrors.Size = new System.Drawing.Size(621, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(615, 39);
            // 
            // buttonAddGroup
            // 
            this.buttonAddGroup.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAddGroup.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAddGroup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAddGroup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAddGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddGroup.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddGroup.ForeColor = System.Drawing.Color.Black;
            this.buttonAddGroup.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddGroup.Image")));
            this.buttonAddGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddGroup.Location = new System.Drawing.Point(3, 3);
            this.buttonAddGroup.Name = "buttonAddGroup";
            this.buttonAddGroup.Size = new System.Drawing.Size(80, 23);
            this.buttonAddGroup.TabIndex = 16;
            this.buttonAddGroup.Text = "Add";
            this.buttonAddGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddGroup.UseVisualStyleBackColor = false;
            this.buttonAddGroup.Click += new System.EventHandler(this.buttonAddGroup_Click);
            // 
            // buttonEditGroup
            // 
            this.buttonEditGroup.BackColor = System.Drawing.Color.DarkGray;
            this.buttonEditGroup.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonEditGroup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonEditGroup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonEditGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditGroup.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditGroup.ForeColor = System.Drawing.Color.Black;
            this.buttonEditGroup.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditGroup.Image")));
            this.buttonEditGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditGroup.Location = new System.Drawing.Point(89, 3);
            this.buttonEditGroup.Name = "buttonEditGroup";
            this.buttonEditGroup.Size = new System.Drawing.Size(80, 23);
            this.buttonEditGroup.TabIndex = 17;
            this.buttonEditGroup.Text = "Edit";
            this.buttonEditGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEditGroup.UseVisualStyleBackColor = false;
            this.buttonEditGroup.Click += new System.EventHandler(this.buttonEditGroup_Click);
            // 
            // buttonDeleteGroup
            // 
            this.buttonDeleteGroup.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDeleteGroup.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonDeleteGroup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonDeleteGroup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonDeleteGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteGroup.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteGroup.ForeColor = System.Drawing.Color.Black;
            this.buttonDeleteGroup.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteGroup.Image")));
            this.buttonDeleteGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteGroup.Location = new System.Drawing.Point(175, 3);
            this.buttonDeleteGroup.Name = "buttonDeleteGroup";
            this.buttonDeleteGroup.Size = new System.Drawing.Size(80, 23);
            this.buttonDeleteGroup.TabIndex = 18;
            this.buttonDeleteGroup.Text = "Delete";
            this.buttonDeleteGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteGroup.UseVisualStyleBackColor = false;
            this.buttonDeleteGroup.Click += new System.EventHandler(this.buttonDeleteGroup_Click);
            // 
            // buttonSetGroupRights
            // 
            this.buttonSetGroupRights.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSetGroupRights.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSetGroupRights.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSetGroupRights.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSetGroupRights.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetGroupRights.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetGroupRights.ForeColor = System.Drawing.Color.Black;
            this.buttonSetGroupRights.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetGroupRights.Image")));
            this.buttonSetGroupRights.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSetGroupRights.Location = new System.Drawing.Point(402, 3);
            this.buttonSetGroupRights.Name = "buttonSetGroupRights";
            this.buttonSetGroupRights.Size = new System.Drawing.Size(120, 23);
            this.buttonSetGroupRights.TabIndex = 20;
            this.buttonSetGroupRights.Text = "Set Rights";
            this.buttonSetGroupRights.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSetGroupRights.UseVisualStyleBackColor = false;
            this.buttonSetGroupRights.Click += new System.EventHandler(this.buttonSetGroupRights_Click);
            // 
            // dataGridViewGroups
            // 
            this.dataGridViewGroups.AllowUserToAddRows = false;
            this.dataGridViewGroups.AllowUserToDeleteRows = false;
            this.dataGridViewGroups.AllowUserToOrderColumns = true;
            this.dataGridViewGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewGroups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewGroups.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewGroups.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewGroups.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewGroups.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewGroups.Location = new System.Drawing.Point(3, 29);
            this.dataGridViewGroups.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewGroups.Name = "dataGridViewGroups";
            this.dataGridViewGroups.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewGroups.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewGroups.Size = new System.Drawing.Size(625, 209);
            this.dataGridViewGroups.TabIndex = 21;
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.Black;
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(547, 3);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 23);
            this.buttonExit.TabIndex = 22;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelFilter,
            this.toolStripStatusLabelShowAll});
            this.statusStrip1.Location = new System.Drawing.Point(5, 216);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(621, 22);
            this.statusStrip1.TabIndex = 23;
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
            // buttonAddEmployees
            // 
            this.buttonAddEmployees.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAddEmployees.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAddEmployees.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAddEmployees.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAddEmployees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddEmployees.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddEmployees.ForeColor = System.Drawing.Color.Black;
            this.buttonAddEmployees.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddEmployees.Image")));
            this.buttonAddEmployees.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddEmployees.Location = new System.Drawing.Point(270, 3);
            this.buttonAddEmployees.Name = "buttonAddEmployees";
            this.buttonAddEmployees.Size = new System.Drawing.Size(126, 23);
            this.buttonAddEmployees.TabIndex = 24;
            this.buttonAddEmployees.Text = "Add Employees";
            this.buttonAddEmployees.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddEmployees.UseVisualStyleBackColor = false;
            this.buttonAddEmployees.Click += new System.EventHandler(this.buttonAddEmployees_Click);
            // 
            // Groups
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(631, 243);
            this.Controls.Add(this.buttonAddEmployees);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.dataGridViewGroups);
            this.Controls.Add(this.buttonSetGroupRights);
            this.Controls.Add(this.buttonDeleteGroup);
            this.Controls.Add(this.buttonEditGroup);
            this.Controls.Add(this.buttonAddGroup);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "Groups";
            this.Text = "Groups";
            this.Load += new System.EventHandler(this.Groups_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.buttonAddGroup, 0);
            this.Controls.SetChildIndex(this.buttonEditGroup, 0);
            this.Controls.SetChildIndex(this.buttonDeleteGroup, 0);
            this.Controls.SetChildIndex(this.buttonSetGroupRights, 0);
            this.Controls.SetChildIndex(this.dataGridViewGroups, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.Controls.SetChildIndex(this.buttonAddEmployees, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroups)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddGroup;
        private System.Windows.Forms.Button buttonEditGroup;
        private System.Windows.Forms.Button buttonDeleteGroup;
        private System.Windows.Forms.Button buttonSetGroupRights;
        private System.Windows.Forms.DataGridView dataGridViewGroups;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFilter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelShowAll;
        private System.Windows.Forms.Button buttonAddEmployees;
    }
}