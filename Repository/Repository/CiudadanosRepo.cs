using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.Models;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Repository.Repository
{
    public class CiudadanosRepo : RepositoryBase<Ciudadanos, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;


        public CiudadanosRepo(ItlaElectorDBContext context, UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager, IMapper mapper) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<CiudadanosViewModel> TraerCiudadanos()
        {
            var ciudadanos = await GetAllAsync();
            CiudadanosViewModel ciu = new CiudadanosViewModel();
            List<CiudadanosViewModel> TodosLosCiudadanos = new List<CiudadanosViewModel>();
            foreach (var c in ciudadanos)
            {
                var ciudadano = _mapper.Map<CiudadanosViewModel>(c);                
                TodosLosCiudadanos.Add(ciudadano);
            }
            ciu.ciudadanos = TodosLosCiudadanos;
            return ciu;
        }

        public async Task CrearCiudadanos(CiudadanosViewModel cvm)
        {
            var ciudadano = _mapper.Map<Ciudadanos>(cvm);
            await AddAsync(ciudadano);

        }

        public async Task EliminarCiudadanos(int id)
        {
            var ciudadano = await GetByIdAsync(id);
            if (ciudadano != null)
            {
                ciudadano.Estado = "Inactivo";
                await Update(ciudadano);
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
