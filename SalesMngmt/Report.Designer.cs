namespace SalesMngmt
{
    partial class Report
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.customerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notPaidMonthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cashBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barcodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vendorLedgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerLedgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expireItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalVoucherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareAccontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saleListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profitAndLossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customerToolStripMenuItem,
            this.notPaidMonthToolStripMenuItem,
            this.cashBookToolStripMenuItem,
            this.barcodeToolStripMenuItem,
            this.vendorLedgerToolStripMenuItem,
            this.customerLedgerToolStripMenuItem,
            this.expireItemsToolStripMenuItem,
            this.stockToolStripMenuItem,
            this.summaryToolStripMenuItem,
            this.generalVoucherToolStripMenuItem,
            this.compareAccontToolStripMenuItem,
            this.purchaseListToolStripMenuItem,
            this.saleListToolStripMenuItem,
            this.profitAndLossToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(20, 60);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1326, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // customerToolStripMenuItem
            // 
            this.customerToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.customerToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            this.customerToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.customerToolStripMenuItem.Text = "PaidMonth";
            this.customerToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.customerToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.customerToolStripMenuItem.Visible = false;
            this.customerToolStripMenuItem.Click += new System.EventHandler(this.customerToolStripMenuItem_Click);
            // 
            // notPaidMonthToolStripMenuItem
            // 
            this.notPaidMonthToolStripMenuItem.Name = "notPaidMonthToolStripMenuItem";
            this.notPaidMonthToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.notPaidMonthToolStripMenuItem.Text = "TrailBalance";
            this.notPaidMonthToolStripMenuItem.Click += new System.EventHandler(this.notPaidMonthToolStripMenuItem_Click);
            // 
            // cashBookToolStripMenuItem
            // 
            this.cashBookToolStripMenuItem.Name = "cashBookToolStripMenuItem";
            this.cashBookToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.cashBookToolStripMenuItem.Text = "Cash Book";
            this.cashBookToolStripMenuItem.Click += new System.EventHandler(this.cashBookToolStripMenuItem_Click);
            // 
            // barcodeToolStripMenuItem
            // 
            this.barcodeToolStripMenuItem.Name = "barcodeToolStripMenuItem";
            this.barcodeToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.barcodeToolStripMenuItem.Text = "Barcode";
            this.barcodeToolStripMenuItem.Click += new System.EventHandler(this.barcodeToolStripMenuItem_Click);
            // 
            // vendorLedgerToolStripMenuItem
            // 
            this.vendorLedgerToolStripMenuItem.Name = "vendorLedgerToolStripMenuItem";
            this.vendorLedgerToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.vendorLedgerToolStripMenuItem.Text = "VendorLedger";
            this.vendorLedgerToolStripMenuItem.Click += new System.EventHandler(this.vendorLedgerToolStripMenuItem_Click);
            // 
            // customerLedgerToolStripMenuItem
            // 
            this.customerLedgerToolStripMenuItem.Name = "customerLedgerToolStripMenuItem";
            this.customerLedgerToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.customerLedgerToolStripMenuItem.Text = "CustomerLedger";
            this.customerLedgerToolStripMenuItem.Click += new System.EventHandler(this.customerLedgerToolStripMenuItem_Click);
            // 
            // expireItemsToolStripMenuItem
            // 
            this.expireItemsToolStripMenuItem.Name = "expireItemsToolStripMenuItem";
            this.expireItemsToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.expireItemsToolStripMenuItem.Text = "Expire Items";
            this.expireItemsToolStripMenuItem.Click += new System.EventHandler(this.expireItemsToolStripMenuItem_Click);
            // 
            // stockToolStripMenuItem
            // 
            this.stockToolStripMenuItem.Name = "stockToolStripMenuItem";
            this.stockToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.stockToolStripMenuItem.Text = "Stock";
            this.stockToolStripMenuItem.Click += new System.EventHandler(this.stockToolStripMenuItem_Click);
            // 
            // summaryToolStripMenuItem
            // 
            this.summaryToolStripMenuItem.Name = "summaryToolStripMenuItem";
            this.summaryToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.summaryToolStripMenuItem.Text = "Demand";
            this.summaryToolStripMenuItem.Click += new System.EventHandler(this.summaryToolStripMenuItem_Click);
            // 
            // generalVoucherToolStripMenuItem
            // 
            this.generalVoucherToolStripMenuItem.Name = "generalVoucherToolStripMenuItem";
            this.generalVoucherToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.generalVoucherToolStripMenuItem.Text = "General Voucher";
            this.generalVoucherToolStripMenuItem.Click += new System.EventHandler(this.generalVoucherToolStripMenuItem_Click);
            // 
            // compareAccontToolStripMenuItem
            // 
            this.compareAccontToolStripMenuItem.Name = "compareAccontToolStripMenuItem";
            this.compareAccontToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.compareAccontToolStripMenuItem.Text = "CompareAccont";
            this.compareAccontToolStripMenuItem.Visible = false;
            this.compareAccontToolStripMenuItem.Click += new System.EventHandler(this.compareAccontToolStripMenuItem_Click);
            // 
            // purchaseListToolStripMenuItem
            // 
            this.purchaseListToolStripMenuItem.Name = "purchaseListToolStripMenuItem";
            this.purchaseListToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.purchaseListToolStripMenuItem.Text = "PurchaseList";
            this.purchaseListToolStripMenuItem.Click += new System.EventHandler(this.purchaseListToolStripMenuItem_Click);
            // 
            // saleListToolStripMenuItem
            // 
            this.saleListToolStripMenuItem.Name = "saleListToolStripMenuItem";
            this.saleListToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.saleListToolStripMenuItem.Text = "SaleList";
            this.saleListToolStripMenuItem.Click += new System.EventHandler(this.saleListToolStripMenuItem_Click);
            // 
            // profitAndLossToolStripMenuItem
            // 
            this.profitAndLossToolStripMenuItem.Name = "profitAndLossToolStripMenuItem";
            this.profitAndLossToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.profitAndLossToolStripMenuItem.Text = "ProfitAndLoss";
            this.profitAndLossToolStripMenuItem.Click += new System.EventHandler(this.profitAndLossToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(97, 20);
            this.toolStripMenuItem1.Text = "Item Summary";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click_1);
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 450);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Report";
            this.Text = "Reporting";
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem customerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notPaidMonthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cashBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem barcodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vendorLedgerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerLedgerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expireItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem summaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalVoucherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareAccontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaseListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profitAndLossToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        ///// <summary>
        ///// Clean up any resources being used.
        ///// </summary>
        ///// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //#region Windows Form Designer generated code

        ///// <summary>
        ///// Required method for Designer support - do not modify
        ///// the contents of this method with the code editor.
        ///// </summary>
        //private void InitializeComponent()
        //{
        //    this.components = new System.ComponentModel.Container();
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.Text = "Report";
        //}

        //#endregion
    }
}