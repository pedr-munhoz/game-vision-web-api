using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public class GameService(GameVisionDbContext dbContext)
{
    private readonly GameVisionDbContext _dbContext = dbContext;

    public async Task<Game> Create(GameViewModel model)
    {
        var entity = new Game
        {
            Name = model.Name,
        };

        await _dbContext.Games.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<List<Game>> GetAll()
    {
        var entities = await _dbContext.Games.ToListAsync();

        return entities;
    }
}