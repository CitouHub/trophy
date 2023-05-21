using AutoMapper;
using Trophy.Data;

namespace Trophy.Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Game, GameDTO>()
                .ForMember(dest => dest.PlayerResults, opt => opt.MapFrom(src => src.PlayerResults.OrderByDescending(_ => _.Win)))
                .ReverseMap();
            CreateMap<PlayerResult, PlayerResultDTO>()
                //.ForMember(dest => dest.Player, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Player, opt => opt.Ignore());
            CreateMap<Player, PlayerDTO>().ReverseMap();
        }
    }
}
