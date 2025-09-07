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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Forms;

namespace SalesMngmt.Reporting
{
    public partial class migrate_data : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.Items> list = null;
        public migrate_data()
        {
            InitializeComponent();
            db = new SaleManagerEntities();
        }

        public void save(Items item, tbl_Warehouse warehouseID)
        {
            List<GL> itemGL = new List<GL>();
            bool isAdd = true;
            SqlConnection con = null;
            SqlTransaction trans = null;
            int warehouse = 0;
            if (warehouseID == null)
            {
                tbl_Warehouse ware = new tbl_Warehouse();
                ware.CompanyID = Convert.ToInt32(textBox2.Text);
                ware.Warehouse = "WareHouse1";
                ware.isDelete = false;
                db.tbl_Warehouse.Add(ware);
                db.SaveChanges();
                warehouseID = db.tbl_Warehouse.Where(x => x.CompanyID == Convert.ToInt32(textBox2.Text)).FirstOrDefault();
                warehouse = warehouseID.WID;
            }
            var Maxcapital = ReportsController.getMixACodeById(12, Convert.ToInt32(textBox2.Text));
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
                coaD.CompanyID = Convert.ToInt32(textBox2.Text);
                coaD.InActive = false;
                db.COA_D.Add(coaD);
                db.SaveChanges();
                a = capital;
            }
            else
            {
                capital = a;
            }
            try
            {
                if (item.IName == "")
                {
                    MessageBox.Show("Please Provide Product Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Lib.Entity.Items obj = new Items();
                    if (obj.IID > 0)
                    {
                        isAdd = false;
                        itemGL = new List<GL>();
                        itemGL = db.GL.Where(x => x.AC_Code == obj.AC_Code_Inv && x.CompID == Convert.ToInt32(textBox2.Text)).ToList();
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
                            coaD.AC_Title = item.IName.Trim();
                            coaD.DR = 0;
                            coaD.CR = 0;
                            coaD.Qty = 0;
                            coaD.InActive = false;
                            coaD.CompanyID = Convert.ToInt32(textBox2.Text);
                            db.COA_D.Add(coaD);
                            vals[i] = Convert.ToInt32(Inventorycode);
                        }
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
                        gl.Qty_IN = 0;//item.OP_Qty;
                        gl.Debit = 0;//item.OP_Qty * item.PurPrice;
                        gl.Credit = 0;
                        gl.CompID = Convert.ToInt32(textBox2.Text);
                        db.GL.Add(gl);

                        GL glCapital = new GL();
                        glCapital.RID = 0;
                        glCapital.RID2 = 0;
                        glCapital.TypeCode = 0;
                        glCapital.GLDate = DateTime.Now;
                        glCapital.AC_Code = capital;
                        glCapital.AC_Code2 = Convert.ToInt32(Inventorycode);
                        glCapital.Narration = "opening Item :" + item.IName.Trim();
                        glCapital.Qty_IN = 0;//item.OP_Qty;
                        glCapital.Debit = 0;
                        glCapital.Credit = 0;//item.OP_Qty * item.PurPrice;
                        glCapital.CompID = Convert.ToInt32(textBox2.Text);
                        db.GL.Add(glCapital);
                    }
                    db.SaveChanges();
                    if (isAdd)
                    {
                        Itemledger itemledger = new Itemledger();
                        itemledger.EDate = System.DateTime.Now;
                        itemledger.AC_CODE = capital;
                        itemledger.WID = (int)warehouseID.WID;
                        itemledger.SID = 1;
                        itemledger.IID = obj.IID;
                        itemledger.BNID = 1;
                        itemledger.TypeCode = 0;
                        itemledger.RID = 0;
                        itemledger.CompanyID = Convert.ToInt32(textBox2.Text);
                        itemledger.OPN = 0;// item.OP_Qty;
                        itemledger.PurPrice = 0;//item.PurPrice;
                        itemledger.PAmt = 0; //item.OP_Qty * item.PurPrice;
                        itemledger.Amt = 0;//item.OP_Qty * item.PurPrice;
                        //itemledger.WID = (int)warehouse;
                        db.Itemledger.Add(itemledger);
                    }
                    db.SaveChanges();

                    var comid = Convert.ToInt32(textBox1.Text);
                    var comid2 = Convert.ToInt32(textBox2.Text);

                    int AC_Code_Cost = Convert.ToInt32( item.AC_Code_Cost);
                    int AC_Code_Inc = Convert.ToInt32(item.AC_Code_Inc);
                    int AC_Code_Inv = Convert.ToInt32(item.AC_Code_Inv);

                    var itemss = db.Items.Where(x => x.IID == item.IID).FirstOrDefault();
                    itemss.AC_Code_Cost = db.COA_D.Where(c => c.CAC_Code == 15 && c.AC_Title == item.IName && c.CompanyID == comid2).Select(c => c.AC_Code).FirstOrDefault(); // You can select specific properties if needed.FirstOrDefault();
                                                                                                                                                                           // x.AC_Code_Cost=db.COA_D.Where(c=>c.CAC_Code=15 && c.AC_Title = x.itemName && c.CompanyID= comid2).select(x.)
                    itemss.AC_Code_Inc = db.COA_D.Where(c => c.CAC_Code == 14 && c.AC_Title == item.IName && c.CompanyID == comid2).Select(c => c.AC_Code).FirstOrDefault();
                    itemss.AC_Code_Inv = db.COA_D.Where(c => c.CAC_Code == 4 && c.AC_Title == item.IName && c.CompanyID == comid2).Select(c => c.AC_Code).FirstOrDefault();

                    // Save changes to the database
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void migrate_data_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            var comid = Convert.ToInt32(textBox1.Text);
            var comid2 = Convert.ToInt32(textBox2.Text);
            var items = db.Items.Where(x => x.CompanyID == comid ).ToList();
            int warehouse = 0;
            var warehouseID = db.tbl_Warehouse.AsNoTracking().Where(x => x.CompanyID == comid2 && x.isDelete == false).FirstOrDefault();

            if (warehouseID == null)
            {
                tbl_Warehouse ware = new tbl_Warehouse();
                ware.CompanyID = comid2;
                ware.Warehouse = "WareHouse1";
                ware.isDelete = false;
                db.tbl_Warehouse.Add(ware);
                db.SaveChanges();

                warehouseID = db.tbl_Warehouse.AsNoTracking().Where(x => x.CompanyID == comid2 && x.isDelete == false).FirstOrDefault();
                warehouse = warehouseID.WID;
            }

            else
            {
                warehouse = warehouseID.WID;

            }

       //     var warehouseID = db.tbl_Warehouse.AsNoTracking().Where(x => x.CompanyID == comid && x.isDelete == false).FirstOrDefault();
            progressBar1.Maximum = items.Count;
            progressBar1.Value = 0;

            items.ForEach(x =>
            {
                x.CompanyID = Convert.ToInt32(textBox2.Text);
                x.WareHouseID = warehouse;
                x.PurPrice = 0;
                x.OP_Qty = 0;
                x.OP_Price = 0;
                x.AveragePrice = 0;


            });
            db.Items.AddRange(items);
            db.SaveChanges();

            foreach (Items item in items)
            {
                this.save(item, warehouseID);
                progressBar1.Value += 1;
                Application.DoEvents();
            }


          
         
            MessageBox.Show("Migration Done");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
