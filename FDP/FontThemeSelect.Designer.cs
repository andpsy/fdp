namespace FDP
{
    partial class FontThemeSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontThemeSelect));
            this.buttonSaveFontTheme = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.userTextBox1 = new FDP.UserTextBox();
            this.SuspendLayout();
            // 
            // buttonSaveFontTheme
            // 
            this.buttonSaveFontTheme.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSaveFontTheme.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSaveFontTheme.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSaveFontTheme.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSaveFontTheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveFontTheme.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonSaveFontTheme.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveFontTheme.Image")));
            this.buttonSaveFontTheme.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveFontTheme.Location = new System.Drawing.Point(8, 32);
            this.buttonSaveFontTheme.Name = "buttonSaveFontTheme";
            this.buttonSaveFontTheme.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveFontTheme.TabIndex = 24;
            this.buttonSaveFontTheme.Text = "Save";
            this.buttonSaveFontTheme.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveFontTheme.UseVisualStyleBackColor = false;
            this.buttonSaveFontTheme.Click += new System.EventHandler(this.buttonSaveFontTheme_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(154, 32);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 23;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackColor = System.Drawing.Color.DarkGray;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(191, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "...";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // userTextBox1
            // 
            this.userTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.userTextBox1.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userTextBox1.ForeColor = System.Drawing.Color.Black;
            this.userTextBox1.Location = new System.Drawing.Point(8, 8);
            this.userTextBox1.Name = "userTextBox1";
            this.userTextBox1.ReadOnly = true;
            this.userTextBox1.Size = new System.Drawing.Size(177, 18);
            this.userTextBox1.TabIndex = 26;
            // 
            // FontThemeSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(240, 64);
            this.Controls.Add(this.userTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSaveFontTheme);
            this.Controls.Add(this.buttonExit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "FontThemeSelect";
            this.Text = "FontThemeSelect";
            this.Load += new System.EventHandler(this.FontThemeSelect_Load);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.buttonSaveFontTheme, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.userTextBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveFontTheme;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button button1;
        private UserTextBox userTextBox1;
    }
}