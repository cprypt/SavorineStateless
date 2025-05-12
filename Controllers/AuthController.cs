// File: Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Savorine.AsyncServer.DTOs;
using Savorine.AsyncServer.Interfaces;

namespace Savorine.AsyncServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service) => _service = service;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var success = await _service.RegisterAsync(dto);
            if (!success) return Conflict(new { message = "Username already exists." });
            return CreatedAtAction(null, new { dto.Username });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _service.AuthenticateAsync(dto);
            if (token == null) return Unauthorized(new { message = "Invalid credentials." });
            return Ok(new { token });
        }
    }
}