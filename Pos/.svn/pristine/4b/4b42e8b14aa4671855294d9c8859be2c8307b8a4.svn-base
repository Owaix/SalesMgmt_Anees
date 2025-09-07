using System;
using System.Data;

namespace Com.LT.LabExpress.Reporting
{
    /// <summary>
    /// Business functions of TestReport_Count object
    /// </summary>
    public class TestReport_Count
    {
        #region ----- Properties -------

        public Int32 ReportCount { get; set; }

        public String reportName { get; set; }

        public String crtBy { get; set; }

        #endregion

        #region ----- Construct --------

        /// <summary>
        /// Default constructor that takes no arguments and initialize all fields with default value
        /// </summary>
        /// <returns>TestReport_Count object</returns>
        public TestReport_Count()
        {
            this.ReportCount = 0;

            this.reportName = "";

            this.crtBy = "";

        }

        /// <summary>
        /// Overloaded contructor that takes values and set all fields
        /// </summary>
        /// <param name="ReportCount">Int32 ReportCount of TestReport_Count</param>

        /// <param name="reportName">String reportName of TestReport_Count</param>

        /// <param name="crtBy">Int32 crtBy of TestReport_Count</param>

        /// <returns>TestReport_Count object</returns>
        public TestReport_Count(Int32 ReportCount, String reportName, String crtBy)
        {
            this.ReportCount = ReportCount;

            this.reportName = reportName;

            this.crtBy = crtBy;

        }

        /// <summary>
        /// Overloaded contructor that takes one row from TestReport_Count table 
        /// and convert it into TestReport_Count object
        /// </summary>
        /// <param name="TestReport_CountDataRow">DataRow one row from TestReport_Count table</param>
        /// <returns>TestReport_Count object</returns>
        public TestReport_Count(DataRow TestReport_CountDataRow)
        {
            try
            {
                if (TestReport_CountDataRow.Table.Columns.Contains("ReportCount") && !String.IsNullOrEmpty(TestReport_CountDataRow["ReportCount"].ToString()))
                {this.ReportCount = (Int32)TestReport_CountDataRow["ReportCount"];}
                else { this.ReportCount = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("reportName") && !String.IsNullOrEmpty(TestReport_CountDataRow["reportName"].ToString()))
                {this.reportName = (String)TestReport_CountDataRow["reportName"];}
                else { this.reportName = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("crtBy") && !String.IsNullOrEmpty(TestReport_CountDataRow["crtBy"].ToString()))
                {this.crtBy = (String)TestReport_CountDataRow["crtBy"];}
                else { this.crtBy = ""; }

            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}