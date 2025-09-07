using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Com.LT.LabExpress.Reporting.ReportModel
{
    public class WidalReport
    {
        #region ----- Properties -------

        public String heading { get; set; }
        public Int32 test1 { get; set; }
        public Int32 test2 { get; set; }
        public Int32 test3 { get; set; }
        public Int32 test4 { get; set; }
        public Int32 test5 { get; set; }

        public String comments { get; set; }

        public String remarks { get; set; }

        [DataMember]
        public bool isPrintOnNextPage { get; set; }

        [DataMember]
        public bool isHidden { get; set; }

        [DataMember]
        public String repComments { get; set; }

        [DataMember]
        public Boolean isDuplicate { get; set; }
        #endregion

        #region ----- Construct --------

        /// <summary>
        /// Default constructor that takes no arguments and initialize all fields with default value
        /// </summary>
        /// <returns>TestReport_Count object</returns>
        public WidalReport()
        {
            this.heading = "";

            this.test1 = 0;

            this.test2 = 0;

            this.test3 = 0;

            this.test4 = 0;
            this.test5 = 0;

            this.comments = "";

            this.remarks = "";

            this.isPrintOnNextPage = false;

            this.isHidden = false;

            this.repComments = "";

            this.isDuplicate = false;
        }

        /// <summary>
        /// Overloaded contructor that takes values and set all fields
        /// </summary>
        /// <param name="ReportCount">Int32 ReportCount of TestReport_Count</param>

        /// <param name="test2">Int32 test2 of TestReport_Count</param>

        /// <param name="crtBy">Int32 crtBy of TestReport_Count</param>

        /// <returns>TestReport_Count object</returns>
        public WidalReport(String heading, Int32 test1, Int32 test2, Int32 test3, Int32 test4, Int32 test5,String comments,String remarks, bool isHidden, bool isPrintOnNextPage,String repComments)
        {
            this.heading = heading;

            this.test1 = test1;

            this.test2 = test2;

            this.test3 = test3;

            this.test4 = test4;

            this.test5 = test5;

            this.comments = comments;

            this.remarks = remarks;

            this.isPrintOnNextPage = isPrintOnNextPage;

            this.isHidden = isHidden;

            this.repComments = repComments;
        }

        /// <summary>
        /// Overloaded contructor that takes one row from TestReport_Count table 
        /// and convert it into TestReport_Count object
        /// </summary>
        /// <param name="TestReport_CountDataRow">DataRow one row from TestReport_Count table</param>
        /// <returns>TestReport_Count object</returns>
        public WidalReport(DataRow TestReport_CountDataRow)
        {
            try
            {
                if (TestReport_CountDataRow.Table.Columns.Contains("heading") && !String.IsNullOrEmpty(TestReport_CountDataRow["heading"].ToString()))
                { this.heading = (String)TestReport_CountDataRow["heading"]; }
                else { this.heading = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("test1") && !String.IsNullOrEmpty(TestReport_CountDataRow["test1"].ToString()))
                { this.test1 = (Int32)TestReport_CountDataRow["test1"]; }
                else { this.test1 = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("test2") && !String.IsNullOrEmpty(TestReport_CountDataRow["test2"].ToString()))
                { this.test2 = (Int32)TestReport_CountDataRow["test2"]; }
                else { this.test2 = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("test3") && !String.IsNullOrEmpty(TestReport_CountDataRow["test3"].ToString()))
                { this.test3 = (Int32)TestReport_CountDataRow["test3"]; }
                else { this.test3 = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("test4") && !String.IsNullOrEmpty(TestReport_CountDataRow["test4"].ToString()))
                { this.test4 = (Int32)TestReport_CountDataRow["test4"]; }
                else { this.test4 = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("test5") && !String.IsNullOrEmpty(TestReport_CountDataRow["test5"].ToString()))
                { this.test5 = (Int32)TestReport_CountDataRow["test5"]; }
                else { this.test5 = 0; }

                if (TestReport_CountDataRow.Table.Columns.Contains("comments") && !String.IsNullOrEmpty(TestReport_CountDataRow["comments"].ToString()))
                { this.comments = (String)TestReport_CountDataRow["comments"]; }
                else { this.comments = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("remarks") && !String.IsNullOrEmpty(TestReport_CountDataRow["remarks"].ToString()))
                { this.remarks = (String)TestReport_CountDataRow["remarks"]; }
                else { this.remarks = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("isPrintOnNextPage") && !String.IsNullOrEmpty(TestReport_CountDataRow["isPrintOnNextPage"].ToString()))
                { this.isPrintOnNextPage = (Boolean)TestReport_CountDataRow["isPrintOnNextPage"]; }
                else { this.isPrintOnNextPage = false; }

                if (TestReport_CountDataRow.Table.Columns.Contains("isHidden") && !String.IsNullOrEmpty(TestReport_CountDataRow["isHidden"].ToString()))
                { this.isHidden = (Boolean)TestReport_CountDataRow["isHidden"]; }
                else { this.isHidden = false; }

                if (TestReport_CountDataRow.Table.Columns.Contains("repComments") && !String.IsNullOrEmpty(TestReport_CountDataRow["repComments"].ToString()))
                { this.repComments = (String)TestReport_CountDataRow["repComments"]; }
                else { this.repComments = ""; }

                if (TestReport_CountDataRow.Table.Columns.Contains("isDuplicate") && !String.IsNullOrEmpty(TestReport_CountDataRow["isDuplicate"].ToString()))
                { this.isDuplicate = (Boolean)TestReport_CountDataRow["isDuplicate"]; }
                else { this.isDuplicate = false; }
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

    }
}
