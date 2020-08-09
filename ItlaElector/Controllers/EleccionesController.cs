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
        public EleccionesController(EleccionesRepo eleccionesRepo)
        {
            _eleccionesRepo = eleccionesRepo;

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
                try
                {
                    await _eleccionesRepo.CrearElecciones(evm);
                }
                catch
                {
                }
            }
            var elecciones = await _eleccionesRepo.TraerElecciones();
            return RedirectToAction("Elecciones");
        }

    }
}