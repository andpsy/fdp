namespace FDP_Admin
{
    partial class main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageConnectionSettings = new System.Windows.Forms.TabPage();
            this.userTextBoxUser = new FDP.UserTextBox();
            this.userTextBoxPassowrd = new FDP.UserTextBox();
            this.userTextBoxDatabase = new FDP.UserTextBox();
            this.userTextBoxPort = new FDP.UserTextBox();
            this.userTextBoxServer = new FDP.UserTextBox();
            this.buttonSaveConnectionString = new System.Windows.Forms.Button();
            this.labelUser = new System.Windows.Forms.Label();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelServer = new System.Windows.Forms.Label();
            this.tabPageMasterPassword = new System.Windows.Forms.TabPage();
            this.buttonSaveMasterPassword = new System.Windows.Forms.Button();
            this.userTextBoxConfirmNewPassword = new FDP.UserTextBox();
            this.labelConfirmNewPassword = new System.Windows.Forms.Label();
            this.userTextBoxNewPassword = new FDP.UserTextBox();
            this.labelNewPassword = new System.Windows.Forms.Label();
            this.userTextBoxOldPassword = new FDP.UserTextBox();
            this.labelOldPassword = new System.Windows.Forms.Label();
            this.tabPageBackupDB = new System.Windows.Forms.TabPage();
            this.pictureBoxSelectFile = new System.Windows.Forms.PictureBox();
            this.buttonBackupDB = new System.Windows.Forms.Button();
            this.labelBackupFile = new System.Windows.Forms.Label();
            this.userTextBoxBackupFile = new FDP.UserTextBox();
            this.tabPageRestoreDB = new System.Windows.Forms.TabPage();
            this.pictureBoxSelectRestoreFile = new System.Windows.Forms.PictureBox();
            this.buttonRestoreDB = new System.Windows.Forms.Button();
            this.labelSelectRestoreFile = new System.Windows.Forms.Label();
            this.userTextBoxRestoreFile = new FDP.UserTextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPageConnectionSettings.SuspendLayout();
            this.tabPageMasterPassword.SuspendLayout();
            this.tabPageBackupDB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectFile)).BeginInit();
            this.tabPageRestoreDB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectRestoreFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPageConnectionSettings);
            this.tabControl1.Controls.Add(this.tabPageMasterPassword);
            this.tabControl1.Controls.Add(this.tabPageBackupDB);
            this.tabControl1.Controls.Add(this.tabPageRestoreDB);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(25, 150);
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(451, 396);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageConnectionSettings
            // 
            this.tabPageConnectionSettings.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageConnectionSettings.Controls.Add(this.userTextBoxUser);
            this.tabPageConnectionSettings.Controls.Add(this.userTextBoxPassowrd);
            this.tabPageConnectionSettings.Controls.Add(this.userTextBoxDatabase);
            this.tabPageConnectionSettings.Controls.Add(this.userTextBoxPort);
            this.tabPageConnectionSettings.Controls.Add(this.userTextBoxServer);
            this.tabPageConnectionSettings.Controls.Add(this.buttonSaveConnectionString);
            this.tabPageConnectionSettings.Controls.Add(this.labelUser);
            this.tabPageConnectionSettings.Controls.Add(this.buttonTestConnection);
            this.tabPageConnectionSettings.Controls.Add(this.labelPassword);
            this.tabPageConnectionSettings.Controls.Add(this.labelDatabase);
            this.tabPageConnectionSettings.Controls.Add(this.labelPort);
            this.tabPageConnectionSettings.Controls.Add(this.labelServer);
            this.tabPageConnectionSettings.Location = new System.Drawing.Point(154, 4);
            this.tabPageConnectionSettings.Name = "tabPageConnectionSettings";
            this.tabPageConnectionSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConnectionSettings.Size = new System.Drawing.Size(293, 388);
            this.tabPageConnectionSettings.TabIndex = 0;
            this.tabPageConnectionSettings.Text = "Connection settings";
            // 
            // userTextBoxUser
            // 
            this.userTextBoxUser.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxUser.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxUser.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxUser.Location = new System.Drawing.Point(114, 199);
            this.userTextBoxUser.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxUser.Name = "userTextBoxUser";
            this.userTextBoxUser.Size = new System.Drawing.Size(135, 18);
            this.userTextBoxUser.TabIndex = 34;
            // 
            // userTextBoxPassowrd
            // 
            this.userTextBoxPassowrd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxPassowrd.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxPassowrd.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxPassowrd.Location = new System.Drawing.Point(114, 229);
            this.userTextBoxPassowrd.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxPassowrd.Name = "userTextBoxPassowrd";
            this.userTextBoxPassowrd.Size = new System.Drawing.Size(135, 18);
            this.userTextBoxPassowrd.TabIndex = 35;
            // 
            // userTextBoxDatabase
            // 
            this.userTextBoxDatabase.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxDatabase.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxDatabase.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxDatabase.Location = new System.Drawing.Point(114, 170);
            this.userTextBoxDatabase.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxDatabase.Name = "userTextBoxDatabase";
            this.userTextBoxDatabase.Size = new System.Drawing.Size(135, 18);
            this.userTextBoxDatabase.TabIndex = 33;
            // 
            // userTextBoxPort
            // 
            this.userTextBoxPort.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxPort.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxPort.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxPort.Location = new System.Drawing.Point(114, 141);
            this.userTextBoxPort.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxPort.Name = "userTextBoxPort";
            this.userTextBoxPort.Size = new System.Drawing.Size(135, 18);
            this.userTextBoxPort.TabIndex = 32;
            // 
            // userTextBoxServer
            // 
            this.userTextBoxServer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxServer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxServer.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxServer.Location = new System.Drawing.Point(114, 112);
            this.userTextBoxServer.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxServer.Name = "userTextBoxServer";
            this.userTextBoxServer.Size = new System.Drawing.Size(135, 18);
            this.userTextBoxServer.TabIndex = 31;
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
            this.buttonSaveConnectionString.Location = new System.Drawing.Point(115, 252);
            this.buttonSaveConnectionString.Name = "buttonSaveConnectionString";
            this.buttonSaveConnectionString.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveConnectionString.TabIndex = 37;
            this.buttonSaveConnectionString.Text = "Save";
            this.buttonSaveConnectionString.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveConnectionString.UseVisualStyleBackColor = false;
            this.buttonSaveConnectionString.Click += new System.EventHandler(this.buttonSaveConnectionString_Click);
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelUser.Location = new System.Drawing.Point(32, 202);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(45, 11);
            this.labelUser.TabIndex = 27;
            this.labelUser.Text = "User:";
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
            this.buttonTestConnection.Location = new System.Drawing.Point(34, 252);
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.Size = new System.Drawing.Size(75, 23);
            this.buttonTestConnection.TabIndex = 36;
            this.buttonTestConnection.Text = "Test";
            this.buttonTestConnection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonTestConnection.UseVisualStyleBackColor = false;
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPassword.Location = new System.Drawing.Point(32, 232);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(77, 11);
            this.labelPassword.TabIndex = 26;
            this.labelPassword.Text = "Password:";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDatabase.Location = new System.Drawing.Point(32, 173);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(77, 11);
            this.labelDatabase.TabIndex = 25;
            this.labelDatabase.Text = "Database:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPort.Location = new System.Drawing.Point(32, 144);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(45, 11);
            this.labelPort.TabIndex = 24;
            this.labelPort.Text = "Port:";
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelServer.Location = new System.Drawing.Point(32, 115);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(61, 11);
            this.labelServer.TabIndex = 23;
            this.labelServer.Text = "Server:";
            // 
            // tabPageMasterPassword
            // 
            this.tabPageMasterPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageMasterPassword.Controls.Add(this.buttonSaveMasterPassword);
            this.tabPageMasterPassword.Controls.Add(this.userTextBoxConfirmNewPassword);
            this.tabPageMasterPassword.Controls.Add(this.labelConfirmNewPassword);
            this.tabPageMasterPassword.Controls.Add(this.userTextBoxNewPassword);
            this.tabPageMasterPassword.Controls.Add(this.labelNewPassword);
            this.tabPageMasterPassword.Controls.Add(this.userTextBoxOldPassword);
            this.tabPageMasterPassword.Controls.Add(this.labelOldPassword);
            this.tabPageMasterPassword.Location = new System.Drawing.Point(154, 4);
            this.tabPageMasterPassword.Name = "tabPageMasterPassword";
            this.tabPageMasterPassword.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMasterPassword.Size = new System.Drawing.Size(303, 398);
            this.tabPageMasterPassword.TabIndex = 1;
            this.tabPageMasterPassword.Text = "Master password";
            // 
            // buttonSaveMasterPassword
            // 
            this.buttonSaveMasterPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveMasterPassword.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSaveMasterPassword.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSaveMasterPassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSaveMasterPassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSaveMasterPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveMasterPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSaveMasterPassword.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveMasterPassword.Image")));
            this.buttonSaveMasterPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveMasterPassword.Location = new System.Drawing.Point(156, 246);
            this.buttonSaveMasterPassword.Name = "buttonSaveMasterPassword";
            this.buttonSaveMasterPassword.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveMasterPassword.TabIndex = 38;
            this.buttonSaveMasterPassword.Text = "Save";
            this.buttonSaveMasterPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveMasterPassword.UseVisualStyleBackColor = false;
            this.buttonSaveMasterPassword.Click += new System.EventHandler(this.buttonSaveMasterPassword_Click);
            // 
            // userTextBoxConfirmNewPassword
            // 
            this.userTextBoxConfirmNewPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxConfirmNewPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxConfirmNewPassword.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxConfirmNewPassword.Location = new System.Drawing.Point(156, 209);
            this.userTextBoxConfirmNewPassword.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxConfirmNewPassword.Name = "userTextBoxConfirmNewPassword";
            this.userTextBoxConfirmNewPassword.PasswordChar = '*';
            this.userTextBoxConfirmNewPassword.Size = new System.Drawing.Size(131, 18);
            this.userTextBoxConfirmNewPassword.TabIndex = 37;
            // 
            // labelConfirmNewPassword
            // 
            this.labelConfirmNewPassword.AutoSize = true;
            this.labelConfirmNewPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelConfirmNewPassword.Location = new System.Drawing.Point(12, 212);
            this.labelConfirmNewPassword.Name = "labelConfirmNewPassword";
            this.labelConfirmNewPassword.Size = new System.Drawing.Size(141, 11);
            this.labelConfirmNewPassword.TabIndex = 36;
            this.labelConfirmNewPassword.Text = "Confirm password:";
            // 
            // userTextBoxNewPassword
            // 
            this.userTextBoxNewPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxNewPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxNewPassword.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxNewPassword.Location = new System.Drawing.Point(156, 182);
            this.userTextBoxNewPassword.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxNewPassword.Name = "userTextBoxNewPassword";
            this.userTextBoxNewPassword.PasswordChar = '*';
            this.userTextBoxNewPassword.Size = new System.Drawing.Size(131, 18);
            this.userTextBoxNewPassword.TabIndex = 35;
            // 
            // labelNewPassword
            // 
            this.labelNewPassword.AutoSize = true;
            this.labelNewPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNewPassword.Location = new System.Drawing.Point(12, 185);
            this.labelNewPassword.Name = "labelNewPassword";
            this.labelNewPassword.Size = new System.Drawing.Size(109, 11);
            this.labelNewPassword.TabIndex = 34;
            this.labelNewPassword.Text = "New password:";
            // 
            // userTextBoxOldPassword
            // 
            this.userTextBoxOldPassword.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxOldPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxOldPassword.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxOldPassword.Location = new System.Drawing.Point(156, 139);
            this.userTextBoxOldPassword.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxOldPassword.Name = "userTextBoxOldPassword";
            this.userTextBoxOldPassword.PasswordChar = '*';
            this.userTextBoxOldPassword.Size = new System.Drawing.Size(131, 18);
            this.userTextBoxOldPassword.TabIndex = 33;
            // 
            // labelOldPassword
            // 
            this.labelOldPassword.AutoSize = true;
            this.labelOldPassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOldPassword.Location = new System.Drawing.Point(12, 142);
            this.labelOldPassword.Name = "labelOldPassword";
            this.labelOldPassword.Size = new System.Drawing.Size(109, 11);
            this.labelOldPassword.TabIndex = 32;
            this.labelOldPassword.Text = "Old password:";
            // 
            // tabPageBackupDB
            // 
            this.tabPageBackupDB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageBackupDB.Controls.Add(this.pictureBoxSelectFile);
            this.tabPageBackupDB.Controls.Add(this.buttonBackupDB);
            this.tabPageBackupDB.Controls.Add(this.labelBackupFile);
            this.tabPageBackupDB.Controls.Add(this.userTextBoxBackupFile);
            this.tabPageBackupDB.Location = new System.Drawing.Point(154, 4);
            this.tabPageBackupDB.Name = "tabPageBackupDB";
            this.tabPageBackupDB.Size = new System.Drawing.Size(303, 398);
            this.tabPageBackupDB.TabIndex = 2;
            this.tabPageBackupDB.Text = "Backup DB";
            // 
            // pictureBoxSelectFile
            // 
            this.pictureBoxSelectFile.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSelectFile.Image")));
            this.pictureBoxSelectFile.Location = new System.Drawing.Point(221, 159);
            this.pictureBoxSelectFile.Name = "pictureBoxSelectFile";
            this.pictureBoxSelectFile.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxSelectFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxSelectFile.TabIndex = 41;
            this.pictureBoxSelectFile.TabStop = false;
            this.pictureBoxSelectFile.Click += new System.EventHandler(this.pictureBoxSelectFile_Click);
            // 
            // buttonBackupDB
            // 
            this.buttonBackupDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBackupDB.BackColor = System.Drawing.Color.DarkGray;
            this.buttonBackupDB.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonBackupDB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonBackupDB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonBackupDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBackupDB.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonBackupDB.Image = ((System.Drawing.Image)(resources.GetObject("buttonBackupDB.Image")));
            this.buttonBackupDB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBackupDB.Location = new System.Drawing.Point(17, 180);
            this.buttonBackupDB.Name = "buttonBackupDB";
            this.buttonBackupDB.Size = new System.Drawing.Size(75, 23);
            this.buttonBackupDB.TabIndex = 40;
            this.buttonBackupDB.Text = "Save";
            this.buttonBackupDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonBackupDB.UseVisualStyleBackColor = false;
            this.buttonBackupDB.Click += new System.EventHandler(this.buttonBackupDB_Click);
            // 
            // labelBackupFile
            // 
            this.labelBackupFile.AutoSize = true;
            this.labelBackupFile.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelBackupFile.Location = new System.Drawing.Point(15, 148);
            this.labelBackupFile.Name = "labelBackupFile";
            this.labelBackupFile.Size = new System.Drawing.Size(101, 11);
            this.labelBackupFile.TabIndex = 34;
            this.labelBackupFile.Text = "Backup file:";
            // 
            // userTextBoxBackupFile
            // 
            this.userTextBoxBackupFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxBackupFile.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxBackupFile.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxBackupFile.Location = new System.Drawing.Point(17, 159);
            this.userTextBoxBackupFile.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxBackupFile.Name = "userTextBoxBackupFile";
            this.userTextBoxBackupFile.Size = new System.Drawing.Size(201, 18);
            this.userTextBoxBackupFile.TabIndex = 35;
            // 
            // tabPageRestoreDB
            // 
            this.tabPageRestoreDB.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageRestoreDB.Controls.Add(this.pictureBoxSelectRestoreFile);
            this.tabPageRestoreDB.Controls.Add(this.buttonRestoreDB);
            this.tabPageRestoreDB.Controls.Add(this.labelSelectRestoreFile);
            this.tabPageRestoreDB.Controls.Add(this.userTextBoxRestoreFile);
            this.tabPageRestoreDB.Location = new System.Drawing.Point(154, 4);
            this.tabPageRestoreDB.Name = "tabPageRestoreDB";
            this.tabPageRestoreDB.Size = new System.Drawing.Size(303, 398);
            this.tabPageRestoreDB.TabIndex = 3;
            this.tabPageRestoreDB.Text = "Restore DB";
            // 
            // pictureBoxSelectRestoreFile
            // 
            this.pictureBoxSelectRestoreFile.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSelectRestoreFile.Image")));
            this.pictureBoxSelectRestoreFile.Location = new System.Drawing.Point(227, 154);
            this.pictureBoxSelectRestoreFile.Name = "pictureBoxSelectRestoreFile";
            this.pictureBoxSelectRestoreFile.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxSelectRestoreFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxSelectRestoreFile.TabIndex = 45;
            this.pictureBoxSelectRestoreFile.TabStop = false;
            this.pictureBoxSelectRestoreFile.Click += new System.EventHandler(this.pictureBoxSelectRestoreFile_Click);
            // 
            // buttonRestoreDB
            // 
            this.buttonRestoreDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRestoreDB.BackColor = System.Drawing.Color.DarkGray;
            this.buttonRestoreDB.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonRestoreDB.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonRestoreDB.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonRestoreDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRestoreDB.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonRestoreDB.Image = ((System.Drawing.Image)(resources.GetObject("buttonRestoreDB.Image")));
            this.buttonRestoreDB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRestoreDB.Location = new System.Drawing.Point(23, 175);
            this.buttonRestoreDB.Name = "buttonRestoreDB";
            this.buttonRestoreDB.Size = new System.Drawing.Size(89, 23);
            this.buttonRestoreDB.TabIndex = 44;
            this.buttonRestoreDB.Text = "Restore";
            this.buttonRestoreDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRestoreDB.UseVisualStyleBackColor = false;
            this.buttonRestoreDB.Click += new System.EventHandler(this.buttonRestoreDB_Click);
            // 
            // labelSelectRestoreFile
            // 
            this.labelSelectRestoreFile.AutoSize = true;
            this.labelSelectRestoreFile.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSelectRestoreFile.Location = new System.Drawing.Point(21, 143);
            this.labelSelectRestoreFile.Name = "labelSelectRestoreFile";
            this.labelSelectRestoreFile.Size = new System.Drawing.Size(109, 11);
            this.labelSelectRestoreFile.TabIndex = 42;
            this.labelSelectRestoreFile.Text = "Restore file:";
            // 
            // userTextBoxRestoreFile
            // 
            this.userTextBoxRestoreFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxRestoreFile.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxRestoreFile.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxRestoreFile.Location = new System.Drawing.Point(23, 154);
            this.userTextBoxRestoreFile.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxRestoreFile.Name = "userTextBoxRestoreFile";
            this.userTextBoxRestoreFile.Size = new System.Drawing.Size(201, 18);
            this.userTextBoxRestoreFile.TabIndex = 43;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(22, 180);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(70, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 42;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(461, 406);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main";
            this.Text = "FDP Admin";
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.Load += new System.EventHandler(this.main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageConnectionSettings.ResumeLayout(false);
            this.tabPageConnectionSettings.PerformLayout();
            this.tabPageMasterPassword.ResumeLayout(false);
            this.tabPageMasterPassword.PerformLayout();
            this.tabPageBackupDB.ResumeLayout(false);
            this.tabPageBackupDB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectFile)).EndInit();
            this.tabPageRestoreDB.ResumeLayout(false);
            this.tabPageRestoreDB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectRestoreFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageConnectionSettings;
        private System.Windows.Forms.TabPage tabPageMasterPassword;
        private System.Windows.Forms.TabPage tabPageBackupDB;
        private System.Windows.Forms.TabPage tabPageRestoreDB;
        private System.Windows.Forms.Button buttonSaveConnectionString;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Button buttonTestConnection;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private FDP.UserTextBox userTextBoxUser;
        private FDP.UserTextBox userTextBoxPassowrd;
        private FDP.UserTextBox userTextBoxDatabase;
        private FDP.UserTextBox userTextBoxPort;
        private FDP.UserTextBox userTextBoxServer;
        private System.Windows.Forms.Button buttonSaveMasterPassword;
        private FDP.UserTextBox userTextBoxConfirmNewPassword;
        private System.Windows.Forms.Label labelConfirmNewPassword;
        private FDP.UserTextBox userTextBoxNewPassword;
        private System.Windows.Forms.Label labelNewPassword;
        private FDP.UserTextBox userTextBoxOldPassword;
        private System.Windows.Forms.Label labelOldPassword;
        private System.Windows.Forms.PictureBox pictureBoxSelectFile;
        private System.Windows.Forms.Button buttonBackupDB;
        private FDP.UserTextBox userTextBoxBackupFile;
        private System.Windows.Forms.Label labelBackupFile;
        private System.Windows.Forms.PictureBox pictureBoxSelectRestoreFile;
        private System.Windows.Forms.Button buttonRestoreDB;
        private FDP.UserTextBox userTextBoxRestoreFile;
        private System.Windows.Forms.Label labelSelectRestoreFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

