using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class NotificationModel
    {
        [Key]
        public int ID { get; set; }
        public int User_Id { get; set; }
        public int NotificationType { get; set; }
        public string Notification { get; set; }
        public int? FriendGroup { get; set; }
    }
}
