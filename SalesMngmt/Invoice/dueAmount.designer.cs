namespace SalesMngmt.Invoice
{
    partial class dueAmount
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OrderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dueAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PAID = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderId,
            this.OrderNo,
            this.dueAmt,
            this.EmployeName,
            this.OrderType,
            this.PAID});
            this.dataGridView1.Location = new System.Drawing.Point(44, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(890, 311);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // OrderId
            // 
            this.OrderId.DataPropertyName = "OrderId";
            this.OrderId.HeaderText = "Order ID";
            this.OrderId.Name = "OrderId";
            this.OrderId.Visible = false;
            // 
            // OrderNo
            // 
            this.OrderNo.DataPropertyName = "OrderNo";
            this.OrderNo.HeaderText = "Order No";
            this.OrderNo.Name = "OrderNo";
            // 
            // dueAmt
            // 
            this.dueAmt.DataPropertyName = "dueAmt";
            this.dueAmt.HeaderText = "Due Amount";
            this.dueAmt.Name = "dueAmt";
            // 
            // EmployeName
            // 
            this.EmployeName.DataPropertyName = "EmployeName";
            this.EmployeName.HeaderText = "Waiter";
            this.EmployeName.Name = "EmployeName";
            // 
            // OrderType
            // 
            this.OrderType.DataPropertyName = "OrderType";
            this.OrderType.HeaderText = "Order Type";
            this.OrderType.Name = "OrderType";
            // 
            // PAID
            // 
            this.PAID.HeaderText = "PAID";
            this.PAID.Name = "PAID";
            this.PAID.ReadOnly = true;
            this.PAID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PAID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PAID.Text = "PAID";
            this.PAID.ToolTipText = "PAID";
            this.PAID.UseColumnTextForButtonValue = true;
            // 
            // dueAmount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 459);
            this.Controls.Add(this.dataGridView1);
            this.Name = "dueAmount";
            this.Text = "Due Amount";
            this.Load += new System.EventHandler(this.dueAmount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dueAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderType;
        private System.Windows.Forms.DataGridViewButtonColumn PAID;
    }
}