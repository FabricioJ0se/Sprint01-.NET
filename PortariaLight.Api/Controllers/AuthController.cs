using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PortariaLight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest req)
    {
        var (ok, role) = (req.Username, req.Password) switch
        {
            ("admin", "admin123") => (true, "Admin"),
            ("porteiro", "porteiro123") => (true, "Porteiro"),
            _ => (false, "")
        };

        if (!ok) return Unauthorized(new { message = "Credenciais inválidas." });

        var jwt = configuration.GetSection("JwtSettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Secret"]!));
        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: [new Claim(ClaimTypes.Name, req.Username), new Claim(ClaimTypes.Role, role)],
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiresIn = 3600 });
    }
}

public record LoginRequest(string Username, string Password);