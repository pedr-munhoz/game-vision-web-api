using game_vision_web_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace game_vision_web_api.Controllers;

[ApiController]
[Route("api/game")]
[Authorize]
public class GameController(GameService gameService, PlayService playService) : ControllerBase
{
    private readonly GameService _gameService = gameService;
    private readonly PlayService _playService = playService;

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var (result, error) = await _gameService.GetById(id);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}/play")]
    public async Task<IActionResult> GetPlays([FromRoute] long id)
    {
        var (result, error) = await _playService.GetByGameId(id);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }

    [HttpPost]
    [Route("{id}/play")]
    public async Task<IActionResult> PostPlay([FromRoute] long id, IFormFile video)
    {
        if (video == null || video.Length == 0)
            return BadRequest("No file uploaded");

        // Check if the file is in MP4 format
        if (video.ContentType != "video/mp4")
            return BadRequest("Only MP4 files are allowed");

        var (result, error) = await _playService.Create(id, video);

        if (result is null || error is not null)
            return UnprocessableEntity(error);

        return Ok(result);
    }
}