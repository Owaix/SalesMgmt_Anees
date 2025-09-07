using Com.LT.LabExpress.Reporting;
using Com.LT.LabExpress.Reporting.ReportModel;
using Lib.Model;
using Lib.Reporting.ReportModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Reporting
{
    public class Reports
    {

        public static List<Report_TestReports> TestReports(String labNo, Int32 reportID)
        {
            List<Report_TestReports> objList = new List<Report_TestReports>();
            DataTable dt = ReportsController.TestReports(labNo, reportID);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new Report_TestReports(row));
            }
            return objList;
        }

        public static List<Report_TestReports> PatientDetails_Select(String labNo)
        {
            List<Report_TestReports> objList = new List<Report_TestReports>();
            DataTable dt = ReportsController.PatientDetails_Select(labNo);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new Report_TestReports(row));
            }
            return objList;
        }

        public static List<Report_TestReports> TestReportByDepartment(String labNo, Int32 labDeptID)
        {
            List<Report_TestReports> objList = new List<Report_TestReports>();
            DataTable dt = ReportsController.TestReport_Depart(labNo, labDeptID);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new Report_TestReports(row));
            }
            return objList;
        }

        public static List<Report_TestReports> SpecialChemistryReport(String labNo, Int32 labDeptID)
        {
            List<Report_TestReports> objList = new List<Report_TestReports>();
            DataTable dt = ReportsController.SpecialChemistry_Report(labNo, labDeptID);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new Report_TestReports(row));
            }
            return objList;
        }

        public static List<WidalReport> WidalReport_Select(String labNo, Int32 labDeptID)
        {
            List<WidalReport> objList = new List<WidalReport>();
            DataTable dt = ReportsController.WidalReport_Select(labNo, labDeptID);
            WidalReport report;
            foreach (DataRow row in dt.Rows)
            {
                report = new WidalReport(row);
                if (report.test5 == 1) { report.test4 = 1; }
                if (report.test4 == 1) { report.test3 = 1; }
                if (report.test3 == 1) { report.test2 = 1; }
                if (report.test2 == 1) { report.test1 = 1; }

                objList.Add(report);
            }
            return objList;
        }

        public static List<DetailReportModel> BookingDetailSummary(DateTime dtStart, DateTime dtEnd, String userName, String type)
        {
            List<DetailReportModel> objList = new List<DetailReportModel>();
            DataTable dt = ReportsController.BookingDetailSummary(dtStart, dtEnd, userName, type);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new DetailReportModel(row));
            }
            return objList;
        }

        public static List<OrderReportModel> BookingSummary(DateTime dtStart, DateTime dtEnd, String userName, String type)
        {
            List<OrderReportModel> objList = new List<OrderReportModel>();
            DataTable dt = ReportsController.BookingSummary(dtStart, dtEnd, userName, type);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new OrderReportModel(row));
            }
            return objList;
        }


        public static List<ItemReportModel> ItemsSummary(DateTime dtStart, DateTime dtEnd, String Items)
        {
            List<ItemReportModel> objList = new List<ItemReportModel>();
            DataTable dt = ReportsController.ItemsSummary(dtStart, dtEnd, Items);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new ItemReportModel(row));
            }
            return objList;
        }
        public static List<BookingSummary> BookingSummary_CategoryandLabWiseTest(DateTime dtStart, DateTime dtEnd, String userName)
        {
            List<BookingSummary> objList = new List<BookingSummary>();
            DataTable dt = ReportsController.CategoryandLabWiseTest(dtStart, dtEnd, userName);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new BookingSummary(row));
            }
            return objList;
        }

        public static List<BookingSummary> MonthlySummary(DateTime dtStart, DateTime dtEnd)
        {
            List<BookingSummary> objList = new List<BookingSummary>();
            DataTable dt = ReportsController.MonthlySummary(dtStart, dtEnd);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new BookingSummary(row));
            }
            return objList;
        }

        public static List<BookingSummary> MonthlySummary_Summary(DateTime dtStart, DateTime dtEnd)
        {
            List<BookingSummary> objList = new List<BookingSummary>();
            DataTable dt = ReportsController.MonthlySummary_Summary(dtStart, dtEnd);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new BookingSummary(row));
            }
            return objList;
        }


        public static List<BookingSummary> BookingSummary_UserTotalAmount(DateTime dtStart, DateTime dtEnd)
        {
            List<BookingSummary> objList = new List<BookingSummary>();
            DataTable dt = ReportsController.BookingSummary_UserAmount(dtStart, dtEnd);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new BookingSummary(row));
            }
            return objList;
        }

        public static List<BookingSummary> BookingSummary_ReportCount(DateTime dtStart, DateTime dtEnd, String userName)
        {
            List<BookingSummary> objList = new List<BookingSummary>();
            DataTable dt = ReportsController.BookingSummary_Report(dtStart, dtEnd, userName);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new BookingSummary(row));
            }
            return objList;
        }

        public static List<FreePatients> FreePatients(DateTime dtStart, DateTime dtEnd, Int32 docID)
        {
            List<FreePatients> objList = new List<FreePatients>();
            DataTable dt = ReportsController.FreePatient(dtStart, dtEnd, docID);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new FreePatients(row));
            }
            return objList;
        }

        public static List<Receipt> Receipt(String labNo)
        {
            List<Receipt> objList = new List<Receipt>();
            DataTable dt = ReportsController.Receipt(labNo);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new Receipt(row));
            }
            return objList;
        }

        public static List<TestReport_Count> TestReportCount(DateTime dtStart, DateTime dtEnd, Int32 reportID, string userName)
        {
            List<TestReport_Count> objList = new List<TestReport_Count>();
            DataTable dt = ReportsController.TestReportCount(dtStart, dtEnd, reportID, userName);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new TestReport_Count(row));
            }
            return objList;
        }

        public static List<DiscountPatients> DiscountPatients(DateTime dtStart, DateTime dtEnd, Int32 docID)
        {
            List<DiscountPatients> objList = new List<DiscountPatients>();
            DataTable dt = ReportsController.DiscountPatients(dtStart, dtEnd, docID);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new DiscountPatients(row));
            }
            return objList;
        }

        public static List<TestWiseDetSummary> TestWiseDetailSummary(DateTime dtStart, DateTime dtEnd, String userName)
        {
            List<TestWiseDetSummary> objList = new List<TestWiseDetSummary>();
            DataTable dt = ReportsController.TestWiseDetailSumm(dtStart, dtEnd, userName);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new TestWiseDetSummary(row));
            }
            return objList;
        }

        public static List<TestWiseDetSummary> TestWiseDetSumm_Count(DateTime dtStart, DateTime dtEnd, String userName)
        {
            List<TestWiseDetSummary> objList = new List<TestWiseDetSummary>();
            DataTable dt = ReportsController.TestWiseDetailSumm_Count(dtStart, dtEnd, userName);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new TestWiseDetSummary(row));
            }
            return objList;
        }
        public static List<TestWiseShoSummary> TestWiseShoSummary(DateTime dtStart, DateTime dtEnd)
        {
            List<TestWiseShoSummary> objList = new List<TestWiseShoSummary>();
            DataTable dt = ReportsController.TestWiseShortSumm(dtStart, dtEnd);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new TestWiseShoSummary(row));
            }
            return objList;
        }

        public static List<SelectAllTestResult> SelectAllTest_Result(Int32 reportID, DateTime dtStart, DateTime dtEnd)
        {
            List<SelectAllTestResult> objList = new List<SelectAllTestResult>();
            DataTable dt = ReportsController.SelectAllTest_Result(reportID, dtStart, dtEnd);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new SelectAllTestResult(row));
            }
            return objList;
        }

        public static List<SelectAllTestResult> ReportTestWisePendingReport(Int32 reportID, DateTime dtStart, DateTime dtEnd)
        {
            List<SelectAllTestResult> objList = new List<SelectAllTestResult>();
            DataTable dt = ReportsController.ReportTestWisePendingReport(reportID, dtStart, dtEnd);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new SelectAllTestResult(row));
            }
            return objList;
        }

        public static List<SelectAllTestResult> BookedTestByname(Int32 reportID, DateTime dtStart, DateTime dtEnd)
        {
            List<SelectAllTestResult> objList = new List<SelectAllTestResult>();
            DataTable dt = ReportsController.BookedTestByname(reportID, dtStart, dtEnd);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new SelectAllTestResult(row));
            }
            return objList;
        }


        public static List<TestRateReport> TestRate_List()
        {
            List<TestRateReport> objList = new List<TestRateReport>();
            DataTable dt = ReportsController.TestRate_List();
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new TestRateReport(row));
            }
            return objList;
        }

        public static List<Report_TestReports> SelectAll_DepartmentTest(Int32 labdeptID, DateTime startDate, DateTime endDate)
        {
            List<Report_TestReports> objList = new List<Report_TestReports>();
            DataTable dt = ReportsController.SelectAll_DepartmentTest(labdeptID, startDate, endDate);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new Report_TestReports(row));
            }
            return objList;
        }

        public static List<Get_PatientInformation> Get_PatientInformation(String labNo)
        {
            List<Get_PatientInformation> objList = new List<ReportModel.Get_PatientInformation>();
            DataTable dt = ReportsController.Get_PatientInformation(labNo);
            foreach (DataRow row in dt.Rows)
            {
                objList.Add(new Get_PatientInformation(row));
            }
            return objList;
        }



    }

}
