using Microsoft.AspNetCore.Mvc;
using Trophy.Domain;
using Trophy.Service;

namespace Trophy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        [Route("")]
        public async Task AddGameAsync([FromBody] GameDTO game)
        {
            await _gameService.AddGameAsync(game);
        }

        [HttpGet]
        [Route("")]
        public async Task<List<GameDTO>> GetGamesAsync()
        {
            return await _gameService.GetGamesAsync();
        }

        [HttpGet]
        [Route("trophy/holder")]
        public async Task<TrophyHolderDTO> GetTrophyHolderAsync()
        {
            return await _gameService.GetTrophyHolderAsync();
        }
    }
}