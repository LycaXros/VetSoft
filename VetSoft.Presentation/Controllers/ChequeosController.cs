using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VetSoft.Data;
using VetSoft.Presentation.Models;

namespace VetSoft.Presentation.Controllers
{
    [Authorize]
    public class ChequeosController : Controller
    {
        VetSoftDBEntities db = new VetSoftDBEntities();
        // GET: Chequeos
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> TraerMedicinas(int MedType)
        {
            var res = (await db.Medicamento.Where(x => x.TipoID == MedType).ToListAsync())
                .Select(x => new
                {
                    x.ID,
                    NombreMed = x.Nombre
                }).ToList();

            return new JsonResult { Data = res, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Route("Chequeo/Nuevo/{id?}")]
        public ActionResult Nuevo(int id = 0)
        {
            ChequeoViewModel model = new ChequeoViewModel();
            if (id != 0)
            {
                var p = db.Paciente.First(x => x.ID == id);
                ViewBag.TipoMed = new SelectList(db.Tipo_Med, "ID", "Nombre");
                if (p == null)
                    return RedirectToAction("Index", "Paciente");

                model.PacienteID = id;
                model.Paciente = new PacienteSingleModel(p);
                model.Fecha = DateTime.Now;

                return View(model);
            }
            return RedirectToAction("Index", "Paciente");

        }

        //public ActionResult PrintPage(int id)
        //{
        //    var PDF = IronPdf.HtmlToPdf.
        //        StaticRenderUrlAsPdf(new Uri("http://localhost:53683/Reportes/ReporteCitasPaciente.aspx"));
        //    return File(PDF.BinaryData, "application/pdf", "Something.Pdf");
        //}

        [HttpPost]
        [Route("Chequeo/Nuevo/{id}")]
        public ActionResult Nuevo(ChequeoViewModel checkModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    checkModel.Fecha = DateTime.Now;
                    var chequeo = checkModel.ReturnModel(new Chequeo());
                    db.Chequeo.Add(chequeo);
                    db.SaveChanges();

                    if (checkModel.FechaCitaProx.HasValue)
                    {
                        var cita = new Cita()
                        {
                            Fecha = checkModel.FechaCitaProx.Value,
                            PacienteID = checkModel.PacienteID,
                            VetID = chequeo.VetID,
                            Motivo = $"Cita Proxima para el paciente, que tuvo las siguientes observaciones :" +
                            $" {chequeo.Observaciones}"
                        };
                        db.Cita.Add(cita);
                    }


                    chequeo.Medicacion = MedicacionSingleModel.ObtenerMedicaciones(checkModel.Medicacion, chequeo.ID, checkModel.FechaCitaProx);
                    db.SaveChanges();

                    return Json(new
                    {
                        success = true,
                        redirectUrl = Url.Action("Index", "Paciente"),
                        message = "Logrado"
                    },
                        JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    redirectUrl = Url.Action("Nuevo", "Chequeos", new { id = checkModel.PacienteID }),
                    message = ex.Message
                },
                    JsonRequestBehavior.AllowGet);
            }
            ViewBag.TipoMed = new SelectList(db.Tipo_Med, "ID", "Nombre");
            return View(checkModel);
        }

        [Route("Chequeos/Paciente/{id?}")]
        public async Task<ActionResult> HistoricoPaciente(int id = 0)
        {
            if (id != 0)
            {
                if (!db.Paciente.Any(x => x.ID == id))
                    return RedirectToAction("Index", "Paciente");

                var pac = new PacienteSingleModel(
                    await db.Paciente.FirstAsync(x => x.ID == id)
                    );
                return View(pac);
            }
            return RedirectToAction("Index", "Paciente");
        }
        public async Task<JsonResult> ListaHistoricoPaciente(int id)
        {
            var listaCheck = await db.Chequeo.Where(x => x.PacienteID == id).ToListAsync();

            var modelList = listaCheck.Select(x => {
                var strFecha = x.Fecha.ToShortDateString();
                var dataMeds = new List<MedicacionDataModel>();
                x.Medicacion.ToList()
                .ForEach(z =>
                {
                    dataMeds.Add(new MedicacionDataModel(z));
                });
                return new {
                    x.ID,
                    Medicaciones = dataMeds,
                    x.Peso, x.Costo,
                    PreD = x.Prediagnostico,
                    Obs = x.Observaciones,
                    Fecha = strFecha
                };
            })
                .ToList();

            return Json(new { data = modelList }, JsonRequestBehavior.AllowGet);

        }

    }
}