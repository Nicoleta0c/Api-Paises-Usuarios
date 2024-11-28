using Microsoft.EntityFrameworkCore;
using WebMyApi.Data;
using WebMyApi.Interfaces;
using WebMyApi.Modelos;
using WebMyApi.ObjetosTransferencia;

namespace WebMyApi.Servicios
{
    public class PaisService : IPaisService
    {
        private readonly ApiDbContext _context;

        public PaisService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<PaisDto> CrearPais(Pais pais)
        {
            if (pais == null) throw new ArgumentNullException(nameof(pais));

            _context.Paises.Add(pais);
            await _context.SaveChangesAsync();
            return new PaisDto { Id = pais.Id, Nombre = pais.Nombre, Gentilicio = pais.Gentilicio, Capital = pais.Capital, Estatus = pais.Estatus };
        }

        public async Task<PaisDto> ActualizarPais(int id, Pais pais)
        {
            if (pais == null) throw new ArgumentNullException(nameof(pais));

            var paisExistente = await _context.Paises.FindAsync(id);
            if (paisExistente == null) return null;

            paisExistente.Nombre = pais.Nombre;
            paisExistente.Gentilicio = pais.Gentilicio;
            paisExistente.Capital = pais.Capital;
            paisExistente.Estatus = pais.Estatus;

            await _context.SaveChangesAsync();
            return new PaisDto { Id = paisExistente.Id, Nombre = paisExistente.Nombre, Gentilicio = paisExistente.Gentilicio, Capital = paisExistente.Capital, Estatus = paisExistente.Estatus };
        }

        public async Task<bool> EliminarPais(int id)
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais == null) return false;

            _context.Paises.Remove(pais);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PaisDto>> ObtenerPaises()
        {
            return await _context.Paises
                .Select(p => new PaisDto { Id = p.Id, Nombre = p.Nombre, Gentilicio = p.Gentilicio, Capital = p.Capital, Estatus = p.Estatus })
                .ToListAsync();
        }

        public async Task<PaisDto> ObtenerPaisPorId(int id)
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais == null) return null;

            return new PaisDto { Id = pais.Id, Nombre = pais.Nombre, Gentilicio = pais.Gentilicio, Capital = pais.Capital, Estatus = pais.Estatus };
        }
    }
}