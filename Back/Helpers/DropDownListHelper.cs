using Back.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace Back.Helpers
{
    public class DropDownListHelper
    {

        public static SelectList p_AEPSAD_Modulos(int? selected = null)
        {

            IEnumerable<Modulos> _list_modulos = p_AEPSAD_GetModulos();

            if (selected != null)
                return new SelectList(_list_modulos, "Id", "Modulo", selected.ToString());

            return new SelectList(_list_modulos, "Id", "Modulo");
        }

        public static SelectList p_AEPSAD_MenuGrupos(int? selected = null)
        {

            IEnumerable<MenuGrupo> _list_modulos = p_AEPSAD_GetMenuGrupos();

            if (selected != null)
                return new SelectList(_list_modulos, "Id", "Nombre", selected.ToString());

            return new SelectList(_list_modulos, "Id", "Nombre");
        }

        public static SelectList p_AEPSAD_FontAwesomeIconList()
        {
            List<SelectListItem> _list = new List<SelectListItem>
            {                
                new SelectListItem() { Text = "&#xf02d; book solid", Value = "fas fa-book" },
                new SelectListItem() { Text = "&#xf379; Bitcoin brands", Value = "fab fa-bitcoin" },
                new SelectListItem() { Text = "&#xf0f3; bell regular", Value = "far fa-bell" }
            };

            return new SelectList(_list);
        }


        public static IEnumerable<Modulos> p_AEPSAD_GetModulos()
        {
            try
            {
                List<Modulos> _list = new List<Modulos>();
                HttpResponseMessage _response = WebApiHelper.p_AEPSAD_Request(WebApiHelper.ENDPOINT_MODULOS_GETALL);

                if (_response.IsSuccessStatusCode)
                {
                    var _json = _response.Content.ReadAsStringAsync().Result;

                    _list = JsonConvert.DeserializeObject<List<Modulos>>(_json);
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.StatusCode.ToString());
                }

                if (_list == null)
                    return new List<Modulos>();

                return _list;
            }

            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);

                return new List<Modulos>();
            }
        }

        public static IEnumerable<MenuGrupo> p_AEPSAD_GetMenuGrupos()
        {
            try
            {
                List<MenuGrupo> _list = new List<MenuGrupo>();
                HttpResponseMessage _response = WebApiHelper.p_AEPSAD_Request(WebApiHelper.ENDPOINT_MENU_GRUPOS_GETALL);

                if (_response.IsSuccessStatusCode)
                {
                    var _json = _response.Content.ReadAsStringAsync().Result;

                    _list = JsonConvert.DeserializeObject<List<MenuGrupo>>(_json);
                }
                else
                {
                    ErrorHelper.p_AEPSAD_Log(_response.StatusCode.ToString());
                }

                if (_list == null)
                    return new List<MenuGrupo>();

                return _list;
            }

            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);

                return new List<MenuGrupo>();
            }
        }


    }
}