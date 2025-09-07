using Lib;
using Lib.Entity;
using Lib.Model;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class PReturn : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.Items> listItems = null;
        int compID = 0;
        List<tblStock> stock = null;
        public PReturn(int Company)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = Company;
            stock = new List<tblStock>();
        }

        private void PInv_Load(object sender, EventArgs e)
        {
            lblRID.Text = "0";
            metroTextBox1.Text = "PI-2010003";
        }

        private void metroPanel4_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

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
        private void metroButton1_Click(object sender, EventArgs e)
        {
            var isAdd = true;
            if (cmbxItems.SelectedValue == null)
            {
                MessageBox.Show("Item is Required", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtCode.Text == "")
            {
                MessageBox.Show("Quantity is Required", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var itemid = Convert.ToInt32(cmbxItems.SelectedValue);
            var item = db.Items.Where(x => x.IID == itemid).FirstOrDefault();
            if (item != null)
            {
                var stock = GetStockQuantity();
                if (Convert.ToInt32(txtCode.Text) > stock)
                {
                    MessageBox.Show("Quantity is Exceed From Current Stock!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        var itemID = Convert.ToInt32(row.Cells[0].Value.DefaultZero());
                        if (Convert.ToInt32(cmbxItems.SelectedValue) == itemID)
                        {
                            row.Cells[0].Value = cmbxItems.SelectedValue;
                            row.Cells[1].Value = item.IName;
                            row.Cells[2].Value = txtCode.Text;
                            row.Cells[3].Value = item.SalesPrice;
                            row.Cells[4].Value = item.DisR;
                            row.Cells[5].Value = Convert.ToString(Convert.ToDecimal(item.SalesPrice) + item.DisR);
                            isAdd = false;
                        }
                    }
                }
                if (isAdd == true)
                {
                    this.invGrid.Rows.Add(cmbxItems.SelectedValue, item.IName, txtCode.Text, item.SalesPrice, item.DisR, (Convert.ToDecimal(item.SalesPrice) + item.DisR), "Remove");
                }

                invGrid_SelectionChanged(null, null);
                clear(true);
            }
        }

        private void invGrid_SelectionChanged(object sender, EventArgs e)
        {
            Decimal totalAmount = 0;
            Decimal Discount = 0;
            foreach (DataGridViewRow row in invGrid.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    cmbxItems.SelectedValue = Convert.ToInt32(row.Cells[0].Value.DefaultZero());
                    txtCode.Text = row.Cells[2].Value.ToString();
                    Discount += Convert.ToDecimal(row.Cells[4].Value.DefaultZero());
                    totalAmount += Convert.ToDecimal(row.Cells[5].Value.DefaultZero());

                }
            }
            txtDisfooter.Text = Discount.ToString();
            txtTotalAm.Text = totalAmount.ToString();
            txtNetAm.Text = (Convert.ToDecimal(txtTotalAm.Text) - Convert.ToDecimal(txtDisfooter.Text)).ToString();
        }

        private void txtPartyType_Leave(object sender, EventArgs e)
        {

        }
        public void SetInvID(String InvID)
        {
            //txtInv.Enabled = false;
        }


        private void invGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 6)
                    {
                        invGrid.Rows.RemoveAt(e.RowIndex);
                        invGrid_SelectionChanged(null, null);
                        clear();
                    }
                    //else
                    //{
                    //    var ItemID = Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells[0].Value);
                    //    var items = db.Items.Where(x => x.IID == ItemID).FirstOrDefault();
                    //    items.SalesPrice = Convert.ToDouble(invGrid.Rows[e.RowIndex].Cells[11].Value.ToString() == "" ? "0" : invGrid.Rows[e.RowIndex].Cells[11].Value);
                    //    db.Entry(items).State = EntityState.Modified;
                    //    db.SaveChanges();
                    //    MessageBox.Show("Item Sale Price Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
                else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
                    ch1 = (DataGridViewCheckBoxCell)invGrid.Rows[invGrid.CurrentRow.Index].Cells[e.ColumnIndex];

                    if (ch1.Value == null)
                        ch1.Value = false;
                    switch (ch1.Value.ToString())
                    {
                        case "True":
                            ch1.Value = false;
                            break;
                        case "False":
                            ch1.Value = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDis_TextChanged(object sender, EventArgs e)
        {
            txtNetAm.Text = (Convert.ToDecimal(txtTotalAm.Text.DefaultZero()) - Convert.ToDecimal(txtDisfooter.Text.DefaultZero())).ToString();
        }
        private void txtCredit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void invGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbxItems.SelectedValue = Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells[0].Value.DefaultZero());
            txtCode.Text = invGrid.Rows[e.RowIndex].Cells[2].Value.DefaultZero();
            //ddlBatch.SelectedText = Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear(bool grid = false)
        {
            txtCode.Text = "";

            cmbxItems.SelectedIndex = -1;

            txtCode.Focus();
            if (grid == false)
            {
                invGrid.DataSource = null;
                invGrid.Rows.Clear();
                txtNetAm.Text = "0";
                txtTotalAm.Text = "0";
                txtDisfooter.Text = "0";

            }

            lblRID.Text = "0";
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            SaveRecord(lblRID.Text);
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            SaveRecord(lblRID.Text);
        }

        private void PInv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Modifiers == Keys.Alt)
            {
                clear();
            }
        }

        private void SaveRecord(String InvoiceNo)
        {
            if (invGrid.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Items In Grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PurR_M purchase = null;
            DbContextTransaction transaction = db.Database.BeginTransaction();
            try
            {
                if (InvoiceNo == "0")
                {
                    purchase = new PurR_M();
                    DataAccess access = new DataAccess();
                    Purc_M coa = new Purc_M();
                    coa.InvType = "PR";
                    SqlConnection con = new SqlConnection(ConnectionStrings.GetCS);
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    purchase.invRNo = access.GetScalar("Gen_NewInvNo", coa, con, trans);
                    purchase.InvDT = DateTime.Now.ToString();
                }
                else
                {
                    var RID = Convert.ToInt32(InvoiceNo);
                    purchase = db.PurR_M.Where(x => x.RID == RID).FirstOrDefault();
                    purchase.Edit_Date = DateTime.Now;
                }
                purchase.invNo = metroTextBox1.Text;
                purchase.EDate = DateTime.Now;
                purchase.WID = 1;// WarehouseID
                purchase.Rem = txtDisfooter.Text;
                purchase.NetAmt = Convert.ToDouble(txtTotalAm.Text);

                if (InvoiceNo == "0")
                {
                    db.PurR_M.Add(purchase);
                }
                db.SaveChanges();

                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        int id = Convert.ToInt32(row.Cells[0].Value);
                        var item = db.Items.Where(x => x.IID == id).FirstOrDefault();
                        PurR_D detail = new PurR_D();
                        detail.RID = purchase.RID;
                        detail.IID = Convert.ToDouble(row.Cells[0].Value.ToString().DefaultZero());
                        detail.Qty = (item.CTN_PCK ?? 0 * Convert.ToInt32(row.Cells[2].Value)) + Convert.ToInt32(row.Cells[2].Value);
                        detail.PurPrice = Convert.ToDouble(row.Cells[3].Value ?? 0);

                        var stocks = db.tblStock.Where(x => x.ItemID == detail.IID).FirstOrDefault();
                        if (stocks != null)
                        {
                            stocks.Quantity = stocks.Quantity - Convert.ToInt32(row.Cells[2].Value);
                        }

                        db.PurR_D.Add(detail);
                        db.SaveChanges();
                    }
                }

                transaction.Commit();
                MessageBox.Show("Invoice Return Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private BarCodeListener ScannerListener;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                SaveRecord(lblRID.Text);
            }
            if (keyData == (Keys.Control | Keys.P))
            {
                SaveRecord(lblRID.Text);
            }
            if (keyData == (Keys.Control | Keys.Q))
            {
                txtCode.Focus();
            }

            if (keyData == (Keys.Escape))
            {
                clear();
            }
            if (keyData == (Keys.Enter) && txtCode.Text == "")
            {
                //int id = Convert.ToInt32(cmbxItems.SelectedValue);
                //var items = db.Items.Where(x => x.IID == id).FirstOrDefault();
                //txtpcs.Text = txtpcs.Text.DefaultZero() == "0" ? "1" : txtpcs.Text;
                //if (items == null) { }
                //else
                //{
                //    calculation();
                //    metroButton1_Click(null, null);
                //    cmbxArticle.Focus();
                //    return true;
                //}
            }

            bool res = false;
            if (ScannerListener != null)
            {
                res = ScannerListener.ProcessCmdKey(ref msg, keyData);
            }
            res = keyData == Keys.Enter ? res : base.ProcessCmdKey(ref msg, keyData);
            return res;
        }


        private void txtRate_Leave(object sender, EventArgs e)
        {
            var itemID = Convert.ToInt32(cmbxItems.SelectedValue);
            var item = db.Items.Where(x => x.IID == itemID).FirstOrDefault();
            if (item != null)
            {
                calculation();
                //var TotalPcs = (item.CTN_PCK ?? 0 * Convert.ToInt32(txtctn.Text)) + Convert.ToInt32(txtpcs.Text.DefaultZero());
                //var PcsRate = Convert.ToDouble(txtRate.Text);
                //var NetAmount = PcsRate - Convert.ToDouble(txtDisc.Text.DefaultZero());
                //txtDisPer.Text = txtDisPer.Text ?? "0";
                //var DiscPerc = Convert.ToDouble(txtDisPer.Text.DefaultZero() == "0" ? "1" : "0." + txtDisPer.Text);
                //NetAmount = NetAmount * (DiscPerc == 1 ? 1 : 1 - DiscPerc);
                //txtNet.Text = String.Format("{0:0.00}", NetAmount);
                //TotalPcs = TotalPcs == 0 ? 1 : TotalPcs;
                //txtPcsRate.Text = String.Format("{0:0.00}", (Convert.ToDouble(txtNet.Text) / TotalPcs));
                //txtSaleP.Focus();
            }
        }

        private void txtSaleP_Leave(object sender, EventArgs e)
        {
            var itemID = Convert.ToInt32(cmbxItems.SelectedValue);
            var item = db.Items.Where(x => x.IID == itemID).FirstOrDefault();
            if (item != null)
            {
                calculation();
                //var TotalPcs = (item.CTN_PCK ?? 0 * Convert.ToInt32(txtctn.Text.DefaultZero())) + Convert.ToInt32(txtpcs.Text.DefaultZero());
                //var PcsRate = Convert.ToDouble(txtSaleP.Text.DefaultZero());
                //TotalPcs = TotalPcs == 0 ? 1 : TotalPcs;
                //txtSaleRate.Text = String.Format("{0:0.00}", (PcsRate / TotalPcs));
                // metroButton1.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    row.Cells[15].Value = checkBox1.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void calculation()
        {
            var itemID = Convert.ToInt32(cmbxItems.SelectedValue);
            var item = db.Items.Where(x => x.IID == itemID).FirstOrDefault();
            if (item != null)
            {
                //var Pcs = (item.CTN_PCK ?? 0 * Convert.ToInt32(txtctn.Text.DefaultZero())) + Convert.ToInt32(txtpcs.Text.DefaultZero());
                //var Rate = Convert.ToDouble(txtRate.Text.DefaultZero());
                //Pcs = Pcs == 0 ? 1 : Pcs;
                //txtNet.Text = String.Format("{0:0.00}", Convert.ToDouble(txtRate.Text.DefaultZero()));
                //var DiscPercValue = Convert.ToDouble(Convert.ToDouble(txtDisPer.Text.DefaultZero()) / 100 * Convert.ToDouble(txtNet.Text.DefaultZero()));
                //var DiscValue = Convert.ToDouble(txtDisc.Text.DefaultZero());
                //txtNet.Text = String.Format("{0:0.00}", Convert.ToDouble(txtNet.Text.DefaultZero()) - (DiscPercValue + DiscValue));
                //txtPcsRate.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) / Convert.ToDouble(Pcs)).ToString();
                //txtSaleRate.Text = (Convert.ToDouble(txtSaleP.Text.DefaultZero()) / Convert.ToDouble(Pcs)).ToString();
            }
        }

        private void txtPcsRate_Leave(object sender, EventArgs e)
        {
            calculation();
        }

        private void txtSaleRate_Leave(object sender, EventArgs e)
        {
            calculation();
        }

        private void metroTextBox1_Leave_1(object sender, EventArgs e)
        {
            Pur_M purchase = db.Pur_M.Where(x => x.InvNo == metroTextBox1.Text).FirstOrDefault();
            if (purchase != null)
            {
                List<ItemCombo> batch = new List<ItemCombo>();
                batch.Add(new ItemCombo { ID = 0, Name = "--SELECT--" });
                batch.AddRange((from t1 in db.Items
                                join t2 in db.Pur_D on t1.IID equals t2.IID
                                join t3 in db.tblStock on t2.IID equals t3.ItemID
                                where t2.RID == purchase.RID && t3.Quantity > 0
                                select new ItemCombo
                                {
                                    ID = t1.IID,
                                    Name = t1.IName + " (Qty : " + t3.Quantity + ")"
                                }).ToList());

                if (batch.Count > 0)
                {
                    FillCombo(cmbxItems, batch, "Name", "ID", -1);
                }
                if (batch.Count > 1)
                {
                    cmbxItems.SelectedValue = 1;
                }
            }
        }
        private int GetStockQuantity()
        {
            int currentStock = 0;
            var ItemID = Convert.ToInt32(cmbxItems.SelectedValue);
            stock = db.tblStock.Where(x => x.ItemID == ItemID).ToList();
            foreach (var item in stock)
            {
                currentStock += item.Quantity ?? 0;
            }
            return currentStock;
        }
        private void txtcode_Leave(object sender, EventArgs e)
        {
            txtCode.Text = GetStockQuantity().ToString();
            label8.Text = GetStockQuantity().ToString();
        }
    }
    public class ItemCombo
    {
        public int ID { get; set; }
        public String Name { get; set; }
    }
}
