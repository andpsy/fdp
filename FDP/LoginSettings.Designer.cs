namespace FDP
{
    partial class LoginSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginSettings));
            this.btnSave = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupBoxPasswords = new System.Windows.Forms.GroupBox();
            this.userTextBoxNewUserName = new FDP.UserTextBox();
            this.labelNewUserName = new System.Windows.Forms.Label();
            this.userTextBoxOldUserName = new FDP.UserTextBox();
            this.labelOldUsername = new System.Windows.Forms.Label();
            this.checkBoxAutoLogin = new System.Windows.Forms.CheckBox();
            this.checkBoxRememberName = new System.Windows.Forms.CheckBox();
            this.userTextBoxConfirmNewPassword = new FDP.UserTextBox();
            this.labelConfirmNewPassword = new System.Windows.Forms.Label();
            this.userTextBoxNewPassword = new FDP.UserTextBox();
            this.labelNewPassword = new System.Windows.Forms.Label();
            this.userTextBoxOldPassword = new FDP.UserTextBox();
            this.labelOldPassword = new System.Windows.Forms.Label();
            this.panelErrors.SuspendLayout();
            this.groupBoxPasswords.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 183);
            this.panelErrors.Size = new System.Drawing.Size(406, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(400, 39);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.AutoSize = true;
            this.btnSave.BackColor = System.Drawing.Color.DarkGray;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.btnSave.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(4, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(3);
            this.btnSave.Size = new System.Drawing.Size(80, 28);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.AutoSize = true;
            this.buttonExit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.Black;
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(315, 210);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Padding = new System.Windows.Forms.Padding(3);
            this.buttonExit.Size = new System.Drawing.Size(80, 28);
            this.buttonExit.TabIndex = 9;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // groupBoxPasswords
            // 
            this.groupBoxPasswords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPasswords.Controls.Add(this.userTextBoxNewUserName);
            this.groupBoxPasswords.Controls.Add(this.labelNewUserName);
            this.groupBoxPasswords.Controls.Add(this.userTextBoxOldUserName);
            this.groupBoxPasswords.Controls.Add(this.labelOldUsername);
            this.groupBoxPasswords.Controls.Add(this.checkBoxAutoLogin);
            this.groupBoxPasswords.Controls.Add(this.checkBoxRememberName);
            this.groupBoxPasswords.Controls.Add(this.userTextBoxConfirmNewPassword);
            this.groupBoxPasswords.Controls.Add(this.labelConfirmNewPassword);
            this.groupBoxPasswords.Controls.Add(this.userTextBoxNewPassword);
            this.groupBoxPasswords.Controls.Add(this.labelNewPassword);
            this.groupBoxPasswords.Controls.Add(this.userTextBoxOldPassword);
            this.groupBoxPasswords.Controls.Add(this.labelOldPassword);
            this.groupBoxPasswords.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBoxPasswords.Location = new System.Drawing.Point(5, 3);
            this.groupBoxPasswords.Name = "groupBoxPasswords";
            this.groupBoxPasswords.Size = new System.Drawing.Size(390, 200);
            this.groupBoxPasswords.TabIndex = 25;
            this.groupBoxPasswords.TabStop = false;
            this.groupBoxPasswords.Text = "Login information";
            // 
            // userTextBoxNewUserName
            // 
            this.userTextBoxNewUserName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxNewUserName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxNewUserName.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxNewUserName.Location = new System.Drawing.Point(190, 75);
            this.userTextBoxNewUserName.Name = "userTextBoxNewUserName";
            this.userTextBoxNewUserName.Size = new System.Drawing.Size(174, 18);
            this.userTextBoxNewUserName.TabIndex = 3;
            this.userTextBoxNewUserName.Visible = false;
            // 
            // labelNewUserName
            // 
            this.labelNewUserName.AutoSize = true;
            this.labelNewUserName.BackColor = System.Drawing.Color.Transparent;
            this.labelNewUserName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNewUserName.ForeColor = System.Drawing.Color.Black;
            this.labelNewUserName.Location = new System.Drawing.Point(11, 78);
            this.labelNewUserName.Name = "labelNewUserName";
            this.labelNewUserName.Size = new System.Drawing.Size(117, 11);
            this.labelNewUserName.TabIndex = 39;
            this.labelNewUserName.Text = "New user name:";
            this.labelNewUserName.Visible = false;
            // 
            // userTextBoxOldUserName
            // 
            this.userTextBoxOldUserName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxOldUserName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxOldUserName.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxOldUserName.Location = new System.Drawing.Point(190, 25);
            this.userTextBoxOldUserName.Name = "userTextBoxOldUserName";
            this.userTextBoxOldUserName.Size = new System.Drawing.Size(174, 18);
            this.userTextBoxOldUserName.TabIndex = 1;
            // 
            // labelOldUsername
            // 
            this.labelOldUsername.AutoSize = true;
            this.labelOldUsername.BackColor = System.Drawing.Color.Transparent;
            this.labelOldUsername.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOldUsername.ForeColor = System.Drawing.Color.Black;
            this.labelOldUsername.Location = new System.Drawing.Point(11, 28);
            this.labelOldUsername.Name = "labelOldUsername";
            this.labelOldUsername.Size = new System.Drawing.Size(117, 11);
            this.labelOldUsername.TabIndex = 37;
            this.labelOldUsername.Text = "Old user name:";
            // 
            // checkBoxAutoLogin
            // 
            this.checkBoxAutoLogin.AutoSize = true;
            this.checkBoxAutoLogin.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAutoLogin.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBoxAutoLogin.ForeColor = System.Drawing.Color.Black;
            this.checkBoxAutoLogin.Location = new System.Drawing.Point(13, 175);
            this.checkBoxAutoLogin.Name = "checkBoxAutoLogin";
            this.checkBoxAutoLogin.Size = new System.Drawing.Size(248, 15);
            this.checkBoxAutoLogin.TabIndex = 7;
            this.checkBoxAutoLogin.Text = "Auto login (not recommended)";
            this.checkBoxAutoLogin.UseVisualStyleBackColor = false;
            // 
            // checkBoxRememberName
            // 
            this.checkBoxRememberName.AutoSize = true;
            this.checkBoxRememberName.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxRememberName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBoxRememberName.ForeColor = System.Drawing.Color.Black;
            this.checkBoxRememberName.Location = new System.Drawing.Point(13, 154);
            this.checkBoxRememberName.Name = "checkBoxRememberName";
            this.checkBoxRememberName.Size = new System.Drawing.Size(168, 15);
            this.checkBoxRememberName.TabIndex = 6;
            this.checkBoxRememberName.Text = "Remember user name";
            this.checkBoxRememberName.UseVisualStyleBackColor = false;
            // 
            // userTextBoxConfirmNewPassword
            // 
            this.userTextBoxConfirmNewPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxConfirmNewPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxConfirmNewPassword.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxConfirmNewPassword.Location = new System.Drawing.Point(190, 126);
            this.userTextBoxConfirmNewPassword.Name = "userTextBoxConfirmNewPassword";
            this.userTextBoxConfirmNewPassword.PasswordChar = '*';
            this.userTextBoxConfirmNewPassword.Size = new System.Drawing.Size(174, 18);
            this.userTextBoxConfirmNewPassword.TabIndex = 5;
            // 
            // labelConfirmNewPassword
            // 
            this.labelConfirmNewPassword.AutoSize = true;
            this.labelConfirmNewPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelConfirmNewPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelConfirmNewPassword.ForeColor = System.Drawing.Color.Black;
            this.labelConfirmNewPassword.Location = new System.Drawing.Point(11, 129);
            this.labelConfirmNewPassword.Name = "labelConfirmNewPassword";
            this.labelConfirmNewPassword.Size = new System.Drawing.Size(173, 11);
            this.labelConfirmNewPassword.TabIndex = 15;
            this.labelConfirmNewPassword.Text = "Confirm new password:";
            // 
            // userTextBoxNewPassword
            // 
            this.userTextBoxNewPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxNewPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxNewPassword.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxNewPassword.Location = new System.Drawing.Point(190, 101);
            this.userTextBoxNewPassword.Name = "userTextBoxNewPassword";
            this.userTextBoxNewPassword.PasswordChar = '*';
            this.userTextBoxNewPassword.Size = new System.Drawing.Size(174, 18);
            this.userTextBoxNewPassword.TabIndex = 4;
            // 
            // labelNewPassword
            // 
            this.labelNewPassword.AutoSize = true;
            this.labelNewPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelNewPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNewPassword.ForeColor = System.Drawing.Color.Black;
            this.labelNewPassword.Location = new System.Drawing.Point(11, 104);
            this.labelNewPassword.Name = "labelNewPassword";
            this.labelNewPassword.Size = new System.Drawing.Size(109, 11);
            this.labelNewPassword.TabIndex = 13;
            this.labelNewPassword.Text = "New password:";
            // 
            // userTextBoxOldPassword
            // 
            this.userTextBoxOldPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxOldPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxOldPassword.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxOldPassword.Location = new System.Drawing.Point(190, 50);
            this.userTextBoxOldPassword.Name = "userTextBoxOldPassword";
            this.userTextBoxOldPassword.PasswordChar = '*';
            this.userTextBoxOldPassword.Size = new System.Drawing.Size(174, 18);
            this.userTextBoxOldPassword.TabIndex = 2;
            // 
            // labelOldPassword
            // 
            this.labelOldPassword.AutoSize = true;
            this.labelOldPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelOldPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOldPassword.ForeColor = System.Drawing.Color.Black;
            this.labelOldPassword.Location = new System.Drawing.Point(11, 53);
            this.labelOldPassword.Name = "labelOldPassword";
            this.labelOldPassword.Size = new System.Drawing.Size(109, 11);
            this.labelOldPassword.TabIndex = 11;
            this.labelOldPassword.Text = "Old password:";
            // 
            // LoginSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(416, 258);
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxPasswords);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoginSettings";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.groupBoxPasswords, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            this.groupBoxPasswords.ResumeLayout(false);
            this.groupBoxPasswords.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.GroupBox groupBoxPasswords;
        private UserTextBox userTextBoxNewUserName;
        private System.Windows.Forms.Label labelNewUserName;
        private UserTextBox userTextBoxOldUserName;
        private System.Windows.Forms.Label labelOldUsername;
        private System.Windows.Forms.CheckBox checkBoxAutoLogin;
        private System.Windows.Forms.CheckBox checkBoxRememberName;
        private UserTextBox userTextBoxConfirmNewPassword;
        private System.Windows.Forms.Label labelConfirmNewPassword;
        private UserTextBox userTextBoxNewPassword;
        private System.Windows.Forms.Label labelNewPassword;
        private UserTextBox userTextBoxOldPassword;
        private System.Windows.Forms.Label labelOldPassword;
    }
}