using AutoMapper;

namespace AutoMap
{
    public class Automapping : Profile
    {
        public Automapping()
        {
            
            MapearEntidad();

        }

        private void MapearEntidad()
        {
            //CreateMap<ViewModel, Entidad>().ReverseMap().
            //ForMember(dest => dest.campo, opt => opt.Ignore()).
            //ForMember(dest => dest.campo, opt => opt.Ignore());
        }


    }
}
