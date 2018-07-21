using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HomeWork1.Model
{
    public class Request
    {
        public string System { get; set; }
        [Required]
        public string SecurityCode { get; set; }
        [Required]
        public List<PatientBasicInfo> PatientInfos { get; set; }
    }


    public class PatientBasicInfo
    {
        public string PatientNo { get; set; }
        public string PatientName { get; set; }
        public string Phoneticize { get; set; }
        public string Sex { get; set; }

        [Required]
        public string Birth { get; set; }

        /// <summary>
        /// 加<![CDATA[ ]]>数据字段
        /// </summary>
        [XmlIgnore] //方式1，这里属性设置忽略
        public string HouseDetail { get; set; }

        [XmlElement("Housedetail")]
        public XmlNode[] CDataContent
        {
            get
            {
                if (HouseDetail != null)
                {
                    return new XmlNode[] { new XmlDocument().CreateCDataSection(HouseDetail) };
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HouseDetail = value.First().Value;
            }
        }

    }
}
