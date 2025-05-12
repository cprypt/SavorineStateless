// File: Repositories/GameDataRepository.cs
using Microsoft.EntityFrameworkCore;
using Savorine.AsyncServer.Data;
using Savorine.AsyncServer.Interfaces;
using Savorine.AsyncServer.Models;

namespace Savorine.AsyncServer.Repositories
{
    public class GameDataRepository : IGameDataRepository
    {
        private readonly GameDbContext _context;
        public GameDataRepository(GameDbContext context) => _context = context;

        public async Task<GameData> SaveAsync(GameData data)
        {
            _context.GameDatas.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<IEnumerable<GameData>> LoadByUserAsync(int userId) =>
            await _context.GameDatas
                .Where(d => d.UserId == userId)
                .OrderByDescending(d => d.SavedAt)
                .ToListAsync();
    }
}