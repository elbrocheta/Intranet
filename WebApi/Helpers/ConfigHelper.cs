using System;
using System.Configuration;

namespace WebApi.Helpers
{
    public class ConfigHelper
    {

        #region Token

        public static String TOKEN_SECRET_KEY = ConfigurationManager.AppSettings ["JWT_SECRET_KEY"];

        public static String TOKEN_AUDIENCE = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];

        public static String TOKEN_ISSUER = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];

        public static String TOKEN_EXPIRE_TIME = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

        #endregion

        #region Active Directory

        public static String AD_URL_ACTIVE_DIRECTORY = ConfigurationManager.AppSettings["URL_ACTIVE_DIRECTORY"];

        #endregion



    }
}