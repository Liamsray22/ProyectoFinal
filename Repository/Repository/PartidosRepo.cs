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
    public class PartidosRepo : RepositoryBase<Partidos, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly CandidatosRepo _candidatorepo;


        public PartidosRepo(ItlaElectorDBContext context, UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager, IMapper mapper, CandidatosRepo candidatorepo) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _candidatorepo = candidatorepo;

        }

        public async Task<PartidosViewModels> TraerPartidos()
        {
            var partidos = await GetAllAsync();
            PartidosViewModels par = new PartidosViewModels();
            List<PartidosViewModels> TodosLosPartidos = new List<PartidosViewModels>();
            foreach (var p in partidos) {
                var partido = _mapper.Map<PartidosViewModels>(p);
                TodosLosPartidos.Add(partido);
            }
            par.partidos = TodosLosPartidos;
            return par;
        }

        public async Task<PartidosViewModels> TraerPartidosActivos()
        {
            var partidos = await _context.Partidos.Where(partido => partido.Estado.Trim() == "Activo").ToListAsync(); 
            PartidosViewModels par = new PartidosViewModels();
            List<PartidosViewModels> TodosLosPartidos = new List<PartidosViewModels>();
            foreach (var p in partidos)
            {
                var partido = _mapper.Map<PartidosViewModels>(p);
                TodosLosPartidos.Add(partido);
            }
            par.partidos = TodosLosPartidos;
            return par;
        }

        public async Task<PartidosViewModels> TraerPartidosById(int id)
        {
            var partido = await GetByIdAsync(id);
            if(partido != null)
            {
                var partidoMap = _mapper.Map<PartidosViewModels>(partido);
                return partidoMap;
            }
            return null;
        }

        public async Task CrearPartido(PartidosViewModels pvm, string webroot) {
            var partido = _mapper.Map<Partidos>(pvm);
            string uniqueName = null;
            var folderPath = Path.Combine(webroot, "images/Partido");
            uniqueName = Guid.NewGuid().ToString() + "_" + pvm.Photo.FileName;
            var filepath = Path.Combine(folderPath, uniqueName);

            if (filepath != null)
            {

                pvm.Photo.CopyTo(new FileStream(filepath, mode: FileMode.Create));
            }
            partido.Logo = uniqueName;
            await AddAsync(partido);
        }

        public async Task EliminarPartido(int id)
        {
            var partido = await GetByIdAsync(id);
            if(partido != null)
            {
                partido.Estado = "Inactivo";
                await Update(partido);
                var candidatos = await _context.Candidatos.Where(a => a.IdPartido == partido.IdPartido).ToListAsync();
                foreach (var can in candidatos)
                {
                    can.Estado = "Inactivo";
                    await _candidatorepo.Update(can);
                }
            }
        }

        public async Task<bool> EditarPartidos(PartidosViewModels upvm, string WebrootPath)
        {
            var partido = await GetByIdAsync(upvm.IdPartido);
            if (partido != null)
            {
                _context.Entry(partido).State = EntityState.Detached;
                var par = _mapper.Map<Partidos>(upvm);
                string uniqueName = null;
                var folderPath = Path.Combine(WebrootPath, "images/Partido");
                uniqueName = Guid.NewGuid().ToString() + "_" + upvm.Photo.FileName;
                var filepath = Path.Combine(folderPath, uniqueName);


                if (filepath != null)
                {

                    upvm.Photo.CopyTo(new FileStream(filepath, mode: FileMode.Create));
                }
                if (partido.Logo != null)
                {
                    var filepathdelete = Path.Combine(folderPath, partido.Logo.Trim());

                    if (File.Exists(filepathdelete))
                    {
                        File.Delete(filepathdelete);
                    }
                }
                par.Logo = uniqueName;
                await Update(par);
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
