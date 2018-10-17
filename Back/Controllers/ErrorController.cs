using Back.Helpers;
using System;
using System.Web.Mvc;

namespace Back.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Error
        public ActionResult Index(string Error)
        {
            ViewBag.Mensaje = "Se ha producido un error";
            ViewBag.Title = "Error";            

            if (Error.Equals(ErrorHelper.ERROR_ACCESS_KEY))
                ViewBag.Mensaje = ErrorHelper.ERROR_ACCESS_VALUE;

            ErrorHelper.p_AEPSAD_Log(ViewBag.Mensaje);

            return View();
        }
    }
}