using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    [Authorize(Roles = "Ciudadanos")]

    public class ElectorController : Controller
    {
        private readonly ElectorRepo _electorRepo;
        private readonly CandidatosRepo _candidatosRepo;
        private readonly VotacionRepo _votacionRepo;

        public ElectorController(ElectorRepo electorRepo, CandidatosRepo candidatosRepo,
            VotacionRepo votacionRepo)
        {
            _electorRepo = electorRepo;
            _candidatosRepo = candidatosRepo;
            _votacionRepo = votacionRepo;
        }
        public async Task<IActionResult> Home()
        {
            var ele = await _electorRepo.elector(User.Identity.Name);
            return View(ele);
        }

        public async Task<IActionResult> ZonaVotacion(int id) {
            var Votacionvm = await _votacionRepo.TraerCandidatosByIdPuestos(id);

            return View(Votacionvm);
        }

        [HttpPost]
        public async Task<IActionResult> ZonaVotacion(VotacionViewModel vvm)
        {
            if (vvm.IdCandidato != null)
            {
                vvm.Cedula = User.Identity.Name;
                await _votacionRepo.Votar(vvm);
                return RedirectToAction("Home");
            }
            var Votacionvm = await _votacionRepo.TraerCandidatosByIdPuestos(vvm.IdEleccion);
            ModelState.AddModelError("", "Elija un candidato");
            return View(Votacionvm);
        }

        public async Task<IActionResult> Finalizar()
        {
            var validar = await _votacionRepo.Finalizar(User.Identity.Name);
            if (validar)
            {
                return RedirectToAction("Start", "Start");
            }
            else
            {
                return RedirectToAction("Home");

            }

        }

    }
}