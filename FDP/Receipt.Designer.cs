namespace FDP
{
    partial class Receipt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Receipt));
            this.userTextBoxSeries = new FDP.UserTextBox();
            this.labelInvoiceSeries = new System.Windows.Forms.Label();
            this.userTextBoxNumber = new FDP.UserTextBox();
            this.labelInvoiceNumber = new System.Windows.Forms.Label();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.labelInvoiceDate = new System.Windows.Forms.Label();
            this.labelInvoice = new System.Windows.Forms.Label();
            this.userTextBoxInvoice = new FDP.UserTextBox();
            this.userTextBoxAmount = new FDP.UserTextBox();
            this.labelAmount = new System.Windows.Forms.Label();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.comboBoxCurrency = new System.Windows.Forms.ComboBox();
            this.labelCurrency = new System.Windows.Forms.Label();
            this.userTextBoxComments = new FDP.UserTextBox();
            this.labelComments = new System.Windows.Forms.Label();
            this.pictureBoxSelectInvoice = new System.Windows.Forms.PictureBox();
            this.panelErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectInvoice)).BeginInit();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 199);
            this.panelErrors.Size = new System.Drawing.Size(256, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(250, 39);
            // 
            // userTextBoxSeries
            // 
            this.userTextBoxSeries.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxSeries.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userTextBoxSeries.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxSeries.Location = new System.Drawing.Point(98, 39);
            this.userTextBoxSeries.Name = "userTextBoxSeries";
            this.userTextBoxSeries.Size = new System.Drawing.Size(155, 18);
            this.userTextBoxSeries.TabIndex = 4;
            // 
            // labelInvoiceSeries
            // 
            this.labelInvoiceSeries.AutoSize = true;
            this.labelInvoiceSeries.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoiceSeries.Location = new System.Drawing.Point(12, 43);
            this.labelInvoiceSeries.Name = "labelInvoiceSeries";
            this.labelInvoiceSeries.Size = new System.Drawing.Size(61, 11);
            this.labelInvoiceSeries.TabIndex = 3;
            this.labelInvoiceSeries.Text = "Series:";
            // 
            // userTextBoxNumber
            // 
            this.userTextBoxNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxNumber.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userTextBoxNumber.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxNumber.Location = new System.Drawing.Point(98, 63);
            this.userTextBoxNumber.Name = "userTextBoxNumber";
            this.userTextBoxNumber.Size = new System.Drawing.Size(155, 18);
            this.userTextBoxNumber.TabIndex = 6;
            // 
            // labelInvoiceNumber
            // 
            this.labelInvoiceNumber.AutoSize = true;
            this.labelInvoiceNumber.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoiceNumber.Location = new System.Drawing.Point(12, 67);
            this.labelInvoiceNumber.Name = "labelInvoiceNumber";
            this.labelInvoiceNumber.Size = new System.Drawing.Size(61, 11);
            this.labelInvoiceNumber.TabIndex = 5;
            this.labelInvoiceNumber.Text = "Number:";
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerDate.CalendarTitleBackColor = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerDate.CalendarTitleForeColor = System.Drawing.Color.SaddleBrown;
            this.dateTimePickerDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(98, 87);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(109, 18);
            this.dateTimePickerDate.TabIndex = 56;
            // 
            // labelInvoiceDate
            // 
            this.labelInvoiceDate.AutoSize = true;
            this.labelInvoiceDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoiceDate.Location = new System.Drawing.Point(12, 90);
            this.labelInvoiceDate.Name = "labelInvoiceDate";
            this.labelInvoiceDate.Size = new System.Drawing.Size(45, 11);
            this.labelInvoiceDate.TabIndex = 55;
            this.labelInvoiceDate.Text = "Date:";
            // 
            // labelInvoice
            // 
            this.labelInvoice.AutoSize = true;
            this.labelInvoice.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoice.Location = new System.Drawing.Point(12, 11);
            this.labelInvoice.Name = "labelInvoice";
            this.labelInvoice.Size = new System.Drawing.Size(69, 11);
            this.labelInvoice.TabIndex = 57;
            this.labelInvoice.Text = "Invoice:";
            // 
            // userTextBoxInvoice
            // 
            this.userTextBoxInvoice.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxInvoice.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userTextBoxInvoice.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxInvoice.Location = new System.Drawing.Point(98, 8);
            this.userTextBoxInvoice.Name = "userTextBoxInvoice";
            this.userTextBoxInvoice.ReadOnly = true;
            this.userTextBoxInvoice.Size = new System.Drawing.Size(133, 18);
            this.userTextBoxInvoice.TabIndex = 58;
            // 
            // userTextBoxAmount
            // 
            this.userTextBoxAmount.BackColor = System.Drawing.Color.SeaShell;
            this.userTextBoxAmount.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userTextBoxAmount.ForeColor = System.Drawing.Color.DarkOrange;
            this.userTextBoxAmount.Location = new System.Drawing.Point(98, 111);
            this.userTextBoxAmount.Name = "userTextBoxAmount";
            this.userTextBoxAmount.Size = new System.Drawing.Size(155, 18);
            this.userTextBoxAmount.TabIndex = 60;
            this.userTextBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmount.Location = new System.Drawing.Point(12, 115);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(61, 11);
            this.labelAmount.TabIndex = 59;
            this.labelAmount.Text = "Amount:";
            // 
            // buttonPrint
            // 
            this.buttonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPrint.BackColor = System.Drawing.Color.DarkGray;
            this.buttonPrint.Enabled = false;
            this.buttonPrint.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrint.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrint.ForeColor = System.Drawing.Color.Black;
            this.buttonPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrint.Image")));
            this.buttonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPrint.Location = new System.Drawing.Point(93, 235);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(77, 23);
            this.buttonPrint.TabIndex = 70;
            this.buttonPrint.Text = "Print";
            this.buttonPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonPrint.UseVisualStyleBackColor = false;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
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
            this.buttonSave.Location = new System.Drawing.Point(10, 235);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(77, 23);
            this.buttonSave.TabIndex = 69;
            this.buttonSave.Text = "Save";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
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
            this.buttonExit.Location = new System.Drawing.Point(176, 235);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(77, 23);
            this.buttonExit.TabIndex = 68;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // comboBoxCurrency
            // 
            this.comboBoxCurrency.FormattingEnabled = true;
            this.comboBoxCurrency.Location = new System.Drawing.Point(98, 135);
            this.comboBoxCurrency.Name = "comboBoxCurrency";
            this.comboBoxCurrency.Size = new System.Drawing.Size(155, 21);
            this.comboBoxCurrency.TabIndex = 72;
            this.comboBoxCurrency.SelectedIndexChanged += new System.EventHandler(this.comboBoxCurrency_SelectedIndexChanged);
            // 
            // labelCurrency
            // 
            this.labelCurrency.AutoSize = true;
            this.labelCurrency.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCurrency.Location = new System.Drawing.Point(12, 140);
            this.labelCurrency.Name = "labelCurrency";
            this.labelCurrency.Size = new System.Drawing.Size(77, 11);
            this.labelCurrency.TabIndex = 71;
            this.labelCurrency.Text = "Currency:";
            // 
            // userTextBoxComments
            // 
            this.userTextBoxComments.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxComments.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userTextBoxComments.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxComments.Location = new System.Drawing.Point(98, 162);
            this.userTextBoxComments.Multiline = true;
            this.userTextBoxComments.Name = "userTextBoxComments";
            this.userTextBoxComments.Size = new System.Drawing.Size(155, 66);
            this.userTextBoxComments.TabIndex = 74;
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComments.Location = new System.Drawing.Point(12, 166);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(77, 11);
            this.labelComments.TabIndex = 73;
            this.labelComments.Text = "Comments:";
            // 
            // pictureBoxSelectInvoice
            // 
            this.pictureBoxSelectInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxSelectInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxSelectInvoice.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxSelectInvoice.Image")));
            this.pictureBoxSelectInvoice.Location = new System.Drawing.Point(237, 8);
            this.pictureBoxSelectInvoice.Name = "pictureBoxSelectInvoice";
            this.pictureBoxSelectInvoice.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxSelectInvoice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxSelectInvoice.TabIndex = 75;
            this.pictureBoxSelectInvoice.TabStop = false;
            this.pictureBoxSelectInvoice.Click += new System.EventHandler(this.pictureBoxSelectInvoice_Click);
            // 
            // Receipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 274);
            this.Controls.Add(this.pictureBoxSelectInvoice);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.comboBoxCurrency);
            this.Controls.Add(this.labelCurrency);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.userTextBoxAmount);
            this.Controls.Add(this.labelAmount);
            this.Controls.Add(this.userTextBoxInvoice);
            this.Controls.Add(this.labelInvoice);
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.labelInvoiceDate);
            this.Controls.Add(this.userTextBoxNumber);
            this.Controls.Add(this.labelInvoiceNumber);
            this.Controls.Add(this.userTextBoxSeries);
            this.Controls.Add(this.labelInvoiceSeries);
            this.Controls.Add(this.userTextBoxComments);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "Receipt";
            this.Text = "Receipt";
            this.Load += new System.EventHandler(this.Receipt_Load);
            this.Controls.SetChildIndex(this.userTextBoxComments, 0);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.labelInvoiceSeries, 0);
            this.Controls.SetChildIndex(this.userTextBoxSeries, 0);
            this.Controls.SetChildIndex(this.labelInvoiceNumber, 0);
            this.Controls.SetChildIndex(this.userTextBoxNumber, 0);
            this.Controls.SetChildIndex(this.labelInvoiceDate, 0);
            this.Controls.SetChildIndex(this.dateTimePickerDate, 0);
            this.Controls.SetChildIndex(this.labelInvoice, 0);
            this.Controls.SetChildIndex(this.userTextBoxInvoice, 0);
            this.Controls.SetChildIndex(this.labelAmount, 0);
            this.Controls.SetChildIndex(this.userTextBoxAmount, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonPrint, 0);
            this.Controls.SetChildIndex(this.labelCurrency, 0);
            this.Controls.SetChildIndex(this.comboBoxCurrency, 0);
            this.Controls.SetChildIndex(this.labelComments, 0);
            this.Controls.SetChildIndex(this.pictureBoxSelectInvoice, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSelectInvoice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserTextBox userTextBoxSeries;
        private System.Windows.Forms.Label labelInvoiceSeries;
        private UserTextBox userTextBoxNumber;
        private System.Windows.Forms.Label labelInvoiceNumber;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label labelInvoiceDate;
        private System.Windows.Forms.Label labelInvoice;
        private UserTextBox userTextBoxInvoice;
        private UserTextBox userTextBoxAmount;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox comboBoxCurrency;
        private System.Windows.Forms.Label labelCurrency;
        private System.Windows.Forms.Label labelComments;
        private UserTextBox userTextBoxComments;
        private System.Windows.Forms.PictureBox pictureBoxSelectInvoice;
    }
}