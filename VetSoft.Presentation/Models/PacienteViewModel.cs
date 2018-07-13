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
            if (paciente.Genero == Constantes.Masculino) Genero = Sexo.Masculino;
            else if (paciente.Genero == Constantes.Femenino) Genero = Sexo.Femenino;
            else Genero = null;
            Microchip_Licencia = paciente.Microchip_Licencia;
            RazaID = paciente.RazaID;
            Raza = paciente.Raza;
            FechaNac = paciente.FechaNac;
            FechaIngreso = paciente.FechaIngreso;
            Propietarios = paciente.Propietarios;
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

        public DateTime FechaIngreso { get; set; }

        public virtual Raza Raza { get; set; }
        public virtual ICollection<PropietarioPaciente> Propietarios { get; set; }
    }

    public class PropietarioPacienteViewModel
    {
        [Required(ErrorMessage = "HACE Falta ID del Cliente")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "HACE Falta ID del Paciente")]
        public int PacienteID { get; set; }
        public int Tipo { get; set; }
    }
}
