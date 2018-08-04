
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{

    public class EspecieViewModel
    {
        public EspecieViewModel()
        {
            this.Razas = new List<RazaSingleModel>();
        }
        public EspecieViewModel(Especie especie)
        {
            ID = especie.ID;
            Nombre = especie.Nombre;
            Nombre_Esp = especie.Nombre_Esp;
            Razas = new List<RazaSingleModel>();
            especie.Razas.ToList()
                .ForEach(x =>
                {
                    Razas.Add(new RazaSingleModel(x));
                });
        }

        [Display(Name = "Identificador")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre Especie")]
        public string Nombre { get; set; }

        [Display(Name = "Especificacion Especie")]
        public string Nombre_Esp { get; set; }

        public List<RazaSingleModel> Razas { get; set; }
    }

    public class EspecieSingleModel
    {
        public EspecieSingleModel()
        {

        }
        public EspecieSingleModel(Especie especie)
        {
            ID = especie.ID;
            Nombre = especie.Nombre;
            Nombre_Esp = especie.Nombre_Esp;
            
        }

        [Display(Name = "Identificador")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre Especie")]
        public string Nombre { get; set; }

        [Display(Name = "Especificacion Especie")]
        public string Nombre_Esp { get; set; }

    }
}
