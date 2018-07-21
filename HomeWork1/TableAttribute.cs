using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1
{
    //别名
    [AttributeUsage(AttributeTargets.Property)]
    sealed class FieldNameAttribute : Attribute
    {
        public string FieldName { get; set; }

        public FieldNameAttribute(string fieldName)
        {
            this.FieldName = fieldName;
        }
    }
    
    
    //table别名
    [AttributeUsage(AttributeTargets.Class)]
    sealed class TableNameAttribute : Attribute
    {
        public string TableName { get; set; }

        public TableNameAttribute(string name)
        {
            this.TableName = name;
        }
    }



    //长度验证
    [AttributeUsage(AttributeTargets.Property)]
    sealed class FieldLengthAttribute : ValidationAttribute
    {
        public int FieldLength { get; set; }
        public FieldLengthAttribute(int length)
        {
            this.FieldLength = length;
        }
        public override bool IsValid(object value)
        {
            return value.ToString().Length < FieldLength;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!IsValid(value))
            {
                return new ValidationResult(validationContext.MemberName+"超出了长度",new string[] { value.ToString() });
            }
            return ValidationResult.Success;
        }

    }





}
