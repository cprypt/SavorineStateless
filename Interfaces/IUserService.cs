// File: Interfaces/IUserService.cs
using Savorine.AsyncServer.DTOs;

namespace Savorine.AsyncServer.Interfaces
{
    public interface IUserService
    {
        Task<string?> AuthenticateAsync(LoginDto dto);
        Task<bool> RegisterAsync(RegisterDto dto);
    }
}