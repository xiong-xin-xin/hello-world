using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.Attributes
{
    ///<summary>
    /// 邮箱验证特性
    /// </summary>
    public class EmailAttribute : RegularExpressionAttribute
    {
        private const string RegexPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        public EmailAttribute() : base(RegexPattern)
        {
            ErrorMessage = "邮箱格式不正确";
        }
    }
}
