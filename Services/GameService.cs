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

    public async Task<(GameDTO?, string?)> Create(GameViewModel model, string teamPrefix)
    {
        var team = await _dbContext.Teams.Where(x => x.Prefix == teamPrefix).FirstOrDefaultAsync();

        if (team is null)
            return (null, "Team not found");

        var entity = new Game
        {
            Name = model.Name,
            Team = team,
        };

        await _dbContext.Games.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return (_mapper.Map<GameDTO>(entity), null);
    }

    public async Task<(List<GameDTO>?, string?)> GetByTeamPrefix(string teamPrefix)
    {
        var team = await _dbContext.Teams.Where(x => x.Prefix == teamPrefix).FirstOrDefaultAsync();

        if (team is null)
            return (null, "Team not found");

        var entities = await _dbContext.Games.Include(x => x.Team).Where(x => x.Team.Prefix == teamPrefix).ToListAsync();

        return (entities.Select(_mapper.Map<GameDTO>).ToList(), null);
    }

    public async Task<(GameDTO?, string?)> GetById(long id)
    {
        var entity = await _dbContext.Games.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (entity is null)
            return (null, "Game not found");

        return (_mapper.Map<GameDTO>(entity), null);
    }
}