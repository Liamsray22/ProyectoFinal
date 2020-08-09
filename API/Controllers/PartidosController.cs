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
    public class PartidosController : ControllerBase
    {

        private readonly PartidoApiRepos  _partidoApiRepos;
        private readonly IMapper _mapper;

        public PartidosController(IMapper mapper, PartidoApiRepos partidoApiRepos){


            _mapper = mapper;
            _partidoApiRepos = partidoApiRepos;

        }


        //GET todos los partidos activos.
        [HttpGet]
        public async Task<ActionResult<List<PartidoDTO>>> PartidosActivos()
        {

            try
            {
                var ListadoPartido = await _partidoApiRepos.GetAllPartidoDTO();

                if (ListadoPartido == null)
                {
                    return NotFound();
                }

                return ListadoPartido;

            }
            catch
            {

                return StatusCode(500);
            }


        }

        //GET Todo los candidatos de un partido Especifico.
        [HttpGet("{Id}")]
        public async Task<ActionResult<List<CanditosPartidoDTO>>> Get(int? Id)
        {
            try
            {
                var ListadoCandidosPartidos = await _partidoApiRepos.GetListSpecificPartido(Id.Value);

                if (ListadoCandidosPartidos == null)
                {
                    return NotFound();
                }

                return ListadoCandidosPartidos;

            }
            catch
            {
                return StatusCode(500);
            }
        }

        //POST agregar Partido.
        [HttpPost]
        public async Task<ActionResult> PostPartido(CrearPartidoDTO Partido)
        {

            if (ModelState.IsValid)
            {

                var action = await _partidoApiRepos.PostNewPartido(Partido);

                if (action)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500);

                }


            }
            return BadRequest();

        }

        //PATCH Desactivar un partido especifico junto con sus candidatos
        [HttpPatch("Desactivar/{IdPartido}")]
        public async Task<ActionResult> DesactivarPartido(int? IdPartido)
        {

            try
            {
                var action = await _partidoApiRepos.DesactivarPartido(IdPartido.Value);

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

        //PATCH Activar un partido especifico junto con sus candidatos
        [HttpPatch("Activar/{IdPartido}")]
        public async Task<ActionResult> ActivarPartido(int? IdPartido)
        {

            try
            {
                var action = await _partidoApiRepos.ActivarPartido(IdPartido.Value);

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





    }


}