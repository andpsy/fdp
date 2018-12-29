using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FDP_Admin
{
    public partial class UserForm : System.Windows.Forms.Form
    {
        public FormWindowState Maximized
        {
            get;
            set;
        }

        public UserForm()
        {
            InitializeComponent();
        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        public virtual void UserForm_Load(object sender, EventArgs e)
        {
            //this.Dock = DockStyle.Fill;
            this.WindowState = this.Maximized;
            CommonFunctions.SetDateFormat(this);
            CommonFunctions.SetFont(this);
            //Language.LoadLabels(this);
        }
    }
}
