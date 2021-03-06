﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.Models;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    [Authorize(Roles = "Administrador")]

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
                    if (evm.Fecha < DateTime.Now.AddDays(-1))
                    {
                        ViewBag.mostrar = "block";
                        var elecciones = await _eleccionesRepo.TraerElecciones();
                        return View(elecciones);


                    }
                    await _eleccionesRepo.CrearElecciones(evm);
                }
                catch
                {

                }
            }
            return RedirectToAction("Elecciones");
        }
        [HttpPost]
        public async Task<IActionResult> EleccionesFinalizar()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _eleccionesRepo.FinalizarEleccion();
                }
                catch
                {

                }
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

       
    }
}