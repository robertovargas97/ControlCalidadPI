using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControlCalidad.Models;
using System.Threading.Tasks;
using System.Data;

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
        public JsonResult CompletedRequirements(string proy)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<PA_req_terminados_proy_Result> reqList = db.PA_req_terminados_proy(proy).ToList();
            return Json(reqList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RunningRequirements(string proy)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<PA_req_en_ejecucion_proy_Result> reqList = db.PA_req_en_ejecucion_proy(proy).ToList();
            return Json(reqList, JsonRequestBehavior.AllowGet);

        }
        public JsonResult TesterRequirements(string proy)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<PA_cant_req_tester_Result> reqList = db.PA_cant_req_tester(proy).ToList();
            return Json(reqList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ParticipationHistory(string tester)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<PA_historial_participacion_tester_Result> reqList = db.PA_historial_participacion_tester(tester).ToList();
            return Json(reqList, JsonRequestBehavior.AllowGet);

        }
    }
}