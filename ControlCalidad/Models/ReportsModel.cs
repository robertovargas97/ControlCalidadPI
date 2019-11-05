using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ControlCalidad.Models
{
    public class Reqterminados {
        public string nombreProyecto { set; get; }
        public string nombre { set; get; }
        public string estado { set; get; }
        public string complejidad { set; get; }
        public string fechaFinalizacion { set; get; }
        public int duracionReal { set; get; }
        public string nombreP { set; get; }
    }

}