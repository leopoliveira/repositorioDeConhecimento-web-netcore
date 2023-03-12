using RepositorioDeConhecimento.Models.Domain.Entities;
using System.Linq.Expressions;

namespace RepositorioDeConhecimento.Models.Domain.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(int id);

        Task<IEnumerable<TEntity>> GetByPages(int page = 1, int offset = 10, int numberOfRecords = 10);

        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountRecords();

        Task Insert(TEntity entity);

        Task InsertRange(IEnumerable<TEntity> entities);

        Task Update(TEntity entity);

        Task UpdateRange(IEnumerable<TEntity> entities);

        Task Delete(TEntity entity);

        Task DeleteRange(IEnumerable<TEntity> entities);

    }
}
