//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VetSoft.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Medicacion
    {
        public int ID { get; set; }
        public int Tipo { get; set; }
        public string Indicacion { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public int ChequeoID { get; set; }
    
        public virtual Chequeo Chequeo { get; set; }
        public virtual Medicamento Medicamento { get; set; }
    }
}
