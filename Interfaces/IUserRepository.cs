// File: Interfaces/IUserRepository.cs
using Savorine.AsyncServer.Models;

namespace Savorine.AsyncServer.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User user);
    }
}