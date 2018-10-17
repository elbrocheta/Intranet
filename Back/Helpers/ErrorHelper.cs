using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Back.Helpers
{
    public class ErrorHelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static String ERROR_VARIABLE = "Error";

        public static String ERROR_ACCESS_KEY = "NoAccess";
        public static String ERROR_ACCESS_VALUE = "No tiene acceso";


        public static String ERROR_EXEC_KEY = "ErrorExecution";
        public static String ERROR_EXEC_VALUE = "Error de ejecución";

        public static void p_AEPSAD_Log(String texto) {
            log.Info(" -> " + texto);
        }

        public static void p_AEPSAD_Log(Exception exception)
        {
            log.Info(" -> " + exception.InnerException);
            log.Info(" -> " + exception.StackTrace);
        }
    }
}