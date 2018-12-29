namespace FDP
{
    partial class BackupDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupDB));
            this.buttonUpload = new System.Windows.Forms.Button();
            this.panelErrors.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 105);
            this.panelErrors.Size = new System.Drawing.Size(208, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(202, 39);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUpload.AutoSize = true;
            this.buttonUpload.BackColor = System.Drawing.Color.DarkGray;
            this.buttonUpload.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonUpload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonUpload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUpload.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUpload.ForeColor = System.Drawing.Color.Black;
            this.buttonUpload.Image = ((System.Drawing.Image)(resources.GetObject("buttonUpload.Image")));
            this.buttonUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUpload.Location = new System.Drawing.Point(28, 33);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(158, 39);
            this.buttonUpload.TabIndex = 85;
            this.buttonUpload.Text = "Upload to Server";
            this.buttonUpload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUpload.UseVisualStyleBackColor = false;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // BackupDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 180);
            this.Controls.Add(this.buttonUpload);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "BackupDB";
            this.Text = "BackupDB";
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.buttonUpload, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpload;
    }
}