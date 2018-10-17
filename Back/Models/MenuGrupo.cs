using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Back.Models
{
    public class MenuGrupo
    {
        public int Id { get; set; }

        [DisplayName("Módulo")]
        [Required(ErrorMessage = "El módulo es necesario")]
        public int ModuloId { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre es necesario")]
        public string Nombre { get; set; }

        [DisplayName("Orden")]
        [Required(ErrorMessage = "El orden es necesario")]
        [Range(0,int.MaxValue,ErrorMessage = "El campo debe ser numérico")]
        public int Orden { get; set; }
        public IEnumerable<SelectListItem> Modulos { get; set; }
    }
}