using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using System.Threading.Tasks;

namespace ItlaElector.Controllers
{
    public class AccountController : Controller
    {
        private readonly AdminRepo _adminRepo;
        private readonly CiudadanosRepo _ciudadanosRepo;

        public AccountController(AdminRepo adminRepo, CiudadanosRepo ciudadanosRepo)
        {
            _adminRepo = adminRepo;
            _ciudadanosRepo = ciudadanosRepo;

        }


        public IActionResult Login() {

           return RedirectToAction("Start", "Start");
        }

        //Crear Administrador  - Ojo las credenciales estan en el repositorio _adminRepo
        public async Task<IActionResult> CrearAdmin()
        {
            await _adminRepo.CrearAdmin();
            return RedirectToAction("Start","Start");
        }

        //Crear roles
        public async Task<IActionResult> CrearRol(string rol)
        {
            await _ciudadanosRepo.CrearRole(rol);
            return RedirectToAction("Start","Start");

        }

        public IActionResult AccessDenied() {

            if (User.Identity.IsAuthenticated && User.IsInRole("Administrador"))
            {
                return RedirectToAction("Home", "Admin");

            }

            if (User.Identity.IsAuthenticated && User.IsInRole("Ciudadano"))
            {
                return RedirectToAction("Home", "Elector");

            }

            return RedirectToAction("Start", "Start");
        }


    }
}
