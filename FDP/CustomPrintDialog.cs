using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FDP
{
    public partial class CustomPrintDialog : UserForm
    {
        public CustomPrintDialog()
        {
            InitializeComponent();
        }

        private void CustomPrintDialog_Load(object sender, EventArgs e)
        {

        }

        private void userButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
