using AutoMapper;
using game_vision_web_api.Models.DTOs;
using game_vision_web_api.Models.Entities;

namespace game_vision_web_api.Models.MapperProfiles;

public class EntityToDtoProfile : Profile
{
    public EntityToDtoProfile()
    {
        CreateMap<Team, TeamDTO>();
        CreateMap<Game, GameDTO>();
        CreateMap<Play, PlayDTO>();
        CreateMap<User, UserDTO>();
    }
}
