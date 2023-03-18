using Microsoft.EntityFrameworkCore;
using RepositorioDeConhecimento.Infrastructure.Context;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;

namespace RepositorioDeConhecimento.Infrastructure.Repositories
{
    public class AutorRepository : GenericRepository<Autor>, IAutorRepository
    {
        private readonly AppDbContext _context;

        public AutorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Autor>> GetByPages(int idUsuario, int page = 1, int offset = 10, int numberOfRecords = 10)
        {
            return await _context.Autores
                        .Where(a => a.IdUsuario == idUsuario)
                        .OrderBy(a => a.Nome)
                        .Skip((page - 1) * offset)
                        .Take(numberOfRecords)
                        .ToListAsync();
        }

        public async Task<ICollection<Autor>> GetAll(int idUsuario)
        {
            return await _context.Autores
                .Where(a => a.IdUsuario == idUsuario)
                .OrderBy(a => a.Nome)
                .ToListAsync();
        }
    }
}
