using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public class GameService(GameVisionDbContext dbContext)
{
    private readonly GameVisionDbContext _dbContext = dbContext;

    public async Task<(Game? result, string? error)> Create(GameViewModel model, string prefix)
    {
        var game = await _dbContext.Teams.Where(x => x.Prefix == prefix).FirstOrDefaultAsync();

        if (game is null)
            return (result: null, error: "Team not found");

        var entity = new Game
        {
            Name = model.Name,
        };

        await _dbContext.Games.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return (entity, null);
    }

    public async Task<List<Game>> GetByTeamPrefix(string teamPrefix)
    {
        var entities = await _dbContext.Games.Include(x => x.Team).Where(x => x.Team.Prefix == teamPrefix).ToListAsync();

        return entities;
    }

    public async Task<Game?> GetById(long id)
    {
        var entity = await _dbContext.Games.Where(x => x.Id == id).FirstOrDefaultAsync();

        return entity;
    }
}