using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using Microsoft.Win32;
using SalesMngmt.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class Bike_Sale_Purchase : MetroFramework.Forms.MetroForm
    {
     System.Windows.Forms.OpenFileDialog openFileDialog1 { get; set; }

    System.Windows.Forms.OpenFileDialog openFileDialog2 { get; set; }
        SaleManagerEntities db = null;
        int compID = 0;
        public Bike_Sale_Purchase(int companyID)
        {
            InitializeComponent();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            db = new SaleManagerEntities();
            compID = companyID;

            List<COA_M> article = new List<COA_M>();
            article.Add(new COA_M { CAC_Code = 0, CATitle = "--SELECT--" });
            article.AddRange(db.COA_M.Where(x => x.CAC_Code == 1 || x.CAC_Code == 3 || x.CAC_Code == 9).ToList());
            FillCombo(cmbxPurchaseAccID, article, "CATitle", "CAC_Code", 0);
            FillCombo(cmbxSaleAccID, article, "CATitle", "CAC_Code", 0);


            List<Lib.Entity.Items> items = new List<Lib.Entity.Items>();
            items.Add(new Lib.Entity.Items { IID = 0, IName = "--SELECT--" });

            // items.AddRange(db.Items.Where(x => x.CompID == companyID).ToList());
            items.AddRange(db.Items.Where(x => x.CompanyID == companyID).ToList());

            FillCombo(cmbxProduct, items, "IName", "IID", 0);
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                      //  label21.Text = openFileDialog1.FileName;
                        pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload image.");
                }
            }
            catch (Exception ex)
            {
                //it will give if file is already exits..
                MessageBox.Show(ex.Message);
            }
        }
        public void FillCombo<T>(ComboBox comboBox, IEnumerable<T> obj, String Name, String ID, int selected = 0)
        {
            try
            {
                if (obj.Count() > 0)
                {
                    comboBox.DataSource = obj;
                    comboBox.DisplayMember = Name; // Column Name
                    comboBox.ValueMember = ID;  // Column Name
                    comboBox.SelectedIndex = selected;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = "C://Desktop";
            openFileDialog2.Title = "Select image to be upload.";
            openFileDialog2.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog2.FilterIndex = 1;
            try
            {
                if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog2.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog2.FileName);
                       // label21.Text = openFileDialog2.FileName;
                        pictureBox2.Image = new Bitmap(openFileDialog2.FileName);
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload image.");
                }
            }
            catch (Exception ex)
            {
                //it will give if file is already exits..
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbxPurchaseAccID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsa = Convert.ToInt32(cmbxPurchaseAccID.SelectedIndex);
            if (dsa >= 1)
            {
                int value = Convert.ToInt32(cmbxPurchaseAccID.SelectedValue);
                var vendor = db.COA_D.AsNoTracking().Where(x => x.CAC_Code == value && x.CompanyID == compID && x.InActive == false).ToList();
                FillCombo(cmbxvendor, vendor, "AC_Title", "AC_Code", 0);




                if (value == 3)
                {

                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getcustumerList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
                    }

                    FillCombo(cmbxvendor, customerList, "Name", "AC_Code", 0);



                  
                  


                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    // vebdorid= Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = debit - credit;

            


                }


                if (value == 9)
                {

                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getVendorList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
                    }

                    FillCombo(cmbxvendor, customerList, "Name", "AC_Code", 0);

                    double balance = 0;
                
               

                 




                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    //vebdorid = Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = credit - debit;

                   

                }


            }
            else if (dsa == 0)
            {

                //   int value = Convert.ToInt32(cmbxAccID.SelectedValue);
                //var vendor = db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList();
                //FillCombo(cmbxvendor, vendor, "AC_Title", "CAC_Code", 0);

                List<COA_D> parties = new List<COA_D>();
                parties.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
                FillCombo(cmbxvendor, parties, "AC_Title", "AC_Code", 0);
                FillCombo(cmbxvendor, parties, "AC_Title", "AC_Code", 0);
                FillCombo(cmbxvendor, parties, "AC_Title", "AC_Code", 0);

            }
        }

        private void cmbxSaleAccID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsa = Convert.ToInt32(cmbxSaleAccID.SelectedIndex);
            if (dsa >= 1)
            {
                int value = Convert.ToInt32(cmbxSaleAccID.SelectedValue);
                var vendor = db.COA_D.AsNoTracking().Where(x => x.CAC_Code == value && x.CompanyID == compID && x.InActive == false).ToList();
                FillCombo(cmbxCustomer, vendor, "AC_Title", "AC_Code", 0);




                if (value == 3)
                {

                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getcustumerList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
                    }

                    FillCombo(cmbxCustomer, customerList, "Name", "AC_Code", 0);







                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    // vebdorid= Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = debit - credit;




                }


                if (value == 9)
                {

                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getVendorList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
                    }

                    FillCombo(cmbxCustomer, customerList, "Name", "AC_Code", 0);

                    double balance = 0;








                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    //vebdorid = Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = credit - debit;



                }


            }
            else if (dsa == 0)
            {

                //   int value = Convert.ToInt32(cmbxAccID.SelectedValue);
                //var vendor = db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList();
                //FillCombo(cmbxvendor, vendor, "AC_Title", "CAC_Code", 0);

                List<COA_D> parties = new List<COA_D>();
                parties.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
                FillCombo(cmbxCustomer, parties, "AC_Title", "AC_Code", 0);
                FillCombo(cmbxCustomer, parties, "AC_Title", "AC_Code", 0);
                FillCombo(cmbxCustomer, parties, "AC_Title", "AC_Code", 0);

            }
        }
    }
}
