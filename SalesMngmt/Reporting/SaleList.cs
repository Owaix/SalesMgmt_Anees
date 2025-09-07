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
using Lib.Reporting;
using Lib.Utilities;
using System.Configuration;
using Microsoft.Reporting.WinForms;
using Lib.Model;
using Lib;
using DIagnoseMgmt;
using System.Globalization;
using SalesMngmt.Invoice;

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




        public SaleList(int company)
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
            var customerCount = db.Customers.Where(x => x.CompanyID == compID && x.InActive==false).FirstOrDefault();
            var AccountCount = db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID==compID && x.InActive == false).FirstOrDefault();
           

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
            var abc = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[0].Value);
            if (e.ColumnIndex == 8) {
                DialogResult dialogResult = MessageBox.Show("Are you sure To Delete", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    db.Sales_D.RemoveRange(db.Sales_D.Where(x => x.RID == abc));
                    db.Itemledger.RemoveRange(db.Itemledger.Where(x => x.RID == abc && x.TypeCode==5));
                    db.GL.RemoveRange(db.GL.Where(x => x.RID == abc && x.TypeCode == 5));
                    db.Sales_M.RemoveRange(db.Sales_M.Where(x => x.RID == abc));
                    db.SaveChanges();

                    CategorysDataGridView.Rows.Clear();
                    var Record = ReportsController.SaleList(dtTo.Value, dtFrom.Value, compID);

                    double Disount = 0;
                    double TotalAmount = 0;
                    foreach (DataRow List in Record.Rows)
                    {


                        CategorysDataGridView.Rows.Add(List["RID"], List["AC_Title"], List["DisAmt"], List["TotalAmount"], List["InvNo"], List["EDate"]);

                        Disount += Convert.ToInt32(List["DisAmt"]);
                        TotalAmount += Convert.ToInt32(List["TotalAmount"]);


                    }
                    lblDiscount.Text = Disount.ToString();
                    lblTotal.Text = TotalAmount.ToString();



                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

              


            }

            if (e.ColumnIndex == 7) {

                var order = db.Sales_M.Where(x => x.RID == abc).FirstOrDefault();
                var list = db.Sales_D.Where(x => x.RID == abc).ToList();
                List<SaleInvoice> orderList = new List<SaleInvoice>();
                List<SaleInvoice> orderHeader = new List<SaleInvoice>();
                int sNO = 1;
                double TotalGross = 0;
                double Amount = 0;
                double DisountTotal = 0;
                double TotalDiscount = 0;
                double DiscountDifference = 0;
                double TotalGrossAmount = 0;

                // geting TotalSum values from this ForEachLoop
                foreach (var itemName in list)
                {
                    SaleInvoice orders = new SaleInvoice();
                    orders.InvoiceID = order.InvNo;
                    orders.Client = db.COA_D.Where(x => x.AC_Code == order.AC_Code).FirstOrDefault().AC_Title;
                    //   orders.user = txtReference.Text.ToString();

                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(order.EDate);
                    orders.Time = dt.ToString("dd/MM/yyyy");


                    //orders.user = txtReference.Text.ToString();
                    //Pending
                    //    orders.SNO = 
                    orders.item = db.Items.Where(x => x.IID == itemName.IID).FirstOrDefault().IName;
                    orders.Rate = Convert.ToDecimal(itemName.SalesPriceP);
                    orders.DiscountRs = Convert.ToDecimal(itemName.DisRs);
                    orders.DiscountPercentage = Convert.ToDecimal(itemName.DisP);
                    orders.Amount = Convert.ToDecimal(itemName.Amt);
                    orders.Ctn = Convert.ToDecimal(itemName.CTN);
                    orders.Pcs = Convert.ToDecimal(itemName.PCS);
                    orders.BatchNo = itemName.BatchNo.ToString();
                    orders.ExpDate = itemName.ExpireDate.ToString();
                    //orders.SNO = sNO;
                    //sNO++;
                    double ctn = Convert.ToDouble(db.Items.Where(x => x.IID == itemName.IID).FirstOrDefault().CTN_PCK);
                    double TOtalValue = 0;
                    if (ctn == 0)
                    {
                        TOtalValue = Convert.ToDouble(itemName.Qty);
                        TotalGross += Convert.ToDouble(itemName.SalesPriceP) * Convert.ToDouble(TOtalValue);
                    }
                    else
                    {
                        TOtalValue = (ctn * Convert.ToDouble(itemName.CTN)) + Convert.ToDouble(itemName.Qty);


                        TotalGross += Convert.ToDouble(itemName.SalesPriceP) * TOtalValue;


                    }
                    Amount += Convert.ToDouble(itemName.Amt);



                    TotalGrossAmount = TotalGross;
                    DisountTotal = Amount;
                    DiscountDifference = Convert.ToDouble(TotalGross) - Convert.ToDouble(Amount);
                    TotalDiscount = Convert.ToDouble(DiscountDifference);  //(Convert.ToDecimal(itemName.SalesPriceP) * Convert.ToDecimal( TOtalValue))- Convert.ToDecimal(itemName.Amt);


                }
                TotalDiscount += Convert.ToDouble(order.DisAmt.DefaultZero());
                DisountTotal -= Convert.ToDouble(order.DisAmt.DefaultZero());


                foreach (var itemName in list)
                {
                    SaleInvoice orders = new SaleInvoice();
                    orders.InvoiceID = order.InvNo;
                    orders.Client = db.COA_D.Where(x => x.AC_Code == order.AC_Code).FirstOrDefault().AC_Title;

                    var clientNAmeAndCity = db.COA_D.Where(x => x.AC_Code == order.AC_Code).FirstOrDefault();

                    if (clientNAmeAndCity.CAC_Code == 3)
                    {
                        var customer = db.Customers.AsNoTracking().Where(x => x.AC_Code == order.AC_Code).FirstOrDefault();
                        int city = Convert.ToInt32(customer.City);
                        var cityName = db.tbl_city.AsNoTracking().Where(x => x.Id == city).FirstOrDefault();
                        if (cityName == null)
                        {
                            orders.City = "";
                        }
                        else
                        {

                            orders.City = cityName.CityName;

                        }

                    }

                    if (clientNAmeAndCity.CAC_Code == 9)
                    {
                        var customer = db.Vendors.AsNoTracking().Where(x => x.AC_Code == order.AC_Code).FirstOrDefault();
                        int city = Convert.ToInt32(customer.City);
                        var cityName = db.tbl_city.AsNoTracking().Where(x => x.Id == city).FirstOrDefault();
                        if (cityName == null)
                        {
                            orders.City = "";
                        }
                        else
                        {

                            orders.City = cityName.CityName;

                        }


                    }

                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(order.EDate);
                    orders.Time = dt.ToString("dd/MM/yyyy");


                    // orders.user = txtReference.Text.ToString();
                    //Pending
                    //    orders.SNO = 
                    orders.item = db.Items.Where(x => x.IID == itemName.IID).FirstOrDefault().IName;
                    orders.Rate = Convert.ToDecimal(itemName.SalesPriceP);
                    orders.DiscountRs = Convert.ToDecimal(itemName.DisRs);
                    orders.DiscountPercentage = Convert.ToDecimal(itemName.DisP);
                    orders.Amount = Convert.ToDecimal(itemName.Amt);
                    orders.Ctn = Convert.ToDecimal(itemName.CTN);
                    orders.Pcs = Convert.ToDecimal(itemName.PCS);
                    orders.BatchNo = itemName.BatchNo.ToString();
                    orders.ExpDate = itemName.ExpireDate.ToString();
                    orders.SNO = sNO;
                    sNO++;
                    double ctn = Convert.ToDouble(db.Items.Where(x => x.IID == itemName.IID).FirstOrDefault().CTN_PCK);
                    double TOtalValue = 0;
                    if (ctn == 0)
                    {
                        TOtalValue = Convert.ToDouble(itemName.Qty);
                        TotalGross += Convert.ToDouble(itemName.SalesPriceP) * Convert.ToDouble(TOtalValue);
                    }
                    else
                    {
                        TOtalValue = (ctn * Convert.ToDouble(itemName.CTN)) + Convert.ToDouble(itemName.Qty);


                        TotalGross += Convert.ToDouble(itemName.SalesPriceP) * TOtalValue;


                    }
                    Amount += Convert.ToDouble(itemName.Amt);

                    orders.GrossAmt = TotalGrossAmount;
                    orders.DiscountTotal = DisountTotal;
                    //decimal DiscountDifference = Convert.ToDecimal(TotalGross) - Convert.ToDecimal(Amount);
                    orders.TotalDiscount = Convert.ToDecimal(TotalDiscount);  //(Convert.ToDecimal(itemName.SalesPriceP) * Convert.ToDecimal( TOtalValue))- Convert.ToDecimal(itemName.Amt);
                    orders.PREVBALANNCE = Convert.ToDecimal( order.PreBal);
                    orders.RCVDBALANCE = Convert.ToDecimal(order.CashAmt.DefaultZero());
                    orderList.Add(orders);
                }
                SaleInvoice orders1 = new SaleInvoice();

                orders1.GrossAmt = TotalGross;
                orders1.DiscountTotal = Amount;
              //  string refer = txtReference.Text.ToString();
              //  orders1.user = refer;

                // orders1.TotalDiscount =  Convert.ToDecimal(DiscountDifference) + Convert.ToDecimal(order.DisAmt);  //(Convert.ToDecimal(itemName.SalesPriceP) * Convert.ToDecimal( TOtalValue))- Convert.ToDecimal(itemName.Amt);

                orderHeader.Add(orders1);



                // add multiple company
                var Companies = new Companies().GetCompanyID(compID);
                orderList.ForEach(x =>
                {
                    x.CompanyTitle = Companies.CompanyTitle;
                    x.CompanyPhone = Companies.CompanyPhone;
                    x.CompanyAddress = Companies.CompanyAddress;
                    x.CompanyID = Companies.CompanyID;
                });

                if (compID == 1017)
                {

                   

                    var getcompanyDetail = db.tbl_Company.AsNoTracking().Where(x => x.CompID == order.diffCompany).FirstOrDefault();


                    orderList.ForEach(x =>
                    {
                        x.CompanyTitle = getcompanyDetail.Comp;
                        x.CompanyPhone = getcompanyDetail.Tel;
                        x.CompanyAddress = getcompanyDetail.Address;
                    });

                }









                Silent silent = new Silent();
                ReportViewer reportViewer1 = new ReportViewer();
                if (radioButton1.Checked)
                {


                    //Printer.setDef(ConfigurationManager.AppSettings["Thermal"].ToString());
                    //     silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.rptSaleInvoiceThermal.rdlc");
                    if (orderList[0].CompanyID == 1013)
                    {
                        silent.Run(reportViewer1, orderList, "SalesMngmt.ThermalReport.NomanCompany1013.rdlc");
                    }
                    else
                    {

                        silent.Run(reportViewer1, orderList, "SalesMngmt.ThermalReport.rptSaleInvoiceThermal.rdlc");
                    }
                }
                else if(radioButton2.Checked)
                {
                    //Printer.setDef(ConfigurationManager.AppSettings["A4"].ToString());

                    // A4 Size Print
                    Form2 form2 = new Form2(orderList);
                    form2.Show();
                    //return;

                    //silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.rptSaleInvPrint.rdlc");
                    //Printer.setDef(ConfigurationManager.AppSettings["Thermal"].ToString());
                }

                else if (radioButton3.Checked)
                {
                    //Printer.setDef(ConfigurationManager.AppSettings["A4"].ToString());

                    // A5 Size Print
                    Form2 form2 = new Form2(orderList, "A5");
                    form2.Show();
                    //return;

                    //silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.rptSaleInvPrint.rdlc");
                    //Printer.setDef(ConfigurationManager.AppSettings["Thermal"].ToString());
                }




                return;

            }


            if (e.ColumnIndex == 6) {


                pnlMain.Show();
                dataGridView1.Rows.Clear();
            }

            if (e.ColumnIndex == 9) {
                int rid = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[0].Value);

                Invoice.SInv pinv = new Invoice.SInv(compID);
                pinv.Show();

                EditMessageBox messageBox = new EditMessageBox(pinv, compID, "PI");

                var purM = db.Sales_M.Where(x => x.RID == rid).FirstOrDefault();
                messageBox.purchaseEdit(purM.InvNo);



            }

           
            var detail = db.Sales_D.AsNoTracking().Where(x => x.RID == abc);

            int a = 1;
            double DiscRs = 0;
            double DiscPenCentage = 0;
            double Total = 0;
            foreach (var loop in detail) {

                var name = db.Items.AsNoTracking().Where(x => x.IID == loop.IID).FirstOrDefault();
                dataGridView1.Rows.Add(a, name.IName, loop.CTN, loop.PCS, loop.SalesPriceP, loop.DisP, loop.DisRs, loop.Amt);
                a++;
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
            var Record = ReportsController.SaleList(dtTo.Value, dtFrom.Value,compID);

            double Disount = 0;
            double Total = 0;
            foreach (DataRow List in Record.Rows) {
             

                CategorysDataGridView.Rows.Add(List["RID"], List["AC_Title"], List["DisAmt"], List["TotalAmount"], List["InvNo"], List["EDate"]);

                Disount += Convert.ToInt32(List["DisAmt"]);
                Total += Convert.ToInt32(List["TotalAmount"]);


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
