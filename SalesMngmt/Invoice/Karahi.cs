using Lib.Entity;
using Lib.Model;
using Microsoft.Reporting.WinForms;
using SalesMngmt.Configs;
using SalesMngmt.Reporting;
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

namespace SalesMngmt.Invoice
{
    public partial class Karahi : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        int compID = 0;
        public Karahi(int Company)
        {
            db = new SaleManagerEntities();
            compID = Company;
            InitializeComponent();
        }

        private void Karahi_Load(object sender, EventArgs e)
        {

            List<tbl_karahi_Item> article = new List<tbl_karahi_Item>();
            article.Add(new tbl_karahi_Item { Id = 0, ItemName = "--SELECT--" });
            article.AddRange(db.tbl_karahi_Item.AsNoTracking().Where(x => x.Company == compID && x.Active == false).ToList());
            FillCombo(cmbxItem, article, "ItemName", "Id", 0);
            cmbxQty.SelectedIndex = 0;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbxItem.Text == "--SELECT--")
            {

                MessageBox.Show("Please select item");
                cmbxItem.Focus();
                return;
            }
            if (txtQty.Text == "" || txtQty.Text=="0")
            {

                MessageBox.Show("Please select Qty");
                txtQty.Focus();
                return;
            }
            if (txtkarahiNO.Text == "")
            {

                MessageBox.Show("Please select Karahi No");
                txtkarahiNO.Focus();
                return;
            }

            this.dataGridView1.Rows.Add(cmbxItem.SelectedValue,
                cmbxItem.Text, txtQty.Text, txtkarahiNO.Text, "Remove");

            cmbxItem.SelectedIndex = 0;
            txtQty.Text = "";
            txtkarahiNO.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == 4)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);

            }
            else
            {

            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {

                MessageBox.Show("Table is empty");
                cmbxItem.Focus();
                return;


            }

            tbl_karahi_M M = new tbl_karahi_M();
            M.Company = compID;
            M.Customer = txtCustomer.Text;
            M.Date = dtpDate.Value;
            db.tbl_karahi_M.Add(M);
            db.SaveChanges();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                tbl_Karahi_D D = new tbl_Karahi_D();
                D.itemId = Convert.ToInt32(row.Cells[0].Value.ToString());
                D.Qty = Convert.ToDouble(row.Cells[2].Value.ToString());
                D.Rid = M.ID;
                D.Date = dtpDate.Value;
                D.karahiNo = row.Cells[3].Value.ToString();
                D.Compy = compID;
               
                db.tbl_Karahi_D.Add(D);
                db.SaveChanges();
            }

           
            int a = 0;
            List<KarahiReceipt> orderList = new List<KarahiReceipt>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                KarahiReceipt orders = new KarahiReceipt();
                orders.Customer = txtCustomer.Text;
                orders.Date = dtpDate.Value.ToString();
                orders.SNO = ++a;
                orders.Qty = Convert.ToDouble(row.Cells[2].Value.ToString());
                orders.ItemNAme = row.Cells[1].Value.ToString();
                orders.Karahi_No = row.Cells[3].Value.ToString();
                orders.item = row.Cells[1].Value.ToString();
   
                orderList.Add(orders);

            }


            Silent silent = new Silent();
            ReportViewer reportViewer1 = new ReportViewer();


            silent.Run(reportViewer1, orderList, "SalesMngmt.ThermalReport.Karahi_receipt.rdlc");

            dataGridView1.Rows.Clear();
            txtCustomer.Text = "";

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            karahi_item t = new karahi_item(compID);
            t.Show();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            KarahiList L = new KarahiList(compID);
            L.Show();
        }

        private void btnSaleReport_Click(object sender, EventArgs e)
        {
            KarahiStockList s = new KarahiStockList(compID);
            s.Show();
        }
    }
}
