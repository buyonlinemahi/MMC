using MMCApp.Domain.Models.Notification;
using System.Collections.Generic;


namespace MMCApp.Domain.ViewModels.NotificationViewModel
{
    public class NotificationViewModel
    {
        public IEnumerable<Notification> NotificationDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
