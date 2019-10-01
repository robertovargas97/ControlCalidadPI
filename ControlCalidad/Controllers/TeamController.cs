using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlCalidad.Models;

namespace ControlCalidad.Controllers
{
    public class TeamController : Controller
    {
        private QASystemEntity db = new QASystemEntity();

        // GET: Team
        public ActionResult Index()
        {
            var trabajaEns = db.TrabajaEns.Include(t => t.Empleado).Include(t => t.Proyecto);
            return View(trabajaEns.ToList());
        }

        // GET: Team/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrabajaEn trabajaEn = db.TrabajaEns.Find(id);
            if (trabajaEn == null)
            {
                return HttpNotFound();
            }
            return View(trabajaEn);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP");
            ViewBag.id_proyectoFK = new SelectList(db.Proyectoes, "idPK", "nombre");
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedula_empleadoFK,id_proyectoFK,id_equipo,rol")] TrabajaEn trabajaEn)
        {
            if (ModelState.IsValid)
            {
                db.TrabajaEns.Add(trabajaEn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", trabajaEn.cedula_empleadoFK);
            ViewBag.id_proyectoFK = new SelectList(db.Proyectoes, "idPK", "nombre", trabajaEn.id_proyectoFK);
            return View(trabajaEn);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrabajaEn trabajaEn = db.TrabajaEns.Find(id);
            if (trabajaEn == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", trabajaEn.cedula_empleadoFK);
            ViewBag.id_proyectoFK = new SelectList(db.Proyectoes, "idPK", "nombre", trabajaEn.id_proyectoFK);
            return View(trabajaEn);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedula_empleadoFK,id_proyectoFK,id_equipo,rol")] TrabajaEn trabajaEn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trabajaEn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", trabajaEn.cedula_empleadoFK);
            ViewBag.id_proyectoFK = new SelectList(db.Proyectoes, "idPK", "nombre", trabajaEn.id_proyectoFK);
            return View(trabajaEn);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrabajaEn trabajaEn = db.TrabajaEns.Find(id);
            if (trabajaEn == null)
            {
                return HttpNotFound();
            }
            return View(trabajaEn);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TrabajaEn trabajaEn = db.TrabajaEns.Find(id);
            db.TrabajaEns.Remove(trabajaEn);
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
