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
    public class GroupController : Controller
    {
        private readonly IGroup FriendGroup;
        public GroupController(IGroup friendGroup)
        {
            FriendGroup = friendGroup;
        }
        [HttpGet("CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] UserAndGroupDTO userAndGroupDTO)
        {
            bool worked = await FriendGroup.CreateFriendGroup(userAndGroupDTO.FriendGroup);
            bool added = await FriendGroup.AddToGroup(userAndGroupDTO);
            return Ok();
        }
        [HttpGet("InviteToGroup")]
        public async Task<IActionResult> InviteToGroup()
        {
            return null;
        }
    }
}
