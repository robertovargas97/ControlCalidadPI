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

        [DbFunctionAttribute("ControlCalidad", "UFN_getId")]
        public static int? getID()
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        public int? isAssignedToTester(int id)
        {
            int? assigned = 0;
            List<int?> isAssigned =  db.USP_estaAsignado( id ).ToList();
            assigned = isAssigned[ 0 ];
            return assigned;
        }

        public void insert(string cedula_empeladoPK, string id_proyectoFK) {
            TieneAsignado newEntity = new TieneAsignado {
                cedula_empleadoFK = cedula_empeladoPK,
                id_requerimientoFK = getID().GetValueOrDefault(),
                id_proyectoFK = Convert.ToInt32(id_proyectoFK),
                horasDedicas = 0
            };
            db.TieneAsignadoes.Add(newEntity);
            db.SaveChanges();
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

    }
}