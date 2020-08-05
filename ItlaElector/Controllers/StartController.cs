using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ItlaElector.Controllers
{
    public class StartController : Controller
    {

      
        public IActionResult Start() {

            return View();
        }

        public IActionResult Login()
        {

            return View();
        }


    }
}