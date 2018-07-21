using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DBHelper
{
    public partial class DbHelper 
    {
        //连接字符串
        private static string connString { get; set; }

        public static Action<Exception> ErrorEvent { get; set; }
        /// <summary>
        /// 同步执行sql
        /// </summary>
        /// <param name="sql">update a set name=@name from a where id=@id</param>
        /// <param name="val">new { id=1 ,name="小明"}</param>
        /// <returns>结果</returns>
        public static bool ExecuteSql(string sql, object val)
        {
            return SqlHelper(sql,cmd =>
             {
                 cmd.CommandType = CommandType.Text;
                 foreach (var item in val.GetType().GetProperties())
                 {
                     var paramName = $"@{item.Name}";
                     var paramValue = item.GetValue(val);
                     var pm = paramValue == null ? new SqlParameter(paramName, DBNull.Value) : new SqlParameter(paramName, paramValue);
                     cmd.Parameters.Add(pm);
                 }
                 return cmd.ExecuteNonQuery() > 0;
             });
        }
        
        //异步执行sql
        public static Task<bool> ExecuteSqlAsync(string sql, object val)
        {
            return SqlHelper(sql, async cmd =>
            {
                cmd.CommandType = CommandType.Text;
                foreach (var item in val.GetType().GetProperties())
                {
                    var paramName = $"@{item.Name}";
                    var paramValue = item.GetValue(val);
                    var pm = paramValue == null ? new SqlParameter(paramName, DBNull.Value) : new SqlParameter(paramName, paramValue);
                    cmd.Parameters.Add(pm);
                }
                return await cmd.ExecuteNonQueryAsync().ContinueWith(t=>Convert.ToInt32(t.Result) > 0);
            });
        }

        //获取DataSet
        public static DataSet GetDataSet(string sql, object val)
        {
            return SqlHelper(sql, cmd =>
            {
                cmd.CommandType = CommandType.Text;
                foreach (var item in val.GetType().GetProperties())
                {
                    var paramName = $"@{item.Name}";
                    var paramValue = item.GetValue(val);
                    var pm = paramValue == null ? new SqlParameter(paramName, DBNull.Value) : new SqlParameter(paramName, paramValue);
                    cmd.Parameters.Add(pm);
                }
                using (SqlDataAdapter objDa = new SqlDataAdapter(cmd))
                {
                    using (DataSet ds = new DataSet())
                    {
                        objDa.Fill(ds);
                        return ds;
                    }
                }
            });
        }

        //查询单值
        public static Dictionary<string, object> GetSingle(string sql, object val)
        {
            return SqlHelper(sql, cmd =>
            {
                cmd.CommandType = CommandType.Text;
                foreach (var item in val.GetType().GetProperties())
                {
                    var paramName = $"@{item.Name}";
                    var paramValue = item.GetValue(val);
                    var pm = paramValue == null ? new SqlParameter(paramName, DBNull.Value) : new SqlParameter(paramName, paramValue);
                    cmd.Parameters.Add(pm);
                }
                using (SqlDataReader data =cmd.ExecuteReader())
                {
                    if (data.HasRows)
                    {
                        if (data.Read())
                        {
                            Dictionary<string, object> dictionary = new Dictionary<string, object>();
                            for (int i = 0; i < data.FieldCount; i++)
                            {
                                dictionary.Add(data.GetString(i), data.GetValue(i));
                            }
                            return dictionary;
                        }
                        return null;
                    }
                    else
                        return null;
                }
            });
        }

        /// 执行存储过程--查询
        public static DataSet QueryProcedure(string storedProcName, object val)
        {
            return SqlHelper(storedProcName, cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in val.GetType().GetProperties())
                {
                    var paramName = $"@{item.Name}";
                    var paramValue = item.GetValue(val);
                    var pm = paramValue == null ? new SqlParameter(paramName, DBNull.Value) : new SqlParameter(paramName, paramValue);
                    cmd.Parameters.Add(pm);
                }
                using (SqlDataAdapter objDa = new SqlDataAdapter(cmd))
                {
                    using (DataSet ds = new DataSet())
                    {
                        objDa.Fill(ds);
                        return ds;
                    }
                }
            });
        }
         // 执行存储过程--修改
        public static bool ExecProcedure(string storedProcName, object val)
         {
            return SqlHelper(storedProcName, cmd =>
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var item in val.GetType().GetProperties())
                {
                    var paramName = $"@{item.Name}";
                    var paramValue = item.GetValue(val);
                    var pm = paramValue == null ? new SqlParameter(paramName, DBNull.Value) : new SqlParameter(paramName, paramValue);
                    cmd.Parameters.Add(pm);
                }
                return cmd.ExecuteNonQuery() > 0;
            });
        }

        private static T SqlHelper<T>(string sql, Func<SqlCommand, T> func)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sql, conn);
                return func(command);
            }
        }
 
    }





    public class DbHelperContext : IDbHelper
    {
        private IDbHelper _base = null;
        public DbHelperContext(IDbHelper @base)
        {
            this._base = @base;
        }
        public bool ExecuteSql(string sql, object val)
        {
            try
            {
                return _base.ExecuteSql(sql, val);
            }
            catch (Exception e)
            {
                return false;
            }
           
        }

        public Task<bool> ExecuteSqlAsync(string sql, object val)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataTable(string sql, object val)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> GetSingle(string sql, object val)
        {
            throw new NotImplementedException();
        }
    }

    public static class JoinToStringExtend
    {
        public static string Join(this IEnumerable<string> list, string connector)
        {
            return string.Join(connector, list.ToArray());
        }
    }
}
