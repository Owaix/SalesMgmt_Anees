using Lib.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace SalesMngmt.Invoice
{
    public partial class EditMessageBox : MetroFramework.Forms.MetroForm
    {
        public String Inv_No { get; set; }
        private int compID = 0;
        PInv purcha = null;
        SInv Sale = null;
        SaleReturn saleReturn = null;
        string InvTyp = "";
        public EditMessageBox(PInv purchaseInv, int CompID, string InvType)
        {
            InitializeComponent();
            compID = CompID;
            purcha = purchaseInv;
            InvTyp = InvType;
        }
        public EditMessageBox(SInv SaleInv, int CompID, string InvType)
        {
            InitializeComponent();
            compID = CompID;
            Sale = SaleInv;
            InvTyp = InvType;
        }
        public EditMessageBox(SaleReturn SaleInv, int CompID, string InvType)
        {
            InitializeComponent();
            compID = CompID;
            saleReturn = SaleInv;
            InvTyp = InvType;
        }

        private void EditMessageBox_Load(object sender, EventArgs e)
        {
            cmbxInvNo.Focus();
            var db = new SaleManagerEntities();
            Inv_No = cmbxInvNo.Text;
            if (InvTyp == "PI")
            {
                List<Pur_M> inv = db.Pur_M.ToList();
                cmbxInvNo.DataSource = inv;
                cmbxInvNo.DisplayMember = "InvNo"; // Column Name
                cmbxInvNo.ValueMember = "RID";  // Column Name
                cmbxInvNo.SelectedIndex = -1;
                var list = inv.Select(x => new Pur_M
                {
                    InvNo = x.InvNo,
                    NetAmt = x.NetAmt
                }).ToList();
                dataGridView1.DataSource = list;
            }
            else if (InvTyp == "SI")
            {
                List<Sales_M> inv = db.Sales_M.ToList();
                cmbxInvNo.DataSource = inv;
                cmbxInvNo.DisplayMember = "InvNo"; // Column Name
                cmbxInvNo.ValueMember = "RID";  // Column Name
                cmbxInvNo.SelectedIndex = -1;
                var list = inv.Select(x => new Pur_M
                {
                    InvNo = x.InvNo,
                    NetAmt = x.NetAmt
                }).ToList();
                dataGridView1.DataSource = list;
            }


        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (Sale == null)
            {
                purcha.EditInv(cmbxInvNo.Text.ToString());
                purcha.SetInvID(cmbxInvNo.Text.ToString());
            }
            else
            {
                Sale.EditInv(cmbxInvNo.Text.ToString());
                Sale.SetInvID(cmbxInvNo.Text.ToString());
            }
            this.Close();
        }

        public void purchaseEdit(string invoice)
        {

            if (Sale == null)
            {
                purcha.EditInv(invoice);
                purcha.SetInvID(invoice);
            }
            else
            {
                Sale.EditInv(invoice);
                Sale.SetInvID(invoice);
            }
            this.Close();



        }
    }
}
