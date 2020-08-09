using AutoMapper;
using DataBase.DTO;
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
        private readonly PuestoCanditadoApiRepos _puestoCanditadoApiRepos;

        public PuestoElectivoApiRepos(ItlaElectorDBContext context, IMapper mapper,
            PuestoCanditadoApiRepos puestoCanditadoApiRepos) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _puestoCanditadoApiRepos = puestoCanditadoApiRepos;

        }


        ///Metodo que adquiere el puesto de un candidato
        public async Task<string> PuestoCandidato(int IdPuesto) {
            var Puesto = await GetByIdAsync(IdPuesto);
            return Puesto.Nombre;
        }

        //Crear Puesto Electivo
        public async Task<bool> PostPuestoElectivo(CrearPuestoElectivoDTO puesto) {

            try
            {
                var PuestoElectivo = _mapper.Map<PuestoElectivo>(puesto);
                PuestoElectivo.Estado= "Activo";
                await AddAsync(PuestoElectivo);

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        //Desactivar Puestos Electivos
        public async Task<bool> DesactivarPuestoElectivos(int IdPuesto)
        {

            var PuestoElectivo = await GetByIdAsync(IdPuesto);

            if (PuestoElectivo == null)
            {
                return false;
            }

            var ok = await _puestoCanditadoApiRepos.DesactivarAllCandidatoPuesto(IdPuesto);

            if (ok)
            {
                PuestoElectivo.Estado = "Inactivo";
                await Update(PuestoElectivo);
            }

       
       
            return true;
        }

        //Activar Puesto Electivo
        public async Task<bool> ActivarPuestoElectivos(int IdPuesto)
        {

            var PuestoElectivo = await GetByIdAsync(IdPuesto);

            if (PuestoElectivo == null)
            {
                return false;
            }

            var ok = await _puestoCanditadoApiRepos.ActivarAllCandidatoPuesto(IdPuesto);

            if (ok)
            {
                PuestoElectivo.Estado = "Activo";
                await Update(PuestoElectivo);
            }

            return true;
        }

        //Actualizar Puesto Electivo
        public async Task<bool> ActualizarPuestoElectivo(int? IdPuesto, CrearPuestoElectivoDTO puestoUpdate)
        {

            try
            {
                var PuestoOriginal = await GetByIdAsync(IdPuesto.Value);

                if (PuestoOriginal == null)
                {
                    return false;
                }

                var Puesto = _mapper.Map<PuestoElectivo>(puestoUpdate);
                PuestoOriginal.Nombre = Puesto.Nombre;
                PuestoOriginal.Descripcion = Puesto.Descripcion;

                await Update(PuestoOriginal);
                return true;

            }
            catch
            {
                return false;
            }


        }




    }
}
