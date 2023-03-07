using RepositorioDeConhecimento.Models.Domain.Entities;

namespace RepositorioDeConhecimento.Models.Domain.Repositories
{
    public interface IAutorRepository : IGenericRepository<Autor>
    {
        Task<ICollection<Autor>> GetAll();
    }
}
