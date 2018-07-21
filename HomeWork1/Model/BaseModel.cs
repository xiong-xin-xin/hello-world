using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork1.Model
{
    public class BaseModel
    {
        public int ID { get; set; }

        public int LastModifierId { get; set; } = 1;

        public DateTime LastLoginTime { get; set; } = DateTime.Now;

        public int CreatorId { get; set; } = 1;

        public DateTime ?CreateTime { get; set; } = DateTime.Now;
    }
}
