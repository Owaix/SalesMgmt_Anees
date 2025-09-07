using Lib.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Lib
{
    public class SyncDb
    {
        public string SyncDbe()
        {
            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Lib.Entity");
            StringBuilder stringbldr = new StringBuilder();
            foreach (Type type in typelist.Where(x => !x.Name.Contains("_Result") && !x.Name.Contains("SaleManagerEntities")).ToList())
            {
                var dbName = SqlHelper.DefaultSqlConnection.Database;
                string query = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG='" + dbName + "' and TABLE_NAME = '" + type.Name + "' ";
                var dt = SqlHelper.ExecuteSP(query);
                if (dt.Rows.Count == 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("create table [dbo].[{0}](", type.Name);
                    int i = 0;
                    for (i = 0; i < type.GetProperties().Length; i++)
                    {
                        string returnType;
                        var prop = type.GetProperties()[i];
                        var types = prop.PropertyType;
                        var underlyingType = Nullable.GetUnderlyingType(types);
                        if (underlyingType != null)
                        {
                            returnType = underlyingType.Name ?? "";
                        }
                        else
                        {
                            returnType = types.Name ?? "";
                        }
                        if (i == 0 && (prop.Name.ToLower().Contains("id")))
                        {
                            sb.AppendFormat("{0} int primary key identity(1,1) not null,", prop.Name);
                            continue;
                        }
                        sb.AppendFormat("[{0}] {1} {2},", prop.Name, GetType(returnType), "");
                    }
                    sb.Append(")");
                    var createQuery = sb.ToString().Substring(0, sb.Length - 2) + ") \n  \n";
                    SqlCommand command = new SqlCommand(createQuery, SqlHelper.DefaultSqlConnection);
                    using (command)
                    {
                        try
                        {
                            var rep = SqlHelper.ExecuteNonQuery(command);
                            stringbldr.Append("Table " + type.Name + " has Successsfully Inserted ... ~");
                        }
                        catch (Exception ex)
                        {
                            stringbldr.Append("Table " + type.Name + " has Failed To Insert !!! ~" + ex.Message);
                        }
                    }
                }
                else
                {
                    if (type.Name.Contains("AspNet"))
                    {
                        continue;
                    }
                    int j;
                    for (j = 0; j < type.GetProperties().Length; j++)
                    {
                        StringBuilder sbProp = new StringBuilder();
                        string returnType;
                        var prop = type.GetProperties()[j];
                        var types = prop.PropertyType;

                        var underlyingType = Nullable.GetUnderlyingType(types);
                        if (underlyingType != null)
                        {
                            returnType = underlyingType.Name ?? "";
                        }
                        else
                        {
                            returnType = types.Name ?? "";
                        }

                        string colQuery = "SELECT COLUMN_NAME , DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'" + type.Name + "' AND COLUMN_NAME = '" + prop.Name + "' ";
                        dt = SqlHelper.ExecuteSP(colQuery);
                        if (dt.Rows.Count == 0)
                        {
                            if (j == 0 && (prop.Name.ToLower().Contains("id")))
                            {
                                sbProp.AppendFormat("ALTER TABLE {0} ADD {1} int primary key identity(1,1) not null", type.Name, prop.Name);
                                continue;
                            }
                            sbProp.AppendFormat("ALTER TABLE {0} ADD {1} {2} ", type.Name, prop.Name, GetType(returnType));
                            SqlCommand command = new SqlCommand(sbProp.ToString(), SqlHelper.DefaultSqlConnection);
                            using (command)
                            {
                                try
                                {
                                    if (sbProp.ToString() != string.Empty)
                                    {
                                        var rep = SqlHelper.ExecuteNonQuery(command);
                                        stringbldr.Append("Column " + prop.Name + " Successsfully Added In Table " + type.Name + " ... ~");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    stringbldr.Append("Column " + prop.Name + " Failed To Added In Table " + type.Name + " !!! ~");
                                }
                            }
                        }
                        else
                        {
                            var DATA_TYPE = dt.Rows[0]["DATA_TYPE"].ToString();
                            var returnTy = GetType(returnType);
                            if (!returnTy.Contains(DATA_TYPE))
                            {
                                if (DATA_TYPE == "nvarchar" && returnTy == "varchar(max)")
                                {
                                    continue;
                                }
                                sbProp = new StringBuilder();
                                sbProp.AppendFormat("ALTER TABLE {0} ALTER COLUMN {1} {2}", type.Name, prop.Name, GetType(returnType));
                                SqlCommand command = new SqlCommand(sbProp.ToString(), SqlHelper.DefaultSqlConnection);
                                using (command)
                                {
                                    try
                                    {
                                        if (sbProp.ToString() != string.Empty)
                                        {
                                            var sa = sbProp.ToString();
                                            var rep = SqlHelper.ExecuteNonQuery(command);
                                            stringbldr.Append("DataType in Column " + prop.Name + " Successsfully Modified In Table " + type.Name + " ... ~");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        stringbldr.Append("DataType in Column " + prop.Name + " Failed To Modified In Table " + type.Name + " !!! ~");
                                    }
                                }
                            }
                        }
                    }
                    //  stringbldr.Append(sbProp);
                }
            }
            return stringbldr.ToString();
        }

        public bool IsNullable<T>(T value)
        {
            return Nullable.GetUnderlyingType(typeof(T)) != null;
        }


        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        private string GetType(string type)
        {
            string sqlType = "";
            switch (type.ToLower())
            {
                case "decimal":
                    sqlType = "decimal(20,2)";
                    break;
                case "string":
                    sqlType = "varchar(max)";
                    break;
                case "int16":
                case "int32":
                case "int64":
                    sqlType = "int";
                    break;
                case "datetime":
                    sqlType = "datetime";
                    break;
                case "boolean":
                    sqlType = "bit";
                    break;
                case "double":
                    sqlType = "float";
                    break;
            }
            return sqlType;
        }


    }
}
