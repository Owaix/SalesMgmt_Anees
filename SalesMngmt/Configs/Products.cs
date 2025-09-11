using Lib;
using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using Lib.Utilities;
using SalesMngmt.Reporting;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Products : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.Items> list = null;
        int compID = 0;
        public Products(int comID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = comID;
            this.Shown += Products_Shown;


            // ProductsDataGridView.Visible = false;
            // ProdBindingNavigator.Visible = false;
            //pnlMain.Show();
            //label3.Text = "ADD";
        }

        private async void Products_Shown(object sender, EventArgs e)
        {
            // Show form immediately (UI is responsive)
            // Disable the whole form while loading
            this.Enabled = false;
            pnlMain.Hide();

            try
            {
                // Load product list in background
                var list = await Task.Run(() => db.Items.AsNoTracking()
                                                        .Where(x => x.CompanyID == compID)
                                                        .ToList());
                ProdBindingSource.DataSource = list;

                // Load all combos
                await LoadCombosAsync();
            }
            finally
            {
                // Re-enable form after everything is ready
                this.Enabled = true;
            }
        }

        private async Task LoadCombosAsync()
        {
            // Units (static, no DB call)
            List<I_Unit> unitList = new List<I_Unit>
    {
        new I_Unit { IUnit = "PCS", unit_id = 1 },
        new I_Unit { IUnit = "CTN", unit_id = 2 }
    };
            FillCombo(cmbcUni, unitList, "IUnit", "unit_id", 1);
            cmbcUni.SelectedIndex = 0;

            // Database lookups in background
            var categories = await Task.Run(() => db.Items_Cat.Where(x => x.CompanyID == compID && x.isDeleted == false).ToList());
            var makers = await Task.Run(() => db.Item_Maker.Where(x => x.CompanyID == compID && x.IsDelete == false).ToList());
            var sizes = await Task.Run(() => db.Sizes.AsNoTracking().Where(x => x.CompanyID == compID && x.IsDeleted == false).ToList());
            var articles = await Task.Run(() => db.Article.AsNoTracking().Where(x => x.CompanyID == compID && x.IsDelete == false).ToList());
            var colors = await Task.Run(() => db.Colors.AsNoTracking().Where(x => x.CompanyID == compID && x.IsDeleted == false).ToList());
            var styles = await Task.Run(() => db.Styles.AsNoTracking().Where(x => x.CompanyID == compID && x.IsDeleted == false).ToList());
            var articleTypes = await Task.Run(() => db.ArticleTypes.AsNoTracking().Where(x => x.CompanyID == compID && x.IsDeleted == false).ToList());
            var warehouses = await Task.Run(() => db.tbl_Warehouse.AsNoTracking().Where(x => x.CompanyID == compID && x.isDelete == false).ToList());

            // Now update UI (on main thread)
            FillCombo(cmbxCat, categories, "Cat", "CatID", 1);
            FillCombo(cmbxMak, makers, "Name", "MakerId", 1);
            FillCombo(cmbxSizes, sizes, "SizeName", "SizeID", 1);
            FillCombo(cmbxArticle, articles, "ArticleNo", "ProductID", 1);
            FillCombo(cmbxColor, colors, "Name", "ColorID", 1);
            FillCombo(CmbxStylr, styles, "StyleName", "StyleID", 1);
            FillCombo(cmbxArticalType, articleTypes, "ArticleTypeName", "ArticleTypeID", 1);
            FillCombo(cmbxWareHouse, warehouses, "Warehouse", "WID", 1);
        }


        private void Products_Load(object sender, EventArgs e)
        {
         
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            ProdBindingSource.AddNew();
            pnlMain.Show();
            txtProdName.Focus();
            label3.Text = "ADD";
            cmbcUni.SelectedIndex = 0;
            chkNonInventory.Checked = false;
            chkIsActive.Checked = false;
            string path = Application.StartupPath + "\\Img\\124444444.png";
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(path);
        }

        public void openForm()
        {

            ProdBindingSource.AddNew();
            pnlMain.Show();
            txtProdName.Focus();
            label3.Text = "ADD";
            string path = Application.StartupPath + "\\Img\\124444444.png";
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(path);


        }
        private void lblEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Items obj = (Items)ProdBindingSource.Current;
                var item = db.Items.AsNoTracking().Where(x => x.IID == obj.IID && x.CompanyID == compID).FirstOrDefault();
                if (item != null)
                {
                    obj.BarCode_ID = item.BarCode_ID;
                }
                pnlMain.Show();
                txtProdName.Focus();
                label3.Text = "EDIT";
                string path = Application.StartupPath + "\\Img\\" + obj.BarCode_ID;
                //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Img\\" + obj.BarCode_ID;
                openFileDialog1.FileName = path;
                label21.Text = obj.BarCode_ID;
                chkNonInventory.Checked =Convert.ToBoolean( item.Inv_YN);
                chkIsActive.Checked = Convert.ToBoolean(item.isDeleted);
                pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                // pictureBox1.Image = Image.FromFile(path);
                pictureBox1.Image = Utillityfunctions.LoadImage(item.Img);
            }
            catch (Exception)
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\Img\\124444444.png");
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();

            pnlMain.Hide();

            // Cancel edits on the current item (go back to original values)
            ProdBindingSource.CancelEdit();
            //Lib.Entity.Items us = (Lib.Entity.Items)ProdBindingSource.Current;
            //if (us.IID == 0)
            //{
            //    ProdBindingSource.RemoveCurrent();
            //    ProdBindingSource.Clear();
            //    list = db.Items.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
            //    ProdBindingSource.DataSource = list;
            //}
            //else
            //{

            //    ProdBindingSource.Clear();
            //    list = db.Items.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
            //    ProdBindingSource.DataSource = list;

            //}

        }
        public string GenerateRandomNo()
        {
            int _min = 1000000;
            int _max = 9999999;
            Random _rdm = new Random();
            var BarcodeNo = _rdm.Next(_min, _max).ToString();
            bool Barcode = db.Items.AsQueryable().Any(record =>
                                      record.BarcodeNo == BarcodeNo
                                      && record.CompanyID == compID);
            if (Barcode)
            {
                GenerateRandomNo();
            }
            return BarcodeNo;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                save();
                return true;
            }
            else {
                return false;
            }

        }


            public void save()
        {

            List<GL> itemGL=new List<GL>();
            
            double checkStockQuantity;
            bool isAdd = true;
            SqlConnection con = null;
            SqlTransaction trans = null;

            var warehouse = cmbxWareHouse.SelectedValue;
            if (warehouse == null)
            {

                var warehouseID = db.tbl_Warehouse.AsNoTracking().Where(x => x.CompanyID == compID && x.isDelete == false).FirstOrDefault();

                if (warehouseID == null)
                {
                    tbl_Warehouse ware = new tbl_Warehouse();
                    ware.CompanyID = compID;
                    ware.Warehouse = "WareHouse1";
                    ware.isDelete = false;
                    db.tbl_Warehouse.Add(ware);
                    db.SaveChanges();

                    warehouseID = db.tbl_Warehouse.Where(x => x.CompanyID == compID).FirstOrDefault();
                    warehouse = warehouseID.WID;
                }

                else
                {
                    warehouse = warehouseID.WID;

                }
            }

            var Maxcapital = ReportsController.getMixACodeById(12, compID);
            int capital = 0;
            int a = (int)Maxcapital.Rows[0]["Min"];


            if (a == 0)
            {
                capital = (int)db.GetAc_Code(12).FirstOrDefault();
                COA_D coaD = new COA_D();
                coaD.CAC_Code = 12;
                coaD.PType_ID = 1;
                coaD.ZID = 0;
                coaD.AC_Code = capital;
                coaD.AC_Title = "Capital";
                coaD.DR = 0;
                coaD.CR = 0;
                coaD.Qty = 0;
                coaD.CompanyID = compID;
                coaD.InActive = false;
                db.COA_D.Add(coaD);
                db.SaveChanges();
                a = capital;

            }
            else
            {

                capital = a;

            }



            //DbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                if (txtProdName.Text == "")
                {
                    MessageBox.Show("Please Provide Product Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //else if (Convert.ToInt32(cmbxCat.SelectedValue) < 1)
                //{
                //    MessageBox.Show("Please Provide Category", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                else
                //if (true)
                {
                    Lib.Entity.Items obj = (Lib.Entity.Items)ProdBindingSource.Current;

                    //  var Currentobj = list.Find(x => x.IName == txtProdName.Text.Trim() && x.CompanyID == compID);

                    if (obj.IID == 0)
                    {
                        //var bar = list.Find(x => x.BarcodeNo == txtBarCode.Text.Trim());
                        //if (bar != null)
                        //{
                        //    //MessageBox.Show("BarcodeNo Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    //return;
                        //}

                        //bool isCodeExist = list.Any(record =>
                        //            record.IName == obj.IName &&
                        //            record.IID != obj.IID &&
                        //            record.CompanyID == compID);

                        bool BarcodeNo = db.Items.Any(record =>
                                           record.BarcodeNo == obj.BarcodeNo
                                           && record.CompanyID == compID);


                        if (obj.IName == "")
                        {
                            bool isCodeempty = list.Any(record =>
                                             record.IName == obj.IName &&
                                             record.IID != obj.IID
                                             && record.CompanyID == compID);

                        }

                        //else if (isCodeExist)
                        //{
                        //    MessageBox.Show("Product Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}

                        if (BarcodeNo)
                        {
                            MessageBox.Show("BarcodeNo Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        //bool isCodeExist = list.Any(record =>
                        //           record.IName == obj.IName &&
                        //           record.IID != obj.IID
                        //           && record.CompanyID == compID);

                        bool BarcodeNo = list.Any(record =>
                                           record.BarcodeNo == obj.BarcodeNo &&
                                           record.IID != obj.IID
                                           && record.CompanyID == compID);


                        if (obj.IName == "")
                        {
                            bool isCodeempty = list.Any(record =>
                                             record.IName == obj.IName &&
                                             record.IID != obj.IID
                                             && record.CompanyID == compID);

                        }

                        //else if (isCodeExist)
                        //{
                        //    MessageBox.Show("Product Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}

                        if (BarcodeNo)
                        {
                            MessageBox.Show("BarcodeNo Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    //   string CustomeBarcode;

                    //if (txtBarCode.Text == "") {
                    //     CustomeBarcode = txtBarCode.Text == "" ? GenerateRandomNo().ToString() : txtBarCode.Text;

                    //    bool BarcodeNo = db.Items.Any(record =>
                    //                                         record.BarcodeNo == CustomeBarcode
                    //                                         && record.CompanyID == compID);

                    //    if (BarcodeNo) {

                    //        GenerateRandomNo();
                    //    }
                    //}
                    //else {

                    //    CustomeBarcode = txtBarCode.Text;
                    //}

                    string path = Application.StartupPath;
                    string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                    string fileNameOnly = Path.GetFileNameWithoutExtension(filename);
                    string extension = Path.GetExtension(filename);

                    if (filename == null)
                    {
                        MessageBox.Show("Please select a valid image.");
                        return;
                    }
                    else
                    {
                        try
                        {
                            //if (!File.Exists(path + "\\Img\\" + filename))
                            //{
                            //    System.IO.File.Copy(openFileDialog1.FileName, path + "\\Img\\" + filename);
                            //}
                        }
                        catch (Exception ex)
                        {
                            var ds = "";
                        }
                    }

                  

                    if (obj.IID > 0)
                    {
                        isAdd = false;
                        itemGL = new List<GL>();
                        itemGL = db.GL.Where(x => x.AC_Code == obj.AC_Code_Inv && x.CompID == compID).ToList();

                    }

                    DataAccess access = new DataAccess();
                    COA coa = new COA();
                    String Inventorycode = "";

                    int[] vals = new int[3] { 14, 15, 4 };

                    for (int i = 0; i < vals.Length; i++)
                    {
                        if (isAdd)
                        {
                            coa.AC_Code = vals[i];
                            con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
                            con.Open();
                            trans = con.BeginTransaction();
                            Inventorycode = access.GetScalar("GetAc_Code", coa, con, trans);
                            //using (var db = new DistributionErpEntities())
                            //{
                            COA_D coaD = new COA_D();
                            coaD.CAC_Code = vals[i];
                            coaD.PType_ID = 1;
                            coaD.ZID = 0;
                            coaD.AC_Code = Convert.ToInt32(Inventorycode);
                            coaD.AC_Title = txtProdName.Text.Trim();
                            coaD.DR = 0;
                            coaD.CR = 0;
                            coaD.Qty = 0;
                            coaD.InActive = false;
                            coaD.CompanyID = compID;
                            db.COA_D.Add(coaD);

                            vals[i] = Convert.ToInt32(Inventorycode);
                        }

                    }

                    if (isAdd == false)
                    {
                        con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
                        con.Open();
                        trans = con.BeginTransaction();

                        var inc = db.COA_D.SingleOrDefault(x => x.AC_Code == obj.AC_Code_Inc && x.CompanyID == compID);
                        var inv = db.COA_D.SingleOrDefault(x => x.AC_Code == obj.AC_Code_Inv && x.CompanyID == compID);
                        var cost = db.COA_D.SingleOrDefault(x => x.AC_Code == obj.AC_Code_Cost && x.CompanyID == compID);

                        inc.AC_Title = txtProdName.Text.Trim();
                        inv.AC_Title = txtProdName.Text.Trim();
                        cost.AC_Title = txtProdName.Text.Trim();

                        inc.InActive = chkIsActive.Checked;
                        inv.InActive = chkIsActive.Checked;
                        cost.InActive = chkIsActive.Checked;
                        //  coa.AC_Code = inc.AC_Code;

                        // var inv = db.COA_D.Where(x => x.AC_Code == obj.AC_Code_Inv).FirstOrDefault();

                    }
                    db.SaveChanges();

                    if (isAdd)
                    {
                        GL gl = new GL();
                        gl.RID = 0;
                        gl.RID2 = 0;
                        gl.TypeCode = 0;
                        gl.GLDate = DateTime.Now;
                        gl.AC_Code = Convert.ToInt32(Inventorycode);
                        gl.AC_Code2 = capital;
                        gl.Narration = "Opening Entry";
                        gl.Qty_IN = Convert.ToDouble(txtOpenQ.Text.DefaultZero());
                        gl.Debit = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero());
                        gl.Credit = 0;
                        gl.CompID = compID;
                        db.GL.Add(gl);

                        ////capital credit
                        GL glCapital = new GL();
                        glCapital.RID = 0;
                        glCapital.RID2 = 0;
                        glCapital.TypeCode = 0;
                        glCapital.GLDate = DateTime.Now;
                        glCapital.AC_Code = capital;
                        glCapital.AC_Code2 = Convert.ToInt32(Inventorycode);
                        glCapital.Narration = "opening Item :" + txtProdName.Text.Trim();
                        glCapital.Qty_IN = Convert.ToDouble(txtOpenQ.Text.DefaultZero());
                        glCapital.Debit = 0;
                        glCapital.Credit = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero());
                        glCapital.CompID = compID;
                        db.GL.Add(glCapital);
                    }
                    else
                    {
                      
                        var gl = db.GL.FirstOrDefault(x => x.AC_Code == obj.AC_Code_Inv && x.TypeCode == 0 && x.CompID == compID);
                        // comment kr hy ishae     


                        if (itemGL.Count == 1)
                        {
                            gl.Qty_IN = Convert.ToDouble(txtOpenQ.Text.DefaultZero());
                            gl.Debit = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero());
                            gl.Credit = 0;

                        }
                        var glCapital = db.GL.FirstOrDefault(x => x.AC_Code2 == obj.AC_Code_Inv && x.TypeCode == 0 && x.CompID == compID);
                        gl.Narration = "Opening Entry";
                        glCapital.Narration = "opening Item :" + txtProdName.Text.Trim();

                        if (itemGL.Count == 1)
                        {
                            glCapital.Qty_IN = Convert.ToDouble(txtOpenQ.Text.DefaultZero());
                            glCapital.Debit = 0;
                            glCapital.Credit = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero());

                        }
                        //upper tak yeha tk
                    }
                    db.SaveChanges();
                    if (obj.IID == 0)
                    {
                        obj.IName = txtProdName.Text.Trim();
                        obj.Desc = txtDes.Text.Trim();
                        obj.OP_Qty = Convert.ToInt32(txtOpenQ.Text.DefaultZero());
                        obj.OP_Price = Convert.ToDouble(txtPurchase.Text.DefaultZero());
                        obj.PurPrice = Convert.ToDouble(txtPurchase.Text.DefaultZero());
                        obj.CompID = Convert.ToInt32(cmbxMak.SelectedValue);
                        obj.SCatID = Convert.ToInt32(cmbxCat.SelectedValue);
                        obj.Unit_ID = Convert.ToInt32(cmbcUni.SelectedValue);
                        obj.isDeleted = chkIsActive.Checked;
                        obj.RetailPrice = Convert.ToDouble(txtSale.Text.DefaultZero());
                        obj.SalesPrice = Convert.ToDouble(txtSale.Text.DefaultZero());
                        obj.DisR = Convert.ToDecimal(metroTextBox3.Text.DefaultZero());
                        obj.DisP = Convert.ToDecimal(txtDisP.Text.DefaultZero());
                        obj.CompanyID = compID;
                        obj.BarCode_ID = filename;
                        obj.CTN_PCK = Convert.ToInt32(txtpacking.Text.DefaultZero());
                        obj.ArticleNoID = Convert.ToInt32(cmbxArticle.SelectedValue);
                        obj.Color = Convert.ToInt32(cmbxColor.SelectedValue.DefaultZero()); //txtColor.Text;
                        obj.Size = Convert.ToInt32(cmbxSizes.SelectedValue.DefaultZero());
                        obj.BarcodeNo = txtBarCode.Text == "" ? GenerateRandomNo().ToString() : txtBarCode.Text;
                        obj.Inv_YN = chkNonInventory.Checked;
                        obj.AveragePrice = Convert.ToDouble(txtPurchase.Text.DefaultZero());
                        obj.Demand = Convert.ToDouble(txtDemand.Text.DefaultZero());
                        obj.ArticleTypeId = Convert.ToInt32(cmbxArticalType.SelectedValue.DefaultZero());
                        obj.Style = Convert.ToInt32(CmbxStylr.SelectedValue.DefaultZero());
                        obj.AC_Code_Cost = vals[1];
                        obj.AC_Code_Inc = vals[0];
                        obj.Img = Utillityfunctions.ToBase64(openFileDialog1.FileName, path + "\\Img\\" + filename);
                        obj.AC_Code_Inv = vals[2];
                        obj.Formula = txtFormula.Text;
                        obj.WholeSale = Convert.ToDecimal(txtWholeSale.Text.DefaultZero());
                        obj.WareHouseID = (int)warehouse;
                        obj.Cabinet = txtCabinet.Text;
                        obj.Meter = Convert.ToDouble(txtMeter.Text.DefaultZero());
                        obj.RetailPOne= Convert.ToDecimal(txtDistribution.Text.DefaultZero());
                        db.Items.Add(obj);
                    }
                    else
                    {
                        var result = db.Items.SingleOrDefault(b => b.IID == obj.IID);
                        if (result != null)
                        {
                            result.IName = txtProdName.Text.Trim();
                            result.Desc = txtDes.Text.Trim();
                            //comment krna hy

                            if (itemGL.Count == 1)
                            {
                                result.OP_Qty = Convert.ToInt32(txtOpenQ.Text.DefaultZero());
                                result.OP_Price = Convert.ToDouble(txtPurchase.Text.DefaultZero());
                                result.PurPrice = Convert.ToDouble(txtPurchase.Text.DefaultZero());
                            }
                            // upper tak




                            result.CompID = Convert.ToInt32(cmbxMak.SelectedValue);
                            result.CompanyID = compID;
                            result.SCatID = Convert.ToInt32(cmbxCat.SelectedValue);
                            result.Unit_ID = Convert.ToInt32(cmbcUni.SelectedValue);
                            result.isDeleted = chkIsActive.Checked;
                            result.RetailPrice = Convert.ToDouble(txtPurchase.Text.DefaultZero());
                            result.SalesPrice = Convert.ToDouble(txtSale.Text.DefaultZero());
                            result.DisR = Convert.ToDecimal(metroTextBox3.Text.DefaultZero());
                            result.DisP = Convert.ToDecimal(txtDisP.Text.DefaultZero());
                            result.ArticleNoID = Convert.ToInt32(cmbxArticle.SelectedValue);
                            result.Color = Convert.ToInt32(cmbxColor.SelectedValue.DefaultZero());//txtColor.Text;
                            result.Size = Convert.ToInt32(cmbxSizes.SelectedValue.DefaultZero());
                            result.BarcodeNo = txtBarCode.Text == "" ? GenerateRandomNo().ToString() : txtBarCode.Text;
                            result.Inv_YN = chkNonInventory.Checked;
                            //comment krna hy
                            if (itemGL.Count == 1)
                            {
                                result.AveragePrice = Convert.ToDouble(txtPurchase.Text.DefaultZero());
                            }
                            result.Demand = Convert.ToDouble(txtDemand.Text.DefaultZero());
                            result.ArticleTypeId = Convert.ToInt32(cmbxArticalType.SelectedValue.DefaultZero());
                            result.Style = Convert.ToInt32(CmbxStylr.SelectedValue.DefaultZero());
                            result.Formula = txtFormula.Text;
                            result.BarCode_ID = filename;
                            result.WareHouseID = (int)warehouse;
                            result.Cabinet = txtCabinet.Text;
                            result.WholeSale = Convert.ToDecimal(txtWholeSale.Text.DefaultZero());
                            result.CTN_PCK = Convert.ToInt32(txtpacking.Text.DefaultZero());
                            result.Img = Utillityfunctions.ToBase64(openFileDialog1.FileName, path + "\\Img\\" + filename);
                            result.Meter = Convert.ToDouble(txtMeter.Text.DefaultZero());
                            result.RetailPOne = Convert.ToDecimal(txtDistribution.Text.DefaultZero());
                        }
                    }
                    db.SaveChanges();

                    var CheckLedger = db.Items.AsNoTracking().Where(x => x.Inv_YN == false && x.IID == obj.IID && x.CompanyID == compID).FirstOrDefault();
                    if (CheckLedger == null)
                    {

                    }

                    else
                    {
                        if (isAdd)
                        {

                            Itemledger itemledger = new Itemledger();
                            itemledger.EDate = System.DateTime.Now;
                            itemledger.AC_CODE = capital;
                            itemledger.WID = (int)warehouse;
                            itemledger.SID = 1;
                            itemledger.IID = obj.IID;
                            itemledger.BNID = 1;
                            itemledger.TypeCode = 0;
                            itemledger.RID = 0;
                            itemledger.CompanyID = compID;
                            // itemledger.ExpDT = System.DateTime.Now;
                            itemledger.OPN = Convert.ToInt32(txtOpenQ.Text.DefaultZero());
                            itemledger.PurPrice = Convert.ToDouble(txtPurchase.Text.DefaultZero());
                            itemledger.PAmt = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero());
                            itemledger.Amt = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero());
                            itemledger.WID = (int)warehouse;
                            db.Itemledger.Add(itemledger);
                        }
                        else
                        {
                            var checkItemAvailable = db.Itemledger.AsNoTracking().Where(x => x.IID == obj.IID && x.TypeCode == 0).ToList();

                       
                            if (checkItemAvailable == null)
                            {
                                Itemledger itemledger = new Itemledger();
                                itemledger.EDate = System.DateTime.Now;
                                itemledger.AC_CODE = capital;
                                itemledger.WID = (int)warehouse;
                                itemledger.SID = 1;
                                itemledger.IID = obj.IID;
                                itemledger.BNID = 1;
                                itemledger.TypeCode = 0;
                                itemledger.RID = 0;
                                itemledger.CompanyID = compID;
                                // itemledger.ExpDT = System.DateTime.Now;
                                itemledger.OPN = Convert.ToInt32(txtOpenQ.Text.DefaultZero());
                                itemledger.PurPrice = Convert.ToDouble(txtPurchase.Text.DefaultZero());
                                itemledger.PAmt = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero());
                                itemledger.Amt = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero());
                                itemledger.WID = (int)warehouse;
                                db.Itemledger.Add(itemledger);
                            }

                            else
                            {

                                // Remove the entity
                                var itemsToRemove = db.Itemledger.Where(x => x.IID == obj.IID && x.TypeCode == 0);
                                db.Itemledger.RemoveRange(itemsToRemove);

                                // Save changes to the database
                                db.SaveChanges();

                                // Remove the entity
                        

                                Itemledger itemledger = new Itemledger();
                                itemledger.EDate = System.DateTime.Now;
                                itemledger.AC_CODE = capital;
                                itemledger.WID = (int)warehouse;
                                itemledger.SID = 1;
                                itemledger.IID = obj.IID;
                                itemledger.BNID = 1;
                                itemledger.TypeCode = 0;
                                itemledger.RID = 0;
                                itemledger.CompanyID = compID;
                                // itemledger.ExpDT = System.DateTime.Now;
                                itemledger.OPN = Convert.ToInt32(txtOpenQ.Text.DefaultZero());
                                itemledger.PurPrice = Convert.ToDouble(txtPurchase.Text.DefaultZero());
                                itemledger.PAmt = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero());
                                itemledger.Amt = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero());
                                itemledger.WID = (int)warehouse;
                                db.Itemledger.Add(itemledger);

                              


                            }



                        }
                        db.SaveChanges();
                    }


                    //pnlMain.Hide();
                    //list = db.Items.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                    //ProdBindingSource.DataSource = list;

                    MessageBox.Show("Product Add Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ProdBindingSource.AddNew();
                    pnlMain.Show();
                    txtProdName.Focus();
                   label3.Text = "ADD";
                    cmbcUni.SelectedIndex = 0;
                    chkNonInventory.Checked = false;
                    chkIsActive.Checked = false;
                    string paths = Application.StartupPath + "\\Img\\124444444.png";
                    pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = Image.FromFile(paths);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        /*
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProdName.Text))
            {
                MessageBox.Show("Please Provide Product Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Convert.ToInt32(cmbxCat.SelectedValue) < 1)
            {
                MessageBox.Show("Please Provide Category", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool isAdd = true;
            int warehouseID = GetOrCreateWarehouseID();
            int capitalAccount = GetOrCreateCapitalAccount();
            Lib.Entity.Items obj = (Lib.Entity.Items)ProdBindingSource.Current;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    try
                    {
                        // Check and handle existing product
                        if (obj.IID > 0)
                        {
                            isAdd = false;
                            UpdateExistingProduct(obj, trans);
                        }
                        else
                        {
                            if (IsDuplicateBarcode(obj.BarcodeNo))
                            {
                                MessageBox.Show("BarcodeNo Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Create new COA_D records
                            int[] accountCodes = CreateCOADRecords(obj, trans, con);
                            obj.AC_Code_Cost = accountCodes[1];
                            obj.AC_Code_Inc = accountCodes[0];
                            obj.AC_Code_Inv = accountCodes[2];

                            // Add new item
                            AddNewItem(obj, warehouseID, trans);
                        }

                        // Update General Ledger
                        UpdateGeneralLedger(obj, capitalAccount, isAdd, trans);

                        // Update Item Ledger
                        UpdateItemLedger(obj, warehouseID, capitalAccount, isAdd, trans);

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            pnlMain.Hide();
            RefreshProductList();
        }

        private int GetOrCreateWarehouseID()
        {
            var warehouse = cmbxWareHouse.SelectedValue;
            if (warehouse == null)
            {
                var warehouseID = db.tbl_Warehouse.AsNoTracking().FirstOrDefault(x => x.CompanyID == compID && x.isDelete==false);
                if (warehouseID == null)
                {
                    var newWarehouse = new tbl_Warehouse
                    {
                        CompanyID = compID,
                        Warehouse = "WareHouse1",
                        isDelete = false
                    };
                    db.tbl_Warehouse.Add(newWarehouse);
                    db.SaveChanges();
                    return newWarehouse.WID;
                }
                return warehouseID.WID;
            }
            return (int)warehouse;
        }

        private int GetOrCreateCapitalAccount()
        {
            var maxCapital = ReportsController.getMixACodeById(12, compID);
            int capital = (int)maxCapital.Rows[0]["Min"];
            if (capital == 0)
            {
                capital = (int)db.GetAc_Code(12).FirstOrDefault();
                var coaD = new COA_D
                {
                    CAC_Code = 12,
                    PType_ID = 1,
                    ZID = 0,
                    AC_Code = capital,
                    AC_Title = "Capital",
                    DR = 0,
                    CR = 0,
                    Qty = 0,
                    CompanyID = compID,
                    InActive = false
                };
                db.COA_D.Add(coaD);
                db.SaveChanges();
            }
            return capital;
        }

        private void UpdateExistingProduct(Lib.Entity.Items obj, SqlTransaction trans)
        {
            var existingProduct = db.Items.SingleOrDefault(b => b.IID == obj.IID);
            if (existingProduct != null)
            {
                // Update existing fields
                existingProduct.IName = txtProdName.Text.Trim();
                existingProduct.Desc = txtDes.Text.Trim();
                // Other fields...

                db.SaveChanges();
            }
        }

        private int[] CreateCOADRecords(Lib.Entity.Items obj, SqlTransaction trans, SqlConnection con)
        {
            DataAccess access = new DataAccess();
            COA coa = new COA();
            String Inventorycode = "";
            int[] vals = new int[3] { 14, 15, 4 };
            for (int i = 0; i < vals.Length; i++)
            {
                coa.AC_Code = vals[i];
                Inventorycode = access.GetScalar("GetAc_Code", coa, con, trans);
                var coaD = new COA_D
                {
                    CAC_Code = vals[i],
                    PType_ID = 1,
                    ZID = 0,
                    AC_Code = Convert.ToInt32(Inventorycode),
                    AC_Title = txtProdName.Text.Trim(),
                    DR = 0,
                    CR = 0,
                    Qty = 0,
                    InActive = false,
                    CompanyID = compID
                };
                db.COA_D.Add(coaD);
                db.SaveChanges();
                vals[i] = Convert.ToInt32( coaD.AC_Code);
            }
            return vals;
        }

        private void AddNewItem(Lib.Entity.Items obj, int warehouseID, SqlTransaction trans)
        {
            obj.IName = txtProdName.Text.Trim();
            obj.Desc = txtDes.Text.Trim();
            // Other fields...
            obj.WareHouseID = warehouseID;
            db.Items.Add(obj);
            db.SaveChanges();
        }

        private void UpdateGeneralLedger(Lib.Entity.Items obj, int capitalAccount, bool isAdd, SqlTransaction trans)
        {
            if (isAdd)
            {
                var gl = new GL
                {
                    RID = 0,
                    RID2 = 0,
                    TypeCode = 0,
                    GLDate = DateTime.Now,
                    AC_Code = obj.AC_Code_Inv,
                    AC_Code2 = capitalAccount,
                    Narration = "Opening Entry",
                    Qty_IN = Convert.ToDouble(txtOpenQ.Text.DefaultZero()),
                    Debit = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurchase.Text.DefaultZero()),
                    Credit = 0,
                    CompID = compID
                };
                db.GL.Add(gl);
                // Capital Credit entry...
            }
            else
            {
                var gl = db.GL.SingleOrDefault(x => x.AC_Code == obj.AC_Code_Inv && x.TypeCode == 0 && x.CompID == compID);
                if (gl != null)
                {
                    gl.Narration = "Opening Entry";
                    // Update other GL fields...
                    db.SaveChanges();
                }
            }
        }

        private void UpdateItemLedger(Lib.Entity.Items obj, int warehouseID, int capitalAccount, bool isAdd, SqlTransaction trans)
        {
            if (isAdd)
            {
                var itemLedger = new Itemledger
                {
                    EDate = DateTime.Now,
                    AC_CODE = capitalAccount,
                    WID = warehouseID,
                    SID = 1,
                    IID = obj.IID,
                    BNID = 1,
                    TypeCode = 0,
                    RID = 0,
                    CompanyID = compID,
                    OPN = Convert.ToInt32(txtOpenQ.Text.DefaultZero()),
                    PurPrice = Convert.ToDouble(txtPurOPen.Text.DefaultZero()),
                    PAmt = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurOPen.Text.DefaultZero()),
                    Amt = Convert.ToDouble(txtOpenQ.Text.DefaultZero()) * Convert.ToDouble(txtPurOPen.Text.DefaultZero())
                };
                db.Itemledger.Add(itemLedger);
                db.SaveChanges();
            }
            else
            {
                var existingLedger = db.Itemledger.SingleOrDefault(x => x.IID == obj.IID && x.TypeCode == 0);
                if (existingLedger != null)
                {
                    // Update existing ledger entries
                }
            }
        }

        private bool IsDuplicateBarcode(string barcode)
        {
            return db.Items.Any(record => record.BarcodeNo == barcode && record.CompanyID == compID);
        }

        private void RefreshProductList()
        {
            list = db.Items.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
            ProdBindingSource.DataSource = list;
        }



        */

        private void PartyTypeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }


        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected = 1)
        {
            try
            {
                comboBox.DataSource = obj;
                comboBox.DisplayMember = Name; // Column Name
                comboBox.ValueMember = ID;  // Column Name
                comboBox.SelectedValue = selected;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {

        }

        private void txtDebit_KeyPress(object sender, KeyPressEventArgs e)
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

        private void metroButton1_Click(object sender, EventArgs e)
        {

            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label21.Text = openFileDialog1.FileName;
                        pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload image.");
                }
            }
            catch (Exception ex)
            {
                //it will give if file is already exits..
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0)
                {
                    ProdBindingSource.Clear();
                    list = db.Items.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                    ProdBindingSource.DataSource = list;

                }
                else
                {
                    //ProductsDataGridView.

                    list = db.Items.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                    ProdBindingSource.DataSource = list;
                    //ProductsDataGridView.DataSource = list.FindAll(x => x.IName.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                    var listitem = list.FindAll(x => x.IName.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));


                    ProdBindingSource.Clear();
                    ProdBindingSource.DataSource = listitem;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ProductsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ProdBindingNavigator_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorCountItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorSeparator_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoProdextItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBoxFind_Click(object sender, EventArgs e)
        {

        }

        private void lblSiz_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmbxMak_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbxMaker_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void txtSale_Click(object sender, EventArgs e)
        {

        }

        private void txtDisP_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void txtOpenQ_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void cmbxCostingType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtTell_Click(object sender, EventArgs e)
        {

        }

        private void txtRetail_Click(object sender, EventArgs e)
        {

        }

        private void cmbxCostongType_Click(object sender, EventArgs e)
        {

        }

        private void metroTabPage3_Click(object sender, EventArgs e)
        {

        }

        private void txtCabinet_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void cmbxWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtWeight_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void txtWholeSale_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void txtFormula_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void CmbxStylr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbxSizes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbxColor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void cmbxArticalType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtDemand_Click(object sender, EventArgs e)
        {

        }

        private void lblDemand_Click(object sender, EventArgs e)
        {

        }

        private void txtAveragePrice_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtDes_Click(object sender, EventArgs e)
        {

        }

        private void txtPurchase_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void cmbxCategory_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbxCat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtProdName_Click(object sender, EventArgs e)
        {

        }

        private void chkNonInventory_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void NonInventory_Click(object sender, EventArgs e)
        {

        }

        private void cmbxArticle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtpacking_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void txtBarCode_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void txtOpenP_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtPurOPen_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cmbxSid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbcUni_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void cmbxUnit_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void txtMeter_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void ProdBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void txtMeter_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
