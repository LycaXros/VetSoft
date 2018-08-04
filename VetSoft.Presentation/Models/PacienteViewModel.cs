using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{
    public class PacienteViewModel
    {
        public PacienteViewModel()
        {
            this.Propietarios = new List<PropietarioPacienteViewModel>();
        }
        public PacienteViewModel(Paciente paciente)
        {
            ID = paciente.ID;
            Nombre = paciente.Nombre;
            Color = paciente.Color;
            if (paciente.Genero == Constantes.Macho) Genero = Sexo.Macho;
            else if (paciente.Genero == Constantes.Hembra) Genero = Sexo.Hembra;
            else Genero = null;
            Microchip_Licencia = paciente.Microchip_Licencia;
            RazaID = paciente.RazaID;
            Raza = new RazaViewModel(paciente.Raza);
            FechaNac = paciente.FechaNac;
            FechaIngreso = paciente.FechaIngreso;
            Propietarios = new List<PropietarioPacienteViewModel>();
            paciente.Propietarios.ToList()
                .ForEach(x =>
                {
                    Propietarios.Add(new PropietarioPacienteViewModel(x));
                });
        }

        public static List<PacienteViewModel> GetFromModel(ICollection<Paciente> animales)
        {
            var r = new List<PacienteViewModel>();
            foreach (var i in animales)
            {
                r.Add(new PacienteViewModel(i));
            }
            return r;
        }

        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre del Animal")]
        public string Nombre { get; set; }

        public string FullName
        {
            get
            {
                System.Text.StringBuilder fullName = new System.Text.StringBuilder();

                fullName.Append(" ");
                fullName.Append(Nombre);
                fullName.Append(" ");
                if (Propietarios.Count > 0)
                    fullName.Append(Propietarios
                        .First(x => x.Tipo.Equals((int)TipoPropietario.Propietario_Actual))
                        .Propietario
                        .Apellido);
                fullName.Append(" ");
                return fullName.ToString();
            }
        }

        [Required]
        [Display(Name = "Color de pelo o de Piel")]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Microchip o Licencia")]
        public string Microchip_Licencia { get; set; }

        [Required]
        [Display(Name = "Genero")]
        public Sexo? Genero { get; set; }

        [Required]
        [Display(Name = "Raza")]
        public int RazaID { get; set; }

        [Required]
        [Display(Name = "Fecha Nacimiento")]
        public DateTime FechaNac { get; set; }

        public string Edad
        {
            get
            {
                var edad = "";
                var now = DateTime.Now;
                var nac = FechaNac;
                var diff = now - nac;
                var years = (diff.Days / 365);
                edad = $"{years} años y {diff.Days - (years * 365)} dias.";
                return edad;
            }
        }


        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }

        public RazaViewModel Raza { get; set; }
        public List<PropietarioPacienteViewModel> Propietarios { get; set; }

    }

    public class PacienteSingleModel
    {
        private string fullName = "";

        public PacienteSingleModel()
        {

        }

        public PacienteSingleModel(Paciente paciente)
        {
            ID = paciente.ID;
            Nombre = paciente.Nombre;
            Color = paciente.Color;
            if (paciente.Genero == Constantes.Macho) Genero = Sexo.Macho;
            else if (paciente.Genero == Constantes.Hembra) Genero = Sexo.Hembra;
            else Genero = null;
            Microchip_Licencia = paciente.Microchip_Licencia;
            RazaID = paciente.RazaID;
            Raza = new RazaSingleModel(paciente.Raza);
            FechaNac = paciente.FechaNac;
            FechaIngreso = paciente.FechaIngreso;
            Propietarios = PropietarioPacienteSingleModel.TraerCarga(paciente.Propietarios);


            System.Text.StringBuilder fullname = new System.Text.StringBuilder();

            fullname.Append(" ");
            fullname.Append(Nombre);
            fullname.Append(" ");
            if (Propietarios.Count > 0)
                fullname.Append(paciente.Propietarios
                    .First(x => x.Tipo.Equals((int)TipoPropietario.Propietario_Actual))
                    .Propietario
                    .Apellido);
            fullname.Append(" ");
            this.fullName = fullname.ToString();
        }

        public static List<PacienteSingleModel> GetFromModel(ICollection<Paciente> animales)
        {
            var r = new List<PacienteSingleModel>();
            foreach (var i in animales)
            {
                r.Add(new PacienteSingleModel(i));
            }
            return r;
        }

        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre del Animal")]
        public string Nombre { get; set; }

        public string FullName
        {
            get
            {
                return fullName;
            }
        }

        [Required]
        [Display(Name = "Color de pelo o de Piel")]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Microchip o Licencia")]
        public string Microchip_Licencia { get; set; }

        [Required]
        [Display(Name = "Genero")]
        public Sexo? Genero { get; set; }

        [Required]
        [Display(Name = "Raza")]
        public int RazaID { get; set; }

        [Required]
        [Display(Name = "Fecha Nacimiento")]
        public DateTime FechaNac { get; set; }

        public string Edad
        {
            get
            {
                var edad = "";
                var now = DateTime.Now;
                var nac = FechaNac;
                var diff = now - nac;
                var years = (diff.Days / 365);
                edad = $"{years} años y {diff.Days - (years * 365)} dias.";
                return edad;
            }
        }


        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }

        public RazaSingleModel Raza { get; set; }
        public List<PropietarioPacienteSingleModel> Propietarios { get; set; }

    }
}
