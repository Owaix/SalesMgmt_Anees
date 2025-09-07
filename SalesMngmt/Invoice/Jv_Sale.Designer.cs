namespace SalesMngmt.Invoice
{
    partial class Jv_Sale
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.COADataGridView = new MetroFramework.Controls.MetroGrid();
            this.aCCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aCTitleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreditACcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DebitACcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreditAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DebitAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Images = new System.Windows.Forms.DataGridViewLinkColumn();
            this.btnSearch = new MetroFramework.Controls.MetroButton();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.dtFrom = new MetroFramework.Controls.MetroDateTime();
            this.dtTo = new MetroFramework.Controls.MetroDateTime();
            ((System.ComponentModel.ISupportInitialize)(this.COADataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // COADataGridView
            // 
            this.COADataGridView.AllowUserToAddRows = false;
            this.COADataGridView.AllowUserToDeleteRows = false;
            this.COADataGridView.AllowUserToResizeColumns = false;
            this.COADataGridView.AllowUserToResizeRows = false;
            this.COADataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.COADataGridView.BackgroundColor = System.Drawing.Color.White;
            this.COADataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.COADataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.COADataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.COADataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.COADataGridView.ColumnHeadersHeight = 44;
            this.COADataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aCCodeDataGridViewTextBoxColumn,
            this.Date,
            this.aCTitleDataGridViewTextBoxColumn,
            this.dRDataGridViewTextBoxColumn,
            this.cRDataGridViewTextBoxColumn,
            this.qtyDataGridViewTextBoxColumn,
            this.CreditACcode,
            this.DebitACcode,
            this.CreditAccount,
            this.DebitAccount,
            this.chkSale,
            this.Images});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Tan;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.COADataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.COADataGridView.EnableHeadersVisualStyles = false;
            this.COADataGridView.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.COADataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.COADataGridView.Location = new System.Drawing.Point(32, 50);
            this.COADataGridView.MultiSelect = false;
            this.COADataGridView.Name = "COADataGridView";
            this.COADataGridView.ReadOnly = true;
            this.COADataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.COADataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.COADataGridView.RowHeadersVisible = false;
            this.COADataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.COADataGridView.RowTemplate.Height = 30;
            this.COADataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.COADataGridView.Size = new System.Drawing.Size(907, 326);
            this.COADataGridView.TabIndex = 4;
            this.COADataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.COADataGridView_CellContentClick);
            // 
            // aCCodeDataGridViewTextBoxColumn
            // 
            this.aCCodeDataGridViewTextBoxColumn.HeaderText = "Rid";
            this.aCCodeDataGridViewTextBoxColumn.Name = "aCCodeDataGridViewTextBoxColumn";
            this.aCCodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.aCCodeDataGridViewTextBoxColumn.Visible = false;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // aCTitleDataGridViewTextBoxColumn
            // 
            this.aCTitleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.aCTitleDataGridViewTextBoxColumn.Name = "aCTitleDataGridViewTextBoxColumn";
            this.aCTitleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dRDataGridViewTextBoxColumn
            // 
            this.dRDataGridViewTextBoxColumn.HeaderText = "Debit";
            this.dRDataGridViewTextBoxColumn.Name = "dRDataGridViewTextBoxColumn";
            this.dRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cRDataGridViewTextBoxColumn
            // 
            this.cRDataGridViewTextBoxColumn.HeaderText = "Credit";
            this.cRDataGridViewTextBoxColumn.Name = "cRDataGridViewTextBoxColumn";
            this.cRDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // qtyDataGridViewTextBoxColumn
            // 
            this.qtyDataGridViewTextBoxColumn.HeaderText = "Amount";
            this.qtyDataGridViewTextBoxColumn.Name = "qtyDataGridViewTextBoxColumn";
            this.qtyDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // CreditACcode
            // 
            this.CreditACcode.HeaderText = "CreditACcode";
            this.CreditACcode.Name = "CreditACcode";
            this.CreditACcode.ReadOnly = true;
            this.CreditACcode.Visible = false;
            // 
            // DebitACcode
            // 
            this.DebitACcode.HeaderText = "DebitACcode";
            this.DebitACcode.Name = "DebitACcode";
            this.DebitACcode.ReadOnly = true;
            this.DebitACcode.Visible = false;
            // 
            // CreditAccount
            // 
            this.CreditAccount.HeaderText = "CreditAccount";
            this.CreditAccount.Name = "CreditAccount";
            this.CreditAccount.ReadOnly = true;
            this.CreditAccount.Visible = false;
            // 
            // DebitAccount
            // 
            this.DebitAccount.HeaderText = "DebitAccount";
            this.DebitAccount.Name = "DebitAccount";
            this.DebitAccount.ReadOnly = true;
            this.DebitAccount.Visible = false;
            // 
            // chkSale
            // 
            this.chkSale.HeaderText = "chkSale";
            this.chkSale.Name = "chkSale";
            this.chkSale.ReadOnly = true;
            this.chkSale.Visible = false;
            // 
            // Images
            // 
            this.Images.HeaderText = "Images";
            this.Images.Name = "Images";
            this.Images.ReadOnly = true;
            this.Images.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Images.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Images.Text = "Image";
            this.Images.UseColumnTextForLinkValue = true;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSearch.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(644, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 32);
            this.btnSearch.TabIndex = 29;
            this.btnSearch.Text = "Search";
            this.btnSearch.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnSearch.UseCustomBackColor = true;
            this.btnSearch.UseCustomForeColor = true;
            this.btnSearch.UseSelectable = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(429, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 15);
            this.label13.TabIndex = 28;
            this.label13.Text = "From";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(221, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 15);
            this.label14.TabIndex = 27;
            this.label14.Text = "To";
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(471, 3);
            this.dtFrom.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(167, 29);
            this.dtFrom.TabIndex = 26;
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(250, 3);
            this.dtTo.MinimumSize = new System.Drawing.Size(0, 29);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(173, 29);
            this.dtTo.TabIndex = 25;
            // 
            // Jv_Sale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(965, 388);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.COADataGridView);
            this.Name = "Jv_Sale";
            this.Text = "Jv_Sale";
            ((System.ComponentModel.ISupportInitialize)(this.COADataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroGrid COADataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn aCCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn aCTitleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreditACcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DebitACcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreditAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DebitAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn chkSale;
        private System.Windows.Forms.DataGridViewLinkColumn Images;
        private MetroFramework.Controls.MetroButton btnSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private MetroFramework.Controls.MetroDateTime dtFrom;
        private MetroFramework.Controls.MetroDateTime dtTo;
    }
}