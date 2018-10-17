using Front.Helpers;
using Front.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using static IntranetData.Enums;

namespace Front.Controllers
{
    public class PrensaController : Controller
    {
        
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewData["Modulo"] = (int)E_Modulos.Prensa;

            List<Noticias> _list = new List<Noticias>();

            HttpClient _client = WebApiHelper.p_APESAD_HttpClient();

            HttpResponseMessage _response = _client.GetAsync(WebApiHelper.ENDPOINT_NOTICIAS_GETBYTYPE + (int)E_NoticiaTipo.Prensa).Result;

            if (_response.IsSuccessStatusCode)
            {
                var _json = _response.Content.ReadAsStringAsync().Result;

                _list = JsonConvert.DeserializeObject<List<Noticias>>(_json);
            }

            return View(_list);
        }

        // GET: Prensa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Prensa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prensa/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Prensa/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Prensa/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Prensa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Prensa/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
