using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;

namespace PortariaLight.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartamentoController : ControllerBase
    {
        private readonly IApartamentoService _apartamentoService;

        public ApartamentoController(IApartamentoService apartamentoService)
        {
            _apartamentoService = apartamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var apartamentos = await _apartamentoService.GetAllApartamentosAsync();
            return Ok(apartamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var apartamento = await _apartamentoService.GetApartamentoByIdAsync(id);
            if (apartamento == null)
                return NotFound();
            return Ok(apartamento);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Apartamento apartamento)
        {
            var created = await _apartamentoService.CreateApartamentoAsync(apartamento);
            return CreatedAtAction(nameof(GetById), new { id = created.IdApartamento }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Apartamento apartamento)
        {
            if (id != apartamento.IdApartamento)
                return BadRequest();

            var updated = await _apartamentoService.UpdateApartamentoAsync(apartamento);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _apartamentoService.DeleteApartamentoAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}