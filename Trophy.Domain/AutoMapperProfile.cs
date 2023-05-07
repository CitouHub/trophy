using AutoMapper;
using Trophy.Data;
using Trophy.Domain;

namespace Trophy.Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Game, GameDTO>().ReverseMap();
            CreateMap<PlayerResult, PlayerResultDTO>().ReverseMap();
        }
    }
}
