using Back.App_Start;
using System.Web.Mvc;

namespace Back.Controllers
{
    [AuthorizationFilter]
    public class NoticiasController : Controller
    {
        // GET: Noticias
        public ActionResult Index()
        {
            return View();
        }
    }
}