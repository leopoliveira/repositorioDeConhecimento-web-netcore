using Microsoft.EntityFrameworkCore;
using RepositorioDeConhecimento.Infrastructure.Context;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;

namespace RepositorioDeConhecimento.Infrastructure.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Categoria>> GetByPages(int idUsuario, int page = 1, int offset = 10, int numberOfRecords = 10)
        {
            return await _context.Categorias
                        .Where(c => c.IdUsuario == idUsuario)
                        .OrderBy(c => c.Nome)
                        .Skip((page - 1) * offset)
                        .Take(numberOfRecords)
                        .ToListAsync();
        }

        public async Task<ICollection<Categoria>> GetAll(int idUsuario)
        {
            return await _context.Categorias
                .Where(c => c.IdUsuario == idUsuario)
                .OrderBy(c => c.Nome)
                .ToListAsync();
        }
    }
}
