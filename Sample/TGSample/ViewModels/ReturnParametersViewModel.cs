using System.Threading.Tasks;
using System.Windows.Input;
using TrueGeek.XFHelpers.ViewModels;
using Xamarin.Forms;

namespace TGSample.ViewModels
{

    public class ReturnParametersViewModel : TGBaseViewModel
    {

        public ReturnParametersViewModel()
        {
            SelectValueCommand = new Command(async (parameter) => await SelectValueCommandExecutor((string)parameter));
        }

        public ICommand SelectValueCommand { get; }
        private async Task SelectValueCommandExecutor(string parameter)
        {

            IsBusy = true;

            ReturnParameters = parameter;

            await TrueGeek.XFHelpers.Navigation.NavigationService.NavigateBack();

            IsBusy = false;

        }

    }

}
