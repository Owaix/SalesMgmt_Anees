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
using SalesMngmt.Invoice;
using DIagnoseMgmt;
using Lib.Model;
using Microsoft.Reporting.WinForms;
using Lib;

namespace SalesMngmt.Reporting
{
    public partial class PurchaseList : MetroFramework.Forms.MetroForm
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




        public PurchaseList(int company)
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
            var AccountCount = db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID==compID && x.InActive==false).FirstOrDefault();
           

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
            if (e.ColumnIndex == 7)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure To Delete", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    db.Pur_M.RemoveRange(db.Pur_M.Where(x => x.RID == abc));
                    db.Pur_D.RemoveRange(db.Pur_D.Where(x => x.RID == abc));
                    db.Itemledger.RemoveRange(db.Itemledger.Where(x => x.RID == abc && x.TypeCode == 2));
                    db.GL.RemoveRange(db.GL.Where(x => x.RID == abc && x.TypeCode == 2));
                    db.SaveChanges();


                    CategorysDataGridView.Rows.Clear();
                    var Record = ReportsController.PurchaseList(dtTo.Value, dtFrom.Value, compID);

                    double Disount = 0;
                    double Total = 0;
                    foreach (DataRow List in Record.Rows)
                    {


                        CategorysDataGridView.Rows.Add(List["RID"], List["AC_Title"], List["DisAmt"], List["TotalAmount"], List["InvNo"], List["EDate"]);

                        Disount += Convert.ToInt32(List["DisAmt"]);
                        Total += Convert.ToInt32(List["TotalAmount"]);


                    }
                    lblDiscount.Text = Disount.ToString();
                    lblTotal.Text = Total.ToString();

                }
            }

            else if (e.ColumnIndex == 8)
            {
                Invoice.PInv pinv = new Invoice.PInv(compID);
                pinv.Show();

                EditMessageBox messageBox = new EditMessageBox(pinv, compID, "PI");

                var purM = db.Pur_M.Where(x => x.RID == abc).FirstOrDefault();
                messageBox.purchaseEdit(purM.InvNo);
             }
            else if (e.ColumnIndex == 6)
            {
                dataGridView1.Rows.Clear();
                pnlMain.Show();


                var detail = db.Pur_D.AsNoTracking().Where(x => x.RID == abc);

                int a = 1;
                double DiscRs = 0;
                double DiscPenCentage = 0;
                double Total = 0;
                foreach (var loop in detail)
                {

                    var name = db.Items.AsNoTracking().Where(x => x.IID == loop.IID).FirstOrDefault();
                    dataGridView1.Rows.Add(a, name.IName, loop.CTN, loop.PCS, loop.PurPrice, loop.DisP, loop.DisRs, loop.Amt);

                    DiscRs += Convert.ToDouble(loop.DisRs);
                    DiscPenCentage += Convert.ToDouble(loop.DisP);
                    Total += Convert.ToDouble(loop.Amt);

                }
                lblDetailDisc.Text = DiscRs.ToString();
                lblDetailDiscPercentage.Text = DiscPenCentage.ToString();
                lblDetailToTal.Text = Total.ToString();

                lblDiscount.Visible = false;
                lblTotal.Visible = false;
            }

            else if(e.ColumnIndex == 9) {
              

                    var order = db.Pur_M.Where(x => x.RID == abc).FirstOrDefault();
                    var list = db.Pur_D.Where(x => x.RID == abc).ToList();
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
                        orders.Rate = Convert.ToDecimal(itemName.PurPrice);
                        orders.DiscountRs = Convert.ToDecimal(itemName.DisRs);
                        orders.DiscountPercentage = Convert.ToDecimal(itemName.DisP);
                        orders.Amount = Convert.ToDecimal(itemName.Amt);
                        orders.Ctn = Convert.ToDecimal(itemName.CTN);
                        orders.Pcs = Convert.ToDecimal(itemName.PCS);
                    orders.BatchNo = "";//itemName.BatchNo.ToString();
                        orders.ExpDate = itemName.ExpDT.ToString();
                        //orders.SNO = sNO;
                        //sNO++;
                        double ctn = Convert.ToDouble(db.Items.Where(x => x.IID == itemName.IID).FirstOrDefault().CTN_PCK);
                        double TOtalValue = 0;
                        if (ctn == 0)
                        {
                            TOtalValue = Convert.ToDouble(itemName.Qty);
                            TotalGross += Convert.ToDouble(itemName.PurPrice) * Convert.ToDouble(TOtalValue);
                        }
                        else
                        {
                            TOtalValue = (ctn * Convert.ToDouble(itemName.CTN)) + Convert.ToDouble(itemName.Qty);


                            TotalGross += Convert.ToDouble(itemName.PurPrice) * TOtalValue;


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
                        orders.Rate = Convert.ToDecimal(itemName.PurPrice);
                        orders.DiscountRs = Convert.ToDecimal(itemName.DisRs);
                        orders.DiscountPercentage = Convert.ToDecimal(itemName.DisP);
                        orders.Amount = Convert.ToDecimal(itemName.Amt);
                        orders.Ctn = Convert.ToDecimal(itemName.CTN);
                        orders.Pcs = Convert.ToDecimal(itemName.PCS);
                    orders.BatchNo = "";//itemName.BatchNo.ToString();
                        orders.ExpDate = itemName.ExpDT.ToString();
                        orders.SNO = sNO;
                        sNO++;
                        double ctn = Convert.ToDouble(db.Items.Where(x => x.IID == itemName.IID).FirstOrDefault().CTN_PCK);
                        double TOtalValue = 0;
                        if (ctn == 0)
                        {
                            TOtalValue = Convert.ToDouble(itemName.Qty);
                            TotalGross += Convert.ToDouble(itemName.PurPrice) * Convert.ToDouble(TOtalValue);
                        }
                        else
                        {
                            TOtalValue = (ctn * Convert.ToDouble(itemName.CTN)) + Convert.ToDouble(itemName.Qty);


                            TotalGross += Convert.ToDouble(itemName.PurPrice) * TOtalValue;


                        }
                        Amount += Convert.ToDouble(itemName.Amt);

                        orders.GrossAmt = TotalGrossAmount;
                        orders.DiscountTotal = DisountTotal;
                        //decimal DiscountDifference = Convert.ToDecimal(TotalGross) - Convert.ToDecimal(Amount);
                        orders.TotalDiscount = Convert.ToDecimal(TotalDiscount);  //(Convert.ToDecimal(itemName.SalesPriceP) * Convert.ToDecimal( TOtalValue))- Convert.ToDecimal(itemName.Amt);
                    orders.PREVBALANNCE = 0;//Convert.ToDecimal(order.PreBal);
                    orders.RCVDBALANCE = 0;// Convert.ToDecimal(order.CashAmt.DefaultZero());
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

                 









                    Silent silent = new Silent();
                    ReportViewer reportViewer1 = new ReportViewer();
                   
               

                 
                        //Printer.setDef(ConfigurationManager.AppSettings["A4"].ToString());

                        // A5 Size Print
                        Form2 form2 = new Form2(orderList, "Purchase_A5");
                        form2.Show();
                        //return;

                        //silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.rptSaleInvPrint.rdlc");
                        //Printer.setDef(ConfigurationManager.AppSettings["Thermal"].ToString());
                    




                    return;

                

            }
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
            var Record = ReportsController.PurchaseList(dtTo.Value, dtFrom.Value,compID);

            double Disount = 0;
            double Total = 0;
            foreach (DataRow List in Record.Rows) {
             

                CategorysDataGridView.Rows.Add(List["RID"] , List["AC_Title"], List["DisAmt"], List["TotalAmount"], List["InvNo"], List["EDate"]);

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
