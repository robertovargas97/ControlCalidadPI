//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ControlCalidad.Models
{
    using System;
    
    public partial class SP_Requeirmientos_Lider_Result
    {
        public string nombre { get; set; }
        public string complejidad { get; set; }
        public float duracionEstimada { get; set; }
        public Nullable<float> duracionReal { get; set; }
        public int id_proyectoFK { get; set; }
        public string estado { get; set; }
    }
}