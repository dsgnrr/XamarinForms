using Android.App;
using Xamarin.Essentials;
using Android.Widget;
using System;
using System.Threading.Tasks;
using XamApp.Droid.AndroidServices;
using XamApp.Interfaces;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;
using Org.Apache.Http;

[assembly: Dependency(typeof(RatingDialog))]
namespace XamApp.Droid.AndroidServices
{
    public class RatingDialog:IAlert
    {

        public void ShowRatingDialog()
        {
            var activity = MainActivity.mainActivity;
            var dialogView = activity.LayoutInflater.Inflate(Resource.Layout.rating_dialog, null);
            var ratingBar = dialogView.FindViewById<RatingBar>(Resource.Id.rating_bar);
            var dialog = new AlertDialog.Builder(activity)
                .SetView(dialogView)
                .SetTitle("Please rate us")
                .SetPositiveButton("Ok", async (s, e) => {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        if (ratingBar.Rating > 3)
                        {
                            try
                            {
                                string url = @"https://play.google.com/";
                                await Browser.OpenAsync(url, BrowserLaunchMode.External);
                            }
                            catch (MethodNotSupportedException ex)
                            {

                            }
                        }
                        else ShowFeedbackDialog();
                    }
                })
                .SetNegativeButton("Cancel", (s, e) => { })
                .Create();
          
            dialog.Show();
        }
        private void ShowFeedbackDialog()
        {
            var dialogView = MainActivity.mainActivity.LayoutInflater.Inflate(Resource.Layout.feedback, null);
            var dialog = new AlertDialog.Builder(MainActivity.mainActivity)
               .SetView(dialogView)
               .SetTitle("Please leave feedback")
               .SetPositiveButton("Ok", (s, e) => {})
               .SetNegativeButton("Cancel", (s, e) => { })
               .Create();

            dialog.Show();
        }
    }
}