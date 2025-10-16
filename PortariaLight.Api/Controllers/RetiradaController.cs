using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.DTOs;
using PortariaLight.Application.Services;

namespace PortariaLight.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetiradaController : ControllerBase
    {
        private readonly IRetiradaService _service;

        public RetiradaController(IRetiradaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var retiradas = await _service.ListarRetiradasAsync();
            return Ok(retiradas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var retirada = await _service.BuscarRetiradaAsync(id);
            if (retirada == null) return NotFound($"Retirada com ID {id} não encontrada.");
            return Ok(retirada);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RetiradaDTO dto)
        {
            await _service.CriarRetiradaAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.IdRetirada }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RetiradaDTO dto)
        {
            if (id != dto.IdRetirada)
                return BadRequest("O ID informado não corresponde à retirada.");

            await _service.AtualizarRetiradaAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverRetiradaAsync(id);
            return NoContent();
        }
    }
}
