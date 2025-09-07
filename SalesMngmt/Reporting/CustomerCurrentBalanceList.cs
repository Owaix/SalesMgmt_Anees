using Lib.Entity;
using Lib.Model;
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
using Lib;
using DIagnoseMgmt;

namespace SalesMngmt.Reporting
{
    public partial class CustomerCurrentBalanceList : Form
    {

        SaleManagerEntities db = null;
        int compy = 0;
        public CustomerCurrentBalanceList(int company)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compy = company;

            List<tbl_city> City = new List<tbl_city>();
            City.Add(new tbl_city { Id = 0, CityName = "--Select--" });
            City.AddRange(db.tbl_city.AsNoTracking().Where(x => x.CompyID == company && x.isDeleted == false).ToList());
            FillCombo(cmbxCity, City, "CityName", "Id", 0);

        }
        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected)
        {
            comboBox.DataSource = obj;
            comboBox.DisplayMember = Name; // Column Name
            comboBox.ValueMember = ID;  // Column Name
            comboBox.SelectedIndex = selected;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();



            int city = Convert.ToInt32(cmbxCity.SelectedValue);
            if (city==0)
            {
                lblAmount.Text = "0";
                var customer = db.Customers.AsNoTracking().Where(x => x.InActive == false && x.CompanyID == compy);
                double TotalcustomersBalance = 0;

                int a = 1;
                foreach (var cus in customer)
                {
                    double balance = 0;
                    DateTime aa = dtpDate.Value.AddDays(-1);
                    var previosBalance = ReportsController.getCustomerPreviousBalance(dtpDate.Value, cus.AC_Code);

                    if (a == 100) {

                        int b = 10;
                    }
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

                    dataGridView1.Rows.Add(a, cus.CusName, balance);
                    a++;
                }

                lblAmount.Text = TotalcustomersBalance.ToString();

            }
            else {


                lblAmount.Text = "0";
                var customer = db.Customers.AsNoTracking().Where(x => x.InActive == false && x.CompanyID == compy && x.City== city);
                double TotalcustomersBalance = 0;

                int a = 1;
                foreach (var cus in customer)
                {
                    double balance = 0;

                    var previosBalance = ReportsController.getCustomerPreviousBalance(dtpDate.Value, cus.AC_Code);


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

                    dataGridView1.Rows.Add(a, cus.CusName, balance);
                    a++;
                }

                lblAmount.Text = TotalcustomersBalance.ToString();


            }


            //get customer
           




        }

        private void btnReport_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count == 0 || dataGridView1.Rows.Count == null)
            {

                MessageBox.Show("Please search First Thanks");

            }


            List<BalanceList> orderList = new List<BalanceList>();
            for (int a = 0; a < dataGridView1.Rows.Count; a++)
            {

                BalanceList order = new BalanceList();
                order.Sno = (int)dataGridView1.Rows[a].Cells[0].Value;
              
                order.Name = dataGridView1.Rows[a].Cells[1].Value.ToString();
           

                order.Date = dtpDate.Value.ToString("dd/MM/yyyy");
               
             
                order.debit = Convert.ToDouble(dataGridView1.Rows[a].Cells[2].Value.ToString());
              order.Note = "CUSTOMER CURRENT BALANCE";
                int city =Convert.ToInt32( cmbxCity.SelectedValue);
                if (city == 0) { order.LegerName = ""; }
                else {
                    order.LegerName = db.tbl_city.AsNoTracking().Where(x => x.CompyID == compy && x.Id == city).FirstOrDefault().CityName;
                }
               
                // add multiple company
                var Companies = new Companies().GetCompanyID(compy);
                orderList.ForEach(x =>
                {
                    x.CompanyTitle = Companies.CompanyTitle;
                    x.CompanyPhone = Companies.CompanyPhone;
                    x.CompanyAddress = Companies.CompanyAddress;
                    x.CompanyID = Companies.CompanyID;
                });

                orderList.Add(order);
            }


            Form2 form2 = new Form2(orderList);
            form2.Show();
        }
    }
}
