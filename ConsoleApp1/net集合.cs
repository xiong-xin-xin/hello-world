using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class net集合
    {
        private static object o = new object();
        private static List<student> _students { get; set; }
        public class student
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        public void JIHE()
        {
            //ArrayList可以不用指定维数 可动态赋值  赋不同类型值
            ArrayList arrayList1 = new ArrayList();
            arrayList1.Add("a");
            arrayList1.Add(1);
            arrayList1.Add(new KeyValuePair<string, object>("key", "value"));

            //Array的容量是固定的 先指定大小 在赋值
            Array arrayList2 = Array.CreateInstance(typeof(string), 6);
            arrayList2.SetValue("a", 0);
            arrayList2.SetValue("b", 1);

            //list<T>集合
            List<student> persons = new List<student>();
            persons.Add(new student() { Name = "小王", ID = 1 });

            //IList  I开头的适接收数据，并不处理数据

            //ArrayList,List < T >：变长数组；
            //HashTable,Dictionary < T,T >：频繁根据key查找value；
            //HashSet < T >：集合运算；
            //Queue、Queue < T >：先进先出；
            //Stack、Stack < T >：堆栈，先进先出；
            //SortedList、SortedList < TKey,TValue >：哈希表，要通过下标，又要通过key取值时，可选用；
            //ListDictionary：单向链表，每次添加数据时都要遍历链表，数据量大时效率较低，数据量较大且插入频繁的情况下，不宜选用。
            //LinkedList < T >：双向链表；
            //HybridDictionary：未知数据量大小时，可用。
            //SortedDictionary < TKey,TValue >：SortedList<TKey, TValue> 的优化版，内部数组转平衡二叉树。
            //BitArray：二进制运算时可选用；


            //二进制位(0和1)的集合
            BitArray BA = new BitArray(3);
            BA[0] = true;
            BA[1] = false;


            #region 并发
            _students = new List<student>();

            Stopwatch swTask = new Stopwatch();//用于统计时间消耗的
            swTask.Start();
            //并发处理加lock锁
            //or
            //1.BlockingCollection 与经典的阻塞队列数据结构类似，能够适用于多个任务添加和删除数据，提供阻塞和限界能力。

            //2.ConcurrentBag 提供对象的线程安全的无序集合

            //3.ConcurrentDictionary  提供可有多个线程同时访问的键值对的线程安全集合

            //4.ConcurrentQueue   提供线程安全的先进先出集合

            //5.ConcurrentStack   提供线程安全的后进先出集合
            Task t1 = Task.Factory.StartNew(() => { AddProducts(); });

            Task t2 = Task.Run((Action)AddProducts);

            Task t3 = new TaskFactory().StartNew(AddProducts);

            Task.WaitAll(t1, t2, t3);

            swTask.Stop();

            Console.WriteLine(" 执行时间为：" + swTask.ElapsedMilliseconds);
            Console.WriteLine(_students.Count);
            #endregion



        }

        /*执行集合数据添加操作*/
        static void AddProducts()
        {
            Parallel.For(0, 1000, (i) =>
            {
                lock (o)
                {
                    _students.Add(new student() { Name = "小明" + i, ID = 1 + i });
                }
            });
        }


    }
}
