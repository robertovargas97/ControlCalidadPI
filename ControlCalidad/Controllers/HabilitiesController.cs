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
    public class HabilitiesController : Controller
    {
        private QASystemEntities db = new QASystemEntities();

        // GET: Habilities
        public async Task<ActionResult> Index()
        {
            var habilidades = db.Habilidades.Include(h => h.Empleado);
            return View(await habilidades.ToListAsync());
        }

        // GET: Habilities/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habilidade habilidade = await db.Habilidades.FindAsync(id);
            if (habilidade == null)
            {
                return HttpNotFound();
            }
            return View(habilidade);
        }

        // GET: Habilities/Create
        public ActionResult Create()
        {
            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP");
            return View();
        }

        // POST: Habilities/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cedula_empleadoFK,categoriaPK,descripcionPK")] Habilidade habilidade)
        {
            if (ModelState.IsValid)
            {
                db.Habilidades.Add(habilidade);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", habilidade.cedula_empleadoFK);
            return View(habilidade);
        }

        // GET: Habilities/Edit/5
        public async Task<ActionResult> Edit(string cedula_empleadoFK, string categoriaPK, string descripcionPK)
        {
            if (cedula_empleadoFK == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habilidade habilidade = await db.Habilidades.FindAsync(cedula_empleadoFK,categoriaPK,descripcionPK);
            if (habilidade == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", habilidade.cedula_empleadoFK);
            return View(habilidade);
        }

        // POST: Habilities/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cedula_empleadoFK,categoriaPK,descripcionPK")] Habilidade habilidad)
        {
            if (ModelState.IsValid)
            {
                var sql =
                    from a in db.Habilidades
                    where a.cedula_empleadoFK == habilidad.cedula_empleadoFK &&
                    a.categoriaPK == habilidad.categoriaPK
                    select a;
                
                foreach (var a in sql)
                {
                    if (habilidad.descripcionPK.Contains(a.descripcionPK) || a.descripcionPK.Contains(habilidad.descripcionPK))
                    {
                        db.Habilidades.Remove(a);
                        break;
                    }
                }

                try
                {
                   await db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // Provide for exceptions.
                }
                
                db.Habilidades.Add(habilidad);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", habilidad.cedula_empleadoFK);
            return View(habilidad);
        }

        // GET: Habilities/Delete/5
        public async Task<ActionResult> Delete(string cedula_empleadoFK, string categoriaPK, string descripcionPK)
        {
            if (cedula_empleadoFK == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habilidade habilidade = await db.Habilidades.FindAsync(cedula_empleadoFK,  categoriaPK,  descripcionPK);
            if (habilidade == null)
            {
                return HttpNotFound();
            }
            return View(habilidade);
        }

        // POST: Habilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string cedula_empleadoFK, string categoriaPK, string descripcionPK)
        {
            Habilidade habilidade = await db.Habilidades.FindAsync(cedula_empleadoFK, categoriaPK, descripcionPK);
            db.Habilidades.Remove(habilidade);
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
