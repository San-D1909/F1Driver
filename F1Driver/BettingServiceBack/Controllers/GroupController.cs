using BettingServiceDataLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLayer.DTO;

namespace BettingServiceDataLayer.Controllers
{
    [Route("[controller]/[action]")]
    public class GroupController : Controller
    {
        private readonly IGroup FriendGroup;
        public GroupController(IGroup friendGroup)
        {
            FriendGroup = friendGroup;
        }
        [HttpPost("CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] UserAndGroupDTO userAndGroupDTO)
        {
            userAndGroupDTO.FriendGroup = await FriendGroup.CreateFriendGroup(userAndGroupDTO.FriendGroup);
            bool added = await FriendGroup.AddToGroup(userAndGroupDTO);
            return Ok();
        }
        [HttpPost("JoinGroup")]
        public async Task<IActionResult> JoinGroup([FromBody] UserAndGroupDTO userAndGroupDTO)
        {
            bool worked = await FriendGroup.JoinGroup(userAndGroupDTO);
            return Ok(worked);
        }
        [HttpPost("InviteToGroup")]
        public async Task<IActionResult> InviteToGroup([FromBody] UserAndGroupDTO userAndGroupDTO)
        {
            bool worked = await FriendGroup.SendGroupInvite(userAndGroupDTO);
            return Ok(worked);
        }
        [HttpPost("GetGroupDetails")]
        public async Task<IActionResult> GetGroupDetails([FromBody] UserModel user)
        {
            return null;
        }
    }
}
