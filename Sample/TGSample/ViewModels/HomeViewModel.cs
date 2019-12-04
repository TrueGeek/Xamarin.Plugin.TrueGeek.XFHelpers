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

    }

}
