using DIagnoseMgmt;
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

namespace SalesMngmt.Reporting
{
    public partial class Stock : MetroFramework.Forms.MetroForm
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




        public Stock(int comp)
        {
            InitializeComponent();

            db = new SaleManagerEntities();

            compID = comp;


            List<Item_Maker> article = new List<Item_Maker>();
            article.Add(new Item_Maker { MakerId = 0, Name = "--SELECT--" });
            article.AddRange(db.Item_Maker.Where(x => x.CompanyID==comp && x.IsDelete==false).ToList());
            FillCombo(cmbxCompany, article, "Name", "MakerId", 0);

            List<tbl_Warehouse> Warehouse = new List<tbl_Warehouse>();
            Warehouse.Add(new tbl_Warehouse { WID = 0, Warehouse = "--Select--" });
            Warehouse.AddRange(db.tbl_Warehouse.Where(x => x.CompanyID == compID && x.isDelete == false).ToList());
            FillCombo(cmbxWareHouse, Warehouse, "Warehouse", "WID", 0);
            List<Items_Cat> catergory = new List<Items_Cat>();
            catergory.Add(new Items_Cat { CatID = 0, Cat = "--SELECT--" });
            catergory.AddRange(db.Items_Cat.Where(x => x.CompanyID == compID && x.isDeleted == false).ToList());

            FillCombo(cmbxCat, catergory, "Cat", "CatID", 0);
 

        }


        private void Category_Load(object sender, EventArgs e)
        {

           

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
            int catergory = (int)cmbxCat.SelectedValue;
            int warehouse = (int)cmbxWareHouse.SelectedValue;
            if (warehouse == 0)
            {







                int companyID = (int)cmbxCompany.SelectedValue;


                if (companyID == 0)
                {


                    var item = db.Items.Where(x => x.isDeleted == false && x.CompanyID == compID && x.Inv_YN == false).ToList();
                    if (catergory == 0)
                    {

                    }
                    else {
                    item=    db.Items.Where(x => x.isDeleted == false && x.CompanyID == compID && x.Inv_YN == false && x.SCatID== catergory).ToList();

                    }
                    var itemcount = item.Count;
                    int Sno = 1;


                    for (int a = 0; a < itemcount; a++)
                    {
                        var previousStock = ReportsController.getStockByPreviosDate(item[a].IID, compID, dtTo.Value);
                        var Todaypurchase = ReportsController.getStockByPurchaseDate(item[a].IID, compID, dtTo.Value);
                        var TodaySale = ReportsController.getStockBySaleDate(item[a].IID, compID, dtTo.Value);
                        var saleAmount = ReportsController.getStockBySalePrice(item[a].IID, compID, dtTo.Value);

                        var TodayPurchase = ReportsController.getStockByPurchasePrice(item[a].IID, compID, dtTo.Value);
                        //   double demandQuantity = Convert.ToDouble(item[a].CTN_PCK) * Convert.ToDouble(item[a].Demand);
                        var stockQuantity = ReportsController.getTodayStockByID(item[a].IID, compID, dtTo.Value);
                        var total = (double)previousStock.Rows[0]["total"] + (double)Todaypurchase.Rows[0]["total"];
                        var OpenAmt = (double)previousStock.Rows[0]["total"] * (double)item[a].AveragePrice;
                        var TotalStock = Convert.ToDouble(((double)stockQuantity.Rows[0]["total"] + (double)previousStock.Rows[0]["total"])) * item[a].AveragePrice;

                        CategorysDataGridView.Rows.Add(Sno, item[a].IName, previousStock.Rows[0]["total"], Todaypurchase.Rows[0]["total"], total, TodaySale.Rows[0]["total"], (double)stockQuantity.Rows[0]["total"] + (double)previousStock.Rows[0]["total"], saleAmount.Rows[0]["total"], TodayPurchase.Rows[0]["total"], OpenAmt, TotalStock);
                        Sno++;
                    }

                }

                else
                {


                    var item = db.Items.Where(x => x.isDeleted == false && x.CompanyID == compID && x.Inv_YN == false && x.CompID == companyID).ToList();

                    if (catergory == 0) { }

                    else {
                        item = db.Items.Where(x => x.isDeleted == false && x.CompanyID == compID && x.Inv_YN == false && x.CompID == companyID && x.SCatID==catergory).ToList();


                    }

                    var itemcount = item.Count;
                    int Sno = 1;


                    for (int a = 0; a < itemcount; a++)
                    {
                        var previousStock = ReportsController.getStockByPreviosDate(item[a].IID, compID, dtTo.Value);
                        var Todaypurchase = ReportsController.getStockByPurchaseDate(item[a].IID, compID, dtTo.Value);
                        var TodaySale = ReportsController.getStockBySaleDate(item[a].IID, compID, dtTo.Value);

                        var saleAmount = ReportsController.getStockBySalePrice(item[a].IID, compID, dtTo.Value);

                        var TodayPurchase = ReportsController.getStockByPurchasePrice(item[a].IID, compID, dtTo.Value);


                        //   double demandQuantity = Convert.ToDouble(item[a].CTN_PCK) * Convert.ToDouble(item[a].Demand);
                        var stockQuantity = ReportsController.getTodayStockByID(item[a].IID, compID, dtTo.Value);
                        var total = (double)previousStock.Rows[0]["total"] + (double)Todaypurchase.Rows[0]["total"];
                        var TotalStock = Convert.ToDouble(((double)stockQuantity.Rows[0]["total"] + (double)previousStock.Rows[0]["total"])) * item[a].AveragePrice;
                        CategorysDataGridView.Rows.Add(Sno, item[a].IName, previousStock.Rows[0]["total"], Todaypurchase.Rows[0]["total"], total, TodaySale.Rows[0]["total"], (double)stockQuantity.Rows[0]["total"] + (double)previousStock.Rows[0]["total"], saleAmount.Rows[0]["total"], TodayPurchase.Rows[0]["total"], (double)previousStock.Rows[0]["total"] * (double)item[a].AveragePrice, TotalStock);
                        Sno++;
                    }




                }






                //    stockQuantity.Rows[0]["total"].ToString();
                //           var ctn = Convert.ToDouble(item[a].CTN_PCK);
                //    if (ctn == 0) {

                //        ctn = 1;
                //    }
                //        var countCtn = 0;


                //        for (double b = ctn; b <=((double) stockQuantity.Rows[0]["total"]); b += ctn)
                //        {

                //            countCtn++;


                //        }
                //        double ctnValue= (countCtn * Convert.ToDouble(item[a].CTN_PCK));
                //    if (ctnValue == 0)
                //    {

                //        var quantity = ((double) stockQuantity.Rows[0]["total"]) - (countCtn * Convert.ToDouble(item[a].CTN_PCK));
                //        CategorysDataGridView.Rows.Add(Sno, item[a].IName, 0, quantity, item[a].CTN_PCK);
                //    }
                //    else {
                //        var quantity = ((double) stockQuantity.Rows[0]["total"]) - (countCtn * Convert.ToDouble(item[a].CTN_PCK));
                //        CategorysDataGridView.Rows.Add(Sno, item[a].IName, countCtn, quantity, item[a].CTN_PCK);
                //    }
                //        Sno++;


                //}



            }


            else {

                int companyID = (int)cmbxCompany.SelectedValue;


                if (companyID == 0)
                {


                    var item = db.Items.Where(x => x.isDeleted == false && x.CompanyID == compID && x.Inv_YN == false).ToList();

                    if (catergory == 0)
                    {


                    }
                    else {
                        item = db.Items.Where(x => x.isDeleted == false && x.CompanyID == compID && x.Inv_YN == false && x.SCatID==catergory).ToList();

                    }
                    var itemcount = item.Count;
                    int Sno = 1;


                    for (int a = 0; a < itemcount; a++)
                    {
                        var previousStock = ReportsController.getStockByPreviosDateByWareHouse(item[a].IID, compID, dtTo.Value,warehouse);
                        var Todaypurchase = ReportsController.getStockByPurchaseDateByWarehouse(item[a].IID, compID, dtTo.Value, warehouse);
                        var TodaySale = ReportsController.getStockBySaleDateByWarehouse(item[a].IID, compID, dtTo.Value, warehouse);
                        var saleAmount = ReportsController.getStockBySalePriceByWarehouse(item[a].IID, compID, dtTo.Value, warehouse);

                        var TodayPurchase = ReportsController.getStockByPurchasePriceByWareHouse(item[a].IID, compID, dtTo.Value, warehouse);
                        //   double demandQuantity = Convert.ToDouble(item[a].CTN_PCK) * Convert.ToDouble(item[a].Demand);
                        var stockQuantity = ReportsController.getTodayStockByIDByWareHouse(item[a].IID, compID, dtTo.Value, warehouse);
                        var total = (double)previousStock.Rows[0]["total"] + (double)Todaypurchase.Rows[0]["total"];
                        var OpenAmt = (double)previousStock.Rows[0]["total"] * (double)item[a].AveragePrice;
                        var TotalStock = Convert.ToDouble(((double)stockQuantity.Rows[0]["total"] + (double)previousStock.Rows[0]["total"])) * item[a].AveragePrice;
                        CategorysDataGridView.Rows.Add(Sno, item[a].IName, previousStock.Rows[0]["total"], Todaypurchase.Rows[0]["total"], total, TodaySale.Rows[0]["total"], (double)stockQuantity.Rows[0]["total"] + (double)previousStock.Rows[0]["total"], saleAmount.Rows[0]["total"], TodayPurchase.Rows[0]["total"], (double)previousStock.Rows[0]["total"] * (double)item[a].AveragePrice, TotalStock);
                        Sno++;
                    }

                }

                else
                {


                    var item = db.Items.Where(x => x.isDeleted == false && x.CompanyID == compID && x.Inv_YN == false && x.CompID == companyID).ToList();

                    if (catergory==0) { }

                    else {
                        item = db.Items.Where(x => x.isDeleted == false && x.CompanyID == compID && x.Inv_YN == false && x.CompID == companyID && x.SCatID==catergory).ToList();

                    }

                    var itemcount = item.Count;
                    int Sno = 1;


                    for (int a = 0; a < itemcount; a++)
                    {
                        var previousStock = ReportsController.getStockByPreviosDateByWareHouse(item[a].IID, compID, dtTo.Value, warehouse);
                        var Todaypurchase = ReportsController.getStockByPurchaseDateByWarehouse(item[a].IID, compID, dtTo.Value, warehouse);
                        var TodaySale = ReportsController.getStockBySaleDateByWarehouse(item[a].IID, compID, dtTo.Value, warehouse);

                        var saleAmount = ReportsController.getStockBySalePriceByWarehouse(item[a].IID, compID, dtTo.Value, warehouse);

                        var TodayPurchase = ReportsController.getStockByPurchasePriceByWareHouse(item[a].IID, compID, dtTo.Value, warehouse);


                        //   double demandQuantity = Convert.ToDouble(item[a].CTN_PCK) * Convert.ToDouble(item[a].Demand);
                        var stockQuantity = ReportsController.getTodayStockByIDByWareHouse(item[a].IID, compID, dtTo.Value, warehouse);
                        var total = (double)previousStock.Rows[0]["total"] + (double)Todaypurchase.Rows[0]["total"];
                        var TotalStock = Convert.ToDouble(((double)stockQuantity.Rows[0]["total"] + (double)previousStock.Rows[0]["total"])) * item[a].AveragePrice;
                        CategorysDataGridView.Rows.Add(Sno, item[a].IName, previousStock.Rows[0]["total"], Todaypurchase.Rows[0]["total"], total, TodaySale.Rows[0]["total"], (double)stockQuantity.Rows[0]["total"] + (double)previousStock.Rows[0]["total"], saleAmount.Rows[0]["total"], TodayPurchase.Rows[0]["total"],(double) previousStock.Rows[0]["total"] *( double)item[a].AveragePrice, TotalStock);
                        Sno++;
                    }




                }



            }

            Double Totalvalue = 0;
            for (int a = 0; a < CategorysDataGridView.Rows.Count; a++)
            {

                Totalvalue += Convert.ToDouble(CategorysDataGridView.Rows[a].Cells[10].Value.ToString());

            }
            lblStockValue.Text = Totalvalue.ToString();




            }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CategorysDataGridView.Rows.Count == 0 || CategorysDataGridView.Rows.Count == null) {

                MessageBox.Show("Please search First Thanks");

            }


            List<StockReport> orderList = new List<StockReport>();
            for (int a = 0; a < CategorysDataGridView.Rows.Count; a++)
            {

                StockReport order = new StockReport();
                order.Sno = (int)CategorysDataGridView.Rows[a].Cells[0].Value;
                order.ProductName = CategorysDataGridView.Rows[a].Cells[1].Value.ToString();
                order.Opening =Convert.ToDouble( CategorysDataGridView.Rows[a].Cells[2].Value.ToString());
                order.purchase =Convert.ToDouble( CategorysDataGridView.Rows[a].Cells[3].Value.ToString());
                order.Total = Convert.ToDouble( CategorysDataGridView.Rows[a].Cells[4].Value.ToString());
                order.Sale = Convert.ToDouble( CategorysDataGridView.Rows[a].Cells[5].Value.ToString());
                order.Closing =Convert.ToDouble( CategorysDataGridView.Rows[a].Cells[6].Value.ToString());
                order.TotalSale = Convert.ToDouble(CategorysDataGridView.Rows[a].Cells[7].Value.ToString());
                order.TotalPurchase= Convert.ToDouble( CategorysDataGridView.Rows[a].Cells[8].Value.ToString());
               
                order.OpenAmt =Convert.ToDouble( CategorysDataGridView.Rows[a].Cells[9].Value.ToString());
                order.Average= Convert.ToDouble(CategorysDataGridView.Rows[a].Cells[10].Value.ToString());
                if (cmbxCompany.Text == "--SELECT--") {

                    order.Company ="null";
                }

                else { 

                order.Company = cmbxCompany.Text;
                }
                order.Date = dtTo.Value.ToString("dd/MM/yyyy");
            orderList.Add(order);
            }
            Form2 form2 = new Form2(orderList);
            form2.Show();
        }
    }
    
    //    public Stock()
    //    {
    //        InitializeComponent();
    //    }
    //}
}
