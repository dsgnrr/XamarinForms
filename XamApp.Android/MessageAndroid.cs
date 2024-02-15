using Android.Widget;
using System;
using System.Collections.Generic;
using System.Text;
using XamApp.Messages;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace XamApp.Messages
{
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}
