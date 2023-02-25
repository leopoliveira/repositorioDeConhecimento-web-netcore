using Microsoft.EntityFrameworkCore;
using RepositorioDeConhecimento.Infrastructure.Context;
using RepositorioDeConhecimento.Infrastructure.Helpers.Exceptions;
using RepositorioDeConhecimento.Models.Domain.Entities;
using RepositorioDeConhecimento.Models.Domain.Repositories;
using System.Linq.Expressions;

namespace RepositorioDeConhecimento.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _context.Set<TEntity>()
                        .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>()
                        .Where(predicate)
                        .ToListAsync();
        }

        public async void Insert(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new SaveChangesException("Erro ao inserir os dados.", ex);
            }  
        }

        public async void InsertRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _context.Set<TEntity>().AddRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new SaveChangesException("Erro ao inserir a lista de dados.", ex);
            }
        }

        public async void Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var exception = new SaveChangesException("Erro ao atualizar os dados.", ex)
                {
                    EntityId = entity.Id,
                    Entity = typeof(TEntity).Name
                };
                throw exception;
            } 
        }

        public async void UpdateRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _context.Set<TEntity>().UpdateRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var exception = new SaveChangesException("Erro ao atualizar a lista de dados.", ex)
                {
                    Entity = typeof(TEntity).Name
                };
                throw exception;
            }
        }

        public async void Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var exception = new SaveChangesException("Erro ao deletar os dados.", ex)
                {
                    EntityId= entity.Id,
                    Entity = typeof(TEntity).Name
                };
                throw exception;
            }
        }

        public async void DeleteRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _context.Set<TEntity>().RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var exception = new SaveChangesException("Erro ao deletar a lista de dados.", ex)
                {
                    Entity = typeof(TEntity).Name
                };
                throw exception;
            }
        }
    }
}
