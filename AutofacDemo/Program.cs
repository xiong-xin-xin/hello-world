using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using AutofacDemo.JuCheap;
using Microsoft.Extensions.Configuration;

namespace AutofacDemo
{
    class Program
    {

      
        static void Main(string[] args)
        {
            //配置文件
            IAnimal cat = Init.GetService<IAnimal>("Dog", new NamedParameter("name", "小小白"));
            cat.Call();

            //普通方式
            // var builder = new ContainerBuilder();
            /// builder.RegisterType<Dog>().AsSelf().As<IAnimal>();
            //表示注册的类型，以接口的方式注册
            // IContainer container;
            // var builder = new ContainerBuilder();
            //builder.RegisterType<Dog>().AsSelf().AsImplementedInterfaces();

            //寻找只带特性的注入
            //builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            //                        .Where(t => t.GetCustomAttribute<DependencyRegisterAttribute>() != null)
            //                        .AsImplementedInterfaces();
            //container = builder.Build();
            //从容器中取出一个T类型的实例
            //IAnimal a = container.Resolve<IAnimal>(new NamedParameter("name","否"));
            //a.Call();


            Console.Read();
        }
    }


    public class Init
    {
        private  IContainer Container { get; set; }
        public Init()
        {
            var config = new ConfigurationBuilder();

            config.AddXmlFile("ioc.xml");

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            Container = builder.Build();
        }
        private static readonly Init instance = new Init();

        public static T GetService<T>(string name, params Parameter[] parameter)
        {
            using (var scope = instance.Container.BeginLifetimeScope())
            {
                return scope.ResolveNamed<T>(name,parameter);
            }
        }


    }
}
