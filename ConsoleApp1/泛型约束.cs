using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class 泛型约束
    {

        public  T SayHi<T>(T t) where T:class,new()
        {
            //class引用类型
            //new() 可以new
            T t1 = new T();
            return default(T);
        }

        public  T GetInt<T>(T t) where T :struct
        {
            //值类型
            return t;
        }


        public void show(int a)
        {
            Debug.WriteLine(a);
        }

        private void showa(int a)
        {
            Debug.WriteLine(a);
        }

    }
}
