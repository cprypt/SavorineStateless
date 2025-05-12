// File: Controllers/GameDataController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Savorine.AsyncServer.DTOs;
using Savorine.AsyncServer.Interfaces;
using System.Security.Claims;

namespace Savorine.AsyncServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GameDataController : ControllerBase
    {
        private readonly IGameDataService _service;

        public GameDataController(IGameDataService service) => _service = service;

        [HttpPost("save")]
        public async Task<IActionResult> Save([FromBody] GameDataDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _service.SaveAsync(userId, dto);
            return Ok();
        }

        [HttpGet("load")]
        public async Task<IActionResult> Load()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var data = await _service.LoadAsync(userId);
            return Ok(data);
        }
    }
}
