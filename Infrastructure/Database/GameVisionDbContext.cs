using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace game_vision_web_api.Infrastructure.Database;

public class GameVisionDbContext : DbContext
{
    public GameVisionDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Play> Plays { get; set; } = null!;
}