using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TrueGeek.XFHelpers.ViewModels;
using TrueGeek.XFHelpers.Views;
using Xamarin.Forms;

namespace TGSample.ViewModels
{
    public class ActivityIndicatorsViewModel : TGBaseViewModel
    {

        public ActivityIndicatorsViewModel()
        {
            ShowIndicator = new Command(async () => await ShowIndicatorExecutor());
            SetCustomIndicator = new Command(async () => await SetCustomIndicatorExecutor());
        }

        public ICommand ShowIndicator { get; }
        private async Task ShowIndicatorExecutor()
        {

            IsBusy = true;
            
            await Task.Delay(5000);

            IsBusy = false;

        }

        public ICommand SetCustomIndicator { get; }
        private async Task SetCustomIndicatorExecutor()
        {

            IsBusy = true;

            TrueGeek.XFHelpers.Init.CustomActivityIndicator = new Components.CustomActivityIndicator();

            await DisplayAlert("Reloading", "The activity indicator won't take effect until the next time the base page is loaded. I'll close and re-open this page so you can see it. Typically you'd load the custom activity indicator on app.xaml.cs so this wouldn't be a problem");

            await TrueGeek.XFHelpers.Navigation.NavigationService.NavigateToRoot();

            await TrueGeek.XFHelpers.Navigation.NavigationService.NavigateTo<ActivityIndicatorsViewModel>();

            IsBusy = false;

        }

    }

}
