//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trabajo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class catTipoProcesos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public catTipoProcesos()
        {
            this.tblTransacciones = new HashSet<tblTransacciones>();
        }
    
        public int idTipoProceso { get; set; }
        public string descripcion { get; set; }
        public Nullable<System.DateTime> fechaingreso { get; set; }
        public Nullable<int> idEstado { get; set; }
    
        public virtual catEstados catEstados { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTransacciones> tblTransacciones { get; set; }
    }
}
