using WebMyApi.Modelos;
using WebMyApi.ObjetosTransferencia;

namespace WebMyApi.Interfaces
{
    public interface IPaisService
    {
        Task<PaisDto> CrearPais(Pais pais);
        Task<PaisDto> ActualizarPais(int id, Pais pais);
        Task<bool> EliminarPais(int id);
        Task<IEnumerable<PaisDto>> ObtenerPaises();
        Task<PaisDto> ObtenerPaisPorId(int id);
    }

}
