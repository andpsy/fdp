namespace FDP
{
    partial class CustomPrintDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomPrintDialog));
            this.userTextBoxDocTitle = new FDP.UserTextBox();
            this.labelDocTitle = new System.Windows.Forms.Label();
            this.userTextBoxDocSubtitle = new FDP.UserTextBox();
            this.labelDocSubtitle = new System.Windows.Forms.Label();
            this.userTextBoxDocFooter = new FDP.UserTextBox();
            this.labelDocFooter = new System.Windows.Forms.Label();
            this.userButtonPrint = new FDP.UserButton();
            this.userButtonCancel = new FDP.UserButton();
            this.SuspendLayout();
            // 
            // userTextBoxDocTitle
            // 
            this.userTextBoxDocTitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxDocTitle.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxDocTitle.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxDocTitle.Location = new System.Drawing.Point(138, 17);
            this.userTextBoxDocTitle.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxDocTitle.Name = "userTextBoxDocTitle";
            this.userTextBoxDocTitle.Size = new System.Drawing.Size(297, 18);
            this.userTextBoxDocTitle.TabIndex = 17;
            // 
            // labelDocTitle
            // 
            this.labelDocTitle.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDocTitle.Location = new System.Drawing.Point(10, 12);
            this.labelDocTitle.Name = "labelDocTitle";
            this.labelDocTitle.Size = new System.Drawing.Size(125, 29);
            this.labelDocTitle.TabIndex = 16;
            this.labelDocTitle.Text = "Document title:";
            // 
            // userTextBoxDocSubtitle
            // 
            this.userTextBoxDocSubtitle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxDocSubtitle.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxDocSubtitle.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxDocSubtitle.Location = new System.Drawing.Point(138, 54);
            this.userTextBoxDocSubtitle.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxDocSubtitle.Name = "userTextBoxDocSubtitle";
            this.userTextBoxDocSubtitle.Size = new System.Drawing.Size(297, 18);
            this.userTextBoxDocSubtitle.TabIndex = 19;
            // 
            // labelDocSubtitle
            // 
            this.labelDocSubtitle.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDocSubtitle.Location = new System.Drawing.Point(10, 51);
            this.labelDocSubtitle.Name = "labelDocSubtitle";
            this.labelDocSubtitle.Size = new System.Drawing.Size(125, 25);
            this.labelDocSubtitle.TabIndex = 18;
            this.labelDocSubtitle.Text = "Document subtitle:";
            // 
            // userTextBoxDocFooter
            // 
            this.userTextBoxDocFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxDocFooter.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxDocFooter.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxDocFooter.Location = new System.Drawing.Point(138, 89);
            this.userTextBoxDocFooter.Margin = new System.Windows.Forms.Padding(0);
            this.userTextBoxDocFooter.Name = "userTextBoxDocFooter";
            this.userTextBoxDocFooter.Size = new System.Drawing.Size(297, 18);
            this.userTextBoxDocFooter.TabIndex = 21;
            // 
            // labelDocFooter
            // 
            this.labelDocFooter.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDocFooter.Location = new System.Drawing.Point(10, 86);
            this.labelDocFooter.Name = "labelDocFooter";
            this.labelDocFooter.Size = new System.Drawing.Size(125, 25);
            this.labelDocFooter.TabIndex = 20;
            this.labelDocFooter.Text = "Document footer:";
            // 
            // userButtonPrint
            // 
            this.userButtonPrint.BackColor = System.Drawing.Color.DarkGray;
            this.userButtonPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.userButtonPrint.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.userButtonPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.userButtonPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.userButtonPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userButtonPrint.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userButtonPrint.Image = ((System.Drawing.Image)(resources.GetObject("userButtonPrint.Image")));
            this.userButtonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.userButtonPrint.Location = new System.Drawing.Point(269, 124);
            this.userButtonPrint.Name = "userButtonPrint";
            this.userButtonPrint.Size = new System.Drawing.Size(80, 23);
            this.userButtonPrint.TabIndex = 22;
            this.userButtonPrint.Text = "Print";
            this.userButtonPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.userButtonPrint.UseVisualStyleBackColor = false;
            // 
            // userButtonCancel
            // 
            this.userButtonCancel.BackColor = System.Drawing.Color.DarkGray;
            this.userButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.userButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.userButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.userButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.userButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userButtonCancel.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("userButtonCancel.Image")));
            this.userButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.userButtonCancel.Location = new System.Drawing.Point(355, 124);
            this.userButtonCancel.Name = "userButtonCancel";
            this.userButtonCancel.Size = new System.Drawing.Size(80, 23);
            this.userButtonCancel.TabIndex = 23;
            this.userButtonCancel.Text = "Cancel";
            this.userButtonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.userButtonCancel.UseVisualStyleBackColor = false;
            this.userButtonCancel.Click += new System.EventHandler(this.userButtonCancel_Click);
            // 
            // CustomPrintDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(443, 163);
            this.Controls.Add(this.userButtonCancel);
            this.Controls.Add(this.userButtonPrint);
            this.Controls.Add(this.userTextBoxDocFooter);
            this.Controls.Add(this.labelDocFooter);
            this.Controls.Add(this.userTextBoxDocSubtitle);
            this.Controls.Add(this.labelDocSubtitle);
            this.Controls.Add(this.userTextBoxDocTitle);
            this.Controls.Add(this.labelDocTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomPrintDialog";
            this.Text = "CustomPrintDialog";
            this.Load += new System.EventHandler(this.CustomPrintDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDocTitle;
        private System.Windows.Forms.Label labelDocSubtitle;
        private System.Windows.Forms.Label labelDocFooter;
        private UserButton userButtonPrint;
        private UserButton userButtonCancel;
        public UserTextBox userTextBoxDocTitle;
        public UserTextBox userTextBoxDocSubtitle;
        public UserTextBox userTextBoxDocFooter;
    }
}