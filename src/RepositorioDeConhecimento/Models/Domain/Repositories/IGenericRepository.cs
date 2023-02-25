using RepositorioDeConhecimento.Models.Domain.Entities;
using System.Linq.Expressions;

namespace RepositorioDeConhecimento.Models.Domain.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(int id);

        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);

        void Insert(TEntity entity);

        public void InsertRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        public void UpdateRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

    }
}
