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
    public class PacienteController : Controller
    {
        VetSoftDBEntities db = new VetSoftDBEntities();

        public async Task<JsonResult> GetList()
        {
            var l = (await db.Paciente.ToListAsync());
            var lista = new List<PacienteViewModel>();
            l.ForEach(x =>
            {
                lista.Add(new PacienteViewModel(x));
            });

            var lr = lista
                .Select(x =>
                {
                    var fechaIng = x.FechaIngreso.ToShortDateString();

                    var now = DateTime.Now;
                    var nac = x.FechaNac;
                    var diff = now - nac;
                    var years = (diff.Days / 365);
                    var edad = $" {years} años y {diff.Days - (years * 365)} dias.";
                    return new
                    {
                        x.ID,
                        Nombre = x.FullName,
                        Raza = x.Raza.Nombre,
                        Ingreso = fechaIng,
                        Nacimiento = edad
                    };
                })
                .OrderBy(x => x.Ingreso)
                .ToList();

            return Json(new { data = lr }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DetalleView(int id)
        {
            var pac = await db.Paciente.FirstOrDefaultAsync(x => x.ID == id);

            if(pac == null)
            {
                return View("Error");
            }

            var model = new PacienteViewModel(pac);
            return PartialView(model);

        }

        [HttpGet]
        public async Task<JsonResult> AutoComplete(string term)
        {
            var l = await db.Paciente
                .Where(x => x.Nombre.StartsWith(term))
                .Take(10)
                .Select(r => new
                {
                    label = r.Nombre
                }).ToListAsync();

            return Json(l, JsonRequestBehavior.AllowGet);
        }

        // GET: Paciente
        [Route("Pacientes", Order = 1)]
        [Route("Pacientes/Index", Order = 2)]
        public async Task<ActionResult> Index(string search, int page = 1, bool isDetail = true)
        {
            if (isDetail)
            {
                return View("TableView");
            }

            List<PacienteViewModel> pacientes = new List<PacienteViewModel>();
            var pa = await db.Paciente.Include(x => x.Propietarios).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                pa = pa.Where(x => x.Nombre.StartsWith(search)).ToList();
            }

            pa.ForEach(x =>
            {
                pacientes.Add(new PacienteViewModel(x));
            });
            var list = pacientes
                .OrderBy(x => x.FechaIngreso)
                .ToList().ToPagedList(page, 10);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_PacienteList", list);
            }
            return View(list);
        }

        public async Task<PartialViewResult> Filtro(string search, int? page)
        {

            List<PacienteViewModel> pacientes = new List<PacienteViewModel>();
            var pa = await db.Paciente.Include(x => x.Propietarios).ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                pa = pa.Where(x => x.Nombre.StartsWith(search)).ToList();
            }

            pa.ForEach(x =>
            {
                pacientes.Add(new PacienteViewModel(x));
            });
            var list = pacientes.ToPagedList(page ?? 1, 10);
            return PartialView("_PacienteList", list);
        }

        [Route("Paciente/Nuevo")]
        public ActionResult Create()
        {
            var listaP = db.Propietario.AsEnumerable()
                .Select(x => {
                    var name = $" {x.Nombre} {x.Apellido}"; 
                    return new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = name
                    };
                }).ToList();
            ViewBag.Razas = new SelectList(db.Raza.ToList(), "ID", "Nombre");
            ViewBag.PropietariosList = listaP;
            //.Select(x => new SelectListItem
            //{
            //    Text = x.Nombre,
            //    Value = x.ID.ToString()
            //})
            //.ToList();
            return View();
        }

        [Route("Paciente/Nuevo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PropPacViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var proppac = new PropietarioPaciente()
                    {
                        Tipo = (int)TipoPropietario.Propietario_Actual,
                        FechaRegistro = DateTime.Now
                    };
                    Paciente p = new Paciente();
                    p.PacienteDeViewModel(model.Paciente);

                    db.Paciente.Add(p);
                    await db.SaveChangesAsync();
                    proppac.PacienteID = p.ID;

                    if (model.Propietario.ID != 0)
                    {
                        proppac.ClienteID = model.Propietario.ID;
                    }
                    else
                    {
                        Propietario pro = new Propietario();
                        pro.PropietarioDeViewModel(model.Propietario);
                        db.Propietario.Add(pro);
                        await db.SaveChangesAsync();
                        proppac.ClienteID = pro.ID;
                    }
                    db.PropietarioPaciente.Add(proppac);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
            var listaP = db.Propietario.AsEnumerable()
                .Select(x => {
                    var name = $" {x.Nombre} {x.Apellido}";
                    return new SelectListItem
                    {
                        Value = x.ID.ToString(),
                        Text = name,
                        Selected = x.ID == model.Propietario.ID
                    };
                }).ToList();
            ViewBag.Razas = new SelectList(db.Raza.ToList(), "ID", "Nombre",model.Paciente.RazaID);
            ViewBag.PropietariosList = listaP;
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
            var obj = new PacienteSingleModel(pac);
            return View(obj);
        }

        [Route("Paciente/Editar/{id?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(PacienteSingleModel paciente)
        {
            if (ModelState.IsValid)
            {
                var pac = await db.Paciente.FindAsync(paciente.ID);
                pac.PacienteDeSingleModel(paciente);
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
            //db.Configuration.UseDatabaseNullSemantics = false;
            var pac = await db.Paciente.Include(x => x.Propietarios).FirstAsync(x => x.ID == id.Value);
            if (pac == null)
            {
                return HttpNotFound();
            }
            var paciente = new PacienteViewModel(pac);
            var propas = pac.Propietarios.ToList();
            var propietarios = db.Propietario.ToList();
            var q = (from pro in propietarios
                     where !propas.Exists(x => x.ClienteID == pro.ID)
                     select pro)
                    .ToList();

            ViewBag.TienePropietario = !propas.Exists(x => x.Tipo == (int)TipoPropietario.Propietario_Actual);

            return View(paciente);
        }

        [HttpGet]
        [Route("Paciente/{id}/Propietarios_Select")]
        public async Task<ActionResult> SelectPropietario(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (!db.Paciente.Any(x => x.ID == id))
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
        public JsonResult SelectPropietario(PropietarioPacienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new VetSoftDBEntities())
                    {

                        if (db.PropietarioPaciente
                            .Any(x => x.ClienteID == model.ClienteID &&
                                x.PacienteID == model.PacienteID))
                        {
                            return Json(new { model, success = false, message = "Ya Exite este registro, no es permitido" }, JsonRequestBehavior.AllowGet);
                        }

                        var pp = new PropietarioPaciente()
                        {
                            ClienteID = model.ClienteID,
                            PacienteID = model.PacienteID,
                            Tipo = (int)TipoPropietario.Propietario_Actual,
                            FechaRegistro = DateTime.Today
                        };

                        db.PropietarioPaciente.Add(pp);
                        db.SaveChanges();
                        return Json(new { model, success = true, message = "Se ha Guardado de forma Exitosa" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Paciente/EliminarPropietario/")]
        public ActionResult EliminarRelacion(int cliID, int pacID)
        {
            return Json(new { success = true, message = "LLega al Metodo" }, JsonRequestBehavior.AllowGet);
        }

    }


}