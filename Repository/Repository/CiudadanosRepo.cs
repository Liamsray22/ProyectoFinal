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
    public class CiudadanosRepo : RepositoryBase<Ciudadanos, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;


        public CiudadanosRepo(ItlaElectorDBContext context, UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager, IMapper mapper, RoleManager<IdentityRole> roleManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager ;
        }
        public async Task CrearRole(string rol)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = rol
            };
            await _roleManager.CreateAsync(identityRole);
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
            var user = new IdentityUser { UserName = cvm.Cedula };
            var result = await _userManager.CreateAsync(user, cvm.Cedula);
            
            await _userManager.AddToRoleAsync(user,"CIUDADANOS");
            if (result.Succeeded) {
                var ciudadano = _mapper.Map<Ciudadanos>(cvm);
                await AddAsync(ciudadano);
            }

        }

        public async Task EliminarCiudadanos(string id)
        {
            var ciudadano = await _context.Ciudadanos.FirstOrDefaultAsync(x=>x.Cedula==id); ;
            if (ciudadano != null)
            {
                if (ciudadano.Estado.Equals("Activo"))
                {
                    ciudadano.Estado = "Inactivo";
                }
                else
                {
                    ciudadano.Estado = "Activo";
                }
                await Update(ciudadano);
            }
        }

        public async Task<bool> EditarCiudadanos(CiudadanosViewModel ucvm)
        {
            var ciudadano = await _context.Ciudadanos.FirstOrDefaultAsync(x => x.Cedula == ucvm.Cedula); ;
            if (ciudadano != null)
            {
                _context.Entry(ciudadano).State = EntityState.Detached;
                var ciud = _mapper.Map<Ciudadanos>(ucvm);
                await Update(ciud);
                return true;
            }
            return false;
        }

        public async Task<bool> verifyusercedula(string cedula)
        {
            var ciudadano = await _userManager.FindByNameAsync(cedula);
            if (ciudadano!=null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> USERactivo(string cedula)
        {

            var user = await _context.Ciudadanos.FirstOrDefaultAsync(a => a.Cedula.Trim() == cedula.Trim());
            if (user != null) { 
            if (user.Estado == "Inactivo")
            {
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
