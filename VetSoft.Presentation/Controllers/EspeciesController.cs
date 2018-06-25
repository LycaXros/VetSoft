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
    public class EspeciesController : Controller
    {
        private VetSoftDBEntities db = new VetSoftDBEntities();

        // GET: Especies
        [Route("Especies")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Especie.ToListAsync());
        }

        // GET: Especies/Details/5
        public async Task<ActionResult> Details(int? id)
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
            return View(especie);
        }

        // GET: Especies/Create
        [Route("Nueva")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Especies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Nueva")]
        public async Task<ActionResult> Create([Bind(Include = "ID,Nombre,Nombre_Esp")] Especie especie)
        {
            if (ModelState.IsValid)
            {
                db.Especie.Add(especie);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(especie);
        }

        // GET: Especies/Edit/5
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
            return View(especie);
        }

        // POST: Especies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Nombre,Nombre_Esp")] Especie especie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especie).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(especie);
        }

        // GET: Especies/Delete/5
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
            return View(especie);
        }

        // POST: Especies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Especie especie = await db.Especie.FindAsync(id);
            //db.Especie.Remove(especie);
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