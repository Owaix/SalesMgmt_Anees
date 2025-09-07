using Lib.Entity;
using Lib.Reporting;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using Microsoft.Reporting.WinForms;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SalesMngmt.Reporting
{
    public partial class AccountComparesion : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<COA_D> list = null;
        int compID = 0;

        public object ChartAreaType { get; private set; }

        public AccountComparesion()
        {
            InitializeComponent();
            db = new SaleManagerEntities();

         
        }

        public void first()
        {

            CategorysDataGridView.Rows.Clear();
            int first = Convert.ToInt32(cmbxAccID.SelectedValue);
            // int Second = Convert.ToInt32(cmbxCreditAccount.SelectedValue);
            var cashMax = db.getMaxACodeById(first).FirstOrDefault();
            var cashMin = db.getMinACodeById(first).FirstOrDefault();

            var openingDate = db.tbl_OpeningCash.FirstOrDefault();








            var balance = 0.0;



            var cashbook1 = db.getCAshBookByDate(cashMin, cashMax, dtTo.Value, dtFrom.Value).ToList();
            var count1 = cashbook1.Count();
            int cacCode = Convert.ToInt32(cmbxAccID.SelectedValue);
            var Mcode = db.COA_M.AsNoTracking().Where(x => x.CAC_Code == cacCode).FirstOrDefault();


            int a = 1;
            for (int b = 0; b < count1; b++)
            {
                //var abc = new MyModels.VendorLedger();
                if (Mcode.MAC_Code == 2 || Mcode.MAC_Code == 3 || Mcode.MAC_Code == 4)
                {
                    balance = balance + (double)cashbook1[b].Credit;
                    balance = balance - (double)cashbook1[b].Debit;
                }
                else
                {


                    balance = balance - (double)cashbook1[b].Credit;
                    balance = balance + (double)cashbook1[b].Debit;
                }

                CategorysDataGridView.Rows.Add(a, (DateTime)cashbook1[b].GLDate, cashbook1[b].Narration, (float)cashbook1[b].Debit, (float)cashbook1[b].Credit, (float)balance, cashbook1[b].RpCode);


                a++;



            }





        }

        public void Second()
        {
            dataGridView1.Rows.Clear();
            int first = Convert.ToInt32(cmbxCreditAccount.SelectedValue);
            // int Second = Convert.ToInt32(cmbxCreditAccount.SelectedValue);
            var cashMax = db.getMaxACodeById(first).FirstOrDefault();
            var cashMin = db.getMinACodeById(first).FirstOrDefault();

            var openingDate = db.tbl_OpeningCash.FirstOrDefault();








            var balance =0.0;
              


                var cashbook1 = db.getCAshBookByDate(cashMin, cashMax, dtTo.Value, dtFrom.Value).ToList();
                var count1 = cashbook1.Count();
            int cacCode = Convert.ToInt32(cmbxCreditAccount.SelectedValue);
            var Mcode = db.COA_M.AsNoTracking().Where(x => x.CAC_Code == cacCode).FirstOrDefault();

           
                int a = 1;
                for (int b = 0; b < count1; b++)
                {
                //var abc = new MyModels.VendorLedger();
                if (Mcode.MAC_Code == 2 || Mcode.MAC_Code == 3 || Mcode.MAC_Code == 4)
                {
                    balance = balance + (double)cashbook1[b].Credit;
                    balance = balance - (double)cashbook1[b].Debit;
                }
                else
                {


                    balance = balance - (double)cashbook1[b].Credit;
                    balance = balance + (double)cashbook1[b].Debit;
                }

                dataGridView1.Rows.Add(a, (DateTime)cashbook1[b].GLDate, cashbook1[b].Narration, (float)cashbook1[b].Debit, (float)cashbook1[b].Credit, (float)balance, cashbook1[b].RpCode);


                a++;


                
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
        public void LoadFuncaTion()
        {
            List<Article> article = new List<Article>();

            article.Add(new Article { ProductID = 0, ArticleNo = "--SELECT--" });
            article.AddRange(db.Article.ToList());
            var Account = db.COA_M.ToList();
            FillCombo(cmbxAccID, Account, "CATitle", "CAC_Code", 0);


            var CreditAccount = db.COA_M.ToList();
            FillCombo(cmbxCreditAccount, CreditAccount, "CATitle", "CAC_Code", 0);


            List<COA_D> vendor = new List<COA_D>();
            vendor.Add(new COA_D { CAC_Code = 0, AC_Title = "--SELECT--" });
            vendor.AddRange(db.COA_D.Where(x => x.CAC_Code == 1).ToList());

            


            List<COA_D> Creditvendor = new List<COA_D>();
            Creditvendor.Add(new COA_D { CAC_Code = 0, AC_Title = "--SELECT--" });
            Creditvendor.AddRange(db.COA_D.Where(x => x.CAC_Code == 1).ToList());

            


        }
        private void AccountComparesion_Load(object sender, EventArgs e)
        {
           // fillChart();
            LoadFuncaTion();
            this.reportViewer1.RefreshReport();
        }
        private void fillChart()
        {
          

          

            int sum = 0;
            for (int i = 0; i < CategorysDataGridView.Rows.Count; ++i)
            {
                sum += Convert.ToInt32(CategorysDataGridView.Rows[i].Cells[5].Value);
            }

            int sum1 = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum1 += Convert.ToInt32(dataGridView1.Rows[i].Cells[5].Value);
            }


            //   chart1.Series.Add(cmbxAccID.Text);
            //chart1.Series[cmbxAccID.Text].ChartType = SeriesChartType.Pie;
            //chart1.Series[cmbxAccID.Text].Points.AddXY(cmbxAccID.Text, sum);



            //  chart1.Series.Add(cmbxCreditAccount.Text);
            //chart1.Series[cmbxCreditAccount.Text].ChartType = SeriesChartType.Pie;
            //chart1.Series[cmbxCreditAccount.Text].Points.AddXY(cmbxCreditAccount.Text, sum1);
            ////chart1.Series[cmbxCreditAccount.Text].Color.GetHashCode()


            //chart1.Series[cmbxCreditAccount.Text].ChartType = SeriesChartType.Pie;
            //chart1.Series[cmbxAccID.Text].ChartType = SeriesChartType.Pie;

            //chart1.Series[cmbxCreditAccount.Text].Points.AddXY(cmbxCreditAccount.Text, sum1);
            //chart1.Series[cmbxAccID.Text].Points.AddXY(cmbxAccID.Text, sum);
            // chart1.Legends[cmbxCreditAccount.Text].Enabled = true;
            // chart1.ChartAreas[cmbxCreditAccount.Text].Area3DStyle.Enable3D = true;
            //Chart1.Series[0].ChartType = SeriesChartType.Pie;
            //Chart1.Series[0].Points.DataBindXY(x, y);
            //Chart1.Legends[0].Enabled = true;
            //Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;
            ////chart1.Legends[cmbxAccID.Text].Enabled = true;
            ////chart1.ChartAreas[cmbxAccID.Text].Area3DStyle.Enable3D = true;

            ////chart title  
            //Chart1.Titles.Add("Compare Accunts");
            string accountid = cmbxAccID.Text;
            string accountCredit = cmbxCreditAccount.Text;
            string[] x = { accountid, accountCredit };

            //Get the Total of Orders for each City.
            int[] y = {sum , sum1 };

            Chart1.Series[0].ChartType = SeriesChartType.Pie;
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Legends[0].Enabled = true;
            Chart1.ChartAreas[0].Area3DStyle.Enable3D = true;


            lblAccount1.Text = cmbxAccID.Text;
            lblAccount2.Text = cmbxCreditAccount.Text;
            lblAccount1Value.Text = sum.ToString();
            lblAccount2Value.Text = sum1.ToString();
            lblToTal.Text = "Total";
            lblValue.Text = (sum - sum1).ToString();

            lblValue.Visible = true;
            lblAccount1.Visible = true;
            lblAccount2.Visible = true;
            lblAccount1Value.Visible = true;
            lblAccount2Value.Visible = true;
            lblToTal.Visible = true;
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            first();
            Second();
            fillChart();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            List<CompareReport> Compare = new List<CompareReport>();


            foreach (DataGridViewRow row in CategorysDataGridView.Rows)
            {
                CompareReport check = new CompareReport();
                check.SNO = row.Cells["SNO"].Value.ToString();

                check.Date = row.Cells["Date"].Value.ToString();

               //  check.Referece = row.Cells["Reference"].Value.ToString();

                check.Debit = row.Cells["Credit"].Value.ToString();

                check.SNO = row.Cells["Balance"].Value.ToString();

                Compare.Add(check);
                //More code here
            }



            reportViewer1.Reset();
           
                                                                                    

          ReportDataSource ds = new ReportDataSource("DataSet1", Compare);


            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(ds);

            reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.Reporting.CompareAccount.rdlc";

            reportViewer1.LocalReport.Refresh();
            reportViewer1.RefreshReport();

        }
    }
}
