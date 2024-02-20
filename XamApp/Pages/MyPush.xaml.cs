using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamApp.Classes;
using XamApp.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace XamApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPush : ContentPage
    {
        INotificationManager notificationManager;
        int notificationNumber = 0;
        bool isFlashLight = false;
        public MyPush()
        {
            InitializeComponent();

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }

        void OnSendClick(object sender, EventArgs e)
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message);
        }
        async Task<PermissionStatus> isCameraPermission()
        {
            return await Permissions.CheckStatusAsync<Permissions.Camera>();
        }
        async void TurnFlashlight(object sender, EventArgs e)
        {
            if (PermissionStatus.Granted == await isCameraPermission())
            {
                if (isFlashLight)
                {
                    // Если включен, выключите его
                    await Xamarin.Essentials.Flashlight.TurnOffAsync();
                    isFlashLight = false;
                }
                else
                {
                    isFlashLight = true;
                    // Если выключен, включите его
                    await Xamarin.Essentials.Flashlight.TurnOnAsync();
                }
            }
            else
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();
            }
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
        }
    }
}