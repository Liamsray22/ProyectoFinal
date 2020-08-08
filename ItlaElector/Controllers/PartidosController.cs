using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using DataBase.ViewModels;

namespace ItlaElector.Controllers
{
    public class PartidosController : Controller
    {
        private readonly PartidosRepo _partidosRepo;
        public PartidosController(PartidosRepo partidosRepo)
        {
            _partidosRepo = partidosRepo;

        }
        public async Task<IActionResult> Partidos()
        {
            var partido = await _partidosRepo.TraerPartidos();
            return View(partido);
        }
    }
}