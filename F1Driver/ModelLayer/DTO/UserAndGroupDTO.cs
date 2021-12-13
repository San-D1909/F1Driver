using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class UserAndGroupDTO
    {
        public UserModel User { get; set; }
        public FriendGroupModel FriendGroup { get; set; }
        public string searchString { get; set; }
    }
}
