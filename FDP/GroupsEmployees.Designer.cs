namespace FDP
{
    partial class GroupsEmployees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupsEmployees));
            this.groupBoxGroups = new System.Windows.Forms.GroupBox();
            this.listBoxGroups = new System.Windows.Forms.ListBox();
            this.groupBoxEmployees = new System.Windows.Forms.GroupBox();
            this.labelGroupEmployees = new System.Windows.Forms.Label();
            this.labelExistingEmployees = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSaveGroupRoles = new System.Windows.Forms.Button();
            this.checkedListBoxGroupEmployees = new FDP.cCheckedListBox();
            this.buttonEmployeesLeft = new System.Windows.Forms.Button();
            this.checkedListBoxExistingEmployees = new FDP.cCheckedListBox();
            this.buttonEmployeeRight = new System.Windows.Forms.Button();
            this.panelErrors.SuspendLayout();
            this.groupBoxGroups.SuspendLayout();
            this.groupBoxEmployees.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 281);
            this.panelErrors.Size = new System.Drawing.Size(628, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(622, 39);
            // 
            // groupBoxGroups
            // 
            this.groupBoxGroups.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxGroups.Controls.Add(this.listBoxGroups);
            this.groupBoxGroups.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBoxGroups.Location = new System.Drawing.Point(8, 8);
            this.groupBoxGroups.Name = "groupBoxGroups";
            this.groupBoxGroups.Size = new System.Drawing.Size(288, 122);
            this.groupBoxGroups.TabIndex = 24;
            this.groupBoxGroups.TabStop = false;
            this.groupBoxGroups.Text = "Groups";
            // 
            // listBoxGroups
            // 
            this.listBoxGroups.FormattingEnabled = true;
            this.listBoxGroups.ItemHeight = 11;
            this.listBoxGroups.Location = new System.Drawing.Point(7, 14);
            this.listBoxGroups.Name = "listBoxGroups";
            this.listBoxGroups.Size = new System.Drawing.Size(275, 103);
            this.listBoxGroups.TabIndex = 0;
            this.listBoxGroups.Click += new System.EventHandler(this.listBoxGroups_Click);
            this.listBoxGroups.SelectedIndexChanged += new System.EventHandler(this.listBoxGroups_SelectedIndexChanged);
            // 
            // groupBoxEmployees
            // 
            this.groupBoxEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxEmployees.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxEmployees.Controls.Add(this.labelGroupEmployees);
            this.groupBoxEmployees.Controls.Add(this.labelExistingEmployees);
            this.groupBoxEmployees.Controls.Add(this.buttonExit);
            this.groupBoxEmployees.Controls.Add(this.buttonSaveGroupRoles);
            this.groupBoxEmployees.Controls.Add(this.checkedListBoxGroupEmployees);
            this.groupBoxEmployees.Controls.Add(this.buttonEmployeesLeft);
            this.groupBoxEmployees.Controls.Add(this.checkedListBoxExistingEmployees);
            this.groupBoxEmployees.Controls.Add(this.buttonEmployeeRight);
            this.groupBoxEmployees.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBoxEmployees.Location = new System.Drawing.Point(8, 136);
            this.groupBoxEmployees.Name = "groupBoxEmployees";
            this.groupBoxEmployees.Size = new System.Drawing.Size(623, 214);
            this.groupBoxEmployees.TabIndex = 26;
            this.groupBoxEmployees.TabStop = false;
            this.groupBoxEmployees.Text = "Employees";
            // 
            // labelGroupEmployees
            // 
            this.labelGroupEmployees.AutoSize = true;
            this.labelGroupEmployees.Location = new System.Drawing.Point(332, 18);
            this.labelGroupEmployees.Name = "labelGroupEmployees";
            this.labelGroupEmployees.Size = new System.Drawing.Size(133, 11);
            this.labelGroupEmployees.TabIndex = 37;
            this.labelGroupEmployees.Text = "Group employees:";
            // 
            // labelExistingEmployees
            // 
            this.labelExistingEmployees.AutoSize = true;
            this.labelExistingEmployees.Location = new System.Drawing.Point(3, 18);
            this.labelExistingEmployees.Name = "labelExistingEmployees";
            this.labelExistingEmployees.Size = new System.Drawing.Size(157, 11);
            this.labelExistingEmployees.TabIndex = 36;
            this.labelExistingEmployees.Text = "Existing employees:";
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.buttonExit.Location = new System.Drawing.Point(89, 180);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 23);
            this.buttonExit.TabIndex = 35;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonSaveGroupRoles
            // 
            this.buttonSaveGroupRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveGroupRoles.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSaveGroupRoles.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSaveGroupRoles.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSaveGroupRoles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSaveGroupRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveGroupRoles.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveGroupRoles.ForeColor = System.Drawing.Color.Black;
            this.buttonSaveGroupRoles.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveGroupRoles.Image")));
            this.buttonSaveGroupRoles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveGroupRoles.Location = new System.Drawing.Point(3, 180);
            this.buttonSaveGroupRoles.Name = "buttonSaveGroupRoles";
            this.buttonSaveGroupRoles.Size = new System.Drawing.Size(80, 23);
            this.buttonSaveGroupRoles.TabIndex = 34;
            this.buttonSaveGroupRoles.Text = "Save";
            this.buttonSaveGroupRoles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveGroupRoles.UseVisualStyleBackColor = false;
            this.buttonSaveGroupRoles.Click += new System.EventHandler(this.buttonSaveGroupRoles_Click);
            // 
            // checkedListBoxGroupEmployees
            // 
            this.checkedListBoxGroupEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxGroupEmployees.CheckOnClick = true;
            this.checkedListBoxGroupEmployees.DataSource = null;
            this.checkedListBoxGroupEmployees.FormattingEnabled = true;
            this.checkedListBoxGroupEmployees.Location = new System.Drawing.Point(334, 40);
            this.checkedListBoxGroupEmployees.Name = "checkedListBoxGroupEmployees";
            this.checkedListBoxGroupEmployees.Size = new System.Drawing.Size(283, 134);
            this.checkedListBoxGroupEmployees.Sorted = true;
            this.checkedListBoxGroupEmployees.TabIndex = 30;
            // 
            // buttonEmployeesLeft
            // 
            this.buttonEmployeesLeft.BackColor = System.Drawing.Color.DarkGray;
            this.buttonEmployeesLeft.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonEmployeesLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonEmployeesLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonEmployeesLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEmployeesLeft.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEmployeesLeft.ForeColor = System.Drawing.Color.Black;
            this.buttonEmployeesLeft.Image = ((System.Drawing.Image)(resources.GetObject("buttonEmployeesLeft.Image")));
            this.buttonEmployeesLeft.Location = new System.Drawing.Point(288, 104);
            this.buttonEmployeesLeft.Name = "buttonEmployeesLeft";
            this.buttonEmployeesLeft.Size = new System.Drawing.Size(40, 23);
            this.buttonEmployeesLeft.TabIndex = 29;
            this.buttonEmployeesLeft.UseVisualStyleBackColor = false;
            this.buttonEmployeesLeft.Click += new System.EventHandler(this.buttonEmployeesLeft_Click);
            // 
            // checkedListBoxExistingEmployees
            // 
            this.checkedListBoxExistingEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkedListBoxExistingEmployees.CheckOnClick = true;
            this.checkedListBoxExistingEmployees.DataSource = null;
            this.checkedListBoxExistingEmployees.FormattingEnabled = true;
            this.checkedListBoxExistingEmployees.Location = new System.Drawing.Point(3, 40);
            this.checkedListBoxExistingEmployees.Name = "checkedListBoxExistingEmployees";
            this.checkedListBoxExistingEmployees.Size = new System.Drawing.Size(279, 134);
            this.checkedListBoxExistingEmployees.Sorted = true;
            this.checkedListBoxExistingEmployees.TabIndex = 8;
            // 
            // buttonEmployeeRight
            // 
            this.buttonEmployeeRight.BackColor = System.Drawing.Color.DarkGray;
            this.buttonEmployeeRight.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonEmployeeRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonEmployeeRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonEmployeeRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEmployeeRight.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEmployeeRight.ForeColor = System.Drawing.Color.Black;
            this.buttonEmployeeRight.Image = ((System.Drawing.Image)(resources.GetObject("buttonEmployeeRight.Image")));
            this.buttonEmployeeRight.Location = new System.Drawing.Point(288, 75);
            this.buttonEmployeeRight.Name = "buttonEmployeeRight";
            this.buttonEmployeeRight.Size = new System.Drawing.Size(40, 23);
            this.buttonEmployeeRight.TabIndex = 28;
            this.buttonEmployeeRight.UseVisualStyleBackColor = false;
            this.buttonEmployeeRight.Click += new System.EventHandler(this.buttonEmployeeRight_Click);
            // 
            // GroupsEmployees
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(638, 356);
            this.Controls.Add(this.groupBoxEmployees);
            this.Controls.Add(this.groupBoxGroups);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "GroupsEmployees";
            this.Text = "GroupsEmployees";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GroupsEmployees_FormClosing);
            this.Load += new System.EventHandler(this.GroupsEmployees_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.groupBoxGroups, 0);
            this.Controls.SetChildIndex(this.groupBoxEmployees, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            this.groupBoxGroups.ResumeLayout(false);
            this.groupBoxEmployees.ResumeLayout(false);
            this.groupBoxEmployees.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxGroups;
        private System.Windows.Forms.ListBox listBoxGroups;
        private System.Windows.Forms.GroupBox groupBoxEmployees;
        private cCheckedListBox checkedListBoxGroupEmployees;
        private System.Windows.Forms.Button buttonEmployeesLeft;
        private cCheckedListBox checkedListBoxExistingEmployees;
        private System.Windows.Forms.Button buttonEmployeeRight;
        private System.Windows.Forms.Button buttonSaveGroupRoles;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelGroupEmployees;
        private System.Windows.Forms.Label labelExistingEmployees;
    }
}