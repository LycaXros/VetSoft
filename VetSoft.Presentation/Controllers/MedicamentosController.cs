using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VetSoft.Data;
using VetSoft.Presentation.Models;

namespace VetSoft.Presentation.Controllers
{
    [Authorize]
    public class MedicamentosController : Controller
    {
        private VetSoftDBEntities db = new VetSoftDBEntities();

        public async Task<JsonResult> GetList()
        {
            var list = (await db.Medicamento.ToListAsync())
                .Select(x => new {
                    x.ID, x.Nombre, x.Descripcion, Tipo = x.Tipo.Nombre
                })
                .OrderBy(x => x.ID)
                .ToList();

            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
        // GET: Medicamentos
        [Route("Medicamentos")]
        [Route("Medicamentos/Index")]
        public async Task<ActionResult> Index()
        {
            var l = await db.Medicamento.ToListAsync();
            List<MedicamentosViewModel> lista = new List<MedicamentosViewModel>();
            l.ForEach((x) =>
            {
                lista.Add(new MedicamentosViewModel(x));
            });
            return View(lista);
        }

        // GET: Medicamentos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicamento medicamento = await db.Medicamento.FindAsync(id);
            if (medicamento == null)
            {
                return HttpNotFound();
            }
            var med = new MedicamentosViewModel(medicamento);
            //{
            //    ID = medicameto.ID,
            //    Nombre = medicamento.Nombre,
            //    Descripcion = medicamento.Descripcion,
            //    TipoID = medicamento.TipoID
            //};
            return PartialView(med);
        }

        // GET: Medicamentos/Create
        [Route("Medicamento/Nuevo")]
        public ActionResult Create()
        {
            ViewBag.MedType = new SelectList(db.Tipo_Med.ToList(), "ID", "Nombre");
            return View();
        }

        // POST: Medicamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Medicamento/Nuevo")]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nombre,Descripcion,TipoID")] Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                db.Medicamento.Add(medicamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(medicamento);
        }

        // GET: Medicamentos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var medicamento = await db.Medicamento.FindAsync(id);

            if (medicamento == null)
            {
                return HttpNotFound();
            }
            var med = new MedicamentosViewModel(medicamento);
            ViewBag.MedType = new SelectList(db.Tipo_Med.ToList(), "ID", "Nombre");
            return PartialView(med);
        }

        // POST: Medicamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nombre,Descripcion,TipoID")] MedicamentosViewModel medicamento)
        {
            if (ModelState.IsValid)
            {
                var med = db.Medicamento.First(x => x.ID == medicamento.ID);
                med.Nombre = medicamento.Nombre;
                med.Descripcion = medicamento.Descripcion;
                med.TipoID = medicamento.TipoID;

                db.Entry(med).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return PartialView(medicamento);
        }

        // GET: Medicamentos/Delete/5
        [Route("Medicamentos/Eliminar/{id?}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicamento medicamento = await db.Medicamento.FindAsync(id);
            if (medicamento == null)
            {
                return HttpNotFound();
            }
            return PartialView(medicamento);
        }

        // POST: Medicamentos/Delete/5
        [Route("Medicamentos/Eliminar/{id}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Medicamento medicamento = await db.Medicamento.FindAsync(id);

            if (medicamento == null)
            {
                return Json(new { success = false, message = "No se encuentra" }, JsonRequestBehavior.AllowGet);
            }
            //if (medicamento.Count > 0)
            //{
            //    return Json(new { success = false, message = "Esta Especie contiene Razas hijos, no es posible Eliminar" }, JsonRequestBehavior.AllowGet);
            //}
            db.Medicamento.Remove(medicamento);
            await db.SaveChangesAsync();
            return Json(new { success = true, message = "Registro de Medicamento Eliminado" }, JsonRequestBehavior.AllowGet);
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
