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
        var entity = await _teamService.Create(model);
        return Ok(entity);
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Get()
    {
        var entities = await _teamService.Get();

        return Ok(entities);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var entity = await _teamService.Get(id);

        if (entity == null)
            return NotFound();

        return Ok(entity);
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
        var entities = await _gameService.GetByTeamPrefix(prefix);

        return Ok(entities);
    }
}
