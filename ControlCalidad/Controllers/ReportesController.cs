using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlCalidad.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequerimientosTerminados()
        {
            return View();
        }
        public ActionResult RequerimientosEjecucion()
        {
            return View();
        }
        public ActionResult RequerimientosTester()
        {
            return View();
        }
        public ActionResult HistorialParticipacion()
        {
            return View();
        }
    }
}