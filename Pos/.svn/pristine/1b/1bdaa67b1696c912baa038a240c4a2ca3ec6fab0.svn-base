using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
    public class DetailReportModel
    {
        public String InvoiceNo { get; set; }
        public DateTime Datetim { get; set; }
        public String userName { get; set; }
        public Decimal Amount { get; set; }
        public String ordrType { get; set; }
        public String KOTID { get; set; }
        public Decimal GST { get; set; }
        public Decimal Discount { get; set; }
        public String IName { get; set; }
        public Decimal Qty { get; set; }
        public Decimal Rate { get; set; }
        public int Rows { get; set; }

        public float RowHeight { get; set; }

        public string item { get; set; }

        public DetailReportModel(DataRow BookingSummaryDataRow)
        {
            try
            {
                if (BookingSummaryDataRow.Table.Columns.Contains("OrderNo") && !String.IsNullOrEmpty(BookingSummaryDataRow["OrderNo"].ToString()))
                { this.InvoiceNo = (String)BookingSummaryDataRow["OrderNo"]; }
                else { this.InvoiceNo = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("OrderDate") && !String.IsNullOrEmpty(BookingSummaryDataRow["OrderDate"].ToString()))
                { this.Datetim = (DateTime)BookingSummaryDataRow["OrderDate"]; }
                else { this.Datetim = DateTime.Now; }

                if (BookingSummaryDataRow.Table.Columns.Contains("UserName") && !String.IsNullOrEmpty(BookingSummaryDataRow["UserName"].ToString()))
                { this.userName = (String)BookingSummaryDataRow["UserName"]; }
                else { this.userName = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Amount") && !String.IsNullOrEmpty(BookingSummaryDataRow["Amount"].ToString()))
                { this.Amount = (Decimal)BookingSummaryDataRow["Amount"]; }
                else { this.Amount = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("OrderType") && !String.IsNullOrEmpty(BookingSummaryDataRow["OrderType"].ToString()))
                { this.ordrType = (String)BookingSummaryDataRow["OrderType"]; }
                else { this.ordrType = ""; }

                //if (BookingSummaryDataRow.Table.Columns.Contains("KOTID") && !String.IsNullOrEmpty(BookingSummaryDataRow["KOTID"].ToString()))
                //{ this.KOTID = (String)BookingSummaryDataRow["KOTID"]; }
                //else { this.KOTID = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("GST") && !String.IsNullOrEmpty(BookingSummaryDataRow["GST"].ToString()))
                { this.GST = (Decimal)BookingSummaryDataRow["GST"]; }
                else { this.GST = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Discount") && !String.IsNullOrEmpty(BookingSummaryDataRow["Discount"].ToString()))
                { this.Discount = (Decimal)BookingSummaryDataRow["Discount"]; }
                else { this.Discount = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("IName") && !String.IsNullOrEmpty(BookingSummaryDataRow["IName"].ToString()))
                { this.IName = (String)BookingSummaryDataRow["IName"]; }
                else { this.IName = ""; }


                if (BookingSummaryDataRow.Table.Columns.Contains("Qty") && !String.IsNullOrEmpty(BookingSummaryDataRow["Qty"].ToString()))
                { this.Qty = (Decimal)BookingSummaryDataRow["Qty"]; }
                else { this.Qty = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Rate") && !String.IsNullOrEmpty(BookingSummaryDataRow["Rate"].ToString()))
                { this.Rate = (Decimal)BookingSummaryDataRow["Rate"]; }
                else { this.Rate = 0; }


            }
            catch (Exception ex) { throw ex; }
        }
    }
}
