namespace FDP
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.userTextBoxName = new FDP.UserTextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.userTextBoxPassword = new FDP.UserTextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelAttemptsLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBoxRememberName = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoLogin = new System.Windows.Forms.CheckBox();
            this.panelErrors.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 113);
            this.panelErrors.Size = new System.Drawing.Size(382, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(376, 39);
            // 
            // userTextBoxName
            // 
            this.userTextBoxName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxName.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxName.Location = new System.Drawing.Point(231, 8);
            this.userTextBoxName.Name = "userTextBoxName";
            this.userTextBoxName.Size = new System.Drawing.Size(128, 18);
            this.userTextBoxName.TabIndex = 7;
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.BackColor = System.Drawing.Color.Transparent;
            this.labelUserName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelUserName.ForeColor = System.Drawing.Color.White;
            this.labelUserName.Location = new System.Drawing.Point(87, 11);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(125, 11);
            this.labelUserName.TabIndex = 8;
            this.labelUserName.Text = "Name/User name:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.BackColor = System.Drawing.Color.Transparent;
            this.labelPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPassword.ForeColor = System.Drawing.Color.White;
            this.labelPassword.Location = new System.Drawing.Point(87, 35);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(77, 11);
            this.labelPassword.TabIndex = 9;
            this.labelPassword.Text = "Password:";
            // 
            // userTextBoxPassword
            // 
            this.userTextBoxPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxPassword.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxPassword.Location = new System.Drawing.Point(231, 32);
            this.userTextBoxPassword.Name = "userTextBoxPassword";
            this.userTextBoxPassword.PasswordChar = '*';
            this.userTextBoxPassword.Size = new System.Drawing.Size(128, 18);
            this.userTextBoxPassword.TabIndex = 10;
            this.userTextBoxPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userTextBoxPassword_KeyPress);
            // 
            // btnLogin
            // 
            this.btnLogin.AutoSize = true;
            this.btnLogin.BackColor = System.Drawing.Color.DarkGray;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.btnLogin.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnLogin.Image")));
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.Location = new System.Drawing.Point(193, 55);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Padding = new System.Windows.Forms.Padding(3);
            this.btnLogin.Size = new System.Drawing.Size(80, 28);
            this.btnLogin.TabIndex = 23;
            this.btnLogin.Text = "Login";
            this.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.AutoSize = true;
            this.buttonExit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.Black;
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(279, 56);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Padding = new System.Windows.Forms.Padding(3);
            this.buttonExit.Size = new System.Drawing.Size(80, 28);
            this.buttonExit.TabIndex = 24;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelAttemptsLeft});
            this.statusStrip1.Location = new System.Drawing.Point(5, 91);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(382, 22);
            this.statusStrip1.TabIndex = 25;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelAttemptsLeft
            // 
            this.toolStripStatusLabelAttemptsLeft.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabelAttemptsLeft.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripStatusLabelAttemptsLeft.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabelAttemptsLeft.Name = "toolStripStatusLabelAttemptsLeft";
            this.toolStripStatusLabelAttemptsLeft.Size = new System.Drawing.Size(117, 17);
            this.toolStripStatusLabelAttemptsLeft.Text = "Attempts left:";
            // 
            // checkBoxRememberName
            // 
            this.checkBoxRememberName.AutoSize = true;
            this.checkBoxRememberName.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxRememberName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBoxRememberName.ForeColor = System.Drawing.Color.White;
            this.checkBoxRememberName.Location = new System.Drawing.Point(89, 88);
            this.checkBoxRememberName.Name = "checkBoxRememberName";
            this.checkBoxRememberName.Size = new System.Drawing.Size(128, 15);
            this.checkBoxRememberName.TabIndex = 26;
            this.checkBoxRememberName.Text = "Remember name";
            this.checkBoxRememberName.UseVisualStyleBackColor = false;
            // 
            // checkBoxAutoLogin
            // 
            this.checkBoxAutoLogin.AutoSize = true;
            this.checkBoxAutoLogin.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxAutoLogin.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBoxAutoLogin.ForeColor = System.Drawing.Color.White;
            this.checkBoxAutoLogin.Location = new System.Drawing.Point(89, 109);
            this.checkBoxAutoLogin.Name = "checkBoxAutoLogin";
            this.checkBoxAutoLogin.Size = new System.Drawing.Size(248, 15);
            this.checkBoxAutoLogin.TabIndex = 27;
            this.checkBoxAutoLogin.Text = "Auto login (not recommended)";
            this.checkBoxAutoLogin.UseVisualStyleBackColor = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(81)))), ((int)(((byte)(80)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(392, 188);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxAutoLogin);
            this.Controls.Add(this.checkBoxRememberName);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.userTextBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.userTextBoxName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.ShowIcon = true;
            this.ShowInTaskbar = true;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.userTextBoxName, 0);
            this.Controls.SetChildIndex(this.labelUserName, 0);
            this.Controls.SetChildIndex(this.labelPassword, 0);
            this.Controls.SetChildIndex(this.userTextBoxPassword, 0);
            this.Controls.SetChildIndex(this.btnLogin, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.Controls.SetChildIndex(this.checkBoxRememberName, 0);
            this.Controls.SetChildIndex(this.checkBoxAutoLogin, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserTextBox userTextBoxName;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelPassword;
        private UserTextBox userTextBoxPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAttemptsLeft;
        private System.Windows.Forms.CheckBox checkBoxRememberName;
        private System.Windows.Forms.CheckBox checkBoxAutoLogin;
    }
}