using DIagnoseMgmt;
using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class ReceiveVoucher : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.RecivedVoucharIndex_Result> list = null;
        int compID = 0;
        int obj = 0;
        int AcCode = 0;
        double Amt = 0;
        DateTime dt = DateTime.Now;
        int ChkNO = 0;
        int Narr = 0;
        int CustomerCode = 0;




        public ReceiveVoucher(int company)
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

            //List<Customers> customer = new List<Customers>();
            //customer.Add(new Customers { AC_Code = 0, CusName = "--SELECT--" });
            //customer.AddRange(db.Customers.AsNoTracking().Where(x => x.CompanyID == compID && x.InActive == false).ToList());
            //FillCombo(cmbxCustomer1, customer, "CusName", "AC_Code", 0);


            List<CustomerList> customerList = new List<CustomerList>();
            customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
            var aa = ReportsController.getcustumerList(compID);

            foreach (DataRow row in aa.Rows)
            {
                customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
            }

            FillCombo(cmbxCustomer, customerList, "Name", "AC_Code", 0);

            List<COA_D> coa = new List<COA_D>();
            coa.Add(new COA_D { AC_Code = 0, AC_Title = "--SELECT--" });
            coa.AddRange(db.COA_D.AsNoTracking().Where(x => x.CompanyID == compID && x.InActive == false && x.CAC_Code == 1 || x.CAC_Code == 2).ToList());
            FillCombo(cmbxAccount, coa, "AC_Title", "AC_Code", 0);



            List<tbl_Employee> employee = new List<tbl_Employee>();
            employee.Add(new tbl_Employee { ID = 0, EmployeName = "--SELECT--" });
            employee.AddRange(db.tbl_Employee.AsNoTracking().Where(x => x.companyID == compID && x.isDeleted == false).ToList());
            FillCombo(cmbxEmployee, employee, "EmployeName", "ID", 0);



            //  FillCombo(cmbxCustomer, db.Customers.Where(x => x.CompanyID == compID && x.InActive == false).ToList(), "CusName", "AC_Code", 0);
            //  FillCombo(cmbxAccount, db.COA_D.Where(x => x.CAC_Code == 1 || x.CAC_Code == 2 && x.CompanyID == compID && x.InActive == false).ToList(), "AC_Title", "AC_Code", 0);

            pnlMain.Hide();
            // var MyNewDateValue = DateTime.Today.AddDays(-10);
            var list1 = ReportsController.RecivedVoucharIndex(DateTime.Today.AddDays(-10), DateTime.Now, compID);
            recivedVoucharIndexResultBindingSource.DataSource = list1;

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
                var list = ReportsController.RecivedVoucharIndex(DateTime.Today.AddDays(-10), DateTime.Now, compID);
                recivedVoucharIndexResultBindingSource.DataSource = list;
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
            txtDiscount.Text = "";
            dtTranscation.Value = DateTime.Now;

            //List<Customers> customer = new List<Customers>();
            //customer.Add(new Customers { AC_Code = 0, CusName = "--SELECT--" });
            //customer.AddRange(db.Customers.AsNoTracking().Where(x => x.CompanyID == compID && x.InActive == false).ToList());
            //FillCombo(cmbxCustomer1, customer, "CusName", "AC_Code", 0);


            List<COA_D> coa = new List<COA_D>();
            coa.Add(new COA_D { AC_Code = 0, AC_Title = "--SELECT--" });
            coa.AddRange(db.COA_D.AsNoTracking().Where(x => x.CompanyID == compID && x.InActive == false && x.CAC_Code == 1 || x.CAC_Code == 2).ToList());
            FillCombo(cmbxAccount, coa, "AC_Title", "AC_Code", 0);

            List<CustomerList> customerList = new List<CustomerList>();
            customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
            var aa = ReportsController.getcustumerList(compID);

            foreach (DataRow row in aa.Rows)
            {
                customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
            }

            FillCombo(cmbxCustomer, customerList, "Name", "AC_Code", 0);
            dtCheckDate.Value = DateTime.Now;
            //chkIsActive.Checked = false;
            txtAmount.Text = "";
            lblAmount.Text = "";

            List<tbl_Employee> employee = new List<tbl_Employee>();
            employee.Add(new tbl_Employee { ID = 0, EmployeName = "--SELECT--" });
            employee.AddRange(db.tbl_Employee.AsNoTracking().Where(x => x.companyID == compID && x.isDeleted == false).ToList());
            FillCombo(cmbxEmployee, employee, "EmployeName", "ID", 0);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            int code = Convert.ToInt32(cmbxAccount.SelectedValue);
            int customer = Convert.ToInt32(CustomerCode);

            if (code == 0)
            {

                MessageBox.Show("Please select Account");
                cmbxAccount.Focus();
                return;

            }

            else if (customer == 0)
            {


                MessageBox.Show("Please select customer");
                cmbxCustomer.Focus();
                return;

            }
            txtDiscount.Text = Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text).ToString();
            int employeeId = Convert.ToInt32(cmbxEmployee.SelectedValue);
            if (obj == 0)
            {

                DbContextTransaction transaction = db.Database.BeginTransaction();

             

                var id = ReportsController.sp_RV_M_Insert(compID, dtTranscation.Value, code, false, employeeId);
                RV_D rv = new RV_D();
                rv.RID = Convert.ToInt32(id.Rows[0][0]);
                rv.Narr = txtNarr.Text;
                rv.MOP_ID = 7;
                rv.AC_Code = customer.ToString();
                rv.ChkNo = Convert.ToInt32(txtChkNo.Text.DefaultZero());
                rv.DisAmt = Convert.ToDouble(txtDiscount.Text.DefaultZero());
                rv.Amt = Convert.ToDouble(txtAmount.Text.DefaultZero());
                rv.checkDate = dtCheckDate.Value;
              
                db.RV_D.Add(rv);

                GL gl = new GL();
                gl.TypeCode = 14;
                gl.SID = employeeId;
                gl.AC_Code = code;
                gl.AC_Code2 = Convert.ToInt32(customer);
                gl.Narration = txtNarr.Text;
                gl.Debit = Convert.ToDouble(txtAmount.Text.DefaultZero());
                gl.Credit = 0;
                gl.RID = Convert.ToInt32(id.Rows[0][0]);
                gl.CompID = compID;
                gl.GLDate = dtTranscation.Value;
                gl.MOP_ID = 7;
                db.GL.Add(gl);


                GL gl2 = new GL();
                gl2.TypeCode = 14;
                gl2.AC_Code = Convert.ToInt32(customer);// customer;
                gl2.AC_Code2 = code;
                gl2.Narration = txtNarr.Text;
                gl2.Debit = 0;
                gl2.Credit = Convert.ToDouble(txtAmount.Text.DefaultZero());
                gl2.RID = Convert.ToInt32(id.Rows[0][0]);
                gl2.GLDate = dtTranscation.Value;
                gl2.MOP_ID = 7;
                gl2.SID = employeeId;
                gl2.CompID = compID;
                db.GL.Add(gl2);

                //if (txtDiscount.Text.Equals("") || txtDiscount.Text.Equals("0")) {

                //}
                //else
                //{

                //    var Expense = db.COA_D.Where(x => x.AC_Code == 16000001).FirstOrDefault();
                //    var customerDetial = db.COA_D.Where(x => x.AC_Code == customer).FirstOrDefault();
                //    db.sp_RV_GL_credit(14, Expense.AC_Code, customer, customerDetial.AC_Title, Convert.ToDouble(txtDiscount.Text.DefaultZero()), 0, Convert.ToInt32(id), dtTranscation.Value);
                //    db.sp_RV_GL_Debit(14, customer, code, Expense.AC_Title, 0, Convert.ToDouble(txtDiscount.Text.DefaultZero()), Convert.ToInt32(id), dtTranscation.Value);
                //}
                db.SaveChanges();
                transaction.Commit();
            }
            else
            {
                DbContextTransaction transaction = db.Database.BeginTransaction();
                //  ReportsController.sp_RV_M_Update(compID, dtTranscation.Value, code, obj , employeeId);

                var record =  db.RV_M.FirstOrDefault(r => r.RID == obj);
                if (record != null)
                {
                    // Update the entity's properties
                    record.CompID = compID;
                    record.EDate = dtTranscation.Value;
                    record.AC_Code = code.ToString();
                    record.SID = employeeId;
                 //   record.Rem = rem;

                    // Save the changes to the database
                  db.SaveChanges();
                }


                var rv_D= db.RV_D.Where(x =>  x.RID == obj);
                db.RV_D.RemoveRange(rv_D);
                db.SaveChanges();

                //  db.sp_RV_D_Update(obj, txtNarr.Text, 7, customer, 1, Convert.ToInt32(txtChkNo.Text.DefaultZero()), 1, 0, Convert.ToDouble(txtAmount.Text.DefaultZero()), Convert.ToDouble(txtDiscount.Text.DefaultZero()), 0, 0, 0, dtCheckDate.Value);


                RV_D rv = new RV_D();
                rv.RID = Convert.ToInt32(obj);
                rv.Narr = txtNarr.Text;
                rv.MOP_ID = 7;
                rv.AC_Code = customer.ToString();
                rv.ChkNo = Convert.ToInt32(txtChkNo.Text.DefaultZero());
                rv.DisAmt = Convert.ToDouble(txtDiscount.Text.DefaultZero());
                rv.Amt = Convert.ToDouble(txtAmount.Text.DefaultZero());
                rv.checkDate = dtCheckDate.Value;

                db.RV_D.Add(rv);


                var all = db.GL.Where(x => x.TypeCode == 14 && x.RID == obj);
                db.GL.RemoveRange(all);
                db.SaveChanges();

                GL gl = new GL();
                gl.TypeCode = 14;
                gl.AC_Code = code;
                gl.AC_Code2 = customer;
                gl.Narration = txtNarr.Text;
                gl.Debit = Convert.ToDouble(txtAmount.Text.DefaultZero());
                gl.Credit = 0;
                gl.RID = obj;
                gl.GLDate = dtTranscation.Value;
                gl.CompID = compID;
                gl.MOP_ID = 7;
                gl.SID = employeeId;
                db.GL.Add(gl);

                GL gl2 = new GL();
                gl2.TypeCode = 14;
                gl2.AC_Code = customer;
                gl2.AC_Code2 = code;
                gl2.Narration = txtNarr.Text;
                gl2.Debit = 0;
                gl2.Credit = Convert.ToDouble(txtAmount.Text.DefaultZero());
                gl2.RID = obj;
                gl2.GLDate = dtTranscation.Value;
                gl2.CompID = compID;
                gl2.SID = employeeId;

                gl2.MOP_ID = 7;
                db.GL.Add(gl2);

              



                db.SaveChanges();
                transaction.Commit();
            }




            var list1 = ReportsController.RecivedVoucharIndex(DateTime.Today.AddDays(-10), DateTime.Now, compID);
            recivedVoucharIndexResultBindingSource.DataSource = list1;
            recivedVoucharIndexResultBindingSource.ResetBindings(false);
            //  pnlMain.Hide();

            clear();

            label3.Text = "ADD";
            obj = 0;

            MessageBox.Show("Entry Save Successfully");
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


            if (e.ColumnIndex == 31)
            {
                List<Lib.Reporting.ReceiveVoucher> orderList = new List<Lib.Reporting.ReceiveVoucher>();
                Lib.Reporting.ReceiveVoucher ab = new Lib.Reporting.ReceiveVoucher();



               ab.CompanyTitle = new Lib.Companies().GetCompanyID(compID).CompanyTitle;
                ab.CompanyPhone = new Lib.Companies().GetCompanyID(compID).CompanyPhone;
                ab.CompanyAddress = new Lib.Companies().GetCompanyID(compID).CompanyAddress;
                //orderList.ForEach(x =>
                //{
                //    x.CompanyTitle = Companies.CompanyTitle;
                //    x.CompanyPhone = Companies.CompanyPhone;
                //    x.CompanyAddress = Companies.CompanyAddress;

                //});
                DateTime dtp= Convert.ToDateTime(CategorysDataGridView.Rows[e.RowIndex].Cells[2].Value);

                int customer = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[29].Value);
                int cash= Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[3].Value);
                ab.Customer = db.COA_D.AsNoTracking().Where(x => x.AC_Code == customer).FirstOrDefault().AC_Title;
                ab.PaymentMode = db.COA_D.AsNoTracking().Where(x => x.AC_Code == cash).FirstOrDefault().AC_Title;
                ab.Description= CategorysDataGridView.Rows[e.RowIndex].Cells[25].Value.ToString();
                ab.Amount =CategorysDataGridView.Rows[e.RowIndex].Cells[16].Value.ToString();
               // ab.PreviousAmount = 
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(CategorysDataGridView.Rows[e.RowIndex].Cells[2].Value);
          
                ab.SNO= dt.ToString("dd/MM/yyyy");

                int Vendorcode = Convert.ToInt32(customer);
                double balance = 0;

                var previosBalance = ReportsController.getCustomerPreviousBalance(dtp, Vendorcode);
                int a = 1;

                double credit = (double)previosBalance.Rows[0]["credit"];
                double debit = (double)previosBalance.Rows[0]["debit"];
                balance = debit - credit;
                if (balance != 0)
                {


                }
                SaleManagerEntities db1 = new SaleManagerEntities();

                var getdata = ReportsController.getcustomerLedgerSummaryByDate(dtp, dtp, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                foreach (DataRow rows in getdata.Rows)
                {


                    balance = balance - (double)rows["credit"];
                    balance = balance + (double)rows["debit"];








                }
                ab.PreviousAmount = balance.ToString();

                orderList.Add(ab);

                // A4 Size Print
                Form2 form2 = new Form2(orderList);
                form2.Show();
            }
        }

        private void CategorysDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            obj = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[0].Value);
            dtTranscation.Value = Convert.ToDateTime(CategorysDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString());
            cmbxAccount.SelectedValue = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[3].Value);
            txtAmount.Text = Convert.ToDouble(CategorysDataGridView.Rows[e.RowIndex].Cells[16].Value).ToString();
            dtCheckDate.Value = Convert.ToDateTime(CategorysDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString());
            txtChkNo.Text = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[21].Value).ToString();
            txtNarr.Text = CategorysDataGridView.Rows[e.RowIndex].Cells[25].Value.ToString();
            cmbxCustomer.SelectedValue = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[29].Value);
            txtDiscount.Text = Convert.ToDouble(CategorysDataGridView.Rows[e.RowIndex].Cells[22].Value).ToString();

         
            cmbxEmployee.SelectedValue= Convert.ToInt32(db.RV_M.AsNoTracking().Where(x => x.RID == obj).FirstOrDefault().SID);

        }

        public void received(int a)
        {


            obj = a;
            RV_M rv_m = new RV_M();
            RV_D rv_d = new RV_D();
            rv_m = db.RV_M.Where(x => x.RID == a).FirstOrDefault();
            rv_d = db.RV_D.Where(x => x.RID == a).FirstOrDefault();

            dtTranscation.Value = Convert.ToDateTime(rv_m.EDate);
            cmbxAccount.SelectedValue = Convert.ToInt32(rv_m.AC_Code);
            txtAmount.Text = Convert.ToDouble(rv_d.Amt).ToString();
            dtCheckDate.Value = Convert.ToDateTime(rv_d.checkDate.ToString());
            txtChkNo.Text = Convert.ToInt32(rv_d.ChkNo).ToString();
            txtNarr.Text = rv_d.Narr.ToString();
            cmbxCustomer.SelectedValue = Convert.ToInt32(rv_d.AC_Code);
            txtDiscount.Text = Convert.ToDouble(rv_d.DisAmt).ToString();


            pnlMain.Show();
            //txtChkNo.Focus();
            label3.Text = "EDIT";

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            var list = ReportsController.RecivedVoucharIndex(dtTo.Value, dtFrom.Value, compID);
            recivedVoucharIndexResultBindingSource.DataSource = list;

            recivedVoucharIndexResultBindingSource.ResetBindings(false);











        }

        private void cmbxCustomer_SelectedIndexChanged(object sender, EventArgs e)
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

                foreach (String s in strlist)
                {
                    var ss = s;
                }
                var customer = db.Customers.AsNoTracking().Where(x => x.CusName == cmbxCustomer.Text && x.CompanyID == compID).FirstOrDefault();
                if (customer == null)
                {

                    MessageBox.Show("you selected wrong customer");

                    cmbxCustomer.Focus();



                }


                else
                {

                    int Vendorcode = Convert.ToInt32(customer.AC_Code);
                    double balance = 0;

                    var previosBalance = ReportsController.getCustomerPreviousBalance(dtTranscation.Value, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = debit - credit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getcustomerLedgerSummaryByDate(dtTranscation.Value, dtTranscation.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                    foreach (DataRow rows in getdata.Rows)
                    {


                        balance = balance - (double)rows["credit"];
                        balance = balance + (double)rows["debit"];








                    }



                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    // vebdorid= Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = debit - credit;

                    lblAmount.Text = balance.ToString();


                }






            }
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
                var customer = db.Customers.AsNoTracking().Where(x => x.AC_Code == code && x.CompanyID == compID).FirstOrDefault();
                if (customer == null)
                {

                    MessageBox.Show("you selected wrong customer");

                    cmbxCustomer.Focus();



                }


                else
                {

                    int Vendorcode = Convert.ToInt32(code);
                    double balance = 0;

                    var previosBalance = ReportsController.getCustomerPreviousBalance(dtTranscation.Value, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = debit - credit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getcustomerLedgerSummaryByDate(dtTranscation.Value, dtTranscation.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                    foreach (DataRow rows in getdata.Rows)
                    {


                        balance = balance - (double)rows["credit"];
                        balance = balance + (double)rows["debit"];








                    }



                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    // vebdorid= Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = debit - credit;

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
                var customer = db.Customers.AsNoTracking().Where(x => x.AC_Code == code && x.CompanyID == compID).FirstOrDefault();
                if (customer == null)
                {

                    MessageBox.Show("you selected wrong customer");

                    cmbxCustomer.Focus();



                }


                else
                {

                    int Vendorcode = Convert.ToInt32(code);
                    double balance = 0;

                    var previosBalance = ReportsController.getCustomerPreviousBalance(dtTranscation.Value, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = debit - credit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getcustomerLedgerSummaryByDate(dtTranscation.Value, dtTranscation.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                    foreach (DataRow rows in getdata.Rows)
                    {


                        balance = balance - (double)rows["credit"];
                        balance = balance + (double)rows["debit"];








                    }



                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    // vebdorid= Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = debit - credit;

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










    //{
    //    public ReceiveVoucher()
    //    {
    //        InitializeComponent();
    //    }
    //}
}
