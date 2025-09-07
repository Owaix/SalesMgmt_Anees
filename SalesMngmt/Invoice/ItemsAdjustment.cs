using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using SalesMngmt.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class ItemsAdjustment : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<getItemAdjustment_Result> list = null;
        int compID = 0;
        int ItemID = 0;
        int Rid = 0;

        int accountCode = 0;
     
        public ItemsAdjustment(int comID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = comID;
        }

        private void Products_Load(object sender, EventArgs e)
        {
            pnlMain.Hide();
            var list = ReportsController.getItemAdjustmen(dtTo.Value,dtFrom.Value,compID);
             getItemAdjustmentResultBindingSource.DataSource = list;

            var item = db.Items.Where(x => x.CompanyID == compID && x.isDeleted == false).ToList();

            List<CustomerList> customerList = new List<CustomerList>();
            customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
            foreach (var itemValue in item) {

                CustomerList cl = new CustomerList();
                cl.AC_Code = itemValue.AC_Code_Inv;
                cl.Name= itemValue.IName + "/" + itemValue.AC_Code_Inv;
                customerList.Add(cl);
            }

            FillCombo(cmbxProduct, customerList, "Name", "AC_Code", 0);
          
           FillCombo(cmbxWarehouse, db.tbl_Warehouse.Where(x=>x.CompanyID==compID && x.isDelete==false).ToList(), "Warehouse", "WID", 0);



            




            cmbxAdjustment.Items.Insert(0,"INPUT");
            cmbxAdjustment.Items.Insert(1, "OUTPUT");

            cmbxAdjustment.SelectedIndex = 0;


            var Account = db.COA_M.ToList();


            var vendor = db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID==compID && x.InActive==false).ToList();


            List<COA_M>  m = new List<COA_M>();
            m.Add(new COA_M { CAC_Code = 0, CATitle = "--Select--" });
            m.AddRange(db.COA_M.ToList());
            FillCombo(cmbxHeadAccount, m, "CATitle", "CAC_Code", 0);


            List<COA_D> d = new List<COA_D>();
            d.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
            d.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID==compID && x.InActive==false).ToList());
            FillCombo(cmbxAccount, d, "AC_Title", "AC_Code", 0);
         

       


        

        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            Rid = 0;
            clear();
            //getItemAdjustmentBindingSource.AddNew();
            pnlMain.Show();
         //   txtProdName.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            // Lib.Entity.Item obj = (Lib.Entity.Item)ProdBindingSource.Current;

            //   Lib.Entity.getItemAdjustment_Result obj = (Lib.Entity.getItemAdjustment_Result)getItemAdjustmentBindingSource.Current;

            if (Rid == 0)
            {

                MessageBox.Show("Please Select Row First Thanks");
            }
            else
            {
           
                //  txtProdName.Focus();
                label3.Text = "EDIT";
                var getAdj = db.Adj_M.AsNoTracking().Where(x => x.RID == Rid).FirstOrDefault();
              //  int cmbxAcc = Convert.ToInt32(cmbxAccount.SelectedValue);
                var headId = db.COA_D.AsNoTracking().Where(x => x.AC_Code == getAdj.AC_Code).FirstOrDefault();

                cmbxHeadAccount.SelectedValue = headId.CAC_Code;
                cmbxAccount.SelectedValue = getAdj.AC_Code;


                pnlMain.Show();

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            Rid = 0;
            pnlMain.Hide();
            clear();

            var list = ReportsController.getItemAdjustmen(dtTo.Value, dtFrom.Value, compID);
            getItemAdjustmentResultBindingSource.DataSource = list;
            //   Lib.Entity.getItemAdjustment_Result us = (Lib.Entity.getItemAdjustment_Result)getItemAdjustmentBindingSource.Current;
            //if (us.RID == 0)
            //{
            //    ProdBindingSource.RemoveCurrent();
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Validation();
            //SqlConnection con = null;
            //SqlTransaction trans = null;
            DbContextTransaction transaction = null;
          
            try
            {
                if (cmbxAccount.Text == "--Select--") {

                    MessageBox.Show("Please select SUB Account");
                    return;

                }
                if (cmbxHeadAccount.Text == "--Select--")
                {

                    MessageBox.Show("Please select  Account");
                    return;

                }
                if (cmbxProduct.Text== "--Select--") {

                    MessageBox.Show("Please select  Product");
                    return;
                }
            
                if (Convert.ToInt32(cmbxWarehouse.SelectedValue) == 0 || cmbxWarehouse.SelectedValue == null)
            {

                MessageBox.Show("Please select  Warehouse");
                return;
            }
            int ItemCode = Convert.ToInt32(ItemID);
                var getITemValue = db.Items.Where(x => x.AC_Code_Inv == ItemCode).FirstOrDefault();


                var stockid = ReportsController.getStockByID(getITemValue.IID, compID);
                //    transaction = db.Database.BeginTransaction();

                //  trans = con.BeginTransaction();
                if (Rid == 0)
                {
                    transaction = db.Database.BeginTransaction();

                    var a = cmbxAdjustment.SelectedIndex;
                    var obj = new Adj_M();
                    obj.AC_Code = Convert.ToInt32(accountCode);
                    obj.CompID = compID;
                    obj.EDate = dtAdjustment.Value;
                    obj.Rem = txtDes.Text;
                    obj.WID = Convert.ToInt32(cmbxWarehouse.SelectedValue);

                    db.Adj_M.Add(obj);
                    db.SaveChanges();



                   

                    Adj_D adjD = new Adj_D();

                    adjD.RID = obj.RID;
                    adjD.IID = getITemValue.IID;
                    if (Convert.ToUInt32(cmbxAdjustment.SelectedIndex) == 0)
                    {

                        adjD.Qty = Convert.ToInt32(txtQty.Text);
                        adjD.PurPrice = Convert.ToInt32(txtPrice.Text);
                        adjD.Debit = Convert.ToInt32(txtQty.Text) * Convert.ToInt32(txtPrice.Text);

                    }
                    else if (Convert.ToUInt32(cmbxAdjustment.SelectedIndex) == 1)
                    {

                        adjD.Qty2 = Convert.ToInt32(txtQty.Text);
                        adjD.PurPrice = Convert.ToInt32(txtPrice.Text);
                        adjD.Credit = Convert.ToInt32(txtQty.Text) * Convert.ToInt32(txtPrice.Text);

                    }
                    db.Adj_D.Add(adjD);

                    Itemledger ledger = new Itemledger();

                    ledger.IID = getITemValue.IID;
                    ledger.EDate = dtAdjustment.Value;
                    ledger.TypeCode = 13;
                    ledger.WID = Convert.ToInt32(cmbxWarehouse.SelectedValue);
                    ledger.SID = 1;
                    ledger.AC_CODE = Convert.ToInt32(accountCode);
                    ledger.RID = obj.RID;
                    ledger.CompanyID = compID;
                    if (Convert.ToInt32(cmbxAdjustment.SelectedIndex) == 0)
                    {
                        ledger.ADJ_IN = Convert.ToInt32(txtQty.Text);
                        ledger.PurPrice = Convert.ToInt32(txtPrice.Text);
                        ledger.Amt = Convert.ToInt32(txtQty.Text) * Convert.ToInt32(txtPrice.Text);

                    }
                    else if (Convert.ToInt32(cmbxAdjustment.SelectedIndex) == 1)
                    {

                        ledger.ADJ_OUT = Convert.ToInt32(txtQty.Text);
                        ledger.PurPrice = Convert.ToInt32(txtPrice.Text);
                        ledger.Amt = Convert.ToInt32(txtQty.Text) * Convert.ToInt32(txtPrice.Text);

                    }
                    db.Itemledger.Add(ledger);

                    GL gl = new GL();
                    gl.TypeCode = 13;

                    if (Convert.ToInt32(cmbxAdjustment.SelectedIndex) == 0)
                    {
                        gl.AC_Code = Convert.ToInt32(ItemID);
                        gl.AC_Code2 = Convert.ToInt32(accountCode);
                        gl.CompID= compID;
                        gl.TypeCode = 13;
                        gl.Debit = Convert.ToDouble(txtAmt.Text);
                        gl.Credit = 0;
                        gl.Narration = txtDes.Text;
                        gl.RID = obj.RID;
                        gl.GLDate = dtAdjustment.Value;
                        gl.MOP_ID = 7;
                        db.GL.Add(gl);



                        GL gl2 = new GL();
                        gl2.TypeCode = 13;
                        gl2.AC_Code = Convert.ToInt32(accountCode);
                        gl2.AC_Code2 = Convert.ToInt32(ItemID);
                        gl2.Narration = txtDes.Text;
                        gl2.Debit = 0;
                        gl2.Credit = Convert.ToDouble(txtAmt.Text);
                        gl2.RID = obj.RID;
                        gl2.CompID = compID;
                        gl2.GLDate = dtAdjustment.Value;
                        gl2.MOP_ID = 7;
                        db.GL.Add(gl2);
                    }
                    else if (Convert.ToInt32(cmbxAdjustment.SelectedIndex) == 1)
                    {
                        gl.AC_Code = Convert.ToInt32(accountCode);
                        gl.AC_Code2 = Convert.ToInt32(ItemID);
                        gl.TypeCode = 13;
                        gl.Debit = Convert.ToDouble(txtAmt.Text); 
                        gl.Credit = 0;
                        gl.Narration = txtDes.Text;
                        gl.RID = obj.RID;
                        gl.GLDate = dtAdjustment.Value;
                        gl.CompID = compID;
                        gl.MOP_ID = 7;
                        db.GL.Add(gl);




                        GL gl2 = new GL();
                        gl2.TypeCode = 13;
                        gl2.AC_Code = Convert.ToInt32(ItemID);
                        gl2.AC_Code2 = Convert.ToInt32(accountCode);
                        gl2.Narration = txtDes.Text;
                        gl2.Debit = 0;
                        gl2.Credit = Convert.ToDouble(txtAmt.Text);
                        gl2.RID = obj.RID;
                        gl2.GLDate = dtAdjustment.Value;
                        gl2.MOP_ID = 7;
                        gl2.CompID = compID;
                        db.GL.Add(gl2);

                    }
                    db.SaveChanges();

                    //if (cmbxAdjustment.SelectedIndex == 0)
                    //{
                    //    int id = Convert.ToInt32(cmbxProduct.SelectedValue);
                    //    var itemTable = db.Items.AsNoTracking().Where(x => x.AC_Code_Inv == id).FirstOrDefault();
                    //  //  var stockid = ReportsController.getStockByID(getITemValue.IID, compID);
                    //    double totalStock = Convert.ToDouble(Convert.ToDouble(stockid.DefaultZero()) + Convert.ToDouble(txtQty.Text));
                    //    double totalvalue = Convert.ToDouble(txtAmt.Text);
                    //    double averagevale = Convert.ToDouble(itemTable.AveragePrice.DefaultZero()) * Convert.ToDouble(stockid.DefaultZero());
                    //    double newAverageValue = (totalvalue + averagevale) / totalStock;

                    //    if (Double.IsInfinity(newAverageValue) || newAverageValue == 0)
                    //    {
                    //        var result = db.Items.SingleOrDefault(b => b.IID == itemTable.IID);
                    //        result.AveragePrice = newAverageValue;
                    //        db.SaveChanges();
                    //    }
                    //}
                    transaction.Commit();
                }

                else
                {

                    transaction = db.Database.BeginTransaction();


                    var a = cmbxAdjustment.SelectedIndex;
                    var obj = db.Adj_M.SingleOrDefault(b=>b.RID==Rid);
                    obj.AC_Code = Convert.ToInt32(accountCode);
                    obj.CompID =compID;
                    obj.EDate = dtAdjustment.Value;
                    obj.WID = Convert.ToInt32(cmbxWarehouse.SelectedValue);
                    obj.RID = Rid;
                    db.SaveChanges();
                    //db.Entry(obj).State = EntityState.Modified;
                    //db..Attach(aViewModel.a);




                    //int ItemCode = Convert.ToInt32(cmbxProduct.SelectedValue);
                    //var getITemValue = db.Items.Where(x => x.AC_Code_Inv == ItemCode).FirstOrDefault();
                    //  var getAdjID = db.Adj_D.Where(x => x.AC_Code_Inv == ItemCode).FirstOrDefault();
                    var deleteAdj_D = db.Adj_D.Where(x => x.RID == Rid).ToList();
                    db.Adj_D.RemoveRange(deleteAdj_D);

                    Adj_D adjD = new Adj_D();

                    adjD.RID = obj.RID;
                    adjD.IID = getITemValue.IID;
                    if (Convert.ToUInt32(cmbxAdjustment.SelectedIndex) == 0)
                    {

                        adjD.Qty = Convert.ToInt32(txtQty.Text);
                        adjD.PurPrice = Convert.ToInt32(txtPrice.Text);
                        adjD.Debit = Convert.ToInt32(txtQty.Text) * Convert.ToInt32(txtPrice.Text);

                    }
                    else if (Convert.ToUInt32(cmbxAdjustment.SelectedIndex) == 1)
                    {

                        adjD.Qty2 = Convert.ToInt32(txtQty.Text);
                        adjD.PurPrice = Convert.ToInt32(txtPrice.Text);
                        adjD.Credit = Convert.ToInt32(txtQty.Text) * Convert.ToInt32(txtPrice.Text);

                    }
                    db.Adj_D.Add(adjD);

                    var getItemledgerID = db.Itemledger.Where(x => x.RID == Rid && x.TypeCode == 13 && x.CompanyID==compID).ToList();
                    db.Itemledger.RemoveRange(getItemledgerID);

                    Itemledger ledger = new Itemledger();

                    ledger.IID = getITemValue.IID;
                    ledger.EDate = dtAdjustment.Value;
                    ledger.TypeCode = 13;
                    ledger.CompanyID = compID;
                    ledger.WID = Convert.ToInt32(cmbxWarehouse.SelectedValue);
                    ledger.SID = 1;
                    ledger.AC_CODE = Convert.ToInt32(accountCode);
                    ledger.RID = Rid;

                    if (Convert.ToInt32(cmbxAdjustment.SelectedIndex) == 0)
                    {
                        ledger.ADJ_IN = Convert.ToInt32(txtQty.Text);
                        ledger.PurPrice = Convert.ToInt32(txtPrice.Text);
                        ledger.Amt = Convert.ToInt32(txtQty.Text) * Convert.ToInt32(txtPrice.Text);

                    }
                    else if (Convert.ToInt32(cmbxAdjustment.SelectedIndex) == 1)
                    {

                        ledger.ADJ_OUT = Convert.ToInt32(txtQty.Text);
                        ledger.PurPrice = Convert.ToInt32(txtPrice.Text);
                        ledger.Amt = Convert.ToInt32(txtQty.Text) * Convert.ToInt32(txtPrice.Text);

                    }
                    db.Itemledger.Add(ledger);

                    var All = db.GL.Where(x => x.RID == Rid && x.TypeCode == 13).ToList();
                    db.GL.RemoveRange(All);
                    GL gl = new GL();
                    gl.TypeCode = 13;

                    if (Convert.ToInt32(cmbxAdjustment.SelectedIndex) == 0)
                    {
                        gl.AC_Code = Convert.ToInt32(ItemID);
                        gl.AC_Code2 = Convert.ToInt32(accountCode);
                        gl.TypeCode = 13;
                        gl.Debit = Convert.ToDouble(txtAmt.Text);
                        gl.CompID = compID;
                        gl.Credit = 0;
                        gl.Narration = txtDes.Text;
                        gl.RID = obj.RID;
                        gl.GLDate = dtAdjustment.Value;
                        gl.MOP_ID = 7;
                        db.GL.Add(gl);



                        GL gl2 = new GL();
                        gl2.TypeCode = 13;
                        gl2.AC_Code = Convert.ToInt32(accountCode);
                        gl2.AC_Code2 = Convert.ToInt32(ItemID);
                        gl2.Narration = txtDes.Text;
                        gl2.CompID = compID;
                        gl2.Debit = 0;
                        gl2.Credit = Convert.ToDouble(txtAmt.Text);
                        gl2.RID = obj.RID;
                        gl2.GLDate = dtAdjustment.Value;
                        gl2.MOP_ID = 7;
                        db.GL.Add(gl2);
                    }
                    else if (Convert.ToInt32(cmbxAdjustment.SelectedIndex) == 1)
                    {
                        gl.AC_Code = Convert.ToInt32(accountCode);
                        gl.AC_Code2 = Convert.ToInt32(ItemID);
                        gl.TypeCode = 13;
                        gl.Debit = Convert.ToDouble(txtAmt.Text);
                        gl.CompID = compID;
                        gl.Credit = 0 ;
                        gl.Narration = txtDes.Text;
                        gl.RID = obj.RID;
                        gl.GLDate = dtAdjustment.Value;
                        gl.MOP_ID = 7;
                        db.GL.Add(gl);



                        GL gl2 = new GL();
                        gl2.TypeCode = 13;
                        gl2.AC_Code = Convert.ToInt32(ItemID);
                        gl2.AC_Code2 = Convert.ToInt32(accountCode);
                        gl2.Narration = txtDes.Text;
                        gl2.Debit = 0;
                        gl2.Credit = Convert.ToDouble(txtAmt.Text);
                        gl2.CompID = compID;
                        gl2.RID = obj.RID;
                        gl2.GLDate = dtAdjustment.Value;
                        gl2.MOP_ID = 7;
                        db.GL.Add(gl2);

                    }

                    db.SaveChanges();
                 //   var ADJ = db.Adj_D.AsNoTracking().Where(x => x.RID == obj.RID).FirstOrDefault();
                 //   double previosPurchase = Convert.ToDouble(ADJ.PurPrice);

                 //   int id = Convert.ToInt32(cmbxProduct.SelectedValue);
                 //   var itemTable = db.Items.AsNoTracking().Where(x => x.AC_Code_Inv == id).FirstOrDefault();
                 ////   var stockid = ReportsController.getStockByID(id, compID);
                 //   double totalStock = Convert.ToDouble(Convert.ToDouble(stockid.DefaultZero()) + Convert.ToDouble(txtQty.Text) -Convert.ToDouble(ADJ.Qty));
                 //   double totalvalue = Convert.ToDouble(txtAmt.Text);
                 //   double averagevale = Convert.ToDouble(itemTable.AveragePrice.DefaultZero()) * Convert.ToDouble(stockid.DefaultZero());
                 //   double newAverageValue = (totalvalue + averagevale - previosPurchase) / totalStock;

                 //   if (Double.IsInfinity(newAverageValue) || newAverageValue == 0)
                 //   {


                 //   }
                 //   else
                 //   {
                 //       var result = db.Items.SingleOrDefault(b => b.IID == itemTable.IID);
                 //       result.AveragePrice = Convert.ToDouble(newAverageValue.DefaultZero());
                 //   }
                    //db.SaveChanges();
                    transaction.Commit();
                }



                //  db.SaveChanges();
                //  transaction.Commit();

                var list = ReportsController.getItemAdjustmen(dtTo.Value, dtFrom.Value, compID);
                getItemAdjustmentResultBindingSource.DataSource = list;
                pnlMain.Hide();
            }
            catch (Exception ex)
            {
              transaction.Rollback();
                MessageBox.Show(ex.Message);
            }
            //    bool isAdd = true;
            //    SqlConnection con = null;
            //    SqlTransaction trans = null;
            //    DbContextTransaction transaction = null;
            //  DateTime abc = Convert.ToDateTime(dtAdjDate.Text);
            //    try
            //    {
            //        if (txtProdName.Text == "")
            //        { MessageBox.Show("Please Provide Proddor Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            //        else
            //        {
            //   Lib.Entity. obj = (Lib.Entity.getItemAdjustment_Result)getItemAdjustmentBindingSource.Current;

            //  var obj = getItemAdjustmentBindingSource.Current;

            //   var Currentobj = db.Items.ToList().Find(x => x.IName == txtProdName.Text.Trim());
            //            if (obj.IID == 0)
            //            {
            //                if (Currentobj != null)
            //                {
            //                    MessageBox.Show("Product Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    return;
            //                }
            //            }
            //            else
            //            {
            //                bool isCodeExist = list.Any(record => record.IName == obj.IName && record.IID != obj.IID);
            //                if (isCodeExist)
            //                {
            //                    MessageBox.Show("Product Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    return;
            //                }
            //            }

            //            transaction = db.Database.BeginTransaction();

            //            if (obj.IID > 0)
            //            {
            //                isAdd = false;
            //            }

            //            DataAccess access = new DataAccess();
            //            COA coa = new COA();
            //            String Inventorycode = "";
            //            int[] balance = new int[3];
            //            int[] vals = new int[3] { 4, 14, 15 };

            //            for (int i = 0; i < vals.Length; i++)
            //            {
            //                coa.AC_Code = vals[i];

            //                con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            //                con.Open();
                    //     trans = con.BeginTransaction();
            //                Inventorycode = access.GetScalar("GetAc_Code", coa, con, trans);
            //                COA_D coaD = new COA_D();
            //                if (!isAdd)
            //                {
            //                    if (i == 0)
            //                    {
            //                        coaD = db.COA_D.Where(b => b.AC_Code == obj.AC_Code_Inv).FirstOrDefault();
            //                    }
            //                    if (i == 1)
            //                    {
            //                        coaD = db.COA_D.Where(b => b.AC_Code == obj.AC_Code_Inc).FirstOrDefault();
            //                    }
            //                    if (i == 2)
            //                    {
            //                        coaD = db.COA_D.Where(b => b.AC_Code == obj.AC_Code_Cost).FirstOrDefault();
            //                    }
            //                }
            //                coaD.CAC_Code = vals[i];
            //                coaD.PType_ID = 1;
            //                coaD.ZID = 0;
            //                coaD.AC_Title = txtProdName.Text.Trim();
            //                coaD.DR = 0;
            //                coaD.CR = 0;
            //                coaD.Qty = 0;
            //                if (!isAdd)
            //                {
            //                    db.Entry(coaD).State = EntityState.Modified;
            //                    if (i == 0)
            //                    {
            //                        balance[i] = Convert.ToInt32(obj.AC_Code_Inv);
            //                        // coaD = db.COA_D.Where(b => b.AC_Code == obj.AC_Code_Inv).FirstOrDefault();
            //                    }
            //                    if (i == 1)
            //                    {
            //                        balance[i] = Convert.ToInt32(obj.AC_Code_Inc);
            //                        //coaD = db.COA_D.Where(b => b.AC_Code == obj.AC_Code_Inc).FirstOrDefault();
            //                    }
            //                    if (i == 2)
            //                    {
            //                        balance[i] = Convert.ToInt32(obj.AC_Code_Cost);
            //                        // coaD = db.COA_D.Where(b => b.AC_Code == obj.AC_Code_Cost).FirstOrDefault();
            //                    }
            //                }
            //                else
            //                {
            //                    balance[i] = Convert.ToInt32(Inventorycode);
            //                    coaD.AC_Code = Convert.ToInt32(Inventorycode);
            //                    db.COA_D.Add(coaD);
            //                }
            //            }
            //            db.SaveChanges();

            //            txtOpenQ.Text = txtOpenQ.Text == "" ? "0" : txtOpenQ.Text;
            //           
            //            txtpacking.Text = txtpacking.Text == "" ? "0" : txtpacking.Text;
            //            txtSaleTax.Text = txtSaleTax.Text == "" ? "0" : txtSaleTax.Text;
            //            GL gl = new GL();
            //            var inventoryCode = Convert.ToInt32(balance[0]);
            //            if (!isAdd)
            //            {
            //                gl = db.GL.Where(b => b.AC_Code == obj.AC_Code_Inv && b.TypeCode == 0).FirstOrDefault();
            //            }
            //            gl.RID = 0;
            //            gl.RID2 = 0;
            //            gl.TypeCode = 0;
            //            gl.GLDate = DateTime.Now;
            //            gl.AC_Code2 = 12000001;
            //            gl.Narration = "Opening Entry";
            //            gl.Qty_IN = Convert.ToDouble(txtOpenQ.Text);
            //            gl.IPrice = Convert.ToDouble(txtOpenP.Text);
            //            gl.Debit = Convert.ToDouble(txtOpenQ.Text) * Convert.ToDouble(txtOpenP.Text);
            //            gl.CompID = compID;
            //            gl.Credit = 0;
            //            if (!isAdd)
            //            {
            //                db.Entry(gl).State = EntityState.Modified;
            //            }
            //            else
            //            {
            //                gl.AC_Code = balance[0];
            //                db.GL.Add(gl);
            //            }

            //            ////capital credit
            //            GL glCapital = new GL();
            //            if (!isAdd)
            //            {
            //                glCapital = db.GL.Where(b => b.AC_Code2 == obj.AC_Code_Inv && b.TypeCode == 0).FirstOrDefault();
            //            }
            //            glCapital.RID = 0;
            //            glCapital.RID2 = 0;
            //            glCapital.TypeCode = 0;
            //            glCapital.GLDate = DateTime.Now;
            //            glCapital.AC_Code = 12000001;
            //            glCapital.Narration = "opening Item :" + txtProdName.Text.Trim();
            //            glCapital.Qty_IN = Convert.ToDouble(txtOpenQ.Text);
            //            glCapital.IPrice = Convert.ToDouble(txtOpenP.Text);
            //            glCapital.Debit = 0;
            //            glCapital.Credit = Convert.ToDouble(txtOpenQ.Text) * Convert.ToDouble(txtOpenP.Text);
            //            glCapital.CompID = compID;
            //            if (!isAdd)
            //            {
            //                db.Entry(glCapital).State = EntityState.Modified;
            //            }
            //            else
            //            {
            //                glCapital.AC_Code2 = Convert.ToInt32(balance[0]);
            //                db.GL.Add(glCapital);
            //            }
            //            db.SaveChanges();

            //            obj.IName = txtProdName.Text.Trim();
            //            obj.Desc = txtDes.Text.Trim();
            //            obj.AC_Code_Inv = Convert.ToInt32(balance[0]);
            //            obj.AC_Code_Inc = Convert.ToInt32(balance[1]);
            //            obj.AC_Code_Cost = Convert.ToInt32(balance[2]);
            //            obj.OP_Qty = Convert.ToInt32(txtOpenQ.Text);
            //            obj.OP_Price = Convert.ToInt32(txtOpenP.Text);
            //            obj.PurPrice = Convert.ToInt32(txtPurchase.Text);
            //            obj.CompID = Convert.ToInt32(cmbxMak.SelectedValue);
            //            obj.SCatID = Convert.ToInt32(cmbxCat.SelectedValue);
            //            obj.Unit_ID = Convert.ToInt32(cmbxUnits.SelectedValue);
            //            obj.isDeleted = chkIsActive.Checked;
            //            obj.RetailPrice = Convert.ToDouble(txtTell.Text.Trim());
            //            obj.SalesPrice = Convert.ToDouble(txtSale.Text.Trim());
            //            obj.CompanyID = compID;
            //            obj.CTN_PCK = Convert.ToInt32(txtpacking.Text);
            //            obj.saleTax = Convert.ToInt32(txtSaleTax.Text);
            //            if (obj.IID == 0)
            //            {
            //                db.Items.Add(obj);
            //            }
            //            else
            //            {
            //                var result = db.Items.Where(b => b.IID == obj.IID).FirstOrDefault();
            //                if (result != null)
            //                {
            //                    result.IName = txtProdName.Text.Trim();
            //                    result.Desc = txtDes.Text.Trim();
            //                    result.OP_Qty = Convert.ToInt32(txtOpenQ.Text);
            //                    result.OP_Price = Convert.ToInt32(txtOpenP.Text);
            //                    result.PurPrice = Convert.ToInt32(txtPurchase.Text);
            //                    result.CompID = Convert.ToInt32(cmbxMak.SelectedValue);
            //                    result.SCatID = Convert.ToInt32(cmbxCat.SelectedValue);
            //                    result.Unit_ID = Convert.ToInt32(cmbxUnits.SelectedValue);
            //                    result.isDeleted = chkIsActive.Checked;
            //                    result.RetailPrice = Convert.ToDouble(txtTell.Text.Trim());
            //                    result.SalesPrice = Convert.ToDouble(txtSale.Text.Trim());
            //                    result.saleTax = Convert.ToInt32(txtSaleTax.Text);

            //                    db.Entry(result).State = EntityState.Modified;
            //                }
            //            }
            //            db.SaveChanges();

            //            Itemledger itemledger = new Itemledger();
            //            if (!isAdd)
            //            {
            //                itemledger = db.Itemledgers.Where(b => b.IID == obj.IID && b.TypeCode == 0).FirstOrDefault();
            //            }
            //            itemledger.EDate = System.DateTime.Now;
            //            itemledger.AC_CODE = 12000001;
            //            itemledger.WID = 1;
            //            itemledger.SID = 1;
            //            itemledger.BNID = 1;
            //            itemledger.TypeCode = 0;
            //            itemledger.RID = 0;
            //            // itemledger.ExpDT = System.DateTime.Now;
            //            itemledger.OPN = Convert.ToInt32(txtOpenQ.Text);
            //            itemledger.PurPrice = Convert.ToDouble(txtOpenP.Text);
            //            itemledger.PAmt = Convert.ToDouble(txtOpenQ.Text) * Convert.ToDouble(txtOpenP.Text);
            //            itemledger.Amt = Convert.ToDouble(txtOpenQ.Text) * Convert.ToDouble(txtOpenP.Text);
            //            if (!isAdd)
            //            {
            //                db.Entry(itemledger).State = EntityState.Modified;
            //            }
            //            else
            //            {
            //                itemledger.IID = obj.IID;
            //                db.Itemledgers.Add(itemledger);
            //            }
            //            db.SaveChanges();
            //            transaction.Commit();

            //            pnlMain.Hide();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        transaction.Rollback();
            //        MessageBox.Show(ex.Message);
            //    }
        }
        private void PartyTypeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void Validation() {

            if (true) {

                char[] spearator = { '/' };

                // using the method
                String[] strlist = cmbxProduct.Text.Split(spearator);
                var dd = strlist.Length;
                if (strlist.Length == 2) { }


                else
                {
                    ItemID = Convert.ToInt32(0);
                    cmbxProduct.Text = "";
                    cmbxProduct.Focus();

                    return;
                }

                string accode = "";
                foreach (String s in strlist)
                {
                    accode = s;
                }

                if (accode == "0" || accode == "" || accode == null)
                {
                    ItemID = Convert.ToInt32(0);
                    cmbxProduct.Text = "";
                    cmbxProduct.Focus();
                    return;


                }
                else
                {
                    ItemID = Convert.ToInt32(accode);
                }




            }

            if (true) {



                var dsa = Convert.ToInt32(cmbxHeadAccount.SelectedIndex);

                if (dsa >= 1)
                {

                    int value = Convert.ToInt32(cmbxHeadAccount.SelectedValue);

                    if (value == 3)
                    {
                        char[] spearator = { '/' };

                        // using the method
                        String[] strlist = cmbxAccount.Text.Split(spearator);
                        var dd = strlist.Length;
                        if (strlist.Length == 3) { }


                        else
                        {
                            accountCode = Convert.ToInt32(0);
                            cmbxAccount.Text = "";
                            cmbxAccount.Focus();

                            return;
                        }

                        string accode = "";
                        foreach (String s in strlist)
                        {
                            accode = s;
                        }

                        if (accode == "0" || accode == "" || accode == null)
                        {
                            accountCode = Convert.ToInt32(0);
                            cmbxAccount.Text = "";
                            cmbxAccount.Focus();
                            return;


                        }
                        else
                        {
                            accountCode = Convert.ToInt32(accode);


                        }
                    }


                    else if (value == 9)
                    {
                        char[] spearator = { '/' };

                        // using the method
                        String[] strlist = cmbxAccount.Text.Split(spearator);
                        var dd = strlist.Length;
                        if (strlist.Length == 3) { }


                        else
                        {
                            accountCode = Convert.ToInt32(0);
                            cmbxAccount.Text = "";
                            cmbxAccount.Focus();

                            return;
                        }

                        string accode = "";
                        foreach (String s in strlist)
                        {
                            accode = s;
                        }

                        if (accode == "0" || accode == "" || accode == null)
                        {
                            accountCode = Convert.ToInt32(0);
                            cmbxAccount.Text = "";
                            cmbxAccount.Focus();
                            return;


                        }
                        else
                        {
                            accountCode = Convert.ToInt32(accode);


                        }
                    }


                    else
                    {


                        var row = db.COA_D.AsNoTracking().Where(x => x.CAC_Code == value && x.AC_Title == cmbxAccount.Text).FirstOrDefault();

                        if (row == null)
                        {

                            accountCode = Convert.ToInt32(0);
                            cmbxAccount.Text = "";
                            cmbxAccount.Focus();
                            return;
                        }
                        else
                        {
                            accountCode = Convert.ToInt32(row.AC_Code);


                        }

                    }

                }




            }



        }

        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected)
        {
            comboBox.DataSource = obj;
            comboBox.DisplayMember = Name; // Column Name
            comboBox.ValueMember = ID;  // Column Name
            comboBox.SelectedValue = selected;
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { ProductsDataGridView.DataSource = list; }
                else
                {
                    //  ProductsDataGridView.DataSource = list.FindAll(x => x..ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception)
            {
                throw;
            }
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

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txtpacking_Click(object sender, EventArgs e)
        {











        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void ProductsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var list = ReportsController.getItemAdjustmen(dtTo.Value, dtFrom.Value, compID);
            getItemAdjustmentResultBindingSource.DataSource = list;
        }

        private void ProductsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clear();
            Rid = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[8].Value);
            cmbxWarehouse.SelectedValue = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString());
            int headAccountID = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[2].Value);
            var headAccount = db.COA_D.AsNoTracking().Where(x => x.AC_Code == headAccountID).FirstOrDefault();

            cmbxHeadAccount.SelectedValue = Convert.ToInt32(headAccount.CAC_Code);
            cmbxAccount.SelectedValue = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[2].Value);
            //txtAmount.Text = Convert.ToDouble(ProductsDataGridView.Rows[e.RowIndex].Cells[18].Value).ToString();
            //dtCheckDate.Value = Convert.ToDateTime(CategorysDataGridView.Rows[e.RowIndex].Cells[20].Value.ToString());
            txtPrice.Text = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[11].Value).ToString();
            //txtNarr.Text = ProductsDataGridView.Rows[e.RowIndex].Cells[24].Value.ToString();
            cmbxProduct.SelectedValue = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[4].Value);

            var qty = ProductsDataGridView.Rows[e.RowIndex].Cells[9].Value;
            double debit=Convert.ToDouble( ProductsDataGridView.Rows[e.RowIndex].Cells[12].Value.DefaultZero());
            double credit= Convert.ToDouble( ProductsDataGridView.Rows[e.RowIndex].Cells[13].Value.DefaultZero());
            if ( debit!=0)
            {
                txtQty.Text = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[9].Value).ToString();
                txtAmt.Text = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[12].Value).ToString();
                cmbxAdjustment.SelectedIndex = 0;
            }

            else { }
            if ( credit != 0)
            {
                txtQty.Text = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[10].Value).ToString();
                txtAmt.Text = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[13].Value).ToString();
                cmbxAdjustment.SelectedIndex = 1;
            }
        
            //if (qty.Equals(null))
            //{
            //    txtQty.Text= Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[9].Value).ToString();

            //}

            //else {
            //    txtQty.Text = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[10].Value.DefaultZero()).ToString();

            //}

            //if (credit==null)
            //{
            //    txtAmt.Text = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[12].Value).ToString();
            //    cmbxAdjustment.SelectedIndex = 0;
            //}

            //else
            //{
            //    txtAmt.Text = Convert.ToInt32(ProductsDataGridView.Rows[e.RowIndex].Cells[13].Value.DefaultZero()).ToString();
            //    cmbxAdjustment.SelectedIndex = 1;
            //}
            //pnlMain.Show();
        }



        public void clear() {


            txtAmt.Text = "";
            txtPrice.Text = "";
            txtQty.Text = "";
            txtDes.Text = "";

            var list = ReportsController.getItemAdjustmen(dtTo.Value, dtFrom.Value, compID);
            getItemAdjustmentResultBindingSource.DataSource = list;

            var item = db.Items.Where(x => x.CompanyID == compID && x.isDeleted == false).ToList();

            List<CustomerList> customerList = new List<CustomerList>();
            customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
            foreach (var itemValue in item)
            {

                CustomerList cl = new CustomerList();
                cl.AC_Code = itemValue.AC_Code_Inv;
                cl.Name = itemValue.IName + "/" + itemValue.AC_Code_Inv;
                customerList.Add(cl);
            }

            FillCombo(cmbxProduct, customerList, "Name", "AC_Code", 0);


            FillCombo(cmbxWarehouse, db.tbl_Warehouse.Where(x => x.CompanyID == compID && x.isDelete == false).ToList(), "Warehouse", "WID", 0);


            //cmbxAdjustment.Items.Insert(0, "INPUT");
            //cmbxAdjustment.Items.Insert(1, "OUTPUT");

            cmbxAdjustment.SelectedIndex = 0;


            var Account = db.COA_M.ToList();


            var vendor = db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList();


            List<COA_M> m = new List<COA_M>();
            m.Add(new COA_M { CAC_Code = 0, CATitle = "--Select--" });
            m.AddRange(db.COA_M.ToList());
            FillCombo(cmbxHeadAccount, m, "CATitle", "CAC_Code", 0);


            List<COA_D> d = new List<COA_D>();
            d.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
            d.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList());
            FillCombo(cmbxAccount, d, "AC_Title", "AC_Code", 0);




            // dtAdjustment.Value = DateTime.Now;
            //cmbxAdjustment.SelectedIndex = 0;
            // FillCombo(cmbxProduct, db.COA_D.Where(x => x.CAC_Code == 4).ToList(), "AC_Title", "AC_Code", 1);
            // var account = db.COA_D.Where(x => x.CAC_Code == 16).FirstOrDefault();
            // var ProductCode = db.COA_D.Where(x => x.CAC_Code == 4).FirstOrDefault();
            //// FillCombo(cmbxAccount, db.COA_D.Where(x => x.CAC_Code == 16).ToList(), "AC_Title", "AC_Code", account.AC_Code);
            // FillCombo(cmbxWarehouse, db.tbl_Warehouse.ToList(), "Warehouse", "WID", 1);
            // FillCombo(cmbxProduct, db.COA_D.Where(x => x.CAC_Code == 4).ToList(), "AC_Title", "AC_Code",0 /*ProductCode.AC_Code*/);


            // List<COA_M> m = new List<COA_M>();
            // m.Add(new COA_M { CAC_Code = 0, CATitle = "--Select--" });
            // m.AddRange(db.COA_M.ToList());
            // FillCombo(cmbxHeadAccount, m, "CATitle", "CAC_Code", 0);


            // List<COA_D> d = new List<COA_D>();
            // d.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
            // d.AddRange(db.COA_D.Where(x => x.CAC_Code == 1).ToList());
            // FillCombo(cmbxAccount, d, "AC_Title", "AC_Code", 0);





        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
           double quauntity =Convert.ToDouble( txtQty.Text == "" ? "0" : txtQty.Text) ;
            double Price = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
            txtAmt.Text= (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text)).ToString();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtQty_KeyUp(object sender, KeyEventArgs e)
        {
            double quauntity = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
            double Price = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
            txtAmt.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text)).ToString();

        }

        private void cmbxHeadAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsa = Convert.ToInt32(cmbxHeadAccount.SelectedIndex);

            if (dsa >= 1)
            {


                int value = Convert.ToInt32(cmbxHeadAccount.SelectedValue);

                if (value == 3)
                {


                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getcustumerList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
                    }

                    FillCombo(cmbxAccount, customerList, "Name", "AC_Code", 0);
                }

                else if (value == 9) {

                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getVendorList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] + "/" + row["AC_Code"] });
                    }

                    FillCombo(cmbxAccount, customerList, "Name", "AC_Code", 0);



                }
                else
                {
                    List<COA_D> d = new List<COA_D>();
                    d.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
                    d.AddRange(db.COA_D.Where(x => x.CAC_Code == value && x.CompanyID == compID && x.InActive == false).ToList());
                    FillCombo(cmbxAccount, d, "AC_Title", "AC_Code", 0);
                    //var vendor = db.COA_D.Where(x => x.CAC_Code == value).ToList();

                }
                //FillCombo(cmbxvendor, vendor, "AC_Title", "CAC_Code", 0);
            }
            else if (dsa == 0)
            {

                //   int value = Convert.ToInt32(cmbxAccID.SelectedValue);
                //List<COA_D> d = new List<COA_D>();
                //d.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
                //d.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID && x.InActive == false).ToList());
                //FillCombo(cmbxAccount, d, "AC_Title", "AC_Code", 0);

            }
        }

        private void cmbxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsa = Convert.ToInt32(cmbxProduct.SelectedIndex);

            if (dsa >= 1)
            {
                int ItemCode = Convert.ToInt32(cmbxProduct.SelectedValue);
                var getITemValue = db.Items.Where(x => x.AC_Code_Inv == ItemCode).FirstOrDefault();

                txtPrice.Text = getITemValue.AveragePrice.ToString();

                
            }
            else if (dsa == 0)
            {

               
            }
        }

        private void cmbxAdjustment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxAdjustment.SelectedIndex == 0)
            {

                txtPrice.Text = "";

            }
            if (cmbxAdjustment.SelectedIndex == 1)
            {
                int code = Convert.ToInt32(cmbxProduct.SelectedValue.DefaultZero());
                if (code == 0)
                {


                }
                else
                {
                    var price = db.Items.AsNoTracking().Where(x => x.AC_Code_Inv == code).FirstOrDefault();
                    txtPrice.Text = price.AveragePrice.DefaultZero();
                }
            }
        }

        private void cmbxProduct_Leave(object sender, EventArgs e)
        {
            char[] spearator = { '/' };

            // using the method
            String[] strlist = cmbxProduct.Text.Split(spearator);
            var dd = strlist.Length;
            if (strlist.Length == 2) { }


            else
            {
                ItemID = Convert.ToInt32(0);
                cmbxProduct.Text = "";
                cmbxProduct.Focus();

                return;
            }

            string accode = "";
            foreach (String s in strlist)
            {
                accode = s;
            }

            if (accode == "0" || accode == "" || accode == null)
            {
                ItemID = Convert.ToInt32(0);
                cmbxProduct.Text = "";
                cmbxProduct.Focus();
                return;


            }
            else {
                ItemID= Convert.ToInt32(accode);
                 }
        }

        private void cmbxAccount_Leave(object sender, EventArgs e)
        {

            var dsa = Convert.ToInt32(cmbxHeadAccount.SelectedIndex);

            if (dsa >= 1)
            {

                int value = Convert.ToInt32(cmbxHeadAccount.SelectedValue);

                if (value == 3)
                {
                    char[] spearator = { '/' };

                    // using the method
                    String[] strlist = cmbxAccount.Text.Split(spearator);
                    var dd = strlist.Length;
                    if (strlist.Length == 3) { }


                    else
                    {
                        accountCode = Convert.ToInt32(0);
                        cmbxAccount.Text = "";
                        cmbxAccount.Focus();

                        return;
                    }

                    string accode = "";
                    foreach (String s in strlist)
                    {
                        accode = s;
                    }

                    if (accode == "0" || accode == "" || accode == null)
                    {
                        accountCode = Convert.ToInt32(0);
                        cmbxAccount.Text = "";
                        cmbxAccount.Focus();
                        return;


                    }
                    else
                    {
                        accountCode = Convert.ToInt32(accode);


                    }
                }


                else if (value == 9)
                {
                    char[] spearator = { '/' };

                    // using the method
                    String[] strlist = cmbxAccount.Text.Split(spearator);
                    var dd = strlist.Length;
                    if (strlist.Length == 3) { }


                    else
                    {
                        accountCode = Convert.ToInt32(0);
                        cmbxAccount.Text = "";
                        cmbxAccount.Focus();

                        return;
                    }

                    string accode = "";
                    foreach (String s in strlist)
                    {
                        accode = s;
                    }

                    if (accode == "0" || accode == "" || accode == null)
                    {
                        accountCode = Convert.ToInt32(0);
                        cmbxAccount.Text = "";
                        cmbxAccount.Focus();
                        return;


                    }
                    else
                    {
                        accountCode = Convert.ToInt32(accode);


                    }
                }


                else {


                    var row = db.COA_D.AsNoTracking().Where(x => x.CAC_Code == value && x.AC_Title == cmbxAccount.Text).FirstOrDefault();

                    if (row == null)
                    {

                        accountCode = Convert.ToInt32(0);
                        cmbxAccount.Text = "";
                        cmbxAccount.Focus();
                        return;
                    }
                    else {
                        accountCode = Convert.ToInt32(row.AC_Code);


                    }

                }

            }
        }












        //public ItemsAdjustment()
        //{
        //    InitializeComponent();
        //}
    }
}
