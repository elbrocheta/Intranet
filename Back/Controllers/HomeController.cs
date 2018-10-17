using Back.App_Start;
using Back.Helpers;
using Back.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Back.Controllers
{
    [AuthorizationFilter]
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Index(Usuario model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                HttpClient _client = WebApiHelper.p_APESAD_HttpClient();

                var serializedProduct = JsonConvert.SerializeObject(model);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                HttpResponseMessage _response = await _client.PostAsync(WebApiHelper.ENDPOINT_LOGIN, content);

                if (_response.IsSuccessStatusCode)
                {
                    var _json = _response.Content.ReadAsStringAsync().Result;

                    model = JsonConvert.DeserializeObject<Usuario>(_json);

                    SessionHelper.p_AEPSAD_set(SessionHelper.SESSION_NAME_TOKEN, model.Token);
                    SessionHelper.p_AEPSAD_set(SessionHelper.SESSION_NAME_USER, model);

                    return RedirectToAction("Index", "Main");
                }
                else
                {
                    ModelState.AddModelError("Login", "Usuario y/o Clave incorrectos");
                    ModelState.AddModelError("Password", "Usuario y/o Clave incorrectos");

                    ErrorHelper.p_AEPSAD_Log(_response.StatusCode.ToString());
                }

                return View("Index", model);
            }
            catch (Exception ex) {

                ErrorHelper.p_AEPSAD_Log(ex.Message);

                return RedirectToAction(ConfigHelper.URL_ERROR + "?" + ErrorHelper.ERROR_VARIABLE + "=" + ErrorHelper.ERROR_EXEC_KEY);
            }
        }

        public ActionResult LogOut()
        {
            SessionHelper.p_AEPSAD_clean_all();

            return View("Index");
        }





    }
}