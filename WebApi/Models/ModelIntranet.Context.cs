﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PruebasIntranet2019Entities : DbContext
    {
        public PruebasIntranet2019Entities()
            : base("name=PruebasIntranet2019Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Noticias> Noticias { get; set; }
        public virtual DbSet<NoticiasTipo> NoticiasTipo { get; set; }
        public virtual DbSet<MenuGrupo> MenuGrupo { get; set; }
        public virtual DbSet<Modulos> Modulos { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<vBack_GruposMenuList> vBack_GruposMenuList { get; set; }
        public virtual DbSet<vFront_Menu> vFront_Menu { get; set; }
    }
}
