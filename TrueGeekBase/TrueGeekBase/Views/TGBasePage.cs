using System.Threading.Tasks;
using TrueGeek.XFHelpers.ViewModels;
using Xamarin.Forms;

namespace TrueGeek.XFHelpers.Views
{

    public class TGBasePage : ContentPage
    {

        protected async override void OnAppearing()
        {
            if (BindingContext is TGBaseViewModel viewModel) await viewModel.ViewAppearingAsync();
            base.OnAppearing();

        }

        protected async override void OnDisappearing()
        {
            if (BindingContext is TGBaseViewModel viewModel) await viewModel.ViewDisappearing();
            base.OnDisappearing();
        }

        public virtual async Task ViewModelInit(object parameters)
        {
            if (BindingContext is TGBaseViewModel viewModel) await viewModel.ViewModelInit(parameters);
        }

    }

}
