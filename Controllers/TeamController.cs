using System.Security.Claims;
using game_vision_web_api.Models.ViewModels;
using game_vision_web_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace game_vision_web_api.Controllers;

[ApiController]
[Route("api/team")]
[Authorize]
public class TeamController(TeamService teamService, GameService gameService) : ControllerBase
{
    private readonly TeamService _teamService = teamService;
    private readonly GameService _gameService = gameService;

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return Unauthorized();

        var (result, error) = await _teamService.GetByUser(userId);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpPost]
    [Route("game")]
    public async Task<IActionResult> PostGame([FromBody] GameViewModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return Unauthorized();

        var (entity, error) = await _gameService.Create(model, userId);

        if (entity is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(entity);
    }

    [HttpGet]
    [Route("game")]
    public async Task<IActionResult> GetGames()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return Unauthorized();

        var (result, error) = await _gameService.GetByUserTeam(userId);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }
}
