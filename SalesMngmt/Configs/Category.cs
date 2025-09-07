using Lib;
using Lib.Entity;
using Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Categories : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Items_Cat> list = null;
        int compID = 0;

        public Categories(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;
        }

        private void Category_Load(object sender, EventArgs e)
        {
            //cat type = new cat();
            //DataAccess access = new DataAccess();
            //type.PType_ID = 1;
            //var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            //con.Open();
            //SqlTransaction trans = con.BeginTransaction();
            //var objcat = access.Get<cat>("Sp_cat_Select", type, con, trans);

            pnlMain.Hide();
            list = db.Items_Cat.Where(x => x.CompanyID == compID).ToList();
            CategoryBindingSource.DataSource = list;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            CategoryBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtcat.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            Items_Cat obj = (Items_Cat)CategoryBindingSource.Current;
            pnlMain.Show();
            txtcat.Focus();
            label3.Text = "EDIT";
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            Items_Cat us = (Items_Cat)CategoryBindingSource.Current;
            if (us.CatID == 0)
            {
                CategoryBindingSource.RemoveCurrent();
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtcat.Text == "")
            { MessageBox.Show("Please Provide Party Type", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                Items_Cat obj = (Items_Cat)CategoryBindingSource.Current;

                var Currentobj = list.Find(x => x.Cat == txtcat.Text.Trim());
                if (obj.CatID == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("Category Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.Cat == obj.Cat &&
                                         record.CatID != obj.CatID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Category Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                obj.Cat = txtcat.Text.Trim();
                obj.isDeleted = chkIsActive.Checked;
                if (obj.CatID == 0)
                {
                    db.Items_Cat.Add(obj);
                }
                else
                {
                    var result = db.Items_Cat.SingleOrDefault(b => b.CatID == obj.CatID);
                    if (result != null)
                    {
                        result.Cat = txtcat.Text.Trim();
                        result.isDeleted = chkIsActive.Checked;
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();
            }
        }
        private void catDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CategorysDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #region -- Helper Method Start --
        public void GetDocCode()
        {
            //catList obj = new catList(new cat { }.Select());
            //docCode = "DOC-" + (obj.Count + 1);
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { CategorysDataGridView.DataSource = list; }
                else
                {
                    CategorysDataGridView.DataSource = list.FindAll(x => x.Cat.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //
        #endregion -- Helper Method End --
    }
}
