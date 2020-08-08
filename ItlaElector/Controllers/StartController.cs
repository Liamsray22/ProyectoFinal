using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    public class StartController : Controller
    {
        private readonly AdminRepo _adminRepo;
        public StartController(AdminRepo adminRepo)
        {
            _adminRepo = adminRepo;

        }



        public IActionResult Start() {

            return View();
        }

        public async Task<IActionResult> Login()
        {
            var log = await _adminRepo.LoguearAdmin("", "");
            if (log)
            {
                return RedirectToAction("Home", "Admin");

            }
            return RedirectToAction("Login", "Start");
        }


    }
}