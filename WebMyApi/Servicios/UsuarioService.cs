using Microsoft.EntityFrameworkCore;
using WebMyApi.Data;
using WebMyApi.Interfaces;
using WebMyApi.Modelos;
using WebMyApi.ObjetosTransferencia;


namespace WebMyApi.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApiDbContext _context; 

        public UsuarioService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDto> CrearUsuario(Usuario usuario)
        {
            _context.Set<Usuario>().Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Estatus = usuario.Estatus
            };
        }

        public async Task<UsuarioDto> ActualizarUsuario(int id, Usuario usuario)
        {
            var usuarioExistente = await _context.Set<Usuario>().FindAsync(id);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nombre = usuario.Nombre ?? usuarioExistente.Nombre;
                usuarioExistente.Correo = usuario.Correo ?? usuarioExistente.Correo;
                usuarioExistente.Clave = usuario.Clave ?? usuarioExistente.Clave;
                usuarioExistente.Estatus = usuario.Estatus;

                await _context.SaveChangesAsync();

                return new UsuarioDto
                {
                    Id = usuarioExistente.Id,
                    Nombre = usuarioExistente.Nombre,
                    Correo = usuarioExistente.Correo,
                    Estatus = usuarioExistente.Estatus
                };
            }
            return null; 
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            var usuario = await _context.Set<Usuario>().FindAsync(id);
            if (usuario != null)
            {
                _context.Set<Usuario>().Remove(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; 
        }

        public async Task<IEnumerable<UsuarioDto>> ObtenerUsuarios()
        {
            return await _context.Set<Usuario>()
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Correo = u.Correo,
                    Estatus = u.Estatus
                }).ToListAsync();
        }

        public async Task<UsuarioDto> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _context.Set<Usuario>().FindAsync(id);
            if (usuario != null)
            {
                return new UsuarioDto
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Correo = usuario.Correo,
                    Estatus = usuario.Estatus
                };
            }
            return null;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            var usuario = await _context.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.Correo == loginDto.Correo && u.Clave == loginDto.Clave);
            return usuario != null;
        }
    }
}
