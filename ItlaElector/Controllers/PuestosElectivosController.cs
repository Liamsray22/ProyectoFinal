using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace ItlaElector.Controllers
{
    [Authorize(Roles = "Administrador")]

    public class PuestosElectivosController : Controller
    {
        private readonly PuestosElectivosRepo _puestosRepo;
        public PuestosElectivosController(PuestosElectivosRepo puestosRepo)
        {
            _puestosRepo = puestosRepo;

        }
        public async Task<IActionResult> PuestosElectivos()
        {
            var puesto = await _puestosRepo.TraerPuestosElectivos();
            return View(puesto);
        }

        [HttpPost]
        public async Task<IActionResult> PuestosElectivos(PuestosElectivosViewModel pevm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _puestosRepo.CrearPuestosElectivos(pevm);
                }
                catch
                {

                }
            }
            var puesto = await _puestosRepo.TraerPuestosElectivos();
            return RedirectToAction("PuestosElectivos");
        }

        public async Task<IActionResult> EliminarPuestosElectivos(int id)
        {
            try
            {
                await _puestosRepo.EliminarPuestosElectivos(id);
            }
            catch
            {

            }
            return RedirectToAction("PuestosElectivos");

        }

        [HttpPost]
        public async Task<IActionResult> EditarPuestosElectivos(PuestosElectivosViewModel upevm)
        {
            var edit = await _puestosRepo.EditarPuestosElectivos(upevm);
            if (edit)
            {
                return RedirectToAction("PuestosElectivos");

            }

            return RedirectToAction("Start", "Start");

        }
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyPuesto(string nombre)
        {
            var verifypuesto = await _puestosRepo.verifyPuestos(nombre);
            if (verifypuesto)
            {
                return Json($"Ya esta puesto esta creado");


            }

            return Json(true);
        }
    }
}