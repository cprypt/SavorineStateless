// Services/ISaveDataService.cs
public interface ISaveDataService
{
    Task SaveAsync(SaveDataDto dto);
    Task<IEnumerable<SaveData>> LoadAsync(int playerId);
}