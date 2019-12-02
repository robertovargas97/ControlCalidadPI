﻿

//------------------------------------------------------------------------------
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
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Canton> Cantons { get; set; }
        public virtual DbSet<Distrito> Distritoes { get; set; }
        public virtual DbSet<Provincia> Provincias { get; set; }
    
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
    
        public virtual int Edit_Empleado(string cedulaAntigua, string cedulaPK, string nombreP, string apellido1, string apellido2, Nullable<System.DateTime> fechaNacimiento, Nullable<int> edad, string telefono, string correo, string provincia, string canton, string distrito, string direccionExacta, string disponibilidad)
        {
            var cedulaAntiguaParameter = cedulaAntigua != null ?
                new ObjectParameter("cedulaAntigua", cedulaAntigua) :
                new ObjectParameter("cedulaAntigua", typeof(string));
    
            var cedulaPKParameter = cedulaPK != null ?
                new ObjectParameter("cedulaPK", cedulaPK) :
                new ObjectParameter("cedulaPK", typeof(string));
    
            var nombrePParameter = nombreP != null ?
                new ObjectParameter("nombreP", nombreP) :
                new ObjectParameter("nombreP", typeof(string));
    
            var apellido1Parameter = apellido1 != null ?
                new ObjectParameter("apellido1", apellido1) :
                new ObjectParameter("apellido1", typeof(string));
    
            var apellido2Parameter = apellido2 != null ?
                new ObjectParameter("apellido2", apellido2) :
                new ObjectParameter("apellido2", typeof(string));
    
            var fechaNacimientoParameter = fechaNacimiento.HasValue ?
                new ObjectParameter("fechaNacimiento", fechaNacimiento) :
                new ObjectParameter("fechaNacimiento", typeof(System.DateTime));
    
            var edadParameter = edad.HasValue ?
                new ObjectParameter("edad", edad) :
                new ObjectParameter("edad", typeof(int));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("telefono", telefono) :
                new ObjectParameter("telefono", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var provinciaParameter = provincia != null ?
                new ObjectParameter("provincia", provincia) :
                new ObjectParameter("provincia", typeof(string));
    
            var cantonParameter = canton != null ?
                new ObjectParameter("canton", canton) :
                new ObjectParameter("canton", typeof(string));
    
            var distritoParameter = distrito != null ?
                new ObjectParameter("distrito", distrito) :
                new ObjectParameter("distrito", typeof(string));
    
            var direccionExactaParameter = direccionExacta != null ?
                new ObjectParameter("direccionExacta", direccionExacta) :
                new ObjectParameter("direccionExacta", typeof(string));
    
            var disponibilidadParameter = disponibilidad != null ?
                new ObjectParameter("disponibilidad", disponibilidad) :
                new ObjectParameter("disponibilidad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Edit_Empleado", cedulaAntiguaParameter, cedulaPKParameter, nombrePParameter, apellido1Parameter, apellido2Parameter, fechaNacimientoParameter, edadParameter, telefonoParameter, correoParameter, provinciaParameter, cantonParameter, distritoParameter, direccionExactaParameter, disponibilidadParameter);
        }
    
        public virtual int Libera_Empleado(Nullable<int> id_proyecto)
        {
            var id_proyectoParameter = id_proyecto.HasValue ?
                new ObjectParameter("id_proyecto", id_proyecto) :
                new ObjectParameter("id_proyecto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Libera_Empleado", id_proyectoParameter);
        }
    
        public virtual ObjectResult<SP_Conseguir_CantReqs_Result> SP_Conseguir_CantReqs(Nullable<int> id_proyecto)
        {
            var id_proyectoParameter = id_proyecto.HasValue ?
                new ObjectParameter("id_proyecto", id_proyecto) :
                new ObjectParameter("id_proyecto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Conseguir_CantReqs_Result>("SP_Conseguir_CantReqs", id_proyectoParameter);
        }
    
        public virtual ObjectResult<PA_cant_req_tester_Result> PA_cant_req_tester(string nombre_proy)
        {
            var nombre_proyParameter = nombre_proy != null ?
                new ObjectParameter("nombre_proy", nombre_proy) :
                new ObjectParameter("nombre_proy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_cant_req_tester_Result>("PA_cant_req_tester", nombre_proyParameter);
        }
    
        public virtual ObjectResult<PA_historial_participacion_tester_Result> PA_historial_participacion_tester(string nombre_tester)
        {
            var nombre_testerParameter = nombre_tester != null ?
                new ObjectParameter("nombre_tester", nombre_tester) :
                new ObjectParameter("nombre_tester", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_historial_participacion_tester_Result>("PA_historial_participacion_tester", nombre_testerParameter);
        }
    
        public virtual ObjectResult<PA_req_en_ejecucion_proy_Result> PA_req_en_ejecucion_proy(string nombre_proy)
        {
            var nombre_proyParameter = nombre_proy != null ?
                new ObjectParameter("nombre_proy", nombre_proy) :
                new ObjectParameter("nombre_proy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_req_en_ejecucion_proy_Result>("PA_req_en_ejecucion_proy", nombre_proyParameter);
        }
    
        public virtual ObjectResult<PA_req_terminados_proy_Result> PA_req_terminados_proy(string nombre_proy)
        {
            var nombre_proyParameter = nombre_proy != null ?
                new ObjectParameter("nombre_proy", nombre_proy) :
                new ObjectParameter("nombre_proy", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_req_terminados_proy_Result>("PA_req_terminados_proy", nombre_proyParameter);
        }
    
        public virtual int Edit_Cliente(string cedulaAntigua, string cedulaPK, string nombreP, string apellido1, string apellido2, string telefono, string correo, string provincia, string canton, string distrito, string direccionExacta, Nullable<System.DateTime> fechaNacimiento)
        {
            var cedulaAntiguaParameter = cedulaAntigua != null ?
                new ObjectParameter("cedulaAntigua", cedulaAntigua) :
                new ObjectParameter("cedulaAntigua", typeof(string));
    
            var cedulaPKParameter = cedulaPK != null ?
                new ObjectParameter("cedulaPK", cedulaPK) :
                new ObjectParameter("cedulaPK", typeof(string));
    
            var nombrePParameter = nombreP != null ?
                new ObjectParameter("nombreP", nombreP) :
                new ObjectParameter("nombreP", typeof(string));
    
            var apellido1Parameter = apellido1 != null ?
                new ObjectParameter("apellido1", apellido1) :
                new ObjectParameter("apellido1", typeof(string));
    
            var apellido2Parameter = apellido2 != null ?
                new ObjectParameter("apellido2", apellido2) :
                new ObjectParameter("apellido2", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("telefono", telefono) :
                new ObjectParameter("telefono", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("correo", correo) :
                new ObjectParameter("correo", typeof(string));
    
            var provinciaParameter = provincia != null ?
                new ObjectParameter("provincia", provincia) :
                new ObjectParameter("provincia", typeof(string));
    
            var cantonParameter = canton != null ?
                new ObjectParameter("canton", canton) :
                new ObjectParameter("canton", typeof(string));
    
            var distritoParameter = distrito != null ?
                new ObjectParameter("distrito", distrito) :
                new ObjectParameter("distrito", typeof(string));
    
            var direccionExactaParameter = direccionExacta != null ?
                new ObjectParameter("direccionExacta", direccionExacta) :
                new ObjectParameter("direccionExacta", typeof(string));
    
            var fechaNacimientoParameter = fechaNacimiento.HasValue ?
                new ObjectParameter("fechaNacimiento", fechaNacimiento) :
                new ObjectParameter("fechaNacimiento", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Edit_Cliente", cedulaAntiguaParameter, cedulaPKParameter, nombrePParameter, apellido1Parameter, apellido2Parameter, telefonoParameter, correoParameter, provinciaParameter, cantonParameter, distritoParameter, direccionExactaParameter, fechaNacimientoParameter);
        }
    
        public virtual ObjectResult<Req_Lider_Result> Req_Lider(string cedula)
        {
            var cedulaParameter = cedula != null ?
                new ObjectParameter("cedula", cedula) :
                new ObjectParameter("cedula", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Req_Lider_Result>("Req_Lider", cedulaParameter);
        }
    
        public virtual ObjectResult<string> nombresLiderers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("nombresLiderers");
        }
    
        public virtual ObjectResult<nombreLideres_Result> nombreLideres()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<nombreLideres_Result>("nombreLideres");
        }
    
        public virtual int USP_consultaObtenerPruebas(Nullable<int> projectId, Nullable<int> requirementId, ObjectParameter testerName, ObjectParameter testName, ObjectParameter result, ObjectParameter successfulTests, ObjectParameter failedTests, ObjectParameter totalTests)
        {
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("projectId", projectId) :
                new ObjectParameter("projectId", typeof(int));
    
            var requirementIdParameter = requirementId.HasValue ?
                new ObjectParameter("requirementId", requirementId) :
                new ObjectParameter("requirementId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("USP_consultaObtenerPruebas", projectIdParameter, requirementIdParameter, testerName, testName, result, successfulTests, failedTests, totalTests);
        }
    
        public virtual ObjectResult<SP_Req_Lider_Result> SP_Req_Lider(string cedula, string complejidad)
        {
            var cedulaParameter = cedula != null ?
                new ObjectParameter("cedula", cedula) :
                new ObjectParameter("cedula", typeof(string));
    
            var complejidadParameter = complejidad != null ?
                new ObjectParameter("complejidad", complejidad) :
                new ObjectParameter("complejidad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Req_Lider_Result>("SP_Req_Lider", cedulaParameter, complejidadParameter);
        }
    
        public virtual ObjectResult<USP_Detalles_Horas_Req_Proyecto_Result> USP_Detalles_Horas_Req_Proyecto(Nullable<int> id_proyecto)
        {
            var id_proyectoParameter = id_proyecto.HasValue ?
                new ObjectParameter("id_proyecto", id_proyecto) :
                new ObjectParameter("id_proyecto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_Detalles_Horas_Req_Proyecto_Result>("USP_Detalles_Horas_Req_Proyecto", id_proyectoParameter);
        }
    
        public virtual ObjectResult<SP_Conseguir_Datos_Lider_Result> SP_Conseguir_Datos_Lider()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Conseguir_Datos_Lider_Result>("SP_Conseguir_Datos_Lider");
        }
    
        public virtual ObjectResult<SP_Conseguir_Datos_Tester_Result> SP_Conseguir_Datos_Tester()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Conseguir_Datos_Tester_Result>("SP_Conseguir_Datos_Tester");
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<USP_obtenerDatosPruebas_Result> USP_obtenerDatosPruebas(Nullable<int> projectId, Nullable<int> requirementId)
        {
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("projectId", projectId) :
                new ObjectParameter("projectId", typeof(int));
    
            var requirementIdParameter = requirementId.HasValue ?
                new ObjectParameter("requirementId", requirementId) :
                new ObjectParameter("requirementId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_obtenerDatosPruebas_Result>("USP_obtenerDatosPruebas", projectIdParameter, requirementIdParameter);
        }
    
        public virtual ObjectResult<SP_consultaObtenerPruebas_Result> SP_consultaObtenerPruebas(Nullable<int> projectId, Nullable<int> requirementId)
        {
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("projectId", projectId) :
                new ObjectParameter("projectId", typeof(int));
    
            var requirementIdParameter = requirementId.HasValue ?
                new ObjectParameter("requirementId", requirementId) :
                new ObjectParameter("requirementId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_consultaObtenerPruebas_Result>("SP_consultaObtenerPruebas", projectIdParameter, requirementIdParameter);
        }
    
        public virtual ObjectResult<SP_consultaObtenerPruebas1_Result> SP_consultaObtenerPruebas1(Nullable<int> projectId, Nullable<int> requirementId)
        {
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("projectId", projectId) :
                new ObjectParameter("projectId", typeof(int));
    
            var requirementIdParameter = requirementId.HasValue ?
                new ObjectParameter("requirementId", requirementId) :
                new ObjectParameter("requirementId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_consultaObtenerPruebas1_Result>("SP_consultaObtenerPruebas1", projectIdParameter, requirementIdParameter);
        }
    
        public virtual ObjectResult<SP_obtenerDatosPruebas_Result> SP_obtenerDatosPruebas(Nullable<int> projectId, Nullable<int> requirementId)
        {
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("projectId", projectId) :
                new ObjectParameter("projectId", typeof(int));
    
            var requirementIdParameter = requirementId.HasValue ?
                new ObjectParameter("requirementId", requirementId) :
                new ObjectParameter("requirementId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_obtenerDatosPruebas_Result>("SP_obtenerDatosPruebas", projectIdParameter, requirementIdParameter);
        }
    
        public virtual ObjectResult<USP_comparacionHorasTester_Result> USP_comparacionHorasTester(string cedulaTester)
        {
            var cedulaTesterParameter = cedulaTester != null ?
                new ObjectParameter("cedulaTester", cedulaTester) :
                new ObjectParameter("cedulaTester", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_comparacionHorasTester_Result>("USP_comparacionHorasTester", cedulaTesterParameter);
        }
    
        public virtual ObjectResult<USP_obtenerTestersExistentes_Result> USP_obtenerTestersExistentes()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_obtenerTestersExistentes_Result>("USP_obtenerTestersExistentes");
        }
    
        public virtual ObjectResult<SP_Conseguir_Datos_Testers_Result> SP_Conseguir_Datos_Testers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Conseguir_Datos_Testers_Result>("SP_Conseguir_Datos_Testers");
        }
    
        public virtual ObjectResult<USP_comparacionHorasTesters_Result> USP_comparacionHorasTesters(string cedulaTester)
        {
            var cedulaTesterParameter = cedulaTester != null ?
                new ObjectParameter("cedulaTester", cedulaTester) :
                new ObjectParameter("cedulaTester", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_comparacionHorasTesters_Result>("USP_comparacionHorasTesters", cedulaTesterParameter);
        }
    
        public virtual ObjectResult<SP_Requeirmientos_Lider_Result> SP_Requeirmientos_Lider(string cedula, string complejidad)
        {
            var cedulaParameter = cedula != null ?
                new ObjectParameter("cedula", cedula) :
                new ObjectParameter("cedula", typeof(string));
    
            var complejidadParameter = complejidad != null ?
                new ObjectParameter("complejidad", complejidad) :
                new ObjectParameter("complejidad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Requeirmientos_Lider_Result>("SP_Requeirmientos_Lider", cedulaParameter, complejidadParameter);
        }
    
        public virtual ObjectResult<SP_Disponibilidad_Tester_Result> SP_Disponibilidad_Tester(string disponibilidad)
        {
            var disponibilidadParameter = disponibilidad != null ?
                new ObjectParameter("disponibilidad", disponibilidad) :
                new ObjectParameter("disponibilidad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Disponibilidad_Tester_Result>("SP_Disponibilidad_Tester", disponibilidadParameter);
        }
    
        public virtual ObjectResult<USP_Disponibilidad_Tester_Result> USP_Disponibilidad_Tester(string disponibilidad)
        {
            var disponibilidadParameter = disponibilidad != null ?
                new ObjectParameter("disponibilidad", disponibilidad) :
                new ObjectParameter("disponibilidad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_Disponibilidad_Tester_Result>("USP_Disponibilidad_Tester", disponibilidadParameter);
        }
    
        public virtual ObjectResult<USP_Disp_Tester_Result> USP_Disp_Tester(string disponibilidad)
        {
            var disponibilidadParameter = disponibilidad != null ?
                new ObjectParameter("disponibilidad", disponibilidad) :
                new ObjectParameter("disponibilidad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_Disp_Tester_Result>("USP_Disp_Tester", disponibilidadParameter);
        }
    
        public virtual ObjectResult<SP_TesterOcupado_Result> SP_TesterOcupado()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_TesterOcupado_Result>("SP_TesterOcupado");
        }
    
        public virtual ObjectResult<USP_buscarPorHabilidad_Result> USP_buscarPorHabilidad(string categoria, string habilidad)
        {
            var categoriaParameter = categoria != null ?
                new ObjectParameter("categoria", categoria) :
                new ObjectParameter("categoria", typeof(string));
    
            var habilidadParameter = habilidad != null ?
                new ObjectParameter("habilidad", habilidad) :
                new ObjectParameter("habilidad", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_buscarPorHabilidad_Result>("USP_buscarPorHabilidad", categoriaParameter, habilidadParameter);
        }
    
        public virtual ObjectResult<string> USP_obtenerHabilidadesPorCategoria(string categoria)
        {
            var categoriaParameter = categoria != null ?
                new ObjectParameter("categoria", categoria) :
                new ObjectParameter("categoria", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("USP_obtenerHabilidadesPorCategoria", categoriaParameter);
        }
    
     
    
        public virtual ObjectResult<USP_obtenerReqsAsignados1_Result> USP_obtenerReqsAsignados1(Nullable<int> projectId)
        {
            var projectIdParameter = projectId.HasValue ?
                new ObjectParameter("projectId", projectId) :
                new ObjectParameter("projectId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USP_obtenerReqsAsignados1_Result>("USP_obtenerReqsAsignados1", projectIdParameter);
        }
  

        public virtual ObjectResult<PA_cant_req_tester_nuevo_Result> PA_cant_req_tester_nuevo(string nombre_proy)
        {

            var nombre_proyParameter = nombre_proy != null ?
                new ObjectParameter("nombre_proy", nombre_proy) :
                new ObjectParameter("nombre_proy", typeof(string));


            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_cant_req_tester_nuevo_Result>("PA_cant_req_tester_nuevo", nombre_proyParameter);
        }


        public virtual ObjectResult<PA_historial_participacion_tester_nuevo_Result> PA_historial_participacion_tester_nuevo(string nombre_tester)
        {

            var nombre_testerParameter = nombre_tester != null ?
                new ObjectParameter("nombre_tester", nombre_tester) :
                new ObjectParameter("nombre_tester", typeof(string));


            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PA_historial_participacion_tester_nuevo_Result>("PA_historial_participacion_tester_nuevo", nombre_testerParameter);
        }
    }
}


