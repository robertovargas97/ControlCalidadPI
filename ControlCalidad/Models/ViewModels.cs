using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlCalidad.Models
{
    public class ViewModels
    {
    }

    public class ClientForproject
    {
        public string cedulaPK
        {
            get; set;
        }
        public string nombreP
        {
            get; set;
        }
        public string apellido1
        {
            get; set;
        }
        public string apellido2
        {
            get; set;
        }

        public string nombreCompleto
        {
            get; set;
        }
    }
}