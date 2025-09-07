using Lib.Entity;
using SalesMngmt.Invoice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Reporting
{
    public partial class DeleteOrders : Form
    {
        //public CustomerLedgerSummary()
        //{
        //    InitializeComponent();
        //}

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

    

        public void customerledgers(int code)
        {
            CategorysDataGridView.Rows.Clear();
            //CategorysDataGridView.Rows.Add(1, 2, 3, 4, 5, 6, 7);
            int Vendorcode = Convert.ToInt32(code);
            dtTo.Value = DateTime.Today.AddDays(-10);
            toDate.Value = DateTime.Now;

            ddlUser.SelectedValue = Vendorcode;
            var previosBalance = db.getCustomerPreviousBalance(fromDate.Value, Vendorcode).FirstOrDefault();
            int a = 1;

            double credit = previosBalance.credit;
            double debit = previosBalance.debit;
            double balance = debit - credit;
            if (balance != 0)
            {
                //var abc = new MyModels.VendorLedger();
                //abc.Credit = (float)credit;
                //abc.Debit = (float)debit;
                //abc.Balance = (float)balance;
                //abc.Naration = "Previous Balance";
                CategorysDataGridView.Rows.Add(a, "", "", debit, credit, balance, "Previous Balance");
                a++;
            }
            SaleManagerEntities db1 = new SaleManagerEntities();

            var getdata = db.getcustomerLedgerSummaryByDate(fromDate.Value, toDate.Value, Vendorcode).ToList();//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;


            var count = getdata.Count();



            for (int b = 0; b < count; b++)
            {
                // var abc = new MyModels.VendorLedger();

                balance = balance - (double)getdata[b].credit;
                balance = balance + (double)getdata[b].debit;

                //getdata[a].abc.Balance = 0;
                //abc.Credit = (float)getdata[a].credit;
                //abc.Debit = (float)getdata[a].debit;
                //abc.GlDate = (DateTime)getdata[a].GLDate;
                //abc.Naration = getdata[a].Narration;
                //abc.Reference = getdata[a].reference;
                //abc.SNO = a;
                //abc.Balance = (float)balance;

                CategorysDataGridView.Rows.Add(a, (DateTime)getdata[b].GLDate, getdata[b].reference, (float)getdata[b].debit, (float)getdata[b].credit, (float)balance, "", getdata[b].TypeCode, getdata[b].RID);
                a++;
            }
        }
        public DeleteOrders(int _compID)
        {
            InitializeComponent();
            compID = _compID;
            db = new SaleManagerEntities();
        }

        private void Category_Load(object sender, EventArgs e)
        {
            var lstItem = db.Items.AsNoTracking().Where(x=>x.CompanyID== compID && x.isDeleted==false).ToList();
            lstItem.Add(new Lib.Entity.Items { IID = 0, IName = "Select Item" });
            lstItem = lstItem.OrderBy(x => x.IID).ToList();
            FillCombo(comboBox1, GetPaymentType(), "Value", "Key", 0);
            FillCombo(ddlUser, PopulateUsers(), "Value", "Key", 0);
        }

        private IEnumerable<Dict> GetPaymentType()
        {
            List<Dict> dict = new List<Dict>();
            dict.Add(new Dict { key = 0, Value = "All" });
            dict.Add(new Dict { key = 1, Value = "Cash" });
            dict.Add(new Dict { key = 3, Value = "Card" });
            dict.Add(new Dict { key = 2, Value = "Void" });
            return dict;
        }

        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected)
        {
            comboBox.DataSource = obj;
            comboBox.DisplayMember = Name; // Column Name
            comboBox.ValueMember = ID;  // Column Name
            comboBox.SelectedValue = selected;
        }

        private IEnumerable<Dict> PopulateUsers()
        {
            List<Dict> dict = new List<Dict>();
            var ul = db.AspNetUsers.OrderByDescending(x => x.UserName == "all").ThenBy(x => x.UserName).ToList();
            foreach (var item in ul)
            {
                dict.Add(new Dict { key = Convert.ToInt32(item.Id), Value = item.UserName });
            }
            return dict;
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



            }



        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {

            //  Lib.Entity.ArticleType us = (Lib.Entity.ArticleType)articleTypeBindingSource.Current;
            obj = 0;
            if (obj == 0)
            {

                list = db.RecivedVoucharIndex(dtTo.Value, toDate.Value).ToList();
                recivedVoucharIndexResultBindingSource.DataSource = list;
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}




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
                //if (toolStripTextBoxFind.Text.Trim().Length == 0) { CategorysDataGridView.DataSource = list; }
                //else
                //{
                //    // CategorysDataGridView.DataSource = list.FindAll(x => x.ArticleType1.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                //}
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
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var ID = CategorysDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
                var itemToRemove = db.tbl_Order.Where(x => x.OrderNo == ID).FirstOrDefault(); //returns a single item.
                if (itemToRemove != null)
                {
                    db.tbl_Order.RemoveRange(db.tbl_Order.Where(x => x.OrderId == itemToRemove.OrderId));
                    db.tbl_OrderDetails.RemoveRange(db.tbl_OrderDetails.Where(x => x.OrderId == itemToRemove.OrderId));


                    db.Itemledger.RemoveRange(db.Itemledger.Where(x => x.RID == itemToRemove.OrderId && x.TypeCode == 24));
                    db.GL.RemoveRange(db.GL.Where(x => x.RID == itemToRemove.OrderId && x.TypeCode == 24));
                    db.SaveChanges();
             
                    btnSearch_Click(null, null);
                }
            }
        }

        private void CategorysDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                int typecode = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[7].Value);

                int rid = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[8].Value);

                if (typecode == 2)
                {

                    Invoice.PInv pinv = new Invoice.PInv(0);
                    pinv.Show();

                    EditMessageBox messageBox = new EditMessageBox(pinv, compID, "PI");

                    var purM = db.Pur_M.Where(x => x.RID == rid).FirstOrDefault();
                    messageBox.purchaseEdit(purM.InvNo);

                }


                else if (typecode == 3)
                {



                }

                else if (typecode == 5)
                {


                    Invoice.SInv pinv = new Invoice.SInv(0);
                    pinv.Show();

                    EditMessageBox messageBox = new EditMessageBox(pinv, compID, "PI");

                    var purM = db.Sales_M.Where(x => x.RID == rid).FirstOrDefault();
                    messageBox.purchaseEdit(purM.InvNo);


                }

                else if (typecode == 6)
                {


                }

                else if (typecode == 14)
                {

                    Invoice.ReceiveVoucher pinv = new Invoice.ReceiveVoucher(0);
                    pinv.Show();
                    pinv.received(rid);
                }

                else if (typecode == 15)
                {


                    Invoice.PaymentVoucher pinv = new Invoice.PaymentVoucher(0);
                    pinv.Show();
                    pinv.Payment(rid);


                }

                else if (typecode == 16)
                {


                }




            }
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now;
            dtStart = fromDate.Value;
            dtEnd = toDate.Value;

            CategorysDataGridView.Rows.Clear();
            int Vendorcode = Convert.ToInt32(ddlUser.SelectedValue);
            SaleManagerEntities db1 = new SaleManagerEntities();

            List<Lib.Model.OrderReportModel> getdata = Lib.Reporting.Reports.BookingSummary(dtStart, dtEnd, ddlUser.Text, comboBox1.SelectedValue.ToString(), compID).OrderByDescending(x => x.Datetim).ToList();
            var count = getdata.Count();
            for (int b = 0; b < count; b++)
            {
                CategorysDataGridView.Rows.Add(getdata[b].InvoiceNo, getdata[b].Datetim, getdata[b].InvoiceNo, getdata[b].ordrType, getdata[b].Amount);
            }
        }

        public void customerledger(int code)
        {

            CategorysDataGridView.Rows.Clear();
            //CategorysDataGridView.Rows.Add(1, 2, 3, 4, 5, 6, 7);
            ddlUser.SelectedValue = Convert.ToInt32(code);
            dtTo.Value = DateTime.Today.AddDays(-10);
            toDate.Value = DateTime.Now;
            var previosBalance = db.getCustomerPreviousBalance(DateTime.Now, code).FirstOrDefault();
            int a = 1;

            double credit = previosBalance.credit;
            double debit = previosBalance.debit;
            double balance = debit - credit;
            if (balance != 0)
            {
                //var abc = new MyModels.VendorLedger();
                //abc.Credit = (float)credit;
                //abc.Debit = (float)debit;
                //abc.Balance = (float)balance;
                //abc.Naration = "Previous Balance";
                CategorysDataGridView.Rows.Add(a, "", "", debit, credit, balance, "Previous Balance");
                a++;
            }
            SaleManagerEntities db1 = new SaleManagerEntities();

            var getdata = db.getCustomerLedgerBYDate(DateTime.Now.AddDays(-10), toDate.Value, code).ToList();//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;


            var count = getdata.Count();



            for (int b = 0; b < count; b++)
            {
                // var abc = new MyModels.VendorLedger();

                balance = balance - (double)getdata[b].credit;
                balance = balance + (double)getdata[b].debit;

                //getdata[a].abc.Balance = 0;
                //abc.Credit = (float)getdata[a].credit;
                //abc.Debit = (float)getdata[a].debit;
                //abc.GlDate = (DateTime)getdata[a].GLDate;
                //abc.Naration = getdata[a].Narration;
                //abc.Reference = getdata[a].reference;
                //abc.SNO = a;
                //abc.Balance = (float)balance;

                CategorysDataGridView.Rows.Add(a, (DateTime)getdata[b].GLDate, getdata[b].reference, (float)getdata[b].debit, (float)getdata[b].credit, (float)balance, getdata[b].Narration);
                a++;



            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
    public class Dict
    {
        public int key { get; set; }
        public String Value { get; set; }
    }
}
