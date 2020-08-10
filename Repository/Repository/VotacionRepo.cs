using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataBase.Models;
using DataBase.ViewModels;
using EmailConfig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public class VotacionRepo : RepositoryBase<Votacion, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;
        private readonly IMessage _message;
        private readonly PartidosRepo _partidosRepo;
        private readonly PuestosElectivosRepo _puestosElectivos;



        public VotacionRepo(ItlaElectorDBContext context, IMapper mapper, PartidosRepo partidosRepo,
            PuestosElectivosRepo puestosElectivos, IMessage message) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _partidosRepo = partidosRepo;
            _puestosElectivos = puestosElectivos;
            _message = message;
        }

        

        public async Task<VotacionViewModel> TraerCandidatosByIdPuestos(int id)
        {
            var candidatos = await _context.Candidatos.Where(x => x.IdPuestoElectivo == id).ToListAsync();
            VotacionViewModel vvm = new VotacionViewModel();
            List<CandidatosViewModel> TodosLosCandidatos = new List<CandidatosViewModel>();
            foreach (var c in candidatos)
            {
                var candidato = _mapper.Map<CandidatosViewModel>(c);
                var puesto = await _puestosElectivos.TraerPuestosElectivosById(candidato.IdPuestoElectivo);
                var partido = await _partidosRepo.TraerPartidosById(candidato.IdPartido);
                candidato.Partido = partido.Nombre;
                candidato.PuestoElectivo = puesto.Nombre;
                TodosLosCandidatos.Add(candidato);
                vvm.PuestoElectivo = candidato.PuestoElectivo;

            }
            vvm.candidatos = TodosLosCandidatos;
            return vvm;
        }

        public async Task Votar(VotacionViewModel vvm)
        {            
                Votacion votacion = new Votacion();
                votacion.Cedula = vvm.Cedula;
                votacion.IdCandidato = vvm.IdCandidato.Value;

            var ele = await _context.Elecciones.FirstOrDefaultAsync(x => x.Estado == "Progreso");
            votacion.IdEleccion = ele.IdEleccion;
                await AddAsync(votacion);
            
        }

        public async Task<bool> Finalizar(string cedula)
        {
            var puestos = await _context.PuestoElectivo.Where(w=>w.Estado == "Activo").ToListAsync();
            var ele = await _context.Elecciones.FirstOrDefaultAsync(x => x.Estado == "Progreso");
            var ciudadano = await _context.Ciudadanos.FirstOrDefaultAsync(c=>c.Cedula.Contains(cedula));
            var votacion = await _context.Votacion.Where(v => v.Cedula.Contains(cedula) && v.IdEleccion == ele.IdEleccion)
                .ToListAsync();
            List<VotacionViewModel> datos = new List<VotacionViewModel>();
            foreach (var cand in votacion)
            {
                VotacionViewModel vm = new VotacionViewModel();
                var candi = await _context.Candidatos.FirstOrDefaultAsync(c=>c.IdCandidato == cand.IdCandidato);
                var parti = await _context.Partidos.FirstOrDefaultAsync(p=>p.IdPartido == candi.IdPartido);
                var puesti = await _context.PuestoElectivo.FirstOrDefaultAsync(pp=>pp.IdPuestoElectivo == candi.IdPuestoElectivo);
                vm.Nombre = candi.Nombre;
                vm.Partido = parti.Nombre;
                vm.PuestoElectivo = puesti.Nombre;
                datos.Add(vm);
            }
            string info = "";
            foreach (var dato in datos)
            {
                info = info + " \n Puesto Electivo: " +dato.PuestoElectivo;
                info = info + "\n Partido Politico: " + dato.Partido;
                info = info + "\n Candidato:  " + dato.Nombre;


            }

            if (puestos.Count() == votacion.Count())
            {
                var mensaje = new Message(new string[] { ciudadano.Email }, "Saludos  "
                                            + ciudadano.Nombre + " " + ciudadano.Apellido + "",
                                            "Usted ha realizado su voto exitosamente "+
                                            info);
                await _message.SendMailAsync(mensaje);
                return true;
            }

            return false;
        }
        public async Task<bool> Verificarsivoto(string cedula)
        {

            var puestos = await _context.PuestoElectivo.Where(w => w.Estado == "Activo").ToListAsync();
            var ele = await _context.Elecciones.FirstOrDefaultAsync(x => x.Estado == "Progreso");

            var votacion = await _context.Votacion.Where(v => v.Cedula.Contains(cedula) && v.IdEleccion == ele.IdEleccion)
                .ToListAsync();

            if (puestos.Count() == votacion.Count())
            {
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
