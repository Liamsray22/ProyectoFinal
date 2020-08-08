using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using System.Threading.Tasks;

namespace ItlaElector.Controllers
{
    public class AccountController : Controller
    {
        private readonly AdminRepo _adminRepo;
        public AccountController(AdminRepo adminRepo)
        {
            _adminRepo = adminRepo;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CrearAdmin()
        {
            await _adminRepo.CrearAdmin();
            return RedirectToAction("Start","Start");
        }

        


    }
}
