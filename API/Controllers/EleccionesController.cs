using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.APIRepository;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EleccionesController : ControllerBase
    {

        private readonly EleccionApiRepos _eleccionApiRepos;
        private readonly IMapper _mapper;

        public EleccionesController(IMapper mapper, EleccionApiRepos eleccionApiRepos)
        {

            _eleccionApiRepos = eleccionApiRepos;
            _mapper = mapper;

        }

        //GET Todas la elecciones a la que a partcipado un ciudadano
        [HttpGet("{Cedula}")]
        public async Task<ActionResult<List<EleccionesDTO>>> Get(string Cedula)
        {
            //try
            //{

                int Ciudadano = await _eleccionApiRepos.VerificandoCiudadano(Cedula);

                switch (Ciudadano) {

                    case 1:
                        ModelState.AddModelError("Error", "Este Ciudada no existe.");
                        return BadRequest(ModelState);
                        
                    case 2:
                        ModelState.AddModelError("Error", "Este Ciudadano esta Inactivo");
                        return BadRequest(ModelState);
                    default:

                    var ListadoEleccionesDTO = await _eleccionApiRepos.EleccionesCiudadano(Cedula);

                    if (ListadoEleccionesDTO == null)
                    {
                        return NotFound();
                    }

                    return ListadoEleccionesDTO;
                    
                }

            //}
            //catch
            //{
            //    return StatusCode(500);
            //}


        }






    }

}