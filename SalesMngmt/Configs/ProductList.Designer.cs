namespace SalesMngmt.Reporting
{
    partial class ProductList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.CategorysDataGridView = new System.Windows.Forms.DataGridView();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.cmbxCompany = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdate = new MetroFramework.Controls.MetroButton();
            this.Update = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WholeSale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Average = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.maker = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategorysDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.checkBox1);
            this.metroPanel1.Controls.Add(this.CategorysDataGridView);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(1242, 472);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(71, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 62;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // CategorysDataGridView
            // 
            this.CategorysDataGridView.AllowUserToAddRows = false;
            this.CategorysDataGridView.AllowUserToDeleteRows = false;
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
            this.Update,
            this.SNO,
            this.ID,
            this.ItemName,
            this.SalePrice,
            this.WholeSale,
            this.Average,
            this.ctn,
            this.category,
            this.unit,
            this.maker});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Tan;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CategorysDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.CategorysDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CategorysDataGridView.EnableHeadersVisualStyles = false;
            this.CategorysDataGridView.Location = new System.Drawing.Point(0, 0);
            this.CategorysDataGridView.MultiSelect = false;
            this.CategorysDataGridView.Name = "CategorysDataGridView";
            this.CategorysDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.CategorysDataGridView.RowHeadersVisible = false;
            this.CategorysDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.CategorysDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.CategorysDataGridView.Size = new System.Drawing.Size(1242, 472);
            this.CategorysDataGridView.TabIndex = 6;
            this.CategorysDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CategorysDataGridView_CellClick);
            this.CategorysDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CategorysDataGridView_CellContentClick);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(544, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(104, 28);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Search";
            this.btnSearch.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnSearch.UseCustomBackColor = true;
            this.btnSearch.UseCustomForeColor = true;
            this.btnSearch.UseSelectable = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbxCompany
            // 
            this.cmbxCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxCompany.FormattingEnabled = true;
            this.cmbxCompany.Location = new System.Drawing.Point(294, 18);
            this.cmbxCompany.Name = "cmbxCompany";
            this.cmbxCompany.Size = new System.Drawing.Size(244, 28);
            this.cmbxCompany.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(208, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Company";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(654, 18);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(93, 28);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnUpdate.UseCustomBackColor = true;
            this.btnUpdate.UseCustomForeColor = true;
            this.btnUpdate.UseSelectable = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // Update
            // 
            this.Update.HeaderText = "Update";
            this.Update.Name = "Update";
            // 
            // SNO
            // 
            this.SNO.HeaderText = "S #";
            this.SNO.Name = "SNO";
            this.SNO.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 600;
            // 
            // SalePrice
            // 
            this.SalePrice.HeaderText = "Sale Price";
            this.SalePrice.Name = "SalePrice";
            // 
            // WholeSale
            // 
            this.WholeSale.HeaderText = "WholeSale";
            this.WholeSale.Name = "WholeSale";
            // 
            // Average
            // 
            this.Average.HeaderText = "Average";
            this.Average.Name = "Average";
            // 
            // ctn
            // 
            this.ctn.HeaderText = "ctn";
            this.ctn.Name = "ctn";
            this.ctn.Visible = false;
            // 
            // category
            // 
            this.category.HeaderText = "Category";
            this.category.Name = "category";
            this.category.Width = 150;
            // 
            // unit
            // 
            this.unit.HeaderText = "Unit";
            this.unit.Name = "unit";
            this.unit.Width = 150;
            // 
            // maker
            // 
            this.maker.HeaderText = "Maker";
            this.maker.Name = "maker";
            this.maker.Width = 150;
            // 
            // ProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1592, 670);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbxCompany);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.metroPanel1);
            this.Name = "ProductList";
            this.Text = "List of Products";
            this.Load += new System.EventHandler(this.Category_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategorysDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView CategorysDataGridView;
        private MetroFramework.Controls.MetroButton btnSearch;
        private System.Windows.Forms.ComboBox cmbxCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private MetroFramework.Controls.MetroButton btnUpdate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Update;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn WholeSale;
        private System.Windows.Forms.DataGridViewTextBoxColumn Average;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctn;
        private System.Windows.Forms.DataGridViewComboBoxColumn category;
        private System.Windows.Forms.DataGridViewComboBoxColumn unit;
        private System.Windows.Forms.DataGridViewComboBoxColumn maker;
    }
}