using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWork1.Attributes;

namespace HomeWork1.Model
{

    [TableName("[User]")]
    public class UserModel : BaseModel
    {

        [Required, DisplayName("名字"), FieldLength(10)]
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        [Chinese]
        public string Email { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        [FieldName("State")]
        public int Status { get; set; }

        public int UserType { get; set; }




    }
}
