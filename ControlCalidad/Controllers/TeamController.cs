using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlCalidad.Models;

namespace ControlCalidad.Controllers
{

    class DbResultE
    {
        public string cedulaPK { get; set; }
        public string nombreP { get; set; }
    }

    public class TeamController : Controller
    {
        private QASystemEntities db = new QASystemEntities();

        // GET: Team/Edit/5
        public ActionResult Edit(int id_proyecto, string ability)
        {
            //This vector works to accumulate all comma separated abilities and then perform the query
            string[] abilities = null;
            string sqlAbilities= "";
            if (ability != null) {
                abilities = ability.Split(',');
                string template = "H.descripcionPK LIKE '%";
                for (int index = 0; index < abilities.Length; ++index)
                {
                    if (index == 0) {
                        sqlAbilities = template + abilities[index].Trim() + "%'";
                    }
                    else
                    {
                        sqlAbilities += " AND " + template + abilities[index].Trim() + "%'";
                    }
                }
            }
            string sqlp = "SELECT nombre FROM ControlCalidad.Proyecto WHERE idPK=" + id_proyecto;
            string name = db.Database.SqlQuery<string>(sqlp).ToList()[0];
            ViewBag.project_name = name;
            string sql = "SELECT E.cedulaPK, E.nombreP+' '+E.apellido1+' '+E.apellido2 AS 'nombreP' FROM ControlCalidad.Empleado E JOIN ControlCalidad.TrabajaEn T ON T.cedula_empleadoFK = E.cedulaPK WHERE T.id_proyectoFK = " + id_proyecto;
            List<DbResultE> team = db.Database.SqlQuery<DbResultE>(sql).ToList();
            ViewBag.cedula_empleadoFK = new SelectList(team, "cedulaPK", "nombreP", "Equipo");
            string sqle = "SELECT E.cedulaPK , E.nombreP+' '+E.apellido1+' '+E.apellido2 AS 'nombreP' " +
                "FROM ControlCalidad.Empleado E JOIN ControlCalidad.Habilidades H ON H.cedula_empleadoFK = E.cedulaPK " +
                "WHERE "+ sqlAbilities +
                " AND E.disponibilidad = 'Disponible'";
            if (ability == null)
            {
                sqle = " SELECT E.cedulaPK , E.nombreP+' '+E.apellido1+' '+E.apellido2 AS 'nombreP' FROM ControlCalidad.Empleado E WHERE E.disponibilidad = 'Disponible'";
            }
            List<DbResultE> available = db.Database.SqlQuery<DbResultE>(sqle).ToList();
            ViewBag.disponibles = new SelectList(available, "cedulaPK", "nombreP", "Disponibles");
            ViewBag.id_proyecto = id_proyecto;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FormCollection fc, string allocate, string deallocate, string search)
        {
            if (search != null)
            {
                string skill = fc["Ability"];
                return RedirectToAction("Edit", new { id_proyecto = Convert.ToInt32(fc["Project"]), ability = skill });
            }
            string cedulaPK = "";
            int id_proyecto = -1;
            string sql = "";
            int result = -1;
            if (allocate != null)
            {
                cedulaPK = fc["IdNew"];
                id_proyecto = Convert.ToInt32(fc["Project"]);
                sql = "INSERT INTO ControlCalidad.TrabajaEn VALUES('" + cedulaPK + "'," + id_proyecto + ", 'Tester')";
                try
                {
                    result = db.Database.ExecuteSqlCommand(sql);
                    sql = "INSERT INTO ControlCalidad.Tester VALUES('" + cedulaPK + "', 0)";
                    result = db.Database.ExecuteSqlCommand(sql);
                    sql = "UPDATE ControlCalidad.Empleado SET disponibilidad = 'Ocupado' WHERE cedulaPK = '" + cedulaPK + "'";
                    result = db.Database.ExecuteSqlCommand(sql);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                cedulaPK = fc["IdMember"];
                id_proyecto = Convert.ToInt32(fc["Project"]);
                sql = "DELETE FROM ControlCalidad.TrabajaEn WHERE cedula_empleadoFK = '" + cedulaPK + "' AND id_proyectoFK = " + id_proyecto + ";";
                try
                {
                    result = db.Database.ExecuteSqlCommand(sql);
                    sql = "UPDATE ControlCalidad.Empleado SET disponibilidad = 'Disponible' WHERE cedulaPK = '" + cedulaPK + "'";
                    result = db.Database.ExecuteSqlCommand(sql);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return RedirectToAction("../Project/Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}