using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class 委托
    {
        public Func<int,int> a = null;

        public int money { get; set; } = 0;
        public void 练习()
        {
            money = 100;
 
            //委托别人卖东西，赋值委托
            a =(t)=> { t -= 10;money = t;return 10; };

            //Invoke()委托调用
            var b = a?.Invoke(100);

            Console.WriteLine(money);

            FunTest(4, stu => { return stu.ID; });
        }


       
        public int FunTest(int id, Func<Student, int> func)
        {
            Student student = new Student();
            student.Name = "小明";
            student.ID = 1 + id;
           return func(student);
        }


        

    }


    public class Student
    {
        public int ID { get; set; }
        public string  Name { get; set; }
    }
}
