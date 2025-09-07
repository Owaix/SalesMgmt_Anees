using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Reporting
{
   public class ReceiveVoucher
    {

        public string SNO { get; set; }
        public String PaymentMode { get; set; }
        public String Date { get; set; }
        public string Customer { get; set; }

        public string Description { get; set; }
      public string CompanyTitle { get; set; }
        public string CompanyAddress { get; set; }

        public string Amount { get; set; }

        public string PreviousAmount { get; set; }

        public string CompanyPhone { get; set; }


    }
}
