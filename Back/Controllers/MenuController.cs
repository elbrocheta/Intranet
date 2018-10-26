using Back.App_Start;
using Back.Helpers;
using Back.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Back.Controllers
{
    [AuthorizationFilter]
    public class MenuController : Controller
    {
        //GET: Menu
        public ActionResult MenuItems()
        {
            try
            {
                List<vBack_Menu> _list = new List<vBack_Menu>();
                HttpResponseMessage _response = WebApiHelper.p_AEPSAD_Request(WebApiHelper.ENDPOINT_MENU_GETALL);

                if (_response.IsSuccessStatusCode)
                {
                    var _json = _response.Content.ReadAsStringAsync().Result;

                    _list = JsonConvert.DeserializeObject<List<vBack_Menu>>(_json);
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.StatusCode.ToString());
                }

                if (_list == null)
                    return View(new List<vBack_Menu>());

                _list = _list.OrderBy(x => x.ModuloId).OrderBy(x => x.MenuGrupoId).ToList();

                return View(_list);
            }

            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);

                return RedirectToAction(ConfigHelper.URL_ERROR + "?" + ErrorHelper.ERROR_VARIABLE + "=" + ErrorHelper.ERROR_EXEC_KEY);
            }
        }

        public ActionResult CrearMenuItems()
        {
            Menu _m = new Menu
            {
                GruposMenu = DropDownListHelper.p_AEPSAD_MenuGrupos()
            };


            return View(_m);
        }

        [HttpPost]
        public ActionResult CrearMenuItems(Menu model, HttpPostedFileBase IconoFile)
        {  
            try
            {
                model.GruposMenu = DropDownListHelper.p_AEPSAD_MenuGrupos(model.MenuGrupoId);

                String _error = String.Empty;
                String _icono = String.Empty;

                bool _isFileUploaded = UploadImageHelper.p_AEPSAD_UploadImage(IconoFile, "IconoMenu_" + model.Id, ref _icono, ref _error);

                if (!_isFileUploaded)
                {
                    ModelState.AddModelError("IconoFile", _error);
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                HttpClient _client = WebApiHelper.p_APESAD_HttpClient(SessionHelper.p_AEPSAD_get_usuario().Token);

                MenuModel _m = new MenuModel
                {
                    Id = model.Id,
                    MenuGrupoId = model.MenuGrupoId,
                    Texto = model.Texto,
                    Enlace = model.Enlace,
                    Icono = _icono,
                    NuevaVentana = (model.NuevaVentana ? Convert.ToByte(1) : Convert.ToByte(0)),
                    Orden = model.Orden
                };

                var _json = JsonConvert.SerializeObject(_m);
                var _content = new StringContent(_json.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage _response = _client.PostAsync(WebApiHelper.ENDPOINT_MENU_CREATE, _content).Result;

                if (_response.IsSuccessStatusCode)
                {
                    ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_save_ok('Los datos han sido creados');");
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.ReasonPhrase);
                    ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_error('" + _response.ReasonPhrase + "');");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);
                ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_error('" + ex.Message + "');");
                return View(model);
            }
        }
        
        [HttpGet]
        public ActionResult EditarMenuItems(String id)
        {
            Menu _m = new Menu();

            try
            {
                List<Menu> _list = new List<Menu>();
                HttpResponseMessage _response = WebApiHelper.p_AEPSAD_Request(WebApiHelper.ENDPOINT_MENU_GETONE + id);

                if (_response.IsSuccessStatusCode)
                {
                    var _json = _response.Content.ReadAsStringAsync().Result;

                    _list = JsonConvert.DeserializeObject<List<Menu>>(_json);

                    if (_list.Count > 0)
                    {
                        _m = _list[0];
                        _m.GruposMenu = DropDownListHelper.p_AEPSAD_MenuGrupos(_m.MenuGrupoId);
                    }
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.StatusCode.ToString());
                }

                ViewBag.FontAwesome = DropDownListHelper.p_AEPSAD_FontAwesomeIconList();

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
        public ActionResult EditarMenuItems(Menu model, HttpPostedFileBase IconoFile)
        {
            model.GruposMenu = DropDownListHelper.p_AEPSAD_MenuGrupos(model.MenuGrupoId);           

            try
            {

                String _error = String.Empty;
                String _icono = String.Empty;

                bool _isFileUploaded = UploadImageHelper.p_AEPSAD_UploadImage(IconoFile, "IconoMenu_" + model.Id, ref _icono, ref _error);

                if (!_isFileUploaded)
                {
                    ModelState.AddModelError("IconoFile", _error);
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                MenuModel _m = new MenuModel
                {
                    Id = model.Id,
                    MenuGrupoId = model.MenuGrupoId,
                    Texto = model.Texto,
                    Enlace = model.Enlace,
                    Icono = _icono,
                    NuevaVentana = (model.NuevaVentana ? Convert.ToByte(1) : Convert.ToByte(0)),
                    Orden = model.Orden
                };


                HttpClient _client = WebApiHelper.p_APESAD_HttpClient(SessionHelper.p_AEPSAD_get_usuario().Token);


                var _json = JsonConvert.SerializeObject(_m);
                var _content = new StringContent(_json.ToString(), Encoding.UTF8, "application/json");

                HttpResponseMessage _response = _client.PutAsync(WebApiHelper.ENDPOINT_MENU_UPDATE, _content).Result;

                if (_response.IsSuccessStatusCode)
                {
                    ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_save_ok('Los datos han sido actualizados');");
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.ReasonPhrase);
                    ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_error('" + _response.ReasonPhrase + "');");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);
                ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_error('" + ex.Message + "');");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> EliminarMenuItems(String id)
        {

            try
            {                
                HttpClient _client = WebApiHelper.p_APESAD_HttpClient(SessionHelper.p_AEPSAD_get_usuario().Token);

                HttpResponseMessage _response = await _client.DeleteAsync(WebApiHelper.ENDPOINT_MENU_DELETE + id);

                if (_response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.StatusCode.ToString());

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Error en el WebApi");
                }
            }
            catch (Exception ex)
            {

                ErrorHelper.p_AEPSAD_Log(ex);

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        #region Grupos Menu

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

                if (_response.IsSuccessStatusCode)
                {
                    ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_save_ok('Los datos han sido actualizados');");
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.ReasonPhrase);
                    ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_error('" + _response.ReasonPhrase + "');");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);
                ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_error('" + ex.Message + "');");
                View(model);
            }

            return View(model);
        }

        public ActionResult CrearGruposMenu()
        {
            MenuGrupo _m = new MenuGrupo
            {
                Modulos = DropDownListHelper.p_AEPSAD_Modulos()
            };

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
                    ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_save_ok('Los datos han sido creados');");
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.ReasonPhrase);
                    ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_error('" + _response.ReasonPhrase + "');");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);
                ViewBag.JavaScriptFunction = string.Format("p_AEPSAD_error('" + ex.Message + "');");
                View(model);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> EliminarGruposMenu(String id)
        {

            try
            {
                //HttpResponseMessage _response = WebApiHelper.p_AEPSAD_Request(WebApiHelper.ENDPOINT_MENU_GRUPOS_DELETE + id);
                HttpClient _client = WebApiHelper.p_APESAD_HttpClient(SessionHelper.p_AEPSAD_get_usuario().Token);

                HttpResponseMessage _response = await _client.DeleteAsync(WebApiHelper.ENDPOINT_MENU_GRUPOS_DELETE + id);

                if (_response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.StatusCode.ToString());

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Error en el WebApi");
                }
            }
            catch (Exception ex)
            {

                ErrorHelper.p_AEPSAD_Log(ex);

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        #endregion
    }

}