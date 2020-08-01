using AutoMapper;

namespace AutoMap
{
    public class APIAutomapping : Profile
    {
        public APIAutomapping()
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
