using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;

namespace PortariaLight.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EncomendaController(IEncomendaService encomendaService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? moradorId = null,
        [FromQuery] string sortBy = "DataRecebimento",
        [FromQuery] bool desc = true)
    {
        var encomendas = await encomendaService.GetAllEncomendasAsync();

        if (moradorId.HasValue)
            encomendas = encomendas.Where(e => e.IdMorador == moradorId.Value);

        encomendas = sortBy.ToLower() switch
        {
            "descricao" => desc ? encomendas.OrderByDescending(e => e.Descricao) : encomendas.OrderBy(e => e.Descricao),
            _ => desc ? encomendas.OrderByDescending(e => e.DataRecebimento) : encomendas.OrderBy(e => e.DataRecebimento)
        };

        var total = encomendas.Count();
        var items = encomendas.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        return Ok(new
        {
            data = items.Select(e => ComLinks(e, baseUrl)),
            pagination = new { page, pageSize, total, totalPages = (int)Math.Ceiling(total / (double)pageSize) },
            _links = new
            {
                self = $"{baseUrl}/api/encomenda?page={page}&pageSize={pageSize}",
                next = page * pageSize < total ? $"{baseUrl}/api/encomenda?page={page + 1}&pageSize={pageSize}" : null,
                prev = page > 1 ? $"{baseUrl}/api/encomenda?page={page - 1}&pageSize={pageSize}" : null
            }
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var encomenda = await encomendaService.GetEncomendaByIdAsync(id);
        if (encomenda == null) return NotFound();
        return Ok(ComLinks(encomenda, $"{Request.Scheme}://{Request.Host}"));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Encomenda encomenda)
    {
        var created = await encomendaService.CreateEncomendaAsync(encomenda);
        return CreatedAtAction(nameof(GetById), new { id = created.IdEncomenda },
            ComLinks(created, $"{Request.Scheme}://{Request.Host}"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Encomenda encomenda)
    {
        if (id != encomenda.IdEncomenda) return BadRequest();
        var updated = await encomendaService.UpdateEncomendaAsync(encomenda);
        return Ok(ComLinks(updated, $"{Request.Scheme}://{Request.Host}"));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await encomendaService.DeleteEncomendaAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpGet("morador/{moradorId}")]
    public async Task<IActionResult> GetByMorador(int moradorId)
    {
        var encomendas = await encomendaService.GetEncomendasByMoradorAsync(moradorId);
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        return Ok(encomendas.Select(e => ComLinks(e, baseUrl)));
    }

    [HttpGet("nao-retiradas")]
    public async Task<IActionResult> GetNaoRetiradas()
    {
        var encomendas = await encomendaService.GetEncomendasNaoRetiradasAsync();
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        return Ok(encomendas.Select(e => ComLinks(e, baseUrl)));
    }

    private static object ComLinks(Encomenda e, string baseUrl) => new
    {
        e.IdEncomenda,
        e.Descricao,
        e.DataRecebimento,
        e.IdMorador,
        e.IdRetirada,
        _links = new[]
        {
            new { rel = "self",     href = $"{baseUrl}/api/encomenda/{e.IdEncomenda}", method = "GET"    },
            new { rel = "update",   href = $"{baseUrl}/api/encomenda/{e.IdEncomenda}", method = "PUT"    },
            new { rel = "delete",   href = $"{baseUrl}/api/encomenda/{e.IdEncomenda}", method = "DELETE" },
            new { rel = "morador",  href = $"{baseUrl}/api/morador/{e.IdMorador}",     method = "GET"    },
            new { rel = "retirada", href = $"{baseUrl}/api/retirada/{e.IdRetirada}",   method = "GET"    }
        }
    };
}