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
            this.Propietarios = new List<PropietarioPaciente>();
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
            Raza = paciente.Raza;
            FechaNac = paciente.FechaNac;
            FechaIngreso = paciente.FechaIngreso;
            Propietarios = paciente.Propietarios;
//            Apellido = Propietarios.FirstOrDefault().Propietario.Apellido;
        }

        public static ICollection<PacienteViewModel> GetFromModel(ICollection<Paciente> animales)
        {
            var r = new List<PacienteViewModel>();
            foreach(var i in animales)
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

        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }

        public virtual Raza Raza { get; set; }
        public virtual ICollection<PropietarioPaciente> Propietarios { get; set; }
        public string Apellido { get; private set; }
    }

    public class PropietarioPacienteViewModel
    {
        public PropietarioPacienteViewModel()
        {
            Paciente = new PacienteViewModel();
            Propietario = new PropietarioViewModel();
        }
        [Required(ErrorMessage = "HACE Falta ID del Cliente")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "HACE Falta ID del Paciente")]
        public int PacienteID { get; set; }
        public int Tipo { get; set; }

        public PacienteViewModel Paciente { get; set; }
        public PropietarioViewModel Propietario { get; set; }
    }
    public class PropPacViewModel
    {

        public PacienteViewModel Paciente { get; set; }
        public PropietarioViewModel Propietario { get; set; }
    }
}
