using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.LT.LabExpress.Reporting.ReportModel
{
    public class TestRateReport
    {
        #region ----- Properties -------

        public Int32 labDeptID { get; set; }
        public String labDeptName { get; set; }
        public String reportName { get; set; }
        public Decimal charges { get; set; }
        public DateTime modDate { get; set; }

        #endregion

        #region ----- Construct --------

        /// <summary>
        /// Default constructor that takes no arguments and initialize all fields with default value
        /// </summary>
        /// <returns>TestReport_Count object</returns>
        public TestRateReport()
        {
            this.labDeptID = 0;

            this.labDeptName = "";

            this.reportName = "";

            this.charges = 0M;

            this.modDate = Convert.ToDateTime("1/1/1900");
        }

        /// <summary>
        /// Overloaded contructor that takes values and set all fields
        /// </summary>
        /// <param name="ReportCount">Int32 ReportCount of TestReport_Count</param>

        /// <param name="reportName">String reportName of TestReport_Count</param>

        /// <param name="crtBy">Int32 crtBy of TestReport_Count</param>

        /// <returns>TestReport_Count object</returns>
        public TestRateReport(Int32 labDeptID,String labDeptName, String reportName, Decimal charges,DateTime modDate)
        {
            this.labDeptID = labDeptID;

            this.labDeptName = labDeptName;

            this.reportName = reportName;

            this.charges = charges;

            this.modDate = modDate;
        }

        /// <summary>
        /// Overloaded contructor that takes one row from TestReport_Count table 
        /// and convert it into TestReport_Count object
        /// </summary>
        /// <param name="TestReport_CountDataRow">DataRow one row from TestReport_Count table</param>
        /// <returns>TestReport_Count object</returns>
        public TestRateReport(DataRow TestReport_CountDataRow)
        {
            try
            {
                if (TestReport_CountDataRow.Table.Columns.Contains("labDeptID") && !String.IsNullOrEmpty(TestReport_CountDataRow["labDeptID"].ToString()))
                { this.labDeptID = (Int32)TestReport_CountDataRow["labDeptID"]; }
                else { this.labDeptID = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("labDeptName") && !String.IsNullOrEmpty(TestReport_CountDataRow["labDeptName"].ToString()))
                { this.labDeptName = (String)TestReport_CountDataRow["labDeptName"]; }
                else { this.labDeptName = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("reportName") && !String.IsNullOrEmpty(TestReport_CountDataRow["reportName"].ToString()))
                { this.reportName = (String)TestReport_CountDataRow["reportName"]; }
                else { this.reportName = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("charges") && !String.IsNullOrEmpty(TestReport_CountDataRow["charges"].ToString()))
                { this.charges = (Decimal)TestReport_CountDataRow["charges"]; }
                else { this.charges = 0M; }

                if (TestReport_CountDataRow.Table.Columns.Contains("modDate") && !String.IsNullOrEmpty(TestReport_CountDataRow["modDate"].ToString()))
                { this.modDate = (DateTime)TestReport_CountDataRow["modDate"]; }
                else { this.modDate = Convert.ToDateTime("1/1/1900"); }
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion
    }
}
