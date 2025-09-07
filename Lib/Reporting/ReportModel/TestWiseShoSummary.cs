using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.LT.LabExpress.Reporting.ReportModel
{
    public class TestWiseShoSummary
    {
        public String reportName { get; set; }
        public Int32 totalfreeCount { get; set; }
        public Int32 totaldiscCount { get; set; }
        public Int32 totalcashCount { get; set; }
        public DateTime dtStart { get; set; }
        public DateTime dtEnd { get; set; }
        public Int32 totalQuantity { get { return (totalfreeCount + totaldiscCount + totalcashCount); } }

        public TestWiseShoSummary()
        {
            this.reportName = "";
            this.totalfreeCount = 0;
            this.totalcashCount = 0;
            this.totaldiscCount = 0;
            this.dtStart = DateTime.MinValue;
            this.dtEnd = DateTime.MinValue;
        }

        public TestWiseShoSummary(DataRow testdataRow)
        {
            if (testdataRow.Table.Columns.Contains("reportName") && !String.IsNullOrEmpty(testdataRow["reportName"].ToString()))
            { this.reportName = (String)testdataRow["reportName"]; }
            else { this.reportName = ""; }

            if (testdataRow.Table.Columns.Contains("totalfreeCount") && !String.IsNullOrEmpty(testdataRow["totalfreeCount"].ToString()))
            { this.totalfreeCount = (Int32)testdataRow["totalfreeCount"]; }
            else { this.totalfreeCount = 0; }

            if (testdataRow.Table.Columns.Contains("totaldiscCount") && !String.IsNullOrEmpty(testdataRow["totaldiscCount"].ToString()))
            { this.totaldiscCount = (Int32)testdataRow["totaldiscCount"]; }
            else { this.totaldiscCount = 0; }

            if (testdataRow.Table.Columns.Contains("totalcashCount") && !String.IsNullOrEmpty(testdataRow["totalcashCount"].ToString()))
            { this.totalcashCount = (Int32)testdataRow["totalcashCount"]; }
            else { this.totalcashCount = 0; }

            if (testdataRow.Table.Columns.Contains("dtStart") && !String.IsNullOrEmpty(testdataRow["dtStart"].ToString()))
            { this.dtStart = (DateTime)testdataRow["dtStart"]; }
            else { this.dtStart = DateTime.MinValue; }

            if (testdataRow.Table.Columns.Contains("dtEnd") && !String.IsNullOrEmpty(testdataRow["dtEnd"].ToString()))
            { this.dtEnd = (DateTime)testdataRow["dtEnd"]; }
            else { this.dtEnd = DateTime.MinValue; }
        }

    }
}
