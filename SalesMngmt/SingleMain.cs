using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using SalesMngmt.Configs;
using SalesMngmt.Invoice;
using SalesMngmt.Reporting;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace SalesMngmt
{
    public partial class SingleMain : MetroFramework.Forms.MetroForm
    {
        int CompanyID = 0;
        Lib.Entity.AspNetUsers User;
        public SingleMain(int cmpID, Lib.Entity.AspNetUsers user)
        {
            InitializeComponent();
            Shown += Config_Shown;
            CompanyID = cmpID;
            User = user;

            if (cmpID == 1024) {

                receiptToolStripMenuItem.Visible=false;
                receiptToolStripMenuItem.Visible = false;

                tableToolStripMenuItem.Visible = false;


                styleToolStripMenuItem.Visible = false;

                labToolStripMenuItem.Visible = false;

                karahiToolStripMenuItem.Visible = false;

                posToolStripMenuItem.Visible = false;

                expireItemToolStripMenuItem.Visible = false;


            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void itemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
          
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
          //  products.MdiParent = this;
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
            Configs.Size inv = new Configs.Size(CompanyID);
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
            Configs.Style inv = new Configs.Style(CompanyID);
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

            Configs.Table inv = new Configs.Table(CompanyID);
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

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer cst = new Customer(CompanyID);
            cst.MdiParent = this;
            cst.Show();
        }

        private void vendorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FVendor vandor = new FVendor(CompanyID);
            vandor.MdiParent = this;
            vandor.Show();

        }

        private void catergoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Catgory category = new Catgory(CompanyID);
            category.MdiParent = this;
            category.Show();
        }

        private void partyTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PartyType item = new PartyType(CompanyID);
            item.MdiParent = this;
            item.Show();
        }

        private void makerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Maker company = new Maker(CompanyID);
            company.MdiParent = this;
            company.Show();
        }

        private void articleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Artical rece = new Artical(CompanyID);
            rece.MdiParent = this;
            rece.Show();
        }

        private void colourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Colour ven = new Colour(CompanyID);
            ven.MdiParent = this;
            ven.Show();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products products = new Products(CompanyID);
          products.MdiParent = this;
            products.Show();
        }

        private void cOAToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Coa inv = new Coa(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void sizeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Configs.Size inv = new Configs.Size(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void employeeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Employee inv = new Employee(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void syncDatabaseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SyncClientDB syncClientDB = new SyncClientDB();
            syncClientDB.MdiParent = this;
            syncClientDB.Show();
        }

        private void styleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Configs.Style inv = new Configs.Style(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void unitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Unit inv = new Unit(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void tableToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Configs.Table inv = new Configs.Table(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void wareHouseToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            Warehouse inv = new Warehouse(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void articleTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArticleType inv = new ArticleType(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void paymentVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaymentVoucher item = new PaymentVoucher(CompanyID);
            item.MdiParent = this;
            item.Show();
        }

        private void receiveVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiveVoucher products = new ReceiveVoucher(CompanyID);
            products.MdiParent = this;
            products.Show();
        }

        private void expenseVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpenseVoucher inv = new ExpenseVoucher(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void itemAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemsAdjustment inv = new ItemsAdjustment(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PInv inv = new PInv(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void saleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SInv inv = new SInv(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void posToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void karahiToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void trialBalanceToolStripMenuItem_Click(object sender, EventArgs e)
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
            //Form1 inv = new Form1(CompanyID);
            //inv.MdiParent = this;
            //inv.Show();
        }

        private void expireItemToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void demandToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dayBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DayBook inv = new DayBook(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void cityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            City inv = new City(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void customerCurrentBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void vendorrCurrentBalanceListToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void databaseBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String DestPath = @"D:\Backup";
            String DbName = "SaleManagerShahzaib";
            try
            {
                string databaseName = DbName;//dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();

                //Define a Backup object variable.
                Backup sqlBackup = new Backup();

                ////Specify the type of backup, the description, the name, and the database to be backed up.
                sqlBackup.Action = BackupActionType.Database;
                sqlBackup.BackupSetDescription = "BackUp of:" + databaseName + "on" + DateTime.Now.ToShortDateString();
                sqlBackup.BackupSetName = "FullBackUp";
                sqlBackup.Database = databaseName;

                ////Declare a BackupDeviceItem
                string destinationPath = DestPath;
                Random _rdm = new Random();
                var rand = _rdm.Next(100000, 999999);
                string backupfileName = rand + DbName + ".bak";
                BackupDeviceItem deviceItem = new BackupDeviceItem(destinationPath + "\\" + backupfileName, DeviceType.File);
                ////Define Server connection

                //ServerConnection connection = new ServerConnection(frm.serverName, frm.userName, frm.password);
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
                ServerConnection connection = new ServerConnection(con);
                ////To Avoid TimeOut Exception
                Server sqlServer = new Server(connection);
                sqlServer.ConnectionContext.StatementTimeout = 60 * 60;
                Database db = sqlServer.Databases[databaseName];

                sqlBackup.Initialize = true;
                sqlBackup.Checksum = true;
                sqlBackup.ContinueAfterError = true;

                ////Add the device to the Backup object.
                sqlBackup.Devices.Add(deviceItem);
                ////Set the Incremental property to False to specify that this is a full database backup.
                sqlBackup.Incremental = false;

                sqlBackup.ExpirationDate = DateTime.Now.AddDays(3);
                ////Specify that the log must be truncated after the backup is complete.
                sqlBackup.LogTruncation = BackupTruncateLogType.Truncate;

                sqlBackup.FormatMedia = false;
                ////Run SqlBackup to perform the full database backup on the instance of SQL Server.
                sqlBackup.SqlBackup(sqlServer);
                ////Remove the backup device from the Backup object.
                sqlBackup.Devices.Remove(deviceItem);
                MessageBox.Show("Backup successfully Created In " + destinationPath + "\\" + backupfileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // MessageBox.Show(ex.Message);
            }


        }

        private void productListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductList inv = new ProductList(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void jVSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Jv_Sale inv = new Jv_Sale(CompanyID);
            inv.MdiParent = this;
            inv.Show();

        }

        private void itemWiseSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemSummary sum = new ItemSummary(CompanyID, 2);
            sum.MdiParent=this;
            sum.Show();
        }

        private void cashBookOpeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpeningCash sum = new OpeningCash(CompanyID);
            sum.Show();

        }

        private void receiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Receipe sum = new Receipe(CompanyID);
            sum.Show();
        }

        private void bikeAndSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bike_Sale_Purchase inv = new Bike_Sale_Purchase(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void customerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CustomerCurrentBalanceList inv = new CustomerCurrentBalanceList(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }

        private void vendorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            VendorCurrentBalanceList inv = new VendorCurrentBalanceList(CompanyID);
            inv.MdiParent = this;
            inv.Show();
        }
    }
}