
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{

    public class EspecieViewModel
    {
        public EspecieViewModel()
        {
            this.Razas = new List<Raza>();
        }
        public EspecieViewModel(Especie especie)
        {
            ID = especie.ID;
            Nombre = especie.Nombre;
            Nombre_Esp = especie.Nombre_Esp;
            Razas = especie.Razas;
        }

        [Display(Name = "Identificador")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre Especie")]
        public string Nombre { get; set; }

        [Display(Name = "Especificacion Especies")]
        public string Nombre_Esp { get; set; }

        public virtual ICollection<Raza> Razas { get; set; }
    }
}
