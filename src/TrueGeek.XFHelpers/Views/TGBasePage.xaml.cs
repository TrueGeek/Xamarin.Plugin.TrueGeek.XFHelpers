using System.Threading.Tasks;
using TrueGeek.XFHelpers.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace TrueGeek.XFHelpers.Views
{

    [ContentProperty(nameof(MainContent))]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TGBasePage : ContentPage
    {

        public Thickness SafeAreaInsets { get; }

        // these are used to manually fire ViewModelInit when the
        // page is set to a Detail in a MasterDetailPage
        public bool FireViewModelInitAfterAppearing { get; set; }
        public object ViewModelInitParameters { get; set; }

        public bool HasAppeared { get; set; }

        public TGBasePage()
        {

            InitializeComponent();

            if (Init.ClearBackButtonTextOnAllPages) NavigationPage.SetBackButtonTitle(this, string.Empty);

            // set default busy overlay content
            IsBusyOverlayContent = Init.CustomActivityIndicator;

            // get value for SafeAreaInsets
            if (Device.RuntimePlatform == Device.iOS)
            {
                SafeAreaInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();    // 20 or 40 px depending on phone type
            }
            else
            {
                SafeAreaInsets = new Thickness();   // we don't care about this on Android
            }

        }

        protected async override void OnAppearing()
        {

            HasAppeared = true;

            if (BindingContext is TGBaseViewModel viewModel)
            {

                await viewModel.ViewAppearingAsync();

                if (FireViewModelInitAfterAppearing) await viewModel.ViewModelInit(ViewModelInitParameters);

            }

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
