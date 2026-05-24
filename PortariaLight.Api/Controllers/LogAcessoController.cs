using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortariaLight.Domain.Repositories;

namespace PortariaLight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class LogAcessoController(ILogAcessoRepository logRepo) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUltimos([FromQuery] int quantidade = 100)
        => Ok(await logRepo.GetUltimosAsync(quantidade));

    [HttpGet("endpoint")]
    public async Task<IActionResult> GetPorEndpoint([FromQuery] string endpoint)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
            return BadRequest(new { message = "Informe o endpoint." });
        return Ok(await logRepo.GetPorEndpointAsync(endpoint));
    }
}