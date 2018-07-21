using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.Attributes
{
    ///<summary>
    /// 手机号码
    /// </summary>
    public class PhoneNumAttribute : RegularExpressionAttribute
    {
        private const string RegexPattern = @"^1[0-9]{10}$";
        public PhoneNumAttribute() : base(RegexPattern)
        {
            ErrorMessage = "手机号码不正确";
        }
    }
}
