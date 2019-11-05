using ControlCalidad.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlCalidad.Controllers
{
    public class TesterRequirementController : Controller
    {

        private QASystemEntities db = new QASystemEntities( );

        private TestController testController = new TestController( );

        public int? isAssignedToTester(int id)
        {
            int? assigned = 0;
            List<int?> isAssigned = db.USP_estaAsignadoR( id ).ToList();
            assigned = isAssigned[ 0 ];
            return assigned;
        }

        public bool canDelete(int id)
        {
            bool deleted = false;

            if( testController.isAssigned( id ) == false && isAssignedToTester( id ) == 0 )
            {
                deleted = true;
            }

            return deleted;

        }

        public void insert(string cedula_empeladoFK, int? id_proyectoFK)
        {
            TieneAsignado newEntity = new TieneAsignado
            {
                cedula_empleadoFK = cedula_empeladoFK,
                id_requerimientoFK = db.Database.SqlQuery<int>("SELECT [ControlCalidad].[UFN_getId]()").Single(),
                id_proyectoFK = Convert.ToInt32(id_proyectoFK),
                horasDedicas = 0
            };
            db.TieneAsignadoes.Add(newEntity);
            db.SaveChanges();
        }

        public void delete(string cedula_empeladoFK, int? id_proyectoFK, int? id_requerimiento)
        {
            TieneAsignado newEntity = db.TieneAsignadoes.Find(cedula_empeladoFK, Convert.ToInt32(id_proyectoFK), Convert.ToInt32(id_requerimiento));

        }
    }
}