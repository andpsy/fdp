namespace FDP
{
    partial class UserForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            this.panelErrors = new System.Windows.Forms.Panel();
            this.toolStripErrors = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCloseErrors = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelErrors = new System.Windows.Forms.ToolStripLabel();
            this.listBoxErrors = new System.Windows.Forms.ListBox();
            this.panelErrors.SuspendLayout();
            this.toolStripErrors.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelErrors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelErrors.Controls.Add(this.toolStripErrors);
            this.panelErrors.Controls.Add(this.listBoxErrors);
            this.panelErrors.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelErrors.ForeColor = System.Drawing.Color.Red;
            this.panelErrors.Location = new System.Drawing.Point(5, 189);
            this.panelErrors.Name = "panelErrors";
            this.panelErrors.Padding = new System.Windows.Forms.Padding(2);
            this.panelErrors.Size = new System.Drawing.Size(333, 70);
            this.panelErrors.TabIndex = 0;
            this.panelErrors.Visible = false;
            // 
            // toolStripErrors
            // 
            this.toolStripErrors.AllowMerge = false;
            this.toolStripErrors.BackColor = System.Drawing.Color.Transparent;
            this.toolStripErrors.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripErrors.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCloseErrors,
            this.toolStripLabelErrors});
            this.toolStripErrors.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripErrors.Location = new System.Drawing.Point(2, 2);
            this.toolStripErrors.Name = "toolStripErrors";
            this.toolStripErrors.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripErrors.ShowItemToolTips = false;
            this.toolStripErrors.Size = new System.Drawing.Size(327, 25);
            this.toolStripErrors.TabIndex = 1;
            // 
            // toolStripButtonCloseErrors
            // 
            this.toolStripButtonCloseErrors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCloseErrors.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCloseErrors.Image")));
            this.toolStripButtonCloseErrors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCloseErrors.Name = "toolStripButtonCloseErrors";
            this.toolStripButtonCloseErrors.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCloseErrors.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripLabelErrors
            // 
            this.toolStripLabelErrors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabelErrors.ForeColor = System.Drawing.Color.Red;
            this.toolStripLabelErrors.Name = "toolStripLabelErrors";
            this.toolStripLabelErrors.Size = new System.Drawing.Size(0, 22);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.listBoxErrors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxErrors.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxErrors.ForeColor = System.Drawing.Color.Blue;
            this.listBoxErrors.FormattingEnabled = true;
            this.listBoxErrors.Location = new System.Drawing.Point(2, 27);
            this.listBoxErrors.Name = "listBoxErrors";
            this.listBoxErrors.Size = new System.Drawing.Size(327, 39);
            this.listBoxErrors.TabIndex = 0;
            this.listBoxErrors.DoubleClick += new System.EventHandler(this.listBoxErrors_DoubleClick);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(343, 264);
            this.Controls.Add(this.panelErrors);
            this.DoubleBuffered = true;
            this.Name = "UserForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UserForm_FormClosed);
            this.Load += new System.EventHandler(this.UserForm_Load);
            this.Enter += new System.EventHandler(this.UserForm_Enter);
            this.Leave += new System.EventHandler(this.UserForm_Leave);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            this.toolStripErrors.ResumeLayout(false);
            this.toolStripErrors.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ToolStrip toolStripErrors;
        public System.Windows.Forms.ToolStripLabel toolStripLabelErrors;
        public System.Windows.Forms.ToolStripButton toolStripButtonCloseErrors;
        public System.Windows.Forms.Panel panelErrors;
        public System.Windows.Forms.ListBox listBoxErrors;

    }
}
