
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Lib
{
    public class SP_Exec
    {
        public string RunStoredProcScalar(String SpName, SqlConnection con, SqlTransaction trans, object[] parameters)
        {
            string val = null;
            try
            {
                SqlCommand cmd = new SqlCommand(SpName, con, trans);
                if (parameters != null && parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                val = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                trans.Rollback();

            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return val;
        }

        public DataTable RunStoredProcParams(String SpName, SqlConnection con, SqlTransaction trans, object[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(SpName, con, trans);

                if (parameters != null && parameters.Any())
                {
                    cmd.Parameters.AddRange(parameters);
                }
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                //new SqlParameter("@CustomerID", custId)
                return dt;

                //trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return dt;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
    }
}