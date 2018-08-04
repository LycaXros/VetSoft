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
    public class ChequeosController : Controller
    {
        VetSoftDBEntities db = new VetSoftDBEntities();
        // GET: Chequeos
        public ActionResult Index()
        {
            return View();
        }

        [Route("Chequeo/Nuevo/{id?}")]
        public ActionResult Nuevo(int id = 0)
        {
            ChequeoViewModel model = new ChequeoViewModel();
            if (id != 0)
            {
                var p = db.Paciente.First(x => x.ID == id);

                if (p == null)
                    return RedirectToAction("Index", "Paciente");

                model.PacienteID = id;
                model.Paciente = new PacienteSingleModel(p);
                model.Fecha = DateTime.Now;

                return View(model);
            }
            return RedirectToAction("Index", "Paciente");
        }

        [HttpPost]
        [Route("Chequeo/Nuevo/{id?}")]
        public ActionResult Nuevo(ChequeoViewModel checkModel)
        {
            if (ModelState.IsValid)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Paciente"), message = "Logrado" }, JsonRequestBehavior.AllowGet);
            }

            return View(checkModel);
        }

    }
}