using Back.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back.Helpers
{
    public class SessionHelper
    {
        public static String SESSION_NAME_TOKEN = "Token";
        public static String SESSION_NAME_USER = "Usuario";

        public static void p_AEPSAD_set(String key, Object value) {
            HttpContext.Current.Session[key] = value;
        }

        public static void p_AEPSAD_clean(String key) {
            HttpContext.Current.Session.Remove(key);
        }

        public static void p_AEPSAD_clean_all()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        public static bool p_AEPSAD_check(String key)
        {
            if (HttpContext.Current.Session[key] == null)
                return false;

            return true;
        }

        public static Usuario p_AEPSAD_get_usuario()
        {
            return (Usuario)HttpContext.Current.Session[SESSION_NAME_USER];
        }


    }
}