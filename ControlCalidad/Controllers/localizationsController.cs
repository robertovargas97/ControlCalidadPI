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
    public class localizationsController : Controller
    {
        private localizacoinesEntities db = new localizacoinesEntities();

        // GET: Province
        public SelectList TraerNombreProvincias()
        {
            return new SelectList((from provincias in db.Provincias
                                   select provincias.nombre).ToList());
        }

        public SelectList TraerNombreCantones()
        {
            return new SelectList((from cantones in db.Cantons
                                   select cantones.nombre).ToList());
        }
        public SelectList TraerNombreDistritos()
        {
            return new SelectList((from distritos in db.Distritoes
                                   select distritos.nombre).ToList());
        }

    }
}
