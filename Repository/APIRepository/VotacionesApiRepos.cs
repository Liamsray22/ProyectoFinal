using AutoMapper;
using DataBase.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBase.DTO;

namespace Repository.APIRepository
{
    public class VotacionesApiRepos : RepositoryBase<Votacion, ItlaElectorDBContext>
    {

        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;

        public VotacionesApiRepos(ItlaElectorDBContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<int>> ListIdElecciones(string cedula) {

            var ListadoIntElecciones = await _context.Votacion.Where(x => x.Cedula == cedula)
                .Select(x => x.IdEleccion).Distinct().ToListAsync();

            return ListadoIntElecciones;

        }

        //Todos los candidatos por puesto electivo de una eleccion especifica
        /*public async Task<List<CandidatoEleccionEspecificaDTO>> EleccionTodosCandiatos(int IdEleccion)
        {
            var AllVotacion = new List<int>();
            AllVotacion = await _context.Votacion.Where(x => x.IdEleccion == IdEleccion).Select(x=>x.IdCandidato).Distinct().ToListAsync();

            var ListadoCandidatoEleccion = new List<CandidatoEleccionEspecificaDTO>();

            var ListadoEnteroPuesto = new List<CandidatoEleccionEspecificaDTO>();

            foreach (var IdCandidato in AllVotacion) {


                var Candidato = _context.Candidatos.FirstOrDefaultAsync(x => x.IdCandidato == IdCandidato);
                
            
            }

            return null;

        }
        */

    }
}
