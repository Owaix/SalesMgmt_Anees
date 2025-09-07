using Lib.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class ArticleType : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.ArticleTypes> list = null;
        int compID = 0;



        public ArticleType(int company)
        {
            InitializeComponent();
            compID = company;
            db = new SaleManagerEntities();
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
            list = db.ArticleTypes.Where(x=>x.CompanyID == compID).ToList();
            articleTypeBindingSource.DataSource = list;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            articleTypeBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtcat.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            Lib.Entity.ArticleTypes obj = (Lib.Entity.ArticleTypes)articleTypeBindingSource.Current;
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
            Lib.Entity.ArticleTypes us = (Lib.Entity.ArticleTypes)articleTypeBindingSource.Current;
            if (us.ArticleTypeID == 0)
            {
                articleTypeBindingSource.RemoveCurrent();
                list = db.ArticleTypes.Where(x => x.CompanyID == compID).ToList();
                articleTypeBindingSource.DataSource = list;
            }
            else
            {

                articleTypeBindingSource.Clear();
                var listcancel = db.ArticleTypes.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                articleTypeBindingSource.DataSource = listcancel;
            }

        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtcat.Text == "")
            { MessageBox.Show("Please Provide ArticalType", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                Lib.Entity.ArticleTypes obj = (Lib.Entity.ArticleTypes)articleTypeBindingSource.Current;

                var Currentobj = db.ArticleTypes.ToList().Find(x => x.ArticleTypeName == txtcat.Text.Trim() && x.CompanyID == compID);// list.Find(x => x.Name == txtcat.Text.Trim());
                if (obj.ArticleTypeID == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("This Artical Type Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.ArticleTypeName == obj.ArticleTypeName &&
                                         record.ArticleTypeID != obj.ArticleTypeID
                                         && record.CompanyID == compID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Artical Type Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                obj.ArticleTypeName = txtcat.Text.Trim();
                obj.IsDeleted = chkIsActive.Checked;
                obj.CompanyID = compID;
                if (obj.ArticleTypeID == 0)
                {
                    db.ArticleTypes.Add(obj);
                }
                else
                {
                    var result = db.ArticleTypes.SingleOrDefault(b => b.ArticleTypeID == obj.ArticleTypeID && b.CompanyID == compID);
                    if (result != null)
                    {
                        result.ArticleTypeName = txtcat.Text.Trim();
                        result.IsDeleted = chkIsActive.Checked;
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();

            
                list = db.ArticleTypes.Where(x=>x.CompanyID == compID).ToList();
                articleTypeBindingSource.DataSource = list;
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
                    CategorysDataGridView.DataSource = list.FindAll(x => x.ArticleTypeName.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //
        #endregion -- Helper Method End --

        private void CategorysDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
    }








}
