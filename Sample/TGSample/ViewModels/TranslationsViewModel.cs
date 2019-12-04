using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using TrueGeek.XFHelpers.ViewModels;
using Xamarin.Forms;

namespace TGSample.ViewModels
{

    public class TranslationsViewModel : TGBaseViewModel
    {

        public string TranslatedText => Resources.AppResources.helper_functions;

    }

}
