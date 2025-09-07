using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using Microsoft.Reporting.WinForms;
using SalesMngmt.Invoice;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Reporting
{
    public partial class ItemSummary : Form
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
        int screenId=0;

      


        public void customerledgers(int code)
        {
            CategorysDataGridView.Rows.Clear();
            //CategorysDataGridView.Rows.Add(1, 2, 3, 4, 5, 6, 7);
            int Vendorcode = Convert.ToInt32(code);
            dtTo.Value = DateTime.Today.AddDays(-10);
            dtFrom.Value = DateTime.Now;

            cmbxItem.SelectedValue = Vendorcode;
            var previosBalance = db.getCustomerPreviousBalance(dtTOdate.Value, Vendorcode).FirstOrDefault();
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

            var getdata = db.getcustomerLedgerSummaryByDate(dtTOdate.Value, dtFrom.Value, Vendorcode).ToList();//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;


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
        public ItemSummary(int _compID,int id)
        {
            InitializeComponent();
            compID = _compID;
            screenId = id;
            db = new SaleManagerEntities();
        }

        private void Category_Load(object sender, EventArgs e)
        {
            var lstItem = db.Items.AsNoTracking().Where(x=>x.CompanyID== compID && x.isDeleted==false).ToList();
            lstItem.Add(new Lib.Entity.Items { IID = 0, IName = "Select Item" });
            lstItem = lstItem.OrderBy(x => x.IID).ToList();
            FillCombo(cmbxItem, lstItem, "IName", "IID", 0);
        }

        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected)
        {
            comboBox.DataSource = obj;
            comboBox.DisplayMember = Name; // Column Name
            comboBox.ValueMember = ID;  // Column Name
            comboBox.SelectedValue = selected;
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

                list = db.RecivedVoucharIndex(dtTo.Value, dtFrom.Value).ToList();
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
            //  var abc=   CategorysDataGridView.Rows[e.RowIndex].Cells[0].Value;
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
                 CategorysDataGridView.Rows.Clear();
            if (screenId == 1) {

                int Itemid = Convert.ToInt32(cmbxItem.SelectedValue);
                if (Itemid > 0)
                {
                    var Items = ReportsController.GetItembyOrderDetail(dtTo.Value, dtFrom.Value, compID, Itemid);

                    double total1 = 0;
                    double qty1 = 0;
                    double disc1 = 0;
                    string name1 = "";



                    for (int b = 0; b < Items.Rows.Count; b++)
                    {
                        total1 += (Convert.ToDouble(Items.Rows[b]["Total"]) * Convert.ToDouble(Items.Rows[b]["Qty"])) - Convert.ToDouble(Items.Rows[b]["Disc"]);
                        qty1 += Convert.ToDouble(Items.Rows[b]["Qty"]);
                        disc1 += Convert.ToDouble(Items.Rows[b]["Disc"]);
                        name1 = Items.Rows[b]["name"].ToString();


                    }
                    if (Items.Rows.Count == 0)
                    {


                    }
                    else
                    {

                        CategorysDataGridView.Rows.Add(1, name1, qty1, total1);
                    }
                    total1 = 0;
                    qty1 = 0;
                    disc1 = 0;
                    name1 = "";

                    return;
                }





                var getitems = db.Items.AsNoTracking().Where(x => x.isDeleted == false && x.CompanyID == compID ).ToList();
            double total = 0;
            double qty = 0;
            double disc = 0;
            string name = "";

            for (int a = 0; a < getitems.Count; a++) {

                var Items = ReportsController.GetItembyOrderDetail(dtTo.Value, dtFrom.Value, compID, getitems[a].IID);
                for (int b = 0; b < Items.Rows.Count; b++)
                {
                    total += (Convert.ToDouble(Items.Rows[b]["Total"]) * Convert.ToDouble(Items.Rows[b]["Qty"])) - Convert.ToDouble(Items.Rows[b]["Disc"]);
                    qty += Convert.ToDouble( Items.Rows[b]["Qty"]);
                    disc+= Convert.ToDouble(Items.Rows[b]["Disc"]);
                    name = Items.Rows[b]["name"].ToString();
                }
                    if (Items.Rows.Count == 0)
                    {


                    }
                    else
                    {

                        CategorysDataGridView.Rows.Add(a + 1, name, qty, total);
                    }
                  
                 total = 0;
                 qty = 0;
                 disc = 0;
                name = "";
            }

            }


            if (screenId == 2)
            {
                int Itemid = Convert.ToInt32(cmbxItem.SelectedValue);
                if (Itemid > 0) {
                    var Items = ReportsController.GetItembySaleDetail(dtTOdate.Value, dtFrom.Value, compID, Itemid);

                    double total1 = 0;
                    double qty1 = 0;
                    double disc1 = 0;
                    string name1 = "";



                    for (int b = 0; b < Items.Rows.Count; b++)
                    {
                        total1 += (Convert.ToDouble(Items.Rows[b]["Total"]) * Convert.ToDouble(Items.Rows[b]["Qty"])) - Convert.ToDouble(Items.Rows[b]["Disc"]);
                        qty1 += Convert.ToDouble(Items.Rows[b]["Qty"]);
                        disc1 += Convert.ToDouble(Items.Rows[b]["Disc"]);
                        name1 = Items.Rows[b]["name"].ToString();


                    }
                    if (Items.Rows.Count == 0)
                    {


                    }
                    else
                    {

                        CategorysDataGridView.Rows.Add(1, name1, qty1, total1);
                    }
                    total1 = 0;
                    qty1 = 0;
                    disc1 = 0;
                    name1 = "";

                    return;
                }

                var getitems = db.Items.AsNoTracking().Where(x => x.isDeleted == false && x.CompanyID == compID).ToList();
                double total = 0;
                double qty = 0;
                double disc = 0;
                string name = "";

                for (int a = 0; a < getitems.Count; a++)
                {

                    var Items = ReportsController.GetItembySaleDetail(dtTOdate.Value, dtFrom.Value, compID, getitems[a].IID);
                    for (int b = 0; b < Items.Rows.Count; b++)
                    {
                        total += (Convert.ToDouble(Items.Rows[b]["Total"]) * Convert.ToDouble(Items.Rows[b]["Qty"])) - Convert.ToDouble(Items.Rows[b]["Disc"]);
                        qty += Convert.ToDouble(Items.Rows[b]["Qty"]);
                        disc += Convert.ToDouble(Items.Rows[b]["Disc"]);
                        name = Items.Rows[b]["name"].ToString();

                       
                    }
                    if (Items.Rows.Count == 0)
                    {
                        

                    }
                    else {

                        CategorysDataGridView.Rows.Add(a + 1, name, qty, total);
                    }
                  
                    total = 0;
                    qty = 0;
                    disc = 0;
                    name = "";
                }



            }

        }

        public void customerledger(int code)
        {

            CategorysDataGridView.Rows.Clear();
            //CategorysDataGridView.Rows.Add(1, 2, 3, 4, 5, 6, 7);
            cmbxItem.SelectedValue = Convert.ToInt32(code);
            dtTo.Value = DateTime.Today.AddDays(-10);
            dtFrom.Value = DateTime.Now;
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

            var getdata = db.getCustomerLedgerBYDate(DateTime.Now.AddDays(-10), dtFrom.Value, code).ToList();//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;


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

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CategorysDataGridView.Rows.Count == 0)
            {
                MessageBox.Show("Table is empty ");
                return;


            }

            int a = 0;
            List<KarahiReceipt> orderList = new List<KarahiReceipt>();
            foreach (DataGridViewRow row in CategorysDataGridView.Rows)
            {
                KarahiReceipt orders = new KarahiReceipt();

                orders.SNO = ++a;
                orders.ItemNAme = row.Cells[1].Value.ToString();

                orders.item = row.Cells[1].Value.ToString();
                orders.Note = row.Cells[2].Value.ToString();
                orders.Total =Convert.ToDouble( row.Cells[3].Value.ToString());
                orderList.Add(orders);

            }


            Silent silent = new Silent();
            ReportViewer reportViewer1 = new ReportViewer();


            silent.Run(reportViewer1, orderList, "SalesMngmt.ThermalReport.Item_List.rdlc");

            CategorysDataGridView.Rows.Clear();
        }
    }
}
