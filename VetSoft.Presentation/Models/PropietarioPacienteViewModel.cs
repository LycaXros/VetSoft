using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{
    public class PropietarioPacienteViewModel
    {
        public PropietarioPacienteViewModel()
        {
            Paciente = new PacienteSingleModel();
            Propietario = new PropietarioViewModel();
        }

        public PropietarioPacienteViewModel( PropietarioPaciente pp)
        {
            ClienteID = pp.ClienteID;
            PacienteID = pp.PacienteID;
            Tipo = pp.Tipo;
            FechaRegistro = pp.FechaRegistro;
            Paciente = new PacienteSingleModel(pp.Paciente);
            Propietario = new PropietarioViewModel(pp.Propietario);
        }
        [Required(ErrorMessage = "HACE Falta ID del Cliente")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "HACE Falta ID del Paciente")]
        public int PacienteID { get; set; }
        public int Tipo { get; set; }

        public DateTime FechaRegistro { get; set; }

        public PacienteSingleModel Paciente { get; set; }
        public PropietarioViewModel Propietario { get; set; }
    }


    public class PropietarioPacienteSingleModel
    {
        public static List<PropietarioPacienteSingleModel> TraerCarga(IEnumerable<PropietarioPaciente> pp)
        {
            var props = new List<PropietarioPacienteSingleModel>();
            pp.ToList()
                   .ForEach(x =>
                   {
                       props.Add(new PropietarioPacienteSingleModel()
                       {
                           ClienteID = x.ClienteID,
                           PacienteID = x.PacienteID,
                           Tipo = x.Tipo
                       });
                   });
            return props;
        }
        [Required(ErrorMessage = "Hace Falta ID del Cliente")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "Hace Falta ID del Paciente")]
        public int PacienteID { get; set; }
        public int Tipo { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
    public class PropPacViewModel
    {
        public PropPacViewModel()
        {
            Paciente = new PacienteViewModel();
            Propietario = new PropietarioSingleValModel();
        }
        public PacienteViewModel Paciente { get; set; }
        public PropietarioSingleValModel Propietario { get; set; }
    }
}
