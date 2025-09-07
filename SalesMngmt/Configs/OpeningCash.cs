using Lib.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class OpeningCash : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = new SaleManagerEntities();
        int companyID = 0;
        public OpeningCash(int company)
        {

            InitializeComponent();

            var openingTable = db.tbl_OpeningCash.AsNoTracking().Where(x=>x.CompanyID== company).FirstOrDefault();
            companyID = company;
            if (openingTable == null) {


            }

            else {

                txtAmt.Text = openingTable.Amount.ToString();
                txtID.Text = openingTable.CashOpeningId.ToString();
                dtOpeningCash.Value = (DateTime)openingTable.EntryDate;



            }
        }

        private void txtAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
         (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaleManagerEntities db1 = new SaleManagerEntities();
            tbl_OpeningCash cash = new tbl_OpeningCash(); 
            if (txtID.Text == "")
            {

                if (txtAmt.Text == "")
                {
                    cash.Amount = 0;
                    cash.EntryDate = dtOpeningCash.Value;
                    cash.CompanyID = companyID;
                    db1.tbl_OpeningCash.Add(cash);

                }
                else
                {

                    cash.Amount = Convert.ToDouble(txtAmt.Text);
                    cash.EntryDate = dtOpeningCash.Value;

                    cash.CompanyID = companyID;
                    db1.tbl_OpeningCash.Add(cash);
                
                }
            }

            else
            {
                if (txtAmt.Text == "")
                {
                    cash.CashOpeningId = Convert.ToInt32( txtID.Text);
                    cash.Amount = 0;
                    cash.EntryDate = dtOpeningCash.Value;
                    cash.CompanyID = companyID;
                    db1.Entry(cash).State = EntityState.Modified;

                }
                else
                {
                    cash.CashOpeningId = Convert.ToInt32(txtID.Text);
                    cash.Amount = Convert.ToDouble(txtAmt.Text);
                    cash.EntryDate = dtOpeningCash.Value;
                    cash.CompanyID = companyID;
                    db1.Entry(cash).State = EntityState.Modified;
                }
            }

            db1.SaveChanges();
        }
    }
}
