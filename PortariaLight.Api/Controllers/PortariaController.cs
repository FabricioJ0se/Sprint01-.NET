using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.DTOs;
using PortariaLight.Application.Services;

namespace PortariaLight.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortariaController : ControllerBase
    {
        private readonly IPortariaService _service;

        public PortariaController(IPortariaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var portarias = await _service.ListarPortariasAsync();
            return Ok(portarias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var portaria = await _service.BuscarPortariaAsync(id);
            if (portaria == null) return NotFound($"Portaria com ID {id} não encontrada.");
            return Ok(portaria);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PortariaDTO dto)
        {
            await _service.CriarPortariaAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.IdPortaria }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PortariaDTO dto)
        {
            if (id != dto.IdPortaria)
                return BadRequest("O ID informado não corresponde à portaria.");

            await _service.AtualizarPortariaAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverPortariaAsync(id);
            return NoContent();
        }
    }
}
