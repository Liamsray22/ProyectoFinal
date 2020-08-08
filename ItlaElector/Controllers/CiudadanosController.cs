using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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





    }
}