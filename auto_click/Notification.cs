using Notification.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace auto_click
{
    static class Notification
    {
        public static void Start()
        {
            var content = new NotificationContent
            {
                Title = "Auto_Clicker",
                Message = "Started!",
                Type = NotificationType.Success,
            };
            var notificationManager = new NotificationManager();
            notificationManager.Show(content);
        }
        public static void Stop()
        {
            var content = new NotificationContent
            {
                Title = "Auto_Clicker",
                Message = "Stopped!",
                Type = NotificationType.Error,
            };
            var notificationManager = new NotificationManager();
            notificationManager.Show(content);
        }
    }
}
