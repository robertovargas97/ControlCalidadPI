using ControlCalidad.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlCalidad.Controllers
{
    public class TesterRequirementController : Controller
    {

        private QASystemEntities db = new QASystemEntities( );
        public int? isAssigned(int id)
        {
            int? assigned = 0;
            List<int?> isAssigned =  db.USP_estaAsignado( id ).ToList();
            assigned = isAssigned[ 0 ];
            return assigned;
        }
        public void insert(string cedula_empeladoPK, int id_proyectoFK, int id_requerimientoFK) {
            TieneAsignado newEntity = new TieneAsignado {
                cedula_empleadoFK = cedula_empeladoPK,
                id_requerimientoFK = id_requerimientoFK,
                id_proyectoFK = id_proyectoFK,
                horasDedicas = 0
            };
            db.TieneAsignadoes.Add(newEntity);
            db.SaveChanges();
        }
    }
}