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
        public async Task<bool> TraerCandidatosActivos()
        {

            var TodosPuesto = await _context.PuestoElectivo.Where(a => a.Estado.Trim() == "Activo").Select(a => a.IdPuestoElectivo).Distinct().ToListAsync();

            int verificado = 0;

            foreach (var id in TodosPuesto) {

                var candidatos = await _context.Candidatos.Where(a => a.Estado.Trim() == "Activo" && a.IdPuestoElectivo == id)
                    .ToListAsync();

                int contador = candidatos.Count();

                if (contador >= 3) {
                    verificado++;
                }

            }
            
            
            if (verificado >=  4)
            {
                return false;
            }
            
            return true;
            
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
                if (candidato.Estado.Equals("Activo"))
                {
                    candidato.Estado = "Inactivo";
               
            }
            else
            {
                candidato.Estado = "Activo";
            }
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
                can.Estado = candidato.Estado;
                if (ucavm.Photo != null) { 
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

                }
                    can.FotoPerfil = uniqueName;
                }
                else
                {

                    can.FotoPerfil = candidato.FotoPerfil;
                }
                can.Nombre = ucavm.Nombreedit.Trim();
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
