using AutoMapper;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBase.DTO;

namespace Repository.APIRepository
{
    public class EleccionApiRepos : RepositoryBase<Elecciones, ItlaElectorDBContext>
    {

        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;
        private readonly CiudadApiRepos _ciudadanoApiRepos;
        private readonly VotacionesApiRepos _votacionesApiRepos;
        private readonly PuestosElectivosRepo _puestosElectivosRepo;
        private readonly CandidatosRepo _candidatosRepo;


        public EleccionApiRepos(ItlaElectorDBContext context, IMapper mapper, CiudadApiRepos ciudadApiRepos,
            VotacionesApiRepos votacionesApiRepos, PuestosElectivosRepo puestosElectivosRepo, CandidatosRepo candidatosRepo) : base(context)
        {

            _context = context;
            _mapper = mapper;
            _ciudadanoApiRepos = ciudadApiRepos;
            _votacionesApiRepos = votacionesApiRepos;
            _puestosElectivosRepo = puestosElectivosRepo;
            _candidatosRepo = candidatosRepo;

        }

        //verificando que el ciudadano exista
        public async Task<bool> VerificandoCiudadano(string Cedula)
        {


            var CiudadanoActivo = await _context.Ciudadanos.FirstOrDefaultAsync(x => x.Cedula == Cedula);

            if (CiudadanoActivo == null)
            {
                return false;
            }
            return true;
        }

        //Elecciones en la que a participado un ciudadano expecificamente.
        public async Task<List<EleccionesDTO>> EleccionesCiudadano(string cedulaCiudadano)
        {

            var ListadoIdElecciones_Ciudadano = await _votacionesApiRepos.ListIdElecciones(cedulaCiudadano);

            if (ListadoIdElecciones_Ciudadano == null)
            {
                return null;
            }

            var List = new List<EleccionesDTO>();

            foreach (var IdElecciones in ListadoIdElecciones_Ciudadano)
            {

                var EleccionBase = await GetByIdAsync(IdElecciones);

                if (EleccionBase.Estado == "Finalizado")
                {
                    var EleccionDTO = _mapper.Map<EleccionesDTO>(EleccionBase);
                    List.Add(EleccionDTO);
                }
            }

            return List;
        }

        public async Task<List<ResultadosDTO>> ResultadosElecciones(int id)
        {

            var totalvotos = await _context.Votacion.Where(a => a.IdEleccion == id).ToListAsync();
            var candidatos = totalvotos.Select(a => a.IdCandidato).Distinct().ToList();
            var list = new List<ResultadosDTO>();
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
                var resul = new ResultadosDTO();
                resul.ideleccion = id;
                resul.Nombre = newcandidato.Nombre;
                resul.Puesto = puesto.Nombre;
                resul.Votos = votos;
                resul.porcentaje = Math.Round((((votos * 100.00) / (totalvotospuestos * 100.00)) * 100.00));

                list.Add(resul);

            }

            return list;
        }

        public async Task<List<List<ResultadosDTO>>> TodasElecciones()
        {
            var elecciones = await _context.Elecciones.ToListAsync();
            List<List<ResultadosDTO>> lili = new List<List<ResultadosDTO>>();
            foreach (var elec in elecciones) {
                var totalvotos = await _context.Votacion.Where(a => a.IdEleccion == elec.IdEleccion).OrderByDescending(c=>c.IdCandidato).ToListAsync();
                var candidatos = totalvotos.Select(a => a.IdCandidato).Distinct().ToList();
                var list = new List<ResultadosDTO>();
                var Listidpuestos = new List<int>();
                foreach (var can in totalvotos)
                {
                    var newcandidato = await _context.Candidatos.FirstOrDefaultAsync(b => b.IdCandidato == can.IdCandidato);
                    var puesto = await _context.PuestoElectivo.FirstOrDefaultAsync(p => p.IdPuestoElectivo == newcandidato.IdPuestoElectivo);
                    Listidpuestos.Add(puesto.IdPuestoElectivo);
                }
                    string puestoanterior= "";
                double porcentajeanterior = 0;


                foreach (var can in candidatos)
                {
                    var newcandidato = await _context.Candidatos.FirstOrDefaultAsync(b => b.IdCandidato == can);
                    var puesto = await _context.PuestoElectivo.FirstOrDefaultAsync(p => p.IdPuestoElectivo == newcandidato.IdPuestoElectivo);
                    var totalvotospuestos = Listidpuestos.Count(a => a == puesto.IdPuestoElectivo);
                    var votos = totalvotos.Count(c => c.IdCandidato == newcandidato.IdCandidato);
                    var resul = new ResultadosDTO();
                    if (puestoanterior == resul.Puesto && porcentajeanterior <= resul.porcentaje)
                    {

                    }
                    else
                    {

                        resul.ideleccion = elec.IdEleccion;
                        resul.Nombre = newcandidato.Nombre;
                        resul.Puesto = puesto.Nombre;
                        resul.Votos = votos;
                        resul.porcentaje = Math.Round((((votos * 100.00) / (totalvotospuestos * 100.00)) * 100.00));
                        puestoanterior = resul.Puesto;
                        porcentajeanterior = resul.porcentaje;
                        list.Add(resul);

                    }

                }
                lili.Add(list);
            }

            return lili;
        }

        public async Task Resultados()
        {
            var elecciones = await GetAllAsync();
            EleccionesResulDTO ele = new EleccionesResulDTO();
            List<EleccionesResulDTO> TodasLasElecciones = new List<EleccionesResulDTO>();
            List<List<ResultadosDTO>> listoflist = new List<List<ResultadosDTO>>();
            foreach (var e in elecciones)
            {
                var eleccione = _mapper.Map<EleccionesResulDTO>(e);
                var resul = await ResultadosElecciones(e.IdEleccion);

                if (resul.Count > 0)
                {
                    listoflist.Add(resul);
                }
                TodasLasElecciones.Add(eleccione);
            }
            var eleccion = await _context.Elecciones.FirstOrDefaultAsync(a => a.Estado.Trim() == "Progreso");
            if (eleccion != null)
            {

                ele.Procesoactivos = true;
            }
            else
            {
                if (await _puestosElectivosRepo.validacionepuestosE())
                {
                    ele.disponibilidadpuestos = true;
                }
                if (await _candidatosRepo.TraerCandidatosActivos())
                {
                    ele.disponibilidadecandidatos = true;
                }
            }


        }

    }
}
