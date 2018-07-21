using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutofacDemo
{
    class Cat : IAnimal
    {
        string _name;
        public Cat(string name)
        {
            _name = name;
        }


        public void Call()
        {
            Console.WriteLine(_name + "猫再叫");
          
        }

        public void Run()
        {
            Console.WriteLine(_name + "猫再跑");
        
        }
    }
}
