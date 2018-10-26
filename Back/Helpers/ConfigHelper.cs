using System;
using System.Configuration;

namespace Back.Helpers
{
    public class ConfigHelper
    {

        #region URL

        public static String URL_BACK = ConfigurationManager.AppSettings["URL_BACK"];

        public static String URL_API = ConfigurationManager.AppSettings["URL_API"];

        public static String URL_ERROR = URL_BACK + ConfigurationManager.AppSettings["URL_ERROR"];        

        public static String URL_ICONS = URL_BACK + ConfigurationManager.AppSettings["URL_ICONS"];

        public static String PATH_UPLOADS_ICONS = ConfigurationManager.AppSettings["URL_ICONS"];

        #endregion

    }
}