// File: Interfaces/IGameDataService.cs
using Savorine.AsyncServer.DTOs;
using Savorine.AsyncServer.Models;

namespace Savorine.AsyncServer.Interfaces
{
    public interface IGameDataService
    {
        Task SaveAsync(int userId, GameDataDto dto);
        Task<IEnumerable<GameData>> LoadAsync(int userId);
    }
}