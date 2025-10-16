using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.DTOs;
using PortariaLight.Application.Services;
using System.Threading.Tasks;

namespace PortariaLight.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoradorController : ControllerBase
    {
        private readonly IMoradorService _service;

        public MoradorController(IMoradorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.ListarMoradoresAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var morador = await _service.BuscarMoradorAsync(id);
            if (morador == null) return NotFound();
            return Ok(morador);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MoradorDTO dto)
        {
            await _service.CriarMoradorAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.IdMorador }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MoradorDTO dto)
        {
            if (id != dto.IdMorador) return BadRequest();
            await _service.AtualizarMoradorAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverMoradorAsync(id);
            return NoContent();
        }
    }
}
