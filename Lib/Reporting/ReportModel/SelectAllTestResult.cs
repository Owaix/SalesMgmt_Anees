using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.LT.LabExpress.Reporting.ReportModel
{
    public class SelectAllTestResult
    {

        #region ----- Properties -------

        public String crtBy { get; set; }
        public DateTime Date { get; set; }
        public String labNo { get; set; }
        public String Stat { get; set; }
        public String Patient { get; set; }
        public String reportName { get; set; }
        public Int32 reportID { get; set; }
        
        public String testName { get; set; }
        public String testResult { get; set; }
        public String comments { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public Int32 positiveCount { get; set; }
        public Int32 negativeCount { get; set; }

        #endregion

        #region ----- Construct --------

        /// <summary>
        /// Default constructor that takes no arguments and initialize all fields with default value
        /// </summary>
        /// <returns>FreePatients object</returns>
        public SelectAllTestResult()
        {
            this.crtBy = "";

            this.labNo = "";

            this.Date = Convert.ToDateTime("1/1/1900");

            this.Stat = "";

            this.Patient = "";

            this.reportID = 0;
            
            this.reportName = "";

            this.comments = "";

            this.testName = "";

            this.testResult = "";
            this.startDate = Convert.ToDateTime("1/1/1900");
            this.endDate = Convert.ToDateTime("1/1/1900");

        }

        /// <summary>
        /// Overloaded contructor that takes values and set all fields
        /// </summary>
        /// <param name="crtBy">Int32 crtBy of FreePatients</param>

        /// <param name="labNo">String labNo of FreePatients</param>

        /// <param name="Date">DateTime Date of FreePatients</param>

        /// <param name="Stat">String Stat of FreePatients</param>

        /// <param name="Patient">String Patient of FreePatients</param>

        /// <param name="reportName">String reportName of FreePatients</param>

        /// <param name="Charges">Decimal Charges of FreePatients</param>

        /// <param name="Disc">Decimal Disc of FreePatients</param>

        /// <param name="patientReportID">Int32 patientReportID of FreePatients</param>

        /// <param name="patientID">Int32 patientID of FreePatients</param>

        /// <param name="docID">Int32 docID of FreePatients</param>

        /// <param name="reportID">Int32 reportID of FreePatients</param>

        /// <returns>FreePatients object</returns>
        public SelectAllTestResult(Int32 reportID,String crtBy, String labNo, String Stat, DateTime Date,String Patient, String reportName, String testName,String testResult,String comments,DateTime startDate,DateTime endDate)
        {
            this.crtBy = crtBy;

            this.labNo = labNo;

            this.Date = Date;

            this.Stat = Stat;

            this.Patient = Patient;

            this.reportID = reportID;

            this.reportName = reportName;

            this.testResult = testResult;

            this.comments = comments;

            this.testName = testName;

            this.startDate = startDate;

            this.endDate = endDate;
        }

        /// <summary>
        /// Overloaded contructor that takes one row from FreePatients table 
        /// and convert it into FreePatients object
        /// </summary>
        /// <param name="FreePatientsDataRow">DataRow one row from FreePatients table</param>
        /// <returns>FreePatients object</returns>
        public SelectAllTestResult(DataRow FreePatientsDataRow)
        {
            try
            {
                
                if (FreePatientsDataRow.Table.Columns.Contains("reportID") && !String.IsNullOrEmpty(FreePatientsDataRow["reportID"].ToString()))
                { this.reportID = (Int32)FreePatientsDataRow["reportID"]; }
                else { this.reportID = 0; }

                if (FreePatientsDataRow.Table.Columns.Contains("crtBy") && !String.IsNullOrEmpty(FreePatientsDataRow["crtBy"].ToString()))
                { this.crtBy = (String)FreePatientsDataRow["crtBy"]; }
                else { this.crtBy = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("labNo") && !String.IsNullOrEmpty(FreePatientsDataRow["labNo"].ToString()))
                { this.labNo = (String)FreePatientsDataRow["labNo"]; }
                else { this.labNo = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("Date") && !String.IsNullOrEmpty(FreePatientsDataRow["Date"].ToString()))
                { this.Date = (DateTime)FreePatientsDataRow["Date"]; }
                else { this.Date = DateTime.MinValue; }

                if (FreePatientsDataRow.Table.Columns.Contains("Stat") && !String.IsNullOrEmpty(FreePatientsDataRow["Stat"].ToString()))
                { this.Stat = (String)FreePatientsDataRow["Stat"]; }
                else { this.Stat = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("Patient") && !String.IsNullOrEmpty(FreePatientsDataRow["Patient"].ToString()))
                { this.Patient = (String)FreePatientsDataRow["Patient"]; }
                else { this.Patient = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("testResult") && !String.IsNullOrEmpty(FreePatientsDataRow["testResult"].ToString()))
                { this.testResult = (String)FreePatientsDataRow["testResult"]; }
                else { this.testResult = ""; }
                
                if (FreePatientsDataRow.Table.Columns.Contains("reportName") && !String.IsNullOrEmpty(FreePatientsDataRow["reportName"].ToString()))
                { this.reportName = (String)FreePatientsDataRow["reportName"]; }
                else { this.reportName = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("testName") && !String.IsNullOrEmpty(FreePatientsDataRow["testName"].ToString()))
                { this.testName = (String)FreePatientsDataRow["testName"]; }
                else { this.testName = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("comments") && !String.IsNullOrEmpty(FreePatientsDataRow["comments"].ToString()))
                { this.comments = (String)FreePatientsDataRow["comments"]; }
                else { this.comments = ""; }

                if (FreePatientsDataRow.Table.Columns.Contains("startDate") && !String.IsNullOrEmpty(FreePatientsDataRow["startDate"].ToString()))
                { this.startDate = (DateTime)FreePatientsDataRow["startDate"]; }
                else { this.startDate = DateTime.MinValue; }

                if (FreePatientsDataRow.Table.Columns.Contains("endDate") && !String.IsNullOrEmpty(FreePatientsDataRow["endDate"].ToString()))
                { this.endDate = (DateTime)FreePatientsDataRow["endDate"]; }
                else { this.endDate = DateTime.MinValue; }
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

    }
}
