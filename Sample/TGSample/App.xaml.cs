using System;
using System.Reflection;
using System.Resources;
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

            TrueGeek.XFHelpers.Init.ResourceManager = new ResourceManager("TGSample.Resources.AppResources", typeof(App).GetTypeInfo().Assembly);
            SetMainPage<HomePage>();

        }

        public void SetMainPage<T>() where T : Page
        {            
            MainPage = new NavigationPage(Activator.CreateInstance<T>());   // instead of a NavigationPage you could also use an AppShell
            TrueGeek.XFHelpers.Init.NavigationService = new TrueGeek.XFHelpers.Services.NavigationService(MainPage.Navigation);
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
