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
using SalesMngmt.Utility;

namespace SalesMngmt.Reporting
{
    public partial class SaleList : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.RecivedVoucharIndex_Result> list = null;
        int compID = 0;
        int obj = 0;
        int AcCode = 0;
        double Amt = 0;
        DateTime dt = DateTime.Now;
        int ChkNO =0;
        int Narr = 0;
        int CustomerCode = 0;




        public SaleList()
        {
            InitializeComponent();

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
            var customerCount = db.Customers.Where(x => x.CompanyID == 0).FirstOrDefault();
            var AccountCount = db.COA_D.Where(x => x.CAC_Code == 1).FirstOrDefault();
           

            pnlMain.Hide();
           // var MyNewDateValue = DateTime.Today.AddDays(-10);
         
        }
        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected )
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
           // txtChkNo.Focus();
            label3.Text = "ADD";
            obj = 0;
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            if (obj == 0)
            {

                MessageBox.Show("Select any row first");

            }
            else
            {

                //var tbl = db.Articles.Where(x => x.ProductID == obj).FirstOrDefault();

                ////txtArticalNo.Text = tbl.ArticleNo;
                //txtChkNo.Text = tbl.ProductName;
                //cmbxAccount.SelectedValue = tbl.ArticleTypeID;
                //cmbxCustomer.SelectedValue = tbl.StyleID;
                //chkIsActive.Checked = (bool)tbl.IsDelete;


                pnlMain.Show();
                //txtChkNo.Focus();
                label3.Text = "EDIT";
            }



        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
            lblDiscount.Visible = true;
            lblTotal.Visible = true;
            pnlMain.Hide();
            //  Lib.Entity.ArticleType us = (Lib.Entity.ArticleType)articleTypeBindingSource.Current;
            obj = 0;
            if (obj == 0)
            {
                dataGridView1.Rows.Clear();
              
            }
           
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}

        public void clear()
        {
          



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
            pnlMain.Show();
            dataGridView1.Rows.Clear();
            var abc=   Convert.ToInt32( CategorysDataGridView.Rows[e.RowIndex].Cells[0].Value);
            var detail = db.Sales_D.AsNoTracking().Where(x => x.RID == abc);

            int a = 1;
            double DiscRs = 0;
            double DiscPenCentage = 0;
            double Total = 0;
            foreach (var loop in detail) {

                var name = db.Items.AsNoTracking().Where(x => x.IID == loop.IID).FirstOrDefault();
                dataGridView1.Rows.Add(a, name.IName, loop.CTN, loop.PCS, loop.SalesPriceP, loop.DisP, loop.DisRs, loop.Amt);

                DiscRs +=Convert.ToDouble( loop.DisRs);
                DiscPenCentage += Convert.ToDouble(loop.DisP); 
                Total += Convert.ToDouble(loop.Amt) ;

            }
            lblDetailDisc.Text = DiscRs.ToString();
            lblDetailDiscPercentage.Text = DiscPenCentage.ToString();
            lblDetailToTal.Text = Total.ToString();

            lblDiscount.Visible = false;
            lblTotal.Visible = false;

        }

        private void CategorysDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

         

        }

      
        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CategorysDataGridView.Rows.Clear();
            var Record = db.SaleList(dtTo.Value, dtFrom.Value);

            double Disount = 0;
            double Total = 0;
            foreach (var List in Record) {
             

                CategorysDataGridView.Rows.Add(List.RID, List.AC_Title, List.DisAmt, List.TotalAmount, List.InvNo, List.EDate);

                Disount += Convert.ToInt32(List.DisAmt);
                Total += Convert.ToInt32(List.TotalAmount);


            }
            lblDiscount.Text = Disount.ToString();
            lblTotal.Text = Total.ToString();













        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }










    //{
    //    public ReceiveVoucher()
    //    {
    //        InitializeComponent();
    //    }
    //}
}
