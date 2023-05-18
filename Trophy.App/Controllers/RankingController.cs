using Microsoft.AspNetCore.Mvc;
using Trophy.Common;
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
        [Route("")]
        public async Task<Dictionary<short, List<RankingDTO>>> GetRankingsAsync()
        {
            var tasks = new List<Task<List<RankingDTO>>>
            {
                _rankingService.GetByWinCountAsync(),
                _rankingService.GetByWinRateAsync(),
                _rankingService.GetByWinStreakAsync(),
                _rankingService.GetByWinSizeAsync(),
                _rankingService.GetByTrophyTimeAsync(DateTime.UtcNow),
                _rankingService.GetByPointCountAsync()
            };
            await Task.WhenAll(tasks.ToArray());

            return new Dictionary<short, List<RankingDTO>>()
            {
                { (short)Ranking.ByWinCount, tasks[0].Result },
                { (short)Ranking.ByWinRate, tasks[1].Result },
                { (short)Ranking.ByWinStreak, tasks[2].Result },
                { (short)Ranking.ByWinSize, tasks[3].Result },
                { (short)Ranking.ByTrophyTime, tasks[4].Result },
                { (short)Ranking.ByPointCount, tasks[5].Result },
            };
        }
    }
}