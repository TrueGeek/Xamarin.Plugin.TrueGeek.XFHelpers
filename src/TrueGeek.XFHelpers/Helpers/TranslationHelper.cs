using System;
using System.Globalization;

namespace TrueGeek.XFHelpers.Helpers
{

    public class TranslationHelper
    {

        public string GetText(string text)
        {

            // this is duplicated here and in TranslateExtension - need to clean that up
            if (text == null) return null;

            var languageCode = Init.LanguageCode;

            if (string.IsNullOrEmpty(languageCode)) languageCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            //var translation = Init.ResourceManager.GetString(Text, CultureInfo.CurrentCulture);            
            var translation = Init.ResourceManager.GetString(text, CultureInfo.GetCultureInfo(languageCode));

            // if we weren't able to find a translation then try it again for English
            if (string.IsNullOrEmpty(translation) && CultureInfo.CurrentCulture.TwoLetterISOLanguageName != "en")
            {
                return Init.ResourceManager.GetString(text, CultureInfo.GetCultureInfo("en-US"));
            }

            return translation;

        }

    }

}
