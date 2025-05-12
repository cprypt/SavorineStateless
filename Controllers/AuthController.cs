// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly IConfiguration _config;

    public AuthController(IPlayerService playerService, IConfiguration config)
    {
        _playerService = playerService;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] SaveDataDto dto)
    {
        var user = await _playerService.RegisterAsync(dto.PlayerId.ToString(), dto.DataJson);
        return Ok(new { user.Id, user.Username });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] SaveDataDto dto)
    {
        var user = await _playerService.AuthenticateAsync(dto.PlayerId.ToString(), dto.DataJson);
        if (user == null) return Unauthorized();

        var token = GenerateJwt(user);
        return Ok(new { token });
    }

    private string GenerateJwt(Player user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddHours(2);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}