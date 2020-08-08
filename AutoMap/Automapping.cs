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

        }

        private void MapearPartidos()
        {
            CreateMap<PartidosViewModels, Partidos>().ReverseMap();
            //ForMember(dest => dest.campo, opt => opt.Ignore()).
            //ForMember(dest => dest.campo, opt => opt.Ignore());
        }


    }
}
