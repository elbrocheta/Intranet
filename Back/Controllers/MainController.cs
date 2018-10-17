using Back.App_Start;
using System.Web.Mvc;

namespace Back.Controllers
{
    [AuthorizationFilter]
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
    }
}