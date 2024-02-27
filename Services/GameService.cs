using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;

namespace game_vision_web_api.Services;

public class GameService
{
    public async Task<Game> Create(GameViewModel model)
    {
        var entity = new Game
        {
            Name = model.Name,
        };

        return entity;
    }

    public async Task<List<Game>> GetAll()
    {
        var entities = new List<Game>();

        return entities;
    }
}