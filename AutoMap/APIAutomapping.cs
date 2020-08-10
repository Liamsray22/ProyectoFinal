using AutoMapper;
using DataBase.DTO;
using DataBase.Models;

namespace AutoMap
{
    public class APIAutomapping : Profile
    {
        public APIAutomapping()
        {

            MapearEntidad();
            MapearPartidoDTO_Partido();
            MapearCandidatoDTO_Candidato();
            MapearCandidatoPartidoDTO_Candidato();
            MapearCrearPartidoDTO_Partido();
            MapearPuestoElectivoDTO_PuestoElectivo();
            MapearEleccionesDTO_Eleccion();

        }

        private void MapearEntidad()
        {
            //CreateMap<ViewModel, Entidad>().ReverseMap().
            //ForMember(dest => dest.campo, opt => opt.Ignore()).
            //ForMember(dest => dest.campo, opt => opt.Ignore());
        }

        private void MapearEleccionesDTO_Eleccion()
        {
            CreateMap<EleccionesDTO, Elecciones>().ReverseMap();
        }



        private void MapearPartidoDTO_Partido()
        {
            CreateMap<PartidoDTO, Partidos>().ReverseMap();
        }


        private void MapearCandidatoDTO_Candidato()
        {
            CreateMap<CandidatoDTO, Candidatos>().ReverseMap().
             ForMember(dest => dest.Puesto, opt => opt.Ignore()); 
        }

        private void MapearCandidatoPartidoDTO_Candidato()
        {
            CreateMap<CanditosPartidoDTO, Candidatos>().ReverseMap().
             ForMember(dest => dest.Puesto, opt => opt.Ignore());
        }

        private void MapearCrearPartidoDTO_Partido()
        {
            CreateMap<Partidos, CrearPartidoDTO>().ReverseMap();
        }

        private void MapearPuestoElectivoDTO_PuestoElectivo()
        {
            CreateMap<PuestoElectivo, CrearPuestoElectivoDTO>().ReverseMap();
        }


    }
}
