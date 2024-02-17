using System;
using System.Collections.Generic;
using System.Text;
using XamApp.Messages;
using Xamarin.Forms;

namespace XamApp.Classes
{
    public static class AndroidFunctions
    {
        public static void toast(string message)
        {
            DependencyService.Get<IMessage>().LongAlert(message);
        }
    }
}
