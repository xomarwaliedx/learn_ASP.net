using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TestProj.DTOs;
using TestProj.Models;

namespace TestProj.Profiles
{
    public class GamesProfile : Profile
    {
        public GamesProfile()
        {
            CreateMap<CreateGameDto, Game>();
            CreateMap<Game, GameSummaryDto>()
                .ForMember(
                    dest => dest.GenreName,
                    opt => opt.MapFrom(src => src.Genre!.Name)
                );
            CreateMap<Game, GameDetailsDto>();
            CreateMap<UpdateGamedDto, Game>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // We'll handle Id in the custom logic
            .AfterMap((src, dest, context) =>
            {
                var id = (int)context.Items["Id"];
                dest.Id = id;
            });
            CreateMap<Genre, GenreDto>();
        }
    }
}