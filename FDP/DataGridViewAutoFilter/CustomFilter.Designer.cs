namespace DataGridViewAutoFilter
{
    partial class CustomFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomFilter));
            this.labelShowRows = new System.Windows.Forms.Label();
            this.comboBoxCondition1 = new System.Windows.Forms.ComboBox();
            this.groupBoxColumn = new System.Windows.Forms.GroupBox();
            this.comboBoxValues1 = new System.Windows.Forms.ComboBox();
            this.radioButtonAnd = new System.Windows.Forms.RadioButton();
            this.radioButtonOr = new System.Windows.Forms.RadioButton();
            this.comboBoxValues2 = new System.Windows.Forms.ComboBox();
            this.labelTip1 = new System.Windows.Forms.Label();
            this.labelTip2 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.comboBoxCondition2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExit)).BeginInit();
            this.groupBoxColumn.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxExit
            // 
            this.pictureBoxExit.Location = new System.Drawing.Point(359, 17);
            // 
            // labelShowRows
            // 
            this.labelShowRows.AutoSize = true;
            this.labelShowRows.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelShowRows.Location = new System.Drawing.Point(8, 5);
            this.labelShowRows.Name = "labelShowRows";
            this.labelShowRows.Size = new System.Drawing.Size(133, 11);
            this.labelShowRows.TabIndex = 24;
            this.labelShowRows.Text = "Show rows where:";
            // 
            // comboBoxCondition1
            // 
            this.comboBoxCondition1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxCondition1.FormattingEnabled = true;
            this.comboBoxCondition1.Items.AddRange(new object[] {
            "equals",
            "does not equal",
            "is greater than",
            "is greater than or equal to",
            "is less than",
            "is less than or equal to",
            "begins with",
            "does not begin with",
            "ends with",
            "does not end with",
            "contains",
            "does not contain"});
            this.comboBoxCondition1.Location = new System.Drawing.Point(6, 18);
            this.comboBoxCondition1.Name = "comboBoxCondition1";
            this.comboBoxCondition1.Size = new System.Drawing.Size(180, 20);
            this.comboBoxCondition1.TabIndex = 26;
            // 
            // groupBoxColumn
            // 
            this.groupBoxColumn.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxColumn.Controls.Add(this.comboBoxCondition2);
            this.groupBoxColumn.Controls.Add(this.labelTip2);
            this.groupBoxColumn.Controls.Add(this.labelTip1);
            this.groupBoxColumn.Controls.Add(this.comboBoxValues2);
            this.groupBoxColumn.Controls.Add(this.radioButtonOr);
            this.groupBoxColumn.Controls.Add(this.radioButtonAnd);
            this.groupBoxColumn.Controls.Add(this.comboBoxValues1);
            this.groupBoxColumn.Controls.Add(this.comboBoxCondition1);
            this.groupBoxColumn.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxColumn.Location = new System.Drawing.Point(10, 29);
            this.groupBoxColumn.Name = "groupBoxColumn";
            this.groupBoxColumn.Size = new System.Drawing.Size(380, 153);
            this.groupBoxColumn.TabIndex = 27;
            this.groupBoxColumn.TabStop = false;
            this.groupBoxColumn.Text = "ColumnName";
            // 
            // comboBoxValues1
            // 
            this.comboBoxValues1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxValues1.FormattingEnabled = true;
            this.comboBoxValues1.Location = new System.Drawing.Point(192, 18);
            this.comboBoxValues1.Name = "comboBoxValues1";
            this.comboBoxValues1.Size = new System.Drawing.Size(180, 20);
            this.comboBoxValues1.TabIndex = 27;
            // 
            // radioButtonAnd
            // 
            this.radioButtonAnd.AutoSize = true;
            this.radioButtonAnd.Location = new System.Drawing.Point(122, 44);
            this.radioButtonAnd.Name = "radioButtonAnd";
            this.radioButtonAnd.Size = new System.Drawing.Size(47, 16);
            this.radioButtonAnd.TabIndex = 28;
            this.radioButtonAnd.TabStop = true;
            this.radioButtonAnd.Text = "And";
            this.radioButtonAnd.UseVisualStyleBackColor = true;
            // 
            // radioButtonOr
            // 
            this.radioButtonOr.AutoSize = true;
            this.radioButtonOr.Location = new System.Drawing.Point(192, 44);
            this.radioButtonOr.Name = "radioButtonOr";
            this.radioButtonOr.Size = new System.Drawing.Size(39, 16);
            this.radioButtonOr.TabIndex = 29;
            this.radioButtonOr.TabStop = true;
            this.radioButtonOr.Text = "Or";
            this.radioButtonOr.UseVisualStyleBackColor = true;
            // 
            // comboBoxValues2
            // 
            this.comboBoxValues2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxValues2.FormattingEnabled = true;
            this.comboBoxValues2.Location = new System.Drawing.Point(192, 66);
            this.comboBoxValues2.Name = "comboBoxValues2";
            this.comboBoxValues2.Size = new System.Drawing.Size(180, 20);
            this.comboBoxValues2.TabIndex = 31;
            // 
            // labelTip1
            // 
            this.labelTip1.AutoSize = true;
            this.labelTip1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTip1.Location = new System.Drawing.Point(6, 107);
            this.labelTip1.Name = "labelTip1";
            this.labelTip1.Size = new System.Drawing.Size(309, 11);
            this.labelTip1.TabIndex = 32;
            this.labelTip1.Text = "Use ? to representany single character";
            // 
            // labelTip2
            // 
            this.labelTip2.AutoSize = true;
            this.labelTip2.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTip2.Location = new System.Drawing.Point(6, 129);
            this.labelTip2.Name = "labelTip2";
            this.labelTip2.Size = new System.Drawing.Size(341, 11);
            this.labelTip2.TabIndex = 33;
            this.labelTip2.Text = "Use * to representany series of characters";
            // 
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.Color.DarkGray;
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOk.ForeColor = System.Drawing.Color.Black;
            this.buttonOk.Image = ((System.Drawing.Image)(resources.GetObject("buttonOk.Image")));
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(216, 188);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(80, 23);
            this.buttonOk.TabIndex = 29;
            this.buttonOk.Text = "OK";
            this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOk.UseVisualStyleBackColor = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.DarkGray;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.Black;
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(302, 188);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(80, 23);
            this.buttonCancel.TabIndex = 30;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // comboBoxCondition2
            // 
            this.comboBoxCondition2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.comboBoxCondition2.FormattingEnabled = true;
            this.comboBoxCondition2.Items.AddRange(new object[] {
            "equals",
            "does not equal",
            "is greater than",
            "is greater than or equal to",
            "is less than",
            "is less than or equal to",
            "begins with",
            "does not begin with",
            "ends with",
            "does not end with",
            "contains",
            "does not contain"});
            this.comboBoxCondition2.Location = new System.Drawing.Point(6, 66);
            this.comboBoxCondition2.Name = "comboBoxCondition2";
            this.comboBoxCondition2.Size = new System.Drawing.Size(180, 20);
            this.comboBoxCondition2.TabIndex = 34;
            // 
            // CustomFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 221);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxColumn);
            this.Controls.Add(this.labelShowRows);
            this.Name = "CustomFilter";
            this.Text = "CustomFilter";
            this.Load += new System.EventHandler(this.CustomFilter_Load);
            this.Controls.SetChildIndex(this.pictureBoxExit, 0);
            this.Controls.SetChildIndex(this.labelShowRows, 0);
            this.Controls.SetChildIndex(this.groupBoxColumn, 0);
            this.Controls.SetChildIndex(this.buttonOk, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxExit)).EndInit();
            this.groupBoxColumn.ResumeLayout(false);
            this.groupBoxColumn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelShowRows;
        private System.Windows.Forms.ComboBox comboBoxCondition1;
        private System.Windows.Forms.GroupBox groupBoxColumn;
        private System.Windows.Forms.Label labelTip2;
        private System.Windows.Forms.Label labelTip1;
        private System.Windows.Forms.ComboBox comboBoxValues2;
        private System.Windows.Forms.RadioButton radioButtonOr;
        private System.Windows.Forms.RadioButton radioButtonAnd;
        private System.Windows.Forms.ComboBox comboBoxValues1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxCondition2;
    }
}