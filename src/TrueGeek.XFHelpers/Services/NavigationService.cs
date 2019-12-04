﻿using System;
using System.Threading.Tasks;
using TrueGeek.XFHelpers.Views;
using Xamarin.Forms;

namespace TrueGeek.XFHelpers.Services
{

    public class NavigationService
    {

        private INavigation _navigation;

        /// <summary>
        /// Call this immediatly after setting the MainPage of your application. Pass it MainPage.Navigation.
        /// </summary>
        /// <param name="navigation"></param>
        public NavigationService(INavigation navigation)
        {
            _navigation = navigation;
        }

        /// <summary>
        /// Call this if the MainPage of your application is changed
        /// </summary>
        /// <param name="navigation"></param>
        public void UpdateMainPage(INavigation navigation)
        {
            _navigation = navigation;
        }

        public async Task NavigateTo<TViewModel>(object parameters = null)
        {

            var page = GetPageFromViewModel(typeof(TViewModel));
            if (page != null)
            {
                await _navigation.PushAsync(page, true);
                await (page as TGBasePage).ViewModelInit(parameters);
            }

        }

        public async Task NavigateTo<TViewModel, TPage>(object parameters = null)
        {

            try
            {

                var page = CreatePageFromPageType(typeof(TPage), typeof(TViewModel));
                if (page != null)
                {
                    await _navigation.PushAsync(page, true);
                    await (page as TGBasePage).ViewModelInit(parameters);
                }

            }
            catch (Exception ex)
            {
                // most likely page not found for view model
                System.Diagnostics.Debug.WriteLine("TrueGeek.XFHelpers - Error at NavigateTo");
                System.Diagnostics.Debug.Write(ex);
            }

        }

        public async Task NavigateToModal<TViewModel>(object parameters = null)
        {

            var page = GetPageFromViewModel(typeof(TViewModel));
            if (page != null)
            {
                await _navigation.PushModalAsync(page, true);
                if (parameters != null)
                {
                    await (page as TGBasePage).ViewModelInit(parameters);
                }
            }

        }

        public async Task NavigateBack()
        {
            await _navigation.PopAsync(true);
        }

        public async Task NavigateToRoot()
        {
            await _navigation.PopToRootAsync(true);
        }

        private Page GetPageFromViewModel(Type viewModelType)
        {

            var pageName = viewModelType
                .AssemblyQualifiedName
                .Replace("ViewModels", "Views")     // namespace
                .Replace("ViewModel", "Page");      // class name

            var pageType = Type.GetType(pageName);

            return CreatePageFromPageType(pageType, viewModelType);

        }

        private Page CreatePageFromPageType(Type pageType, Type viewModelType)
        {

            if (pageType == null) return null;

            var page = (Page)Activator.CreateInstance(pageType);
            page.BindingContext = Activator.CreateInstance(viewModelType);
            return page;

        }

    }

}