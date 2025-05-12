// File: Models/GameData.cs
namespace Savorine.AsyncServer.Models
{
    public class GameData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PayloadJson { get; set; } = string.Empty;
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

        public User? User { get; set; }
    }
}