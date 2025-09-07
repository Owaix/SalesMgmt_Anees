using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
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

namespace SalesMngmt.Reporting
{
    public partial class KarahiStockList : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        int compID = 0;

        public KarahiStockList(int companyID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = companyID;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CategorysDataGridView.Rows.Clear();

            var Items = ReportsController.GetKarahiSummary(dtTo.Value, dtFrom.Value, compID);
            int a = 0;
            foreach (DataRow rows in Items.Rows)
            {
                CategorysDataGridView.Rows.Add(++a, rows["ItemName"], rows["Total"]);

            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (CategorysDataGridView.Rows.Count == 0) {
                MessageBox.Show("Table is empty ");
                return;


            }

            int a = 0;
            List<KarahiReceipt> orderList = new List<KarahiReceipt>();
            foreach (DataGridViewRow row in CategorysDataGridView.Rows)
            {
                KarahiReceipt orders = new KarahiReceipt();
               
                orders.SNO = ++a;
                orders.ItemNAme = row.Cells[1].Value.ToString();
            
                orders.item = row.Cells[1].Value.ToString();
                orders.Total= Convert.ToDouble( row.Cells[2].Value.ToString());
                orderList.Add(orders);

            }


            Silent silent = new Silent();
            ReportViewer reportViewer1 = new ReportViewer();


            silent.Run(reportViewer1, orderList, "SalesMngmt.ThermalReport.Karahi_Stock_List.rdlc");

            CategorysDataGridView.Rows.Clear();
          
        }
    }
}
