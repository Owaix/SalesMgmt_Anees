using Lib;
using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using Lib.Utilities;
using SalesMngmt.Configs;
using SalesMngmt.Reporting;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class AVERAGEITEM : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.Items> list = null;
        int company = 0;
        public AVERAGEITEM(int companyID=1024)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            company = companyID;
        }

    

        private void migrate_data_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            AverageCalculation();
        }

        public void AverageCalculation() {

            var items = db.Items.Where(x => x.CompanyID == company && x.Inv_YN==false && x.isDeleted==false).ToList();

            progressBar1.Maximum = items.Count;
            progressBar1.Value = 0;



            foreach (Lib.Entity.Items item in items)
            {
                int AC_Code_Inv = Convert.ToInt32(item.AC_Code_Inv);
                int AC_Code_Cost = Convert.ToInt32( item.AC_Code_Cost);



                this.IIDAverageSET(item.IID, AC_Code_Inv, AC_Code_Cost);
                progressBar1.Value += 1;
                Application.DoEvents();
            }


            MessageBox.Show("Migration Done");



        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void IIDAverageSET(int IID, int ac_code_inv, int Ac_code_cost)
        {
            List<LedgerAverage> results = new List<LedgerAverage>();

            string Query = @"
SELECT 
    IL.CompanyID,
    IL.IID AS ItemID,

    SUM(
        CASE 
            WHEN IL.TypeCode = 0 THEN IL.OPN * IL.PurPrice
            WHEN IL.TypeCode = 2 THEN IL.PJ * IL.PurPrice
            WHEN IL.TypeCode = 3 THEN -IL.PRJ * IL.PurPrice
            ELSE 0
        END
    ) AS TotalAmount,

    SUM(
        CASE 
            WHEN IL.TypeCode = 0 THEN IL.OPN
            WHEN IL.TypeCode = 2 THEN IL.PJ
            WHEN IL.TypeCode = 3 THEN -IL.PRJ
            ELSE 0
        END
    ) AS TotalQty,

    CASE 
        WHEN SUM(
            CASE 
                WHEN IL.TypeCode = 0 THEN IL.OPN
                WHEN IL.TypeCode = 2 THEN IL.PJ
                WHEN IL.TypeCode = 3 THEN -IL.PRJ
                ELSE 0
            END
        ) = 0 THEN 0
        ELSE 
            SUM(
                CASE 
                    WHEN IL.TypeCode = 0 THEN IL.OPN * IL.PurPrice
                    WHEN IL.TypeCode = 2 THEN IL.PJ * IL.PurPrice
                    WHEN IL.TypeCode = 3 THEN -IL.PRJ * IL.PurPrice
                    ELSE 0
                END
            ) 
            /
            SUM(
                CASE 
                    WHEN IL.TypeCode = 0 THEN IL.OPN
                    WHEN IL.TypeCode = 2 THEN IL.PJ
                    WHEN IL.TypeCode = 3 THEN -IL.PRJ
                    ELSE 0
                END
            )
    END AS AvgPrice

FROM Itemledger IL
WHERE IL.IID = @IID
GROUP BY IL.CompanyID, IL.IID
ORDER BY IL.CompanyID, IL.IID;
";
            SqlCommand cmd = new SqlCommand(Query, SqlHelper.DefaultSqlConnection);
            cmd.CommandType = CommandType.Text;
                       cmd.Parameters.Add("@IID", SqlDbType.Int).Value = IID;
              var rows = SqlHelper.ExecuteDataset(cmd).Tables[0];
            var items = rows.ToList<LedgerAverage>();

            int iid = items[0].ItemID;
            double avgPrice = items[0].AvgPrice;
            using (var context = new SaleManagerEntities())
            {
                var item = context.Items.FirstOrDefault(i => i.IID == iid);
                if (item != null)
                {
                    item.AveragePrice = avgPrice;
                    context.SaveChanges();
                }
            }


            var saleItems = db.Sales_D
     .Where(i => i.IID== iid)
     .ToList();

            foreach (var dbItem in saleItems)
            {
                dbItem.Amt2 = avgPrice * dbItem.Qty;
                dbItem.PurPrice = avgPrice;
            }

            db.SaveChanges();


            var GlInventory = db.GL
    .Where(i => i.AC_Code == ac_code_inv)
    .ToList();

            foreach (var dbItem in GlInventory)
            {
                dbItem.IPrice = avgPrice;
                dbItem.PAmt = avgPrice * dbItem.Qty_Out;
                dbItem.Credit = avgPrice * dbItem.Qty_Out;
                dbItem.Debit = 0;
            }

            db.SaveChanges();


            var GlCost = db.GL
  .Where(i => i.AC_Code == Ac_code_cost)
  .ToList();

            foreach (var dbItem in GlCost)
            {
                dbItem.IPrice = avgPrice;
                dbItem.PAmt = avgPrice * dbItem.Qty_Out;
                dbItem.Debit = avgPrice * dbItem.Qty_Out;
                dbItem.Credit = 0;

            }

            db.SaveChanges();


        }




    }
    public class LedgerAverage
    {
        public int CompanyID { get; set; }
        public int ItemID { get; set; }
        public double TotalAmount { get; set; }
        public double TotalQty { get; set; }
        public double AvgPrice { get; set; }
    }
}
