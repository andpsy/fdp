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
    public partial class OwnersRights : UserForm
    {
        //this.dataGrid1 = new DataGrid("CONTROLLS_OWNERS_RIGHTSsp_select", null, "CONTROLLS_OWNERS_RIGHTSsp_insert", null, "CONTROLLS_OWNERS_RIGHTSsp_update", null, "CONTROLLS_OWNERS_RIGHTSsp_delete", null, null, null, new string[]{"VISIBLE"}, new string[] { "OWNER_ID", "CONTROLL_ID" }, null, new string[] { "OWNER", "CONTROL NAME", "CONTROL FORM", "CONTROL TYPE", "VISIBLE" }, false, false);        
        public OwnersRights()
        {
            base.Maximized = FormWindowState.Maximized;
            InitializeComponent();
        }

        private void OwnersRights_Load(object sender, EventArgs e)
        {
            dataGrid1.dataGridView.ReadOnly = false;
            dataGrid1.toolStrip1.Visible = false;
            FillCombos();
            try
            {
                DataSet ds = new DataAccess(CommandType.StoredProcedure, "CONTROLLSsp_select_by_parent_controll_id", new object[] { new MySqlParameter("_PARENT_CONTROLL_ID", null) }).ExecuteSelectQuery();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TreeNode root = new TreeNode(ds.Tables[0].Rows[i]["caption"].ToString());
                    root.Tag = ds.Tables[0].Rows[i];
                    CreateNode(root);
                    treeView1.Nodes.Add(root);
                }
            }
            catch { }
        }

        private void FillCombos()
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "OWNERSsp_list");
            DataTable dtOwners = da.ExecuteSelectQuery().Tables[0];
            if (dtOwners != null)
            {
                comboBoxOwner.DisplayMember = "name";
                comboBoxOwner.ValueMember = "id";
                comboBoxOwner.DataSource = dtOwners;
            }
        }


        void CreateNode(TreeNode node)
        {
            try
            {
                DataSet ds = new DataAccess(CommandType.StoredProcedure, "CONTROLLSsp_select_by_parent_controll_id", new object[] { new MySqlParameter("_PARENT_CONTROLL_ID", ((DataRow)node.Tag)["ID"]) }).ExecuteSelectQuery();
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TreeNode tnode = new TreeNode(ds.Tables[0].Rows[i]["caption"].ToString());
                    tnode.Tag = ds.Tables[0].Rows[i];
                    node.Nodes.Add(tnode);
                    CreateNode(tnode);
                }
            }
            catch { }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckAllChildren(e.Node);
        }

        private void CheckAllChildren(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = node.Checked;
                CheckAllChildren(child);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTROLLS_OWNERS_RIGHTSsp_delete_by_owner_id", new object[] { new MySqlParameter("_OWNER_ID", CommonFunctions.SetNullable(comboBoxOwner)) });
                da.ExecuteNonQuery();
                foreach (TreeNode node in treeView1.Nodes)
                {
                    InsertRight(node);
                }
                base.ShowConfirmationDialog(Language.GetMessageBoxText("dataSaved", "Information was saved successfully!"));
            }
            catch { }
        }

        private void InsertRight(TreeNode node)
        {
            DataAccess da = new DataAccess(CommandType.StoredProcedure, "CONTROLLS_OWNERS_RIGHTSsp_insert", new object[]{
                new MySqlParameter("_OWNER_ID", CommonFunctions.SetNullable(comboBoxOwner)),
                new MySqlParameter("_CONTROLL_ID", ((DataRow)node.Tag)["id"]),
                new MySqlParameter("_VISIBLE", node.Checked)
            });
            da.ExecuteInsertQuery();
            foreach (TreeNode child in node.Nodes)
                InsertRight(child);
        }

        private void comboBoxOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ((BindingSource)dataGrid1.dataGridView.DataSource).Filter = String.Format("owner_id = {0}", comboBoxOwner.SelectedValue);
                foreach (TreeNode node in treeView1.Nodes)
                {
                    DataRow dr = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).Select(String.Format("owner_id={0} and controll_id={1}", comboBoxOwner.SelectedValue.ToString(), ((DataRow)node.Tag)["id"].ToString()))[0];
                    node.Checked = Convert.ToBoolean(dr["visible"]);
                    CheckVisible(node);
                }
            }
            catch { }
        }

        private void CheckVisible(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                DataRow dr = ((DataTable)((BindingSource)dataGrid1.dataGridView.DataSource).DataSource).Select(String.Format("owner_id={0} and controll_id={1}", comboBoxOwner.SelectedValue.ToString(), ((DataRow)child.Tag)["id"].ToString()))[0];
                child.Checked = Convert.ToBoolean(dr["visible"]);
                CheckVisible(child);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
