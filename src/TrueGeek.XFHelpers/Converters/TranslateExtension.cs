using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrueGeek.XFHelpers.Converters
{

    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {

            if (Text == null) return null;

            var translation = Init.ResourceManager.GetString(Text, CultureInfo.CurrentCulture);

            // if we weren't able to find a translation then try it again for English
            if (string.IsNullOrEmpty(translation) && CultureInfo.CurrentCulture.TwoLetterISOLanguageName != "en")
            {
                return Init.ResourceManager.GetString(Text, CultureInfo.GetCultureInfo("en-US"));
            }

            return translation;

        }

    }

}
