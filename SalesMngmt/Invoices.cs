using Lib.Entity;
using SalesMngmt.Configs;
using SalesMngmt.Invoice;
using SalesMngmt.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt
{
    public partial class Invoices : MetroFramework.Forms.MetroForm
    {
        int CompanyID = 0;
        string userID = "0";
        AspNetUsers User;

        public Invoices(int cmpID, AspNetUsers _user)
        {
            InitializeComponent();
            Shown += Config_Shown;
            CompanyID = cmpID;
            User = _user;
            userID = _user.Id;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerVendor cst = new CustomerVendor(CompanyID);
            cst.MdiParent = this;
            cst.Show();
        }

        private void itemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PaymentVoucher item = new PaymentVoucher(CompanyID);
            item.MdiParent = this;
            item.Show();
        }

        private void Config_Shown(object sender, EventArgs e)
        {
            Main main = new Main(CompanyID, User);
            main.Close();
        }

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Company company = new Company();
            company.MdiParent = this;
            company.Show();
        }

        private void vendorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FVendor vandor = new FVendor(CompanyID);
            vandor.MdiParent = this;
            vandor.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            PaymentVoucher rece = new PaymentVoucher(CompanyID);
            rece.MdiParent = this;
            rece.Show();
            //Unit unit = new Unit(CompanyID);
            //unit.MdiParent = this;
            //unit.Show();
        }

        private void itemsToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void itemCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void itemsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            VendorLedger category = new VendorLedger();
            category.MdiParent = this;
            category.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            VendorLedger ven = new VendorLedger();
            ven.MdiParent = this;
            ven.Show();
            //Maker maker = new Maker(CompanyID);
            //maker.MdiParent = this;
            //maker.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ReceiveVoucher products = new ReceiveVoucher(CompanyID);
            products.MdiParent = this;
            products.Show();
        }

        private void cOAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Coa coa = new Coa(CompanyID);
            //coa.MdiParent = this;
            //coa.Show();
            ExpenseVoucher inv = new ExpenseVoucher(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void itemAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemsAdjustment inv = new ItemsAdjustment(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void purchaseInvoiceqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PInv inv = new PInv(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void saleInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SInv inv = new SInv(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void pointOfSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pos inv = new Pos(User, CompanyID);
           // inv.MdiParent = this;
            inv.Show();

        }

        private void journalVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JournalVoucher
                inv = new JournalVoucher(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void labToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labINventory
                         inv = new labINventory(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void saleReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaleReturn
                        inv = new SaleReturn(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void karahiInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Karahi k = new Karahi(CompanyID);
            k.MdiParent = this;
            k.Show();
        }

        private void purchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurchaseReturn k = new PurchaseReturn(CompanyID);
            k.MdiParent = this;
            k.Show();

        }
    }
    //public Invoices()
    //{
    //    InitializeComponent();
    //}

}
