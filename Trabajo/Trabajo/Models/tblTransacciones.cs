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
    
    public partial class tblTransacciones
    {
        public int idTransacciones { get; set; }
        public Nullable<int> monto { get; set; }
        public Nullable<System.DateTime> fechaTransaccion { get; set; }
        public Nullable<int> idClientes { get; set; }
        public Nullable<int> idTipoProducto { get; set; }
        public Nullable<int> idTipoProceso { get; set; }
        public Nullable<int> idEstadoProceso { get; set; }
        public Nullable<int> idOrigen { get; set; }
    
        public virtual catEstadoProcesos catEstadoProcesos { get; set; }
        public virtual catOrigen catOrigen { get; set; }
        public virtual catProductos catProductos { get; set; }
        public virtual catTipoProcesos catTipoProcesos { get; set; }
        public virtual tblClientes tblClientes { get; set; }
    }
}