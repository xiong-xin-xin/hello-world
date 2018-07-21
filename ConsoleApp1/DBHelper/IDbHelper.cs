using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.DBHelper
{
 public   interface IDbHelper
    {
        Task<bool> ExecuteSqlAsync(string sql,object val);

        bool ExecuteSql(string sql, object val);

        Dictionary<string,object> GetSingle(string sql, object val);
        DataSet GetDataTable(string sql, object val);

    }
}
