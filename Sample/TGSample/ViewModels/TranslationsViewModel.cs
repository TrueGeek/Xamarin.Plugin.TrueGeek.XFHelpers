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

        public string TranslatedText { get; set; }

        public TranslationsViewModel()
        {
            TranslatedText = new TrueGeek.XFHelpers.Helpers.TranslationHelper().GetText("helper_functions");
        }

    }

}
