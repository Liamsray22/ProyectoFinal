using System;
using AutoMapper;
using DataBase.Models;
using DataBase.ViewModels;

namespace AutoMap
{
    public class Automapping : Profile
    {
        public Automapping()
        {

            MapearPartidos();
            MapearPuestosElectivos();
            MapearCandidatos();
            MapearCiudadanos();
            MapearElecciones();

        }

        private void MapearElecciones()
        {
            CreateMap<EleccionesViewModel, Elecciones>().ReverseMap().
            ForMember(dest => dest.Procesoactivos, opt => opt.Ignore());
        }

        private void MapearCiudadanos()
        {
            CreateMap<CiudadanosViewModel, Ciudadanos>().ReverseMap();
        }

        private void MapearPuestosElectivos()
        {
            CreateMap<PuestosElectivosViewModel, PuestoElectivo>().ReverseMap();
        }
        private void MapearCandidatos()
        {
            CreateMap<CandidatosViewModel, Candidatos>().ReverseMap().
            ForMember(dest => dest.Partido, opt => opt.Ignore()).
            ForMember(dest => dest.PuestoElectivo, opt => opt.Ignore());
        }

        private void MapearPartidos()
        {
            CreateMap<PartidosViewModels, Partidos>().ReverseMap();
            //ForMember(dest => dest.campo, opt => opt.Ignore()).
            //ForMember(dest => dest.campo, opt => opt.Ignore());
        }



    }
}
