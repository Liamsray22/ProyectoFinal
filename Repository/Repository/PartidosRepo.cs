using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.Models;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Repository.Repository
{
    public class PartidosRepo : RepositoryBase<Partidos, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;


        public PartidosRepo(ItlaElectorDBContext context, UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager, IMapper mapper) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
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

            //public void Borrar(string path)
            //{
            //    File.SetAttributes(path, FileAttributes.Normal);
            //    System.GC.Collect();
            //    System.GC.WaitForPendingFinalizers();

            //    File.Delete(path);
            //}


        }
    }
