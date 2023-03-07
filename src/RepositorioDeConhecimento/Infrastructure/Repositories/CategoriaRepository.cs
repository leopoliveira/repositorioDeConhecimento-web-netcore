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

        public async Task<ICollection<Categoria>> GetAll()
        {
            return await _context.Categorias.ToListAsync();
        }
    }
}
