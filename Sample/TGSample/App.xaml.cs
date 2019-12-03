using System;
using TGSample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TGSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ResetMainPage<HomePage>();
        }

        public void ResetMainPage<T>() where T : Page
        {
            // instead of a NavigationPage you could also use an AppShell
            MainPage = new NavigationPage(Activator.CreateInstance<T>());
            TrueGeek.XFHelpers.Statics.NavigationService = new TrueGeek.XFHelpers.Services.NavigationService(MainPage.Navigation);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
