// Models/SaveData.cs
public class SaveData
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public string DataJson { get; set; }
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    public Player Player { get; set; }
}