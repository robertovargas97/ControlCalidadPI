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

        //Controllers to get values from other models
        private ProjectController projectController = new ProjectController();
        private EmployeeController employeeController = new EmployeeController();
        private QASystemEntities db = new QASystemEntities();

        // GET: Reportes
        public ActionResult Index(string proy)
        {
            ViewBag.allprojects = projectController.GetProjects();
            ViewBag.alltesters = employeeController.GetTesters();

            string query = "EXEC PA_req_terminados_proy" + "'" + "Nuevo PA" + "'";
            var tempEnded = (db.Database.SqlQuery<Reqterminados>(query)).ToList();

            return View(tempEnded);
        }

        public ActionResult CompletedRequirements()
        {
            return View();
        }
        public ActionResult RunningRequirements()
        {
            return View();
        }
        public ActionResult TesterRequirements()
        {
            return View();
        }
        public ActionResult ParticipationHistory()
        {
            return View();
        }
    }
}