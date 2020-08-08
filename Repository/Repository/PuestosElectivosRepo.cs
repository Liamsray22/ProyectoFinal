using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.Models;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Repository.Repository
{
    public class PuestosElectivosRepo : RepositoryBase<PuestoElectivo, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;


        public PuestosElectivosRepo(ItlaElectorDBContext context, UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager, IMapper mapper) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<PuestosElectivosViewModel> TraerPuestosElectivos()
        {
            var puestos = await GetAllAsync();
            PuestosElectivosViewModel par = new PuestosElectivosViewModel();
            List<PuestosElectivosViewModel> TodosLosPartidos = new List<PuestosElectivosViewModel>();
            foreach (var p in puestos) {
                var puesto = _mapper.Map<PuestosElectivosViewModel>(p);
                TodosLosPartidos.Add(puesto);
            }
            par.puestos = TodosLosPartidos;
            return par;
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
