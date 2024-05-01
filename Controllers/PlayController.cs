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
    [Route("game/{gameId}")]
    public async Task<IActionResult> GetPlays([FromRoute] long gameId)
    {
        var entities = await _playService.Get(gameId);

        return Ok(entities);
    }

    [HttpPost]
    [Route("game/{gameId}")]
    public async Task<IActionResult> CreatePlay([FromRoute] long gameId, IFormFile video)
    {
        if (video == null || video.Length == 0)
            return BadRequest("No file uploaded");

        // Check if the file is in MP4 format
        if (video.ContentType != "video/mp4")
            return BadRequest("Only MP4 files are allowed");

        var (play, error) = await _playService.Create(gameId, video);

        if (play is null || error is not null)
            return UnprocessableEntity(error);

        return Ok($"https://d1x95g1lk7jxvh.cloudfront.net/{play.GameId}/{play.FileId}");
    }

    [HttpPost]
    [Route("game/{gameId}/batch")]
    public async Task<IActionResult> CreatePlays([FromRoute] long gameId, IFormFileCollection videos)
    {
        if (videos.Any(video => video == null || video.Length == 0))
            return BadRequest("No file uploaded");

        if (videos.Any(video => video.ContentType != "video/mp4"))
            return BadRequest("Only MP4 files are allowed");

        var result = await _playService.Create(gameId, videos);

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdatePlay([FromRoute] long id, [FromBody] PlayViewModel model)
    {
        var entity = await _playService.Update(id, model);

        return Ok(entity);
    }
}
