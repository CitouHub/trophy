using Microsoft.EntityFrameworkCore;
using Trophy.Data;

namespace Trophy.Service
{
    public interface IPlayerService
    {
        Task<List<string>> GetPlayersAsync();
        Task AddPlayerAsync(string name);
    }

    public class PlayerService : IPlayerService
    {
        private readonly TrophyDbContext _context;

        public PlayerService(TrophyDbContext context)
        {
            _context = context;
        }

        public async Task AddPlayerAsync(string name)
        {
            await _context.Players
                .AddAsync(new Player { Name = name });
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetPlayersAsync()
        {
            return await _context.Players
                .Select(_ => _.Name)
                .OrderBy(_ => _)
                .ToListAsync();
        }
    }
}