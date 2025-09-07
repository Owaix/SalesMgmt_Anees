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
                this.IIDAverageSET(item.IID);
                progressBar1.Value += 1;
                Application.DoEvents();
            }


            MessageBox.Show("Migration Done");



        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        public void IIDAverageSET(int IID)
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
            //    using (SqlConnection conn = new SqlConnection(Query,SqlHelper.DefaultSqlConnection))
            //    {
            //        conn.Open();

            //        using (SqlCommand cmd = new SqlCommand(Query, conn))
            //        {
            //            cmd.CommandType = CommandType.Text;
            //            cmd.Parameters.Add("@IID", SqlDbType.Int).Value = IID;

            //            using (SqlDataReader reader = cmd.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    LedgerAverage item = new LedgerAverage
            //                    {
            //                        CompanyID = reader.GetInt32(reader.GetOrdinal("CompanyID")),
            //                        ItemID = reader.GetInt32(reader.GetOrdinal("ItemID")),
            //                        TotalAmount = reader.IsDBNull(reader.GetOrdinal("TotalAmount")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
            //                        TotalQty = reader.IsDBNull(reader.GetOrdinal("TotalQty")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalQty")),
            //                        AvgPrice = reader.IsDBNull(reader.GetOrdinal("AvgPrice")) ? 0 : reader.GetDecimal(reader.GetOrdinal("AvgPrice"))
            //                    };

            //                    results.Add(item);
            //                }
            //            }
            //        }
            //    }


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
