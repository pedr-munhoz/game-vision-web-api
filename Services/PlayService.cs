using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public class PlayService(GameVisionDbContext dbContext, GoogleDriveService googleDriveService)
{
    private readonly GameVisionDbContext _dbContext = dbContext;
    private readonly GoogleDriveService _googleDriveService = googleDriveService;

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

    public async Task<(int count, string? error)> Load(long gameId, string folderId)
    {
        var game = await _dbContext.Games.Where(x => x.Id == gameId).FirstOrDefaultAsync();

        if (game is null)
            return (count: 0, error: "Game not found");

        var (fileIds, fileListingError) = await _googleDriveService.ListFileIds(folderId);

        if (fileListingError is not null)
            return (count: 0, error: fileListingError);

        var createdPlays = await GeneratePlays(game, fileIds);

        return (count: createdPlays, error: null);
    }

    private async Task<int> GeneratePlays(Game game, List<string> fileIds)
    {
        var entities = fileIds.Select(x => new Play
        {
            Game = game,
            FileId = x,
        });

        await _dbContext.Plays.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();

        return entities.Count();
    }
}