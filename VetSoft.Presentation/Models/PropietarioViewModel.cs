using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{
    public class PropietarioViewModel
    {
        public PropietarioViewModel()
        {
            this.Mascotas = new List<PropietarioPacienteSingleModel>();
        }
        public PropietarioViewModel(Propietario propietario)
        {
            ID = propietario.ID;
            Nombre = propietario.Nombre;
            Apellido = propietario.Apellido;
            Email = propietario.Email;
            Telefono = propietario.Telefono;
            Mascotas = PropietarioPacienteSingleModel.TraerCarga(propietario.Mascotas);
            Telefono_2 = propietario.Telefono_2;
            Direccion = propietario.Direccion;
        }

        public Propietario Transform(Propietario p)
        {

            p.Nombre = this.Nombre;
            p.Apellido = this.Apellido;
            p.Email = this.Email;
            p.Telefono = this.Telefono;
            p.Telefono_2 = this.Telefono_2;
            p.Direccion = this.Direccion;
            return p;
        }

        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(maximumLength: 100, MinimumLength = 4, ErrorMessage = "Nombre Entre 4 y 100 Caracteres")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        [StringLength(maximumLength: 100, MinimumLength = 4, ErrorMessage = "Apellido Entre 4 y 100 Caracteres")]
        public string Apellido { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un Correo Valido")]
        [Display(Name = "Direccion de Correo Electronico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Increse de esta forma '(000)000-0000'")]
        [Display(Name = "Numero Telefonico")]
        public string Telefono { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Increse de esta forma '(000)000-0000'")]
        [Display(Name = "Numero Telefonico de Respaldo")]
        public string Telefono_2 { get; set; }


        [Required]
        [StringLength(500,ErrorMessage ="La Direccion no puede ser tan pequeña",MinimumLength = 10)]
        public string Direccion { get; set; }


        public List<PropietarioPacienteSingleModel> Mascotas { get; set; }
    }

}
