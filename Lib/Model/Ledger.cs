using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
   public class Ledger : ReportsModel
    {

        public Nullable<int> Sno { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double debit { get; set; }


        public double credit { get; set; }

        public double balance { get; set; }

        public string referernce { get; set; }

        public string OPeningDate { get; set; }

        public string ClosingDate { get; set; }
        public string Date { get; set; }
        public int CompanyID { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string Note { get; set; }

        public string LegerName { get; set; }

        public int Rows
        
           { get; set; }
      

        public float RowHeight
        { get; set; }

        public string item
        { get; set; }
    }
}
