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
            return Ok(await FriendGroup.JoinGroup(userAndGroupDTO));
        }
        [HttpPost("LeaveGroup")]
        public async Task<IActionResult> LeaveGroup([FromQuery] string userID)
        {
            return Ok(await FriendGroup.LeaveGroup(Convert.ToInt32(userID)));
        }
        [HttpPost("InviteToGroup")]
        public async Task<IActionResult> InviteToGroup([FromBody] UserAndGroupDTO userAndGroupDTO)
        {
            bool worked = await FriendGroup.SendGroupInvite(userAndGroupDTO);
            return Ok(worked);
        }
        [HttpPost("GetGroupDetails")]
        public async Task<IActionResult> GetGroupDetails([FromQuery] string groupID)
        {
            return Ok(await FriendGroup.GetGroupDetails(Convert.ToInt32(groupID)));
        }
    }
}
