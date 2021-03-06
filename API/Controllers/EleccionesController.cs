﻿using System;
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

        [HttpGet("ResultadosElecciones/{id}")]
        public async Task<ActionResult<List<ResultadosDTO>>> ResultadosElecciones(int? id)
        {
            try
            {
                var res = await _eleccionApiRepos.ResultadosElecciones(id.Value);
                return res;
            }
            catch
            {
                return StatusCode(500);

            }
            
        }

        [HttpGet]
        public async Task<ActionResult<List<List<ResultadosDTO>>>> TodasElecciones()
        {
            try{
                var res = await _eleccionApiRepos.TodasElecciones();
                return res;
            }
            catch 
            {
                return StatusCode(500);
            }


        }


    }

}