using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    public class StartController : Controller
    {
        private readonly AdminRepo _adminRepo;
        private readonly VotacionRepo _votacionRepo;
        private readonly EleccionesRepo _EleccionesRepo;
        private readonly CiudadanosRepo _CiudadanosRepo;


        public StartController(AdminRepo adminRepo, VotacionRepo votacionRepo, EleccionesRepo EleccionesRepo, CiudadanosRepo CiudadanosRepo)
        {
            _adminRepo = adminRepo;
            _votacionRepo = votacionRepo;
            _CiudadanosRepo = CiudadanosRepo;
            _EleccionesRepo = EleccionesRepo;

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

        [HttpPost]
        public async Task<IActionResult> Start(LoginUserViewModel loginViewModel)
        {
            
                var log = await _adminRepo.LoguearCiudadano(loginViewModel);
                if (log)
                {
                    return RedirectToAction("Home", "Elector");
                }
                ModelState.AddModelError("", "Usuario o clave incorrectos");

                ViewBag.error = "Usuario o clave incorrectos";
                return View(loginViewModel);
            
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyCedula(string Cedula)
        {
            Cedula = Cedula.Replace("-","");
          var verify = await _votacionRepo.Verificarsivoto(Cedula);
            if (verify)
            {
                return Json($"Usted ya participo en estas elecciones");


            }
            var verifyelecciones = await _EleccionesRepo.eleccionesactivas();
            if (verifyelecciones)
            {
                return Json($"No hay ningún proceso electoral en estos momentos");


            }
            var verifyestadouser = await _CiudadanosRepo.USERactivo(Cedula) ;
            if (verifyestadouser)
            {
                return Json($"Usted ha sido restringido ");


            }


            return Json(true);
        }


    }
}