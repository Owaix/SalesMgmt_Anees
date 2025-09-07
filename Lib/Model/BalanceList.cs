using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
   public class BalanceList : ReportsModel
    {
        public Nullable<int> Sno { get; set; }
        public string Name { get; set; }
     
        public double debit { get; set; }

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



