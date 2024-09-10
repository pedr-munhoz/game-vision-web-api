using AutoMapper;
using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.DTOs;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public class GameService(GameVisionDbContext dbContext, IMapper mapper)
{
    private readonly GameVisionDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<(GameDTO?, string?)> Create(GameViewModel model, string userId)
    {
        var user = await _dbContext.Users
            .Include(x => x.Team)
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return (null, "User not found");

        if (user.Team is null)
            return (null, "Team not found");

        var game = new Game
        {
            Name = model.Name,
            Team = user.Team,
        };

        await _dbContext.Games.AddAsync(game);
        await _dbContext.SaveChangesAsync();

        return (_mapper.Map<GameDTO>(game), null);
    }

    public async Task<(List<GameDTO>?, string?)> GetByUserTeam(string userId)
    {
        var user = await _dbContext.Users
            .Include(x => x.Team)
#nullable disable
            .ThenInclude(x => x.Games)
#nullable restore
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return (null, "User not found");

        if (user.Team is null)
            return (null, "Team not found");

        return (user.Team.Games.Select(_mapper.Map<GameDTO>).ToList(), null);
    }

    public async Task<(GameDTO?, string?)> GetById(long id, string userId)
    {
        var user = await _dbContext.Users
            .Include(x => x.Team)
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return (null, "User not found");

        var game = await _dbContext.Games
            .Where(x => x.Id == id)
            .Where(x => x.TeamId == user.TeamId)
            .FirstOrDefaultAsync();

        if (game is null)
            return (null, "Game not found");

        return (_mapper.Map<GameDTO>(game), null);
    }
}