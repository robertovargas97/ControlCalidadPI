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
    public class tieneAsignado
    {
        public tieneAsignado(string cedula_empeladoPK, int id_proyectoFK, int id_requerimientoFK)
        {
            this.cedula_empeladoPK = cedula_empeladoPK;
            this.id_proyectoFK = id_proyectoFK;
            this.id_requerimientoFK = id_requerimientoFK;
        }
        public tieneAsignado(){}
        public string cedula_empeladoPK { get; set; }
        public int id_proyectoFK { get; set; }
        public int id_requerimientoFK { get; set; }
    }
    public class RequirementController : Controller
    {
        private QASystemEntities db = new QASystemEntities( );
        private ProjectController projectController = new ProjectController( );
        private TesterRequirementController tieneAsignado = new TesterRequirementController();
        // GET: Requirement
        public async Task<ActionResult> Index( int projectId, tieneAsignado entity)
        {
            if (entity.cedula_empeladoPK != null)
            {
                tieneAsignado.insert(entity.cedula_empeladoPK, entity.id_proyectoFK, entity.id_requerimientoFK);
            }
            var requerimientoes = db.Requerimientoes.Include( r => r.Proyecto ).Where( r => r.id_proyectoFK == projectId );
            ViewBag.projectId = projectId ;
            ViewBag.projectName = projectController.getProjectName( projectId );
            return View( requerimientoes.ToList( ) );
        }

        // GET: Requirement/Details/5
        public async Task<ActionResult> Details( int? id , int? projectId )
        {
            if( id == null || projectId == null )
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Requerimiento requerimiento = await db.Requerimientoes.FindAsync( id , projectId );
            if ( requerimiento == null )
            {
                return HttpNotFound( );
            }
            return View( requerimiento );
        }

        // GET: Requirement/Create
        public ActionResult Create(int projectId )
        {
            ViewBag.id_proyectoFK = new SelectList( db.Proyectoes , "idPK" , "nombre" );
            ViewBag.projectId = projectId;
            List<ControlCalidad.Models.SP_Conseguir_testers_req_Result> employees = db.SP_Conseguir_testers_req(projectId).ToList();
            List<SelectListItem> allEmployess = employees.ConvertAll(
                employee => {
                    return new SelectListItem()
                    {
                        Text = employee.nombreP,
                        Value = employee.cedulaPK,
                        Selected = false
                    };
                });
            ViewBag.testers = allEmployess;

            return View( );
        }

        // POST: Requirement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( [Bind( Include = "idPK,nombre,id_proyectoFK,fechaInicio,fechaFinalizacion,fechaAsignacion,estado,complejidad,descripcion,duracionEstimada,duracionReal" )] Requerimiento requerimiento , FormCollection fc)
        {
            int projectId = requerimiento.id_proyectoFK;
            if( ModelState.IsValid )
            {
                string idTester = fc["idTester"];
                await db.SaveChangesAsync( );
                tieneAsignado newEntity = new tieneAsignado(idTester, requerimiento.id_proyectoFK, requerimiento.idPK);
                return RedirectToAction( "Index" , new {projectId = requerimiento.id_proyectoFK, entity = newEntity} );
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
        public ActionResult Edit( [Bind( Include = "idPK,nombre,id_proyectoFK,fechaInicio,fechaFinalizacion,fechaAsignacion,estado,complejidad,descripcion,duracionEstimada,duracionReal" )] Requerimiento requerimiento )
        {

            if( ModelState.IsValid )
            {
                db.Entry( requerimiento ).State = EntityState.Modified;
                db.SaveChanges( );

                return RedirectToAction( "Index" , new {projectId = requerimiento.id_proyectoFK
            } );

            }
            ViewBag.id_proyectoFK = new SelectList( db.Proyectoes , "idPK" , "nombre" , requerimiento.id_proyectoFK );
            return View( requerimiento );
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                db.Dispose( );
            }
            base.Dispose( disposing );
        }

        //DOCUMENTAR
        public ActionResult removeRequirement( int id , int projectId )
        {
            Requerimiento requerimiento = db.Requerimientoes.Find( id , projectId );
            db.Requerimientoes.Remove( requerimiento );
            db.SaveChanges( );
            return RedirectToAction( "Index" , new { projectId = projectId} );
        }
    }
}
