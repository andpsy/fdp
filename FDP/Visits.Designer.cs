namespace FDP
{
    partial class Visits
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visits));
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.buttonAssetConditions = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxPayer = new System.Windows.Forms.ComboBox();
            this.labelPayer = new System.Windows.Forms.Label();
            this.comboBoxEmployee = new System.Windows.Forms.ComboBox();
            this.labelEmployee = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labelDate = new System.Windows.Forms.Label();
            this.comboBoxReason = new System.Windows.Forms.ComboBox();
            this.labelReason = new System.Windows.Forms.Label();
            this.labelProperty = new System.Windows.Forms.Label();
            this.pictureBoxSelectOwner = new System.Windows.Forms.PictureBox();
            this.comboBoxProperties = new System.Windows.Forms.ComboBox();
            this.userTextBoxComments = new FDP.UserTextBox();
            this.labelComments = new System.Windows.Forms.Label();
            this.panelErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectOwner)).BeginInit();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 221);
            this.panelErrors.Size = new System.Drawing.Size(481, 76);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Location = new System.Drawing.Point(2, 33);
            this.listBoxErrors.Size = new System.Drawing.Size(475, 39);
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
            this.buttonExit.Location = new System.Drawing.Point(403, 269);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 23);
            this.buttonExit.TabIndex = 23;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSave.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.Black;
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(9, 269);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(80, 23);
            this.buttonSave.TabIndex = 35;
            this.buttonSave.Text = "Save";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // buttonAssetConditions
            // 
            this.buttonAssetConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAssetConditions.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAssetConditions.Enabled = false;
            this.buttonAssetConditions.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAssetConditions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAssetConditions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAssetConditions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAssetConditions.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAssetConditions.ForeColor = System.Drawing.Color.Black;
            this.buttonAssetConditions.Image = ((System.Drawing.Image)(resources.GetObject("buttonAssetConditions.Image")));
            this.buttonAssetConditions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAssetConditions.Location = new System.Drawing.Point(95, 269);
            this.buttonAssetConditions.Name = "buttonAssetConditions";
            this.buttonAssetConditions.Size = new System.Drawing.Size(80, 23);
            this.buttonAssetConditions.TabIndex = 36;
            this.buttonAssetConditions.Text = "Assets";
            this.buttonAssetConditions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAssetConditions.UseVisualStyleBackColor = false;
            this.buttonAssetConditions.Click += new System.EventHandler(this.buttonAssetConditions_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxPayer);
            this.panel1.Controls.Add(this.labelPayer);
            this.panel1.Controls.Add(this.comboBoxEmployee);
            this.panel1.Controls.Add(this.labelEmployee);
            this.panel1.Controls.Add(this.dateTimePickerDate);
            this.panel1.Controls.Add(this.labelDate);
            this.panel1.Controls.Add(this.comboBoxReason);
            this.panel1.Controls.Add(this.labelReason);
            this.panel1.Controls.Add(this.labelProperty);
            this.panel1.Controls.Add(this.pictureBoxSelectOwner);
            this.panel1.Controls.Add(this.comboBoxProperties);
            this.panel1.Controls.Add(this.userTextBoxComments);
            this.panel1.Controls.Add(this.labelComments);
            this.panel1.Location = new System.Drawing.Point(5, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 255);
            this.panel1.TabIndex = 37;
            // 
            // comboBoxPayer
            // 
            this.comboBoxPayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPayer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxPayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPayer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxPayer.FormattingEnabled = true;
            this.comboBoxPayer.Location = new System.Drawing.Point(210, 219);
            this.comboBoxPayer.Name = "comboBoxPayer";
            this.comboBoxPayer.Size = new System.Drawing.Size(213, 19);
            this.comboBoxPayer.TabIndex = 83;
            this.comboBoxPayer.Visible = false;
            // 
            // labelPayer
            // 
            this.labelPayer.AutoSize = true;
            this.labelPayer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPayer.Location = new System.Drawing.Point(59, 229);
            this.labelPayer.Name = "labelPayer";
            this.labelPayer.Size = new System.Drawing.Size(53, 11);
            this.labelPayer.TabIndex = 84;
            this.labelPayer.Text = "Payer:";
            this.labelPayer.Visible = false;
            // 
            // comboBoxEmployee
            // 
            this.comboBoxEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEmployee.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmployee.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxEmployee.FormattingEnabled = true;
            this.comboBoxEmployee.Location = new System.Drawing.Point(210, 112);
            this.comboBoxEmployee.Name = "comboBoxEmployee";
            this.comboBoxEmployee.Size = new System.Drawing.Size(213, 19);
            this.comboBoxEmployee.TabIndex = 81;
            // 
            // labelEmployee
            // 
            this.labelEmployee.AutoSize = true;
            this.labelEmployee.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEmployee.Location = new System.Drawing.Point(59, 116);
            this.labelEmployee.Name = "labelEmployee";
            this.labelEmployee.Size = new System.Drawing.Size(77, 11);
            this.labelEmployee.TabIndex = 82;
            this.labelEmployee.Text = "Employee:";
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerDate.CalendarTitleBackColor = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerDate.CalendarTitleForeColor = System.Drawing.Color.SaddleBrown;
            this.dateTimePickerDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(210, 14);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(109, 18);
            this.dateTimePickerDate.TabIndex = 79;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDate.Location = new System.Drawing.Point(59, 18);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(45, 11);
            this.labelDate.TabIndex = 80;
            this.labelDate.Text = "Date:";
            // 
            // comboBoxReason
            // 
            this.comboBoxReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxReason.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReason.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxReason.FormattingEnabled = true;
            this.comboBoxReason.Location = new System.Drawing.Point(210, 77);
            this.comboBoxReason.Name = "comboBoxReason";
            this.comboBoxReason.Size = new System.Drawing.Size(213, 19);
            this.comboBoxReason.TabIndex = 77;
            // 
            // labelReason
            // 
            this.labelReason.AutoSize = true;
            this.labelReason.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelReason.Location = new System.Drawing.Point(59, 81);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(61, 11);
            this.labelReason.TabIndex = 78;
            this.labelReason.Text = "Reason:";
            // 
            // labelProperty
            // 
            this.labelProperty.AutoSize = true;
            this.labelProperty.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelProperty.Location = new System.Drawing.Point(59, 47);
            this.labelProperty.Name = "labelProperty";
            this.labelProperty.Size = new System.Drawing.Size(93, 11);
            this.labelProperty.TabIndex = 76;
            this.labelProperty.Text = "Properties:";
            // 
            // pictureBoxSelectOwner
            // 
            this.pictureBoxSelectOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSelectOwner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSelectOwner.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSelectOwner.Image")));
            this.pictureBoxSelectOwner.Location = new System.Drawing.Point(407, 45);
            this.pictureBoxSelectOwner.Name = "pictureBoxSelectOwner";
            this.pictureBoxSelectOwner.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxSelectOwner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxSelectOwner.TabIndex = 75;
            this.pictureBoxSelectOwner.TabStop = false;
            // 
            // comboBoxProperties
            // 
            this.comboBoxProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProperties.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxProperties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProperties.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxProperties.FormattingEnabled = true;
            this.comboBoxProperties.Location = new System.Drawing.Point(210, 44);
            this.comboBoxProperties.Name = "comboBoxProperties";
            this.comboBoxProperties.Size = new System.Drawing.Size(197, 19);
            this.comboBoxProperties.TabIndex = 74;
            // 
            // userTextBoxComments
            // 
            this.userTextBoxComments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userTextBoxComments.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxComments.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxComments.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxComments.Location = new System.Drawing.Point(210, 144);
            this.userTextBoxComments.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxComments.Multiline = true;
            this.userTextBoxComments.Name = "userTextBoxComments";
            this.userTextBoxComments.Size = new System.Drawing.Size(213, 63);
            this.userTextBoxComments.TabIndex = 73;
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelComments.Location = new System.Drawing.Point(57, 147);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(141, 11);
            this.labelComments.TabIndex = 72;
            this.labelComments.Text = "Details/Comments:";
            // 
            // Visits
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(491, 302);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonAssetConditions);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "Visits";
            this.Text = "Inspection";
            this.Load += new System.EventHandler(this.Visits_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.buttonAssetConditions, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectOwner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button buttonAssetConditions;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBoxPayer;
        private System.Windows.Forms.Label labelPayer;
        private System.Windows.Forms.ComboBox comboBoxEmployee;
        private System.Windows.Forms.Label labelEmployee;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.ComboBox comboBoxReason;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.Label labelProperty;
        private System.Windows.Forms.PictureBox pictureBoxSelectOwner;
        private System.Windows.Forms.ComboBox comboBoxProperties;
        private UserTextBox userTextBoxComments;
        private System.Windows.Forms.Label labelComments;
    }
}