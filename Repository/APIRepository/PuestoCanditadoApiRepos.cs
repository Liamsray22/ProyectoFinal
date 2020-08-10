using AutoMapper;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.APIRepository
{
    public class PuestoCanditadoApiRepos : RepositoryBase<Candidatos, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;
        private readonly PuestoElectivoApiRepos _puestoElectivoApiRepos;

        public PuestoCanditadoApiRepos(ItlaElectorDBContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        
        }


        //Activar Todos los candidatos con ese puesto! inactivo
        public async Task<bool> ActivarAllCandidatoPuesto(int IdPuesto)
        {

            var AllCandidatoPuesto = await _context.Candidatos.Where(op => op.IdPuestoElectivo == IdPuesto).ToListAsync();

            if (AllCandidatoPuesto == null)
            {
                return true;
            }

            foreach (var candidato in AllCandidatoPuesto)
            {

                candidato.Estado = "Activo";
                await Update(candidato);

            }

            return true;

        }

        //Desactivar Todos los candidatos con ese puesto! inactivo
        public async Task<bool> DesactivarAllCandidatoPuesto(int IdPuesto)
        {

            var AllCandidatoPuesto = await _context.Candidatos.Where(op => op.IdPuestoElectivo == IdPuesto).ToListAsync();

            if (AllCandidatoPuesto == null)
            {
                return true;
            }

            foreach (var candidato in AllCandidatoPuesto)
            {

                candidato.Estado = "Inactivo";
                await Update(candidato);

            }

            return true;

        }




    }
}
