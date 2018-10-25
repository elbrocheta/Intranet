namespace Back.Models
{
    public class vBack_Menu
    {
        public string MenuGrupo { get; set; }
        public int ModuloId { get; set; }
        public int Id { get; set; }
        public int MenuGrupoId { get; set; }
        public string Texto { get; set; }
        public string Enlace { get; set; }
        public string Icono { get; set; }
        public bool NuevaVentana { get; set; }
        public int Orden { get; set; }
        public string Modulo { get; set; }
    }
}