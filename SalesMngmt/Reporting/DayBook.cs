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

namespace SalesMngmt.Reporting
{
    public partial class DayBook : Form
    {

        SaleManagerEntities db = null;
        int compy = 0;
        public DayBook(int company)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compy = company;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();

            var Sale = ReportsController.getTotalSaleAfterDiscount(compy, dtpDate.Value);
            dataGridView1.Rows.Add(1, "Sale", (double)Sale.Rows[0]["NetAmt"] - (double)Sale.Rows[0]["DisAmt"]);

            var Purchase = ReportsController.getTotalPurchaseAfterDiscount(compy, dtpDate.Value);
            dataGridView1.Rows.Add(2, "Purchase", (double)Purchase.Rows[0]["NetAmt"] - (double)Purchase.Rows[0]["DisAmt"]);

            var payment = ReportsController.getTotalPaymentAfterDiscount(compy, dtpDate.Value);
            dataGridView1.Rows.Add(3, "payment", (double)payment.Rows[0]["Amt"]);

            var Receive = ReportsController.getTotalReceiveAfterDiscount(compy, dtpDate.Value);
            dataGridView1.Rows.Add(4, "Receive", (double)Receive.Rows[0]["Amt"]);





            //get customer

            var customer = db.Customers.AsNoTracking().Where(x => x.InActive == false && x.CompanyID == compy);
            double TotalcustomersBalance = 0;


            foreach (var cus in customer)
            {
                double balance = 0;
               
                var previosBalance = ReportsController.getCustomerPreviousBalance(dtpDate.Value, cus.AC_Code);
                int a = 1;

                double credit = (double)previosBalance.Rows[0]["credit"];
                double debit = (double)previosBalance.Rows[0]["debit"];
                 balance = debit - credit;
                if (balance != 0)
                {


                }
                SaleManagerEntities db1 = new SaleManagerEntities();

                var getdata = ReportsController.getcustomerLedgerSummaryByDate(dtpDate.Value, dtpDate.Value, cus.AC_Code);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                foreach (DataRow rows in getdata.Rows)
                {


                    balance = balance - (double)rows["credit"];
                    balance = balance + (double)rows["debit"];








                }

                TotalcustomersBalance += balance;



            }
            dataGridView1.Rows.Add(5, "you Will receive", TotalcustomersBalance);



            // get vendors

            var vendors = db.Vendors.AsNoTracking().Where(x => x.InActive == false && x.CompanyID == compy);
            double TotalVendorsBalance = 0;


            foreach (var ven in vendors)
            {
                double balance = 0;

                var previosBalance = ReportsController.getVendorPreviousBalance(dtpDate.Value,(int) ven.AC_Code);
                int a = 1;

                double credit = (double)previosBalance.Rows[0]["credit"];
                double debit = (double)previosBalance.Rows[0]["debit"];
                balance = credit - debit;
                if (balance != 0)
                {


                }
                SaleManagerEntities db1 = new SaleManagerEntities();

                var getdata = ReportsController.getVendorLedgerSummaryByDate(dtpDate.Value, dtpDate.Value, (int)ven.AC_Code);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                foreach (DataRow rows in getdata.Rows)
                {


                    balance = balance + (double)rows["credit"];
                    balance = balance - (double)rows["debit"];






                }

                TotalVendorsBalance += balance;



            }
            dataGridView1.Rows.Add(6, "you Will Pay", TotalVendorsBalance);

        }
    }
}
