using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using game_vision_web_api.Models.DTOs;
using game_vision_web_api.Models.Entities;

namespace game_vision_web_api.Models.MapperProfiles;

public class DomainToDtoProfile : Profile
{
    public DomainToDtoProfile()
    {
        CreateMap<Team, TeamDTO>();
        CreateMap<Game, GameDTO>();
        CreateMap<Play, PlayDTO>();
    }
}
