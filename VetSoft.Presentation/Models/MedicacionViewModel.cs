using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VetSoft.Data;

namespace VetSoft.Presentation.Models
{
    public class MedicacionViewModel
    {
        public MedicacionViewModel()
        {
            Chequeo = new ChequeoViewModel();
        }
        public MedicacionViewModel(Medicacion med)
        {
            ID = med.ID;
            Tipo = med.Tipo;
            Indicacion = med.Indicacion;
            Fecha = med.Fecha;
            ChequeoID = med.ChequeoID;
            Chequeo = new ChequeoViewModel(med.Chequeo);
        }
        public static List<MedicacionViewModel> ListaMedTrans(IEnumerable<Medicacion> meds)
        {
            var ret = new List<MedicacionViewModel>();
            meds.ToList()
                .ForEach(x =>
                {
                    ret.Add(new MedicacionViewModel(x));
                });
            return ret;
        }

        public int ID { get; set; }
        [Required(ErrorMessage = "Seleccione un Tipo valido")]
        [Display(Name = "Tipo de Medicacion")]
        public int Tipo { get; set; }

        [Required(ErrorMessage = "Ingrese las indicaciones")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 4, ErrorMessage = "Muy pocas Palabras, escriba con más detalle")]
        public string Indicacion { get; set; }

        public DateTime? Fecha { get; set; }
        public int ChequeoID { get; set; }

        public  ChequeoViewModel Chequeo
        {
            get; set;
        }
    }

    public class MedicacionSingleModel
    {
        public MedicacionSingleModel()
        {
           // Chequeo = new ChequeoViewModel();
        }
        public MedicacionSingleModel(Medicacion med)
        {
            ID = med.ID;
            Tipo = med.Tipo;
            Indicacion = med.Indicacion;
            Fecha = med.Fecha;
            ChequeoID = med.ChequeoID;
           // Chequeo = new ChequeoSingleModel(med.Chequeo);
        }
        public static List<MedicacionSingleModel> ListaMedTrans(IEnumerable<Medicacion> meds)
        {
            var ret = new List<MedicacionSingleModel>();
            meds.ToList()
                .ForEach(x =>
                {
                    ret.Add(new MedicacionSingleModel(x));
                });
            return ret;
        }
        public int ID { get; set; }
        [Required(ErrorMessage = "Seleccione un Tipo valido")]
        [Display(Name = "Tipo de Medicacion")]
        public int Tipo { get; set; }

        public static List<Medicacion> ObtenerMedicaciones(List<MedicacionSingleModel> medicaciones, int checkID, DateTime? posibleNextDate)
        {
            List<Medicacion> res = new List<Medicacion>();
            medicaciones.ForEach(x => {
                res.Add(new Medicacion
                {
                    Tipo= x.Tipo,
                    Indicacion = x.Indicacion,
                    ChequeoID = checkID,
                    Fecha = posibleNextDate
                });
            });
            return res;
        }

        public string TipoName { get; set; }

        [Required(ErrorMessage = "Ingrese las indicaciones")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 4, ErrorMessage = "Muy pocas Palabras, escriba con más detalle")]
        public string Indicacion { get; set; }

        public DateTime? Fecha { get; set; }
        public int ChequeoID { get; set; }

        //public ChequeoSingleModel Chequeo
        //{
        //    get; set;
        //}
    }

    public class MedicacionDataModel
    {
        public MedicacionDataModel(Medicacion med)
        {
            MedName = med.Medicamento.Nombre;
            MedIndicacion = med.Indicacion;
        }

        public string MedName { get; }
        public string MedIndicacion { get; }
    }
}