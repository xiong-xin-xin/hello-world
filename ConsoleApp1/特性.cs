using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
  public  class 特性
    {
        public void aa()
        {
            Type typ = typeof(MyClass);
            var obj = typ.GetCustomAttributes().ToArray();
            MyTestAttribute my = obj[0] as MyTestAttribute;
            Console.WriteLine(my.Description);


            //Type type11 = MyClass.GetType();
            //if (type.IsDefined(typeof(MyTestAttribute), true))
            //{
            //    MyTestAttribute attribute = type.GetCustomAttributes(typeof(MyTestAttribute), true)[0] as MyTestAttribute;
            //    //attribute.Description（);
            //}
        }
        //特性取值
        //public static string Display(this Enum t)
        //{
        //    var t_type = t.GetType();
        //    var fieldName = Enum.GetName(t_type, t);
        //    var attributes = t_type.GetField(fieldName).GetCustomAttributes(false);
        //    var enumDisplayAttribute = attributes.FirstOrDefault(p => p.GetType().Equals(typeof(MyTestAttribute))) as MyTestAttribute;
        //    return enumDisplayAttribute == null ? fieldName : enumDisplayAttribute.Description;
        //}
    }
    
    [MyTest("简单的特性类", Version = "1.0")]
    public class MyClass
    {
        //析构函数
        ~MyClass()
        {

        }
        public int Age;
        public string Name { get; set; }

        [Obsolete("该方法以过时,使用xxx代替")]//表示方法被弃用
        //[Conditional("aaa")]//可以忽略此方法调用，如需调用头部需加 #define aaa
        [DebuggerStepThrough]//调试时候f11不进入该方法
        public void OldMethod()
        {
            Console.WriteLine("该方法以过时");
        }


        public void PrintOut(string str, [CallerFilePath] string filename = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string callmemberName = "")
        {
            Console.WriteLine($"路径{filename}\n行号 {lineNumber} \n方法名 {callmemberName}");
            Console.WriteLine(str);
        }
    }

    /// <summary>
    /// 自定义特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    sealed class MyTestAttribute : Attribute
    {
        public string Description { get; set; }
        public string Version { get; set; }

        public MyTestAttribute(string desc)
        {
            this.Description = desc;
        }

    }
}
