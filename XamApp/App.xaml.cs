using System;
using XamApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //MainPage = new TikTakToe();
            //MainPage = new MainPage();
            //MainPage = new WishList();
            //MainPage = new CustomFragment();
            //MainPage = new MyPush();
            MainPage = new TellUs();
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
