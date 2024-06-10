using AutoMapper;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;

namespace game_vision_web_api.Models.MapperProfiles;

public class ViewModelToEntityProfile : Profile
{
    public ViewModelToEntityProfile()
    {
        CreateMap<TeamViewModel, Team>();
        CreateMap<GameViewModel, Game>();
        CreateMap<PlayViewModel, Play>();
    }
}
