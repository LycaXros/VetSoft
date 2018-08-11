using Foolproof;
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
            p.Direccion = this.Direccion.ToString();
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
        [Display(Name = "Dirección de Correo Electronico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Increse con este formato '(000)000-0000'")]
        [Display(Name = "Número Telefónico")]
        public string Telefono { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Increse con este formato '(000)000-0000'")]
        [Display(Name = "Numero Telefónico Alternativo")]
        public string Telefono_2 { get; set; }

        [Required]
        [Display(Name = "Direccion Residencia")]
        [StringLength(500, ErrorMessage = "La Direccion no puede ser tan pequeña", MinimumLength = 10)]
        public string Direccion { get; set; }


        public string FullName()
        {
            return $"{Nombre} {Apellido}";
        }
        public List<PropietarioPacienteSingleModel> Mascotas { get; set; }
    }

    public class DireccionC
    {
        public DireccionC()
        {

        }
        public DireccionC(string fullDir)
        {
            var splitted = fullDir.Split(';');

        }
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();

            res.Append(Calle + "; ");
            res.Append(Casa + "; ");
            res.Append(Sector + "; ");
            res.Append(Municipio + "; ");
            res.Append(Provincia + ";");

            return res.ToString();
        }

        [Required]
        public string Calle { set; get; }

        [Required]
        [Display(Name ="Numero de Casa")]
        public string Casa { set; get; }

        [Required]
        public string Sector { set; get; }

        [Required]
        public string Municipio { set; get; }

        [Required]
        public string Provincia { set; get; }
    }

    public class PropietarioSingleValModel
    {
        public PropietarioSingleValModel()
        {
            NeedID = true;
            NeedOther = true;
            this.Mascotas = new List<PropietarioPacienteSingleModel>();
        }
        public PropietarioSingleValModel(Propietario propietario)
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
            p.Direccion = this.Direccion.ToString();
            return p;
        }
        [Required]
        public bool NeedID { get; set; }

        [Required]
        public bool NeedOther { get; set; }

        [RequiredIf("NeedID", true, ErrorMessage = "Es campo no debe Faltar")]
        public int ID { get; set; }

        [RequiredIf("NeedID", false, ErrorMessage = "Es campo no debe Faltar")]
        [Display(Name = "Nombre")]
        [StringLength(maximumLength: 100, MinimumLength = 4, ErrorMessage = "Nombre Entre 4 y 100 Caracteres")]
        public string Nombre { get; set; }

        [RequiredIf("NeedID", false, ErrorMessage = "Es campo no debe Faltar")]
        [Display(Name = "Apellido")]
        [StringLength(maximumLength: 100, MinimumLength = 4, ErrorMessage = "Apellido Entre 4 y 100 Caracteres")]
        public string Apellido { get; set; }

        [RequiredIf("NeedID", false, ErrorMessage = "Es campo no debe Faltar")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un Correo Valido")]
        [Display(Name = "Direccion de Correo Electronico")]
        public string Email { get; set; }

        [RequiredIf("NeedID", false, ErrorMessage = "Es campo no debe Faltar")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Increse de esta forma '(000)000-0000'")]
        [Display(Name = "Numero Telefonico")]
        public string Telefono { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Increse de esta forma '(000)000-0000'")]
        [Display(Name = "Numero Telefonico Alterno")]
        public string Telefono_2 { get; set; }

        [RequiredIf("NeedID", false, ErrorMessage = "Es campo no debe Faltar" )]
        [Display(Name = "Direccion Residencia")]
        [StringLength(500, ErrorMessage = "La Direccion no puede ser tan pequeña", MinimumLength = 10)]
        public string Direccion { get; set; }

        public string FullName()
        {
            return $"{Nombre} {Apellido}";
        }
        public List<PropietarioPacienteSingleModel> Mascotas { get; set; }
    }

}
