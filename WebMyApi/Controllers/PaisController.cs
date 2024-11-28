using Microsoft.AspNetCore.Mvc;
using WebMyApi.Interfaces;
using WebMyApi.Modelos;

namespace WebMyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisController : ControllerBase
    {
        private readonly IPaisService _paisService;

        public PaisController(IPaisService paisService)
        {
            _paisService = paisService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPais([FromBody] Pais pais)
        {
            if (pais == null) return BadRequest("El país no puede ser nulo.");
            var nuevoPais = await _paisService.CrearPais(pais);
            return CreatedAtAction(nameof(ObtenerPaisPorId), new { id = nuevoPais.Id }, nuevoPais);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPais(int id, [FromBody] Pais pais)
        {
            if (pais == null) return BadRequest("El país no puede ser nulo.");
            var paisActualizado = await _paisService.ActualizarPais(id, pais);
            if (paisActualizado == null) return NotFound();
            return Ok(paisActualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPais(int id)
        {
            var eliminado = await _paisService.EliminarPais(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPaises()
        {
            var paises = await _paisService.ObtenerPaises();
            return Ok(paises);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPaisPorId(int id)
        {
            var pais = await _paisService.ObtenerPaisPorId(id);
            if (pais == null) return NotFound();
            return Ok(pais);
        }
    }
}