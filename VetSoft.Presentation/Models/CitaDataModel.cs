using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{
    public class CitaDataModel
    {
        public CitaDataModel(Cita model)
        {
            ID = model.ID;
            Fecha = model.Fecha;
            FechaString = model.Fecha.ToShortDateString();
            Paciente = new PacienteSingleModel(model.Paciente);
            Motivo = model.Motivo;
        }

        public int ID { get; set; }
        public DateTime Fecha { get; set; }

        [Display(Name ="Fecha de la Cita")]
        public string FechaString { get; set; }
        [Display(Name ="Paciente")]
        public PacienteSingleModel Paciente { get; set;  }
        [Display(Name = "Motivo de la Cita")]
        public string Motivo { get; set; }
    }
}