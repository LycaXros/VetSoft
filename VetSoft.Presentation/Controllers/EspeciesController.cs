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
    public class EspeciesController : Controller
    {
        private VetSoftDBEntities db = new VetSoftDBEntities();

        // GET: Especies
        [Route("Especies", Order = 1)]
        [Route("Especies/Index", Order = 2)]
        public async Task<ActionResult> Index()
        {
            return View(await db.Especie.ToListAsync());
        }

        // GET: Especies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Especie especie = await db.Especie.FindAsync(id);
                if (especie == null)
                {
                    return HttpNotFound();
                }
                return PartialView(especie);
            }
            Response.StatusCode = 500;
            return PartialView("Error");
        }

        // GET: Especies/Create
        [Route("Especie/Nueva")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Especies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Especie/Nueva")]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nombre,Nombre_Esp")] EspecieViewModel especie)
        {
            if (ModelState.IsValid)
            {
                var esp = new Especie()
                {
                    Nombre = especie.Nombre,
                    Nombre_Esp = especie.Nombre_Esp
                };
                db.Especie.Add(esp);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(especie);
        }

        // GET: Especies/Edit/5
        [Route("Especies/Editar/{id?}")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = await db.Especie.FindAsync(id);

            if (especie == null)
            {
                return HttpNotFound();
            }
            var esp = new EspecieViewModel()
            {
                ID = especie.ID,
                Nombre = especie.Nombre,
                Nombre_Esp = especie.Nombre_Esp,
                Razas = especie.Razas
            };
            return PartialView(esp);
        }

        // POST: Especies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Especies/Editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nombre,Nombre_Esp")] EspecieViewModel especie)
        {
            if (ModelState.IsValid)
            {
                var esp = db.Especie.First(x => x.ID == especie.ID);
                esp.Nombre = especie.Nombre;
                esp.Nombre_Esp = especie.Nombre_Esp;

                db.Entry(esp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return PartialView(especie);
        }

        // GET: Especies/Delete/5
        [Route("Especies/Eliminar/{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Especie especie = await db.Especie.FindAsync(id);
            if (especie == null)
            {
                return HttpNotFound();
            }
            return PartialView(especie);
        }

        // POST: Especies/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [Route("Especies/Eliminar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Especie especie = await db.Especie.FindAsync(id);
            if(especie == null)
            {
                return Json(new { success = false, message = "No se encuentra" }, JsonRequestBehavior.AllowGet);
            }
            if(especie.Razas.Count > 0)
            {
                return Json(new { success = false, message = "Esta Especie contiene Razas hijos, no es posible Eliminar" }, JsonRequestBehavior.AllowGet);
            }
            db.Especie.Remove(especie);
            await db.SaveChangesAsync();
            //return RedirectToAction("Index");
            return Json(new { success = true, message = "Especie Eliminada" }, JsonRequestBehavior.AllowGet);
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
