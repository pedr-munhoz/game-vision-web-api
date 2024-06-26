using AutoMapper;
using game_vision_web_api.Infrastructure.Database;
using game_vision_web_api.Models.Entities;
using game_vision_web_api.Models.MapperProfiles;
using game_vision_web_api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration["GameVisionDbConnectionString"];

builder.Services.AddDbContext<GameVisionDbContext>(options =>
    options.UseNpgsql(connectionString)
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<GameVisionDbContext>()
    .AddApiEndpoints();

builder.Services.AddTransient<TeamService>();
builder.Services.AddTransient<GameService>();
builder.Services.AddTransient<PlayService>();
builder.Services.AddTransient<S3Service>();

builder.Services.AddAutoMapper(p =>
    _ = new MapperConfiguration(q =>
    {
        p.AddProfile(new EntityToDtoProfile());
    })
);

var app = builder.Build();

app.UseCors(policy => policy
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
                .WithExposedHeaders("Content-Disposition"));

DatabaseManagementService.MigrationInitialisation(app);

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
