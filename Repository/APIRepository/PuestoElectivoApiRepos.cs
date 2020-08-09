using AutoMapper;
using DataBase.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.APIRepository
{
    public class PuestoElectivoApiRepos : RepositoryBase<PuestoElectivo, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;

        public PuestoElectivoApiRepos(ItlaElectorDBContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;

        }


        ///Metodo que adquiere el puesto de un candidato
        public async Task<string> PuestoCandidato(int IdPuesto) {
            var Puesto = await GetByIdAsync(IdPuesto);
            return Puesto.Nombre;
        }


    }
}
