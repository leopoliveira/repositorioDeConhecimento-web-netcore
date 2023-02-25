using RepositorioDeConhecimento.Infrastructure.Context;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;

namespace RepositorioDeConhecimento.Infrastructure.Repositories
{
    public class ConhecimentoRepository : GenericRepository<Conhecimento>, IConhecimentoRepository
    {
        public ConhecimentoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
