// Models/GameDbContext.cs
using Microsoft.EntityFrameworkCore;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

    public DbSet<Player> Players { get; set; }
    public DbSet<SaveData> SaveDatas { get; set; }
}