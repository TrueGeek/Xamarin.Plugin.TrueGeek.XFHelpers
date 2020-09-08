using System;
using System.Threading.Tasks;
using TrueGeek.XFHelpers.ViewModels;
using TrueGeek.XFHelpers.Views;
using Xamarin.Forms;

namespace TrueGeek.XFHelpers.Services
{

    public class PageDisappearingEventArgs : EventArgs
    {
        public Type ViewModelType { get; set; }
        public object ReturnParameters { get; set; }
    }

    public class NavigationService
    {

        private INavigation _navigation;

        public event EventHandler<PageDisappearingEventArgs> OnPageDisappearing;

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
                page.Disappearing += OnPageDisappearingInternal;
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
                    page.Disappearing += OnPageDisappearingInternal;
                    await _navigation.PushAsync(page, true);
                    await (page as TGBasePage).ViewModelInit(parameters);
                }

            }
            catch (Exception ex)
            {
                // most likely page not found for view model
                Helpers.LoggingHelper.Log($"TrueGeek.XFHelpers - Error at NavigateTo {ex.Message}", Models.LogLevel.Error);
            }

        }

        public async Task NavigateToModal<TViewModel>(object parameters = null, bool useNavigationPage = false, Style navigationPageStyle = null)
        {

            var page = GetPageFromViewModel(typeof(TViewModel));
            if (page != null)
            {
                page.Disappearing += OnPageDisappearingInternal;

                if (useNavigationPage)
                {
                    await _navigation.PushModalAsync(new NavigationPage(page)
                    {
                        Style = navigationPageStyle,
                    }, true);
                }
                else
                {
                    await _navigation.PushModalAsync(page, true);
                }                

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

        public async Task NavigateBackFromModal()
        {
            await _navigation.PopModalAsync(true);
        }

        public async Task NavigateToRoot()
        {
            await _navigation.PopToRootAsync(true);
        }

        public Page GetPageForMasterDetail<TViewModel>(object parameters = null, bool useNavigationPage = true, Style navigationPageStyle = null)
        {

            try
            {

                var page = GetPageFromViewModel(typeof(TViewModel)) as TGBasePage;
                if (page != null)
                {

                    page.Disappearing += OnPageDisappearingInternal;

                    if (parameters != null)
                    {
                        page.FireViewModelInitAfterAppearing = true;
                        page.ViewModelInitParameters = parameters;
                    }

                }

                if (useNavigationPage)
                {

                    return new NavigationPage(page)
                    {
                        Style = navigationPageStyle,
                    };

                }
                else
                {

                    return page;

                }

            }
            catch (Exception ex)
            {
                Helpers.LoggingHelper.Log($"TrueGeek.XFHelpers.SetMasterDetailDetailPage {ex.Message}", Models.LogLevel.Error);
                return null;
            }

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

        private void OnPageDisappearingInternal(object sender, EventArgs e)
        {

            try
            {

                // get page
                var page = sender as Page;

                // get ViewModel
                var viewModel = page.BindingContext as TGBaseViewModel;

                // get return parameters
                var returnParameters = viewModel.ReturnParameters;

                // unsubscribe from event
                page.Disappearing -= OnPageDisappearingInternal;

                // fire event
                OnPageDisappearing?.Invoke(page.BindingContext, new PageDisappearingEventArgs()
                {
                    ReturnParameters = returnParameters,
                    ViewModelType = viewModel.GetType(),
                });

            }
            catch (Exception ex)
            {
                Helpers.LoggingHelper.Log($"TrueGeek.XFHelpers - Error at OnPageDisappearingInternal {ex.Message}", Models.LogLevel.Error);
            }

        }

    }

}
