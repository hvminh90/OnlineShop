using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace OnlineShop.Common
{
    public class SiteLanguage
    {
        public static List<Language> AvailableLanguages = new List<Language>
        {
            new Language{LangFullname="Tiếng việt", LangCultureName="vi"},
            new Language{LangFullname = "Tiếng anh", LangCultureName = "en"}

        };

        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.Where(p => p.LangCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
        }

        public static string GetLanguageDefault()
        {
            return AvailableLanguages[0].LangCultureName;
        }
        public void SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang))
                    lang = GetLanguageDefault();

                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                HttpCookie langCookie = new HttpCookie("culture",lang);
                langCookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(langCookie);



            }
            catch ( Exception ex)
            {
                
                throw;
            }
        }
    }

    public class Language
    {
        public string LangFullname { get; set; }
        public string LangCultureName { get; set; }
    }
}