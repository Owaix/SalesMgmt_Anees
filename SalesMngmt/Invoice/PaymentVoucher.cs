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
using System.Data.Entity;
using Lib.Reporting;
using Lib.Model;

namespace SalesMngmt.Invoice
{
    public partial class PaymentVoucher : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.PaymentVoucharIndex_Result> list = null;
        int compID = 0;
        int obj = 0;
        int AcCode = 0;
        double Amt = 0;
        DateTime dt = DateTime.Now;
        int ChkNO = 0;
        int Narr = 0;
        int CustomerCode = 0;




        public PaymentVoucher(int company)
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
            bindData();
        }

        public void bindData()
        {


            List<COA_D> coa = new List<COA_D>();
            coa.Add(new COA_D { AC_Code = 0, AC_Title = "--SELECT--" });
            coa.AddRange(db.COA_D.AsNoTracking().Where(x => x.CompanyID == compID && x.InActive == false && x.CAC_Code == 1 || x.CAC_Code == 2).ToList());
            FillCombo(cmbxAccount, coa, "AC_Title", "AC_Code", 0);

            //List<Vendors> customer = new List<Vendors>();
            //customer.Add(new Vendors { AC_Code = 0, VendName = "--SELECT--" });
            //customer.AddRange(db.Vendors.AsNoTracking().Where(x => x.CompanyID == compID && x.InActive == false).ToList());
            //FillCombo(cmbxCustomer1, customer, "VendName", "AC_Code", 0);

            List<CustomerList> customerList = new List<CustomerList>();
            customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
            var aa = ReportsController.getVendorList(compID);

            foreach (DataRow row in aa.Rows)
            {
                customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
            }

            FillCombo(cmbxCustomer, customerList, "Name", "AC_Code", 0);
            //var customerCount = db.Vendors.Where(x => x.CompanyID == compID  && x.InActive==false).FirstOrDefault();
            //var AccountCount = db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID).FirstOrDefault();
            //FillCombo(cmbxCustomer, db.Vendors.Where(x => x.CompanyID == compID && x.InActive == false).ToList(), "VendName", "AC_Code", 0/*Convert.ToInt32(customerCount.AC_Code)*/);
            //FillCombo(cmbxAccount, db.COA_D.Where(x => x.CAC_Code == 1 || x.CAC_Code == 2 && x.InActive == false && x.CompanyID == compID).ToList(), "AC_Title", "AC_Code", 0 /*AccountCount.AC_Code*/);

            pnlMain.Hide();
            // var MyNewDateValue = DateTime.Today.AddDays(-10);
            var list1 = ReportsController.PaymentVoucharIndex(DateTime.Today.AddDays(-10), DateTime.Now, compID);
            paymentVoucharIndexResultBindingSource.DataSource = list1;


        }
        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected)
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
            pnlMain.Hide();
            //  Lib.Entity.ArticleType us = (Lib.Entity.ArticleType)articleTypeBindingSource.Current;
            obj = 0;
            if (obj == 0)
            {
                clear();
                var list2 = ReportsController.PaymentVoucharIndex(dtTo.Value, dtFrom.Value, compID);
                paymentVoucharIndexResultBindingSource.DataSource = list2;
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}

        public void clear()
        {
            txtNarr.Text = "";
            txtChkNo.Text = "";
            dtTranscation.Value = DateTime.Now;

            List<COA_D> coa = new List<COA_D>();
            coa.Add(new COA_D { AC_Code = 0, AC_Title = "--SELECT--" });
            coa.AddRange(db.COA_D.AsNoTracking().Where(x => x.CompanyID == compID && x.InActive == false && x.CAC_Code == 1 || x.CAC_Code == 2).ToList());
            FillCombo(cmbxAccount, coa, "AC_Title", "AC_Code", 0);

            //List<Vendors> customer = new List<Vendors>();
            //customer.Add(new Vendors { AC_Code = 0, VendName = "--SELECT--" });
            //customer.AddRange(db.Vendors.AsNoTracking().Where(x => x.CompanyID == compID && x.InActive == false).ToList());
            //FillCombo(cmbxCustomer1, customer, "VendName", "AC_Code", 0);

            List<CustomerList> customerList = new List<CustomerList>();
            customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
            var aa = ReportsController.getVendorList(compID);

            foreach (DataRow row in aa.Rows)
            {
                customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
            }

            FillCombo(cmbxCustomer, customerList, "Name", "AC_Code", 0);


            dtCheckDate.Value = DateTime.Now;
            //chkIsActive.Checked = false;
            txtAmount.Text = "";



        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int code = Convert.ToInt32(cmbxAccount.SelectedValue);
            int customer = Convert.ToInt32(CustomerCode);

            if (code == 0)
            {

                MessageBox.Show("Please select Account");
                return;

            }

            else if (customer == 0)
            {


                MessageBox.Show("Please select vendor");
                return;

            }



            if (obj == 0)
            {

                DbContextTransaction transaction = db.Database.BeginTransaction();


                var id = ReportsController.sp_PV_M_Insert(compID, dtTranscation.Value, code, false);//db.sp_PV_M_Insert(0, dtTranscation.Value, code, 0, "0").FirstOrDefault();
                PV_D PV = new PV_D();
                PV.RID = Convert.ToInt32(id.Rows[0][0]);
                PV.Narr = txtNarr.Text;
                PV.MOP_ID = 7;
                PV.AC_Code = customer;
                PV.ChkNo = txtChkNo.Text.DefaultZero();
                PV.Amt = Convert.ToDouble(txtAmount.Text.DefaultZero());
                PV.checkDate = dtCheckDate.Value;

                db.PV_D.Add(PV);
                //    db.sp_PV_D_Insert(Convert.ToInt32(id), txtNarr.Text, 7, customer, 1, Convert.ToInt32(txtChkNo.Text ),1, 0, Convert.ToDouble(txtAmount.Text.DefaultZero()), 0, 0, 0, 0, dtCheckDate.Value);// int a = Convert.ToInt32(id);

                ReportsController.sp_PV_GL_Debit(15, customer, code, txtNarr.Text, Convert.ToDouble(txtAmount.Text.DefaultZero()), 0, Convert.ToInt32(id.Rows[0][0]), dtTranscation.Value, compID);
                ReportsController.sp_RV_GL_credit(15, code, customer, txtNarr.Text, 0, Convert.ToDouble(txtAmount.Text.DefaultZero()), Convert.ToInt32(id.Rows[0][0]), dtTranscation.Value, compID);

                db.SaveChanges();
                transaction.Commit();
            }
            else
            {
                DbContextTransaction transaction = db.Database.BeginTransaction();

                ReportsController.sp_PV_M_Update(code, dtTranscation.Value, compID, obj);

                ReportsController.sp_PV_D_Update(txtNarr.Text, 0, customer, 0, Convert.ToInt32(txtChkNo.Text), 0, 0, Convert.ToDouble(txtAmount.Text.DefaultZero()), 0, 0, dtCheckDate.Value, obj);


                var all = db.GL.Where(x => x.TypeCode == 15 && x.RID == obj && x.CompID == compID);
                db.GL.RemoveRange(all);
                db.SaveChanges();


                ReportsController.sp_PV_GL_Debit(15, customer, code, txtNarr.Text, Convert.ToDouble(txtAmount.Text.DefaultZero()), 0, Convert.ToInt32(obj), dtTranscation.Value, compID);
                ReportsController.sp_RV_GL_credit(15, code, customer, txtNarr.Text, 0, Convert.ToDouble(txtAmount.Text.DefaultZero()), Convert.ToInt32(obj), dtTranscation.Value, compID);


                transaction.Commit();

            }

            var list1 = ReportsController.PaymentVoucharIndex(dtTo.Value, dtFrom.Value, compID);
            paymentVoucharIndexResultBindingSource.DataSource = list1;

    

            clear();
            label3.Text = "ADD";
            obj = 0;

            MessageBox.Show("Entry Suceessfully Saved");
            cmbxCustomer.Focus();
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

            obj = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[0].Value.DefaultZero());
            dtTranscation.Value = Convert.ToDateTime(CategorysDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
            cmbxAccount.SelectedValue = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[3].Value);
            txtAmount.Text = Convert.ToDouble(CategorysDataGridView.Rows[e.RowIndex].Cells[16].Value).ToString();
            dtCheckDate.Value = Convert.ToDateTime(CategorysDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString());
            txtChkNo.Text = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[21].Value).ToString();
            txtNarr.Text = CategorysDataGridView.Rows[e.RowIndex].Cells[25].Value.ToString();
            cmbxCustomer.SelectedValue = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[29].Value);



        }

        public void Payment(int a)
        {



            obj = a;
            PV_M rv_m = new PV_M();
            PV_D rv_d = new PV_D();

            rv_m = db.PV_M.Where(x => x.RID == a).FirstOrDefault();
            rv_d = db.PV_D.Where(x => x.RID == a).FirstOrDefault();

            dtTranscation.Value = Convert.ToDateTime(rv_m.EDate);
            cmbxAccount.SelectedValue = Convert.ToInt32(rv_m.AC_Code);
            txtAmount.Text = Convert.ToDouble(rv_d.Amt).ToString();
            dtCheckDate.Value = Convert.ToDateTime(rv_d.checkDate.ToString());
            txtChkNo.Text = Convert.ToInt32(rv_d.ChkNo).ToString();
            txtNarr.Text = rv_d.Narr.ToString();
            cmbxCustomer.SelectedValue = Convert.ToInt32(rv_d.AC_Code);
            // txtDiscount.Text = Convert.ToDouble(rv_d.DisAmt).ToString();


            pnlMain.Show();
            //txtChkNo.Focus();
            label3.Text = "EDIT";




        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            //list = db.PaymentVoucharIndex(dtTo.Value, dtFrom.Value).ToList();
            //paymentVoucharIndexResultBindingSource.DataSource = list;

            var list1 = ReportsController.PaymentVoucharIndex(dtTo.Value, dtFrom.Value, compID);
            paymentVoucharIndexResultBindingSource.DataSource = list1;











        }

        private void txtChkNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtNarr_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cmbxUnit_Click(object sender, EventArgs e)
        {

        }

       

        private void cmbxCustomer_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbxCustomer.SelectedIndex == 0)
            {
                lblAmount.Text = "0";
            }

            else
            {


                char[] spearator = { '/' };

                // using the method
                String[] strlist = cmbxCustomer.Text.Split(spearator);
                var dd = strlist.Length;
                if (strlist.Length == 3) { }


                else
                {
                    CustomerCode = Convert.ToInt32(0);
                    cmbxCustomer.Text = "";
                    cmbxCustomer.Focus();

                    return;
                }

                string accode = "";
                foreach (String s in strlist)
                {
                    accode = s;
                }

                if (accode == "0" || accode == "" || accode == null)
                {
                    CustomerCode = Convert.ToInt32(0);
                    cmbxCustomer.Text = "";
                    cmbxCustomer.Focus();
                    return;


                }
                int code = Convert.ToInt32(accode);
                CustomerCode = Convert.ToInt32(accode);
                var customer = db.Vendors.AsNoTracking().Where(x => x.AC_Code == code && x.CompanyID == compID).FirstOrDefault();
                if (customer == null)
                {

                    MessageBox.Show("you selected wrong customer");

                    cmbxCustomer.Focus();



                }


                else
                {

                    int Vendorcode = Convert.ToInt32(CustomerCode);

                    double balance = 0;

                    var previosBalance = ReportsController.getVendorPreviousBalance(dtTranscation.Value, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = credit - debit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getVendorLedgerSummaryByDate(dtTranscation.Value, dtTranscation.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                    foreach (DataRow rows in getdata.Rows)
                    {


                        balance = balance + (double)rows["credit"];
                        balance = balance - (double)rows["debit"];






                    }


                    lblAmount.Text = balance.ToString();
                }


            }

        }

        private void cmbxCustomer_Leave(object sender, EventArgs e)
        {
            if (cmbxCustomer.SelectedIndex == 0)
            {
                lblAmount.Text = "0";
            }

            else
            {


                char[] spearator = { '/' };

                // using the method
                String[] strlist = cmbxCustomer.Text.Split(spearator);
                var dd = strlist.Length;
                if (strlist.Length == 3) { }


                else
                {
                    CustomerCode = Convert.ToInt32(0);
                    cmbxCustomer.Text = "";
                    cmbxCustomer.Focus();

                    return;
                }

                string accode = "";
                foreach (String s in strlist)
                {
                    accode = s;
                }

                if (accode == "0" || accode == "" || accode == null)
                {
                    CustomerCode = Convert.ToInt32(0);
                    cmbxCustomer.Text = "";
                    cmbxCustomer.Focus();
                    return;


                }
                int code = Convert.ToInt32(accode);
                CustomerCode = Convert.ToInt32(accode);
                var customer = db.Vendors.AsNoTracking().Where(x => x.AC_Code == code && x.CompanyID == compID).FirstOrDefault();
                if (customer == null)
                {

                    MessageBox.Show("you selected wrong customer");

                    cmbxCustomer.Focus();



                }


                else
                {

                    int Vendorcode = Convert.ToInt32(CustomerCode);

                    double balance = 0;

                    var previosBalance = ReportsController.getVendorPreviousBalance(dtTranscation.Value, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = credit - debit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getVendorLedgerSummaryByDate(dtTranscation.Value, dtTranscation.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                    foreach (DataRow rows in getdata.Rows)
                    {


                        balance = balance + (double)rows["credit"];
                        balance = balance - (double)rows["debit"];






                    }


                    lblAmount.Text = balance.ToString();
                }


            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
   (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
   

}
