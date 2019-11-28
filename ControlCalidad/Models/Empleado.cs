//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ControlCalidad.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Empleado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empleado()
        {
            this.Habilidades = new HashSet<Habilidade>();
            this.TrabajaEns = new HashSet<TrabajaEn>();
        }
    
        public string cedulaPK { get; set; }
        public string nombreP { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public Nullable<System.DateTime> fechaNacimiento { get; set; }
        public Nullable<int> edad { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string provincia { get; set; }
        public string canton { get; set; }
        public string distrito { get; set; }
        public string direccionExacta { get; set; }
        public string disponibilidad { get; set; }
    
        public virtual Tester Tester { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Habilidade> Habilidades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrabajaEn> TrabajaEns { get; set; }
    }
}
