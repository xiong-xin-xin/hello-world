using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
  public  class Lambda
    {
        public static void Lambda_xin()
        {
            List<XinEntity> xe = new List<XinEntity>() {
                new XinEntity() { ID = 1, Name = "001", SexID = 1 },
                new XinEntity() { ID = 2, Name = "001", SexID = 1 },
                new XinEntity() { ID = 3, Name = "003", SexID = 2 },
                new XinEntity() { ID = 4, Name = "004", SexID = 3 }
            };
            List<Sex> sex = new List<Sex>() {
                new Sex() { ID = 1, SexName = "男" },
                new Sex() { ID = 2, SexName = "女" }
            };
            List<Money> money = new List<Money>() {
                new Money(1,1,50), new Money(1,1,20),new Money(1,1,-5),
                 new Money(1,2,50), new Money(1,2,200),new Money(1,2,-50),
                 new Money(1,3,50), new Money(1,3,100),new Money(1,3,-50)
            };
            //join查询
            var list = xe.Join(sex, x => x.SexID, y => y.ID, (x, y) => new { ID = x.ID, Name = x.Name, SexName = y.SexName });
             //var list1 = xe.SelectMany(s => sex, (s, x) => new {a=x,b=s}).Where(x=>x.a.ID==x.b.SexID);

            //分组查询
            var money1 = money.GroupBy(s => s.XinEntityID).Select(s => new { ID = s.Key, Count = s.Count(), Money = s.Sum(item => item.Moneys) });

            var list1 = list.Join(money1, x => x.ID, y => y.ID, (x, y) => new { ID = x.ID, Name = x.Name, SexName = x.SexName, Money = y.Money }).OrderByDescending(x => x.Money).ThenBy(s => s.Name);

            //判断是否满足条件
            bool a = list1.Any(s => s.ID == 1);
            bool a1 = list1.All(s => s.ID == 1);
            foreach (var item in list1)
                Console.WriteLine(list1.Count());
            Console.Read();
        }
    }
    public class XinEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SexID { get; set; }

    }
    public class Sex
    {
        public int ID { get; set; }
        public string SexName { get; set; }
    }

    public class Money
    {
        public Money(int id, int xinEntityID, int moneys)
        {
            ID = id;
            XinEntityID = xinEntityID;
            Moneys = moneys;
        }
        public int ID { get; set; }
        public int XinEntityID { get; set; }
        public int Moneys { get; set; }


    }




}
