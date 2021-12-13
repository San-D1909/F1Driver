using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingServiceDataLayer.Interfaces
{
    public interface INotification
    {
        public Task<List<NotificationModel>> GetNotification(string type, int userID);
    }
}
