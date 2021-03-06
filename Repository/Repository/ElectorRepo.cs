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
    public class ElectorRepo : RepositoryBase<Elecciones, ItlaElectorDBContext>
    {
        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;
        private readonly PartidosRepo _partidosRepo;
        private readonly PuestosElectivosRepo _puestosElectivos;



        public ElectorRepo(ItlaElectorDBContext context, IMapper mapper, PartidosRepo partidosRepo,
            PuestosElectivosRepo puestosElectivos) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _partidosRepo = partidosRepo;
            _puestosElectivos = puestosElectivos;
        }

        public async Task<ElectorViewModel> elector(string cedula)
        {
            var puestos = await _puestosElectivos.TraerPuestosElectivos();
            //var lispuestos = new IEnumerable<PuestosElectivosViewModel>();
            ElectorViewModel el = new ElectorViewModel();
            el.puestosElectivos = puestos.puestos;
            var idcands = await _context.Votacion.Where(x=>x.Cedula.Contains(cedula)).Select(s=>s.IdCandidato).ToListAsync();

            if (idcands.Count() != 0)
            {
                List<Candidatos> listcandidatos = new List<Candidatos>();
                foreach (int idcand in idcands)
                {
                    var cand = await _context.Candidatos.FirstOrDefaultAsync(f => f.IdCandidato == idcand);
                    listcandidatos.Add(cand);
                    //if (cand != null)
                    //{
                    //    el.votados.Add(cand.IdPuestoElectivo);
                    //}

                }
                List<int> listid = new List<int>();

                foreach (var can in listcandidatos)
                {
                    int id = can.IdPuestoElectivo;
                    listid.Add(id);

                }
                el.votados = listid;
            }
            return el;
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
