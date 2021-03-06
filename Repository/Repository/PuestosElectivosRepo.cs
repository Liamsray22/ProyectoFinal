﻿using System.Collections.Generic;
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

        public async Task<PuestosElectivosViewModel> TraerPuestosElectivosActivos()
        {
            var puestos = await _context.PuestoElectivo.Where(puesto => puesto.Estado.Trim() == "Activo").ToListAsync();
            PuestosElectivosViewModel par = new PuestosElectivosViewModel();
            List<PuestosElectivosViewModel> TodosLosPartidos = new List<PuestosElectivosViewModel>();
            foreach (var p in puestos)
            {
                var puesto = _mapper.Map<PuestosElectivosViewModel>(p);
                TodosLosPartidos.Add(puesto);
            }
            par.puestos = TodosLosPartidos;
            return par;
        }
        public async Task<PuestosElectivosViewModel> TraerPuestosElectivosById(int id)
        {
            var PuestosElectivos = await GetByIdAsync(id);
            if (PuestosElectivos != null)
            {
                var PuestosElectivosMap = _mapper.Map<PuestosElectivosViewModel>(PuestosElectivos);
                return PuestosElectivosMap;
            }
            return null;
        }
        public async Task<bool> validacionepuestosE()
        {
            var PuestosElectivos = await _context.PuestoElectivo.Where(a => a.Estado.Trim() == "Activo").ToListAsync();
            if (PuestosElectivos.Count>=4)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> verifyPuestos(string puessto)
        {
            var puesto = await _context.PuestoElectivo.FirstOrDefaultAsync(a => a.Nombre.Contains(puessto));
            if (puesto != null)
            {
                return true;
            }
            return false;
        }
        public async Task CrearPuestosElectivos(PuestosElectivosViewModel pevm)
        {
            var puesto = _mapper.Map<PuestoElectivo>(pevm);
            await AddAsync(puesto);
            var partido = await _context.Partidos.FirstOrDefaultAsync(p=>p.Nombre.Contains("Ninguno"));
            if (partido == null)
            {
                var part = new Partidos();
                part.Nombre = "Ninguno";
                part.Descripcion = "Partido Ninguno";
                part.Logo = "UserPuestos.jpg";
                part.Estado = "Activo";

                await _context.Partidos.AddAsync(part);
                await _context.SaveChangesAsync();
            }
            var partido2 = await _context.Partidos.FirstOrDefaultAsync(p => p.Nombre.Contains("Ninguno"));
            var cand = new Candidatos();
            cand.Nombre = "Ninguno";
            cand.Apellido = "";
            cand.IdPuestoElectivo = puesto.IdPuestoElectivo;
            cand.IdPartido = partido2.IdPartido;
            cand.FotoPerfil = "UserPuestos.jpg";
            cand.Estado = "Activo";
            await _context.Candidatos.AddAsync(cand);
            await _context.SaveChangesAsync();


        }

        public async Task EliminarPuestosElectivos(int id)
        {
            var puesto = await GetByIdAsync(id);
            if (puesto != null)
            {
                if (puesto.Estado.Equals("Activo"))
                {
                    puesto.Estado = "Inactivo";
                var candidatos = await _context.Candidatos.Where(a => a.IdPuestoElectivo == puesto.IdPuestoElectivo).ToListAsync();
                foreach (var can in candidatos)
                {
                    
                    can.Estado = "Inactivo";
                    _context.Entry(can).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
             
                }
                else
                {
                    puesto.Estado = "Activo";
                }
                await Update(puesto);
            }

        }
        

        public async Task<bool> EditarPuestosElectivos(PuestosElectivosViewModel upevm)
        {
            var puesto = await GetByIdAsync(upevm.IdPuestoElectivo);
            if (puesto != null)
            {
                _context.Entry(puesto).State = EntityState.Detached;
                var pue = _mapper.Map<PuestoElectivo>(upevm);
                await Update(pue);
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
