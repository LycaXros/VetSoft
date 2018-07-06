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
    public class PropietariosController : Controller
    {

        // GET: Propietarios
        [Route("Propietarios")]
        [Route("Propietarios/Index")]
        public ActionResult Index()
        {
            //List<PropietarioViewModel> lista = await ListarPropietarios();
            return View();
        }

        public async Task<JsonResult> GetList()
        {
            var lista = await ListarPropietarios();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [Route("Propietario/NuevoEditar/{id?}")]
        public async Task<ActionResult> AddOrUpdate(int id = 0)
        {
            if (id == 0)
            {
                return PartialView(new PropietarioViewModel());
            }
            else
            {
                using (var db = new VetSoftDBEntities())
                {

                    var p = await db.Propietario.FirstAsync(x=>x.ID == id );
                    if (p == null)
                    {
                        return HttpNotFound();
                    }
                    return PartialView(new PropietarioViewModel(p));
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Propietario/NuevoEditar/{id?}")]
        public JsonResult AddOrUpdate(PropietarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new VetSoftDBEntities())
                {

                    var p = new Propietario();
                    if (model.ID == 0)
                    {
                        p = model.Transform(p);
                        db.Propietario.Add(p);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Nuevo Registro de Propietario Guardado" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (db.Propietario.Any(x => x.ID == model.ID))
                        {

                            p = db.Propietario.First(x => x.ID == model.ID);

                            p = model.Transform(p);
                            db.Entry(p).State = EntityState.Modified;
                            db.SaveChanges();
                            return Json(new { success = true, message = "Registro de Propietario Editado y Guardado" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                            return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);

                    }
                }
            }
            return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Route("Propietario/Eliminar/{id?}")]
        public async Task<ActionResult> Eliminar(int? id)
        {
            await Task.Run(()=> { System.Threading.Thread.Sleep(1000); });

            if (id == null) return HttpNotFound();

            return Json(new { success = true, message = "Eliminando Registro" }, JsonRequestBehavior.AllowGet);

        }
        private async Task<List<PropietarioViewModel>> ListarPropietarios()
        {
            var res = new List<PropietarioViewModel>();
            using (var db = new VetSoftDBEntities())
            {
                var l = await db.Propietario.ToListAsync();
                l.ForEach(x =>
                {
                    res.Add(new PropietarioViewModel(x));
                });

                return res;
            }

        }


    }
}