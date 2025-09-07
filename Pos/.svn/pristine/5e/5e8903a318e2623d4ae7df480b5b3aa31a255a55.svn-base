using System;
using System.Data;

namespace Lib.Model
{
    public class ItemReportModel
    {
        public String OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public int IID { get; set; }
        public String IName { get; set; }

        public int Qty { get; set; }
        public Decimal Rate { get; set; }

        public ItemReportModel(DataRow BookingSummaryDataRow)
        {
            try
            {
                if (BookingSummaryDataRow.Table.Columns.Contains("OrderNo") && !String.IsNullOrEmpty(BookingSummaryDataRow["OrderNo"].ToString()))
                { this.OrderNo = (String)BookingSummaryDataRow["OrderNo"]; }
                else { this.OrderNo = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("OrderDate") && !String.IsNullOrEmpty(BookingSummaryDataRow["OrderDate"].ToString()))
                { this.OrderDate = (DateTime)BookingSummaryDataRow["OrderDate"]; }
                else { this.OrderDate = DateTime.Now; }

                if (BookingSummaryDataRow.Table.Columns.Contains("IID") && !String.IsNullOrEmpty(BookingSummaryDataRow["IID"].ToString()))
                { this.IID = (int)BookingSummaryDataRow["IID"]; }
                else { this.IID = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("IName") && !String.IsNullOrEmpty(BookingSummaryDataRow["IName"].ToString()))
                { this.IName = (String)BookingSummaryDataRow["IName"]; }
                else { this.IName = ""; }


                if (BookingSummaryDataRow.Table.Columns.Contains("Qty") && !String.IsNullOrEmpty(BookingSummaryDataRow["Qty"].ToString()))
                { this.Qty = (int)BookingSummaryDataRow["Qty"]; }
                else { this.Qty = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Rate") && !String.IsNullOrEmpty(BookingSummaryDataRow["Rate"].ToString()))
                { this.Rate = (Decimal)BookingSummaryDataRow["Rate"]; }
                else { this.Rate = 0; }

            }
            catch (Exception ex) { throw ex; }
        }
    }
}
