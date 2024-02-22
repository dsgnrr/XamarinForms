using System;
using Android.Hardware;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using XamApp.Messages;
using Xamarin.Essentials;
using System.Threading.Tasks;
using XamApp.Interfaces;

namespace XamApp.Droid
{
    [Activity(Label = "Learn Xamarin", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity mainActivity { get; set; }
        private bool isFlashLight = false;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            mainActivity = this;
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            checkFlashLightState();
            
            if (PermissionStatus.Granted == await isCameraPermission())
            {
                
                if (isFlashLight)
                {
                    // Если включен, выключите его
                    await Xamarin.Essentials.Flashlight.TurnOffAsync();
                    isFlashLight = false;
                    App.Current.Properties["flashState"] = isFlashLight;
                }
                else
                {
                    isFlashLight = true;
                    App.Current.Properties["flashState"] = isFlashLight;
                    // Если выключен, включите его
                    await Xamarin.Essentials.Flashlight.TurnOnAsync();
                }
            }
            else
            {
                var status = await Permissions.RequestAsync<Permissions.Camera>();
            }
            // Закрываем приложение
            Finish();
            
        }
        public void checkFlashLightState()
        {
            object flashState = false;
            App.Current.Properties.TryGetValue("flashState", out flashState);
            if (flashState != null)
            {
                isFlashLight = (bool)flashState;
            }
            else
            {
                App.Current.Properties.Add("flashState",isFlashLight);
            }
        }
        async Task<PermissionStatus> isCameraPermission()
        {
            return await Permissions.CheckStatusAsync<Permissions.Camera>();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}