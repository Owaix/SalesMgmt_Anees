using Lib.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesMngmt.Utility;
using System.Data.Entity;
using Lib;
using Lib.Model;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using SnaksalesERP;

namespace SalesMngmt.Invoice
{
    public partial class patientRegistration : MetroFramework.Forms.MetroForm
    {

        SaleManagerEntities db = null;
        List<COA_D> list = null;
        int compID = 0;
        SqlConnection con = null;
        SqlTransaction trans = null;
        DbContextTransaction transaction = null;
        int InvoiceNo = 0;
        public patientRegistration(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;
        }

        public void LoadFuncaTion()
        {
            //list = db.COA_D.ToList();
            //COABindingSource.DataSource = list;
            //var listCOA = new List<COA_M>();
            //listCOA.Add(new COA_M { CATitle = "Select", CAC_Code = 0 });
            //listCOA.AddRange(db.COA_M.ToList());
            //FillCombo(ddlCOA, listCOA, "CATitle", "CAC_Code", 0);

            var listPartyType = new List<Party_Type>();
            listPartyType.Add(new Party_Type { Party_Type1 = "Select", PType_ID = 0 });
            listPartyType.AddRange(db.Party_Type.ToList());
            FillCombo(cmbxEmployeeType, listPartyType, "Party_Type1", "PType_ID", 0);

            var item = new List<Lib.Entity.Items>();
            item.Add(new Lib.Entity.Items { IName = "Select", IID = 0 });
            item.AddRange(db.Items.Where(x => x.isDeleted == false).ToList());
            FillCombo(cmbxFee, item, "IName", "IID", 0);

            var Cash = new List<COA_D>();
            Cash.Add(new COA_D { AC_Title = "Credit", AC_Code = 0 });
            Cash.AddRange(db.COA_D.Where(x => x.CAC_Code == 1).ToList());
            FillCombo(cmbxPaymentMode, Cash, "AC_Title", "AC_Code", 0);
        }

        private void COA_Load(object sender, EventArgs e)
        {
            pnlMain.Hide();
            LoadFuncaTion();


        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            //COABindingSource.AddNew();
            pnlMain.Show();
            //  GetDocCode("1");
            //LoadFuncaTion();

            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            COA_D obj = (COA_D)COABindingSource.Current;
            pnlMain.Show();
            txtTotalAmount.Focus();
            // GetDocCode(obj.CAC_Code.ToString());
            // cmbxEmployeeType.Enabled = false;
            label3.Text = "EDIT";
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataAccess access = new DataAccess();
            DbContextTransaction transaction = null;
            int PatientCode = 0;
            if (txtNumber.Text == "")
            {
                MessageBox.Show("Please write number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            if (cmbxPatient.Text == "Select")
            {
                MessageBox.Show("Please write patient name or select patient name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;

            }



            String code = "";
            var patient = db.Customers.AsNoTracking().Where(x => x.Cell == txtNumber.Text && x.CusName == cmbxPatient.Text).FirstOrDefault();
            if (patient != null)
            {


                PatientCode = patient.AC_Code;


            }
            else
            {

                COA coa = new COA();

                coa.AC_Code = 3;

                con = new SqlConnection(ConnectionStrings.GetCS);
                con.Open();
                trans = con.BeginTransaction();
                code = access.GetScalar("GetAc_Code", coa, con, trans);

                COA_D coaD = new COA_D();

                coaD.CAC_Code = 3;
                //   coaD.PType_ID = Convert.ToInt32(cmbCType.SelectedValue);//1;
                coaD.ZID = 0;
                coaD.AC_Title = cmbxPatient.Text.ToString();
                coaD.DR = 0;
                coaD.CR = 0;
                coaD.Qty = 0;
                coaD.InActive = false;//chkIsActive.Checked;

                coaD.AC_Code = Convert.ToInt32(code);
                db.COA_D.Add(coaD);

                db.SaveChanges();

                Customers customer = new Customers();

                customer.AC_Code = Convert.ToInt32(code);
                customer.CusName = cmbxPatient.Text;
                customer.Cell = txtNumber.Text;
                db.Customers.Add(customer);
                db.SaveChanges();

                GL gl = new GL();

                gl.RID = 0;
                gl.RID2 = 0;
                gl.TypeCode = 0;
                gl.GLDate = dtEntryDate.Value;
                gl.AC_Code2 = 12000001;
                gl.Narration = "Opening Entry";
                gl.Debit = Convert.ToDouble(0);
                gl.Credit = Convert.ToDouble(0);

                gl.AC_Code = Convert.ToInt32(code);
                db.GL.Add(gl);

                //db.SaveChanges();

                GL gl2 = new GL();

                gl2.RID = 0;
                gl2.RID2 = 0;
                gl2.TypeCode = 0;
                gl2.GLDate = dtEntryDate.Value;
                gl2.AC_Code = 12000001;
                gl2.Narration = "Opening " + cmbxPatient.Text.Trim();
                gl2.Debit = Convert.ToDouble(0);
                gl2.Credit = Convert.ToDouble(0);

                gl2.AC_Code2 = Convert.ToInt32(code);
                db.GL.Add(gl2);

                db.SaveChanges();

                PatientCode = Convert.ToInt32(code);
            }
            int vebdorid = Convert.ToInt32(PatientCode);
            if (vebdorid == 0 || vebdorid == null)
            {
                MessageBox.Show("Please select AccountName", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //cmbxvendor.Focus();
                return;
            }


            transaction = db.Database.BeginTransaction();
            tbl_Opd sale = null;

            try
            {
                if (InvoiceNo == 0)
                {
                    sale = new tbl_Opd();
                    int ABC = db.tbl_Opd.Select(x => x.id).DefaultIfEmpty(0).Max();
                    sale.id = 1 + ABC;
                    sale.ItemID = Convert.ToInt32(cmbxFee.SelectedValue);
                    sale.PatientID = Convert.ToInt32(PatientCode);
                    //var RID = Convert.ToInt32(InvoiceNo);
                    //sale = db.Sales_M.Where(x => x.RID == RID).FirstOrDefault();
                    //sale.Edit_Date = DateTime.Now;
                    sale.DoctorID = Convert.ToInt32(cmbxDoctor.SelectedValue);
                    sale.DateTime = dtEntryDate.Value;
                    sale.fee = Convert.ToDecimal(txtAmount.Text.DefaultZero());
                    sale.discount = Convert.ToDecimal(txtDiscount.Text.DefaultZero());
                    sale.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text.DefaultZero());
                    sale.PaymentMode = Convert.ToInt32(cmbxPaymentMode.SelectedValue);
                }
                else
                {
                    var saleEdit = db.tbl_Opd.SingleOrDefault(b => b.id == InvoiceNo);
                    saleEdit.ItemID = Convert.ToInt32(cmbxFee.SelectedValue);
                    saleEdit.PatientID = Convert.ToInt32(code);
                    //var RID = Convert.ToInt32(InvoiceNo);
                    //sale = db.Sales_M.Where(x => x.RID == RID).FirstOrDefault();
                    //sale.Edit_Date = DateTime.Now;
                    saleEdit.DoctorID = Convert.ToInt32(cmbxDoctor.SelectedValue);
                    saleEdit.DateTime = dtEntryDate.Value;
                    saleEdit.fee = Convert.ToDecimal(txtAmount.Text.DefaultZero());
                    saleEdit.discount = Convert.ToDecimal(txtDiscount.Text.DefaultZero());
                    saleEdit.TotalAmount = Convert.ToDecimal(txtTotalAmount.Text.DefaultZero());
                    saleEdit.PaymentMode = Convert.ToInt32(cmbxPaymentMode.SelectedValue);
                    db.SaveChanges();
                }


                // sale.PatientID= Convert.ToInt32(cmbxvendor.SelectedValue);

                if (InvoiceNo == 0)
                {
                    db.tbl_Opd.Add(sale);
                    db.SaveChanges();
                }

                if (InvoiceNo != 0)
                {

                    // db.tbl_Opd.RemoveRange(db.tbl_Opd.Where(x => x.id == InvoiceNo));
                    db.Itemledger.RemoveRange(db.Itemledger.Where(x => x.RID == InvoiceNo && x.TypeCode == 25));
                    db.GL.RemoveRange(db.GL.Where(x => x.RID == InvoiceNo && x.TypeCode == 25));
                }



                int id = Convert.ToInt32(cmbxFee.SelectedValue);
                var item = db.Items.Where(x => x.IID == id).FirstOrDefault();

                int itemid = Convert.ToInt32(cmbxFee.SelectedValue);
                var checkstock = db.Items.Where(x => x.IID == itemid).FirstOrDefault();
                if (InvoiceNo == 0)
                {
                    if (checkstock.Inv_YN == false)
                    {
                        Itemledger ledger = new Itemledger();
                        ledger.RID = sale.id;
                        ledger.IID = Convert.ToInt32(cmbxFee.SelectedValue);
                        ledger.EDate = dtEntryDate.Value;
                        // ledger.Bnid = BatchNo
                        ledger.TypeCode = 25;
                        ledger.AC_CODE = Convert.ToInt32(PatientCode);
                        ledger.WID = 1;

                        // ledger.SiD =
                        //    var ctnledger = Convert.ToDouble(item.CTN_PCK.DefaultZero()) * Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                        //   ledger.CTN = Convert.ToInt32(row.Cells[2].Value.ToString());
                        ledger.PCS = Convert.ToInt32(1);
                        // ledger.SJ = ctn + Convert.ToInt32(row.Cells[3].Value);
                        //ledger.PurPrice = Convert.ToDouble(item.AveragePrice.DefaultZero());
                        ledger.SalesPriceP = Convert.ToInt32(txtAmount.Text.DefaultZero());//  ledger.Pamt = TotalAmount(pj * PurPrice)
                                                                                           //  ledger.DisP = Convert.ToDouble(row.Cells[6].Value.DefaultZero());
                                                                                           //ledger.DisAmount = percentage in rupess
                        ledger.DisRs = Convert.ToDouble(txtDiscount.Text.DefaultZero());
                        ledger.Amt = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        //ledger.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        db.Itemledger.Add(ledger);
                        db.SaveChanges();

                        GL gl = new GL();
                        gl.RID = sale.id;
                        gl.TypeCode = 25;
                        gl.GLDate = dtEntryDate.Value;
                        gl.IPrice = Convert.ToDouble(txtAmount.Text.DefaultZero());
                        gl.AC_Code = Convert.ToInt32(PatientCode);
                        gl.AC_Code2 = item.AC_Code_Inv;
                        gl.Narration = " ";
                        //  gl.MOP_ID = 2;
                        gl.Qty_Out = Convert.ToInt32(1);
                        //   gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        // gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl.DisRs = Convert.ToDouble((txtDiscount.Text));
                        //   gl.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl.Debit = Convert.ToDouble(txtTotalAmount.Text);
                        gl.Credit = 0;
                        db.GL.Add(gl);
                        db.SaveChanges();

                        GL gl1 = new GL();
                        gl1.RID = sale.id;
                        gl1.TypeCode = 25;
                        gl1.GLDate = dtEntryDate.Value;
                        gl1.IPrice = item.AveragePrice;
                        gl1.AC_Code = item.AC_Code_Inv;
                        gl1.AC_Code2 = Convert.ToInt32(PatientCode);
                        // gl1.Narration = cmbxvendor.SelectedText;
                        //  gl.MOP_ID = 2;
                        gl1.Qty_Out = Convert.ToInt32(1);
                        // gl1.PAmt = Convert.ToDouble(item.AveragePrice.DefaultZero()) * (ctn + Convert.ToInt32(row.Cells[3].Value));//Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        gl1.DisP = 0;// Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl1.DisRs = 0; //Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        gl1.DisAmt = 0;// Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl1.Debit = 0;
                        gl1.Credit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        db.GL.Add(gl1);
                        db.SaveChanges();


                        GL gl2 = new GL();
                        gl2.RID = sale.id;
                        gl2.TypeCode = 25;
                        gl2.GLDate = dtEntryDate.Value;
                        gl2.IPrice = Convert.ToDouble(txtAmount.Text);
                        gl2.AC_Code = item.AC_Code_Inc;
                        gl2.AC_Code2 = Convert.ToInt32(PatientCode);
                        //gl2.Narration = cmbxvendor.SelectedText;
                        //  gl.MOP_ID = 2;
                        gl2.Qty_Out = Convert.ToInt32(1);
                        //gl2.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        //  gl2.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl2.DisRs = Convert.ToDouble((txtDiscount.Text.DefaultZero()));
                        //  gl2.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl2.Debit = 0;
                        gl2.Credit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        db.GL.Add(gl2);
                        db.SaveChanges();

                        GL gl3 = new GL();
                        gl3.RID = sale.id;
                        gl3.TypeCode = 25;
                        gl3.GLDate = dtEntryDate.Value;

                        gl3.AC_Code = item.AC_Code_Cost;
                        gl3.AC_Code2 = Convert.ToInt32(PatientCode);
                        //  gl3.Narration = cmbxvendor.SelectedText;
                        //  gl.MOP_ID = 2;
                        gl3.Qty_Out = Convert.ToInt32(1);
                        //   gl3.PAmt = Convert.ToDouble(item.AveragePrice.DefaultZero()) * gl1.Qty_Out;//Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        gl3.DisP = 0;// Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl3.DisRs = 0; //Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        gl3.DisAmt = 0;// Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl3.Debit = Convert.ToDouble(item.AveragePrice.DefaultZero()) * gl1.Qty_Out;
                        gl3.Credit = 0;
                        db.GL.Add(gl3);
                        db.SaveChanges();

                    }
                    else
                    {
                        GL gl = new GL();
                        gl.RID = sale.id;
                        gl.TypeCode = 25;
                        gl.GLDate = dtEntryDate.Value;
                        gl.IPrice = Convert.ToDouble(txtAmount.Text);
                        gl.AC_Code = Convert.ToInt32(PatientCode);
                        gl.AC_Code2 = item.AC_Code_Inc;
                        // gl.Narration = row.Cells[1].Value.ToString();
                        //  gl.MOP_ID = 2;
                        // var ctnledger = Convert.ToDouble(item.CTN_PCK.DefaultZero()) * Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                        gl.Qty_Out = Convert.ToInt32(1);
                        //  gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        //gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl.DisRs = Convert.ToDouble((txtDiscount.Text).DefaultZero());
                        // gl.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl.Debit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        gl.Credit = 0;
                        db.GL.Add(gl);
                        db.SaveChanges();

                        GL gl1 = new GL();
                        gl1.RID = sale.id;
                        gl1.TypeCode = 5;
                        gl1.GLDate = dtEntryDate.Value;
                        gl1.IPrice = Convert.ToDouble(txtAmount);
                        gl1.AC_Code = item.AC_Code_Inc;
                        gl1.AC_Code2 = Convert.ToInt32(PatientCode);
                        //gl1.Narration = cmbxvendor.SelectedText;
                        //  gl.MOP_ID = 2;
                        gl1.Qty_Out = Convert.ToInt32(1);
                        //  gl1.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        // gl1.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl1.DisRs = Convert.ToDouble((txtDiscount.Text.DefaultZero()));
                        // gl1.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl1.Debit = 0;
                        gl1.Credit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        db.GL.Add(gl1);
                        db.SaveChanges();



                    }
                }

                else
                {

                    if (checkstock.Inv_YN == false)
                    {
                        Itemledger ledger = new Itemledger();
                        ledger.RID = InvoiceNo;
                        ledger.IID = Convert.ToInt32(cmbxFee.SelectedValue);
                        ledger.EDate = dtEntryDate.Value;
                        // ledger.Bnid = BatchNo
                        ledger.TypeCode = 25;
                        ledger.AC_CODE = Convert.ToInt32(PatientCode);
                        ledger.WID = 1;

                        // ledger.SiD =
                        //    var ctnledger = Convert.ToDouble(item.CTN_PCK.DefaultZero()) * Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                        //   ledger.CTN = Convert.ToInt32(row.Cells[2].Value.ToString());
                        ledger.PCS = Convert.ToInt32(1);
                        // ledger.SJ = ctn + Convert.ToInt32(row.Cells[3].Value);
                        //ledger.PurPrice = Convert.ToDouble(item.AveragePrice.DefaultZero());
                        ledger.SalesPriceP = Convert.ToInt32(txtAmount.Text.DefaultZero());//  ledger.Pamt = TotalAmount(pj * PurPrice)
                                                                                           //  ledger.DisP = Convert.ToDouble(row.Cells[6].Value.DefaultZero());
                                                                                           //ledger.DisAmount = percentage in rupess
                        ledger.DisRs = Convert.ToDouble(txtDiscount.Text.DefaultZero());
                        ledger.Amt = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        //ledger.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        db.Itemledger.Add(ledger);
                        db.SaveChanges();

                        GL gl = new GL();
                        gl.RID = InvoiceNo;
                        gl.TypeCode = 25;
                        gl.GLDate = dtEntryDate.Value;
                        gl.IPrice = Convert.ToDouble(txtAmount.Text.DefaultZero());
                        gl.AC_Code = Convert.ToInt32(PatientCode);
                        gl.AC_Code2 = item.AC_Code_Inv;
                        gl.Narration = " ";
                        //  gl.MOP_ID = 2;
                        gl.Qty_Out = Convert.ToInt32(1);
                        //   gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        // gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl.DisRs = Convert.ToDouble((txtDiscount.Text));
                        //   gl.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl.Debit = Convert.ToDouble(txtTotalAmount.Text);
                        gl.Credit = 0;
                        db.GL.Add(gl);
                        db.SaveChanges();

                        GL gl1 = new GL();
                        gl1.RID = InvoiceNo;
                        gl1.TypeCode = 25;
                        gl1.GLDate = dtEntryDate.Value;
                        gl1.IPrice = item.AveragePrice;
                        gl1.AC_Code = item.AC_Code_Inv;
                        gl1.AC_Code2 = Convert.ToInt32(PatientCode);
                        // gl1.Narration = cmbxvendor.SelectedText;
                        //  gl.MOP_ID = 2;
                        gl1.Qty_Out = Convert.ToInt32(1);
                        // gl1.PAmt = Convert.ToDouble(item.AveragePrice.DefaultZero()) * (ctn + Convert.ToInt32(row.Cells[3].Value));//Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        gl1.DisP = 0;// Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl1.DisRs = 0; //Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        gl1.DisAmt = 0;// Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl1.Debit = 0;
                        gl1.Credit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        db.GL.Add(gl1);
                        db.SaveChanges();


                        GL gl2 = new GL();
                        gl2.RID = InvoiceNo;
                        gl2.TypeCode = 25;
                        gl2.GLDate = dtEntryDate.Value;
                        gl2.IPrice = Convert.ToDouble(txtAmount.Text);
                        gl2.AC_Code = item.AC_Code_Inc;
                        gl2.AC_Code2 = Convert.ToInt32(PatientCode);
                        //gl2.Narration = cmbxvendor.SelectedText;
                        //  gl.MOP_ID = 2;
                        gl2.Qty_Out = Convert.ToInt32(1);
                        //gl2.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        //  gl2.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl2.DisRs = Convert.ToDouble((txtDiscount.Text.DefaultZero()));
                        //  gl2.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl2.Debit = 0;
                        gl2.Credit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        db.GL.Add(gl2);
                        db.SaveChanges();

                        GL gl3 = new GL();
                        gl3.RID = InvoiceNo;
                        gl3.TypeCode = 25;
                        gl3.GLDate = dtEntryDate.Value;

                        gl3.AC_Code = item.AC_Code_Cost;
                        gl3.AC_Code2 = Convert.ToInt32(PatientCode);
                        //  gl3.Narration = cmbxvendor.SelectedText;
                        //  gl.MOP_ID = 2;
                        gl3.Qty_Out = Convert.ToInt32(1);
                        //   gl3.PAmt = Convert.ToDouble(item.AveragePrice.DefaultZero()) * gl1.Qty_Out;//Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        gl3.DisP = 0;// Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl3.DisRs = 0; //Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        gl3.DisAmt = 0;// Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl3.Debit = Convert.ToDouble(item.AveragePrice.DefaultZero()) * gl1.Qty_Out;
                        gl3.Credit = 0;
                        db.GL.Add(gl3);
                        db.SaveChanges();

                    }
                    else
                    {
                        GL gl = new GL();
                        gl.RID = InvoiceNo;
                        gl.TypeCode = 25;
                        gl.GLDate = dtEntryDate.Value;
                        gl.IPrice = Convert.ToDouble(txtAmount.Text);
                        gl.AC_Code = Convert.ToInt32(PatientCode);
                        gl.AC_Code2 = item.AC_Code_Inc;
                        // gl.Narration = row.Cells[1].Value.ToString();
                        //  gl.MOP_ID = 2;
                        // var ctnledger = Convert.ToDouble(item.CTN_PCK.DefaultZero()) * Convert.ToDouble(row.Cells[2].Value.DefaultZero());
                        gl.Qty_Out = Convert.ToInt32(1);
                        //  gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        //gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl.DisRs = Convert.ToDouble((txtDiscount.Text).DefaultZero());
                        // gl.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl.Debit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        gl.Credit = 0;
                        db.GL.Add(gl);
                        db.SaveChanges();

                        GL gl1 = new GL();
                        gl1.RID = InvoiceNo;
                        gl1.TypeCode = 5;
                        gl1.GLDate = dtEntryDate.Value;
                        gl1.IPrice = Convert.ToDouble(txtAmount);
                        gl1.AC_Code = item.AC_Code_Inc;
                        gl1.AC_Code2 = Convert.ToInt32(PatientCode);
                        //gl1.Narration = cmbxvendor.SelectedText;
                        //  gl.MOP_ID = 2;
                        gl1.Qty_Out = Convert.ToInt32(1);
                        //  gl1.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        // gl1.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        gl1.DisRs = Convert.ToDouble((txtDiscount.Text.DefaultZero()));
                        // gl1.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl1.Debit = 0;
                        gl1.Credit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        db.GL.Add(gl1);
                        db.SaveChanges();



                    }

                }
                //if (txtDisfooter.Text != "0")
                //{
                //    int customer = Convert.ToInt32(cmbxvendor.SelectedIndex);
                //    var Expense = db.COA_D.Where(x => x.AC_Code == 16000001).FirstOrDefault();
                //    var customerDetial = db.COA_D.Where(x => x.AC_Code == customer).FirstOrDefault();

                //    GL gl = new GL();
                //    gl.RID = sale.RID;
                //    gl.TypeCode = 5;
                //    gl.GLDate = DateTime.Now;
                //    //  gl.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                //    gl.AC_Code = Expense.AC_Code;
                //    gl.AC_Code2 = Convert.ToInt32(cmbxvendor.SelectedValue);
                //    gl.Narration = cmbxvendor.SelectedText;
                //    //  gl.MOP_ID = 2;
                //    // gl.Qty_Out = (item.CTN_PCK ?? 0 * Convert.ToInt32(row.Cells[2].Value)) + Convert.ToInt32(row.Cells[3].Value);
                //    // gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                //    //  gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                //    //  gl.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                //    gl.DisAmt = 0;
                //    gl.Debit = Convert.ToDouble(txtDisfooter.Text);
                //    gl.Credit = 0;
                //    db.GL.Add(gl);
                //    db.SaveChanges();

                //    GL gl1 = new GL();
                //    gl1.RID = sale.RID;
                //    gl1.TypeCode = 5;
                //    gl1.GLDate = DateTime.Now;
                //    gl1.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                //    gl1.AC_Code = Convert.ToInt32(cmbxvendor.SelectedValue);
                //    gl1.AC_Code2 = Expense.AC_Code;
                //    gl1.Narration = Expense.AC_Title;
                //    //  gl.MOP_ID = 2;
                //    // gl1.Qty_Out = (item.CTN_PCK ?? 0 * Convert.ToInt32(row.Cells[2].Value)) + Convert.ToInt32(row.Cells[3].Value);
                //    //gl1.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                //    //gl1.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                //    //gl1.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                //    //gl1.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                //    gl1.Debit = 0;
                //    gl1.Credit = Convert.ToDouble(txtDisfooter.Text);
                //    db.GL.Add(gl1);
                //    db.SaveChanges();
                //}



                //var order = db.Sales_M.Where(x => x.RID == sale.id).FirstOrDefault();
                //var list = db.Sales_D.Where(x => x.RID == sale.id).ToList();
                //List<SaleInvoice> orderList = new List<SaleInvoice>();

                //int sNO = 1;


                //foreach (var itemName in list)
                //{
                //    SaleInvoice orders = new SaleInvoice();
                //    orders.InvoiceID = order.InvNo;
                //    orders.Client = db.COA_D.Where(x => x.AC_Code == order.AC_Code).FirstOrDefault().AC_Title;
                //    orders.Time = Convert.ToDateTime(order.EDate);

                //    //Pending
                //    //    orders.SNO = 
                //    orders.item = db.Items.Where(x => x.IID == itemName.IID).FirstOrDefault().IName; ;
                //    orders.Rate = Convert.ToDecimal(itemName.SalesPriceP);
                //    orders.DiscountRs = Convert.ToDecimal(itemName.DisAmt);
                //    orders.DiscountPercentage = Convert.ToDecimal(itemName.DisP);
                //    orders.Amount = Convert.ToDecimal(itemName.Amt);
                //    orders.Ctn = Convert.ToDecimal(itemName.CTN);
                //    orders.Pcs = Convert.ToDecimal(itemName.PCS);
                //    orders.SNO = sNO;
                //    sNO++;
                //    //orders.GrossAmt += orders.Rate = Convert.ToDecimal(itemName.SalesPriceP) * Convert.ToDecimal(itemName.Qty);
                //    //orders.DiscountTotal += Convert.ToDecimal(itemName.Amt);
                //    ////orders.Total = item.Rate * item.Qty;
                //    //orders.orderType = order.OrderType;
                //    //orders.TblID = order.TblID;
                //    //orders.Gst = order.GST;
                //    //var tbl = db.tbl_Table.Where(x => x.ID == order.TblID).FirstOrDefault();
                //    //if (tbl != null)
                //    //{
                //    //    orders.Tbl = tbl.TableName;
                //    //}
                //    //orders.WaiterID = order.WaiterID;
                //    //orders.item = db.Items.Where(x => x.IID == item.itemID).FirstOrDefault().IName;
                //    //orders.booker = "Owais";
                //    //orders.CatID = item.CatID ?? 0;
                //    //orders.ItemID = item.itemID ?? 0;
                //    //orders.Qty = item.Qty;
                //    //orders.Rate = item.Rate;
                //    //orders.CashCard = Convert.ToDecimal(txtCards2.Text == "" ? "0" : txtCards2.Text);
                //    orderList.Add(orders);
                //}


                //LocalReport localReport = new LocalReport();
                //localReport.DataSources.Add(new ReportDataSource("DataSet1", orderList));
                ////localReport.ReportPath = "SkyIce.Report1.rdlc";
                //localReport.ReportEmbeddedResource = "SalesMngmt.Reporting.Definition.SaleReceiptDisc.rdlc";
                ////    C:\Users\hair\Desktop\New folder (5)\Setup\New folder\SalesMgmt\SalesMngmt\Reporting\Definition\SaleReceiptDisc.rdlc

                //localReport.PrintToPrinter();
                int mode = Convert.ToInt32(cmbxPaymentMode.SelectedIndex);

                int Cashmode = Convert.ToInt32(cmbxPaymentMode.SelectedIndex);
                if (mode == 0)
                {



                }
                else if (Cashmode == 1)
                {




                }

                else
                {
                    if (InvoiceNo == 0)
                    {
                        GL gl = new GL();
                        gl.RID = sale.id;
                        gl.TypeCode = 25;
                        gl.GLDate = dtEntryDate.Value;
                        // gl.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                        gl.AC_Code = Convert.ToInt32(cmbxPaymentMode.SelectedValue);
                        gl.AC_Code2 = Convert.ToInt32(PatientCode);
                        // gl.Narration = cmbxvendor.SelectedText + " has paid cash";//row.Cells[1].Value.ToString();
                        //  gl.MOP_ID = 2;
                        //gl.Qty_Out =// ctnledger + Convert.ToInt32(row.Cells[3].Value);
                        //gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        //gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        //gl.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        //gl.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl.Debit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        gl.Credit = 0;
                        db.GL.Add(gl);
                        db.SaveChanges();

                        GL gl1 = new GL();
                        gl1.RID = sale.id;
                        gl1.TypeCode = 25;
                        gl1.GLDate = dtEntryDate.Value;
                        //   gl1.IPrice = item.AveragePrice;
                        gl1.AC_Code = Convert.ToInt32(PatientCode); //item.AC_Code_Inv;
                        gl1.AC_Code2 = Convert.ToInt32(cmbxPaymentMode.SelectedValue);
                        //  gl1.Narration = cm.SelectedText + " has paid cash";
                        //  gl.MOP_ID = 2;
                        //   gl1.Qty_Out = ctnledger + Convert.ToInt32(row.Cells[3].Value);
                        //  gl1.PAmt = Convert.ToDouble(item.AveragePrice.DefaultZero()) * (ctnledger + Convert.ToInt32(row.Cells[3].Value));//Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        //  gl1.DisP = 0;// Convert.ToDouble(row.Cells[6].Value ?? "0");
                        // gl1.DisRs = 0; //Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        // gl1.DisAmt = 0;// Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl1.Debit = 0;
                        gl1.Credit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        db.GL.Add(gl1);
                        db.SaveChanges();

                    }
                    else
                    {
                        GL gl = new GL();
                        gl.RID = InvoiceNo;
                        gl.TypeCode = 25;
                        gl.GLDate = dtEntryDate.Value;
                        // gl.IPrice = Convert.ToDouble(row.Cells[4].Value.DefaultZero());
                        gl.AC_Code = Convert.ToInt32(cmbxPaymentMode.SelectedValue);
                        gl.AC_Code2 = Convert.ToInt32(PatientCode);
                        // gl.Narration = cmbxvendor.SelectedText + " has paid cash";//row.Cells[1].Value.ToString();
                        //  gl.MOP_ID = 2;
                        //gl.Qty_Out =// ctnledger + Convert.ToInt32(row.Cells[3].Value);
                        //gl.PAmt = Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        //gl.DisP = Convert.ToDouble(row.Cells[6].Value ?? "0");
                        //gl.DisRs = Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        //gl.DisAmt = Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl.Debit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        gl.Credit = 0;
                        db.GL.Add(gl);
                        db.SaveChanges();

                        GL gl1 = new GL();
                        gl1.RID = InvoiceNo;
                        gl1.TypeCode = 25;
                        gl1.GLDate = dtEntryDate.Value;
                        //   gl1.IPrice = item.AveragePrice;
                        gl1.AC_Code = Convert.ToInt32(PatientCode); //item.AC_Code_Inv;
                        gl1.AC_Code2 = Convert.ToInt32(cmbxPaymentMode.SelectedValue);
                        //  gl1.Narration = cm.SelectedText + " has paid cash";
                        //  gl.MOP_ID = 2;
                        //   gl1.Qty_Out = ctnledger + Convert.ToInt32(row.Cells[3].Value);
                        //  gl1.PAmt = Convert.ToDouble(item.AveragePrice.DefaultZero()) * (ctnledger + Convert.ToInt32(row.Cells[3].Value));//Convert.ToDouble(row.Cells[5].Value.DefaultZero());
                        //  gl1.DisP = 0;// Convert.ToDouble(row.Cells[6].Value ?? "0");
                        // gl1.DisRs = 0; //Convert.ToDouble((row.Cells[7].Value ?? "0"));
                        // gl1.DisAmt = 0;// Convert.ToDouble(Convert.ToDouble(row.Cells[6].Value.DefaultZero()) / 100 * Convert.ToDouble(row.Cells[5].Value.DefaultZero()));//Convert.ToDouble(txtSaleRate.Text);
                        gl1.Debit = 0;
                        gl1.Credit = Convert.ToDouble(txtTotalAmount.Text.DefaultZero());
                        db.GL.Add(gl1);
                        db.SaveChanges();

                    }




                }
                InvoiceNo = 0;
                transaction.Commit();
                //  UpdateItemRate();
                MessageBox.Show("Invoice Save Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                bindGrid();
                pnlMain.Hide();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }
        public void clear()
        {

            var employee = new List<Customers>();
            employee.Add(new Customers { CusName = "Select", AC_Code = 0 });
            // employee.AddRange(db.Customers.Where(x => x.Cell == txtNumber.Text).ToList());
            FillCombo(cmbxPatient, employee, "CusName", "AC_Code", 0);

            txtNumber.Text = "0";


        }
        private void CoaDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            COADataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #region -- Helper Method Start --
        public void GetDocCode(string selected)
        {
            cmbxEmployeeType.DataSource = db.COA_M.ToList();
            cmbxEmployeeType.ValueMember = "CAC_Code";
            cmbxEmployeeType.DisplayMember = "CATitle";
            cmbxEmployeeType.SelectedIndex = Convert.ToInt32(selected) - 1;
        }
        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected = 1)
        {
            comboBox.DataSource = obj;
            comboBox.DisplayMember = Name; // Column Name
            comboBox.ValueMember = ID;  // Column Name
            comboBox.SelectedIndex = selected;
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { COADataGridView.DataSource = list; }
                else
                {
                    COADataGridView.DataSource = list.FindAll(x => x.AC_Title.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //
        #endregion -- Helper Method End --

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ddlCOA_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var ddl = (COA_M)ddlCOA.SelectedItem;
            //if (ddl.CAC_Code == 0)
            //{
            //    list = db.COA_D.ToList();
            //    COABindingSource.DataSource = list;
            //}
            //else
            //{
            //    list = db.COA_D.Where(x => x.CAC_Code == ddl.CAC_Code).ToList();
            //    COABindingSource.DataSource = list;
            //}
        }

        private void txtDebit_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbxEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsa = Convert.ToInt32(cmbxEmployeeType.SelectedIndex);

            if (dsa >= 1)
            {


                int value = Convert.ToInt32(cmbxEmployeeType.SelectedValue);
                var vendor = db.COA_D.AsNoTracking().Where(x => x.CAC_Code == value).ToList();

                var employee = new List<tbl_Employee>();
                employee.Add(new tbl_Employee { EmployeName = "Select", ID = 0 });
                employee.AddRange(db.tbl_Employee.Where(x => x.PyteTypeID == value).ToList());
                FillCombo(cmbxDoctor, employee, "EmployeName", "ID", 0);

            }
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumber_Leave(object sender, EventArgs e)
        {
            var check = db.Customers.AsNoTracking().Where(x => x.Cell == txtNumber.Text).ToList();
            if (check.Count == 0)
            {

                var employee = new List<Lib.Entity.Customers>();
                employee.Add(new Lib.Entity.Customers { CusName = "Select", AC_Code = 0 });
                // employee.AddRange(db.Customers.Where(x => x.Cell == txtNumber.Text).ToList());
                FillCombo(cmbxPatient, employee, "CusName", "AC_Code", 0);


            }
            else
            {

                var employee = new List<Lib.Entity.Customers>();
                employee.Add(new Customers { CusName = "Select", AC_Code = 0 });
                employee.AddRange(db.Customers.Where(x => x.Cell == txtNumber.Text).ToList());
                FillCombo(cmbxPatient, employee, "CusName", "AC_Code", 0);
            }
        }

        private void cmbxFee_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsa = Convert.ToInt32(cmbxFee.SelectedIndex);

            if (dsa >= 1)
            {


                int value = Convert.ToInt32(cmbxFee.SelectedValue);
                var item = db.Items.AsNoTracking().Where(x => x.IID == value).FirstOrDefault();

                txtAmount.Text = item.SalesPrice.DefaultZero().ToString();
                txtDiscount.Text = item.DisR.DefaultZero().ToString();
                txtTotalAmount.Text = (Convert.ToDouble(item.SalesPrice.DefaultZero()) - Convert.ToDouble(item.DisR.DefaultZero())).ToString();


            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            COADataGridView.Rows.Clear();
            var query = db.getOpdRecords(dtTo.Value, dtFrom.Value);

            foreach (var list in query)
            {

                var doctor = db.tbl_Employee.Where(x => x.ID == list.DoctorID).FirstOrDefault();
                var item = db.Items.Where(x => x.IID == list.ItemID).FirstOrDefault();
                var patient = db.Customers.Where(x => x.AC_Code == list.PatientID).FirstOrDefault();

                COADataGridView.Rows.Add(list.DoctorID, list.ItemID, list.PatientID, list.id, list.discount, list.fee, list.TotalAmount, doctor.EmployeName, item.IName, patient.CusName, list.DateTime
                    );



            }



        }
        public void bindGrid()
        {

            COADataGridView.Rows.Clear();
            var query = db.getOpdRecords(dtTo.Value, dtFrom.Value);

            foreach (var list in query)
            {

                var doctor = db.tbl_Employee.Where(x => x.ID == list.DoctorID).FirstOrDefault();
                var item = db.Items.Where(x => x.IID == list.ItemID).FirstOrDefault();
                var patient = db.Customers.Where(x => x.AC_Code == list.PatientID).FirstOrDefault();

                COADataGridView.Rows.Add(list.DoctorID, list.ItemID, list.PatientID, list.id, list.discount, list.fee, list.TotalAmount, doctor.EmployeName, item.IName, patient.CusName, list.DateTime
                    );



            }


        }

        private void COADataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (COADataGridView.Rows.Count >= 1)
            {

                var ItemID = Convert.ToInt32(COADataGridView.Rows[e.RowIndex].Cells[3].Value);
                InvoiceNo = ItemID;
                var opd = db.tbl_Opd.AsNoTracking().Where(x => x.id == ItemID).FirstOrDefault();
                cmbxFee.SelectedValue = opd.ItemID;
                txtAmount.Text = opd.fee.ToString();
                txtDiscount.Text = opd.discount.ToString();
                txtTotalAmount.Text = opd.TotalAmount.ToString();
                cmbxPaymentMode.SelectedValue = opd.PaymentMode;
                var custumer = db.Customers.AsNoTracking().Where(x => x.AC_Code == opd.PatientID).FirstOrDefault();
                txtNumber.Text = custumer.Cell.ToString();

                var employee = new List<Customers>();
                employee.Add(new Customers { CusName = "Select", AC_Code = 0 });
                employee.AddRange(db.Customers.Where(x => x.Cell == txtNumber.Text).ToList());
                FillCombo(cmbxPatient, employee, "CusName", "AC_Code", 0);

                cmbxPatient.SelectedValue = custumer.AC_Code;

                var doctor = db.tbl_Employee.AsNoTracking().Where(x => x.ID == opd.DoctorID).FirstOrDefault();

                var employeetype = new List<tbl_Employee>();
                employeetype.Add(new tbl_Employee { EmployeName = "Select", ID = 0 });
                employeetype.AddRange(db.tbl_Employee.Where(x => x.PyteTypeID == doctor.PyteTypeID).ToList());
                FillCombo(cmbxDoctor, employeetype, "EmployeName", "ID", 0);

                cmbxEmployeeType.SelectedValue = doctor.PyteTypeID;
                cmbxDoctor.SelectedValue = doctor.ID;
                dtEntryDate.Value = opd.DateTime.Value;
            }

            else
            {

                MessageBox.Show("datatable is empty");

            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            txtTotalAmount.Text = (Convert.ToDouble(txtAmount.Text.DefaultZero()) - Convert.ToDouble(txtDiscount.Text.DefaultZero())).ToString();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            txtTotalAmount.Text = (Convert.ToDouble(txtAmount.Text.DefaultZero()) - Convert.ToDouble(txtDiscount.Text)).ToString();
        }

        private void COADataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (COADataGridView.Rows.Count >= 1)
            {

                var ItemID = Convert.ToInt32(COADataGridView.Rows[e.RowIndex].Cells[3].Value);
                InvoiceNo = ItemID;
                var Cash = new List<COA_D>();
                Cash.Add(new COA_D { AC_Title = "Credit", AC_Code = 0 });
                Cash.AddRange(db.COA_D.Where(x => x.CAC_Code == 1).ToList());
                FillCombo(cmbxPaymentMode, Cash, "AC_Title", "AC_Code", 0);

                var opd = db.tbl_Opd.AsNoTracking().Where(x => x.id == ItemID).FirstOrDefault();
                cmbxFee.SelectedValue = opd.ItemID;
                txtAmount.Text = opd.fee.ToString();
                txtDiscount.Text = opd.discount.ToString();
                txtTotalAmount.Text = opd.TotalAmount.ToString();
                cmbxPaymentMode.SelectedValue = opd.PaymentMode;
                var custumer = db.Customers.AsNoTracking().Where(x => x.AC_Code == opd.PatientID).FirstOrDefault();
                txtNumber.Text = custumer.Cell.ToString();

                var employee = new List<Customers>();
                employee.Add(new Customers { CusName = "Select", AC_Code = 0 });
                employee.AddRange(db.Customers.Where(x => x.Cell == txtNumber.Text).ToList());
                FillCombo(cmbxPatient, employee, "CusName", "AC_Code", 0);

                cmbxPatient.SelectedValue = custumer.AC_Code;

                var doctor = db.tbl_Employee.AsNoTracking().Where(x => x.ID == opd.DoctorID).FirstOrDefault();

                var employeetype = new List<tbl_Employee>();
                employeetype.Add(new tbl_Employee { EmployeName = "Select", ID = 0 });
                employeetype.AddRange(db.tbl_Employee.Where(x => x.PyteTypeID == doctor.PyteTypeID).ToList());
                FillCombo(cmbxDoctor, employeetype, "EmployeName", "ID", 0);

                cmbxEmployeeType.SelectedValue = doctor.PyteTypeID;
                cmbxDoctor.SelectedValue = doctor.ID;
                dtEntryDate.Value = opd.DateTime.Value;
            }

            else
            {

                MessageBox.Show("datatable is empty");

            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtTo_ValueChanged(object sender, EventArgs e)
        {

        }
        //public patientRegistration()
        //{
        //    InitializeComponent();
        //}
    }
}
