using Trophy.Data;

namespace Trophy.Test.Helper
{
    public static class GameHelper
    {
        public static async Task<short> AddPlayer(TrophyDbContext context, string name)
        {
            if (!context.Players.Any(p => p.Name == name))
            {
                await context.AddRangeAsync(new Player() { Name = name });
                await context.SaveChangesAsync();
            }

            return context.Players.First(p => p.Name == name).Id;
        }

        public static async Task AddGame(
            TrophyDbContext context, 
            DateTime matchDate, 
            string winningPlayer, 
            string losingPlayer, 
            short winningPlayerScore, 
            short losingPlayerScore)
        {
            await context.AddAsync(new Game()
            {
                Location = "RankingServiceTest",
                MatchDate = matchDate,
                PlayerResults = new List<PlayerResult>()
                {
                    new PlayerResult
                    {
                        PlayerId = await AddPlayer(context, winningPlayer),
                        Score = winningPlayerScore,
                        Win = true
                    },
                    new PlayerResult
                    {
                        PlayerId = await AddPlayer(context, losingPlayer),
                        Score = losingPlayerScore,
                        Win = false
                    },
                }
            });
            await context.SaveChangesAsync();
        }
    }
}
