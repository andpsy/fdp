namespace FDP
{
    partial class Lists
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lists));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonExit = new System.Windows.Forms.Button();
            this.listBoxListTypes = new System.Windows.Forms.ListBox();
            this.groupBoxListTypes = new System.Windows.Forms.GroupBox();
            this.buttonDeleteListType = new System.Windows.Forms.Button();
            this.buttonAddListType = new System.Windows.Forms.Button();
            this.userTextBoxNewListType = new FDP.UserTextBox();
            this.dataGridViewList = new System.Windows.Forms.DataGridView();
            this.groupBoxLists = new System.Windows.Forms.GroupBox();
            this.buttonDeleteListItem = new System.Windows.Forms.Button();
            this.buttonEditListItem = new System.Windows.Forms.Button();
            this.buttonAddListItem = new System.Windows.Forms.Button();
            this.groupBoxListTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).BeginInit();
            this.groupBoxLists.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonExit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.Black;
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(290, 206);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 23);
            this.buttonExit.TabIndex = 11;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // listBoxListTypes
            // 
            this.listBoxListTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxListTypes.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBoxListTypes.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.listBoxListTypes.FormattingEnabled = true;
            this.listBoxListTypes.ItemHeight = 11;
            this.listBoxListTypes.Location = new System.Drawing.Point(16, 49);
            this.listBoxListTypes.Name = "listBoxListTypes";
            this.listBoxListTypes.ScrollAlwaysVisible = true;
            this.listBoxListTypes.Size = new System.Drawing.Size(269, 59);
            this.listBoxListTypes.TabIndex = 12;
            this.listBoxListTypes.SelectedIndexChanged += new System.EventHandler(this.listBoxListTypes_SelectedIndexChanged);
            // 
            // groupBoxListTypes
            // 
            this.groupBoxListTypes.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxListTypes.Controls.Add(this.buttonDeleteListType);
            this.groupBoxListTypes.Controls.Add(this.buttonAddListType);
            this.groupBoxListTypes.Controls.Add(this.userTextBoxNewListType);
            this.groupBoxListTypes.Controls.Add(this.listBoxListTypes);
            this.groupBoxListTypes.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBoxListTypes.Location = new System.Drawing.Point(9, 12);
            this.groupBoxListTypes.Name = "groupBoxListTypes";
            this.groupBoxListTypes.Size = new System.Drawing.Size(379, 117);
            this.groupBoxListTypes.TabIndex = 13;
            this.groupBoxListTypes.TabStop = false;
            this.groupBoxListTypes.Text = "List Types";
            // 
            // buttonDeleteListType
            // 
            this.buttonDeleteListType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteListType.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDeleteListType.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonDeleteListType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonDeleteListType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonDeleteListType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteListType.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteListType.ForeColor = System.Drawing.Color.Black;
            this.buttonDeleteListType.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteListType.Image")));
            this.buttonDeleteListType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteListType.Location = new System.Drawing.Point(291, 49);
            this.buttonDeleteListType.Name = "buttonDeleteListType";
            this.buttonDeleteListType.Size = new System.Drawing.Size(80, 23);
            this.buttonDeleteListType.TabIndex = 15;
            this.buttonDeleteListType.Text = "Delete";
            this.buttonDeleteListType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteListType.UseVisualStyleBackColor = false;
            this.buttonDeleteListType.Click += new System.EventHandler(this.buttonDeleteListType_Click);
            // 
            // buttonAddListType
            // 
            this.buttonAddListType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddListType.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAddListType.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAddListType.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAddListType.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAddListType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddListType.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddListType.ForeColor = System.Drawing.Color.Black;
            this.buttonAddListType.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddListType.Image")));
            this.buttonAddListType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddListType.Location = new System.Drawing.Point(290, 19);
            this.buttonAddListType.Name = "buttonAddListType";
            this.buttonAddListType.Size = new System.Drawing.Size(80, 23);
            this.buttonAddListType.TabIndex = 14;
            this.buttonAddListType.Text = "Add";
            this.buttonAddListType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddListType.UseVisualStyleBackColor = false;
            this.buttonAddListType.Click += new System.EventHandler(this.buttonAddListType_Click);
            // 
            // userTextBoxNewListType
            // 
            this.userTextBoxNewListType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.userTextBoxNewListType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxNewListType.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxNewListType.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxNewListType.Location = new System.Drawing.Point(16, 21);
            this.userTextBoxNewListType.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxNewListType.Name = "userTextBoxNewListType";
            this.userTextBoxNewListType.Size = new System.Drawing.Size(268, 18);
            this.userTextBoxNewListType.TabIndex = 13;
            // 
            // dataGridViewList
            // 
            this.dataGridViewList.AllowUserToAddRows = false;
            this.dataGridViewList.AllowUserToDeleteRows = false;
            this.dataGridViewList.AllowUserToOrderColumns = true;
            this.dataGridViewList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dataGridViewList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewList.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewList.Location = new System.Drawing.Point(16, 15);
            this.dataGridViewList.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewList.Name = "dataGridViewList";
            this.dataGridViewList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewList.Size = new System.Drawing.Size(271, 214);
            this.dataGridViewList.TabIndex = 9;
            // 
            // groupBoxLists
            // 
            this.groupBoxLists.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxLists.Controls.Add(this.buttonDeleteListItem);
            this.groupBoxLists.Controls.Add(this.buttonEditListItem);
            this.groupBoxLists.Controls.Add(this.buttonAddListItem);
            this.groupBoxLists.Controls.Add(this.dataGridViewList);
            this.groupBoxLists.Controls.Add(this.buttonExit);
            this.groupBoxLists.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBoxLists.Location = new System.Drawing.Point(9, 142);
            this.groupBoxLists.Name = "groupBoxLists";
            this.groupBoxLists.Size = new System.Drawing.Size(379, 237);
            this.groupBoxLists.TabIndex = 14;
            this.groupBoxLists.TabStop = false;
            this.groupBoxLists.Text = "Lists";
            // 
            // buttonDeleteListItem
            // 
            this.buttonDeleteListItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteListItem.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDeleteListItem.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonDeleteListItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonDeleteListItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonDeleteListItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteListItem.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteListItem.ForeColor = System.Drawing.Color.Black;
            this.buttonDeleteListItem.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteListItem.Image")));
            this.buttonDeleteListItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteListItem.Location = new System.Drawing.Point(290, 73);
            this.buttonDeleteListItem.Name = "buttonDeleteListItem";
            this.buttonDeleteListItem.Size = new System.Drawing.Size(80, 23);
            this.buttonDeleteListItem.TabIndex = 17;
            this.buttonDeleteListItem.Text = "Delete";
            this.buttonDeleteListItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteListItem.UseVisualStyleBackColor = false;
            this.buttonDeleteListItem.Click += new System.EventHandler(this.buttonDeleteListItem_Click);
            // 
            // buttonEditListItem
            // 
            this.buttonEditListItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEditListItem.BackColor = System.Drawing.Color.DarkGray;
            this.buttonEditListItem.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonEditListItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonEditListItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonEditListItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditListItem.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditListItem.ForeColor = System.Drawing.Color.Black;
            this.buttonEditListItem.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditListItem.Image")));
            this.buttonEditListItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditListItem.Location = new System.Drawing.Point(290, 44);
            this.buttonEditListItem.Name = "buttonEditListItem";
            this.buttonEditListItem.Size = new System.Drawing.Size(80, 23);
            this.buttonEditListItem.TabIndex = 16;
            this.buttonEditListItem.Text = "Edit";
            this.buttonEditListItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEditListItem.UseVisualStyleBackColor = false;
            this.buttonEditListItem.Click += new System.EventHandler(this.buttonEditListItem_Click);
            // 
            // buttonAddListItem
            // 
            this.buttonAddListItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddListItem.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAddListItem.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAddListItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAddListItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAddListItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddListItem.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddListItem.ForeColor = System.Drawing.Color.Black;
            this.buttonAddListItem.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddListItem.Image")));
            this.buttonAddListItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddListItem.Location = new System.Drawing.Point(290, 15);
            this.buttonAddListItem.Name = "buttonAddListItem";
            this.buttonAddListItem.Size = new System.Drawing.Size(80, 23);
            this.buttonAddListItem.TabIndex = 15;
            this.buttonAddListItem.Text = "Add";
            this.buttonAddListItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddListItem.UseVisualStyleBackColor = false;
            this.buttonAddListItem.Click += new System.EventHandler(this.buttonAddListItem_Click);
            // 
            // Lists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(398, 387);
            this.Controls.Add(this.groupBoxLists);
            this.Controls.Add(this.groupBoxListTypes);
            this.Name = "Lists";
            this.Text = "Lists";
            this.Load += new System.EventHandler(this.Lists_Load);
            this.Controls.SetChildIndex(this.groupBoxListTypes, 0);
            this.Controls.SetChildIndex(this.groupBoxLists, 0);
            this.groupBoxListTypes.ResumeLayout(false);
            this.groupBoxListTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewList)).EndInit();
            this.groupBoxLists.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.ListBox listBoxListTypes;
        private System.Windows.Forms.GroupBox groupBoxListTypes;
        private UserTextBox userTextBoxNewListType;
        private System.Windows.Forms.Button buttonAddListType;
        private System.Windows.Forms.Button buttonDeleteListType;
        private System.Windows.Forms.DataGridView dataGridViewList;
        private System.Windows.Forms.GroupBox groupBoxLists;
        private System.Windows.Forms.Button buttonDeleteListItem;
        private System.Windows.Forms.Button buttonEditListItem;
        private System.Windows.Forms.Button buttonAddListItem;
    }
}