using System;
using System.Data;

namespace Lib.Reporting.ReportModel
{
    /// <summary>
    /// Business functions of BookingSummary object
    /// </summary>
    public class BookingSummary
    {
        #region ----- Properties -------

        public string MyProperty { get; set; }
        public String crtBy { get; set; }

        public Decimal rcvdAmount { get; set; }

        public Decimal totalrcvdAmount { get; set; }
        public String labno { get; set; }

        public String reportName { get; set; }

        public Int32 reportCount { get; set; }

        public DateTime Date { get; set; }

        public String Stat { get; set; }

        public String Patient { get; set; }

        public String Authorized_By { get; set; }

        public Decimal Gross { get; set; }

        public Decimal Disc { get; set; }

        public Decimal Net_Amount { get; set; }

        public DateTime dtStart { get; set; }

        public DateTime dtEnd { get; set; }

        public String userName { get; set; }

        public String ward { get; set; }

        public Decimal Amt { get; set; }

        public Decimal discountRate { get; set; }

        public Decimal BAL { get; set; }

        public Int32 cashCount { get; set; }

        public Int32 billCount { get; set; }

        public Int32 DiscCount { get; set; }

        public Int32 billCountBydoctor { get; set; }

        public Int32 discCountbydoctor { get; set; }

        public decimal cashSum { get; set; }

        public decimal discSum { get; set; }

        public decimal freeSum { get; set; }

        public decimal opdSum { get; set; }

        public decimal Cashdisc { get; set; }

        public Int32 CompliCount { get; set; }

        public Int32 total { get; set; }

        public Int32 totalOpd { get; set; }

        public Int32 SumtotalAndOpd { get; set; }

        public String totalopd8Pat { get; set; }

        public String Opd8recNo { get; set; }
        public String Location { get; set; } = "";
        public String bed { get; set; }
        public String email { get; set; }
        public int MRI { get; set; } = 0;
        public int UltraSound { get; set; } = 0;
        public int CTScan { get; set; } = 0;
        public int XRay { get; set; } = 0;

        #endregion

        #region ----- Construct --------

        /// <summary>
        /// Default constructor that takes no arguments and initialize all fields with default value
        /// </summary>
        /// <returns>BookingSummary object</returns>
        public BookingSummary()
        {
            this.MyProperty = "";

            this.crtBy = "";

            this.ward = "";

            this.rcvdAmount = 0M;

            this.reportName = "";

            this.reportCount = 0;

            this.labno = "";

            this.Date = DateTime.MinValue;

            this.Stat = "";

            this.Patient = "";

            this.Authorized_By = "";

            this.Gross = 0;

            this.Disc = 0;

            this.discountRate = 0;

            this.Net_Amount = 0;

            this.dtStart = DateTime.MinValue;

            this.dtEnd = DateTime.MinValue;

            this.userName = "";

            this.Amt = 0;

            this.BAL = 0;

            this.cashCount = 0;

            this.billCount = 0;

            this.DiscCount = 0;

            this.billCountBydoctor = 0;

            this.discCountbydoctor = 0;

            this.cashSum = 0M;

            this.discSum = 0M;

            this.freeSum = 0M;

            this.opdSum = 0M;

            this.Cashdisc = 0M;

            this.CompliCount = 0;

            this.total = 0;

            this.totalOpd = 0;

            this.SumtotalAndOpd = 0;

            this.totalopd8Pat = "";

            this.Opd8recNo = "";

            this.bed = String.Empty;
        }

        /// <summary>
        /// Overloaded contructor that takes values and set all fields
        /// </summary>
        /// <param name="crtBy">Int32 crtBy of BookingSummary</param>

        /// <param name="labno">String labno of BookingSummary</param>

        /// <param name="Date">DateTime Date of BookingSummary</param>

        /// <param name="Stat">String Stat of BookingSummary</param>

        /// <param name="Patient">String Patient of BookingSummary</param>

        /// <param name="Authorized_By">String Authorized_By of BookingSummary</param>

        /// <param name="Gross">Decimal Gross of BookingSummary</param>

        /// <param name="Disc">Decimal Disc of BookingSummary</param>

        /// <param name="Net_Amount">Decimal Net_Amount of BookingSummary</param>

        /// <returns>BookingSummary object</returns>
        public BookingSummary(String crtBy, Decimal rcvdAmount, String ward, String labno, DateTime Date, String Stat, String Patient, String Authorized_By, Decimal Gross, Decimal discountRate, Decimal Disc, Decimal Net_Amount, String totalopd8Pat, String opd8recNo, string bed)
        {
            this.crtBy = crtBy;

            this.labno = labno;

            this.ward = ward;

            this.Date = Date;

            this.Stat = Stat;

            this.Patient = Patient;

            this.Authorized_By = Authorized_By;

            this.Gross = Gross;

            this.discountRate = discountRate;

            this.Disc = Disc;

            this.Net_Amount = Net_Amount;

            this.totalopd8Pat = totalopd8Pat;

            this.Opd8recNo = opd8recNo;

            this.bed = bed;
            //this.discCountbydoctor = discCountbydoctor;

            //this.billCountBydoctor = billCountBydoctor;

        }

        /// <summary>
        /// Overloaded contructor that takes one row from BookingSummary table 
        /// and convert it into BookingSummary object
        /// </summary>
        /// <param name="BookingSummaryDataRow">DataRow one row from BookingSummary table</param>
        /// <returns>BookingSummary object</returns>
        public BookingSummary(DataRow BookingSummaryDataRow)
        {
            try
            {
                if (BookingSummaryDataRow.Table.Columns.Contains("crtBy") && !String.IsNullOrEmpty(BookingSummaryDataRow["crtBy"].ToString()))
                { this.crtBy = (String)BookingSummaryDataRow["crtBy"]; }
                else { this.crtBy = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("ward") && !String.IsNullOrEmpty(BookingSummaryDataRow["ward"].ToString()))
                { this.ward = (String)BookingSummaryDataRow["ward"]; }
                else { this.ward = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("rcvdAmount") && !String.IsNullOrEmpty(BookingSummaryDataRow["rcvdAmount"].ToString()))
                { this.rcvdAmount = (Decimal)BookingSummaryDataRow["rcvdAmount"]; }
                else { this.rcvdAmount = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("labno") && !String.IsNullOrEmpty(BookingSummaryDataRow["labno"].ToString()))
                { this.labno = (String)BookingSummaryDataRow["labno"]; }
                else { this.labno = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Date") && !String.IsNullOrEmpty(BookingSummaryDataRow["Date"].ToString()))
                { this.Date = (DateTime)BookingSummaryDataRow["Date"]; }
                else { this.Date = DateTime.MinValue; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Stat") && !String.IsNullOrEmpty(BookingSummaryDataRow["Stat"].ToString()))
                { this.Stat = (String)BookingSummaryDataRow["Stat"]; }
                else { this.Stat = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Patient") && !String.IsNullOrEmpty(BookingSummaryDataRow["Patient"].ToString()))
                { this.Patient = (String)BookingSummaryDataRow["Patient"]; }
                else { this.Patient = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Authorized_By") && !String.IsNullOrEmpty(BookingSummaryDataRow["Authorized_By"].ToString()))
                { this.Authorized_By = (String)BookingSummaryDataRow["Authorized_By"]; }
                else { this.Authorized_By = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Gross") && !String.IsNullOrEmpty(BookingSummaryDataRow["Gross"].ToString()))
                { this.Gross = (Decimal)BookingSummaryDataRow["Gross"]; }
                else { this.Gross = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("discountRate") && !String.IsNullOrEmpty(BookingSummaryDataRow["discountRate"].ToString()))
                { this.Disc = (Decimal)BookingSummaryDataRow["discountRate"]; }
                else { this.discountRate = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Disc") && !String.IsNullOrEmpty(BookingSummaryDataRow["Disc"].ToString()))
                { this.Disc = (Decimal)BookingSummaryDataRow["Disc"]; }
                else { this.Disc = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Net_Amount") && !String.IsNullOrEmpty(BookingSummaryDataRow["Net_Amount"].ToString()))
                { this.Net_Amount = (Decimal)BookingSummaryDataRow["Net_Amount"]; }
                else { this.Net_Amount = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("dtStart") && !String.IsNullOrEmpty(BookingSummaryDataRow["dtStart"].ToString()))
                { this.dtStart = (DateTime)BookingSummaryDataRow["dtStart"]; }
                else { this.dtStart = DateTime.MinValue; }

                if (BookingSummaryDataRow.Table.Columns.Contains("dtEnd") && !String.IsNullOrEmpty(BookingSummaryDataRow["dtEnd"].ToString()))
                { this.dtEnd = (DateTime)BookingSummaryDataRow["dtEnd"]; }
                else { this.dtEnd = DateTime.MinValue; }

                if (BookingSummaryDataRow.Table.Columns.Contains("userName") && !String.IsNullOrEmpty(BookingSummaryDataRow["userName"].ToString()))
                { this.userName = (String)BookingSummaryDataRow["userName"]; }
                else { this.userName = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Amt") && !String.IsNullOrEmpty(BookingSummaryDataRow["Amt"].ToString()))
                { this.Amt = (Decimal)BookingSummaryDataRow["Amt"]; }
                else { this.Amt = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("BAL") && !String.IsNullOrEmpty(BookingSummaryDataRow["BAL"].ToString()))
                { this.BAL = (Decimal)BookingSummaryDataRow["BAL"]; }
                else { this.BAL = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("discCountbydoctor") && !String.IsNullOrEmpty(BookingSummaryDataRow["discCountbydoctor"].ToString()))
                { this.discCountbydoctor = Convert.ToInt32(BookingSummaryDataRow["discCountbydoctor"]); }
                else { this.discCountbydoctor = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("billCountBydoctor") && !String.IsNullOrEmpty(BookingSummaryDataRow["billCountBydoctor"].ToString()))
                { this.billCountBydoctor = Convert.ToInt32(BookingSummaryDataRow["billCountBydoctor"]); }
                else { this.billCountBydoctor = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("reportName") && !String.IsNullOrEmpty(BookingSummaryDataRow["reportName"].ToString()))
                { this.reportName = (String)BookingSummaryDataRow["reportName"]; }
                else { this.reportName = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("reportCount") && !String.IsNullOrEmpty(BookingSummaryDataRow["reportCount"].ToString()))
                { this.reportCount = Convert.ToInt32(BookingSummaryDataRow["reportCount"]); }
                else { this.reportCount = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("totalopd8Pat") && !String.IsNullOrEmpty(BookingSummaryDataRow["totalopd8Pat"].ToString()))
                { this.totalopd8Pat = (String)BookingSummaryDataRow["totalopd8Pat"]; }
                else { this.totalopd8Pat = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Opd8recNo") && !String.IsNullOrEmpty(BookingSummaryDataRow["Opd8recNo"].ToString()))
                { this.Opd8recNo = (String)BookingSummaryDataRow["Opd8recNo"]; }
                else { this.Opd8recNo = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("bed") && !String.IsNullOrEmpty(BookingSummaryDataRow["bed"].ToString()))
                { this.bed = (String)BookingSummaryDataRow["bed"]; }
                else { this.bed = ""; }


                if (BookingSummaryDataRow.Table.Columns.Contains("XRay") && !String.IsNullOrEmpty(BookingSummaryDataRow["XRay"].ToString()))
                { this.XRay = (int)BookingSummaryDataRow["XRay"]; }
                else { this.XRay = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("UltraSound") && !String.IsNullOrEmpty(BookingSummaryDataRow["UltraSound"].ToString()))
                { this.UltraSound = (int)BookingSummaryDataRow["UltraSound"]; }
                else { this.UltraSound = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("MRI") && !String.IsNullOrEmpty(BookingSummaryDataRow["MRI"].ToString()))
                { this.MRI = (int)BookingSummaryDataRow["MRI"]; }
                else { this.MRI = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("CTScan") && !String.IsNullOrEmpty(BookingSummaryDataRow["CTScan"].ToString()))
                { this.CTScan = (int)BookingSummaryDataRow["CTScan"]; }
                else { this.CTScan = 0; }

                if (BookingSummaryDataRow.Table.Columns.Contains("Location") && !String.IsNullOrEmpty(BookingSummaryDataRow["Location"].ToString()))
                { this.Location = (String)BookingSummaryDataRow["Location"]; }
                else { this.Location = ""; }

                if (BookingSummaryDataRow.Table.Columns.Contains("email") && !String.IsNullOrEmpty(BookingSummaryDataRow["email"].ToString()))
                { this.email = (String)BookingSummaryDataRow["email"]; }
                else { this.email = ""; }
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}