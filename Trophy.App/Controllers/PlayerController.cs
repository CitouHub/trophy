using Microsoft.AspNetCore.Mvc;
using Trophy.Domain;
using Trophy.Service;

namespace Trophy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost]
        [Route("{name}")]
        public async Task AddPlayerAsync(string name)
        {
            await _playerService.AddPlayerAsync(name);
        }

        [HttpGet]
        [Route("")]
        public async Task<List<PlayerDTO>> GetPlayersAsync()
        {
            return await _playerService.GetPlayersAsync();
        }
    }
}