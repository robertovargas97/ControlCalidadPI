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
        private EmployeeController employeeController = new EmployeeController( );
        private string projectLeader;

        // GET: Project
        public async Task<ActionResult> Index()
        {
            var proyectoes = db.Proyectoes.Include(p => p.Cliente);
            string email = User.Identity.Name;
            if (User.IsInRole("Tester") || User.IsInRole("Lider"))
            {
                ViewBag.projectId = GetProjectIdByEmail(email);
            }
            
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
            ViewBag.projectLeader = GetLeaderName(id);
            return View(proyecto);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.leaders = employeeController.GetLeaders( );
            ViewBag.allClientsId = clientController.GetClients( );
           // ViewBag.cedulaClienteFK = new SelectList(db.Clientes, "cedulaPK", "nombreP");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idPK,nombre,objetivo,fechaInicio,fechaFin,estado,duracionEstimada,duracionReal,cedulaClienteFK")] Proyecto proyecto, string cedula_empleadoFK)
        {
            //Console.WriteLine(cedula_empleadoFK);
            
            if (ModelState.IsValid)
            {
               
                db.Proyectoes.Add(proyecto);
                await db.SaveChangesAsync();
                SetLeaderToProject(cedula_empleadoFK, proyecto.idPK, "Lider");
                ViewBag.leader = cedula_empleadoFK;
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
            
            ViewBag.leaders = employeeController.GetLeaders( );
            ViewBag.allClientsId = clientController.GetClients( );
            ViewBag.cedulaClienteFK = proyecto.cedulaClienteFK;
            return View(proyecto);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idPK,nombre,objetivo,fechaInicio,fechaFin,estado,duracionEstimada,duracionReal,cedulaClienteFK")] Proyecto proyecto, string newProjectLeader)
        {
            if (ModelState.IsValid)
            {    
                db.Entry(proyecto).State = EntityState.Modified;
                EditProjectLeader(newProjectLeader, proyecto.idPK);
                
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

        public string GetLeaderName(int? id)
        {
            string projectLeader = "";
            if (id != null)
            {
                string query = "SELECT	E.nombreP, E.apellido1, E.apellido2 FROM ControlCalidad.TrabajaEn TE JOIN ControlCalidad.Empleado E ON E.cedulaPK = TE.cedula_empleadoFK " +
                    "WHERE TE.id_proyectoFK = " + id + " AND TE.rol = 'Lider';";
                List<Leader> leader = db.Database.SqlQuery<Leader>(query).ToList();
                foreach (Leader l in leader)
                {
                    l.nombreCompleto = l.nombreP + " " + l.apellido1 + " " + l.apellido2;

                }
                if (leader.Count() > 0)
                {
                    Leader leaderForProject = leader.Last();
                    projectLeader = leaderForProject.nombreCompleto;
                }
                
            }
            
            return projectLeader;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void SetLeaderToProject(string cedula_empleadoFK, int idPK, string rol)
        {
            if (cedula_empleadoFK != "")
            {
                var TrabajaEn = new TrabajaEn
                {
                    cedula_empleadoFK = cedula_empleadoFK,
                    id_proyectoFK = idPK,
                    rol = rol
                };
                var employee = db.Empleadoes.Find(cedula_empleadoFK);
                employee.disponibilidad = employee.disponibilidad.Replace("Disponible", "Ocupado");

                db.TrabajaEns.Add(TrabajaEn);

                db.SaveChanges();
            }

        }

        public void EditProjectLeader(string newProjectLeader, int id)
        {
            if (newProjectLeader != "")
            {
                string projectLeader = "";

                string query = "SELECT	E.cedulaPK FROM ControlCalidad.TrabajaEn TE JOIN ControlCalidad.Empleado E ON E.cedulaPK = TE.cedula_empleadoFK " +
                    "WHERE TE.id_proyectoFK = " + id + " AND TE.rol = 'Lider';";
                List<CedulaLider> leader = db.Database.SqlQuery<CedulaLider>(query).ToList();
                if (leader.Count() > 0)
                {
                    CedulaLider leaderForProject = leader.Last();
                    projectLeader = leaderForProject.cedulaPK;


                    //Actualiza el estado del lider anterior a disponible.
                    var employee = db.Empleadoes.Find(projectLeader);
                    employee.disponibilidad = employee.disponibilidad.Replace("Ocupado", "Disponible");
                    //Extrae la tupla del lider en el equipo
                    var leaderInfo = db.TrabajaEns.Find(employee.cedulaPK, id);
                    db.TrabajaEns.Remove(leaderInfo);

                    SetLeaderToProject(newProjectLeader, id, "Lider");
                }
                else {
                    SetLeaderToProject(newProjectLeader, id, "Lider");
                }

                db.SaveChanges();
            }
            
        }

        public int GetProjectIdByEmail(string email)
        {
            int projectId = 0;
            if (email != null)
            {
                string idEmployee = employeeController.GetEmployeeIdByEmail(email);

                string query = "SELECT	TE.id_proyectoFK FROM ControlCalidad.TrabajaEn TE " +
                    "WHERE TE.cedula_empleadoFK = '" + idEmployee + "'";
                List<ProjectId> projectIdList = db.Database.SqlQuery<ProjectId>(query).ToList();
                if (projectIdList.Count() > 0) {
                    var project = projectIdList.Last();
                    projectId = project.id_proyectoFK;
                }
            }

            return projectId;
        }

        
    }
}
