using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.DTOs;
using PortariaLight.Application.Services;

namespace PortariaLight.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EncomendaController : ControllerBase
    {
        private readonly IEncomendaService _service;

        public EncomendaController(IEncomendaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var encomendas = await _service.ListarEncomendasAsync();
            return Ok(encomendas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var encomenda = await _service.BuscarEncomendaAsync(id);
            if (encomenda == null) return NotFound($"Encomenda com ID {id} não encontrada.");
            return Ok(encomenda);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EncomendaDTO dto)
        {
            await _service.CriarEncomendaAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.IdEncomenda }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EncomendaDTO dto)
        {
            if (id != dto.IdEncomenda)
                return BadRequest("O ID informado não corresponde à encomenda.");

            await _service.AtualizarEncomendaAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverEncomendaAsync(id);
            return NoContent();
        }
    }
}
