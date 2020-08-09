using AutoMapper;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
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
    public class EleccionApiRepos : RepositoryBase<Elecciones, ItlaElectorDBContext>
    {

        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;
        private readonly CiudadApiRepos _ciudadanoApiRepos;
        private readonly VotacionesApiRepos _votacionesApiRepos;

        public EleccionApiRepos(ItlaElectorDBContext context, IMapper mapper, CiudadApiRepos ciudadApiRepos,
            VotacionesApiRepos votacionesApiRepos) : base(context)
        {

            _context = context;
            _mapper = mapper;
            _ciudadanoApiRepos = ciudadApiRepos;
            _votacionesApiRepos = votacionesApiRepos

        }


        public async Task<int> VerificandoCiudadano(string Cedula) {


            var CiudadanoActivo = await _context.Ciudadanos.FirstOrDefaultAsync(x => x.Cedula == Cedula);

            if (CiudadanoActivo == null) {
                return 1;
            }

            if (CiudadanoActivo.Estado == "Inactivo") {
                return 2;
            }

            return 3;
        }

        //Elecciones en la que a participado un ciudadano expecificamente.
        public async Task<List<EleccionesDTO>> EleccionesCiudadano(string cedulaCiudadano) {

            var ListadoIdElecciones_Ciudadano = await _votacionesApiRepos.ListIdElecciones(cedulaCiudadano);

            if (ListadoIdElecciones_Ciudadano == null) {
                return null;
            }

           var List = new List<EleccionesDTO>();

            foreach(var IdElecciones in ListadoIdElecciones_Ciudadano){

                var EleccionBase  = await GetByIdAsync(IdElecciones);

                if(EleccionBase.Estado == "Finalizado") {
                    var EleccionDTO = _mapper.Map<EleccionesDTO>(EleccionBase);
                    List.Add(EleccionDTO);
                }
            }

            return List;
        }



    }




}
