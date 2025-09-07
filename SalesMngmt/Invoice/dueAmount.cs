using Lib.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class dueAmount : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.sp_getArticle_Result> list = null;
        int compID = 0;
        int obj = 0;

        public dueAmount(int company)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = company;
        }

        private void dueAmount_Load(object sender, EventArgs e)
        {
            var list = (from a in db.tbl_Order
                        join b in db.tbl_Employee on a.WaiterID equals b.ID
                        where a.CompanyID == compID && a.dueAmt > 0
                        select new
                        {
                            OrderId = a.OrderId,
                            OrderNo = a.OrderNo,
                            dueAmt = a.dueAmt,
                            EmployeName = b.EmployeName,
                            OrderType = a.OrderType
                        }).ToList();

            dataGridView1.DataSource = list;
        }

        private void dataGridView1_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var orderID = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["OrderId"].Value.ToString());
                var order = db.tbl_Order.Where(x => x.OrderId == orderID).FirstOrDefault();
                if (order != null)
                {
                    order.Amount = order.dueAmt;
                    order.dueAmt = 0;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                    dueAmount_Load(null, null);
                }
            }
        }
    }
}