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
    public class PartidoApiRepos : RepositoryBase<Partidos, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;
        private readonly CandidatoApiRepos _candidatoApiRepos;

        public PartidoApiRepos(ItlaElectorDBContext context, IMapper mapper,
            CandidatoApiRepos candidatoApiRepos) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _candidatoApiRepos = candidatoApiRepos;

        }

        ///Todos los partidos
        public async Task<List<PartidoDTO>> GetAllPartidoDTO()
        {
            string Activo = "Activo";

            var GetAllPartidosActivos = await _context.Partidos.Where(x => x.Estado == Activo).ToListAsync();
            var ObjectListPartidoDTO = new List<PartidoDTO>();

            if (GetAllPartidosActivos == null)
            {
                return null;
            }

            foreach (var partidos in GetAllPartidosActivos)
            {

                var PartidoDTO = _mapper.Map<PartidoDTO>(partidos);
                ObjectListPartidoDTO.Add(PartidoDTO);

            }


            return ObjectListPartidoDTO;


        }

        //Todos Los Candidatos de un partido
        public async Task<List<CanditosPartidoDTO>> GetListSpecificPartido(int id)
        {

            var ListadoCandidatosPartido = await _candidatoApiRepos.GetPartidoCandidatosList(id);

            if (ListadoCandidatosPartido == null)
            {
                return null;
            }

            return ListadoCandidatosPartido;
        }

        //Agreando Partido
        public async Task<bool> PostNewPartido(CrearPartidoDTO newPartido)
        {

            try
            {
                var Partido = _mapper.Map<Partidos>(newPartido);
                Partido.Estado = "Activo";
                Partido.Logo = "LogoNone.jpg";
                await AddAsync(Partido);
                return true;
            }
            catch
            {
                return false;
            }

        }


        //Desactivar Partido
        public async Task<bool> DesactivarPartido(int IdPartido)
        {

            var GetPartido = await GetByIdAsync(IdPartido);

            if (GetPartido == null)
            {
                return false;
            }

            var ok = await _candidatoApiRepos.DesactivarAllCandidato(IdPartido);

            if (ok)
            {
                GetPartido.Estado = "Inactivo";
                await Update(GetPartido);
            }

            return true;
        }

        //Activar Partido
        public async Task<bool> ActivarPartido(int IdPartido)
        {

            var GetPartido = await GetByIdAsync(IdPartido);

            if (GetPartido == null)
            {
                return false;
            }

            var ok = await _candidatoApiRepos.ActivarAllCandidato(IdPartido);

            if (ok)
            {
                GetPartido.Estado = "Activo";
                await Update(GetPartido);
            }

            return true;
        }

        //Actualizar Partido
        public async Task<bool> ActualizarPartido(int? IdPartido, CrearPartidoDTO PartidoUpdate)
        {

            try
            {
                var PartidoOriginal = await GetByIdAsync(IdPartido.Value);

                if (PartidoOriginal == null)
                {
                    return false;
                }

                var Partido = _mapper.Map<Partidos>(PartidoUpdate);
                PartidoOriginal.Nombre = Partido.Nombre;
                PartidoOriginal.Descripcion = Partido.Descripcion;

                await Update(PartidoOriginal);
                return true;

            }
            catch
            {
                return false;
            }


        }

        //Verificando que el partido no este Inactivo
        public async Task<bool> PartidoInactivoVerificacion(int IdPartido)
        {

            var action = await GetByIdAsync(IdPartido);

            if (action.Estado == "Inactivo")
            {
                return false;
            }

            return true;

        }

    }

}
