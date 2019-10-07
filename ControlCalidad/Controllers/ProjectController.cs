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
    public class ProjectController : Controller
    {
        private QASystemEntities db = new QASystemEntities();
        private ClientController clientController = new ClientController( );
        

        // GET: Project
        public async Task<ActionResult> Index()
        {
            var proyectoes = db.Proyectoes.Include(p => p.Cliente);
            return View(await proyectoes.ToListAsync());
        }

        // GET: Project/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = await db.Proyectoes.FindAsync(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.allClientsId = clientController.GetClients( );
           // ViewBag.cedulaClienteFK = new SelectList(db.Clientes, "cedulaPK", "nombreP");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idPK,nombre,objetivo,fechaInicio,fechaFin,estado,duracionEstimada,duracionReal,cedulaClienteFK")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                db.Proyectoes.Add(proyecto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cedulaClienteFK = new SelectList(db.Clientes, "cedulaPK", "nombreP", proyecto.cedulaClienteFK);
            return View(proyecto);
        }

        // GET: Project/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = await db.Proyectoes.FindAsync(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.allClientsId = clientController.GetClients( );
            ViewBag.cedulaClienteFK = proyecto.cedulaClienteFK;
            return View(proyecto);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idPK,nombre,objetivo,fechaInicio,fechaFin,estado,duracionEstimada,duracionReal,cedulaClienteFK")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(proyecto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaClienteFK = new SelectList(db.Clientes, "cedulaPK", "nombreP", proyecto.cedulaClienteFK);
            return View(proyecto);
        }

        public ActionResult EditProject([Bind(Include = "idPK,nombre,objetivo,fechaInicio,fechaFin,estado,duracionEstimada,duracionReal,cedulaClienteFK")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {

                db.Entry(proyecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedulaClienteFK = new SelectList(db.Clientes, "cedulaPK", "nombreP", proyecto.cedulaClienteFK);
            return RedirectToAction("Index");
        }


        // GET: Project/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proyecto proyecto = await db.Proyectoes.FindAsync(id);
            if (proyecto == null)
            {
                return HttpNotFound();
            }
            return View(proyecto);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Proyecto proyecto = await db.Proyectoes.FindAsync(id);
            db.Proyectoes.Remove(proyecto);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //COMENTAR ESTE METODO****************************************
        public ActionResult RemoveProject(int id)
        {
            Proyecto project = db.Proyectoes.Find(id);
            db.Proyectoes.Remove(project);
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
    }
}
