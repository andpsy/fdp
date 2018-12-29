namespace FDP
{
    partial class InvoiceRequirements
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceRequirements));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelNotInvoiceable = new System.Windows.Forms.Label();
            this.checkBoxNotInvoiceable = new System.Windows.Forms.CheckBox();
            this.comboBoxCurrency = new System.Windows.Forms.ComboBox();
            this.labelCurrency = new System.Windows.Forms.Label();
            this.dataGridViewContracts = new System.Windows.Forms.DataGridView();
            this.labelContract = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labelDate = new System.Windows.Forms.Label();
            this.comboBoxService = new System.Windows.Forms.ComboBox();
            this.labelService = new System.Windows.Forms.Label();
            this.dataGridViewProperties = new System.Windows.Forms.DataGridView();
            this.labelProperty = new System.Windows.Forms.Label();
            this.pictureBoxSelectOwner = new System.Windows.Forms.PictureBox();
            this.comboBoxOwner = new System.Windows.Forms.ComboBox();
            this.labelOwner = new System.Windows.Forms.Label();
            this.userTextBoxComments = new FDP.UserTextBox();
            this.labelComments = new System.Windows.Forms.Label();
            this.userTextBoxPrice = new FDP.UserTextBox();
            this.labelPrice = new System.Windows.Forms.Label();
            this.panelErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContracts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectOwner)).BeginInit();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 383);
            this.panelErrors.Size = new System.Drawing.Size(502, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(496, 39);
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
            this.buttonExit.Location = new System.Drawing.Point(424, 425);
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
            this.buttonSave.Location = new System.Drawing.Point(9, 425);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.labelNotInvoiceable);
            this.panel1.Controls.Add(this.checkBoxNotInvoiceable);
            this.panel1.Controls.Add(this.comboBoxCurrency);
            this.panel1.Controls.Add(this.labelCurrency);
            this.panel1.Controls.Add(this.dataGridViewContracts);
            this.panel1.Controls.Add(this.labelContract);
            this.panel1.Controls.Add(this.dateTimePickerDate);
            this.panel1.Controls.Add(this.labelDate);
            this.panel1.Controls.Add(this.comboBoxService);
            this.panel1.Controls.Add(this.labelService);
            this.panel1.Controls.Add(this.dataGridViewProperties);
            this.panel1.Controls.Add(this.labelProperty);
            this.panel1.Controls.Add(this.pictureBoxSelectOwner);
            this.panel1.Controls.Add(this.comboBoxOwner);
            this.panel1.Controls.Add(this.labelOwner);
            this.panel1.Controls.Add(this.userTextBoxComments);
            this.panel1.Controls.Add(this.labelComments);
            this.panel1.Controls.Add(this.userTextBoxPrice);
            this.panel1.Controls.Add(this.labelPrice);
            this.panel1.Location = new System.Drawing.Point(5, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(502, 411);
            this.panel1.TabIndex = 36;
            // 
            // labelNotInvoiceable
            // 
            this.labelNotInvoiceable.AutoSize = true;
            this.labelNotInvoiceable.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNotInvoiceable.Location = new System.Drawing.Point(317, 314);
            this.labelNotInvoiceable.Name = "labelNotInvoiceable";
            this.labelNotInvoiceable.Size = new System.Drawing.Size(133, 11);
            this.labelNotInvoiceable.TabIndex = 92;
            this.labelNotInvoiceable.Text = "Not Invoiceable:";
            // 
            // checkBoxNotInvoiceable
            // 
            this.checkBoxNotInvoiceable.AutoSize = true;
            this.checkBoxNotInvoiceable.Location = new System.Drawing.Point(468, 311);
            this.checkBoxNotInvoiceable.Name = "checkBoxNotInvoiceable";
            this.checkBoxNotInvoiceable.Size = new System.Drawing.Size(15, 14);
            this.checkBoxNotInvoiceable.TabIndex = 91;
            this.checkBoxNotInvoiceable.UseVisualStyleBackColor = true;
            // 
            // comboBoxCurrency
            // 
            this.comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrency.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxCurrency.FormattingEnabled = true;
            this.comboBoxCurrency.Location = new System.Drawing.Point(171, 369);
            this.comboBoxCurrency.Name = "comboBoxCurrency";
            this.comboBoxCurrency.Size = new System.Drawing.Size(109, 19);
            this.comboBoxCurrency.TabIndex = 89;
            // 
            // labelCurrency
            // 
            this.labelCurrency.AutoSize = true;
            this.labelCurrency.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCurrency.Location = new System.Drawing.Point(20, 373);
            this.labelCurrency.Name = "labelCurrency";
            this.labelCurrency.Size = new System.Drawing.Size(77, 11);
            this.labelCurrency.TabIndex = 90;
            this.labelCurrency.Text = "Currency:";
            // 
            // dataGridViewContracts
            // 
            this.dataGridViewContracts.AllowUserToAddRows = false;
            this.dataGridViewContracts.AllowUserToDeleteRows = false;
            this.dataGridViewContracts.AllowUserToOrderColumns = true;
            this.dataGridViewContracts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewContracts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewContracts.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewContracts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewContracts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewContracts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewContracts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewContracts.Location = new System.Drawing.Point(171, 47);
            this.dataGridViewContracts.Name = "dataGridViewContracts";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewContracts.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewContracts.RowHeadersVisible = false;
            this.dataGridViewContracts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewContracts.ShowCellErrors = false;
            this.dataGridViewContracts.ShowEditingIcon = false;
            this.dataGridViewContracts.ShowRowErrors = false;
            this.dataGridViewContracts.Size = new System.Drawing.Size(312, 62);
            this.dataGridViewContracts.TabIndex = 88;
            // 
            // labelContract
            // 
            this.labelContract.AutoSize = true;
            this.labelContract.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelContract.Location = new System.Drawing.Point(20, 51);
            this.labelContract.Name = "labelContract";
            this.labelContract.Size = new System.Drawing.Size(77, 11);
            this.labelContract.TabIndex = 87;
            this.labelContract.Text = "Contract:";
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerDate.CalendarTitleBackColor = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerDate.CalendarTitleForeColor = System.Drawing.Color.SaddleBrown;
            this.dateTimePickerDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(171, 310);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(109, 18);
            this.dateTimePickerDate.TabIndex = 85;
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDate.Location = new System.Drawing.Point(20, 314);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(45, 11);
            this.labelDate.TabIndex = 86;
            this.labelDate.Text = "Date:";
            // 
            // comboBoxService
            // 
            this.comboBoxService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxService.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxService.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxService.FormattingEnabled = true;
            this.comboBoxService.Location = new System.Drawing.Point(171, 207);
            this.comboBoxService.Name = "comboBoxService";
            this.comboBoxService.Size = new System.Drawing.Size(312, 19);
            this.comboBoxService.TabIndex = 83;
            // 
            // labelService
            // 
            this.labelService.AutoSize = true;
            this.labelService.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelService.Location = new System.Drawing.Point(20, 211);
            this.labelService.Name = "labelService";
            this.labelService.Size = new System.Drawing.Size(69, 11);
            this.labelService.TabIndex = 84;
            this.labelService.Text = "Service:";
            // 
            // dataGridViewProperties
            // 
            this.dataGridViewProperties.AllowUserToAddRows = false;
            this.dataGridViewProperties.AllowUserToDeleteRows = false;
            this.dataGridViewProperties.AllowUserToOrderColumns = true;
            this.dataGridViewProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProperties.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProperties.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewProperties.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProperties.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewProperties.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewProperties.Location = new System.Drawing.Point(171, 120);
            this.dataGridViewProperties.Name = "dataGridViewProperties";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProperties.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewProperties.RowHeadersVisible = false;
            this.dataGridViewProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProperties.ShowCellErrors = false;
            this.dataGridViewProperties.ShowEditingIcon = false;
            this.dataGridViewProperties.ShowRowErrors = false;
            this.dataGridViewProperties.Size = new System.Drawing.Size(312, 81);
            this.dataGridViewProperties.TabIndex = 82;
            // 
            // labelProperty
            // 
            this.labelProperty.AutoSize = true;
            this.labelProperty.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelProperty.Location = new System.Drawing.Point(20, 127);
            this.labelProperty.Name = "labelProperty";
            this.labelProperty.Size = new System.Drawing.Size(93, 11);
            this.labelProperty.TabIndex = 81;
            this.labelProperty.Text = "Properties:";
            // 
            // pictureBoxSelectOwner
            // 
            this.pictureBoxSelectOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSelectOwner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSelectOwner.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSelectOwner.Image")));
            this.pictureBoxSelectOwner.Location = new System.Drawing.Point(465, 18);
            this.pictureBoxSelectOwner.Name = "pictureBoxSelectOwner";
            this.pictureBoxSelectOwner.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxSelectOwner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxSelectOwner.TabIndex = 80;
            this.pictureBoxSelectOwner.TabStop = false;
            // 
            // comboBoxOwner
            // 
            this.comboBoxOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOwner.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOwner.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxOwner.FormattingEnabled = true;
            this.comboBoxOwner.Location = new System.Drawing.Point(171, 17);
            this.comboBoxOwner.Name = "comboBoxOwner";
            this.comboBoxOwner.Size = new System.Drawing.Size(294, 19);
            this.comboBoxOwner.TabIndex = 78;
            // 
            // labelOwner
            // 
            this.labelOwner.AutoSize = true;
            this.labelOwner.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOwner.Location = new System.Drawing.Point(20, 21);
            this.labelOwner.Name = "labelOwner";
            this.labelOwner.Size = new System.Drawing.Size(53, 11);
            this.labelOwner.TabIndex = 79;
            this.labelOwner.Text = "Owner:";
            // 
            // userTextBoxComments
            // 
            this.userTextBoxComments.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxComments.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxComments.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxComments.Location = new System.Drawing.Point(171, 237);
            this.userTextBoxComments.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxComments.Multiline = true;
            this.userTextBoxComments.Name = "userTextBoxComments";
            this.userTextBoxComments.Size = new System.Drawing.Size(312, 57);
            this.userTextBoxComments.TabIndex = 77;
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelComments.Location = new System.Drawing.Point(20, 242);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(141, 11);
            this.labelComments.TabIndex = 76;
            this.labelComments.Text = "Details/Comments:";
            // 
            // userTextBoxPrice
            // 
            this.userTextBoxPrice.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxPrice.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxPrice.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxPrice.Location = new System.Drawing.Point(171, 340);
            this.userTextBoxPrice.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxPrice.Name = "userTextBoxPrice";
            this.userTextBoxPrice.Size = new System.Drawing.Size(109, 18);
            this.userTextBoxPrice.TabIndex = 75;
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPrice.Location = new System.Drawing.Point(20, 344);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(53, 11);
            this.labelPrice.TabIndex = 74;
            this.labelPrice.Text = "Price:";
            // 
            // InvoiceRequirements
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(512, 458);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonSave);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "InvoiceRequirements";
            this.Text = "Invoice Requirements";
            this.Load += new System.EventHandler(this.InvoiceRequirements_Load);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewContracts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectOwner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelNotInvoiceable;
        private System.Windows.Forms.CheckBox checkBoxNotInvoiceable;
        private System.Windows.Forms.ComboBox comboBoxCurrency;
        private System.Windows.Forms.Label labelCurrency;
        public System.Windows.Forms.DataGridView dataGridViewContracts;
        private System.Windows.Forms.Label labelContract;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.ComboBox comboBoxService;
        private System.Windows.Forms.Label labelService;
        public System.Windows.Forms.DataGridView dataGridViewProperties;
        private System.Windows.Forms.Label labelProperty;
        private System.Windows.Forms.PictureBox pictureBoxSelectOwner;
        private System.Windows.Forms.ComboBox comboBoxOwner;
        private System.Windows.Forms.Label labelOwner;
        private UserTextBox userTextBoxComments;
        private System.Windows.Forms.Label labelComments;
        private UserTextBox userTextBoxPrice;
        private System.Windows.Forms.Label labelPrice;
    }
}