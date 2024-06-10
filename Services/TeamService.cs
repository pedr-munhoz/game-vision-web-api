using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.DTOs;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public class TeamService(GameVisionDbContext dbContext)
{
    private readonly GameVisionDbContext _dbContext = dbContext;

    public async Task<(TeamDTO?, string?)> Create(TeamViewModel model)
    {
        var entity = new Team
        {
            Name = model.Name,
            Prefix = Guid.NewGuid().ToString(),
        };

        await _dbContext.Teams.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return (entity, null);
    }

    public async Task<(List<TeamDTO>, string?)> Get()
    {
        var entities = await _dbContext.Teams.ToListAsync();

        return (entities, null);
    }

    public async Task<(TeamDTO?, string?)> Get(long id)
    {
        var entity = await _dbContext.Teams.Where(x => x.Id == id).FirstOrDefaultAsync();

        return (entity, null);
    }
}
