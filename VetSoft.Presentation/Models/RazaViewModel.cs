using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{

    public class RazaViewModel
    {
        public RazaViewModel()
        {
            this.Animales = new List<PacienteSingleModel>();
        }

        public RazaViewModel(Raza raza)
        {
            ID = raza.ID;
            Nombre = raza.Nombre;
            EspecieID = raza.EspecieID;
            Especie = new EspecieSingleModel(raza.Especie);
            Animales = PacienteSingleModel.GetFromModel(raza.Animales);
        }

        public int ID { get; set; }
        [Required(ErrorMessage = "No puede dejar esto vacio")]
        [Display(Name = "Nombre Raza")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "No debe faltar la Especie Madre")]
        [Display(Name = "Identificador de la Especie")]
        public int EspecieID { get; set; }

        [Display(Name = "Especie Madre")]
        public EspecieSingleModel Especie { get; set; }
        public List<PacienteSingleModel> Animales { get; set; }

        public Raza GetModel(Raza r)
        {
            r.Nombre = Nombre;
            r.EspecieID = EspecieID;
            return r;
        }
    }
    
    public class RazaSingleModel
    {
        public RazaSingleModel()
        {

        }

        public RazaSingleModel(Raza raza)
        {
            ID = raza.ID;
            Nombre = raza.Nombre;
            EspecieID = raza.EspecieID;
            Especie = new EspecieSingleModel(raza.Especie);
        }

        public int ID { get; set; }
        [Required(ErrorMessage = "No puede dejar esto vacio")]
        [Display(Name = "Nombre Raza")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "No debe faltar la Especie Madre")]
        [Display(Name = "Identificador de la Especie")]
        public int EspecieID { get; set; }

        [Display(Name = "Especie Madre")]
        public EspecieSingleModel Especie { get; set; }

    }

}
