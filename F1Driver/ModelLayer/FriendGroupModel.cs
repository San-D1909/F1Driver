using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class FriendGroupModel
    {
        [Key]
        public int ID { get; set; }
        public int TotalScore { get; set; }
        public string GroupName { get; set; }

    }
}
