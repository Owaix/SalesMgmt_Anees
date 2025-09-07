using Lib.Entity;
using Lib.Reporting;
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
    public partial class Artical : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.sp_getArticle_Result> list = null;
        int compID = 0;
        int obj = 0;



        public Artical(int company)
        {
            InitializeComponent();

            db = new SaleManagerEntities();
            compID = company;
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

            FillCombo(cmbxStyle, db.Styles.AsNoTracking().Where(x=>x.CompanyID==compID && x.IsDeleted==false).ToList(), "StyleName", "StyleID", 1);
            FillCombo(cmbxArticalType, db.ArticleTypes.AsNoTracking().Where(x => x.CompanyID == compID && x.IsDeleted == false).ToList(), "ArticleTypeName", "ArticleTypeID", 1);

            pnlMain.Hide();
            //list = db.sp_getArticle().ToList();
             DataTable list1 = ReportsController.sp_getArticle(compID);

            spgetArticleResultBindingSource.DataSource = list1;
        }
        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected = 1)
        {
            comboBox.DataSource = obj;
            comboBox.DisplayMember = Name; // Column Name
            comboBox.ValueMember = ID;  // Column Name
            comboBox.SelectedValue = selected;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            clear();
            pnlMain.Show();
            GetDocCode();
            txtName.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            if (obj == 0)
            {

                MessageBox.Show("Select any row first");

            }
            else
            {

                var tbl = db.Article.Where(x => x.ProductID == obj && x.CompanyID==compID).FirstOrDefault();

                txtArticalNo.Text = tbl.ArticleNo;
                txtName.Text = tbl.ProductName;
                cmbxArticalType.SelectedValue = tbl.ArticleTypeID;
                cmbxStyle.SelectedValue = tbl.StyleID;
                chkIsActive.Checked = (bool)tbl.IsDelete;


                pnlMain.Show();
                txtName.Focus();
                label3.Text = "EDIT";
            }

            
          
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            //  Lib.Entity.ArticleType us = (Lib.Entity.ArticleType)articleTypeBindingSource.Current;
            obj = 0;
            if (obj == 0)
            {
                clear();
                DataTable list1 = ReportsController.sp_getArticle(compID);

                spgetArticleResultBindingSource.DataSource = list1;
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}

        public void clear() {
            txtArticalNo.Text = "";
            txtName.Text = "";

            FillCombo(cmbxStyle, db.Styles.AsNoTracking().Where(x => x.CompanyID == compID && x.IsDeleted == false).ToList(), "StyleName", "StyleID", 1);
            FillCombo(cmbxArticalType, db.ArticleTypes.AsNoTracking().Where(x => x.CompanyID == compID && x.IsDeleted == false).ToList(), "ArticleTypeName", "ArticleTypeID", 1);


            chkIsActive.Checked = false;




        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtArticalNo.Text == "")
            { MessageBox.Show("Please Provide ArticleNo", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
               // Lib.Entity.ArticleType obj = (Lib.Entity.ArticleType)articleTypeBindingSource.Current;

               var Currentobj = db.Article.ToList().Find(x => x.ArticleNo == txtArticalNo.Text.Trim() && x.CompanyID==compID);// list.Find(x => x.Name == txtcat.Text.Trim());
                if (obj == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("Artical Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    var articalTbl= db.Article.Where(x => x.ProductID == obj).FirstOrDefault();
                    bool isCodeExist = db.Article.Any(record =>
                                         record.ArticleNo == txtArticalNo.Text &&
                                         record.ProductID != articalTbl.ProductID
                                         && record.CompanyID==compID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Artical Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                Article ar = new Article();

                ar.ArticleTypeID = Convert.ToInt32( cmbxArticalType.SelectedValue);//txtcat.Text.Trim();
                ar.StyleID = Convert.ToInt32(cmbxStyle.SelectedValue);
                ar.ProductName = txtName.Text;
                ar.ArticleNo = txtArticalNo.Text;
                ar.IsDelete = chkIsActive.Checked;
                ar.CompanyID = compID;

                if (obj == 0)
                {
                    db.Article.Add(ar);
                }
                else
                {

                    var articalTbl = db.Article.Where(x => x.ProductID == obj  && x.CompanyID==compID).FirstOrDefault();

                    var result = db.Article.SingleOrDefault(b => b.ProductID == articalTbl.ProductID && b.CompanyID == compID);
                    if (result != null)
                    {


                        result.ArticleTypeID = Convert.ToInt32(cmbxArticalType.SelectedValue);//txtcat.Text.Trim();
                        result.StyleID = Convert.ToInt32(cmbxStyle.SelectedValue);
                        result.ProductName = txtName.Text;
                        result.ArticleNo = txtArticalNo.Text;
                        result.IsDelete = chkIsActive.Checked;

                    }
                }
                db.SaveChanges();
                pnlMain.Hide();
                obj = 0;
                //list = db.sp_getArticle(compID).ToList();
                //spgetArticleResultBindingSource.DataSource = list;

                DataTable list1 = ReportsController.sp_getArticle(compID);

                spgetArticleResultBindingSource.DataSource = list1;

                //list = db.ArticleTypes.ToList();
                //articleTypeBindingSource.DataSource = list;
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
                   // CategorysDataGridView.DataSource = list.FindAll(x => x.ArticleType1.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
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
       //  var abc=   CategorysDataGridView.Rows[e.RowIndex].Cells[0].Value;
        }

        private void CategorysDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           obj = Convert.ToInt32( CategorysDataGridView.Rows[e.RowIndex].Cells[7].Value);
        }
    }










    //    public Artical()
    //    {
    //        InitializeComponent();
    //    }
    //}
}
