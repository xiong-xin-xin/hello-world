using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class OtherConfigInfo : ConfigurationSection
    {
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        public static OtherConfigInfo GetConfig()
        {
            return GetConfig("otherconfig");
        }
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="sectionName">xml节点名称</param>
        /// <returns></returns>
        public static OtherConfigInfo GetConfig(string sectionName)
        {
            OtherConfigInfo section = (OtherConfigInfo)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }

        [ConfigurationProperty("a", IsRequired = false)]
        public string a
        {
            get
            {
                return (string)base["a"];
            }
            set
            {
                base["a"] = value;
            }
        }

        [ConfigurationProperty("b", IsRequired = false)]
        public string b
        {
            get
            {
                return (string)base["b"];
            }
            set
            {
                base["b"] = value;
            }
        }

        [ConfigurationProperty("c", IsRequired = false)]
        public string c
        {
            get
            {
                return (string)base["c"];
            }
            set
            {
                base["c"] = value;
            }
        }
    }
}
