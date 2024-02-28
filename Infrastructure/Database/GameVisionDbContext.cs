using game_vision_web_api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Infrastructure.Database;

public class GameVisionDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Play> Plays { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Play>()
                    .HasOne(p => p.Game)
                    .WithMany(p => p.Plays)
                    .HasForeignKey(p => p.GameId);
    }
}