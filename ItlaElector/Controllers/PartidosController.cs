using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ItlaElector.Controllers
{
    public class PartidosController : Controller
    {
        public IActionResult Partidos()
        {
            return View();
        }
    }
}