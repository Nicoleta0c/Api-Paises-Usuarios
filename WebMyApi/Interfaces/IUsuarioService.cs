using WebMyApi.Modelos;
using WebMyApi.ObjetosTransferencia;

namespace WebMyApi.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> CrearUsuario(Usuario usuario);
        Task<UsuarioDto> ActualizarUsuario(int id, Usuario usuario);
        Task<bool> EliminarUsuario(int id);
        Task<IEnumerable<UsuarioDto>> ObtenerUsuarios();
        Task<UsuarioDto> ObtenerUsuarioPorId(int id);
        Task<bool> Login(LoginDto loginDto);
    }
}

