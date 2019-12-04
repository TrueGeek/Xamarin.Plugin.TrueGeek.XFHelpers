using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TrueGeek.XFHelpers.ViewModels;
using Xamarin.Forms;

namespace TGSample.ViewModels
{

    public class EffectsViewModel : TGBaseViewModel
    {

        public EffectsViewModel()
        {
            PressCommand = new Command(async () => await PressCommandExecutor());
            LongPressCommand = new Command(async () => await LongPressCommandExecutor());
        }

        public ICommand PressCommand { get; }
        private async Task PressCommandExecutor()
        {
            if (IsBusy) return;
            IsBusy = true;
            await DisplayAlert("Effects", "This is a press");
            IsBusy = false;
        }

        public ICommand LongPressCommand { get; }
        private async Task LongPressCommandExecutor()
        {
            if (IsBusy) return;
            IsBusy = true;
            await DisplayAlert("Effects", "This is a long press");
            IsBusy = false;
        }

    }

}
