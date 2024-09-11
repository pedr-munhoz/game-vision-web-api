using AutoMapper;
using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.DTOs;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public class PlayService(GameVisionDbContext dbContext, IMapper mapper, S3Service s3Service)
{
    private readonly GameVisionDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly S3Service _s3Service = s3Service;

    public async Task<(List<PlayDTO>?, string?)> GetByGameId(long gameId, string userId)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return (null, "User not found");

        var game = await _dbContext.Games
            .Include(x => x.Plays)
            .Where(x => x.Id == gameId)
            .Where(x => x.TeamId == user.TeamId)
            .FirstOrDefaultAsync();

        if (game is null)
            return (null, "Game not found");

        return (game.Plays.Select(_mapper.Map<PlayDTO>).ToList(), null);
    }

    public async Task<(Play?, string?)> Update(long id, PlayViewModel model, string userId)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return (null, "User not found");

        var play = await _dbContext.Plays
            .Include(x => x.Game)
            .Where(x => x.Id == id)
            .Where(x => x.Game.TeamId == user.TeamId)
            .FirstOrDefaultAsync();

        if (play is null)
            return (null, "Play not found");

        play.PlayNumber = model.PlayNumber;
        play.Offense = model.Offense;
        play.Defense = model.Defense;
        play.Down = model.Down;
        play.Distance = model.Distance;
        play.Goal = model.Goal;
        play.Situation = model.Situation;
        play.Yards = model.Yards;
        play.OffensiveFormation = model.OffensiveFormation;
        play.OffensivePlay = model.OffensivePlay;
        play.DefensiveFormation = model.DefensiveFormation;
        play.DefensivePlay = model.DefensivePlay;
        play.Result = model.Result;
        play.Penalty = model.Penalty;
        play.FirstDown = model.FirstDown;
        play.Touchdown = model.Touchdown;
        play.Safety = model.Safety;
        play.Runner = model.Runner;
        play.Passer = model.Passer;
        play.Target = model.Target;
        play.TargetPosition = model.TargetPosition;
        play.DefensiveTarget = model.DefensiveTarget;
        play.Tackler = model.Tackler;
        play.Interceptor = model.Interceptor;
        play.OfensiveNotes = model.OfensiveNotes;
        play.DefensiveNotes = model.DefensiveNotes;

        await _dbContext.SaveChangesAsync();

        return (play, null);
    }

    public async Task<(PlayDTO?, string?)> Create(long gameId, IFormFile video, string userId)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return (null, "User not found");

        var game = await _dbContext.Games
            .Where(x => x.Id == gameId)
            .Where(x => x.TeamId == user.TeamId)
            .FirstOrDefaultAsync();

        if (game is null)
            return (null, "Game not found");

        var fileId = Guid.NewGuid().ToString();

        var success = await _s3Service.UploadFile(fileId, game.Id.ToString(), video, "video/mp4");

        if (!success)
            return (null, "Failed to upload video");

        var play = new Play
        {
            Game = game,
            FileId = fileId,
        };

        await _dbContext.Plays.AddAsync(play);
        await _dbContext.SaveChangesAsync();

        return (_mapper.Map<PlayDTO>(play), null);
    }

    public async Task<(bool, string?)> Delete(long id, string userId)
    {
        var user = await _dbContext.Users
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return (false, "User not found");

        var play = await _dbContext.Plays
            .Include(x => x.Game)
            .Where(x => x.Id == id)
            .Where(x => x.Game.TeamId == user.TeamId)
            .FirstOrDefaultAsync();

        if (play is null)
            return (false, "Play not found");

        return await Delete(play);
    }

    public async Task<(bool, string?)> Delete(Play play)
    {
        var success = await _s3Service.DeleteFile(play.FileId, play.GameId.ToString());

        if (!success)
            return (false, "Failed to remove video");

        _dbContext.Plays.Remove(play);
        await _dbContext.SaveChangesAsync();

        return (true, null);
    }
}