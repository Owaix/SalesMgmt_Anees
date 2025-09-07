using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.LT.LabExpress.Reporting.ReportModel
{
    public class TestWiseDetSummary
    {
        public String labno { get; set; }
        public String patientName { get; set; }
        public String Stat { get; set; }

        public String FreeStatus { get; set; }
        public String docName { get; set; }

        public String crtBy { get; set; }
        public String reportName { get; set; }
        public Decimal totalAmount { get; set; }

        public Decimal charges { get; set; }
        public DateTime dtStart { get; set; }
        public DateTime dtEnd { get; set; }
        
        public TestWiseDetSummary()
        {
            this.labno = "";
            this.patientName = "";
            this.dtStart = DateTime.MinValue;
            this.dtEnd = DateTime.MinValue;
            this.Stat = "";
            this.FreeStatus = "";
            this.crtBy = "";
            this.docName = "";
            this.reportName = "";
            this.totalAmount = 0M;
            this.charges = 0M;
        }

        public TestWiseDetSummary(DataRow testdataRow)
        {
            if (testdataRow.Table.Columns.Contains("labno") && !String.IsNullOrEmpty(testdataRow["labno"].ToString()))
            { this.labno = (String)testdataRow["labno"]; }
            else { this.labno = ""; }

            if (testdataRow.Table.Columns.Contains("patientName") && !String.IsNullOrEmpty(testdataRow["patientName"].ToString()))
            { this.patientName = (String)testdataRow["patientName"]; }
            else { this.patientName = ""; }

            if (testdataRow.Table.Columns.Contains("dtStart") && !String.IsNullOrEmpty(testdataRow["dtStart"].ToString()))
            { this.dtStart = (DateTime)testdataRow["dtStart"]; }
            else { this.dtStart = DateTime.MinValue; }

            if (testdataRow.Table.Columns.Contains("dtEnd") && !String.IsNullOrEmpty(testdataRow["dtEnd"].ToString()))
            { this.dtEnd = (DateTime)testdataRow["dtEnd"]; }
            else { this.dtEnd = DateTime.MinValue; }

            if (testdataRow.Table.Columns.Contains("Stat") && !String.IsNullOrEmpty(testdataRow["Stat"].ToString()))
            { this.Stat = (String)testdataRow["Stat"]; }
            else { this.Stat = ""; }

            if (testdataRow.Table.Columns.Contains("FreeStatus") && !String.IsNullOrEmpty(testdataRow["FreeStatus"].ToString()))
            { this.FreeStatus = (String)testdataRow["FreeStatus"]; }
            else { this.FreeStatus = ""; }

            if (testdataRow.Table.Columns.Contains("crtBy") && !String.IsNullOrEmpty(testdataRow["crtBy"].ToString()))
            { this.crtBy = (String)testdataRow["crtBy"]; }
            else { this.crtBy = ""; }

            if (testdataRow.Table.Columns.Contains("docName") && !String.IsNullOrEmpty(testdataRow["docName"].ToString()))
            { this.docName = (String)testdataRow["docName"]; }
            else { this.docName = ""; }

            
            if (testdataRow.Table.Columns.Contains("reportName") && !String.IsNullOrEmpty(testdataRow["reportName"].ToString()))
            { this.reportName = (String)testdataRow["reportName"]; }
            else { this.reportName = ""; }

            if (testdataRow.Table.Columns.Contains("totalAmount ") && !String.IsNullOrEmpty(testdataRow["totalAmount "].ToString()))
            { this.totalAmount = (Decimal)testdataRow["totalAmount "]; }
            else { this.totalAmount = 0; }

            if (testdataRow.Table.Columns.Contains("charges") && !String.IsNullOrEmpty(testdataRow["charges"].ToString()))
            { this.charges = (Decimal)testdataRow["charges"]; }
            else { this.charges = 0; }

        }

    }

}
