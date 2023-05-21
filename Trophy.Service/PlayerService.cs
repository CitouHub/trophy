using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trophy.Data;
using Trophy.Domain;

namespace Trophy.Service
{
    public interface IPlayerService
    {
        Task<List<PlayerDTO>> GetPlayersAsync();
        Task AddPlayerAsync(string name);
    }

    public class PlayerService : IPlayerService
    {
        private readonly TrophyDbContext _context;
        private readonly IMapper _mapper;

        public PlayerService(TrophyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddPlayerAsync(string name)
        {
            await _context.Players
                .AddAsync(new Player { Name = name });
            await _context.SaveChangesAsync();
        }

        public async Task<List<PlayerDTO>> GetPlayersAsync()
        {
            var players = await _context.Players
                .OrderBy(_ => _.Name)
                .ToListAsync();

            return _mapper.Map<List<PlayerDTO>>(players);
        }
    }
}