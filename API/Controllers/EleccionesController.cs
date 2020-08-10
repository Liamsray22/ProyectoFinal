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

    [Route("api/elecciones/[controller]")]
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

        //GET Todas la elecciones a la que a participado un ciudadano
        [HttpGet("{Cedula}")]
        public async Task<ActionResult<List<EleccionesDTO>>> Get(string Cedula)
        {
            try
            {

                var Ciudadano = await _eleccionApiRepos.VerificandoCiudadano(Cedula);

                    if(Ciudadano){

                            var ListadoEleccionesDTO = await _eleccionApiRepos.EleccionesCiudadano(Cedula);

                            if (ListadoEleccionesDTO == null)
                            {
                                return NotFound();
                            }

                            return ListadoEleccionesDTO;


                     }else{

                            ModelState.AddModelError("Error", "Este Ciudadano/a no existe.");
                            return BadRequest(ModelState);
                        }

                        }catch{
                           return StatusCode(500);
                        }


}


        //Listado de todos los candidatos por puesto electivos,  elección específica.
        [HttpGet("{Id}")]
        public async Task<ActionResult<List<CandidatoEleccionesDTO>>> GetCandidatosEleccion(int? Id)
        {

            var EleccionExists = await _eleccionApiRepos.GetByIdAsync(Id.Value);

            if (EleccionExists == null) {
                ModelState.AddModelError("Error", "La Eleccion que busca no existe...");
                return BadRequest(ModelState);
            }


            var ListadoPuestoEleccion = await _eleccionApiRepos.ListaCandidatosElecciones(Id.Value);

            if (ListadoPuestoEleccion == null)
            {
                return NotFound();
            }

            return ListadoPuestoEleccion;

        }



        }

}