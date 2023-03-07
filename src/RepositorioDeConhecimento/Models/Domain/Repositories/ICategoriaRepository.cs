using RepositorioDeConhecimento.Models.Domain.Entities;
namespace RepositorioDeConhecimento.Models.Domain.Repositories
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        Task<ICollection<Categoria>> GetAll();
    }
}
