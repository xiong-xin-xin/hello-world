using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.配置文件
{
    class Main_配置
    {
        static void Main_(string[] args)
        {

            //读
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var mySection = config.GetSection("mySection") as MySection;
            foreach (MySection.MyKeyValueSetting add in mySection.KeyValues)
            {
                Console.WriteLine(string.Format("{0}-{1}", add.Key, add.Value));
            }

            //写
            mySection.KeyValues.Clear();
            mySection.KeyValues.Add(new MySection.MyKeyValueSetting() { Key = "aaaaaa", Value = "ddddddd" });

            config.Save();
            ConfigurationManager.RefreshSection("mySection");  //刷新
        }

    }
}
