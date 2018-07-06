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
            this.Mascotas = new List<PropietarioPaciente>();
        }
        public PropietarioViewModel(Propietario propietario)
        {
            ID = propietario.ID;
            Nombre = propietario.Nombre;
            Apellido = propietario.Apellido;
            Email = propietario.Email;
            Telefono = propietario.Telefono;
            Mascotas = propietario.Mascotas;
        }

        public Propietario Transform(Propietario p)
        {

            p.Nombre = this.Nombre;
            p.Apellido = this.Apellido;
            p.Email = this.Email;
            p.Telefono = this.Telefono;

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

        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un Correo Valido")]
        [Display(Name = "Direccion de Correo Electronico")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numero Telefonico")]
        [RegularExpression("[\\(]\\d{3}[\\)]\\d{3}[\\-]\\d{4}", ErrorMessage = "Formato invalido Usar '(000)000-0000'")]
        public string Telefono { get; set; }

        public virtual ICollection<PropietarioPaciente> Mascotas { get; set; }
    }
}
