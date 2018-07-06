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
    public class PacienteController : Controller
    {
        VetSoftDBEntities db = new VetSoftDBEntities();

        // GET: Paciente
        [Route("Pacientes", Order = 1)]
        [Route("Paciente/Index", Order = 2)]
        public async Task<ActionResult> Index()
        {
            List<PacienteViewModel> pacientes = new List<PacienteViewModel>();
            var pa = db.Paciente.Include(x => x.Propietarios);
            (await pa.ToListAsync()).ForEach(x =>
            {
                pacientes.Add(new PacienteViewModel(x));
            });
            return View(pacientes);
        }

        [Route("Paciente/Nuevo")]
        public ActionResult Create()
        {
            ViewBag.Razas = db.Raza
                .Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.ID.ToString()
                })
                .ToList();
            return View();
        }

        [Route("Paciente/Nuevo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PacienteViewModel paciente)
        {
            if (ModelState.IsValid)
            {
                Paciente p = new Paciente();
                p.PacienteDeViewModel(paciente);

                db.Paciente.Add(p);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Razas = db.Raza
                .Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.ID.ToString()
                })
                .ToList();
            return View();
        }

        [Route("Paciente/Editar/{id?}")]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var pac = await db.Paciente.FindAsync(id);
            if (pac == null)
            {
                return HttpNotFound();
            }

            ViewBag.Razas = db.Raza
                .Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.ID.ToString()
                })
                .ToList();
            var obj = new PacienteViewModel(pac);
            return View(obj);
        }

        [Route("Paciente/Editar/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(PacienteViewModel paciente)
        {
            if (ModelState.IsValid)
            {
                var pac = await db.Paciente.FindAsync(paciente.ID);
                pac.PacienteDeViewModel(paciente);
                db.Entry(pac).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Razas = db.Raza
                .Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.ID.ToString()
                })
                .ToList();
            return View(paciente);
        }

        [Route("Paciente/Eliminar/{id?}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var pac = await db.Paciente.FindAsync(id);
            if (pac == null)
            {
                return HttpNotFound();
            }
            var paciente = new PacienteViewModel(pac);
            return PartialView("Elim", paciente);
        }

        [HttpPost]
        [Route("Paciente/Eliminar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Task.Run(() => { System.Threading.Thread.Sleep(1000); });
            TempData["msgError"] =
                "No se pudo Eliminar Registro" +
                " debido Paciente Tiene Chequeos Registrados";

            //db.Raza.Remove(raza);
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Route("Paciente/{id?}/Propietarios")]
        public async Task<ActionResult> VerPropietarios(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            db.Configuration.UseDatabaseNullSemantics = false;
            var pac = await db.Paciente.AsNoTracking().Include(x => x.Propietarios).FirstAsync(x => x.ID == id.Value);
            if (pac == null)
            {
                return HttpNotFound();
            }
            var paciente = new PacienteViewModel(pac);
            var propas = pac.Propietarios.ToList();
            var q = (from pro in db.Propietario
                     where !propas.Any(x => x.Propietario == pro)
                     select pro)
                    .ToList();

            ViewBag.TienePropietario = !propas.ToList().Any(x => x.Tipo == (int)TipoPropietario.Propietario_Actual);

            return View(paciente);
        }

        [HttpGet]
        [Route("Paciente/{id}/Propietarios_Select")]
        public async Task<ActionResult> SelectPropietario(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if(!db.Paciente.Any(x=>x.ID==id))
            {
                return HttpNotFound();
            }
            var pp = new PropietarioPacienteViewModel()
            {
                PacienteID = id.Value
            };
            ViewBag.PropietariosList = await db.Propietario
                .Select(x => new SelectListItem()
                {
                    Text = x.Nombre,
                    Value = x.ID.ToString()
                })
                .ToListAsync();
            return PartialView(pp);
        }

        [HttpPost]
        [Route("Paciente/{id}/Propietarios_Select")]
        public ActionResult SelectPropietario(PropietarioPacienteViewModel model)
        {

            return Json(new { model, success = true, message = "Se ha Guardado de forma Exitosa" }, JsonRequestBehavior.AllowGet);
        }
    }
}