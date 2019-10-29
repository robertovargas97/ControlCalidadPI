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
    public class RequirementController : Controller
    {
        private QASystemEntities db = new QASystemEntities( );

        // GET: Requirement
        public async Task<ActionResult> Index( int id_proyecto )
        {
            var requerimientoes = db.Requerimientoes.Include( r => r.Proyecto ).Where( r => r.id_proyectoFK == id_proyecto );
            ViewBag.id_proyecto = id_proyecto;
            return View( await requerimientoes.ToListAsync( ) );
        }

        // GET: Requirement/Details/5
        public async Task<ActionResult> Details( int? id , int? projectId )
        {
            if( id == null || projectId == null )
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Requerimiento requerimiento = await db.Requerimientoes.FindAsync( id , projectId );
            if( requerimiento == null )
            {
                return HttpNotFound( );
            }
            return View( requerimiento );
        }

        // GET: Requirement/Create
        public ActionResult Create(int id_proyecto)
        {
            ViewBag.id_proyectoFK = new SelectList( db.Proyectoes , "idPK" , "nombre" );
            ViewBag.id_proyecto = id_proyecto;

            return View( );
        }

        // POST: Requirement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( [Bind( Include = "idPK,nombre,id_proyectoFK,fechaInicio,fechaFinalizacion,fechaAsignacion,estado,complejidad,descripcion,duracionEstimada,duracionReal" )] Requerimiento requerimiento )
        {
            if( ModelState.IsValid )
            {
                db.Requerimientoes.Add( requerimiento );
                await db.SaveChangesAsync( );
                return RedirectToAction( "Index" , new {id_proyecto = requerimiento.id_proyectoFK} );
            }

            ViewBag.id_proyectoFK = new SelectList( db.Proyectoes , "idPK" , "nombre" , requerimiento.id_proyectoFK );
            return View( requerimiento );
        }

        // GET: Requirement/Edit/5
        public async Task<ActionResult> Edit( int? id , int? projectId )
        {
            if( id == null || projectId == null )
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Requerimiento requerimiento = await db.Requerimientoes.FindAsync( id , projectId );
            if( requerimiento == null )
            {
                return HttpNotFound( );
            }
            ViewBag.id_proyectoFK = new SelectList( db.Proyectoes , "idPK" , "nombre" , requerimiento.id_proyectoFK );
            return View( requerimiento );
        }

        // POST: Requirement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( [Bind( Include = "idPK,nombre,id_proyectoFK,fechaInicio,fechaFinalizacion,fechaAsignacion,estado,complejidad,descripcion,duracionEstimada,duracionReal" )] Requerimiento requerimiento )
        {
            if( ModelState.IsValid )
            {
                db.Entry( requerimiento ).State = EntityState.Modified;
                await db.SaveChangesAsync( );
                return RedirectToAction( "Index" , new {id_proyecto = requerimiento.id_proyectoFK} );
            }
            ViewBag.id_proyectoFK = new SelectList( db.Proyectoes , "idPK" , "nombre" , requerimiento.id_proyectoFK );
            return View( requerimiento );
        }

        // GET: Requirement/Delete/5
        public async Task<ActionResult> Delete( int? id , int? projectId )
        {
            if( id == null || projectId == null )
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Requerimiento requerimiento = await db.Requerimientoes.FindAsync( id , projectId );
            if( requerimiento == null )
            {
                return HttpNotFound( );
            }
            return View( requerimiento );
        }

        // POST: Requirement/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed( int id , int projectId )
        {
            Requerimiento requerimiento = await db.Requerimientoes.FindAsync( id , projectId );
            db.Requerimientoes.Remove( requerimiento );
            await db.SaveChangesAsync( );
            return RedirectToAction( "Index" , new {id_proyecto = projectId} );
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                db.Dispose( );
            }
            base.Dispose( disposing );
        }
    }
}
