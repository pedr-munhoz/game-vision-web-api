using game_vision_web_api.Models.ViewModels;
using game_vision_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace game_vision_web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayController(PlayService playService) : ControllerBase
{
    private readonly PlayService _playService = playService;

    [HttpGet]
    [Route("{gameId}")]
    public async Task<IActionResult> GetPlays([FromRoute] long gameId)
    {
        var entities = await _playService.Get(gameId);

        return Ok(entities);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdatePlay([FromRoute] long id, [FromBody] PlayViewModel model)
    {
        var entity = await _playService.Update(id, model);

        return Ok(entity);
    }

    [HttpPost]
    [Route("load")]
    public async Task<IActionResult> LoadPlays([FromQuery] string folderId, long gameId)
    {
        var count = await _playService.Load(gameId, folderId);

        return Ok(count);
    }
}