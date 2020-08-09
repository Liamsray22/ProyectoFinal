using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    public class EleccionesController : Controller
    {
        private readonly EleccionesRepo _eleccionesRepo;
        private readonly CandidatosRepo _candidatosRepo;
        public EleccionesController(EleccionesRepo eleccionesRepo, CandidatosRepo candidatosRepo)
        {
            _eleccionesRepo = eleccionesRepo;
            _candidatosRepo = candidatosRepo;


        }
        public async Task<IActionResult> Elecciones()
        {
            var elecciones = await _eleccionesRepo.TraerElecciones();
            return View(elecciones);
        }

        [HttpPost]
        public async Task<IActionResult> Elecciones(EleccionesViewModel evm)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                    await _eleccionesRepo.CrearElecciones(evm);
                //}
                //catch
                //{

                //}
            }
            var elecciones = await _eleccionesRepo.TraerElecciones();
            return RedirectToAction("Elecciones");
        }
        [HttpPost]
        public async Task<IActionResult> EleccionesFinalizar()
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                await _eleccionesRepo.FinalizarEleccion();
                //}
                //catch
                //{

                //}
            }
            var elecciones = await _eleccionesRepo.TraerElecciones();
            return RedirectToAction("Elecciones");
        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> Verifydate( DateTime date)
        {

            if (date < DateTime.Now)
            {
                return Json($"Debe elegir una fecha valida");
            }

            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> Verifycandidatos(string Nombre)
        {

            if (await _candidatosRepo.TraerCandidatosActivos())
            {
                return Json($"Debe haber al menos dos candidatos activos para proceder en las elecciones");
            }

            return Json(true);
        }
    }
}