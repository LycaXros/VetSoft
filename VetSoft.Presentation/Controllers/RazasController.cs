﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VetSoft.Data;

namespace VetSoft.Presentation.Controllers
{
    [Authorize]
    public class RazasController : Controller
    {
        private VetSoftDBEntities db = new VetSoftDBEntities();

        // GET: Razas
        [Route("Razas", Order = 1)]
        [Route("Razas/Index", Order = 2)]
        public async Task<ActionResult> Index()
        {
            var raza = db.Raza.Include(r => r.Especie);
            return View(await raza.ToListAsync());
        }

        // GET: Razas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Request.IsAjaxRequest())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Raza raza = await db.Raza.FindAsync(id);
                if (raza == null)
                {
                    return HttpNotFound();
                }
                return PartialView(raza);
            }
            Response.StatusCode = 500;
            return PartialView("Error");
        }

        // GET: Razas/Create
        [Route("Razas/Nuevo")]
        public ActionResult Create()
        {
            ViewBag.EspecieID = new SelectList(db.Especie, "ID", "Nombre");
            return View();
        }

        // POST: Razas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Razas/Nuevo")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nombre,EspecieID")] Raza raza)
        {
            if (ModelState.IsValid)
            {
                db.Raza.Add(raza);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EspecieID = new SelectList(db.Especie, "ID", "Nombre", raza.EspecieID);
            return View(raza);
        }

        // GET: Razas/Edit/5
        [Route("Razas/Editar/{id?}")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raza raza = await db.Raza.FindAsync(id);
            if (raza == null)
            {
                return HttpNotFound();
            }
            ViewBag.EspecieID = new SelectList(db.Especie, "ID", "Nombre", raza.EspecieID);
            return PartialView(raza);
        }

        // POST: Razas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Razas/Editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nombre,EspecieID")] Raza raza)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raza).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EspecieID = new SelectList(db.Especie, "ID", "Nombre", raza.EspecieID);
            return PartialView(raza);
        }

        // GET: Razas/Delete/5
        [Route("Razas/Eliminar/{id?}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Raza raza = await db.Raza.FindAsync(id);
            if (raza == null)
            {
                return HttpNotFound();
            }
            return PartialView(raza);
        }

        // POST: Razas/Delete/5
        [HttpPost]
        [Route("Razas/Eliminar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Raza raza = await db.Raza.FindAsync(id);
            if(raza.Animales.Count > 0)
            {
                var msgError = 
                    "No se pudo Eliminar Registro" +
                    " debido a que exiten pacientes de la raza";
                return Json(new { success = false, message = msgError }, JsonRequestBehavior.AllowGet);
            }
            db.Raza.Remove(raza);
            await db.SaveChangesAsync();
            return Json(new { success = true, message = "Raza Eliminada" }, JsonRequestBehavior.AllowGet);
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
