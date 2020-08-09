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
    public class PuestoElectivosController : ControllerBase
    {

        private readonly PuestoElectivoApiRepos _PuestoElectivoApiRepos;
        private readonly IMapper _mapper;

        public PuestoElectivosController(IMapper mapper, PuestoElectivoApiRepos PuestoElectivoApiRepos)
        {
            _mapper = mapper;
            _PuestoElectivoApiRepos = PuestoElectivoApiRepos;
        }
          
        //POST Agregar Puesto Electivo.
        [HttpPost]
        public async Task<ActionResult> PostPartido(CrearPuestoElectivoDTO Partido)
        {

            if (ModelState.IsValid)
            {

                var action = await _PuestoElectivoApiRepos.PostPuestoElectivo(Partido);

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

        //PATCH Desactivar los puestos electivos.
        [HttpPatch("Desactivar/{IdPuesto}")]
        public async Task<ActionResult> DesactivarPuesto(int? IdPuesto)
        {

            try
            {
                var action = await _PuestoElectivoApiRepos.DesactivarPuestoElectivos(IdPuesto.Value);

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

        //PATCH Activar el puesto Electivo.
        [HttpPatch("Activar/{IdPuesto}")]
        public async Task<ActionResult> ActivarPuestoElectivo(int? IdPuesto)
        {

            try
            {
                var action = await _PuestoElectivoApiRepos.ActivarPuestoElectivos(IdPuesto.Value);

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

        //PUT actualizar puesto Electivo
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPartido(int? id, CrearPuestoElectivoDTO partido)
        {

            if (ModelState.IsValid)
            {
                var respuesta = await _PuestoElectivoApiRepos.ActualizarPuestoElectivo(id.Value, partido);

                if (respuesta)
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


    }

}