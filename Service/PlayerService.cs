using Microsoft.EntityFrameworkCore;            // FirstOrDefaultAsync, ToListAsync
using System.Security.Cryptography;             // SHA256
using System.Text;   

// Services/PlayerService.cs
public class PlayerService : IPlayerService
{
    private readonly GameDbContext _db;
    public PlayerService(GameDbContext db) => _db = db;

    public async Task<Player> AuthenticateAsync(string username, string password)
    {
        var hash = ComputeHash(password);
        return await _db.Players.FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == hash);
    }

    public async Task<Player> RegisterAsync(string username, string password)
    {
        var user = new Player { Username = username, PasswordHash = ComputeHash(password) };
        _db.Players.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    private string ComputeHash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}