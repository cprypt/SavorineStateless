// Services/IPlayerService.cs
public interface IPlayerService
{
    Task<Player> AuthenticateAsync(string username, string password);
    Task<Player> RegisterAsync(string username, string password);
}