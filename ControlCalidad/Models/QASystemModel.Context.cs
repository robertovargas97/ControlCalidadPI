﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ControlCalidad.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QASystemEntity : DbContext
    {
        public QASystemEntity()
            : base("name=QASystemEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Empleado> Empleadoes { get; set; }
        public virtual DbSet<Habilidade> Habilidades { get; set; }
        public virtual DbSet<Proyecto> Proyectoes { get; set; }
        public virtual DbSet<Prueba> Pruebas { get; set; }
        public virtual DbSet<Requerimiento> Requerimientoes { get; set; }
        public virtual DbSet<Tester> Testers { get; set; }
        public virtual DbSet<TieneAsignado> TieneAsignadoes { get; set; }
        public virtual DbSet<TrabajaEn> TrabajaEns { get; set; }
    }
}
