using System;
using System.Configuration;

namespace Back.Helpers
{
    public class ConfigHelper
    {

        #region URL

        public static String URL_BACK = ConfigurationManager.AppSettings["URL_BACK"];

        public static String URL_API = ConfigurationManager.AppSettings["URL_API"];

        public static String URL_ERROR = URL_BACK + "/Error/Index";

        #endregion

    }
}