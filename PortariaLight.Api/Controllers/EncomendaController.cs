using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;

namespace PortariaLight.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncomendaController : ControllerBase
    {
        private readonly IEncomendaService _encomendaService;

        public EncomendaController(IEncomendaService encomendaService)
        {
            _encomendaService = encomendaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var encomendas = await _encomendaService.GetAllEncomendasAsync();
            return Ok(encomendas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var encomenda = await _encomendaService.GetEncomendaByIdAsync(id);
            if (encomenda == null)
                return NotFound();
            return Ok(encomenda);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Encomenda encomenda)
        {
            var created = await _encomendaService.CreateEncomendaAsync(encomenda);
            return CreatedAtAction(nameof(GetById), new { id = created.IdEncomenda }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Encomenda encomenda)
        {
            if (id != encomenda.IdEncomenda)
                return BadRequest();

            var updated = await _encomendaService.UpdateEncomendaAsync(encomenda);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _encomendaService.DeleteEncomendaAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("morador/{moradorId}")]
        public async Task<IActionResult> GetByMorador(int moradorId)
        {
            var encomendas = await _encomendaService.GetEncomendasByMoradorAsync(moradorId);
            return Ok(encomendas);
        }

        [HttpGet("nao-retiradas")]
        public async Task<IActionResult> GetNaoRetiradas()
        {
            var encomendas = await _encomendaService.GetEncomendasNaoRetiradasAsync();
            return Ok(encomendas);
        }
    }
}