using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.Models;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            var partidoslist = await _partidosRepo.TraerPartidosActivos();
            var puestoslist = await _puestosElectivos.TraerPuestosElectivosActivos();

            can.ListPartido = (List<PartidosViewModels>)partidoslist.partidos;
            can.ListPuestoElectivo = (List<PuestosElectivosViewModel>)puestoslist.puestos;
            can.candidatos = TodosLosCandidatos;
            return can;
        }

        public async Task CrearCandidatos(CandidatosViewModel cavm, string WebrootPath)
        {
            var candidato = _mapper.Map<Candidatos>(cavm);

            string uniqueName = null;
            var folderPath = Path.Combine(WebrootPath, "images/Candidato");
            uniqueName = Guid.NewGuid().ToString() + "_" + cavm.Photo.FileName;
            var filepath = Path.Combine(folderPath, uniqueName);

            if (filepath != null)
            {

                cavm.Photo.CopyTo(new FileStream(filepath, mode: FileMode.Create));
            }
            candidato.FotoPerfil = uniqueName;
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

        public async Task<bool> EditarCandidatos(CandidatosViewModel ucavm, string WebrootPath)
        {
            var candidato = await GetByIdAsync(ucavm.IdCandidato);
            if (candidato != null)
            {
                _context.Entry(candidato).State = EntityState.Detached;
                var can = _mapper.Map<Candidatos>(ucavm);

                string uniqueName = null;
                var folderPath = Path.Combine(WebrootPath, "images/Candidato");
                uniqueName = Guid.NewGuid().ToString() + "_" + ucavm.Photo.FileName;
                var filepath = Path.Combine(folderPath, uniqueName);
              
                
                if (filepath != null)
                {

                    ucavm.Photo.CopyTo(new FileStream(filepath, mode: FileMode.Create));
                }
                if (candidato.FotoPerfil != null)
                {
                    var filepathdelete = Path.Combine(folderPath, candidato.FotoPerfil.Trim());

                    if (File.Exists(filepathdelete))
                    {
                        File.Delete(filepathdelete);
                    }
                }
                    can.FotoPerfil = uniqueName;
                await Update(can);
                return true;
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
