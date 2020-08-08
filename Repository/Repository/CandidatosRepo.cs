using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.Models;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Repository.Repository
{
    public class CandidatosRepo : RepositoryBase<Candidatos, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;
        private readonly PartidosRepo _partidosRepo;
        private readonly PuestosElectivosRepo _puestosElectivos;



        public CandidatosRepo(ItlaElectorDBContext context, IMapper mapper, PartidosRepo partidosRepo,
            PuestosElectivosRepo puestosElectivos) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _partidosRepo = partidosRepo;
            _puestosElectivos = puestosElectivos;
        }

        public async Task<CandidatosViewModel> TraerCandidatos()
        {
            var candidatos = await GetAllAsync();
            CandidatosViewModel can = new CandidatosViewModel();
            List<CandidatosViewModel> TodosLosCandidatos = new List<CandidatosViewModel>();
            foreach (var c in candidatos) {
                var candidato = _mapper.Map<CandidatosViewModel>(c);
                var puesto = await _puestosElectivos.TraerPuestosElectivosById(candidato.IdPuestoElectivo);
                var partido = await _partidosRepo.TraerPartidosById(candidato.IdPartido);
                candidato.Partido = partido.Nombre;
                candidato.PuestoElectivo = puesto.Nombre;
                TodosLosCandidatos.Add(candidato);
            }
            can.candidatos = TodosLosCandidatos;
            return can;
        }

        public async Task CrearCandidatos(CandidatosViewModel cavm)
        {
            var candidato = _mapper.Map<Candidatos>(cavm);
            await AddAsync(candidato);

        }

        public async Task EliminarCandidatos(int id)
        {
            var candidato = await GetByIdAsync(id);
            if (candidato != null)
            {
                candidato.Estado = "Inactivo";
                await Update(candidato);

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
