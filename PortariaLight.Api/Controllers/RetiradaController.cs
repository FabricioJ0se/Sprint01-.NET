using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get() =>
            Ok(await _service.ListarRetiradasAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var retirada = await _service.BuscarRetiradaAsync(id);
            if (retirada == null)
                return NotFound();
            return Ok(retirada);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Retirada retirada)
        {
            if (retirada == null)
                return BadRequest("Dados da retirada inválidos.");

            await _service.CriarRetiradaAsync(retirada);
            return CreatedAtAction(nameof(Get), new { id = retirada.IdRetirada }, retirada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Retirada retirada)
        {
            if (retirada == null || id != retirada.IdRetirada)
                return BadRequest("ID inconsistente ou dados inválidos.");

            await _service.AtualizarRetiradaAsync(retirada);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existente = await _service.BuscarRetiradaAsync(id);
            if (existente == null)
                return NotFound();

            await _service.RemoverRetiradaAsync(id);
            return NoContent();
        }
    }
}