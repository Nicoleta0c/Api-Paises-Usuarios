using Microsoft.AspNetCore.Mvc;
using WebMyApi.Interfaces;
using WebMyApi.Modelos;
using WebMyApi.ObjetosTransferencia;

namespace WebMyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<UsuarioDto>> CrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrEmpty(usuario.Clave))
            {
                return BadRequest("los datos son invalidos");
            }

            var usuarioCreado = await _usuarioService.CrearUsuario(usuario);
            return CreatedAtAction(nameof(ObtenerUsuarioPorId), new { id = usuarioCreado.Id }, usuarioCreado);
        }

        [HttpPut("actualizar/{id}")]
        public async Task<ActionResult<UsuarioDto>> ActualizarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Correo) || string.IsNullOrEmpty(usuario.Clave))
            {
                return BadRequest("los datos son invalidos");
            }

            var usuarioActualizado = await _usuarioService.ActualizarUsuario(id, usuario);
            return usuarioActualizado != null ? Ok(usuarioActualizado) : NotFound();
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            var eliminado = await _usuarioService.EliminarUsuario(id);
            return eliminado ? Ok() : NotFound();
        }

        [HttpGet("obtener")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> ObtenerUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("obtener/{id}")]
        public async Task<ActionResult<UsuarioDto>> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorId(id);
            return usuario != null ? Ok(usuario) : NotFound();
        }

        [HttpPost("login-usuarios")]
        public async Task<ActionResult<bool>> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Correo) || string.IsNullOrEmpty(loginDto.Clave))
            {
                return BadRequest("los datos son invalidos");
            }

            var loginExitoso = await _usuarioService.Login(loginDto);
            if (!loginExitoso)
            {
                return Unauthorized("los datos son invalidos");
            }
            return Ok(loginExitoso);
        }
    }
}
