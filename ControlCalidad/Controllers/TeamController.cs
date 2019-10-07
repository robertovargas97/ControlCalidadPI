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
    class DbResultP
    {
        public int idPK { get; set; }
        public string nombre { get; set; }
    }

    class DbResultE
    {
        public string cedulaPK { get; set; }
        public string nombreP { get; set; }
    }

    public class TeamController : Controller
    {
        private QASystemEntities db = new QASystemEntities();
        private S3G4CEntity userC = new S3G4CEntity();
        // GET: Team
        public async Task<ActionResult> Index()
        {

            var trabajaEns = db.TrabajaEns.Include(t => t.Empleado).Include(t => t.Proyecto);
            return View(await trabajaEns.ToListAsync());
        }
        /*
        public async Task<ActionResult> Add(string cedulaPK, int id_proyecto)
        {

            string sql = "INSERT INTO ControlCalidad.TrabajaEn VALUES('" + cedulaPK + "'," + id_proyecto + ", 'Tester')";
            _ = db.Database.ExecuteSqlCommand(sql);
            return RedirectToAction("Index");
        }
        */
        public async Task<ActionResult> EditTeam(string id_proyecto, string id_empleado)
        {
            if (id_proyecto == null || id_empleado == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrabajaEn trabajaEn = await db.TrabajaEns.FindAsync(id_proyecto, id_empleado);
            if (trabajaEn == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", trabajaEn.cedula_empleadoFK);
            ViewBag.id_proyectoFK = new SelectList(db.Proyectoes, "idPK", "nombre", trabajaEn.id_proyectoFK);
            return View(trabajaEn);
        }


        // GET: Team/Details/5
        public async Task<ActionResult> Details(int id_proyecto)
        {
            //e.AspNetUsers.
            var trabajaEns = db.TrabajaEns.Include(t => t.Empleado).Where(t => t.id_proyectoFK == id_proyecto);
            return View(await trabajaEns.ToListAsync());
        }

        // GET: Team/Create
        public ActionResult Create(string email)
        {
            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes.Where(e => e.disponibilidad == "Disponible"), "cedulaPK", "nombreP");
            string sqlp = "SELECT P.idPK , P.nombre FROM ControlCalidad.Proyecto P JOIN ControlCalidad.TrabajaEn T ON	P.idPK = T.id_proyectoFK JOIN ControlCalidad.Empleado E ON E.cedulaPK = T.cedula_empleadoFK WHERE E.correo = '"+email+"'";
            List<DbResultP> resultp = db.Database.SqlQuery<DbResultP>(sqlp).ToList();
            ViewBag.id_proyectoFK = new SelectList(resultp, "idPK", "nombre");
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cedula_empleadoFK,id_proyectoFK,rol")] TrabajaEn trabajaEn, string correo)
        {
            if (ModelState.IsValid)
            {
                string cedula = trabajaEn.cedula_empleadoFK;
                string sql = "UPDATE ControlCalidad.Empleado SET disponibilidad = 'Ocupado' WHERE cedulaPK = '" + cedula + "'";
                db.Database.ExecuteSqlCommand(sql);
                db.TrabajaEns.Add(trabajaEn);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            string sqle = "SELECT E.cedulaPK, E.nombreP FROM ControlCalidad.Empleado E JOIN ControlCalidad.Tester T ON E.cedulaPK = T.cedula_empleadoFk WHERE E.disponibilidad = 'Disponible'";
            List<DbResultE> resulte = db.Database.SqlQuery<DbResultE>(sqle).ToList();
            ViewBag.cedula_empleadoFk = new SelectList(resulte, "cedulaPK", "nombreP");
            string sqlp = "SELECT P.idPK, P.nombre FROM ControlCalidad.Proyecto P JOIN ControlCalidad.TrabajaEn T ON	P.idPK = T.id_proyectoFK JOIN ControlCalidad.Empleado E ON E.cedulaPK = T.cedula_empleadoFK WHERE E.correo = '"+correo+"'";
            List<DbResultP> resultp = db.Database.SqlQuery<DbResultP>(sqlp).ToList();
            ViewBag.id_proyectoFK = new SelectList(resultp, "idPK", "nombre");
            return View(trabajaEn);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int id_proyecto, string hability)
        {
            string sql = "SELECT E.cedulaPK, E.nombreP FROM ControlCalidad.Empleado E JOIN ControlCalidad.TrabajaEn T ON T.cedula_empleadoFK = E.cedulaPK WHERE T.id_proyectoFK = " + id_proyecto;
            List<DbResultE> team = db.Database.SqlQuery<DbResultE>(sql).ToList();
            ViewBag.cedula_empleadoFK = new SelectList(team, "cedulaPK", "nombreP", "Equipo");
            string sqle = "SELECT E.cedulaPK , E.nombreP FROM ControlCalidad.Empleado E JOIN ControlCalidad.Habilidades H ON H.cedula_empleadoFK = E.cedulaPK WHERE H.descripcionPK LIKE '%" + hability + "%' AND E.disponibilidad = 'Disponible'";
            if (hability == null)
            {
                sqle = " SELECT E.cedulaPK , E.nombreP FROM ControlCalidad.Empleado E WHERE E.disponibilidad = 'Disponible'";
            }
            List<DbResultE> disponibles = db.Database.SqlQuery<DbResultE>(sqle).ToList();
            ViewBag.disponibles = new SelectList(disponibles, "cedulaPK", "nombreP", "Disponibles");
            ViewBag.id_proyecto = id_proyecto;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FormCollection fc, string asignar, string desasignar, string buscar)
        {
            if (buscar != null) {
                string skill = fc["Hability"];
                return RedirectToAction("Edit", new { id_proyecto = Convert.ToInt32(fc["Proyecto"]), hability = skill });
            }
            string cedulaPK = "";
            int id_proyecto = -1;
            string sql = "";
            int result = -1;
            if (asignar != null)
            {
                cedulaPK = fc["CedulaN"];
                id_proyecto = Convert.ToInt32(fc["Proyecto"]);
                sql = "INSERT INTO ControlCalidad.TrabajaEn VALUES('" + cedulaPK + "'," + id_proyecto + ", 'Tester')";
                try {
                    result = db.Database.ExecuteSqlCommand(sql);
                    sql = "UPDATE ControlCalidad.Empleado SET disponibilidad = 'Ocupado' WHERE cedulaPK = '" + cedulaPK + "'";
                    result = db.Database.ExecuteSqlCommand(sql);
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            }
            else {
                cedulaPK = fc["CedulaT"];
                id_proyecto = Convert.ToInt32(fc["Proyecto"]);
                sql = "DELETE FROM ControlCalidad.TrabajaEn WHERE cedula_empleadoFK = '" + cedulaPK + "' AND id_proyectoFK = " + id_proyecto + ";";
                try {
                    result = db.Database.ExecuteSqlCommand(sql);
                    sql = "UPDATE ControlCalidad.Empleado SET disponibilidad = 'Disponible' WHERE cedulaPK = '" + cedulaPK + "'";
                    result = db.Database.ExecuteSqlCommand(sql);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return RedirectToAction("Index");
        }


        // GET: Team/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrabajaEn trabajaEn = await db.TrabajaEns.FindAsync(id);
            if (trabajaEn == null)
            {
                return HttpNotFound();
            }
            return View(trabajaEn);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id_proyecto, string id_empleado)
        {
            TrabajaEn trabajaEn = await db.TrabajaEns.FindAsync(id_proyecto);
            db.TrabajaEns.Remove(trabajaEn);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
