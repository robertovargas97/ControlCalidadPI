﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ControlCalidad.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class QASystemEntities : DbContext
    {
        public QASystemEntities()
            : base("name=QASystemEntities")
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
        public virtual DbSet<Requerimiento> Requerimientoes { get; set; }
        public virtual DbSet<Tester> Testers { get; set; }
        public virtual DbSet<TieneAsignado> TieneAsignadoes { get; set; }
        public virtual DbSet<TrabajaEn> TrabajaEns { get; set; }
        public virtual DbSet<Prueba> Pruebas { get; set; }
    
        public virtual int USP_calcularEdadEmpleado(string cedula)
        {
            var cedulaParameter = cedula != null ?
                new ObjectParameter("cedula", cedula) :
                new ObjectParameter("cedula", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("USP_calcularEdadEmpleado", cedulaParameter);
        }
    
        public virtual ObjectResult<SP_Conseguir_testers_req_Result> SP_Conseguir_testers_req(Nullable<int> id_proyecto)
        {
            var id_proyectoParameter = id_proyecto.HasValue ?
                new ObjectParameter("id_proyecto", id_proyecto) :
                new ObjectParameter("id_proyecto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Conseguir_testers_req_Result>("SP_Conseguir_testers_req", id_proyectoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> USP_estaAsignado(ObjectParameter id_requerimiento)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("USP_estaAsignado", id_requerimiento);
        }
    
        public virtual int SP_Actualizar_TieneAsignado(Nullable<int> id_proyecto, Nullable<int> id_requerimiento, string id_tester)
        {
            var id_proyectoParameter = id_proyecto.HasValue ?
                new ObjectParameter("id_proyecto", id_proyecto) :
                new ObjectParameter("id_proyecto", typeof(int));
    
            var id_requerimientoParameter = id_requerimiento.HasValue ?
                new ObjectParameter("id_requerimiento", id_requerimiento) :
                new ObjectParameter("id_requerimiento", typeof(int));
    
            var id_testerParameter = id_tester != null ?
                new ObjectParameter("id_tester", id_tester) :
                new ObjectParameter("id_tester", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_Actualizar_TieneAsignado", id_proyectoParameter, id_requerimientoParameter, id_testerParameter);
        }
    
        public virtual ObjectResult<SP_Conseguir_Tester_Result> SP_Conseguir_Tester(Nullable<int> id_proyecto, Nullable<int> id_requerimiento)
        {
            var id_proyectoParameter = id_proyecto.HasValue ?
                new ObjectParameter("id_proyecto", id_proyecto) :
                new ObjectParameter("id_proyecto", typeof(int));
    
            var id_requerimientoParameter = id_requerimiento.HasValue ?
                new ObjectParameter("id_requerimiento", id_requerimiento) :
                new ObjectParameter("id_requerimiento", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Conseguir_Tester_Result>("SP_Conseguir_Tester", id_proyectoParameter, id_requerimientoParameter);
        }

        public virtual ObjectResult<string> SP_Requerimientos_ProPA_req_terminados_proy(string nameProject) {
            ObjectParameter objectParameter = nameProject != null ?
              new ObjectParameter("nameProject", nameProject) :
              new ObjectParameter("nameProject", typeof(string));
            var proj = objectParameter;
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SP_Requerimientos_ProPA_req_terminados_proy", proj);
        }
    }
}
