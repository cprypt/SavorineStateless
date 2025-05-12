// File: Data/GameDbContext.cs
using Microsoft.EntityFrameworkCore;
using Savorine.AsyncServer.Models;

namespace Savorine.AsyncServer.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users => Set<User>();
        public DbSet<GameData> GameDatas => Set<GameData>();
    }
}