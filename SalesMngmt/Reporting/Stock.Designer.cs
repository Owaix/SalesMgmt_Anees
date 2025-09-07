namespace SalesMngmt.Reporting
{
    partial class Stock
    {
        /// <summary>
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTotalStock = new MetroFramework.Controls.MetroPanel();
            this.lblStockValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CategorysDataGridView = new System.Windows.Forms.DataGridView();
            this.SNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opening = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pieces = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpireDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Closing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpenAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Average = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtTo = new MetroFramework.Controls.MetroDateTime();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.sizeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.recivedVoucharIndexResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.spgetArticleResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.articleTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.styleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbxCompany = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReport = new MetroFramework.Controls.MetroButton();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbxWareHouse = new MetroFramework.Controls.MetroComboBox();
            this.cmbxCat = new MetroFramework.Controls.MetroComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategorysDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recivedVoucharIndexResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spgetArticleResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.articleTypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalStock
            // 
            this.lblTotalStock.Controls.Add(this.lblStockValue);
            this.lblTotalStock.Controls.Add(this.label2);
            this.lblTotalStock.Controls.Add(this.CategorysDataGridView);
            this.lblTotalStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalStock.HorizontalScrollbarBarColor = true;
            this.lblTotalStock.HorizontalScrollbarHighlightOnWheel = false;
            this.lblTotalStock.HorizontalScrollbarSize = 10;
            this.lblTotalStock.Location = new System.Drawing.Point(20, 60);
            this.lblTotalStock.Name = "lblTotalStock";
            this.lblTotalStock.Size = new System.Drawing.Size(1217, 437);
            this.lblTotalStock.TabIndex = 0;
            this.lblTotalStock.VerticalScrollbarBarColor = true;
            this.lblTotalStock.VerticalScrollbarHighlightOnWheel = false;
            this.lblTotalStock.VerticalScrollbarSize = 10;
            // 
            // lblStockValue
            // 
            this.lblStockValue.AutoSize = true;
            this.lblStockValue.Location = new System.Drawing.Point(139, 365);
            this.lblStockValue.Name = "lblStockValue";
            this.lblStockValue.Size = new System.Drawing.Size(13, 13);
            this.lblStockValue.TabIndex = 8;
            this.lblStockValue.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Total Stock";
            // 
            // CategorysDataGridView
            // 
            this.CategorysDataGridView.AllowUserToAddRows = false;
            this.CategorysDataGridView.AllowUserToDeleteRows = false;
            this.CategorysDataGridView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CategorysDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CategorysDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Book Antiqua", 11.25F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CategorysDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.CategorysDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CategorysDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNO,
            this.ProductName,
            this.opening,
            this.Pieces,
            this.ExpireDate,
            this.Sale,
            this.Closing,
            this.SaleAmt,
            this.PurchaseAmt,
            this.OpenAmt,
            this.Average});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Tan;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CategorysDataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.CategorysDataGridView.EnableHeadersVisualStyles = false;
            this.CategorysDataGridView.Location = new System.Drawing.Point(104, 24);
            this.CategorysDataGridView.Name = "CategorysDataGridView";
            this.CategorysDataGridView.ReadOnly = true;
            this.CategorysDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.CategorysDataGridView.RowHeadersVisible = false;
            this.CategorysDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.CategorysDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CategorysDataGridView.Size = new System.Drawing.Size(988, 315);
            this.CategorysDataGridView.TabIndex = 6;
            this.CategorysDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CategorysDataGridView_CellClick);
            this.CategorysDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CategorysDataGridView_CellContentClick);
            // 
            // SNO
            // 
            this.SNO.HeaderText = "SNO";
            this.SNO.Name = "SNO";
            this.SNO.ReadOnly = true;
            // 
            // ProductName
            // 
            this.ProductName.HeaderText = "ProductName";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.Width = 300;
            // 
            // opening
            // 
            this.opening.HeaderText = "opening";
            this.opening.Name = "opening";
            this.opening.ReadOnly = true;
            // 
            // Pieces
            // 
            this.Pieces.HeaderText = "purchase";
            this.Pieces.Name = "Pieces";
            this.Pieces.ReadOnly = true;
            // 
            // ExpireDate
            // 
            this.ExpireDate.HeaderText = "Total";
            this.ExpireDate.Name = "ExpireDate";
            this.ExpireDate.ReadOnly = true;
            // 
            // Sale
            // 
            this.Sale.HeaderText = "Sale";
            this.Sale.Name = "Sale";
            this.Sale.ReadOnly = true;
            // 
            // Closing
            // 
            this.Closing.HeaderText = "Closing";
            this.Closing.Name = "Closing";
            this.Closing.ReadOnly = true;
            // 
            // SaleAmt
            // 
            this.SaleAmt.HeaderText = "SaleAmt";
            this.SaleAmt.Name = "SaleAmt";
            this.SaleAmt.ReadOnly = true;
            // 
            // PurchaseAmt
            // 
            this.PurchaseAmt.HeaderText = "PurchaseAmt";
            this.PurchaseAmt.Name = "PurchaseAmt";
            this.PurchaseAmt.ReadOnly = true;
            // 
            // OpenAmt
            // 
            this.OpenAmt.HeaderText = "OpenAmt";
            this.OpenAmt.Name = "OpenAmt";
            this.OpenAmt.ReadOnly = true;
            // 
            // Average
            // 
            this.Average.HeaderText = "Stock";
            this.Average.Name = "Average";
            this.Average.ReadOnly = true;
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(391, 24);
            this.dtTo.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(205, 29);
            this.dtTo.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(348, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Date";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1116, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(57, 32);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Search";
            this.btnSearch.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnSearch.UseCustomBackColor = true;
            this.btnSearch.UseCustomForeColor = true;
            this.btnSearch.UseSelectable = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // sizeBindingSource
            // 
            this.sizeBindingSource.DataSource = typeof(Lib.Entity.Sizes);
            // 
            // colorBindingSource
            // 
            this.colorBindingSource.DataSource = typeof(Lib.Entity.Colors);
            // 
            // CategoryBindingSource
            // 
            this.CategoryBindingSource.DataSource = typeof(Lib.Entity.Items_Cat);
            // 
            // recivedVoucharIndexResultBindingSource
            // 
            this.recivedVoucharIndexResultBindingSource.DataSource = typeof(Lib.Entity.RecivedVoucharIndex_Result);
            // 
            // spgetArticleResultBindingSource
            // 
            this.spgetArticleResultBindingSource.DataSource = typeof(Lib.Entity.sp_getArticle_Result);
            // 
            // articleTypeBindingSource
            // 
            this.articleTypeBindingSource.DataSource = typeof(Lib.Entity.ArticleTypes);
            // 
            // styleBindingSource
            // 
            this.styleBindingSource.DataSource = typeof(Lib.Entity.Styles);
            // 
            // cmbxCompany
            // 
            this.cmbxCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxCompany.FormattingEnabled = true;
            this.cmbxCompany.Location = new System.Drawing.Point(674, 24);
            this.cmbxCompany.Name = "cmbxCompany";
            this.cmbxCompany.Size = new System.Drawing.Size(156, 28);
            this.cmbxCompany.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(602, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Company";
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnReport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(1178, 19);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(57, 32);
            this.btnReport.TabIndex = 17;
            this.btnReport.Text = "Report";
            this.btnReport.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnReport.UseCustomBackColor = true;
            this.btnReport.UseCustomForeColor = true;
            this.btnReport.UseSelectable = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(131, 32);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(81, 15);
            this.label26.TabIndex = 83;
            this.label26.Text = "WareHouse";
            // 
            // cmbxWareHouse
            // 
            this.cmbxWareHouse.FormattingEnabled = true;
            this.cmbxWareHouse.ItemHeight = 23;
            this.cmbxWareHouse.Location = new System.Drawing.Point(217, 25);
            this.cmbxWareHouse.Name = "cmbxWareHouse";
            this.cmbxWareHouse.Size = new System.Drawing.Size(125, 29);
            this.cmbxWareHouse.TabIndex = 82;
            this.cmbxWareHouse.UseSelectable = true;
            // 
            // cmbxCat
            // 
            this.cmbxCat.AllowDrop = true;
            this.cmbxCat.DisplayFocus = true;
            this.cmbxCat.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.cmbxCat.FormattingEnabled = true;
            this.cmbxCat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbxCat.ItemHeight = 29;
            this.cmbxCat.Location = new System.Drawing.Point(917, 24);
            this.cmbxCat.Name = "cmbxCat";
            this.cmbxCat.Size = new System.Drawing.Size(193, 35);
            this.cmbxCat.TabIndex = 84;
            this.cmbxCat.UseSelectable = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(845, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 85;
            this.label3.Text = "Catergory";
            // 
            // Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 517);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbxCat);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.cmbxWareHouse);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbxCompany);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTotalStock);
            this.Controls.Add(this.dtTo);
            this.Name = "Stock";
            this.Text = "ItemStock";
            this.Load += new System.EventHandler(this.Category_Load);
            this.lblTotalStock.ResumeLayout(false);
            this.lblTotalStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategorysDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recivedVoucharIndexResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spgetArticleResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articleTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroPanel lblTotalStock;
        public System.Windows.Forms.BindingSource CategoryBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView CategorysDataGridView;
        private System.Windows.Forms.BindingSource colorBindingSource;
        private System.Windows.Forms.BindingSource sizeBindingSource;
        private System.Windows.Forms.BindingSource styleBindingSource;
        private System.Windows.Forms.BindingSource articleTypeBindingSource;
        private System.Windows.Forms.BindingSource spgetArticleResultBindingSource;
        private MetroFramework.Controls.MetroDateTime dtTo;
        private System.Windows.Forms.Label label5;
        private MetroFramework.Controls.MetroButton btnSearch;
        private System.Windows.Forms.BindingSource recivedVoucharIndexResultBindingSource;
        private System.Windows.Forms.ComboBox cmbxCompany;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroButton btnReport;
        private System.Windows.Forms.Label label26;
        private MetroFramework.Controls.MetroComboBox cmbxWareHouse;
        private System.Windows.Forms.Label lblStockValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn opening;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pieces;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpireDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sale;
        private System.Windows.Forms.DataGridViewTextBoxColumn Closing;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Average;
        private MetroFramework.Controls.MetroComboBox cmbxCat;
        private System.Windows.Forms.Label label3;
    }
}