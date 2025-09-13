using DIagnoseMgmt;
using Lib;
using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using Lib.Utilities;
using Microsoft.Reporting.WinForms;
using SalesMngmt.Configs;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SalesMngmt.Invoice
{
    public partial class PurchaseReturn : Form
    {
        //public SaleReturn()
        //{
        //    InitializeComponent();
        //}

        SaleManagerEntities db = null;
        List<Lib.Entity.Items> listItems = null;
        int compID = 0;
        int itemID = 0;
        List<tblStock> stock = null;
        public PurchaseReturn SInvoice { get; set; }
        public PurchaseReturn(int Company)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = Company;
            SInvoice = this;
            stock = new List<tblStock>();
            listItems = db.Items.Where(x => x.CompanyID == compID && x.isDeleted == false).ToList();
        }

        private int ItemID()
        {
            var itemID = cmbxItems.Text;
            var items = listItems.Where(x => x.IName.ToLower().Trim().Contains(cmbxItems.Text.ToLower().Trim()) && x.CompanyID == compID).FirstOrDefault();
            if (items != null)
            {
                return items.IID;
            }
            return 0;
        }

        private int ItemID(int ItemID)
        {
            var itemID = cmbxItems.Text;
            var items = listItems.Where(x => x.IID == ItemID).FirstOrDefault();
            if (items != null)
            {
                return items.IID;
            }
            return 0;
        }

        public void loadfuntion() {
            cmbxAccID.Enabled = false;
            cmbxvendor.Enabled = false;
            cmbxSaleMan.Enabled = false;
          //  cmbxWareHouse.Enabled = false;

            //account headr
            List<COA_M> article = new List<COA_M>();
            article.Add(new COA_M { CAC_Code = 0, CATitle = "--SELECT--" });
            article.AddRange(db.COA_M.Where(x => x.CAC_Code == 1 || x.CAC_Code == 3 || x.CAC_Code == 9).ToList());

            FillCombo(cmbxAccID, article, "CATitle", "CAC_Code", 0);
            List<COA_M> saleRetail = new List<COA_M>();
            saleRetail.Add(new COA_M { CAC_Code = 1, CATitle = "Retail Price" });
            saleRetail.Add(new COA_M { CAC_Code = 2, CATitle = "WholeSale Price" });
            FillCombo(ddlWSR, saleRetail, "CATitle", "CAC_Code", 0);

            //parties
            List<COA_D> parties = new List<COA_D>();
            parties.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
            parties.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList());
            //   var vendor = ;
            FillCombo(cmbxvendor, parties, "AC_Title", "AC_Code", 0);

            ////account headr
            //article.Add(new Article { ProductID = 0, ArticleNo = "--SELECT--" });
            //article.AddRange(db.Articles.ToList());
            //var Account = db.COA_M.Where(x => x.CAC_Code == 1 || x.CAC_Code == 3 || x.CAC_Code == 9).ToList();
            //FillCombo(cmbxAccID, Account, "CATitle", "CAC_Code", 0);

            //payment mode
            List<COA_D> cash = new List<COA_D>();
            cash.Add(new COA_D { AC_Code = 0, AC_Title = "--Credit--" });
            cash.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList());
            FillCombo(cmbxPaymentMode, cash, "AC_Title", "AC_Code", 0);

            List<Lib.Entity.Items> Items = new List<Lib.Entity.Items>();

            Items.Add(new Lib.Entity.Items { IID = 0, IName = "--SELECT--" });
            Items.AddRange(db.Items.Where(x => x.CompanyID == compID && x.isDeleted == false).ToList());

            List<tbl_Warehouse> Warehouse = new List<tbl_Warehouse>();
            Warehouse.Add(new tbl_Warehouse { WID = 0, Warehouse = "--Select--" });
            Warehouse.AddRange(db.tbl_Warehouse.Where(x => x.CompanyID == compID && x.isDelete == false).ToList());
            FillCombo(cmbxWareHouse, Warehouse, "Warehouse", "WID", 0);

            List<tbl_Employee> Employe = new List<tbl_Employee>();
            Employe.Add(new tbl_Employee { ID = 0, EmployeName = "--Select--" });
            Employe.AddRange(db.tbl_Employee.Where(x => x.companyID == compID && x.isDeleted == false).ToList());
            FillCombo(cmbxSaleMan, Employe, "EmployeName", "ID", 0);


            lblRID.Text = "0";
            radioButton1.Checked = true;
        }
        private void SInv_Load(object sender, EventArgs e)
        {
            loadfuntion();
        }

        private void metroTextBox1_Leave(object sender, EventArgs e)
        {
            var dsa = txtCode.Text;
            if (dsa != "")
            {
                var items = listItems.Where(x => x.BarcodeNo == dsa).FirstOrDefault();
                if (items != null)
                {
                    txtRate.Text = items.SalesPrice.ToString();
                }
            }

            //else
            //{
            //    MessageBox.Show("Invalid Barcode");
            //    txtCode.Focus();
            //}
        }

        private void metroPanel4_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

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

        private void cmbxItems_Leave(object sender, EventArgs e)
        {

        }

        //private void AddIntoGrid()
        //{
        //    var isAdd = true;

        //    var iid = Convert.ToInt32(cmbxItems.SelectedValue);
        //    var item = db.Items.Where(x => x.IID == iid).FirstOrDefault();
        //    if (item != null)
        //    {
        //        var totalCTn = item.CTN_PCK * Convert.ToInt32(txtctn.Text.DefaultZero());
        //        var TotalPcs = totalCTn + Convert.ToInt32(txtpcs.Text);
        //        foreach (DataGridViewRow row in invGrid.Rows)
        //        {
        //            if (row.Cells[0].Value != null)
        //            {
        //                var itemID = Convert.ToInt32(row.Cells[0].Value.DefaultZero());
        //                if (Convert.ToInt32(cmbxItems.SelectedValue) == itemID)
        //                {
        //                    var PcsRate = Convert.ToDouble(txtRate.Text.DefaultZero()) * TotalPcs;
        //                    var NetAmount = PcsRate - Convert.ToDouble(txtDisc.Text.DefaultZero());
        //                    NetAmount = NetAmount * Convert.ToDouble(txtDisPer.Text.DefaultZero() == "0" ? "1" : "0." + txtDisPer.Text);
        //                    row.Cells[2].Value = txtctn.Text.DefaultZero();
        //                    row.Cells[3].Value = Convert.ToInt32(row.Cells[3].Value) + Convert.ToInt32(txtpcs.Text.DefaultZero());
        //                    row.Cells[4].Value = txtRate.Text.DefaultZero();
        //                    row.Cells[5].Value = String.Format("{0:0.00}", PcsRate).DefaultZero();
        //                    row.Cells[6].Value = txtDisPer.Text.DefaultZero();
        //                    row.Cells[7].Value = txtDisc.Text.DefaultZero();
        //                    row.Cells[8].Value = String.Format("{0:0.00}", NetAmount).DefaultZero();
        //                    row.Cells[9].Value = txtSaleP.Text.DefaultZero();
        //                    row.Cells["SaleRate"].Value = txtSaleRate.Text.DefaultZero();
        //                    isAdd = false;
        //                }
        //            }
        //        }
        //        if (isAdd == true)
        //        {
        //            this.invGrid.Rows.Add(cmbxItems.SelectedValue,
        //                cmbxItems.Text,
        //                txtctn.Text.DefaultZero(),
        //                txtpcs.Text.DefaultZero(),
        //                txtRate.Text.DefaultZero(),
        //                String.Format("{0:0.00}", PcsRate).DefaultZero(),
        //                txtDisPer.Text.DefaultZero(), txtDisc.Text.DefaultZero(),
        //                String.Format("{0:0.00}", NetAmount).DefaultZero(),
        //                txtSaleP.Text.DefaultZero(),
        //                txtSaleRate.Text.DefaultZero());
        //        }

        //        invGrid_SelectionChanged(null, null);
        //        clear(true);
        //    }
        //}
        //Replace into Process Cmd
        private void metroButton1_Click(object sender, EventArgs e)
        {
            int itemValue = itemID;
            var isAdd = true;
            if (txtCode.Text != "")
            {
                var items = db.Items.AsNoTracking().Where(x => x.BarcodeNo == txtCode.Text && x.CompanyID == compID).FirstOrDefault();
                itemValue = Convert.ToInt32(items.IID.DefaultZero());
            }

            if (ItemID(itemValue) == 0)
            {
                MessageBox.Show("Item is Required", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbxItems.Focus();
                return;
            }
            if (txtpcs.Text == "")
            {
                MessageBox.Show("Pieces is Required", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbxItems.Focus();
                return;
            }
            var iid = ItemID(itemValue);
            var item = db.Items.Where(x => x.IID == iid).FirstOrDefault();
            if (item != null)
            {
                // var totalCTn = item.CTN_PCK * Convert.ToInt32(txtctn.Text.DefaultZero());
                //var TotalPcs = totalCTn + Convert.ToInt32(txtpcs.Text); // -->
                //var PcsRate = Convert.ToDouble(txtRate.Text.DefaultZero()) * TotalPcs;
                //var NetAmount = PcsRate - Convert.ToDouble(txtDisc.Text.DefaultZero());
                //   NetAmount = NetAmount * Convert.ToDouble(txtDisPer.Text.DefaultZero() == "0" ? "1" : "0." + txtDisPer.Text);

                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        var itemID = Convert.ToInt32(row.Cells[0].Value.DefaultZero());
                        var stockid = db.getStockByID(itemID).FirstOrDefault();
                        if (ItemID(itemValue) == itemID)
                        {
                            row.Cells[2].Value = txtctn.Text.DefaultZero();
                            if (txtCode.Text != "")
                            {
                                double value = Convert.ToDouble(txtpcs.Text);
                                double ctn;
                                if (Convert.ToDouble(item.CTN_PCK.DefaultZero()) == 0)
                                {
                                    ctn = (Convert.ToDouble(txtctn.Text.DefaultZero()));
                                }
                                else
                                {
                                    ctn = (Convert.ToDouble(txtctn.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));
                                }
                                //  ctn = (Convert.ToDouble(txtctn.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));
                                value += ctn;
                                calculation();
                                //if (stockid >= value)
                                //{


                                //}
                                if (item.Inv_YN == true)
                                {
                                    row.Cells[0].Value = ItemID(itemValue);
                                    row.Cells[1].Value = cmbxItems.Text;
                                    row.Cells[2].Value = txtctn.Text.DefaultZero();
                                    row.Cells[3].Value = txtpcs.Text.DefaultZero();
                                    row.Cells[4].Value = txtRate.Text.DefaultZero();
                                    row.Cells[5].Value = txtSaleP.Text.DefaultZero();
                                    row.Cells[6].Value = txtDisPer.Text.DefaultZero();
                                    row.Cells[7].Value = txtDisc.Text.DefaultZero();
                                    row.Cells[8].Value = txtNet.Text.DefaultZero();
                                    row.Cells[9].Value = txtPcsRate.Text.DefaultZero();
                                    row.Cells[10].Value = txtSaleRate.Text.DefaultZero();
                                }
                                else
                                {
                                    row.Cells[0].Value = ItemID(itemValue);
                                    row.Cells[1].Value = cmbxItems.Text;
                                    row.Cells[2].Value = txtctn.Text.DefaultZero();
                                    row.Cells[3].Value = txtpcs.Text.DefaultZero();
                                    row.Cells[4].Value = txtRate.Text.DefaultZero();
                                    row.Cells[5].Value = txtSaleP.Text.DefaultZero();
                                    row.Cells[6].Value = txtDisPer.Text.DefaultZero();
                                    row.Cells[7].Value = txtDisc.Text.DefaultZero();
                                    row.Cells[8].Value = txtNet.Text.DefaultZero();
                                    row.Cells[9].Value = txtPcsRate.Text.DefaultZero();
                                    row.Cells[10].Value = txtSaleRate.Text.DefaultZero();
                                }
                                txtpcs.Text = row.Cells[3].Value.ToString();
                            }
                            else
                            {
                                calculation();
                                double value = Convert.ToDouble(txtpcs.Text);
                                double ctn = (Convert.ToDouble(txtctn.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));
                                value += ctn;
                                //if (stockid >= value)
                                //{


                                // }
                                if (item.Inv_YN == true)
                                {
                                    row.Cells[0].Value = ItemID(itemValue);
                                    row.Cells[1].Value = cmbxItems.Text;
                                    row.Cells[2].Value = txtctn.Text.DefaultZero();
                                    row.Cells[3].Value = txtpcs.Text.DefaultZero();
                                    row.Cells[4].Value = txtRate.Text.DefaultZero();
                                    row.Cells[5].Value = txtSaleP.Text.DefaultZero();
                                    row.Cells[6].Value = txtDisPer.Text.DefaultZero();
                                    row.Cells[7].Value = txtDisc.Text.DefaultZero();
                                    row.Cells[8].Value = txtNet.Text.DefaultZero();
                                    row.Cells[9].Value = txtPcsRate.Text.DefaultZero();
                                    row.Cells[10].Value = txtSaleRate.Text.DefaultZero();

                                }
                                else
                                {
                                    row.Cells[0].Value = ItemID(itemValue);
                                    row.Cells[1].Value = cmbxItems.Text;
                                    row.Cells[2].Value = txtctn.Text.DefaultZero();
                                    row.Cells[3].Value = txtpcs.Text.DefaultZero();
                                    row.Cells[4].Value = txtRate.Text.DefaultZero();
                                    row.Cells[5].Value = txtSaleP.Text.DefaultZero();
                                    row.Cells[6].Value = txtDisPer.Text.DefaultZero();
                                    row.Cells[7].Value = txtDisc.Text.DefaultZero();
                                    row.Cells[8].Value = txtNet.Text.DefaultZero();
                                    row.Cells[9].Value = txtPcsRate.Text.DefaultZero();
                                    row.Cells[10].Value = txtSaleRate.Text.DefaultZero();
                                }
                            }

                            //      row.Cells[4].Value = txtRate.Text.DefaultZero();

                            //      row.Cells[6].Value = txtDisPer.Text.DefaultZero();
                            //      row.Cells[7].Value = txtDisc.Text.DefaultZero();
                            ////-->
                            //      row.Cells[9].Value = txtSaleP.Text.DefaultZero();
                            //      row.Cells["SaleRate"].Value = txtSaleRate.Text.DefaultZero();
                            isAdd = false;
                        }
                    }
                }
                if (isAdd == true)
                {
                    calculation();

                    var stockid = db.getStockByID(item.IID).FirstOrDefault();
                    double value = Convert.ToDouble(txtpcs.Text);
                    double ctn = 0;
                    //  double ctn = (Convert.ToDouble(txtctn.Text.DefaultZero() == "0" ? "1" : txtctn.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));
                    if (Convert.ToDouble(item.CTN_PCK.DefaultZero()) == 0)
                    {
                        ctn = (Convert.ToDouble(txtctn.Text.DefaultZero()));
                    }
                    else
                    {
                        ctn = (Convert.ToDouble(txtctn.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));
                    }

                    value += ctn;
                    //if (stockid >= value)
                    //{

                    //}
                    if (item.Inv_YN == true)
                    {

                        this.invGrid.Rows.Add(ItemID(itemValue),
                        cmbxItems.Text,
                        txtctn.Text.DefaultZero(),
                        txtpcs.Text.DefaultZero(),
                        txtRate.Text.DefaultZero(),
                        txtSaleP.Text.DefaultZero(),
                        txtDisPer.Text.DefaultZero(),
                        txtDisc.Text.DefaultZero(),
                        txtNet.Text.DefaultZero(),
                        txtPcsRate.Text.DefaultZero(),
                        txtSaleRate.Text.DefaultZero(),
                        "Remove",
                       "0");

                        invGrid_SelectionChanged(null, null);
                        clear(true);

                    }
                    else
                    {

                        this.invGrid.Rows.Add(ItemID(itemValue),
                      cmbxItems.Text,
                      txtctn.Text.DefaultZero(),
                      txtpcs.Text.DefaultZero(),
                      txtRate.Text.DefaultZero(),
                      txtSaleP.Text.DefaultZero(),
                      txtDisPer.Text.DefaultZero(),
                      txtDisc.Text.DefaultZero(),
                      txtNet.Text.DefaultZero(),
                      txtPcsRate.Text.DefaultZero(),
                      txtSaleRate.Text.DefaultZero(),
                      "Remove", "0");


                        invGrid_SelectionChanged(null, null);
                        clear(true);
                        //MessageBox.Show("stock can,t be more than that " + stockid);
                        //return;
                    }
                }

                invGrid_SelectionChanged(null, null);
                calculation();
                txtCode.Focus();
            }
        }

        private void invGrid_SelectionChanged(object sender, EventArgs e)
        {
            Decimal totalAmount = 0;
            foreach (DataGridViewRow row in invGrid.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    lblItemID.Text = row.Cells[0].Value.DefaultZero();
                    txtctn.Text = row.Cells[2].Value.ToString();
                    txtpcs.Text = row.Cells[3].Value.ToString();
                    txtRate.Text = row.Cells[4].Value.ToString();
                    txtDisPer.Text = (row.Cells[6].Value ?? "0").ToString();
                    txtDisc.Text = (row.Cells[7].Value ?? "0").ToString();
                    txtSaleP.Text = (row.Cells[9].Value ?? "0").ToString();
                    totalAmount += Convert.ToDecimal(row.Cells["netAm"].Value.DefaultZero());
                    txtNet.Text = totalAmount.ToString();
                }
            }
            txtDisfooter.Text = txtDisfooter.Text.DefaultZero();
            txtTotalAm.Text = totalAmount.ToString();
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
            //txtInv.Enabled = false;
        }

        public void EditInv(string invNo)
        {
            clear();
            var Master = db.Sales_M.Where(x => x.InvNo == invNo).FirstOrDefault();
            if (Master != null)
            {
                lblRID.Text = Master.RID.ToString();
                txtNetAm.Text = Master.NetAmt.ToString();
                txtDisfooter.Text = Master.DisAmt.ToString();
                txtTotalAm.Text = Master.TotalAmount.ToString();
                txtRemarks.Text = Master.Rem.ToString();
                cmbxAccID.SelectedValue = Convert.ToInt32(Master.AC_Code);
                cmbxvendor.SelectedValue = Convert.ToInt32(Master.AC_Code);
                txtbilty.Text = Master.BiltyNo;
                txtInv.Text = Master.CstInvNo;
                txtInvDate.Text = Master.VenInvDate;

                var Detail = db.Sales_D.Where(x => x.RID == Master.RID).ToList();
                foreach (var item in Detail)
                {
                    var itemName = db.Items.Where(x => x.IID == item.IID).FirstOrDefault();
                    if (itemName != null)
                    {

                        var TotalPcs = (item.Qty * item.SalesPriceP);
                        var PcsRate = Convert.ToDouble(item.SalesPriceP) * TotalPcs;
                        var NetAmount = PcsRate - Convert.ToDouble(item.DisRs);
                        txtDisPer.Text = txtDisPer.Text ?? "0";
                        // NetAmount = NetAmount * Convert.ToDouble(item.DisP == "0" ? "1" : "0." + item.DisP);



                        this.invGrid.Rows.Add(item.IID, itemName.IName, item.CTN, item.PCS, item.SalesPriceP, TotalPcs, item.DisP, item.DisRs, item.Amt, "", "", "Remove", itemName.ArticleNoID);
                        invGrid_SelectionChanged(null, null);
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
                    if (e.ColumnIndex == 11)
                    {
                        invGrid.Rows.RemoveAt(e.RowIndex);
                        invGrid_SelectionChanged(null, null);
                    }
                    else
                    {
                        var ItemID = Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells[0].Value);
                        var items = db.Items.Where(x => x.IID == ItemID).FirstOrDefault();
                        items.SalesPrice = Convert.ToDouble(invGrid.Rows[e.RowIndex].Cells[9].Value.DefaultZero());
                        db.Entry(items).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Item Sale Price Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
            txtNetAm.Text = (Convert.ToDouble(txtTotalAm.Text.DefaultZero()) + Convert.ToDouble(txtTransportExpense.Text.DefaultZero()) - Convert.ToDouble(txtDisfooter.Text.DefaultZero())).ToString();

        }

        private void txtCredit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
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
            if (e != null && e.RowIndex > -1)
            {
                lblItemID.Text = invGrid.Rows[e.RowIndex].Cells[0].Value.DefaultZero();
                cmbxItems.Text = invGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtctn.Text = invGrid.Rows[e.RowIndex].Cells[2].Value.DefaultZero();
                txtpcs.Text = invGrid.Rows[e.RowIndex].Cells[3].Value.DefaultZero();
                txtRate.Text = invGrid.Rows[e.RowIndex].Cells[4].Value.DefaultZero();
                txtDisPer.Text = invGrid.Rows[e.RowIndex].Cells[6].Value.DefaultZero();
                txtDisc.Text = invGrid.Rows[e.RowIndex].Cells[7].Value.DefaultZero();
                txtNet.Text = invGrid.Rows[e.RowIndex].Cells[8].Value.DefaultZero();
                txtSaleP.Text = invGrid.Rows[e.RowIndex].Cells[9].Value.DefaultZero();
                txtRate_Leave(null, null);
                txtSaleP_Leave(null, null);

                ItemRecord(Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells[0].Value));
            }
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

            cmbxItems.Text = "";
            txtCode.Focus();
            if (grid == false)
            {
                //var vendorAcode = db.Customers.Where(x => x.CompanyID == compID).FirstOrDefault();
                //cmbxvendor.SelectedIndex = vendorAcode.AC_Code;
                //cmbxAccID.SelectedIndex = vendorAcode.AC_Code;
                //invGrid.DataSource = null;
                invGrid.Rows.Clear();
                txtNetAm.Text = "0";
                txtSaleP.Text = "0";
                txtTotalAm.Text = "0";
                txtDisfooter.Text = "0";
                lblRID.Text = "0";
                lblInvN.Visible = false;
                lblInvHeader.Visible = false;
                txtInv.Focus();
            }
            txtDisc.Text = "0";
            txtDisPer.Text = "0";
            txtctn.Text = "0";
            txtpcs.Text = "0";
            txtbilty.Text = "0";
            txtRate.Text = "0";
            txtNet.Text = "0";
            txtPcsRate.Text = "0";
            txtSaleRate.Text = "0";
            dataGridView1.Visible = false;
            panel1.Visible = false;
            txtSaleP.Text = "0";
        }

        private async void metroButton3_Click(object sender, EventArgs e)
        {
            await SaveRecordAsync(lblRID.Text);
        }

        private async void metroButton4_Click(object sender, EventArgs e)
        {
           await SaveRecordAsync(lblRID.Text);
        }

        private void SInv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Modifiers == Keys.Alt)
            {
                clear();
            }
        }

        

        private async Task SaveRecordAsync(string invoiceNo)
        {
            if (!ValidateInvoice())
                return;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var sale = await SavePurchaseReturnMasterAsync(invoiceNo);

                    if (invoiceNo != "0")
                        await RemoveOldRecordsAsync(sale.RID);

                    foreach (DataGridViewRow row in invGrid.Rows)
                    {
                        if (row.Cells[0].Value == null) continue;
                        await SavePurchaseReturnDetailAsync(sale, row);
                    }

                    if (ToDouble(txtDisfooter.Text) != 0)
                        await SaveDiscountGLAsync(sale);

                    await db.SaveChangesAsync();
                    transaction.Commit();

                    MessageBox.Show("Invoice Save Successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ---------------- Validation ----------------
        private bool ValidateInvoice()
        {
            Accountvalidation();

            if (invGrid.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Items In Grid", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbxvendor.SelectedValue == null || ToInt(cmbxvendor.SelectedValue) == 0)
            {
                MessageBox.Show("Please select AccountName", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbxvendor.Focus();
                return false;
            }

            if (cmbxWareHouse.SelectedValue == null || ToInt(cmbxWareHouse.SelectedValue) == 0)
            {
                MessageBox.Show("Please select WareHouse", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        // ---------------- Master Save ----------------
        private async Task<PurR_M> SavePurchaseReturnMasterAsync(string invoiceNo)
        {
            PurR_M sale;

            if (invoiceNo == "0")
            {
                sale = new PurR_M
                {
                    invNo = txtInvoice.Text,
                    EDate = DateTime.Now
                };
                db.PurR_M.Add(sale);
            }
            else
            {
                int rid;
                if (int.TryParse(invoiceNo, out rid))
                {
                    sale = await db.PurR_M.FirstAsync(x => x.RID == rid);
                    sale.EDate = DateTime.Now;
                    db.Entry(sale).State = EntityState.Modified;
                }
                else
                {
                    sale = await db.PurR_M.FirstOrDefaultAsync(x => x.invNo == invoiceNo)
                           ?? new PurR_M { invNo = invoiceNo, EDate = DateTime.Now };
                    if (sale.RID == 0) db.PurR_M.Add(sale);
                }
            }

            sale.AC_Code = ToInt(cmbxvendor.SelectedValue);
            sale.WID = ToInt(cmbxWareHouse.SelectedValue);
            sale.NetAmt = ToDouble(txtTotalAm.Text);
            sale.BiltyNo = txtbilty.Text;
            sale.Rem = txtRemarks.Text;
            sale.CompID = compID;

            return sale;
        }

        // ---------------- Remove Old ----------------
        private async Task RemoveOldRecordsAsync(int rid)
        {
            db.Sales_D.RemoveRange(db.Sales_D.Where(x => x.RID == rid));
            db.Itemledger.RemoveRange(db.Itemledger.Where(x => x.RID == rid));
            db.GL.RemoveRange(db.GL.Where(x => x.RID == rid));
            await Task.CompletedTask;
        }

        // ---------------- Save Detail ----------------
        private async Task SavePurchaseReturnDetailAsync(PurR_M sale, DataGridViewRow row)
        {
            int id = ToInt(row.Cells[0].Value);

            var saleM = await db.Pur_M.AsNoTracking()
                                      .FirstOrDefaultAsync(x => x.InvNo == txtInvoice.Text);

            Pur_D itemFromPur = null;
            if (saleM != null)
                itemFromPur = await db.Pur_D.FirstOrDefaultAsync(x => x.IID == id && x.RID == saleM.RID);

            var item1 = await db.Items.AsNoTracking().FirstAsync(x => x.IID == id);

            double ctn = (ToInt(item1.CTN_PCK) == 0)
                ? ToDouble(row.Cells[2].Value)
                : ToInt(item1.CTN_PCK) * ToDouble(row.Cells[2].Value);

            var detail = new PurR_D
            {
                RID = sale.RID,
                IID = id,
                CTN = ToDouble(row.Cells[2].Value),
                PCS = ToDouble(row.Cells[3].Value),
                PurPrice = itemFromPur != null ? ToDouble(itemFromPur.PurPrice) : ToDouble(row.Cells[4].Value),
                DisP = ToDouble(row.Cells[6].Value),
                DisRs = ToDouble(row.Cells[7].Value),
                Qty = ctn + ToDouble(row.Cells[3].Value),
                DisAmt = ToDouble(row.Cells[6].Value) / 100.0 * ToDouble(row.Cells[5].Value),
                Amt = ToDouble(row.Cells[8].Value)
            };

            db.PurR_D.Add(detail);

            var checkstock = await db.Items.FirstOrDefaultAsync(x => x.IID == id);
            if (checkstock != null && checkstock.Inv_YN == false)
            {
                var ledger = new Itemledger
                {
                    RID = sale.RID,
                    IID = id,
                    EDate = DateTime.Now,
                    TypeCode = 3,
                    AC_CODE = ToInt(cmbxvendor.SelectedValue),
                    WID = ToInt(cmbxWareHouse.SelectedValue),
                    SID = ToInt(cmbxSaleMan.SelectedValue),
                    CTN = ToDouble(row.Cells[2].Value),
                    PCS = ToDouble(row.Cells[3].Value),
                    PRJ = ctn + ToDouble(row.Cells[3].Value),
                    PurPrice = ToDouble(itemFromPur?.PurPrice ?? item1.PurPrice),
                    SalesPriceP = ToDouble(row.Cells[4].Value),
                    DisP = ToDouble(row.Cells[6].Value),
                    DisRs = ToDouble(row.Cells[7].Value),
                    Amt = ToDouble(row.Cells[8].Value),
                    DisAmt = ToDouble(row.Cells[6].Value) / 100.0 * ToDouble(row.Cells[5].Value),
                    CompanyID = compID
                };
                db.Itemledger.Add(ledger);

                CreateGLsForRow(sale, row, item1, ctn);
            }
            else
            {
                CreateGLsForRow(sale, row, item1, ctn);
            }
        }

        // ---------------- GL Entries ----------------
        private void CreateGLsForRow(PurR_M sale, DataGridViewRow row, Lib.Entity.Items item1, double ctn)
        {
            double qty = ctn + ToDouble(row.Cells[3].Value);

            var glDebit = new GL
            {
                RID = sale.RID,
                TypeCode = 3,
                SID = ToInt(cmbxSaleMan.SelectedValue),
                GLDate = DateTime.Now,
                IPrice = ToDouble(row.Cells[4].Value),
                AC_Code = ToInt(cmbxvendor.SelectedValue),
                AC_Code2 = item1.AC_Code_Inv,
                Narration = row.Cells[1].Value?.ToString(),
                Qty_Out = qty,
                PAmt = ToDouble(row.Cells[5].Value),
                DisP = ToDouble(row.Cells[6].Value),
                DisRs = ToDouble(row.Cells[7].Value),
                DisAmt = ToDouble(row.Cells[6].Value) / 100.0 * ToDouble(row.Cells[5].Value),
                Debit = ToDouble(row.Cells[8].Value),
                Credit = 0,
                CompID = compID
            };
            db.GL.Add(glDebit);

            var purchasePrice = ToDouble(item1.PurPrice);
            var creditAmt = purchasePrice * qty;

            var glCredit = new GL
            {
                RID = sale.RID,
                TypeCode = 3,
                SID = ToInt(cmbxSaleMan.SelectedValue),
                GLDate = DateTime.Now,
                IPrice = purchasePrice,
                AC_Code = item1.AC_Code_Inv,
                AC_Code2 = ToInt(cmbxvendor.SelectedValue),
                Narration = cmbxvendor.Text,
                Qty_Out = qty,
                PAmt = creditAmt,
                Debit = 0,
                Credit = creditAmt,
                CompID = compID
            };
            db.GL.Add(glCredit);
        }

        // ---------------- Discount GL ----------------
        private async Task SaveDiscountGLAsync(PurR_M sale)
        {
            double discount = ToDouble(txtDisfooter.Text);

            var expense = await db.COA_D
                .FirstOrDefaultAsync(x => x.AC_Title == "Total Sale Discount Expense"
                                       && x.CompanyID == compID && x.CAC_Code == 16);

            if (expense == null)
            {
                expense = new COA_D
                {
                    CAC_Code = 16,
                    PType_ID = 1,
                    ZID = 0,
                    AC_Code = ToInt(db.GetAc_Code(16).FirstOrDefault()),
                    AC_Title = "Total Sale Discount Expense",
                    DR = 0,
                    CR = 0,
                    Qty = 0,
                    CompanyID = compID,
                    InActive = false
                };
                db.COA_D.Add(expense);
                await db.SaveChangesAsync();
            }

            var customerDetail = await db.COA_D.FirstOrDefaultAsync(x => x.AC_Code == sale.AC_Code);

            var glDebit = new GL
            {
                RID = sale.RID,
                SID = ToInt(cmbxSaleMan.SelectedValue),
                TypeCode = 3,
                GLDate = DateTime.Now,
                AC_Code = expense.AC_Code,
                AC_Code2 = customerDetail.AC_Code,
                Narration = customerDetail?.AC_Title ?? "Sale Discount",
                Debit = discount,
                Credit = 0,
                CompID = compID
            };
            db.GL.Add(glDebit);

            var glCredit = new GL
            {
                RID = sale.RID,
                TypeCode = 3,
                SID = ToInt(cmbxSaleMan.SelectedValue),
                GLDate = DateTime.Now,
                AC_Code = customerDetail.AC_Code,
                AC_Code2 = expense.AC_Code,
                Narration = expense.AC_Title,
                Debit = 0,
                Credit = discount,
                CompID = compID
            };
            db.GL.Add(glCredit);
        }

        // ---------------- Helpers ----------------
        private int ToInt(object val)
        {
            if (val == null) return 0;
            int r;
            return int.TryParse(val.ToString(), out r) ? r : 0;
        }

        private double ToDouble(object val)
        {
            if (val == null) return 0.0;
            double r;
            return double.TryParse(val.ToString(), out r) ? r : 0.0;
        }

        private void UpdateItemRate()
        {
            //foreach (DataGridViewRow row in invGrid.Rows)
            //{
            //    var check = Convert.ToBoolean(row.Cells["chkRate"].Value);
            //    if (check)
            //    {
            //        var ItemID = Convert.ToInt32(row.Cells[0].Value);
            //        var items = db.Items.Where(x => x.IID == ItemID).FirstOrDefault();
            //        items.SalesPrice = Convert.ToDouble(row.Cells["SaleRate"].Value.DefaultZero());
            //        db.Entry(items).State = EntityState.Modified;
            //        db.SaveChanges();
            //    }
            //}
        }

        private void cmbxAccID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsa = Convert.ToInt32(cmbxAccID.SelectedIndex);
            if (dsa >= 1)
            {
                int value = Convert.ToInt32(cmbxAccID.SelectedValue);
                var vendor = db.COA_D.AsNoTracking().Where(x => x.CAC_Code == value && x.CompanyID == compID && x.InActive == false).ToList();
                FillCombo(cmbxvendor, vendor, "AC_Title", "AC_Code", 0);
            }
            else if (dsa == 0)
            {

                //   int value = Convert.ToInt32(cmbxAccID.SelectedValue);
                var vendor = db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList();
                FillCombo(cmbxvendor, vendor, "AC_Title", "CAC_Code", 0);

            }


            //if (cmbxAccID.SelectedValue == null)
            //{

            //}
            //else {
            //    cmbxvendor.SelectedValue = cmbxAccID.SelectedValue ?? 0;
            //}
        }

        //MetroFramework.Controls.MetroTextBox lastFocused;
        //private void cmbxItems_Enter(object sender, EventArgs e)
        //{
        //    lastFocused = sender as MetroFramework.Controls.MetroTextBox;
        //}

        private BarCodeListener ScannerListener;
        bool isGrid = false;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.G))
            {
                isGrid = true;
                return true;
            }
            if (keyData == Keys.Up && isGrid)
            {
                moveUp();
                return true;
            }
            if (keyData == Keys.Down && isGrid)
            {
                moveDown();
                return true;
            }

            if (keyData == (Keys.Control | Keys.W))
            {
                txtctn.Focus();
                isGrid = false;
                return true;
            }
            if (keyData == (Keys.Control | Keys.I))
            {
                cmbxItems.Focus();
                isGrid = false;
                return true;
            }
            if (keyData == (Keys.Control | Keys.T))
            {
                isGrid = false;
                return true;
            }
            else if (keyData == Keys.Delete && invGrid.SelectedRows.Count > 0)
            {
                invGrid.Rows.RemoveAt(invGrid.SelectedRows[0].Index);
                invGrid_SelectionChanged(null, null);
                if (invGrid.Rows.Count > 0)
                    invGrid.Rows[0].Selected = true;

                isGrid = false;
                return true;
            }
            else if (keyData == (Keys.Control | Keys.S))
            {
                _ = SaveRecordAsync(lblRID.Text); // fire-and-forget
                isGrid = false;
                return true;
            }
            else if (keyData == (Keys.Control | Keys.P))
            {
                _ = SaveRecordAsync(lblRID.Text); // fire-and-forget
                isGrid = false;
                return true;
            }
            else if (keyData == (Keys.Control | Keys.B))
            {
                txtCode.Focus();
                isGrid = false;
                return true;
            }
            else if (keyData == (Keys.Control | Keys.E))
            {
                metroButton5_Click(null, null);
                isGrid = false;
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Q))
            {
                if (invGrid.Rows.Count > 0 && invGrid.SelectedRows.Count > 0)
                {
                    var itemID = invGrid.SelectedRows[0].Cells["itemID"].Value;
                    var pcs = invGrid.SelectedRows[0].Cells["Pcs"].Value.DefaultZero();
                    lblItemID.Text = itemID?.ToString();
                    txtpcs.Text = pcs;
                    txtpcs.Focus();
                }
                isGrid = false;
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                clear();
                isGrid = false;
                return true;
            }
            else if (keyData == Keys.Enter && txtCode.Text == "")
            {
                if (dataGridView1.Visible && dataGridView1.SelectedRows.Count > 0)
                {
                    var id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    lblItemID.Text = id.ToString();
                    dataGridView1.Visible = false;

                    var items = db.Items.FirstOrDefault(x => x.IID == id);
                    if (items != null)
                    {
                        Items_Leave(items);
                        isGrid = false;
                    }
                }
                else if (!string.IsNullOrEmpty(lblItemID.Text))
                {
                    int id = Convert.ToInt32(lblItemID.Text);
                    var items = db.Items.FirstOrDefault(x => x.IID == id);
                    if (items != null)
                    {
                        txtpcs.Text = txtpcs.Text.DefaultZero();
                        txtRate.Text = txtRate.Text.DefaultZero();
                        lblItemID.Text = items.IID.ToString();
                        calculation();
                        metroButton1_Click(null, null);
                        cmbxItems.Focus();
                        isGrid = false;
                    }
                }
                return true;
            }
            else if (keyData == Keys.Enter && !string.IsNullOrEmpty(txtCode.Text))
            {
                var id = txtCode.Text;
                var items = db.Items.FirstOrDefault(x => x.BarcodeNo == id);
                if (items != null)
                {
                    txtpcs.Text = txtpcs.Text.DefaultZero() == "0" ? "1" : txtpcs.Text;
                    txtRate.Text = txtRate.Text.DefaultZero();
                    lblItemID.Text = items.IID.ToString();
                    calculation();
                    metroButton1_Click(null, null);
                    txtCode.Focus();
                    isGrid = false;
                }
                return true;
            }

            // Pass keys to ScannerListener if available
            bool res = false;
            if (ScannerListener != null)
                res = ScannerListener.ProcessCmdKey(ref msg, keyData);

            res = keyData == Keys.Enter ? res : base.ProcessCmdKey(ref msg, keyData);
            return res;
        }


        private void metroButton5_Click(object sender, EventArgs e)
        {
            //EditMessageBox messageBox = new EditMessageBox(SInvoice, compID, "SI");
            //messageBox.Show();
        }

        private void txtRate_Leave(object sender, EventArgs e)
        {
            calculation();
            //var itemID = Convert.ToInt32(cmbxItems.SelectedValue);
            //var item = db.Items.Where(x => x.IID == itemID).FirstOrDefault();
            //if (item != null)
            //{
            //    var TotalPcs = (item.CTN_PCK ?? 0 * Convert.ToInt32(txtctn.Text.DefaultZero())) + Convert.ToInt32(txtpcs.Text.DefaultZero());
            //    txtRate.Text = txtRate.Text.DefaultZero() == "0" ? item.SalesPrice.ToString() : txtRate.Text;
            //    var PcsRate = Convert.ToDouble(txtRate.Text.DefaultZero());
            //    var NetAmount = (PcsRate * TotalPcs) - Convert.ToDouble(txtDisc.Text.DefaultZero());
            //    txtDisPer.Text = txtDisPer.Text ?? "0";
            //    var DiscPerc = Convert.ToDouble(txtDisPer.Text.DefaultZero() == "0" ? "1" : "0." + txtDisPer.Text);
            //    NetAmount = NetAmount * (DiscPerc == 1 ? 1 : 1 - DiscPerc);
            //    txtNet.Text = String.Format("{0:0.00}", NetAmount);
            //    TotalPcs = TotalPcs == 0 ? 1 : TotalPcs;
            //    txtPcsRate.Text = String.Format("{0:0.00}", (Convert.ToDouble(txtNet.Text) / TotalPcs));
            //    txtSaleP.Focus();
            //}
        }

        private void txtSaleP_Leave(object sender, EventArgs e)
        {
            calculation();
            //var itemID = Convert.ToInt32(cmbxItems.SelectedValue);
            //var item = db.Items.Where(x => x.IID == itemID).FirstOrDefault();
            //if (item != null)
            //{
            //    var TotalPcs = (item.CTN_PCK ?? 0 * Convert.ToInt32(txtctn.Text.DefaultZero())) + Convert.ToInt32(txtpcs.Text.DefaultZero());
            //    var PcsRate = Convert.ToDouble(txtSaleP.Text.DefaultZero());
            //    TotalPcs = TotalPcs == 0 ? 1 : TotalPcs;
            //    txtSaleRate.Text = String.Format("{0:0.00}", (PcsRate / TotalPcs));
            //    // metroButton1.Focus();
            //}
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    // row.Cells[15].Value = checkBox1.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtDisPer_Leave(object sender, EventArgs e)
        {
            calculation();
            //var itemID = Convert.ToInt32(cmbxItems.SelectedValue);
            //var item = db.Items.Where(x => x.IID == itemID).FirstOrDefault();
            //if (item != null)
            //{
            //    var Pcs = (item.CTN_PCK ?? 0 * Convert.ToInt32(txtctn.Text.DefaultZero())) + Convert.ToInt32(txtpcs.Text.DefaultZero());
            //    var Rate = Convert.ToDouble(txtRate.Text.DefaultZero());
            //    Pcs = Pcs == 0 ? 1 : Pcs;
            //    var NetAmount = Rate / Pcs;
            //    txtNet.Text = String.Format("{0:0.00}", NetAmount);
            //    txtNet.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) - Convert.ToDouble(txtDisc.Text.DefaultZero())).ToString();
            //    txtDisPer.Text = txtDisPer.Text ?? "0";
            //    var DiscPerc = Convert.ToDouble(txtDisPer.Text.DefaultZero() == "0" ? "1" : "0." + txtDisPer.Text);
            //    txtNet.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) * (DiscPerc == 1 ? 1 : 1 - DiscPerc)).ToString();
            //    txtPcsRate.Text = String.Format("{0:0.0}", item.SalesPrice * (item.CTN_PCK ?? 0 * Convert.ToInt32(txtctn.Text.DefaultZero())));
            //    txtSaleRate.Text = String.Format("{0:0.0}", item.SalesPrice * Convert.ToInt32(txtpcs.Text.DefaultZero()));
            //}
        }

        public void calculation()
        {
            int itemID = ItemID();
            var item = db.Items.FirstOrDefault(x => x.IID == itemID);

            if (item == null) return;

            // Ensure default values
            if (string.IsNullOrWhiteSpace(txtRate.Text) || txtRate.Text == "0")
                txtRate.Text = item.SalesPrice.DefaultZero();

            if (string.IsNullOrWhiteSpace(txtDisc.Text) || txtDisc.Text == "0")
                txtDisc.Text = item.DisR.DefaultZero();

            if (string.IsNullOrWhiteSpace(txtDisPer.Text) || txtDisPer.Text == "0")
                txtDisPer.Text = item.DisP.DefaultZero();

            if (string.IsNullOrWhiteSpace(txtpcs.Text) || txtpcs.Text == "0")
                txtpcs.Text = "1";

            if (string.IsNullOrWhiteSpace(txtctn.Text) || txtctn.Text == "0")
                txtctn.Text = "0";

            // Convert to doubles once
            double rate = txtRate.Text.DefaultZeroD();
            double disc = txtDisc.Text.DefaultZeroD();
            double discPer = txtDisPer.Text.DefaultZeroD();
            double pcsQty = txtpcs.Text.DefaultZeroD();
            double ctnQty = txtctn.Text.DefaultZeroD();
            double ctnPack = item.CTN_PCK.DefaultZeroD();

            // Calculate carton quantity in pieces
            double ctn = (ctnQty * ctnPack);
            if (ctn == 0) ctn = ctnQty; // fallback if no packing defined

            // Base amounts
            double ctnValue = rate * ctn;
            double pcsValue = rate * pcsQty;
            double grossAmount = ctnValue + pcsValue;

            // Discounts
            double discPerValue = (discPer / 100.0) * grossAmount;
            double netAmount = grossAmount - (discPerValue + disc);

            // Assign results
            txtSaleP.Text = grossAmount.ToString("0.##");
            txtNet.Text = netAmount.ToString("0.##");
            txtPcsRate.Text = (rate * ctnPack).ToString("0.##");
            txtSaleRate.Text = rate.ToString("0.##");
        }

        private void invGrid_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void moveUp()
        {
            if (invGrid.RowCount > 0)
            {
                if (invGrid.SelectedRows.Count > 0)
                {
                    int rowCount = invGrid.Rows.Count;
                    int index = invGrid.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }
                    invGrid.ClearSelection();
                    invGrid.Rows[index - 1].Selected = true;
                    SelectedRow(invGrid.Rows[index - 1]);
                }
            }
        }

        private void moveDown()
        {
            if (invGrid.RowCount > 0)
            {
                if (invGrid.SelectedRows.Count > 0)
                {
                    int rowCount = invGrid.Rows.Count;
                    int index = invGrid.SelectedCells[0].OwningRow.Index;

                    if (index == (rowCount - 1)) // include the header row
                    {
                        return;
                    }
                    invGrid.ClearSelection();
                    invGrid.Rows[index + 1].Selected = true;
                    SelectedRow(invGrid.Rows[index + 1]);
                }
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

        private void SelectedRow(DataGridViewRow dataGridViewRow)
        {

            lblItemID.Text = dataGridViewRow.Cells[0].Value.ToString();
            cmbxItems.Text = dataGridViewRow.Cells[1].Value.DefaultZero();
            txtctn.Text = dataGridViewRow.Cells[2].Value.DefaultZero();
            txtpcs.Text = dataGridViewRow.Cells[3].Value.DefaultZero();
            txtRate.Text = dataGridViewRow.Cells[4].Value.DefaultZero();
            txtDisPer.Text = dataGridViewRow.Cells[6].Value.DefaultZero();
            txtDisc.Text = dataGridViewRow.Cells[7].Value.DefaultZero();
            txtNet.Text = dataGridViewRow.Cells[8].Value.DefaultZero();
            txtSaleP.Text = dataGridViewRow.Cells[9].Value.DefaultZero();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            Products prod = new Products(0);
            prod.Show();
            prod.openForm();
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {

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

                    var saleM = db.Pur_M.AsNoTracking().Where(x => x.InvNo == txtInvoice.Text && x.CompID == compID).FirstOrDefault();
                   
                    if (saleM == null)
                    {

                        MessageBox.Show("Your Invoice is not Correct");
                        cmbxItems.Text = "";
                        txtInvoice.Focus();
                        return;

                    }

                    else {

                        itemID = Convert.ToInt32(item.IID.DefaultZero());
                        cmbxItems.Text = item.IName;
                   //     txtCode.Text = item.BarcodeNo;


                        var saleD = db.Pur_D.AsNoTracking().Where(x => x.IID == item.IID && x.RID == saleM.RID).FirstOrDefault();
                        if (saleD != null)
                        {
                            cmbxItems.Text = item.IName;
                            txtCode.Text = item.BarcodeNo;
                            txtDisc.Text = saleD.DisRs.DefaultZero();
                            txtDisPer.Text = saleD.DisP.DefaultZero();
                            txtpcs.Text = 1.ToString();
                            txtctn.Text = 0.ToString();
                            txtRate.Text = saleD.PurPrice.DefaultZero();
                            txtNet.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero())).ToString();
                            var DiscPercValue = Convert.ToDouble(Convert.ToDouble(txtDisPer.Text.DefaultZero()) / 100 * Convert.ToDouble(txtNet.Text.DefaultZero()));
                            var DiscValue = Convert.ToDouble(txtDisc.Text.DefaultZero());

                            txtSaleP.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero())).ToString();
                            txtPcsRate.Text = String.Format("{0:0.00}", Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));
                            txtSaleRate.Text = String.Format("{0:0.00}", Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(1));
                        }
                        else {

                            MessageBox.Show("please select Correct item accounding to the Invoice  No ");
                            cmbxItems.Text = "";
                            cmbxItems.Focus();
                            return;

                        }
                    }

                    

                    if (ddlWSR.SelectedValue.ToString() == "1")
                    {
                      //  txtRate.Text = item.SalesPrice.DefaultZero();
                    }
                    else
                    {
                        //txtRate.Text = item.WholeSale.DefaultZero();
                    }

                  
                    if (Convert.ToInt32(cmbxAccID.SelectedValue) == 3 || Convert.ToInt32(cmbxAccID.SelectedValue) == 9)
                    {
                        int Code = Convert.ToInt32(cmbxvendor.SelectedValue);
                        var abc = db.Itemledger.AsNoTracking().Where(x => x.IID == item.IID && x.TypeCode == 5 && x.AC_CODE == Code);
                        if (abc == null)
                        {
                        }
                        else
                        {
                            double quantity = 0;
                            double amt = 0;
                            foreach (Itemledger getledger in abc)
                            {
                                quantity = Convert.ToDouble(getledger.SJ);
                                amt = Convert.ToDouble(getledger.Amt);
                            }
                            lblPreviousPrice.Text = Convert.ToDouble(amt / quantity).ToString();
                        }
                    }
                    txtpcs.Focus();
                    txtbox = null;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void cmbxItems_Leave_1(object sender, EventArgs e)
        {

        }


        public void ItemRecord(int id)
        {


            if (Convert.ToInt32(cmbxAccID.SelectedValue) == 3 || Convert.ToInt32(cmbxAccID.SelectedValue) == 9)
            {
                int Code = Convert.ToInt32(cmbxvendor.SelectedValue);
                var abc = db.Itemledger.AsNoTracking().Where(x => x.IID == id && x.TypeCode == 5 && x.AC_CODE == Code);
                if (abc == null)
                {
                }
                else
                {
                    double quantity = 0;
                    double amt = 0;
                    foreach (Itemledger getledger in abc)
                    {
                        quantity = Convert.ToDouble(getledger.SJ);
                        amt = Convert.ToDouble(getledger.Amt);
                    }
                    lblPreviousPrice.Text = Convert.ToDouble(amt / quantity).ToString();
                }
            }
            var RR = ReportsController.getStockByID(id, compID);

            lblStock.Text = RR.Rows[0]["total"].ToString();

            var Acode = Convert.ToInt32(cmbxvendor.SelectedValue);

            var CACCode = db.COA_D.Where(x => x.AC_Code == Acode).FirstOrDefault();
            if (CACCode == null)
            {


                return;
            }


            if (CACCode.CAC_Code == 3)
            {

                int Vendorcode = Convert.ToInt32(Acode);

                var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Now, Vendorcode);
                int a = 1;

                double credit = (double)previosBalance.Rows[0]["credit"];
                double debit = (double)previosBalance.Rows[0]["debit"];
                double balance = debit - credit;

                lblAccountBalance.Text = balance.ToString();


            }


            if (CACCode.CAC_Code == 9)
            {
                int Vendorcode = Convert.ToInt32(Acode);

                var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Now, Vendorcode);
                int a = 1;

                double credit = (double)previosBalance.Rows[0]["credit"];
                double debit = (double)previosBalance.Rows[0]["debit"];
                double balance = credit - debit;

                lblAccountBalance.Text = balance.ToString();

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

        private void cmbxvendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbxAccID.SelectedIndex) != 0)
            {

                if (Convert.ToInt32(cmbxAccID.SelectedValue) == 3)
                {

                    customerLedger();
                }
                else if (Convert.ToInt32(cmbxAccID.SelectedValue) == 9)
                {


                    vendorledger();

                }
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            Configs.Customer cus = new Configs.Customer(compID);
            cus.Show();
            cus.customerAdd();
        }

        public void customerLedger()
        {

            int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);

            var previosBalance = db.getCustomerPreviousBalance(DateTime.Now, Vendorcode).FirstOrDefault();
            int a = 1;

            double credit = previosBalance.credit;
            double debit = previosBalance.debit;
            double balance = debit - credit;
            if (balance != 0)
            {
                //var abc = new MyModels.VendorLedger();
                //abc.Credit = (float)credit;
                //abc.Debit = (float)debit;
                //abc.Balance = (float)balance;
                //abc.Naration = "Previous Balance";
                // CategorysDataGridView.Rows.Add(a, "", "", debit, credit, balance, "Previous Balance");
                a++;
            }
            SaleManagerEntities db1 = new SaleManagerEntities();

            var getdata = db.getCustomerLedgerBYDate(DateTime.Now, DateTime.Now, Vendorcode).ToList();//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;


            var count = getdata.Count();



            for (int b = 0; b < count; b++)
            {
                // var abc = new MyModels.VendorLedger();

                balance = balance - (double)getdata[b].credit;
                balance = balance + (double)getdata[b].debit;

                //getdata[a].abc.Balance = 0;
                //abc.Credit = (float)getdata[a].credit;
                //abc.Debit = (float)getdata[a].debit;
                //abc.GlDate = (DateTime)getdata[a].GLDate;
                //abc.Naration = getdata[a].Narration;
                //abc.Reference = getdata[a].reference;
                //abc.SNO = a;
                //abc.Balance = (float)balance;


                a++;

                //studentList.Add(abc);
            }


            lblAccountBalance.Text = balance.ToString();
        }

        public void vendorledger()
        {


            //CategorysDataGridView.Rows.Add(1, 2, 3, 4, 5, 6, 7);
            int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);

            var previosBalance = db.getVendorPreviousBalance(DateTime.Now, Vendorcode).FirstOrDefault();
            int a = 1;

            double credit = previosBalance.credit;
            double debit = previosBalance.debit;
            double balance = credit - debit;
            if (balance != 0)
            {
                //var abc = new MyModels.VendorLedger();
                //abc.Credit = (float)credit;
                //abc.Debit = (float)debit;
                //abc.Balance = (float)balance;
                //abc.Naration = "Previous Balance";

                a++;
            }
            SaleManagerEntities db1 = new SaleManagerEntities();

            var getdata = db.getVendorLedgerBYDate(DateTime.Now, DateTime.Now, Vendorcode).ToList();//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;


            var count = getdata.Count();



            for (int b = 0; b < count; b++)
            {
                // var abc = new MyModels.VendorLedger();

                balance = balance + (double)getdata[b].credit;
                balance = balance - (double)getdata[b].debit;

                //getdata[a].abc.Balance = 0;
                //abc.Credit = (float)getdata[a].credit;
                //abc.Debit = (float)getdata[a].debit;
                //abc.GlDate = (DateTime)getdata[a].GLDate;
                //abc.Naration = getdata[a].Narration;
                //abc.Reference = getdata[a].reference;
                //abc.SNO = a;
                //abc.Balance = (float)balance;


                a++;



            }

            lblAccountBalance.Text = balance.ToString();
        }

        class Batches
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }

        private void txtTransportExpense_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtTransportExpense_TextChanged(object sender, EventArgs e)
        {
            txtNetAm.Text = (Convert.ToDouble(txtTotalAm.Text.DefaultZero()) + Convert.ToDouble(txtTransportExpense.Text.DefaultZero()) - Convert.ToDouble(txtDisfooter.Text.DefaultZero())).ToString();
        }

        private async void cmbxItems_KeyDown(object sender, KeyEventArgs e)
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

            string query = @"
        SELECT TOP 10 
               Items.IID,
               ISNULL(ArticleNo, '0') AS Article,
               Items.IName AS Product,
               ISNULL(Color, 0) AS Color,
               ISNULL(Size, 0) AS Size,
               ISNULL(IComp_M.Comp, '') AS Comp,
               ISNULL(Items.Formula, '') AS Formula,
               ISNULL(Items.Cabinet, '') AS Cabinet,
               ISNULL(SalesPrice,0) AS Price
        FROM Items
             LEFT JOIN Article ON Items.IID = Article.ProductID
             LEFT JOIN Colors ON Items.Color = Colors.ColorID
             LEFT JOIN Sizes ON Items.Size = Sizes.SizeID
             LEFT JOIN IComp_M ON Items.CompID = IComp_M.CompID
        WHERE Items.CompanyID = @company
          AND Items.isDeleted = 'false'
          AND (
                Items.IName LIKE '%' + @Param + '%'
             OR Article.ArticleNo LIKE '%' + @Param + '%'
             OR Colors.Name LIKE '%' + @Param + '%'
             OR Sizes.SizeName LIKE '%' + @Param + '%'
             OR IComp_M.Comp LIKE '%' + @Param + '%'
             OR Items.Formula LIKE '%' + @Param + '%'
             OR Items.Cabinet LIKE '%' + @Param + '%'
          );";

            try
            {
                var items = await Task.Run(() =>
                {
                    using (var cmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection))
                    {
                        cmd.Parameters.Add("@Param", SqlDbType.NVarChar).Value = cmbxItems.Text;
                        cmd.Parameters.Add("@company", SqlDbType.Int).Value = compID;

                        var rows = SqlHelper.ExecuteDataset(cmd).Tables[0];
                        return rows.ToList<purchaseItems>();
                    }
                });

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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
              
            }
        }

       
        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int i = dataGridView1.CurrentRow.Index;
                MessageBox.Show(i.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var ItemID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                lblItemID.Text = ItemID;
                var ID = Convert.ToInt32(ItemID);
                var item = listItems.Find(x => x.IID == ID);
                Items_Leave(item);
            }
        }

        private void metroButton1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //if (cmbxItems.Text.Trim() != string.Empty)
            //{
            //var itemname = cmbxItems.Text.ToLower().Trim();
            //var item = listItems.FirstOrDefault(x => x.IName.ToLower().Trim() == itemname);
            //if (item == null)
            //{
            //    return;
            //}
            //Items_Leave(item, null);

            //var RR = ReportsController.getStockByID(item.IID, compID);

            //lblStock.Text = RR.Rows[0]["total"].ToString();

            //    txtCode.Text = "";
            //    var Acode = Convert.ToInt32(cmbxvendor.SelectedValue);

            //    var CACCode = db.COA_D.Where(x => x.AC_Code == Acode).FirstOrDefault();
            //    if (CACCode == null)
            //    {


            //        return;
            //    }


            //    if (CACCode.CAC_Code == 3)
            //    {

            //        int Vendorcode = Convert.ToInt32(Acode);

            //        var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Now, Vendorcode);
            //        int a = 1;

            //        double credit = (double)previosBalance.Rows[0]["credit"];
            //        double debit = (double)previosBalance.Rows[0]["debit"];
            //        double balance = debit - credit;

            //        lblAccountBalance.Text = balance.ToString();


            //    }


            //    if (CACCode.CAC_Code == 9)
            //    {
            //        int Vendorcode = Convert.ToInt32(Acode);

            //        var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Now, Vendorcode);
            //        int a = 1;

            //        double credit = (double)previosBalance.Rows[0]["credit"];
            //        double debit = (double)previosBalance.Rows[0]["debit"];
            //        double balance = credit - debit;

            //        lblAccountBalance.Text = balance.ToString();

            //    }
            //    if (CACCode.CAC_Code == 1)
            //    {

            //        lblAccountBalance.Text = "0";
            //    }
            //}
            lblAccountBalance.Text = "0";
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtContactNo_Leave(object sender, EventArgs e)
        {
            var check = db.Customers.AsNoTracking().Where(x => x.Cell == txtContactNo.Text && x.CompanyID == compID).ToList();
            if (check.Count == 0 || txtContactNo.Text == "")
            {
                //account headr
                List<COA_M> article = new List<COA_M>();
                article.Add(new COA_M { CAC_Code = 0, CATitle = "--SELECT--" });
                article.AddRange(db.COA_M.AsNoTracking().Where(x => x.CAC_Code == 1 || x.CAC_Code == 3 || x.CAC_Code == 9).ToList());

                FillCombo(cmbxAccID, article, "CATitle", "CAC_Code", 0);

                //parties
                List<COA_D> parties = new List<COA_D>();
                parties.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
                parties.AddRange(db.COA_D.AsNoTracking().Where(x => x.CAC_Code == 1).ToList());
                //   var vendor = ;
                FillCombo(cmbxvendor, parties, "AC_Title", "AC_Code", 0);

            }
            else
            {
                int accode = Convert.ToInt32(check[0].AC_Code);
                var customer = db.COA_D.AsNoTracking().Where(x => x.AC_Code == accode).FirstOrDefault();

                // account headr
                List<COA_M> article = new List<COA_M>();
                article.Add(new COA_M { CAC_Code = 0, CATitle = "--SELECT--" });
                //   article.AddRange(db.COA_M.AsNoTracking().Where(x => x.CAC_Code == customer.CAC_Code));

                FillCombo(cmbxAccID, article, "CATitle", "CAC_Code", 0);


                List<Lib.Entity.Customers> parties = new List<Lib.Entity.Customers>();
                parties.Add(new Lib.Entity.Customers { AC_Code = 0, CusName = "--Select--" });
                parties.AddRange(db.Customers.AsNoTracking().Where(x => x.Cell == txtContactNo.Text && x.CompanyID == compID).ToList());
                //   var vendor = ;
                FillCombo(cmbxvendor, parties, "CusName", "AC_Code", 0);

                //parties 

                //var employee = new List<Customer>();
                //employee.Add(new Customer { CusName = "Select", AC_Code = 0 });
                //employee.AddRange(db.Customers.Where(x => x.Cell == txtNumber.Text).ToList());
                //FillCombo(cmbxPatient, employee, "CusName", "AC_Code", 0);
            }
        }

        private void ddlWSR_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemID = Convert.ToInt32(lblItemID.Text);
            var obj = listItems.Where(x => x.IID == itemID).FirstOrDefault();
            if (ddlWSR.SelectedValue.ToString() == "2")
            {
                label4.Text = "Whole Sale";
                label12.Text = "WholeSale Price";
            }
            else
            {
                label12.Text = "Sale Price";
                label4.Text = "Retail Rate";
            }
            Items_Leave(obj, null);
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

            }




        }

        private void cmbxvendor_Leave(object sender, EventArgs e)
        {



        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bnInvoice_Click(object sender, EventArgs e)
        {
            var saleM = db.Pur_M.AsNoTracking().Where(x => x.InvNo == txtInvoice.Text && x.CompID== compID).FirstOrDefault();

            if (saleM == null)
            {


                MessageBox.Show("Your Invoice is NOt Correct");

            }
            else {

                cmbxWareHouse.SelectedValue =Convert.ToInt32( saleM.WID ==null? 0 : saleM.WID);
                cmbxSaleMan.SelectedValue = Convert.ToInt32(saleM.SID == null ? 0 : saleM.SID);
                cmbxvendor.SelectedValue = Convert.ToInt32(saleM.AC_Code == null ? 0 : saleM.AC_Code);

                var account = db.COA_D.AsNoTracking().Where(x => x.AC_Code == (saleM.AC_Code == null ? 0 : saleM.AC_Code)).FirstOrDefault();
                cmbxAccID.SelectedValue = Convert.ToInt32(account.CAC_Code == null ? 0 : account.CAC_Code);

                cmbxAccID.Enabled = false;
                cmbxvendor.Enabled = false;
                cmbxSaleMan.Enabled = false;
               // cmbxWareHouse.Enabled = false;

                txtInvoice.ReadOnly = true ;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            txtInvoice.ReadOnly = false;
            loadfuntion();
            txtInvoice.Text = "";
            txtInvoice.Focus();

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }
    }

    public class purchaseItems
    {
        public int IID { get; set; }
        public string Article { get; set; }
        public String Product { get; set; }
        public double Price { get; set; }
       // public double Stock { get; set; }
        public int Color { get; set; }
        public int Size { get; set; }
        public String Comp { get; set; }
        public string Formula { get; set; }
        public string Cabinet { get; set; }
    }

}
