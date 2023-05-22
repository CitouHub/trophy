using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Trophy.Data;
using Trophy.Domain;

namespace Trophy.Service
{
    public interface IPlayerService
    {
        Task<List<PlayerDTO>> GetPlayersAsync();
        Task<PlayerDTO?> AddPlayerAsync(string name);
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

        public async Task<PlayerDTO?> AddPlayerAsync(string name)
        {
            if(!await _context.Players.AnyAsync(_ => _.Name == name)) 
            {
                var player = new Player { Name = name };
                await _context.Players.AddAsync(player);
                await _context.SaveChangesAsync();

                return _mapper.Map<PlayerDTO>(player);
            }

            return null;
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