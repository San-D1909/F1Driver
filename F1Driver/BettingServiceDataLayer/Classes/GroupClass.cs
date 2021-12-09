using BettingServiceDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingServiceDataLayer.Classes
{
    public class GroupClass : IGroup
    {
        private readonly ApplicationDbContext _context;
        public GroupClass(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToGroup(UserAndGroupDTO userAndGroupDTO)
        {
            UserModel userModel = await _context.User.Where(x => x.ID == userAndGroupDTO.User.ID).FirstOrDefaultAsync();
            _context.User.Update(userModel).CurrentValues.SetValues(userAndGroupDTO.User);
            return true;
        }

        public async Task<bool> CreateFriendGroup(FriendGroupModel friendGroupModel)
        {
            if (_context.FriendGroup.Add(friendGroupModel).IsKeySet)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<bool> SendGroupInvite(UserAndGroupDTO userAndGroupDTO)
        {
            throw new NotImplementedException();
        }
    }
}
