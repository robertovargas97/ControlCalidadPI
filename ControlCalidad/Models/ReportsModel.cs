using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ControlCalidad.Models
{
    public class Reports {
        public string nombreRequerimiento { set; get; }
        public string estadoRequerimiento { set; get; }
        public string complejidadRequerimiento { set; get; }
        public string nombreResponsable { set; get; }
        
        public string nombreReq { set; get; }
        public string complejidadReq { set; get; }
        public string nombreResp { set; get; }
        
        public string nombreTester { set; get; }
        public int cantidadReqAsignados { set; get; }
        public string nombreP { set; get; }

        public string nombre { set; get; }
        public string estado { set; get; }
        public float horasdedicas { set; get; }
    }

}