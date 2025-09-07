using Lib;
using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using Lib.Utilities;
using SalesMngmt.Configs;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class PInv : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.Items> listItems = null;
        int compID = 0;
        bool account = false;
        int vebdorid = 0;
        List<tblStock> stock = null;

        int itemIDValue = 0;
        public PInv PInvoice { get; set; }
        public PInv(int Company)
        {
            InitializeComponent();

            lblItemID.Text = "";
            db = new SaleManagerEntities();
            compID = Company;
            PInvoice = this;
            stock = new List<tblStock>();
            listItems = db.Items.AsNoTracking().Where(x => x.CompanyID == compID && x.isDeleted == false).ToList();
            if (Company == 1015)
            {
                txtBatch.Visible = false;
                dtExpirt.Visible = false;
                label5.Visible = false;
                sad.Visible = false;
                cmbxItems.Width = 350;
                this.Column3.Visible = false;
                this.Column2.Visible = false;

            }

        }

        private void PInv_Load(object sender, EventArgs e)
        {
            List<I_Unit> unitList;
            if (compID == 1024)
            {
                unitList = new List<I_Unit>
        {
            new I_Unit { IUnit = "PCS", unit_id = 1 },
            new I_Unit { IUnit = "SET", unit_id = 2 }
        };

            }


            else
            {

                unitList = new List<I_Unit>
        {
            new I_Unit { IUnit = "PCS", unit_id = 1 },
            new I_Unit { IUnit = "CTN", unit_id = 2 }
        };

            }






            FillCombo(cmbxPackaging, unitList, "IUnit", "unit_id", 1);


            cmbxPackaging.SelectedIndex = 0;

            dtExpirt.Value = new DateTime(1900, 01, 01);
            var Account = db.COA_M.Where(x => x.CAC_Code == 1 || x.CAC_Code == 3 || x.CAC_Code == 9).ToList();


            var vendor = db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList();

            //FillCombo(cmbxvendor, vendor, "AC_Title", "CAC_Code", 0);
            //FillCombo(cmbxAccID, Account, "CATitle", "CAC_Code", 0);

            List<COA_M> article = new List<COA_M>();
            article.Add(new COA_M { CAC_Code = 0, CATitle = "--SELECT--" });
            article.AddRange(db.COA_M.Where(x => x.CAC_Code == 1 || x.CAC_Code == 3 || x.CAC_Code == 9).ToList());
            FillCombo(cmbxAccID, article, "CATitle", "CAC_Code", 0);


            List<COA_D> cash = new List<COA_D>();
            cash.Add(new COA_D { AC_Code = 0, AC_Title = "--Credit--" });
            cash.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList());
            FillCombo(cmbxPaymentMode, cash, "AC_Title", "AC_Code", 0);

            List<tbl_Warehouse> Warehouse = new List<tbl_Warehouse>();
            Warehouse.Add(new tbl_Warehouse { WID = 0, Warehouse = "--Select--" });
            Warehouse.AddRange(db.tbl_Warehouse.Where(x => x.CompanyID == compID && x.isDelete == false).ToList());
            FillCombo(cmbxWareHouse, Warehouse, "Warehouse", "WID", 0);



            if (compID == 1022)
            {


                cmbxWareHouse.SelectedIndex = 1;
                cmbxAccID.SelectedIndex = 3;
            }

        }
        private void Accountvalidation()
        {
            int account = (int)cmbxAccID.SelectedValue;


            if (account == 0)
            {
                MessageBox.Show("Please Select Account");
                cmbxAccID.Focus();
                return;

            }
            else if (account == 3)
            {
                var name = db.Customers.AsNoTracking().Where(x => x.CusName == cmbxvendor.Text.ToString() && x.CompanyID == compID && x.InActive == false).FirstOrDefault();
                if (name == null)
                {

                    MessageBox.Show("Please Select Account");
                    cmbxvendor.Focus();
                    return;
                }
                else
                {


                    vebdorid = name.AC_Code;

                }


            }
            else if (account == 9)
            {

                var name = db.Vendors.AsNoTracking().Where(x => x.VendName == cmbxvendor.Text.ToString() && x.CompanyID == compID && x.InActive == false).FirstOrDefault();
                if (name == null)
                {

                    MessageBox.Show("Please Select Account");
                    cmbxvendor.Focus();
                    return;
                }
                else
                {

                    vebdorid = Convert.ToInt32(name.AC_Code);


                }



            }
            else if (account == 1)
            {

                var name = db.COA_D.AsNoTracking().Where(x => x.AC_Title == cmbxvendor.Text.ToString() && x.CompanyID == compID && x.InActive == false).FirstOrDefault();
                if (name == null)
                {

                    MessageBox.Show("Please Select Account");
                    cmbxvendor.Focus();
                    return;
                }
                else
                {

                    vebdorid = Convert.ToInt32(name.AC_Code);
                }

            }












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

        private void cmbxItems_Leave(object sender, EventArgs e)
        {

            if (cmbxItems.Text == "")
            {


                return;

            }


            if (lblItemID.Text != string.Empty && lblItemID.Text != "0")
            {
                //var item = listItems.Find(x => x.IName.ToLower().Trim() == cmbxItems.Text.ToLower().Trim());
                //Items_Leave(item);

                txtCode.Text = "";


                txtRate.Text = "0";


            }

            else
            {

                cmbxItems.Focus();

            }


        }

        private void moveDown(string Autofill)
        {
            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowCount = dataGridView1.Rows.Count;
                    int index = dataGridView1.SelectedCells[0].OwningRow.Index;

                    if (index == (rowCount - 1)) // include the header row
                    {
                        return;
                    }
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index + 1].Selected = true;
                    EnsureVisibleRow(dataGridView1, index);
                }
            }
        }

        private static void EnsureVisibleRow(DataGridView view, int rowToShow)
        {
            if (rowToShow >= 0 && rowToShow < view.RowCount)
            {
                var countVisible = view.DisplayedRowCount(false);
                var firstVisible = view.FirstDisplayedScrollingRowIndex;
                if (rowToShow < firstVisible)
                {
                    view.FirstDisplayedScrollingRowIndex = rowToShow;
                }
                else if (rowToShow >= firstVisible + countVisible)
                {
                    view.FirstDisplayedScrollingRowIndex = rowToShow - countVisible + 1;
                }
            }
        }
        private void moveUp(string Autofill)
        {
            if (dataGridView1.RowCount > 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int rowCount = dataGridView1.Rows.Count;
                    int index = dataGridView1.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[index - 1].Selected = true;
                }
            }
        }


        private int ItemID(int ItemID)
        {
            // var itemID = cmbxItems.Text;
            var items = listItems.Where(x => x.IID == ItemID).FirstOrDefault();
            if (items != null)
            {
                return items.IID;
            }
            return 0;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            var isAdd = true;
            if (txtCode.Text != "")
            {
                var itemValue = db.Items.AsNoTracking().Where(x => x.BarcodeNo == txtCode.Text && x.CompanyID == compID).FirstOrDefault();
                itemIDValue = Convert.ToInt32(itemValue.IID.DefaultZero());
            }

            if (ItemID(itemIDValue) == 0)
            {
                MessageBox.Show("Item is Required", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbxItems.Focus();
                return;
            }
            if (txtpcs.Text == "")
            {
                MessageBox.Show("Pieces is Required", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtpcs.Focus();
                return;
            }

            if (txtctn.Text == "")
            {
                MessageBox.Show("Ctn is Required", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtctn.Focus();
                return;
            }

            if(txtctn.Text == "0" && txtpcs.Text == "0")
            {
                MessageBox.Show("Please enter quantity or ctn", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtctn.Focus();
                return;

            }



            var iid = ItemID(itemIDValue);
            var check123 = txtCode.Text;
            var item = db.Items.Where(x => x.IID == iid).FirstOrDefault();
            if (item != null)
            {
                //var TotalPcs = (item.CTN_PCK ?? 0 * Convert.ToInt32( txtctn.Text) + Convert.ToInt32( txtpcs.Text));
                //var PcsRate = Convert.ToDouble(txtRate.Text) / TotalPcs;
                //PcsRate = Convert.ToDouble( PcsRate.DefaultZero());
                //var NetAmount = PcsRate - Convert.ToDouble(txtDisc.Text);
                //txtDisPer.Text = txtDisPer.Text ?? "0";
                // NetAmount = NetAmount * Convert.ToDouble( txtDisPer.Text.DefaultZero()) == 0 ? 1 : ( txtDisPer.Text);
                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        var itemID = Convert.ToInt32(row.Cells[0].Value.DefaultZero());
                        if (Convert.ToInt32(lblItemID.Text) == itemID)
                        {
                            row.Cells[0].Value = ItemID(itemIDValue);
                            row.Cells[1].Value = cmbxItems.Text;
                            if (compID != 1015)
                            {
                                row.Cells[2].Value = txtBatch.Text;
                                row.Cells["Column3"].Value = dtExpirt.Text;
                            }
                            row.Cells["Column4"].Value = txtctn.Text.DefaultZero();
                            row.Cells["Pcs"].Value = txtpcs.Text.DefaultZero();
                            row.Cells["Amt"].Value = txtRate.Text.DefaultZero();
                            row.Cells["rte"].Value = String.Format("{0:0.00}", txtPcsRate.Text).DefaultZero();
                            row.Cells["disp"].Value = txtDisPer.Text.DefaultZero();
                            row.Cells["Column5"].Value = txtDisc.Text.DefaultZero();
                            row.Cells["netAm"].Value = String.Format("{0:0.00}", txtNet.Text).DefaultZero();
                            row.Cells["Sp"].Value = txtSaleP.Text.DefaultZero();
                            row.Cells["SaleRate"].Value = txtSaleRate.Text.DefaultZero();
                            row.Cells["article"].Value = "";
                            row.Cells["Unit"].Value = cmbxPackaging.SelectedIndex.DefaultZero() ;
                            row.Cells["TotalAmount"].Value = String.Format("{0:0.00}", txtTotalAmount.Text).DefaultZero();
                            isAdd = false;
                        }
                    }
                }
                if (isAdd == true)
                {
                    this.invGrid.Rows.Add(
                          ItemID(itemIDValue),
                    cmbxItems.Text,
                        txtBatch.Text,
                        dtExpirt.Text,
                        txtctn.Text.DefaultZero(),
                        txtpcs.Text.DefaultZero(),
                        txtRate.Text.DefaultZero(),
                        String.Format("{0:0.00}",
                        txtPcsRate.Text).DefaultZero(),
                        txtDisPer.Text.DefaultZero(),
                        txtDisc.Text.DefaultZero(),
                        String.Format("{0:0.00}", txtNet.Text).DefaultZero(),
                        txtSaleP.Text.DefaultZero(),
                        txtSaleRate.Text.DefaultZero(),
                       "Remove", "Update", false, "",cmbxPackaging.SelectedIndex, String.Format("{0:0.00}", txtTotalAmount.Text).DefaultZero());
                }

                invGrid_SelectionChanged(null, null);
                clear(true);
            }
        }

        private void metroTextBox1_Leave(object sender, EventArgs e)
        {
            var dsa = txtCode.Text;
            if (dsa != "")
            {
                var items = listItems.Where(x => x.BarcodeNo == dsa).FirstOrDefault();
                if (items != null)
                {
                    cmbxItems.Text = items.IName;
                    txtRate.Text = items.PurPrice.DefaultZero().ToString();
                }
            }
        }

        private void invGrid_SelectionChanged(object sender, EventArgs e)
        {
            Decimal totalNetAmount = 0;
            decimal totalAmount = 0;
            foreach (DataGridViewRow row in invGrid.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    lblItemID.Text = row.Cells[0].Value.DefaultZero();
                    cmbxItems.Text = row.Cells[1].Value.ToString();
                    if (compID != 1015)
                    {
                        //dtExpirt.Text = row.Cells["Column3"].Value.ToString();
                    }
                    txtctn.Text = row.Cells["Column4"].Value.ToString();
                    txtpcs.Text = row.Cells["Pcs"].Value.ToString();
                    txtRate.Text = row.Cells["Amt"].Value.ToString();
                    txtDisPer.Text = (row.Cells["disp"].Value ?? "0").ToString();
                    txtDisc.Text = (row.Cells["Column5"].Value ?? "0").ToString();
                    txtSaleP.Text = (row.Cells["Sp"].Value ?? "0").ToString();
                    totalNetAmount += Convert.ToDecimal(row.Cells["netAm"].Value.DefaultZero());
                    totalAmount += Convert.ToDecimal(row.Cells["TotalAmount"].Value.DefaultZero());
                    txtNet.Text = totalNetAmount.ToString();
                }
            }
            txtDisfooter.Text = txtDisfooter.Text.DefaultZero();
            txtTotalAm.Text = totalNetAmount.ToString();
            lblTotalItemDisc.Text= (totalAmount - totalNetAmount).ToString();
            txtNetAm.Text = (Convert.ToDecimal(txtTotalAm.Text) - Convert.ToDecimal(txtDisfooter.Text)).ToString();
        }

        private void txtPartyType_Leave(object sender, EventArgs e)
        {

        }
        public void SetInvID(String InvID)
        {
            lblInvN.Text = InvID;
            lblInvN.Visible = true;
            lblInvHeader.Visible = true;
            txtInv.Enabled = false;

        }

        public void EditInv(string invNo)
        {


            clear();
            var Master = db.Pur_M.Where(x => x.InvNo == invNo).FirstOrDefault();
            if (Master != null)
            {
                lblRID.Text = Master.RID.ToString();
                txtDisfooter.Text = Master.DisAmt.ToString();
                txtTotalAm.Text = Master.TotalAmount.ToString();
                txtRemarks.Text = Master.Rem.ToString();
                txtNetAm.Text = Master.NetAmt.ToString();
                var AcoountId = db.COA_D.AsNoTracking().Where(x => x.AC_Code == Master.AC_Code).FirstOrDefault();

                cmbxAccID.SelectedValue = AcoountId.CAC_Code;


                cmbxvendor.SelectedValue = Convert.ToInt32(Master.AC_Code);
                txtbilty.Text = Master.BiltyNo;
                txtInv.Text = Master.VenInvNo;
                txtInvDate.Text = Master.VenInvDate;

                var Detail = db.Pur_D.Where(x => x.RID == Master.RID).ToList();
                foreach (var item in Detail)
                {
                    var itemName = db.Items.Where(x => x.IID == item.IID).FirstOrDefault();
                    if (itemName != null)
                    {
                        string batch = "";
                        DateTime Expiry = DateTime.Now;
                        var stk = stock.Where(x => x.ItemID == itemName.IID).FirstOrDefault();
                        if (stk != null)
                        {
                            batch = stk.BatchNO;
                            Expiry = stk.Expiry ?? DateTime.Now;
                        }

                        //var PcsRate = Convert.ToDouble(item.PurPrice) / ((item.CTN * itemName.CTN_PCK) + Convert.ToInt32(item.PCS));
                        //var NetAmount = PcsRate - Convert.ToDouble(txtDisc.Text == "" ? "0" : txtDisc.Text);
                        //txtDisPer.Text = txtDisPer.Text ?? "0";
                        //NetAmount = NetAmount * Convert.ToDouble(txtDisPer.Text == "0" ? "1" : "0." + txtDisPer.Text);

                        //var TotalPcs = (itemName.CTN_PCK * Convert.ToInt32(item.CTN)) + Convert.ToInt32(item.PCS);
                        //TotalPcs = TotalPcs == 0 ? 1 : TotalPcs;
                        //var PcsRate = Convert.ToDouble(item.PurPrice) / TotalPcs;
                        //var NetAmount = PcsRate - Convert.ToDouble(item.DisRs);
                        //txtDisPer.Text = txtDisPer.Text ?? "0";
                        //NetAmount = NetAmount * Convert.ToDouble(item.DisP == 0 ? 1 : 0.0 + item.DisP);
                        //txtNet.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) - (DiscPercValue + DiscValue)).ToString();


                        var ctnValue = (Convert.ToDouble(item.PurPrice.DefaultZero()) * Convert.ToDouble(item.CTN));

                        var pcs = (Convert.ToDouble(item.PurPrice.DefaultZero()) * Convert.ToDouble(item.PCS.DefaultZero()));

                        var netAmt = (ctnValue + pcs).ToString();




                        var DiscPercValue = Convert.ToDouble(Convert.ToDouble(item.DisP.DefaultZero()) / 100 * Convert.ToDouble(netAmt.DefaultZero()));

                        var DiscValue = Convert.ToDouble(item.DisRs.DefaultZero());


                       netAmt= (Convert.ToDouble(netAmt.DefaultZero()) - (DiscPercValue + DiscValue)).ToString();

                        this.invGrid.Rows.Add(item.IID, itemName.IName, batch, Expiry, item.CTN, item.PCS, item.PurPrice, String.Format("{0:0.00}", item.PurPrice), item.DisP, item.DisRs, netAmt, "",
 txtSaleRate.Text.DefaultZero(),
"Remove", "Update", false, "", item.Pur_D_UnitID == null ? 0 : item.Pur_D_UnitID);

                        //             invGrid_SelectionChanged(null, null);
                    }
                }

            }
        }

        private void invGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 13)
                    {
                        invGrid.Rows.RemoveAt(e.RowIndex);
                        invGrid_SelectionChanged(null, null);
                         clear(true);
                    }
                    //else
                    //{
                    //    var ItemID = Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells["itemID"].Value);
                    //    var items = db.Items.Where(x => x.IID == ItemID).FirstOrDefault();
                    //    items.SalesPrice = Convert.ToDouble(invGrid.Rows[e.RowIndex].Cells["Sp"].Value.ToString() == "" ? "0" : invGrid.Rows[e.RowIndex].Cells["Sp"].Value);
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
            lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text.DefaultZero()) + Convert.ToDouble(txtNetAm.Text.DefaultZero())).ToString();


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
        private void BindItems(object ArticleID)
        {
            if (Convert.ToInt32(ArticleID) != 0)
            {
                //List<Batches> batch = new List<Batches>();
                //batch.Add(new Batches { ID = 0, Name = "--SELECT--" });
                //batch.AddRange((from t1 in db.Items
                //                join t2 in db.Sizes on t1.IID equals t2.IID
                //                join t3 in db.Colors on t1.IID equals t3.IID
                //                select new Batches
                //                {
                //                    ID = t1.IID,
                //                    Name = t1.IName + "-" + t2.SizeName + "-" + t3.Name
                //                }).ToList());

                //if (batch.Count > 0)
                //{
                //    FillCombo(cmbxItems, batch, "Name", "ID", -1);
                //}
            }
            else
            {
                List<Lib.Entity.Items> Items = new List<Lib.Entity.Items>();
                Items.Add(new Lib.Entity.Items { IID = 0, IName = "--SELECT--" });
                Items.AddRange(db.Items.Where(x => x.CompanyID == compID).ToList());
            }
        }
        private void invGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            itemIDValue = Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells[0].Value.DefaultZero());
            cmbxItems.Text = invGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            //ddlBatch.SelectedText = Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
            dtExpirt.Text = invGrid.Rows[e.RowIndex].Cells["Column3"].Value.ToString();
            txtctn.Text = invGrid.Rows[e.RowIndex].Cells["Column4"].Value.ToString();
            txtpcs.Text = invGrid.Rows[e.RowIndex].Cells["Pcs"].Value.ToString();
            txtRate.Text = invGrid.Rows[e.RowIndex].Cells["Amt"].Value.ToString();
            txtDisPer.Text = (invGrid.Rows[e.RowIndex].Cells["disp"].Value ?? "0").ToString();
            txtDisc.Text = (invGrid.Rows[e.RowIndex].Cells["Column5"].Value ?? "0").ToString();
            txtSaleP.Text = (invGrid.Rows[e.RowIndex].Cells["Sp"].Value ?? "0").ToString();
            cmbxPackaging.SelectedIndex =Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells["Unit"].Value ?? "0");
            txtTotalAmount.Text = Convert.ToDecimal(invGrid.Rows[e.RowIndex].Cells["TotalAmount"].Value ?? "0").ToString();
            lblItemID.Text = Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells[0].Value.DefaultZero()).ToString();
            txtRate_Leave(null, null);
            txtSaleP_Leave(null, null);
        }

        private void Items_Leave(Lib.Entity.Items item, MetroFramework.Controls.MetroTextBox txtbox = null)
        {
            dataGridView1.Visible = false;
            panel1.Visible = false;
            if (item == null)
            {
                cmbxItems.Focus();
            }
            else
            {
                try
                {
                    itemIDValue = Convert.ToInt32(item.IID.DefaultZero());
                    cmbxItems.Text = item.IName;
                    txtCode.Text = item.BarcodeNo;

                    int warehouseId = (int)cmbxWareHouse.SelectedValue;
                    var RR = ReportsController.getWareHouseStockByID(item.IID, compID, warehouseId);


                    lblStock.Text = RR.Rows[0]["total"].ToString();



                    txtDisc.Text = item.DisR.DefaultZero();
                    txtDisPer.Text = item.DisP.DefaultZero();
                    txtpcs.Text = 0.ToString();
                    txtctn.Text = 0.ToString();
                    txtNet.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero())).ToString();
                    var DiscPercValue = Convert.ToDouble(Convert.ToDouble(txtDisPer.Text.DefaultZero()) / 100 * Convert.ToDouble(txtNet.Text.DefaultZero()));
                    var DiscValue = Convert.ToDouble(txtDisc.Text.DefaultZero());

                    txtSaleP.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero())).ToString();
                    txtPcsRate.Text = String.Format("{0:0.00}", Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));
                    txtSaleRate.Text = String.Format("{0:0.00}", Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(1));

                    if (item.Unit_ID == null)
                    {

                        cmbxPackaging.SelectedIndex = 0;
                    }
                    else if (item.Unit_ID == 1)
                    {

                        cmbxPackaging.SelectedIndex = 0;
                    }
                    else {
                        cmbxPackaging.SelectedIndex = 1;
                    }

                    if (Convert.ToInt32(cmbxAccID.SelectedValue) == 3 || Convert.ToInt32(cmbxAccID.SelectedValue) == 9)
                    {
                        int Code = Convert.ToInt32(cmbxvendor.SelectedValue);
                        var abc = db.Itemledger.AsNoTracking().Where(x => x.IID == item.IID && x.TypeCode == 2 && x.AC_CODE == Code);
                        if (abc == null)
                        {
                        }
                        else
                        {
                            double quantity = 0;
                            double amt = 0;
                            foreach (Itemledger getledger in abc)
                            {
                                quantity = Convert.ToDouble(getledger.PJ);
                                amt = Convert.ToDouble(getledger.Amt);
                            }
                            lblPreviousPrice.Text = Convert.ToDouble(amt / quantity).ToString();
                        }
                    }
                    txtctn.Focus();
                    txtbox = null;
                }
                catch (Exception ex)
                {

                }
            }



            //    dataGridView1.Visible = false;
            //    panel1.Visible = false;
            //    if (item == null)
            //    {
            //        cmbxItems.Focus();
            //    }
            //    else
            //    {
            //        try
            //        {

            //            int warehouseId = (int)cmbxWareHouse.SelectedValue;
            //            var RR = ReportsController.getWareHouseStockByID(item.IID, compID, warehouseId);
            //            lblStock.Text = RR.Rows[0]["total"].ToString();
            //            cmbxItems.Text = item.IName;
            //            txtCode.Text = item.BarcodeNo;
            //            txtRate.Text = item.PurPrice.DefaultZero();
            //            txtDisc.Text = item.DisR.DefaultZero();
            //            txtDisPer.Text = item.DisP.DefaultZero();
            //            txtpcs.Text = 1.ToString();
            //            txtctn.Text = 0.ToString();
            //            txtNet.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero())).ToString();
            //            var DiscPercValue = Convert.ToDouble(Convert.ToDouble(txtDisPer.Text.DefaultZero()) / 100 * Convert.ToDouble(txtNet.Text.DefaultZero()));
            //            var DiscValue = Convert.ToDouble(txtDisc.Text.DefaultZero());
            //            txtNet.Text = String.Format("{0:0.00}", Convert.ToDouble(txtRate.Text.DefaultZero()) - (DiscPercValue + DiscValue));
            //            txtSaleP.Text = (Convert.ToDouble(item.SalesPrice.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero())).ToString();
            //            txtPcsRate.Text = String.Format("{0:0.00}", Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));
            //            txtSaleRate.Text = item.SalesPrice.DefaultZero(); //String.Format("{0:0.00}", Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(1));
            //            txtctn.Focus();

            //        }
            //        catch (Exception ex)
            //        {

            //        }

        }


        private void metroButton2_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear(bool grid = false)
        {

            txtInv.Enabled = true;
            txtInv.Text = "";
            txtCode.Text = "";
            // cmbxvendor.SelectedIndex = -1;
            cmbxItems.Text = "";
            txtBatch.Text = "0";
            txtCode.Focus();
            if (grid == false)
            {
                invGrid.DataSource = null;
                invGrid.Rows.Clear();
                txtNetAm.Text = "0";
                txtTotalAm.Text = "0";
                txtDisfooter.Text = "0";
                lblRID.Text = "0";
                txtInv.Focus();
            }
            txtDisc.Text = "0";
            txtDisPer.Text = "0";
            txtctn.Text = "0";
            txtpcs.Text = "0";
            txtbilty.Text = "0";
            txtRate.Text = "0";
            txtNet.Text = "0";
            txtTotalAmount.Text="0";
            txtPcsRate.Text = "0";
            txtSaleRate.Text = "0";
            lblInvN.Visible = false;
            lblInvHeader.Visible = false;
            dtExpirt.Value = new DateTime(1900, 01, 01);

            txtSaleP.Text = "0";
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
            Accountvalidation();
            if (invGrid.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Items In Grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int Warehousehouse = (int)cmbxWareHouse.SelectedValue;

            if (Warehousehouse == 0)
            {
                MessageBox.Show("Please Select WareHouse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            int vebdorid = Convert.ToInt32(cmbxvendor.SelectedValue);
            if (vebdorid == 0 || vebdorid == null)
            {
                MessageBox.Show("Please select AccountName", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbxvendor.Focus();
                return;
            }
            Pur_M purchase = null;
            DbContextTransaction transaction = db.Database.BeginTransaction();
            try
            {
                if (InvoiceNo == "0")
                {
                    purchase = new Pur_M();
                    DataAccess access = new DataAccess();
                    Purc_M coa = new Purc_M();
                    coa.InvType = "PI";
                    SqlConnection con = new SqlConnection(ConnectionStrings.GetCS);
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    purchase.InvNo = access.GetScalar("Gen_NewInvNo", coa, con, trans);
                    purchase.InvDT = DateTime.Now.ToString();
                }
                else
                {
                    var RID = Convert.ToInt32(InvoiceNo);
                    purchase = db.Pur_M.Where(x => x.RID == RID).FirstOrDefault();
                    purchase.Edit_Date = DateTime.Now;
                }
                purchase.InvType = "PI";
                purchase.EDate = dtpInvoice.Value;
                //purchase.VendorId = Convert.ToInt32(cmbxvendor.SelectedValue);
                purchase.WID = Warehousehouse;
                purchase.AC_Code = Convert.ToInt32(vebdorid);
                purchase.TotalAmount = Convert.ToDouble(txtTotalAm.Text);
                purchase.DisAmt = Convert.ToDouble(txtDisfooter.Text);
                purchase.NetAmt =Convert.ToDouble(txtNetAm.Text);
                purchase.BiltyNo = txtbilty.Text;
                purchase.Rem = txtRemarks.Text;
                purchase.CompID = compID;
                purchase.VenInvNo = txtInv.Text;
                purchase.VenInvDate = txtInvDate.Text;

                if (InvoiceNo == "0")
                {
                    db.Pur_M.Add(purchase);
                }
             
                if (InvoiceNo != "0")
                {
                    db.Entry(purchase).State = EntityState.Modified;
                    db.Pur_D.RemoveRange(db.Pur_D.Where(x => x.RID == purchase.RID));
                    db.Itemledger.RemoveRange(db.Itemledger.Where(x => x.RID == purchase.RID && x.TypeCode == 2));
                    db.GL.RemoveRange(db.GL.Where(x => x.RID == purchase.RID && x.TypeCode == 2));
                }
                db.SaveChanges();
                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        int id = Convert.ToInt32(row.Cells[0].Value);
                        var item = db.Items.Where(x => x.IID == id).FirstOrDefault();
                        Pur_D detail = new Pur_D();
                        detail.RID = purchase.RID;
                        detail.IID = Convert.ToDouble(row.Cells[0].Value.ToString().DefaultZero());
                        if (compID != 1015)
                        {
                            detail.ExpDT = Convert.ToDateTime(row.Cells["Column3"].Value);
                        }
                        detail.CTN = Convert.ToDouble(row.Cells["Column4"].Value.ToString().DefaultZero());
                        detail.PCS = Convert.ToDouble(row.Cells["Pcs"].Value.ToString().DefaultZero());

                        if (item.CTN_PCK == 0 || item.CTN_PCK == null)
                        {
                            detail.Qty = (Convert.ToDouble(row.Cells["Column4"].Value.DefaultZero())) + Convert.ToDouble(row.Cells["Pcs"].Value);

                        }
                        else
                        {
                            detail.Qty = (item.CTN_PCK * Convert.ToDouble(row.Cells["Column4"].Value)) + Convert.ToDouble(row.Cells["Pcs"].Value);
                        }

                       
                        detail.Pur_D_UnitID = Convert.ToInt32(row.Cells["Unit"].Value ?? 0);
                        detail.PurPrice = Convert.ToDouble(row.Cells["rte"].Value ?? 0);
                        detail.DisP = Convert.ToDouble(row.Cells["disp"].Value ?? 0);
                        detail.DisRs = Convert.ToDouble(row.Cells["Column5"].Value ?? 0);
                        //BatchNo BAtchNO
                        //Quantity Caton+Pices
                        //  detail.PurPrice = Convert.ToDouble(row.Cells["Amt"].Value.ToString().DefaultZero());
                        detail.PAmt = Convert.ToDouble(row.Cells["Amt"].Value.ToString().DefaultZero());
                        detail.Amt = Convert.ToDouble(row.Cells["netAm"].Value.ToString().DefaultZero());
                        detail.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells["disp"].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells["Amt"].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        //DisAmount   percentage in rupess
                        //Amt         after complete discount
                        db.Pur_D.Add(detail);
                        db.SaveChanges();


                        if (item.Inv_YN == false)
                        {

                            double ctn = Convert.ToDouble(item.CTN_PCK) * Convert.ToDouble(row.Cells["Column4"].Value);
                            double purchaseQuantity = ctn + Convert.ToDouble(row.Cells["Pcs"].Value);

                            var stockid = db.getStockByID(Convert.ToInt32(row.Cells[0].Value)).FirstOrDefault();
                            double totalStock = Convert.ToDouble(stockid.DefaultZero()) + purchaseQuantity;
                            double totalvalue = Convert.ToDouble(row.Cells["netAm"].Value.DefaultZero());
                            double averagevale = Convert.ToDouble(item.AveragePrice.DefaultZero()) * Convert.ToDouble(stockid.DefaultZero());
                            double newAverageValue = (totalvalue + averagevale) / totalStock;
                            var result = db.Items.SingleOrDefault(b => b.IID == item.IID);

                            if (totalStock < 0 || totalStock == 0)
                            {

                                result.AveragePrice = 0;
                            }
                            else
                            {

                                result.AveragePrice = newAverageValue;
                            }


                            Itemledger ledger = new Itemledger();
                            ledger.RID = purchase.RID;
                            ledger.IID = Convert.ToInt32(row.Cells[0].Value.DefaultZero());
                            ledger.EDate = dtpInvoice.Value;
                            // ledger.Bnid = BatchNo
                            ledger.TypeCode = 2;
                            ledger.AC_CODE = Convert.ToInt32(vebdorid);
                            ledger.WID = Warehousehouse;
                            // ledger.SiD =
                            if (compID != 1015)
                            {
                                ledger.ExpDT = Convert.ToDateTime(row.Cells["Column3"].Value.ToString());
                            }
                            ledger.CTN = Convert.ToDouble(row.Cells["Column4"].Value.ToString());
                            ledger.PCS = Convert.ToDouble(row.Cells["Pcs"].Value.ToString());
                            if (item.CTN_PCK == 0 || item.CTN_PCK == null)
                            {
                                ledger.PJ = (Convert.ToDouble(row.Cells["Column4"].Value.DefaultZero())) + Convert.ToDouble(row.Cells["Pcs"].Value);

                            }
                            else
                            {
                                ledger.PJ = (item.CTN_PCK * Convert.ToDouble(row.Cells["Column4"].Value)) + Convert.ToDouble(row.Cells["Pcs"].Value);
                            }
                            //ledger.PJ = (item.CTN_PCK ?? 0 * Convert.ToDouble(row.Cells["Column4"].Value)) + Convert.ToDouble(row.Cells["Pcs"].Value);
                            ledger.PurPrice = Convert.ToDouble(row.Cells["rte"].Value ?? 0);//Convert.ToDouble(row.Cells["Amt"].Value.ToString());
                            ledger.PAmt = Convert.ToDouble(row.Cells["Amt"].Value.ToString().DefaultZero());
                            ledger.Amt = Convert.ToDouble(row.Cells["netAm"].Value.ToString().DefaultZero());
                            ledger.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells["disp"].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells["Amt"].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                            detail.DisRs = Convert.ToDouble(row.Cells["Column5"].Value ?? 0);
                            ledger.DisP = Convert.ToDouble(row.Cells["disp"].Value ?? "0");
                            ledger.CompanyID = compID;
                            db.Itemledger.Add(ledger);
                            db.SaveChanges();
                        }


                        GL gl = new GL();
                        gl.RID = purchase.RID;
                        gl.TypeCode = 2;
                        gl.GLDate = dtpInvoice.Value;

                        gl.AC_Code = Convert.ToInt32(vebdorid);
                        gl.AC_Code2 = Convert.ToInt32(item.AC_Code_Inv);
                        gl.Narration = item.IName;
                        gl.MOP_ID = 2;

                        if (item.CTN_PCK == 0 || item.CTN_PCK == null)
                        {
                            gl.Qty_IN = (Convert.ToDouble(row.Cells["Column4"].Value.DefaultZero())) + Convert.ToDouble(row.Cells["Pcs"].Value);

                        }
                        else
                        {
                            gl.Qty_IN = (item.CTN_PCK * Convert.ToDouble(row.Cells["Column4"].Value)) + Convert.ToDouble(row.Cells["Pcs"].Value);
                        }

                        //gl.Qty_IN = (item.CTN_PCK ?? 0 * Convert.ToDouble(row.Cells["Column4"].Value)) + Convert.ToDouble(row.Cells["Pcs"].Value);
                        gl.PAmt = Convert.ToDouble(row.Cells["Amt"].Value.DefaultZero());

                        gl.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells["disp"].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells["Amt"].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl.DisRs = Convert.ToDouble(row.Cells["Column5"].Value ?? 0);
                        gl.DisP = Convert.ToDouble(row.Cells["disp"].Value ?? "0");
                        gl.Debit = 0;
                        gl.Credit = Convert.ToDouble(row.Cells["netAm"].Value.DefaultZero());
                        gl.CompID = compID;
                        db.GL.Add(gl);
                        db.SaveChanges();

                        GL gl2 = new GL();
                        gl2.RID = purchase.RID;
                        gl2.TypeCode = 2;
                        gl2.GLDate = dtpInvoice.Value;

                        gl2.AC_Code = Convert.ToInt32(item.AC_Code_Inv);
                        gl2.AC_Code2 = Convert.ToInt32(vebdorid);
                        gl2.Narration = (cmbxvendor.Text).ToString();
                        gl2.MOP_ID = 2;

                        if (item.CTN_PCK == 0 || item.CTN_PCK == null)
                        {
                            gl2.Qty_IN = (Convert.ToDouble(row.Cells["Column4"].Value.DefaultZero())) + Convert.ToDouble(row.Cells["Pcs"].Value);

                        }
                        else
                        {
                            gl2.Qty_IN = (item.CTN_PCK ?? 0 * Convert.ToDouble(row.Cells["Column4"].Value)) + Convert.ToDouble(row.Cells["Pcs"].Value);
                        }
                        // gl2.Qty_IN = (item.CTN_PCK ?? 0 * Convert.ToDouble(row.Cells["Column4"].Value)) + Convert.ToDouble(row.Cells["Pcs"].Value);
                        gl2.PAmt = Convert.ToDouble(row.Cells["Amt"].Value.DefaultZero());

                        gl2.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells["disp"].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells["Amt"].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl2.DisRs = Convert.ToDouble(row.Cells["Column5"].Value ?? 0);
                        gl2.DisP = Convert.ToDouble(row.Cells["disp"].Value ?? "0");
                        gl2.Debit = Convert.ToDouble(row.Cells["netAm"].Value.DefaultZero());
                        gl2.Credit = 0;
                        gl2.CompID = compID;

                        db.GL.Add(gl2);
                        db.SaveChanges();
                    }
                 



                }


                if (txtDisfooter.Text == "" || txtDisfooter.Text == "0")
                {

                }
                else
                {
                    int customer = Convert.ToInt32(cmbxvendor.SelectedValue);
                    var Income = db.COA_D.Where(x => x.AC_Title == "Purchase Total Discount Income" && x.CompanyID == compID && x.CAC_Code == 14).FirstOrDefault();

                    if (Income == null)
                    {
                        COA_D coaD = new COA_D();
                        coaD.CAC_Code = 14;
                        coaD.PType_ID = 1;
                        coaD.ZID = 0;
                        coaD.AC_Code = (int)db.GetAc_Code(14).FirstOrDefault();
                        coaD.AC_Title = "Purchase Total Discount Income";
                        coaD.DR = 0;
                        coaD.CR = 0;
                        coaD.Qty = 0;
                        coaD.CompanyID = compID;
                        coaD.InActive = false;
                        db.COA_D.Add(coaD);
                        db.SaveChanges();



                        Income = db.COA_D.AsNoTracking().Where(x => x.AC_Title == "Purchase Total Discount Income" && x.CompanyID == compID && x.CAC_Code == 14).FirstOrDefault();





                    }



                    var customerDetial = db.COA_D.AsNoTracking().Where(x => x.AC_Code == customer).FirstOrDefault();

                    GL gl = new GL();
                    gl.RID = purchase.RID;
                    gl.TypeCode = 2;
                    gl.GLDate = dtpInvoice.Value;
                    //  gl.IPrice = Convert.ToDouble(row.Cells["Column4"].Value.DefaultZero());
                    gl.AC_Code = (int)Income.AC_Code;
                    gl.AC_Code2 = Convert.ToInt32(vebdorid);
                    gl.Narration = customerDetial.AC_Title.ToString();
                    //  gl.MOP_ID = 2;
                    // gl.Qty_Out = (item.CTN_PCK ?? 0 * Convert.ToInt32(row.Cells[2].Value)) + Convert.ToInt32(row.Cells["Column3"].Value);
                    // gl.PAmt = Convert.ToDouble(row.Cells["Pcs"].Value.DefaultZero());
                    //  gl.DisP = Convert.ToDouble(row.Cells["Amt"].Value ?? "0");
                    //  gl.DisRs = Convert.ToDouble((row.Cells["rte"].Value ?? "0"));
                    gl.DisAmt = 0;
                    gl.Debit = 0;
                    gl.Credit = Convert.ToDouble(txtDisfooter.Text);
                    gl.CompID = compID;
                    db.GL.Add(gl);
                    db.SaveChanges();

                    GL gl1 = new GL();
                    gl1.RID = purchase.RID;
                    gl1.TypeCode = 2;
                    gl1.GLDate = dtpInvoice.Value;

                    gl1.AC_Code = Convert.ToInt32(vebdorid);
                    gl1.AC_Code2 = Income.AC_Code;
                    gl1.Narration = (string)Income.AC_Title;
                    //  gl.MOP_ID = 2;
                    // gl1.Qty_Out = (item.CTN_PCK ?? 0 * Convert.ToInt32(row.Cells[2].Value)) + Convert.ToInt32(row.Cells["Column3"].Value);
                    //gl1.PAmt = Convert.ToDouble(row.Cells["Pcs"].Value.DefaultZero());
                    //gl1.DisP = Convert.ToDouble(row.Cells["Amt"].Value ?? "0");
                    //gl1.DisRs = Convert.ToDouble((row.Cells["rte"].Value ?? "0"));
                    //gl1.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells["Amt"].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells["Pcs"].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                    gl1.Debit = Convert.ToDouble(txtDisfooter.Text);
                    gl1.Credit = 0;
                    gl1.CompID = compID;
                    db.GL.Add(gl1);
                    db.SaveChanges();
                }

                int mode = Convert.ToInt32(cmbxPaymentMode.SelectedValue);

                int Cashmode = Convert.ToInt32(cmbxAccID.SelectedValue);
                if (mode == 0)
                {



                }
                else if (Cashmode == 1)
                {




                }

                else
                {

                    GL gl = new GL();
                    gl.RID = purchase.RID;
                    gl.TypeCode = 2;
                    gl.GLDate = dtpInvoice.Value;
                    // gl.IPrice = Convert.ToDouble(row.Cells["Column4"].Value.DefaultZero());
                    gl.AC_Code = Convert.ToInt32(cmbxPaymentMode.SelectedValue);
                    gl.AC_Code2 = Convert.ToInt32(vebdorid);
                    gl.Narration = cmbxvendor.SelectedText + " has paid cash";
                    gl.Debit = 0;
                    gl.Credit = Convert.ToDouble(txtNetAm.Text.DefaultZero());
                    gl.CompID = compID;
                    db.GL.Add(gl);
                    db.SaveChanges();

                    GL gl1 = new GL();
                    gl1.RID = purchase.RID;
                    gl1.TypeCode = 2;
                    gl1.GLDate = dtpInvoice.Value;
                    //   gl1.IPrice = item.AveragePrice;
                    gl1.AC_Code = Convert.ToInt32(vebdorid); //item.AC_Code_Inv;
                    gl1.AC_Code2 = Convert.ToInt32(cmbxPaymentMode.SelectedValue);
                    gl1.Narration = cmbxvendor.SelectedText + " has paid cash";
                    gl1.Debit = Convert.ToDouble(txtNetAm.Text.DefaultZero()); ;
                    gl1.Credit = 0;
                    gl1.CompID = compID;
                    db.GL.Add(gl1);
                    db.SaveChanges();


                }
                transaction.Commit();
                UpdateItemRate();
                MessageBox.Show("Invoice Save Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateItemRate()
        {
            foreach (DataGridViewRow row in invGrid.Rows)
            {
                var check = Convert.ToBoolean(row.Cells["chkRate"].Value);
                if (check)
                {
                    var ItemID = Convert.ToInt32(row.Cells["ID"].Value);
                    var items = db.Items.Where(x => x.IID == ItemID).FirstOrDefault();
                    items.SalesPrice = Convert.ToDouble(row.Cells["SaleRate"].Value.DefaultZero());
                    db.Entry(items).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        private void cmbxAccID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsa = Convert.ToInt32(cmbxAccID.SelectedIndex);
            if (dsa >= 1)
            {
                int value = Convert.ToInt32(cmbxAccID.SelectedValue);
                var vendor = db.COA_D.AsNoTracking().Where(x => x.CAC_Code == value && x.CompanyID == compID && x.InActive == false).ToList();
                FillCombo(cmbxvendor, vendor, "AC_Title", "AC_Code", 0);

                int account = (int)cmbxAccID.SelectedValue;
                if (account == 1)
                {
                    lblGrandTotal.Text = "0";
                    lblAccountBalance.Text = "0";
                }
                else
                {


                    lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text.DefaultZero()) + Convert.ToDouble(txtNetAm.Text.DefaultZero())).ToString();
                }


                if (value == 3)
                {

                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    //vebdorid = Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Now, Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = debit - credit;


                    double balance = 0;
                    int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Now, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = debit - credit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getcustomerLedgerSummaryByDate(DateTime.Now, DateTime.Now, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                    foreach (DataRow rows in getdata.Rows)
                    {


                        balance = balance - (double)rows["credit"];
                        balance = balance + (double)rows["debit"];








                    }




                    lblAccountBalance.Text = balance.ToString();


                }


                if (value == 9)
                {
                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    //vebdorid = Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Now, Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = credit - debit;





                    double balance = 0;
                    int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    var previosBalance = ReportsController.getVendorPreviousBalance(DateTime.Now, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = credit - debit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getVendorLedgerSummaryByDate(DateTime.Now, DateTime.Now, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                    foreach (DataRow rows in getdata.Rows)
                    {


                        balance = balance + (double)rows["credit"];
                        balance = balance - (double)rows["debit"];






                    }


                    lblAccountBalance.Text = balance.ToString();

                }


            }
            else if (dsa == 0)
            {

                //   int value = Convert.ToInt32(cmbxAccID.SelectedValue);
                //var vendor = db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList();
                //FillCombo(cmbxvendor, vendor, "AC_Title", "CAC_Code", 0);

                List<COA_D> parties = new List<COA_D>();
                parties.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
                FillCombo(cmbxvendor, parties, "AC_Title", "AC_Code", 0);
                FillCombo(cmbxvendor, parties, "AC_Title", "AC_Code", 0);
                FillCombo(cmbxvendor, parties, "AC_Title", "AC_Code", 0);

            }
            calculation();



        }


        //if (cmbxAccID.SelectedValue.ToString() != null)
        //{
        //   // int value = Convert.ToInt32(cmbxAccID.SelectedValue.ToString());
        //   // var vendor = db.COA_D.Where(x => x.CAC_Code == value).ToList();

        //   // FillCombo(cmbxvendor, vendor, "AC_Title", "CAC_Code", 0);


        //}
        //cmbxvendor.SelectedValue = cmbxAccID.SelectedValue;
        //var account = Convert.ToInt32(cmbxAccID.SelectedValue);
        //cmbxvendor.SelectedValue = cmbxAccID.SelectedValue;

        //if (cmbxvendor.SelectedValue != null)
        //{

        private BarCodeListener ScannerListener;
        bool isGrid = false;
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
            if (keyData == (Keys.Control | Keys.E))
            {
                metroButton5_Click(null, null);
            }
            if (keyData == (Keys.Escape))
            {
                clear();
            }
            else if (keyData == (Keys.Enter) && txtCode.Text == "")
            {
                if (dataGridView1.Visible == true)
                {
                    var ID = dataGridView1.SelectedRows[0].Cells[0].Value;
                    int IID = Convert.ToInt32(ID);
                    lblItemID.Text = ID.ToString();
                    dataGridView1.Visible = false;
                    var items = db.Items.AsNoTracking().Where(x => x.IID == IID).FirstOrDefault();
                    if (items != null)
                    {
                        Items_Leave(items);
                        isGrid = false;
                        // metroButton1_Click(null, null);
                    }
                }

                else if (lblItemID.Text != "")
                {
                    int id = Convert.ToInt32(lblItemID.Text);
                    var items = db.Items.AsNoTracking().Where(x => x.IID == id).FirstOrDefault();
                    if (items != null)
                    {
                        txtpcs.Text = txtpcs.Text.DefaultZero();
                        txtRate.Text = txtRate.Text.DefaultZero();//items.SalesPrice.DefaultZero();
                        lblItemID.Text = items.IID.ToString();
                        calculation();
                        metroButton1_Click(null, null); // Replacw with new 
                                                        //txtCode.Text = "";
                        cmbxItems.Focus();
                    }


                }
                return true;
            }
            else if (keyData == (Keys.Enter) && (txtCode.Text != ""))
            {
                String id = txtCode.Text;
                var items = db.Items.Where(x => x.BarcodeNo == id).FirstOrDefault();
                if (items != null)
                {
                    txtpcs.Text = txtpcs.Text.DefaultZero() == "0" ? "1" : txtpcs.Text;
                    txtRate.Text = txtRate.Text.DefaultZero();//items.SalesPrice.DefaultZero();
                    lblItemID.Text = items.IID.ToString();
                    // calculation();
                    metroButton1_Click(null, null); // Replacw with new 
                                                    //txtCode.Text = "";
                    txtCode.Focus();
                }
                return true;
            }
            bool res = false;
            if (ScannerListener != null)
            {
                res = ScannerListener.ProcessCmdKey(ref msg, keyData);
            }
            res = keyData == Keys.Enter ? res : base.ProcessCmdKey(ref msg, keyData);
            return res;
        }

        private int ItemID()
        {
            var itemID = cmbxItems.Text;
            var items = listItems.Where(x => x.IName.ToLower().Trim().Contains(cmbxItems.Text.ToLower().Trim())).FirstOrDefault();
            if (items != null)
            {
                return items.IID;
            }
            return 0;
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            EditMessageBox messageBox = new EditMessageBox(PInvoice, compID, "PI");
            messageBox.Show();
        }

        private void txtRate_Leave(object sender, EventArgs e)
        {
            var ID = Convert.ToInt32(lblItemID.Text);
            var item = listItems.Where(x => x.IID == ID).FirstOrDefault();
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
            var ID = Convert.ToInt32(lblItemID.Text);
            var item = listItems.Where(x => x.IID == ID).FirstOrDefault();
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
                    row.Cells["chkRate"].Value = checkBox1.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDisPer_Leave(object sender, EventArgs e)
        {
            var ID = Convert.ToInt32(lblItemID.Text);
            var item = listItems.Where(x => x.IID == ID).FirstOrDefault();
            if (item != null)
            {
                calculation();
                //var Pcs = (item.CTN_PCK ?? 0 * Convert.ToInt32(txtctn.Text.DefaultZero())) + Convert.ToInt32(txtpcs.Text.DefaultZero());
                //var Rate = Convert.ToDouble(txtRate.Text.DefaultZero());
                //Pcs = Pcs == 0 ? 1 : Pcs;
                //var NetAmount = Rate / Pcs;
                //txtNet.Text = String.Format("{0:0.00}", NetAmount);
                //txtNet.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) - Convert.ToDouble(txtDisc.Text.DefaultZero())).ToString();
                //txtDisPer.Text = txtDisPer.Text ?? "0";
                //var DiscPerc = Convert.ToDouble(txtDisPer.Text.DefaultZero() == "0" ? "1" : "0." + txtDisPer.Text);
                //txtNet.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) * (DiscPerc == 1 ? 1 : 1 - DiscPerc)).ToString();
                //txtPcsRate.Text = String.Format("{0:0.00}", (Convert.ToDouble(txtNet.Text) / Pcs));
                //txtSaleRate.Text = String.Format("{0:0.00}", (Convert.ToDouble(txtSaleP.Text.DefaultZero()) / Pcs));
            }
        }
        public void calculation()
        {
            var ID = Convert.ToInt32(lblItemID.Text.DefaultZero());
            var itemID = ItemID(ID);
            var item = db.Items.Where(x => x.IID == itemID).FirstOrDefault();
            if (item != null)
            {

                if (txtRate.Text == "" || txtRate.Text == "0")
                {

                    txtRate.Text = item.SalesPrice.DefaultZero();
                }
                if (txtDisc.Text == "" || txtDisc.Text == "0")
                {
                    txtDisc.Text = item.DisR.DefaultZero();
                }

                if (txtDisPer.Text == "" || txtDisPer.Text == "0")
                {
                    txtDisPer.Text = item.DisP.DefaultZero();
                }

                if (txtpcs.Text == "" || txtpcs.Text == "0")
                {
                    txtpcs.Text = 0.ToString();

                }


                if (txtctn.Text == "" || txtctn.Text == "0")
                {
                    txtctn.Text = 0.ToString();

                }


                var ctn = (Convert.ToDouble(txtctn.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));

                if (ctn == 0)
                {
                    ctn = Convert.ToDouble(txtctn.Text.DefaultZero());
                }
              

                if (cmbxPackaging.SelectedIndex == 0)
                {


                    var ctnValue = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(ctn));

                    var pcs = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero()));

                    txtNet.Text = (ctnValue + pcs).ToString();

                    // txtNet.Text = txtRate.Text;
                    //  txtSaleP.Text = (ctnValue + pcs).ToString();
                    txtTotalAmount.Text = (ctnValue + pcs).ToString();
                    var DiscPercValue = Convert.ToDouble(Convert.ToDouble(txtDisPer.Text.DefaultZero()) / 100 * Convert.ToDouble(txtNet.Text.DefaultZero()));

                    var DiscValue = Convert.ToDouble(txtDisc.Text.DefaultZero());


                    txtNet.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) - (DiscPercValue + DiscValue)).ToString();

                    //    txtSaleP.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble((ctn + Convert.ToDouble(txtpcs.Text.DefaultZero())))).ToString();



                    txtPcsRate.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) / (ctn + Convert.ToDouble(txtpcs.Text.DefaultZero()))).ToString();
                    if (txtPcsRate.Text == "NaN") {


                        txtPcsRate.Text = "0";
                    }
                   
                 
                    txtSaleRate.Text = (Convert.ToDouble(txtSaleP.Text.DefaultZero())).ToString(); /*/ (ctn + Convert.ToDouble(txtpcs.Text.DefaultZero()))).ToString();*/

                    lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text.DefaultZero()) + Convert.ToDouble(txtNetAm.Text.DefaultZero())).ToString();
                }

                else {

                    var ctnValue = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtctn.Text.DefaultZero()));

                    var pcs = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero()));
                    if (txtpcs.Text == "" || txtpcs.Text == "0")
                    {


                    }

                    else {

                        if (item.CTN_PCK == 0 || item.CTN_PCK == null)
                        {
                            pcs = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero()));



                        }
                        else { 
                        var total=( Convert.ToDouble(txtRate.Text.DefaultZero()) / Convert.ToDouble(item.CTN_PCK.DefaultZero()));
                        pcs= ((total) * Convert.ToDouble(txtpcs.Text.DefaultZero()));

                        }
                     

                    }
  

                    txtNet.Text = (ctnValue + pcs).ToString();

                    // txtNet.Text = txtRate.Text;
                    //  txtSaleP.Text = (ctnValue + pcs).ToString();
                    txtTotalAmount.Text = (ctnValue + pcs).ToString();
                    var DiscPercValue = Convert.ToDouble(Convert.ToDouble(txtDisPer.Text.DefaultZero()) / 100 * Convert.ToDouble(txtNet.Text.DefaultZero()));

                    var DiscValue = Convert.ToDouble(txtDisc.Text.DefaultZero());


                    txtNet.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) - (DiscPercValue + DiscValue)).ToString();

                    //    txtSaleP.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble((ctn + Convert.ToDouble(txtpcs.Text.DefaultZero())))).ToString();



                    txtPcsRate.Text = ((Convert.ToDouble(txtNet.Text.DefaultZero()) / (ctn + Convert.ToDouble(txtpcs.Text.DefaultZero())))).ToString();

                    if (txtPcsRate.Text == "NaN")
                    {


                        txtPcsRate.Text = "0";
                    }

                    txtSaleRate.Text = (Convert.ToDouble(txtSaleP.Text.DefaultZero())).ToString(); /*/ (ctn + Convert.ToDouble(txtpcs.Text.DefaultZero()))).ToString();*/

                    lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text.DefaultZero()) + Convert.ToDouble(txtNetAm.Text.DefaultZero())).ToString();



                }

                if (txtPcsRate.Text == "NaN")
                {
                    MessageBox.Show("please select Quantity");


                }
            }
        }

        private void txtPcsRate_Leave(object sender, EventArgs e)
        {
            calculation();
        }

        private void txtSaleRate_Leave(object sender, EventArgs e)
        {
            // calculation();
        }

        private void cmbxItems_KeyDown(object sender, KeyEventArgs e)
        {

            lblItemID.Text = "0";
            if (e.KeyCode == Keys.Down)
            {
                e.Handled = true; // to prevent system processing
                moveDown("");
                return;
            }
            if (e.KeyCode == Keys.Up)
            {
                e.Handled = true; // to prevent system processing
                moveUp("");
                return;
            }

            if (cmbxItems.Text == "")
            {
                dataGridView1.Visible = false;
                panel1.Visible = false;
                return;
            }
            string Query = @"SELECT TOP 10 Items.IID , ISNULL(ArticleNo, '0') as Article, 
                           Items.IName Product, ISNULL(Color, 0) as Color, ISNULL(Size, 0) as Size
                          ,ISNULL(IComp_M.Comp , '') as Comp,ISNULL(Items.Formula , '') as Formula
                          ,ISNULL(Items.Cabinet , '') as Cabinet , ISNULL(vw.total,0) as Stock                 
                          FROM Items
                            left join Article on Items.IID = Article.ProductID
                            left join Colors on Items.Color = Colors.ColorID
                            left join Sizes on Items.Size = Sizes.SizeID
                            left join getWarehouseStocks_vw vw on vw.IID = Items.IID AND vw.CompanyID = @company AND vw.WID = @warehouseId
                            left join IComp_M on Items.CompID = IComp_M.CompID
                            where
                                 Items.CompanyID=@company
				AND Items.isDeleted='false'	
				And(Items.IName like '%'+ @Param +'%'
								 OR Article.ArticleNo like '%'+ @Param +'%'
                                 OR Colors.Name like '%'+ @Param +'%'
								 OR Sizes.SizeName like '%'+ @Param +'%'
                                 OR IComp_M.Comp like '%'+ @Param +'%'
								 OR Items.Formula like '%'+ @Param +'%'
                                 OR Items.Cabinet like '%'+ @Param +'%')";


            SqlCommand cmd = new SqlCommand(Query, SqlHelper.DefaultSqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Param", SqlDbType.NVarChar).Value = cmbxItems.Text;
            cmd.Parameters.Add("@company", SqlDbType.Int).Value = compID;
            cmd.Parameters.Add("@warehouseId", SqlDbType.Int).Value = (int)cmbxWareHouse.SelectedValue;
            cmd.Parameters.Add("@Delete", SqlDbType.Bit).Value = false;

            //cmd.Parameters.AddWithValue("@Param", "shahzaib");
            var rows = SqlHelper.ExecuteDataset(cmd).Tables[0];
            var items = rows.ToList<PurchaseItems>();

            if (items.Count > 0)
            {
                dataGridView1.Visible = true;
                panel1.Visible = true;
                dataGridView1.DataSource = items;
                dataGridView1.Columns["IID"].Visible = false;
                dataGridView1.Columns["Product"].Width = 300;
                this.dataGridView1.Height = 300;
                this.dataGridView1.Width = 1000;
                panel1.Height = 300;
                panel1.Width = 1000;
            }
            else
            {
                dataGridView1.Visible = false;
                panel1.Visible = false;
            }
        }

        private void cmbxAccID_SelectedValueChanged(object sender, EventArgs e)
        {
            //cmbxvendor.SelectedValue = cmbxAccID.SelectedValue;

            //if (cmbxvendor.SelectedValue != null)
            //{

            //    int value = Convert.ToInt32(cmbxvendor.SelectedValue);
            //    var vendor = db.COA_D.Where(x => x.CAC_Code == value).ToList();




            //    FillCombo(cmbxvendor, vendor, "AC_Title", "CAC_Code", 0);


            //}

            //else
            //{

            //    cmbxvendor.SelectedValue = cmbxAccID.SelectedValue;

            //    if (cmbxvendor.SelectedValue != null)
            //    {

            //    }
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        class Batch
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }

        private void cmbxvendor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbxvendor_Leave(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(cmbxAccID.SelectedIndex);

            if (value >= 1)
            {
                int account = Convert.ToInt32(cmbxAccID.SelectedValue);






                if (account == 3)
                {




                    var customer = db.Customers.AsNoTracking().Where(x => x.CusName == cmbxvendor.Text && x.CompanyID == compID).FirstOrDefault();
                    if (customer == null)
                    {

                        MessageBox.Show("you selected wrong customer");

                        cmbxvendor.Focus();



                    }


                    else
                    {

                        int Vendorcode = Convert.ToInt32(customer.AC_Code);
                        double balance = 0;

                        var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Now, Vendorcode);
                        int a = 1;

                        double credit = (double)previosBalance.Rows[0]["credit"];
                        double debit = (double)previosBalance.Rows[0]["debit"];
                        balance = debit - credit;
                        if (balance != 0)
                        {


                        }
                        SaleManagerEntities db1 = new SaleManagerEntities();

                        var getdata = ReportsController.getcustomerLedgerSummaryByDate(DateTime.Now, DateTime.Now, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                        foreach (DataRow rows in getdata.Rows)
                        {


                            balance = balance - (double)rows["credit"];
                            balance = balance + (double)rows["debit"];








                        }



                        //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                        // vebdorid= Convert.ToInt32(cmbxvendor.SelectedValue);
                        //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                        //int a = 1;

                        //double credit = (double)previosBalance.Rows[0]["credit"];
                        //double debit = (double)previosBalance.Rows[0]["debit"];
                        //double balance = debit - credit;

                        lblAccountBalance.Text = balance.ToString();


                    }



                }

                if (account == 9)
                {
                    var customer = db.Vendors.AsNoTracking().Where(x => x.VendName == cmbxvendor.Text && x.CompanyID == compID).FirstOrDefault();
                    if (customer == null)
                    {

                        MessageBox.Show("you selected wrong vendor");

                        cmbxvendor.Focus();



                    }


                    else
                    {

                        int Vendorcode = Convert.ToInt32(customer.AC_Code);

                        double balance = 0;

                        var previosBalance = ReportsController.getVendorPreviousBalance(DateTime.Now, Vendorcode);
                        int a = 1;

                        double credit = (double)previosBalance.Rows[0]["credit"];
                        double debit = (double)previosBalance.Rows[0]["debit"];
                        balance = credit - debit;
                        if (balance != 0)
                        {


                        }
                        SaleManagerEntities db1 = new SaleManagerEntities();

                        var getdata = ReportsController.getVendorLedgerSummaryByDate(DateTime.Now, DateTime.Now, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                        foreach (DataRow rows in getdata.Rows)
                        {


                            balance = balance + (double)rows["credit"];
                            balance = balance - (double)rows["debit"];






                        }




                        //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                        //vebdorid = Convert.ToInt32(cmbxvendor.SelectedValue);
                        //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                        //int a = 1;

                        //double credit = (double)previosBalance.Rows[0]["credit"];
                        //double debit = (double)previosBalance.Rows[0]["debit"];
                        //double balance = credit - debit;

                        lblAccountBalance.Text = balance.ToString();

                        vebdorid = Convert.ToInt32(customer.AC_Code);



                    }



                }
                if (account == 1)
                {

                    var customer = db.COA_D.AsNoTracking().Where(x => x.CAC_Code == 1 && x.AC_Title == cmbxvendor.Text && x.CompanyID == compID).FirstOrDefault();

                    if (customer == null)
                    {
                        MessageBox.Show("you selected wrong cash");

                        cmbxvendor.Focus();


                    }

                    else
                    {
                        vebdorid = Convert.ToInt32(customer.AC_Code);


                    }

                }

                if (account == 0)
                {
                    lblGrandTotal.Text = "0";

                }
                else
                {


                    lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text.DefaultZero()) + Convert.ToDouble(txtNetAm.Text.DefaultZero())).ToString();
                }



            }
        }

        private void cmbxvendor_SelectedValueChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(cmbxAccID.SelectedIndex);

            if (value >= 1)
            {
                int account = Convert.ToInt32(cmbxAccID.SelectedValue);






                if (account == 3)
                {

                    double balance = 0;
                    int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Now, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = debit - credit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getcustomerLedgerSummaryByDate(DateTime.Now, DateTime.Now, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                    foreach (DataRow rows in getdata.Rows)
                    {


                        balance = balance - (double)rows["credit"];
                        balance = balance + (double)rows["debit"];








                    }



                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    // vebdorid= Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = debit - credit;

                    lblAccountBalance.Text = balance.ToString();


                }


                if (account == 9)
                {

                    double balance = 0;
                    int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    var previosBalance = ReportsController.getVendorPreviousBalance(DateTime.Now, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = credit - debit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getVendorLedgerSummaryByDate(DateTime.Now, DateTime.Now, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



                    foreach (DataRow rows in getdata.Rows)
                    {


                        balance = balance + (double)rows["credit"];
                        balance = balance - (double)rows["debit"];






                    }




                    //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    //vebdorid = Convert.ToInt32(cmbxvendor.SelectedValue);
                    //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                    //int a = 1;

                    //double credit = (double)previosBalance.Rows[0]["credit"];
                    //double debit = (double)previosBalance.Rows[0]["debit"];
                    //double balance = credit - debit;

                    lblAccountBalance.Text = balance.ToString();

                }

                if (account == 1)
                {
                    lblGrandTotal.Text = "0";
                    vebdorid = Convert.ToInt32(cmbxvendor.SelectedValue);

                }
                if (account == 0)
                {

                    lblGrandTotal.Text = "0";

                }

                else
                {


                    lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text.DefaultZero()) + Convert.ToDouble(txtNetAm.Text.DefaultZero())).ToString();
                }

            }
            calculation();

        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void invGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkUpdateAllSRate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    row.Cells["chkRate"].Value = chkUpdateAllSRate.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbxPackaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculation();
        }
    }

    public class PurchaseItems
    {
        public int IID { get; set; }
        public string Article { get; set; }
        public String Product { get; set; }
        public double Stock { get; set; }
        public int Color { get; set; }
        public int Size { get; set; }
        public String Comp { get; set; }
        public string Formula { get; set; }
        public string Cabinet { get; set; }
    }
}