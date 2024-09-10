using game_vision_web_api.Models.ViewModels;
using game_vision_web_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace game_vision_web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AdminController(TeamService teamService, UserService userService) : ControllerBase
{
    private readonly TeamService _teamService = teamService;
    private readonly UserService _userService = userService;

    [HttpGet]
    [Route("user")]
    public async Task<IActionResult> GetUsers()
    {
        var (result, error) = await _userService.Get();

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpPost]
    [Route("team")]
    public async Task<IActionResult> PostTeam([FromBody] TeamViewModel model)
    {
        var (result, error) = await _teamService.Create(model);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpGet]
    [Route("team")]
    public async Task<IActionResult> GetTeams()
    {
        var (result, error) = await _teamService.Get();

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpPost]
    [Route("team/{teamPrefix}/user/{userId}")]
    public async Task<IActionResult> LinkUserToTeam([FromRoute] string teamPrefix, [FromRoute] string userId)
    {
        var (result, error) = await _teamService.LinkUser(teamPrefix, userId);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }
}
