using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class DBHelper0
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string connectionString;
        public DBHelper0()
        {
            connectionString = "";
        }

        public DataTable NewSearch(string sql, int pageindex, int pagesize, List<SqlParameter> list = null, string sortname = "ID", string asc = "ASC")
        {
            string sqlinfo = $@"SELECT  * FROM  (SELECT  ROW_NUMBER() OVER ( ORDER BY {sortname} {asc} ) AS RowNumber , * FROM  ({sql}) T) T1
               WHERE RowNumber BETWEEN {pagesize * (pageindex - 1) + 1} AND {pagesize * pageindex} ";
            sqlinfo+= $"select @RecordCount=count(1) from ({sql})t";
            return GetDataTable(sql,list.ToArray());
        }
        public bool NewDele(string id, string tableName)
        {
            return ExecuteSql($"delete  from {tableName} where ID in (Select Data FROM dbo.Split(@ID,','))",new SqlParameter("@ID", id)) > 0;  
        }
        public Dictionary<string,object> NewSearchOne(string id, string tableName, string sql=null)
        {

            Dictionary<string, object> drDic = new Dictionary<string, object>();
            DataTable table;
            if (sql==null)
            {
                 table = GetDataTable($"select * from {tableName} where ID = @ID", new SqlParameter("@ID", id));
            }
            else
            {
                table = GetDataTable(sql, new SqlParameter("@ID", id));
            }
          
            if (table != null && table.Rows.Count > 0)
            {
                drDic = new Dictionary<string, object>();
                foreach (DataColumn item in table.Rows[0].Table.Columns)
                {
                    drDic.Add(item.ColumnName, table.Rows[0][item.ColumnName]);
                }
            }
            return drDic;
        }
        public bool NewEdit<T>(T t,string tableName = null)
        {
            string[] columns = t.GetType().GetProperties().Where(p => p.Name.ToUpper() != "ID").Select(i =>i.Name).ToArray();

            string sql;
            if (tableName == null)  sql = $"INSERT INTO {t.GetType().Name} ({columns.Select(i=>string.Format("[{0}]",i))}) VALUES ({columns.Select(i => string.Format("@{0}", i))})";
            else sql = $"INSERT INTO {tableName} ({columns.Select(i => string.Format("[{0}]", i))}) VALUES ({columns.Select(i => string.Format("@{0}", i))})";

            return ExecuteSql(sql,GetSqlParameter(t)) > 0;
        }
        public bool NewAdd<T>(T t, string tableName=null)
        {
            string sql = string.Join(",", t.GetType().GetProperties().Where(p => p.Name.ToUpper() != "ID").Select(i => string.Format("[{0}]=@{0}", i.Name)));

            if (tableName==null) sql = $"UPDATE {t.GetType().Name} SET {sql} WHERE ID = @ID";
            else sql = $"UPDATE {tableName} SET {sql} WHERE ID = @ID";

            return ExecuteSql(sql,GetSqlParameter(t)) > 0;
        }


        //根据实体获取SqlParameter[]
        public static SqlParameter[] GetSqlParameter<T>(T t)
        {
            PropertyInfo[] property = t.GetType().GetProperties();

            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var item in property)
            {
                if (string.IsNullOrWhiteSpace(item.GetValue(t).ToString())) parameters.Add(new SqlParameter("@" + item.Name, DBNull.Value));
                else parameters.Add(new SqlParameter("@" + item.Name, item.GetValue(t)));
            }
            return parameters.ToArray();
        }
      
        //查询tDataSet
        public static DataSet GetDataSet(string sql, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    PrepareCommand(cmd, con, null, CommandType.Text, sql, sqlParameters);

                    using (SqlDataAdapter objDa = new SqlDataAdapter(cmd))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            objDa.Fill(ds);
                            cmd.Parameters.Clear();
                            return ds;
                        }
                    }
                }
            }
        }
        //查询table
        public static DataTable GetDataTable(string CommandText, params SqlParameter[] sqlParameters)
        {
            return GetDataSet(CommandText, sqlParameters).Tables[0];
        }
        //分页用
        public static DataTable GetDataTable(string sql, out int recordCount, params SqlParameter[] cmdParms)
        {
            recordCount = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter outputPara = new SqlParameter("@RecordCount", SqlDbType.Int) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(outputPara);
                PrepareCommand(cmd, con, null, CommandType.Text, sql, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        da.Fill(dt);
                        recordCount = outputPara.Value is DBNull ? 0 : Convert.ToInt32(outputPara.Value.ToString());
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException ex)
                    {
                        //log.WriteLog(ex.Message.ToString());
                    }
                    return dt;
                }
            }
        }


        /// 执行一条计算查询结果语句，返回查询结果（object）。
        public static object GetSingle(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, CommandType.Text, sql, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((object.Equals(obj, null)) || (object.Equals(obj, DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }



        /// 执行SQL语句，返回影响的记录数
        public static int ExecuteSql(string sql, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, CommandType.Text, sql, sqlParameters);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        public static void ExecuteSqlTran(string sql, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        PrepareCommand(cmd, conn, trans, CommandType.Text, sql, sqlParameters);
                        int val = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }


        /// 执行存储过程
        public static DataSet RunQueryProcedure(string storedProcName, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet);
                return dataSet;
            }
        }
        /// 执行存储过程，，返回影响的行数		
        public static int RunExecProcedure(string storedProcName, SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = BuildCommand(connection, storedProcName, parameters);
                return command.ExecuteNonQuery();
            }
        }

        private static SqlCommand BuildCommand(SqlConnection connection, string storedProcName, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    if (string.IsNullOrWhiteSpace(parameter.ToString())) parameter.Value = DBNull.Value;
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType commandType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open) conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null) cmd.Transaction = trans;
            cmd.CommandType = commandType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if (string.IsNullOrWhiteSpace(parameter.ToString())) parameter.Value = DBNull.Value;

                    cmd.Parameters.Add(parameter);
                }
            }
        }




        //查询单实体
        public static T QueryDomain<T>(string id) where T : new()
        {
            T model = new T();

            string columns = string.Join(",", model.GetType().GetProperties().Select(p => string.Format("[{0}]", p.Name)));

            string sql = $"SELECT {columns} FROM {model.GetType().Name} WHERE ID=@ID;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                cmd.Parameters.Add(new SqlParameter("@ID", id));

                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (reader.Read())
                    {
                        foreach (var prop in model.GetType().GetProperties())
                        {
                            prop.SetValue(model, reader[prop.Name]);
                        }
                    }
                }
            }
            return model;
        }






    }
}
