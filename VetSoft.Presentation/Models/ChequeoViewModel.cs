using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{
    public class ChequeoViewModel
    {

        public ChequeoViewModel()
        {
            Medicacion = new List<MedicacionSingleModel>();
            Paciente = new PacienteSingleModel();
        }
        public ChequeoViewModel(Chequeo check)
        {
            ID = check.ID;
            PacienteID = check.PacienteID;
            Paciente = new PacienteSingleModel(check.Paciente);
            VetID = check.ID;
            Prediagnostico = check.Prediagnostico;
            Costo = check.Costo;
            Peso = check.Peso;
            Fecha = check.Fecha;
            Observaciones = check.Observaciones;
            Medicacion = MedicacionSingleModel.ListaMedTrans(check.Medicacion);
        }
        [Required]
        public int ID { get; set; }
        public int PacienteID { get; set; }
        public int VetID { get; set; }

        [Required(ErrorMessage = "Debe de digitar un Pre-Diagnostico")]
        [StringLength(500,MinimumLength = 4, ErrorMessage ="Muy pocas Palabras, escriba con más detalle")]
        [Display(Name ="Pre-Diagnostico")]
        [DataType(DataType.MultilineText)]
        public string Prediagnostico { get; set; }

        [Required(ErrorMessage = "Ingrese el valor del Servicio")]
        [DataType(DataType.Currency,ErrorMessage ="Favor de usar solo valores numericos")]
        public double Costo { get; set; }

        [Required(ErrorMessage = "Ingrese el peso del Paciente")]
        [Display(Name ="Peso del Paciente (KG - KiloGramos)")]
        [DataType(DataType.Currency,ErrorMessage ="Favor de solo usar valores numericos")]
        public double Peso { get; set; }
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe de digitar las observaciones sobre el estado del paciente")]
        [StringLength(1000, MinimumLength = 4, ErrorMessage = "Muy pocas Palabras, escriba con más detalle")]
        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        public PacienteSingleModel Paciente { get; set; }
        public List<MedicacionSingleModel> Medicacion { get; set; }
    }
}