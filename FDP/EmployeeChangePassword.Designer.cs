namespace FDP
{
    partial class EmployeeChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeChangePassword));
            this.userTextBoxPassword = new FDP.UserTextBox();
            this.userTextBoxName = new FDP.UserTextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelEmployeeName = new System.Windows.Forms.Label();
            this.buttonSavePassword = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.labelWarningOverwritePassword = new System.Windows.Forms.Label();
            this.panelErrors.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 63);
            this.panelErrors.Size = new System.Drawing.Size(384, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(378, 39);
            // 
            // userTextBoxPassword
            // 
            this.userTextBoxPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxPassword.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxPassword.Location = new System.Drawing.Point(180, 38);
            this.userTextBoxPassword.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxPassword.Name = "userTextBoxPassword";
            this.userTextBoxPassword.PasswordChar = '*';
            this.userTextBoxPassword.Size = new System.Drawing.Size(196, 18);
            this.userTextBoxPassword.TabIndex = 16;
            // 
            // userTextBoxName
            // 
            this.userTextBoxName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxName.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxName.Location = new System.Drawing.Point(180, 9);
            this.userTextBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxName.Name = "userTextBoxName";
            this.userTextBoxName.ReadOnly = true;
            this.userTextBoxName.Size = new System.Drawing.Size(196, 18);
            this.userTextBoxName.TabIndex = 15;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPassword.Location = new System.Drawing.Point(11, 41);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(109, 11);
            this.labelPassword.TabIndex = 14;
            this.labelPassword.Text = "New Password:";
            // 
            // labelEmployeeName
            // 
            this.labelEmployeeName.AutoSize = true;
            this.labelEmployeeName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEmployeeName.Location = new System.Drawing.Point(11, 12);
            this.labelEmployeeName.Name = "labelEmployeeName";
            this.labelEmployeeName.Size = new System.Drawing.Size(45, 11);
            this.labelEmployeeName.TabIndex = 13;
            this.labelEmployeeName.Text = "Name:";
            // 
            // buttonSavePassword
            // 
            this.buttonSavePassword.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSavePassword.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSavePassword.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSavePassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSavePassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSavePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSavePassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSavePassword.Image = ((System.Drawing.Image)(resources.GetObject("buttonSavePassword.Image")));
            this.buttonSavePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSavePassword.Location = new System.Drawing.Point(13, 69);
            this.buttonSavePassword.Name = "buttonSavePassword";
            this.buttonSavePassword.Size = new System.Drawing.Size(75, 23);
            this.buttonSavePassword.TabIndex = 24;
            this.buttonSavePassword.Text = "Save";
            this.buttonSavePassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSavePassword.UseVisualStyleBackColor = false;
            this.buttonSavePassword.Click += new System.EventHandler(this.buttonSavePassword_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(94, 69);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 23;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // labelWarningOverwritePassword
            // 
            this.labelWarningOverwritePassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWarningOverwritePassword.AutoSize = true;
            this.labelWarningOverwritePassword.BackColor = System.Drawing.Color.Transparent;
            this.labelWarningOverwritePassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelWarningOverwritePassword.ForeColor = System.Drawing.Color.Red;
            this.labelWarningOverwritePassword.Location = new System.Drawing.Point(11, 111);
            this.labelWarningOverwritePassword.Name = "labelWarningOverwritePassword";
            this.labelWarningOverwritePassword.Size = new System.Drawing.Size(365, 11);
            this.labelWarningOverwritePassword.TabIndex = 26;
            this.labelWarningOverwritePassword.Text = "Warning! The old password will be overwritten";
            // 
            // EmployeeChangePassword
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(394, 138);
            this.Controls.Add(this.buttonSavePassword);
            this.Controls.Add(this.labelWarningOverwritePassword);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.userTextBoxPassword);
            this.Controls.Add(this.userTextBoxName);
            this.Controls.Add(this.labelEmployeeName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "EmployeeChangePassword";
            this.Text = "EmployeeChangePassword";
            this.Load += new System.EventHandler(this.EmployeeChangePassword_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.labelEmployeeName, 0);
            this.Controls.SetChildIndex(this.userTextBoxName, 0);
            this.Controls.SetChildIndex(this.userTextBoxPassword, 0);
            this.Controls.SetChildIndex(this.labelPassword, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.labelWarningOverwritePassword, 0);
            this.Controls.SetChildIndex(this.buttonSavePassword, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserTextBox userTextBoxPassword;
        private UserTextBox userTextBoxName;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelEmployeeName;
        private System.Windows.Forms.Button buttonSavePassword;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelWarningOverwritePassword;
    }
}