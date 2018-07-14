
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{

    public class MedicamentosViewModel
    {
        public MedicamentosViewModel()
        {
            Tipo = null;
           // this.Medicinas = new List<Medicamento>();
        }
        public MedicamentosViewModel(Medicamento medicamento)
        {
            ID = medicamento.ID;
            Nombre = medicamento.Nombre;
            Descripcion = medicamento.Descripcion;
            TipoID = medicamento.TipoID;
            Tipo = medicamento.Tipo;
        }

        [Display(Name ="Identificador")]
        public int ID { get; set; }
        [Display(Name = "Nombre Medicina")]
        public string Nombre { get; set; }
        [Display(Name = "Descripcion medicina")]
        public string Descripcion { get; set; }
        [Display(Name = "Tipo de medicina")]
        public int TipoID { get; set; }

        public Tipo_Med Tipo { get; set; }
    }
}
