using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Back.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int MenuGrupoId { get; set; }
        public IEnumerable<SelectListItem> GruposMenu { get; set; }
        public string Texto { get; set; }
        public string Enlace { get; set; }
        public string Icono { get; set; }       
        public bool NuevaVentana { get; set; }
        public int Orden { get; set; }
    }

    public class MenuModel
    {
        public int Id { get; set; }
        public int MenuGrupoId { get; set; }
        public string Texto { get; set; }
        public string Enlace { get; set; }
        public string Icono { get; set; }
        public byte NuevaVentana { get; set; }
        public int Orden { get; set; }
    }
}