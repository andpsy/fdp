namespace FDP
{
    partial class BankReceipt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BankReceipt));
            this.labelInvoiceBankAccount = new System.Windows.Forms.Label();
            this.userTextBoxExtractNumber = new FDP.UserTextBox();
            this.labelInvoiceExtractNumber = new System.Windows.Forms.Label();
            this.dateTimePickerExtractDate = new System.Windows.Forms.DateTimePicker();
            this.labelInvoiceExtractDate = new System.Windows.Forms.Label();
            this.labelInvoice = new System.Windows.Forms.Label();
            this.userTextBoxAmount = new FDP.UserTextBox();
            this.labelAmount = new System.Windows.Forms.Label();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxBankAccount = new System.Windows.Forms.ComboBox();
            this.comboBoxOwner = new System.Windows.Forms.ComboBox();
            this.labelOwner = new System.Windows.Forms.Label();
            this.comboBoxProperty = new System.Windows.Forms.ComboBox();
            this.labelProperty = new System.Windows.Forms.Label();
            this.comboBoxInvoice = new System.Windows.Forms.ComboBox();
            this.labelComments = new System.Windows.Forms.Label();
            this.userTextBoxComments = new FDP.UserTextBox();
            this.panelErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 232);
            this.panelErrors.Size = new System.Drawing.Size(270, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(264, 39);
            // 
            // labelInvoiceBankAccount
            // 
            this.labelInvoiceBankAccount.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoiceBankAccount.Location = new System.Drawing.Point(8, 156);
            this.labelInvoiceBankAccount.Name = "labelInvoiceBankAccount";
            this.labelInvoiceBankAccount.Size = new System.Drawing.Size(79, 27);
            this.labelInvoiceBankAccount.TabIndex = 3;
            this.labelInvoiceBankAccount.Text = "Bank account:";
            // 
            // userTextBoxExtractNumber
            // 
            this.userTextBoxExtractNumber.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxExtractNumber.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userTextBoxExtractNumber.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxExtractNumber.Location = new System.Drawing.Point(144, 59);
            this.userTextBoxExtractNumber.Name = "userTextBoxExtractNumber";
            this.userTextBoxExtractNumber.Size = new System.Drawing.Size(128, 18);
            this.userTextBoxExtractNumber.TabIndex = 6;
            // 
            // labelInvoiceExtractNumber
            // 
            this.labelInvoiceExtractNumber.AutoSize = true;
            this.labelInvoiceExtractNumber.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoiceExtractNumber.Location = new System.Drawing.Point(8, 63);
            this.labelInvoiceExtractNumber.Name = "labelInvoiceExtractNumber";
            this.labelInvoiceExtractNumber.Size = new System.Drawing.Size(125, 11);
            this.labelInvoiceExtractNumber.TabIndex = 5;
            this.labelInvoiceExtractNumber.Text = "Extract number:";
            // 
            // dateTimePickerExtractDate
            // 
            this.dateTimePickerExtractDate.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerExtractDate.CalendarTitleBackColor = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerExtractDate.CalendarTitleForeColor = System.Drawing.Color.SaddleBrown;
            this.dateTimePickerExtractDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerExtractDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerExtractDate.Location = new System.Drawing.Point(144, 83);
            this.dateTimePickerExtractDate.Name = "dateTimePickerExtractDate";
            this.dateTimePickerExtractDate.Size = new System.Drawing.Size(128, 18);
            this.dateTimePickerExtractDate.TabIndex = 56;
            // 
            // labelInvoiceExtractDate
            // 
            this.labelInvoiceExtractDate.AutoSize = true;
            this.labelInvoiceExtractDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoiceExtractDate.Location = new System.Drawing.Point(8, 87);
            this.labelInvoiceExtractDate.Name = "labelInvoiceExtractDate";
            this.labelInvoiceExtractDate.Size = new System.Drawing.Size(109, 11);
            this.labelInvoiceExtractDate.TabIndex = 55;
            this.labelInvoiceExtractDate.Text = "Extract date:";
            // 
            // labelInvoice
            // 
            this.labelInvoice.AutoSize = true;
            this.labelInvoice.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoice.Location = new System.Drawing.Point(6, 11);
            this.labelInvoice.Name = "labelInvoice";
            this.labelInvoice.Size = new System.Drawing.Size(69, 11);
            this.labelInvoice.TabIndex = 57;
            this.labelInvoice.Text = "Invoice:";
            // 
            // userTextBoxAmount
            // 
            this.userTextBoxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxAmount.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userTextBoxAmount.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxAmount.Location = new System.Drawing.Point(144, 132);
            this.userTextBoxAmount.Name = "userTextBoxAmount";
            this.userTextBoxAmount.Size = new System.Drawing.Size(128, 18);
            this.userTextBoxAmount.TabIndex = 60;
            this.userTextBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAmount.Location = new System.Drawing.Point(8, 135);
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
            this.buttonPrint.Location = new System.Drawing.Point(93, 268);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(77, 23);
            this.buttonPrint.TabIndex = 70;
            this.buttonPrint.Text = "Print";
            this.buttonPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonPrint.UseVisualStyleBackColor = false;
            this.buttonPrint.Visible = false;
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
            this.buttonSave.Location = new System.Drawing.Point(10, 268);
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
            this.buttonExit.Location = new System.Drawing.Point(193, 268);
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
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.CalendarMonthBackground = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerDate.CalendarTitleBackColor = System.Drawing.Color.WhiteSmoke;
            this.dateTimePickerDate.CalendarTitleForeColor = System.Drawing.Color.SaddleBrown;
            this.dateTimePickerDate.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDate.Location = new System.Drawing.Point(144, 107);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(128, 18);
            this.dateTimePickerDate.TabIndex = 72;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 11);
            this.label1.TabIndex = 71;
            this.label1.Text = "Date:";
            // 
            // comboBoxBankAccount
            // 
            this.comboBoxBankAccount.FormattingEnabled = true;
            this.comboBoxBankAccount.Location = new System.Drawing.Point(93, 156);
            this.comboBoxBankAccount.Name = "comboBoxBankAccount";
            this.comboBoxBankAccount.Size = new System.Drawing.Size(179, 21);
            this.comboBoxBankAccount.TabIndex = 73;
            // 
            // comboBoxOwner
            // 
            this.comboBoxOwner.Enabled = false;
            this.comboBoxOwner.FormattingEnabled = true;
            this.comboBoxOwner.Location = new System.Drawing.Point(93, 32);
            this.comboBoxOwner.Name = "comboBoxOwner";
            this.comboBoxOwner.Size = new System.Drawing.Size(179, 21);
            this.comboBoxOwner.TabIndex = 75;
            // 
            // labelOwner
            // 
            this.labelOwner.AutoSize = true;
            this.labelOwner.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOwner.Location = new System.Drawing.Point(8, 37);
            this.labelOwner.Name = "labelOwner";
            this.labelOwner.Size = new System.Drawing.Size(53, 11);
            this.labelOwner.TabIndex = 74;
            this.labelOwner.Text = "Owner:";
            // 
            // comboBoxProperty
            // 
            this.comboBoxProperty.Enabled = false;
            this.comboBoxProperty.FormattingEnabled = true;
            this.comboBoxProperty.Location = new System.Drawing.Point(93, 182);
            this.comboBoxProperty.Name = "comboBoxProperty";
            this.comboBoxProperty.Size = new System.Drawing.Size(179, 21);
            this.comboBoxProperty.TabIndex = 77;
            this.comboBoxProperty.Visible = false;
            // 
            // labelProperty
            // 
            this.labelProperty.AutoSize = true;
            this.labelProperty.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProperty.Location = new System.Drawing.Point(8, 187);
            this.labelProperty.Name = "labelProperty";
            this.labelProperty.Size = new System.Drawing.Size(77, 11);
            this.labelProperty.TabIndex = 76;
            this.labelProperty.Text = "Property:";
            this.labelProperty.Visible = false;
            // 
            // comboBoxInvoice
            // 
            this.comboBoxInvoice.FormattingEnabled = true;
            this.comboBoxInvoice.Location = new System.Drawing.Point(93, 6);
            this.comboBoxInvoice.Name = "comboBoxInvoice";
            this.comboBoxInvoice.Size = new System.Drawing.Size(179, 21);
            this.comboBoxInvoice.TabIndex = 78;
            this.comboBoxInvoice.SelectedIndexChanged += new System.EventHandler(this.comboBoxInvoice_SelectedIndexChanged);
            // 
            // labelComments
            // 
            this.labelComments.AutoSize = true;
            this.labelComments.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelComments.Location = new System.Drawing.Point(7, 213);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(77, 11);
            this.labelComments.TabIndex = 79;
            this.labelComments.Text = "Comments:";
            // 
            // userTextBoxComments
            // 
            this.userTextBoxComments.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxComments.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userTextBoxComments.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxComments.Location = new System.Drawing.Point(93, 209);
            this.userTextBoxComments.Multiline = true;
            this.userTextBoxComments.Name = "userTextBoxComments";
            this.userTextBoxComments.Size = new System.Drawing.Size(179, 66);
            this.userTextBoxComments.TabIndex = 80;
            // 
            // BankReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 307);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.userTextBoxComments);
            this.Controls.Add(this.comboBoxInvoice);
            this.Controls.Add(this.comboBoxOwner);
            this.Controls.Add(this.labelOwner);
            this.Controls.Add(this.comboBoxBankAccount);
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.userTextBoxAmount);
            this.Controls.Add(this.labelAmount);
            this.Controls.Add(this.labelInvoice);
            this.Controls.Add(this.dateTimePickerExtractDate);
            this.Controls.Add(this.labelInvoiceExtractDate);
            this.Controls.Add(this.userTextBoxExtractNumber);
            this.Controls.Add(this.labelInvoiceExtractNumber);
            this.Controls.Add(this.labelInvoiceBankAccount);
            this.Controls.Add(this.comboBoxProperty);
            this.Controls.Add(this.labelProperty);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "BankReceipt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bank Receipt";
            this.Load += new System.EventHandler(this.BankReceipt_Load);
            this.Controls.SetChildIndex(this.labelProperty, 0);
            this.Controls.SetChildIndex(this.comboBoxProperty, 0);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.labelInvoiceBankAccount, 0);
            this.Controls.SetChildIndex(this.labelInvoiceExtractNumber, 0);
            this.Controls.SetChildIndex(this.userTextBoxExtractNumber, 0);
            this.Controls.SetChildIndex(this.labelInvoiceExtractDate, 0);
            this.Controls.SetChildIndex(this.dateTimePickerExtractDate, 0);
            this.Controls.SetChildIndex(this.labelInvoice, 0);
            this.Controls.SetChildIndex(this.labelAmount, 0);
            this.Controls.SetChildIndex(this.userTextBoxAmount, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.buttonSave, 0);
            this.Controls.SetChildIndex(this.buttonPrint, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dateTimePickerDate, 0);
            this.Controls.SetChildIndex(this.comboBoxBankAccount, 0);
            this.Controls.SetChildIndex(this.labelOwner, 0);
            this.Controls.SetChildIndex(this.comboBoxOwner, 0);
            this.Controls.SetChildIndex(this.comboBoxInvoice, 0);
            this.Controls.SetChildIndex(this.userTextBoxComments, 0);
            this.Controls.SetChildIndex(this.labelComments, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInvoiceBankAccount;
        private UserTextBox userTextBoxExtractNumber;
        private System.Windows.Forms.Label labelInvoiceExtractNumber;
        private System.Windows.Forms.DateTimePicker dateTimePickerExtractDate;
        private System.Windows.Forms.Label labelInvoiceExtractDate;
        private System.Windows.Forms.Label labelInvoice;
        private UserTextBox userTextBoxAmount;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxInvoice;
        private System.Windows.Forms.ComboBox comboBoxProperty;
        private System.Windows.Forms.Label labelProperty;
        private System.Windows.Forms.ComboBox comboBoxOwner;
        private System.Windows.Forms.Label labelOwner;
        private System.Windows.Forms.ComboBox comboBoxBankAccount;
        private System.Windows.Forms.Label labelComments;
        private UserTextBox userTextBoxComments;
    }
}