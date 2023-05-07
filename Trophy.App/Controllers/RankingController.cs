using Microsoft.AspNetCore.Mvc;
using Trophy.Domain;
using Trophy.Service;

namespace Trophy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : ControllerBase
    {
        private IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        [HttpGet]
        [Route("by/winstreak")]
        public async Task<List<RankingDTO>> GetByWinStreakAsync()
        {
            return await _rankingService.GetByWinStreakAsync();
        }

        [HttpGet]
        [Route("by/wincount")]
        public async Task<List<RankingDTO>> GetByWinCountAsync()
        {
            return await _rankingService.GetByWinCountAsync();
        }

        [HttpGet]
        [Route("by/winrate")]
        public async Task<List<RankingDTO>> GetByWinRateAsync()
        {
            return await _rankingService.GetByWinRateAsync();
        }

        [HttpGet]
        [Route("by/winsize")]
        public async Task<List<RankingDTO>> GetByWinSizeAsync()
        {
            return await _rankingService.GetByWinSizeAsync();
        }

        [HttpGet]
        [Route("by/trophytime")]
        public async Task<List<RankingDTO>> GetByTrophyTimeAsync()
        {
            return await _rankingService.GetByTrophyTimeAsync(DateTime.UtcNow);
        }

        [HttpGet]
        [Route("by/pointcount")]
        public async Task<List<RankingDTO>> GetByPointCountAsync()
        {
            return await _rankingService.GetByPointCountAsync();
        }
    }
}