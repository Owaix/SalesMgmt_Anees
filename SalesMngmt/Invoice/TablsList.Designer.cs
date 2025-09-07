namespace SalesMngmt.Invoice
{
    partial class TablsList
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbxWaiter = new MetroFramework.Controls.MetroComboBox();
            this.lblTblID = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.cmbxWaiter);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(27, 74);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1013, 455);
            this.panel5.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(705, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Waiter";
            // 
            // cmbxWaiter
            // 
            this.cmbxWaiter.FormattingEnabled = true;
            this.cmbxWaiter.ItemHeight = 24;
            this.cmbxWaiter.Location = new System.Drawing.Point(783, 16);
            this.cmbxWaiter.Name = "cmbxWaiter";
            this.cmbxWaiter.Size = new System.Drawing.Size(209, 30);
            this.cmbxWaiter.TabIndex = 0;
            this.cmbxWaiter.UseSelectable = true;
            this.cmbxWaiter.SelectedIndexChanged += new System.EventHandler(this.cmbxWaiter_SelectedIndexChanged);
            // 
            // lblTblID
            // 
            this.lblTblID.AutoSize = true;
            this.lblTblID.Location = new System.Drawing.Point(740, 31);
            this.lblTblID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTblID.Name = "lblTblID";
            this.lblTblID.Size = new System.Drawing.Size(16, 17);
            this.lblTblID.TabIndex = 12;
            this.lblTblID.Text = "0";
            this.lblTblID.Visible = false;
            // 
            // TablsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.lblTblID);
            this.Controls.Add(this.panel5);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TablsList";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Text = "Dine In Tables";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TablsList_FormClosing);
            this.Load += new System.EventHandler(this.TablsList_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTblID;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroComboBox cmbxWaiter;
    }
}