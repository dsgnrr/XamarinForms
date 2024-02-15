using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new TikTakToe();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
