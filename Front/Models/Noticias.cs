using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front.Models
{
    public class Noticias
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public string UsuarioAlta { get; set; }
        public int NoticiaTipo { get; set; }
        public Nullable<int> MostrarEnInicio { get; set; }

        public virtual NoticiasTipo NoticiasTipo { get; set; }
    }
}