using Lib.Entity;
using Lib.Reporting;
using Microsoft.Reporting.WinForms;
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
    public partial class ProfitAndLoss : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        int CompyID = 0;
        public ProfitAndLoss(int Company)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            CompyID = Company;
        }

        private void ProfitAndLoss_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
         //   List<Lib.Reporting.ProfitAnd_Loss> Compare = new List<Lib.Reporting.ProfitAnd_Loss>();

            List<Lib.Reporting.ProfitAnd_Loss> profit = new List<Lib.Reporting.ProfitAnd_Loss>();
            var Coa_M = db.COA_M.Where(x => x.CAC_Code == 14 || x.CAC_Code == 15 || x.CAC_Code == 16).ToList();

            double TotalIncome =0;
            double TotalExpense =0;
            double TotalCost=0;

            for (int a = 14; a <= 16; a++)
            {
                int id = a;
                var cashMax = ReportsController.getMaxACodeById(id, CompyID);
                var cashMin = ReportsController.getMixACodeById(id, CompyID);

                var cashbook1 = ReportsController.getProfitAndLoss((int)cashMin.Rows[0]["Min"], (int)cashMax.Rows[0]["Max"], dtTo.Value, dtFrom.Value,CompyID);

                foreach (DataRow table in cashbook1.Rows)
                {


                    if (a == 14) {

                        TotalIncome += Convert.ToDouble( table["Credit"]);

                    }

                    else if (a == 15) {
                        TotalCost += Convert.ToDouble(table["debit"]);

                    }
                    else if (a == 16) {
                        TotalExpense += Convert.ToDouble(table["debit"]);
                    }
                }
            }
            foreach (var table in Coa_M)
            {
                Lib.Reporting.ProfitAnd_Loss detail = new Lib.Reporting.ProfitAnd_Loss();


                detail.AccountHeader = table.CATitle;
                detail.ID = table.CAC_Code;
                detail.TotalExpense = Math.Round(Math.Abs( TotalExpense));
                detail.TotalIncome = Math.Round(Math.Abs( TotalIncome));
                detail.TotalCost = Math.Round(Math.Abs( TotalCost));

                detail.Remaining = Math.Round(detail.TotalIncome - detail.TotalExpense - detail.TotalCost);
                //    Compare.Add(check);
                //    //More code here
                //}
                profit.Add(detail);

            }

            reportViewer1.Reset();



            ReportDataSource ds = new ReportDataSource("DataSet1", profit);


            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(ds);

            reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.Reporting.Definition.ProfitAndLoss.rdlc";
            reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OrderDetailProcession);
            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();
        }
        void OrderDetailProcession(object sender, SubreportProcessingEventArgs e) {

            int id = Convert.ToInt32(e.Parameters["ID"].Values[0].ToString());
           
            List<Lib.Reporting.ProfitAnd_Loss> profit = new List<Lib.Reporting.ProfitAnd_Loss>();
           

            var cashMax = ReportsController.getMaxACodeById(id, CompyID);
            var cashMin = ReportsController.getMixACodeById(id, CompyID);

            var cashbook1 = ReportsController.getProfitAndLoss((int)cashMin.Rows[0]["Min"], (int)cashMax.Rows[0]["Max"], dtTo.Value, dtFrom.Value, CompyID);





            foreach (DataRow table in cashbook1.Rows) {


                Lib.Reporting.ProfitAnd_Loss detail = new Lib.Reporting.ProfitAnd_Loss();

                if (id == 14)
                {
                    detail.Price =Math.Abs( Convert.ToDouble(table["Credit"]));
                }
                else {
                    detail.Price =Math.Abs( Convert.ToDouble(table["debit"]));
                }
                detail.AccountHeader = table["AC_Title"].ToString();
             //   detail.
             //detail.ID = table.CAC_Code;

                detail.Name = table["AC_Title"].ToString();
                //    Compare.Add(check);
                //    //More code here
                //}
                profit.Add(detail);

            }
            ReportDataSource ds = new ReportDataSource("Detail", profit);
            e.DataSources.Add(ds);
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
