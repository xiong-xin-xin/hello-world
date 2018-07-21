using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public static class XmlHelper
    {
        /// <summary>
        /// xml序列化
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">值</param>
        /// <returns></returns>
        public static string XmlSerialize<T>(this T obj)
        {
            if (obj == null) throw new NullReferenceException("空");

            var settings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Encoding = Encoding.Default
            };
            var output = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(output, settings))
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add(string.Empty, string.Empty);

                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(writer, obj, ns);
            }
            return output.ToString();
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="strXML">值</param>
        /// <returns></returns>
        public static T DeserializeXml<T>(this string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static void CheckIsNull<T>(T t)
        {
            // 获取当前类中的公共字段
            var fields = t.GetType().GetProperties();

            // 查找有DbKey特性的字段
            var keyFields = fields.Where(field => Attribute.GetCustomAttribute(field, typeof(NotNullAttribute)) != null);


            //var pvalue = p.GetValue(t);
            //if (at != null && string.IsNullOrEmpty(pvalue == null ? "" : pvalue.ToString()))
            //{
            //    throw new Exception(string.Format("{0}不能为空", p.Name));
            //}
        }

    }
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullAttribute : Attribute
    {


        /// <summary>
        /// 接口参数为空或空字符串都返回false
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            return !string.IsNullOrEmpty("");
        }
    }
}
