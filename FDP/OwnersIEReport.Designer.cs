namespace FDP
{
    partial class OwnersIEReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OwnersIEReport));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBoxSelectOwner = new System.Windows.Forms.PictureBox();
            this.comboBoxOwner = new System.Windows.Forms.ComboBox();
            this.labelOwner = new System.Windows.Forms.Label();
            this.dataGridViewProperties = new System.Windows.Forms.DataGridView();
            this.labelProperty = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comboBoxSource = new System.Windows.Forms.ComboBox();
            this.labelSource = new System.Windows.Forms.Label();
            this.comboBoxCurrency = new System.Windows.Forms.ComboBox();
            this.labelCurrency = new System.Windows.Forms.Label();
            this.dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
            this.labelEndDate = new System.Windows.Forms.Label();
            this.buttonReport = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.checkBoxPerProperty = new System.Windows.Forms.CheckBox();
            this.labelPerProperty = new System.Windows.Forms.Label();
            this.labelUseStartDate = new System.Windows.Forms.Label();
            this.checkBoxUseStartDate = new System.Windows.Forms.CheckBox();
            this.labelUseEndDate = new System.Windows.Forms.Label();
            this.checkBoxUseEndDate = new System.Windows.Forms.CheckBox();
            this.labelUseProperty = new System.Windows.Forms.Label();
            this.checkBoxUseProperty = new System.Windows.Forms.CheckBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectOwner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 330);
            this.panelErrors.Size = new System.Drawing.Size(915, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(909, 39);
            // 
            // pictureBoxSelectOwner
            // 
            this.pictureBoxSelectOwner.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSelectOwner.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSelectOwner.Image")));
            this.pictureBoxSelectOwner.Location = new System.Drawing.Point(555, 9);
            this.pictureBoxSelectOwner.Name = "pictureBoxSelectOwner";
            this.pictureBoxSelectOwner.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxSelectOwner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxSelectOwner.TabIndex = 55;
            this.pictureBoxSelectOwner.TabStop = false;
            // 
            // comboBoxOwner
            // 
            this.comboBoxOwner.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOwner.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxOwner.FormattingEnabled = true;
            this.comboBoxOwner.Location = new System.Drawing.Point(112, 7);
            this.comboBoxOwner.Name = "comboBoxOwner";
            this.comboBoxOwner.Size = new System.Drawing.Size(440, 19);
            this.comboBoxOwner.TabIndex = 53;
            this.comboBoxOwner.SelectedIndexChanged += new System.EventHandler(this.comboBoxOwner_SelectedIndexChanged);
            // 
            // labelOwner
            // 
            this.labelOwner.AutoSize = true;
            this.labelOwner.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOwner.Location = new System.Drawing.Point(12, 11);
            this.labelOwner.Name = "labelOwner";
            this.labelOwner.Size = new System.Drawing.Size(53, 11);
            this.labelOwner.TabIndex = 54;
            this.labelOwner.Text = "Owner:";
            // 
            // dataGridViewProperties
            // 
            this.dataGridViewProperties.AllowUserToAddRows = false;
            this.dataGridViewProperties.AllowUserToDeleteRows = false;
            this.dataGridViewProperties.AllowUserToOrderColumns = true;
            this.dataGridViewProperties.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProperties.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewProperties.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProperties.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewProperties.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewProperties.Location = new System.Drawing.Point(112, 32);
            this.dataGridViewProperties.Name = "dataGridViewProperties";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewProperties.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewProperties.RowHeadersVisible = false;
            this.dataGridViewProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProperties.ShowCellErrors = false;
            this.dataGridViewProperties.ShowEditingIcon = false;
            this.dataGridViewProperties.ShowRowErrors = false;
            this.dataGridViewProperties.Size = new System.Drawing.Size(463, 62);
            this.dataGridViewProperties.TabIndex = 65;
            // 
            // labelProperty
            // 
            this.labelProperty.AutoSize = true;
            this.labelProperty.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelProperty.Location = new System.Drawing.Point(12, 39);
            this.labelProperty.Name = "labelProperty";
            this.labelProperty.Size = new System.Drawing.Size(93, 11);
            this.labelProperty.TabIndex = 64;
            this.labelProperty.Text = "Properties:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "-- ALL --",
            "EXPENSE",
            "INCOME"});
            this.comboBoxType.Location = new System.Drawing.Point(682, 32);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(109, 19);
            this.comboBoxType.TabIndex = 74;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelType.Location = new System.Drawing.Point(582, 36);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(45, 11);
            this.labelType.TabIndex = 75;
            this.labelType.Text = "Type:";
            // 
            // comboBoxSource
            // 
            this.comboBoxSource.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSource.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxSource.FormattingEnabled = true;
            this.comboBoxSource.Items.AddRange(new object[] {
            "-- ALL --",
            "CASH",
            "BANK"});
            this.comboBoxSource.Location = new System.Drawing.Point(682, 7);
            this.comboBoxSource.Name = "comboBoxSource";
            this.comboBoxSource.Size = new System.Drawing.Size(109, 19);
            this.comboBoxSource.TabIndex = 76;
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSource.Location = new System.Drawing.Point(582, 11);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(61, 11);
            this.labelSource.TabIndex = 77;
            this.labelSource.Text = "Source:";
            // 
            // comboBoxCurrency
            // 
            this.comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurrency.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxCurrency.FormattingEnabled = true;
            this.comboBoxCurrency.Location = new System.Drawing.Point(682, 57);
            this.comboBoxCurrency.Name = "comboBoxCurrency";
            this.comboBoxCurrency.Size = new System.Drawing.Size(109, 19);
            this.comboBoxCurrency.TabIndex = 78;
            // 
            // labelCurrency
            // 
            this.labelCurrency.AutoSize = true;
            this.labelCurrency.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelCurrency.Location = new System.Drawing.Point(582, 61);
            this.labelCurrency.Name = "labelCurrency";
            this.labelCurrency.Size = new System.Drawing.Size(77, 11);
            this.labelCurrency.TabIndex = 79;
            this.labelCurrency.Text = "Currency:";
            // 
            // dateTimePickerStartDate
            // 
            this.dateTimePickerStartDate.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerStartDate.CalendarTitleBackColor = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerStartDate.CalendarTitleForeColor = System.Drawing.Color.SaddleBrown;
            this.dateTimePickerStartDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartDate.Location = new System.Drawing.Point(111, 125);
            this.dateTimePickerStartDate.Name = "dateTimePickerStartDate";
            this.dateTimePickerStartDate.Size = new System.Drawing.Size(109, 18);
            this.dateTimePickerStartDate.TabIndex = 80;
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStartDate.Location = new System.Drawing.Point(11, 129);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(93, 11);
            this.labelStartDate.TabIndex = 81;
            this.labelStartDate.Text = "Start Date:";
            // 
            // dateTimePickerEndDate
            // 
            this.dateTimePickerEndDate.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerEndDate.CalendarTitleBackColor = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerEndDate.CalendarTitleForeColor = System.Drawing.Color.SaddleBrown;
            this.dateTimePickerEndDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndDate.Location = new System.Drawing.Point(533, 125);
            this.dateTimePickerEndDate.Name = "dateTimePickerEndDate";
            this.dateTimePickerEndDate.Size = new System.Drawing.Size(109, 18);
            this.dateTimePickerEndDate.TabIndex = 82;
            // 
            // labelEndDate
            // 
            this.labelEndDate.AutoSize = true;
            this.labelEndDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEndDate.Location = new System.Drawing.Point(433, 129);
            this.labelEndDate.Name = "labelEndDate";
            this.labelEndDate.Size = new System.Drawing.Size(77, 11);
            this.labelEndDate.TabIndex = 83;
            this.labelEndDate.Text = "End Date:";
            // 
            // buttonReport
            // 
            this.buttonReport.AutoSize = true;
            this.buttonReport.BackColor = System.Drawing.Color.DarkGray;
            this.buttonReport.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonReport.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReport.ForeColor = System.Drawing.Color.Black;
            this.buttonReport.Image = ((System.Drawing.Image)(resources.GetObject("buttonReport.Image")));
            this.buttonReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonReport.Location = new System.Drawing.Point(808, 7);
            this.buttonReport.Name = "buttonReport";
            this.buttonReport.Padding = new System.Windows.Forms.Padding(3);
            this.buttonReport.Size = new System.Drawing.Size(80, 28);
            this.buttonReport.TabIndex = 84;
            this.buttonReport.Text = "Report";
            this.buttonReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonReport.UseVisualStyleBackColor = true;
            this.buttonReport.Click += new System.EventHandler(this.buttonReport_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.AutoSize = true;
            this.buttonExit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.Black;
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(808, 41);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Padding = new System.Windows.Forms.Padding(3);
            this.buttonExit.Size = new System.Drawing.Size(80, 28);
            this.buttonExit.TabIndex = 85;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // checkBoxPerProperty
            // 
            this.checkBoxPerProperty.AutoSize = true;
            this.checkBoxPerProperty.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxPerProperty.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.checkBoxPerProperty.Location = new System.Drawing.Point(562, 100);
            this.checkBoxPerProperty.Name = "checkBoxPerProperty";
            this.checkBoxPerProperty.Size = new System.Drawing.Size(15, 14);
            this.checkBoxPerProperty.TabIndex = 86;
            this.checkBoxPerProperty.UseVisualStyleBackColor = false;
            // 
            // labelPerProperty
            // 
            this.labelPerProperty.AutoSize = true;
            this.labelPerProperty.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPerProperty.Location = new System.Drawing.Point(407, 102);
            this.labelPerProperty.Name = "labelPerProperty";
            this.labelPerProperty.Size = new System.Drawing.Size(149, 11);
            this.labelPerProperty.TabIndex = 87;
            this.labelPerProperty.Text = "Group by property:";
            // 
            // labelUseStartDate
            // 
            this.labelUseStartDate.AutoSize = true;
            this.labelUseStartDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelUseStartDate.Location = new System.Drawing.Point(226, 129);
            this.labelUseStartDate.Name = "labelUseStartDate";
            this.labelUseStartDate.Size = new System.Drawing.Size(125, 11);
            this.labelUseStartDate.TabIndex = 89;
            this.labelUseStartDate.Text = "Use start date:";
            // 
            // checkBoxUseStartDate
            // 
            this.checkBoxUseStartDate.AutoSize = true;
            this.checkBoxUseStartDate.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxUseStartDate.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.checkBoxUseStartDate.Location = new System.Drawing.Point(353, 127);
            this.checkBoxUseStartDate.Name = "checkBoxUseStartDate";
            this.checkBoxUseStartDate.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUseStartDate.TabIndex = 88;
            this.checkBoxUseStartDate.UseVisualStyleBackColor = false;
            // 
            // labelUseEndDate
            // 
            this.labelUseEndDate.AutoSize = true;
            this.labelUseEndDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelUseEndDate.Location = new System.Drawing.Point(648, 129);
            this.labelUseEndDate.Name = "labelUseEndDate";
            this.labelUseEndDate.Size = new System.Drawing.Size(109, 11);
            this.labelUseEndDate.TabIndex = 90;
            this.labelUseEndDate.Text = "Use end date:";
            // 
            // checkBoxUseEndDate
            // 
            this.checkBoxUseEndDate.AutoSize = true;
            this.checkBoxUseEndDate.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxUseEndDate.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.checkBoxUseEndDate.Location = new System.Drawing.Point(775, 127);
            this.checkBoxUseEndDate.Name = "checkBoxUseEndDate";
            this.checkBoxUseEndDate.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUseEndDate.TabIndex = 91;
            this.checkBoxUseEndDate.UseVisualStyleBackColor = false;
            // 
            // labelUseProperty
            // 
            this.labelUseProperty.AutoSize = true;
            this.labelUseProperty.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelUseProperty.Location = new System.Drawing.Point(115, 102);
            this.labelUseProperty.Name = "labelUseProperty";
            this.labelUseProperty.Size = new System.Drawing.Size(109, 11);
            this.labelUseProperty.TabIndex = 93;
            this.labelUseProperty.Text = "Use property:";
            // 
            // checkBoxUseProperty
            // 
            this.checkBoxUseProperty.AutoSize = true;
            this.checkBoxUseProperty.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxUseProperty.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.checkBoxUseProperty.Location = new System.Drawing.Point(232, 100);
            this.checkBoxUseProperty.Name = "checkBoxUseProperty";
            this.checkBoxUseProperty.Size = new System.Drawing.Size(15, 14);
            this.checkBoxUseProperty.TabIndex = 92;
            this.checkBoxUseProperty.UseVisualStyleBackColor = false;
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Items.AddRange(new object[] {
            "Real",
            "Predicted"});
            this.comboBoxStatus.Location = new System.Drawing.Point(682, 82);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(109, 19);
            this.comboBoxStatus.TabIndex = 94;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStatus.Location = new System.Drawing.Point(582, 86);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(61, 11);
            this.labelStatus.TabIndex = 95;
            this.labelStatus.Text = "Status:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Linen;
            this.splitContainer1.Panel1.Controls.Add(this.labelOwner);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxOwner);
            this.splitContainer1.Panel1.Controls.Add(this.buttonExit);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxStatus);
            this.splitContainer1.Panel1.Controls.Add(this.buttonReport);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxSelectOwner);
            this.splitContainer1.Panel1.Controls.Add(this.labelStatus);
            this.splitContainer1.Panel1.Controls.Add(this.labelProperty);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxUseEndDate);
            this.splitContainer1.Panel1.Controls.Add(this.labelUseProperty);
            this.splitContainer1.Panel1.Controls.Add(this.labelUseEndDate);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewProperties);
            this.splitContainer1.Panel1.Controls.Add(this.labelUseStartDate);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxUseProperty);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxUseStartDate);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxPerProperty);
            this.splitContainer1.Panel1.Controls.Add(this.labelPerProperty);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxSource);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePickerEndDate);
            this.splitContainer1.Panel1.Controls.Add(this.labelType);
            this.splitContainer1.Panel1.Controls.Add(this.labelEndDate);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxType);
            this.splitContainer1.Panel1.Controls.Add(this.dateTimePickerStartDate);
            this.splitContainer1.Panel1.Controls.Add(this.labelSource);
            this.splitContainer1.Panel1.Controls.Add(this.labelStartDate);
            this.splitContainer1.Panel1.Controls.Add(this.labelCurrency);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxCurrency);
            this.splitContainer1.Size = new System.Drawing.Size(915, 325);
            this.splitContainer1.SplitterDistance = 157;
            this.splitContainer1.TabIndex = 96;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // OwnersIEReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 405);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "OwnersIEReport";
            this.Text = "OwnersIEReport";
            this.Load += new System.EventHandler(this.OwnersIEReport_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectOwner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProperties)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxSelectOwner;
        private System.Windows.Forms.ComboBox comboBoxOwner;
        private System.Windows.Forms.Label labelOwner;
        public System.Windows.Forms.DataGridView dataGridViewProperties;
        private System.Windows.Forms.Label labelProperty;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboBoxSource;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.ComboBox comboBoxCurrency;
        private System.Windows.Forms.Label labelCurrency;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDate;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDate;
        private System.Windows.Forms.Label labelEndDate;
        private System.Windows.Forms.Button buttonReport;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.CheckBox checkBoxPerProperty;
        private System.Windows.Forms.Label labelPerProperty;
        private System.Windows.Forms.Label labelUseStartDate;
        private System.Windows.Forms.CheckBox checkBoxUseStartDate;
        private System.Windows.Forms.Label labelUseEndDate;
        private System.Windows.Forms.CheckBox checkBoxUseEndDate;
        private System.Windows.Forms.Label labelUseProperty;
        private System.Windows.Forms.CheckBox checkBoxUseProperty;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}