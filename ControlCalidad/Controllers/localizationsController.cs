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
        public List<SelectListItem> provinceList()
        {
            List<Provincia> provinces = db.Provincias.ToList();

            List<SelectListItem> provinceList = provinces.ConvertAll(province => { return new SelectListItem() {
                Text = province.nombre,
                Value = province.codigoPK.ToString(),
                Selected = false
                };
             });
            return provinceList;
        }


        public JsonResult cantonesList(int provincia)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Canton> cantonList = db.Cantons.Where(x => x.provinciaFK == provincia).ToList();
            return Json(cantonList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult districtsList(int provincia,int canton)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Distrito> cantonList = db.Distritoes.Where(x => x.cantonFK == canton && x.provinciaFK == provincia).ToList();
            return Json(cantonList, JsonRequestBehavior.AllowGet);


            //return new SelectList((from provincias in db.Provincias
            //                       select provincias.nombre).ToList());
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
