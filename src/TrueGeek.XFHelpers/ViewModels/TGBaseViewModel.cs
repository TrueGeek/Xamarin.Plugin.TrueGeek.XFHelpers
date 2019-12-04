using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TrueGeek.XFHelpers.Helpers;
using TrueGeek.XFHelpers.Views;
using Xamarin.Forms;

namespace TrueGeek.XFHelpers.ViewModels
{

    public class TGBaseViewModel : INotifyPropertyChanged
    {

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        #pragma warning disable
        public virtual async Task ViewAppearingAsync()
        {
        }

        public virtual async Task ViewDisappearing()
        {
        }

        public virtual async Task ViewModelInit(object parameters)
        {
        }
        #pragma warning restore

        //
        // Page Action Sheet / Popup
        //

        protected async Task<string> DisplayActionSheet(string title, string cancelButtonText, params string[] buttons)
        {
            // note that on Android if cancelButtonText is null the ActionSheet is shown as a modal
            return await Application.Current.MainPage.DisplayActionSheet(title, cancelButtonText, null, buttons);
        }

        protected Task<bool> DisplayAlert(string title, string message, string accept = "okay", string cancel = "cancel")
        {
            return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
        }

        protected Task<string> DisplayPrompt(string title, string message, string accept = "okay", string cancel = "cancel", string placeholder = null, int maxLength = -1, Keyboard keyboard = null)
        {
            return Application.Current.MainPage.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, keyboard);
        }

        //
        // INotifyPropertyChanged
        //

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        // 3 ways to call OnPropertyChanged:
        //      1. OnPropertyChanged()                                      -- will fire for the current setter using [CallerMemberName]
        //      2. OnPropertyChanged(() => VariableThatJustUpdated)         -- will fire for VariableThatJustUpdated
        //      3. OnPropertyChanged(nameof(VariableThatJustUpdated))       -- will fire for VariableThatJustUpdated
        //
        //  Never do this as it's just too easy to make a typo:
        //      x. OnPropertyChanged("VaraibleThetJustUpdetad")
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(projection.PropertyName()).PropertyName);
        }

    }

}
