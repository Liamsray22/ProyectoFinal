using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminRepo _adminRepo;
        public AdminController(AdminRepo adminRepo)
        {
            _adminRepo = adminRepo;

        }
        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await _adminRepo.CerrarSesion();
            return RedirectToAction("Start", "Start");
        }


    }
}