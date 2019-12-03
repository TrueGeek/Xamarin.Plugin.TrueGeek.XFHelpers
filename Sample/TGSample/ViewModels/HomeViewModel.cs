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
        }

        public ICommand GoToActivityIndicatorPage { get; }
        private async Task GoToActivityIndicatorPageExecutor()
        {
            IsBusy = true;
            await TrueGeek.XFHelpers.Statics.NavigationService.NavigateTo<ActivityIndicatorsViewModel>();
            IsBusy = false;
        }

    }

}
