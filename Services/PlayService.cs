using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public class PlayService(GameVisionDbContext dbContext, S3Service s3Service)
{
    private readonly GameVisionDbContext _dbContext = dbContext;
    private readonly S3Service _s3Service = s3Service;

    public async Task<List<Play>> GetByGameId(long gameId)
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
        entity.Goal = model.Goal;
        entity.Situation = model.Situation;
        entity.Yards = model.Yards;
        entity.OffensiveFormation = model.OffensiveFormation;
        entity.OffensivePlay = model.OffensivePlay;
        entity.DefensiveFormation = model.DefensiveFormation;
        entity.DefensivePlay = model.DefensivePlay;
        entity.Result = model.Result;
        entity.Penalty = model.Penalty;
        entity.FirstDown = model.FirstDown;
        entity.Touchdown = model.Touchdown;
        entity.Safety = model.Safety;
        entity.Runner = model.Runner;
        entity.Passer = model.Passer;
        entity.Target = model.Target;
        entity.TargetPosition = model.TargetPosition;
        entity.DefensiveTarget = model.DefensiveTarget;
        entity.Tackler = model.Tackler;
        entity.Interceptor = model.Interceptor;
        entity.OfensiveNotes = model.OfensiveNotes;
        entity.DefensiveNotes = model.DefensiveNotes;

        await _dbContext.SaveChangesAsync();

        return (result: entity, error: null);
    }

    public async Task<List<(Play? result, string? error)>> Create(long gameId, IFormFileCollection videos)
    {
        var listResult = new List<(Play?, string?)>();

        foreach (var video in videos)
        {
            var (result, error) = await Create(gameId, video);
            listResult.Add((result, error));
        }

        return listResult;
    }

    public async Task<(Play? result, string? error)> Create(long gameId, IFormFile video)
    {
        var game = await _dbContext.Games.Where(x => x.Id == gameId).FirstOrDefaultAsync();

        if (game is null)
            return (result: null, error: "Game not found");

        var fileId = Guid.NewGuid().ToString();

        var success = await _s3Service.UploadFile(fileId, game.Id.ToString(), video, "video/mp4");

        if (!success)
            return (result: null, error: "Failed to upload video");

        var entity = new Play
        {
            Game = game,
            FileId = fileId,
        };

        await _dbContext.Plays.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return (result: entity, error: null);
    }
}