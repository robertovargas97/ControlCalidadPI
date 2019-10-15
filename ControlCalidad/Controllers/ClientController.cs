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
    public class ClientController : Controller
    {
        private localizationsController localizations = new localizationsController();

        private QASystemEntities db = new QASystemEntities();


        //<summary> : Toma todos lo clientes de la base de datos para convertirlos en una lista
        //<return> : List<SelectListItem> que corresponde a los clientes en la base de datos.
        public List<SelectListItem> GetClients()
        {
            List<ClientForproject> clientsList =
                ( from client in db.Clientes
                  select new ClientForproject {
                      cedulaPK = client.cedulaPK ,
                      nombreP = client.nombreP,
                      apellido1 = client.apellido1,
                      apellido2 = client.apellido2,
                      nombreCompleto = client.nombreP + " " + client.apellido1 + " " + client.apellido2
                      
                      
                  } ).ToList( );


            List<SelectListItem> allClients = clientsList.ConvertAll(
                client => { return new SelectListItem( )
                {
                    Text = client.nombreCompleto ,
                    Value = client.cedulaPK.ToString( ) ,
                    Selected = false
                };
            } );

            return allClients;
        }

        // GET: Client
        public async Task<ActionResult> Index()
        {

            return View( await db.Clientes.ToListAsync( ) );
        }

        // GET: Client/Details/5
        public async Task<ActionResult> Details( string id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Cliente cliente = await db.Clientes.FindAsync( id );
            if( cliente == null )
            {
                return HttpNotFound( );
            }
            return View( cliente );
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            ViewBag.provinces = this.localizations.provinceList();
            return View();

        }

        // POST: Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create([Bind(Include = "cedulaPK,nombreP,apellido1,apellido2,telefono,correo,provincia,canton,distrito,direccionExacta,fechaNacimiento")] Cliente cliente)
        {
            string provinceName = localizations.provinceName(cliente.provincia);
            string cantonName = localizations.cantonName(cliente.provincia, cliente.canton);
            string districtName = localizations.districtName(cliente.provincia, cliente.canton, cliente.distrito);
            cliente.provincia = provinceName;
            cliente.canton = cantonName;
            cliente.distrito = districtName;
            if ( ModelState.IsValid )
            {
                db.Clientes.Add( cliente );
                try
                {
                    await db.SaveChangesAsync( );
                }

                catch
                {
                    ModelState.AddModelError( "" , "No puede crear clientes con la misma cédula." );
                    return View( cliente );

                }
                return RedirectToAction( "Index" );
            }

            return View( cliente );
        }

        // GET: Client/Edit/5
        public async Task<ActionResult> Edit( string id )
        {
            ViewBag.provinces = this.localizations.provinceList();
            if (id == null)
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Cliente cliente = await db.Clientes.FindAsync( id );
            if( cliente == null )
            {
                return HttpNotFound( );
            }
            return View( cliente );
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit([Bind(Include = "cedulaPK,nombreP,apellido1,apellido2,telefono,correo,provincia,canton,distrito,direccionExacta,fechaNacimiento")] Cliente cliente)
        {
            string provinceName = localizations.provinceName(cliente.provincia);
            string cantonName = localizations.cantonName(cliente.provincia, cliente.canton);
            string districtName = localizations.districtName(cliente.provincia, cliente.canton, cliente.distrito);
            cliente.provincia = provinceName;
            cliente.canton = cantonName;
            cliente.distrito = districtName;
            if ( ModelState.IsValid )
            {

                db.Entry( cliente ).State = EntityState.Modified;
                await db.SaveChangesAsync( );
                return RedirectToAction( "Index" );
            }
            return View( cliente );
        }


        // GET: Client/Delete/5
        public async Task<ActionResult> Delete( string id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            Cliente cliente = await db.Clientes.FindAsync( id );
            if( cliente == null )
            {
                return HttpNotFound( );
            }
            return View( cliente );
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed( string id )
        {
            Cliente cliente = await db.Clientes.FindAsync( id );
            db.Clientes.Remove( cliente );
            await db.SaveChangesAsync( );
            return RedirectToAction( "Index" );
        }

        public ActionResult RemoveClient(string clientId)
        {  
            Cliente client= db.Clientes.Find(clientId);
            db.Clientes.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if( disposing )
            {
                db.Dispose( );
            }
            base.Dispose( disposing );
        }

    }
}
