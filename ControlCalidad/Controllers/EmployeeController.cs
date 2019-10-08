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
    public class EmployeeController : Controller
    {
        private QASystemEntities db = new QASystemEntities();
        private localizationsController localizations = new localizationsController();



        // GET: Employee
        public async Task<ActionResult> Index()
        {
            var empleadoes = db.Empleadoes.Include(e => e.Tester);
           
            return View(await empleadoes.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = await db.Empleadoes.FindAsync(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.provinces = this.localizations.provinceList();
            ViewBag.cedulaPK = new SelectList(db.Testers, "cedula_empleadoFk", "cedula_empleadoFk");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cedulaPK,nombreP,apellido1,apellido2,fechaNacimiento,edad,telefono,correo,provincia,canton,distrito,direccionExacta,disponibilidad")] Empleado empleado)
        {
            string provinceName = localizations.provinceName(empleado.provincia);
            string cantonName = localizations.cantonName(empleado.provincia, empleado.canton);
            string districtName = localizations.districtName(empleado.provincia, empleado.canton, empleado.distrito);
            empleado.provincia = provinceName;
            empleado.canton = cantonName;
            empleado.distrito = districtName;
            if (ModelState.IsValid)
            {
                db.Empleadoes.Add(empleado);
                await db.SaveChangesAsync();
                return RedirectToAction("../Habilities/Index",new {cedula_empleado = empleado.cedulaPK });
            }

            ViewBag.cedulaPK = new SelectList(db.Testers, "cedula_empleadoFk", "cedula_empleadoFk", empleado.cedulaPK);
            return View(empleado);
        }

        // GET: Employee/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = await db.Empleadoes.FindAsync(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedulaPK = new SelectList(db.Testers, "cedula_empleadoFk", "cedula_empleadoFk", empleado.cedulaPK);
            return View(empleado);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cedulaPK,nombreP,apellido1,apellido2,fechaNacimiento,edad,telefono,correo,provincia,canton,distrito,direccionExacta,disponibilidad")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
  
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaPK = new SelectList(db.Testers, "cedula_empleadoFk", "cedula_empleadoFk", empleado.cedulaPK);
            return View(empleado);
        }

        // GET: Employee/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = await db.Empleadoes.FindAsync(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Empleado empleado = await db.Empleadoes.FindAsync(id);
            db.Empleadoes.Remove(empleado);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveEmployee(string employeeId)
        {
            Empleado employee = db.Empleadoes.Find(employeeId);
            db.Empleadoes.Remove(employee);
            db.SaveChanges();
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

        public List<SelectListItem> GetLeaders()
        {
            string query = "SELECT	E.nombreP, E.apellido1, E.apellido2, E.cedulaPK, E.disponibilidad FROM ControlCalidad.Empleado E WHERE E.cedulaPK NOT IN(SELECT  T.cedula_empleadoFk FROM    ControlCalidad.Tester T) " +
                "AND E.disponibilidad = 'Disponible';";
            List<LeaderForProject> leaderList = db.Database.SqlQuery<LeaderForProject>(query).ToList();

            foreach (LeaderForProject leader in leaderList){
                leader.nombreCompleto = leader.nombreP + " " + leader.apellido1 + " " + leader.apellido2;
            }

            List< SelectListItem > leadersItemList = leaderList.ConvertAll(
                leader => {
                    return new SelectListItem()
                    {
                        Text = leader.nombreCompleto,
                        Value = leader.cedulaPK.ToString(),
                        Selected = false
                    };
                });
            return leadersItemList;
        }

        public void SetLeaderToProject(string cedula_empleadoFK, int idPK, string rol)
        {
            var TrabajaEn = new TrabajaEn
            {
                cedula_empleadoFK = cedula_empleadoFK,
                id_proyectoFK = idPK,
                rol = rol
            };
            var employee = db.Empleadoes.Find(cedula_empleadoFK);
            employee.disponibilidad =  employee.disponibilidad.Replace("Disponible", "No disponible");

            db.TrabajaEns.Add(TrabajaEn);
            db.SaveChangesAsync();
        }
    }
}
