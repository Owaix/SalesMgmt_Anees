namespace SalesMngmt.Reporting
{
    partial class ItemSummary
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.btnReport = new MetroFramework.Controls.MetroButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbxItem = new MetroFramework.Controls.MetroComboBox();
            this.dtFrom = new MetroFramework.Controls.MetroDateTime();
            this.dtTOdate = new MetroFramework.Controls.MetroDateTime();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.CategorysDataGridView = new System.Windows.Forms.DataGridView();
            this.SNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtTo = new MetroFramework.Controls.MetroDateTime();
            this.sizeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.recivedVoucharIndexResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.spgetArticleResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.articleTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.styleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.metroPanel1.SuspendLayout();
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
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.btnReport);
            this.metroPanel1.Controls.Add(this.label5);
            this.metroPanel1.Controls.Add(this.label6);
            this.metroPanel1.Controls.Add(this.label10);
            this.metroPanel1.Controls.Add(this.cmbxItem);
            this.metroPanel1.Controls.Add(this.dtFrom);
            this.metroPanel1.Controls.Add(this.dtTOdate);
            this.metroPanel1.Controls.Add(this.btnSearch);
            this.metroPanel1.Controls.Add(this.CategorysDataGridView);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(989, 450);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnReport.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(839, 3);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(94, 32);
            this.btnReport.TabIndex = 60;
            this.btnReport.Text = "Report";
            this.btnReport.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnReport.UseCustomBackColor = true;
            this.btnReport.UseCustomForeColor = true;
            this.btnReport.UseSelectable = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "To";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(256, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "From";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(534, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 15);
            this.label10.TabIndex = 58;
            this.label10.Text = "Item";
            // 
            // cmbxItem
            // 
            this.cmbxItem.FormattingEnabled = true;
            this.cmbxItem.ItemHeight = 23;
            this.cmbxItem.Location = new System.Drawing.Point(575, 6);
            this.cmbxItem.Name = "cmbxItem";
            this.cmbxItem.Size = new System.Drawing.Size(153, 29);
            this.cmbxItem.TabIndex = 59;
            this.cmbxItem.UseSelectable = true;
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(307, 5);
            this.dtFrom.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(203, 29);
            this.dtFrom.TabIndex = 11;
            // 
            // dtTOdate
            // 
            this.dtTOdate.Location = new System.Drawing.Point(41, 5);
            this.dtTOdate.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtTOdate.Name = "dtTOdate";
            this.dtTOdate.Size = new System.Drawing.Size(200, 29);
            this.dtTOdate.TabIndex = 12;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(737, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 32);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Search";
            this.btnSearch.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnSearch.UseCustomBackColor = true;
            this.btnSearch.UseCustomForeColor = true;
            this.btnSearch.UseSelectable = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // CategorysDataGridView
            // 
            this.CategorysDataGridView.AllowUserToAddRows = false;
            this.CategorysDataGridView.AllowUserToDeleteRows = false;
            this.CategorysDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CategorysDataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CategorysDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CategorysDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CategorysDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CategorysDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNO,
            this.Date,
            this.Reference,
            this.Debit});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Tan;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CategorysDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.CategorysDataGridView.EnableHeadersVisualStyles = false;
            this.CategorysDataGridView.Location = new System.Drawing.Point(3, 41);
            this.CategorysDataGridView.Name = "CategorysDataGridView";
            this.CategorysDataGridView.ReadOnly = true;
            this.CategorysDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.CategorysDataGridView.RowHeadersVisible = false;
            this.CategorysDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.CategorysDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CategorysDataGridView.Size = new System.Drawing.Size(859, 370);
            this.CategorysDataGridView.TabIndex = 6;
            this.CategorysDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CategorysDataGridView_CellClick);
            this.CategorysDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CategorysDataGridView_CellContentClick);
            // 
            // SNO
            // 
            this.SNO.HeaderText = "Serial";
            this.SNO.Name = "SNO";
            this.SNO.ReadOnly = true;
            // 
            // Date
            // 
            this.Date.HeaderText = "Item";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Reference
            // 
            this.Reference.HeaderText = "Quantity";
            this.Reference.Name = "Reference";
            this.Reference.ReadOnly = true;
            // 
            // Debit
            // 
            this.Debit.HeaderText = "Amount";
            this.Debit.Name = "Debit";
            this.Debit.ReadOnly = true;
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(230, 25);
            this.dtTo.MinimumSize = new System.Drawing.Size(4, 29);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(98, 29);
            this.dtTo.TabIndex = 10;
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
            // ItemSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 450);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.dtTo);
            this.Name = "ItemSummary";
            this.Text = "Item Summary";
            this.Load += new System.EventHandler(this.Category_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategorysDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sizeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recivedVoucharIndexResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spgetArticleResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articleTypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        public System.Windows.Forms.BindingSource CategoryBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView CategorysDataGridView;
        private System.Windows.Forms.BindingSource colorBindingSource;
        private System.Windows.Forms.BindingSource sizeBindingSource;
        private System.Windows.Forms.BindingSource styleBindingSource;
        private System.Windows.Forms.BindingSource articleTypeBindingSource;
        private System.Windows.Forms.BindingSource spgetArticleResultBindingSource;
        private MetroFramework.Controls.MetroDateTime dtTo;
        private MetroFramework.Controls.MetroDateTime dtFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private MetroFramework.Controls.MetroButton btnSearch;
        private System.Windows.Forms.BindingSource recivedVoucharIndexResultBindingSource;
        private MetroFramework.Controls.MetroComboBox cmbxItem;
        private System.Windows.Forms.Label label10;
        private MetroFramework.Controls.MetroDateTime dtTOdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reference;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debit;
        private MetroFramework.Controls.MetroButton btnReport;



        ///// <summary>
        ///// Required designer variable.
        ///// </summary>
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
        //    this.Text = "Colour";
        //}

        //#endregion


        /// <summary>




        /// <summary>
        /// Required designer variable.
        ///// </summary>
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
        //    this.Text = "VendorLedger";
        //}

        //#endregion












        ///// <summary>
        ///// Required designer variable.
        ///// </summary>
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
        //    this.Text = "CustomerVendor";
        //}

        //#endregion



    }
}