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

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Post([FromBody] TeamViewModel model)
    {
        var (result, error) = await _teamService.Create(model);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        var (result, error) = await _teamService.Get();

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var (result, error) = await _teamService.GetByUser(userId);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpPost]
    [Route("{prefix}/game")]
    public async Task<IActionResult> PostGame([FromRoute] string prefix, [FromBody] GameViewModel model)
    {
        var (entity, error) = await _gameService.Create(model, prefix);

        if (entity is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(entity);
    }

    [HttpGet]
    [Route("{prefix}/game")]
    public async Task<IActionResult> GetGames([FromRoute] string prefix)
    {
        var (result, error) = await _gameService.GetByTeamPrefix(prefix);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpPost]
    [Route("{prefix}/user")]
    public async Task<IActionResult> LinkUser([FromRoute] string prefix)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var (result, error) = await _teamService.LinkUser(prefix, userId);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }
}
