using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
   public class KarahiReceipt : ReportsModel
    {

        public string Customer { get; set; }
        public string Date { get; set; }
       
       
        public string Client { get; set; }
        public string ItemNAme { get; set; }
        public string Karahi_No { get; set; }

        public Nullable<int> SNO { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<double> Qty { get; set; }
        public    int Rows { get; set; }
        public float RowHeight { get; set; }
        public String item { get; set; }
        public int CompanyID { get; set; }
        public string CompanyTitle { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string Note { get; set; }

    }
}
