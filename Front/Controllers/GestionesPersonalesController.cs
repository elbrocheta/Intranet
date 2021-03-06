﻿using Front.Helpers;
using Front.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using static IntranetData.Enums;

namespace Front.Controllers
{
    public class GestionesPersonalesController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewData["Modulo"] = (int)E_Modulos.GestionesPersonales;

            List<Noticias> _list = new List<Noticias>();

            HttpClient _client = WebApiHelper.p_APESAD_HttpClient();

            HttpResponseMessage _response = _client.GetAsync(WebApiHelper.ENDPOINT_NOTICIAS_GETBYTYPE + (int)E_NoticiaTipo.GestionesPersonales).Result;

            if (_response.IsSuccessStatusCode)
            {
                var _json = _response.Content.ReadAsStringAsync().Result;

                _list = JsonConvert.DeserializeObject<List<Noticias>>(_json);
            }

            return View(_list);
        }
    }
}
