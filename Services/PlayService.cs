using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public class PlayService(GameVisionDbContext dbContext)
{
    private readonly GameVisionDbContext _dbContext = dbContext;

    public async Task<List<Play>> Get(long gameId)
    {
        var entities = await _dbContext.Plays.Where(x => x.GameId == gameId).ToListAsync();

        return entities;
    }

    public async Task<(Play? result, string? error)> Update(long id, PlayViewModel model)
    {
        var entity = await _dbContext.Plays.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (entity is null)
            return (result: null, error: "Play not found");

        entity.PlayNumber = model.PlayNumber;
        entity.Offense = model.Offense;
        entity.Defense = model.Defense;
        entity.Down = model.Down;
        entity.Distance = model.Distance;
        entity.Formation = model.Formation;
        entity.Name = model.Name;
        entity.Yards = model.Yards;
        entity.Result = model.Result;
        entity.FirstDown = model.FirstDown;
        entity.Touchdown = model.Touchdown;
        entity.Notes = model.Notes;

        await _dbContext.SaveChangesAsync();

        return (result: entity, error: null);
    }
}