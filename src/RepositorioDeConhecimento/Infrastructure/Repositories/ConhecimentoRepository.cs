using Microsoft.EntityFrameworkCore;
using RepositorioDeConhecimento.Infrastructure.Context;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;
using System.Linq.Expressions;

namespace RepositorioDeConhecimento.Infrastructure.Repositories
{
    public class ConhecimentoRepository : GenericRepository<Conhecimento>, IConhecimentoRepository
    {
        private readonly AppDbContext _context;

        public ConhecimentoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Conhecimento>> GetByPages(int page = 1, int offset = 10, int numberOfRecords = 10)
        {
            return await _context.Conhecimentos
                .Include(c => c.Categoria)
                .Include(c => c.Autor)
                .Skip((page - 1) * offset)
                .Take(numberOfRecords)
                .ToListAsync();
        }

        public override async Task<Conhecimento> GetById(int id)
        {
            return await _context.Conhecimentos
                .Include(c => c.Categoria)
                .Include(c => c.Autor)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<IEnumerable<Conhecimento>> GetWhere(Expression<Func<Conhecimento, bool>> predicate)
        {
            return await _context.Conhecimentos
                .Include(c => c.Categoria)
                .Include(c => c.Autor)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
