using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControlCalidad.Models;
using System.Threading.Tasks;

namespace ControlCalidad.Controllers
{

    public class ReportsController : Controller
    {
        private QASystemEntities db = new QASystemEntities();

        //Controllers to get values from other models
        private ProjectController projectController = new ProjectController();
        private EmployeeController employeeController = new EmployeeController();

        // GET: Reports
        public ActionResult Index()
        {
            ViewBag.allprojects = projectController.GetProjects();
            ViewBag.alltesters = employeeController.GetTesters();
            return View();
        }

        //<summary> : This method is used to call the store procedure "PA_req_terminados_proy".
        //<params>  : input : It's the proy needed to make the consult.
        //<return>  : Returns a list.
        public string CompletedRequirements(string proy)
        {
            ModelState.AddModelError("", "Proyecto: " + proy);
            //return Json(db.PA_req_terminados_proy(proy));
            return proy;

        }
        public JsonResult RunningRequirements(string proy)
        {
            return Json(db.PA_req_en_ejecucion_proy(proy));

        }
        public JsonResult TesterRequirements(string proy)
        {
            return Json(db.PA_cant_req_tester(proy));

        }
        public JsonResult ParticipationHistory(string proy)
        {
            return Json(db.PA_historial_participacion_tester(proy));

        }
    }
}