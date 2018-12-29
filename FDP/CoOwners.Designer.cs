namespace FDP
{
    partial class CoOwners
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoOwners));
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSaveCoOwners = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxRenouncementForRentIncomes = new System.Windows.Forms.CheckBox();
            this.labelRenouncementForRentIncomes = new System.Windows.Forms.Label();
            this.userTextBoxNIF = new FDP.UserTextBox();
            this.labelNif = new System.Windows.Forms.Label();
            this.userTextBoxOwnerComments = new FDP.UserTextBox();
            this.labelComments = new System.Windows.Forms.Label();
            this.userTextBoxOwnerCui = new FDP.UserTextBox();
            this.labelCui = new System.Windows.Forms.Label();
            this.userTextBoxOwnerCnp = new FDP.UserTextBox();
            this.labelCnp = new System.Windows.Forms.Label();
            this.userTextBoxOwnerCif = new FDP.UserTextBox();
            this.labelCif = new System.Windows.Forms.Label();
            this.userTextBoxOwnerFullName = new FDP.UserTextBox();
            this.labelFullName = new System.Windows.Forms.Label();
            this.userTextBoxOwnerName = new FDP.UserTextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.panelErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 337);
            this.panelErrors.Size = new System.Drawing.Size(572, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(566, 39);
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
            this.buttonExit.Location = new System.Drawing.Point(492, 379);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 23);
            this.buttonExit.TabIndex = 23;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonSaveCoOwners
            // 
            this.buttonSaveCoOwners.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSaveCoOwners.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSaveCoOwners.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSaveCoOwners.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSaveCoOwners.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSaveCoOwners.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveCoOwners.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveCoOwners.ForeColor = System.Drawing.Color.Black;
            this.buttonSaveCoOwners.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveCoOwners.Image")));
            this.buttonSaveCoOwners.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveCoOwners.Location = new System.Drawing.Point(9, 379);
            this.buttonSaveCoOwners.Name = "buttonSaveCoOwners";
            this.buttonSaveCoOwners.Size = new System.Drawing.Size(80, 23);
            this.buttonSaveCoOwners.TabIndex = 35;
            this.buttonSaveCoOwners.Text = "Save";
            this.buttonSaveCoOwners.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveCoOwners.UseVisualStyleBackColor = false;
            this.buttonSaveCoOwners.Click += new System.EventHandler(this.buttonSaveCoOwners_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.checkBoxRenouncementForRentIncomes);
            this.panel1.Controls.Add(this.labelRenouncementForRentIncomes);
            this.panel1.Controls.Add(this.userTextBoxNIF);
            this.panel1.Controls.Add(this.labelNif);
            this.panel1.Controls.Add(this.userTextBoxOwnerComments);
            this.panel1.Controls.Add(this.labelComments);
            this.panel1.Controls.Add(this.userTextBoxOwnerCui);
            this.panel1.Controls.Add(this.labelCui);
            this.panel1.Controls.Add(this.userTextBoxOwnerCnp);
            this.panel1.Controls.Add(this.labelCnp);
            this.panel1.Controls.Add(this.userTextBoxOwnerCif);
            this.panel1.Controls.Add(this.labelCif);
            this.panel1.Controls.Add(this.userTextBoxOwnerFullName);
            this.panel1.Controls.Add(this.labelFullName);
            this.panel1.Controls.Add(this.userTextBoxOwnerName);
            this.panel1.Controls.Add(this.labelName);
            this.panel1.Location = new System.Drawing.Point(5, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(572, 365);
            this.panel1.TabIndex = 36;
            // 
            // checkBoxRenouncementForRentIncomes
            // 
            this.checkBoxRenouncementForRentIncomes.AutoSize = true;
            this.checkBoxRenouncementForRentIncomes.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkBoxRenouncementForRentIncomes.Location = new System.Drawing.Point(266, 295);
            this.checkBoxRenouncementForRentIncomes.Name = "checkBoxRenouncementForRentIncomes";
            this.checkBoxRenouncementForRentIncomes.Size = new System.Drawing.Size(15, 14);
            this.checkBoxRenouncementForRentIncomes.TabIndex = 51;
            this.checkBoxRenouncementForRentIncomes.UseVisualStyleBackColor = true;
            // 
            // labelRenouncementForRentIncomes
            // 
            this.labelRenouncementForRentIncomes.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRenouncementForRentIncomes.Location = new System.Drawing.Point(115, 289);
            this.labelRenouncementForRentIncomes.Name = "labelRenouncementForRentIncomes";
            this.labelRenouncementForRentIncomes.Size = new System.Drawing.Size(139, 28);
            this.labelRenouncementForRentIncomes.TabIndex = 52;
            this.labelRenouncementForRentIncomes.Text = "Renouncement for rent income:";
            // 
            // userTextBoxNIF
            // 
            this.userTextBoxNIF.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxNIF.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxNIF.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxNIF.Location = new System.Drawing.Point(266, 160);
            this.userTextBoxNIF.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxNIF.Name = "userTextBoxNIF";
            this.userTextBoxNIF.Size = new System.Drawing.Size(190, 18);
            this.userTextBoxNIF.TabIndex = 50;
            // 
            // labelNif
            // 
            this.labelNif.AutoSize = true;
            this.labelNif.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNif.Location = new System.Drawing.Point(113, 163);
            this.labelNif.Name = "labelNif";
            this.labelNif.Size = new System.Drawing.Size(37, 11);
            this.labelNif.TabIndex = 49;
            this.labelNif.Text = "NIF:";
            // 
            // userTextBoxOwnerComments
            // 
            this.userTextBoxOwnerComments.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxOwnerComments.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxOwnerComments.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxOwnerComments.Location = new System.Drawing.Point(266, 216);
            this.userTextBoxOwnerComments.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxOwnerComments.Multiline = true;
            this.userTextBoxOwnerComments.Name = "userTextBoxOwnerComments";
            this.userTextBoxOwnerComments.Size = new System.Drawing.Size(190, 70);
            this.userTextBoxOwnerComments.TabIndex = 48;
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelComments.Location = new System.Drawing.Point(113, 219);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(141, 11);
            this.labelComments.TabIndex = 47;
            this.labelComments.Text = "Details/Comments:";
            // 
            // userTextBoxOwnerCui
            // 
            this.userTextBoxOwnerCui.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxOwnerCui.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxOwnerCui.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxOwnerCui.Location = new System.Drawing.Point(266, 131);
            this.userTextBoxOwnerCui.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxOwnerCui.Name = "userTextBoxOwnerCui";
            this.userTextBoxOwnerCui.Size = new System.Drawing.Size(190, 18);
            this.userTextBoxOwnerCui.TabIndex = 46;
            // 
            // labelCui
            // 
            this.labelCui.AutoSize = true;
            this.labelCui.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCui.Location = new System.Drawing.Point(113, 134);
            this.labelCui.Name = "labelCui";
            this.labelCui.Size = new System.Drawing.Size(37, 11);
            this.labelCui.TabIndex = 45;
            this.labelCui.Text = "CUI:";
            // 
            // userTextBoxOwnerCnp
            // 
            this.userTextBoxOwnerCnp.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxOwnerCnp.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxOwnerCnp.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxOwnerCnp.Location = new System.Drawing.Point(266, 188);
            this.userTextBoxOwnerCnp.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxOwnerCnp.Name = "userTextBoxOwnerCnp";
            this.userTextBoxOwnerCnp.Size = new System.Drawing.Size(190, 18);
            this.userTextBoxOwnerCnp.TabIndex = 44;
            // 
            // labelCnp
            // 
            this.labelCnp.AutoSize = true;
            this.labelCnp.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCnp.Location = new System.Drawing.Point(113, 191);
            this.labelCnp.Name = "labelCnp";
            this.labelCnp.Size = new System.Drawing.Size(37, 11);
            this.labelCnp.TabIndex = 43;
            this.labelCnp.Text = "CNP:";
            // 
            // userTextBoxOwnerCif
            // 
            this.userTextBoxOwnerCif.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxOwnerCif.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxOwnerCif.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxOwnerCif.Location = new System.Drawing.Point(266, 103);
            this.userTextBoxOwnerCif.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxOwnerCif.Name = "userTextBoxOwnerCif";
            this.userTextBoxOwnerCif.Size = new System.Drawing.Size(190, 18);
            this.userTextBoxOwnerCif.TabIndex = 42;
            // 
            // labelCif
            // 
            this.labelCif.AutoSize = true;
            this.labelCif.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCif.Location = new System.Drawing.Point(113, 106);
            this.labelCif.Name = "labelCif";
            this.labelCif.Size = new System.Drawing.Size(37, 11);
            this.labelCif.TabIndex = 41;
            this.labelCif.Text = "CIF:";
            // 
            // userTextBoxOwnerFullName
            // 
            this.userTextBoxOwnerFullName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxOwnerFullName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxOwnerFullName.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxOwnerFullName.Location = new System.Drawing.Point(266, 75);
            this.userTextBoxOwnerFullName.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxOwnerFullName.Name = "userTextBoxOwnerFullName";
            this.userTextBoxOwnerFullName.Size = new System.Drawing.Size(190, 18);
            this.userTextBoxOwnerFullName.TabIndex = 40;
            // 
            // labelFullName
            // 
            this.labelFullName.AutoSize = true;
            this.labelFullName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFullName.Location = new System.Drawing.Point(113, 78);
            this.labelFullName.Name = "labelFullName";
            this.labelFullName.Size = new System.Drawing.Size(85, 11);
            this.labelFullName.TabIndex = 39;
            this.labelFullName.Text = "Full Name:";
            // 
            // userTextBoxOwnerName
            // 
            this.userTextBoxOwnerName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxOwnerName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxOwnerName.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxOwnerName.Location = new System.Drawing.Point(266, 47);
            this.userTextBoxOwnerName.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxOwnerName.Name = "userTextBoxOwnerName";
            this.userTextBoxOwnerName.Size = new System.Drawing.Size(190, 18);
            this.userTextBoxOwnerName.TabIndex = 38;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelName.Location = new System.Drawing.Point(113, 50);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(45, 11);
            this.labelName.TabIndex = 37;
            this.labelName.Text = "Name:";
            // 
            // CoOwners
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(582, 412);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonSaveCoOwners);
            this.Controls.Add(this.buttonExit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "CoOwners";
            this.Text = "Co-Owners";
            this.Load += new System.EventHandler(this.Owners_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.buttonSaveCoOwners, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonSaveCoOwners;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxRenouncementForRentIncomes;
        private System.Windows.Forms.Label labelRenouncementForRentIncomes;
        private UserTextBox userTextBoxNIF;
        private System.Windows.Forms.Label labelNif;
        private UserTextBox userTextBoxOwnerComments;
        private System.Windows.Forms.Label labelComments;
        private UserTextBox userTextBoxOwnerCui;
        private System.Windows.Forms.Label labelCui;
        private UserTextBox userTextBoxOwnerCnp;
        private System.Windows.Forms.Label labelCnp;
        private UserTextBox userTextBoxOwnerCif;
        private System.Windows.Forms.Label labelCif;
        private UserTextBox userTextBoxOwnerFullName;
        private System.Windows.Forms.Label labelFullName;
        private UserTextBox userTextBoxOwnerName;
        private System.Windows.Forms.Label labelName;
    }
}