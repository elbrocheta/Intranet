using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Usuario
    {
        public String Id { get; set; }

        public String Nombre { get; set; }

        public String Apellidos { get; set; }

        public String Login { get; set; }

        public String Password { get; set; }

        public String Email { get; set; }

        public String Token { get; set; }

        public List<UsuarioGrupos> Groups { get; set; }
    }
}