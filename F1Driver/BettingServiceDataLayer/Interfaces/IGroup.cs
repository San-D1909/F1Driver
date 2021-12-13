using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using ModelLayer.DTO;
using System.Threading.Tasks;

namespace BettingServiceDataLayer.Interfaces
{
    public interface IGroup
    {
        public Task<FriendGroupModel> CreateFriendGroup(FriendGroupModel friendGroupModel);
        public Task<bool> AddToGroup(UserAndGroupDTO userAndGroupDTO);
        public Task<bool> JoinGroup(UserAndGroupDTO userAndGroupDTO);
        public Task<bool> SendGroupInvite(UserAndGroupDTO userAndGroupDTO);
    }
}
