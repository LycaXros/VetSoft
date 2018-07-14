﻿using System;
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
            var pa =await db.Paciente.Include(x => x.Propietarios).ToListAsync();
            pa.ForEach(x =>
            {
                pacientes.Add(new PacienteViewModel(x));
            });
            return View(pacientes);
        }

        [Route("Paciente/Nuevo")]
        public ActionResult Create()
        {
            ViewBag.Razas = new SelectList(db.Raza.ToList(), "ID", "Nombre");               
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
            var propietarios = db.Propietario.ToList();
            var q = (from pro in propietarios
                     where !propas.Exists(x => x.ClienteID == pro.ID )
                     select pro)
                    .ToList();

            ViewBag.TienePropietario = !propas.Exists(x => x.Tipo == (int)TipoPropietario.Propietario_Actual);

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
        public JsonResult SelectPropietario(PropietarioPacienteViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using ( var db = new VetSoftDBEntities())
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

                    return Json(new { success = false, message = "Error: "+ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Paciente/EliminarPropietario/")]
        public ActionResult EliminarRelacion(int cliID, int pacID)
        {
            return Json(new {  success = true, message = "LLega al Metodo" }, JsonRequestBehavior.AllowGet);
        }

    }


}