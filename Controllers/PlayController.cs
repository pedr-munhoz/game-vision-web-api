using game_vision_web_api.Models.ViewModels;
using game_vision_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace game_vision_web_api.Controllers;

[ApiController]
[Route("api/play")]
public class PlayController(PlayService playService) : ControllerBase
{
    private readonly PlayService _playService = playService;

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdatePlay([FromRoute] long id, [FromBody] PlayViewModel model)
    {
        var entity = await _playService.Update(id, model);

        return Ok(entity);
    }
}
