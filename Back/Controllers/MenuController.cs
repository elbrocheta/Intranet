using Back.App_Start;
using Back.Helpers;
using Back.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Back.Controllers
{
    [AuthorizationFilter]
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult MenuItems()
        {
            return View();
        }

       /// <summary>
       /// Listado sin filtro
       /// </summary>
       /// <returns></returns>
        public ActionResult GruposMenu()
        {
            try
            {
                List<vBack_GruposMenuList> _list = new List<vBack_GruposMenuList>();
                HttpResponseMessage _response = WebApiHelper.p_AEPSAD_Request(WebApiHelper.ENDPOINT_MENU_GRUPOS_GETALL);

                if (_response.IsSuccessStatusCode)
                {
                    var _json = _response.Content.ReadAsStringAsync().Result;

                    _list = JsonConvert.DeserializeObject<List<vBack_GruposMenuList>>(_json);
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.StatusCode.ToString());
                }

                if (_list == null)
                    return View(new List<vBack_GruposMenuList>());

                ViewBag.Opciones = DropDownListHelper.p_AEPSAD_Modulos();

                return View(_list);
            }

            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);

                return RedirectToAction(ConfigHelper.URL_ERROR + "?" + ErrorHelper.ERROR_VARIABLE + "=" + ErrorHelper.ERROR_EXEC_KEY);
            }
        }

        /// <summary>
        /// Listado con filtro
        /// </summary>
        /// <param name="filterByModulo"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GruposMenu(int filterByModulo = -1)
        {
            try
            {
                List<vBack_GruposMenuList> _list = new List<vBack_GruposMenuList>();
                HttpResponseMessage _response = WebApiHelper.p_AEPSAD_Request(WebApiHelper.ENDPOINT_MENU_GRUPOS_GETALL);

                if (_response.IsSuccessStatusCode)
                {
                    var _json = _response.Content.ReadAsStringAsync().Result;

                    _list = JsonConvert.DeserializeObject<List<vBack_GruposMenuList>>(_json);
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.StatusCode.ToString());
                }

                if (_list == null)
                    return View(new List<vBack_GruposMenuList>());

                ViewBag.Opciones = DropDownListHelper.p_AEPSAD_Modulos(filterByModulo);

                if (filterByModulo > 0)
                    _list = _list.Where(x => x.ModuloId == filterByModulo).ToList();

                return View(_list);
            }

            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);

                return RedirectToAction(ConfigHelper.URL_ERROR + "?" + ErrorHelper.ERROR_VARIABLE + "=" + ErrorHelper.ERROR_EXEC_KEY);
            }
        }


        [HttpGet]
        public ActionResult EditarGruposMenu(String id)
        {
            MenuGrupo _m = new MenuGrupo();

            try
            {
                List<MenuGrupo> _list = new List<MenuGrupo>();
                HttpResponseMessage _response = WebApiHelper.p_AEPSAD_Request(WebApiHelper.ENDPOINT_MENU_GRUPOS_GETONE + id);

                if (_response.IsSuccessStatusCode)
                {
                    var _json = _response.Content.ReadAsStringAsync().Result;

                    _list = JsonConvert.DeserializeObject<List<MenuGrupo>>(_json);

                    if (_list.Count > 0)
                    {
                        _m = _list[0];
                        _m.Modulos = DropDownListHelper.p_AEPSAD_Modulos(_m.ModuloId);
                    }
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.StatusCode.ToString());
                }

                if (_list == null)
                    return View(_m);

                return View(_m);
            }

            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);

                return RedirectToAction(ConfigHelper.URL_ERROR + "?" + ErrorHelper.ERROR_VARIABLE + "=" + ErrorHelper.ERROR_EXEC_KEY);
            }
        }

        [HttpPost]
        public ActionResult EditarGruposMenu(MenuGrupo model)
        {
            model.Modulos = DropDownListHelper.p_AEPSAD_Modulos(model.ModuloId);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                HttpClient _client = WebApiHelper.p_APESAD_HttpClient(SessionHelper.p_AEPSAD_get_usuario().Token);

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage _response = _client.PutAsync(WebApiHelper.ENDPOINT_MENU_GRUPOS_UPDATE, content).Result;

                if (_response.IsSuccessStatusCode) {
                    ViewBag.JavaScriptFunction = string.Format("waitingDialog.datosGuardados();");
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.ReasonPhrase);
                    ViewBag.JavaScriptFunction = string.Format("waitingDialog.datosError();");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);
                ViewBag.JavaScriptFunction = string.Format("waitingDialog.datosError();");
                View(model);
            }

            return View(model);
        }
         
        public ActionResult CrearGruposMenu()
        {
            MenuGrupo _m = new MenuGrupo();
            _m.Modulos = DropDownListHelper.p_AEPSAD_Modulos();

            return View(_m);
        }

        [HttpPost]
        public ActionResult CrearGruposMenu(MenuGrupo model)
        {
            model.Modulos = DropDownListHelper.p_AEPSAD_Modulos(model.ModuloId);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                HttpClient _client = WebApiHelper.p_APESAD_HttpClient(SessionHelper.p_AEPSAD_get_usuario().Token);

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage _response = _client.PostAsync(WebApiHelper.ENDPOINT_MENU_GRUPOS_CREATE, content).Result;

                if (_response.IsSuccessStatusCode)
                {
                    ViewBag.JavaScriptFunction = string.Format("waitingDialog.datosGuardados();");
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.ReasonPhrase);
                    ViewBag.JavaScriptFunction = string.Format("waitingDialog.datosError();");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);
                ViewBag.JavaScriptFunction = string.Format("waitingDialog.datosError();");
                View(model);
            }

            return View(model);
        }
        
        [HttpGet]
        public ActionResult EliminarGruposMenu(String id)
        {
            return View();
        }




    }

}