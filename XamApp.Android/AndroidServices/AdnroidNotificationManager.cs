using System;
using Xamarin.Forms;
using XamApp.Interfaces;
using Android.App;
using AndroidApp = Android.App.Application;
using Android.OS;
using XamApp.Classes;
using Android.Content;
using Android.Graphics;
using AndroidX.Core.App;
using Android.Widget;
using Android.Media;

[assembly: Dependency(typeof(XamApp.Droid.AndroidServices.AdnroidNotificationManager))]
namespace XamApp.Droid.AndroidServices
{
    public class AdnroidNotificationManager : INotificationManager
    {
        const string channelId = "default";
        const string channelName = "Default";
        const string channelDescription = "The default channel for notifications.";

        public const string TitleKey = "title";
        public const string MessageKey = "message";

        bool channelInitialized = false;
        int messageId = 0;

        NotificationManager manager;

        public event EventHandler NotificationReceived;

        public static AdnroidNotificationManager Instance { get; private set; }

        public AdnroidNotificationManager() => Initialize();
        public void Initialize()
        {
            if (Instance == null)
            {
                CreateNotificationChannel();
                Instance = this;
            }
        }

        public void RecieveNotification(string title, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
            };
            NotificationReceived?.Invoke(null, args);

        }

        public void SendNotification(string title, string message, DateTime? notifyTime = null)
        {
            messageId++;
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            RemoteViews remoteViews = new RemoteViews(AndroidApp.Context.PackageName, Resource.Layout.notification);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetSmallIcon(Resource.Drawable.lviv)
                .SetCustomContentView(remoteViews)
                .SetStyle(new NotificationCompat.DecoratedCustomViewStyle())
                .SetPriority(NotificationCompat.PriorityHigh);

            NotificationManagerCompat notificationManager = NotificationManagerCompat.From(AndroidApp.Context);
            notificationManager.Notify(messageId, builder.Build());

        }
        void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }
    }
}