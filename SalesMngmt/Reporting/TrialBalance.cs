using Lib.Entity;
using Lib.Reporting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Reporting
{
    public partial class TrialBalance : Form
    {

        SaleManagerEntities db = null;
        int compy = 0;
        public TrialBalance(int company)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compy = company;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();

            var coa_M = db.COA_M.ToList();

            foreach (var M in coa_M) {



                var Coa_D = db.COA_D.AsNoTracking().Where(x => x.CAC_Code == M.CAC_Code && x.CompanyID == compy).ToList();
                double credit = 0;
                double debit = 0;
             
                foreach (var D in Coa_D) {
                    var total = ReportsController.getTotalDebitAndCreditByACcode((int)D.AC_Code, dtpDate.Value);

                    credit += (double)total.Rows[0]["credit"];
                    debit += (double)total.Rows[0]["debit"];


                }
                if (credit > debit)
                {
                    dataGridView1.Rows.Add(M.CATitle, 0, credit - debit);

                }


                else if (credit < debit) {

                    dataGridView1.Rows.Add(M.CATitle, debit - credit,0);

                }


                else if(debit==credit){



                }

               
            }

            double TotalDebit = 0;
            double TotalCredit = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                TotalDebit += Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value);
                TotalCredit += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value);
            }
            lblDebit.Text = TotalDebit.ToString();
            lblCredit.Text = TotalCredit.ToString();
        }
    }
}
