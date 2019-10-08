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
    public class LoginUsersController : Controller
    {
        private S3G4CEntity db = new S3G4CEntity( );
        private S3G4CUREntity userRoles = new S3G4CUREntity( );

        // GET: LoginUsers
        public ActionResult Index()
        {
            return View( db.AspNetUsers.ToList( ) );
        }

        // GET: LoginUsers/Details/5
        public ActionResult Details( string id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find( id );
            if( aspNetUser == null )
            {
                return HttpNotFound( );
            }
            return View( aspNetUser );
        }

        // GET: LoginUsers/Create
        public ActionResult Create()
        {
            return View( );
        }

        // POST: LoginUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( [Bind( Include = "Id,Role,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName" )] AspNetUser aspNetUser )
        {
            if( ModelState.IsValid )
            {
                db.AspNetUsers.Add( aspNetUser );
                db.SaveChanges( );
                return RedirectToAction( "Index" );
            }
            return View( aspNetUser );
        }

        // GET: LoginUsers/Edit/5
        public ActionResult Edit( string id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find( id );
            if( aspNetUser == null )
            {
                return HttpNotFound( );
            }
            return View( aspNetUser );
        }

        // POST: LoginUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( [Bind( Include = "Id,Role,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName" )] AspNetUser aspNetUser )
        {
            if( ModelState.IsValid )
            {
                var roleId = "";
                switch( aspNetUser.Role )
                {
                    case "Jefe":
                    roleId = "1";
                    break;
                    case "Lider":
                    roleId = "2";
                    break;
                    case "Tester":
                    roleId = "3";
                    break;
                    case "Cliente":
                    roleId = "4";
                    break;

                    default:
                    roleId = "1";
                    break;
                }
                /*
                if( ( userRoles.AspNetUserRoles.Where( userExist => userExist.UserId == aspNetUser.Id ).Any( ) == true) )
                {
                   AspNetUserRole userRoleToDelete = userRoles.AspNetUserRoles.Find( aspNetUser.Id);
                    userRoles.AspNetUserRoles.Remove( userRoleToDelete );
                }*/

                AspNetUser aspNetUserToDelete = db.AspNetUsers.Find( aspNetUser.Id );
                db.AspNetUsers.Remove( aspNetUserToDelete );

                AspNetUserRole userRole = new AspNetUserRole {
                    UserId = aspNetUser.Id ,
                    RoleId = roleId
                };

                aspNetUser.Email = aspNetUser.UserName;
                db.AspNetUsers.Add( aspNetUser );
                userRoles.AspNetUserRoles.Add( userRole );

                db.SaveChanges( );
                userRoles.SaveChanges( );

                return RedirectToAction( "Index" );
            }
            return View( aspNetUser );
        }

        // GET: LoginUsers/Delete/5
        public ActionResult Delete( string id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find( id );
            if( aspNetUser == null )
            {
                return HttpNotFound( );
            }
            return View( aspNetUser );

        }

        //COMENTAR ESTE METODO****************************************
        public ActionResult RemoveUser( string userId )
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find( userId );
            db.AspNetUsers.Remove( aspNetUser );
            db.SaveChanges( );
            return RedirectToAction( "Index" );
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
