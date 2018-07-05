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

        // GET: Medicamentos
        [Route("Medicamentos")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Medicamento.ToListAsync());
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
            return View(med);
        }

        // GET: Medicamentos/Create
        [Route("Nueva")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medicamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Nueva")]
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
            var med = new MedicamentosViewModel()
            {
                ID = medicamento.ID,
                Nombre = medicamento.Nombre,
                Descripcion = medicamento.Descripcion,
                TipoID = medicamento.TipoID
            };
            return View(med);
        }

        // POST: Medicamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nombre,Descripcion,TipoID")] Medicamento medicamento)
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
            return View(medicamento);
        }

        // GET: Medicamentos/Delete/5
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
            return View(medicamento);
        }

        // POST: Medicamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Medicamento medicamento = await db.Medicamento.FindAsync(id);
            //db.Medicamento.Remove(medicamento);
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
