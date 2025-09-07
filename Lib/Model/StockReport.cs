using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model 
{
    public class StockReport
    {
        public Nullable<int>  Sno { get; set; }
        public string ProductName { get; set; }

        public double Opening { get; set; }


        public double purchase { get; set; }

        public double Total { get; set; }
        public double Average { get; set; }

        public double Sale { get; set; }

        public double Closing { get; set; }

        public string Date { get; set; }

        public string Company { get; set; }

        public double TotalPurchase { get; set; }

        public double TotalSale { get; set; }
        public double OpenAmt { get; set; }
    }
}






