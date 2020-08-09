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
    public class CandidatosController : ControllerBase
    {

        private readonly CandidatoApiRepos _candidatosApiRepos;
        private readonly IMapper _mapper;

        public CandidatosController(IMapper mapper, CandidatoApiRepos candidatoApiRepos) {

            _candidatosApiRepos = candidatoApiRepos;
            _mapper = mapper;

        }


        //GET Candidatos con su puesto
        [HttpGet]
        public async Task<ActionResult<List<CandidatoDTO>>> CandidatosGetALl()
        {

            //try
            //{
                var ListaCandidatos = await _candidatosApiRepos.GetAllCandidatosDTO();

                if (ListaCandidatos == null)
                {
                    return NotFound();
                }

                return ListaCandidatos;

            //}
            //catch
            //{

            //    return StatusCode(500);
            //}


        }


    }


}