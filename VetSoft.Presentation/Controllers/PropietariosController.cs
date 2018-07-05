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

        [Route("Propietario/Nuevo")]
        [Route("Propietario/Editar/{id?}")]
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

                    var p = await db.Propietario.FindAsync(new { ID = id });
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
        [Route("Propietario/Nuevo")]
        [Route("Propietario/Editar/{id}")]
        public JsonResult AddOrUpdate(PropietarioViewModel model)
        {
            return Json(new { success = true, message = "Listo, y Guardado" }, JsonRequestBehavior.AllowGet);
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