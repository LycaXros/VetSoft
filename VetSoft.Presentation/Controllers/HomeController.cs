
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VetSoft.Data;
using VetSoft.Presentation.Models;

namespace VetSoft.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        VetSoftDBEntities db = new VetSoftDBEntities();
        #region Index method.  
        /// <summary>  
        /// Index method.   
        /// </summary>  
        /// <returns>Returns - Index view</returns>  
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        [AllowAnonymous]
        [Route("Citas/Listar/{anio}/{mes}")]
        public JsonResult CitasPorMes(int mes, int anio)
        {
            var checkList = db.Chequeo
                .Where(x => x.Fecha.Month == mes && x.Fecha.Year == anio)
                .ToList();

            //var data = checkList.Select(x => new {
            //    Dia = x.Fecha.Day,
            //    Cantidad = Entit
            //}).ToList();
            var data = (from x in db.Cita
                        where x.Fecha.Month == mes && x.Fecha.Year == anio
                        group x by DbFunctions.TruncateTime(x.Fecha) into g
                        select new
                        {
                            Fecha = g.Key.Value,
                            Cantidad = g.Count()
                        }
                       ).ToList();
            var dataUrl = data.Select(x => new
            {
                Dia = x.Fecha.Day,
                x.Cantidad,
                Url = Url.Action("CitasDia", new { mes = x.Fecha.Month, anio = x.Fecha.Year, dia = x.Fecha.Day })
            }).ToList();

            return new JsonResult() { Data = dataUrl, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Route("Citas/Listar/{anio}/{mes}/{dia}")]
        public ActionResult CitasDia(int anio, int mes, int dia)
        {
            var chList = db.Cita.Where(x =>
                x.Fecha.Day == dia &&
                x.Fecha.Month == mes &&
                x.Fecha.Year == anio).ToList();

            if(chList == null || chList.Count <= 0)
            {
                return RedirectToAction("Index");
            }

            var model = new List<CitaDataModel>();

            chList.ForEach(x => {
                model.Add(new CitaDataModel(x));
            });
            ViewBag.Fecha = new DateTime(anio, mes, dia).ToShortDateString();
            return View(model);
        }

    }
}