using Lib.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Lib
{
    public class DataAccess
    {
        public List<T> Get<T>(String sp, Object obj, SqlConnection con, SqlTransaction trans)
        {
            var param = obj.ToSqlParamsArray();
            SP_Exec exec = new SP_Exec();
            var result = exec.RunStoredProcParams(sp, con, trans, param);
            var objResult = Common.ConvertDataTable<T>(result);
            return objResult;
        }

        public String GetScalar(String sp, Object obj, SqlConnection con, SqlTransaction trans)
        {
            var param = obj.ToSqlParamsArray();
            SP_Exec exec = new SP_Exec();
            var objResult = exec.RunStoredProcScalar(sp, con, trans, param);
            return objResult;
        }
    }
}
