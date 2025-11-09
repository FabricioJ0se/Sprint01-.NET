using Microsoft.AspNetCore.Mvc;
using PortariaLight.Application.Services;
using PortariaLight.Domain.Entities;

namespace PortariaLight.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortariaController : ControllerBase
    {
        private readonly IPortariaService _portariaService;

        public PortariaController(IPortariaService portariaService)
        {
            _portariaService = portariaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var portarias = await _portariaService.GetAllPortariasAsync();
            return Ok(portarias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var portaria = await _portariaService.GetPortariaByIdAsync(id);
            if (portaria == null)
                return NotFound();
            return Ok(portaria);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Portaria portaria)
        {
            var created = await _portariaService.CreatePortariaAsync(portaria);
            return CreatedAtAction(nameof(GetById), new { id = created.IdPortaria }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Portaria portaria)
        {
            if (id != portaria.IdPortaria)
                return BadRequest();

            var updated = await _portariaService.UpdatePortariaAsync(portaria);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _portariaService.DeletePortariaAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}