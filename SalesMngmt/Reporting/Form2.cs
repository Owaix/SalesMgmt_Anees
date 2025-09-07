using Lib.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DIagnoseMgmt
{
    public partial class Form2 : Form
    {
        List<SaleInvoice> List = null;
        List<StockReport> Stock = null;
        List<Ledger> legder = null;
        List<Lib.Reporting.ReceiveVoucher> Receive = null;
        string size = "";

        List<BalanceList> BalanceList = null;
        public Form2(List<SaleInvoice> list)
        {
            InitializeComponent();
            List = list;
        }
        public Form2(List<SaleInvoice> list, string name)
        {
            InitializeComponent();
            List = list;
            size = name;
        }
        public Form2(List<BalanceList> BalanceLists)
        {
            InitializeComponent();
            BalanceList = BalanceLists;
        }

        public Form2(List<StockReport> stock)
        {
            InitializeComponent();
            Stock = stock;
        }

        public Form2(List<Ledger> Legder)
        {
            InitializeComponent();
            legder = Legder;
        }

        public Form2(List<Lib.Reporting.ReceiveVoucher> Legder)
        {
            InitializeComponent();
            Receive = Legder;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //ReportDataSource rds = new ReportDataSource("Ds", List);
            //this.reportViewer1.LocalReport.DataSources.Clear();
            //this.reportViewer1.LocalReport.DataSources.Add(rds);
            //this.reportViewer1.RefreshReport();

             if (List != null)
            {


                ReportDataSource ds = new ReportDataSource("Ds", List);


                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(ds);
                if (List[0].CompanyID == 1017)
                {
                    reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.ThermalReport.rptSaleInvPrint.rdlc";
                }

                else if (size == "A5" && List[0].CompanyID == 1024)
                {


                    reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.ThermalReport.HalfSizeA4Set.rdlc";

                }
                else if (size == "A5")
                {


                    reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.ThermalReport.HalfSizeA4.rdlc";

                }
                else if (size == "Purchase_A5")
                {


                    reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.ThermalReport.PURCHASEHalfSizeA4.rdlc";

                }

                else
                {

                    reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.ThermalReport.rptSaleInvPrintA4.rdlc";
                }
                // reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OrderDetailProcession);
                reportViewer1.LocalReport.Refresh();
                reportViewer1.RefreshReport();

            }
            else if (Stock != null)
            {

                ReportDataSource ds = new ReportDataSource("Ds", Stock);


                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(ds);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.ThermalReport.StockReport.rdlc";
                // reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OrderDetailProcession);
                reportViewer1.LocalReport.Refresh();
                reportViewer1.RefreshReport();

            }

            else if (legder != null)
            {


                ReportDataSource ds = new ReportDataSource("Ds", legder);


                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(ds);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.ThermalReport.LedgerReport.rdlc";
                // reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OrderDetailProcession);
                reportViewer1.LocalReport.Refresh();
                reportViewer1.RefreshReport();


            }

            else if (BalanceList != null)
            {


                ReportDataSource ds = new ReportDataSource("Ds", BalanceList);


                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(ds);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.ThermalReport.CurrentBalanceList.rdlc";
                // reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OrderDetailProcession);
                reportViewer1.LocalReport.Refresh();
                reportViewer1.RefreshReport();


            }




            else if (Receive != null)
            {


                ReportDataSource ds = new ReportDataSource("Ds", Receive);


                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(ds);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SalesMngmt.ThermalReport.ReceiveVoucherA4.rdlc";
                // reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(OrderDetailProcession);
                reportViewer1.LocalReport.Refresh();
                reportViewer1.RefreshReport();


            }


        }


        public DataTable RunStoredProc(String SpName, object[] parameters, CommandType type = CommandType.Text)
        {
            DataTable dataTable = new DataTable();
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDBEntities"].ToString());
                SqlCommand cmd = new SqlCommand(SpName, con);
                if (parameters != null && parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                cmd.CommandType = type;
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
            return dataTable;
        }

    }
}


