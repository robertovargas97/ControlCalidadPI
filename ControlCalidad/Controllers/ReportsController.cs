﻿using System;
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
            string queryReq2 = "EXEC PA_req_en_ejecucion_proy" + "'" + "Proyecto Sebas" + "'";
            var tempEjec = db.Database.SqlQuery<Reports>(queryReq2).ToList();
            return View(tempEjec);
        }

        //<summary> : This method is used to call the store procedure "PA_req_terminados_proy".
        //<params>  : input : It's the proy needed to make the consult.
        //<return>  : Returns a list.
        public List<Reports> CompletedRequirements(string proy)
        {

            string queryReq = "EXEC PA_req_terminados_proy" + "'" + proy + "'";
            var tempEnded = db.Database.SqlQuery<Reports>(queryReq).ToList();
            return tempEnded;

        }

        public List<Reports> RunningRequirements(string proy)
        {
            string queryReq2 = "EXEC PA_req_en_ejecucion_proy" + "'" + proy + "'";
            var tempEjec = db.Database.SqlQuery<Reports>(queryReq2).ToList();
            return tempEjec;
        }

        public List<Reports> TesterRequirements(string proy)
        {
            string queryCant = "EXEC PA_cant_req_tester" + "'" + proy + "'";
            var tempCant = db.Database.SqlQuery<Reports>(queryCant).ToList();
            return tempCant;
        }

        public List<Reports> ParticipationHistory(string tester)
        {
            string queryHist = "EXEC PA_historial_participacion_tester" + "'" + tester + "'";
            var tempHist = db.Database.SqlQuery<Reports>(queryHist).ToList();
            return tempHist;
        }
    }
}