using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TrueGeek.XFHelpers.ViewModels;
using Xamarin.Forms;

namespace TGSample.ViewModels
{

    public class HomeViewModel : TGBaseViewModel
    {

        public HomeViewModel()
        {

            GoToActivityIndicatorPage = new Command(async () => await GoToActivityIndicatorPageExecutor());
            GoToEffectsPage = new Command(async () => await GoToEffectsPageExecutor());
            GoToTranslationsPage = new Command(async () => await GoToTranslationsPageExecutor());
            GoToReturnParametersPage = new Command(async () => await GoToReturnParametersPageExecutor());
        }

        public override Task ViewAppearingAsync()
        {            
            return base.ViewDisappearing();
        }

        public override Task ViewDisappearing()
        {
            return base.ViewDisappearing();
        }

        private void NavigationService_OnPageDisappearing(object sender, TrueGeek.XFHelpers.Services.PageDisappearingEventArgs e)
        {

            if (e.ViewModelType == typeof(ReturnParametersViewModel))
            {

                var selectedValue = (string)e.ReturnParameters;

                DisplayAlert("Return Parameter", selectedValue, "Okay");

                // unsubscribe from event (we'll resubscribe every time we re-navigate to the page
                TrueGeek.XFHelpers.Navigation.NavigationService.OnPageDisappearing -= NavigationService_OnPageDisappearing;

            }

        }

        public ICommand GoToActivityIndicatorPage { get; }
        private async Task GoToActivityIndicatorPageExecutor()
        {
            IsBusy = true;
            await TrueGeek.XFHelpers.Navigation.NavigationService.NavigateTo<ActivityIndicatorsViewModel>();
            IsBusy = false;
        }

        public ICommand GoToEffectsPage { get; }
        private async Task GoToEffectsPageExecutor()
        {
            IsBusy = true;
            await TrueGeek.XFHelpers.Navigation.NavigationService.NavigateTo<EffectsViewModel>();
            IsBusy = false;
        }

        public ICommand GoToTranslationsPage { get; }
        private async Task GoToTranslationsPageExecutor()
        {
            IsBusy = true;
            await TrueGeek.XFHelpers.Navigation.NavigationService.NavigateTo<TranslationsViewModel>();
            IsBusy = false;
        }

        public ICommand GoToReturnParametersPage { get; }
        private async Task GoToReturnParametersPageExecutor()
        {
            IsBusy = true;

            // subscribe to the event - we'll unsubscribe in NavigationService_OnPageDisappearing
            TrueGeek.XFHelpers.Navigation.NavigationService.OnPageDisappearing += NavigationService_OnPageDisappearing;

            await TrueGeek.XFHelpers.Navigation.NavigationService.NavigateTo<ReturnParametersViewModel>();

            IsBusy = false;
        }

    }

}
