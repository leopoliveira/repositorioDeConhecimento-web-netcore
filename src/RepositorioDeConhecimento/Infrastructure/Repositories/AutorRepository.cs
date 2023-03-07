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

        public async Task<ICollection<Autor>> GetAll()
        {
            return await _context.Autores.ToListAsync();
        }
    }
}
