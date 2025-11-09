using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;

namespace PortariaLight.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoradorController : ControllerBase
    {
        private readonly IMoradorService _moradorService;

        public MoradorController(IMoradorService moradorService)
        {
            _moradorService = moradorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var moradores = await _moradorService.GetAllMoradoresAsync();
            return Ok(moradores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var morador = await _moradorService.GetMoradorByIdAsync(id);
            if (morador == null)
                return NotFound();
            return Ok(morador);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Morador morador)
        {
            var created = await _moradorService.CreateMoradorAsync(morador);
            return CreatedAtAction(nameof(GetById), new { id = created.IdMorador }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Morador morador)
        {
            if (id != morador.IdMorador)
                return BadRequest();

            var updated = await _moradorService.UpdateMoradorAsync(morador);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _moradorService.DeleteMoradorAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("apartamento/{apartamentoId}")]
        public async Task<IActionResult> GetByApartamento(int apartamentoId)
        {
            var moradores = await _moradorService.GetMoradoresByApartamentoAsync(apartamentoId);
            return Ok(moradores);
        }

        [HttpGet("contato/{contato}")]
        public async Task<IActionResult> GetByContato(string contato)
        {
            var morador = await _moradorService.GetMoradorByContatoAsync(contato);
            if (morador == null)
                return NotFound();
            return Ok(morador);
        }
    }
}