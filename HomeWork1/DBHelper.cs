using HomeWork1.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1
{
    public class DBHelper<T> where T : new()
    {
        private static string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={Environment.CurrentDirectory}\App_Data\HomeWork.mdf;Integrated Security=True";

        private static DBHelper<T> _instance;
        private static readonly object SyncObject = new object();

        private DBHelper() { }

        public static DBHelper<T> Instance()
        {
            if ( _instance == null)
            {
                lock (SyncObject)
                {
                    if (_instance == null)
                    {
                        _instance = new DBHelper<T>();
                    }
                }
            }
            return _instance;
        }



        //查询单实体
        public  T QueryDomain(int id)
        {
            string sql = $"SELECT {string.Join(",", Columns())} FROM {TableName()} WHERE ID=@ID;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();

                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        var list = GetEntity(reader);
                        if (list.Count()==0)
                        {
                            return default(T);
                        }
                        return list.First();
                    }
                }
            }
        }

        //查询list
        public List<T> QueryDomainList()
        {
            string sql = $"SELECT {string.Join(",", Columns())} FROM {TableName()} ;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        return GetEntity(reader);
                    }
                }
            }
        }

        //删除
        public  void DelDomain(int id)
        {
            string sql = $"delete from {TableName()} where Id = 3";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        if (connection.State != ConnectionState.Open) connection.Open();
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }

        }

        //新增
        public  void AddDomain(T t)
        {
            object[] obj = Columns().Where(i => i.ToString().ToUpper() != "ID").ToArray();

            string sql = $"INSERT INTO {TableName()} ({string.Join(",",obj.Select(i => string.Format("[{0}]", i))) }) VALUES ({string.Join(",", obj.Select(i => string.Format("@{0}", i))) })";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        if (connection.State != ConnectionState.Open) connection.Open();
                        cmd.Parameters.AddRange(GetSqlParameter(t));
                        int rows = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }

        }

        //修改
        public void EditDomain(T t)
        {
            object[] obj = Columns().Where(i => i.ToString().ToUpper() != "ID").ToArray();

            string sql = $"update a set {string.Join(",", obj.Select(i => string.Format("[{0}]=@{0}", i))) } from {TableName()} where ID=@ID)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        if (connection.State != ConnectionState.Open) connection.Open();
                        cmd.Parameters.AddRange(GetSqlParameter(t));
                        int rows = cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }


        #region 辅助方法
        //获取实体的SqlParameter
        public  SqlParameter[] GetSqlParameter(T t)
        {
            PropertyInfo[] property = t.GetType().GetProperties();

            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var item in property)
            {
                if (item.IsDefined(typeof(FieldNameAttribute), true))
                {
                    FieldNameAttribute attribute = item.GetCustomAttributes(typeof(FieldNameAttribute), true)[0] as FieldNameAttribute;
                    if (string.IsNullOrEmpty(item.GetValue(t)?.ToString())) parameters.Add(new SqlParameter("@" + attribute.FieldName, DBNull.Value));
                    else parameters.Add(new SqlParameter("@" + attribute.FieldName, item.GetValue(t)));
                }
                else
                {
                    if (string.IsNullOrEmpty(item.GetValue(t)?.ToString())) parameters.Add(new SqlParameter("@" + item.Name, DBNull.Value));
                    else parameters.Add(new SqlParameter("@" + item.Name, item.GetValue(t)));
                }
            }
            return parameters.ToArray();
        }

        //获取TableName
        public  string TableName()
        {
            T model = new T();
            string tableName = model.GetType().Name;
            if (model.GetType().IsDefined(typeof(TableNameAttribute), true))
            {
                TableNameAttribute attribute = model.GetType().GetCustomAttributes(typeof(TableNameAttribute), true)[0] as TableNameAttribute;
                tableName = attribute.TableName;
            }
            return tableName;
        }

        //获取查询的列名
        public  object[] Columns()
        {
            T model = new T();
            ArrayList columnList = new ArrayList();
            //处理查询的列名
            foreach (PropertyInfo item in model.GetType().GetProperties())
            {
                //判断是否有别名
                if (item.IsDefined(typeof(FieldNameAttribute), true))
                {
                    FieldNameAttribute attribute = item.GetCustomAttributes(typeof(FieldNameAttribute), true)[0] as FieldNameAttribute;
                    columnList.Add(attribute.FieldName);
                }
                else
                {
                    columnList.Add(item.Name);
                }
                //。。。可添加另外的
            }
            return columnList.ToArray();
        }

        //获取单实体
        public  List<T> GetEntity(SqlDataReader dr)
        {
            List<T> list = new List<T>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T destObj = new T();
                    foreach (PropertyInfo prop in typeof(T).GetProperties())
                    {
                        try
                        {
                            if (prop.IsDefined(typeof(FieldNameAttribute), true))
                            {
                                FieldNameAttribute attribute = prop.GetCustomAttributes(typeof(FieldNameAttribute), true)[0] as FieldNameAttribute;

                                prop.SetValue(destObj, ChangeType(prop.PropertyType, dr[attribute.FieldName]));
                            }
                            else
                            {
                                prop.SetValue(destObj, ChangeType(prop.PropertyType, dr[prop.Name]));
                            }
                        }
                        catch { }
                    }
                    list.Add(destObj);
                }
            }
            return list;
        }

        //返回值为DB的默认值  
        private  object ChangeType(Type type, object value)
        {
            if (value == null)
            {
                return null;
            }
            //类型一样直接返回
            if (type == value.GetType())
            {
                return value;
            }
            //枚举
            if (type.IsEnum)
            {
                if (value is string)
                {
                    return Enum.Parse(type, value as string);
                }
                return Enum.ToObject(type, value);
            }
            //bool
            if (type == typeof(bool) && typeof(int).IsInstanceOfType(value))
            {
                int temp = int.Parse(value.ToString());
                return temp != 0;
            }
            //guid
            if ((value is string) && (type == typeof(Guid)))
            {
                return new Guid(value as string);
            }
            return Convert.ChangeType(value, type);
        } 
        #endregion
    }


}

