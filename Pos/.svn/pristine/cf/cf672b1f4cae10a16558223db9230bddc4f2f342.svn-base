using System;
using System.Data;

namespace Com.LT.LabExpress.Reporting
{
    /// <summary>
    /// Business functions of TestReport_Count object
    /// </summary>
    public class DiscountPatients
    {
        #region ----- Properties -------

        //public Decimal Discount { get; set; }

        //public Decimal Free { get; set; }

        //public String docName { get; set; }
        public String crtBy { get; set; }

        public String labno { get; set; }

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

        public Decimal Amt { get; set; }

        public Decimal discountRate { get; set; }

        public Decimal BAL { get; set; }

        public Int32 cashCount { get; set; }

        public Int32 billCount { get; set; }

        public Int32 DiscCount { get; set; }

        public decimal cashSum { get; set; }

        public decimal discSum { get; set; }

        public decimal freeSum { get; set; }

        public decimal Cashdisc { get; set; }

        public Int32 CompliCount { get; set; }

        public Int32 total { get; set; }

        #endregion

        #region ----- Construct --------

        /// <summary>
        /// Default constructor that takes no arguments and initialize all fields with default value
        /// </summary>
        /// <returns>TestReport_Count object</returns>
        public DiscountPatients()
        {
            
            this.crtBy = "";

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

            this.cashSum = 0M;

            this.discSum = 0M;

            this.freeSum = 0M;

            this.Cashdisc = 0M;

            this.CompliCount = 0;

            this.total = 0;

        }

        public DiscountPatients(String crtBy, String labno, DateTime Date, String Stat, String Patient, String Authorized_By, Decimal Gross, Decimal discountRate, Decimal Disc, Decimal Net_Amount)
        {
            this.crtBy = crtBy;

            this.labno = labno;

            this.Date = Date;

            this.Stat = Stat;

            this.Patient = Patient;

            this.Authorized_By = Authorized_By;

            this.Gross = Gross;

            this.discountRate = discountRate;

            this.Disc = Disc;

            this.Net_Amount = Net_Amount;

        }

        /// <summary>
        /// Overloaded contructor that takes values and set all fields
        /// </summary>
        /// <param name="ReportCount">Int32 ReportCount of TestReport_Count</param>

        /// <param name="reportName">String reportName of TestReport_Count</param>

        /// <param name="crtBy">Int32 crtBy of TestReport_Count</param>

        /// <returns>TestReport_Count object</returns>
        //public DiscountPatients(Decimal SUM, String docName)
        //{
        //    this.SUM = SUM;

        //    this.docName = docName;

        //}

        /// <summary>
        /// Overloaded contructor that takes one row from TestReport_Count table 
        /// and convert it into TestReport_Count object
        /// </summary>
        /// <param name="TestReport_CountDataRow">DataRow one row from TestReport_Count table</param>
        /// <returns>TestReport_Count object</returns>
        public DiscountPatients(DataRow TestReport_CountDataRow)
        {
            try
            {
                if (TestReport_CountDataRow.Table.Columns.Contains("crtBy") && !String.IsNullOrEmpty(TestReport_CountDataRow["crtBy"].ToString()))
                { this.crtBy = (String)TestReport_CountDataRow["crtBy"]; }
                else { this.crtBy = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("labno") && !String.IsNullOrEmpty(TestReport_CountDataRow["labno"].ToString()))
                { this.labno = (String)TestReport_CountDataRow["labno"]; }
                else { this.labno = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("Date") && !String.IsNullOrEmpty(TestReport_CountDataRow["Date"].ToString()))
                { this.Date = (DateTime)TestReport_CountDataRow["Date"]; }
                else { this.Date = DateTime.MinValue; }

                if (TestReport_CountDataRow.Table.Columns.Contains("Stat") && !String.IsNullOrEmpty(TestReport_CountDataRow["Stat"].ToString()))
                { this.Stat = (String)TestReport_CountDataRow["Stat"]; }
                else { this.Stat = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("Patient") && !String.IsNullOrEmpty(TestReport_CountDataRow["Patient"].ToString()))
                { this.Patient = (String)TestReport_CountDataRow["Patient"]; }
                else { this.Patient = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("Authorized_By") && !String.IsNullOrEmpty(TestReport_CountDataRow["Authorized_By"].ToString()))
                { this.Authorized_By = (String)TestReport_CountDataRow["Authorized_By"]; }
                else { this.Authorized_By = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("Gross") && !String.IsNullOrEmpty(TestReport_CountDataRow["Gross"].ToString()))
                { this.Gross = (Decimal)TestReport_CountDataRow["Gross"]; }
                else { this.Gross = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("discountRate") && !String.IsNullOrEmpty(TestReport_CountDataRow["discountRate"].ToString()))
                { this.Disc = (Decimal)TestReport_CountDataRow["discountRate"]; }
                else { this.discountRate = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("Disc") && !String.IsNullOrEmpty(TestReport_CountDataRow["Disc"].ToString()))
                { this.Disc = (Decimal)TestReport_CountDataRow["Disc"]; }
                else { this.Disc = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("Net_Amount") && !String.IsNullOrEmpty(TestReport_CountDataRow["Net_Amount"].ToString()))
                { this.Net_Amount = (Decimal)TestReport_CountDataRow["Net_Amount"]; }
                else { this.Net_Amount = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("dtStart") && !String.IsNullOrEmpty(TestReport_CountDataRow["dtStart"].ToString()))
                { this.dtStart = (DateTime)TestReport_CountDataRow["dtStart"]; }
                else { this.dtStart = DateTime.MinValue; }

                if (TestReport_CountDataRow.Table.Columns.Contains("dtEnd") && !String.IsNullOrEmpty(TestReport_CountDataRow["dtEnd"].ToString()))
                { this.dtEnd = (DateTime)TestReport_CountDataRow["dtEnd"]; }
                else { this.dtEnd = DateTime.MinValue; }

                if (TestReport_CountDataRow.Table.Columns.Contains("userName") && !String.IsNullOrEmpty(TestReport_CountDataRow["userName"].ToString()))
                { this.userName = (String)TestReport_CountDataRow["userName"]; }
                else { this.userName = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("Amt") && !String.IsNullOrEmpty(TestReport_CountDataRow["Amt"].ToString()))
                { this.Amt = (Decimal)TestReport_CountDataRow["Amt"]; }
                else { this.Amt = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("BAL") && !String.IsNullOrEmpty(TestReport_CountDataRow["BAL"].ToString()))
                { this.BAL = (Decimal)TestReport_CountDataRow["BAL"]; }
                else { this.BAL = 0; }

            }
            catch (Exception ex) { throw ex; }

           
        }

        #endregion
    }
}