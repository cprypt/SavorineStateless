// Controllers/SaveDataController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SaveDataController : ControllerBase
{
    private readonly ISaveDataService _svc;
    public SaveDataController(ISaveDataService svc) => _svc = svc;

    [HttpPost]
    public async Task<IActionResult> Save([FromBody] SaveDataDto dto)
    {
        await _svc.SaveAsync(dto);
        return NoContent();
    }

    [HttpGet("{playerId}")]
    public async Task<IActionResult> Load(int playerId)
    {
        var data = await _svc.LoadAsync(playerId);
        return Ok(data);
    }
}