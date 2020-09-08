using System;
using System.Globalization;
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

            // set language code either of these two ways
            // if you don't set it, it'll default to CultureInfo.CurrentCulture.TwoLetterISOLanguageName
            //TrueGeek.XFHelpers.Init.LanguageCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            TrueGeek.XFHelpers.Init.LanguageCode = "en";        // try "de"
           
            TrueGeek.XFHelpers.Init.ResourceManager = new ResourceManager("TGSample.Resources.AppResources", typeof(App).GetTypeInfo().Assembly);

            SetMainPage<HomePage>();

        }

        public void SetMainPage<T>() where T : Page
        {            
            MainPage = new NavigationPage(Activator.CreateInstance<T>());   // instead of a NavigationPage you could also use an AppShell
            TrueGeek.XFHelpers.Init.NavigationService = new TrueGeek.XFHelpers.Services.NavigationService(MainPage.Navigation);

            var page = TrueGeek.XFHelpers.Navigation.NavigationService.GetDetailPageForMasterDetail<ReturnParametersPage>(null);
            var x = "";
            

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
