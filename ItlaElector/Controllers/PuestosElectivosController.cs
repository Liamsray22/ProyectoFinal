﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    public class PuestosElectivosController : Controller
    {
        private readonly PuestosElectivosRepo _puestosRepo;
        public PuestosElectivosController(PuestosElectivosRepo puestosRepo)
        {
            _puestosRepo = puestosRepo;

        }
        public async Task<IActionResult> PuestosElectivos()
        {
            var puesto = await _puestosRepo.TraerPuestosElectivos();
            return View(puesto);
        }

    }
}