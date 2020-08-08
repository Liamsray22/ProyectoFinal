using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.Models;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Repository.Repository
{
    public class EleccionesRepo : RepositoryBase<Elecciones, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;


        public EleccionesRepo(ItlaElectorDBContext context, UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager, IMapper mapper) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<EleccionesViewModel> TraerElecciones()
        {
            var elecciones = await GetAllAsync();
            EleccionesViewModel ele = new EleccionesViewModel();
            List<EleccionesViewModel> TodasLasElecciones = new List<EleccionesViewModel>();
            foreach (var e in elecciones)
            {
                var eleccione = _mapper.Map<EleccionesViewModel>(e);
                TodasLasElecciones.Add(eleccione);
            }
            ele.elecciones = TodasLasElecciones;
            return ele;
        }

        public async Task CrearElecciones(EleccionesViewModel evm) {
            var eleccion = _mapper.Map<Elecciones>(evm);
            await AddAsync(eleccion);
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
