using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{
    public class ChequeoViewModel
    {

        public ChequeoViewModel()
        {
            Medicacion = new List<Medicacion>();
            Paciente = new PacienteViewModel();
        }
        public ChequeoViewModel(Chequeo check)
        {
            ID = check.ID;
            PacienteID = check.PacienteID;
            Paciente = new PacienteViewModel(check.Paciente);
            VetID = check.ID;
            Prediagnostico = check.Prediagnostico;
            Costo = check.Costo;
            Peso = check.Peso;
            Fecha = check.Fecha;
            Observaciones = check.Observaciones;
            Medicacion = check.Medicacion.ToList();
        }

        public int ID { get; set; }
        public int PacienteID { get; set; }
        public int VetID { get; set; }
        public string Prediagnostico { get; set; }
        public double Costo { get; set; }
        public double Peso { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }

        public PacienteViewModel Paciente { get; set; }
        public List<Medicacion> Medicacion { get; set; }
    }
}