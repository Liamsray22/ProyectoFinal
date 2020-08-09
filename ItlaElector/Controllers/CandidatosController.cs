using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    public class CandidatosController : Controller
    {
        private readonly CandidatosRepo _candidatosRepo;
        private readonly IHostingEnvironment hostEnvironment;

        public CandidatosController(CandidatosRepo candidatosRepo, IHostingEnvironment hostEnvironment)
        {
            _candidatosRepo = candidatosRepo;
            this.hostEnvironment = hostEnvironment;

        }
        public async Task<IActionResult> Candidatos()
        {
            var candidatos = await _candidatosRepo.TraerCandidatos();
            return View(candidatos);
        }

        [HttpPost]
        public async Task<IActionResult> Candidatos(CandidatosViewModel cavm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _candidatosRepo.CrearCandidatos(cavm, hostEnvironment.WebRootPath);
                }
                catch
                {

                }
            }
            var candidatos = await _candidatosRepo.TraerCandidatos();
            return RedirectToAction("Candidatos");
        }

        public async Task<IActionResult> EliminarCandidatos(int id)
        {
            try
            {
                await _candidatosRepo.EliminarCandidatos(id);
            }
            catch
            {

            }
            return RedirectToAction("Candidatos");

        }

        [HttpPost]
        public async Task<IActionResult> EditarCandidatos(CandidatosViewModel ucavm)
        {
            var edit = await _candidatosRepo.EditarCandidatos(ucavm, hostEnvironment.WebRootPath);
            if (edit)
            {
                return RedirectToAction("Candidatos");
            }
            return RedirectToAction("Start", "Start");

        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> Verifyphoto(IFormFile photo)
        {

         
            if (photo.FileName == null)
            {
                return Json($"Se requiere foto que identifique el candidato");
            }

            return Json(true);
        }
    }
}