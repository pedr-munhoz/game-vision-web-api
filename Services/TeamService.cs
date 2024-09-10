using AutoMapper;
using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.DTOs;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public class TeamService(GameVisionDbContext dbContext, IMapper mapper)
{
    private readonly GameVisionDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<(TeamDTO?, string?)> Create(TeamViewModel model)
    {
        var entity = new Team
        {
            Name = model.Name,
            Prefix = Guid.NewGuid().ToString(),
        };

        await _dbContext.Teams.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return (_mapper.Map<TeamDTO>(entity), null);
    }

    public async Task<(List<TeamDTO>, string?)> Get()
    {
        var entities = await _dbContext.Teams.ToListAsync();

        return (entities.Select(_mapper.Map<TeamDTO>).ToList(), null);
    }

    public async Task<(TeamDTO?, string?)> LinkUser(string prefix, string? userId)
    {
        var team = await _dbContext.Teams.Where(x => x.Prefix == prefix).FirstOrDefaultAsync();

        if (team is null)
            return (null, "Team not found");

        var user = await _dbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

        if (user is null)
            return (null, "User not found");

        user.Team = team;
        await _dbContext.SaveChangesAsync();

        return (_mapper.Map<TeamDTO>(team), null);
    }

    public async Task<(TeamDTO?, string?)> GetByUser(string? userId)
    {
        var user = await _dbContext.Users
            .Include(x => x.Team)
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

        if (user is null)
            return (null, "User not found");

        if (user.Team is null)
            return (null, "User does not have a team");

        return (_mapper.Map<TeamDTO>(user.Team), null);
    }
}
