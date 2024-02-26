using game_vision_web_api.Models.ViewModels;
using game_vision_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace game_vision_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController(GameService gameService) : ControllerBase
    {
        private readonly GameService _gameService = gameService;

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create([FromBody] GameViewModel model)
        {
            var entity = await _gameService.Create(model);
            return Ok(entity);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _gameService.GetAll();

            return Ok(entities);
        }
    }
}