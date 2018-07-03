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
    public class PacienteController : Controller
    {
        Data.VetSoftDBEntities db = new Data.VetSoftDBEntities();
        // GET: Paciente
        [Route("Pacientes", Order = 1)]
        [Route("Paciente/Index", Order = 2)]
        public async Task<ActionResult> Index()
        {
            List<PacienteViewModel> pacientes = new List<PacienteViewModel>();
            var pa = db.Paciente.Include(x => x.Propietarios);
            (await pa.ToListAsync()).ForEach(x =>
            {
                pacientes.Add(new PacienteViewModel(x));
            });
            return View(pacientes);
        }

        [Route("Paciente/Nuevo")]
        public ActionResult Create()
        {
            ViewBag.Razas = db.Raza
                .Select(x => new SelectListItem
                {
                    Text = x.Nombre,
                    Value = x.ID.ToString()
                })
                .ToList();
            return View();
        }
        [Route("Paciente/Nuevo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PacienteViewModel paciente)
        {
            if (ModelState.IsValid)
            {
                var p = new Paciente
                {
                    Nombre = paciente.Nombre,
                    Microchip_Licencia = paciente.Microchip_Licencia,
                    FechaIngreso = DateTime.Now,
                    FechaNac = paciente.FechaNac,
                    Color = paciente.Color,
                    RazaID = paciente.RazaID
                };
                switch (paciente.Genero.Value)
                {
                    case Sexo.Masculino:
                        p.Genero = Constantes.Masculino; break;
                    case Sexo.Femenino:
                        p.Genero = Constantes.Femenino; break;
                }

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
    }
}