using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class DataTable练习
    {
        public void DataTable_xin()
        {
            DataTable dt = new DataTable("MyDatatable");
            //AutoIncrement设置是否为自增列,AutoIncrementSeed设置自增初始值,AutoIncrementStep设置每次子增值
            DataColumn column1 = new DataColumn("User_ID", Type.GetType("System.Int32")) { AutoIncrement = true, AutoIncrementSeed = 1, AutoIncrementStep = 1 };
            dt.Columns.Add(column1);
            dt.Columns.Add(new DataColumn("User_Name", Type.GetType("System.String")) { DefaultValue = "凉生" });
            dt.Rows.Add(new object[] { null, 1233 });
            dt.Rows.Add(new object[] { null, 3333 });
            dt.Rows.Add(new object[] { null, 3333 });
            dt.Rows.Add(new object[] { });

            //输出列名
            dt.Columns.Cast<DataColumn>().ToList().ForEach(x => Console.Write(x + "\t"));
            Console.WriteLine();
            //输出每列 OFType<int>
            dt.Rows.Cast<DataRow>().Select(x => new { ID = x["User_ID"], Name = x["User_Name"] }).ToList().ForEach(x => Console.WriteLine(x));

            var rowss = dt.AsEnumerable().Select(e => e["User_ID"]).ToList();
            var rowsss = dt.Rows.Cast<DataRow>().Select(e => e.Field<int>("User_ID")).ToList();
            foreach (var row in dt.AsEnumerable())//修改值噢
            {
                row.SetField<int>("User_ID", row.Field<int>("User_ID") + 1);
            }
           
            DataTable dt1 = dt.Copy();
            dt.Rows.Add(new object[] { null, 4444 });
            dt1.Rows.Add(new object[] { null, 5555 });
            //AsEnumerable()将datatable转换成IEnumerable<DataRow>的一个序列
            IEnumerable<DataRow> rows = dt.AsEnumerable();
            IEnumerable<DataRow> rows1 = dt1.AsEnumerable();
            //去重复
            IEnumerable<DataRow> r = rows.Distinct(DataRowComparer.Default);
            //获取rows1跟rows对比没有的序列
            var r1 = rows.Except(rows1, DataRowComparer.Default);
            //获取共有的序列
            var r2 = rows.Intersect(rows1, DataRowComparer.Default);
            //合并两个序列
            var r3 = rows.Union(rows1, DataRowComparer.Default);
            //判断是否相等
            var equal = rows.SequenceEqual(rows1, DataRowComparer.Default);

            foreach (var item in r3)
                Console.WriteLine(item["User_Name"].ToString());




            string strConn = "...";//连接字符串
            string strSql = "select * from MytableName1；" + "select * from MytableName2";
            SqlDataAdapter da = new SqlDataAdapter(strSql, strConn);
            da.TableMappings.Add("Table1", "MyTableName2");

            DataTableMapping tableMap;
            tableMap = da.TableMappings.Add("Table", "MyTableName");
            tableMap.ColumnMappings.Add("EmpID", "MyEmpID");
            tableMap.ColumnMappings.Add("EmpName", "MyEmpName");

            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataTable table in ds.Tables)
            {
                Console.WriteLine("{0}", table.TableName);
            }
            Console.Read();
        }
    }
}
