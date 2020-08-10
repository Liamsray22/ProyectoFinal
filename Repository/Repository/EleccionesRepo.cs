using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.Models;
using DataBase.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace Repository.Repository
{
    public class EleccionesRepo : RepositoryBase<Elecciones, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly PuestosElectivosRepo _puestosElectivos;



        public EleccionesRepo(ItlaElectorDBContext context, UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager, IMapper mapper,
                            PuestosElectivosRepo puestosElectivos) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _puestosElectivos = puestosElectivos;
        }

        public async Task<EleccionesViewModel> TraerElecciones()
        {
            var elecciones = await GetAllAsync();
            EleccionesViewModel ele = new EleccionesViewModel();
            List<EleccionesViewModel> TodasLasElecciones = new List<EleccionesViewModel>();
            List<List<ResultadosViewModel>> listoflist = new List<List<ResultadosViewModel>>();
            foreach (var e in elecciones)
            {
                var eleccione = _mapper.Map<EleccionesViewModel>(e);
                var resul = await ResultadosElecciones(e.IdEleccion);

                if (resul.Count >0)
                {
                    listoflist.Add(resul);
                }
                TodasLasElecciones.Add(eleccione);
            }
            var eleccion = await _context.Elecciones.FirstOrDefaultAsync(a => a.Estado.Trim() == "Progreso");
            if (eleccion !=null)
            {

                ele.Procesoactivos = true;
            }
            ele.Resultados = listoflist;
            ele.elecciones = TodasLasElecciones;
            return ele;
        }

        public async Task<bool> CrearElecciones(EleccionesViewModel evm) {
            var pe = await _context.PuestoElectivo.Where(p=>p.Estado.Contains("Activo")).ToListAsync();
            if (pe.Count()<4)
            {
                return false;

            }
            var eleccion = _mapper.Map<Elecciones>(evm);
            await AddAsync(eleccion);
            return true;
        }

        public async Task<bool> eleccionesactivas()
        {

            var eleccion = await _context.Elecciones.FirstOrDefaultAsync(a => a.Estado == "Progreso");
            if (eleccion == null)
            {
                return true;
            }
            return false;
        }


        public async Task<List<ResultadosViewModel>> ResultadosElecciones(int id)
        {

            var totalvotos = await _context.Votacion.Where(a => a.IdEleccion == id).ToListAsync();
            var candidatos =  totalvotos.Select(a => a.IdCandidato).Distinct().ToList();
            var list = new List<ResultadosViewModel>();
            var Listidpuestos = new List<int>();
            foreach (var can in totalvotos)
            {
                var newcandidato = await _context.Candidatos.FirstOrDefaultAsync(b => b.IdCandidato == can.IdCandidato);
                var puesto = await _context.PuestoElectivo.FirstOrDefaultAsync(p => p.IdPuestoElectivo == newcandidato.IdPuestoElectivo);
                Listidpuestos.Add(puesto.IdPuestoElectivo);
            }

            foreach (var can in candidatos)
            {
                var newcandidato = await _context.Candidatos.FirstOrDefaultAsync(b => b.IdCandidato == can);
                var puesto = await _context.PuestoElectivo.FirstOrDefaultAsync(p => p.IdPuestoElectivo == newcandidato.IdPuestoElectivo);
                var totalvotospuestos = Listidpuestos.Count(a => a == puesto.IdPuestoElectivo);
                var votos = totalvotos.Count(c => c.IdCandidato == newcandidato.IdCandidato);
                var resul = new ResultadosViewModel();
                resul.ideleccion = id;
                resul.Nombre = newcandidato.Nombre;
                resul.Puesto = puesto.Nombre;
                resul.Votos = votos;
                resul.porcentaje = (votos / totalvotospuestos) * 100;

                list.Add(resul);
               
            }

            return list;
        }


        public async Task<bool> FinalizarEleccion()
        {

            var eleccion = await _context.Elecciones.FirstOrDefaultAsync(a => a.Estado.Trim() == "Progreso");
            if (eleccion != null)
            {
                eleccion.Estado = "Finalizado";
                await Update(eleccion);
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
