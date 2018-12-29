namespace FDP
{
    partial class DataGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGrid));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonMove = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButtonLayout = new System.Windows.Forms.ToolStripSplitButton();
            this.scrollableGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixedGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripActiveLayoutLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButtonExcel = new System.Windows.Forms.ToolStripSplitButton();
            this.allRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSearchBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBoxSearchFor = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonSearchForward = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxSearchType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonComplexSort = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonExit = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStripDataGrid = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelShowAll = new System.Windows.Forms.ToolStripStatusLabel();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStripDataGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelect.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSelect.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelect.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelect.ForeColor = System.Drawing.Color.Black;
            this.buttonSelect.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelect.Image")));
            this.buttonSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSelect.Location = new System.Drawing.Point(296, 264);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(80, 23);
            this.buttonSelect.TabIndex = 36;
            this.buttonSelect.Text = "Select";
            this.buttonSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSelect.UseVisualStyleBackColor = false;
            this.buttonSelect.Visible = false;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
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
            this.buttonExit.Location = new System.Drawing.Point(878, 264);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(80, 23);
            this.buttonExit.TabIndex = 34;
            this.buttonExit.Text = "Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Visible = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.BackColor = System.Drawing.Color.DarkGray;
            this.buttonDelete.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.ForeColor = System.Drawing.Color.Black;
            this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
            this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelete.Location = new System.Drawing.Point(175, 264);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(80, 23);
            this.buttonDelete.TabIndex = 33;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Visible = false;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEdit.BackColor = System.Drawing.Color.DarkGray;
            this.buttonEdit.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonEdit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEdit.ForeColor = System.Drawing.Color.Black;
            this.buttonEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonEdit.Image")));
            this.buttonEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEdit.Location = new System.Drawing.Point(89, 264);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(80, 23);
            this.buttonEdit.TabIndex = 32;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Visible = false;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.BackColor = System.Drawing.Color.DarkGray;
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkOrange;
            this.buttonAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PapayaWhip;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.Black;
            this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(4, 264);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(80, 23);
            this.buttonAdd.TabIndex = 31;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Visible = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(81)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(97)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(81)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(81)))), ((int)(((byte)(80)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.DarkOrange;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.RowHeadersWidth = 55;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.Size = new System.Drawing.Size(963, 326);
            this.dataGridView.TabIndex = 35;
            this.dataGridView.DataSourceChanged += new System.EventHandler(this.dataGridView_DataSourceChanged);
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            this.dataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView_CellValidating);
            this.dataGridView.ColumnDividerDoubleClick += new System.Windows.Forms.DataGridViewColumnDividerDoubleClickEventHandler(this.dataGridView_ColumnDividerDoubleClick);
            this.dataGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_ColumnHeaderMouseClick);
            this.dataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView_DataBindingComplete);
            this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            this.dataGridView.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_RowValidated);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            this.dataGridView.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView_SortCompare);
            this.dataGridView.BindingContextChanged += new System.EventHandler(this.dataGridView_BindingContextChanged);
            this.dataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_KeyDown);
            this.dataGridView.LostFocus += new System.EventHandler(this.dataGridView_LostFocus);
            this.dataGridView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseClick);
            this.dataGridView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDoubleClick);
            this.dataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonMove,
            this.toolStripButtonRefresh,
            this.toolStripSeparator1,
            this.toolStripButtonAdd,
            this.toolStripButtonSelect,
            this.toolStripSeparator2,
            this.toolStripButtonEdit,
            this.toolStripButtonDelete,
            this.toolStripSeparator3,
            this.toolStripSplitButtonLayout,
            this.toolStripActiveLayoutLabel,
            this.toolStripSeparator7,
            this.toolStripButtonPrint,
            this.toolStripSplitButtonExcel,
            this.toolStripSeparator4,
            this.toolStripButtonSearchBack,
            this.toolStripTextBoxSearchFor,
            this.toolStripButtonSearchForward,
            this.toolStripComboBoxSearchType,
            this.toolStripSeparator5,
            this.toolStripButtonComplexSort,
            this.toolStripSeparator6,
            this.toolStripButtonExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(963, 25);
            this.toolStrip1.TabIndex = 38;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonMove
            // 
            this.toolStripButtonMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonMove.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonMove.Image")));
            this.toolStripButtonMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonMove.Name = "toolStripButtonMove";
            this.toolStripButtonMove.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonMove.Click += new System.EventHandler(this.toolStripButtonMove_Click);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAdd
            // 
            this.toolStripButtonAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAdd.Image")));
            this.toolStripButtonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAdd.Name = "toolStripButtonAdd";
            this.toolStripButtonAdd.Size = new System.Drawing.Size(49, 22);
            this.toolStripButtonAdd.Text = "Add";
            this.toolStripButtonAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripButtonSelect
            // 
            this.toolStripButtonSelect.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelect.Image")));
            this.toolStripButtonSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelect.Name = "toolStripButtonSelect";
            this.toolStripButtonSelect.Size = new System.Drawing.Size(58, 22);
            this.toolStripButtonSelect.Text = "Select";
            this.toolStripButtonSelect.Visible = false;
            this.toolStripButtonSelect.Click += new System.EventHandler(this.toolStripButtonSelect_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonEdit
            // 
            this.toolStripButtonEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEdit.Image")));
            this.toolStripButtonEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEdit.Name = "toolStripButtonEdit";
            this.toolStripButtonEdit.Size = new System.Drawing.Size(47, 22);
            this.toolStripButtonEdit.Text = "Edit";
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonDelete.Text = "Delete";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSplitButtonLayout
            // 
            this.toolStripSplitButtonLayout.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripSplitButtonLayout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scrollableGridToolStripMenuItem,
            this.fixedGridToolStripMenuItem,
            this.saveLayoutToolStripMenuItem,
            this.openLayoutToolStripMenuItem});
            this.toolStripSplitButtonLayout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonLayout.Image")));
            this.toolStripSplitButtonLayout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripSplitButtonLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonLayout.Name = "toolStripSplitButtonLayout";
            this.toolStripSplitButtonLayout.Size = new System.Drawing.Size(75, 22);
            this.toolStripSplitButtonLayout.Text = "Layout";
            this.toolStripSplitButtonLayout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // scrollableGridToolStripMenuItem
            // 
            this.scrollableGridToolStripMenuItem.Name = "scrollableGridToolStripMenuItem";
            this.scrollableGridToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.scrollableGridToolStripMenuItem.Text = "Scrollable Grid";
            this.scrollableGridToolStripMenuItem.Click += new System.EventHandler(this.scrollableGridToolStripMenuItem_Click);
            // 
            // fixedGridToolStripMenuItem
            // 
            this.fixedGridToolStripMenuItem.Name = "fixedGridToolStripMenuItem";
            this.fixedGridToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.fixedGridToolStripMenuItem.Text = "Fixed Grid";
            this.fixedGridToolStripMenuItem.Click += new System.EventHandler(this.fixedGridToolStripMenuItem_Click);
            // 
            // saveLayoutToolStripMenuItem
            // 
            this.saveLayoutToolStripMenuItem.Name = "saveLayoutToolStripMenuItem";
            this.saveLayoutToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.saveLayoutToolStripMenuItem.Text = "Save layout";
            this.saveLayoutToolStripMenuItem.Click += new System.EventHandler(this.saveLayoutToolStripMenuItem_Click);
            // 
            // openLayoutToolStripMenuItem
            // 
            this.openLayoutToolStripMenuItem.Name = "openLayoutToolStripMenuItem";
            this.openLayoutToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.openLayoutToolStripMenuItem.Text = "Open layout";
            this.openLayoutToolStripMenuItem.Click += new System.EventHandler(this.openLayoutToolStripMenuItem_Click);
            // 
            // toolStripActiveLayoutLabel
            // 
            this.toolStripActiveLayoutLabel.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripActiveLayoutLabel.ForeColor = System.Drawing.Color.Black;
            this.toolStripActiveLayoutLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripActiveLayoutLabel.Name = "toolStripActiveLayoutLabel";
            this.toolStripActiveLayoutLabel.Size = new System.Drawing.Size(93, 22);
            this.toolStripActiveLayoutLabel.Text = "No active layout";
            this.toolStripActiveLayoutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripActiveLayoutLabel.Visible = false;
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonPrint
            // 
            this.toolStripButtonPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPrint.Image")));
            this.toolStripButtonPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPrint.Name = "toolStripButtonPrint";
            this.toolStripButtonPrint.Size = new System.Drawing.Size(52, 22);
            this.toolStripButtonPrint.Text = "Print";
            this.toolStripButtonPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonPrint.Click += new System.EventHandler(this.toolStripButtonPrint_Click);
            // 
            // toolStripSplitButtonExcel
            // 
            this.toolStripSplitButtonExcel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allRowsToolStripMenuItem,
            this.selectedRowsToolStripMenuItem});
            this.toolStripSplitButtonExcel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonExcel.Image")));
            this.toolStripSplitButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonExcel.Name = "toolStripSplitButtonExcel";
            this.toolStripSplitButtonExcel.Size = new System.Drawing.Size(65, 22);
            this.toolStripSplitButtonExcel.Text = "Excel";
            // 
            // allRowsToolStripMenuItem
            // 
            this.allRowsToolStripMenuItem.Name = "allRowsToolStripMenuItem";
            this.allRowsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.allRowsToolStripMenuItem.Text = "All rows";
            this.allRowsToolStripMenuItem.Click += new System.EventHandler(this.allRowsToolStripMenuItem_Click);
            // 
            // selectedRowsToolStripMenuItem
            // 
            this.selectedRowsToolStripMenuItem.Name = "selectedRowsToolStripMenuItem";
            this.selectedRowsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.selectedRowsToolStripMenuItem.Text = "Selected rows";
            this.selectedRowsToolStripMenuItem.Click += new System.EventHandler(this.selectedRowsToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSearchBack
            // 
            this.toolStripButtonSearchBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSearchBack.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSearchBack.Image")));
            this.toolStripButtonSearchBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearchBack.Name = "toolStripButtonSearchBack";
            this.toolStripButtonSearchBack.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSearchBack.Click += new System.EventHandler(this.toolStripButtonSearchBack_Click);
            // 
            // toolStripTextBoxSearchFor
            // 
            this.toolStripTextBoxSearchFor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripTextBoxSearchFor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBoxSearchFor.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.toolStripTextBoxSearchFor.Name = "toolStripTextBoxSearchFor";
            this.toolStripTextBoxSearchFor.Size = new System.Drawing.Size(150, 25);
            this.toolStripTextBoxSearchFor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxSearchFor_KeyPress);
            // 
            // toolStripButtonSearchForward
            // 
            this.toolStripButtonSearchForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSearchForward.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSearchForward.Image")));
            this.toolStripButtonSearchForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSearchForward.Name = "toolStripButtonSearchForward";
            this.toolStripButtonSearchForward.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSearchForward.Text = "toolStripButton2";
            this.toolStripButtonSearchForward.Click += new System.EventHandler(this.toolStripButtonSearchForward_Click);
            // 
            // toolStripComboBoxSearchType
            // 
            this.toolStripComboBoxSearchType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStripComboBoxSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSearchType.Items.AddRange(new object[] {
            "Exact search",
            "Partial search"});
            this.toolStripComboBoxSearchType.Name = "toolStripComboBoxSearchType";
            this.toolStripComboBoxSearchType.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonComplexSort
            // 
            this.toolStripButtonComplexSort.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonComplexSort.Image")));
            this.toolStripButtonComplexSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonComplexSort.Name = "toolStripButtonComplexSort";
            this.toolStripButtonComplexSort.Size = new System.Drawing.Size(48, 22);
            this.toolStripButtonComplexSort.Text = "Sort";
            this.toolStripButtonComplexSort.Click += new System.EventHandler(this.toolStripButtonComplexSort_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonExit
            // 
            this.toolStripButtonExit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonExit.Image")));
            this.toolStripButtonExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonExit.Name = "toolStripButtonExit";
            this.toolStripButtonExit.Size = new System.Drawing.Size(45, 22);
            this.toolStripButtonExit.Text = "Exit";
            this.toolStripButtonExit.Click += new System.EventHandler(this.toolStripButtonExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView);
            this.panel1.Controls.Add(this.statusStripDataGrid);
            this.panel1.Controls.Add(this.buttonSelect);
            this.panel1.Controls.Add(this.buttonExit);
            this.panel1.Controls.Add(this.buttonDelete);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Controls.Add(this.buttonEdit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 326);
            this.panel1.TabIndex = 39;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // statusStripDataGrid
            // 
            this.statusStripDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelFilter,
            this.toolStripStatusLabelShowAll});
            this.statusStripDataGrid.Location = new System.Drawing.Point(0, 304);
            this.statusStripDataGrid.Name = "statusStripDataGrid";
            this.statusStripDataGrid.Size = new System.Drawing.Size(963, 22);
            this.statusStripDataGrid.TabIndex = 37;
            this.statusStripDataGrid.Visible = false;
            // 
            // toolStripStatusLabelFilter
            // 
            this.toolStripStatusLabelFilter.BackColor = System.Drawing.Color.Moccasin;
            this.toolStripStatusLabelFilter.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabelFilter.Name = "toolStripStatusLabelFilter";
            this.toolStripStatusLabelFilter.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelShowAll
            // 
            this.toolStripStatusLabelShowAll.BackColor = System.Drawing.Color.Moccasin;
            this.toolStripStatusLabelShowAll.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabelShowAll.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.toolStripStatusLabelShowAll.IsLink = true;
            this.toolStripStatusLabelShowAll.Name = "toolStripStatusLabelShowAll";
            this.toolStripStatusLabelShowAll.Size = new System.Drawing.Size(57, 17);
            this.toolStripStatusLabelShowAll.Text = "Show All";
            this.toolStripStatusLabelShowAll.Click += new System.EventHandler(this.toolStripStatusLabelShowAll_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // DataGrid
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "DataGrid";
            this.Size = new System.Drawing.Size(963, 351);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStripDataGrid.ResumeLayout(false);
            this.statusStripDataGrid.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonExit;
        public System.Windows.Forms.Button buttonDelete;
        public System.Windows.Forms.Button buttonEdit;
        public System.Windows.Forms.Button buttonAdd;
        public System.Windows.Forms.DataGridView dataGridView;
        public System.Windows.Forms.Button buttonSelect;
        public System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonMove;
        public System.Windows.Forms.ToolStripButton toolStripButtonAdd;
        public System.Windows.Forms.ToolStripButton toolStripButtonEdit;
        public System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        public System.Windows.Forms.ToolStripButton toolStripButtonSelect;
        public System.Windows.Forms.ToolStripButton toolStripButtonExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStripDataGrid;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelShowAll;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFilter;
        public System.Windows.Forms.ToolStripButton toolStripButtonPrint;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonLayout;
        private System.Windows.Forms.ToolStripMenuItem scrollableGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixedGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearchBack;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSearchFor;
        private System.Windows.Forms.ToolStripButton toolStripButtonSearchForward;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButtonComplexSort;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSearchType;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonExcel;
        private System.Windows.Forms.ToolStripMenuItem allRowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedRowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripActiveLayoutLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        public System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
