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

        public CandidatosController(IMapper mapper, CandidatoApiRepos candidatoApiRepos)
        {

            _candidatosApiRepos = candidatoApiRepos;
            _mapper = mapper;

        }

        //  VERIFICACIONES
        //GET Candidatos con su puesto
        [HttpGet]
        public async Task<ActionResult<List<CandidatoDTO>>> CandidatosGetALl()
        {

            try
            {
                var ListaCandidatos = await _candidatosApiRepos.GetAllCandidatosDTO();

                if (ListaCandidatos == null)
                {
                    return NotFound();
                }

                return ListaCandidatos;

            }
            catch
            {

                return StatusCode(500);
            }


        }

        //Creando un nuevo candidato
        [HttpPost]
        public async Task<ActionResult> PostCandidato(CandidatoDTO candidato)
        {

            if (ModelState.IsValid)
            {

                var Exists = await _candidatosApiRepos.VerificarCandidatoPartido(candidato);

                if (Exists)
                {

                    var action = await _candidatosApiRepos.PostNewCandidato(candidato);

                    if (action)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return StatusCode(500);

                    }

                }
                else
                {
                    ModelState.AddModelError("Error", "El puesto que intenta ocupar este candidato ya esta ocupado por otro del mismo partido.");
                    return BadRequest(ModelState);
                }


            }

            return BadRequest();

        }

        //PATCH Desactivar un candidato
        [HttpPatch("Desactivar/{IdCandidato}")]
        public async Task<ActionResult> DesactivarPartido(int? IdCandidato)
        {

            try
            {
                var action = await _candidatosApiRepos.DesactivarCandidato(IdCandidato.Value);

                if (action)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500);
                }


            }
            catch
            {
                return BadRequest();
            }
        }

        // PATCH Activar un candidato.
        [HttpPatch("Activar/{IdCandidato}")]
        public async Task<ActionResult> ActivarPartido(int? IdCandidato)
        {

            try
            {
                var action = await _candidatosApiRepos.ActivarCandidato(IdCandidato.Value);

                if (action)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500);
                }

            }
            catch
            {
                return BadRequest();
            }


        }

        //PUT actualizar Partido
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int? id, CandidatoDTO candidato)
        {

            if (ModelState.IsValid)
            {

                var Exists = await _candidatosApiRepos.VerificarCandidatoUpdate(candidato.IdPuestoElectivo,candidato.IdPartido);

                if (Exists)
                {

                    var action = await _candidatosApiRepos.UpdateCandidatoDTO(id.Value, candidato);

                    if (action)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return StatusCode(500);

                    }

                }
                else
                {
                    ModelState.AddModelError("Error", "El puesto que intenta ocupar este candidato ya esta ocupado por otro del mismo partido.");
                    return BadRequest(ModelState);
                }


            }

            return BadRequest();

        }


    }


}