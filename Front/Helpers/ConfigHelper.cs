using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Front.Helpers
{
    public class ConfigHelper
    {
        #region WebApi

        public static String URL_FRONT = ConfigurationManager.AppSettings["URL_FRONT"];

        public static String URL_API = ConfigurationManager.AppSettings["URL_API"];

        #endregion

       


    }
}