using System;

namespace Lib.Model
{
    public class SaleInvoice : ReportsModel
    {

        public string InvoiceID { get; set; }
        public string Time { get; set; }
        public string user { get; set; }
        public string Client { get; set; }
        public Nullable<decimal> Ctn { get; set; }
        public Nullable<Double> GrossAmt { get; set; }
        public Nullable<decimal> Pcs { get; set; }

        public string ExpDate { get; set; }

        public string ReferenceNo { get; set; }
        public string BatchNo { get; set; }

        public Nullable<int> SNO { get; set; }
        public String item { get; set; }
        public Nullable<double> Qty { get; set; }
        public String City { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> DiscountPercentage { get; set; }
        public Nullable<decimal> DiscountRs { get; set; }
        public Nullable<Double> DiscountTotal { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> GrossTotal { get; set; }
        public Nullable<decimal> TotalDiscount { get; set; }

        public string CompanyTitle { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyAddress { get; set; }
        public int Rows { get; set;  }
        public float RowHeight { get; set; }
        public int CompanyID { get; set; }
        public string Note { get; set; }
        public Nullable<decimal> PREVBALANNCE { get; set; }

        public Nullable<decimal> RCVDBALANCE { get; set; }
    }
}