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
using System.IO.Ports;
using SalesMngmt.Utility;


namespace SalesMngmt.Reporting
{
    public partial class cashBook : MetroFramework.Forms.MetroForm
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




        public cashBook()
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
            var vendorCount = db.Vendors.Where(x => x.CompanyID == 0).FirstOrDefault();
            var AccountCount = db.COA_D.Where(x => x.CAC_Code == 1).FirstOrDefault();
            FillCombo(cmbxVendor, db.Vendors.Where(x => x.CompanyID == 0).ToList(), "VendName", "AC_Code", Convert.ToInt32(vendorCount.AC_Code));
            //  FillCombo(cmbxAccount, db.COA_D.Where(x => x.CAC_Code == 1).ToList(), "AC_Title", "AC_Code", AccountCount.AC_Code);

            // var MyNewDateValue = DateTime.Today.AddDays(-10);

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
            
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
     



        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
         
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}

      

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

         

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CategorysDataGridView.Rows.Clear();
           
            var cashMax = db.getMaxACodeById(1).FirstOrDefault();
            var cashMin = db.getMinACodeById(1).FirstOrDefault();

            var openingDate = db.tbl_OpeningCash.FirstOrDefault();
            if (openingDate == null)
            {

                var cashbook = db.getCAshBookByDate(cashMin, cashMax, dtTo.Value, dtFrom.Value).ToList();
                var count = cashbook.Count();
                int a = 1;
                var balance = 0.0;

                for (int b = 0; b < count; b++)
                {
                    //var abc = new MyModels.VendorLedger();

                    balance = balance - (double)cashbook[b].Credit;
                    balance = balance + (double)cashbook[b].Debit;
                    CategorysDataGridView.Rows.Add(a, (DateTime)cashbook[b].GLDate, cashbook[b].RpCode, (float)cashbook[b].Debit, (float)cashbook[b].Credit, (float)balance,    cashbook[b].Narration);
                    a++;

                }
            }
            else if (openingDate.EntryDate > dtTo.Value)
            {
                var cashbook = db.getCAshBookByDate(cashMin, cashMax, openingDate.EntryDate, dtFrom.Value).ToList();
                var count = cashbook.Count();
                int a = 1;
                var balance = openingDate.Amount;

                CategorysDataGridView.Rows.Add(a, openingDate.EntryDate, "opening Cash book", openingDate.Amount, 0, 0, openingDate.Amount);
                a++;
                for (int b = 0; b < count; b++)
                {
                    //var abc = new MyModels.VendorLedger();

                    balance = balance - (double)cashbook[b].Credit;
                    balance = balance + (double)cashbook[b].Debit;


                    CategorysDataGridView.Rows.Add(a, (DateTime)cashbook[b].GLDate, cashbook[b].RpCode, (float)cashbook[b].Debit, (float)cashbook[b].Credit, (float)balance,  cashbook[b].Narration);
                    a++;

                }


            }
            else if (openingDate.EntryDate < dtTo.Value)
            {

                var date = dtTo.Value.AddDays(-1);
                var cashbook = db.getCAshBookByDate(cashMin, cashMax, openingDate.EntryDate, date).ToList();
                var count = cashbook.Count();
                int a = 1;
                var balance = 0.0;

               

                var credit = 0.0;
                var debit = 0.0;
                debit =Convert.ToDouble( openingDate.Amount.DefaultZero());
                for (int b = 0; b < count; b++)
                {
                    //var abc = new MyModels.VendorLedger();

                    credit = credit + (double)cashbook[b].Credit;
                    debit = debit + (double)cashbook[b].Debit;





                }


                if (credit > debit)
                {
                    balance = credit - debit;

                    CategorysDataGridView.Rows.Add(a, "", "Previous", 0, balance, balance, 0);
                    a++;
                }
                else if (debit > credit)
                {

                    balance = debit - credit;
                    CategorysDataGridView.Rows.Add(a, "", "Previous amount", balance, 0, balance, 0);
                    a++;

                }
                else
                {


                    CategorysDataGridView.Rows.Add(a, "", "Previous amount", debit, credit, balance, 0);
                    a++;
                }


                var cashbook1 = db.getCAshBookByDate(cashMin, cashMax, dtTo.Value, dtFrom.Value).ToList();
                var count1 = cashbook1.Count();


                for (int b = 0; b < count1; b++)
                {
                    //var abc = new MyModels.VendorLedger();

                    balance = balance - (double)cashbook1[b].Credit;
                    balance = balance + (double)cashbook1[b].Debit;


                    CategorysDataGridView.Rows.Add(a, (DateTime)cashbook1[b].GLDate, cashbook1[b].RpCode, (float)cashbook1[b].Debit, (float)cashbook1[b].Credit, (float)balance, cashbook1[b].Narration );
                    a++;

                }




            }


            else if (openingDate.EntryDate.Value.Date == dtTo.Value.Date)
            {
             

                var cashbook = db.getCAshBookByDate(cashMin, cashMax, openingDate.EntryDate, dtFrom.Value).ToList();
                var count = cashbook.Count();
                int a = 1;
                var balance = openingDate.Amount;

                CategorysDataGridView.Rows.Add(a, openingDate.EntryDate, "opening Cash book", openingDate.Amount, 0, 0, openingDate.Amount);
                a++;
                for (int b = 0; b < count; b++)
                {
                    //var abc = new MyModels.VendorLedger();

                    balance = balance - (double)cashbook[b].Credit;
                    balance = balance + (double)cashbook[b].Debit;


                    CategorysDataGridView.Rows.Add(a, (DateTime)cashbook[b].GLDate, cashbook[b].RpCode, (float)cashbook[b].Debit, (float)cashbook[b].Credit, (float)balance, cashbook[b].Narration);
                    a++;

                }



            }

            }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }


     
    }


