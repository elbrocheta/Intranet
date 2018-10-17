using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Back.Models
{

    public class Usuario
    {
        public String Id { get; set; }

        public String Nombre { get; set; }

        public String Apellidos { get; set; }

        [DisplayName("Usuario")]
        [Required(ErrorMessage = "El usuario es necesario")]
        public String Login { get; set; }

        [DisplayName("Clave")]
        [Required(ErrorMessage = "La clave es necesaria")]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public String Email { get; set; }

        public String Token { get; set; }

        public List<UsuarioGrupos> Groups { get; set; }
    }


}
