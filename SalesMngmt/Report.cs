using LabExpressDesktop.Reporting;
using Lib.Entity;
using SalesMngmt.Configs;
using SalesMngmt.Invoice;
using SalesMngmt.Reporting;
using System;
using System.Windows.Forms;

namespace SalesMngmt
{
    public partial class Report : MetroFramework.Forms.MetroForm
    {
        int CompanyID = 0;
        AspNetUsers user;
        public Report(int cmpID, AspNetUsers aspNetUser)
        {
            InitializeComponent();
            Shown += Config_Shown;
            CompanyID = cmpID;
            user = aspNetUser;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paidCustomerByMonth cst = new paidCustomerByMonth(CompanyID);
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
            Main main = new Main(CompanyID, user);
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
            Pos inv = new Pos(user, CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void notPaidMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrialBalance inv = new TrialBalance(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void cashBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cashBook inv = new cashBook(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void barcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 inv = new Form1();
            inv.MdiParent = this;
            inv.Show();

        }

        private void vendorLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vendorLedgerSummary inv = new vendorLedgerSummary(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void customerLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomerLedgerSummary inv = new CustomerLedgerSummary(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void expireItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpiredItems inv = new ExpiredItems(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stock inv = new Stock(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void summaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Demand inv = new Demand(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void generalVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralLedger inv = new GeneralLedger(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void compareAccontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountComparesion inv = new AccountComparesion();
            inv.MdiParent = this;
            inv.Show();

        }

        private void purchaseListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurchaseList inv = new PurchaseList(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void saleListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaleList inv = new SaleList(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void profitAndLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfitAndLoss inv = new ProfitAndLoss(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            //ItemSummary sum = new ItemSummary(CompanyID);
            //sum.MdiParent = this;
            //sum.Show();
        }
    }
}
