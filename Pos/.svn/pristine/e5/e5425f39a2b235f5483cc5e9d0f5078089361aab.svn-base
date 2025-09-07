using Lib.Entity;
using Lib.Model;
using Microsoft.Reporting.WinForms;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class DineInBilling : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        public DineInBilling()
        {
            InitializeComponent();
            db = new SaleManagerEntities();
        }

        private void DineInBilling_Load(object sender, EventArgs e)
        {
            generateChk();
            var list = new List<COA_M>();
            list.Add(new COA_M { CAC_Code = 1, CATitle = "%" });
            list.Add(new COA_M { CAC_Code = 2, CATitle = "Rs" });
            FillCombo<COA_M>(cmbxDis, list, "CATitle", "CAC_Code");
            FillCombo<COA_M>(cmbxGst, list, "CATitle", "CAC_Code");
        }

        private void generateChk()
        {
            var TableList = db.tbl_Order.Where(x => x.isComplete == false).ToList();
            int locY = 43; // 73
            int TableLen = TableList.Count;

            for (int i = 0; i < TableLen; i++)
            {
                int ID = TableList[i].TblID ?? 0;
                var tableName = db.tbl_Table.Where(x => x.ID == ID).FirstOrDefault().TableName;
                var checkBox1 = new CheckBox();
                checkBox1.AutoSize = true;
                checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                checkBox1.Location = new System.Drawing.Point(13, locY);
                checkBox1.Name = ID.ToString();
                checkBox1.Size = new System.Drawing.Size(86, 19);
                checkBox1.TabIndex = 13;
                checkBox1.Text = tableName;
                checkBox1.UseVisualStyleBackColor = true;
                checkBox1.CheckedChanged += new System.EventHandler(CheckBoxCheckedChanged);
                pnlChk.Controls.Add(checkBox1);
                locY += 21;
            }
        }

        public void FillCombo<T>(ComboBox comboBox, IEnumerable<T> obj, String Name, String ID, int selected = 1)
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

        private void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = sender as CheckBox;
            foreach (var control in pnlChk.Controls)
            {
                if (control is CheckBox)
                {
                    if (((CheckBox)control).Checked)
                    {
                        metroButton3.Enabled = true;
                        if (((CheckBox)control).Name == checkbox.Name)
                        {
                            ((CheckBox)control).Checked = true;
                        }
                        else
                        {
                            ((CheckBox)control).Checked = false;
                        }
                    }
                    else
                    {
                        metroButton3.Enabled = false;
                        gridInv.DataSource = null;
                        gridInv.Rows.Clear();
                    }
                }
            }

            var tblName = Convert.ToInt32(checkbox.Name);
            List<Orders> orders = (from order in db.tbl_Order
                                   join kot in db.tbl_KOT on order.OrderId.ToString() equals kot.OrderID
                                   join OrderDetails in db.tbl_OrderDetails on kot.Id.ToString() equals OrderDetails.KOTID
                                   join Items in db.Items on OrderDetails.itemID equals Items.IID
                                   where order.TblID == tblName && order.isComplete == false
                                   select new Orders
                                   {
                                       OrderId = order.OrderId,
                                       OrderDetailId = OrderDetails.Id,
                                       OrderNo = order.OrderNo,
                                       TblID = order.TblID,
                                       item = Items.IName,
                                       ItemID = Items.IID,
                                       Qty =  OrderDetails.Qty,
                                       Rate = OrderDetails.Rate,
                                       Amount = OrderDetails.Qty * OrderDetails.Rate,
                                       Discount = order.Discount,
                                       Total = order.Amount
                                   }).ToList();
            gridInv.DataSource = null;

            //orders = (from x in orders
            //          group x by x.item into y
            //          select new Orders
            //          {
            //              item = y.Key,
            //              OrderDetailId = y.First().OrderDetailId,
            //              OrderId = y.First().OrderId,
            //              OrderNo = y.First().OrderNo,
            //              TblID = y.First().TblID,
            //              ItemID = y.First().ItemID,
            //              Rate = y.First().Rate,
            //              Amount = y.Sum(z => z.Qty) * y.First().Rate,
            //              Discount = y.First().Discount,
            //              Total = y.Sum(z => z.Amount),
            //              Qty = y.Sum(z => z.Qty)
            //          }
            //   ).ToList();

            if (orders.Count > 0)
            {
                var listBinding = new BindingList<Orders>(orders);
                gridInv.DataSource = listBinding;
                txtGrandtotal.Text = orders[0].Total.ToString();
                lblBillNo.Text = orders[0].OrderNo.ToString();
            }
            else
            {
                MessageBox.Show("No Item Found");
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            textBox5.Text = (
                Convert.ToDecimal(txtCards.Text == "" ? "0" : txtCards.Text)
                - (Convert.ToDecimal(txtGrandtotal.Text == "" ? "0" : txtGrandtotal.Text)
                + (
                Convert.ToDecimal(txtGstTotl.Text == "" ? "0" : txtGstTotl.Text) -
                Convert.ToDecimal(textBox7.Text == "" ? "0" : textBox7.Text)
                ))
                ).ToString();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                metroButton3_Click(null, null);
            }
            if (keyData == (Keys.Control | Keys.P))
            {

            }
            if (keyData == (Keys.Control | Keys.Q))
            {

            }
            if (keyData == (Keys.Control | Keys.E))
            {

            }
            if (keyData == (Keys.Escape))
            {

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            DineInBilling dinebill = new DineInBilling();
            dinebill.Show();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (gridInv.Rows.Count > 0)
            {
                Saves();
            }
        }

        private void Saves()
        {
            bool orderUpdate = true;
            int orderID = 0;
            foreach (DataGridViewRow row in gridInv.Rows)
            {
                tbl_Order order = null;
                if (orderUpdate)
                {
                    orderID = Convert.ToInt32(row.Cells[0].Value.ToString() == "" ? "0" : row.Cells[0].Value);
                    order = db.tbl_Order.Where(x => x.OrderId == orderID).FirstOrDefault();
                    order.isComplete = true;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();

                    //var detls = db.tbl_OrderDetails.Where(x => x.OrderId == orderID).ToList();
                    //db.Entry(detls).State = EntityState.Deleted;
                    //db.SaveChanges();
                }
                orderUpdate = false;
                //tbl_OrderDetails details = new tbl_OrderDetails();
                //details.OrderId = orderID;
                //details.KOTID = row.Cells["KOTID"].Value.ToString() == "" ? "0" : row.Cells["KOTID"].Value.ToString();
                //details.itemID = Convert.ToInt32(row.Cells["itemID"].Value.ToString() == "" ? "0" : row.Cells["itemID"].Value); ;
                //details.Qty = Convert.ToInt32(row.Cells["Qty"].Value.ToString() == "" ? "0" : row.Cells["Qty"].Value); ;
                //details.Rate = Convert.ToInt32(row.Cells["Rate"].Value.ToString() == "" ? "0" : row.Cells["Rate"].Value); ;
                //details.CatID = Convert.ToInt32(row.Cells["CatID"].Value.ToString() == "" ? "0" : row.Cells["CatID"].Value); ;
            }
            SilentPrint(orderID);
            clear();
        }

        private void clear()
        {
            pnlChk.Controls.OfType<CheckBox>().ToList().ForEach(btn => btn.Dispose());
            gridInv.DataSource = null;
            gridInv.Rows.Clear();
            txtGrandtotal.Text = "";
        }

        private Control _focusedControl;

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            _focusedControl = (Control)sender;
        }
        private void metroButton19_Click(object sender, EventArgs e)
        {
            if (_focusedControl != null)
            {
                Button btn = sender as Button;
                if (btn.Text == "." && !_focusedControl.Text.Contains('.'))
                {
                    _focusedControl.Text += btn.Text;
                }
                if (btn.Text != ".")
                {
                    _focusedControl.Text += btn.Text;
                }
            }
        }

        private void metroButton18_Click(object sender, EventArgs e)
        {

        }

        private void metroButton17_Click(object sender, EventArgs e)
        {

        }

        private void cmbxDis_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            int val = Convert.ToInt32(cmbxDis.SelectedValue);
            var total = (Convert.ToDecimal(txtGrandtotal.Text == "" ? "0" : txtGrandtotal.Text));
            if (val == 1)
            {
                var perc = (Convert.ToDecimal(textBox1.Text == "" ? "0" : textBox1.Text));
                total = total * Convert.ToDecimal(("0." + perc.ToString()));
                textBox7.Text = String.Format("{0:0.00}", total);
            }
            else
            {
                textBox7.Text = textBox1.Text == "" ? "0" : textBox1.Text;
            }
        }

        private void txtGstAmt_Leave(object sender, EventArgs e)
        {
            int val = Convert.ToInt32(cmbxGst.SelectedValue);
            var total = (Convert.ToDecimal(txtGrandtotal.Text == "" ? "0" : txtGrandtotal.Text));
            if (val == 1)
            {
                var perc = (Convert.ToDecimal(txtGstAmt.Text == "" ? "0" : txtGstAmt.Text));
                total = total * Convert.ToDecimal(("0." + perc.ToString()));
                txtGstTotl.Text = String.Format("{0:0.00}", total);
            }
            else
            {
                txtGstTotl.Text = txtGstAmt.Text == "" ? "0" : txtGstAmt.Text;
            }
        }
        public void SilentPrint(int OrderId)
        {
            ReportViewer reportViewer1 = new ReportViewer();
            var order = db.tbl_Order.Where(x => x.OrderId == OrderId).FirstOrDefault();
            var list = db.tbl_OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
            List<Orders> orderList = new List<Orders>();
            foreach (var item in list)
            {
                Orders orders = new Orders();
                orders.OrderId = order.OrderId;
                orders.OrderDetailId = item.Id;
                orders.KOTID = order.KOTID;
                orders.OrderNo = order.OrderNo;
                orders.OrderDate = order.OrderDate;
                orders.isComplete = order.isComplete;
                orders.Discount = order.Discount;
                orders.Amount = order.Amount;
                orders.Total = item.Rate * item.Qty;
                orders.CashCard = Convert.ToDecimal(txtCards.Text == "" ? "0" : txtCards.Text);
                orders.TblID = order.TblID;
                orders.Gst = order.GST;
                var tbl = db.tbl_Table.Where(x => x.ID == order.TblID).FirstOrDefault();
                if (tbl != null)
                {
                    orders.Tbl = tbl.TableName;
                }
                orders.WaiterID = order.WaiterID;
                orders.item = db.Items.Where(x => x.IID == item.itemID).FirstOrDefault().IName;
                orders.booker = "Owais";
                orders.CatID = item.CatID ?? 0;
                orders.ItemID = item.itemID ?? 0;
                orders.Qty = item.Qty;
                orders.Rate = item.Rate;
                orderList.Add(orders);
            }
            Silent silent = new Silent();
            //silent.Run<Orders>(reportViewer1, orderList, "");
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.gridInv.SelectedRows.Count > 0)
                {
                    var orderDetailsID = Convert.ToInt32(this.gridInv.SelectedRows[0].Cells["OrderDetailId"].Value);
                    gridInv.Rows.RemoveAt(this.gridInv.SelectedRows[0].Index);
                    var Order = new tbl_OrderDetails { Id = orderDetailsID };
                    db.Entry(Order).State = EntityState.Deleted;
                    db.SaveChanges();
                    gridInv_SelectionChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        private void gridInv_SelectionChanged(object sender, EventArgs e)
        {
            Decimal totalAmount = 0;
            foreach (DataGridViewRow row in gridInv.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    //            cmbxItems.SelectedValue = Convert.ToInt32(row.Cells[0].Value.ToString() == "" ? "0" : row.Cells[0].Value);
                    //            //ddlBatch.SelectedText = Convert.ToInt32(row.Cells[2].Value.ToString());
                    //            dtExpirt.Text = row.Cells[3].Value.ToString();
                    //            txtctn.Text = row.Cells[4].Value.ToString();
                    //            txtpcs.Text = row.Cells[5].Value.ToString();
                    //            txtRate.Text = row.Cells[6].Value.ToString();
                    //            txtDisPer.Text = (row.Cells[8].Value ?? "0").ToString();
                    //            txtDisc.Text = (row.Cells[9].Value ?? "0").ToString();
                    //            txtSaleP.Text = (row.Cells[11].Value ?? "0").ToString();
                    totalAmount += Convert.ToDecimal(row.Cells["Total"].Value);
                    //txtNet.Text = totalAmount.ToString();
                }
            }
            //    txtDisfooter.Text = txtDisfooter.Text == "" ? "0" : txtDisfooter.Text;
            txtGrandtotal.Text = totalAmount.ToString();
            //    txtNetAm.Text = (Convert.ToDecimal(txtTotalAm.Text) - Convert.ToDecimal(txtDisfooter.Text)).ToString();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {

        }

        private void metroButton11_Click(object sender, EventArgs e)
        {

        }

        private void Tile1_Click(object sender, EventArgs e)
        {
            DineInBilling billing = new DineInBilling();
            billing.Hide();
            //pnlTab2.Visible = false;
        }
    }
}