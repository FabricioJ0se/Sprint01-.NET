using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;

namespace PortariaLight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MoradorController(IMoradorService moradorService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? nome = null,
        [FromQuery] string sortBy = "IdMorador",
        [FromQuery] bool desc = false)
    {
        var moradores = await moradorService.GetAllMoradoresAsync();

        if (!string.IsNullOrWhiteSpace(nome))
            moradores = moradores.Where(m => m.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));

        moradores = sortBy.ToLower() switch
        {
            "nome" => desc ? moradores.OrderByDescending(m => m.Nome) : moradores.OrderBy(m => m.Nome),
            _ => desc ? moradores.OrderByDescending(m => m.IdMorador) : moradores.OrderBy(m => m.IdMorador)
        };

        var total = moradores.Count();
        var items = moradores.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        return Ok(new
        {
            data = items.Select(m => ComLinks(m, baseUrl)),
            pagination = new { page, pageSize, total, totalPages = (int)Math.Ceiling(total / (double)pageSize) },
            _links = new
            {
                self = $"{baseUrl}/api/morador?page={page}&pageSize={pageSize}",
                next = page * pageSize < total ? $"{baseUrl}/api/morador?page={page + 1}&pageSize={pageSize}" : null,
                prev = page > 1 ? $"{baseUrl}/api/morador?page={page - 1}&pageSize={pageSize}" : null
            }
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var morador = await moradorService.GetMoradorByIdAsync(id);
        if (morador == null) return NotFound();
        return Ok(ComLinks(morador, $"{Request.Scheme}://{Request.Host}"));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(Morador morador)
    {
        var created = await moradorService.CreateMoradorAsync(morador);
        return CreatedAtAction(nameof(GetById), new { id = created.IdMorador },
            ComLinks(created, $"{Request.Scheme}://{Request.Host}"));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, Morador morador)
    {
        if (id != morador.IdMorador) return BadRequest();
        var updated = await moradorService.UpdateMoradorAsync(morador);
        return Ok(ComLinks(updated, $"{Request.Scheme}://{Request.Host}"));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await moradorService.DeleteMoradorAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpGet("apartamento/{apartamentoId}")]
    public async Task<IActionResult> GetByApartamento(int apartamentoId)
    {
        var moradores = await moradorService.GetMoradoresByApartamentoAsync(apartamentoId);
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        return Ok(moradores.Select(m => ComLinks(m, baseUrl)));
    }

    [HttpGet("contato/{contato}")]
    public async Task<IActionResult> GetByContato(string contato)
    {
        var morador = await moradorService.GetMoradorByContatoAsync(contato);
        if (morador == null) return NotFound();
        return Ok(ComLinks(morador, $"{Request.Scheme}://{Request.Host}"));
    }

    private static object ComLinks(Morador m, string baseUrl) => new
    {
        m.IdMorador,
        m.Nome,
        m.Contato,
        m.IdApartamento,
        _links = new[]
        {
            new { rel = "self",        href = $"{baseUrl}/api/morador/{m.IdMorador}",           method = "GET"    },
            new { rel = "update",      href = $"{baseUrl}/api/morador/{m.IdMorador}",           method = "PUT"    },
            new { rel = "delete",      href = $"{baseUrl}/api/morador/{m.IdMorador}",           method = "DELETE" },
            new { rel = "encomendas",  href = $"{baseUrl}/api/encomenda/morador/{m.IdMorador}", method = "GET"    },
            new { rel = "apartamento", href = $"{baseUrl}/api/apartamento/{m.IdApartamento}",   method = "GET"    }
        }
    };
}