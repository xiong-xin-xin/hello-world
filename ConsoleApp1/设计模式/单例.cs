using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.设计模式
{
    //单例模式
    public class Singleton
    {
        private static Singleton _instance;
        private static readonly object SyncObject = new object();

        private Singleton() { }


        static Singleton()
        {
            //这里只会进一次
            //_instance = new Singleton();
        }

        public static Singleton Instance()
        {
            //原型模式  可用序列化 类上加Serializable
            //Singleton singleton = (Singleton)_instance.MemberwiseClone();//浅克隆一个对象
            //singleton.MyProperty = new a { };//深克隆
            // return singleton;


            if (_instance == null)
            {
                lock (SyncObject)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                }
            }
            return _instance;
        }

        public int ID { get; set; }
        public a MyProperty { get; set; }

    }

    public class a {


    }
}
