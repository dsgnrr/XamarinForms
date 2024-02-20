using System;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Interfaces
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;
        void Initialize();
        void SendNotification(string title, string message, DateTime?notifyTime=null);
        void RecieveNotification(string title, string message);
    }
}
