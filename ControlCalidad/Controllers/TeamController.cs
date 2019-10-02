﻿using System;
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
    public class TeamController : Controller
    {
        private QASystemEntities db = new QASystemEntities();
        //private S3G4CEntity e = new S3G4CEntity();

        // GET: Team
        public async Task<ActionResult> Index()
        {
            //e.AspNetUsers.
            var trabajaEns = db.TrabajaEns.Include(t => t.Empleado).Include(t => t.Proyecto);
            return View(await trabajaEns.ToListAsync());
        }

        // GET: Team/Details/5
        public async Task<ActionResult> Details(string id)
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
        //string sqlp = "SELECT E.nombreP+' '+E.apellido1+' '+E.apellido2 AS 'Nombre' FROM ControlCalidad.Empleado E JOIN ControlCalidad.Tester T ON E.cedulaPK = T.cedula_empleadoFk JOIN ControlCalidad.Habilidades H ON H.cedula_empleadoFK = E.cedulaPK WHERE E.disponibilidad = 'Available' AND categoriaPK = 'Lenguaje' AND descripcionPK LIKE '%' + @descripcion + '%'"

        // GET: Team/Create
        public ActionResult Create(/*RegisterViewModel model*/)
        {
            string sqle = "SELECT E.cedulaPK FROM ControlCalidad.Empleado E JOIN ControlCalidad.Tester T ON E.cedulaPK = T.cedula_empleadoFk WHERE E.disponibilidad = 'Disponible'";
            List<string> resulte = db.Database.SqlQuery<string>(sqle).ToList();
            ViewBag.cedula_empleadoFk = new SelectList(resulte);
            string sqlp = "SELECT P.idPK FROM ControlCalidad.Proyecto P JOIN ControlCalidad.TrabajaEn T ON	P.idPK = T.id_proyectoFK JOIN ControlCalidad.Empleado E ON E.cedulaPK = T.cedula_empleadoFK WHERE E.correo = 'grubio010@gmail.com'";
            List<int> resultp = db.Database.SqlQuery<int>(sqlp).ToList();
            ViewBag.id_proyectoFK = new SelectList(resultp);
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "cedula_empleadoFK,id_proyectoFK,rol")] TrabajaEn trabajaEn)
        {
            if (ModelState.IsValid)
            {
                db.TrabajaEns.Add(trabajaEn);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", trabajaEn.cedula_empleadoFK);
            ViewBag.id_proyectoFK = new SelectList(db.Proyectoes, "idPK", "nombre", trabajaEn.id_proyectoFK);
            return View(trabajaEn);
        }

        // GET: Team/Edit/5
        public async Task<ActionResult> Edit(string id)
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
            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", trabajaEn.cedula_empleadoFK);
            ViewBag.id_proyectoFK = new SelectList(db.Proyectoes, "idPK", "nombre", trabajaEn.id_proyectoFK);
            return View(trabajaEn);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "cedula_empleadoFK,id_proyectoFK,rol")] TrabajaEn trabajaEn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trabajaEn).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.cedula_empleadoFK = new SelectList(db.Empleadoes, "cedulaPK", "nombreP", trabajaEn.cedula_empleadoFK);
            ViewBag.id_proyectoFK = new SelectList(db.Proyectoes, "idPK", "nombre", trabajaEn.id_proyectoFK);
            return View(trabajaEn);
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
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TrabajaEn trabajaEn = await db.TrabajaEns.FindAsync(id);
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
