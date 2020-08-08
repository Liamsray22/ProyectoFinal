using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Hosting;
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
            var edit = await _candidatosRepo.EditarCandidatos(ucavm);
            if (edit)
            {
                return RedirectToAction("Candidatos");
            }
            return RedirectToAction("Start", "Start");

        }
    }
}