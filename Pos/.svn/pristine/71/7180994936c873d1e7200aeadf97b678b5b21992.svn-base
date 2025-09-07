using System;
using System.Data;

namespace Com.LT.LabExpress.Reporting
{
    /// <summary>
    /// Business functions of Receipt object
    /// </summary>
    public class Receipt
    {
        #region ----- Properties -------

        public String labNo { get; set; }

        public DateTime reportingDate { get; set; }

        public Decimal balAmount { get; set; }

        public String comments { get; set; }

        public DateTime deliveryDate { get; set; }

        public Int32 discountRate { get; set; }

        public Decimal discountSum { get; set; }

        public Decimal totalAmount { get; set; }

        public String opd8 { get; set; }

        public Decimal rcvdAmount { get; set; }

        public String patientName { get; set; }

        public String gender { get; set; }

        public String reportName { get; set; }

        public Decimal charges { get; set; }

        public Int32 patientReportID { get; set; }

        public Int32 patientID { get; set; }

        public Int32 reportID { get; set; }

        public String docName { get; set; }

        public String crtBy { get; set; }
        public Int32 AGE { get; set; }

        public string ward { get; set; }

        public string bed { get; set; }
        public Int32 labDeptID { get; set; }

        public String department { get; set; }

        public String patAge { get; set; }

        public String ageType { get; set; }
        public int Rows { get; set; }
        public float RowHeight { get; set; }
        public String barCode { get; set; }

        #endregion

        #region ----- Construct --------

        /// <summary>
        /// Default constructor that takes no arguments and initialize all fields with default value
        /// </summary>
        /// <returns>Receipt object</returns>
        public Receipt()
        {
            this.labNo = "";

            this.reportingDate = DateTime.MinValue;

            this.balAmount = 0;

            this.crtBy = "";

            this.comments = "";

            this.deliveryDate = DateTime.MinValue;

            this.discountRate = 0;

            this.discountSum = 0;

            this.totalAmount = 0;

            this.rcvdAmount = 0;

            this.opd8 = "";

            this.patientName = "";

            this.gender = "";

            this.reportName = "";

            this.charges = 0;

            this.patientReportID = 0;

            this.patientID = 0;

            this.reportID = 0;

            this.docName = "";

            this.AGE = 0;

            this.ward = "";

            this.bed = "";

            this.labDeptID = 0;

            this.department = "";

            this.patAge = "";

            this.ageType = "";

        }

        /// <summary>
        /// Overloaded contructor that takes values and set all fields
        /// </summary>
        /// <param name="labNo">String labNo of Receipt</param>

        /// <param name="reportingDate">DateTime reportingDate of Receipt</param>

        /// <param name="balAmount">Decimal balAmount of Receipt</param>

        /// <param name="comments">String comments of Receipt</param>

        /// <param name="deliveryDate">DateTime deliveryDate of Receipt</param>

        /// <param name="discountRate">Int32 discountRate of Receipt</param>

        /// <param name="discountSum">Decimal discountSum of Receipt</param>

        /// <param name="totalAmount">Decimal totalAmount of Receipt</param>

        /// <param name="rcvdAmount">Decimal rcvdAmount of Receipt</param>

        /// <param name="patientName">String patientName of Receipt</param>

        /// <param name="gender">String gender of Receipt</param>

        /// <param name="reportName">String reportName of Receipt</param>

        /// <param name="charges">Decimal charges of Receipt</param>

        /// <param name="patientReportID">Int32 patientReportID of Receipt</param>

        /// <param name="patientID">Int32 patientID of Receipt</param>

        /// <param name="reportID">Int32 reportID of Receipt</param>

        /// <returns>Receipt object</returns>
        public Receipt(String labNo, String opd8, String crtBy, String ward, String bed, DateTime reportingDate, Decimal balAmount, String comments, DateTime deliveryDate, Int32 discountRate, Decimal discountSum, Decimal totalAmount, Decimal rcvdAmount, String patientName, String gender, String reportName, Decimal charges, Int32 patientReportID, Int32 patientID, Int32 reportID, String patAge, String ageType)
        {
            this.labNo = labNo;

            this.reportingDate = reportingDate;

            this.balAmount = balAmount;

            this.crtBy = crtBy;

            this.opd8 = opd8;

            this.comments = comments;

            this.deliveryDate = deliveryDate;

            this.discountRate = discountRate;

            this.discountSum = discountSum;

            this.totalAmount = totalAmount;

            this.rcvdAmount = rcvdAmount;

            this.patientName = patientName;

            this.gender = gender;

            this.ward = ward;

            this.bed = bed;

            this.reportName = reportName;

            this.charges = charges;

            this.patientReportID = patientReportID;

            this.patientID = patientID;

            this.reportID = reportID;

            this.patAge = patAge;

            this.ageType = ageType;

        }

        /// <summary>
        /// Overloaded contructor that takes one row from Receipt table 
        /// and convert it into Receipt object
        /// </summary>
        /// <param name="ReceiptDataRow">DataRow one row from Receipt table</param>
        /// <returns>Receipt object</returns>
        public Receipt(DataRow ReceiptDataRow)
        {
            try
            {
                if (ReceiptDataRow.Table.Columns.Contains("labNo") && !String.IsNullOrEmpty(ReceiptDataRow["labNo"].ToString()))
                { this.labNo = (String)ReceiptDataRow["labNo"]; }
                else { this.labNo = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("opd8") && !String.IsNullOrEmpty(ReceiptDataRow["opd8"].ToString()))
                { this.opd8 = (String)ReceiptDataRow["opd8"]; }
                else { this.opd8 = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("reportingDate") && !String.IsNullOrEmpty(ReceiptDataRow["reportingDate"].ToString()))
                { this.reportingDate = (DateTime)ReceiptDataRow["reportingDate"]; }
                else { this.reportingDate = DateTime.MinValue; }

                if (ReceiptDataRow.Table.Columns.Contains("balAmount") && !String.IsNullOrEmpty(ReceiptDataRow["balAmount"].ToString()))
                { this.balAmount = (Decimal)ReceiptDataRow["balAmount"]; }
                else { this.balAmount = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("crtBy") && !String.IsNullOrEmpty(ReceiptDataRow["crtBy"].ToString()))
                { this.crtBy = (String)ReceiptDataRow["crtBy"]; }
                else { this.crtBy = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("comments") && !String.IsNullOrEmpty(ReceiptDataRow["comments"].ToString()))
                { this.comments = (String)ReceiptDataRow["comments"]; }
                else { this.comments = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("deliveryDate") && !String.IsNullOrEmpty(ReceiptDataRow["deliveryDate"].ToString()))
                { this.deliveryDate = (DateTime)ReceiptDataRow["deliveryDate"]; }
                else { this.deliveryDate = DateTime.MinValue; }

                if (ReceiptDataRow.Table.Columns.Contains("discountRate") && !String.IsNullOrEmpty(ReceiptDataRow["discountRate"].ToString()))
                { this.discountRate = (Int32)ReceiptDataRow["discountRate"]; }
                else { this.discountRate = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("discountSum") && !String.IsNullOrEmpty(ReceiptDataRow["discountSum"].ToString()))
                { this.discountSum = (Decimal)ReceiptDataRow["discountSum"]; }
                else { this.discountSum = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("totalAmount") && !String.IsNullOrEmpty(ReceiptDataRow["totalAmount"].ToString()))
                { this.totalAmount = (Decimal)ReceiptDataRow["totalAmount"]; }
                else { this.totalAmount = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("rcvdAmount") && !String.IsNullOrEmpty(ReceiptDataRow["rcvdAmount"].ToString()))
                { this.rcvdAmount = (Decimal)ReceiptDataRow["rcvdAmount"]; }
                else { this.rcvdAmount = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("patientName") && !String.IsNullOrEmpty(ReceiptDataRow["patientName"].ToString()))
                { this.patientName = (String)ReceiptDataRow["patientName"]; }
                else { this.patientName = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("ward") && !String.IsNullOrEmpty(ReceiptDataRow["ward"].ToString()))
                { this.ward = (String)ReceiptDataRow["ward"]; }
                else { this.ward = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("bed") && !String.IsNullOrEmpty(ReceiptDataRow["bed"].ToString()))
                { this.bed = (String)ReceiptDataRow["bed"]; }
                else { this.bed = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("gender") && !String.IsNullOrEmpty(ReceiptDataRow["gender"].ToString()))
                { this.gender = (String)ReceiptDataRow["gender"]; }
                else { this.gender = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("reportName") && !String.IsNullOrEmpty(ReceiptDataRow["reportName"].ToString()))
                { this.reportName = (String)ReceiptDataRow["reportName"]; }
                else { this.reportName = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("charges") && !String.IsNullOrEmpty(ReceiptDataRow["charges"].ToString()))
                { this.charges = (Decimal)ReceiptDataRow["charges"]; }
                else { this.charges = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("patientReportID") && !String.IsNullOrEmpty(ReceiptDataRow["patientReportID"].ToString()))
                { this.patientReportID = (Int32)ReceiptDataRow["patientReportID"]; }
                else { this.patientReportID = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("patientID") && !String.IsNullOrEmpty(ReceiptDataRow["patientID"].ToString()))
                { this.patientID = (Int32)ReceiptDataRow["patientID"]; }
                else { this.patientID = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("reportID") && !String.IsNullOrEmpty(ReceiptDataRow["reportID"].ToString()))
                { this.reportID = (Int32)ReceiptDataRow["reportID"]; }
                else { this.reportID = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("docName") && !String.IsNullOrEmpty(ReceiptDataRow["docName"].ToString()))
                { this.docName = (String)ReceiptDataRow["docName"]; }
                else { this.docName = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("AGE") && !String.IsNullOrEmpty(ReceiptDataRow["AGE"].ToString()))
                { this.AGE = (Int32)ReceiptDataRow["AGE"]; }
                else { this.AGE = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("patAge") && !String.IsNullOrEmpty(ReceiptDataRow["patAge"].ToString()))
                { this.patAge = (String)ReceiptDataRow["patAge"]; }
                else { this.patAge = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("ageType") && !String.IsNullOrEmpty(ReceiptDataRow["ageType"].ToString()))
                { this.ageType = (String)ReceiptDataRow["ageType"]; }
                else { this.ageType = ""; }

                if (ReceiptDataRow.Table.Columns.Contains("labDeptID") && !String.IsNullOrEmpty(ReceiptDataRow["labDeptID"].ToString()))
                { this.labDeptID = (Int32)ReceiptDataRow["labDeptID"]; }
                else { this.labDeptID = 0; }

                if (ReceiptDataRow.Table.Columns.Contains("department") && !String.IsNullOrEmpty(ReceiptDataRow["department"].ToString()))
                { this.department = (String)ReceiptDataRow["department"]; }
                else { this.department = ""; }

            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}