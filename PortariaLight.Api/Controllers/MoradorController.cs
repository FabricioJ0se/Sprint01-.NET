using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;
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

        // GET: api/morador
        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _service.ListarMoradoresAsync());

        // GET: api/morador/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var morador = await _service.BuscarMoradorAsync(id);
            if (morador == null)
                return NotFound();
            return Ok(morador);
        }

        // POST: api/morador
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Morador morador)
        {
            if (morador == null)
                return BadRequest("Dados do morador inválidos.");

            await _service.CriarMoradorAsync(morador);
            return CreatedAtAction(nameof(Get), new { id = morador.IdMorador }, morador);
        }

        // PUT: api/morador/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Morador morador)
        {
            if (morador == null || id != morador.IdMorador)
                return BadRequest("ID inconsistente ou dados inválidos.");

            await _service.AtualizarMoradorAsync(morador);
            return NoContent();
        }

        // DELETE: api/morador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existente = await _service.BuscarMoradorAsync(id);
            if (existente == null)
                return NotFound();

            await _service.RemoverMoradorAsync(id);
            return NoContent();
        }
    }
}
