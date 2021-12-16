using BettingServiceDataLayer.Interfaces;
using BettingServiceDataLayer.Classes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLayer;

namespace BettingServiceBack.Controllers
{
    [Route("[controller]/[action]")]
    public class NotificationController : Controller
    {
        private readonly INotification Notification;
        public NotificationController(INotification notification)
        {
            Notification = notification;
        }
        [HttpPost("GetNotifications")]
        public async Task<IActionResult> GetNotifications([FromQuery] string notificationType,[FromQuery] string userID)
        {
            List<NotificationModel> notifications = await Notification.GetNotification(notificationType, Convert.ToInt32(userID));
            return Ok(notifications);
        }
        [HttpPost("DeleteNotification")]
        public async Task<IActionResult> DeleteNotification([FromQuery] string notificationID)
        {
            return Ok(await Notification.DeleteNotification(notificationID));
        }
    }
}
