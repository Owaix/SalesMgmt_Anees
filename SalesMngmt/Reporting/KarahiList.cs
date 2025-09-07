using Lib.Entity;
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
    public partial class KarahiList : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        int compID = 0;
        public KarahiList(int companyID)
        {
            InitializeComponent();
            compID = companyID;
            db = new SaleManagerEntities();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CategorysDataGridView.Rows.Clear();
            var Items = ReportsController.GetKarahiList(dtTo.Value, dtFrom.Value, compID);
            int a = 0;
            foreach (DataRow rows in Items.Rows)
            {
                CategorysDataGridView.Rows.Add(rows["ID"], ++a, rows["Customer"], rows["Date"], "Remove");
              


            }

        }

        private void CategorysDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == 4)
            {
                DialogResult dialogResult = MessageBox.Show("Are you Sure", "Delete", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                  int  itemID = Convert.ToInt32(CategorysDataGridView.Rows[e.RowIndex].Cells[0].Value);
                    db.tbl_karahi_M.RemoveRange(db.tbl_karahi_M.Where(x => x.ID == itemID));
                    db.tbl_Karahi_D.RemoveRange(db.tbl_Karahi_D.Where(x => x.Rid == itemID));

                    db.SaveChanges();

                    CategorysDataGridView.Rows.Clear();

                    var Items = ReportsController.GetKarahiList(dtTo.Value, dtFrom.Value, compID);
                    int a = 0;
                    foreach (DataRow rows in Items.Rows)
                    {
                        CategorysDataGridView.Rows.Add(rows["ID"], ++a, rows["Customer"], rows["Date"], "Remove");



                    }


                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }

              
            }
            else
            {

            }

        }
    }
}
