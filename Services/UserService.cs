using AutoMapper;
using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public class UserService(GameVisionDbContext dbContext, IMapper mapper)
{
    private readonly GameVisionDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<(List<UserDTO>, string?)> Get()
    {
        var entities = await _dbContext.Users.ToListAsync();

        return (entities.Select(_mapper.Map<UserDTO>).ToList(), null);
    }
}
