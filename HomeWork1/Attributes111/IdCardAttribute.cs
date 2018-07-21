using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.Attributes
{
    public class IdCardAttribute : RegularExpressionAttribute
    {
        private const string RegexPattern = @"^[1-9]\d{16}[\dXx]$";
        public IdCardAttribute() : base(RegexPattern)
        {
            ErrorMessage = "身份证格式不正确";
        }
    }
}
