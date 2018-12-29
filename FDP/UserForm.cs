using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace FDP
{
    public partial class UserForm : System.Windows.Forms.Form
    {



        #region -- VARIABLES --
        private ArrayList EnabledOwnerObjects = new ArrayList(){
            "checkBoxRememberName",
            "checkBoxAutoLogin",
            "buttonSaveLogin",
            "userTextBoxOldUserName",
            "userTextBoxOldPassword",
            "userTextBoxNewUserName",
            "userTextBoxNewPassword",
            "userTextBoxConfirmNewPassword",
            "panelErrors",
            "listBoxErrors",
            "toolStripButtonCloseErrors",
            "toolStripErrors",
            "toolStripLabelErrors"
        };

        private ArrayList EnabledOwnerForms = new ArrayList(){
            "ComplexSort",
            "CustomFilter",
            "CustomPrintDialog",
            "DataGridLoadLayout",
            "DataGridSaveLayout",
            "OwnersIEReport"
        };

        public string[] ClickedObject = new string[2];

        public int EditMode  // 1 = ADD, 2 = EDIT
        {
            get;
            set;
        }

        public bool SavedOrCancelled
        {
            get;
            set;
        }

        public UserForm Launcher
        {
            get;
            set;
        }

        public UserForm ChildLaunched
        {
            get;
            set;
        }

        public FormWindowState Maximized
        {
            get;
            set;
        }

        public bool FormHasDataChanges
        {
            get;
            set;
        }

        public bool CheckDataOnClosing
        {
            get;
            set;
        }

        public bool ConfirmSaveAfterWarnings
        {
            get;
            set;
        }

        //public Thread oThread;

        //public Dictionary<string, string> ErrorList = new Dictionary<string, string>();
        public List<KeyValuePair<string, string>> ErrorList = new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, string>> WarningList = new List<KeyValuePair<string, string>>();

        #endregion

        public UserForm()
        {
            InitializeComponent();

            //this.TopLevel = false;
            //this.Show();
        }

        private NativeForm ParentWindow;
        
        [System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc( ref m);
        }
        
        
        protected override void OnVisibleChanged(System.EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Parent == null)
                return;
            ParentWindow = new NativeForm(this.ParentForm, this);
        }

        /*
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            foreach (Control child in this.Controls)
            {
                if (child is Form)
                {
                    Form f = (Form)child;
                    if (f.WindowState == FormWindowState.Maximized)
                        NativeMethods.SetWindowPos(f.Handle, (IntPtr)NativeMethods.HWND_NOTOPMOST, 0, 0, this.Width, this.Height - SystemInformation.CaptionHeight, Convert.ToUInt32(NativeMethods.SWP_NOZORDER | NativeMethods.SWP_NOMOVE));
                }
            }
        }
        */

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public virtual void UserForm_Load(object sender, EventArgs e)
        {
            this.ConfirmSaveAfterWarnings = false;
            if (this.Modal && (this.Parent != null || this.ParentForm != null || this.Owner != null)) this.MinimizeBox = false;
            try
            {
                //if (this.Owner != null && Convert.ToInt32(this.Owner.GetType().GetProperty("EditMode").GetValue(this.Owner, null)) > 0)
                if ((this.Launcher != null && Convert.ToInt32(this.Launcher.GetType().GetProperty("EditMode").GetValue(this.Launcher, null)) > 0) || this.EditMode > 0)
                {
                    EnableDisableMainMenuButtons(false);
                    EnableDisableToolBarButtons(false);
                }
            }
            catch { }
            ////this.Dock = DockStyle.Fill;
            CommonFunctions.SetDateFormat(this);
            CommonFunctions.SetFont(this);
            Language.LoadLabels(this);
            this.WindowState = this.Maximized;
            this.SizeChanged += new System.EventHandler(this.UserForm_SizeChanged);
            AddFormToStatusBar();
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            //this.ControlBox = true;
            //this.TransparencyKey = new Color();
            //this.Opacity = 100;
            try
            {
                ((DataGrid)this.Controls["dataGrid1"]).LoadDefaultLayout(this.Name);
            }
            catch { }
            //this.Visible = true;
            AddContextMenus(this);

            // --- DISABLE CONTROLLS FOR OWNER LOGIN ---
            DisableControllsForOwnerLogin(this);

            try
            {
                SplashScreen.SplashScreen.CloseForm();
            }
            catch { }
        }

        private void DisableControllsForOwnerLogin(Control control)
        {
            if (SettingsClass.LoginOwnerId <= 0 ||
                this.Name.ToLower().IndexOf("select") != -1 ||
                EnabledOwnerForms.IndexOf(this.Name) != -1)
                return;

            foreach (Control ctrl in control.Controls)
            {
                ctrl.Enabled = !(((ctrl is Button && ctrl.Name.ToLower().IndexOf("exit") == -1) || ctrl is UserTextBox || ctrl is TextBox || ctrl is UserButton || ctrl is ComboBox || ctrl is CheckBox || ctrl is ToolStrip || ctrl is PictureBox || ctrl is DateTimePicker || ctrl is ListBox) && EnabledOwnerObjects.IndexOf(ctrl.Name) == -1 );
                DisableControllsForOwnerLogin(ctrl);
            }
        }

        public virtual void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ChildLaunched != null)
            {
                MessageBox.Show(Language.GetMessageBoxText("formHasOpenedChildren", "The form has openned child form(s) and can not be closed before all children are closed!"), "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Cancel = true;
                return;
            }

            try
            {
                if (this.Launcher != null && ((UserForm)this.Launcher).EditMode > 0)
                {
                    dynamic cs = this.Launcher;
                    switch ((int)cs.EditMode)
                    {
                        case 1: // ADD
                            cs.SaveAddRecord();
                            break;
                        case 2: // EDIT
                            cs.SaveEditRecord();
                            break;
                        case 3: // DELETE
                            break;
                        case 4: // ADD ADENDUM
                            cs.SaveAdendumRecord();
                            break;
                    }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                e.Cancel = true;
                return;
            }



            RemoveFormFromStatusBar();
            // TO DO: To check if there are modifications  on the data
            FormHasDataChanges = false;
            if (CheckDataOnClosing & SavedOrCancelled == false)
            {
                try
                {
                    if (this is UserForm)
                    {
                        switch (this.Name)
                        {
                            case "Contracts":
                                if ((((Contracts)this).ContractServices.GetChanges() != null && ((Contracts)this).ContractServices.GetChanges().Rows.Count > 0) || (((Contracts)this).ServicesAdditionalCosts.GetChanges() != null && ((Contracts)this).ServicesAdditionalCosts.GetChanges().Rows.Count > 0))
                                {
                                    FormHasDataChanges = true;
                                }
                                break;
                            case "RentContracts":
                                DataTable tenants = (DataTable)((BindingSource)((RentContracts)this).dataGridViewTenants.DataSource).DataSource;
                                DataTable properties = (DataTable)((RentContracts)this).dataGridViewProperties.DataSource;
                                if ((tenants.GetChanges() != null && tenants.GetChanges().Rows.Count > 0) || (properties.GetChanges() != null && properties.GetChanges().Rows.Count > 0))
                                {
                                    FormHasDataChanges = true;
                                }
                                break;
                        }
                    }
                }
                catch { }
                try
                {
                    dynamic frm = this;
                    frm.GenerateMySqlParameters();
                    foreach (DataColumn dc in frm.NewDR.Table.Columns)
                    {
                        if (dc.ColumnName.ToLower() != "deleted" && dc.ColumnName.ToLower() != "id")
                        {
                            switch (dc.DataType.ToString())
                            //if (dc.DataType.ToString() == "System.DateTime")
                            {
                                case "System.DateTime":
                                    try
                                    {
                                        if (Convert.ToDateTime(frm.NewDR[dc.ColumnName]).Date.ToString() != Convert.ToDateTime(frm.InitialDR[dc.ColumnName]).Date.ToString())
                                        {
                                            FormHasDataChanges = true;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            if ((frm.NewDR[dc.ColumnName] == DBNull.Value && frm.InitialDR[dc.ColumnName] != DBNull.Value) || (frm.NewDR[dc.ColumnName] != DBNull.Value && frm.InitialDR[dc.ColumnName] == DBNull.Value))
                                            {
                                                FormHasDataChanges = true;
                                                break;
                                            }
                                        }
                                        catch { }
                                    }
                                    break;
                                case "System.Boolean":
                                    try
                                    {
                                        DataTable services = new DataAccess(CommandType.StoredProcedure, "SERVICESsp_list").ExecuteSelectQuery().Tables[0];
                                        ArrayList service_names = new ArrayList(services.Rows.Count);
                                        foreach (DataRow dr in services.Rows) service_names.Add(dr["name"].ToString().ToLower());
                                        if (frm.NewDR[dc.ColumnName].ToString().ToLower() != frm.InitialDR[dc.ColumnName].ToString().ToLower() && (frm.NewDR[dc.ColumnName].ToString().ToLower() == "true" || frm.InitialDR[dc.ColumnName].ToString().ToLower() == "true") &&
                                            (this.Name != "Contracts" || (this.Name == "Contracts" && service_names.IndexOf(dc.ColumnName.ToLower()) < 0))
                                            )
                                        {
                                            FormHasDataChanges = true;
                                            break;
                                        }
                                    }
                                    catch { }
                                    break;
                                default:
                                    try
                                    {
                                        if (frm.NewDR[dc.ColumnName].ToString() != frm.InitialDR[dc.ColumnName].ToString())
                                        {
                                            FormHasDataChanges = true;
                                            break;
                                        }
                                    }
                                    catch { }
                                    break;
                            }
                        }
                    }
                    if (CheckDataOnClosing && FormHasDataChanges)
                    {
                        DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmExitWithDataChanges", "There are unsaved modifications on the form? Exit without saving?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (ans != DialogResult.Yes)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }

                }
                catch (Exception exp)
                {
                    LogWriter.Log(exp, SettingsClass.ErrorLogFile);
                }
            }
        }

        public virtual void UserForm_SizeChanged(object sender, EventArgs e)
        {
            /*
            if (this.WindowState != FormWindowState.Maximized)
            {
                AddFormToStatusBar();
            }
            else
            {
                RemoveFormFromStatusBar();
            }
            */
            this.Visible = !(this.WindowState == FormWindowState.Minimized);
        }
        public void AddFormToStatusBar()
        {
            try
            {
                //if (this.ParentForm != null)
                //{
                //    ToolStripButton new_form = new ToolStripButton(this.Text, this.Icon.ToBitmap(), new EventHandler(FormSelect_Click));
                //    new_form.Name = String.Format("{0}Form", this.Name);
                //    ((main)this.ParentForm).toolStripOpenForms.Items.Add(new_form);
                //    //if(((main)this.ParentForm).toolStripOpenForms.Items.Count > 0)
                //    //    ((main)this.ParentForm).toolStripOpenForms.Visible = true;
                //    this.Visible = !(this.WindowState == FormWindowState.Minimized);
                //}
                //else if (this.Owner != null)
                //{
                //    ToolStripButton new_form = new ToolStripButton(this.Text, this.Icon.ToBitmap(), new EventHandler(FormSelect_Click));
                //    new_form.Name = String.Format("{0}Form", this.Name);
                //    ((main)this.Owner).toolStripOpenForms.Items.Add(new_form);
                //    this.Visible = !(this.WindowState == FormWindowState.Minimized);
                //}
                main m = FindMainForm();
                if (m != null)
                {
                    ToolStripButton new_form = new ToolStripButton(this.Text, this.Icon.ToBitmap(), new EventHandler(FormSelect_Click));
                    new_form.Name = String.Format("{0}Form", this.Name);
                    m.toolStripOpenForms.Items.Add(new_form);
                    this.Visible = !(this.WindowState == FormWindowState.Minimized);
                }
            }
            catch { }
        }

        public main FindMainForm()
        {
            bool found = false;
            UserForm usrfrm = this;
            try
            {
                while ((this.ParentForm != null || this.Owner != null) && usrfrm != null && !found)
                {
                    if (usrfrm.ParentForm != null)
                    {
                        if (usrfrm.ParentForm is main) return (main)usrfrm.ParentForm;
                        else usrfrm = (UserForm)this.ParentForm;
                    }
                    else if (this.Owner != null)
                    {
                        if (usrfrm.Owner is main) return (main)usrfrm.Owner;
                        else usrfrm = (UserForm)usrfrm.Owner;
                    }
                }
                return null;
            }
            catch { return null; }
        }

        public void RemoveFormFromStatusBar()
        {
            /*
            try
            {
                ((main)this.ParentForm).toolStripOpenForms.Items.Remove(((main)this.ParentForm).toolStripOpenForms.Items[String.Format("{0}Form", this.Name)]);
                //if(((main)this.ParentForm).toolStripOpenForms.Items.Count < 1)
                //    ((main)this.ParentForm).toolStripOpenForms.Visible = false;
                this.Visible = !(this.WindowState == FormWindowState.Minimized);
            }
            catch { }
            try
            {
                ((main)this.Owner).toolStripOpenForms.Items.Remove(((main)this.Owner).toolStripOpenForms.Items[String.Format("{0}Form", this.Name)]);
                this.Visible = !(this.WindowState == FormWindowState.Minimized);
            }
            catch { }
            */
            try
            {
                main m = FindMainForm();
                if (m != null)
                {
                    m.toolStripOpenForms.Items.Remove(m.toolStripOpenForms.Items[String.Format("{0}Form", this.Name)]);
                    this.Visible = !(this.WindowState == FormWindowState.Minimized);
                }
            }
            catch { }
        }


        public void AddLabelToStatusBar(string name, string text)
        {
            try
            {
                if (this.ParentForm != null)
                {
                    if (((main)this.ParentForm).toolStripOpenForms.Items[name] != null)
                    {
                        ((ToolStripLabel)((main)this.ParentForm).toolStripOpenForms.Items[name]).Text = text;
                    }
                    else
                    {
                        ToolStrip open_forms = ((main)this.ParentForm).toolStripOpenForms;
                        ToolStripLabel new_lbl = new ToolStripLabel(text);
                        new_lbl.Name = name;
                        new_lbl.Font = new Font(new_lbl.Font, FontStyle.Bold);
                        new_lbl.ForeColor = Color.DarkOrange;
                        new_lbl.TextAlign = ContentAlignment.MiddleRight;
                        new_lbl.AutoSize = false;
                        int toolstrip_items_width = 0;
                        for (int i = 0; i < open_forms.Items.Count; i++)
                            toolstrip_items_width += open_forms.Items[i].Width;
                        new_lbl.Width = open_forms.Width - toolstrip_items_width - 100;
                        open_forms.Items.Add(new_lbl);
                    }
                }
            }
            catch { }
        }

        public void RemoveLabelFromStatusBar(string name)
        {
            try
            {
                ((main)this.ParentForm).toolStripOpenForms.Items.Remove(((main)this.ParentForm).toolStripOpenForms.Items[name]);
            }
            catch { }
        }

        public virtual void FormSelect_Click(object sender, EventArgs e)
        {
            try
            {
                main m = FindMainForm();
                ToolStripButton new_form = (ToolStripButton)sender;
                //UserForm frm = (UserForm)(((main)ParentForm).Controls.Find(new_form.Name.Replace("Form", ""), true)[0]);
                UserForm frm = (UserForm)(m.Controls.Find(new_form.Name.Replace("Form", "").Replace("DynamicLists", "DynamicFormLists").Replace("DynamicEmployees", "DynamicFormEmployees").Replace("DynamicRoles", "DynamicFormRoles"), true)[0]);
                frm.Visible = true;
                frm.WindowState = (frm.WindowState == FormWindowState.Minimized ? FormWindowState.Maximized : frm.WindowState);
                frm.BringToFront();
                frm.Focus();
            }
            catch (Exception exp) { LogWriter.Log(exp, SettingsClass.ErrorLogFile); }
        }

        public virtual void ShowErrorsDialog(string message)
        {
            listBoxErrors.DataSource = null;
            if (ErrorList.Count > 0)
            {
                listBoxErrors.DisplayMember = "Value";
                listBoxErrors.ValueMember = "Key";
                listBoxErrors.DataSource = ErrorList;
            }
            toolStripLabelErrors.ForeColor = Color.Red;
            toolStripLabelErrors.Text = message;
            panelErrors.Visible = true;
            panelErrors.BringToFront();
        }

        public virtual void ShowWarningsDialog(string message)
        {
            listBoxErrors.DataSource = null;
            if (WarningList.Count > 0)
            {
                listBoxErrors.DisplayMember = "Value";
                listBoxErrors.ValueMember = "Key";
                listBoxErrors.DataSource = WarningList;
            }
            toolStripLabelErrors.ForeColor = Color.DarkOrange;
            toolStripLabelErrors.Text = message;
            panelErrors.Visible = true;
            panelErrors.BringToFront();
        }

        public virtual void ShowConfirmationDialog(string message)
        {
            listBoxErrors.DataSource = null;
            toolStripLabelErrors.ForeColor = Color.Green;
            toolStripLabelErrors.Text = message;
            panelErrors.Visible = true;
            panelErrors.BringToFront();
            try
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is UserButton && ctrl.Name.ToLower().IndexOf("save") > -1)
                    {
                        ctrl.Enabled = false;
                        break;
                    }
                }
            }
            catch { }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panelErrors.Visible = false;
            try
            {
                listBoxErrors.Items.Clear(); // de vazut daca nu apar probleme de validare la salvari
            }
            catch
            {
                try
                {
                    listBoxErrors.DataSource = null;
                }
                catch { }
            }
            if(this.WarningList.Count > 0)
                this.ConfirmSaveAfterWarnings = true;
        }

        private void listBoxErrors_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                dynamic x = this;
                string tabControlName = "";
                foreach (Control ctrl in x.Controls)
                {
                    if (ctrl is LidorSystems.IntegralUI.Containers.TabControl)
                    {
                        tabControlName = ctrl.Name;
                        break;
                    }
                }
                foreach (LidorSystems.IntegralUI.Containers.TabPage tp in ((LidorSystems.IntegralUI.Containers.TabControl)x.Controls[tabControlName]).Pages)
                {
                    if (tp.Controls.Find(((ListBox)sender).SelectedValue.ToString(), true).Length > 0 && tp.Controls.Find(((ListBox)sender).SelectedValue.ToString(), true)[0] != null)
                    {
                        ((LidorSystems.IntegralUI.Containers.TabControl)x.Controls[tabControlName]).SelectedPage = tp;
                        //tp.Select();
                        //tp.Show();
                        break;
                    }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }
        /*
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        */

        public void EnableDisableToolBarButtons(bool _enabled)
        {
            try
            {
                dynamic frm = this.Launcher;
                //switch (this.Name.ToLower())
                //{
                //    case "contracts":
                //        frm = (ContractSelect)((main)FindMainForm()).Controls.Find("ContractSelect", true)[0];
                //        break;
                //}
                for (int i = 0; i < ((DataGrid)frm.dataGrid1).toolStrip1.Items.Count; i++)
                {
                    if (
                        !(((DataGrid)frm.dataGrid1).toolStrip1.Items[i] is System.Windows.Forms.ToolStripSeparator) &&
                        !(((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripButtonMove" ||
                        //((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripButtonRefresh" ||
                        ((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripSplitButtonLayout" ||
                        ((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripButtonPrint" ||
                        ((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripSplitButtonExcel" ||
                        ((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripButtonSearchBack" ||
                        ((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripButtonSearchForward" ||
                        ((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripButtonComplexSort" ||
                        ((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripButtonExit" ||
                        ((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripTextBoxSearchFor" ||
                        ((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Name == "toolStripComboBoxSearchType"
                        ))
                        ((DataGrid)frm.dataGrid1).toolStrip1.Items[i].Enabled = _enabled;
                    try
                    {
                        if (_enabled)
                            frm.dataGrid1.toolStrip1.Items["toolStripButtonAddAddendum"].Enabled = (frm.dataGrid1.dataGridView.Rows.Count > 0 && frm.dataGrid1.dataGridView.SelectedRows[0].Cells["parent_contract_id"].Value.ToString().Trim() == "") ? true : false;
                    }
                    catch { }
                }
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        public void EnableDisableMainMenuButtons(bool _enabled)
        {
            try
            {
                switch (this.Name.ToLower())
                {
                    case "contracts":
                        ((main)FindMainForm()).contractsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "bankextractimport":
                        ((main)FindMainForm()).bankReceiptsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "companybankextractimport":
                        ((main)FindMainForm()).bankReceiptsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "bankreceipt":
                        ((main)FindMainForm()).bankReceiptsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "bank":
                        ((main)FindMainForm()).banksToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "dynamicformlists_cities":
                        ((main)FindMainForm()).citiesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "companyincomeexpenses":
                        ((main)FindMainForm()).companyIncomeExpensesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "connectionsettings":
                        ((main)FindMainForm()).serverConnectionToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "dynamicformlists_contractreasons":
                        ((main)FindMainForm()).addendumReasonsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "coowners":
                        ((main)FindMainForm()).coOwnersToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "currencies":
                        ((main)FindMainForm()).currenciesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "dynamicformemployees_employees":
                        ((main)FindMainForm()).employeesToolStripMenuItem.Enabled = _enabled;
                        break;
                    /*
                    case "employeesroles":
                        ((main)FindMainForm()).employeesToolStripMenuItem.Enabled = _enabled;
                        break;
                    */
                    case "employees":
                        ((main)FindMainForm()).employeesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "fontthemeselect":
                        ((main)FindMainForm()).fontThemeToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "groups":
                        ((main)FindMainForm()).groupsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "incomeexpenses":
                        ((main)FindMainForm()).incomeExpensesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "invoicerequirements":
                        ((main)FindMainForm()).invoiceRequirementsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "invoices":
                        ((main)FindMainForm()).invoiceRequirementsToolStripMenuItem.Enabled = _enabled;
                        ((main)FindMainForm()).invoicesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "languagefileeditor":
                        ((main)FindMainForm()).languageEditorToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "lists":
                        ((main)FindMainForm()).generalToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "monthlyrentrates":
                        ((main)FindMainForm()).monthlyRentRatesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "owners":
                        ((main)FindMainForm()).ownersToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "projects":
                        ((main)FindMainForm()).projectsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "properties":
                        ((main)FindMainForm()).propertiesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "dynamicformlists_propertyrooms":
                        ((main)FindMainForm()).propertyRoomsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "rentcontracts":
                        ((main)FindMainForm()).rentContractsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "roles":
                        ((main)FindMainForm()).rolesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "dynamicformlists_roomassets":
                        ((main)FindMainForm()).roomAssetsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "dynamicformlists_roomassetsconditions":
                        ((main)FindMainForm()).roomAssetsConditionsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "dynamicformlists_services":
                        ((main)FindMainForm()).servicesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "servicesservicetypes":
                        ((main)FindMainForm()).servicesServiceTypesToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "tenants":
                        ((main)FindMainForm()).tenantsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "dynamicformlists_visitreasons":
                        ((main)FindMainForm()).inspectionReasonsToolStripMenuItem.Enabled = _enabled;
                        break;
                    case "visits":
                        ((main)FindMainForm()).visitsToolStripMenuItem.Enabled = _enabled;
                        break;

                }
                //((main)FindMainForm()).contractsToolStripMenuItem.Enabled = _enabled;
            }
            catch (Exception exp)
            {
                LogWriter.Log(exp, SettingsClass.ErrorLogFile);
            }
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                try
                {
                    if (this.Launcher != null && (EditMode > 0 || this.Launcher.EditMode > 0))
                    {
                        dynamic frm = this.Launcher;
                        frm.dataGrid1.toolStripButtonRefresh.Enabled = true;
                        frm.dataGrid1.toolStripButtonRefresh.PerformClick();
                    }
                }
                catch { }
                EnableDisableMainMenuButtons(true);
                EnableDisableToolBarButtons(true);
                this.Launcher.ChildLaunched = null;
                this.Launcher = null;
                this.EditMode = 0;
                this.Launcher.EditMode = 0;
            }
            catch { }
        }

        private void UserForm_Paint(object sender, EventArgs e)
        {
            ChangeStatusBarFormLabel();
        }

        private void UserForm_Enter(object sender, EventArgs e)
        {
            ChangeStatusBarFormLabel();
        }

        private void UserForm_GotFocus(object sender, EventArgs e)
        {
            ChangeStatusBarFormLabel();
        }

        private void ChangeStatusBarFormLabel()
        {
            try
            {
                main m = FindMainForm();
                if (m != null)
                {
                    m.toolStripOpenForms.Items[String.Format("{0}Form", this.Name)].ForeColor = Color.DarkOrange;
                    ((ToolStripButton)m.toolStripOpenForms.Items[String.Format("{0}Form", this.Name)]).Font = new Font(((ToolStripButton)m.toolStripOpenForms.Items[String.Format("{0}Form", this.Name)]).Font, FontStyle.Bold);
                }
            }
            catch { }
        }


        private void UserForm_Leave(object sender, EventArgs e)
        {
            try
            {
                main m = FindMainForm();
                if (m != null)
                {
                    m.toolStripOpenForms.Items[String.Format("{0}Form", this.Name)].ForeColor = Color.Black;
                    ((ToolStripButton)m.toolStripOpenForms.Items[String.Format("{0}Form", this.Name)]).Font = new Font(((ToolStripButton)m.toolStripOpenForms.Items[String.Format("{0}Form", this.Name)]).Font, FontStyle.Regular);
                }
            }
            catch { }
        }

        private void AddContextMenus(object control)
        {
            try
            {
                if (control is Button)
                {
                    ((Button)control).MouseDown += new MouseEventHandler(ctrl_MouseDown);
                }
                try
                {
                    foreach (Control ctrl in ((Control)control).Controls)
                    {
                        try
                        {
                            AddContextMenus(ctrl);
                        }
                        catch { }
                    }
                }
                catch { }

                if (control is ToolStripButton)
                {
                    ((ToolStripButton)control).MouseDown += new MouseEventHandler(ctrl_MouseDown);
                }
                try
                {
                    foreach (ToolStripItem ctrl in ((ToolStrip)control).Items)
                    {
                        try
                        {
                            AddContextMenus(ctrl);
                        }
                        catch { }
                    }
                }
                catch { }
            }
            catch { }
        }

        private void ctrl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && SettingsClass.UserName == "daniel")
            {
                MenuItem[] menuItems = new MenuItem[] { new MenuItem("Add to Controls table", new System.EventHandler(this.AddToControlsMenuItemClick)) };
                ContextMenu rightcontext = new ContextMenu(menuItems);

                int xOffset = e.X; //Cursor.Position.X - Dte.ActiveForm.Location.X;
                int yOffset = e.Y; //Cursor.Position.Y - Dte.ActiveForm.Location.Y;

                try
                {
                    //MessageBox.Show(String.Format("{0} - {1}", ((Control)sender).Name, ((Control)sender).GetType().Name));
                    rightcontext.Show((Control)sender, new Point(xOffset, yOffset));
                    ClickedObject[0] = ((Control)sender).Name;
                    ClickedObject[1] = ((Control)sender).GetType().Name;
                }
                catch { }
                try
                {
                    //MessageBox.Show(String.Format("{0} - {1}", ((ToolStripItem)sender).Name, ((ToolStripItem)sender).GetType().Name));
                    rightcontext.Show(((ToolStripItem)sender).Owner, new Point(xOffset, yOffset));
                    ClickedObject[0] = ((ToolStripItem)sender).Name;
                    ClickedObject[1] = ((ToolStripItem)sender).GetType().Name;
                }
                catch { }
            }
        }

        private void AddToControlsMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTROLLSsp_get_by_name_form_type", new object[]{
                    new MySqlParameter("_NAME", ClickedObject[0]),
                    new MySqlParameter("_FORM", this.Name),
                    new MySqlParameter("_TYPE", ClickedObject[1])
                });
                object id = da.ExecuteScalarQuery();
                if (id != null && id != DBNull.Value)
                {
                    MessageBox.Show("Control already added!");
                }
                else
                {
                    da = new DataAccess(CommandType.StoredProcedure, "CONTROLLSsp_insert", new object[]{
                        new MySqlParameter("_NAME", ClickedObject[0]),
                        new MySqlParameter("_FORM", this.Name),
                        new MySqlParameter("_TYPE", ClickedObject[1])
                    });
                    da.ExecuteInsertQuery();
                    MessageBox.Show("Control added!");
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
    }


    public class LoaderClass
    {
        public void ShowLoader()
        {
            Loader l = new Loader();
            l.TopMost = true;
            l.TopLevel = true;
            l.BringToFront();
            //l.ShowDialog();
            l.Show();
        }
    }

    public class NativeForm : NativeWindow
    {
        
        private Form parent;
        private Form child;
       
        public NativeForm(Form parent, Form child)
        {
            if (parent != null)
            {
                this.parent = parent;
                this.child = child;
                child.HandleDestroyed += new EventHandler(this.OnHandleDestroyed);
                AssignHandle(parent.Handle);
                ActivateChild(true);
            }
        }
        
        private void OnHandleDestroyed(Object sender, EventArgs e)
        {
            ReleaseHandle();
        }

        
        [System.Security.Permissions.PermissionSetAttribute(System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.DefWndProc(ref m);
            if (m.Msg == NativeMethods.WM_NCACTIVATE)
                ActivateChild(Convert.ToBoolean(m.WParam.ToInt32()));
        }
        

        internal void ActivateChild(bool active)
        {
            if (child.IsDisposed)
                return;
            NativeMethods.SendMessage(child.Handle, Convert.ToUInt32(NativeMethods.WM_NCACTIVATE), (IntPtr)Convert.ToInt32(active), IntPtr.Zero);
        }
        

    }


    internal sealed class NativeMethods
    {
        private NativeMethods()
        {
            //Not Instantiable Class
        }
        
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, Int32 X, Int32 Y, Int32 cx, Int32 cy, UInt32 uFlags);

        internal const Int32 WM_NCACTIVATE = 0x86;

        internal const Int32 WM_NCLBUTTONDOWN = 0xA1;
        internal const Int32 WM_SYSCOMMAND = 0x112;

        internal const Int32 HTCAPTION = 0x2;

        internal const Int32 SC_MOVE = 0xF010;

        internal const Int32 HWND_NOTOPMOST = -2;
        internal const Int32 SWP_NOMOVE = 0x2;
        internal const Int32 SWP_NOZORDER = 0x4;
        
    }

}
