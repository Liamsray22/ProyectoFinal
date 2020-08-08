using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    public class CiudadanosController : Controller
    {
        private readonly CiudadanosRepo _ciudadanosRepo;
        public CiudadanosController(CiudadanosRepo ciudadanosRepo)
        {
            _ciudadanosRepo = ciudadanosRepo;

        }
        public async Task<IActionResult> Ciudadanos()
        {
            var ciudadanos =await _ciudadanosRepo.TraerCiudadanos();
            return View(ciudadanos);
        }
        [HttpPost]
        public async Task<IActionResult> Ciudadanos(CiudadanosViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _ciudadanosRepo.CrearCiudadanos(cvm);
                }
                catch
                {

                }
            }
            var ciudadanos = await _ciudadanosRepo.TraerCiudadanos();
            return RedirectToAction("Ciudadanos");
        }





    }
}