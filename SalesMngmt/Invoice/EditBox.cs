using Lib.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class EditBox : MetroFramework.Forms.MetroForm
    {
        public String InvNo { get; set; }
        private int compID = 0;
        Pos purcha = null;
        string InvTyp = "";
        DataGridView grid = null;
        Label lblBal = null;
        public EditBox(string f, DataGridView selectedIndex, Label lblBa, String _UsrID , Pos _pos)
        {
            InitializeComponent();
            purcha = _pos ;
            InvTyp = f;
            grid = selectedIndex;
            lblBal = lblBa;
            var list = new List<COA_M>();
            list.Add(new COA_M { CAC_Code = 1, CATitle = "%" });
            list.Add(new COA_M { CAC_Code = 2, CATitle = "Rs" });
            cmbxPe.DataSource = list;
            cmbxPe.DisplayMember = "CATitle"; // Column Name
            cmbxPe.ValueMember = "CAC_Code";  // Column Name
            cmbxPe.SelectedIndex = 0;
        }

        private void EditMessageBox_Load(object sender, EventArgs e)
        {
            if (InvTyp == "qty")
            {
                this.Text = "Change Quantity";
            }
            else if (InvTyp == "Disc")
            {
                cmbxPe.Visible = true;
                this.Text = "ADD Discount";
            }
            else
            {
                this.Text = "Change Rate";
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            purcha.EditPOS(txtVal.Text.ToString(), InvTyp, grid, lblBal, Convert.ToInt32(cmbxPe.SelectedValue));
            this.Close();
        }

        private void metroButton34_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text == "." && !txtVal.Text.Contains('.'))
            {
                txtVal.Text += btn.Text;
            }
            if (btn.Text != ".")
            {
                txtVal.Text += btn.Text;
            }
        }
    }
}