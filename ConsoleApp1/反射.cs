using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class 反射
    {
        public void 反射Demo()
        {
            Assembly assem = Assembly.Load("ConsoleApp1");//加载dll

            foreach (Module item in assem.GetModules())
            {
                Debug.WriteLine(item.FullyQualifiedName);
            }

            foreach (Type type in assem.GetTypes())
            {
                Debug.WriteLine(type.FullName);
            }

            Type demo = assem.GetType("ConsoleApp1.泛型约束");//基于类的完整名称 找出类型

            foreach (MethodInfo item in demo.GetMethods()) {}//找出类的方法

            object fanx = Activator.CreateInstance(demo);//根据类型创建对象

            //InvokeMember

            泛型约束 a = (泛型约束)fanx;
            Trace.WriteLine(a.GetInt<int>(1));

            MethodInfo methodInfo = demo.GetMethod("GetInt");//找到找个方法
            //执行方法  t用这种
            Debug.WriteLine(methodInfo.MakeGenericMethod(new Type[] { typeof(int) }).Invoke(fanx,new object[] { 1}));

            //执行普通方法并输出结果 重载需要new Type[] { typeof(int) }
            Trace.WriteLine(demo.GetMethod("show", new Type[] { typeof(int) }).Invoke(fanx, new object[] { 1111 }));
            //私有方法
            Trace.WriteLine(demo.GetMethod("showa", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Invoke(fanx, new object[] { 1 }));


            //取对象的值
            MyClass myClass = new MyClass() { Age = 10, Name = "xxx" };
            //字段
            foreach (var item in myClass.GetType().GetFields()) Trace.WriteLine(item.Name);
            //get set属性
            foreach (var item in myClass.GetType().GetProperties()) Trace.WriteLine(string.Format("属性名称为:{0}值是{1}",item.Name, item.GetValue(myClass)));

            //赋值
            Type ty = typeof(MyClass);
            object my = Activator.CreateInstance(myClass.GetType());
            foreach (var item in ty.GetProperties())
            {
                if (item.Name.Equals("Name"))
                {
                    item.SetValue(my, "xxx");
                }
                Trace.WriteLine(string.Format("属性名称为:{0}值是{1}", item.Name, item.GetValue(my)));
            }

        }


        public class MyClass
        {
            //析构函数
            ~MyClass()
            {

            }
            public int Age;
            public string Name { get; set; }
        }
        }
    }
