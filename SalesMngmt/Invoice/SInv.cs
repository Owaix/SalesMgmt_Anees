
using DIagnoseMgmt;
using Lib;
using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using Lib.Utilities;
using MetroFramework.Drawing.Html;
using Microsoft.Reporting.WinForms;
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
    public partial class SInv : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.Items> listItems = null;
        int compID = 0;
        int vebdorid = 0;
        int itemID = 0;
        bool printInvoice = true;
        List<tblStock> stock = null;
        public SInv SInvoice { get; set; }
        public SInv(int Company)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = Company;
            SInvoice = this;
            stock = new List<tblStock>();
            listItems = db.Items.Where(x => x.CompanyID == compID && x.isDeleted == false).ToList();

            if (Company != 1017)
            {


                txtBatch.Visible = false;
                txtExpDate.Visible = false;
                cmbxItems.Width = 350;

                label31.Visible = false;
                label30.Visible = false;
                label33.Visible = false;
                cmbxCompanyAccount.Visible = false;
                chkWarrenty.Visible = false;
                btnAddCompany.Visible = false;

            }
            else
            {


                pictureBox1.Visible = false;
                // txtInvoiceCustom.Visible = false;
                // label38.Visible = false;
            }



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
            // var itemID = cmbxItems.Text;
            var items = db.Items.AsNoTracking().Where(x => x.IID == ItemID).FirstOrDefault();
            if (items != null)
            {
                return items.IID;
            }
            return 0;
        }

        private void SInv_Load(object sender, EventArgs e)
        {
            //account headrif
            List<I_Unit> unitList;
            if (compID ==1024) {
               unitList = new List<I_Unit>
        {
            new I_Unit { IUnit = "PCS", unit_id = 1 },
            new I_Unit { IUnit = "SET", unit_id = 2 }
        };

            }


            else {

               unitList = new List<I_Unit>
        {
            new I_Unit { IUnit = "PCS", unit_id = 1 },
            new I_Unit { IUnit = "CTN", unit_id = 2 }
        };

            }



     

            FillCombo(cmbxPackaging, unitList, "IUnit", "unit_id", 1);
            cmbxPackaging.SelectedIndex = 0;


            List<COA_M> article = new List<COA_M>();
            article.Add(new COA_M { CAC_Code = 0, CATitle = "--SELECT--" });
            article.AddRange(db.COA_M.Where(x => x.CAC_Code == 1 || x.CAC_Code == 3 || x.CAC_Code == 9).ToList());
            FillCombo(cmbxAccID, article, "CATitle", "CAC_Code", 0);

            List<tbl_Company> companyNAme = new List<tbl_Company>();
            companyNAme.Add(new tbl_Company { CompID = 0, Comp = "--SELECT--" });
            companyNAme.AddRange(db.tbl_Company.Where(x => x.companyID == compID && x.isDelete == false).ToList());
            FillCombo(cmbxCompanyAccount, companyNAme, "Comp", "CompID", 0);







            List<COA_M> saleRetail = new List<COA_M>();
            saleRetail.Add(new COA_M { CAC_Code = 1, CATitle = "Retail Price" });
            saleRetail.Add(new COA_M { CAC_Code = 2, CATitle = "WholeSale Price" });
            saleRetail.Add(new COA_M { CAC_Code = 3, CATitle = "Distribution" });
            FillCombo(ddlWSR, saleRetail, "CATitle", "CAC_Code", 0);

            //parties
            List<COA_D> parties = new List<COA_D>();
            parties.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
            //parties.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList());
            ////   var vendor = ;
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

            if (compID == 1022 || compID == 1024)
            {
                radioButton4.Checked = true;

                cmbxWareHouse.SelectedIndex = 1;
                cmbxAccID.SelectedIndex = 1;
            }



            else
            {

                radioButton1.Checked = true;

                cmbxWareHouse.SelectedIndex = 1;



                cmbxAccID.SelectedIndex = 1;


            }

            if (compID == 1017)
            {


                radioButton2.Checked = true;
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

            if (cmbxItems.Text == "")
            {

                MessageBox.Show("Please select item first thanks");
                cmbxItems.Focus();
                return;
            }

            var isAdd = true;
            if (txtCode.Text != "")
            {
                var items = db.Items.AsNoTracking().Where(x => x.BarcodeNo == txtCode.Text && x.CompanyID == compID).FirstOrDefault();
                itemID = Convert.ToInt32(items.IID.DefaultZero());
            }

            if (ItemID(itemID) == 0)
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
            var iid = ItemID(itemID);
            var check123 = txtCode.Text;
            var item = db.Items.AsNoTracking().Where(x => x.IID == iid).FirstOrDefault();
            if (item != null)
            {

                if (txtpcs.Text == "" || txtpcs.Text == "0")
                {
                    txtpcs.Text = 0.ToString();

                }


                if (txtctn.Text == "" || txtctn.Text == "0")
                {
                    txtctn.Text = 0.ToString();

                }

                var ctn1 = (Convert.ToDouble(txtctn.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));

                if (ctn1 == 0)
                {
                    ctn1 = Convert.ToDouble(txtctn.Text.DefaultZero());
                }
                var ctnValue = (Convert.ToDouble(ctn1));

                var pcs = Convert.ToDouble(txtpcs.Text.DefaultZero());

                double totalQuantity = ctnValue + pcs;

                double stock = Convert.ToDouble(lblStock.Text.DefaultZero());

                if (item.Inv_YN == true)
                {



                }


                else if (totalQuantity > stock)
                {

                    DialogResult dialogResult = MessageBox.Show("Max entry", "you have in enter over stock  " + totalQuantity, MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        txtctn.Focus();

                        return;
                    }



                }





                // var totalCTn = item.CTN_PCK * Convert.ToInt32(txtctn.Text.DefaultZero());
                //var TotalPcs = totalCTn + Convert.ToInt32(txtpcs.Text); // -->
                //var PcsRate = Convert.ToDouble(txtRate.Text.DefaultZero()) * TotalPcs;
                //var NetAmount = PcsRate - Convert.ToDouble(txtDisc.Text.DefaultZero());
                //   NetAmount = NetAmount * Convert.ToDouble(txtDisPer.Text.DefaultZero() == "0" ? "1" : "0." + txtDisPer.Text);

                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        var itemIID = Convert.ToInt32(row.Cells[0].Value.DefaultZero());
                        var stockid = db.getStockByID(itemIID).FirstOrDefault();
                        int val = ItemID();
                        if (ItemID(itemID) == itemIID)
                        {
                            row.Cells[2].Value = txtctn.Text.DefaultZero();
                            if (txtCode.Text != "")
                            {
                                txtpcs.Text = "";
                                double value = Convert.ToDouble(txtpcs.Text.DefaultZero()) + 1;
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

                                //if (stockid >= value)
                                //{


                                //}
                                int values = Convert.ToInt32(row.Cells[3].Value.DefaultZero()) + 1;
                                txtpcs.Text = values.ToString();

                                calculation();
                                if (item.Inv_YN == true)
                                {
                                    row.Cells[0].Value = ItemID(itemID);
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
                                    row.Cells[0].Value = ItemID(itemID);
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
                                    row.Cells[0].Value = ItemID(itemID);
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
                                    row.Cells[0].Value = ItemID(itemID);
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

                        this.invGrid.Rows.Add(ItemID(itemID),
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
                       "0", txtBatch.Text, txtExpDate.Text, cmbxPackaging.SelectedIndex);

                        invGrid_SelectionChanged(null, null);
                        clear(true);

                    }
                    else
                    {

                        this.invGrid.Rows.Add(ItemID(itemID),
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
                      "Remove", "0", txtBatch.Text, txtExpDate.Text, cmbxPackaging.SelectedIndex);


                        invGrid_SelectionChanged(null, null);
                        clear(true);
                        //MessageBox.Show("stock can,t be more than that " + stockid);
                        //return;
                    }
                }

                invGrid_SelectionChanged(null, null);
                calculation();

                txtCode.Focus();

                lblItemID.Text = "";

                txtDisc.Text = "0";
                txtDisPer.Text = "0";
                txtctn.Text = "0";
                txtpcs.Text = "0";

                txtRate.Text = "0";
                txtNet.Text = "0";
                txtPcsRate.Text = "0";
                txtSaleRate.Text = "0";
                txtSaleP.Text = "0";
                cmbxItems.Text = "";
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

            lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text.DefaultZero()) + Convert.ToDouble(txtNetAm.Text.DefaultZero())).ToString();
            //int account = (int) cmbxAccID.SelectedValue;
            //if (account == 0)
            //{
            //    lblGrandTotal.Text = "0";

            //}
            //else {


            //    lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text) + Convert.ToDouble(txtNetAm.Text)).ToString();
            //}
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
            List<COA_M> article = new List<COA_M>();
            article.Add(new COA_M { CAC_Code = 0, CATitle = "--SELECT--" });
            article.AddRange(db.COA_M.Where(x => x.CAC_Code == 1 || x.CAC_Code == 3 || x.CAC_Code == 9).ToList());
            FillCombo(cmbxAccID, article, "CATitle", "CAC_Code", 0);

            List<tbl_Company> companyNAme = new List<tbl_Company>();
            companyNAme.Add(new tbl_Company { CompID = 0, Comp = "--SELECT--" });
            companyNAme.AddRange(db.tbl_Company.Where(x => x.companyID == compID && x.isDelete == false).ToList());
            FillCombo(cmbxCompanyAccount, companyNAme, "Comp", "CompID", 0);







            List<COA_M> saleRetail = new List<COA_M>();
            saleRetail.Add(new COA_M { CAC_Code = 1, CATitle = "Retail Price" });
            saleRetail.Add(new COA_M { CAC_Code = 2, CATitle = "WholeSale Price" });
            FillCombo(ddlWSR, saleRetail, "CATitle", "CAC_Code", 0);

            //parties
            List<COA_D> parties = new List<COA_D>();
            parties.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
            //parties.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList());
            ////   var vendor = ;
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




            clear();
            var Master = db.Sales_M.Where(x => x.InvNo == invNo).FirstOrDefault();
            if (Master != null)
            {
                dtpInvoice.Value = (DateTime)Master.EDate;
                lblRID.Text = Master.RID.ToString();
                txtNetAm.Text = Master.NetAmt.ToString();
                txtDisfooter.Text = Master.DisAmt.ToString();
                txtTotalAm.Text = Master.TotalAmount.ToString();
                txtRemarks.Text = Master.Rem.ToString();
                int AccID = Convert.ToInt32(db.COA_D.AsNoTracking().Where(x => x.AC_Code == Master.AC_Code).FirstOrDefault().CAC_Code.DefaultZero());

                cmbxAccID.SelectedValue = AccID;
                cmbxvendor.SelectedValue = Convert.ToInt32(Master.AC_Code);
                txtName.Text = Master.CashCustomer;
                txtInv.Text = Master.CstInvNo;
                txtInvDate.Text = Master.VenInvDate;
                txtTransportExpense.Text = Master.TransportExpense.ToString();
                cmbxWareHouse.SelectedValue = Convert.ToInt32(Master.WID.DefaultZero());
                cmbxSaleMan.SelectedValue = Convert.ToInt32(db.Itemledger.AsNoTracking().Where(x => x.TypeCode == 5 && x.RID == Master.RID).FirstOrDefault().SID.DefaultZero());

                compID = Convert.ToInt32(Master.CompID);

                lblAccountBalance.Text = Master.PreBal.ToString().DefaultZero();

                lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text.DefaultZero()) + Convert.ToDouble(txtNetAm.Text.DefaultZero())).ToString();

                txtCashReceived.Text = Master.CashAmt.ToString().DefaultZero();

                if (Master.CashAmt == "" || Master.CashAmt == "0")
                {


                    cmbxPaymentMode.SelectedValue = 0;

                }
                else
                {
                    int masterCode = Convert.ToInt32(Master.CompID);
                    if (Master.AC_Code3 == null)
                    {
                        int code = Convert.ToInt32(db.COA_D.AsNoTracking().Where(x => x.CompanyID == masterCode && x.CAC_Code == 1).FirstOrDefault().AC_Code);
                        cmbxPaymentMode.SelectedValue = code;
                    }

                    else
                    {

                        cmbxPaymentMode.SelectedValue = Master.AC_Code3;
                    }


                }
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




                        this.invGrid.Rows.Add(item.IID, itemName.IName, item.CTN, item.PCS, item.SalesPriceP, TotalPcs, item.DisP, item.DisRs, item.Amt, "", "", "Remove", itemName.ArticleNoID, item.BatchNo, item.ExpireDate,item.Pur_D_UnitID==null?0: item.Pur_D_UnitID);
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
            int account = (int)cmbxAccID.SelectedValue;
            if (account == 0)
            {
                lblGrandTotal.Text = "0";

            }
            else
            {


                lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text) + Convert.ToDouble(txtNetAm.Text)).ToString();
            }

        }

        private void txtCredit_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}

            //// only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void invGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex > -1)
            {
                itemID = Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells[0].Value.DefaultZero());
                cmbxItems.Text = invGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtctn.Text = invGrid.Rows[e.RowIndex].Cells[2].Value.DefaultZero();
                label34.Text = invGrid.Rows[e.RowIndex].Cells[3].Value.DefaultZero();
                txtpcs.Text = invGrid.Rows[e.RowIndex].Cells[3].Value.DefaultZero();
                txtRate.Text = invGrid.Rows[e.RowIndex].Cells[4].Value.DefaultZero();
                txtDisPer.Text = invGrid.Rows[e.RowIndex].Cells[6].Value.DefaultZero();
                txtDisc.Text = invGrid.Rows[e.RowIndex].Cells[7].Value.DefaultZero();
                txtNet.Text = invGrid.Rows[e.RowIndex].Cells[8].Value.DefaultZero();
                txtSaleP.Text = invGrid.Rows[e.RowIndex].Cells[9].Value.DefaultZero();
                cmbxPackaging.SelectedIndex = Convert.ToInt32(invGrid.Rows[e.RowIndex].Cells["Unit"].Value ?? "0");

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

            txtRate.Text = "0";
            txtNet.Text = "0";
            txtPcsRate.Text = "0";
            txtSaleRate.Text = "0";
            dataGridView1.Visible = false;
            panel1.Visible = false;
            txtSaleP.Text = "0";
            lblGrandTotal.Text = "0";
            lblItemID.Text = "";
            txtTransportExpense.Text = "";
            txtCashReceived.Text = "0";
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            printInvoice = false;
            SaveRecord(lblRID.Text);
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            SaveRecord(lblRID.Text);
        }

        private void SInv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Modifiers == Keys.Alt)
            {
                clear();
            }
        }

        private void SaveRecord(String InvoiceNo)
        {
            Accountvalidation();


            #region check Validation
            if (invGrid.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Items In Grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbxAccID.Text == "--SELECT--")
            {
                MessageBox.Show("Please select AccountName", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbxAccID.Focus();
                return;


            }

            if (vebdorid == 0 || vebdorid == null)
            {
                MessageBox.Show("Please select AccountName", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbxvendor.Focus();
                return;
            }
            int warehouseId = (int)cmbxWareHouse.SelectedValue;

            if (warehouseId == 0)
            {
                MessageBox.Show("Please select WareHouse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;

            }


            #endregion


            Sales_M sale = null;

            #region Transaction Begin
            DbContextTransaction transaction = db.Database.BeginTransaction();
            try
            {
                #region Sale Master Create And Edit

                if (InvoiceNo == "0")
                {
                    sale = new Sales_M();
                    DataAccess access = new DataAccess();
                    Purc_M coa = new Purc_M();
                    coa.InvType = "SI";
                    SqlConnection con = new SqlConnection(ConnectionStrings.GetCS);
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    if (txtInvoiceCustom.Text == null || txtInvoiceCustom.Text == "" || txtInvoiceCustom.Text == "")
                    {
                        sale.InvNo = access.GetScalar("Gen_NewInvNoSale", coa, con, trans);
                    }

                    else { sale.InvNo = txtInvoiceCustom.Text; }
                    sale.InvDT = dtpInvoice.Value;
                    sale.ReferenceNo = txtReference.Text;
                    sale.EDate = dtpInvoice.Value;
                }
                else
                {
                    var RID = Convert.ToInt32(InvoiceNo);
                    sale = db.Sales_M.Where(x => x.RID == RID).FirstOrDefault();
                    sale.Edit_Date = DateTime.Now;
                    sale.ReferenceNo = txtReference.Text;
                    sale.EDate = dtpInvoice.Value;
                }

                sale.InvType = "SI";

                sale.AC_Code = Convert.ToInt32(vebdorid);
                sale.SID = Convert.ToInt32(cmbxSaleMan.SelectedValue);
                sale.WID = (int)cmbxWareHouse.SelectedValue;
                sale.TotalAmount = Convert.ToDecimal(txtTotalAm.Text);
                sale.DisAmt = Convert.ToDouble(txtDisfooter.Text);
                sale.NetAmt = Convert.ToDouble(txtTotalAm.Text);
                sale.CashCustomer = txtName.Text;
                sale.Rem = txtRemarks.Text;
                sale.CompID = compID;
                sale.CstInvNo = txtInv.Text;
                sale.VenInvDate = txtInvDate.Text;
                sale.CashAmt = txtCashReceived.Text;
                sale.diffCompany = Convert.ToInt32(cmbxCompanyAccount.SelectedValue);
                sale.diffCompanyStatus = chkWarrenty.Checked;
                sale.PreBal = Convert.ToDouble(lblAccountBalance.Text);
                sale.TransportExpense = Convert.ToDouble(txtTransportExpense.Text.DefaultZero());
                sale.AC_Code3 = cmbxPaymentMode.SelectedValue.ToString();
                if (InvoiceNo == "0")
                {
                    db.Sales_M.Add(sale);
                }
           
                if (InvoiceNo != "0")
                {
                    db.Entry(sale).State = EntityState.Modified;
                    db.Sales_D.RemoveRange(db.Sales_D.Where(x => x.RID == sale.RID));
                    db.Itemledger.RemoveRange(db.Itemledger.Where(x => x.RID == sale.RID && x.CompanyID == compID && x.TypeCode == 5));
                    db.GL.RemoveRange(db.GL.Where(x => x.RID == sale.RID && x.CompID == compID && x.TypeCode == 5));
                }
                db.SaveChanges();
                #endregion

                #region Add And update Sale Details Row in multiple Table

                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        int id = Convert.ToInt32(row.Cells[0].Value);
                        var item = db.Items.AsNoTracking().Where(x => x.IID == id).FirstOrDefault();

                        Sales_D detail = new Sales_D();
                        detail.ExpireDate = row.Cells[14].Value.ToString();
                        detail.BatchNo = row.Cells[13].Value.ToString();
                        detail.RID = sale.RID;
                        detail.IID = Convert.ToDouble(row.Cells[0].Value.ToString().DefaultZero());
                        detail.CTN = row.Cells[2].Value.ToString().DefaultZero();
                        detail.PCS = Convert.ToDouble(row.Cells[3].Value.ToString().DefaultZero());
                        detail.PurPrice = Convert.ToDouble(item.AveragePrice.DefaultZero());
                        detail.DisP = (row.Cells[6].Value ?? "0").ToString();
                        detail.DisRs = (row.Cells[7].Value ?? "0").ToString();
                        detail.BatchNo = (row.Cells[13].Value ?? "").ToString();
                        detail.ExpireDate = (row.Cells[14].Value ?? "").ToString();
                       
                        detail.Pur_D_UnitID = Convert.ToInt32((row.Cells["Unit"].Value ?? "0"));
                        double ctn; 
                        if (Convert.ToInt32(item.CTN_PCK.DefaultZero()) == 0)
                        {
                            ctn = Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                        }
                        else
                        {
                            ctn = Convert.ToDouble(item.CTN_PCK.DefaultZero()) * Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                        }

                        detail.SalesPriceP = Convert.ToDouble(row.Cells[4].Value.ToString().DefaultZero());
                        detail.Qty = ctn + Convert.ToDouble(row.Cells[3].Value);
                        detail.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero())).ToString();//Convert.ToDouble(txtSaleRate.Text);
                        //BatchNo BAtchNO
                        //Quantity Caton+Pices
                        detail.Amt = Convert.ToDouble(row.Cells[8].Value.ToString().DefaultZero());
                        detail.Amt2 = Convert.ToDouble(item.AveragePrice.DefaultZero()) * detail.Qty;
                       
                        //DisAmount   percentage in rupess
                        //Amt         after complete discount
                        db.Sales_D.Add(detail);
                        db.SaveChanges();
                        int Ridid = Convert.ToInt32(row.Cells[0].Value.DefaultZero());
                        var checkstock = db.Items.AsNoTracking().Where(x => x.IID == Ridid).FirstOrDefault();
                        if (checkstock.Inv_YN == false)
                        {
                            Itemledger ledger = new Itemledger();
                            ledger.RID = sale.RID;
                            ledger.IID = Convert.ToInt32(row.Cells[0].Value.DefaultZero());
                            if (InvoiceNo == "0")
                            {
                                ledger.EDate = dtpInvoice.Value;
                            }

                            else
                            {
                                ledger.EDate = dtpInvoice.Value;

                            }
                            // ledger.Bnid = BatchNo
                            ledger.TypeCode = 5;
                            ledger.AC_CODE = Convert.ToInt32(vebdorid);
                            ledger.WID = (int)cmbxWareHouse.SelectedValue;
                            ledger.SID = Convert.ToInt32(cmbxSaleMan.SelectedValue);
                            // ledger.SiD =
                            //    var ctnledger = Convert.ToDouble(item.CTN_PCK.DefaultZero()) * Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                            ledger.CTN = Convert.ToDouble(row.Cells[2].Value.ToString());
                            ledger.PCS = Convert.ToDouble(row.Cells[3].Value.ToString());
                            ledger.SJ = ctn + Convert.ToDouble(row.Cells[3].Value);
                            ledger.PurPrice = Convert.ToDouble(item.AveragePrice.DefaultZero());
                            ledger.SalesPriceP = Convert.ToDouble(row.Cells[4].Value.ToString());//  ledger.Pamt = TotalAmount(pj * PurPrice)
                            ledger.DisP = Convert.ToDouble(row.Cells[6].Value.DefaultZero());
                            //ledger.DisAmount = percentage in rupess
                            ledger.DisRs = Convert.ToDouble(row.Cells[7].Value.DefaultZero());
                            ledger.Amt = Convert.ToDouble(row.Cells[8].Value.DefaultZero());
                            ledger.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                            ledger.CompanyID = compID;

                            db.Itemledger.Add(ledger);
                            db.SaveChanges();

                            GL gl = new GL();
                            gl.RID = sale.RID;
                            gl.TypeCode = 5;
                            gl.SID = (int)cmbxSaleMan.SelectedValue;

                            if (InvoiceNo == "0")
                            {

                                gl.GLDate = dtpInvoice.Value;
                            }

                            else { gl.GLDate = dtpInvoice.Value; }

                            gl.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                            gl.AC_Code = Convert.ToInt32(vebdorid);
                            gl.AC_Code2 = item.AC_Code_Inv;
                            gl.Narration = row.Cells[1].Value.ToString();
                            //  gl.MOP_ID = 2;
                            gl.Qty_Out = ctn + Convert.ToDouble(row.Cells[3].Value);
                            gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                            gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                            gl.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                            gl.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                            gl.Debit = Convert.ToDouble(row.Cells[8].Value.DefaultZero());
                            gl.CompID = compID;
                            gl.Credit = 0;
                            db.GL.Add(gl);
                            db.SaveChanges();

                            GL gl1 = new GL();
                            gl1.RID = sale.RID;
                            gl1.TypeCode = 5;
                            gl1.SID = (int)cmbxSaleMan.SelectedValue;

                            if (InvoiceNo == "0")
                            {

                                gl1.GLDate = dtpInvoice.Value;
                            }

                            else { gl1.GLDate = dtpInvoice.Value; }
                            gl1.IPrice = item.AveragePrice;
                            gl1.AC_Code = item.AC_Code_Inv;
                            gl1.AC_Code2 = Convert.ToInt32(vebdorid);
                            gl1.Narration = cmbxvendor.SelectedText;
                            //  gl.MOP_ID = 2;
                            gl1.Qty_Out = ctn + Convert.ToDouble(row.Cells[3].Value);
                            gl1.PAmt = Convert.ToDouble(item.AveragePrice.DefaultZero()) * (ctn + Convert.ToDouble(row.Cells[3].Value));//Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                            gl1.DisP = 0;// Convert.ToDouble(row.Cells[6].Value ?? "0");
                            gl1.DisRs = 0; //Convert.ToDouble((row.Cells[7].Value ?? "0"));
                            gl1.DisAmt = 0;// Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                            gl1.Debit = 0;
                            gl1.Credit = Convert.ToDouble(item.AveragePrice.DefaultZero()) * (ctn + Convert.ToDouble(row.Cells[3].Value));
                            gl1.CompID = compID;
                            db.GL.Add(gl1);
                            db.SaveChanges();


                            GL gl2 = new GL();
                            gl2.RID = sale.RID;
                            gl2.TypeCode = 5;
                            gl2.SID = (int)cmbxSaleMan.SelectedValue;

                            if (InvoiceNo == "0")
                            {

                                gl2.GLDate = dtpInvoice.Value;
                            }
                            else { gl2.GLDate = dtpInvoice.Value; }
                            gl2.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                            gl2.AC_Code = item.AC_Code_Inc;
                            gl2.AC_Code2 = Convert.ToInt32(vebdorid);
                            gl2.Narration = cmbxvendor.SelectedText;
                            //  gl.MOP_ID = 2;
                            gl2.Qty_Out = ctn + Convert.ToDouble(row.Cells[3].Value);
                            gl2.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                            gl2.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                            gl2.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                            gl2.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                            gl2.Debit = 0;
                            gl2.Credit = Convert.ToDouble(row.Cells[8].Value.DefaultZero()); ;
                            gl2.CompID = compID;
                            db.GL.Add(gl2);
                            db.SaveChanges();

                            GL gl3 = new GL();
                            gl3.RID = sale.RID;
                            gl3.TypeCode = 5;
                            gl3.SID = (int)cmbxSaleMan.SelectedValue;

                            if (InvoiceNo == "0")
                            {
                                gl3.GLDate = dtpInvoice.Value;
                            }

                            else
                            {

                                gl3.GLDate = dtpInvoice.Value;
                            }
                            gl3.IPrice = item.AveragePrice;
                            gl3.AC_Code = item.AC_Code_Cost;
                            gl3.AC_Code2 = Convert.ToInt32(vebdorid);
                            gl3.Narration = cmbxvendor.SelectedText;
                            //  gl.MOP_ID = 2;
                            gl3.Qty_Out = ctn + Convert.ToDouble(row.Cells[3].Value);
                            gl3.PAmt = Convert.ToDouble(item.AveragePrice.DefaultZero()) * gl1.Qty_Out;//Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                            gl3.DisP = 0;// Convert.ToDouble(row.Cells[6].Value ?? "0");
                            gl3.DisRs = 0; //Convert.ToDouble((row.Cells[7].Value ?? "0"));
                            gl3.DisAmt = 0;// Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                            gl3.Debit = Convert.ToDouble(item.AveragePrice.DefaultZero()) * gl1.Qty_Out; ;
                            gl3.Credit = 0;
                            gl3.CompID = compID;
                            db.GL.Add(gl3);
                            db.SaveChanges();
                        }
                        else
                        {
                            GL gl = new GL();
                            gl.RID = sale.RID;
                            gl.TypeCode = 5;
                            gl.SID = (int)cmbxSaleMan.SelectedValue;

                            if (InvoiceNo == "0")
                            {
                                gl.GLDate = dtpInvoice.Value;
                            }
                            else
                            {

                                gl.GLDate = dtpInvoice.Value;

                            }
                            gl.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                            gl.AC_Code = Convert.ToInt32(vebdorid);
                            gl.AC_Code2 = item.AC_Code_Inc;
                            gl.Narration = row.Cells[1].Value.ToString();
                            //  gl.MOP_ID = 2;
                            // var ctnledger = Convert.ToDouble(item.CTN_PCK.DefaultZero()) * Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                            gl.Qty_Out = ctn + Convert.ToDouble(row.Cells[3].Value);
                            gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                            gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                            gl.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                            gl.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                            gl.Debit = Convert.ToDouble(row.Cells[8].Value.DefaultZero());
                            gl.Credit = 0;
                            gl.CompID = compID;
                            db.GL.Add(gl);
                            db.SaveChanges();

                            GL gl1 = new GL();
                            gl1.RID = sale.RID;
                            gl1.TypeCode = 5;
                            gl1.SID = (int)cmbxSaleMan.SelectedValue;

                            if (InvoiceNo == "0")
                            {
                                gl1.GLDate = dtpInvoice.Value;
                            }

                            else { gl1.GLDate = dtpInvoice.Value; }
                            gl1.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                            gl1.AC_Code = item.AC_Code_Inc;
                            gl1.AC_Code2 = Convert.ToInt32(vebdorid);
                            gl1.Narration = cmbxvendor.SelectedText;
                            //  gl.MOP_ID = 2;
                            gl1.Qty_Out = ctn + Convert.ToDouble(row.Cells[3].Value);
                            gl1.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                            gl1.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                            gl1.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                            gl1.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                            gl1.Debit = 0;
                            gl1.Credit = Convert.ToDouble(row.Cells[8].Value.DefaultZero());
                            gl1.CompID = compID;
                            db.GL.Add(gl1);
                            db.SaveChanges();
                        }
                     
                    }
                }

                #endregion


                #region Total Discount  

                if (txtDisfooter.Text != "0" && txtDisfooter.Text != "")
                {
                    int customer = Convert.ToInt32(vebdorid);
                    var Expense = db.COA_D.AsNoTracking().Where(x => x.AC_Title == "Total Sale Discount Expense" && x.CompanyID == compID && x.CAC_Code == 16).FirstOrDefault();
                    var customerDetial = db.COA_D.AsNoTracking().Where(x => x.AC_Code == customer).FirstOrDefault();

                    // var Income = db.COA_D.Where(x => x.AC_Title == "Purchase Total Discount Income" && x.CompanyID == compID && x.CAC_Code == 14).FirstOrDefault();

                    if (Expense == null)
                    {






                        COA_D coaD = new COA_D();
                        coaD.CAC_Code = 16;
                        coaD.PType_ID = 1;
                        coaD.ZID = 0;
                        coaD.AC_Code = (int)db.GetAc_Code(16).FirstOrDefault();
                        coaD.AC_Title = "Total Sale Discount Expense";
                        coaD.DR = 0;
                        coaD.CR = 0;
                        coaD.Qty = 0;
                        coaD.CompanyID = compID;
                        coaD.InActive = false;
                        db.COA_D.Add(coaD);
                        db.SaveChanges();



                        Expense = db.COA_D.AsNoTracking().Where(x => x.AC_Title == "Total Sale Discount Expense" && x.CompanyID == compID && x.CAC_Code == 16).FirstOrDefault();

                    }

                    else
                    {
                        GL gl = new GL();
                        gl.RID = sale.RID;
                        gl.SID = (int)cmbxSaleMan.SelectedValue;
                        gl.TypeCode = 5;
                        if (InvoiceNo == "0")
                        {
                            gl.GLDate = dtpInvoice.Value;
                        }

                        else
                        {

                            gl.GLDate = dtpInvoice.Value;
                        }
                        //  gl.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                        gl.AC_Code = Expense.AC_Code;
                        gl.AC_Code2 = Convert.ToInt32(vebdorid);
                        gl.Narration = customerDetial.AC_Title;
                        //  gl.MOP_ID = 2;
                        // gl.Qty_Out = (item.CTN_PCK ?? 0 * Convert.ToInt32(row.Cells[2].Value)) + Convert.ToInt32(row.Cells[3].Value);
                        // gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        //  gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        //  gl.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        gl.DisAmt = 0;
                        gl.Debit = Convert.ToDouble(txtDisfooter.Text);
                        gl.Credit = 0;
                        gl.CompID = compID;
                        db.GL.Add(gl);
                        db.SaveChanges();

                        GL gl1 = new GL();
                        gl1.RID = sale.RID;
                        gl1.TypeCode = 5;
                        gl1.SID = (int)cmbxSaleMan.SelectedValue;

                        if (InvoiceNo == "0")
                        {
                            gl1.GLDate = dtpInvoice.Value;
                        }

                        else { gl1.GLDate = dtpInvoice.Value; }
                        //   gl1.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                        gl1.AC_Code = Convert.ToInt32(vebdorid);
                        gl1.AC_Code2 = Expense.AC_Code;
                        gl1.Narration = Expense.AC_Title;
                        //  gl.MOP_ID = 2;
                        // gl1.Qty_Out = (item.CTN_PCK ?? 0 * Convert.ToInt32(row.Cells[2].Value)) + Convert.ToInt32(row.Cells[3].Value);
                        //gl1.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        //gl1.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        //gl1.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        //gl1.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl1.Debit = 0;
                        gl1.Credit = Convert.ToDouble(txtDisfooter.Text);
                        gl1.CompID = compID;
                        db.GL.Add(gl1);
                        db.SaveChanges();
                    }
                }

                #endregion
                #region Recipe
                receiptManagement(sale.RID);
                #endregion

                var order = db.Sales_M.AsNoTracking().Where(x => x.RID == sale.RID).FirstOrDefault();
                var list = db.Sales_D.AsNoTracking().Where(x => x.RID == sale.RID).ToList();
                List<SaleInvoice> orderList = new List<SaleInvoice>();
                List<SaleInvoice> orderHeader = new List<SaleInvoice>();
                int sNO = 1;
                double TotalGross = 0;
                double Amount = 0;
                double DisountTotal = 0;
                double TotalDiscount = 0;
                double DiscountDifference = 0;
                double TotalGrossAmount = 0;

                // geting TotalSum values from this ForEachLoop
                foreach (var itemName in list)
                {
                    SaleInvoice orders = new SaleInvoice();
                    orders.InvoiceID = order.InvNo;

                    int accountCode = (int)cmbxAccID.SelectedValue;
                    if (accountCode == 1)
                    {

                        if (order.CashCustomer == "")
                        {
                            orders.Client = db.COA_D.AsNoTracking().Where(x => x.AC_Code == order.AC_Code).FirstOrDefault().AC_Title;
                            txtName.Text = "";
                        }
                        else
                        {
                            orders.Client = order.CashCustomer;
                            txtName.Text = "";

                        }

                    }
                    else
                    {
                        var clientNAmeAndCity = db.COA_D.AsNoTracking().Where(x => x.AC_Code == order.AC_Code).FirstOrDefault();
                        orders.Client = clientNAmeAndCity.AC_Title;

                        txtName.Text = "";

                        if (clientNAmeAndCity.CAC_Code == 3)
                        {
                            var customer = db.Customers.AsNoTracking().Where(x => x.AC_Code == order.AC_Code).FirstOrDefault();
                            int city = Convert.ToInt32(customer.City);
                            var cityName = db.tbl_city.AsNoTracking().Where(x => x.Id == city).FirstOrDefault();
                            if (cityName == null)
                            {
                                orders.City = "";
                            }
                            else
                            {

                                orders.City = cityName.CityName;

                            }

                        }

                        if (clientNAmeAndCity.CAC_Code == 9)
                        {
                            var customer = db.Vendors.AsNoTracking().Where(x => x.AC_Code == order.AC_Code).FirstOrDefault();
                            int city = Convert.ToInt32(customer.City);
                            var cityName = db.tbl_city.AsNoTracking().Where(x => x.Id == city).FirstOrDefault();
                            if (cityName == null)
                            {
                                orders.City = "";
                            }
                            else
                            {

                                orders.City = cityName.CityName;

                            }


                        }
                        //orders.Client = db.COA_D.Where(x => x.AC_Code == order.AC_Code).FirstOrDefault().AC_Title;
                        // DateTime dt = DateTime.ParseExact(order.EDate.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                        //   string s = order.EDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                        //   string s=     order.EDate.to().to;



                    }
                    orders.user = txtReference.Text.ToString();
                    //   DateTime dt = DateTime.ParseExact(order.EDate.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(order.EDate);
                    orders.Time = dt.ToString("dd/MM/yyyy");
                    var itemvalue = db.Items.AsNoTracking().Where(x => x.IID == itemName.IID).FirstOrDefault();
                    if (itemvalue.Meter <= 0 || itemvalue.Meter == null)
                    {


                        orders.item = itemvalue.IName;
                    }


                    else
                    {

                        double totalQty;
                        if (Convert.ToInt32(itemvalue.CTN_PCK.DefaultZero()) == 0)
                        {
                            totalQty = Convert.ToDouble(itemName.CTN.DefaultZero());
                        }
                        else
                        {
                            totalQty = Convert.ToDouble(itemName.CTN.DefaultZero()) * Convert.ToDouble(itemvalue.CTN_PCK.DefaultZero());
                        }

                        totalQty += Convert.ToDouble(itemName.PCS);

                        double meteerValue = Convert.ToDouble(itemvalue.Meter * totalQty);
                        orders.item = itemvalue.IName + "( Meter =" + meteerValue + " )";
                    }
                    orders.Rate = Convert.ToDecimal(itemName.SalesPriceP);
                    orders.DiscountRs = Convert.ToDecimal(itemName.DisRs);
                    orders.DiscountPercentage = Convert.ToDecimal(itemName.DisP);
                    orders.Amount = Convert.ToDecimal(itemName.Amt);
                    orders.Ctn = Convert.ToDecimal(itemName.CTN);
                    orders.Pcs = Convert.ToDecimal(itemName.PCS);
                    orders.BatchNo = itemName.BatchNo.ToString();
                    orders.ExpDate = itemName.ExpireDate.ToString();
                    //orders.SNO = sNO;
                    //sNO++;
                    double ctn = Convert.ToDouble(db.Items.AsNoTracking().Where(x => x.IID == itemName.IID).FirstOrDefault().CTN_PCK);
                    double TOtalValue = 0;
                    if (ctn == 0)
                    {
                        TOtalValue = Convert.ToDouble(itemName.Qty);
                        TotalGross += Convert.ToDouble(itemName.SalesPriceP) * Convert.ToDouble(TOtalValue);
                    }
                    else
                    {
                        TOtalValue = (ctn * Convert.ToDouble(itemName.CTN)) + Convert.ToDouble(itemName.Qty);


                        TotalGross += Convert.ToDouble(itemName.SalesPriceP) * TOtalValue;


                    }
                    Amount += Convert.ToDouble(itemName.Amt);



                    TotalGrossAmount = TotalGross;
                    DisountTotal = Amount;
                    DiscountDifference = Convert.ToDouble(TotalGross) - Convert.ToDouble(Amount);
                    TotalDiscount = Convert.ToDouble(DiscountDifference);  //(Convert.ToDecimal(itemName.SalesPriceP) * Convert.ToDecimal( TOtalValue))- Convert.ToDecimal(itemName.Amt);


                }
                TotalDiscount += Convert.ToDouble(order.DisAmt.DefaultZero());
                DisountTotal -= Convert.ToDouble(order.DisAmt.DefaultZero());


                foreach (var itemName in list)
                {
                    SaleInvoice orders = new SaleInvoice();
                    orders.InvoiceID = order.InvNo;

                    int accountCode = (int)cmbxAccID.SelectedValue;
                    if (accountCode == 1)
                    {

                        if (order.CashCustomer == "")
                        {
                            orders.Client = db.COA_D.AsNoTracking().Where(x => x.AC_Code == order.AC_Code).FirstOrDefault().AC_Title;
                            txtName.Text = "";
                        }
                        else
                        {
                            orders.Client = order.CashCustomer;
                            txtName.Text = "";

                        }

                    }
                    else
                    {
                        var clientNAmeAndCity = db.COA_D.AsNoTracking().Where(x => x.AC_Code == order.AC_Code).FirstOrDefault();
                        orders.Client = clientNAmeAndCity.AC_Title;

                        txtName.Text = "";

                        if (clientNAmeAndCity.CAC_Code == 3)
                        {
                            var customer = db.Customers.AsNoTracking().Where(x => x.AC_Code == order.AC_Code).FirstOrDefault();
                            int city = Convert.ToInt32(customer.City);
                            var cityName = db.tbl_city.AsNoTracking().Where(x => x.Id == city).FirstOrDefault();
                            if (cityName == null)
                            {
                                orders.City = "";
                            }
                            else
                            {

                                orders.City = cityName.CityName;

                            }

                        }

                        if (clientNAmeAndCity.CAC_Code == 9)
                        {
                            var customer = db.Vendors.AsNoTracking().Where(x => x.AC_Code == order.AC_Code).FirstOrDefault();
                            int city = Convert.ToInt32(customer.City);
                            var cityName = db.tbl_city.AsNoTracking().Where(x => x.Id == city).FirstOrDefault();
                            if (cityName == null)
                            {
                                orders.City = "";
                            }
                            else
                            {

                                orders.City = cityName.CityName;

                            }


                        }
              



                    }

                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(order.EDate);
                    orders.Time = dt.ToString("dd/MM/yyyy");



                    var itemvalue = db.Items.AsNoTracking().Where(x => x.IID == itemName.IID).FirstOrDefault();
                    if (itemvalue.Meter <= 0 || itemvalue.Meter == null)
                    {


                        orders.item = itemvalue.IName;
                    }


                    else
                    {

                        double totalQty;
                        if (Convert.ToInt32(itemvalue.CTN_PCK.DefaultZero()) == 0)
                        {
                            totalQty = Convert.ToDouble(itemName.CTN.DefaultZero());
                        }
                        else
                        {
                            totalQty = Convert.ToDouble(itemName.CTN.DefaultZero()) * Convert.ToDouble(itemvalue.CTN_PCK.DefaultZero());
                        }

                        totalQty += Convert.ToDouble(itemName.PCS.DefaultZero());

                        double meteerValue = Convert.ToDouble(itemvalue.Meter * totalQty);
                        orders.item = itemvalue.IName + "( Meter =" + meteerValue + " )";
                    }


                    orders.user = txtReference.Text.ToString();
                    //Pending
                    //    orders.SNO = 
                    //orders.item = db.Items.AsNoTracking().Where(x => x.IID == itemName.IID).FirstOrDefault().IName;
                    orders.Rate = Convert.ToDecimal(itemName.SalesPriceP);
                    orders.DiscountRs = Convert.ToDecimal(itemName.DisRs);
                    orders.DiscountPercentage = Convert.ToDecimal(itemName.DisP);
                    orders.Amount = Convert.ToDecimal(itemName.Amt);
                    orders.Ctn = Convert.ToDecimal(itemName.CTN);
                    orders.Pcs = Convert.ToDecimal(itemName.PCS);
                    orders.BatchNo = itemName.BatchNo.ToString();
                    orders.ExpDate = itemName.ExpireDate.ToString();
                    orders.SNO = sNO;
                    sNO++;
                    double ctn = Convert.ToDouble(db.Items.AsNoTracking().Where(x => x.IID == itemName.IID).FirstOrDefault().CTN_PCK);
                    double TOtalValue = 0;
                    if (ctn == 0)
                    {
                        TOtalValue = Convert.ToDouble(itemName.Qty);
                        TotalGross += Convert.ToDouble(itemName.SalesPriceP) * Convert.ToDouble(TOtalValue);
                    }
                    else
                    {
                        TOtalValue = (ctn * Convert.ToDouble(itemName.CTN)) + Convert.ToDouble(itemName.Qty);


                        TotalGross += Convert.ToDouble(itemName.SalesPriceP) * TOtalValue;


                    }
                    Amount += Convert.ToDouble(itemName.Amt);

                    orders.GrossAmt = TotalGrossAmount;
                    orders.DiscountTotal = DisountTotal;
                    //decimal DiscountDifference = Convert.ToDecimal(TotalGross) - Convert.ToDecimal(Amount);
                    orders.TotalDiscount = Convert.ToDecimal(TotalDiscount);  //(Convert.ToDecimal(itemName.SalesPriceP) * Convert.ToDecimal( TOtalValue))- Convert.ToDecimal(itemName.Amt);
                    orders.PREVBALANNCE = (decimal)order.PreBal;
                    orders.RCVDBALANCE = Convert.ToDecimal(order.CashAmt.ToString());
                    orderList.Add(orders);


                    SaleInvoice orders1 = new SaleInvoice();

                    orders1.GrossAmt = TotalGross;
                    orders1.DiscountTotal = Amount;
                    string refer = txtReference.Text.ToString();
                    orders1.user = refer;

                    // orders1.TotalDiscount =  Convert.ToDecimal(DiscountDifference) + Convert.ToDecimal(order.DisAmt);  //(Convert.ToDecimal(itemName.SalesPriceP) * Convert.ToDecimal( TOtalValue))- Convert.ToDecimal(itemName.Amt);

                    orderHeader.Add(orders1);

                    // add multiple company
                    var Companies = new Companies().GetCompanyID(compID);
                    orderList.ForEach(x =>
                    {
                        x.CompanyTitle = Companies.CompanyTitle;
                        x.CompanyPhone = Companies.CompanyPhone;
                        x.CompanyAddress = Companies.CompanyAddress;
                        x.CompanyID = Companies.CompanyID;
                    });

                    if (compID == 1017)
                    {

                        int companyId = Convert.ToInt32(cmbxCompanyAccount.SelectedValue.DefaultZero());

                        var getcompanyDetail = db.tbl_Company.AsNoTracking().Where(x => x.CompID == sale.diffCompany).FirstOrDefault();


                        orderList.ForEach(x =>
                        {
                            x.CompanyTitle = getcompanyDetail.Comp;
                            x.CompanyPhone = getcompanyDetail.Tel;
                            x.CompanyAddress = getcompanyDetail.Address;
                        });

                    }
                    //LocalReport localReport = new LocalReport();
                    //localReport.DataSources.Add(new ReportDataSource("DataSet1", orderList));
                    //localReport.DataSources.Add(new ReportDataSource("DataSet2", orderHeader));
                    ////localReport.ReportPath = "SkyIce.Report1.rdlc";
                    //localReport.ReportEmbeddedResource = "SalesMngmt.Reporting.Definition.SaleReceiptDisc.rdlc";
                    ////    C:\Users\hair\Desktop\New folder (5)\Setup\New folder\SalesMgmt\SalesMngmt\Reporting\Definition\SaleReceiptDisc.rdlc

                    //localReport.PrintToPrinter();

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
                    gl.RID = sale.RID;
                    gl.TypeCode = 5;
                    gl.GLDate = dtpInvoice.Value;
                    // gl.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                    gl.AC_Code = Convert.ToInt32(cmbxPaymentMode.SelectedValue);
                    gl.AC_Code2 = Convert.ToInt32(vebdorid);
                    gl.Narration = cmbxvendor.SelectedText + " has paid cash";//row.Cells[1].Value.ToString();
                                                                              //  gl.MOP_ID = 2;
                                                                              //gl.Qty_Out =// ctnledger + Convert.ToInt32(row.Cells[3].Value);
                                                                              //gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                                                                              //gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                                                                              //gl.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                                                                              //gl.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);


                    if (txtCashReceived.Text == "" || txtCashReceived.Text == "0")
                    {

                        gl.Debit = Convert.ToDouble(txtNetAm.Text.DefaultZero());

                    }
                    else
                    {

                        gl.Debit = Convert.ToDouble(txtCashReceived.Text.DefaultZero());

                    }
                    gl.Credit = 0;
                    gl.CompID = compID;
                    db.GL.Add(gl);
                    db.SaveChanges();

                    GL gl1 = new GL();
                    gl1.RID = sale.RID;
                    gl1.TypeCode = 5;
                    gl1.GLDate = dtpInvoice.Value;
                    //   gl1.IPrice = item.AveragePrice;
                    gl1.AC_Code = Convert.ToInt32(vebdorid); //item.AC_Code_Inv;
                    gl1.AC_Code2 = Convert.ToInt32(cmbxPaymentMode.SelectedValue);
                    gl1.Narration = cmbxvendor.SelectedText + " has paid cash";
                    //  gl.MOP_ID = 2;
                    //   gl1.Qty_Out = ctnledger + Convert.ToInt32(row.Cells[3].Value);
                    //  gl1.PAmt = Convert.ToDouble(item.AveragePrice.DefaultZero()) * (ctnledger + Convert.ToInt32(row.Cells[3].Value));//Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                    //  gl1.DisP = 0;// Convert.ToDouble(row.Cells[6].Value ?? "0");
                    // gl1.DisRs = 0; //Convert.ToDouble((row.Cells[7].Value ?? "0"));
                    // gl1.DisAmt = 0;// Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                    gl1.Debit = 0;

                    if (txtCashReceived.Text == "" || txtCashReceived.Text == "0")
                    {

                        gl1.Credit = Convert.ToDouble(txtNetAm.Text.DefaultZero());
                    }
                    else
                    {

                        gl1.Credit = Convert.ToDouble(txtCashReceived.Text.DefaultZero());

                    }



                    gl1.CompID = compID;
                    db.GL.Add(gl1);
                    db.SaveChanges();






                }
                transaction.Commit();
                #endregion

                //  UpdateItemRate();



                clear();

                cmbxPaymentMode.SelectedIndex = 0;
                // account balance
                var dsa = Convert.ToInt32(cmbxAccID.SelectedIndex);

                MessageBox.Show("Invoice Save Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


                txtInvoiceCustom.Text = "";



                if (printInvoice)
                {
                    Silent silent = new Silent();
                    ReportViewer reportViewer1 = new ReportViewer();
                    if (radioButton1.Checked)
                    {


                        // Printer.setDef(ConfigurationManager.AppSettings["Thermal"].ToString());
                        //     silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.rptSaleInvoiceThermal.rdlc");
                        if (orderList[0].CompanyID == 1013)
                        {
                            silent.Run(reportViewer1, orderList, "SalesMngmt.ThermalReport.NomanCompany1013.rdlc");
                        }
                        else
                        {

                            silent.Run(reportViewer1, orderList, "SalesMngmt.ThermalReport.rptSaleInvoiceThermal.rdlc");
                        }
                    }
                    else if (radioButton2.Checked)
                    {
                        //Printer.setDef(ConfigurationManager.AppSettings["A4"].ToString());

                        // A4 Size Print
                        Form2 form2 = new Form2(orderList);
                        form2.Show();
                        //return;

                        //silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.rptSaleInvPrint.rdlc");
                        //Printer.setDef(ConfigurationManager.AppSettings["Thermal"].ToString());
                    }
                    else if (radioButton4.Checked)
                    {
                        //Printer.setDef(ConfigurationManager.AppSettings["A4"].ToString());

                        // A5 Size Print
                        Form2 form2 = new Form2(orderList, "A5");
                        form2.Show();
                        //return;

                        //silent.Run(reportViewer1, orderList, "SalesMngmt.Reporting.Definition.rptSaleInvPrint.rdlc");
                        //Printer.setDef(ConfigurationManager.AppSettings["Thermal"].ToString());
                    }


                }
                printInvoice = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


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

                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getcustumerList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
                    }

                    FillCombo(cmbxvendor, customerList, "Name", "AC_Code", 0);



                    double balance = 0;
                    int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    var previosBalance = ReportsController.getCustomerPreviousBalance(dtpInvoice.Value, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = debit - credit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getcustomerLedgerSummaryByDate(dtpInvoice.Value, dtpInvoice.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



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


                if (value == 9)
                {

                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getVendorList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
                    }

                    FillCombo(cmbxvendor, customerList, "Name", "AC_Code", 0);

                    double balance = 0;
                    int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    var previosBalance = ReportsController.getVendorPreviousBalance(dtpInvoice.Value, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = credit - debit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getVendorLedgerSummaryByDate(dtpInvoice.Value, dtpInvoice.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



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
            if (keyData == (Keys.Up) && isGrid == true)
            {
                moveUp();
                return true;
            }
            if (keyData == (Keys.Down) && isGrid == true)
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
            else if (keyData == (Keys.Delete))
            {
                invGrid.Rows.RemoveAt(invGrid.SelectedRows[0].Index);
                invGrid_SelectionChanged(null, null);
                if (invGrid.Rows.Count > 0)
                {
                    invGrid.Rows[0].Selected = true;
                }
                isGrid = false;
                return true;
            }
            else if (keyData == (Keys.Control | Keys.S))
            {
                SaveRecord(lblRID.Text);
                isGrid = false;
                return true;
            }
            else if (keyData == (Keys.Control | Keys.P))
            {
                SaveRecord(lblRID.Text);
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
                if (invGrid.Rows.Count > 0)
                {
                    var itemID = invGrid.Rows[invGrid.SelectedRows[0].Index].Cells["ID"].Value;
                    var Pcs = invGrid.Rows[invGrid.SelectedRows[0].Index].Cells["Pcs"].Value.DefaultZero();
                    lblItemID.Text = itemID.ToString();
                    txtpcs.Text = Pcs;
                    txtpcs.Focus();
                }
                isGrid = false;
                return true;
            }
            else if (keyData == (Keys.Escape))
            {
                clear();
                isGrid = false;
                return true;
            }
            //else if (keyData == (Keys.Tab) && lastFocused != null)
            //{
            //    var item = listItems.Find(x => x.IName.ToLower().Trim() == cmbxItems.Text.ToLower().Trim());
            //    Items_Leave(item, lastFocused);
            //    return true;
            //}
            else if (keyData == (Keys.Enter) && txtCode.Text == "")
            {
                if (dataGridView1.Visible == true)
                {
                    var ID = dataGridView1.SelectedRows[0].Cells[0].Value;
                    var IID = Convert.ToInt32(ID);
                    lblItemID.Text = ID.ToString();
                    dataGridView1.Visible = false;
                    var items = db.Items.AsNoTracking().Where(x => x.IID == IID).FirstOrDefault();
                    if (items != null)
                    {
                        Items_Leave(items);
                        isGrid = false;
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

                        itemID = Convert.ToInt32(items.IID.ToString());
                        calculation();
                        metroButton1_Click(null, null); // Replacw with new 
                                                        //txtCode.Text = "";
                        cmbxItems.Focus();
                    }



                }
                //else if (lastFocused != null)
                //{
                //    var ID = dataGridView1.SelectedRows[0].Cells[0].Value;
                //    var IID = Convert.ToInt32(ID);
                //    var items = db.Items.Where(x => x.IID == IID).FirstOrDefault();
                //    if (items != null)
                //    {
                //        txtpcs.Text = txtpcs.Text.DefaultZero() == "0" ? "1" : txtpcs.Text;
                //        txtRate.Text = items.SalesPrice.DefaultZero();
                //        lblItemID.Text = items.IID.ToString();
                //        calculation();
                //        metroButton1_Click(null, null);
                //        isGrid = false;
                //        lastFocused = null;
                //    }
                //}
                return true;
            }
            else if (keyData == (Keys.Enter) && (txtCode.Text != ""))
            {
                String id = txtCode.Text;
                var items = db.Items.AsNoTracking().Where(x => x.BarcodeNo == id && x.CompanyID == compID).FirstOrDefault();
                if (items != null)
                {
                    //    if (items.IID.ToString() != label35.Text.DefaultZero())
                    //    {
                    //        label34.Text = "0";
                    //    }
                    //    label34.Text = (Convert.ToInt32(label34.Text.DefaultZero()) + 1).ToString();
                    //if (compID == 1024)
                    //{
                    //    txtctn.Text = "1";
                    //}
                    //else
                    //{
                    //    txtpcs.Text = "1";
                    //}

                    //txtRate.Text = items.SalesPrice.ToString();
                    //txtDisc.Text = items.DisR.DefaultZero().ToString();
                    //txtDisPer.Text = items.DisP.DefaultZero().ToString();
                    //cmbxItems.Text = items.IName.ToString();
                    ItemID();
                    //  label35.Text = items.IID.ToString();
                    lblItemID.Text = items.IID.ToString();

                    calculation();
                    metroButton1_Click(null, null); // Replacw with new 
                                                    //txtCode.Text = "";
                    txtCode.Focus();
                    txtCode.Text = "";
                    txtCode.Focus();
                    isGrid = false;
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

        public void receiptManagement(int Rid)
        {


            foreach (DataGridViewRow row in invGrid.Rows)
            {
                int id = Convert.ToInt32(row.Cells[0].Value);
                var item = db.Items.AsNoTracking().Where(x => x.IID == id).FirstOrDefault();

                double ctn;
                if (Convert.ToInt32(item.CTN_PCK.DefaultZero()) == 0)
                {
                    ctn = Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                }
                else
                {
                    ctn = Convert.ToDouble(item.CTN_PCK.DefaultZero()) * Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                }
                double Qty = ctn + Convert.ToDouble(row.Cells[3].Value);


                var recipe = db.tbl_Receipe.AsNoTracking().Where(x => x.compy == compID && x.isActive == false && x.ItemID == id).ToList();

                if (recipe == null)
                {

                }

                else
                {
                    foreach (var receipe in recipe)
                    {
                        var itemvalues = db.Items.AsNoTracking().Where(x => x.IID == receipe.RecipceItemId).FirstOrDefault();

                        Itemledger ledger = new Itemledger();
                        ledger.RID = Rid;
                        ledger.IID = itemvalues.IID;
                        ledger.EDate = dtpInvoice.Value;
                        // ledger.Bnid = BatchNo
                        ledger.TypeCode = 5;
                        ledger.AC_CODE = Convert.ToInt32(cmbxvendor.SelectedValue);
                        ledger.WID = (int)cmbxWareHouse.SelectedValue;
                        //jhjhjjjjjjjj

                        //ledger.WID = (int)cmbxWareHouse.SelectedValue;
                        //ledger.SID = (int)cmbxSaleMan.SelectedValue;


                        // ledger.SiD =
                        //    var ctnledger = Convert.ToDouble(item.CTN_PCK.DefaultZero()) * Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                        //ledger.CTN = Convert.ToDouble(row.Cells[2].Value.ToString());
                        //ledger.PCS = Convert.ToDouble(receipe.qty);



                        ledger.SJ = Convert.ToDouble(receipe.qty) * Qty;
                        ledger.PurPrice = Convert.ToDouble(itemvalues.AveragePrice.DefaultZero());
                        ledger.SalesPriceP = 0;//  ledger.Pamt = TotalAmount(pj * PurPrice)
                                               //  ledger.DisP = Convert.ToDouble(row.Cells[6].Value.DefaultZero());
                                               //ledger.DisAmount = percentage in rupess
                                               // ledger.DisRs = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        ledger.Amt = 0;
                        ledger.DisAmt = 0;
                        ledger.CompanyID = compID;

                        db.Itemledger.Add(ledger);
                        db.SaveChanges();

                        GL glv = new GL();
                        glv.RID = Rid;
                        glv.TypeCode = 5;

                        // jhkjhkjh
                        //gl.SID = (int)cmbxSaleMan.SelectedValue;

                        glv.GLDate = dtpInvoice.Value;
                        glv.IPrice = 0;
                        glv.AC_Code = itemvalues.AC_Code_Cost;
                        glv.AC_Code2 = itemvalues.AC_Code_Inv;
                        //  gl.Narration = row.Cells[1].Value.ToString();
                        //  gl.MOP_ID = 2;
                        glv.Qty_Out = /*ctn +*/ Convert.ToDouble(receipe.qty);
                        glv.PAmt = 0;
                        //gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        //gl.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        glv.DisAmt = 0;//Convert.ToDouble(txtSaleRate.Text);
                        glv.Debit = (Convert.ToDouble(receipe.qty) * Qty) * Convert.ToDouble(itemvalues.AveragePrice.DefaultZero());
                        glv.CompID = compID;
                        glv.Credit = 0;
                        db.GL.Add(glv);
                        db.SaveChanges();

                        GL glI = new GL();
                        glI.RID = Rid;
                        glI.TypeCode = 5;
                        //kljlkj
                        //gl1.SID = (int)cmbxSaleMan.SelectedValue;
                        glI.GLDate = dtpInvoice.Value;
                        glI.IPrice = Convert.ToDouble(itemvalues.AveragePrice.DefaultZero());
                        glI.AC_Code = itemvalues.AC_Code_Inv;
                        glI.AC_Code2 = itemvalues.AC_Code_Cost;
                        glI.Narration = cmbxvendor.SelectedText;
                        //  gl.MOP_ID = 2;
                        glI.Qty_Out = /*ctn +*/ Convert.ToDouble(receipe.qty) * Qty;
                        glI.PAmt = Convert.ToDouble(itemvalues.AveragePrice.DefaultZero()) * (/*ctn +*/ Convert.ToDouble(receipe.qty));//Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        glI.DisP = 0;// Convert.ToDouble(row.Cells[6].Value ?? "0");
                        glI.DisRs = 0; //Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        glI.DisAmt = 0;// Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        glI.Debit = 0;
                        glI.Credit = (Convert.ToDouble(receipe.qty) * Qty) * Convert.ToDouble(itemvalues.AveragePrice.DefaultZero());
                        glI.CompID = compID;
                        db.GL.Add(glI);
                        db.SaveChanges();

                    }

                }



            }



        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            //EditMessageBox messageBox = new EditMessageBox(SInvoice, compID, "SI");
            //messageBox.Show();
            List<SaleInvoice> orderList = new List<SaleInvoice>();
            int sNO = 1;
            double TotalGross = 0;
            double Amount = 0;
            double DisountTotal = 0;
            double TotalDiscount = 0;
            double DiscountDifference = 0;
            double TotalGrossAmount = 0;




            foreach (DataGridViewRow row in invGrid.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    SaleInvoice orders = new SaleInvoice();

                    int id = Convert.ToInt32(row.Cells[0].Value.ToString().DefaultZero());

                    var itemvalue = db.Items.AsNoTracking().Where(x => x.IID == id).FirstOrDefault();
                    if (itemvalue.Meter <= 0 || itemvalue.Meter == null)
                    {

                        //orders.user = txtReference.Text.ToString();
                        //Pending
                        //    orders.SNO = 
                        orders.item = itemvalue.IName;
                    }


                    else
                    {

                        double totalQty;
                        if (Convert.ToInt32(itemvalue.CTN_PCK.DefaultZero()) == 0)
                        {
                            totalQty = Convert.ToDouble(Convert.ToDecimal(row.Cells[2].Value.ToString().DefaultZero()));
                        }
                        else
                        {
                            totalQty = Convert.ToDouble(row.Cells[2].Value.ToString().DefaultZero()) * Convert.ToDouble(itemvalue.CTN_PCK);
                        }
                        totalQty += Convert.ToDouble(row.Cells[3].Value.ToString().DefaultZero());
                        double meteerValue = Convert.ToDouble(itemvalue.Meter * totalQty);
                        orders.item = itemvalue.IName + " ( meter =" + meteerValue + " )";
                    }





                    orders.Rate = Convert.ToDecimal(row.Cells[4].Value.ToString().DefaultZero());

                    orders.DiscountRs = Convert.ToDecimal(row.Cells[7].Value ?? "0");

                    orders.DiscountPercentage = Convert.ToDecimal(row.Cells[6].Value ?? "0");

                    orders.Amount = Convert.ToDecimal(row.Cells[8].Value.ToString().DefaultZero());
                    orders.Ctn = Convert.ToDecimal(row.Cells[2].Value.ToString().DefaultZero());
                    orders.Pcs = Convert.ToDecimal(row.Cells[3].Value.ToString().DefaultZero());
                    orders.BatchNo = (row.Cells[13].Value ?? "").ToString();
                    orders.ExpDate = (row.Cells[14].Value ?? "").ToString();
                    orders.SNO = sNO;
                    sNO++;
                    double ctn = Convert.ToDouble(db.Items.AsNoTracking().Where(x => x.IID == id).FirstOrDefault().CTN_PCK);
                    double TOtalValue = 0;
                    if (ctn == 0)
                    {
                        TOtalValue = Convert.ToDouble(row.Cells[3].Value.ToString().DefaultZero());
                        TotalGross += Convert.ToDouble(row.Cells[4].Value.ToString().DefaultZero()) * Convert.ToDouble(TOtalValue);
                    }
                    else
                    {
                        TOtalValue = (ctn * Convert.ToDouble(row.Cells[2].Value.ToString().DefaultZero())) + Convert.ToDouble(row.Cells[3].Value.ToString().DefaultZero());


                        TotalGross += Convert.ToDouble(row.Cells[4].Value.ToString().DefaultZero()) * TOtalValue;


                    }
                    Amount += Convert.ToDouble(row.Cells[8].Value.ToString().DefaultZero());



                    TotalGrossAmount = TotalGross;
                    DisountTotal = Amount;
                    DiscountDifference = Convert.ToDouble(TotalGross) - Convert.ToDouble(Amount);
                    TotalDiscount = Convert.ToDouble(DiscountDifference);  //(Convert.ToDecimal(itemName.SalesPriceP) * Convert.ToDecimal( TOtalValue))- Convert.ToDecimal(itemName.Amt);






                }
            }


            TotalDiscount += Convert.ToDouble(txtDisfooter.Text);
            DisountTotal -= Convert.ToDouble(txtDisfooter.Text);

            foreach (DataGridViewRow row in invGrid.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    SaleInvoice orders = new SaleInvoice();


                    int value = Convert.ToInt32(cmbxAccID.SelectedIndex);

                    if (value >= 1)
                    {
                        int account = Convert.ToInt32(cmbxAccID.SelectedValue);






                        if (account == 3)
                        {

                            char[] spearator = { '/' };

                            // using the method
                            String[] strlist = cmbxvendor.Text.Split(spearator);
                            var dd = strlist.Length;
                            if (strlist.Length == 3) { }


                            else
                            {
                                vebdorid = Convert.ToInt32(0);
                                cmbxvendor.Text = "";
                                cmbxvendor.Focus();

                                return;
                            }

                            string accode = "";
                            foreach (String s in strlist)
                            {
                                accode = s;
                            }

                            if (accode == "0" || accode == "" || accode == null)
                            {
                                vebdorid = Convert.ToInt32(0);
                                cmbxvendor.Text = "";
                                cmbxvendor.Focus();
                                return;


                            }
                            int code = Convert.ToInt32(accode);


                            var customer = db.Customers.AsNoTracking().Where(x => x.AC_Code == code && x.CompanyID == compID).FirstOrDefault();
                            if (customer == null)
                            {

                                MessageBox.Show("you selected wrong customer");
                                cmbxvendor.Text = "";
                                cmbxvendor.Focus();



                            }


                            else
                            {









                                vebdorid = Convert.ToInt32(customer.AC_Code);

                            }



                        }

                        if (account == 9)
                        {
                            char[] spearator = { '/' };

                            // using the method
                            String[] strlist = cmbxvendor.Text.Split(spearator);
                            var dd = strlist.Length;
                            if (strlist.Length == 3) { }


                            else
                            {
                                vebdorid = Convert.ToInt32(0);
                                cmbxvendor.Text = "";
                                cmbxvendor.Focus();

                                return;
                            }

                            string accode = "";
                            foreach (String s in strlist)
                            {
                                accode = s;
                            }

                            if (accode == "0" || accode == "" || accode == null)
                            {
                                vebdorid = Convert.ToInt32(0);
                                cmbxvendor.Text = "";
                                cmbxvendor.Focus();
                                return;


                            }
                            int code = Convert.ToInt32(accode);


                            var customer = db.Vendors.AsNoTracking().Where(x => x.AC_Code == code && x.CompanyID == compID).FirstOrDefault();
                            if (customer == null)
                            {

                                MessageBox.Show("you selected wrong customer");
                                cmbxvendor.Text = "";
                                cmbxvendor.Focus();



                            }



                            else
                            {








                                //int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                                //vebdorid = Convert.ToInt32(cmbxvendor.SelectedValue);
                                //var previosBalance = ReportsController.getCustomerPreviousBalance(DateTime.Today.AddDays(1), Vendorcode);
                                //int a = 1;

                                //double credit = (double)previosBalance.Rows[0]["credit"];
                                //double debit = (double)previosBalance.Rows[0]["debit"];
                                //double balance = credit - debit;



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





                    }





                    int accountCode = (int)cmbxAccID.SelectedValue;
                    if (accountCode == 1)
                    {

                        if (txtName.Text == "")
                        {
                            orders.Client = db.COA_D.AsNoTracking().Where(x => x.AC_Code == vebdorid).FirstOrDefault().AC_Title;
                            txtName.Text = "";
                        }
                        else
                        {
                            orders.Client = txtName.Text;
                            txtName.Text = "";

                        }

                    }
                    else
                    {
                        var clientNAmeAndCity = db.COA_D.AsNoTracking().Where(x => x.AC_Code == vebdorid).FirstOrDefault();
                        orders.Client = clientNAmeAndCity.AC_Title;

                        txtName.Text = "";

                        if (clientNAmeAndCity.CAC_Code == 3)
                        {
                            var customer = db.Customers.AsNoTracking().Where(x => x.AC_Code == vebdorid).FirstOrDefault();
                            int city = Convert.ToInt32(customer.City);
                            var cityName = db.tbl_city.AsNoTracking().Where(x => x.Id == city).FirstOrDefault();
                            if (cityName == null)
                            {
                                orders.City = "";
                            }
                            else
                            {

                                orders.City = cityName.CityName;

                            }

                        }

                        if (clientNAmeAndCity.CAC_Code == 9)
                        {
                            var customer = db.Vendors.AsNoTracking().Where(x => x.AC_Code == vebdorid).FirstOrDefault();
                            int city = Convert.ToInt32(customer.City);
                            var cityName = db.tbl_city.AsNoTracking().Where(x => x.Id == city).FirstOrDefault();
                            if (cityName == null)
                            {
                                orders.City = "";
                            }
                            else
                            {

                                orders.City = cityName.CityName;

                            }


                        }
                        //orders.Client = db.COA_D.Where(x => x.AC_Code == order.AC_Code).FirstOrDefault().AC_Title;
                        // DateTime dt = DateTime.ParseExact(order.EDate.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                        //   string s = order.EDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                        //   string s=     order.EDate.to().to;



                    }


                    DateTime dt = new DateTime();
                    dt = Convert.ToDateTime(dtpInvoice.Value);
                    orders.Time = dt.ToString("dd/MM/yyyy");
                    //orders.user = txtReference.Text.ToString();
                    //Pending
                    //    orders.SNO = 

                    int id = Convert.ToInt32(row.Cells[0].Value.ToString().DefaultZero());


                    var itemvalue = db.Items.AsNoTracking().Where(x => x.IID == id).FirstOrDefault();
                    if (itemvalue.Meter <= 0 || itemvalue.Meter == null)
                    {

                        //orders.user = txtReference.Text.ToString();
                        //Pending
                        //    orders.SNO = 
                        orders.item = itemvalue.IName;
                    }


                    else
                    {

                        double totalQty;
                        if (Convert.ToInt32(itemvalue.CTN_PCK.DefaultZero()) == 0)
                        {
                            totalQty = Convert.ToDouble(Convert.ToDecimal(row.Cells[2].Value.ToString().DefaultZero()));
                        }
                        else
                        {
                            totalQty = Convert.ToDouble(row.Cells[2].Value.ToString().DefaultZero()) * Convert.ToDouble(itemvalue.CTN_PCK.DefaultZero());
                        }

                        totalQty += Convert.ToDouble(row.Cells[3].Value.ToString().DefaultZero());

                        double meteerValue = Convert.ToDouble(itemvalue.Meter * totalQty);
                        orders.item = itemvalue.IName + " ( Meter =" + meteerValue + " )";
                    }




                    //orders.item = db.Items.AsNoTracking().Where(x => x.IID == id).FirstOrDefault().IName;


                    orders.Rate = Convert.ToDecimal(row.Cells[4].Value.ToString().DefaultZero());

                    orders.DiscountRs = Convert.ToDecimal(row.Cells[7].Value ?? "0");

                    orders.DiscountPercentage = Convert.ToDecimal(row.Cells[6].Value ?? "0");

                    orders.Amount = Convert.ToDecimal(row.Cells[8].Value.ToString().DefaultZero());
                    orders.Ctn = Convert.ToDecimal(row.Cells[2].Value.ToString().DefaultZero());
                    orders.Pcs = Convert.ToDecimal(row.Cells[3].Value.ToString().DefaultZero());
                    orders.BatchNo = (row.Cells[13].Value ?? "").ToString();
                    orders.ExpDate = (row.Cells[14].Value ?? "").ToString();
                    orders.SNO = sNO;
                    sNO++;
                    double ctn = Convert.ToDouble(db.Items.AsNoTracking().Where(x => x.IID == id).FirstOrDefault().CTN_PCK);
                    double TOtalValue = 0;
                    if (ctn == 0)
                    {
                        TOtalValue = Convert.ToDouble(row.Cells[3].Value.ToString().DefaultZero());
                        TotalGross += Convert.ToDouble(row.Cells[4].Value.ToString().DefaultZero()) * Convert.ToDouble(TOtalValue);
                    }
                    else
                    {
                        TOtalValue = (ctn * Convert.ToDouble(row.Cells[2].Value.ToString().DefaultZero())) + Convert.ToDouble(row.Cells[3].Value.ToString().DefaultZero());


                        TotalGross += Convert.ToDouble(row.Cells[4].Value.ToString().DefaultZero()) * TOtalValue;


                    }
                    Amount += Convert.ToDouble(row.Cells[8].Value.ToString().DefaultZero());





                    orders.GrossAmt = TotalGrossAmount;
                    orders.DiscountTotal = DisountTotal;
                    //decimal DiscountDifference = Convert.ToDecimal(TotalGross) - Convert.ToDecimal(Amount);
                    orders.TotalDiscount = Convert.ToDecimal(TotalDiscount);  //(Convert.ToDecimal(itemName.SalesPriceP) * Convert.ToDecimal( TOtalValue))- Convert.ToDecimal(itemName.Amt);
                    orders.PREVBALANNCE = Convert.ToDecimal(lblAccountBalance.Text);
                    orders.RCVDBALANCE = Convert.ToDecimal(txtCashReceived.Text);








                    orderList.Add(orders);

                }
            }




            // add multiple company
            var Companies = new Companies().GetCompanyID(compID);
            orderList.ForEach(x =>
            {
                x.CompanyTitle = Companies.CompanyTitle;
                x.CompanyPhone = Companies.CompanyPhone;
                x.CompanyAddress = Companies.CompanyAddress;
                x.CompanyID = Companies.CompanyID;
            });

            if (compID == 1017)
            {

                int companyId = Convert.ToInt32(cmbxCompanyAccount.SelectedValue.DefaultZero());

                var getcompanyDetail = db.tbl_Company.AsNoTracking().Where(x => x.CompID == companyId).FirstOrDefault();


                orderList.ForEach(x =>
                {
                    x.CompanyTitle = getcompanyDetail.Comp;
                    x.CompanyPhone = getcompanyDetail.Tel;
                    x.CompanyAddress = getcompanyDetail.Address;
                });

            }
            Form2 form2 = new Form2(orderList, "A5");
            form2.Show();
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
            var ID = Convert.ToInt32(lblItemID.Text.DefaultZero());
            int itemID = Convert.ToInt32(ItemID(ID));
            var item = db.Items.Where(x => x.IID == itemID).FirstOrDefault();
            if (item != null)
            {

                if (txtRate.Text == "" || txtRate.Text == "0")
                {


                    if (ddlWSR.SelectedValue.ToString() == "1")
                    {
                        txtRate.Text = item.SalesPrice.DefaultZero();
                    }
                    if (ddlWSR.SelectedValue.ToString() == "2") {
                        txtRate.Text = item.WholeSale.DefaultZero();
                    }

                    if (ddlWSR.SelectedValue.ToString() == "3")
                    {
                        txtRate.Text = item.RetailPOne.DefaultZero();
                    }

                 


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
                    // txtpcs.Text = 1.ToString();

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

                    txtSaleP.Text = (ctnValue + pcs).ToString();

                    var DiscPercValue = Convert.ToDouble(Convert.ToDouble(txtDisPer.Text.DefaultZero()) / 100 * Convert.ToDouble(txtNet.Text.DefaultZero()));

                    var DiscValue = Convert.ToDouble(txtDisc.Text.DefaultZero());


                    txtNet.Text = (Convert.ToDouble(txtSaleP.Text.DefaultZero()) - (DiscPercValue + DiscValue)).ToString();

                    // txtSaleP.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero())).ToString();

                    txtPcsRate.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero())).ToString();

                    txtSaleRate.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(1)).ToString();

                    lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text.DefaultZero()) + Convert.ToDouble(txtNetAm.Text.DefaultZero())).ToString();



                }

                else
                {

                    var ctnValue = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtctn.Text.DefaultZero()));

                    var pcs = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero()));
                    if (txtpcs.Text == "" || txtpcs.Text == "0")
                    {


                    }

                    else
                    {

                        if (item.CTN_PCK == 0 || item.CTN_PCK == null)
                        {
                            pcs = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble(txtpcs.Text.DefaultZero()));



                        }
                        else
                        {
                            var total = (Convert.ToDouble(txtRate.Text.DefaultZero()) / Convert.ToDouble(item.CTN_PCK.DefaultZero()));
                            pcs = ((total) * Convert.ToDouble(txtpcs.Text.DefaultZero()));

                        }


                    }


                    txtNet.Text = (ctnValue + pcs).ToString();

                    // txtNet.Text = txtRate.Text;
                    //  txtSaleP.Text = (ctnValue + pcs).ToString();

                    var DiscPercValue = Convert.ToDouble(Convert.ToDouble(txtDisPer.Text.DefaultZero()) / 100 * Convert.ToDouble(txtNet.Text.DefaultZero()));

                    var DiscValue = Convert.ToDouble(txtDisc.Text.DefaultZero());


                    txtNet.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) - (DiscPercValue + DiscValue)).ToString();

                    //    txtSaleP.Text = (Convert.ToDouble(txtRate.Text.DefaultZero()) * Convert.ToDouble((ctn + Convert.ToDouble(txtpcs.Text.DefaultZero())))).ToString();



                    txtPcsRate.Text = (Convert.ToDouble(txtNet.Text.DefaultZero()) / (ctn + Convert.ToDouble(txtpcs.Text.DefaultZero()))).ToString();

                    txtSaleRate.Text = (Convert.ToDouble(txtSaleP.Text.DefaultZero())).ToString(); /*/ (ctn + Convert.ToDouble(txtpcs.Text.DefaultZero()))).ToString();*/

                    lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text.DefaultZero()) + Convert.ToDouble(txtNetAm.Text.DefaultZero())).ToString();



                }
            }
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
                    itemID = Convert.ToInt32(item.IID.DefaultZero());
                    cmbxItems.Text = item.IName;
                    txtCode.Text = item.BarcodeNo;

                    int warehouseId = (int)cmbxWareHouse.SelectedValue;
                    var RR = ReportsController.getWareHouseStockByID(item.IID, compID, warehouseId);


                    lblStock.Text = RR.Rows[0]["total"].ToString();

                    if (ddlWSR.SelectedValue.ToString() == "1")
                    {
                        txtRate.Text = item.SalesPrice.DefaultZero();
                    }
                    else if (ddlWSR.SelectedValue.ToString() == "3") {
                        txtRate.Text = item.RetailPOne.DefaultZero();

                    }
                    else
                    {
                        txtRate.Text = item.WholeSale.DefaultZero();
                    }

                    txtDisc.Text = item.DisR.DefaultZero();
                    txtDisPer.Text = item.DisP.DefaultZero();
                    //if (compID == 1024)
                    //{
                    //    txtctn.Text = 1.ToString();
                    //}
                    //else
                    //{
                    //    txtpcs.Text = 1.ToString();

                    //}



                    string path = Application.StartupPath + "\\Img\\" + item.Img;
                    //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Img\\" + obj.BarCode_ID;


                    // label10.Text = tbl.img;
                    pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = Utillityfunctions.LoadImage(item.Img);// Image.FromFile(path);


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
                    else
                    {
                        cmbxPackaging.SelectedIndex = 1;
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
                    //if (compID == 1024)
                    //{
                    //    txtctn.Focus();

                    //}
                    //else
                    //{
                    //    txtpcs.Focus();
                    //}
                    txtctn.Focus();
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
            int warehouseId = (int)cmbxWareHouse.SelectedValue;
            var RR = ReportsController.getWareHouseStockByID(id, compID, warehouseId);

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
            int account = (int)cmbxAccID.SelectedValue;
            if (account == 0)
            {
                lblGrandTotal.Text = "0";

            }
            else
            {


                lblGrandTotal.Text = (Convert.ToDouble(lblAccountBalance.Text) + Convert.ToDouble(txtNetAm.Text)).ToString();
            }


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
            string Query = @"SELECT TOP 10 Items.IID , 
                            ISNULL(Article.ArticleNo, '0') as Article, 
                            Items.IName as Product,ISNULL(SalesPrice,0) as Price ,
                            ISNULL(Color, 0) as Color,
                            ISNULL(Size, 0) as Size,
                            ISNULL(IComp_M.Comp , '') as Comp,
                            ISNULL(Items.Formula , '') as Formula,
                            ISNULL(Items.Cabinet , '') as Cabinet ,
                            ISNULL(vw.total,0) as Stock                 
                            FROM Items
                            left join Article on Items.ArticleNoID = Article.ProductID
                            left join Colors on Items.Color = Colors.ColorID
                            left join Sizes on Items.Size = Sizes.SizeID
                            left join getWarehouseStocks_vw vw on vw.IID = Items.IID AND vw.CompanyID = @company AND vw.WID = @warehouseId
                            left join IComp_M on Items.CompID = IComp_M.CompID                            
                            where Items.CompanyID=@company AND Items.isDeleted='false' And(Items.IName like '%'+ @Param +'%'
						    OR Article.ArticleNo like '%'+ @Param +'%' OR Colors.Name like '%'+ @Param +'%' OR Sizes.SizeName like '%'+ @Param +'%'
                            OR IComp_M.Comp like '%'+ @Param +'%'
							OR Items.Formula like '%'+ @Param +'%'
                            OR Items.SalesPrice like '%'+ @Param +'%'
                            OR Items.Cabinet like '%'+ @Param +'%')";

            SqlCommand cmd = new SqlCommand(Query, SqlHelper.DefaultSqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@Param", SqlDbType.NVarChar).Value = cmbxItems.Text;
            cmd.Parameters.Add("@company", SqlDbType.Int).Value = compID;
            cmd.Parameters.Add("@warehouseId", SqlDbType.Int).Value = (int)cmbxWareHouse.SelectedValue;
            cmd.Parameters.Add("@Delete", SqlDbType.Bit).Value = false;

            //cmd.Parameters.AddWithValue("@Param", "shahzaib");
            var rows = SqlHelper.ExecuteDataset(cmd).Tables[0];
            var items = rows.ToList<Items>();


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

            if (cmbxItems.Text == "")
            {


                return;

            }


            if (lblItemID.Text != string.Empty && lblItemID.Text != "0")
            {

                txtCode.Text = "";



            }



            else
            {

                cmbxItems.Focus();

            }


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
                cmbxAccID.SelectedValue = Convert.ToInt32(customer.CAC_Code);

                cmbxvendor.SelectedValue = Convert.ToInt32(accode);
                // account headr
                //List<COA_M> article = new List<COA_M>();
                //article.Add(new COA_M { CAC_Code = 0, CATitle = "--SELECT--" });
                ////   article.AddRange(db.COA_M.AsNoTracking().Where(x => x.CAC_Code == customer.CAC_Code));

                //FillCombo(cmbxAccID, article, "CATitle", "CAC_Code", 0);


                //List<Lib.Entity.Customers> parties = new List<Lib.Entity.Customers>();
                //parties.Add(new Lib.Entity.Customers { AC_Code = 0, CusName = "--Select--" });
                //parties.AddRange(db.Customers.AsNoTracking().Where(x => x.Cell == txtContactNo.Text && x.CompanyID == compID).ToList());
                ////   var vendor = ;
                //FillCombo(cmbxvendor, parties, "CusName", "AC_Code", 0);


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
            else if (ddlWSR.SelectedValue.ToString() == "3") {
                label4.Text = "Distribution";
                label12.Text = "Distribution Price";

            }
            else
            {
                label12.Text = "Sale Price";
                label4.Text = "Retail Rate";
            }

                if (obj != null)
                {
                    Items_Leave(obj, null);
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

                char[] spearator = { '/' };

                // using the method
                String[] strlist = cmbxvendor.Text.Split(spearator);
                var dd = strlist.Length;
                if (strlist.Length == 3) { }


                else
                {
                    vebdorid = Convert.ToInt32(0);
                    cmbxvendor.Text = "";
                    cmbxvendor.Focus();

                    return;
                }

                string accode = "";
                foreach (String s in strlist)
                {
                    accode = s;
                }

                if (accode == "0" || accode == "" || accode == null)
                {
                    vebdorid = Convert.ToInt32(0);
                    cmbxvendor.Text = "";
                    cmbxvendor.Focus();
                    return;


                }
                int code = Convert.ToInt32(accode);


                var name = db.Customers.AsNoTracking().Where(x => x.AC_Code == code && x.CompanyID == compID).FirstOrDefault();

                //  var name = db.Customers.AsNoTracking().Where(x => x.CusName == cmbxvendor.Text.ToString() && x.CompanyID == compID && x.InActive == false).FirstOrDefault();
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
                char[] spearator = { '/' };

                // using the method
                String[] strlist = cmbxvendor.Text.Split(spearator);
                var dd = strlist.Length;
                if (strlist.Length == 3) { }


                else
                {
                    vebdorid = Convert.ToInt32(0);
                    cmbxvendor.Text = "";
                    cmbxvendor.Focus();

                    return;
                }

                string accode = "";
                foreach (String s in strlist)
                {
                    accode = s;
                }

                if (accode == "0" || accode == "" || accode == null)
                {
                    vebdorid = Convert.ToInt32(0);
                    cmbxvendor.Text = "";
                    cmbxvendor.Focus();
                    return;


                }
                int code = Convert.ToInt32(accode);


                var name = db.Vendors.AsNoTracking().Where(x => x.AC_Code == code && x.CompanyID == compID).FirstOrDefault();

                //   var name = db.Vendors.AsNoTracking().Where(x => x.VendName == cmbxvendor.Text.ToString() && x.CompanyID == compID && x.InActive == false).FirstOrDefault();
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

        private void cmbxvendor_Leave(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(cmbxAccID.SelectedIndex);

            if (value >= 1)
            {
                int account = Convert.ToInt32(cmbxAccID.SelectedValue);






                if (account == 3)
                {

                    char[] spearator = { '/' };

                    // using the method
                    String[] strlist = cmbxvendor.Text.Split(spearator);
                    var dd = strlist.Length;
                    if (strlist.Length == 3) { }


                    else
                    {
                        vebdorid = Convert.ToInt32(0);
                        cmbxvendor.Text = "";
                        cmbxvendor.Focus();

                        return;
                    }

                    string accode = "";
                    foreach (String s in strlist)
                    {
                        accode = s;
                    }

                    if (accode == "0" || accode == "" || accode == null)
                    {
                        vebdorid = Convert.ToInt32(0);
                        cmbxvendor.Text = "";
                        cmbxvendor.Focus();
                        return;


                    }
                    int code = Convert.ToInt32(accode);


                    var customer = db.Customers.AsNoTracking().Where(x => x.AC_Code == code && x.CompanyID == compID).FirstOrDefault();
                    if (customer == null)
                    {

                        MessageBox.Show("you selected wrong customer");
                        cmbxvendor.Text = "";
                        cmbxvendor.Focus();



                    }


                    else
                    {

                        int Vendorcode = Convert.ToInt32(customer.AC_Code);
                        double balance = 0;

                        var previosBalance = ReportsController.getCustomerPreviousBalance(dtpInvoice.Value, Vendorcode);
                        int a = 1;

                        double credit = (double)previosBalance.Rows[0]["credit"];
                        double debit = (double)previosBalance.Rows[0]["debit"];
                        balance = debit - credit;
                        if (balance != 0)
                        {


                        }
                        SaleManagerEntities db1 = new SaleManagerEntities();

                        var getdata = ReportsController.getcustomerLedgerSummaryByDate(dtpInvoice.Value, dtpInvoice.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



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
                        vebdorid = Convert.ToInt32(customer.AC_Code);

                    }



                }

                if (account == 9)
                {
                    char[] spearator = { '/' };

                    // using the method
                    String[] strlist = cmbxvendor.Text.Split(spearator);
                    var dd = strlist.Length;
                    if (strlist.Length == 3) { }


                    else
                    {
                        vebdorid = Convert.ToInt32(0);
                        cmbxvendor.Text = "";
                        cmbxvendor.Focus();

                        return;
                    }

                    string accode = "";
                    foreach (String s in strlist)
                    {
                        accode = s;
                    }

                    if (accode == "0" || accode == "" || accode == null)
                    {
                        vebdorid = Convert.ToInt32(0);
                        cmbxvendor.Text = "";
                        cmbxvendor.Focus();
                        return;


                    }
                    int code = Convert.ToInt32(accode);


                    var customer = db.Vendors.AsNoTracking().Where(x => x.AC_Code == code && x.CompanyID == compID).FirstOrDefault();
                    if (customer == null)
                    {

                        MessageBox.Show("you selected wrong customer");
                        cmbxvendor.Text = "";
                        cmbxvendor.Focus();



                    }



                    else
                    {

                        int Vendorcode = Convert.ToInt32(customer.AC_Code);

                        double balance = 0;

                        var previosBalance = ReportsController.getVendorPreviousBalance(dtpInvoice.Value, Vendorcode);
                        int a = 1;

                        double credit = (double)previosBalance.Rows[0]["credit"];
                        double debit = (double)previosBalance.Rows[0]["debit"];
                        balance = credit - debit;
                        if (balance != 0)
                        {


                        }
                        SaleManagerEntities db1 = new SaleManagerEntities();

                        var getdata = ReportsController.getVendorLedgerSummaryByDate(dtpInvoice.Value, dtpInvoice.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



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
            txtCode.Focus();
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
                    var previosBalance = ReportsController.getCustomerPreviousBalance(dtpInvoice.Value, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = debit - credit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getcustomerLedgerSummaryByDate(dtpInvoice.Value, dtpInvoice.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



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
                    //  vebdorid = Convert.ToInt32(customer.AC_Code);

                }


                if (account == 9)
                {

                    double balance = 0;
                    int Vendorcode = Convert.ToInt32(cmbxvendor.SelectedValue);
                    var previosBalance = ReportsController.getVendorPreviousBalance(dtpInvoice.Value, Vendorcode);
                    int a = 1;

                    double credit = (double)previosBalance.Rows[0]["credit"];
                    double debit = (double)previosBalance.Rows[0]["debit"];
                    balance = credit - debit;
                    if (balance != 0)
                    {


                    }
                    SaleManagerEntities db1 = new SaleManagerEntities();

                    var getdata = ReportsController.getVendorLedgerSummaryByDate(dtpInvoice.Value, dtpInvoice.Value, Vendorcode);//db.getVendorLedgerBYDate(dtTo.Value, dtFrom.Value,;



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


        private void txtCode_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            MultipleCompany company = new MultipleCompany(compID);
            company.Show();
        }

        private void cmbxPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxPaymentMode.SelectedIndex == 0)
            {
                txtCashReceived.Text = "0";
                txtCashReceived.ReadOnly = true;


            }

            else
            {
                txtCashReceived.ReadOnly = false;

            }
        }

        private void lblGrandTotal_Click(object sender, EventArgs e)
        {

        }

        private void txtCtnLeave(object sender, EventArgs e)
        {
            calculation();



        }

        private void txtPcsLeave(object sender, EventArgs e)
        {
            calculation();




        }

        private void cmbxWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbxPackaging_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculation();
        }

        private void btnCost_Click(object sender, EventArgs e)
        {
            if (invGrid.Rows.Count == 0)
            {
                MessageBox.Show("Please Add Items In Grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else {

                double totalAmount = 0;
                foreach (DataGridViewRow row in invGrid.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        int IID =Convert.ToInt32( row.Cells[0].Value.DefaultZero());
                     string ctnValue    = row.Cells[2].Value.ToString();
                        string PcsValus = row.Cells[3].Value.ToString();

                        var items = db.Items.AsNoTracking().Where(x => x.IID == IID).FirstOrDefault();


                        double value = Convert.ToDouble(PcsValus.DefaultZero());
                        double ctn;
                        if (Convert.ToDouble(items.CTN_PCK.DefaultZero()) == 0)
                        {
                            ctn = (Convert.ToDouble(ctnValue.DefaultZero()));
                        }
                        else
                        {
                            ctn = (Convert.ToDouble(ctnValue.DefaultZero()) * Convert.ToDouble(items.CTN_PCK.DefaultZero()));
                        }
                        //  ctn = (Convert.ToDouble(txtctn.Text.DefaultZero()) * Convert.ToDouble(item.CTN_PCK.DefaultZero()));
                       double totalQty = ctn + value ;
                        totalAmount = totalQty * Convert.ToDouble(items.AveragePrice.DefaultZero());

                        // txtRate.Text = row.Cells[4].Value.ToString();
                        // txtDisPer.Text = (row.Cells[6].Value ?? "0").ToString();
                        // txtDisc.Text = (row.Cells[7].Value ?? "0").ToString();
                        // txtSaleP.Text = (row.Cells[9].Value ?? "0").ToString();
                        // totalAmount += Convert.ToDecimal(row.Cells["netAm"].Value.DefaultZero());
                        // txtNet.Text = totalAmount.ToString();
                    }
                }
                


                    MessageBox.Show("Total COST = "+ totalAmount, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
        }
    }
    public class Items
    {
        public int IID { get; set; }
        public string Article { get; set; }
        public String Product { get; set; }
        public double Price { get; set; }
        public double Stock { get; set; }
        public int Color { get; set; }
        public int Size { get; set; }
        public String Comp { get; set; }
        public string Formula { get; set; }
        public string Cabinet { get; set; }
    }
}