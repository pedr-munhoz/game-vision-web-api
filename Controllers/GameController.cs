using game_vision_web_api.Models.ViewModels;
using game_vision_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace game_vision_web_api.Controllers;

[ApiController]
[Route("api/game")]
public class GameController(GameService gameService, PlayService playService) : ControllerBase
{
    private readonly GameService _gameService = gameService;
    private readonly PlayService _playService = playService;

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var entity = await _gameService.GetById(id);

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    [HttpGet]
    [Route("{id}/play")]
    public async Task<IActionResult> GetPlays([FromRoute] long id)
    {
        var entities = await _playService.GetByGameId(id);

        return Ok(entities);
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

        var (play, error) = await _playService.Create(id, video);

        if (play is null || error is not null)
            return UnprocessableEntity(error);

        return Ok($"https://d1x95g1lk7jxvh.cloudfront.net/{play.GameId}/{play.FileId}");
    }

    [HttpPost]
    [Route("{id}/play/batch")]
    public async Task<IActionResult> PostPlays([FromRoute] long id, IFormFileCollection videos)
    {
        if (videos.Any(video => video == null || video.Length == 0))
            return BadRequest("No file uploaded");

        if (videos.Any(video => video.ContentType != "video/mp4"))
            return BadRequest("Only MP4 files are allowed");

        var result = await _playService.Create(id, videos);

        return Ok(result);
    }
}