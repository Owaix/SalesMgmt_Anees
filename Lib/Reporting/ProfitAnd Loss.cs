using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Reporting
{
  public  class ProfitAnd_Loss
    {
        public string SNO { get; set; }
        public String AccountHeader { get; set; }

        public string Referece { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public double TotalIncome { get; set; }

        public double TotalExpense { get; set; }

        public double TotalCost { get; set; }

        public double Remaining { get; set; }

    }
}
