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
    public partial class Loader : Form
    {
        public Loader()
        {
            InitializeComponent();
        }
        private void Loader_Load(object sender, EventArgs e)
        {
            int i = 0;
            while (true)
            {
                i = (i == 2 ? 0 : i);
                label1.Text = i == 0 ? "Loading" : String.Format("{0}.", label1.Text);
                i++;
            }
        }
    }
}
