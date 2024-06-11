using game_vision_web_api.Models.ViewModels;
using game_vision_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace game_vision_web_api.Controllers;

[ApiController]
[Route("api/team")]
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
    [Route("")]
    public async Task<IActionResult> Get()
    {
        var (result, error) = await _teamService.Get();

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpGet]
    [Route("{prefix}")]
    public async Task<IActionResult> Get([FromRoute] string prefix)
    {
        var (result, error) = await _teamService.Get(prefix);

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
}
