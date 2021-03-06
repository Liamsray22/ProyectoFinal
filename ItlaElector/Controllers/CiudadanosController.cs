﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    [Authorize(Roles = "Administrador")]

    public class CiudadanosController : Controller
    {
        private readonly CiudadanosRepo _ciudadanosRepo;
        public CiudadanosController(CiudadanosRepo ciudadanosRepo)
        {
            _ciudadanosRepo = ciudadanosRepo;

        }

        public async Task<IActionResult> Ciudadanos()
        {
            var ciudadanos = await _ciudadanosRepo.TraerCiudadanos();
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

        public async Task<IActionResult> EliminarCiudadano(string id)
        {
            try
            {
                await _ciudadanosRepo.EliminarCiudadanos(id);
            }
            catch
            {

            }
            return RedirectToAction("Ciudadanos");

        }
        [HttpPost]
        public async Task<IActionResult> EditarCiudadano(CiudadanosViewModel ucvm)
        {
            var edit = await _ciudadanosRepo.EditarCiudadanos(ucvm);

            if (edit)
            {
                return RedirectToAction("Ciudadanos");

            }

            return RedirectToAction("Ciudadanos");
        }


        public async Task<IActionResult> CrearRol()
        {
            await _ciudadanosRepo.CrearRole();
            return RedirectToAction("Ciudadanos");

        }


        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyCedula(string Cedula)
        {
            Cedula = Cedula.Replace("-", "");
            var verifyelecciones = await _ciudadanosRepo.verifyusercedula(Cedula);
            if (verifyelecciones)
            {
                return Json($"Este usuario ya esta registrado");


            }

            return Json(true);
        }
    }
}

    
