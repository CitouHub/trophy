using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trophy.Data;
using Trophy.Domain;

namespace Trophy.Service
{
    public interface IGameService
    {
        Task AddGameAsync(GameDTO gameDto);
        Task<List<GameDTO>> GetGamesAsync();
        Task<string> GetTrophyHolderAsync();
    }

    public class GameService : IGameService
    {
        private readonly TrophyDbContext _context;
        private readonly IMapper _mapper;

        public GameService(TrophyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddGameAsync(GameDTO gameDto)
        {
            var game = _mapper.Map<Game>(gameDto);
            game.PlayerResults.OrderByDescending(_ => _.Score).ToList()[0].Win = true;
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GameDTO>> GetGamesAsync()
        {
            var games = await _context.Games
                .OrderByDescending(_ => _.MatchDate)
                .ToArrayAsync();
            return _mapper.Map<List<GameDTO>>(games);
        }

        public async Task<string> GetTrophyHolderAsync()
        {
            return await _context.Games
                .OrderByDescending(_ => _.MatchDate)
                .Select(_ => _.PlayerResults.First(_ => _.Win).Player.Name)
                .FirstAsync();
        }
    }
}