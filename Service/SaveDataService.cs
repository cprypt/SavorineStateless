using Microsoft.EntityFrameworkCore;            // ToListAsync

// Services/SaveDataService.cs
public class SaveDataService : ISaveDataService
{
    private readonly GameDbContext _db;
    public SaveDataService(GameDbContext db) => _db = db;

    public async Task SaveAsync(SaveDataDto dto)
    {
        var entity = new SaveData
        {
            PlayerId = dto.PlayerId,
            DataJson = dto.DataJson
        };
        _db.SaveDatas.Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<SaveData>> LoadAsync(int playerId)
    {
        return await _db.SaveDatas
            .Where(s => s.PlayerId == playerId)
            .OrderByDescending(s => s.SavedAt)
            .ToListAsync();
    }
}