using SalesMngmt.Configs;
using System;

namespace SalesMngmt
{
    public partial class Config : MetroFramework.Forms.MetroForm
    {
        int CompanyID = 0;
        Lib.Entity.AspNetUsers User;
        public Config(int cmpID, Lib.Entity.AspNetUsers user)
        {
            InitializeComponent();
            Shown += Config_Shown;
            CompanyID = cmpID;
            User = user;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer cst = new Customer(CompanyID);
            cst.MdiParent = this;
            cst.Show();
        }

        private void itemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PartyType item = new PartyType(CompanyID);
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
            Maker company = new Maker(CompanyID);
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

            Artical rece = new Artical(CompanyID);
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
            Catgory category = new Catgory(CompanyID);
            category.MdiParent = this;
            category.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Colour ven = new Colour(CompanyID);
            ven.MdiParent = this;
            ven.Show();
            //Maker maker = new Maker(CompanyID);
            //maker.MdiParent = this;
            //maker.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Products products = new Products(CompanyID);
            products.MdiParent = this;
            products.Show();
        }

        private void cOAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Coa coa = new Coa(CompanyID);
            //coa.MdiParent = this;
            //coa.Show();
            Coa inv = new Coa(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size inv = new Size(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee inv = new Employee(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void styleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Style inv = new Style(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void unitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Unit inv = new Unit(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void tableToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Table inv = new Table(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void wareHouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Warehouse inv = new Warehouse();
            //inv.MdiParent = this;
            //inv.Show();
        }

        private void wareHouseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Warehouse inv = new Warehouse(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void articalTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArticleType inv = new ArticleType(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void openCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpeningCash inv = new OpeningCash(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void syncDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SyncClientDB syncClientDB = new SyncClientDB();
            syncClientDB.MdiParent = this;
            syncClientDB.Show();
        }

        private void menuStrip1_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {

        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Receipe syncClientDB = new Receipe(CompanyID);

         //   Users syncClientDB = new Users(CompanyID);
            syncClientDB.MdiParent = this;
            syncClientDB.Show();
        }
    }
}