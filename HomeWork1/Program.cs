using HomeWork1.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1
{
    class Program
    {

        static void Main(string[] args)
        {

            //查询单值
            //UserModel user = DBHelper<UserModel>.Instance().QueryDomain(5);
            //if (user != null)
            //{
            //    foreach (var item in user.GetType().GetProperties())
            //    {
            //        if (item.IsDefined(typeof(DisplayNameAttribute), true))
            //        {
            //            DisplayNameAttribute attribute = item.GetCustomAttributes(typeof(DisplayNameAttribute), true)[0] as DisplayNameAttribute;
            //            Console.WriteLine(attribute.DisplayName + ":  " + item.GetValue(user));
            //        }
            //        else
            //        {
            //            Console.WriteLine(item.Name + ":  " + item.GetValue(user));
            //        }
            //    }
            //}


            ////查询所有
            //var list = DBHelper<UserModel>.Instance().QueryDomainList();

            ////删除
            //DBHelper<UserModel>.Instance().DelDomain(2);



            ////新增

            //UserModel userModel = new UserModel() { Name = "aaaaaaaaaaaaaaaaaaaaaaa", Account = "aaa", Password = "1234", Email = "123456@qq.com" };
            ////错误信息
            //var results = new List<ValidationResult>();
            ////是否通过验证
            //var isValid = Validator.TryValidateObject(userModel, new ValidationContext(userModel), results, true);


            //DBHelper<UserModel>.Instance().AddDomain(userModel);

            ////修改
            ////。。。。

            //Console.Read();
            //var a = DBHelper<UserModel>.Instance().QueryDomainList();


            //UserModel userModel = new UserModel() { Email = "111" };

            //var results = new List<ValidationResult>();
            //var isValid = Validator.TryValidateObject(userModel, new ValidationContext(userModel), results, true);


            Request request = new Request();
            request.System = "1111";

            //request.PatientInfos = new List<PatientBasicInfo>();
            //request.PatientInfos.Add(new PatientBasicInfo()
            //{
            //    Sex = "1"

            //});
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(request, new ValidationContext(request), results, true);




        }



    }
}
