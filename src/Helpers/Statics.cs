using TrueGeek.XFHelpers.Services;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TrueGeek.XFHelpers
{

    public static class Statics
    {

        static Statics()
        {

            CustomActivityIndicator = new ActivityIndicator()
            {
                IsVisible = true,
                IsRunning = true,
                Color = Color.Black,
                HorizontalOptions = DeviceInfo.Platform == DevicePlatform.iOS ? LayoutOptions.FillAndExpand : LayoutOptions.Center,
                VerticalOptions = DeviceInfo.Platform == DevicePlatform.iOS ? LayoutOptions.FillAndExpand : LayoutOptions.Center,
            };

        }

        public static NavigationService NavigationService { get; set; }

        public static View CustomActivityIndicator { get; set; }

    }

}
