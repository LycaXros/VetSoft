using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetSoft.Data
{
    public static class Constantes
    {
        public const string Macho = "M";
        public const string Hembra = "H";
    }
    public enum Sexo
    {
        Macho,
        Hembra 
    }
    public enum TipoPropietario
    {
        [Display(Name ="Propietario Actual")]
        Propietario_Actual = 1,
        [Display(Name = "Propietario Anterior")]
        Propietario_Anterior = 2,
        [Display(Name = "Propietario Compartido")]
        Propietario_Compartido = 3
    }
    public enum TipoMedicacion
    {
        Vacuna = 1,
        Desparacitante
    }
    
}
