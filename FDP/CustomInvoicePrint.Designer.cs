namespace FDP
{
    partial class CustomInvoicePrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomInvoicePrint));
            this.buttonPDF = new System.Windows.Forms.Button();
            this.checkBoxReceiptWithInvoice = new System.Windows.Forms.CheckBox();
            this.radioButtonOneLanguage = new System.Windows.Forms.RadioButton();
            this.radioButtonBillingual = new System.Windows.Forms.RadioButton();
            this.comboBoxOneLanguage = new System.Windows.Forms.ComboBox();
            this.comboBoxBillingual1 = new System.Windows.Forms.ComboBox();
            this.comboBoxBillingual2 = new System.Windows.Forms.ComboBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.panelErrors.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Size = new System.Drawing.Size(355, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(349, 39);
            // 
            // buttonPDF
            // 
            this.buttonPDF.BackColor = System.Drawing.Color.DarkGray;
            this.buttonPDF.Enabled = false;
            this.buttonPDF.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonPDF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPDF.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPDF.ForeColor = System.Drawing.Color.Black;
            this.buttonPDF.Image = ((System.Drawing.Image)(resources.GetObject("buttonPDF.Image")));
            this.buttonPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPDF.Location = new System.Drawing.Point(8, 106);
            this.buttonPDF.Name = "buttonPDF";
            this.buttonPDF.Size = new System.Drawing.Size(92, 23);
            this.buttonPDF.TabIndex = 73;
            this.buttonPDF.Text = "Print";
            this.buttonPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonPDF.UseVisualStyleBackColor = false;
            // 
            // checkBoxReceiptWithInvoice
            // 
            this.checkBoxReceiptWithInvoice.AutoSize = true;
            this.checkBoxReceiptWithInvoice.Font = new System.Drawing.Font("Lucida Console", 8.25F);
            this.checkBoxReceiptWithInvoice.Location = new System.Drawing.Point(8, 70);
            this.checkBoxReceiptWithInvoice.Name = "checkBoxReceiptWithInvoice";
            this.checkBoxReceiptWithInvoice.Size = new System.Drawing.Size(255, 15);
            this.checkBoxReceiptWithInvoice.TabIndex = 74;
            this.checkBoxReceiptWithInvoice.Text = "Receipt together with the Invoice";
            this.checkBoxReceiptWithInvoice.UseVisualStyleBackColor = true;
            // 
            // radioButtonOneLanguage
            // 
            this.radioButtonOneLanguage.AutoSize = true;
            this.radioButtonOneLanguage.Location = new System.Drawing.Point(8, 8);
            this.radioButtonOneLanguage.Name = "radioButtonOneLanguage";
            this.radioButtonOneLanguage.Size = new System.Drawing.Size(92, 17);
            this.radioButtonOneLanguage.TabIndex = 75;
            this.radioButtonOneLanguage.TabStop = true;
            this.radioButtonOneLanguage.Text = "One language";
            this.radioButtonOneLanguage.UseVisualStyleBackColor = true;
            // 
            // radioButtonBillingual
            // 
            this.radioButtonBillingual.AutoSize = true;
            this.radioButtonBillingual.Location = new System.Drawing.Point(8, 35);
            this.radioButtonBillingual.Name = "radioButtonBillingual";
            this.radioButtonBillingual.Size = new System.Drawing.Size(66, 17);
            this.radioButtonBillingual.TabIndex = 76;
            this.radioButtonBillingual.TabStop = true;
            this.radioButtonBillingual.Text = "Billingual";
            this.radioButtonBillingual.UseVisualStyleBackColor = true;
            // 
            // comboBoxOneLanguage
            // 
            this.comboBoxOneLanguage.FormattingEnabled = true;
            this.comboBoxOneLanguage.Location = new System.Drawing.Point(106, 7);
            this.comboBoxOneLanguage.Name = "comboBoxOneLanguage";
            this.comboBoxOneLanguage.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOneLanguage.TabIndex = 77;
            // 
            // comboBoxBillingual1
            // 
            this.comboBoxBillingual1.FormattingEnabled = true;
            this.comboBoxBillingual1.Location = new System.Drawing.Point(106, 34);
            this.comboBoxBillingual1.Name = "comboBoxBillingual1";
            this.comboBoxBillingual1.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBillingual1.TabIndex = 78;
            // 
            // comboBoxBillingual2
            // 
            this.comboBoxBillingual2.FormattingEnabled = true;
            this.comboBoxBillingual2.Location = new System.Drawing.Point(233, 34);
            this.comboBoxBillingual2.Name = "comboBoxBillingual2";
            this.comboBoxBillingual2.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBillingual2.TabIndex = 79;
            // 
            // buttonExit
            // 
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
            this.buttonExit.Location = new System.Drawing.Point(262, 106);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(92, 23);
            this.buttonExit.TabIndex = 80;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            // 
            // CustomInvoicePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 264);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.comboBoxBillingual2);
            this.Controls.Add(this.comboBoxBillingual1);
            this.Controls.Add(this.comboBoxOneLanguage);
            this.Controls.Add(this.radioButtonBillingual);
            this.Controls.Add(this.radioButtonOneLanguage);
            this.Controls.Add(this.buttonPDF);
            this.Controls.Add(this.checkBoxReceiptWithInvoice);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "CustomInvoicePrint";
            this.Text = "CustomInvoicePrint";
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.checkBoxReceiptWithInvoice, 0);
            this.Controls.SetChildIndex(this.buttonPDF, 0);
            this.Controls.SetChildIndex(this.radioButtonOneLanguage, 0);
            this.Controls.SetChildIndex(this.radioButtonBillingual, 0);
            this.Controls.SetChildIndex(this.comboBoxOneLanguage, 0);
            this.Controls.SetChildIndex(this.comboBoxBillingual1, 0);
            this.Controls.SetChildIndex(this.comboBoxBillingual2, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPDF;
        private System.Windows.Forms.CheckBox checkBoxReceiptWithInvoice;
        private System.Windows.Forms.RadioButton radioButtonOneLanguage;
        private System.Windows.Forms.RadioButton radioButtonBillingual;
        private System.Windows.Forms.ComboBox comboBoxOneLanguage;
        private System.Windows.Forms.ComboBox comboBoxBillingual1;
        private System.Windows.Forms.ComboBox comboBoxBillingual2;
        private System.Windows.Forms.Button buttonExit;
    }
}