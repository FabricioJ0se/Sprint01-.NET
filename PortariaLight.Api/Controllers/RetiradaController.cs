using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;

namespace PortariaLight.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetiradaController : ControllerBase
    {
        private readonly IRetiradaService _retiradaService;

        public RetiradaController(IRetiradaService retiradaService)
        {
            _retiradaService = retiradaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var retiradas = await _retiradaService.GetAllRetiradasAsync();
            return Ok(retiradas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var retirada = await _retiradaService.GetRetiradaByIdAsync(id);
            if (retirada == null)
                return NotFound();
            return Ok(retirada);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Retirada retirada)
        {
            var created = await _retiradaService.CreateRetiradaAsync(retirada);
            return CreatedAtAction(nameof(GetById), new { id = created.IdRetirada }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Retirada retirada)
        {
            if (id != retirada.IdRetirada)
                return BadRequest();

            var updated = await _retiradaService.UpdateRetiradaAsync(retirada);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _retiradaService.DeleteRetiradaAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("encomenda/{encomendaId}")]
        public async Task<IActionResult> GetByEncomenda(int encomendaId)
        {
            var retiradas = await _retiradaService.GetRetiradasByEncomendaAsync(encomendaId);
            return Ok(retiradas);
        }

        [HttpGet("morador/{moradorId}")]
        public async Task<IActionResult> GetByMorador(int moradorId)
        {
            var retiradas = await _retiradaService.GetRetiradasByMoradorAsync(moradorId);
            return Ok(retiradas);
        }
    }
}