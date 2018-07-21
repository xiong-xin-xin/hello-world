using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.Attributes
{
    ///<summary>
    /// 中文
    /// </summary>
    public class ChineseAttribute : RegularExpressionAttribute
    {
        private const string RegexPattern = @"^[\u4e00-\u9fa5]*$";
        public ChineseAttribute() : base(RegexPattern)
        {
            ErrorMessage = "请输入中文";
        }
    }
}
