using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueGeek.XFHelpers.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrueGeek.XFHelpers.Views
{

    [ContentProperty(nameof(MainContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TGBasePage : ContentPage
    {

        public TGBasePage()
        {

            InitializeComponent();

            // set default busy overlay content
            IsBusyOverlayContent = Statics.CustomActivityIndicator;

        }

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

        // hat tip to https://jaredrhodes.com/2017/12/11/creating-a-common-loading-page-for-xamarin-forms/

        public static readonly BindableProperty MainContentProperty = BindableProperty.Create(
            nameof(MainContent),
            typeof(View),
            typeof(TGBasePage));
        public View MainContent
        {
            get { return (View)GetValue(MainContentProperty); }
            set { SetValue(MainContentProperty, value); }
        }

        public static readonly BindableProperty IsBusyOverlayContentProperty = BindableProperty.Create(
            nameof(IsBusyOverlayContent),
            typeof(View),
            typeof(TGBasePage));
        public View IsBusyOverlayContent
        {
            get => (View)GetValue(IsBusyOverlayContentProperty);
            set => SetValue(IsBusyOverlayContentProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (MainContent == null)
            {
                return;
            }
            SetInheritedBindingContext(MainContent, BindingContext);
            SetInheritedBindingContext(IsBusyOverlayContent, BindingContext);
        }

    }

}
