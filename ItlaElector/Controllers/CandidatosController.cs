using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    public class CandidatosController : Controller
    {
        private readonly CandidatosRepo _candidatosRepo;
        public CandidatosController(CandidatosRepo candidatosRepo)
        {
            _candidatosRepo = candidatosRepo;

        }
        public async Task<IActionResult> Candidatos()
        {
            var candidatos = await _candidatosRepo.TraerCandidatos();
            return View(candidatos);
        }
    }
}