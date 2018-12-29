using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Guifreaks.NavigationBar;
using System.Reflection;
using System.IO;
using System.Xml;
using Microsoft.Xml.Serialization.GeneratedAssembly;
using SplashScreen;

namespace FDP
{
    public partial class main : Form
    {
        public main()
        {
            //this.MdiChildActivate += new EventHandler(main_MdiChildActivate);
            InitializeComponent();
            try
            {
                CurrenciesClass.GetCurrencies();
            }
            catch (Exception exp)
            {
                MessageBox.Show(String.Format(Language.GetMessageBoxText("errorGettingExchangeRates", "There was an error getting the exchange rates from BNR site:\r\n{0}"), ExceptionParser.ParseException(exp)), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            serverConnectionToolStripMenuItem.Enabled = Rights.HasVisualize("admin");
            generalToolStripMenuItem.Enabled = Rights.HasVisualize("admin");
        }
        /*
        private void buttonCreateOwner_Click(object sender, EventArgs e)
        {

        }

        private void naviBandExit_Click(object sender, EventArgs e)
        {
        }

        private void createButton_Activated(object sender, EventArgs e)
        {
        }
        private void createButton_Click(object sender, EventArgs e)
        {
        }
        */
        private void naviBandExit_MouseClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void naviBarMain_ActiveBandChanging(object sender, NaviBandEventArgs e)
        {
            if (e.NewActiveBand.Name == "naviBandExit")
            {
                /*
                DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmExit", "Are you sure you want to exit?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(ans == DialogResult.Yes)
                    Application.Exit();
                else
                {
                    e.Canceled = true;
                    return;
                }
                */
                this.Close();
            }
            if (e.NewActiveBand.Name == "naviBandReminders")
            {
                OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "Reminders", typeof(Reminders), "visualize", false);
            }
        }
        /*
        private void languageFileEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        */
        private void serverConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            var f = new ConnectionSettings();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ConnectionSettings", typeof(ConnectionSettings), "special", false);
        }

        private void main_Load(object sender, EventArgs e)
        {
            CommonFunctions.SetFont(this);
            Language.LoadLabels(this);

            try
            {
                NaviBarSettingsSerializer serial = new NaviBarSettingsSerializer();
                using (StreamReader reader = new StreamReader(Path.Combine(SettingsClass.SettingsFilesPath, "naviband.xml")))
                {
                    NaviBarSettings settings = serial.Deserialize(reader) as NaviBarSettings;
                    if (settings != null)
                    {
                        naviBarMain.Settings = settings;
                        naviBarMain.ApplySettings();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            naviBarMain.SetActiveBand(naviBandDatabases);
            if(SettingsClass.LoadRemindersOnStartup)
                OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "Reminders", typeof(Reminders), "visualize", false);
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString(); 
            labelVersion.Text = String.Format("version {0}", version);
            if(SettingsClass.LoginOwnerId > 0)
                SetOwnerRights(this);
        }
        /*
        private void main_MdiChildActivate(object sender, EventArgs e)
        {
            //this.LayoutMdi(MdiLayout.TileVertical);
        }
        private void naviBandExit_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void listsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        */
        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            var f = new Employees();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "Employees", typeof(Employees), "special", false);
        }

        private void generalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            var f = new Lists();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "Lists", typeof(Lists), "special", false);
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            var f = new Roles();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "Roles", typeof(Roles), "special", false);
        }

        private void groupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            var f = new Groups();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "Groups", typeof(Groups), "special", false);
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "OwnerSelect", typeof(Owners), "add", false);
            /*
            var f = new Owners();
            //f.TopLevel = false;
            //f.MdiParent = this;
            //splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.BringToFront();
            //f.StartPosition = FormStartPosition.Manual;
            //f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            f.Dispose();
            */
        }

        private void languageEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "LanguageFileEditor", typeof(LanguageFileEditor), "special", false);
            /*
            var f = new LanguageFileEditor();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void currenciesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CurrenciesForm", typeof(Currencies), "special", false);
            /*
            var f = new CurrenciesForm();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void visualizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync(this);
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "OwnerSelect", typeof(OwnerSelect), "visualize", false);
            /*
            var f = new OwnerSelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void visualizeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CoOwnerSelect", typeof(CoOwnerSelect), "visualize", false);
            /*
            var f = new CoOwnerSelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */ 
        }

        private void createToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CoOwnerSelect", typeof(CoOwners), "add", false);
            /*
            var f = new CoOwners();
            //f.TopLevel = false;
            //f.MdiParent = this;
            //splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.BringToFront();
            //f.StartPosition = FormStartPosition.Manual;
            //f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            f.Dispose();
            */
        }

        private void createToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "TenantSelect", typeof(Tenants), "add", false);
            /*
            var f = new Tenants();
            //f.TopLevel = false;
            //f.MdiParent = this;
            //splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.BringToFront();
            //f.StartPosition = FormStartPosition.Manual;
            //f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            f.Dispose();
            */
        }

        private void visualizeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "TenantSelect", typeof(TenantSelect), "visualize", false);
            /*
            var f = new TenantSelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void visualizeToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ProportySelect", typeof(ProportySelect), "visualize", false);
            /*
            var f = new ProportySelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            //f.WindowState = FormWindowState.Maximized;
            f.Show();
            */
        }

        private void citiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "Cities", typeof(Cities), "visualize", false);
            /*
            var f = new Cities();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void fontThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "FontThemeSelect", typeof(FontThemeSelect), "special", false);
            /*
            var f = new FontThemeSelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */ 
        }

        private void visualizeToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ProjectSelect", typeof(ProjectSelect), "visualize", false);
            /*
            var f = new ProjectSelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void createToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ProportySelect", typeof(Property), "add", false);
            /*
            var f = new Property();
            //f.TopLevel = false;
            //f.MdiParent = this;
            //splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.BringToFront();
            //f.StartPosition = FormStartPosition.Manual;
            //f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            f.Dispose();
            */
        }

        private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //visualizeToolStripMenuItem1_Click(null, null);
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "TenantSelect", typeof(TenantSelect), "edit", false);
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //visualizeToolStripMenuItem_Click(null, null);
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "OwnerSelect", typeof(OwnerSelect), "edit", false);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //visualizeToolStripMenuItem_Click(null, null);
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "OwnerSelect", typeof(OwnerSelect), "delete", false);
        }

        private void createToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ProjectSelect", typeof(Projects), "add", false);
            /*
            var f = new Projects();
            //f.TopLevel = false;
            //f.MdiParent = this;
            //splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.BringToFront();
            //f.StartPosition = FormStartPosition.Manual;
            //f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            f.Dispose();
            */
        }

        private void propertyRoomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "PropertyRooms", typeof(PropertyRooms), "visualize", false);
            /*
            var f = new PropertyRooms();
            //f.TopLevel = false;
            //f.MdiParent = this;
            //splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.BringToFront();
            //f.StartPosition = FormStartPosition.Manual;
            //f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            f.Dispose();
            */
        }

        private void roomAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "RoomAssets", typeof(RoomAssets), "visualize", false);
            /*
            var f = new RoomAssets();
            //f.TopLevel = false;
            //f.MdiParent = this;
            //splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.BringToFront();
            //f.StartPosition = FormStartPosition.Manual;
            //f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            f.Dispose();
            */
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //visualizeToolStripMenuItem1_Click(null, null);
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "TenantSelect", typeof(TenantSelect), "delete", false);
        }

        private void editToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //visualizeToolStripMenuItem3_Click(null, null);
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ProportySelect", typeof(ProportySelect), "edit", false);
        }

        private void deleteToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //visualizeToolStripMenuItem3_Click(null, null);
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ProportySelect", typeof(ProportySelect), "delete", false);
        }

        private void editToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            //visualizeToolStripMenuItem4_Click(null, null);
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ProjectSelect", typeof(ProjectSelect), "edit", false);
        }

        private void deleteToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //visualizeToolStripMenuItem4_Click(null, null);
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ProjectSelect", typeof(ProjectSelect), "delete", false);
        }

        private void createToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            //OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ContractSelect", typeof(Contracts), "add", false);
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ContractSelect", typeof(Contracts), "add", false);
            /*
            var f = new Contracts();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void servicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "Services", typeof(Services), "visualize", false);
            /*
            var f = new Services();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void visualizeToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ContractSelect", typeof(ContractSelect), "visualize", false);
            /*
            var f = new ContractSelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void addendumReasonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ContractReasons", typeof(ContractReasons), "visualize", false);
            /*    
            var f = new ContractReasons();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void visualiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "RentContractSelect", typeof(RentContractSelect), "visualize", false);
            /*
            var f = new RentContractSelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            //f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            //f.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            f.Show();
            */
        }

        private void createToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "RentContractSelect", typeof(RentContracts), "add", false);
            /*
            var f = new RentContracts();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void visualiseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "InvoiceRequirementSelect", typeof(InvoiceRequirementSelect), "visualize", false);
            /*
            var f = new InvoiceRequirementSelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void visualizeToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "VisitSelect", typeof(VisitSelect), "visualize", false);
            /*
            var f = new VisitSelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void createToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "InvoiceRequirementSelect", typeof(InvoiceRequirements), "add", false);
            /*
            var f = new InvoiceRequirements();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void createToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "VisitSelect", typeof(Visits), "add", false);
            /*
            var f = new Visits();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void inspectionReasonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "VisitReasons", typeof(VisitReasons), "visualize", false);
            /*
            var f = new VisitReasons();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void roomAssetsConditionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "RoomAssetsConditions", typeof(RoomAssetsConditions), "visualize", false);
            /*
            var f = new RoomAssetsConditions();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void generalSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "main", typeof(FDP_Client_Admin.main), "visualize", false);
            /*
            var f = new FDP_Client_Admin.main();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */ 
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ContractSelect", typeof(ContractSelect), "edit", false);
        }

        private void editToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CoOwnerSelect", typeof(CoOwnerSelect), "edit", false);
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CoOwnerSelect", typeof(CoOwnerSelect), "delete", false);
        }

        private void deleteToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ContractSelect", typeof(ContractSelect), "delete", false);
        }

        private void modifiyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "RentContractSelect", typeof(RentContractSelect), "edit", false);
        }

        private void deleteToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "RentContractSelect", typeof(RentContractSelect), "delete", false);
        }

        private void modifiyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "InvoiceRequirementSelect", typeof(InvoiceRequirementSelect), "edit", false);
        }

        private void deleteToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "InvoiceRequirementSelect", typeof(InvoiceRequirementSelect), "delete", false);
        }

        private void modifiyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "VisitSelect", typeof(VisitSelect), "edit", false);
        }

        private void deleteToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "VisitSelect", typeof(VisitSelect), "delete", false);
        }

        private void visualizeToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "PredictedIncomeExpenseSelect", typeof(PredictedIncomeExpenseSelect), "visualize", false);
            /*
            var f = new PredictedIncomeExpenseSelect();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void createToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "PredictedIncomeExpenseSelect", typeof(PredictedIncomeExpenses), "add", false);
            /*
            var f = new PredictedIncomeExpenses();
            f.TopLevel = false;
            f.MdiParent = this;
            splitContainerMain.Panel2.Controls["panelMain"].Controls.Add(f);
            //f.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            //f.Dock = DockStyle.Fill;
            f.BringToFront();
            //f.StartPosition = FormStartPosition.CenterParent;
            f.StartPosition = FormStartPosition.Manual;
            f.Location = new Point(splitContainerMain.Panel2.Width / 2 - f.Width / 2, splitContainerMain.Panel2.Height / 2 - f.Height / 2);
            f.Show();
            */
        }

        private void modifiyToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "PredictedIncomeExpenseSelect", typeof(PredictedIncomeExpenseSelect), "edit", false);
        }

        private void deleteToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "PredictedIncomeExpenseSelect", typeof(PredictedIncomeExpenseSelect), "delete", false);
        }

        private object GetTopForm(Control container)
        {
            int child_index = container.Controls.GetChildIndex(container.Controls[0]);
            object f = container.Controls[0];
            foreach (Control frm in container.Controls)
            {
                if (frm is UserForm && ((UserForm)frm).WindowState != FormWindowState.Minimized && ((UserForm)frm).Visible && ((UserForm)frm).Focused)
                {
                    child_index = container.Controls.GetChildIndex(frm);
                    f = frm;
                    break;
                }
            }
            /*
            foreach (Control frm in container.Controls)
            {
                if (container.Controls.GetChildIndex(frm) > child_index)
                {
                    child_index = container.Controls.GetChildIndex(frm);
                    f = frm;
                    break;
                }
            }
            */
            return f;
        }

        private object FindForm(string form_name, Control container)
        {
            return container.Controls.Find(form_name, true)[0];
        }

        /// <summary>
        /// The function for openning a form from the main menu
        /// </summary>
        /// <param name="container">the main panel in which to open the form</param>
        /// <param name="form_name">form name to open</param>
        /// <param name="t">type of the form to open</param>
        /// <param name="operation">operation to perform ( add / edit/ delete / visualize )</param>
        /// <param name="showdialog">open form as modal or not</param>
        private void OpenForm(Control container, string form_name, Type t, string operation, bool showdialog)
        {
            try
            {
                //object f = GetTopForm(container);
                object f = FindForm(form_name, container);
                if (f != null && f.GetType().Name == form_name)
                {
                    dynamic frm = f;
                    if (operation == "add")
                    {
                        ((UserForm)frm).EditMode = 1;
                        frm.BringToFront();
                        frm.buttonAdd_Click(null, null);
                    }
                    else if (operation == "visualize")
                    {
                        frm.BringToFront();
                        frm.Focus();
                        try
                        {
                            toolStripOpenForms.Items[form_name].ForeColor = Color.DarkOrange;
                            ((ToolStripButton)toolStripOpenForms.Items[form_name]).Font = new Font(((ToolStripButton)toolStripOpenForms.Items[form_name]).Font, FontStyle.Bold);
                        }
                        catch { }

                    }
                    else
                    {
                        if (frm.dataGrid1.dataGridView.SelectedRows.Count == 1)
                        {
                            switch (operation)
                            {
                                case "edit":
                                    ((UserForm)frm).EditMode = 2;
                                    frm.BringToFront();
                                    frm.buttonEdit_Click(null, null);
                                    break;
                                case "delete":
                                    ((UserForm)frm).EditMode = 3;
                                    frm.BringToFront();
                                    frm.buttonDelete_Click(null, null);
                                    break;
                                case "special":
                                    ((UserForm)frm).EditMode = 9;
                                    frm.Visible = false;
                                    frm.BringToFront();
                                    frm.buttonDelete_Click(null, null);
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    //SplashScreen.SplashScreen.ShowSplashScreen(this.PointToScreen(this.panelMain.Location).X, this.PointToScreen(this.panelMain.Location).Y, this.panelMain.Width, this.panelMain.Height);
                    SplashScreen.SplashScreen.ShowSplashScreen(panelMain.PointToScreen(Point.Empty).X, panelMain.PointToScreen(Point.Empty).Y, this.panelMain.Width, this.panelMain.Height);
                    Application.DoEvents();
                    //System.Threading.Thread.Sleep(SplashScreen.SplashScreen.TIMER_INTERVAL);
                    var frm = Activator.CreateInstance(t);
                    try
                    {
                        var fx = new Form();
                        if (frm is UserForm) fx = (UserForm)frm;
                        if (frm is FDP_Client_Admin.UserForm) fx = (FDP_Client_Admin.UserForm)frm;
                        if (showdialog)
                        {
                            ((UserForm)fx).EditMode = operation == "add" ? 1 : operation == "edit" ? 2 : operation == "delete" ? 3 : operation == "special" ? 9 : 0;
                            fx.StartPosition = FormStartPosition.CenterParent;
                            fx.ShowDialog(this);
                            fx.Dispose();
                        }
                        else
                        {
                            ((UserForm)fx).EditMode = operation == "add" ? 1 : operation == "edit" ? 2 : operation == "delete" ? 3 : operation == "special" ? 9 : 0;
                            fx.Visible = false;
                            fx.TopLevel = false;
                            fx.MdiParent = this;
                            container.Controls.Add(fx);
                            //fx.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                            //fx.Dock = DockStyle.Fill;
                            fx.BringToFront();
                            //fx.StartPosition = FormStartPosition.CenterParent;
                            fx.StartPosition = FormStartPosition.Manual;
                            fx.Location = new Point(container.Width / 2 - fx.Width / 2, container.Height / 2 - fx.Height / 2);
                            fx.Show();
                        }
                    }
                    catch (Exception exp0) { exp0.ToString(); }
                }
            }
            catch (Exception exp)
            {
                exp.ToString();
                //SplashScreen.SplashScreen.ShowSplashScreen(this.PointToScreen(this.panelMain.Location).X, this.PointToScreen(this.panelMain.Location).Y, this.panelMain.Width, this.panelMain.Height);
                SplashScreen.SplashScreen.ShowSplashScreen(panelMain.PointToScreen(Point.Empty).X, panelMain.PointToScreen(Point.Empty).Y, this.panelMain.Width, this.panelMain.Height);
                Application.DoEvents();
                //System.Threading.Thread.Sleep(SplashScreen.SplashScreen.TIMER_INTERVAL);
                try
                {
                    var frm = Activator.CreateInstance(t);
                    var fx = new Form();
                    fx.Visible = false;
                    if (frm is UserForm) fx = (UserForm)frm;
                    if (frm is FDP_Client_Admin.UserForm) fx = (FDP_Client_Admin.UserForm)frm;
                    if (showdialog)
                    {
                        ((UserForm)fx).EditMode = operation == "add" ? 1 : operation == "edit" ? 2 : operation == "delete" ? 3 : operation == "special" ? 9 : 0;
                        fx.StartPosition = FormStartPosition.CenterParent;
                        //fx.Visible = true;
                        fx.ShowDialog(this);
                        fx.Dispose();
                    }
                    else
                    {
                        ((UserForm)fx).EditMode = operation == "add" ? 1 : operation == "edit" ? 2 : operation == "delete" ? 3 : operation == "special" ? 9 : 0;
                        fx.Visible = false;
                        fx.TopLevel = false;
                        fx.MdiParent = this;
                        container.Controls.Add(fx);
                        //fx.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
                        //fx.Dock = DockStyle.Fill;
                        fx.BringToFront();
                        //fx.StartPosition = FormStartPosition.CenterParent;
                        fx.StartPosition = FormStartPosition.Manual;
                        fx.Location = new Point(container.Width / 2 - fx.Width / 2, container.Height / 2 - fx.Height / 2);
                        fx.Show();
                    }
                }
                catch (Exception exp2) { exp2.ToString(); }
            }
            //SplashScreen.SplashScreen.CloseForm();
        }

        private void ownersToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem_Click(null, null);
        }

        private void tenantsToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem1_Click(null, null);
        }

        private void propertiesToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem3_Click(null, null);
        }

        private void projectsToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem4_Click(null, null);
        }

        private void contractsToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem5_Click(null, null);
        }

        private void rentContractsToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualiseToolStripMenuItem_Click(null, null);
        }

        private void invoiceRequirementsToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualiseToolStripMenuItem1_Click(null, null);
        }

        private void visitsToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem6_Click(null, null);
        }

        private void predictedIncomeExpensesToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem7_Click(null, null);
        }

        private void HideSubMenus(object sender)
        {
            ((ToolStripMenuItem)sender).HideDropDown();
        }

        private void main_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Tab)
            {
                try
                {
                    UserForm frm = (UserForm)GetTopForm(this.splitContainerMain.Panel2.Controls["panelMain"]);
                    for (int i = 0; i < toolStripOpenForms.Items.Count; i++)
                    {
                        ToolStripButton tb = (ToolStripButton)toolStripOpenForms.Items[i];
                        if (tb.Name.IndexOf(frm.Name) > -1)
                        {
                            int index = i;
                            try
                            {
                                UserForm next_frm = (UserForm)this.splitContainerMain.Panel2.Controls["panelMain"].Controls[toolStripOpenForms.Items[index == toolStripOpenForms.Items.Count - 1 ? 0 : index + 1].Name.Replace("Form", "")];
                                if (next_frm.WindowState == FormWindowState.Minimized)
                                {
                                    next_frm.Visible = true;
                                    next_frm.WindowState = FormWindowState.Maximized;
                                }
                                next_frm.BringToFront();
                                next_frm.Focus();
                                break;
                            }
                            catch { }
                        }
                    }
                }
                catch { }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CompanyPredictedIncomeExpenseSelect", typeof(CompanyPredictedIncomeExpenses), "add", false);
        }

        private void modifiyToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CompanyPredictedIncomeExpenseSelect", typeof(CompanyPredictedIncomeExpenseSelect), "edit", false);
        }

        private void deleteToolStripMenuItem7_Click_1(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CompanyPredictedIncomeExpenseSelect", typeof(CompanyPredictedIncomeExpenseSelect), "delete", false);
        }

        private void companyPredictedIEToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualiseToolStripCompanyMenuItem1_Click(null, null);
        }

        private void visualiseToolStripCompanyMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CompanyPredictedIncomeExpenseSelect", typeof(CompanyPredictedIncomeExpenseSelect), "visualize", false);
        }

        private void monthlyRentRatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "MonthlyRentRates", typeof(MonthlyRentRates), "special", false);
        }

        private void visualizeToolStripMenuItem6_Click_1(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "InvoiceSelect", typeof(InvoiceSelect), "visualize", false);
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "InvoiceSelect", typeof(Invoices), "add", false);
        }

        private void modifyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "InvoiceSelect", typeof(InvoiceSelect), "edit", false);
        }

        private void deleteToolStripMenuItem8_Click_1(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "InvoiceSelect", typeof(InvoiceSelect), "delete", false);
        }

        private void invoicesToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem6_Click_1(null, null);
        }

        private void servicesServiceTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ServicesServicetypes", typeof(ServicesServicetypes), "special", false);
        }

        private void ownersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InitialImportOwners iio = new InitialImportOwners();
            iio.ShowDialog();
            iio.Dispose();
        }

        private void propertiesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InitialImportProperties iip = new InitialImportProperties();
            iip.ShowDialog();
            iip.Dispose();
        }

        private void projectsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InitialImportProjects iip = new InitialImportProjects();
            iip.ShowDialog();
            iip.Dispose();
        }

        private void addToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CompanyIncomeExpenseSelect", typeof(CompanyIncomeExpenses), "add", false);
        }

        private void companyIncomeExpensesToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem8_Click(null, null);
        }

        private void visualizeToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CompanyIncomeExpenseSelect", typeof(CompanyIncomeExpenseSelect), "visualize", false);
        }

        private void modifyToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CompanyIncomeExpenseSelect", typeof(CompanyIncomeExpenseSelect), "edit", false);
        }

        private void deleteToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CompanyIncomeExpenseSelect", typeof(CompanyIncomeExpenseSelect), "delete", false);
        }

        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "IncomeExpenseSelect", typeof(IncomeExpenses), "add", false);
        }

        private void incomeExpensesToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem7_Click_1(null, null);
        }

        private void visualizeToolStripMenuItem7_Click_1(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "IncomeExpenseSelect", typeof(IncomeExpenseSelect), "visualize", false);
        }

        private void modifyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "IncomeExpenseSelect", typeof(IncomeExpenseSelect), "edit", false);
        }

        private void deleteToolStripMenuItem9_Click_1(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "IncomeExpenseSelect", typeof(IncomeExpenseSelect), "delete", false);
        }

        private void cashReceiptsToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem9_Click(null, null);
        }

        private void visualizeToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ReceiptSelect", typeof(ReceiptSelect), "visualize", false);
        }

        private void deleteToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ReceiptSelect", typeof(ReceiptSelect), "delete", false);
        }

        private void modifyToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ReceiptSelect", typeof(ReceiptSelect), "edit", false);
        }

        private void addToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "ReceiptSelect", typeof(Receipt), "add", false);
        }

        private void tenantsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            InitialImportTenants iit = new InitialImportTenants();
            iit.ShowDialog();
            iit.Dispose();
        }

        private void banksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "BankSelect", typeof(BankSelect), "visualize", false);
        }

        private void importExtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            BankExtractImport bei = new BankExtractImport();
            bei.ShowDialog();
            bei.Dispose();
            */
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "BankExtractImport", typeof(BankExtractImport), "special", false);
        }

        private void importCompanyExtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            CompanyBankExtractImport cbei = new CompanyBankExtractImport();
            cbei.ShowDialog();
            cbei.Dispose();
            */
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "CompanyBankExtractImport", typeof(CompanyBankExtractImport), "special", false);
        }

        private void bankReceiptsToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            HideSubMenus(sender);
            visualizeToolStripMenuItem10_Click(null, null);
        }

        private void visualizeToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "BankReceiptSelect", typeof(BankReceiptSelect), "visualize", false);
        }

        private void addToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "BankReceiptSelect", typeof(BankReceipt), "add", false);
        }

        private void editToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "BankReceiptSelect", typeof(BankReceiptSelect), "edit", false);
        }

        private void deleteToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "BankReceiptSelect", typeof(BankReceiptSelect), "delete", false);
        }

        private void ownerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "OwnersIEReport", typeof(OwnersIEReport), "visualize", false);
        }

        private void serverUploadDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "BackupDB", typeof(BackupDB), "visualize", false);
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult ans = MessageBox.Show(Language.GetMessageBoxText("confirmExit", "Are you sure you want to exit?"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (!(ans == DialogResult.Yes))
            {
                e.Cancel = true;
                return;
            }
                /*
            else
            {
                //Application.Exit();
                ;
            }
                 */
        }

        private void ownersRightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "OwnersRights", typeof(OwnersRights), "visualize", false);
        }



        public void SetOwnerRights(Control ctrl)
        {
            foreach (System.Windows.Forms.Control child_ctrl in ctrl.Controls)
            {
                try
                {
                    switch (child_ctrl.GetType().Name)
                    {
                        case "NaviBar":
                            try
                            {
                                foreach (Guifreaks.NavigationBar.NaviBand ti in ((Guifreaks.NavigationBar.NaviBar)child_ctrl).Bands)
                                {
                                    ti.Visible = Rights.GetOwnerRight(ti.Name, this.Name);
                                }
                            }
                            catch { }
                            break;
                        case "NaviBand":
                            try
                            {
                                foreach (Guifreaks.NavigationBar.NaviBandClientArea ti in ((Guifreaks.NavigationBar.NaviBand)child_ctrl).Controls)
                                {
                                    ti.Visible = Rights.GetOwnerRight(ti.Name, this.Name);
                                }
                            }
                            catch { }
                            break;
                        case "NaviBandClientArea":
                            try
                            {
                                foreach (System.Windows.Forms.Control ti in ((Guifreaks.NavigationBar.NaviBandClientArea)child_ctrl).Controls)
                                {
                                    ti.Visible = Rights.GetOwnerRight(ti.Name, this.Name);
                                }
                            }
                            catch { }
                            break;
                        case "ToolStrip":
                            try
                            {
                                foreach (System.Windows.Forms.ToolStripItem ti in ((System.Windows.Forms.ToolStrip)child_ctrl).Items)
                                {
                                    ti.Visible = Rights.GetOwnerRight(ti.Name, this.Name);
                                }
                            }
                            catch { }
                            break;
                        case "MenuStrip":
                            try
                            {
                                //foreach (System.Windows.Forms.ToolStripMenuItem ti in ((System.Windows.Forms.MenuStrip)child_ctrl).Items)
                                foreach (System.Windows.Forms.ToolStripMenuItem ti in CommonFunctions.GetItems((System.Windows.Forms.MenuStrip)child_ctrl))
                                {
                                    ti.Visible = Rights.GetOwnerRight(ti.Name, this.Name);
                                }
                            }
                            catch (Exception exp) { exp.ToString(); }
                            break;
                            /*
                        case "StatusStrip":
                            try
                            {
                                foreach (System.Windows.Forms.ToolStripStatusLabel ti in ((System.Windows.Forms.StatusStrip)child_ctrl).Items)
                                {
                                    ti.Visible = Rights.GetOwnerRight(ti.Name, this.Name);
                                }
                            }
                            catch { }
                            break;
                            */
                        default:
                            try
                            {
                                child_ctrl.Visible = Rights.GetOwnerRight(child_ctrl.Name, this.Name);
                            }
                            catch { }
                            break;
                    }
                }
                catch { }
                SetOwnerRights(child_ctrl);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                string fileName = Path.Combine(SettingsClass.PDFExportPath, "naviband.xml");
                NaviBarSettingsSerializer serial = new NaviBarSettingsSerializer();
                using (TextWriter w = new StreamWriter(fileName))
                {
                    serial.Serialize(w, naviBarMain.Settings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void loginSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "Loginsettings", typeof(LoginSettings), "visualize", false);
        }

        private void districtsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(this.splitContainerMain.Panel2.Controls["panelMain"], "Districts", typeof(Districts), "visualize", false);
        }
    }
}
