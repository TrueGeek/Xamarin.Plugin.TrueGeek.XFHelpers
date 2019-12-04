using TrueGeek.XFHelpers.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Resources;
using System;

namespace TrueGeek.XFHelpers
{

    public static class Init
    {

        static Init()
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

        public static NavigationService NavigationService
        {
            get => Navigation.NavigationService;
            set
            {
                Navigation.NavigationService = value;
            }
        }

        public static View CustomActivityIndicator { get; set; }

        public static ResourceManager ResourceManager { get; set; }

    }

}
