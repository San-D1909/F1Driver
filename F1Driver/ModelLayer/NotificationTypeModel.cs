using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class NotificationTypeModel
    {
        [Key]
        public int ID { get; set; }
        public string type { get; set; }
    }
}
