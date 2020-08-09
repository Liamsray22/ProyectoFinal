using AutoMapper;
using DataBase.DTO;
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
    public class CandidatoApiRepos : RepositoryBase<Candidatos, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;
        private readonly PuestoElectivoApiRepos _puestoElectivoApiRepos;

        public CandidatoApiRepos(ItlaElectorDBContext context, IMapper mapper,
            PuestoElectivoApiRepos puestoElectivoApiRepos) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _puestoElectivoApiRepos = puestoElectivoApiRepos;

        }

        //Todos los candidatos
        public async Task<List<CandidatoDTO>> GetAllCandidatosDTO()
        {
            var AllCandidatos = await GetAllAsync();
                
            var ObjectListCandidatosDTO = new List<CandidatoDTO>();

            if (AllCandidatos == null)
            {
                return null;
            }

            foreach (var candidato in AllCandidatos)
            {

                var CandidatoDTO = _mapper.Map<CandidatoDTO>(candidato);
                CandidatoDTO.Puesto = await _puestoElectivoApiRepos.PuestoCandidato(candidato.IdPuestoElectivo);

                ObjectListCandidatosDTO.Add(CandidatoDTO);

            }


            return ObjectListCandidatosDTO;


        }


        //TraerTodos los Candidatos de un Partido Especifico
        public async Task<List<CanditosPartidoDTO>> GetPartidoCandidatosList(int IdPartido)
        {

            var AllCandidatoPartido = await _context.Candidatos.Where(op => op.IdPartido == IdPartido).ToListAsync();
            var ObjectListCandidatosPartidoDTO = new List<CanditosPartidoDTO>();

            if (AllCandidatoPartido == null) {

                return null;
            }

            foreach (var candidato in AllCandidatoPartido)
            {

                var CandidatoDTO = _mapper.Map<CanditosPartidoDTO>(candidato);
                CandidatoDTO.Puesto = await _puestoElectivoApiRepos.PuestoCandidato(candidato.IdPuestoElectivo);

                ObjectListCandidatosPartidoDTO.Add(CandidatoDTO);

            }

            return ObjectListCandidatosPartidoDTO;


        }

        //Desactivar Todos Los Candidatos de un partido
        public async Task<bool> DesactivarAllCandidato(int IdPartido) {

            var AllCandidatoPartido = await _context.Candidatos.Where(op => op.IdPartido== IdPartido).ToListAsync();
           
            if (AllCandidatoPartido == null) {
                return true;
            }

            foreach(var candidato in AllCandidatoPartido) {

                candidato.Estado = "Inactivo";
                await Update(candidato);

            }

            return true;

        }

        //Activar Todos Los Candidatos de un partido
        public async Task<bool> ActivarAllCandidato(int IdPartido)
        {

            var AllCandidatoPartido = await _context.Candidatos.Where(op => op.IdPartido == IdPartido).ToListAsync();

            if (AllCandidatoPartido == null)
            {
                return true;
            }

            foreach (var candidato in AllCandidatoPartido)
            {

                candidato.Estado = "Activo";
                await Update(candidato);

            }

            return true;

        }

        //Agregar nuevo Candidato
        public async Task<bool> PostNewCandidato(CandidatoDTO newCandidato)
        {

            //try
            //{
                var Candidato = _mapper.Map<Candidatos>(newCandidato);
                Candidato.Estado = "Activo";
                Candidato.FotoPerfil = "CandidatoNone.jpg";
                await AddAsync(Candidato);
                return true;
            //}
            //catch
            //{
            //    return false;
            //}

        }

        //Verificar Puesto candidato
        public async Task<bool> VerificarCandidatoPartido(CandidatoDTO cant) {

            var CandidatoExists = await _context.Candidatos.FirstOrDefaultAsync(x => x.IdPuestoElectivo == cant.IdPuestoElectivo && x.IdPartido == cant.IdPartido);

            if (CandidatoExists == null) {
                return true;
            }

            return false;
        }

        //Desactivar Candidato
        public async Task<bool> DesactivarCandidato(int IdCandidato)
        {

            var GetCandidato = await GetByIdAsync(IdCandidato);

            if (GetCandidato == null)
            {
                return false;
            }

            GetCandidato.Estado = "Inactivo";
            await Update(GetCandidato);
            return true;

        }

        //Activar Candidato
        public async Task<bool> ActivarCandidato(int IdCandidato)
        {

            var GetCandidato = await GetByIdAsync(IdCandidato);

            if (GetCandidato == null)
            {
                return false;
            }

            GetCandidato.Estado = "Activo";
            await Update(GetCandidato);
            return true;

        }

        //Verificar Candidato
        public async Task<bool> VerificarCandidatoUpdate(int IdPuesto, int IdPartido)
        {

            var CandidatoExists = await _context.Candidatos.FirstOrDefaultAsync(x => x.IdPuestoElectivo == IdPuesto && x.IdPartido == IdPartido);

            if (CandidatoExists == null)
            {
                return true;
            }

            return false;
        }


        //Actualizar Candidato
        public async Task<bool> UpdateCandidatoDTO(int IdCandidato, CandidatoDTO CandidatoUpdate)
        {

            //try
            //{
                var CandidatoOriginall = await GetByIdAsync(IdCandidato);

                if (CandidatoOriginall == null)
                {
                    return false;
                }
                
                var Candidato = _mapper.Map<Candidatos>(CandidatoUpdate);
                CandidatoOriginall.Nombre = Candidato.Nombre;
                CandidatoOriginall.Apellido = Candidato.Apellido;
                CandidatoOriginall.IdPartido = Candidato.IdPartido;
                CandidatoOriginall.IdPuestoElectivo = Candidato.IdPuestoElectivo;

                await Update(CandidatoOriginall);
                return true;
                
            //}
            //catch
            //{
            //}


        }

        

    }
}
