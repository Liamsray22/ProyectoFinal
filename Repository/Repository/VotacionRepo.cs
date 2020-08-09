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

        public async Task Votar(CandidatosViewModel vcvm)
        {

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
            if (vvm.IdCandidato != 0 || vvm.IdCandidato != null) {
                Votacion votacion = new Votacion();
                votacion.Cedula = vvm.Cedula;
                votacion.IdCandidato = vvm.IdCandidato;
                var ele = await _context.Elecciones.FirstOrDefaultAsync(x => x.Estado == "En Proceso");
                votacion.IdEleccion = ele.IdEleccion;
                await AddAsync(votacion);
            }
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
