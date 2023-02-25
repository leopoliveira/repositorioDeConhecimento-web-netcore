using RepositorioDeConhecimento.Infrastructure.Context;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;

namespace RepositorioDeConhecimento.Infrastructure.Repositories
{
    public class ImagemRepository : GenericRepository<Imagem>, IImagemRepository
    {
        public ImagemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
