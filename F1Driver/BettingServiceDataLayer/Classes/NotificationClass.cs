using BettingServiceDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingServiceDataLayer.Classes
{
    public class NotificationClass : INotification
    {
        private readonly ApplicationDbContext _context;
        public NotificationClass(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteNotification(string ID)
        {
            try
            {
                NotificationModel notification = await _context.Notification.Where(n => n.ID == Convert.ToInt32(ID)).FirstOrDefaultAsync();
                _context.Notification.Remove(notification);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<NotificationModel>> GetNotification(string type, int userID)
        {
            NotificationTypeModel ntype = await _context.NotificationType.Where(n => n.type == type).FirstOrDefaultAsync();
            List<NotificationModel> notifications = await _context.Notification.Where(n => n.NotificationType == ntype.ID && n.User_Id == userID).ToListAsync();
            return notifications;
        }
    }
}
