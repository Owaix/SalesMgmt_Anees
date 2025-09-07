using Lib.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Lib.Reporting
{
    public class ReportsController
    {

        private const String Report_TestReports = "Report_TestReports";

        private const String Report_TestReportByDepartment = "Report_TestReportByDepartment";

        private String Report_BookingSummary = "";

        private const String Report_BookingSummary_Summary = "Report_BookingSummary_Summary";

        private const String Report_MonthlySummary = "Report_MonthlySummary";

        private const String Report_MonthlySummary_Summary = "Report_MonthlySummary_Summary";

        private const String BookingSummary_ReportCount = "BookingSummary_ReportCount";

        private const String Report_FreePatients = "Report_FreePatients";

        private const String Report_Receipt = "Report_Receipt";

        private const String Report_TestReport_Count = "Report_TestReport_Count";

        private const String Report_DiscountPatients = "Report_DiscountPatients";

        private const String TestWiseDetailSummary = "TestWiseDetailSummary ";

        private const String TestWiseDetailSummary_Count = "TestWiseDetailSummary_Count ";

        private const String TestWiseShortSummary = "TestWiseShortSummary ";

        private const String BookingSummary_UserTotalAmount = "BookingSummary_UserTotalAmount ";

        private const String Select_PatientReportRecord = "Select_PatientReportRecord ";

        private const String PatientReportDetails_Delete = "PatientReportDetails_Delete";

        private const String SelectAllTestResult = "SelectAllTestResult";

        private const String TestRateList = "TestRateList";

        private const String Select_WidalReport = "Select_WidalReport";

        private const String Select_PatientDetails = "Select_PatientDetails";

        private const String Select_BookedTestByname = "Select_BookedTestByname";

        private const String Report_SpecialChemistry = "Report_SpecialChemistry";

        private const String SelectAllDepartmentTest = "SelectAllDepartmentTest";

        private const String GetPatientInformation = "GetPatientInformation";

        private const String Report_TestWisePendingReport = "Report_TestWisePendingReport";
        private const String Report_CategoryandLabWiseTest = "Report_CategoryandLabWiseTest";

      

        public static DataTable GetItemSummary(int Item, DateTime reportStartDate, DateTime reportEndDate, int CompanyID)
        {
            var _query = @"Select  IName, Items.IID , SUM(Qty) as Qty , SUM(Amount) as Amount ,SUM(Discount) as Discount from tbl_Order 
              left join tbl_KOT on tbl_KOT.Id = tbl_Order.KOTID 
              left join tbl_OrderDetails on tbl_OrderDetails.OrderId = tbl_Order.OrderId 
              left join Items on Items.IID = tbl_OrderDetails.itemID 
              WHERE ('0' = @Item OR Items.IID LIKE @Item) 
              AND  (CAST(tbl_Order.OrderDate as Date) >= CAST(@reportStartDate  as Date) and CAST(tbl_Order.OrderDate as Date ) <= CAST(@reportEndDate as Date)) And tbl_Order.CompanyID = @CompanyID
              group by IName  ,Items.IID 
               ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(_query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Item", Item);
            sqlCmd.Parameters.AddWithValue("@reportStartDate", reportStartDate);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", reportEndDate);
            sqlCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getcustomerLedgerSummaryByDate(DateTime StartDate, DateTime EndDate, int CustomerID)
        {
            string query = @"SELECT 
    convert(varchar(10), gl.GLDate, 101) AS gldate,
    concat(
        MAX(tt.Symbol), 
        '-', 
        gl.RID, 
        ' (', 
        CASE 
            WHEN MAX(tt.Symbol) = 'SJ' THEN (select InvNo from Sales_M where RID=gl.RID ) 
			 WHEN MAX(tt.Symbol) = 'PJ' THEN (select InvNo from Pur_M where RID=gl.RID ) 
            ELSE MAX(gl.Narration) 
        END, 
        ')'
    ) AS reference,
    gl.RID,
    gl.TypeCode,
    SUM(debit) AS debit,
    SUM(credit) AS credit
FROM 
    GL gl
LEFT JOIN 
    RP tt ON gl.TypeCode = tt.TypeCode
WHERE 
    (CAST(gl.GLDate AS Date) >= CAST(@StartDate AS Date) 
    AND CAST(gl.GLDate AS Date) <= CAST(@EndDate AS Date)) 
    AND AC_Code = @acCode
GROUP BY  
    gl.TypeCode,
    gl.RID,
    convert(varchar(10), gl.GLDate, 101)
ORDER BY 
    convert(varchar(10), gl.GLDate, 101)
";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@StartDate", StartDate);
            sqlCmd.Parameters.AddWithValue("@EndDate", EndDate);
            sqlCmd.Parameters.AddWithValue("@acCode", CustomerID);


            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }
        public static DataTable PurchaseList( DateTime reportStartDate, DateTime reportEndDate, int CompanyID)
        {

            var _query = @"select D.AC_Title ,S.* from Pur_M as S left join COA_D D on S.AC_Code = D.AC_Code
	                       where 
                           (CAST(S.EDate as Date) >= CAST(@StartDate as Date) and CAST(S.EDate as Date ) <= CAST(@EndDate as Date)) and S.CompID=@CompyID ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(_query, SqlHelper.DefaultSqlConnection);
         
           
            sqlCmd.Parameters.AddWithValue("@StartDate", reportStartDate);
            sqlCmd.Parameters.AddWithValue("@EndDate", reportEndDate);
            sqlCmd.Parameters.AddWithValue("@CompyID", CompanyID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable orderNO(DateTime reportStartDate, int CompanyID)
        {
        
            var _query = @"select count(*) from tbl_Order S

where (CAST(S.OrderDate as Date) = CAST(@StartDate as Date))
  And S.CompanyID = @CompyID";

         
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(_query, SqlHelper.DefaultSqlConnection);


            sqlCmd.Parameters.AddWithValue("@StartDate", reportStartDate);
           
            sqlCmd.Parameters.AddWithValue("@CompyID", CompanyID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static int GetOrderCount(DateTime reportStartDate, int companyID)
        {
            const string query = @"
        SELECT COUNT(*)
        FROM tbl_Order S
        WHERE CAST(S.OrderDate AS DATE) = CAST(@StartDate AS DATE)
          AND S.CompanyID = @CompanyID";

            using (var connection = SqlHelper.DefaultSqlConnection)
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", reportStartDate);
                    command.Parameters.AddWithValue("@CompanyID", companyID);

                    connection.Open();
                    return (int)command.ExecuteScalar();
                }
            }
        }
        public static DataTable SaleList(DateTime reportStartDate, DateTime reportEndDate, int CompanyID)
        {
            var _query = @"select D.AC_Title ,S.* from Sales_M as S left join COA_D D on S.AC_Code = D.AC_Code
	where 

	(CAST(S.EDate as Date) >= CAST(@StartDate as Date) and CAST(S.EDate as Date ) <= CAST(@EndDate as Date)) And S.CompID=@compyID";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(_query, SqlHelper.DefaultSqlConnection);


            sqlCmd.Parameters.AddWithValue("@StartDate", reportStartDate);
            sqlCmd.Parameters.AddWithValue("@EndDate", reportEndDate);
            sqlCmd.Parameters.AddWithValue("@compyID", CompanyID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable sp_getArticle(int companyID)
        {
            var _query = @"select ArticleTypes.ArticleTypeID,Styles.StyleID,Styles.StyleName,ArticleTypes.ArticleTypeName,
                           Article.ArticleNo,Article.ProductName,Article.IsDelete,Article.ProductID
                          from Article left join  ArticleTypes on Article.ArticleTypeID=ArticleTypes.ArticleTypeID
                          left join Styles on Styles.StyleID = Article.StyleID
	                      where Article.CompanyID=@companyID";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(_query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@companyID", companyID);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable sp_PV_M_Insert(int companyID, DateTime Date, int AcCode, bool value)
        {
            string query = @"insert into PV_M (CompID,EDate,AC_Code,isDeleted) values (@CompID,@EDate,@AC_Code,@IsDelete)

	                      SELECT SCOPE_IDENTITY()";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@CompID", companyID);
            sqlCmd.Parameters.AddWithValue("@EDate", Date);
            sqlCmd.Parameters.AddWithValue("@AC_Code", AcCode);
            sqlCmd.Parameters.AddWithValue("@IsDelete", Convert.ToBoolean(value));
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;



        }


        public static DataTable sp_RV_M_Insert(int companyID, DateTime Date, int AcCode, bool value, int employee)
        {
            string query = @"insert into RV_M (CompID,EDate,AC_Code,isDeleted ,SID) values (@CompID,@EDate,@AC_Code,'false' ,@SID ) 
	                             SELECT SCOPE_IDENTITY()";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@CompID", companyID);
            sqlCmd.Parameters.AddWithValue("@EDate", Date);
            sqlCmd.Parameters.AddWithValue("@AC_Code", AcCode);
            sqlCmd.Parameters.AddWithValue("@IsDelete", Convert.ToBoolean(value));
            sqlCmd.Parameters.AddWithValue("@SID", employee);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;



        }


        public static DataTable sp_RV_M_Update(int companyID, DateTime Date, int AcCode, int id , int EmployeeId)
        {
            string query = @"update RV_M set CompID=@CompID,EDate=@EDate,AC_Code =@AC_Code  , SID=@SID where RID= @Id";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@CompID", companyID);
            sqlCmd.Parameters.AddWithValue("@EDate", Date);
            sqlCmd.Parameters.AddWithValue("@AC_Code", AcCode);
            sqlCmd.Parameters.AddWithValue("@Id", id);
            sqlCmd.Parameters.AddWithValue("@SID", EmployeeId);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }


        public static DataTable RecivedVoucharIndex(DateTime StartDate, DateTime EndDate , int CompanyID)
        {
            string query = @"select m.*,c.Amt,rvm.AC_Title as customer,d.AC_Title as cashBAnk,c.BalAmt,c.checkDate,c.ChkNo,c.DisAmt,
                             c.InvNo,c.MOP_ID,c.Narr,c.PreAmt,c.SlipNo,c.SRT,c.AC_Code as RV_TransactionCode,c.ID from RV_M as m
                             inner join RV_d as c on m.RID=c.RID
                             inner join COA_D as d on d.AC_Code=c.AC_Code
                             inner join COA_D rvm on rvm.AC_Code=m.AC_Code
                             inner join AspNetUsers Aspuser on Aspuser.AccessFailedCount=m.CompID
                             where (CAST(m.EDate as Date) >= CAST(@StartDate as Date) and CAST(m.EDate as Date) <= CAST(@EndDate as Date)) 
                             AND m.CompID = @CompID";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@StartDate", StartDate);
            sqlCmd.Parameters.AddWithValue("@EndDate", EndDate);
            sqlCmd.Parameters.AddWithValue("@CompID", CompanyID); 

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }


        public static DataTable sp_RV_D_Update(String Narr, int MOP_ID, int AC_Code, int InvNo, int ChkNo, int SlipNo, Double PreAmt, Double Amt, double DisAmt, Double BalAmt, DateTime checkDate, int id)
        {
            string query = @"update RV_D set Narr =@Narr,MOP_ID =@MOP_ID,AC_Code=@AC_Code,InvNo=@InvNo,ChkNo=@ChkNo,SlipNo=@SlipNo,PreAmt=PreAmt,
	                       Amt=@Amt,DisAmt=@DisAmt,BalAmt=@BalAmt,SRT=@SRT,RCancel=@RCancel,checkDate=@checkDate where RID=@RID";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Narr", Narr);
            sqlCmd.Parameters.AddWithValue("@MOP_ID", MOP_ID);
            sqlCmd.Parameters.AddWithValue("@AC_Code", AC_Code);

            sqlCmd.Parameters.AddWithValue("@InvNo", InvNo);
            sqlCmd.Parameters.AddWithValue("@ChkNo", ChkNo);
            sqlCmd.Parameters.AddWithValue("@SlipNo", SlipNo);



            sqlCmd.Parameters.AddWithValue("@Amt", Amt);
            sqlCmd.Parameters.AddWithValue("@DisAmt", DisAmt);

            sqlCmd.Parameters.AddWithValue("@BalAmt", BalAmt);
            sqlCmd.Parameters.AddWithValue("@checkDate", checkDate);
            sqlCmd.Parameters.AddWithValue("@RID", id);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;



        }

        public static DataTable sp_RV_GL_credit(int TypeCode, int AC_Code, int AC_Code2, string Narration, Double Debit, Double Credit, int RID, DateTime GLDate, int companyID)
        {
            string query = @"insert into GL (TypeCode,AC_Code,AC_Code2,Narration,Debit,Credit,RID,GLDate,CompID) 
                           values(@TypeCode,@AC_Code,@AC_Code2,@Narration,@Debit,@Credit,@RID,@gl,@companyID)
";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@TypeCode", TypeCode);
            sqlCmd.Parameters.AddWithValue("@AC_Code", AC_Code);
            sqlCmd.Parameters.AddWithValue("@AC_Code2", AC_Code2);

            sqlCmd.Parameters.AddWithValue("@Narration", Narration);
            sqlCmd.Parameters.AddWithValue("@Debit", Debit);
            sqlCmd.Parameters.AddWithValue("@Credit", Credit);

            sqlCmd.Parameters.AddWithValue("@RID", RID);
            sqlCmd.Parameters.AddWithValue("@gl", GLDate);
            sqlCmd.Parameters.AddWithValue("@companyID", companyID);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;



        }



        public static DataTable sp_PV_GL_Debit(int TypeCode, int AC_Code, int AC_Code2, string Narration, Double Debit, Double Credit, int RID, DateTime GLDate, int companyID)
        {
            string query = @"insert into GL (TypeCode,AC_Code,AC_Code2,Narration,Debit,Credit,RID,GLDate,CompID) 
                           values(@TypeCode,@AC_Code,@AC_Code2,@Narration,@Debit,@Credit,@RID,@gl,@companyID)
";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@TypeCode", TypeCode);
            sqlCmd.Parameters.AddWithValue("@AC_Code", AC_Code);
            sqlCmd.Parameters.AddWithValue("@AC_Code2", AC_Code2);

            sqlCmd.Parameters.AddWithValue("@Narration", Narration);
            sqlCmd.Parameters.AddWithValue("@Debit", Debit);
            sqlCmd.Parameters.AddWithValue("@Credit", Credit);

            sqlCmd.Parameters.AddWithValue("@RID", RID);
            sqlCmd.Parameters.AddWithValue("@gl", GLDate);
            sqlCmd.Parameters.AddWithValue("@companyID", companyID);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;



        }
        public static DataTable sp_PV_M_Update(int AC_Code, DateTime GLDate, int companyID, int ID)
        {
            string query = @"update PV_M set
	                     CompID=@CompID,EDate=@EDate,AC_Code =@AC_Code where RID= @Id";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@CompID", companyID);
            sqlCmd.Parameters.AddWithValue("@EDate", GLDate);
            sqlCmd.Parameters.AddWithValue("@AC_Code", AC_Code);
            sqlCmd.Parameters.AddWithValue("@Id", ID);



            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;



        }

        public static DataTable sp_PV_D_Update(String Narr, int MOP_ID, int AC_Code, int InvNo, int ChkNo, int SlipNo, Double PreAmt, Double Amt, double DisAmt, Double BalAmt, DateTime checkDate, int id)
        {
            string query = @"update PV_D set Narr =@Narr,MOP_ID =@MOP_ID,AC_Code=@AC_Code,InvNo=@InvNo,ChkNo=@ChkNo,SlipNo=@SlipNo,
	Amt=@Amt,DisAmt=@DisAmt,BalAmt=@BalAmt,checkDate=@checkDate where RID=@RID";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Narr", Narr);
            sqlCmd.Parameters.AddWithValue("@MOP_ID", MOP_ID);
            sqlCmd.Parameters.AddWithValue("@AC_Code", AC_Code);

            sqlCmd.Parameters.AddWithValue("@InvNo", InvNo);
            sqlCmd.Parameters.AddWithValue("@ChkNo", ChkNo);
            sqlCmd.Parameters.AddWithValue("@SlipNo", SlipNo);



            sqlCmd.Parameters.AddWithValue("@Amt", Amt);
            sqlCmd.Parameters.AddWithValue("@DisAmt", DisAmt);

            sqlCmd.Parameters.AddWithValue("@BalAmt", BalAmt);
            sqlCmd.Parameters.AddWithValue("@checkDate", checkDate);
            sqlCmd.Parameters.AddWithValue("@RID", id);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;



        }

        public static DataTable PaymentVoucharIndex(DateTime start, DateTime End , int CompanyID)
        {
            string query = @"select m.*,c.Amt,rvm.AC_Title as customer,d.AC_Title as cashBAnk,c.BalAmt,c.checkDate,c.ChkNo,c.DisAmt,c.InvNo,c.MOP_ID,c.Narr,c.PreAmt,c.SlipNo,c.SRT,c.AC_Code as RV_TransactionCode,c.ID from PV_M as m
                             inner join PV_D as c on m.RID=c.RID
                             inner join COA_D as d on d.AC_Code=c.AC_Code
                             inner join COA_D rvm on rvm.AC_Code=m.AC_Code
                             inner join AspNetUsers u on u.AccessFailedCount=m.CompID
                             where (CAST(m.EDate as Date) >= CAST(@StartDate as Date) and CAST(m.EDate as Date) <= CAST(@EndDate as Date))
                             AND m.CompID = @CompID";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Parameters.AddWithValue("@StartDate", start);
            sqlCmd.Parameters.AddWithValue("@EndDate", End);
            sqlCmd.Parameters.AddWithValue("@CompID", CompanyID);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;

        }

        public static DataTable ExpenseVoucharIndex(DateTime start, DateTime End, int CompanyID)
        {
            string query = @"select m.*,c.Amt as amount,rvm.AC_Title as customer,d.AC_Title as cashBAnk,c.ChkNo,c.InvNo,c.MOP_ID,c.Narr,c.SlipNo,c.SRT,c.AC_Code as RV_TransactionCode,c.ID from EV_M as m
inner join EV_D as c on m.RID=c.RID
inner join COA_D as d on d.AC_Code=c.AC_Code
inner join COA_D rvm on rvm.AC_Code=m.AC_Code

where (CAST(m.EDate as Date) >= CAST(@StartDate as Date) and CAST(m.EDate as Date) <= CAST(@EndDate as Date))  AND m.CompID = @CompID";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Parameters.AddWithValue("@StartDate", start);
            sqlCmd.Parameters.AddWithValue("@EndDate", End);
            sqlCmd.Parameters.AddWithValue("@CompID", CompanyID);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;

        }

        public static DataTable BookingSummary(DateTime dtStart, DateTime dtEnd, String userName, String type, int CompanyID)
        {
            string query = @"SELECT 
	                         OrderNo , Amount , OrderDate , OrderType  , KOTID , us.UserName , GST , Discount	
                             FROM tbl_Order PR
	                         INNER JOIN AspNetUsers us ON us.Id = PR.UserID
                             WHERE
                             ('All' = @userName OR us.[UserName] LIKE @userName)
                             AND ('0' = @type OR PR.[KOTID] LIKE @type)
                             AND (CAST( PR.OrderDate as Date) >= CAST(@reportStartDate  as Date) and CAST( PR.OrderDate as Date ) <= CAST(@reportEndDate as Date)) AND PR.CompanyID = @CompanyID";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@reportStartDate", dtStart);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", dtEnd);
            sqlCmd.Parameters.AddWithValue("@userName", userName);
            sqlCmd.Parameters.AddWithValue("@type", type);
            sqlCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }


        public static DataTable GetNewSerialNo()
        {
            string query = @"SELECT TOP 1 ISNULL(RIGHT(OrderNo, LEN(OrderNo) - 1), 0 ) AS OrdrID from tbl_Order  order by OrdrID desc";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable GetPaidCustomerByMonth(int month, int year)
        {
            var query = @"select DATEADD(month,@Month-1,DATEADD(year,@Year-1900,0))as ToDate,DATEADD(day,-1,DATEADD(month,@Month,DATEADD(year,@Year-1900,0))) as FromDate";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@Month", month);
            sqlCmd.Parameters.AddWithValue("@Year", year);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable GetPaidCustomerByMonth(DateTime fromDate, DateTime ToDate , int CompanyID)
        {
            var query = @"select c.CusName, SUM(g.Credit) AS [TOTAL AMOUNT],c.AC_Code
              from Customers c left join GL g
              ON c.AC_Code= g.AC_Code
              where  g.[GLDate]  between @StartDate and @EndDate AND c.CompanyID = CompanyID
              GROUP BY c.CusName , c.AC_Code";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@StartDate", fromDate);
            sqlCmd.Parameters.AddWithValue("@EndDate", ToDate);
            sqlCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }


        public static DataTable CategoryandLabWiseTest(DateTime dtStart, DateTime dtEnd, String userName)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(Report_CategoryandLabWiseTest, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@reportStartDate", dtStart);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", dtEnd);
            sqlCmd.Parameters.AddWithValue("@userName", userName);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "Report_CategoryandLabWiseTest";
            return dt;
        }

        public static DataTable ItemsSummary(DateTime dtStart, DateTime dtEnd, String Item)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand("Item_Summary", SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@reportStartDate", dtStart);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", dtEnd);
            sqlCmd.Parameters.AddWithValue("@Item", Item);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "Item_Summary";
            return dt;
        }

        public static DataTable BookingDetailSummary(DateTime dtStart, DateTime dtEnd, String userName, String type)
        {
            string _query = @"SELECT  OrderNo , Amount , OrderDate , OrderType  , PR.KOTID , us.UserName , GST , Discount
	                         ,Items.IName , Qty as Qty , Rate as Rate  
                              FROM 
                              	tbl_Order PR
                              	INNER JOIN AspNetUsers us ON us.Id = PR.UserID
                              	left join tbl_KOT on tbl_KOT.Id = PR.KOTID
                              	left join tbl_OrderDetails on tbl_OrderDetails.OrderId = PR.OrderId
                              	left join Items on Items.IID = tbl_OrderDetails.itemID
                              WHERE
                                  ('All' = @userName OR us.[UserName] LIKE @userName)
                              	AND ('0' = @type OR PR.[KOTID] LIKE @type)
                              	AND PR.OrderDate BETWEEN @reportStartDate AND @reportEndDate";
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(_query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@reportStartDate", dtStart);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", dtEnd);
            sqlCmd.Parameters.AddWithValue("@userName", userName);
            sqlCmd.Parameters.AddWithValue("@type", type);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            // dt.TableName = "Report_BookingDetailSummary";
            return dt;
        }

        public static DataTable MonthlySummary(DateTime dtStart, DateTime dtEnd)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(Report_MonthlySummary, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@reportStartDate", dtStart);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", dtEnd);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "Report_MonthlySummary";
            return dt;
        }

        public static DataTable MonthlySummary_Summary(DateTime dtStart, DateTime dtEnd)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(Report_MonthlySummary_Summary, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@reportStartDate", dtStart);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", dtEnd);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "Report_MonthlySummary_Summary";
            return dt;
        }

        public static DataTable BookingSummary_Report(DateTime dtStart, DateTime dtEnd, String userName)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(BookingSummary_ReportCount, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@reportStartDate", dtStart);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", dtEnd);
            sqlCmd.Parameters.AddWithValue("@userName", userName);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "BookingSummary_ReportCount";
            return dt;
        }

        public static DataTable BookingSummary_UserAmount(DateTime dtStart, DateTime dtEnd)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(BookingSummary_UserTotalAmount, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@reportStartDate", dtStart);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", dtEnd);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "BookingSummary_UserTotalAmount";
            return dt;
        }



        public static DataTable FreePatient(DateTime dtStart, DateTime dtEnd, Int32 docID)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(Report_FreePatients, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@dtStart", dtStart);
            sqlCmd.Parameters.AddWithValue("@dtEnd", dtEnd);
            sqlCmd.Parameters.AddWithValue("@docID", docID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "Report_FreePatients";
            return dt;
        }

        public static DataTable Receipt(String labNo)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(Report_Receipt, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@labNo", labNo);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "Report_Receipt";
            return dt;
        }

        public static DataTable TestReportCount(DateTime dtStart, DateTime dtEnd, Int32 reportID, String userName)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(Report_TestReport_Count, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@dtStart", dtStart);
            sqlCmd.Parameters.AddWithValue("@dtEnd", dtEnd);
            sqlCmd.Parameters.AddWithValue("@reportID", reportID);
            sqlCmd.Parameters.AddWithValue("@userName", userName);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "Report_TestReport_Count";
            return dt;
        }

        public static DataTable DiscountPatients(DateTime dtStart, DateTime dtEnd, Int32 docID)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(Report_DiscountPatients, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@dtStart", dtStart);
            sqlCmd.Parameters.AddWithValue("@dtEnd", dtEnd);
            sqlCmd.Parameters.AddWithValue("@docID", docID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "Report_DiscountPatients";
            return dt;
        }

        public static DataTable TestWiseDetailSumm_Count(DateTime dtStart, DateTime dtEnd, String userName)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(TestWiseDetailSummary_Count, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@reportStartDate", dtStart);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", dtEnd);
            sqlCmd.Parameters.AddWithValue("@userName", userName);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "TestWiseDetailSummary_Count ";
            return dt;
        }
        public static DataTable TestWiseDetailSumm(DateTime dtStart, DateTime dtEnd, String userName)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(TestWiseDetailSummary, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@reportStartDate", dtStart);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", dtEnd);
            sqlCmd.Parameters.AddWithValue("@userName", userName);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            dt.TableName = "TestWiseDetailSummary ";
            return dt;
        }


        public static DataTable getMaxACodeById(int CodeID, int CompanyID)
        {
            string query = @"select ISNULL(max(AC_Code),0) as Max from COA_D where CAC_Code=@cac_Code And CompanyID=@CompanyID  And InActive=@Delete ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
            sqlCmd.Parameters.AddWithValue("@cac_Code", CodeID);
            sqlCmd.Parameters.AddWithValue("@Delete", false);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }
        public static DataTable getMixACodeById(int CodeID, int CompanyID)
        {
            string query = @"select ISNULL(min(AC_Code),0) as Min from COA_D where CAC_Code=@cac_Code And CompanyID=@CompanyID  And InActive=@Delete";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
            sqlCmd.Parameters.AddWithValue("@cac_Code", CodeID);
            sqlCmd.Parameters.AddWithValue("@Delete", false);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable GetExpiredItems(DateTime Start, DateTime End,int CompanyID)
        {
            string query = @"select Itemledger.ExpDT,Itemledger.CTN
      ,Itemledger.PCS
      ,Itemledger.[OPN] ,Items.IName
       from Itemledger
	   
	    left join Items on Itemledger.IID= Items.IID 
		 where  Itemledger.TypeCode=2 and
		 
		 (CAST(Itemledger.ExpDT as Date) >= CAST(@ToDate as Date) and CAST(Itemledger.ExpDT as Date ) <= CAST(@FromDate as Date))

	And Itemledger.CompanyID=@compy";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Parameters.AddWithValue("@ToDate", Start);
            sqlCmd.Parameters.AddWithValue("@FromDate", End);
            sqlCmd.Parameters.AddWithValue("@compy", CompanyID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable GetKarahiList(DateTime Start, DateTime End, int CompanyID)
        {
            string query = @"select * from tbl_karahi_M
		 where  
		 
		 (CAST(tbl_karahi_M.Date as Date) >= CAST(@ToDate as Date) and CAST(tbl_karahi_M.Date as Date ) <= CAST(@FromDate as Date))

	And tbl_karahi_M.Company=@compy";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Parameters.AddWithValue("@ToDate", Start);
            sqlCmd.Parameters.AddWithValue("@FromDate", End);
            sqlCmd.Parameters.AddWithValue("@compy", CompanyID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getCAshBookByDate(int Acode, int Acode2, DateTime StartDate, DateTime EndDate, int CompanyID)
        {
            string query = @"SELECT gl.GLDate,gl.Debit,gl.Credit,gl.Narration,concat(tt.Symbol, '-',gl.RID  ) as RpCode
                             FROM gl  left join RP tt on gl.TypeCode = tt.TypeCode
                             WHERE AC_Code BETWEEN @AcodeMin AND @AcodeMax and
                 (CAST(gl.GLDate as Date) >= CAST(@StartDate as Date) and CAST(gl.GLDate as Date ) <= CAST(@EndDate as Date))
                                       And CompID=@compyID";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@StartDate", StartDate);
            sqlCmd.Parameters.AddWithValue("@EndDate", EndDate);
            sqlCmd.Parameters.AddWithValue("@AcodeMin", Acode);
            sqlCmd.Parameters.AddWithValue("@AcodeMax", Acode2);
            sqlCmd.Parameters.AddWithValue("@compyID", CompanyID);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getCustomerPreviousBalance(DateTime Date, int CustomerID)
        {
            string query = @"SELECT
                           isnull(SUM( debit),0) as debit,ISNULL( sum(Credit),0) as credit
                           FROM
                           GL
                           WHERE

                           CAST(GLDate as Date) < CAST(@dtFrom as Date)
                            and AC_Code=@acCode";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@dtFrom", Date);
            sqlCmd.Parameters.AddWithValue("@acCode", CustomerID);


            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getVendorPreviousBalance(DateTime Date, int CustomerID)
        {
            string query = @"SELECT
                           isnull(SUM( debit),0) as debit,ISNULL( sum(Credit),0) as credit
                           FROM
                           GL
                           WHERE
                           CAST(GLDate as Date) < CAST(@dtFrom as Date) and AC_Code=@acCode";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@dtFrom", Date);
            sqlCmd.Parameters.AddWithValue("@acCode", CustomerID);


            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }
        public static DataTable getVendorLedgerSummaryByDate(DateTime StartDate, DateTime EndDate, int CustomerID)
        {
            string query = @"SELECT 
    convert(varchar(10), gl.GLDate, 101) AS gldate,
    concat(
        MAX(tt.Symbol), 
        '-', 
        gl.RID, 
        ' (', 
        CASE 
            WHEN MAX(tt.Symbol) = 'SJ' THEN (select InvNo from Sales_M where RID=gl.RID ) 
			 WHEN MAX(tt.Symbol) = 'PJ' THEN (select InvNo from Pur_M where RID=gl.RID ) 
            ELSE MAX(gl.Narration) 
        END, 
        ')'
    ) AS reference,
    gl.RID,
    gl.TypeCode,
    SUM(debit) AS debit,
    SUM(credit) AS credit
FROM 
    GL gl
LEFT JOIN 
    RP tt ON gl.TypeCode = tt.TypeCode
WHERE 
    (CAST(gl.GLDate AS Date) >= CAST(@StartDate AS Date) 
    AND CAST(gl.GLDate AS Date) <= CAST(@EndDate AS Date)) 
    AND AC_Code = @acCode
GROUP BY  
    gl.TypeCode,
    gl.RID,
    convert(varchar(10), gl.GLDate, 101)
ORDER BY 
    convert(varchar(10), gl.GLDate, 101)";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@StartDate", StartDate);
            sqlCmd.Parameters.AddWithValue("@EndDate", EndDate);
            sqlCmd.Parameters.AddWithValue("@acCode", CustomerID);


            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getItemAdjustmen(DateTime StartDate, DateTime EndDate, int CompanyID)
        {
            string query = @"select Adj_M.EDate ,COA_D.AC_Title as Account ,COA_D.AC_Code as AccountID,
                             Items.IName as product,Items.AC_Code_Inv as productCode,Items.IID as productID,
                             tbl_Warehouse.Warehouse as warehouse ,tbl_Warehouse.WID as warehouseID ,Adj_M.RID ,
                             Adj_D.Qty as QtyIN , Adj_D.Qty2 as QtyOut,
                             Adj_D.PurPrice , Adj_D.Debit ,Adj_D.Credit from Adj_M

                            inner join Adj_D on Adj_M.RID=Adj_D.RID
                            left join tbl_Warehouse on tbl_Warehouse.WID=Adj_M.WID
                            left join COA_D on COA_D.AC_Code=Adj_M.AC_Code
                            left join Items on Items.IID=Adj_D.IID

                    where

                            CAST(Adj_M.EDate AS DATE) BETWEEN CAST(@dtfrom AS DATE) AND CAST(@dtto AS DATE)
                            And Adj_M.CompID=@companyid

                            order by Adj_M.EDate";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@dtfrom", StartDate);
            sqlCmd.Parameters.AddWithValue("@dtto", EndDate);
            sqlCmd.Parameters.AddWithValue("@companyid", CompanyID);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getProfitAndLoss(int Min, int max,DateTime StartDate, DateTime EndDate, int CompanyID)
        {
            string query = @"select sum(GL.Debit) as debit,sum(GL.Credit) as Credit,COA_D.AC_Code,COA_D.AC_Title from GL left join COA_D on GL.AC_Code= COA_D.AC_Code
WHERE GL.AC_Code BETWEEN @AcodeMin AND @AcodeMax and
(CAST(GL.GLDate as Date) >= CAST(@StartDate as Date) and CAST(GLDate as Date ) <= CAST(@EndDate as Date))
and GL.CompID=@company
group by COA_D.AC_Code , COA_D.AC_Title;";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@AcodeMin", Min);
            sqlCmd.Parameters.AddWithValue("@AcodeMax", max);
            sqlCmd.Parameters.AddWithValue("@StartDate", StartDate);
            sqlCmd.Parameters.AddWithValue("@EndDate", EndDate);
            sqlCmd.Parameters.AddWithValue("@company", CompanyID);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getStockByID(int ID, int companyID)
        {
            string query = @"select ISNULL(sum(OPN),0)+ISNULL(sum(pj),0)-ISNULL(sum(PRJ),0)
                           - ISNULL(sum(SJ),0) +ISNULL(sum(SRJ),0) +  ISNULL(sum(SCH_IN),0) 
                           -  ISNULL(sum(SCH_Out),0) +  ISNULL(sum(ADJ_IN),0) -  ISNULL(sum(ADJ_OUT),0)
                           +  ISNULL(sum(TR_IN),0) - ISNULL(sum(TR_OUT),0)
                           +  ISNULL( sum(MFG_IN),0)  -ISNULL( sum(MFG_OUT),0) as total

                             
                            from  [dbo].[Itemledger] where IID=@id and CompanyID=@compyID";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }
        public static DataTable getWareHouseStockByID(int ID, int companyID,int warehouse)
        {
            string query = @"select ISNULL(sum(OPN),0)+ISNULL(sum(pj),0)-ISNULL(sum(PRJ),0)
                           - ISNULL(sum(SJ),0) +ISNULL(sum(SRJ),0) +  ISNULL(sum(SCH_IN),0) 
                           -  ISNULL(sum(SCH_Out),0) +  ISNULL(sum(ADJ_IN),0) -  ISNULL(sum(ADJ_OUT),0)
                           +  ISNULL(sum(TR_IN),0) - ISNULL(sum(TR_OUT),0)
                           +  ISNULL( sum(MFG_IN),0)  -ISNULL( sum(MFG_OUT),0) as total

                             
                            from  [dbo].[Itemledger] where IID=@id and CompanyID=@compyID and WID =@WID";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@WID", warehouse);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getTodayStockByID(int ID, int companyID,DateTime time)
        {
            string query = @"select ISNULL(sum(OPN),0)+ISNULL(sum(pj),0)-ISNULL(sum(PRJ),0)
                           - ISNULL(sum(SJ),0) +ISNULL(sum(SRJ),0) +  ISNULL(sum(SCH_IN),0) 
                           -  ISNULL(sum(SCH_Out),0) +  ISNULL(sum(ADJ_IN),0) -  ISNULL(sum(ADJ_OUT),0)
                           +  ISNULL(sum(TR_IN),0) - ISNULL(sum(TR_OUT),0)
                           +  ISNULL( sum(MFG_IN),0)  -ISNULL( sum(MFG_OUT),0) as total

                             from  [dbo].[Itemledger] where IID=@id And CompanyID=@compyID and (CAST(EDate as Date) = CAST(@dtFrom as Date ))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }
        public static DataTable getTodayStockByIDByWareHouse(int ID, int companyID, DateTime time,int warehouse)
        {
            string query = @"select ISNULL(sum(OPN),0)+ISNULL(sum(pj),0)-ISNULL(sum(PRJ),0)
                           - ISNULL(sum(SJ),0) +ISNULL(sum(SRJ),0) +  ISNULL(sum(SCH_IN),0) 
                           -  ISNULL(sum(SCH_Out),0) +  ISNULL(sum(ADJ_IN),0) -  ISNULL(sum(ADJ_OUT),0)
                           +  ISNULL(sum(TR_IN),0) - ISNULL(sum(TR_OUT),0)
                           +  ISNULL( sum(MFG_IN),0)  -ISNULL( sum(MFG_OUT),0) as total

                             from  [dbo].[Itemledger] where IID=@id And CompanyID=@compyID and WID =@WID and (CAST(EDate as Date) = CAST(@dtFrom as Date ))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@WID", warehouse);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getStockByPreviosDate(int ID, int companyID,DateTime time)
        {
            string query = @"select ISNULL(sum(OPN),0)+ISNULL(sum(pj),0)-ISNULL(sum(PRJ),0)
                           - ISNULL(sum(SJ),0) +ISNULL(sum(SRJ),0) +  ISNULL(sum(SCH_IN),0) 
                           -  ISNULL(sum(SCH_Out),0) +  ISNULL(sum(ADJ_IN),0) -  ISNULL(sum(ADJ_OUT),0)
                           +  ISNULL(sum(TR_IN),0) - ISNULL(sum(TR_OUT),0)
                           +  ISNULL( sum(MFG_IN),0)  -ISNULL( sum(MFG_OUT),0) as total

                             from  [dbo].[Itemledger] where IID=@id And CompanyID=@compyID and (CAST(EDate as Date) < CAST(@dtFrom as Date ))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);

            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getStockByPreviosDateByWareHouse(int ID, int companyID, DateTime time,int warehouse)
        {
            string query = @"select ISNULL(sum(OPN),0)+ISNULL(sum(pj),0)-ISNULL(sum(PRJ),0)
                           - ISNULL(sum(SJ),0) +ISNULL(sum(SRJ),0) +  ISNULL(sum(SCH_IN),0) 
                           -  ISNULL(sum(SCH_Out),0) +  ISNULL(sum(ADJ_IN),0) -  ISNULL(sum(ADJ_OUT),0)
                           +  ISNULL(sum(TR_IN),0) - ISNULL(sum(TR_OUT),0)
                           +  ISNULL( sum(MFG_IN),0)  -ISNULL( sum(MFG_OUT),0) as total

                             from  [dbo].[Itemledger] where IID=@id And CompanyID=@compyID and WID =@WID  and (CAST(EDate as Date) < CAST(@dtFrom as Date ))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandTimeout = 0;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@WID", warehouse);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);

            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }
        public static DataTable getStockByPurchaseDate(int ID, int companyID, DateTime time)
        {
            string query = @"select ISNULL(sum(OPN),0)+ ISNULL(sum(pj),0) as total

                             from  [dbo].[Itemledger] where IID=@id And CompanyID=@compyID and  (CAST(EDate as Date) = CAST(@dtFrom as Date))"; 

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection); 
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getStockByPurchaseDateByWarehouse(int ID, int companyID, DateTime time, int warehouse)
        {
            string query = @"select ISNULL(sum(OPN),0)+ ISNULL(sum(pj),0) as total

                             from  [dbo].[Itemledger] where IID=@id And CompanyID=@compyID and WID =@WID and  (CAST(EDate as Date) = CAST(@dtFrom as Date))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@WID", warehouse);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }
        public static DataTable getStockBySaleDate(int ID, int companyID, DateTime time)
        {
            string query = @"select ISNULL(sum(SJ),0) as total

                             from  [dbo].[Itemledger] where IID=@id And CompanyID=@compyID and  (CAST(EDate as Date) = CAST(@dtFrom as Date)) ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getStockBySaleDateByWarehouse(int ID, int companyID, DateTime time, int warehouse)
        {
            string query = @"select ISNULL(sum(SJ),0) as total

                             from  [dbo].[Itemledger] where IID=@id And CompanyID=@compyID  and WID =@WID and  (CAST(EDate as Date) = CAST(@dtFrom as Date)) ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@WID", warehouse);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getStockBySalePrice(int ID, int companyID, DateTime time,int Rid=5)
        {
            string query = @"select ISNULL(sum(Amt),0) as total

                             from  [dbo].[Itemledger] where IID=@id and TypeCode=@Rid And CompanyID=@compyID and  (CAST(EDate as Date) = CAST(@dtFrom as Date)) ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@Rid", Rid);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getStockBySalePriceByWarehouse(int ID, int companyID, DateTime time, int warehouse, int Rid = 5)
        {
            string query = @"select ISNULL(sum(Amt),0) as total

                             from  [dbo].[Itemledger] where IID=@id and WID =@WID and TypeCode=@Rid And CompanyID=@compyID and  (CAST(EDate as Date) = CAST(@dtFrom as Date)) ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@Rid", Rid);
            sqlCmd.Parameters.AddWithValue("@WID", warehouse);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getStockByPurchasePrice(int ID, int companyID, DateTime time ,int Rid = 2)
        {
            string query = @"select ISNULL(sum(Amt),0) as total

                             from  [dbo].[Itemledger] where IID=@id and TypeCode=@Rid And CompanyID=@compyID and  (CAST(EDate as Date) = CAST(@dtFrom as Date)) ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@Rid", Rid);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }
        public static DataTable getStockByPurchasePriceByWareHouse(int ID, int companyID, DateTime time,  int warehouse,int Rid = 2)
        {
            string query = @"select ISNULL(sum(Amt),0) as total

                             from  [dbo].[Itemledger] where IID=@id and TypeCode=@Rid And CompanyID=@compyID and WID =@WID and  (CAST(EDate as Date) = CAST(@dtFrom as Date)) ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@id", ID);
            sqlCmd.Parameters.AddWithValue("@compyID", companyID);
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@Rid", Rid);
            sqlCmd.Parameters.AddWithValue("@WID", warehouse);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }




        public static DataTable GetKarahiSummary( DateTime reportStartDate, DateTime reportEndDate, int CompanyID)

        {
            var _query = @"select tbl_karahi_Item.ItemName ,tbl_karahi_Item.Id, ISNULL(sum(tbl_Karahi_D.Qty),0) as Total from tbl_karahi_M 

              left join tbl_Karahi_D on tbl_Karahi_D.Rid = tbl_karahi_M.ID 

              left join tbl_karahi_Item on tbl_karahi_Item.Id = tbl_Karahi_D.itemId
              
      where     (CAST(tbl_karahi_M.Date as Date) >= CAST(@reportStartDate  as Date) and CAST(tbl_karahi_M.Date as Date ) <= CAST(@reportEndDate as Date)) And tbl_karahi_M.Company = @CompanyID
              
                group by tbl_karahi_Item.ItemName ,tbl_karahi_Item.Id
                order by tbl_karahi_Item.ItemName
               ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(_query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
          
            sqlCmd.Parameters.AddWithValue("@reportStartDate", reportStartDate);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", reportEndDate);
            sqlCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }


        public static DataTable GetItembyOrderDetail(DateTime reportStartDate, DateTime reportEndDate, int CompanyID, int id)

        {
            var _query = @"select Items.IName as name ,Items.IID, tbl_OrderDetails.OrderId,ISNULL(sum(tbl_OrderDetails.Qty),0) as Qty , ISNULL(sum(tbl_OrderDetails.disc),0) as Disc, 

               ISNULL(sum(tbl_OrderDetails.Rate),0) as Total    from tbl_Order 

              left join tbl_OrderDetails on tbl_OrderDetails.OrderId = tbl_Order.OrderId 

              left join Items on Items.IID = tbl_OrderDetails.itemID
              
      where     (CAST(tbl_Order.OrderDate as Date) >= CAST(@reportStartDate  as Date) and CAST(tbl_Order.OrderDate as Date ) <= CAST(@reportEndDate as Date)) And tbl_Order.CompanyID = @CompanyID and tbl_OrderDetails.itemID=@IID
              
                group by Items.IName ,Items.IID,tbl_OrderDetails.OrderId
                order by tbl_OrderDetails.OrderId
               ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(_query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Parameters.AddWithValue("@reportStartDate", reportStartDate);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", reportEndDate);
            sqlCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
            sqlCmd.Parameters.AddWithValue("@IID", id);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable GetItembySaleDetail(DateTime reportStartDate, DateTime reportEndDate, int CompanyID, int id)

        {
            var _query = @"select Items.IName as name ,Items.IID, Sales_D.RID,ISNULL(sum(Sales_D.Qty),0) as Qty , ISNULL(sum(Sales_D.Qty),0) as Disc, ISNULL(sum(Sales_D.Pur_D_UnitID),0) as Unit, 

               ISNULL(sum(Sales_D.Amt),0) as Total    from Sales_M 

              left join Sales_D on Sales_M.RID = Sales_D.RID 

              left join Items on   Sales_D.IID  = Items.IID 
              
      where     (CAST(Sales_M.EDate as Date) >= CAST(@reportStartDate  as Date) and CAST(Sales_M.EDate as Date ) <= CAST(@reportEndDate as Date)) And Sales_M.CompID = @CompanyID and Sales_D.IID=@IID
              
                group by Items.IName ,Items.IID,Sales_D.RID
                order by Sales_D.RID
               ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(_query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Parameters.AddWithValue("@reportStartDate", reportStartDate);
            sqlCmd.Parameters.AddWithValue("@reportEndDate", reportEndDate);
            sqlCmd.Parameters.AddWithValue("@CompanyID", CompanyID);
            sqlCmd.Parameters.AddWithValue("@IID", id);

            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }


        public static DataTable getTotalDebitAndCreditByACcode(int code, DateTime time)
        {
            string query = @"select ISNULL(sum(Debit),0) as debit, ISNULL(sum(Credit),0) as credit

                             from  [dbo].[GL] where AC_Code=@code  and  (CAST(GLDate as Date) = CAST(@dtFrom as Date))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@code", code);
          
            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getTotalSaleAfterDiscount(int company, DateTime time)
        {
            string query = @"select ISNULL(sum(NetAmt),0) as NetAmt, ISNULL(sum(DisAmt),0) as DisAmt

                             from  [dbo].[Sales_M] where CompID=@company and (CAST(EDate as Date) = CAST(@dtFrom as Date))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
         

            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@company", company);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getTotalPurchaseAfterDiscount(int company, DateTime time)
        {
            string query = @"select ISNULL(sum(NetAmt),0) as NetAmt, ISNULL(sum(DisAmt),0) as DisAmt

                             from  [dbo].[Pur_M] where CompID=@company and (CAST(EDate as Date) = CAST(@dtFrom as Date))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;


            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@company", company);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getTotalPaymentAfterDiscount(int company, DateTime time)
        {
            string query = @"select ISNULL(sum(Amt),0) as Amt

                             from  [dbo].[PV_D] left join [dbo].[PV_M] on [dbo].[PV_D].RID=[dbo].[PV_M].RID where CompID=@company and (CAST(EDate as Date) = CAST(@dtFrom as Date))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;


            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@company", company);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getTotalReceiveAfterDiscount(int company, DateTime time)
        {
            string query = @"select ISNULL(sum(Amt),0) as Amt

                             from  [dbo].[RV_D] left join [dbo].[RV_M] on [dbo].[RV_D].RID=[dbo].[RV_M].RID where CompID=@company and (CAST(EDate as Date) = CAST(@dtFrom as Date))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;


            sqlCmd.Parameters.AddWithValue("@dtFrom", time);
            sqlCmd.Parameters.AddWithValue("@company", company);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getcustumerList(int company)
        {
            string query = @"  select [dbo].[Customers].AC_Code as AC_Code ,[dbo].[Customers].CusName as Name ,
                            [dbo].[tbl_city].[CityName] as city

                             from  [dbo].[Customers] left join [dbo].[tbl_city] on [dbo].[Customers].City=[dbo].[tbl_city].Id

                             where [dbo].[Customers].CompanyID=@company and [dbo].[Customers].InActive='false' ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;


           
            sqlCmd.Parameters.AddWithValue("@company", company);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }

        public static DataTable getVendorList(int company)
        {
            string query = @" select [dbo].[Vendors].AC_Code as AC_Code ,[dbo].[Vendors].VendName as Name ,
                            [dbo].[tbl_city].[CityName] as city

                             from  [dbo].[Vendors] left join [dbo].[tbl_city] on [dbo].[Vendors].City=[dbo].[tbl_city].Id

                             where [dbo].[Vendors].CompanyID=@company and [dbo].[Vendors].InActive='false'  ";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;



            sqlCmd.Parameters.AddWithValue("@company", company);
            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }


        public static DataTable getjpurnalVoucherSale(DateTime StartDate, DateTime EndDate, int company)
        {
            string query = @"	SELECT jv.AC_Code,jv.AC_Code2,jv.Narr,jv.Amt,jm.EDate ,jv.RID from JV_D as jv left join JV_M as jm on jv.RID=jm.RID

	where 
jm.CompID=@company and jm.ForSale='true' and
          (CAST( [EDate]as Date) >= CAST(@StartDate as Date) and CAST(@StartDate as Date ) <= CAST(@EndDate as Date))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@StartDate", StartDate);
            sqlCmd.Parameters.AddWithValue("@EndDate", EndDate);
            sqlCmd.Parameters.AddWithValue("@company", company);


            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }


        public static DataTable getjpurnalVoucher(DateTime StartDate, DateTime EndDate, int company)
        {
            string query = @"SELECT jv.AC_Code,jv.AC_Code2,jv.Narr,jv.Amt,jm.EDate ,jv.RID from JV_D as jv left join JV_M as jm on jv.RID=jm.RID

        where 
          jm.CompID=@company and 
          (CAST( [EDate]as Date) >= CAST(@StartDate as Date) and CAST(@StartDate as Date ) <= CAST(@EndDate as Date))";

            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand(query, SqlHelper.DefaultSqlConnection);
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Parameters.AddWithValue("@StartDate", StartDate);
            sqlCmd.Parameters.AddWithValue("@EndDate", EndDate);
            sqlCmd.Parameters.AddWithValue("@company", company);


            SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
            adp.Fill(dt);
            adp.Dispose();
            sqlCmd.Dispose();
            return dt;
        }



    }
}
