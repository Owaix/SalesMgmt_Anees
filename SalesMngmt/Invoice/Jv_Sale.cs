using Lib.Entity;
using Lib.Reporting;
using SalesMngmt.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class Jv_Sale : Form
    {
        SaleManagerEntities db = null;
        int compID = 0;
        public Jv_Sale(int company)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = company;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            COADataGridView.Rows.Clear();


            var query = ReportsController.getjpurnalVoucherSale(dtTo.Value, dtFrom.Value, compID);

            foreach (DataRow list in query.Rows)
            {


                int account1 = (int)list["AC_Code"];
                int account2 = (int)list["AC_Code2"];

                var DebitName = db.COA_D.AsNoTracking().Where(x => x.AC_Code == account1).FirstOrDefault();
                var CreditName = db.COA_D.AsNoTracking().Where(x => x.AC_Code == account2).FirstOrDefault();


                COADataGridView.Rows.Add(list["RID"], list["EDate"], list["Narr"], DebitName.AC_Title, CreditName.AC_Title, list["Amt"], CreditName.AC_Code, DebitName.AC_Code, CreditName.CAC_Code, DebitName.CAC_Code);

            }
        }

        private void COADataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (COADataGridView.Rows.Count >= 1)
            {



                if (e.ColumnIndex == 11)
                {
                    int value = Convert.ToInt32(COADataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                    jv_sale_pic jp = new jv_sale_pic(value);

                    jp.Show();


                }




            }
        }
    }
}