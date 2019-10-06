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
            /*int[] prov_cod_int = (from provincia in db.Provincias
                                  select provincia.codigoPK).ToArray();
            Console.WriteLine("DEBUG 01");
            Console.WriteLine(prov_cod_int);
            Console.WriteLine("DEBUG 02");*/
            return new SelectList(db.Provincias, "nombre");
        }

        public SelectList TraerNombreCantones(string provincia)
        {
            int prov_cod_int =  (from prov in db.Provincias
                                           where prov.nombre == provincia
                                           select prov.codigoPK).ElementAt(0);
            Console.WriteLine(prov_cod_int);
            int prov_cod = 0;
            return new SelectList((from canton in db.Cantons
                                   where canton.provinciaFK == prov_cod
                                   select canton.nombre).ToList());
        }

    }
}
