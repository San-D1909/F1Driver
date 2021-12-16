using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class GroupDetailDTO
    {
        public List<UserModel> Users { get; set; }
        public FriendGroupModel FriendGroup { get; set; }
    }
}
