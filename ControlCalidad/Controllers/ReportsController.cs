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
        private RequirementController requirementController = new RequirementController();

        // GET: Reports
        public ActionResult Index()
        {
            ViewBag.allprojects = projectController.GetProjects();
            ViewBag.alltesters = employeeController.GetTesters();
            ViewBag.allLeaders = LeadersList();
            ViewBag.finishedProjects = projectController.GetFinishedProjects( );
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

        public List<SelectListItem> LeadersList()
        {
            List<nombreLideres_Result> leader = db.nombreLideres().ToList();

            List<SelectListItem> leaderList = leader.ConvertAll(leaders => {
                return new SelectListItem()
                {
                    Text = leaders.nombreP,
                    Value = leaders.cedulaPK,
                    Selected = false
                };
            });
            return leaderList;
        }

        public void leaderRequirementsStatistics(string leaderId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SP_Req_Lider_Result> reqs = db.SP_Req_Lider(leaderId).ToList();
        }

        //<summary> : Used to get information about finished projects its hours and requirements
        //<params>  : projectId : represents the project identifier
        //<return>  : Returns a Json with the results of SP
        public JsonResult ProjectRequirementHours( int projectId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<USP_Detalles_Horas_Req_Proyecto_Result> projectInformation = db.USP_Detalles_Horas_Req_Proyecto( projectId ).ToList( );
            return Json( projectInformation , JsonRequestBehavior.AllowGet );
        }

        public JsonResult testsResults(int projectId, int requirementId) {

            db.Configuration.ProxyCreationEnabled = false;
            List<SP_consultaObtenerPruebas_Result> testsReportTable = db.SP_consultaObtenerPruebas(projectId, requirementId).ToList( );
            return Json( testsReportTable , JsonRequestBehavior.AllowGet );

        }
    }
}