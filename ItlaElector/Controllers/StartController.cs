using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.ViewModels;
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
        public IActionResult Login() {
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Home", "Admin");

            //}
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var log = await _adminRepo.LoguearAdmin(loginViewModel);
                if (log)
                {
                    return RedirectToAction("Home", "Admin");

                }
                ModelState.AddModelError("", "Usuario o clave incorrectos");

                return View(loginViewModel);
            }
            return View(loginViewModel);
        }


    }
}