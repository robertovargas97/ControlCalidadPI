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
    
    public partial class Tester
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tester()
        {
            this.TieneAsignadoes = new HashSet<TieneAsignado>();
        }
    
        public string cedula_empleadoFK { get; set; }
        public Nullable<int> cantidadReqAsignados { get; set; }
    
        public virtual Empleado Empleado { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TieneAsignado> TieneAsignadoes { get; set; }
    }
}
