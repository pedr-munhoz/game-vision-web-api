using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using game_vision_web_api.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace game_vision_web_api.Services;

public static class DatabaseManagementService
{
    public static void MigrationInitialisation(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        serviceScope.ServiceProvider.GetService<GameVisionDbContext>()?.Database.Migrate();
    }
}
