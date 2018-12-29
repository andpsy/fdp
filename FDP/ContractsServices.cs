using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FDP
{
    public partial class ContractsServices : UserForm
    {
        public DataSet Content = new DataSet();
        public bool Selectable = false;
        public int ContractId;

        public ContractsServices()
        {
            //base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
        }

        public ContractsServices(bool selectable)
        {
            //base.Maximized = FormWindowState.Maximized;
            Selectable = selectable;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
        }

        public ContractsServices(int contract_id)
        {
            //base.Maximized = FormWindowState.Maximized;
            ContractId = contract_id;
            InitializeComponent();
            dataGrid1.buttonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.buttonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.buttonDelete.Click += new EventHandler(buttonDelete_Click);
            dataGrid1.toolStripButtonAdd.Click += new EventHandler(buttonAdd_Click);
            dataGrid1.toolStripButtonEdit.Click += new EventHandler(buttonEdit_Click);
            dataGrid1.toolStripButtonDelete.Click += new EventHandler(buttonDelete_Click);
        }


        public void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        public void buttonEdit_Click(object sender, EventArgs e)
        {
        }

        public void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void ContractsServices_Load(object sender, EventArgs e)
        {
            dataGrid1.buttonAdd.Visible = false;
            dataGrid1.buttonEdit.Visible = false;
            dataGrid1.buttonDelete.Visible = false;
            dataGrid1.toolStripButtonAdd.Visible = false;
            dataGrid1.toolStripButtonEdit.Visible = false;
            dataGrid1.toolStripButtonDelete.Visible = false;
        }
    }
}
