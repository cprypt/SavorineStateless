// File: Services/GameDataService.cs
using Savorine.AsyncServer.DTOs;
using Savorine.AsyncServer.Interfaces;
using Savorine.AsyncServer.Models;

namespace Savorine.AsyncServer.Services
{
    public class GameDataService : IGameDataService
    {
        private readonly IGameDataRepository _repo;
        public GameDataService(IGameDataRepository repo) => _repo = repo;

        public async Task SaveAsync(int userId, GameDataDto dto)
        {
            var data = new GameData
            {
                UserId = userId,
                PayloadJson = dto.PayloadJson,
                SavedAt = DateTime.UtcNow
            };
            await _repo.SaveAsync(data);
        }

        public async Task<IEnumerable<GameData>> LoadAsync(int userId) =>
            await _repo.LoadByUserAsync(userId);
    }
}