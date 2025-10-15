using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get() =>
            Ok(await _service.ListarEncomendasAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var encomenda = await _service.BuscarEncomendaAsync(id);
            if (encomenda == null) return NotFound();
            return Ok(encomenda);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Encomenda encomenda)
        {
            await _service.CriarEncomendaAsync(encomenda);
            return CreatedAtAction(nameof(Get), new { id = encomenda.IdEncomenda }, encomenda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Encomenda encomenda)
        {
            if (id != encomenda.IdEncomenda) return BadRequest();
            await _service.AtualizarEncomendaAsync(encomenda);
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
