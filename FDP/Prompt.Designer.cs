namespace FDP
{
    partial class Prompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Prompt));
            this.userTextBoxPrompt = new FDP.UserTextBox();
            this.userButtonOk = new FDP.UserButton();
            this.userButtonCancel = new FDP.UserButton();
            this.SuspendLayout();
            // 
            // userTextBoxPrompt
            // 
            this.userTextBoxPrompt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.userTextBoxPrompt.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBoxPrompt.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBoxPrompt.ForeColor = System.Drawing.Color.Black;
            this.userTextBoxPrompt.Location = new System.Drawing.Point(9, 8);
            this.userTextBoxPrompt.Name = "userTextBoxPrompt";
            this.userTextBoxPrompt.Size = new System.Drawing.Size(197, 18);
            this.userTextBoxPrompt.TabIndex = 7;
            this.userTextBoxPrompt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(userTextBoxPrompt_KeyPress);
            // 
            // userButtonOk
            // 
            this.userButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.userButtonOk.BackColor = System.Drawing.Color.DarkGray;
            this.userButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.userButtonOk.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.userButtonOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.userButtonOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.userButtonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userButtonOk.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userButtonOk.Image = ((System.Drawing.Image)(resources.GetObject("userButtonOk.Image")));
            this.userButtonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.userButtonOk.Location = new System.Drawing.Point(9, 32);
            this.userButtonOk.Name = "userButtonOk";
            this.userButtonOk.Size = new System.Drawing.Size(95, 33);
            this.userButtonOk.TabIndex = 8;
            this.userButtonOk.Text = "OK";
            this.userButtonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.userButtonOk.UseVisualStyleBackColor = false;
            this.userButtonOk.Click += new System.EventHandler(this.userButtonOk_Click);
            // 
            // userButtonCancel
            // 
            this.userButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.userButtonCancel.BackColor = System.Drawing.Color.DarkGray;
            this.userButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.userButtonCancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.userButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.userButtonCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.userButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.userButtonCancel.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userButtonCancel.Image = ((System.Drawing.Image)(resources.GetObject("userButtonCancel.Image")));
            this.userButtonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.userButtonCancel.Location = new System.Drawing.Point(111, 32);
            this.userButtonCancel.Name = "userButtonCancel";
            this.userButtonCancel.Size = new System.Drawing.Size(95, 33);
            this.userButtonCancel.TabIndex = 9;
            this.userButtonCancel.Text = "Cancel";
            this.userButtonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.userButtonCancel.UseVisualStyleBackColor = false;
            this.userButtonCancel.Click += new System.EventHandler(this.userButtonCancel_Click);
            // 
            // Prompt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(214, 75);
            this.Controls.Add(this.userTextBoxPrompt);
            this.Controls.Add(this.userButtonCancel);
            this.Controls.Add(this.userButtonOk);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Prompt";
            this.Load += new System.EventHandler(this.Prompt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserButton userButtonOk;
        private UserButton userButtonCancel;
        public UserTextBox userTextBoxPrompt;
    }
}