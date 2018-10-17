using Front.Helpers;
using Front.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using static IntranetData.Enums;

namespace Front.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                ViewData["Modulo"] = (int)E_Modulos.Inicio;

                List<Noticias> _list = new List<Noticias>();

                HttpClient _client = WebApiHelper.p_APESAD_HttpClient();

                HttpResponseMessage _response = _client.GetAsync(WebApiHelper.ENDPOINT_NOTICIAS_GETINDEX).Result;

                if (_response.IsSuccessStatusCode)
                {
                    var _json = _response.Content.ReadAsStringAsync().Result;

                    _list = JsonConvert.DeserializeObject<List<Noticias>>(_json);
                }
                else
                {
                    log.Info(DateTime.Now.ToString("dd-MM-yyyy | HH:mm:ss") + " -> " + _response.StatusCode.ToString());
                }

                return View(_list);
            }
            catch(Exception ex)
            {
                log.Info(DateTime.Now.ToString("dd-MM-yyyy | HH:mm:ss") + " -> " + ex.Message);

                return View();
            }
        }
    }
}