using System;
using System.Data;
using System.Runtime.Serialization;

namespace Com.LT.LabExpress.Reporting
{
    /// <summary>
    /// Business functions of Report_TestReports object
    /// </summary>
    public class Report_TestReports
    {
        #region ----- Properties -------

        public String patientName { get; set; }

        public String patientCode { get; set; }
        
        public int labDeptID { get; set; } = 0;

        public string labDeptName { get; set; } = "";
        
        public string ward { get; set; }

        public string bed { get; set; }

        public String docName { get; set; }

        public Int32 discountRate { get; set; }

        public DateTime dob { get; set; }

        public Int32 AGE { get; set; }

        public String bacteria { get; set; }

        public String verifiyBy { get; set; }
        
        public String gender { get; set; }

        public String reportName { get; set; }

        public String testName { get; set; }

        public Int32 patientReportID { get; set; }

        public DateTime deliveryDate { get; set; }

        public DateTime reportingDate { get; set; }

        public String labNo { get; set; }

        public String testUnit { get; set; }

        public String normalRange { get; set; }

        public String comments { get; set; }

        public String testResult { get; set; }

        public Int32 patientID { get; set; }

        public Int32 reportID { get; set; }

        public Int32 reportTestID { get; set; }

        public Int32 testID { get; set; }

        public Int32 PRDID { get; set; }

        [DataMember]
        public string opd8 { get; set; }

        [DataMember]
        public int deptCode { get; set; }

        [DataMember]
        public String patAge { get; set; }

        [DataMember]
        public String ageType { get; set; }

        [DataMember]
        public String heading { get; set; }
        
        [DataMember]
        public String crtBy { get; set; }


        [DataMember]
        public DateTime crtDate { get; set; }

        [DataMember]
        public String bookingComments { get; set; }

        [DataMember]
        public int orderNo { get; set; }

        [DataMember]
        public String clinicalInterpetaion { get; set; }

        [DataMember]
        public bool isPrintOnNextPage { get; set; }

        [DataMember]
        public bool isHidden { get; set; }

        [DataMember]
        public String repComments { get; set; }

        [DataMember]
        public string locName { get; set; }

        [DataMember]
        public string locCode { get; set; }

        [DataMember]
        public Boolean isDuplicate { get; set; }
        #endregion

        #region ----- Construct --------

        /// <summary>
        /// Default constructor that takes no arguments and initialize all fields with default value
        /// </summary>
        /// <returns>Report_TestReports object</returns>
        public Report_TestReports()
        {
            this.patientName = "";

            this.patientCode = "";

            this.dob = DateTime.MinValue;

            this.gender = "";

            this.heading = "";

            this.docName = "";

            this.ward = "";

            this.bed = "";

            this.AGE = 0;

            this.verifiyBy= "";

            this.discountRate = 0;

            this.reportName = "";

            this.testName = "";

            this.patientReportID = 0;

            this.deliveryDate = DateTime.MinValue;

            this.reportingDate = DateTime.MinValue;

            this.labNo = "";

            this.testName = "";

            this.bacteria = "";

            this.testUnit = "";

            this.normalRange = "";

            this.comments = "";

            this.testResult = "";

            this.patientID = 0;

            this.reportID = 0;

            this.reportTestID = 0;

            this.testID = 0;

            this.PRDID = 0;

            this.testID = 0;

            this.labDeptID = 0;

            this.labDeptName = "";

            this.opd8 = "";

            this.deptCode = 0;

            this.patAge = "";

            this.ageType = "";

            this.crtBy = "";

            this.crtDate = Convert.ToDateTime("1/1/1900");

            this.bookingComments = "";

            this.orderNo = 0;

            this.clinicalInterpetaion = "";

            this.isPrintOnNextPage = false;

            this.isHidden = false;

            this.repComments = "";

            this.locName = string.Empty;

            this.locCode = string.Empty;
        }

        /// <summary>
        /// Overloaded contructor that takes values and set all fields
        /// </summary>
        /// <param name="patientName">String patientName of Report_TestReports</param>

        /// <param name="patientCode">String patientCode of Report_TestReports</param>

        /// <param name="dob">DateTime dob of Report_TestReports</param>

        /// <param name="gender">String gender of Report_TestReports</param>

        /// <param name="reportName">String reportName of Report_TestReports</param>

        /// <param name="testName">String testName of Report_TestReports</param>

        /// <param name="patientReportID">Int32 patientReportID of Report_TestReports</param>

        /// <param name="deliveryDate">DateTime deliveryDate of Report_TestReports</param>

        /// <param name="reportingDate">DateTime reportingDate of Report_TestReports</param>

        /// <param name="labNo">String labNo of Report_TestReports</param>

        /// <param name="testName">String testName of Report_TestReports</param>

        /// <param name="testUnit">String testUnit of Report_TestReports</param>

        /// <param name="normalRange">String normalRange of Report_TestReports</param>

        /// <param name="comments">String comments of Report_TestReports</param>

        /// <param name="testResult">String testResult of Report_TestReports</param>

        /// <param name="patientID">Int32 patientID of Report_TestReports</param>

        /// <param name="reportID">Int32 reportID of Report_TestReports</param>

        /// <param name="reportTestID">Int32 reportTestID of Report_TestReports</param>

        /// <param name="testID">Int32 testID of Report_TestReports</param>

        /// <param name="PRDID">Int32 PRDID of Report_TestReports</param>

        /// <param name="testID">Int32 testID of Report_TestReports</param>

        /// <returns>Report_TestReports object</returns>
        public Report_TestReports(String patientName,String verifiyBy, String heading,String bacteria ,Int32 AGE, String docName,String ward, String bed, Int32 discountRate ,Int32 labDeptID , String labDeptName, String patientCode, DateTime dob, String gender, String reportName, String testName, Int32 patientReportID, DateTime deliveryDate, DateTime reportingDate, String labNo, String testUnit, String normalRange, String comments, String testResult, Int32 patientID, Int32 reportID, Int32 reportTestID, Int32 testID, Int32 PRDID ,string opd8, int deptCode, String patAge, String ageType,String crtBy,DateTime crtDate,String bookingComments, int orderNo, String clinicalInterpetaion, bool isHidden, bool isPrintOnNextPage,string repComments, string locName,string locCode)
        {
            this.patientName = patientName;

            this.patientCode = patientCode;

            this.labDeptID = labDeptID;

            this.labDeptName = labDeptName;

            this.AGE = AGE;

            this.bacteria = bacteria;

            this.verifiyBy = verifiyBy;

            this.docName = docName;

            this.ward = ward;

            this.heading = heading;

            this.bed = bed;

            this.discountRate = discountRate;

            this.dob = dob;

            this.gender = gender;

            this.reportName = reportName;

            this.testName = testName;

            this.patientReportID = patientReportID;

            this.deliveryDate = deliveryDate;

            this.reportingDate = reportingDate;

            this.labNo = labNo;

            this.testName = testName;

            this.testUnit = testUnit;

            this.normalRange = normalRange;

            this.comments = comments;

            this.testResult = testResult;

            this.patientID = patientID;

            this.reportID = reportID;

            this.reportTestID = reportTestID;

            this.testID = testID;

            this.PRDID = PRDID;

            this.testID = testID;

            this.opd8 = opd8;

            this.deptCode = deptCode;

            this.crtBy = crtBy;

            this.crtDate = crtDate;

            this.bookingComments = bookingComments;

            this.orderNo = orderNo;

            this.clinicalInterpetaion = clinicalInterpetaion;

            this.isPrintOnNextPage = isPrintOnNextPage;

            this.isHidden = isHidden;

            this.repComments = "";

            this.locCode = locCode;

            this.locName = locName;

            this.isDuplicate = false;
        }

        /// <summary>
        /// Overloaded contructor that takes one row from Report_TestReports table 
        /// and convert it into Report_TestReports object
        /// </summary>
        /// <param name="Report_TestReportsDataRow">DataRow one row from Report_TestReports table</param>
        /// <returns>Report_TestReports object</returns>
        public Report_TestReports(DataRow Report_TestReportsDataRow)
        {
            try
            {
                if (Report_TestReportsDataRow.Table.Columns.Contains("patientName") && !String.IsNullOrEmpty(Report_TestReportsDataRow["patientName"].ToString()))
                {this.patientName = (String)Report_TestReportsDataRow["patientName"];}
                else { this.patientName = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("patientCode") && !String.IsNullOrEmpty(Report_TestReportsDataRow["patientCode"].ToString()))
                {this.patientCode = (String)Report_TestReportsDataRow["patientCode"];}
                else { this.patientCode = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("bacteria") && !String.IsNullOrEmpty(Report_TestReportsDataRow["bacteria"].ToString()))
                { this.bacteria = (String)Report_TestReportsDataRow["bacteria"]; }
                else { this.bacteria =""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("verifiyBy") && !String.IsNullOrEmpty(Report_TestReportsDataRow["verifiyBy"].ToString()))
                { this.verifiyBy = (String)Report_TestReportsDataRow["verifiyBy"]; }
                else { this.verifiyBy = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("dob") && !String.IsNullOrEmpty(Report_TestReportsDataRow["dob"].ToString()))
                {this.dob = (DateTime)Report_TestReportsDataRow["dob"];}
                else { this.dob = DateTime.MinValue; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("gender") && !String.IsNullOrEmpty(Report_TestReportsDataRow["gender"].ToString()))
                {this.gender = (String)Report_TestReportsDataRow["gender"];}
                else { this.gender = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("bed") && !String.IsNullOrEmpty(Report_TestReportsDataRow["bed"].ToString()))
                { this.bed = (String)Report_TestReportsDataRow["bed"]; }
                else { this.bed = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("heading") && !String.IsNullOrEmpty(Report_TestReportsDataRow["heading"].ToString()))
                { this.heading = (String)Report_TestReportsDataRow["heading"]; }
                else { this.heading = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("ward") && !String.IsNullOrEmpty(Report_TestReportsDataRow["ward"].ToString()))
                { this.ward = (String)Report_TestReportsDataRow["ward"]; }
                else { this.ward = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("docName") && !String.IsNullOrEmpty(Report_TestReportsDataRow["docName"].ToString()))
                { this.docName = (String)Report_TestReportsDataRow["docName"]; }
                else { this.docName = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("discountRate") && !String.IsNullOrEmpty(Report_TestReportsDataRow["discountRate"].ToString()))
                { this.discountRate = (Int32)Report_TestReportsDataRow["discountRate"]; }
                else { this.discountRate = 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("AGE") && !String.IsNullOrEmpty(Report_TestReportsDataRow["AGE"].ToString()))
                { this.AGE = (Int32)Report_TestReportsDataRow["AGE"]; }
                else { this.AGE= 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("patAge") && !String.IsNullOrEmpty(Report_TestReportsDataRow["patAge"].ToString()))
                { this.patAge = (String)Report_TestReportsDataRow["patAge"]; }
                else { this.patAge = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("ageType") && !String.IsNullOrEmpty(Report_TestReportsDataRow["ageType"].ToString()))
                { this.ageType = (String)Report_TestReportsDataRow["ageType"]; }
                else { this.ageType = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("reportName") && !String.IsNullOrEmpty(Report_TestReportsDataRow["reportName"].ToString()))
                {this.reportName = (String)Report_TestReportsDataRow["reportName"];}
                else { this.reportName = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("testName") && !String.IsNullOrEmpty(Report_TestReportsDataRow["testName"].ToString()))
                {this.testName = (String)Report_TestReportsDataRow["testName"];}
                else { this.testName = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("patientReportID") && !String.IsNullOrEmpty(Report_TestReportsDataRow["patientReportID"].ToString()))
                {this.patientReportID = (Int32)Report_TestReportsDataRow["patientReportID"];}
                else { this.patientReportID = 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("labDeptID") && !String.IsNullOrEmpty(Report_TestReportsDataRow["labDeptID"].ToString()))
                { this.labDeptID = (Int32)Report_TestReportsDataRow["labDeptID"]; }
                else { this.labDeptID = 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("labDeptName") && !String.IsNullOrEmpty(Report_TestReportsDataRow["labDeptName"].ToString()))
                { this.labDeptName = (String)Report_TestReportsDataRow["labDeptName"]; }
                else { this.labDeptName = ""; }


                if (Report_TestReportsDataRow.Table.Columns.Contains("deliveryDate") && !String.IsNullOrEmpty(Report_TestReportsDataRow["deliveryDate"].ToString()))
                {this.deliveryDate = (DateTime)Report_TestReportsDataRow["deliveryDate"];}
                else { this.deliveryDate = DateTime.MinValue; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("reportingDate") && !String.IsNullOrEmpty(Report_TestReportsDataRow["reportingDate"].ToString()))
                {this.reportingDate = (DateTime)Report_TestReportsDataRow["reportingDate"];}
                else { this.reportingDate = DateTime.MinValue; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("labNo") && !String.IsNullOrEmpty(Report_TestReportsDataRow["labNo"].ToString()))
                {this.labNo = (String)Report_TestReportsDataRow["labNo"];}
                else { this.labNo = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("testName") && !String.IsNullOrEmpty(Report_TestReportsDataRow["testName"].ToString()))
                {this.testName = (String)Report_TestReportsDataRow["testName"];}
                else { this.testName = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("testUnit") && !String.IsNullOrEmpty(Report_TestReportsDataRow["testUnit"].ToString()))
                {this.testUnit = (String)Report_TestReportsDataRow["testUnit"];}
                else { this.testUnit = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("normalRange") && !String.IsNullOrEmpty(Report_TestReportsDataRow["normalRange"].ToString()))
                {this.normalRange = (String)Report_TestReportsDataRow["normalRange"];}
                else { this.normalRange = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("comments") && !String.IsNullOrEmpty(Report_TestReportsDataRow["comments"].ToString()))
                {this.comments = (String)Report_TestReportsDataRow["comments"];}
                else { this.comments = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("testResult") && !String.IsNullOrEmpty(Report_TestReportsDataRow["testResult"].ToString()))
                {this.testResult = (String)Report_TestReportsDataRow["testResult"];}
                else { this.testResult = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("patientID") && !String.IsNullOrEmpty(Report_TestReportsDataRow["patientID"].ToString()))
                {this.patientID = (Int32)Report_TestReportsDataRow["patientID"];}
                else { this.patientID = 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("reportID") && !String.IsNullOrEmpty(Report_TestReportsDataRow["reportID"].ToString()))
                {this.reportID = (Int32)Report_TestReportsDataRow["reportID"];}
                else { this.reportID = 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("reportTestID") && !String.IsNullOrEmpty(Report_TestReportsDataRow["reportTestID"].ToString()))
                {this.reportTestID = (Int32)Report_TestReportsDataRow["reportTestID"];}
                else { this.reportTestID = 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("testID") && !String.IsNullOrEmpty(Report_TestReportsDataRow["testID"].ToString()))
                {this.testID = (Int32)Report_TestReportsDataRow["testID"];}
                else { this.testID = 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("PRDID") && !String.IsNullOrEmpty(Report_TestReportsDataRow["PRDID"].ToString()))
                {this.PRDID = (Int32)Report_TestReportsDataRow["PRDID"];}
                else { this.PRDID = 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("testID") && !String.IsNullOrEmpty(Report_TestReportsDataRow["testID"].ToString()))
                {this.testID = (Int32)Report_TestReportsDataRow["testID"];}
                else { this.testID = 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("deptCode") && !String.IsNullOrEmpty(Report_TestReportsDataRow["deptCode"].ToString()))
                { this.deptCode = (Int32)Report_TestReportsDataRow["deptCode"]; }
                else { this.deptCode = 0; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("opd8") && !String.IsNullOrEmpty(Report_TestReportsDataRow["opd8"].ToString()))
                { this.opd8 = (String)Report_TestReportsDataRow["opd8"]; }
                else { this.opd8 = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("crtBy") && !String.IsNullOrEmpty(Report_TestReportsDataRow["crtBy"].ToString()))
                { this.crtBy = (String)Report_TestReportsDataRow["crtBy"]; }
                else { this.crtBy = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("crtDate") && !String.IsNullOrEmpty(Report_TestReportsDataRow["crtDate"].ToString()))
                { this.crtDate = (DateTime)Report_TestReportsDataRow["crtDate"]; }
                else { this.crtDate = Convert.ToDateTime("1/1/1900"); }

                if (Report_TestReportsDataRow.Table.Columns.Contains("bookingComments") && !String.IsNullOrEmpty(Report_TestReportsDataRow["bookingComments"].ToString()))
                { this.bookingComments = (String)Report_TestReportsDataRow["bookingComments"]; }
                else { this.bookingComments = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("orderNo") && !String.IsNullOrEmpty(Report_TestReportsDataRow["orderNo"].ToString()))
                { this.orderNo = (int)Report_TestReportsDataRow["orderNo"]; }
                else { this.orderNo = 1000; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("clinicalInterpetaion") && !String.IsNullOrEmpty(Report_TestReportsDataRow["clinicalInterpetaion"].ToString()))
                { this.clinicalInterpetaion = (String)Report_TestReportsDataRow["clinicalInterpetaion"]; }
                else { this.clinicalInterpetaion = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("isPrintOnNextPage") && !String.IsNullOrEmpty(Report_TestReportsDataRow["isPrintOnNextPage"].ToString()))
                { this.isPrintOnNextPage = (Boolean)Report_TestReportsDataRow["isPrintOnNextPage"]; }
                else { this.isPrintOnNextPage = false; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("isHidden") && !String.IsNullOrEmpty(Report_TestReportsDataRow["isHidden"].ToString()))
                { this.isHidden = (Boolean)Report_TestReportsDataRow["isHidden"]; }
                else { this.isHidden = false; }


                if (Report_TestReportsDataRow.Table.Columns.Contains("repComments") && !String.IsNullOrEmpty(Report_TestReportsDataRow["repComments"].ToString()))
                { this.repComments = (String)Report_TestReportsDataRow["repComments"]; }
                else { this.repComments = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("locName") && !String.IsNullOrEmpty(Report_TestReportsDataRow["locName"].ToString()))
                { this.locName = (String)Report_TestReportsDataRow["locName"]; }
                else { this.locName = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("locCode") && !String.IsNullOrEmpty(Report_TestReportsDataRow["locCode"].ToString()))
                { this.locCode = (String)Report_TestReportsDataRow["locCode"]; }
                else { this.locCode = ""; }

                if (Report_TestReportsDataRow.Table.Columns.Contains("isDuplicate") && !String.IsNullOrEmpty(Report_TestReportsDataRow["isDuplicate"].ToString()))
                { this.isDuplicate = (Boolean)Report_TestReportsDataRow["isDuplicate"]; }
                else { this.isDuplicate = false; }

            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}