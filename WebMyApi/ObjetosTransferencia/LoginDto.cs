using WebMyApi.Modelos;

namespace WebMyApi.ObjetosTransferencia
{
    public class LoginDto
    {
        public string Correo { get; set; }
        public string Clave { get; set; }
    }

    public class AuthService 
    {
        private readonly List<Usuario> _usuarios; 

        public AuthService(List<Usuario> usuarios) 
        {
            _usuarios = usuarios;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Correo == loginDto.Correo && u.Clave == loginDto.Clave);
            return await Task.FromResult(usuario != null);
        }
    }
}

