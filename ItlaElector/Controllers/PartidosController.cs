﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace ItlaElector.Controllers
{
    [Authorize(Roles = "Administrador")]

    public class PartidosController : Controller
    {
        private readonly PartidosRepo _partidosRepo;
        private readonly IHostingEnvironment hostEnvironment;

        public PartidosController(PartidosRepo partidosRepo, IHostingEnvironment hostEnvironment)
        {
            _partidosRepo = partidosRepo;
            this.hostEnvironment = hostEnvironment;

        }
        public async Task<IActionResult> Partidos()
        {
            var partido = await _partidosRepo.TraerPartidos();

            return View(partido);
        }
        [HttpPost]
        public async Task<IActionResult> Partidos(PartidosViewModels pvm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _partidosRepo.CrearPartido(pvm, hostEnvironment.WebRootPath);
                }
                catch
                {

                }
            }

            var partido = await _partidosRepo.TraerPartidos();
            return RedirectToAction("Partidos");
        }

        public async Task<IActionResult> EliminarPartido(int id)
        {
            try
            {
                await _partidosRepo.EliminarPartido(id);
            }
            catch
            {

            }
            return RedirectToAction("Partidos");

        }


        [HttpPost]
        public async Task<IActionResult> EditarPartido(PartidosViewModels upvm)
        {
            var edit = await _partidosRepo.EditarPartidos(upvm, hostEnvironment.WebRootPath);
            if (edit)
            {
                return RedirectToAction("Partidos");

            }

            return RedirectToAction("Start", "Start");

        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyPartido(string Nombre)
        {
            var verifypartido = await _partidosRepo.verifyPartido(Nombre);
            if (verifypartido)
            {
                return Json($"Ya esta partido esta creado");


            }

            return Json(true);
        }
    }
}