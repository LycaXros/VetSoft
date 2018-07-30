using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{

    public partial class RazaViewModel
    {
        public RazaViewModel()
        {
            this.Animales = new List<PacienteViewModel>();
        }

        public RazaViewModel(Data.Raza raza)
        {
            ID = raza.ID;
            Nombre = raza.Nombre;
            EspecieID = raza.EspecieID;
            Especie = new EspecieViewModel(raza.Especie);
            Animales = PacienteViewModel.GetFromModel(raza.Animales);
        }

        public int ID { get; set; }
        [Required(ErrorMessage = "No puede dejar esto vacio")]
        [Display(Name = "Nombre Raza")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "No debe faltar la Especie Madre")]
        [Display(Name = "Identificador de la Especie")]
        public int EspecieID { get; set; }

        [Display(Name = "Especie Madre")]
        public EspecieViewModel Especie { get; set; }
        public ICollection<PacienteViewModel> Animales { get; set; }

        public Raza GetModel(Raza r)
        {
            r.Nombre = Nombre;
            r.EspecieID = EspecieID;
            return r;
        }
    }
}
