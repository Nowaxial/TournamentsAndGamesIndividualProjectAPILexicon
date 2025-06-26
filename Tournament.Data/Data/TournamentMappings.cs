using AutoMapper;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;

namespace Tournament.Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings()
        {
            CreateMap<TournamentDetails, TournamentDetailsDto>()
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.StartDate.AddMonths(3)))
                .ReverseMap();
            CreateMap<Game, GameDto>().ReverseMap();
            CreateMap<GameCreateDto, Game>().ReverseMap();
            CreateMap<GameUpdateDto, Game>().ReverseMap();
            CreateMap<TournamentDetailsCreateDto, TournamentDetails>().ReverseMap();
            CreateMap<TournamentDetailsUpdateDto, TournamentDetails>().ReverseMap();
        }
    }
}