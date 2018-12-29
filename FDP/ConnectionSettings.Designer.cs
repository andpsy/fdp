using System;

namespace FDP
{
    partial class ConnectionSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionSettings));
            this.labelServer = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.userTextBoxServer = new FDP.UserTextBox();
            this.userTextBoxPort = new FDP.UserTextBox();
            this.userTextBoxDatabase = new FDP.UserTextBox();
            this.userTextBoxPassowrd = new FDP.UserTextBox();
            this.userTextBoxUser = new FDP.UserTextBox();
            this.labelUser = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.buttonSaveConnectionString = new System.Windows.Forms.Button();
            this.panelErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 124);
            this.panelErrors.Size = new System.Drawing.Size(253, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(247, 39);
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelServer.Location = new System.Drawing.Point(12, 18);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(61, 11);
            this.labelServer.TabIndex = 7;
            this.labelServer.Text = "Server:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPort.Location = new System.Drawing.Point(12, 47);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(45, 11);
            this.labelPort.TabIndex = 8;
            this.labelPort.Text = "Port:";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDatabase.Location = new System.Drawing.Point(12, 76);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(77, 11);
            this.labelDatabase.TabIndex = 9;
            this.labelDatabase.Text = "Database:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPassword.Location = new System.Drawing.Point(12, 135);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(77, 11);
            this.labelPassword.TabIndex = 10;
            this.labelPassword.Text = "Password:";
            // 
            // userTextBoxServer
            // 
            this.userTextBoxServer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxServer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxServer.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxServer.Location = new System.Drawing.Point(114, 14);
            this.userTextBoxServer.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxServer.Name = "userTextBoxServer";
            this.userTextBoxServer.Size = new System.Drawing.Size(135, 18);
            this.userTextBoxServer.TabIndex = 11;
            // 
            // userTextBoxPort
            // 
            this.userTextBoxPort.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxPort.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxPort.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxPort.Location = new System.Drawing.Point(114, 43);
            this.userTextBoxPort.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxPort.Name = "userTextBoxPort";
            this.userTextBoxPort.Size = new System.Drawing.Size(135, 18);
            this.userTextBoxPort.TabIndex = 12;
            // 
            // userTextBoxDatabase
            // 
            this.userTextBoxDatabase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxDatabase.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxDatabase.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxDatabase.Location = new System.Drawing.Point(114, 72);
            this.userTextBoxDatabase.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxDatabase.Name = "userTextBoxDatabase";
            this.userTextBoxDatabase.Size = new System.Drawing.Size(135, 18);
            this.userTextBoxDatabase.TabIndex = 13;
            // 
            // userTextBoxPassowrd
            // 
            this.userTextBoxPassowrd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxPassowrd.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxPassowrd.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxPassowrd.Location = new System.Drawing.Point(114, 131);
            this.userTextBoxPassowrd.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxPassowrd.Name = "userTextBoxPassowrd";
            this.userTextBoxPassowrd.Size = new System.Drawing.Size(135, 18);
            this.userTextBoxPassowrd.TabIndex = 14;
            // 
            // userTextBoxUser
            // 
            this.userTextBoxUser.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxUser.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxUser.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxUser.Location = new System.Drawing.Point(114, 101);
            this.userTextBoxUser.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxUser.Name = "userTextBoxUser";
            this.userTextBoxUser.Size = new System.Drawing.Size(135, 18);
            this.userTextBoxUser.TabIndex = 19;
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelUser.Location = new System.Drawing.Point(12, 105);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(45, 11);
            this.labelUser.TabIndex = 18;
            this.labelUser.Text = "User:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(176, 164);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 20;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonTestConnection
            // 
            this.buttonTestConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTestConnection.BackColor = System.Drawing.Color.DarkGray;
            this.buttonTestConnection.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonTestConnection.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonTestConnection.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTestConnection.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonTestConnection.Image = ((System.Drawing.Image)(resources.GetObject("buttonTestConnection.Image")));
            this.buttonTestConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTestConnection.Location = new System.Drawing.Point(94, 164);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(75, 23);
            this.buttonTestConnection.TabIndex = 21;
            this.buttonTestConnection.Text = "Test";
            this.buttonTestConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonTestConnection.UseVisualStyleBackColor = false;
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // buttonSaveConnectionString
            // 
            this.buttonSaveConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveConnectionString.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSaveConnectionString.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSaveConnectionString.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSaveConnectionString.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSaveConnectionString.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveConnectionString.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSaveConnectionString.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveConnectionString.Image")));
            this.buttonSaveConnectionString.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveConnectionString.Location = new System.Drawing.Point(12, 164);
            this.buttonSaveConnectionString.Name = "buttonSaveConnectionString";
            this.buttonSaveConnectionString.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveConnectionString.TabIndex = 22;
            this.buttonSaveConnectionString.Text = "Save";
            this.buttonSaveConnectionString.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveConnectionString.UseVisualStyleBackColor = false;
            this.buttonSaveConnectionString.Click += new System.EventHandler(this.buttonSaveConnectionString_Click);
            // 
            // ConnectionSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(263, 199);
            this.Controls.Add(this.buttonSaveConnectionString);
            this.Controls.Add(this.buttonTestConnection);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.userTextBoxUser);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.userTextBoxPassowrd);
            this.Controls.Add(this.userTextBoxDatabase);
            this.Controls.Add(this.userTextBoxPort);
            this.Controls.Add(this.userTextBoxServer);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelDatabase);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelServer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "ConnectionSettings";
            this.Text = "ConnectionSettings";
            this.Load += new System.EventHandler(this.ConnectionSettings_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.labelServer, 0);
            this.Controls.SetChildIndex(this.labelPort, 0);
            this.Controls.SetChildIndex(this.labelDatabase, 0);
            this.Controls.SetChildIndex(this.labelPassword, 0);
            this.Controls.SetChildIndex(this.userTextBoxServer, 0);
            this.Controls.SetChildIndex(this.userTextBoxPort, 0);
            this.Controls.SetChildIndex(this.userTextBoxDatabase, 0);
            this.Controls.SetChildIndex(this.userTextBoxPassowrd, 0);
            this.Controls.SetChildIndex(this.labelUser, 0);
            this.Controls.SetChildIndex(this.userTextBoxUser, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.buttonTestConnection, 0);
            this.Controls.SetChildIndex(this.buttonSaveConnectionString, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.Label labelPassword;
        private UserTextBox userTextBoxServer;
        private UserTextBox userTextBoxPort;
        private UserTextBox userTextBoxDatabase;
        private UserTextBox userTextBoxPassowrd;
        private UserTextBox userTextBoxUser;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonTestConnection;
        private System.Windows.Forms.Button buttonSaveConnectionString;
    }
}