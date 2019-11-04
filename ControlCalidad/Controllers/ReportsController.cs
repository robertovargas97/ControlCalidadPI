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

        // GET: Reportes
        public ActionResult Index( string x)
        {
            ViewBag.allprojects = projectController.GetProjects();
            ViewBag.alltesters = employeeController.GetTesters();
            return View();
        }

        public ActionResult CompletedRequirements(string x)
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