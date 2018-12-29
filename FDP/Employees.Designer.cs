namespace FDP
{
    partial class Employees
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Employees));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonAddEmployee = new System.Windows.Forms.Button();
            this.buttonEditEmployee = new System.Windows.Forms.Button();
            this.buttonDeleteEmployee = new System.Windows.Forms.Button();
            this.buttonSetEmployeePassword = new System.Windows.Forms.Button();
            this.buttonSetEmployeeRights = new System.Windows.Forms.Button();
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.buttonExit = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelShowAll = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelErrors
            // 
            this.panelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelErrors.Location = new System.Drawing.Point(5, 168);
            this.panelErrors.Size = new System.Drawing.Size(621, 70);
            // 
            // listBoxErrors
            // 
            this.listBoxErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.listBoxErrors.Size = new System.Drawing.Size(615, 39);
            // 
            // buttonAddEmployee
            // 
            this.buttonAddEmployee.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAddEmployee.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAddEmployee.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAddEmployee.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAddEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddEmployee.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddEmployee.ForeColor = System.Drawing.Color.Black;
            this.buttonAddEmployee.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddEmployee.Image")));
            this.buttonAddEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddEmployee.Location = new System.Drawing.Point(3, 3);
            this.buttonAddEmployee.Name = "buttonAddEmployee";
            this.buttonAddEmployee.Size = new System.Drawing.Size(80, 23);
            this.buttonAddEmployee.TabIndex = 16;
            this.buttonAddEmployee.Text = "Add";
            this.buttonAddEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddEmployee.UseVisualStyleBackColor = false;
            this.buttonAddEmployee.Click += new System.EventHandler(this.buttonAddEmployee_Click);
            // 
            // buttonEditEmployee
            // 
            this.buttonEditEmployee.BackColor = System.Drawing.Color.DarkGray;
            this.buttonEditEmployee.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonEditEmployee.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonEditEmployee.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonEditEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditEmployee.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditEmployee.ForeColor = System.Drawing.Color.Black;
            this.buttonEditEmployee.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditEmployee.Image")));
            this.buttonEditEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditEmployee.Location = new System.Drawing.Point(89, 3);
            this.buttonEditEmployee.Name = "buttonEditEmployee";
            this.buttonEditEmployee.Size = new System.Drawing.Size(80, 23);
            this.buttonEditEmployee.TabIndex = 17;
            this.buttonEditEmployee.Text = "Edit";
            this.buttonEditEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEditEmployee.UseVisualStyleBackColor = false;
            this.buttonEditEmployee.Click += new System.EventHandler(this.buttonEditEmployee_Click);
            // 
            // buttonDeleteEmployee
            // 
            this.buttonDeleteEmployee.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDeleteEmployee.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonDeleteEmployee.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonDeleteEmployee.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonDeleteEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteEmployee.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteEmployee.ForeColor = System.Drawing.Color.Black;
            this.buttonDeleteEmployee.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteEmployee.Image")));
            this.buttonDeleteEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteEmployee.Location = new System.Drawing.Point(175, 3);
            this.buttonDeleteEmployee.Name = "buttonDeleteEmployee";
            this.buttonDeleteEmployee.Size = new System.Drawing.Size(80, 23);
            this.buttonDeleteEmployee.TabIndex = 18;
            this.buttonDeleteEmployee.Text = "Delete";
            this.buttonDeleteEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDeleteEmployee.UseVisualStyleBackColor = false;
            this.buttonDeleteEmployee.Click += new System.EventHandler(this.buttonDeleteEmployee_Click);
            // 
            // buttonSetEmployeePassword
            // 
            this.buttonSetEmployeePassword.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSetEmployeePassword.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSetEmployeePassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSetEmployeePassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSetEmployeePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetEmployeePassword.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetEmployeePassword.ForeColor = System.Drawing.Color.Black;
            this.buttonSetEmployeePassword.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetEmployeePassword.Image")));
            this.buttonSetEmployeePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSetEmployeePassword.Location = new System.Drawing.Point(270, 3);
            this.buttonSetEmployeePassword.Name = "buttonSetEmployeePassword";
            this.buttonSetEmployeePassword.Size = new System.Drawing.Size(120, 23);
            this.buttonSetEmployeePassword.TabIndex = 19;
            this.buttonSetEmployeePassword.Text = "Set Password";
            this.buttonSetEmployeePassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSetEmployeePassword.UseVisualStyleBackColor = false;
            this.buttonSetEmployeePassword.Click += new System.EventHandler(this.buttonSetEmployeePassword_Click);
            // 
            // buttonSetEmployeeRights
            // 
            this.buttonSetEmployeeRights.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSetEmployeeRights.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSetEmployeeRights.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSetEmployeeRights.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSetEmployeeRights.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetEmployeeRights.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetEmployeeRights.ForeColor = System.Drawing.Color.Black;
            this.buttonSetEmployeeRights.Image = ((System.Drawing.Image)(resources.GetObject("buttonSetEmployeeRights.Image")));
            this.buttonSetEmployeeRights.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSetEmployeeRights.Location = new System.Drawing.Point(396, 3);
            this.buttonSetEmployeeRights.Name = "buttonSetEmployeeRights";
            this.buttonSetEmployeeRights.Size = new System.Drawing.Size(120, 23);
            this.buttonSetEmployeeRights.TabIndex = 20;
            this.buttonSetEmployeeRights.Text = "Set Rights";
            this.buttonSetEmployeeRights.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSetEmployeeRights.UseVisualStyleBackColor = false;
            this.buttonSetEmployeeRights.Click += new System.EventHandler(this.buttonSetEmployeeRights_Click);
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.AllowUserToAddRows = false;
            this.dataGridViewEmployees.AllowUserToDeleteRows = false;
            this.dataGridViewEmployees.AllowUserToOrderColumns = true;
            this.dataGridViewEmployees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEmployees.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewEmployees.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEmployees.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEmployees.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(3, 29);
            this.dataGridViewEmployees.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEmployees.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmployees.Size = new System.Drawing.Size(625, 209);
            this.dataGridViewEmployees.TabIndex = 21;
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonExit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.Black;
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(547, 3);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 23);
            this.buttonExit.TabIndex = 22;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelFilter,
            this.toolStripStatusLabelShowAll});
            this.statusStrip1.Location = new System.Drawing.Point(5, 216);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(621, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // toolStripStatusLabelFilter
            // 
            this.toolStripStatusLabelFilter.Name = "toolStripStatusLabelFilter";
            this.toolStripStatusLabelFilter.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelShowAll
            // 
            this.toolStripStatusLabelShowAll.IsLink = true;
            this.toolStripStatusLabelShowAll.Name = "toolStripStatusLabelShowAll";
            this.toolStripStatusLabelShowAll.Size = new System.Drawing.Size(47, 17);
            this.toolStripStatusLabelShowAll.Text = "Show All";
            this.toolStripStatusLabelShowAll.VisitedLinkColor = System.Drawing.Color.Blue;
            // 
            // Employees
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(631, 243);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.dataGridViewEmployees);
            this.Controls.Add(this.buttonSetEmployeeRights);
            this.Controls.Add(this.buttonSetEmployeePassword);
            this.Controls.Add(this.buttonDeleteEmployee);
            this.Controls.Add(this.buttonEditEmployee);
            this.Controls.Add(this.buttonAddEmployee);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "Employees";
            this.Text = "Employees";
            this.Load += new System.EventHandler(this.Employees_Load);
            this.Controls.SetChildIndex(this.panelErrors, 0);
            this.Controls.SetChildIndex(this.buttonAddEmployee, 0);
            this.Controls.SetChildIndex(this.buttonEditEmployee, 0);
            this.Controls.SetChildIndex(this.buttonDeleteEmployee, 0);
            this.Controls.SetChildIndex(this.buttonSetEmployeePassword, 0);
            this.Controls.SetChildIndex(this.buttonSetEmployeeRights, 0);
            this.Controls.SetChildIndex(this.dataGridViewEmployees, 0);
            this.Controls.SetChildIndex(this.buttonExit, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddEmployee;
        private System.Windows.Forms.Button buttonEditEmployee;
        private System.Windows.Forms.Button buttonDeleteEmployee;
        private System.Windows.Forms.Button buttonSetEmployeePassword;
        private System.Windows.Forms.Button buttonSetEmployeeRights;
        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFilter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelShowAll;
    }
}