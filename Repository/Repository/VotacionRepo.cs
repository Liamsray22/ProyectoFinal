using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.Models;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class VotacionRepo : RepositoryBase<Votacion, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;
        private readonly PartidosRepo _partidosRepo;
        private readonly PuestosElectivosRepo _puestosElectivos;



        public VotacionRepo(ItlaElectorDBContext context, IMapper mapper, PartidosRepo partidosRepo,
            PuestosElectivosRepo puestosElectivos) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _partidosRepo = partidosRepo;
            _puestosElectivos = puestosElectivos;
        }

        

        public async Task<VotacionViewModel> TraerCandidatosByIdPuestos(int id)
        {
            var candidatos = await _context.Candidatos.Where(x => x.IdPuestoElectivo == id).ToListAsync();
            VotacionViewModel vvm = new VotacionViewModel();
            List<CandidatosViewModel> TodosLosCandidatos = new List<CandidatosViewModel>();
            foreach (var c in candidatos)
            {
                var candidato = _mapper.Map<CandidatosViewModel>(c);
                var puesto = await _puestosElectivos.TraerPuestosElectivosById(candidato.IdPuestoElectivo);
                var partido = await _partidosRepo.TraerPartidosById(candidato.IdPartido);
                candidato.Partido = partido.Nombre;
                candidato.PuestoElectivo = puesto.Nombre;
                TodosLosCandidatos.Add(candidato);
                vvm.PuestoElectivo = candidato.PuestoElectivo;

            }
            vvm.candidatos = TodosLosCandidatos;
            return vvm;
        }

        public async Task Votar(VotacionViewModel vvm)
        {            
                Votacion votacion = new Votacion();
                votacion.Cedula = vvm.Cedula;
                votacion.IdCandidato = vvm.IdCandidato.Value;
                var ele = await _context.Elecciones.FirstOrDefaultAsync(x => x.Estado == "En Proceso");
                votacion.IdEleccion = ele.IdEleccion;
                await AddAsync(votacion);
            
        }

        public async Task<bool> Finalizar(string cedula)
        {
            var puestos = await _context.PuestoElectivo.Where(w=>w.Estado == "Activo").ToListAsync();
            var ele = await _context.Elecciones.FirstOrDefaultAsync(x => x.Estado == "En Proceso");

            var votacion = await _context.Votacion.Where(v => v.Cedula.Contains(cedula) && v.IdEleccion == ele.IdEleccion)
                .ToListAsync();

            if (puestos.Count() == votacion.Count())
            {
                return true;
            }

            return false;
        }
        public async Task<bool> Verificarsivoto(string cedula)
        {

            var eleccion = await _context.Elecciones.FirstOrDefaultAsync(a => a.Estado.Trim() == "Activo");
            if(eleccion != null)
            {
                var voto = await _context.Votacion.FirstOrDefaultAsync(a => ((a.IdEleccion == eleccion.IdEleccion) && (a.Cedula.Trim() == cedula.Trim())));
                if (voto != null) {

                    return true;
                }
              
            }
            return false;
        }

        //public void Borrar(string path)
        //{
        //    File.SetAttributes(path, FileAttributes.Normal);
        //    System.GC.Collect();
        //    System.GC.WaitForPendingFinalizers();

        //    File.Delete(path);
        //}


    }
}
