using Microsoft.EntityFrameworkCore;
using Trophy.Data;
using Trophy.Domain;

namespace Trophy.Service
{
    public interface IRankingService
    {
        Task<List<RankingDTO>> GetByWinCountAsync();
        Task<List<RankingDTO>> GetByWinRateAsync();
        Task<List<RankingDTO>> GetByWinStreakAsync();
        Task<List<RankingDTO>> GetByWinSizeAsync();
        Task<List<RankingDTO>> GetByTrophyTimeAsync(DateTime untilTime);
        Task<List<RankingDTO>> GetByPointCountAsync();
    }

    public class RankingService : IRankingService
    {
        private readonly TrophyDbContext _context;

        public RankingService(TrophyDbContext context)
        {
            _context = context;
        }

        public async Task<List<RankingDTO>> GetByWinCountAsync()
        {
            var playerResult = await _context.PlayerResults.ToListAsync();
            var players = await _context.Players.ToListAsync();
            return players
                .Select(p => new RankingDTO()
                {
                    Player = p.Name,
                    Value = playerResult.Count(r => r.PlayerId == p.Id && r.Win),
                    Unit = "Times"
                }).OrderByDescending(_ => _.Value)
                .ThenBy(_ => _.Player)
                .ToList();
        }

        public async Task<List<RankingDTO>> GetByWinRateAsync()
        {
            var winRate = await _context.PlayerResults
                .GroupBy(_ => _.Player.Id)
                .Select(_ => new 
                {
                    Player = _.Key,
                    Rate = (decimal)Math.Round((_.Count(_ => _.Win) / (decimal)_.Count()) * 100, 2),
                }).ToDictionaryAsync(_ => _.Player, _ => _.Rate);
            
            var players = await _context.Players.ToListAsync();
            return players.Select(p => new RankingDTO()
                {
                    Player = p.Name,
                    Value = winRate.ContainsKey(p.Id) ? winRate[p.Id] : 0,
                    Unit = "%"
                }).OrderByDescending(_ => _.Value)
                .ThenBy(_ => _.Player)
                .ToList();
        }

        public async Task<List<RankingDTO>> GetByWinSizeAsync()
        {
            var winSize = (await _context.PlayerResults
                .GroupBy(_ => _.GameId)
                .Select(_ => new
                {
                    Player = _.First(_ => _.Win).PlayerId,
                    Size = Math.Abs(_.ToList()[0].Score - _.ToList()[1].Score)
                })
                .ToArrayAsync())
                .GroupBy(_ => _.Player)
                .ToDictionary(_ => _.Key, _ => _.Max(x => x.Size));

            var players = await _context.Players.ToListAsync();
            return players
                .Select(p => new RankingDTO()
                {
                    Player = p.Name,
                    Value = winSize.ContainsKey(p.Id) ? winSize[p.Id] : 0,
                    Unit = "Points"
                })
                .OrderByDescending(_ => _.Value)
                .ThenBy(_ => _.Player)
                .ToList();
        }

        public async Task<List<RankingDTO>> GetByPointCountAsync()
        {
            return await _context.Players
                .Select(_ => new RankingDTO()
                {
                    Player = _.Name,
                    Value = _.PlayerResults.Sum(x => x.Score),
                    Unit = "Points"
                })
                .OrderByDescending(_ => _.Value)
                .ThenBy(_ => _.Player)
                .ToListAsync();
        }

        public async Task<List<RankingDTO>> GetByWinStreakAsync()
        {
            var games = await _context.Games
                .Include(_ => _.PlayerResults)
                .ThenInclude(_ => _.Player)
                .OrderBy(_ => _.MatchDate)
                .ToListAsync();
            var rankings = await _context.Players
                .Select(_ => new RankingDTO()
                {
                    Player = _.Name,
                    Value = 0,
                    Unit = "Wins"
                }).ToListAsync();

            var streaks = games.SelectMany(_ => _.PlayerResults)
                .GroupBy(_ => _.Player.Name)
                .ToDictionary(_ => _.Key, _ => 0);

            foreach (var game in games)
            {
                foreach (var playerResult in game.PlayerResults)
                {
                    if (playerResult.Win == true)
                    {
                        streaks[playerResult.Player.Name]++;
                    }
                    else
                    {
                        StreakBroken(rankings, streaks, playerResult.Player.Name);
                    }
                }
            }

            foreach (var streak in streaks)
            {
                StreakBroken(rankings, streaks, streak.Key);
            }

            return rankings
                .OrderByDescending(_ => _.Value)
                .ThenBy(_ => _.Player)
                .ToList();
        }

        private void StreakBroken(List<RankingDTO> rankings, Dictionary<string, int> streaks, string playerName)
        {
            var playerStreak = rankings.First(_ => _.Player == playerName);
            if (streaks[playerName] > playerStreak.Value)
            {
                playerStreak.Value = streaks[playerName];
            }
            streaks[playerName] = 0;
        }

        public async Task<List<RankingDTO>> GetByTrophyTimeAsync(DateTime untilTime)
        {
            var games = await _context.Games
                .Include(_ => _.PlayerResults)
                .ThenInclude(_ => _.Player)
                .OrderBy(_ => _.MatchDate)
                .ToListAsync();
            var rankings = await _context.Players
                .Select(_ => new RankingDTO()
                {
                    Player = _.Name,
                    Value = 0,
                    Unit = "Days"
                }).ToListAsync();

            for(var i = 0; i < games.Count() - 1; i++)
            {
                var game = games[i];
                var nextGame = games[i + 1];
                var holdingPlayer = game.PlayerResults.First(_ => _.Win).Player.Name;
                PassTrophy(rankings, game.MatchDate, nextGame.MatchDate, holdingPlayer);
            }

            var lastGame = games.LastOrDefault();
            if(lastGame != null)
            {
                var currentHoldingPlayer = lastGame.PlayerResults.First(_ => _.Win).Player.Name;
                PassTrophy(rankings, lastGame.MatchDate, untilTime, currentHoldingPlayer);
            }

            return rankings
                .OrderByDescending(_ => _.Value)
                .ThenBy(_ => _.Player)
                .ToList();
        }

        private void PassTrophy(List<RankingDTO> rankings, DateTime fromMatchDate, DateTime toMatchDate, string holdingPlayer)
        {
            var days = (int)Math.Round((toMatchDate - fromMatchDate).TotalDays);
            var ranking = rankings.First(_ => _.Player == holdingPlayer);
            ranking.Value = ranking.Value + days;
        }
    }
}