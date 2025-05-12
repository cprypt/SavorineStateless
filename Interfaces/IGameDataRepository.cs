// File: Interfaces/IGameDataRepository.cs
using Savorine.AsyncServer.Models;

namespace Savorine.AsyncServer.Interfaces
{
    public interface IGameDataRepository
    {
        Task<GameData> SaveAsync(GameData data);
        Task<IEnumerable<GameData>> LoadByUserAsync(int userId);
    }
}