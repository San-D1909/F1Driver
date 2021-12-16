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
            userAndGroupDTO.User.FriendGroup = userAndGroupDTO.FriendGroup.ID;
            UserModel userModel = await _context.User.Where(x => x.ID == userAndGroupDTO.User.ID).FirstOrDefaultAsync();
            _context.User.Update(userModel).CurrentValues.SetValues(userAndGroupDTO.User);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<FriendGroupModel> CreateFriendGroup(FriendGroupModel friendGroupModel)
        {
            _context.FriendGroup.Add(friendGroupModel);
            await _context.SaveChangesAsync();
            return friendGroupModel;
        }

        public async Task<GroupDetailDTO> GetGroupDetails(int groupID)
        {
            GroupDetailDTO group = new GroupDetailDTO { FriendGroup = await _context.FriendGroup.Where(g => g.ID == groupID).FirstOrDefaultAsync(), Users = await _context.User.Where(u => u.FriendGroup == groupID).ToListAsync()};
            foreach(UserModel user in group.Users)
            {
                if (user.BettingScore == null)
                {
                    user.BettingScore = 0;
                }
            }    
            return group;
        }

        public async Task<bool> JoinGroup(UserAndGroupDTO userAndGroupDTO)
        {
            try
            {
                UserModel user = await _context.User.Where(user => user.Email == userAndGroupDTO.User.Email).FirstOrDefaultAsync();
                if (user == null)
                {
                    return false;
                }
                UserModel newUser = user;
                newUser.FriendGroup = userAndGroupDTO.groupID;
                _context.User.Update(user).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;

            }
        }

        public async Task<bool> LeaveGroup(int userID)
        {
            try
            {
                UserModel user = await _context.User.Where(u => u.ID == userID).FirstOrDefaultAsync();
                user.FriendGroup = null;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;

            }
        }

        public async Task<bool> SendGroupInvite(UserAndGroupDTO userAndGroupDTO)
        {
            try
            {
                UserModel user = await _context.User.Where(user => user.Email == userAndGroupDTO.searchString).FirstOrDefaultAsync();
                FriendGroupModel group = await _context.FriendGroup.Where(g => g.ID == userAndGroupDTO.User.FriendGroup).FirstOrDefaultAsync();
                NotificationModel notification = new NotificationModel {Notification= userAndGroupDTO.User.UserName+" has invited you to the group "+group.GroupName+"!", NotificationType=1,User_Id = user.ID,FriendGroup = userAndGroupDTO.User.FriendGroup};
                _context.Notification.Add(notification);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;

            }
        }
    }
}
