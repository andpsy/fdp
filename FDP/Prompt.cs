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
    public partial class Prompt : UserForm
    {
        public Prompt()
        {
            InitializeComponent();
        }

        public Prompt(string caption, bool password)
        {
            InitializeComponent();
            this.Text = caption;
            if (password)
                userTextBoxPrompt.PasswordChar = '*';
        }

        private void Prompt_Load(object sender, EventArgs e)
        {

        }

        private void userButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userButtonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userTextBoxPrompt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.userButtonOk_Click(null, null);
                return;
            }
        }
    }
}
