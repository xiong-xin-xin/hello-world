using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutofacDemo.JuCheap;

namespace AutofacDemo
{
    [DependencyRegister]
    class Dog: IAnimal
    {
        string _name;
        public Dog(string name)
        {
            _name = name;
        }


        public void Call()
        {
            Console.WriteLine(_name + "狗再叫");

        }

        public void Run()
        {
            Console.WriteLine(_name + "狗再跑");

        }

    }
}
