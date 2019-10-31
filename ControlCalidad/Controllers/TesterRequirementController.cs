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

    }
}