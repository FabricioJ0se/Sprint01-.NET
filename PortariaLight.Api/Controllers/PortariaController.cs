using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get() =>
            Ok(await _service.ListarPortariasAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var portaria = await _service.BuscarPortariaAsync(id);
            if (portaria == null)
                return NotFound();
            return Ok(portaria);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Portaria portaria)
        {
            if (portaria == null)
                return BadRequest("Dados da portaria inválidos.");

            await _service.CriarPortariaAsync(portaria);
            return CreatedAtAction(nameof(Get), new { id = portaria.IdPortaria }, portaria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Portaria portaria)
        {
            if (portaria == null || id != portaria.IdPortaria)
                return BadRequest("ID inconsistente ou dados inválidos.");

            await _service.AtualizarPortariaAsync(portaria);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existente = await _service.BuscarPortariaAsync(id);
            if (existente == null)
                return NotFound();

            await _service.RemoverPortariaAsync(id);
            return NoContent();
        }
    }
}