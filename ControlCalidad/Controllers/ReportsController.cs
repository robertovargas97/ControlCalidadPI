﻿using System;
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
        private TesterController testerController = new TesterController( );

        // GET: Reports
        public ActionResult Index()
        {
            ViewBag.allprojects = projectController.GetProjects();
            ViewBag.alltesters = employeeController.GetTesters();
            ViewBag.allLeaders = LeadersList();
            ViewBag.finishedProjects = projectController.GetFinishedProjects( );
            ViewBag.allTesters = testerController.getAllTesters( );
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

        public JsonResult leaderRequirementsStatistics(string leaderId,string complexity)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SP_Requeirmientos_Lider_Result> reqs = db.SP_Requeirmientos_Lider(leaderId, complexity).ToList();
            return Json(reqs, JsonRequestBehavior.AllowGet);
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
            List<SP_obtenerDatosPruebas_Result> testsReportTable = db.SP_obtenerDatosPruebas(projectId, requirementId).ToList( );
            return Json( testsReportTable , JsonRequestBehavior.AllowGet );
        }

        //<summary> : Used to get information about the requirements assigned to a tester in specific
        //<params>  : employeeId  : represents the tester identifier
        //<return>  : Returns a Json with the results of SP
        public JsonResult testerRequirementsHours( string employeeId )
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<USP_comparacionHorasTesters_Result> projectInformation = db.USP_comparacionHorasTesters( employeeId ).ToList( );
            return Json( projectInformation , JsonRequestBehavior.AllowGet );
        }

        public JsonResult availableTesters(string availability)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SP_Disponibilidad_Tester_Result> availableTesters = db.SP_Disponibilidad_Tester(availability).ToList();
            return Json(availableTesters, JsonRequestBehavior.AllowGet);
        }
    }
}