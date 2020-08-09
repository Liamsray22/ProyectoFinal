using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    public class ElectorController : Controller
    {
        private readonly ElectorRepo _electorRepo;
        private readonly CandidatosRepo _candidatosRepo;
        public ElectorController(ElectorRepo electorRepo, CandidatosRepo candidatosRepo)
        {
            _electorRepo = electorRepo;
            _candidatosRepo = candidatosRepo;
        }
        public async Task<IActionResult> Home()
        {
            var ele = await _electorRepo.elector();
            return View(ele);
        }

        public async Task<IActionResult> ZonaVotacion(int id) {
            var Candidatos = await _candidatosRepo.TraerCandidatosByIdPuestos(id);

            return View(Candidatos);
        }

        public async Task<IActionResult> VotarPuesto(CandidatosViewModel vcvm)
        {
            await _electorRepo.Votar(vcvm);
            return RedirectToAction();


        }

    }
}