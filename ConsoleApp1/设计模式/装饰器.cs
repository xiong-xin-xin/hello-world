using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.设计模式
{
    public abstract class Base
    {
        public abstract void Show();

    }


    public class Realization : Base
    {
        private Base _base = null;
        public Realization(Base @base)
        {
            this._base = @base;
        }

        public override void Show()
        {
            this._base.Show();
            Console.WriteLine("技能1：猛龙摆尾");
        }
    }


    public class Realization1 : Realization
    {
        private Base _base = null;
        public Realization1(Base @base):base(@base)
        {
            this._base = @base;
        }
        public override void Show()
        {
            this._base.Show();
            Console.WriteLine("技能2：天雷破/摧筋断骨");
        }
    }


    public class AA : Base
    {
        //继承
        public override void Show()
        {
            Console.WriteLine("初始化：李青");
        }
    }


    public class aaaaaa
    {
        //static void Main(string[] args)
        //{
        //    Base @base = new AA();
        //    @base = new Realization(@base);
        //    @base = new Realization1(@base);
        //    @base.Show();


        //    Console.WriteLine(0>0);
        //    Console.Read();

        //}

    }

}
