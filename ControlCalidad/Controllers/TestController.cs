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
    public class TestController : Controller
    {
        private QASystemEntities db = new QASystemEntities();

        // GET: Test
        public async Task<ActionResult> Index()
        {
            var pruebas = db.Pruebas.Include(p => p.Requerimiento);
            return View(await pruebas.ToListAsync());
        }

        // GET: Test/Details/5
        public async Task<ActionResult> Details(int? id, int? projectID, int? requirementID)
        {
            
            if (id == null || projectID == null || requirementID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prueba prueba = await db.Pruebas.FindAsync( id,  projectID,  requirementID);
            if (prueba == null)
            {
                return HttpNotFound();
            }
            return View(prueba);
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            ViewBag.id_requerimientoFK = new SelectList(db.Requerimientoes, "idPK", "nombre");
            return View();
        }

        // POST: Test/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idPK, id_proyectoFK, id_requerimientoFK, nombre, detalleResultado, resultadoFinal")] Prueba prueba)
        {
            if (ModelState.IsValid)
            {
                db.Pruebas.Add(prueba);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_requerimientoFK = new SelectList(db.Requerimientoes, "idPK", "nombre", prueba.id_requerimientoFK);
            return View(prueba);
        }

        // GET: Test/Edit/5
        public async Task<ActionResult> Edit(int id, int projectID, int requirementID)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }

            Prueba prueba = await db.Pruebas.FindAsync(id, projectID, requirementID);
            if (prueba == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_requerimientoFK = new SelectList(db.Requerimientoes, "idPK", "nombre", prueba.id_requerimientoFK);
            return View(prueba);
        }

        // POST: Test/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idPK, id_proyectoFK, id_requerimientoFK, nombre, detalleResultado, resultadoFinal")] Prueba prueba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prueba).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_requerimientoFK = new SelectList(db.Requerimientoes, "idPK", "nombre", prueba.id_requerimientoFK);
            return View(prueba);
        }

        // GET: Test/Delete/5
        public async Task<ActionResult> Delete(int? id, int? projectID, int? requirementID)
        {
            if (id == null || projectID == null || requirementID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prueba prueba = await db.Pruebas.FindAsync( id,  projectID,  requirementID);
            if (prueba == null)
            {
                return HttpNotFound();
            }
            return View(prueba);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Prueba prueba = await db.Pruebas.FindAsync(id);
            db.Pruebas.Remove(prueba);
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
