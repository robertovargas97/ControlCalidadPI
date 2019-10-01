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
    public class TesterController : Controller
    {
        private QASystemEntity db = new QASystemEntity();

        // GET: Tester
        public ActionResult Index()
        {
            var testers = db.Testers.Include(t => t.Empleado);
            return View(testers.ToList());
        }

        // GET: Tester/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tester tester = db.Testers.Find(id);
            if (tester == null)
            {
                return HttpNotFound();
            }
            return View(tester);
        }

        // GET: Tester/Create
        public ActionResult Create()
        {
            ViewBag.cedula_empleadoFk = new SelectList(db.Empleadoes, "cedulaPK", "nombreP");
            return View();
        }

        // POST: Tester/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedula_empleadoFk,cantidadReqAsignados")] Tester tester)
        {
            if (ModelState.IsValid)
            {
                db.Testers.Add(tester);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cedula_empleadoFk = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", tester.cedula_empleadoFk);
            return View(tester);
        }

        // GET: Tester/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tester tester = db.Testers.Find(id);
            if (tester == null)
            {
                return HttpNotFound();
            }
            ViewBag.cedula_empleadoFk = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", tester.cedula_empleadoFk);
            return View(tester);
        }

        // POST: Tester/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cedula_empleadoFk,cantidadReqAsignados")] Tester tester)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tester).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cedula_empleadoFk = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", tester.cedula_empleadoFk);
            return View(tester);
        }

        // GET: Tester/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tester tester = db.Testers.Find(id);
            if (tester == null)
            {
                return HttpNotFound();
            }
            return View(tester);
        }

        // POST: Tester/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Tester tester = db.Testers.Find(id);
            db.Testers.Remove(tester);
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
